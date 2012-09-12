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
        <SkinControlAttribute(2)> Protected _btnNow As GUIButtonControl = Nothing
        <SkinControlAttribute(3)> Protected _btnPrimeTime As GUIButtonControl = Nothing
        <SkinControlAttribute(4)> Protected _btnLateTime As GUIButtonControl = Nothing
        <SkinControlAttribute(5)> Protected _btnAllMovies As GUIButtonControl = Nothing
        <SkinControlAttribute(6)> Protected _btnHighlights As GUIButtonControl = Nothing
        <SkinControlAttribute(7)> Protected _btnPreview As GUIButtonControl = Nothing

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

        Private _LastFocusedItemIndex As Integer
        Private _LastFocusedControlID As Integer

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

            MyLog.Info("")
            MyLog.Info("")
            MyLog.Info("[HighlightsGuiWindow] -------------[OPEN]-------------")


            Try
                Translator.SetProperty("#CurrentDate", Translation.Loading)

                _DaysProgress.Percentage = _ProgressPercentagValue
                If _ClickfinderCurrentDate = Nothing Then
                    _ClickfinderCurrentDate = Today
                    If Not _DaysProgress.Percentage = 1 * 100 / (CDbl(_layer.GetSetting("ClickfinderOverviewMaxDays", "10").Value) + 1) Then
                        _DaysProgress.Percentage = 1 * 100 / (CDbl(_layer.GetSetting("ClickfinderOverviewMaxDays", "10").Value) + 1)
                    End If
                End If

                Translator.SetProperty("#CurrentDate", getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy"))
                MyLog.Debug("[HighlightsGuiWindow] [OnPageLoad]: _ClickfinderCurrentDate = {0}", getTranslatedDayOfWeek(_ClickfinderCurrentDate) & " " & Format(_ClickfinderCurrentDate, "dd.MM.yyyy"))

                Try
                    If _ThreadFillMovieList.IsAlive = True Or _ThreadFillHighlightsList.IsAlive = True Then
                        MyLog.Debug("")
                        _ThreadFillMovieList.Abort()
                        _ThreadFillHighlightsList.Abort()
                    End If

                Catch ex3 As Exception ' Ignore this exception
                    'Eventuell auftretende Exception abfangen
                    ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
                End Try

                _ThreadFillMovieList = New Thread(AddressOf FillMovieList)
                _ThreadFillHighlightsList = New Thread(AddressOf FillHighlightsList)
                _ThreadFillMovieList.IsBackground = True
                _ThreadFillHighlightsList.IsBackground = True
                _ThreadFillMovieList.Start()
                _ThreadFillHighlightsList.Start()
                '_Thread1.Join()
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

                    If Not Date.Equals(_ClickfinderCurrentDate, Today.AddDays(CDbl(_layer.GetSetting("ClickfinderOverviewMaxDays", "10").Value))) Then
                        _ClickfinderCurrentDate = _ClickfinderCurrentDate.AddDays(1)
                        _DaysProgress.Percentage = _DaysProgress.Percentage + 1 * 100 / (CDbl(_layer.GetSetting("ClickfinderOverviewMaxDays", "10").Value) + 1)
                    ElseIf _ClickfinderCurrentDate = Today.AddDays(CDbl(_layer.GetSetting("ClickfinderOverviewMaxDays", "10").Value)) Then
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
                        _DaysProgress.Percentage = _DaysProgress.Percentage - 1 * 100 / (CDbl(_layer.GetSetting("ClickfinderOverviewMaxDays", "10").Value) + 1)
                    ElseIf _ClickfinderCurrentDate = Today.AddDays(-1) Then
                        _ClickfinderCurrentDate = Today.AddDays(CDbl(_layer.GetSetting("ClickfinderOverviewMaxDays", "10").Value))
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
                End If

                'Menu Button (F9) -> Context Menu open
                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_CONTEXT_MENU Then
                    MyLog.[Debug]("[HighlightsGUIWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", Action.m_key.KeyChar, Action.m_key.KeyCode, Action.wID.ToString)

                    If _MovieList.IsFocused = True Then ShowMoviesMenu(_MovieList.SelectedListItem.ItemId)
                    If _HighlightsList.IsFocused = True Then
                        'Falls im Label2 Translation.NewLabel gefunden -> Series Context Menu
                        If _HighlightsList.SelectedListItem.Label2 = Translation.NewLabel Then
                            ShowSeriesContextMenu(_HighlightsList.SelectedListItem.ItemId)
                        Else
                            ShowHighlightsMenu(_HighlightsList.SelectedListItem.ItemId)
                        End If
                    End If
                End If

                'OSD Info Button (Y) -> Context Menu open (gleiche Fkt. wie Menu Button)
                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_KEY_PRESSED Then
                    If Action.m_key IsNot Nothing Then
                        If Action.m_key.KeyChar = 121 Then

                            MyLog.[Debug]("[HighlightsGUIWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", Action.m_key.KeyChar, Action.m_key.KeyCode, Action.wID.ToString)

                            If _MovieList.IsFocused = True Then ShowMoviesMenu(_MovieList.SelectedListItem.ItemId)
                            If _HighlightsList.IsFocused = True Then
                                'Falls im Label2 Translation.NewLabel gefunden -> Series Context Menu
                                If _HighlightsList.SelectedListItem.Label2 = Translation.NewLabel Then
                                    ShowSeriesContextMenu(_HighlightsList.SelectedListItem.ItemId)
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
                    If Not _DaysProgress.Percentage = 1 * 100 / (CDbl(_layer.GetSetting("ClickfinderOverviewMaxDays", "10").Value) + 1) Then
                        _DaysProgress.Percentage = 1 * 100 / (CDbl(_layer.GetSetting("ClickfinderOverviewMaxDays", "10").Value) + 1)
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

            For i = 1 To 6
                Translator.SetProperty("#MovieListTvMovieStar" & i, "")
                Translator.SetProperty("#MovieListImage" & i, "")
                Translator.SetProperty("#MovieListRatingStar" & i, 0)
            Next

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

            _MovieList.Clear()
            _ProgressPercentagValue = _DaysProgress.Percentage

            AbortRunningThreads()

            MyBase.OnPageDestroy(new_windowId)

        End Sub
        Protected Overrides Sub OnClicked(ByVal controlId As Integer, _
                                 ByVal control As GUIControl, _
                                 ByVal actionType As  _
                                 MediaPortal.GUI.Library.Action.ActionType)

            MyBase.OnClicked(controlId, control, actionType)

            If control Is _btnNow Then
                GuiButtons.Now()
            End If

            If control Is _btnPrimeTime Then
                GuiButtons.PrimeTime()
            End If

            If control Is _btnLateTime Then
                GuiButtons.LateTime()
            End If

            If control Is _btnAllMovies Then
                GuiButtons.AllMovies()
            End If

            If control Is _btnHighlights Then
                GuiButtons.Highlights()
            End If

            If control Is _btnPreview Then
                GuiButtons.Preview()
            End If

            If control Is _MovieList Or control Is _HighlightsList Then
                If actionType = MediaPortal.GUI.Library.Action.ActionType.ACTION_SELECT_ITEM Then
                    Action_SelectItem()
                End If
            End If
        End Sub

        Private Sub Action_SelectItem()
            If _MovieList.IsFocused = True Then
                ListControlClick(_MovieList.SelectedListItem.ItemId)
            End If

            If _HighlightsList.IsFocused = True Then
                'Falls im Label2 Translation.NewLabel gefunden -> Series Context Menu
                If _HighlightsList.SelectedListItem.Label2 = Translation.NewLabel Then
                    ShowSeriesContextMenu(_HighlightsList.SelectedListItem.ItemId, True)
                Else
                    ListControlClick(_HighlightsList.SelectedListItem.ItemId)
                End If
            End If
        End Sub

#End Region

#Region "Functions"
        Private Sub FillMovieList()
            Dim _lastTitle As String = String.Empty
            Dim _ItemCounter As Integer = 0
            Dim _timeLabel As String = String.Empty
            Dim _infoLabel As String = String.Empty
            Dim _imagepath As String = String.Empty
            Dim _SQLstring As String = String.Empty
            Dim _idProgramTagesTipp As Integer = 0
            Dim _LogLocalMovies As String = String.Empty
            Dim _LogLocalSortedBy As String = String.Empty
            Dim _logShowTagesTipp As String = "false"

            _MovieList.Visible = False

            MyLog.Debug("")
            MyLog.Debug("[HighlightsGUIWindow] [FillMovieList]: Thread started")

            Try

                For i = 1 To 6
                    Translator.SetProperty("#MovieListTvMovieStar" & i, "")
                    Translator.SetProperty("#MovieListImage" & i, "")
                    Translator.SetProperty("#MovieListRatingStar" & i, 0)
                Next
                _MovieList.Clear()

                Dim _Result As New ArrayList

                '[Optional] TvMovie Tages Tipp immer als erstes anzeigen
                If CBool(_layer.GetSetting("ClickfinderOverviewShowTagesTipp").Value) = True Then
                    _SQLstring = _
                    "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                    "WHERE TvMovieBewertung = 4 " & _
                    "AND program.startTime > " & MySqlDate(_ClickfinderCurrentDate) & " " & _
                    "AND program.startTime < " & MySqlDate(_ClickfinderCurrentDate.AddHours(24))

                    Dim _TvMovieTagesTipps As New ArrayList(Broker.Execute(_SQLstring).TransposeToFieldList("idProgram", False))

                    If _TvMovieTagesTipps.Count > 0 Then

                        For i = 0 To _TvMovieTagesTipps.Count - 1
                            Dim _TvMovieProgram As TVMovieProgram = TVMovieProgram.Retrieve(_TvMovieTagesTipps.Item(i))

                            'Prüfen ob in Standard Tv Gruppe
                            Dim key As New Gentle.Framework.Key(GetType(ChannelGroup), True, "groupName", _layer.GetSetting("ClickfinderStandardTvGroup", "All Channels").Value)
                            Dim _Group As ChannelGroup = Gentle.Framework.Broker.RetrieveInstance(Of ChannelGroup)(key)

                            'Alle Gruppen des _program laden
                            Dim sb As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(GroupMap))
                            sb.AddConstraint([Operator].Equals, "idgroup", _Group.IdGroup)
                            sb.AddConstraint([Operator].Equals, "idChannel", _TvMovieProgram.ReferencedProgram.IdChannel)
                            Dim stmt As SqlStatement = sb.GetStatement(True)
                            Dim _isInFavGroup As IList(Of GroupMap) = ObjectFactory.GetCollection(GetType(GroupMap), stmt.Execute())

                            If _isInFavGroup.Count = 0 Then
                                Continue For
                            End If

                            _Result.Add(_TvMovieTagesTipps.Item(i))

                            _idProgramTagesTipp = _TvMovieTagesTipps.Item(i)

                        Next
                        _logShowTagesTipp = "true"
                    Else
                        _logShowTagesTipp = "not found"
                    End If

                End If


                'Manuelle Sqlabfrage starten (wegen InnerJoin) -> idprogram 
                _SQLstring = _
                    "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                    "WHERE starRating > 1 AND TvMovieBewertung < 6 " & _
                    "AND startTime > " & MySqlDate(_ClickfinderCurrentDate.AddHours(CDbl(_layer.GetSetting("ClickfinderOverviewShowMoviesAfter", "12").Value))) & " " & _
                    "AND startTime < " & MySqlDate(_ClickfinderCurrentDate.AddHours(24)) & " " & _
                    "AND genre NOT LIKE '%Serie%' " & _
                    "AND genre NOT LIKE '%Sitcom%' " & _
                    "AND genre NOT LIKE '%Zeichentrick%' "

                Select Case (_layer.GetSetting("ClickfinderOverviewMovieSort", SortMethode.startTime.ToString).Value)
                    Case Is = SortMethode.startTime.ToString
                        _LogLocalSortedBy = SortMethode.startTime.ToString
                        _SQLstring = AppendSqlLimit(_SQLstring & Helper.ORDERBYstartTime, 25)


                    Case Is = SortMethode.TvMovieStar.ToString
                        _LogLocalSortedBy = SortMethode.TvMovieStar.ToString
                        _SQLstring = AppendSqlLimit(_SQLstring & Helper.ORDERBYtvMovieBewertung, 25)

                    Case Is = SortMethode.RatingStar.ToString
                        _LogLocalSortedBy = SortMethode.RatingStar.ToString
                        _SQLstring = AppendSqlLimit(_SQLstring & Helper.ORDERBYstarRating, 25)

                End Select


                MyLog.Debug("[HighlightsGUIWindow] [FillMovieList]: sorted by {0}, show TvMovie TagesTipp = {1}, SQLString: {2}", _LogLocalSortedBy, _logShowTagesTipp, _SQLstring)

                _Result.AddRange(Broker.Execute(_SQLstring).TransposeToFieldList("idProgram", False))


                MyLog.Debug("[HighlightsGUIWindow] [FillMovieList]: {0} program found", _Result.Count)

                If _Result.Count > 0 Then

                    For i = 0 To _Result.Count - 1
                        Try

                            'ProgramDaten über TvMovieProgram laden
                            Dim _TvMovieProgram As TVMovieProgram = TVMovieProgram.Retrieve(_Result.Item(i))

                            '[Option] Falls Datei lokal existiert, dann nicht anzeigen (-> Continue For)
                            'Wenn TagesTipp aktiviert, dann nicht ausführen
                            If CBool(_layer.GetSetting("ClickfinderOverviewShowLocalMovies", "false").Value) = True And Not _TvMovieProgram.idProgram = _idProgramTagesTipp Then
                                If _TvMovieProgram.local = True Then
                                    _LogLocalMovies = _LogLocalMovies & _TvMovieProgram.ReferencedProgram.Title & ", "
                                    Continue For
                                End If
                            End If

                            'Wenn TagesTipp immer anzeigen aktiviert, prüfen ob TagesTipp, dann weiter
                            If _TvMovieProgram.idProgram = _idProgramTagesTipp And _ItemCounter > 1 Then
                                Continue For
                            End If

                            'Prüfen ob in Standard Tv Gruppe
                            Dim key As New Gentle.Framework.Key(GetType(ChannelGroup), True, "groupName", _layer.GetSetting("ClickfinderStandardTvGroup", "All Channels").Value)
                            Dim _Group As ChannelGroup = Gentle.Framework.Broker.RetrieveInstance(Of ChannelGroup)(key)

                            'Alle Gruppen des _program laden
                            Dim sb As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(GroupMap))
                            sb.AddConstraint([Operator].Equals, "idgroup", _Group.IdGroup)
                            sb.AddConstraint([Operator].Equals, "idChannel", _TvMovieProgram.ReferencedProgram.IdChannel)
                            Dim stmt As SqlStatement = sb.GetStatement(True)
                            Dim _isInFavGroup As IList(Of GroupMap) = ObjectFactory.GetCollection(GetType(GroupMap), stmt.Execute())

                            If _isInFavGroup.Count = 0 Then
                                Continue For
                            End If

                            'Prüfen ob schon in der Liste vorhanden
                            If String.Equals(_lastTitle, _TvMovieProgram.ReferencedProgram.Title & _TvMovieProgram.ReferencedProgram.EpisodeName) = False Then

                                _ItemCounter = _ItemCounter + 1

                                Translator.SetProperty("#MovieListRatingStar" & _ItemCounter, GuiLayout.ratingStar(_TvMovieProgram.ReferencedProgram))
                                Translator.SetProperty("#MovieListTvMovieStar" & _ItemCounter, GuiLayout.TvMovieStar(_TvMovieProgram))
                                Translator.SetProperty("#MovieListImage" & _ItemCounter, GuiLayout.Image(_TvMovieProgram))


                                AddListControlItem(_MovieList, _TvMovieProgram.ReferencedProgram.IdProgram, _TvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName, _TvMovieProgram.ReferencedProgram.Title, GuiLayout.TimeLabel(_TvMovieProgram), GuiLayout.InfoLabel(_TvMovieProgram), , , GuiLayout.RecordingStatus(_TvMovieProgram.ReferencedProgram))

                                MyLog.Debug("[HighlightsGUIWindow] [FillMovieList]: Add ListItem {0} (Title: {1}, Channel: {2}, startTime: {3}, idprogram: {4}, ratingStar: {5}, TvMovieStar: {6}, image: {7})", _
                                            _ItemCounter, _TvMovieProgram.ReferencedProgram.Title, _TvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName, _
                                            _TvMovieProgram.ReferencedProgram.StartTime, _TvMovieProgram.ReferencedProgram.IdProgram, _
                                            GuiLayout.ratingStar(_TvMovieProgram.ReferencedProgram), _
                                            _TvMovieProgram.TVMovieBewertung, GuiLayout.Image(_TvMovieProgram))

                                _lastTitle = _TvMovieProgram.ReferencedProgram.Title & _TvMovieProgram.ReferencedProgram.EpisodeName

                            End If

                            If _ItemCounter = 6 Then Exit For

                            'log Ausgabe abfangen, falls der Thread abgebrochen wird
                        Catch ex As ThreadAbortException ' Ignore this exception
                            '_isAbortException = True
                            MyLog.Debug("[HighlightsGUIWindow] [FillMovieList]: --- THREAD ABORTED ---")
                            MyLog.Debug("")
                        Catch ex As GentleException
                        Catch ex As Exception
                            MyLog.Error("[HighlightsGUIWindow] [FillMovieList]: Loop exception err:" & ex.Message & " stack:" & ex.StackTrace)
                        End Try
                    Next
                End If

                _MoviesProgressBar.Visible = False
                _MovieList.Visible = True

                If CBool(_layer.GetSetting("ClickfinderOverviewShowLocalMovies", "false").Value) = True Then
                    MyLog.Debug("[HighlightsGUIWindow] [FillMovieList]: Movies exist local and will not be displayed ({0})", _LogLocalMovies)
                End If

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

            Dim _lastTitle As String = String.Empty
            Dim _ItemCounter As Integer = 0
            Dim _timeLabel As String = String.Empty
            Dim _infoLabel As String = String.Empty
            Dim _imagepath As String = String.Empty
            Dim SQLstring As String = String.Empty
            Dim _logNewShowSeries As String = "false"
            Dim _logNewSeriesCount As Integer = 0


            MyLog.Debug("")
            MyLog.Debug("[HighlightsGUIWindow] [FillHighlightsList]: Thread started")

            Try
                _HighlightsList.Visible = False
                _HighlightsList.Clear()

                Dim _Result As New ArrayList

                Dim _lastRecordingStatus As String = String.Empty

                'Wenn TvSeries Import aktiviert, dann neue Episoden des Tages anzeigen
                If CBool(_layer.GetSetting("TvMovieImportTvSeriesInfos", "false").Value) = True Then

                    Dim _SeriesResult As New ArrayList
                    Dim _ListTvMovieProgram As New List(Of TVMovieProgram)

                    'Alle neuen Episoden (idSeries > 0 & local = false) laden mit Group By
                    SQLstring = "Select program.idprogram from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                        "WHERE idSeries > 0 " & _
                                        "AND local = 0 " & _
                                        "AND startTime > " & MySqlDate(_ClickfinderCurrentDate.AddHours(0)) & " " & _
                                        "AND startTime < " & MySqlDate(_ClickfinderCurrentDate.AddHours(24)) & " " & _
                                        "GROUP BY title"

                    _logNewShowSeries = "true"
                    _SeriesResult.AddRange(Broker.Execute(SQLstring).TransposeToFieldList("idProgram", True))
                    '_logNewSeriesCount = _Result.Count

                    If _SeriesResult.Count > 0 Then
                        For i = 0 To _SeriesResult.Count - 1
                            Dim _NewEpisodeList As New ArrayList
                            Dim _RecordingStatus As String = String.Empty

                            'Prüfen ob noch immer lokal nicht vorhanden
                            'CheckSeriesLocalStatus(_TvMovieSeriesProgram)

                            'ProgramDaten über TvMovieProgram laden
                            Dim _TvMovieSeriesProgram As TVMovieProgram = TVMovieProgram.Retrieve(_SeriesResult.Item(i))

                            'Alle neuen Episoden der Serie zählen die aufgenommen werden sollen
                            SQLstring = "Select program.idprogram from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                            "WHERE idSeries = " & _TvMovieSeriesProgram.idSeries & " " & _
                                            "AND local = 0 " & _
                                            "AND startTime > " & MySqlDate(_ClickfinderCurrentDate.AddHours(0)) & " " & _
                                            "AND startTime < " & MySqlDate(_ClickfinderCurrentDate.AddHours(24)) & " " & _
                                            "Order BY state DESC"

                            _NewEpisodeList.AddRange(Broker.Execute(SQLstring).TransposeToFieldList("idProgram", True))

                            'Daten als Highlightslist übergeben
                            _timeLabel = Translation.NewLabel
                            _infoLabel = _NewEpisodeList.Count & " " & Translation.NewEpisodeFound
                            _imagepath = Config.GetFile(Config.Dir.Thumbs, "MPTVSeriesBanners\") & _TvMovieSeriesProgram.SeriesPosterImage
                            _RecordingStatus = GuiLayout.RecordingStatus(_TvMovieSeriesProgram.ReferencedProgram)

                            'Prüfen ob eine Episode als Aufnahme geplant ist
                            If _RecordingStatus = String.Empty Then
                                If _NewEpisodeList.Count > 0 Then
                                    Dim _Episode As Program = Program.Retrieve(_NewEpisodeList.Item(0))
                                    _RecordingStatus = GuiLayout.RecordingStatus(_Episode)

                                    'Prüfen ob Episode an anderem Tag aufgenommen wird
                                    If _RecordingStatus = String.Empty Then
                                        Dim _RecordingList As New ArrayList

                                        SQLstring = "Select program.idprogram from program " & _
                                                "WHERE title = '" & _Episode.Title & "' " & _
                                                "AND episodeName = '" & _Episode.EpisodeName & "' " & _
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
                            End If

                            'If Not _lastTitle = _TvMovieSeriesProgram.ReferencedProgram.Title Then
                            AddListControlItem(_HighlightsList, _TvMovieSeriesProgram.ReferencedProgram.IdProgram, _TvMovieSeriesProgram.ReferencedProgram.ReferencedChannel.DisplayName, _TvMovieSeriesProgram.ReferencedProgram.Title, _timeLabel, _infoLabel, _imagepath, , _RecordingStatus)
                            'End If

                            _lastRecordingStatus = _RecordingStatus
                            _lastTitle = _TvMovieSeriesProgram.ReferencedProgram.Title
                        Next
                    End If



                End If

                'Manuelle Sqlabfrage starten (wegen InnerJoin) -> idprogram 
                SQLstring = _
                    "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                    "WHERE TvMovieBewertung = 6 " & _
                    "AND genre NOT LIKE '%Zeichentrick%' " & _
                    "AND genre NOT LIKE '%Serie%' " & _
                    "AND genre NOT LIKE '%Sitcom%' " & _
                    "AND genre NOT LIKE '%Zeichentrick%' " & _
                    "AND startTime > " & MySqlDate(_ClickfinderCurrentDate.AddHours(CDbl(_layer.GetSetting("ClickfinderOverviewShowHighlightsAfter", "15").Value))) & " " & _
                    "AND startTime < " & MySqlDate(_ClickfinderCurrentDate.AddHours(24)) & " " & _
                    Helper.ORDERBYstartTime

                _Result.AddRange(Broker.Execute(SQLstring).TransposeToFieldList("idProgram", False))

                MyLog.Debug("[HighlightsGUIWindow] [FillHighlightsList]: {0} program found, Show Series = {1} ({2} new series found), SqlString: {3}", _Result.Count, _logNewShowSeries, _logNewSeriesCount, SQLstring)

                If _Result.Count > 0 Then

                    For i = 0 To _Result.Count - 1
                        Try

                            'ProgramDaten über TvMovieProgram laden
                            Dim _TvMovieProgram As TVMovieProgram = TVMovieProgram.Retrieve(_Result.Item(i))

                            Dim _Dauer As DateTime = _TvMovieProgram.ReferencedProgram.EndTime.Subtract(New System.TimeSpan(_TvMovieProgram.ReferencedProgram.StartTime.Hour, _TvMovieProgram.ReferencedProgram.StartTime.Minute, 0))

                            'If _Dauer.Minute > CInt(_layer.GetSetting("ClickfinderOverviewHighlightsMinRuntime", "16").Value) Or _TvMovieProgram.idSeries > 0 Then
                            If DateDiff(DateInterval.Minute, _TvMovieProgram.ReferencedProgram.StartTime, _TvMovieProgram.ReferencedProgram.EndTime) > _layer.GetSetting("ClickfinderOverviewHighlightsMinRuntime", "16").Value Or _TvMovieProgram.idSeries > 0 Then

                                'Prüfen ob schon in der Liste vorhanden
                                If String.Equals(_lastTitle, _TvMovieProgram.ReferencedProgram.Title & _TvMovieProgram.ReferencedProgram.EpisodeName) = False Then
                                    _ItemCounter = _ItemCounter + 1

                                    _timeLabel = Format(_TvMovieProgram.ReferencedProgram.StartTime.Hour, "00") & _
                                                        ":" & Format(_TvMovieProgram.ReferencedProgram.StartTime.Minute, "00")

                                    If String.IsNullOrEmpty(_TvMovieProgram.ReferencedProgram.EpisodeName) Then
                                        _infoLabel = _TvMovieProgram.ReferencedProgram.Genre
                                    Else
                                        _infoLabel = _TvMovieProgram.ReferencedProgram.EpisodeName
                                    End If


                                    'Image zunächst auf SenderLogo festlegen
                                    _imagepath = Config.GetFile(Config.Dir.Thumbs, "tv\logos\") & Replace(_TvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName, "/", "_") & ".png"

                                    'Falls TvSeries Import an & ist TvSeries, poster Image anzeigen
                                    'If CBool(_layer.GetSetting("TvMovieImportTvSeriesInfos", "false").Value) = True And _TvMovieProgram.idSeries > 0 Then
                                    '    _timeLabel = Translation.NewLabel
                                    '    _infoLabel = Translation.NewEpisodeFound
                                    '    _imagepath = Config.GetFile(Config.Dir.Thumbs, "MPTVSeriesBanners\") & _TvMovieProgram.SeriesPosterImage
                                    'Else
                                    'Prüfen ob in Standard Tv Gruppe
                                    Dim key As New Gentle.Framework.Key(GetType(ChannelGroup), True, "groupName", _layer.GetSetting("ClickfinderStandardTvGroup", "All Channels").Value)
                                    Dim _Group As ChannelGroup = Gentle.Framework.Broker.RetrieveInstance(Of ChannelGroup)(key)

                                    'Alle Gruppen des _program laden
                                    Dim sb As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(GroupMap))
                                    sb.AddConstraint([Operator].Equals, "idgroup", _Group.IdGroup)
                                    sb.AddConstraint([Operator].Equals, "idChannel", _TvMovieProgram.ReferencedProgram.IdChannel)
                                    Dim stmt As SqlStatement = sb.GetStatement(True)
                                    Dim _isInFavGroup As IList(Of GroupMap) = ObjectFactory.GetCollection(GetType(GroupMap), stmt.Execute())

                                    If _isInFavGroup.Count = 0 Then
                                        Continue For
                                    End If
                                    'End If

                                    If CBool(_layer.GetSetting("ClickfinderUseSportLogos", "false").Value) = True Then
                                        _imagepath = UseSportLogos(_TvMovieProgram.ReferencedProgram.Title, _imagepath)
                                    End If

                                    AddListControlItem(_HighlightsList, _TvMovieProgram.ReferencedProgram.IdProgram, _TvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName, _TvMovieProgram.ReferencedProgram.Title, _timeLabel, _infoLabel, _imagepath, , GuiLayout.RecordingStatus(_TvMovieProgram.ReferencedProgram))

                                    _lastTitle = _TvMovieProgram.ReferencedProgram.Title & _TvMovieProgram.ReferencedProgram.EpisodeName

                                End If
                            End If

                            'log Ausgabe abfangen, falls der Thread abgebrochen wird
                        Catch ex As ThreadAbortException ' Ignore this exception
                            MyLog.Debug("[HighlightsGUIWindow] [FillHighlightsList]: --- THREAD ABORTED ---")
                            MyLog.Debug("")
                        Catch ex As GentleException
                        Catch ex As Exception
                            MyLog.Error("[HighlightsGUIWindow] [FillHighlightsList]: Loop exception err:" & ex.Message & " stack:" & ex.StackTrace)
                        End Try
                    Next
                End If

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
#End Region

#Region "MediaPortal Funktionen / Dialogs"
        Private Sub ShowSeriesContextMenu(ByVal idProgram As Integer, Optional ByVal SelectItem As Boolean = False)
            Dim dlgContext As GUIDialogSelect2Custom = CType(GUIWindowManager.GetWindow(CType(1656544655, Integer)), GUIDialogSelect2Custom)
            dlgContext.Reset()
            Try
                Dim _idProgramContainer As Dictionary(Of Integer, Integer) = New Dictionary(Of Integer, Integer)
                Dim _SeriesProgram As TVMovieProgram = TVMovieProgram.Retrieve(idProgram)
                'ContextMenu Layout
                dlgContext.SetHeading(_SeriesProgram.ReferencedProgram.Title)
                'dlgContext.ShowQuickNumbers = False

                MyLog.Debug("[HighlightsGuiWindow] [ShowSeriesContextMenu]: idprogram = {0}, title = {1}", _SeriesProgram.ReferencedProgram.IdProgram, _SeriesProgram.ReferencedProgram.Title)

                If CBool(_layer.GetSetting("TvMovieImportTvSeriesInfos", "false").Value) = True Then

                    Dim SQLstring As String = "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                        "WHERE idSeries = " & _SeriesProgram.idSeries & " " & _
                                        "AND local = 0 " & _
                                        "AND startTime > " & MySqlDate(_ClickfinderCurrentDate.AddHours(0)) & " " & _
                                        "AND startTime < " & MySqlDate(_ClickfinderCurrentDate.AddHours(24)) & " " & _
                                        Helper.ORDERBYstartTime

                    Dim _Result As New ArrayList
                    _Result.AddRange(Broker.Execute(SQLstring).TransposeToFieldList("idProgram", True))

                    If _Result.Count = 1 Then

                        Dim _TvMovieProgram As TVMovieProgram = TVMovieProgram.Retrieve(_Result.Item(0))

                        If SelectItem = True Then
                            ListControlClick(_TvMovieProgram.idProgram)
                        Else
                            ShowHighlightsMenu(_TvMovieProgram.idProgram)
                        End If
                        Exit Sub
                    Else
                        If _Result.Count > 0 Then
                            For i = 0 To _Result.Count - 1
                                Try

                                    'ProgramDaten über TvMovieProgram laden
                                    Dim _TvMovieProgram As TVMovieProgram = TVMovieProgram.Retrieve(_Result.Item(i))

                                    Dim lItemEpisode As New GUIListItem
                                    lItemEpisode.Label = _TvMovieProgram.ReferencedProgram.EpisodeName
                                    lItemEpisode.Label2 = Format(_TvMovieProgram.ReferencedProgram.StartTime.Hour, "00") & _
                                                        ":" & Format(_TvMovieProgram.ReferencedProgram.StartTime.Minute, "00") & " - " & _TvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName
                                    lItemEpisode.Label3 = "Staffel " & Format(CInt(_TvMovieProgram.ReferencedProgram.SeriesNum), "00") & ", Episode " & Format(CInt(_TvMovieProgram.ReferencedProgram.EpisodeNum), "00")
                                    lItemEpisode.IconImage = Config.GetFile(Config.Dir.Thumbs, "MPTVSeriesBanners\") & _TvMovieProgram.SeriesPosterImage

                                    lItemEpisode.PinImage = GuiLayout.RecordingStatus(_TvMovieProgram.ReferencedProgram)

                                    _idProgramContainer.Add(i, _TvMovieProgram.idProgram)

                                    dlgContext.Add(lItemEpisode)
                                    lItemEpisode.Dispose()
                                Catch ex As Exception
                                    MyLog.Error("[ShowSeriesContextMenu]: Loop: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
                                End Try
                            Next

                            dlgContext.DoModal(GetID)

                            If SelectItem = True Then
                                ListControlClick(_idProgramContainer.Item(dlgContext.SelectedLabel))
                            Else
                                ShowHighlightsMenu(_idProgramContainer.Item(dlgContext.SelectedLabel))
                            End If

                        End If

                    End If
                End If

                _idProgramContainer.Clear()
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

                        CategoriesGuiWindow.SetGuiProperties(CategoriesGuiWindow.CategorieView.Day, _Program.StartTime.Date)
                        GUIWindowManager.ActivateWindow(1656544654)
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
                        Try

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

                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try
                    Case Is = 4
                        MyLog.Debug("[HighlightsGuiWindow] [ShowHighlightsMenu]:  selected -> action menu")
                        MyLog.Debug("")
                        ShowActionMenu(_Program)
                    Case Else
                        MyLog.Debug("[HighlightsGuiWindow] [ShowHighlightsMenu]: exit")
                        MyLog.Debug("")
                End Select

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

                        CategoriesGuiWindow.SetGuiProperties(CategoriesGuiWindow.CategorieView.Day, _Program.StartTime.Date)
                        GUIWindowManager.ActivateWindow(1656544654)

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

        End Sub
        Private Sub InfoMenu()
            Dim dlgContext As GUIDialogExif = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_EXIF, Integer)), GUIDialogExif)
            dlgContext.Reset()
        End Sub
#End Region


    End Class
End Namespace
