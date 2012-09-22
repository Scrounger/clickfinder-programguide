Imports MediaPortal.GUI.Library
Imports TvDatabase
Imports ClickfinderProgramGuide.TvDatabase
Imports MediaPortal.Configuration
Imports MediaPortal.Util
Imports Gentle.Framework



Namespace ClickfinderProgramGuide
    Public Class DetailGuiWindow
        Inherits GUIWindow

#Region "Skin Controls"

        'Buttons


        <SkinControlAttribute(4)> Protected _btnLateTime As GUIButtonControl = Nothing
        <SkinControlAttribute(5)> Protected _btnAllMovies As GUIButtonControl = Nothing
        <SkinControlAttribute(6)> Protected _btnHighlights As GUIButtonControl = Nothing



        <SkinControlAttribute(10)> Protected _btnBack As GUIButtonControl = Nothing
        <SkinControlAttribute(11)> Protected _btnPlay As GUIButtonControl = Nothing
        <SkinControlAttribute(12)> Protected _btnRecord As GUIButtonControl = Nothing
        <SkinControlAttribute(13)> Protected _btnRemember As GUIButtonControl = Nothing
        <SkinControlAttribute(14)> Protected _btnJumpTo As GUIButtonControl = Nothing

#End Region

#Region "Members"
        Friend Shared _DetailTvMovieProgram As TVMovieProgram
        Private _layer As New TvBusinessLayer

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub

        Friend Shared Sub SetGuiProperties(ByVal TvMovieProgram As TVMovieProgram)
            _DetailTvMovieProgram = TvMovieProgram
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
            MyBase.OnPageLoad()
            GUIWindowManager.NeedRefresh()

            If String.IsNullOrEmpty(_loadParameter) = False Then
                Try
                    If _layer.GetSetting("TvMovieImportIsRunning", "false").Value = "true" Then
                        Translator.SetProperty("#SettingLastUpdate", Translation.ImportIsRunning)
                        MyLog.Debug("[DetailGuiWindow] [OnPageLoad]: {0}", "TvMovie++ Import is running !")
                    Else
                        Translator.SetProperty("#SettingLastUpdate", GuiLayout.LastUpdateLabel)
                    End If



                    MyLog.Debug("[DetailGuiWindow]: [HyperlinkParameter]: {0}", _loadParameter)


                    If InStr(_loadParameter, "TITLE: ") > 0 Then
                        'Hyperlink Parameter: Title, Channel, Start, Stop
                        Dim _parametersList As New ArrayList(Split(_loadParameter, "|"))
                        Dim _title As String = String.Empty
                        Dim _channel As String = String.Empty
                        Dim _start As DateTime
                        Dim _end As DateTime

                        For i = 0 To _parametersList.Count - 1
                            'MsgBox(_parametersList.Item(i))
                            If InStr(_parametersList.Item(i), "TITLE: ") > 0 Then _title = Replace(_parametersList.Item(i), "TITLE: ", "")
                            If InStr(_parametersList.Item(i), "CHANNEL: ") > 0 Then _channel = Replace(_parametersList.Item(i), "CHANNEL: ", "")
                            If InStr(_parametersList.Item(i), "START: ") > 0 Then _start = DateTime.Parse(Replace(_parametersList.Item(i), "START: ", ""))
                            If InStr(_parametersList.Item(i), "STOP: ") > 0 Then _end = DateTime.Parse(Replace(_parametersList.Item(i), "STOP: ", ""))
                        Next


                        Dim SQLstring As String = String.Empty

                        Dim _Result As New ArrayList

                        'program laden
                        SQLstring = "Select * from program INNER JOIN channel ON program.idChannel = channel.idChannel " & _
                                                    "WHERE title = '" & _title & "' " & _
                                                    "AND displayName = '" & _channel & "' " & _
                                                    "AND startTime > " & Helper.MySqlDate(_start.AddMinutes(-1)) & " " & _
                                                    "AND startTime < " & Helper.MySqlDate(_end) & " " & _
                                                    ""

                        _Result.AddRange(Broker.Execute(SQLstring).TransposeToFieldList("idProgram", False))


                        _DetailTvMovieProgram = Helper.getTvMovieProgram(Program.Retrieve(_Result.Item(0)))

                    ElseIf InStr(_loadParameter, "idProgram: ") > 0 Then
                        'Hyperlink Parameter: idProgram
                        MsgBox(CInt(Replace(_loadParameter, "idProgram: ", "")))
                        _DetailTvMovieProgram = Helper.getTvMovieProgram(Program.Retrieve(CInt(Replace(_loadParameter, "idProgram: ", ""))))
                    End If

                Catch ex As Exception
                    MyLog.Error("[DetailGuiWindow]: [HyperlinkParameter]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
                End Try

            End If

            Try

                MyLog.Info("")
                MyLog.Info("")
                MyLog.Info("[DetailGuiWindow] -------------[OPEN]-------------")
                MyLog.Debug("[DetailGuiWindow] [OnPageLoad]: {0}, idProgram = {1}, needsUpdate = {2}", _DetailTvMovieProgram.ReferencedProgram.Title, _DetailTvMovieProgram.idProgram, _DetailTvMovieProgram.needsUpdate)

                Helper.CheckConnectionState()
                ShowDetails()

            Catch ex As Exception
                MyLog.Error("[DetailGuiWindow]: [OnPageLoad]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try

            'MsgBox(Details_idProgram)

        End Sub

        Protected Overrides Sub OnPageDestroy(ByVal new_windowId As Integer)
            'MsgBox("Hallo")
            'GC.Collect()
            MyBase.OnPageDestroy(new_windowId)
        End Sub

        Public Overrides Sub OnAction(ByVal action As MediaPortal.GUI.Library.Action)
            MyBase.OnAction(action)

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

            If control Is _btnHighlights Then
                GuiButtons.Highlights()
            End If


            If control Is _btnRecord Then
                Helper.LoadTVProgramInfo(_DetailTvMovieProgram.ReferencedProgram)
            End If

            If control Is _btnRemember Then
                Helper.SetNotify(_DetailTvMovieProgram.ReferencedProgram)
            End If

            If control Is _btnPlay Then
                Helper.StartTv(_DetailTvMovieProgram.ReferencedProgram)
            End If

            If control Is _btnBack Then
                GUIWindowManager.ShowPreviousWindow()
            End If

            If control Is _btnJumpTo Then
                Select Case GUIPropertyManager.GetProperty("#DetailJumpToLabel")
                    Case Is = Translation.MovingPictures
                        GUIWindowManager.ActivateWindow(96742, "movieid:" & GUIPropertyManager.GetProperty("#DetailMovPicID"))
                    Case Is = Translation.MPtvSeries
                        GUIWindowManager.ActivateWindow(9811, "seriesid:" & _
                                                        GUIPropertyManager.GetProperty("#DetailSeriesID") & _
                                                        "|seasonidx:" & GUIPropertyManager.GetProperty("#Detailseasonidx") & _
                                                        "|episodeidx:" & GUIPropertyManager.GetProperty("#Detailepisodeidx"))
                End Select
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


#End Region

#Region "Functions"
        Private Sub ShowDetails()

            Try
                Dim _recordingStatus As String = String.Empty

                If _DetailTvMovieProgram.needsUpdate = True Then
                    Helper.UpdateProgramData(_DetailTvMovieProgram)
                End If


                If _DetailTvMovieProgram.idSeries > 0 Then
                    Helper.CheckSeriesLocalStatus(_DetailTvMovieProgram)

                    Select Case _layer.GetSetting("ClickfinderDetailsSeriesImage", "Cover").Value
                        Case Is = "Cover"
                            Translator.SetProperty("#DetailImage", Config.GetFile(Config.Dir.Thumbs, "MPTVSeriesBanners\") & _DetailTvMovieProgram.SeriesPosterImage)
                        Case Is = "FanArt"
                            Translator.SetProperty("#DetailImage", Config.GetFile(Config.Dir.Thumbs, "") & _DetailTvMovieProgram.FanArt)
                        Case Is = "Episode"
                            Translator.SetProperty("#DetailImage", Config.GetFile(Config.Dir.Thumbs, "MPTVSeriesBanners\") & _DetailTvMovieProgram.EpisodeImage)
                        Case Is = "TvMovie"
                            If Not String.IsNullOrEmpty(_DetailTvMovieProgram.BildDateiname) Then
                                Translator.SetProperty("#DetailImage", ClickfinderDB.ImagePath & "\" & _DetailTvMovieProgram.BildDateiname)
                            Else
                                Translator.SetProperty("#DetailImage", Config.GetFile(Config.Dir.Thumbs, "tv\logos\") & Replace(_DetailTvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName, "/", "_") & ".png")
                            End If

                    End Select

                Else
                    Translator.SetProperty("#DetailImage", GuiLayout.Image(_DetailTvMovieProgram))
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
                                "WHERE title = '" & _DetailTvMovieProgram.ReferencedProgram.Title & "' " & _
                                "AND episodeName = '" & _DetailTvMovieProgram.ReferencedProgram.EpisodeName & "' " & _
                                "Order BY state DESC"

                        Helper.AppendSqlLimit(SQLstring, 1)

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

                If Not _DetailTvMovieProgram.Year < New Date(1900, 1, 1) Then
                    Translator.SetProperty("#DetailYear", _DetailTvMovieProgram.Year.Year & " ")
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
                MyLog.Error("[DetailGuiWindow]: [ShowDetails]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try
        End Sub


#End Region

    End Class
End Namespace
