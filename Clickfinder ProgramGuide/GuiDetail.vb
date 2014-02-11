Imports MediaPortal.GUI.Library
Imports TvDatabase
Imports ClickfinderProgramGuide.TvDatabase
Imports MediaPortal.Configuration
Imports MediaPortal.Util
Imports Gentle.Framework
Imports enrichEPG.TvDatabase
Imports enrichEPG.Database
Imports Layout = MediaPortal.GUI.Library.GUIFacadeControl.Layout
Imports System.Windows.Forms
Imports MediaPortal.Dialogs
Imports MediaPortal.Player
Imports System.Threading


Namespace ClickfinderProgramGuide
    Public Class DetailGuiWindow
        Inherits GUIWindow

#Region "Skin Controls"

        'Buttons



        <SkinControlAttribute(5)> Protected _btnAllMovies As GUIButtonControl = Nothing

        <SkinControlAttribute(10)> Protected _Coverflow As GUICoverFlow = Nothing

#End Region

#Region "Members"
        Friend Shared _DetailTvMovieProgram As TVMovieProgram
        Public Shared _DetailGuiWindowList As New List(Of GUIListItem)

#End Region

#Region "Constructors"
        Public Sub New()
            _
        End Sub
#End Region

#Region "GUI Properties"
        Public Overloads Overrides Property GetID() As Integer
            Get
                Return 1656544652
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property


        Public Overloads Overrides Function Init() As Boolean
            'Beim initialisieren des Plugin den Screen laden

            Return Load(GUIGraphicsContext.Skin + "\ClickfinderProgramGuideDetail.xml")
        End Function

        Public Overrides Function GetModuleName() As String
            Return "Clickfinder ProgramGuide Details"
        End Function
#End Region

