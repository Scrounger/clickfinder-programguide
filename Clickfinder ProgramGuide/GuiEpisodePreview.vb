Imports MediaPortal.GUI.Library
Imports TvDatabase
Imports Gentle.Framework
Imports ClickfinderProgramGuide.Helper
Imports ClickfinderProgramGuide.TvDatabase
Imports MediaPortal.Configuration
Imports System.Threading
Imports Gentle.Common
Imports MediaPortal.Dialogs


Namespace ClickfinderProgramGuide
    Public Class EpisodePreviewGuiWindow
        Inherits GUIWindow


#Region "Skin Controls"

        'Buttons



        <SkinControlAttribute(5)> Protected _btnAllMovies As GUIButtonControl = Nothing



        'ProgressBar
        <SkinControlAttribute(8)> Protected _SeriesProgressBar As GUIAnimation = Nothing
        <SkinControlAttribute(9)> Protected _RepeatsProgressBar As GUIAnimation = Nothing

        'ListControl
        <SkinControlAttribute(10)> Protected _SeriesList As GUIListControl = Nothing
        <SkinControlAttribute(30)> Protected _RepeatsList As GUIListControl = Nothing

        'Image, sonst geflacker bug ?!
        <SkinControlAttribute(40)> Protected _SeriesPoster As GUIImage = Nothing

#End Region

#Region "Members"
        Private Shared _layer As New TvBusinessLayer
        Private ThreadSeriesFill As Threading.Thread
        Private ThreadRepeatsFill As Threading.Thread
        Private ThreadSeriesInfoFill As Threading.Thread
        Private _selectedListItemIndex As Integer

        Friend Shared _selectedSeries As TVMovieProgram
        Friend Shared _ShowEpisodes As Boolean

        Private _LastFocusedSeriesIndex As Integer
        Private _LastFocusedEpisodeIndex As Integer
        'Private _LastFocusedControlID As Integer

        Private _SeriesListItemCache As New List(Of GUIListItem)

#End Region

#Region "Constructors"
        Public Sub New()
        End Sub
#End Region

#Region "Properties"


#End Region

#Region "GUI Properties"
        Public Overloads Overrides Property GetID() As Integer
            Get
                Return 1656544657
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property


        Public Overloads Overrides Function Init() As Boolean
            'Beim initialisieren des Plugin den Screen laden

            Return Load(GUIGraphicsContext.Skin + "\ClickfinderProgramGuideEpisodePreview.xml")
        End Function

        Public Overrides Function GetModuleName() As String
            Return "Clickfinder ProgramGuide Episode Preview"
        End Function

#End Region

