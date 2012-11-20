Imports MediaPortal.GUI.Library
Imports TvDatabase
Imports ClickfinderProgramGuide.ClickfinderProgramGuide.HighlightsGUIWindow
Imports Gentle.Framework
Imports ClickfinderProgramGuide.TvDatabase.TVMovieProgram
Imports ClickfinderProgramGuide.Helper
Imports MediaPortal.Configuration
Imports ClickfinderProgramGuide.TvDatabase
Imports MediaPortal.Dialogs
Imports System.Threading
Imports Gentle.Common
Imports MediaPortal.GUI.Library.Action

Namespace ClickfinderProgramGuide
    Public Class HighlightsGuiWindow
        Inherits GUIWindow

#Region "Skin Controls"

        'Buttons



        <SkinControlAttribute(5)> Protected _btnAllMovies As GUIButtonControl = Nothing



        'ProgressBar
        <SkinControlAttribute(9)> Protected _HighlightsProgressBar As GUIAnimation = Nothing
        <SkinControlAttribute(8)> Protected _MoviesProgressBar As GUIAnimation = Nothing
        <SkinControlAttribute(11)> Protected _DaysProgress As GUIProgressControl = Nothing

        'ListControl
        <SkinControlAttribute(10)> Protected _MovieList As GUIListControl = Nothing
        <SkinControlAttribute(30)> Protected _HighlightsList As GUIListControl = Nothing

#End Region

#Region "Members"
        Private _ThreadFillMovieList As Threading.Thread
        Private _ThreadFillHighlightsList As Threading.Thread
        Friend _ClickfinderCurrentDate As Date = Nothing
        Friend _ProgressPercentagValue As Single
        Private _layer As New TvBusinessLayer
        Friend Shared _DebugModeOn As Boolean = False

        Private _LastFocusedItemIndex As Integer = 0
        Private _LastFocusedControlID As Integer

        Private _logMovieTime As String
        Private _logHighlights As String

        Private _NewEpisodesList As New List(Of TVMovieProgram)

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
#End Region

#Region "GUI Properties"

        Public Overloads Overrides Property GetID() As Integer
            Get
                Return 1656544656
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property


        Public Overloads Overrides Function Init() As Boolean
            'Beim initialisieren des Plugin den Screen laden
            Return Load(GUIGraphicsContext.Skin + "\ClickfinderProgramGuideHighlights.xml")
        End Function

        Public Overrides Function GetModuleName() As String
            Return "Clickfinder ProgramGuide Highlights"
        End Function
#End Region