#Region "GUI Events"

        Protected Overrides Sub OnPageLoad()
            Try
                MyBase.OnPageLoad()
                GUIWindowManager.NeedRefresh()

                Helper.CheckConnectionState()

                GuiLayout.SetSettingLastUpdateProperty()


                MyLog.Info("")
                MyLog.Info("")
                MyLog.Info("[DetailGuiWindow] -------------[OPEN]-------------")
                MyLog.Info("[DetailGuiWindow]: [HyperlinkParameter]: {0}", _loadParameter)

                _Coverflow.Clear()
                _Coverflow.Visible = False

                If String.IsNullOrEmpty(_loadParameter) = False Then
                    If InStr(_loadParameter, "TITLE: ") > 0 Then
                        'Hyperlink Parameter: Title, Channel, Start, Stop
                        Dim _parametersList As New ArrayList(Split(_loadParameter, "|"))
                        Dim _title As String = String.Empty
                        Dim _channel As String = String.Empty
                        Dim _start As DateTime
                        Dim _end As DateTime

          

                        _DetailGuiWindowList.Clear()
                        

                        For Each _parameter In _parametersList
                            Try
                                If InStr(_parameter, "TITLE: ") > 0 Then _title = Replace(_parameter, "TITLE: ", "")
                                If InStr(_parameter, "CHANNEL: ") > 0 Then _channel = Replace(_parameter, "CHANNEL: ", "")
                                If InStr(_parameter, "START: ") > 0 Then _start = DateTime.Parse(Replace(_parameter, "START: ", ""))
                                If InStr(_parameter, "STOP: ") > 0 Then _end = DateTime.Parse(Replace(_parameter, "STOP: ", ""))
                            Catch ex As Exception
                                MyLog.Error("[DetailGuiWindow]: [OnPageLoad]: parameter = {0}", _parameter)
                                MyLog.Error("[DetailGuiWindow]: [OnPageLoad]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
                            End Try
                        Next

                        Translator.SetProperty("#DetailCoverflowLabel", "jetzt und gleich")
                        Translator.SetProperty("#DetailCoverflowLabel2", _channel)

                        'Now program laden
                        Dim _SQLstring As String = "Select * from program INNER JOIN channel ON program.idChannel = channel.idChannel " & _
                                                    "WHERE title LIKE '" & MyTvSeries.Helper.allowedSigns(_title) & "' " & _
                                                    "AND displayName = '" & _channel & "' " & _
                                                    "AND startTime > " & Helper.MySqlDate(_start.AddMinutes(-1)) & " " & _
                                                    "AND startTime < " & Helper.MySqlDate(_end)



                        _SQLstring = Replace(_SQLstring, " * ", " Program.IdProgram, Program.Classification, Program.Description, Program.EndTime, Program.EpisodeName, Program.EpisodeNum, Program.EpisodePart, Program.Genre, Program.IdChannel, Program.OriginalAirDate, Program.ParentalRating, Program.SeriesNum, Program.StarRating, Program.StartTime, Program.state, Program.Title ")
                        Dim _SQLstate As SqlStatement = Broker.GetStatement(_SQLstring)
                        Dim _Result As List(Of Program) = ObjectFactory.GetCollection(GetType(Program), _SQLstate.Execute())

                        If _Result.Count > 0 Then
                            Try
                                _DetailTvMovieProgram = TVMovieProgram.Retrieve(_Result(0).IdProgram)

                                'Now an Coverflow
                                Dim _item As New GUIListItem
                                _item.ItemId = _DetailTvMovieProgram.idProgram
                                _item.ThumbnailImage = GuiLayout.Image(_DetailTvMovieProgram)

                                _DetailGuiWindowList.Add(_item)

                                _item.Dispose()

                                'next an CoverFlow
                                _SQLstring = "Select * from program INNER JOIN channel ON program.idChannel = channel.idChannel " & _
                                                            "WHERE displayName = '" & _channel & "' " & _
                                                            "AND startTime > " & Helper.MySqlDate(_end.AddMinutes(-1)) & " " & _
                                                            "Order BY startTime"

                                _SQLstring = Helper.AppendSqlLimit(_SQLstring, 3)


                                _SQLstring = Replace(_SQLstring, " * ", " Program.IdProgram, Program.Classification, Program.Description, Program.EndTime, Program.EpisodeName, Program.EpisodeNum, Program.EpisodePart, Program.Genre, Program.IdChannel, Program.OriginalAirDate, Program.ParentalRating, Program.SeriesNum, Program.StarRating, Program.StartTime, Program.state, Program.Title ")
                                Dim _SQLstate2 As SqlStatement = Broker.GetStatement(_SQLstring)
                                Dim _ResultNext As List(Of Program) = ObjectFactory.GetCollection(GetType(Program), _SQLstate2.Execute())

                                For Each _program In _ResultNext
                                    Dim _itemNext As New GUIListItem
                                    _itemNext.ItemId = _program.IdProgram
                                    _itemNext.ThumbnailImage = GuiLayout.Image(TVMovieProgram.Retrieve(_program.IdProgram))

                                    _DetailGuiWindowList.Add(_itemNext)

                                    _item.Dispose()
                                Next

                                ShowCPGDetails()

                            Catch ex As Exception
                                ClearAllProperties()
                                _DetailGuiWindowList.Clear()
                                Helper.Notify("program nicht in TvMovieProgram!")
                            End Try
                        Else
                            ClearAllProperties()
                            _DetailGuiWindowList.Clear()
                            Helper.Notify("nichts gefunden!")
                        End If

                    ElseIf InStr(_loadParameter, "idProgram: ") > 0 Then
                        'Hyperlink Parameter: idProgram
                        _DetailTvMovieProgram = TVMovieProgram.Retrieve(CInt(Replace(_loadParameter, "idProgram: ", String.Empty)))
                        ClearAllProperties()
                        ShowCPGDetails()
                    ElseIf InStr(_loadParameter, "list: ") > 0 Then
                        'mit Coverflow aufrufen
                        _DetailTvMovieProgram = TVMovieProgram.Retrieve(CInt(Replace(_loadParameter, "list: ", String.Empty)))
                        ClearAllProperties()
                        ShowCPGDetails()
                    End If
                Else
                    'ohne Parameter - also zurück zu Details
                    ClearAllProperties()
                    ShowCPGDetails()
                End If

                'CoverFlow füllen
                If _DetailGuiWindowList.Count > 0 Then
                    For Each _Item In _DetailGuiWindowList
                        _Coverflow.Add(_Item)
                    Next

                    _Coverflow.Visible = True
                End If

                Dim _FocusedItemIndex As Integer = _DetailGuiWindowList.FindIndex(Function(x) x.ItemId = _DetailTvMovieProgram.idProgram)
                GUICoverFlow.SelectItemControl(GetID, _Coverflow.GetID, _FocusedItemIndex)

            Catch ex As Exception
                MyLog.Error("[DetailGuiWindow]: [OnPageLoad]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try
        End Sub

        Protected Overrides Sub OnPageDestroy(ByVal new_windowId As Integer)

            Dispose()
            AllocResources()

            MyBase.OnPageDestroy(new_windowId)
        End Sub

        Public Overrides Sub OnAction(ByVal action As MediaPortal.GUI.Library.Action)
            MyBase.OnAction(action)

            If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MOVE_LEFT Or _
            action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MOVE_RIGHT Or _
            action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_PAGE_UP Or _
            action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_PAGE_DOWN Or _
             action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_END Or _
             action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_HOME Then
                If _Coverflow.IsFocused = True Then

                    _DetailTvMovieProgram = TVMovieProgram.Retrieve(CInt(_Coverflow(_Coverflow.SelectedListItemIndex).ItemId))

                    Dim _tmp As New Threading.Thread(AddressOf ShowCPGDetails)
                    _tmp.Start()

                End If
            End If



            'If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MOVE_RIGHT Then
            '    If _Coverflow.IsFocused = True Then
            '        _DetailTvMovieProgram = TVMovieProgram.Retrieve(CInt(_Coverflow(_Coverflow.SelectedListItemIndex).ItemId))

            '        Dim _tmp As New Threading.Thread(AddressOf ShowCPGDetails)
            '        _tmp.Start()

            '    End If
            'End If

            'If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_PAGE_UP Then
            '    If _Coverflow.IsFocused = True Then
            '        _DetailTvMovieProgram = TVMovieProgram.Retrieve(CInt(_Coverflow(_Coverflow.SelectedListItemIndex).ItemId))

            '        Dim _tmp As New Threading.Thread(AddressOf ShowCPGDetails)
            '        _tmp.Start()

            '    End If
            'End If

            'If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_PAGE_DOWN Then
            '    If _Coverflow.IsFocused = True Then
            '        _DetailTvMovieProgram = TVMovieProgram.Retrieve(CInt(_Coverflow(_Coverflow.SelectedListItemIndex).ItemId))

            '        Dim _tmp As New Threading.Thread(AddressOf ShowCPGDetails)
            '        _tmp.Start()

            '    End If
            'End If

            'Play Button (P) -> Start channel
            If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MUSIC_PLAY Then
                Try
                    MyLog.[Debug]("[DetailGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)
                    Helper.StartTv(_DetailTvMovieProgram.ReferencedProgram)
                Catch ex As Exception
                    MyLog.Error("[Play Button]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
                End Try
            End If

            'Record Button (R) -> MP TvProgramInfo aufrufen --Über keychar--
            If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_KEY_PRESSED Then
                If action.m_key IsNot Nothing Then
                    If action.m_key.KeyChar = 114 Then
                        MyLog.[Debug]("[DetailGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)

                        Helper.LoadTVProgramInfo(_DetailTvMovieProgram.ReferencedProgram)
                    End If
                End If
            End If



        End Sub

        Protected Overrides Sub OnClicked(ByVal controlId As Integer, _
                                  ByVal control As GUIControl, _
                                  ByVal actionType As  _
                                  MediaPortal.GUI.Library.Action.ActionType)

            MyBase.OnClicked(controlId, control, actionType)




            If control Is _btnAllMovies Then
                GuiButtons.AllMovies()
            End If

            'If control Is _btnRecord Then
            '    Helper.LoadTVProgramInfo(_DetailTvMovieProgram.ReferencedProgram)
            'End If

            'If control Is _btnRemember Then
            '    Helper.SetNotify(_DetailTvMovieProgram.ReferencedProgram)
            'End If

            'If control Is _btnPlay Then
            '    Helper.StartTv(_DetailTvMovieProgram.ReferencedProgram)
            'End If

            'If control Is _btnBack Then
            '    GUIWindowManager.ShowPreviousWindow()
            'End If

            'If control Is _btnJumpTo Then
            '    Select Case GUIPropertyManager.GetProperty("#DetailJumpToLabel")
            '        Case Is = Translation.MovingPictures
            '            GUIWindowManager.ActivateWindow(96742, "movieid:" & GUIPropertyManager.GetProperty("#DetailMovPicID"))
            '        Case Is = Translation.MPtvSeries
            '            GUIWindowManager.ActivateWindow(9811, "seriesid:" & _
            '                                            GUIPropertyManager.GetProperty("#DetailSeriesID") & _
            '                                            "|seasonidx:" & GUIPropertyManager.GetProperty("#Detailseasonidx") & _
            '                                            "|episodeidx:" & GUIPropertyManager.GetProperty("#Detailepisodeidx"))
            '    End Select
            'End If

            If control Is _Coverflow Then
                If actionType = MediaPortal.GUI.Library.Action.ActionType.ACTION_SELECT_ITEM Then
                    ShowMenu(TVMovieProgram.Retrieve(_Coverflow.SelectedListItem.ItemId))
                End If
            End If

        End Sub

        Private Function getRatingpercentage(ByVal TvMovieRating As Integer) As Integer
            Select Case TvMovieRating
                Case Is = 0
                    Return 0
                Case Is = 1
                    Return 5
                Case Is = 2
                    Return 8
                Case Is = 3
                    Return 10
            End Select

        End Function

        Private Sub ShowMenu(ByVal TVMovieProgram As TVMovieProgram)
            Dim dlgContext As GUIDialogSelect2 = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_SELECT2, Integer)), GUIDialogSelect2)
            dlgContext.Reset()
            dlgContext.SetHeading(TVMovieProgram.ReferencedProgram.Title)

            If PluginManager.IsWindowPlugInEnabled("OnlineVideos") Then
                'Trailer
                Dim lItemTrailert As New GUIListItem
                lItemTrailert.Label = Translation.IMDB
                dlgContext.Add(lItemTrailert)
                lItemTrailert.Dispose()
            End If

            'Erinnern
            Dim lItemRem As New GUIListItem
            lItemRem.Label = Translation.Remember
            dlgContext.Add(lItemRem)
            lItemRem.Dispose()

            If PluginManager.IsWindowPlugInEnabled("MP-TV Series") And Not GUIPropertyManager.GetProperty("#DetailSeriesID") = 0 Then
                'TvSeries
                Dim lItemSeries As New GUIListItem
                lItemSeries.Label = Translation.MPtvSeries
                dlgContext.Add(lItemSeries)
                lItemSeries.Dispose()

                'Neue Episoden
                Dim lItemNewEpisodes As New GUIListItem
                lItemNewEpisodes.Label = Translation.allNewEpisodes
                dlgContext.Add(lItemNewEpisodes)
                lItemNewEpisodes.Dispose()
            End If

            If PluginManager.IsWindowPlugInEnabled("Moving Pictures") And Not GUIPropertyManager.GetProperty("#DetailMovPicID") = 0 Then
                'Mov.Pic
                Dim lItemMovPic As New GUIListItem
                lItemMovPic.Label = Translation.MovingPictures
                dlgContext.Add(lItemMovPic)
                lItemMovPic.Dispose()
            End If

            'Kanal einschalten
            Dim lItemOn As New GUIListItem
            lItemOn.Label = Translation.ChannelON
            dlgContext.Add(lItemOn)
            lItemOn.Dispose()

            'Aufnehmen
            Dim lItemRec As New GUIListItem
            lItemRec.Label = Translation.Record
            dlgContext.Add(lItemRec)
            lItemRec.Dispose()

            If PluginManager.IsPlugInEnabled("TvWishListMP") Then
                'TvWishList
                Dim lItemTvWishList As New GUIListItem
                lItemTvWishList.Label = Translation.TvWishListMP
                dlgContext.Add(lItemTvWishList)
                lItemTvWishList.Dispose()
            End If

            'mit Serie verlinken
            Dim lItemSerieLink As New GUIListItem
            lItemSerieLink.Label = Translation.SerieLinkLabel
            dlgContext.Add(lItemSerieLink)
            lItemSerieLink.Dispose()

            'mit DB aktualisieren
            Dim lItemDBUpdate As New GUIListItem
            lItemDBUpdate.Label = Translation.DBRefresh
            dlgContext.Add(lItemDBUpdate)
            lItemDBUpdate.Dispose()

            dlgContext.DoModal(GetID)

            Select Case dlgContext.SelectedLabelText
                Case Translation.ChannelON
                    Helper.StartTv(TVMovieProgram.ReferencedProgram)
                    MyLog.Debug("[ShowMenu]: selected -> start tv (channel)")
                    MyLog.Debug("")
                Case Translation.Record
                    Helper.LoadTVProgramInfo(TVMovieProgram.ReferencedProgram)
                    MyLog.Debug("[ShowMenu]: selected -> open TvProgramInfo Gui")
                    MyLog.Debug("")
                Case Translation.Remember
                    Helper.SetNotify(TVMovieProgram.ReferencedProgram)
                    MyLog.Debug("[ShowMenu]: selected -> set notify")
                    MyLog.Debug("")
                Case Translation.TvWishListMP
                    GUIWindowManager.ActivateWindow(70943675, String.Format("eq(#currentmoduleid,'1656544652'), 'NEWTVWISH//TITLE={0}", TVMovieProgram.ReferencedProgram.Title))
                    MyLog.Debug("[ShowMenu]: selected -> TvWishListMP")
                    MyLog.Debug("")
                Case Translation.IMDB
                    GUIWindowManager.ActivateWindow(4755, String.Format("site:IMDb Movie Trailers|search:{0}|return:Locked", TVMovieProgram.ReferencedProgram.Title))
                    MyLog.Debug("[ShowMenu]: selected -> OnlineVideos")
                    MyLog.Debug("")
                Case Translation.MPtvSeries
                    GUIWindowManager.ActivateWindow(9811, "seriesid:" & _
                                    GUIPropertyManager.GetProperty("#DetailSeriesID") & _
                                    "|seasonidx:" & GUIPropertyManager.GetProperty("#Detailseasonidx") & _
                                    "|episodeidx:" & GUIPropertyManager.GetProperty("#Detailepisodeidx"))
                    MyLog.Debug("[ShowMenu]: selected -> MP-TvSeries")
                    MyLog.Debug("")
                Case Translation.allNewEpisodes
                    GUIWindowManager.ActivateWindow(1656544657, String.Format("idSeries: {0}", TVMovieProgram.idSeries))
                    MyLog.Debug("[ShowMenu]: selected -> GuiEpisodePreview")
                    MyLog.Debug("")
                Case Translation.MovingPictures
                    GUIWindowManager.ActivateWindow(96742, "movieid:" & GUIPropertyManager.GetProperty("#DetailMovPicID"))
                    MyLog.Debug("[ShowMenu]: selected -> MovingPictures")
                    MyLog.Debug("")
                Case Translation.SerieLinkLabel
                    Helper.ShowSerieLinkContextMenu(TVMovieProgram.ReferencedProgram)
                Case Translation.DBRefresh
                    If Helper._DbAbgleichRuning = True Then

                        Helper.Notify(Translation.DBRefreshRunning)
                    Else
                        Dim tmp As New Thread(AddressOf Helper.startEnrichEPG)
                        tmp.IsBackground = True
                        tmp.Start()
                    End If
            End Select


        End Sub


#End Region

#Region "Functions"

        Private Sub showEPGDetail()
            Try


            Catch ex As Exception
                MyLog.Error("[DetailGuiWindow]: [ShowEPGDetail]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try
        End Sub
        Private Sub ShowCPGDetails()

            Try
                Dim _recordingStatus As String = String.Empty

                Translator.SetProperty("#DetailSource", "Clickfinder")

                'Falls TvSeries
                If _DetailTvMovieProgram.idSeries > 0 Then
                    Helper.CheckSeriesLocalStatus(_DetailTvMovieProgram)

                    Select Case CPGsettings.DetailSeriesImage
                        Case Is = "Cover"
                            Translator.SetProperty("#DetailImage", Config.GetFile(Config.Dir.Thumbs, "MPTVSeriesBanners\") & _DetailTvMovieProgram.SeriesPosterImage)
                        Case Is = "FanArt"
                            Translator.SetProperty("#DetailImage", Config.GetFile(Config.Dir.Thumbs, "") & _DetailTvMovieProgram.FanArt)
                        Case Is = "Episode"
                            Translator.SetProperty("#DetailImage", Config.GetFile(Config.Dir.Thumbs, "MPTVSeriesBanners\") & _DetailTvMovieProgram.EpisodeImage)
                        Case Is = "TvMovie"
                            If Not String.IsNullOrEmpty(_DetailTvMovieProgram.BildDateiname) Then
                                Translator.SetProperty("#DetailImage", CPGsettings.ClickfinderImagePath & "\" & _DetailTvMovieProgram.BildDateiname)
                            Else
                                Translator.SetProperty("#DetailImage", Config.GetFile(Config.Dir.Thumbs, "tv\logos\") & Replace(_DetailTvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName, "/", "_") & ".png")
                            End If
                    End Select

                Else
                    Translator.SetProperty("#DetailImage", GuiLayout.Image(_DetailTvMovieProgram))
                End If


                If Not GUIPropertyManager.GetProperty("#DetailImage") = Config.GetFile(Config.Dir.Thumbs, "tv\logos\") & Replace(_DetailTvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName, "/", "_") & ".png" Then
                    Translator.SetProperty("#DetailChannelLogo", Config.GetFile(Config.Dir.Thumbs, "tv\logos\") & Replace(_DetailTvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName, "/", "_") & ".png")
                Else
                    Translator.SetProperty("#DetailChannelLogo", String.Empty)
                End If

                Translator.SetProperty("#DetailTitle", _DetailTvMovieProgram.ReferencedProgram.Title)

                Translator.SetProperty("#DetailorgTitle", GuiLayout.DetailOrgTitle(_DetailTvMovieProgram))

                Translator.SetProperty("#DetailTvMovieStar", GuiLayout.TvMovieStar(_DetailTvMovieProgram))
                Translator.SetProperty("#DetailRatingStar", GuiLayout.ratingStar(_DetailTvMovieProgram.ReferencedProgram))

                _recordingStatus = GuiLayout.RecordingStatus(_DetailTvMovieProgram.ReferencedProgram)

                Translator.SetProperty("#DetailTime", GuiLayout.TimeLabel(_DetailTvMovieProgram))
                Translator.SetProperty("#DetailDuration", DateDiff(DateInterval.Minute, _DetailTvMovieProgram.ReferencedProgram.StartTime, _DetailTvMovieProgram.ReferencedProgram.EndTime) & " " & Translation.MinuteLabel)
                Translator.SetProperty("#DetailChannel", _DetailTvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName)
                Translator.SetProperty("#DetailGenre", _DetailTvMovieProgram.ReferencedProgram.Genre)

                Translator.SetProperty("#DetailRegie", _DetailTvMovieProgram.Regie)
                Translator.SetProperty("#DetailActors", Replace(_DetailTvMovieProgram.Actors, ";", ", "))
                Translator.SetProperty("#DetailKritik", _DetailTvMovieProgram.KurzKritik)
                Translator.SetProperty("#DetailDescription", _DetailTvMovieProgram.Describtion)


                If _DetailTvMovieProgram.idSeries > 0 Then
                    Translator.SetProperty("#DetailSeriesID", _DetailTvMovieProgram.idSeries)
                    Translator.SetProperty("#Detailseasonidx", _DetailTvMovieProgram.ReferencedProgram.SeriesNum)
                    Translator.SetProperty("#Detailepisodeidx", _DetailTvMovieProgram.ReferencedProgram.EpisodeNum)
                    Translator.SetProperty("#DetailJumpToLabel", Translation.MPtvSeries)

                    'schauen ob episode an anderem Tag aufgenommen wird
                    If _recordingStatus = String.Empty Then
                        Dim _RecordingList As New ArrayList
                        Dim SQLstring As String = String.Empty

                        SQLstring = "Select program.idprogram from program " & _
                                "WHERE title LIKE '" & MyTvSeries.Helper.allowedSigns(_DetailTvMovieProgram.ReferencedProgram.Title) & "' " & _
                                "AND episodeName LIKE '" & MyTvSeries.Helper.allowedSigns(_DetailTvMovieProgram.ReferencedProgram.EpisodeName) & "' " & _
                                "Order BY state DESC"

                        SQLstring = Helper.AppendSqlLimit(SQLstring, 1)

                        _RecordingList.AddRange(Broker.Execute(SQLstring).TransposeToFieldList("idProgram", True))

                        If _RecordingList.Count > 0 Then
                            Dim _Record As Program = Program.Retrieve(_RecordingList.Item(0))
                            _recordingStatus = GuiLayout.RecordingStatus(_Record)

                            Select Case (_recordingStatus)
                                Case Is = "tvguide_record_button.png"
                                    _recordingStatus = "ClickfinderPG_recOnce.png"
                                Case Is = "tvguide_recordserie_button.png"
                                    _recordingStatus = "ClickfinderPG_recSeries.png"
                            End Select
                        End If

                    End If

                Else
                    Translator.SetProperty("#DetailSeriesID", 0)
                    Translator.SetProperty("#DetailEpisodeID", 0)
                    If _DetailTvMovieProgram.idMovingPictures > 0 Then
                        Translator.SetProperty("#DetailMovPicID", _DetailTvMovieProgram.idMovingPictures)
                        Translator.SetProperty("#DetailJumpToLabel", Translation.MovingPictures)
                    Else
                        Translator.SetProperty("#DetailMovPicID", 0)
                    End If
                End If

                Translator.SetProperty("#DetailRecordingState", _recordingStatus)

                If Not _DetailTvMovieProgram.ReferencedProgram.OriginalAirDate < New Date(1900, 1, 1) Then
                    Translator.SetProperty("#DetailYear", _DetailTvMovieProgram.ReferencedProgram.OriginalAirDate.Year & " ")
                Else
                    Translator.SetProperty("#DetailYear", "")
                End If

                Translator.SetProperty("#DetailCountry", _DetailTvMovieProgram.Country)

                If _DetailTvMovieProgram.Dolby = True Then
                    Translator.SetProperty("#DetailAudioImage", "Logos\ClickfinderPG\dolby.png")
                Else
                    Translator.SetProperty("#DetailAudioImage", "Logos\ClickfinderPG\stereo.png")
                End If

                If _DetailTvMovieProgram.HDTV = True Then
                    Translator.SetProperty("#DetailProgramFormat", "Logos\ClickfinderPG\hd.png")
                Else
                    Translator.SetProperty("#DetailProgramFormat", "Logos\ClickfinderPG\sd.png")
                End If

                Translator.SetProperty("#DetailFSK", GuiLayout.DetailFSK(_DetailTvMovieProgram))

                If Not String.IsNullOrEmpty(_DetailTvMovieProgram.FanArt) Then
                    Translator.SetProperty("#DetailFanArt", Config.GetFile(Config.Dir.Thumbs, "") & _DetailTvMovieProgram.FanArt)
                Else
                    Translator.SetProperty("#DetailFanArt", "")
                End If

                Translator.SetProperty("#DetailRatingFun", getRatingpercentage(_DetailTvMovieProgram.Fun))
                Translator.SetProperty("#DetailRatingAction", getRatingpercentage(_DetailTvMovieProgram.Action))
                Translator.SetProperty("#DetailRatingErotic", getRatingpercentage(_DetailTvMovieProgram.Erotic))
                Translator.SetProperty("#DetailRatingSuspense", getRatingpercentage(_DetailTvMovieProgram.Tension))
                Translator.SetProperty("#DetailRatingLevel", getRatingpercentage(_DetailTvMovieProgram.Requirement))
                Translator.SetProperty("#DetailRatingEmotions", getRatingpercentage(_DetailTvMovieProgram.Feelings))

            Catch ex As Exception
                MyLog.Error("[DetailGuiWindow]: [ShowCPGDetails]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try
        End Sub

        Private Sub ClearAllProperties()
            Translator.SetProperty("#DetailImage", String.Empty)
            Translator.SetProperty("#DetailChannelLogo", String.Empty)
            Translator.SetProperty("#DetailTitle", String.Empty)
            Translator.SetProperty("#DetailorgTitle", String.Empty)
            Translator.SetProperty("#DetailTvMovieStar", String.Empty)
            Translator.SetProperty("#DetailRatingStar", 0)
            Translator.SetProperty("#DetailTime", String.Empty)
            Translator.SetProperty("#DetailDuration", String.Empty)
            Translator.SetProperty("#DetailChannel", String.Empty)
            Translator.SetProperty("#DetailGenre", String.Empty)

            Translator.SetProperty("#DetailRegie", String.Empty)
            Translator.SetProperty("#DetailActors", String.Empty)
            Translator.SetProperty("#DetailKritik", String.Empty)
            Translator.SetProperty("#DetailDescription", String.Empty)

            Translator.SetProperty("#DetailYear", String.Empty)
            Translator.SetProperty("#DetailCountry", String.Empty)

            Translator.SetProperty("#DetailSeriesID", 0)
            Translator.SetProperty("#DetailEpisodeID", 0)
            Translator.SetProperty("#DetailMovPicID", 0)

            Translator.SetProperty("#DetailFSK", String.Empty)
            Translator.SetProperty("#DetailProgramFormat", String.Empty)
            Translator.SetProperty("#DetailAudioImage", String.Empty)




            Translator.SetProperty("#DetailRatingFun", 0)
            Translator.SetProperty("#DetailRatingAction", 0)
            Translator.SetProperty("#DetailRatingErotic", 0)
            Translator.SetProperty("#DetailRatingSuspense", 0)
            Translator.SetProperty("#DetailRatingLevel", 0)
            Translator.SetProperty("#DetailRatingEmotions", 0)

        End Sub
#End Region

    End Class
End Namespace