#Region "GUI Events"
        Protected Overrides Sub OnPageLoad()
            MyBase.OnPageLoad()
            GUIWindowManager.NeedRefresh()

            Helper.CheckConnectionState()

            MyLog.Info("")
            MyLog.Info("")
            MyLog.Info("[EpisodePreviewGuiWindow] -------------[OPEN]-------------")
            GuiLayout.SetSettingLastUpdateProperty()

            Translator.SetProperty("#EpisodePreviewLabel", "")
            Translator.SetProperty("#EpisodesPreviewSeriesHeaderLabel", "")
            Translator.SetProperty("#EpisodesPreviewEpisodeDescription", "")
            Translator.SetProperty("#EpisodesPreviewLabel1", "")
            Translator.SetProperty("#EpisodesPreviewLabel2", "")
            Translator.SetProperty("#EpisodesPreviewLabel3", "")
            Translator.SetProperty("#EpisodesPreviewSeriesRatingStar", "")
            Translator.SetProperty("#EpisodesPreviewEpisodeDescription", "")
            Translator.SetProperty("#EpisodesPreviewSeriesSummary", "")
            Translator.SetProperty("#EpisodesPreviewTitle", "")
            Translator.SetProperty("#EpisodesPreviewEpisodeDescription", "")

            _SeriesPoster.SetFileName("")

            _SeriesListItemCache.Clear()

            'Kein load parameter
            If _loadParameter = String.Empty Then
                'Serienansicht
                If _ShowEpisodes = False Then

                    ClearSeriesInfos()

                    If _LastFocusedSeriesIndex = 0 Then
                        _selectedListItemIndex = 0
                    End If

                    'erstes Element laden wegen SeriesInfo
                    'Serien mit neuen Episoden laden

                    Dim _SQLstring As String = String.Empty
                    Dim _Result As New ArrayList
                    _SQLstring = "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                                "WHERE idSeries > 0 " & _
                                                "AND local = 0 " & _
                                                "GROUP BY title ORDER BY startTime ASC"

                    _SQLstring = Helper.AppendSqlLimit(_SQLstring, 1)


                    _Result.AddRange(Broker.Execute(_SQLstring).TransposeToFieldList("idProgram", False))

                    If _LastFocusedSeriesIndex < 1 Then
                        _selectedSeries = TVMovieProgram.Retrieve(_Result.Item(0))
                    End If

                    Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowSeriesProgressBar)
                    _ProgressBarThread.Start()

                    Try
                        If ThreadSeriesFill.IsAlive = True Then ThreadSeriesFill.Abort()
                    Catch ex As Exception
                        'Eventuell auftretende Exception abfangen
                        ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
                    End Try

                    ThreadSeriesFill = New Threading.Thread(AddressOf FillSeriesList)
                    ThreadSeriesFill.IsBackground = True
                    ThreadSeriesFill.Start()

                    FillSeriesInfos()
                Else
                    'Epsidoen der Serie anzeigen
                    Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowSeriesProgressBar)
                    _ProgressBarThread.Start()

                    Try
                        If ThreadSeriesFill.IsAlive = True Then ThreadSeriesFill.Abort()
                    Catch ex As Exception
                        'Eventuell auftretende Exception abfangen
                        ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
                    End Try

                    ThreadSeriesFill = New Threading.Thread(AddressOf FillEpisodesList)
                    ThreadSeriesFill.IsBackground = True
                    ThreadSeriesFill.Start()
                End If

            Else
                'Hyyplerink Parameter
                MyLog.Info("[EpisodePreviewGuiWindow] Hyperlink parameter: {0}", _loadParameter)

                'Hyperlink Parameter: idSeries
                If InStr(_loadParameter, "idSeries: ") > 0 Then

                    _selectedListItemIndex = 0


                    Dim sb As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(TVMovieProgram))
                    sb.AddConstraint([Operator].Equals, "idSeries", CInt(Replace(_loadParameter, "idSeries: ", "")))
                    sb.AddConstraint([Operator].Equals, "local", False)
                    Dim stmt As SqlStatement = sb.GetStatement(True)
                    Dim _SeriesFoundList As IList(Of TVMovieProgram) = ObjectFactory.GetCollection(GetType(TVMovieProgram), stmt.Execute())


                    'Serie gefunden
                    If _SeriesFoundList.Count > 0 Then
                        _selectedSeries = _SeriesFoundList(0)

                        _ShowEpisodes = True

                        _LastFocusedEpisodeIndex = 0

                        'Epsidoen der Serie anzeigen
                        Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowSeriesProgressBar)
                        _ProgressBarThread.Start()

                        'FillSeriesInfos()

                        Try
                            If ThreadSeriesFill.IsAlive = True Then ThreadSeriesFill.Abort()
                        Catch ex As Exception
                            'Eventuell auftretende Exception abfangen
                            ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
                        End Try

                        ThreadSeriesFill = New Threading.Thread(AddressOf FillEpisodesList)
                        ThreadSeriesFill.IsBackground = True
                        ThreadSeriesFill.Start()
                    Else
                        'Series nicht gefunden
                        _SeriesProgressBar.Visible = False
                        _RepeatsProgressBar.Visible = False

                        'TvWish Server plugin aktiv
                        If _layer.GetSetting("pluginTvWishList", "false").Value = True Then

                            'Serie über id laden & 
                            Dim _TvSeriesDB As New TVSeriesDB
                            _TvSeriesDB.LoadSeriesName(CInt(Replace(_loadParameter, "idSeries: ", "")))

                            'prüfen ob schon in TvWishList
                            If _TvSeriesDB.CountSeries = 1 Then
                                If InStr(_layer.GetSetting("TvWishList_ListView").Value, "(title like '" & _TvSeriesDB(0).SeriesName & "' ) AND (description like '%Neue Folge: %')") > 0 Then
                                    Helper.ShowNotify("Keine neuen Episoden im EPG gefunden! TvWish für " & _TvSeriesDB(0).SeriesName & " wurde bereits angelegt")
                                    GUIWindowManager.ShowPreviousWindow()
                                Else
                                    'tvWish anlegen - fragen
                                    Dim dlgContext As GUIDialogYesNo = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_YES_NO, Integer)), GUIDialogYesNo)
                                    dlgContext.Reset()

                                    dlgContext.SetHeading(_TvSeriesDB(0).SeriesName)
                                    dlgContext.SetLine(1, "TvWish für neue Episoden anlegen?")
                                    dlgContext.SetLine(2, "")
                                    dlgContext.SetLine(3, "")
                                    dlgContext.DoModal(GUIWindowManager.ActiveWindow)

                                    ' ja anlegen
                                    If dlgContext.IsConfirmed = True Then
                                        GUIWindowManager.ActivateWindow(70943675, "eq(#currentmoduleid,'1656544652'), 'NEWTVWISH//EXPRESSION=(title like \'" & _TvSeriesDB(0).SeriesName & "\' ) AND (description like \'%Neue Folge: %\')//NAME=CPG: " & _TvSeriesDB(0).SeriesName & ": " & Translation.NewEpisodes, True)
                                    Else
                                        GUIWindowManager.ShowPreviousWindow()
                                    End If
                                End If
                            End If

                        Else
                            Helper.ShowNotify("Keine neuen Episoden im EPG gefunden!")
                            GUIWindowManager.ShowPreviousWindow()
                        End If


                    End If

                End If

            End If

        End Sub
        Public Overrides Sub OnAction(ByVal Action As MediaPortal.GUI.Library.Action)
            If GUIWindowManager.ActiveWindow = GetID Then

                'Wenn Previous Window (ESC), erst prüfen ob Episodenliste angezeigt wird -> zurück Serienliste
                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_PREVIOUS_MENU Then

                    If _ShowEpisodes = True Then
                        'zurück zur Serien Übersicht wenn kein load parameter
                        If _loadParameter = String.Empty Then
                            _ShowEpisodes = False

                            _LastFocusedEpisodeIndex = 0

                            Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowSeriesProgressBar)
                            _ProgressBarThread.Start()

                            Try
                                If ThreadSeriesFill.IsAlive = True Then ThreadSeriesFill.Abort()
                            Catch ex As Exception
                                'Eventuell auftretende Exception abfangen
                                ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
                            End Try

                            ThreadSeriesFill = New Threading.Thread(AddressOf FillSeriesList)
                            ThreadSeriesFill.IsBackground = True
                            ThreadSeriesFill.Start()
                            Return
                        End If

                    Else
                        If _loadParameter = String.Empty Then
                            _LastFocusedSeriesIndex = 0
                            _LastFocusedEpisodeIndex = 0
                        End If
                    End If

                End If

                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MOVE_UP Then
                    If _SeriesList.IsFocused = True Then
                        If _ShowEpisodes = True Then

                            If _SeriesList.SelectedListItem.ItemId = _SeriesList.Item(0).ItemId Then
                                _selectedListItemIndex = _SeriesList.Count - 1
                                _LastFocusedEpisodeIndex = _selectedListItemIndex
                            Else
                                _selectedListItemIndex = _SeriesList.SelectedListItemIndex - 1
                                _LastFocusedEpisodeIndex = _selectedListItemIndex
                            End If

                            Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowRepeatsProgressBar)
                            _ProgressBarThread.Start()

                            Try
                                If ThreadRepeatsFill.IsAlive = True Then ThreadRepeatsFill.Abort()
                            Catch ex As Exception
                                'Eventuell auftretende Exception abfangen
                                ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
                            End Try

                            ThreadRepeatsFill = New Threading.Thread(AddressOf FillRepeatsList)
                            ThreadRepeatsFill.IsBackground = True
                            ThreadRepeatsFill.Start()

                            '_LastFocusedEpisodeIndex = _selectedListItemIndex

                        Else

                            If _SeriesList.SelectedListItem.ItemId = _SeriesList.Item(0).ItemId Then
                                _selectedListItemIndex = _SeriesList.Count - 1
                                _selectedSeries = TVMovieProgram.Retrieve(_SeriesList.Item(_selectedListItemIndex).TVTag)
                                _LastFocusedSeriesIndex = _selectedListItemIndex
                            Else
                                _selectedListItemIndex = _SeriesList.SelectedListItemIndex - 1
                                _selectedSeries = TVMovieProgram.Retrieve(_SeriesList.Item(_selectedListItemIndex).TVTag)
                                _LastFocusedSeriesIndex = _selectedListItemIndex
                            End If

                            Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowRepeatsProgressBar)
                            _ProgressBarThread.Start()

                            Try
                                If ThreadSeriesInfoFill.IsAlive = True Then ThreadSeriesInfoFill.Abort()
                            Catch ex As Exception
                                'Eventuell auftretende Exception abfangen
                                ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
                            End Try

                            ThreadSeriesInfoFill = New Threading.Thread(AddressOf FillSeriesInfos)
                            ThreadSeriesInfoFill.IsBackground = True
                            ThreadSeriesInfoFill.Start()

                            '_LastFocusedIndex = _selectedListItemIndex
                        End If


                        '_LastFocusedControlID = _SeriesList.GetID
                    End If
                End If

                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MOVE_DOWN Then
                    If _SeriesList.IsFocused = True Then
                        If _ShowEpisodes = True Then

                            If _SeriesList.SelectedListItem.ItemId = _SeriesList.Item(_SeriesList.Count - 1).ItemId Then
                                _selectedListItemIndex = 0
                                _LastFocusedEpisodeIndex = _selectedListItemIndex
                            Else
                                _selectedListItemIndex = _SeriesList.SelectedListItemIndex + 1
                                _LastFocusedEpisodeIndex = _selectedListItemIndex
                            End If

                            Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowRepeatsProgressBar)
                            _ProgressBarThread.Start()

                            Try
                                If ThreadRepeatsFill.IsAlive = True Then ThreadRepeatsFill.Abort()
                            Catch ex As Exception
                                'Eventuell auftretende Exception abfangen
                                ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
                            End Try

                            ThreadRepeatsFill = New Threading.Thread(AddressOf FillRepeatsList)
                            ThreadRepeatsFill.IsBackground = True
                            ThreadRepeatsFill.Start()

                            '_LastFocusedEpisodeIndex = _selectedListItemIndex

                        Else

                            If _SeriesList.SelectedListItem.ItemId = _SeriesList.Item(_SeriesList.Count - 1).ItemId Then
                                _selectedListItemIndex = 0
                                _selectedSeries = TVMovieProgram.Retrieve(_SeriesList.Item(_selectedListItemIndex).TVTag)
                                _LastFocusedSeriesIndex = _selectedListItemIndex
                            Else
                                _selectedListItemIndex = _SeriesList.SelectedListItemIndex + 1
                                _selectedSeries = TVMovieProgram.Retrieve(_SeriesList.Item(_selectedListItemIndex).TVTag)
                                _LastFocusedSeriesIndex = _selectedListItemIndex
                            End If

                            'FillSeriesInfos()

                            Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowRepeatsProgressBar)
                            _ProgressBarThread.Start()

                            Try
                                If ThreadSeriesInfoFill.IsAlive = True Then ThreadSeriesInfoFill.Abort()
                            Catch ex As Exception
                                'Eventuell auftretende Exception abfangen
                                ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
                            End Try

                            ThreadSeriesInfoFill = New Threading.Thread(AddressOf FillSeriesInfos)
                            ThreadSeriesInfoFill.IsBackground = True
                            ThreadSeriesInfoFill.Start()

                            '_LastFocusedIndex = _selectedListItemIndex

                        End If


                        '_LastFocusedControlID = _SeriesList.GetID

                    End If
                End If

                'Play Button (P) -> Start channel
                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MUSIC_PLAY Then

                    Try
                        If _SeriesList.IsFocused = True Then StartTv(_selectedSeries.ReferencedProgram)
                        If _RepeatsList.IsFocused = True Then StartTv(Program.Retrieve(_RepeatsList.SelectedListItem.ItemId))
                    Catch ex As Exception
                        MyLog.Error("[Play Button]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
                    End Try

                End If

                'Record Button (R) -> MP TvProgramInfo aufrufen --Über keychar--
                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_KEY_PRESSED Then

                    If Action.m_key IsNot Nothing Then
                        If Action.m_key.KeyChar = 114 Then
                            If _SeriesList.IsFocused = True Then LoadTVProgramInfo(_selectedSeries.ReferencedProgram)
                            If _RepeatsList.IsFocused = True Then LoadTVProgramInfo(Program.Retrieve(_RepeatsList.SelectedListItem.ItemId))
                        End If
                    End If

                End If

                'Menu Button (F9) -> Context Menu open
                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_CONTEXT_MENU Then

                    If _SeriesList.IsFocused = True Then Helper.ShowActionMenu(_selectedSeries.ReferencedProgram)
                    If _RepeatsList.IsFocused = True Then Helper.ShowActionMenu(Program.Retrieve(_RepeatsList.SelectedListItem.ItemId))

                End If

                'OSD Info Button (Y) -> Context Menu open (gleiche Fkt. wie Menu Button)
                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_KEY_PRESSED Then

                    If Action.m_key IsNot Nothing Then
                        If Action.m_key.KeyChar = 121 Then
                            If _SeriesList.IsFocused = True Then Helper.ShowActionMenu(_selectedSeries.ReferencedProgram)
                            If _RepeatsList.IsFocused = True Then Helper.ShowActionMenu(Program.Retrieve(_RepeatsList.SelectedListItem.ItemId))
                        End If
                    End If
                End If

            End If
            MyBase.OnAction(Action)
        End Sub

        Protected Overrides Sub OnPageDestroy(ByVal new_windowId As Integer)
            Try
                ThreadSeriesFill.Abort()
            Catch ex As Exception
            End Try

            Try
                ThreadRepeatsFill.Abort()
            Catch ex As Exception
            End Try

            Try
                ThreadSeriesInfoFill.Abort()
            Catch ex As Exception
            End Try

            'If _SeriesList.IsFocused Then
            '    'MsgBox(_MovieList.SelectedListItemIndex)
            '    _LastFocusedIndex = _SeriesList.SelectedListItemIndex
            '    _LastFocusedControlID = _SeriesList.GetID
            '    If _ShowEpisodes = True Then
            '        _LastFocusedEpisodeIndex = _SeriesList.SelectedListItemIndex
            '    End If
            'ElseIf _RepeatsList.IsFocused Then
            '    _LastFocusedEpisodeIndex = _RepeatsList.SelectedListItemIndex
            '    _LastFocusedControlID = _RepeatsList.GetID
            'Else
            '    _LastFocusedIndex = 0
            '    _LastFocusedControlID = _SeriesList.GetID
            'End If

            Dispose()
            AllocResources()
            MyBase.OnPageDestroy(new_windowId)

        End Sub
        Protected Overrides Sub OnClicked(ByVal controlId As Integer, _
                                 ByVal control As GUIControl, _
                                 ByVal actionType As  _
                                 MediaPortal.GUI.Library.Action.ActionType)

            MyBase.OnClicked(controlId, control, actionType)




            If control Is _btnAllMovies Then
                GuiButtons.AllMovies()
            End If



            If control Is _SeriesList Or control Is _RepeatsList Then
                If actionType = MediaPortal.GUI.Library.Action.ActionType.ACTION_SELECT_ITEM Then
                    Action_SelectItem()
                End If
            End If

        End Sub

        Private Sub Action_SelectItem()

            If _SeriesList.IsFocused = True Then

                If _SeriesList.SelectedListItem.Label = ".." Then
                    'zurück zur Serien Übersicht
                    If _loadParameter = String.Empty Then
                        _ShowEpisodes = False

                        _LastFocusedEpisodeIndex = 0

                        Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowSeriesProgressBar)
                        _ProgressBarThread.Start()

                        Try
                            If ThreadSeriesFill.IsAlive = True Then ThreadSeriesFill.Abort()
                        Catch ex As Exception
                            'Eventuell auftretende Exception abfangen
                            ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
                        End Try

                        ThreadSeriesFill = New Threading.Thread(AddressOf FillSeriesList)
                        ThreadSeriesFill.IsBackground = True
                        ThreadSeriesFill.Start()

                    Else
                        GUIWindowManager.ShowPreviousWindow()
                    End If

                ElseIf _SeriesList.Item(0).ItemId = 0 Then
                    'DetailScreen anzeigen

                    _LastFocusedEpisodeIndex = _SeriesList.SelectedListItemIndex

                    ListControlClick(_SeriesList.SelectedListItem.ItemId)
                Else
                    'Epsidoen der Serie anzeigen
                    _ShowEpisodes = True

                    _LastFocusedEpisodeIndex = 0

                    _selectedSeries = TVMovieProgram.Retrieve(_SeriesList.SelectedListItem.TVTag)

                    _LastFocusedSeriesIndex = _SeriesList.SelectedListItemIndex

                    Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowSeriesProgressBar)
                    _ProgressBarThread.Start()

                    Try
                        If ThreadSeriesFill.IsAlive = True Then ThreadSeriesFill.Abort()
                    Catch ex As Exception
                        'Eventuell auftretende Exception abfangen
                        ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
                    End Try

                    FillSeriesInfos()

                    ThreadSeriesFill = New Threading.Thread(AddressOf FillEpisodesList)
                    ThreadSeriesFill.IsBackground = True
                    ThreadSeriesFill.Start()
                End If

            End If

        End Sub