#Region "GUI Events"
        Protected Overrides Sub OnPageLoad()
            MyBase.OnPageLoad()
            GUIWindowManager.NeedRefresh()

            Helper.CheckConnectionState()

            MyLog.Info("")
            MyLog.Info("")
            MyLog.Info("[HighlightsGuiWindow] -------------[OPEN]-------------")

            GuiLayout.SetSettingLastUpdateProperty()

            Try
                Translator.SetProperty("#CurrentDate", Translation.Loading)

                _DaysProgress.Percentage = _ProgressPercentagValue
                If _ClickfinderCurrentDate = Nothing Then
                    _ClickfinderCurrentDate = Today
                    If Not _DaysProgress.Percentage = 1 * 100 / (CPGsettings.OverviewMaxDays + 1) Then
                        _DaysProgress.Percentage = 1 * 100 / (CPGsettings.OverviewMaxDays + 1)
                    End If
                End If

                Translator.SetProperty("#CurrentDate", getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy"))
                MyLog.Debug("[HighlightsGuiWindow] [OnPageLoad]: _ClickfinderCurrentDate = {0}", getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy"))

                AbortRunningThreads

                _ThreadFillMovieList = New Thread(AddressOf FillMovieList)
                _ThreadFillHighlightsList = New Thread(AddressOf FillHighlightsList)
                _ThreadFillMovieList.Start()
                _ThreadFillHighlightsList.Start()

            Catch ex As Exception
                MyLog.Error("[HighlightsGUIWindow] [OnPageLoad]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try

        End Sub
        Public Overrides Sub OnAction(ByVal Action As MediaPortal.GUI.Library.Action)

            If GUIWindowManager.ActiveWindow = GetID Then
                'Try
                '    MyLog.[Debug]("[OnAction] Keypress KeyChar={0} ; KeyCode={1} ; Actiontype={2}", Action.m_key.KeyChar, Action.m_key.KeyCode, Action.wID.ToString)
                'Catch
                '    MyLog.[Debug]("[OnAction] Keypress KeyChar={0} ; KeyCode={1} ; Actiontype={2}", Action.m_key.KeyChar, Action.m_key.KeyCode, Action.wID.ToString)
                'End Try

                'Select Item (Enter) -> MP TvProgramInfo aufrufen --Über keychar--
                'If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_SELECT_ITEM Then
                '    MyLog.[Debug]("[HighlightsGUIWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", Action.m_key.KeyChar, Action.m_key.KeyCode, Action.wID.ToString)

                '    Action_SelectItem()

                'End If

                'Next Item (F8) -> einen Tag vor
                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_NEXT_ITEM Then

                    _LastFocusedItemIndex = 0
                    _LastFocusedControlID = _MovieList.GetID

                    Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowHighlightsProgressBar)
                    _ProgressBarThread.Start()

                    Dim _ProgressBarThread2 As New Threading.Thread(AddressOf ShowMoviesProgressBar)
                    _ProgressBarThread2.Start()

                    AbortRunningThreads()

                    If Not Date.Equals(_ClickfinderCurrentDate, Today.AddDays(CPGsettings.OverviewMaxDays)) Then
                        _ClickfinderCurrentDate = _ClickfinderCurrentDate.AddDays(1)
                        _DaysProgress.Percentage = _DaysProgress.Percentage + 1 * 100 / (CPGsettings.OverviewMaxDays + 1)
                    ElseIf _ClickfinderCurrentDate = Today.AddDays(CPGsettings.OverviewMaxDays) Then
                        _ClickfinderCurrentDate = Today.AddDays(-1)
                        _DaysProgress.Percentage = 0
                    End If

                    Translator.SetProperty("#CurrentDate", getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy"))
                    'MyLog.Debug("")
                    MyLog.[Debug]("[HighlightsGUIWindow] [OnAction]: Keypress - Actiontype={0}: _ClickfinderCurrentDate = {1}", Action.wID.ToString, getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy"))

                    _ThreadFillMovieList = New Thread(AddressOf FillMovieList)
                    _ThreadFillHighlightsList = New Thread(AddressOf FillHighlightsList)
                    _ThreadFillMovieList.IsBackground = True
                    _ThreadFillHighlightsList.IsBackground = True
                    _ThreadFillMovieList.Start()
                    _ThreadFillHighlightsList.Start()
                    '_Thread1.Join()
                    Return
                End If

                'Prev. Item (F7) -> einen Tag zurück
                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_PREV_ITEM Then

                    _LastFocusedItemIndex = 0
                    _LastFocusedControlID = _MovieList.GetID

                    Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowHighlightsProgressBar)
                    _ProgressBarThread.Start()

                    Dim _ProgressBarThread2 As New Threading.Thread(AddressOf ShowMoviesProgressBar)
                    _ProgressBarThread2.Start()

                    AbortRunningThreads()

                    If Not Date.Equals(_ClickfinderCurrentDate, Today.AddDays(-1)) Then
                        _ClickfinderCurrentDate = _ClickfinderCurrentDate.AddDays(-1)
                        _DaysProgress.Percentage = _DaysProgress.Percentage - 1 * 100 / (CPGsettings.OverviewMaxDays + 1)
                    ElseIf _ClickfinderCurrentDate = Today.AddDays(-1) Then
                        _ClickfinderCurrentDate = Today.AddDays(CPGsettings.OverviewMaxDays)
                        _DaysProgress.Percentage = 100
                    End If

                    Translator.SetProperty("#CurrentDate", getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy"))
                    'MyLog.Debug("")
                    MyLog.[Debug]("[HighlightsGUIWindow] [OnAction]: Keypress - Actiontype={0}: _ClickfinderCurrentDate = {1}", Action.wID.ToString, getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy"))

                    _ThreadFillMovieList = New Thread(AddressOf FillMovieList)
                    _ThreadFillHighlightsList = New Thread(AddressOf FillHighlightsList)
                    _ThreadFillMovieList.IsBackground = True
                    _ThreadFillHighlightsList.IsBackground = True
                    _ThreadFillMovieList.Start()
                    _ThreadFillHighlightsList.Start()
                    Return
                End If

                'Menu Button (F9) -> Context Menu open
                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_CONTEXT_MENU Then
                    MyLog.[Debug]("[HighlightsGUIWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", Action.m_key.KeyChar, Action.m_key.KeyCode, Action.wID.ToString)

                    RememberLastFocusedItem()

                    If _MovieList.IsFocused = True Then ShowMoviesMenu(_MovieList.SelectedListItem.ItemId)

                    If _HighlightsList.IsFocused = True Then
                        'Falls im Label2 Translation.NewLabel gefunden -> Series Context Menu
                        If _HighlightsList.SelectedListItem.Label2 = Translation.NewLabel Then
                            ShowSeriesContextMenu(_HighlightsList.SelectedListItem.TVTag)
                        Else
                            ShowHighlightsMenu(_HighlightsList.SelectedListItem.ItemId)
                        End If
                    End If
                End If

                'OSD Info Button (Y) -> Context Menu open (gleiche Fkt. wie Menu Button)
                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_KEY_PRESSED Then
                    If Action.m_key IsNot Nothing Then
                        If Action.m_key.KeyChar = 121 Then

                            RememberLastFocusedItem()

                            MyLog.[Debug]("[HighlightsGUIWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", Action.m_key.KeyChar, Action.m_key.KeyCode, Action.wID.ToString)

                            If _MovieList.IsFocused = True Then ShowMoviesMenu(_MovieList.SelectedListItem.ItemId)
                            If _HighlightsList.IsFocused = True Then
                                'Falls im Label2 Translation.NewLabel gefunden -> Series Context Menu
                                If _HighlightsList.SelectedListItem.Label2 = Translation.NewLabel Then
                                    ShowSeriesContextMenu(_HighlightsList.SelectedListItem.TVTag)
                                Else
                                    ShowHighlightsMenu(_HighlightsList.SelectedListItem.ItemId)
                                End If
                            End If
                        End If
                    End If
                End If

                'Play Button (P) -> Start channel
                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MUSIC_PLAY Then
                    Try
                        MyLog.[Debug]("[HighlightsGUIWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", Action.m_key.KeyChar, Action.m_key.KeyCode, Action.wID.ToString)

                        If _MovieList.IsFocused = True Then StartTv(Program.Retrieve(_MovieList.SelectedListItem.ItemId))
                        If _HighlightsList.IsFocused = True Then StartTv(Program.Retrieve(_HighlightsList.SelectedListItem.ItemId))
                    Catch ex As Exception
                        MyLog.Error("[Play Button]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
                    End Try
                End If

                'Record Button (R) -> MP TvProgramInfo aufrufen --Über keychar--
                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_KEY_PRESSED Then
                    If Action.m_key IsNot Nothing Then
                        If Action.m_key.KeyChar = 114 Then
                            MyLog.[Debug]("[HighlightsGUIWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", Action.m_key.KeyChar, Action.m_key.KeyCode, Action.wID.ToString)

                            If _MovieList.IsFocused = True Then LoadTVProgramInfo(Program.Retrieve(_MovieList.SelectedListItem.ItemId))
                            If _HighlightsList.IsFocused = True Then LoadTVProgramInfo(Program.Retrieve(_HighlightsList.SelectedListItem.ItemId))
                        End If
                    End If
                End If


                'Remote 5 Button (5) -> zu Heute springen
                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.REMOTE_5 Then
                    MyLog.[Debug]("[HighlightsGUIWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", Action.m_key.KeyChar, Action.m_key.KeyCode, Action.wID.ToString)

                    _ClickfinderCurrentDate = Today
                    If Not _DaysProgress.Percentage = 1 * 100 / (CPGsettings.OverviewMaxDays + 1) Then
                        _DaysProgress.Percentage = 1 * 100 / (CPGsettings.OverviewMaxDays + 1)
                    End If
                    Translator.SetProperty("#CurrentDate", getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy"))

                    Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowHighlightsProgressBar)
                    _ProgressBarThread.Start()

                    Dim _ProgressBarThread2 As New Threading.Thread(AddressOf ShowMoviesProgressBar)
                    _ProgressBarThread2.Start()

                    AbortRunningThreads()

                    _ThreadFillMovieList = New Thread(AddressOf FillMovieList)
                    _ThreadFillHighlightsList = New Thread(AddressOf FillHighlightsList)
                    _ThreadFillMovieList.IsBackground = True
                    _ThreadFillHighlightsList.IsBackground = True
                    _ThreadFillMovieList.Start()
                    _ThreadFillHighlightsList.Start()
                End If

            End If


            MyBase.OnAction(Action)
        End Sub

        Protected Overrides Sub OnPageDestroy(ByVal new_windowId As Integer)

            RememberLastFocusedItem()
            _ProgressPercentagValue = _DaysProgress.Percentage

            AbortRunningThreads()

            'Dispose()
            'AllocResources()

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

            If control Is _MovieList Or control Is _HighlightsList Then
                If actionType = MediaPortal.GUI.Library.Action.ActionType.ACTION_SELECT_ITEM Then
                    Action_SelectItem()
                End If
            End If
        End Sub

        Private Sub Action_SelectItem()

            RememberLastFocusedItem()

            If _MovieList.IsFocused = True Then
                ListControlClick(_MovieList.SelectedListItem.ItemId)
            End If

            If _HighlightsList.IsFocused = True Then
                'Falls im Label2 Translation.NewLabel gefunden -> Series Context Menu
                If _HighlightsList.SelectedListItem.Label2 = Translation.NewLabel Then
                    ShowSeriesContextMenu(_HighlightsList.SelectedListItem.TVTag, True)
                Else
                    ListControlClick(_HighlightsList.SelectedListItem.ItemId)
                End If
            End If
        End Sub

#End Region

#Region "Functions"

        Private Sub FillMovieList()

            Try
                MyLog.Debug("[HighlightsGUIWindow] [FillMovieList]: Thread started")

                Dim _TopMoviesOfDayList As New List(Of TVMovieProgram)
                Dim _ShowTagesTipp As Boolean = False
                Dim _ItemCounter As Integer = 0

                ''MovieListe + Bilder löschen
                _MovieList.Visible = False
                _MovieList.Clear()
                For i = 1 To 6
                    Translator.SetProperty("#MovieListTvMovieStar" & i, "")
                    Translator.SetProperty("#MovieListImage" & i, "")
                    Translator.SetProperty("#MovieListRatingStar" & i, 0)
                Next

                'SQLstring: Alle Movies (2 > TvMovieBewertung < 6), inkl. TagesTipps des Tages laden
                Dim _SQLstring As String = _
                    "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                    "WHERE TvMovieBewertung > 2 AND TvMovieBewertung < 6 " & _
                    "AND startTime > " & MySqlDate(_ClickfinderCurrentDate.AddHours(CPGsettings.OverviewShowHighlightsAfter)) & " " & _
                    "AND startTime < " & MySqlDate(_ClickfinderCurrentDate.AddDays(1).AddHours(0).AddMinutes(0)) & " " & _
                    "AND genre NOT LIKE '%Serie%' " & _
                    "AND genre NOT LIKE '%Sitcom%' " & _
                    "AND genre NOT LIKE '%Zeichentrick%'"

                'SqlString: Orderby TvMovieBewertung & Limit
                _SQLstring = Helper.AppendSqlLimit(_SQLstring & Helper.ORDERBYtvMovieBewertung, 25)

                'List: Daten laden
                _SQLstring = Replace(_SQLstring, " * ", " TVMovieProgram.idProgram, TVMovieProgram.Action, TVMovieProgram.Actors, TVMovieProgram.BildDateiname, TVMovieProgram.Country, TVMovieProgram.Cover, TVMovieProgram.Describtion, TVMovieProgram.Dolby, TVMovieProgram.EpisodeImage, TVMovieProgram.Erotic, TVMovieProgram.FanArt, TVMovieProgram.Feelings, TVMovieProgram.FileName, TVMovieProgram.Fun, TVMovieProgram.HDTV, TVMovieProgram.idEpisode, TVMovieProgram.idMovingPictures, TVMovieProgram.idSeries, TVMovieProgram.idTVMovieProgram, TVMovieProgram.idVideo, TVMovieProgram.KurzKritik, TVMovieProgram.local, TVMovieProgram.needsUpdate, TVMovieProgram.Regie, TVMovieProgram.Requirement, TVMovieProgram.SeriesPosterImage, TVMovieProgram.ShortDescribtion, TVMovieProgram.Tension, TVMovieProgram.TVMovieBewertung, TVMovieProgram.Year ")
                Dim _SQLstate1 As SqlStatement = Broker.GetStatement(_SQLstring)
                _TopMoviesOfDayList = ObjectFactory.GetCollection(GetType(TVMovieProgram), _SQLstate1.Execute())

                MyLog.Debug("[HighlightsGUIWindow] [FillMovieList]:  {0} Movie Highlights loaded from Database", _TopMoviesOfDayList.Count)

                'List: TagesTipp extrahieren
                Dim _TagesTippList As New List(Of TVMovieProgram)
                If CPGsettings.OverviewShowTagesTipp = True Then
                    Try
                        _TagesTippList = _TopMoviesOfDayList.FindAll(Function(x As TVMovieProgram) _
                                        x.TVMovieBewertung = 4).GetRange(0, 1)

                        'TagesTipp vorhanden -> Listcontrol
                        If _TagesTippList.Count > 0 Then
                            _ShowTagesTipp = True

                            _TopMoviesOfDayList.RemoveAll(Function(x As TVMovieProgram) x.TVMovieBewertung = 4)

                            _MovieList.ListItems.AddRange(_TagesTippList.ConvertAll(Of GUIListItem)(New Converter(Of TVMovieProgram, GUIListItem)(Function(c As TVMovieProgram) New GUIListItem() With { _
                                            .ItemId = c.idProgram, _
                                            .Path = c.ReferencedProgram.ReferencedChannel.DisplayName, _
                                            .Label = c.ReferencedProgram.Title, _
                                            .Label2 = GuiLayout.TimeLabel(c), _
                                            .Label3 = GuiLayout.InfoLabel(c), _
                                            .PinImage = GuiLayout.RecordingStatus(c.ReferencedProgram) _
                                           })))

                            Translator.SetProperty("#MovieListTvMovieStar" & 1, GuiLayout.TvMovieStar(_TagesTippList(0)))
                            Translator.SetProperty("#MovieListImage" & 1, GuiLayout.Image(_TagesTippList(0)))
                            Translator.SetProperty("#MovieListRatingStar" & 1, GuiLayout.ratingStar(_TagesTippList(0).ReferencedProgram))

                            MyLog.Debug("[HighlightsGUIWindow] [FillMovieList]: TagesTipp added to Listcontrol ({0} ,{1} ,{2})", _TagesTippList(0).ReferencedProgram.Title, _TagesTippList(0).ReferencedProgram.ReferencedChannel.DisplayName, _TagesTippList(0).ReferencedProgram.StartTime)
                        Else
                            MyLog.Warn("[HighlightsGUIWindow] [FillMovieList]: No TagesTipp - {0}", _ClickfinderCurrentDate)
                        End If

                    Catch ex As Exception
                        'Tipp für date existiert nicht
                        MyLog.Warn("[HighlightsGUIWindow] [FillMovieList]: No TagesTipp - {0}", _ClickfinderCurrentDate)
                    End Try
                End If

                'List: nur standard TvGruppe
                _TopMoviesOfDayList = _TopMoviesOfDayList.FindAll(Function(x As TVMovieProgram) x.ReferencedProgram.ReferencedChannel.GroupNames.Contains(CPGsettings.StandardTvGroup))

                'List: nicht local = 0 -> sofern in Settings
                If CPGsettings.OverviewShowLocalMovies = True Then
                    _TopMoviesOfDayList = _TopMoviesOfDayList.FindAll(Function(p As TVMovieProgram) p.local = False)
                End If

                MyLog.Debug("[HighlightsGUIWindow] [FillMovieList]: {0} Movie Highlights filtered by standard TvGroup and local = {1}", _TopMoviesOfDayList.Count, CPGsettings.OverviewShowLocalMovies)

                'List: sortieren
                Select Case (CPGsettings.OverviewMovieSort)
                    Case Is = SortMethode.startTime.ToString
                        _TopMoviesOfDayList.Sort(New TVMovieProgram_SortByStartTime)
                    Case Is = SortMethode.TvMovieStar.ToString
                        _TopMoviesOfDayList.Sort(New TVMovieProgram_SortByTvMovieBewertung)
                    Case Is = SortMethode.RatingStar.ToString
                        _TopMoviesOfDayList.Sort(New TVMovieProgram_SortByRating)
                End Select

                MyLog.Debug("[HighlightsGUIWindow] [FillMovieList]: {0} Movie Highlights sorted by {1}", _TopMoviesOfDayList.Count, CPGsettings.OverviewMovieSort)

                'GroupBy: Title And EpisodeName (doppelte Einträge raus)
                _TopMoviesOfDayList = _TopMoviesOfDayList.Distinct(New TVMovieProgram_GroupByTitleAndEpisodeName()).ToList

                If _ShowTagesTipp = True Then
                    _TopMoviesOfDayList = _TopMoviesOfDayList.GetRange(0, 5)
                    _ItemCounter = 2
                Else
                    _TopMoviesOfDayList = _TopMoviesOfDayList.GetRange(0, 6)
                    _ItemCounter = 1
                End If

                For Each _TvMovieProgram In _TopMoviesOfDayList
                    Translator.SetProperty("#MovieListRatingStar" & _ItemCounter, GuiLayout.ratingStar(_TvMovieProgram.ReferencedProgram))
                    Translator.SetProperty("#MovieListTvMovieStar" & _ItemCounter, GuiLayout.TvMovieStar(_TvMovieProgram))
                    Translator.SetProperty("#MovieListImage" & _ItemCounter, GuiLayout.Image(_TvMovieProgram))

                    AddListControlItem(_MovieList, _TvMovieProgram.ReferencedProgram.IdProgram, _TvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName, _TvMovieProgram.ReferencedProgram.Title, GuiLayout.TimeLabel(_TvMovieProgram), GuiLayout.InfoLabel(_TvMovieProgram), , , GuiLayout.RecordingStatus(_TvMovieProgram.ReferencedProgram))
                    _ItemCounter = _ItemCounter + 1
                Next

                MyLog.Debug("[HighlightsGUIWindow] [FillMovieList]: {0} Movie Highlights added to Listcontrol", _TopMoviesOfDayList.Count)

                _MoviesProgressBar.Visible = False
                _MovieList.Visible = True

                If _ThreadFillHighlightsList.IsAlive = False Then
                    GUIListControl.SelectItemControl(GetID, _LastFocusedControlID, _LastFocusedItemIndex)
                    GUIListControl.FocusControl(GetID, _LastFocusedControlID)
                End If

                MyLog.Debug("[HighlightsGUIWindow] [FillMovieList]: Thread finished")
                MyLog.Debug("")

                'log Ausgabe abfangen, falls der Thread abgebrochen wird
            Catch ex2 As ThreadAbortException ' Ignore this exception
                MyLog.Debug("[HighlightsGUIWindow] [FillMovieList]: --- THREAD ABORTED ---")
                MyLog.Debug("")
            Catch ex2 As GentleException
            Catch ex2 As Exception
                MyLog.Error("[HighlightsGUIWindow] [FillMovieList]: exception err:" & ex2.Message & " stack:" & ex2.StackTrace)
            End Try
        End Sub
        Private Sub FillHighlightsList()

            MyLog.Debug("")
            MyLog.Debug("[HighlightsGUIWindow] [FillHighlightsList]: Thread started")

            Try
                'HighlightsList + Bilder löschen
                _HighlightsList.Visible = False
                _HighlightsList.Clear()
                _HighlightsList.AllocResources()

                Dim _LoadHighlightsDataList As New List(Of TVMovieProgram)
                Dim _HighLightsItemsList As New List(Of TVMovieProgram)
                Dim _RepeatsScheduledRecordings As New List(Of Program)

                'SQLstring: Alle Highlights & neuen Episoden des Tages laden
                Dim _SQLstring As String = _
                "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                     "WHERE startTime > " & MySqlDate(_ClickfinderCurrentDate) & " " & _
                     "AND startTime < " & MySqlDate(_ClickfinderCurrentDate.AddDays(1).AddHours(0).AddMinutes(0)) & " " & _
                     "AND ((TvMovieBewertung = 6 " & _
                     "AND genre NOT LIKE '%Zeichentrick%' " & _
                     "AND genre NOT LIKE '%Serie%' " & _
                     "AND genre NOT LIKE '%Sitcom%' " & _
                     "AND genre NOT LIKE '%Zeichentrick%') " & _
                     "OR (local = 0 AND idSeries > 0)) " & _
                                Helper.ORDERBYstartTime

                'List: Daten laden
                _SQLstring = Replace(_SQLstring, " * ", " TVMovieProgram.idProgram, TVMovieProgram.Action, TVMovieProgram.Actors, TVMovieProgram.BildDateiname, TVMovieProgram.Country, TVMovieProgram.Cover, TVMovieProgram.Describtion, TVMovieProgram.Dolby, TVMovieProgram.EpisodeImage, TVMovieProgram.Erotic, TVMovieProgram.FanArt, TVMovieProgram.Feelings, TVMovieProgram.FileName, TVMovieProgram.Fun, TVMovieProgram.HDTV, TVMovieProgram.idEpisode, TVMovieProgram.idMovingPictures, TVMovieProgram.idSeries, TVMovieProgram.idTVMovieProgram, TVMovieProgram.idVideo, TVMovieProgram.KurzKritik, TVMovieProgram.local, TVMovieProgram.needsUpdate, TVMovieProgram.Regie, TVMovieProgram.Requirement, TVMovieProgram.SeriesPosterImage, TVMovieProgram.ShortDescribtion, TVMovieProgram.Tension, TVMovieProgram.TVMovieBewertung, TVMovieProgram.Year ")
                Dim _SQLstate3 As SqlStatement = Broker.GetStatement(_SQLstring)
                _LoadHighlightsDataList = ObjectFactory.GetCollection(GetType(TVMovieProgram), _SQLstate3.Execute())

                'Sofern TvMovie Series aktiviert
                If CPGsettings.TvMovieImportTvSeriesInfos = True Then
                    'List: NewEpisodesList übergeben 
                    _NewEpisodesList = _LoadHighlightsDataList _
                                        .FindAll(Function(x As TVMovieProgram) x.local = False And x.idSeries > 0)

                    'Nach Rec State sortieren
                    _NewEpisodesList.Sort(New TVMovieProgram_SortByAllRecordingStates)

                    'Alle doppelten Einträge raus
                    _NewEpisodesList = _NewEpisodesList.Distinct(New TVMovieProgram_GroupByTitleAndEpisodeName).ToList

                    Dim tmp As List(Of TVMovieProgram) = _NewEpisodesList.Distinct(New TVMovieProgram_GroupByIdSeries).ToList
                    tmp.Sort(New TVMovieProgram_SortByTitle)
                    'Alle items, GroupBy idSeries durchlaufen -> Listcontrol übergeben + zählen (_NewEpisodesList)
                    For Each _Episode In tmp
                        Dim _RecordingStatus As String = String.Empty

                        _RecordingStatus = GuiLayout.RecordingStatus(_Episode.ReferencedProgram)

                        'Falls nicht als Aufnahme geplant, prüfen ob an anderem Tag / Uhrzeit
                        If _RecordingStatus = String.Empty Then
                            _SQLstring = _
                                    "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                    "WHERE idSeries = " & _Episode.idSeries & " " & _
                                    "AND local = 0 " & _
                                    "AND episodeName = '" & TVSeriesDB.allowedSigns(_Episode.ReferencedProgram.EpisodeName) & "' " & _
                                    "ORDER BY state DESC"

                            _SQLstring = Helper.AppendSqlLimit(_SQLstring, 1)

                            _SQLstring = Replace(_SQLstring, " * ", " Program.IdProgram, Program.Classification, Program.Description, Program.EndTime, Program.EpisodeName, Program.EpisodeNum, Program.EpisodePart, Program.Genre, Program.IdChannel, Program.OriginalAirDate, Program.ParentalRating, Program.SeriesNum, Program.StarRating, Program.StartTime, Program.state, Program.Title ")
                            Dim _SQLstate4 As SqlStatement = Broker.GetStatement(_SQLstring)
                            _RepeatsScheduledRecordings = ObjectFactory.GetCollection(GetType(Program), _SQLstate4.Execute())

                            If _RepeatsScheduledRecordings.Count > 0 Then
                                _RecordingStatus = GuiLayout.RecordingStatus(_RepeatsScheduledRecordings.Item(0))

                                'MsgBox("episode: " & _Episode.ReferencedProgram.Title & " - " & _Episode.ReferencedProgram.EpisodeName & vbNewLine & _
                                '       "recState: " & _RecordingStatus)

                                Select Case (_RecordingStatus)
                                    Case Is = "tvguide_record_button.png"
                                        _RecordingStatus = "ClickfinderPG_recOnce.png"
                                    Case Is = "tvguide_recordserie_button.png"
                                        _RecordingStatus = "ClickfinderPG_recSeries.png"
                                    Case Else
                                        _RecordingStatus = String.Empty
                                End Select
                            End If
                        End If


                        Dim _idSeries As Integer = _Episode.idSeries
                        AddListControlItem(_HighlightsList, _Episode.idProgram, _Episode.ReferencedProgram.ReferencedChannel.DisplayName, _Episode.ReferencedProgram.Title, Translation.NewLabel, _NewEpisodesList.FindAll(Function(x As TVMovieProgram) x.idSeries = _idSeries).Count & " " & Translation.NewEpisodeFound, Config.GetFile(Config.Dir.Thumbs, "MPTVSeriesBanners\") & _Episode.SeriesPosterImage, , _RecordingStatus, _Episode.idSeries)
                    Next
                End If

                'List: HighLightsItemsList übergeben + filtern local = 0, startTime > OverviewShowHighlightsAfter, StandardTvGroup
                _HighLightsItemsList = _LoadHighlightsDataList _
                                        .FindAll(Function(y As TVMovieProgram) y.idSeries = 0 _
                                                 And y.ReferencedProgram.StartTime > _ClickfinderCurrentDate.AddHours(CPGsettings.OverviewShowHighlightsAfter) _
                                                 And y.ReferencedProgram.ReferencedChannel.GroupNames.Contains(CPGsettings.StandardTvGroup)) _
                                        .Distinct(New TVMovieProgram_GroupByTitleAndEpisodeName).ToList

                ' _HighlightsList.ListItems.AddRange(_HighLightsItemsList.ConvertAll(Of GUIListItem)(New Converter(Of TVMovieProgram, GUIListItem)(Function(c As TVMovieProgram) New GUIListItem() With { _
                ' .ItemId = c.idProgram, _
                ' .Path = c.ReferencedProgram.ReferencedChannel.DisplayName, _
                ' .Label = c.ReferencedProgram.Title, _
                ' .Label2 = GuiLayout.TimeLabel(c), _
                ' .IconImage = Config.GetFile(Config.Dir.Thumbs, "tv\logos\") & Replace(c.ReferencedProgram.ReferencedChannel.DisplayName, "/", "_") & ".png", _
                ' .PinImage = GuiLayout.RecordingStatus(c.ReferencedProgram) _
                '})))

                For Each _TvMovieProgram In _HighLightsItemsList
                    Dim _timeLabel As String = Format(_TvMovieProgram.ReferencedProgram.StartTime.Hour, "00") & _
                                        ":" & Format(_TvMovieProgram.ReferencedProgram.StartTime.Minute, "00")

                    Dim _infoLabel As String = String.Empty
                    If String.IsNullOrEmpty(_TvMovieProgram.ReferencedProgram.EpisodeName) Then
                        _infoLabel = _TvMovieProgram.ReferencedProgram.Genre
                    Else
                        _infoLabel = _TvMovieProgram.ReferencedProgram.EpisodeName
                    End If

                    'Image zunächst auf SenderLogo festlegen
                    Dim _imagepath As String = Config.GetFile(Config.Dir.Thumbs, "tv\logos\") & Replace(_TvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName, "/", "_") & ".png"

                    If CPGsettings.UseSportLogos = True Then
                        _imagepath = UseSportLogos(_TvMovieProgram.ReferencedProgram.Title, _imagepath)
                    End If

                    AddListControlItem(_HighlightsList, _TvMovieProgram.ReferencedProgram.IdProgram, _TvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName, _TvMovieProgram.ReferencedProgram.Title, _timeLabel, _infoLabel, _imagepath, , GuiLayout.RecordingStatus(_TvMovieProgram.ReferencedProgram))
                Next

                _HighlightsProgressBar.Visible = False
                _HighlightsList.Visible = True

                MyLog.Debug("[HighlightsGUIWindow] [FillHighlightsList]: Thread finished")
                MyLog.Debug("")

                If _ThreadFillMovieList.IsAlive = False Then
                    GUIListControl.SelectItemControl(GetID, _LastFocusedControlID, _LastFocusedItemIndex)
                    GUIListControl.FocusControl(GetID, _LastFocusedControlID)
                End If

                'log Ausgabe abfangen, falls der Thread abgebrochen wird
            Catch ex2 As ThreadAbortException ' Ignore this exception
                MyLog.Debug("[HighlightsGUIWindow] [FillHighlightsList]: --- THREAD ABORTED ---")
            Catch ex As GentleException
            Catch ex2 As Exception
                MyLog.Error("[HighlightsGUIWindow] [FillHighlightsList]: exception err:" & ex2.Message & " stack:" & ex2.StackTrace)
            End Try

        End Sub

        Private Shared Function InfoLabelFormat( _
    ByVal x As TVMovieProgram) As Integer
            If String.IsNullOrEmpty(x.ReferencedProgram.EpisodeName) Then
                Return x.ReferencedProgram.Genre
            Else
                Return x.ReferencedProgram.EpisodeName
            End If
        End Function

        'ProgresBar paralell anzeigen
        Private Sub ShowHighlightsProgressBar()
            _HighlightsProgressBar.Visible = True
        End Sub
        Private Sub ShowMoviesProgressBar()
            _MoviesProgressBar.Visible = True
        End Sub

        Private Sub AbortRunningThreads()

            Try
                If _ThreadFillMovieList.IsAlive = True Then
                    _ThreadFillMovieList.Abort()
                    _ThreadFillHighlightsList.Abort()
                ElseIf _ThreadFillHighlightsList.IsAlive = True Then
                    _ThreadFillHighlightsList.Abort()
                    _ThreadFillMovieList.Abort()
                ElseIf _ThreadFillMovieList.IsAlive = True And _ThreadFillHighlightsList.IsAlive = True Then
                    _ThreadFillMovieList.Abort()
                    _ThreadFillHighlightsList.Abort()
                End If

            Catch ex3 As Exception ' Ignore this exception
                'Eventuell auftretende Exception abfangen
                ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
            End Try
        End Sub

        Private Sub RememberLastFocusedItem()

            If _MovieList.IsFocused Then
                'MsgBox(_MovieList.SelectedListItemIndex)
                _LastFocusedItemIndex = _MovieList.SelectedListItemIndex
                _LastFocusedControlID = _MovieList.GetID
            ElseIf _HighlightsList.IsFocused Then
                _LastFocusedItemIndex = _HighlightsList.SelectedListItemIndex
                _LastFocusedControlID = _HighlightsList.GetID
            Else
                _LastFocusedItemIndex = 0
                _LastFocusedControlID = _MovieList.GetID
            End If
        End Sub

#End Region

#Region "MediaPortal Funktionen / Dialogs"
        Private Sub ShowSeriesContextMenu(ByVal idSeries As Integer, Optional ByVal SelectItem As Boolean = False)
            Try

                Dim _MenuEpisodesList As List(Of TVMovieProgram) = _NewEpisodesList.FindAll(Function(x As TVMovieProgram) x.idSeries = idSeries)
                Dim _MenuNotLocalEpisodesList As New List(Of TVMovieProgram)

                'Prüfen ob noch lokal = false
                For Each _episode In _MenuEpisodesList
                    CheckSeriesLocalStatus(_episode)
                    If _episode.local = False Then
                        _MenuNotLocalEpisodesList.Add(_episode)
                    End If
                Next

                If _MenuNotLocalEpisodesList.Count > 0 Then
                    'Nur ein Eintrag vorhanden -> ContextMenu oder GuiDetails aufrufen
                    If _MenuNotLocalEpisodesList.Count = 1 Then

                        If SelectItem = True Then
                            ListControlClick(_MenuNotLocalEpisodesList(0).idProgram)
                        Else
                            ShowHighlightsMenu(_MenuNotLocalEpisodesList(0).idProgram)
                        End If
                        Exit Sub
                    Else
                        'Mehrere Einträge -> dlgcontext Listcontrol zeigen
                        Dim _idProgramContainer As Dictionary(Of Integer, Integer) = New Dictionary(Of Integer, Integer)
                        Dim _counter As Integer = 0

                        Dim dlgContext As GUIDialogSelect2Custom = CType(GUIWindowManager.GetWindow(CType(1656544655, Integer)), GUIDialogSelect2Custom)
                        dlgContext.Reset()
                        dlgContext.SetHeading(_MenuNotLocalEpisodesList(0).ReferencedProgram.Title)
                        'dlgContext.ShowQuickNumbers = False

                        MyLog.Debug("[HighlightsGuiWindow] [ShowSeriesContextMenu]: idprogram = {0}, title = {1}", _MenuNotLocalEpisodesList(0).ReferencedProgram.IdProgram, _MenuNotLocalEpisodesList(0).ReferencedProgram.Title)

                        _MenuNotLocalEpisodesList.Sort(New TVMovieProgram_SortByStartTime)

                        For Each _TVMovieProgram In _MenuNotLocalEpisodesList
                            Dim _RecordingStatus As String = String.Empty
                            Dim lItemEpisode As New GUIListItem


                            lItemEpisode.Label = _TVMovieProgram.ReferencedProgram.EpisodeName
                            lItemEpisode.Label2 = Format(_TVMovieProgram.ReferencedProgram.StartTime.Hour, "00") & _
                                                    ":" & Format(_TVMovieProgram.ReferencedProgram.StartTime.Minute, "00") & " - " & _TVMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName

                            'Falls die EPisode als Neu gekeinzeichnet ist, aber keine SxxExx nummer hat -> nicht identifiziert worden
                            If String.IsNullOrEmpty(_TVMovieProgram.ReferencedProgram.SeriesNum) = True Or String.IsNullOrEmpty(_TVMovieProgram.ReferencedProgram.EpisodeNum) Then
                                lItemEpisode.Label3 = Translation.EpisodeNotIdentify
                            Else
                                lItemEpisode.Label3 = Translation.Season & " " & Format(CInt(_TVMovieProgram.ReferencedProgram.SeriesNum), "00") & ", " & Translation.Episode & Format(CInt(_TVMovieProgram.ReferencedProgram.EpisodeNum), "00")
                            End If

                            lItemEpisode.IconImage = Config.GetFile(Config.Dir.Thumbs, "MPTVSeriesBanners\") & _TVMovieProgram.SeriesPosterImage
                            _RecordingStatus = GuiLayout.RecordingStatus(_TVMovieProgram.ReferencedProgram)

                            'Falls nicht als Aufnahme geplant, prüfen ob an anderem Tag
                            If _RecordingStatus = String.Empty Then
                                Dim _RepeatsScheduledRecordings As List(Of Program)
                                Dim _SQLstring As String = _
                                        "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                        "WHERE idSeries = " & _TVMovieProgram.idSeries & " " & _
                                        "AND local = 0 " & _
                                        "AND episodeName = '" & TVSeriesDB.allowedSigns(_TVMovieProgram.ReferencedProgram.EpisodeName) & "' " & _
                                        "ORDER BY state DESC"

                                _SQLstring = Helper.AppendSqlLimit(_SQLstring, 1)

                                _SQLstring = Replace(_SQLstring, " * ", " Program.IdProgram, Program.Classification, Program.Description, Program.EndTime, Program.EpisodeName, Program.EpisodeNum, Program.EpisodePart, Program.Genre, Program.IdChannel, Program.OriginalAirDate, Program.ParentalRating, Program.SeriesNum, Program.StarRating, Program.StartTime, Program.state, Program.Title ")
                                Dim _SQLstate4 As SqlStatement = Broker.GetStatement(_SQLstring)
                                _RepeatsScheduledRecordings = ObjectFactory.GetCollection(GetType(Program), _SQLstate4.Execute())

                                If _RepeatsScheduledRecordings.Count > 0 Then
                                    _RecordingStatus = GuiLayout.RecordingStatus(_RepeatsScheduledRecordings.Item(0))

                                    Select Case (_RecordingStatus)
                                        Case Is = "tvguide_record_button.png"
                                            _RecordingStatus = "ClickfinderPG_recOnce.png"
                                        Case Is = "tvguide_recordserie_button.png"
                                            _RecordingStatus = "ClickfinderPG_recSeries.png"
                                        Case Else
                                            _RecordingStatus = String.Empty
                                    End Select
                                End If
                            End If

                            _idProgramContainer.Add(_counter, _TVMovieProgram.idProgram)
                            lItemEpisode.PinImage = _RecordingStatus
                            _counter = _counter + 1
                            dlgContext.Add(lItemEpisode)
                            lItemEpisode.Dispose()
                        Next

                        dlgContext.DoModal(GetID)

                        If SelectItem = True Then
                            ListControlClick(_idProgramContainer.Item(dlgContext.SelectedLabel))
                        ElseIf SelectItem = False Then
                            ShowHighlightsMenu(_idProgramContainer.Item(dlgContext.SelectedLabel))
                        End If

                    End If

                Else
                    'Alle Episoden inzwischen lokal, HighlightsList refresh
                    Helper.Notify("Episode(n) lokal gefunden", Config.GetFile(Config.Dir.Thumbs, "MPTVSeriesBanners\") & _MenuEpisodesList(0).SeriesPosterImage)

                    Dim _ProgressBarThread2 As New Threading.Thread(AddressOf ShowHighlightsProgressBar)
                    _ProgressBarThread2.Start()

                    AbortRunningThreads()

                    _ThreadFillHighlightsList = New Thread(AddressOf FillHighlightsList)
                    _ThreadFillHighlightsList.Start()
                End If

            Catch ex As Exception
                MyLog.Error("[HighlightsGUIWindow] [ShowSeriesContextMenu]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try
        End Sub
        Private Sub ShowHighlightsMenu(ByVal idProgram As Integer)

            Dim _Menu_AllMovies As String = String.Empty
            Dim _Menu_AllHighlights As String = String.Empty

            Try

                Dim dlgContext As GUIDialogMenu = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_MENU, Integer)), GUIDialogMenu)
                dlgContext.Reset()

                Dim _Program As Program = Program.Retrieve(idProgram)
                dlgContext.ShowQuickNumbers = True
                dlgContext.SetHeading(_Program.Title)

                MyLog.Debug("[HighlightsGuiWindow] [ShowHighlightsMenu]: idprogram = {0}, title = {1}", _Program.IdProgram, _Program.Title)

                'Alle Highlights + Serien des Tages
                Dim lItemHighlights As New GUIListItem
                _Menu_AllHighlights = Translation.allHighlightsAt & " " & getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy")
                lItemHighlights.Label = Translation.allHighlightsAt & " " & getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy")
                dlgContext.Add(lItemHighlights)
                lItemHighlights.Dispose()

                'Kategorien des Tages
                Dim lItemCategories As New GUIListItem
                lItemCategories.Label = Translation.allCategoriesAt & " " & getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy")
                dlgContext.Add(lItemCategories)
                lItemCategories.Dispose()

                'Program gleichen Genres anzeigen
                Dim lItemCategorie As New GUIListItem
                lItemCategorie.Label = Translation.SameGenre & " " & _Program.Genre
                dlgContext.Add(lItemCategorie)
                lItemCategorie.Dispose()

                'Neue Episoden
                Dim lItemLocalSeries As New GUIListItem
                lItemLocalSeries.Label = Translation.NewEpisodes & " " & getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy")
                dlgContext.Add(lItemLocalSeries)
                lItemLocalSeries.Dispose()

                'Action SubMenu
                Dim lItemActionOn As New GUIListItem
                lItemActionOn.Label = Translation.action
                dlgContext.Add(lItemActionOn)
                lItemActionOn.Dispose()
                dlgContext.DoModal(GetID)

                Select Case dlgContext.SelectedLabel
                    Case Is = 0
                        MyLog.Debug("[HighlightsGuiWindow] [ShowHighlightsMenu]:  selected -> " & Translation.allHighlightsAt & " " & getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy"))
                        MyLog.Debug("")

                        ItemsGuiWindow.SetGuiProperties("Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                                        "WHERE startTime >= " & MySqlDate(_ClickfinderCurrentDate) & " " & _
                                                        "AND startTime <= " & MySqlDate(_ClickfinderCurrentDate.AddHours(24)) & " " & _
                                                        "AND (TVMovieBewertung = 6 " & _
                                                        "OR idSeries > 0) " & _
                                                        Helper.ORDERBYstartTime)
                        Translator.SetProperty("#ItemsLeftListLabel", Translation.allHighlightsAt & " " & getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy"))
                        GUIWindowManager.ActivateWindow(1656544653)

                    Case Is = 1
                        MyLog.Debug("[HighlightsGuiWindow] [ShowHighlightsMenu]:  selected -> " & Translation.allCategoriesAt & " " & getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy"))
                        MyLog.Debug("")

                        GUIWindowManager.ActivateWindow(1656544654, "CPG.Day:" & _Program.StartTime.Date)
                    Case Is = 2
                        MyLog.Debug("[HighlightsGuiWindow] [ShowHighlightsMenu]:  selected -> " & Translation.SameGenre & " " & _Program.Genre)
                        MyLog.Debug("")
                        ItemsGuiWindow.SetGuiProperties("Select * FROM program LEFT JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                        "WHERE genre LIKE '" & _Program.Genre & "' " & _
                        "AND startTime > " & MySqlDate(_ClickfinderCurrentDate) & " " & _
                        "AND startTime < " & MySqlDate(_ClickfinderCurrentDate.AddHours(24)) & " " & _
                        Helper.ORDERBYstartTime)

                        Translator.SetProperty("#ItemsLeftListLabel", _Program.Genre & ": " & getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy"))
                        GUIWindowManager.ActivateWindow(1656544653)

                    Case Is = 3
                        MyLog.Debug("[HighlightsGuiWindow] [ShowHighlightsMenu]:  selected -> " & Translation.NewEpisodes & " " & getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy"))
                        MyLog.Debug("")

                        ItemsGuiWindow.SetGuiProperties("Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                    "WHERE idSeries > 0 " & _
                                    "AND local = 0 " & _
                                    "AND startTime > " & MySqlDate(_ClickfinderCurrentDate) & " " & _
                                    "AND startTime < " & MySqlDate(_ClickfinderCurrentDate.AddHours(24)) & " " & _
                                    "ORDER BY title ASC, seriesNum ASC, episodeNum ASC, startTime ASC", , SortMethode.Series.ToString)

                        Translator.SetProperty("#ItemsLeftListLabel", Translation.NewEpisodes & " " & getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy"))
                        GUIWindowManager.ActivateWindow(1656544653)
                    Case Is = 4
                        MyLog.Debug("[HighlightsGuiWindow] [ShowHighlightsMenu]:  selected -> action menu")
                        MyLog.Debug("")
                        ShowActionMenu(_Program)
                    Case Else
                        MyLog.Debug("[HighlightsGuiWindow] [ShowHighlightsMenu]: exit")
                        MyLog.Debug("")
                End Select

                dlgContext.Dispose()
                dlgContext.AllocResources()

            Catch ex As Exception
                MyLog.Error("[HighlightsGUIWindow] [ShowHighlightsContextMenu]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try
        End Sub
        Private Sub ShowMoviesMenu(ByVal idProgram As Integer)
            Try

                Dim dlgContext As GUIDialogMenu = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_MENU, Integer)), GUIDialogMenu)
                dlgContext.Reset()

                Dim _Program As Program = Program.Retrieve(idProgram)
                dlgContext.ShowQuickNumbers = True
                dlgContext.SetHeading(_Program.Title)

                MyLog.Debug("[HighlightsGuiWindow] [ShowMoviesMenu]: idprogram = {0}, title = {1}", _Program.IdProgram, _Program.Title)

                'Sort SubMenu
                Dim lItemSort As New GUIListItem
                lItemSort.Label = Translation.Sortby
                dlgContext.Add(lItemSort)
                lItemSort.Dispose()

                'Alle Movies des Tages
                Dim lItemMovies As New GUIListItem
                lItemMovies.Label = Translation.allMoviesAt & " " & getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy")
                dlgContext.Add(lItemMovies)
                lItemMovies.Dispose()

                'Kategorien des Tages
                Dim lItemCategories As New GUIListItem
                lItemCategories.Label = Translation.allCategoriesAt & " " & getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy")
                dlgContext.Add(lItemCategories)
                lItemCategories.Dispose()

                'Program gleichen Genres anzeigen
                Dim lItemCategorie As New GUIListItem
                lItemCategorie.Label = Translation.SameGenre & " " & _Program.Genre
                dlgContext.Add(lItemCategorie)
                lItemCategorie.Dispose()

                'lokale Filme
                Dim lItemLocalMovies As New GUIListItem
                lItemLocalMovies.Label = Translation.MyMovies & " " & getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy")
                dlgContext.Add(lItemLocalMovies)
                lItemLocalMovies.Dispose()

                'Action SubMenu
                Dim lItemActionOn As New GUIListItem
                lItemActionOn.Label = Translation.action
                dlgContext.Add(lItemActionOn)
                lItemActionOn.Dispose()

                dlgContext.DoModal(GetID)

                Select Case dlgContext.SelectedLabel
                    Case Is = 0
                        MyLog.Debug("[HighlightsGuiWindow] [ShowMoviesMenu]: selected -> sort menu")
                        ShowSortMenu()

                    Case Is = 1
                        MyLog.Debug("[HighlightsGuiWindow] [ShowMoviesMenu]:  selected -> " & Translation.allMoviesAt & " " & getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy"))
                        MyLog.Debug("")

                        ItemsGuiWindow.SetGuiProperties("Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                                            "WHERE startTime >= " & MySqlDate(_ClickfinderCurrentDate) & " " & _
                                                            "AND startTime <= " & MySqlDate(_ClickfinderCurrentDate.AddHours(24)) & " " & _
                                                            "AND TVMovieBewertung > 1 " & _
                                                            "AND NOT TVMovieBewertung = 6 " & _
                                                            "AND starRating > 1 " & _
                                                            "AND genre NOT LIKE '%Serie%' " & _
                                                            "AND genre NOT LIKE '%Sitcom%' " & _
                                                            "AND genre NOT LIKE '%Zeichentrick%' " & _
                                                            Helper.ORDERBYstartTime, 85)

                        Translator.SetProperty("#ItemsLeftListLabel", Translation.allMoviesAt & " " & getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy"))
                        GUIWindowManager.ActivateWindow(1656544653)

                    Case Is = 2
                        MyLog.Debug("[HighlightsGuiWindow] [ShowMoviesMenu]:  selected -> " & Translation.allCategoriesAt & " " & getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy"))
                        MyLog.Debug("")

                        GUIWindowManager.ActivateWindow(1656544654, "CPG.Day:" & _Program.StartTime.Date)

                    Case Is = 3
                        MyLog.Debug("[HighlightsGuiWindow] [ShowMoviesMenu]:  selected -> " & Translation.SameGenre & " " & _Program.Genre)
                        MyLog.Debug("")
                        ItemsGuiWindow.SetGuiProperties("Select * FROM program LEFT JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                        "WHERE genre LIKE '" & _Program.Genre & "' " & _
                        "AND startTime > " & MySqlDate(_ClickfinderCurrentDate) & " " & _
                        "AND startTime < " & MySqlDate(_ClickfinderCurrentDate.AddHours(24)) & " " & _
                        Helper.ORDERBYstartTime)

                        Translator.SetProperty("#ItemsLeftListLabel", _Program.Genre & ": " & getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy"))

                        GUIWindowManager.ActivateWindow(1656544653)

                    Case Is = 4
                        MyLog.Debug("[HighlightsGuiWindow] [ShowMoviesMenu]:  selected -> " & Translation.MyMovies & " " & getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy"))
                        MyLog.Debug("")

                        ItemsGuiWindow.SetGuiProperties("Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                                            "WHERE startTime >= " & MySqlDate(_ClickfinderCurrentDate.AddHours(0).AddMinutes(0)) & " " & _
                                                            "AND startTime <= " & MySqlDate(_ClickfinderCurrentDate.AddHours(24)) & " " & _
                                                            "AND TVMovieBewertung > 1 " & _
                                                            "AND NOT TVMovieBewertung = 6 " & _
                                                            "AND starRating > 1 " & _
                                                            "AND local = 1 " & _
                                                            "AND genre NOT LIKE '%Serie%' " & _
                                                            "AND genre NOT LIKE '%Sitcom%' " & _
                                                            "AND genre NOT LIKE '%Zeichentrick%' " & _
                                                            Helper.ORDERBYstartTime, 85)

                        Translator.SetProperty("#ItemsLeftListLabel", Translation.MyMovies & " " & getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy"))
                        GUIWindowManager.ActivateWindow(1656544653)

                    Case Is = 5
                        MyLog.Debug("[HighlightsGuiWindow] [ShowMoviesMenu]: selected -> action menu")
                        ShowActionMenu(_Program)

                    Case Else
                        MyLog.Debug("[HighlightsGuiWindow] [ShowMoviesMenu]: exit")
                        MyLog.Debug("")
                End Select

                dlgContext.Dispose()
                dlgContext.AllocResources()

            Catch ex As Exception
                MyLog.Error("[HighlightsGUIWindow] [ShowMovieMenu]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try
        End Sub
        Private Sub ShowSortMenu()
            Dim dlgContext As GUIDialogMenu = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_MENU, Integer)), GUIDialogMenu)
            dlgContext.Reset()

            'Program gleichen Genres anzeigen
            Dim lItemStartTime As New GUIListItem
            lItemStartTime.Label = Translation.SortStartTime
            dlgContext.Add(lItemStartTime)
            lItemStartTime.Dispose()

            Dim lItemTvMovie As New GUIListItem
            lItemTvMovie.Label = Translation.SortTvMovieStar
            dlgContext.Add(lItemTvMovie)
            lItemTvMovie.Dispose()

            Dim lItemRating As New GUIListItem
            lItemRating.Label = Translation.SortRatingStar
            dlgContext.Add(lItemRating)
            lItemRating.Dispose()

            dlgContext.DoModal(GetID)

            Dim setting As Setting = _layer.GetSetting("ClickfinderOverviewMovieSort", SortMethode.startTime.ToString)
            Select Case dlgContext.SelectedLabel
                Case Is = 0
                    setting.Value = SortMethode.startTime.ToString
                    MyLog.Debug("[HighlightsGuiWindow] [ShowSortMenu]: sorted by startTime")
                    MyLog.Debug("")
                Case Is = 1
                    setting.Value = SortMethode.TvMovieStar.ToString
                    MyLog.Debug("[HighlightsGuiWindow] [ShowSortMenu]: sorted by TvMovieStar")
                    MyLog.Debug("")
                Case Is = 2
                    setting.Value = SortMethode.RatingStar.ToString
                    MyLog.Debug("[HighlightsGuiWindow] [ShowSortMenu]: sorted by RatingStar")
                    MyLog.Debug("")
                Case Else
                    MyLog.Debug("[HighlightsGuiWindow] [ShowSortMenu]: exit")
                    MyLog.Debug("")
                    Exit Sub
            End Select
            setting.Persist()

            Dim _Thread1 As New Thread(AddressOf FillMovieList)
            _Thread1.Start()

            dlgContext.Dispose()
            dlgContext.AllocResources()

        End Sub
        Private Sub InfoMenu()
            Dim dlgContext As GUIDialogExif = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_EXIF, Integer)), GUIDialogExif)
            dlgContext.Reset()

            dlgContext.Dispose()
            dlgContext.AllocResources()

        End Sub

        Private Sub ShowNewEpisodesThread()

        End Sub
#End Region


    End Class

End Namespace