#End Region

#Region "functions"
        Private Sub FillSeriesList()

            _SeriesList.AllocResources()
            _SeriesList.Visible = False
            _SeriesList.Clear()
            _RepeatsList.AllocResources()
            _RepeatsList.Visible = False
            _RepeatsList.Clear()

            If _SeriesListItemCache.Count > 0 Then
                _SeriesList.ListItems.AddRange(_SeriesListItemCache)
                _SeriesList.Visible = True

                GUIListControl.SelectItemControl(GetID, _SeriesList.GetID, _LastFocusedSeriesIndex)
                GUIListControl.FocusControl(GetID, _SeriesList.GetID)

                _selectedSeries = TVMovieProgram.Retrieve(_SeriesList.Item(_LastFocusedSeriesIndex).TVTag)

                FillSeriesInfos()
                _SeriesProgressBar.Visible = False
            Else

                Dim SQLstring As String = String.Empty

                Translator.SetProperty("#EpisodePreviewLabel", Translation.NewEpisodes & " " & Translation.InTheNext & " " & _layer.GetSetting("ClickfinderPreviewMaxDays", "7").Value & " " & Translation.Day)
                Translator.SetProperty("#EpisodesPreviewSeriesHeaderLabel", Translation.SeriesInfos)
                Translator.SetProperty("#EpisodesPreviewEpisodeDescription", "")

                Try

                    Dim _Result As New ArrayList
                    Dim _DummySeriesResult As New ArrayList
                    'Dim _timeLabel As String = String.Empty
                    Dim _FoundedNewEpisodes As String = String.Empty

                    'Serien mit neuen Episoden laden
                    SQLstring = "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                                "WHERE idSeries > 0 " & _
                                                "AND local = 0 " & _
                                                "GROUP BY title ORDER BY startTime ASC"

                    _Result.AddRange(Broker.Execute(SQLstring).TransposeToFieldList("idProgram", False))

                    For i = 0 To _Result.Count - 1

                        'ProgramDaten über TvMovieProgram laden
                        Dim _Series As TVMovieProgram = TVMovieProgram.Retrieve(_Result.Item(i))

                        'Prüfen ob noch immer lokal nicht vorhanden
                        CheckSeriesLocalStatus(_Series)
                        If _Series.local = False Then

                            If _DummySeriesResult.Contains(_Series.idSeries) = False Then
                                _DummySeriesResult.Add(_Series.idSeries)

                                '_timeLabel = Helper.getTranslatedDayOfWeek(_Series.ReferencedProgram.StartTime) & " " & Format(_Series.ReferencedProgram.StartTime.Day, "00") & "." & Format(_Series.ReferencedProgram.StartTime.Month, "00") & " - " & Format(_Series.ReferencedProgram.StartTime.Hour, "00") & ":" & Format(_Series.ReferencedProgram.StartTime.Minute, "00")

                                'alle neuen episoden zählen
                                Dim _newEpisodesCount As New ArrayList
                                Dim _newEpisodesSQLStringCount As String = String.Empty
                                Dim _SeriesPosterPath As String = Config.GetFile(Config.Dir.Thumbs, "MPTVSeriesBanners\") & _Series.SeriesPosterImage

                                _newEpisodesSQLStringCount = "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                                "WHERE idSeries = " & _Series.idSeries & " " & _
                                                "AND local = 0 " & _
                                                "GROUP BY episodeName"

                                '_newEpisodesSQLStringCount = "Select * from program " & _
                                '                "WHERE SeriesNum IS NOT NULL " & _
                                '                "AND EpisodeNum IS NOT NULL " & _
                                '                "AND startTime > " & StartTime & " " & _
                                '                "AND startTime < " & EndTime & " " & _
                                '                "AND title = '" & _Series.ReferencedProgram.Title & "' " & _
                                '                "GROUP BY SeriesNum, EpisodeNum ASC"

                                _newEpisodesCount.AddRange(Broker.Execute(_newEpisodesSQLStringCount).TransposeToFieldList("idProgram", False))

                                _FoundedNewEpisodes = _newEpisodesCount.Count & " " & Translation.NewEpisodeFound

                                'Dim _SeriesName As New TVSeriesDB
                                '_SeriesName.LoadSeriesName(_Series.idSeries)

                                AddListControlItem(_SeriesList, _Series.idSeries, _Series.ReferencedProgram.ReferencedChannel.DisplayName, _Series.ReferencedProgram.Title, , _FoundedNewEpisodes, _SeriesPosterPath, , , _Series.ReferencedProgram.IdProgram)

                            End If

                        End If

                    Next

                    _SeriesList.Visible = True
                    _SeriesProgressBar.Visible = False

                    GUIListControl.SelectItemControl(GetID, _SeriesList.GetID, _LastFocusedSeriesIndex)
                    GUIListControl.FocusControl(GetID, _SeriesList.GetID)

                    If _selectedListItemIndex = 0 Then
                        _selectedSeries = TVMovieProgram.Retrieve(_SeriesList.Item(_selectedListItemIndex).TVTag)
                    End If

                    FillSeriesInfos()
                    _SeriesListItemCache.Clear()
                    _SeriesListItemCache.AddRange(_SeriesList.ListItems)

                Catch ex As ThreadAbortException
                    MyLog.Debug("[EpisodePreviewGuiWindow] [FillSeriesList]: --- THREAD ABORTED ---")
                    MyLog.Debug("")
                Catch ex As GentleException
                Catch ex As Exception
                    MyLog.Error("[EpisodePreviewGuiWindow] [FillSeriesList]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
                End Try
            End If

        End Sub

        Private Sub FillEpisodesList()

            Try

                Dim SQLstring As String = String.Empty
                Dim _Result As New ArrayList
                Dim _timeLabel As String = String.Empty
                Dim _infoLabel As String = String.Empty

                Translator.SetProperty("#EpisodePreviewLabel", Translation.NewEpisodes & ": " & _selectedSeries.ReferencedProgram.Title)
                Translator.SetProperty("#EpisodesPreviewSeriesHeaderLabel", Translation.Repeats)

                _SeriesList.AllocResources()
                _SeriesList.Visible = False
                _SeriesList.Clear()
                _RepeatsList.AllocResources()
                _RepeatsList.Visible = False
                _RepeatsList.Clear()

                'Neue Episoden der Serie laden
                SQLstring = "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                            "WHERE idSeries = " & _selectedSeries.idSeries & " " & _
                                            "AND local = 0 " & _
                                            "GROUP BY episodeName ORDER BY startTime ASC"
                '"AND title = '" & _selectedSeries.ReferencedProgram.Title & "' " & _

                _Result.AddRange(Broker.Execute(SQLstring).TransposeToFieldList("idProgram", False))

                'zurück in Listcontrol einfügen
                AddListControlItem(_SeriesList, 0, "channel", "..", , , Config.GetFile(Config.Dir.Thumbs, "Clickfinder ProgramGuide\Arrow back.png"))

                MyLog.Info(_Result.Count)

                For i = 0 To _Result.Count - 1

                    Dim _RecordingStatus As String = String.Empty

                    'ProgramDaten über TvMovieProgram laden
                    Dim _Episode As TVMovieProgram = TVMovieProgram.Retrieve(_Result.Item(i))

                    'Prüfen ob noch immer lokal nicht vorhanden -> Anzeigen
                    CheckSeriesLocalStatus(_Episode)
                    If _Episode.local = False Then

                        _timeLabel = Helper.getTranslatedDayOfWeek(_Episode.ReferencedProgram.StartTime) & " " & Format(_Episode.ReferencedProgram.StartTime.Day, "00") & "." & Format(_Episode.ReferencedProgram.StartTime.Month, "00") & " - " & Format(_Episode.ReferencedProgram.StartTime.Hour, "00") & ":" & Format(_Episode.ReferencedProgram.StartTime.Minute, "00")

                        'Falls die EPisode als Neu gekeinzeichnet ist, aber keine SxxExx nummer hat -> nicht identifiziert worden
                        If String.IsNullOrEmpty(_Episode.ReferencedProgram.SeriesNum) = True Or String.IsNullOrEmpty(_Episode.ReferencedProgram.EpisodeNum) Then
                            _infoLabel = Translation.EpisodeNotIdentify
                        Else
                            _infoLabel = Translation.Season & " " & Format(CInt(_Episode.ReferencedProgram.SeriesNum), "00") & ", " & Translation.Episode & Format(CInt(_Episode.ReferencedProgram.EpisodeNum), "00")
                        End If

                        _RecordingStatus = GuiLayout.RecordingStatus(_Episode.ReferencedProgram)

                        'Prüfen ob Episode an anderem Tag aufgenommen wird
                        If _RecordingStatus = String.Empty Then
                            Dim _RecordingList As New ArrayList

                            SQLstring = "Select program.idprogram from program " & _
                                    "WHERE title = '" & TVSeriesDB.allowedSigns(_Episode.ReferencedProgram.Title) & "' " & _
                                    "AND episodeName = '" & TVSeriesDB.allowedSigns(_Episode.ReferencedProgram.EpisodeName) & "' " & _
                                    "Order BY state DESC"

                            Helper.AppendSqlLimit(SQLstring, 1)

                            _RecordingList.AddRange(Broker.Execute(SQLstring).TransposeToFieldList("idProgram", True))

                            If _RecordingList.Count > 0 Then
                                Dim _Record As Program = Program.Retrieve(_RecordingList.Item(0))
                                _RecordingStatus = GuiLayout.RecordingStatus(_Record)

                                Select Case (_RecordingStatus)
                                    Case Is = "tvguide_record_button.png"
                                        _RecordingStatus = "ClickfinderPG_recOnce.png"
                                    Case Is = "tvguide_recordserie_button.png"
                                        _RecordingStatus = "ClickfinderPG_recSeries.png"
                                End Select
                            End If
                        End If
                    End If

                    AddListControlItem(_SeriesList, _Episode.ReferencedProgram.IdProgram, _Episode.ReferencedProgram.ReferencedChannel.DisplayName, _Episode.ReferencedProgram.EpisodeName, _timeLabel, _infoLabel, Config.GetFile(Config.Dir.Thumbs, "tv\logos\") & Replace(_Episode.ReferencedProgram.ReferencedChannel.DisplayName, "/", "_") & ".png", , _RecordingStatus)

                Next

                _SeriesList.Visible = True
                _SeriesProgressBar.Visible = False

                GUIListControl.SelectItemControl(GetID, _SeriesList.GetID, _LastFocusedEpisodeIndex)
                GUIListControl.FocusControl(GetID, _SeriesList.GetID)

                FillRepeatsList()

            Catch ex As ThreadAbortException
                MyLog.Debug("[EpisodePreviewGuiWindow] [FillEpisodesList]: --- THREAD ABORTED ---")
                MyLog.Debug("")
            Catch ex As GentleException
            Catch ex As Exception
                MyLog.Error("[EpisodePreviewGuiWindow] [FillEpisodesList]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try

        End Sub

        Private Sub FillRepeatsList()

            Try

                If Not _SeriesList.Item(_selectedListItemIndex).Label = ".." Then

                    Dim SQLstring As String = String.Empty
                    Dim _Result As New ArrayList
                    Dim _timeLabel As String = String.Empty
                    Dim _infoLabel As String = String.Empty

                    Dim _EpisodeInfos As TVMovieProgram = TVMovieProgram.Retrieve(_SeriesList.Item(_selectedListItemIndex).ItemId)

                    If _EpisodeInfos.needsUpdate = True Then
                        Helper.UpdateProgramData(_EpisodeInfos)
                    End If

                    _RepeatsList.AllocResources()
                    _RepeatsList.Visible = False
                    _RepeatsList.Clear()

                    _SeriesPoster.SetFileName(Config.GetFile(Config.Dir.Thumbs, "MPTVSeriesBanners\") & _selectedSeries.SeriesPosterImage)

                    Translator.SetProperty("#EpisodesPreviewLabel1", Translation.EpisodeNewPrefixLabel & " " & _SeriesList.Item(_selectedListItemIndex).Label)
                    Translator.SetProperty("#EpisodesPreviewLabel2", _SeriesList.Item(_selectedListItemIndex).Label3)
                    Translator.SetProperty("#EpisodesPreviewLabel3", _SeriesList.Item(_selectedListItemIndex).Path & ": " & _SeriesList.Item(_selectedListItemIndex).Label2)
                    Translator.SetProperty("#EpisodesPreviewSeriesRatingStar", GuiLayout.ratingStar(_EpisodeInfos.ReferencedProgram))
                    Translator.SetProperty("#EpisodesPreviewEpisodeDescription", _EpisodeInfos.Describtion)

                    Translator.SetProperty("#EpisodesPreviewSeriesSummary", "")


                    'Wiederholung der selektierten Episode laden
                    SQLstring = "Select * from program " & _
                                                "WHERE title = '" & TVSeriesDB.allowedSigns(_selectedSeries.ReferencedProgram.Title) & "' " & _
                                                "AND episodeName = '" & TVSeriesDB.allowedSigns(_SeriesList.SelectedListItem.Label) & "' " & _
                                                "ORDER BY startTime ASC"

                    _Result.AddRange(Broker.Execute(SQLstring).TransposeToFieldList("idProgram", False))

                    If _Result.Count > 0 Then
                        For i = 0 To _Result.Count - 1
                            'ProgramDaten über TvMovieProgram laden
                            Dim _Repeat As TVMovieProgram = TVMovieProgram.Retrieve(_Result.Item(i))

                            If Not _Repeat.ReferencedProgram.IdProgram = _SeriesList.Item(_selectedListItemIndex).ItemId Then

                                _timeLabel = Helper.getTranslatedDayOfWeek(_Repeat.ReferencedProgram.StartTime) & " " & Format(_Repeat.ReferencedProgram.StartTime.Day, "00") & "." & Format(_Repeat.ReferencedProgram.StartTime.Month, "00") & " - " & Format(_Repeat.ReferencedProgram.StartTime.Hour, "00") & ":" & Format(_Repeat.ReferencedProgram.StartTime.Minute, "00")
                                _infoLabel = Translation.Season & " " & _Repeat.ReferencedProgram.SeriesNum & ", " & Translation.Episode & " " & _Repeat.ReferencedProgram.EpisodeNum

                                AddListControlItem(_RepeatsList, _Repeat.ReferencedProgram.IdProgram, _Repeat.ReferencedProgram.ReferencedChannel.DisplayName, _Repeat.ReferencedProgram.EpisodeName, , _timeLabel, Config.GetFile(Config.Dir.Thumbs, "tv\logos\") & Replace(_Repeat.ReferencedProgram.ReferencedChannel.DisplayName, "/", "_") & ".png", , GuiLayout.RecordingStatus(_Repeat.ReferencedProgram))
                            End If

                        Next
                    End If

                    _RepeatsList.Visible = True
                    _RepeatsProgressBar.Visible = False

                Else

                    If _SeriesList.Item(_selectedListItemIndex).Label = ".." Then
                        FillSeriesInfos()
                    End If
                    _RepeatsList.Visible = False
                    _RepeatsList.Clear()
                    _RepeatsProgressBar.Visible = False


                End If

            Catch ex As ThreadAbortException
                MyLog.Debug("[EpisodePreviewGuiWindow] [FillRepeatsList]: --- THREAD ABORTED ---")
                MyLog.Debug("")
            Catch ex As GentleException
            Catch ex As Exception
                MyLog.Error("[EpisodePreviewGuiWindow] [FillRepeatsList]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try

        End Sub

        Private Sub FillSeriesInfos()

            Try
                'If _ShowEpisodes = False Then
                '    _selectedSeries = TVMovieProgram.Retrieve(_SeriesList.Item(_selectedListItemIndex).TVTag)
                'End If

                _SeriesPoster.SetFileName(Config.GetFile(Config.Dir.Thumbs, "MPTVSeriesBanners\") & _selectedSeries.SeriesPosterImage)

                Translator.SetProperty("#EpisodesPreviewTitle", _selectedSeries.ReferencedProgram.Title)
                Translator.SetProperty("#EpisodesPreviewEpisodeDescription", "")

                Dim _TvSeries As New TVSeriesDB
                _TvSeries.LoadSeriesName(_selectedSeries.idSeries)
                Translator.SetProperty("#EpisodesPreviewSeriesSummary", _TvSeries(0).Summary)
                Translator.SetProperty("#EpisodesPreviewSeriesRatingStar", _TvSeries(0).Rating)
                Translator.SetProperty("#EpisodesPreviewLabel1", _TvSeries(0).Network)
                Translator.SetProperty("#EpisodesPreviewLabel2", _TvSeries(0).Status)

                If _TvSeries(0).Status = "Ended" Then
                    Translator.SetProperty("#EpisodesPreviewLabel2", Translation.Ended)
                Else
                    Translator.SetProperty("#EpisodesPreviewLabel2", Translation.Continous)
                End If

                If _layer.GetSetting("pluginTvWishList", "false").Value = True Then

                    Dim _sqlString As String = String.Empty
                    Dim _result As New ArrayList
                    Dim _EntryFoundinTvWishList As Boolean = False

                    _sqlString = "Select idSetting from setting WHERE tag LIKE 'TvWishList_ListView' ORDER BY TAG"
                    _result.AddRange(Broker.Execute(_sqlString).TransposeToFieldList("idSetting", False))

                    _sqlString = "Select idSetting from setting WHERE tag LIKE 'TvWishList_ListView____' ORDER BY TAG"
                    _result.AddRange(Broker.Execute(_sqlString).TransposeToFieldList("idSetting", False))

                    If _result.Count > 0 Then
                        For o = 0 To _result.Count - 1
                            Dim _TvWishListEntry As Setting = Setting.Retrieve(_result(o))
                            If InStr(_TvWishListEntry.Value, "(title like '" & _selectedSeries.ReferencedProgram.Title & "' ) AND (description like '%Neue Folge: %')") > 0 Then
                                _EntryFoundinTvWishList = True
                            End If
                        Next
                    End If

                    Translator.SetProperty("#EpisodesPreviewTvWishEntryFounded", CStr(_EntryFoundinTvWishList))

                End If

                Translator.SetProperty("#EpisodesPreviewLabel3", "")
                Translator.SetProperty("#EpisodesPreviewFanArt", Config.GetFile(Config.Dir.Thumbs, "") & _selectedSeries.FanArt)
                _TvSeries.Dispose()

                _RepeatsProgressBar.Visible = False

            Catch ex As ThreadAbortException
                MyLog.Debug("[EpisodePreviewGuiWindow] [FillSeriesInfos]: --- THREAD ABORTED ---")
                MyLog.Debug("")
            Catch ex As GentleException
            Catch ex As Exception
                MyLog.Error("[EpisodePreviewGuiWindow] [FillSeriesInfos]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try

        End Sub

        Private Sub ShowSeriesProgressBar()
            _SeriesProgressBar.Visible = True
        End Sub

        Private Sub ShowRepeatsProgressBar()
            _RepeatsProgressBar.Visible = True
        End Sub

        Private Sub ClearSeriesInfos()
            Translator.SetProperty("#EpisodesPreviewSeriesPosterImage", "")
            Translator.SetProperty("#EpisodesPreviewTitle", "")
            Translator.SetProperty("#EpisodesPreviewSeriesSummary", "")
            Translator.SetProperty("#EpisodesPreviewSeriesRatingStar", 0)
            Translator.SetProperty("#EpisodesPreviewLabel1", "")
            Translator.SetProperty("#EpisodesPreviewLabel2", "")
            Translator.SetProperty("#EpisodesPreviewLabel3", "")
            Translator.SetProperty("#EpisodesPreviewFanArt", "")
            Translator.SetProperty("#EpisodesPreviewTvWishEntryFounded", "false")
        End Sub

#End Region


    End Class
End Namespace
