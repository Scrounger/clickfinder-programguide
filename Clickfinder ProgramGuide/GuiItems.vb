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
Imports ClickfinderProgramGuide.ClickfinderProgramGuide.CategoriesGuiWindow
Imports Gentle.Common

Namespace ClickfinderProgramGuide
    Public Class ItemsGuiWindow
        Inherits GUIWindow

#Region "Skin Controls"

        'Buttons



        <SkinControlAttribute(5)> Protected _btnAllMovies As GUIButtonControl = Nothing



        'ProgressBar
        'ProgressBar
        <SkinControlAttribute(8)> Protected _RightProgressBar As GUIAnimation = Nothing
        <SkinControlAttribute(9)> Protected _LeftProgressBar As GUIAnimation = Nothing

        <SkinControlAttribute(11)> Protected _PageProgress As GUIProgressControl = Nothing

        'ListControl
        <SkinControlAttribute(10)> Protected _leftList As GUIListControl = Nothing
        <SkinControlAttribute(30)> Protected _rightList As GUIListControl = Nothing

#End Region

#Region "Members"
        Friend Shared _ItemsCache As New List(Of TVMovieProgram)
        Friend Shared _ItemsOnLoad As New List(Of TVMovieProgram)
        Friend Shared _CurrentCounter As Integer = 0
        Private _ThreadLeftList As Threading.Thread
        Private _ThreadRightList As Threading.Thread
        Private _isAbortException As Boolean = False
        Private _layer As New TvBusinessLayer
        Friend Shared _CategorieMinRuntime As Integer
        Friend Shared _idCategorie As Integer = 0
        Friend Shared _sortedBy As String = String.Empty
        Private _FilterByGroup As String = String.Empty
        Private _LastFocusedIndex As Integer
        Private _LastFocusedControlID As Integer

        Friend Shared _ItemsSqlString As String = String.Empty
#End Region

#Region "Constructors"

        Public Sub New()
        End Sub
        'Public Shared Sub SetGuiProperties(ByVal idprogram As Integer, ByVal PeriodeStartTime As Date, ByVal PeriodeEndTime As Date, Optional ByVal Genre As String = "%")

        Friend Shared Sub SetGuiProperties(ByVal SqlString As String, Optional ByVal CategorieMinRuntime As Integer = 0, Optional ByVal sortedby As String = "startTime", Optional ByVal idCategorie As Integer = 0)

            _CategorieMinRuntime = CategorieMinRuntime
            _ItemsSqlString = SqlString
            _sortedBy = sortedby
            _idCategorie = idCategorie
            _ItemsCache.Clear()

        End Sub

#End Region

#Region "GUI Properties"
        Public Overloads Overrides Property GetID() As Integer
            Get
                Return 1656544653
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property


        Public Overloads Overrides Function Init() As Boolean
            'Beim initialisieren des Plugin den Screen laden

            Return Load(GUIGraphicsContext.Skin + "\ClickfinderProgramGuideItems.xml")
        End Function

        Public Overrides Function GetModuleName() As String
            Return "Clickfinder ProgramGuide Items"
        End Function

#End Region

#Region "GUI Events"

        Protected Overrides Sub OnPageLoad()
            MyBase.OnPageLoad()
            GUIWindowManager.NeedRefresh()

            Helper.CheckConnectionState()

            MyLog.Info("")
            MyLog.Info("[ItemsGuiWindow] -------------[OPEN]-------------")

            GuiLayoutLoading()

            GuiLayout.SetSettingLastUpdateProperty()

            _FilterByGroup = _layer.GetSetting("ClickfinderStandardTvGroup", "All Channels").Value

            If _ItemsCache.Count = 0 Then
                Dim _FillLists As New Threading.Thread(AddressOf GetItemsOnLoad)
                _FillLists.Start()
            Else
                Dim _Thread1 As New Thread(AddressOf FillLeftList)
                Dim _Thread2 As New Thread(AddressOf FillRightList)
                _Thread1.Start()
                _Thread2.Start()
            End If

            '_Thread1.Join()


        End Sub

        Protected Overrides Sub OnPageDestroy(ByVal new_windowId As Integer)
            For i = 1 To 12
                Translator.SetProperty("#ItemsListTvMovieStar" & i, "")
                Translator.SetProperty("#ItemsListImage" & i, "")
                Translator.SetProperty("#ItemsListRatingStar" & i, 0)
            Next

            RememberLastFocusedItem()

            AbortRunningThreads()

            Dispose()
            AllocResources()

            MyBase.OnPageDestroy(new_windowId)
        End Sub

        Public Overrides Sub OnAction(ByVal action As MediaPortal.GUI.Library.Action)

            If GUIWindowManager.ActiveWindow = GetID Then
                'Try
                '    MyLog.[Debug]("Keypress on VideoReDo Screen. KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)
                'Catch
                '    MyLog.[Debug]("Keypress on VideoReDo Screen. KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)
                'End Try

                'Select Item (Enter) -> MP TvProgramInfo aufrufen --Über keychar--
                'If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_SELECT_ITEM Then

                '    MyLog.[Debug]("[ItemsGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)

                '    Action_SelectItem()
                'End If

                'Next Item (F8) -> eine Seite vor
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_NEXT_ITEM Then

                    _LastFocusedIndex = 0
                    _LastFocusedControlID = _leftList.GetID

                    Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowLeftProgressBar)
                    _ProgressBarThread.Start()

                    Dim _ProgressBarThread2 As New Threading.Thread(AddressOf ShowRightProgressBar)
                    _ProgressBarThread2.Start()

                    AbortRunningThreads()

                    If CInt(_CurrentCounter / 12) * 12 + 12 > _ItemsCache.Count - 1 Then
                        _CurrentCounter = 0
                        _PageProgress.Percentage = 0
                    Else
                        _CurrentCounter = _CurrentCounter + 12
                        _PageProgress.Percentage = (CShort((_CurrentCounter + 12) / 12)) * 100 / (CShort((_ItemsCache.Count) / 12 + 0.5))
                    End If

                    Translator.SetProperty("#CurrentPageLabel", Translation.PageLabel & " " & CShort((_CurrentCounter + 12) / 12) & " / " & CShort((_ItemsCache.Count) / 12 + 0.5))

                    MyLog.[Debug]("[HighlightsGUIWindow] [OnAction]: Keypress - Actiontype={0}: page = {1}", action.wID.ToString, CShort((_CurrentCounter + 12) / 12) & " / " & CShort((_ItemsCache.Count) / 12 + 0.5))

                    _ThreadLeftList = New Thread(AddressOf FillLeftList)
                    _ThreadRightList = New Thread(AddressOf FillRightList)
                    _ThreadLeftList.IsBackground = True
                    _ThreadRightList.IsBackground = True

                    _ThreadLeftList.Start()
                    _ThreadRightList.Start()

                End If

                'Prev. Item (F7) -> einen Tag zurück
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_PREV_ITEM Then

                    _LastFocusedIndex = 0
                    _LastFocusedControlID = _leftList.GetID

                    Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowLeftProgressBar)
                    _ProgressBarThread.Start()

                    Dim _ProgressBarThread2 As New Threading.Thread(AddressOf ShowRightProgressBar)
                    _ProgressBarThread2.Start()

                    AbortRunningThreads()

                    If _CurrentCounter <= 0 Then
                        _CurrentCounter = Int(_ItemsCache.Count / 12) * 12
                        _PageProgress.Percentage = 100
                    Else
                        _CurrentCounter = _CurrentCounter - 12
                        _PageProgress.Percentage = (CShort((_CurrentCounter + 12) / 12)) * 100 / (CShort((_ItemsCache.Count) / 12 + 0.5))
                    End If

                    Translator.SetProperty("#CurrentPageLabel", Translation.PageLabel & " " & CShort((_CurrentCounter + 12) / 12) & " / " & CShort((_ItemsCache.Count) / 12 + 0.5))

                    MyLog.[Debug]("[HighlightsGUIWindow] [OnAction]: Keypress - Actiontype={0}: page = {1}", action.wID.ToString, CShort((_CurrentCounter + 12) / 12) & " / " & CShort((_ItemsCache.Count) / 12 + 0.5))

                    _ThreadLeftList = New Thread(AddressOf FillLeftList)
                    _ThreadRightList = New Thread(AddressOf FillRightList)
                    _ThreadLeftList.IsBackground = True
                    _ThreadRightList.IsBackground = True

                    _ThreadLeftList.Start()
                    _ThreadRightList.Start()

                End If

                'Play Button (P) -> Start channel
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MUSIC_PLAY Then
                    Try
                        If _leftList.IsFocused = True Then StartTv(Program.Retrieve(_leftList.SelectedListItem.ItemId))
                        If _rightList.IsFocused = True Then StartTv(Program.Retrieve(_rightList.SelectedListItem.ItemId))
                    Catch ex As Exception
                        MyLog.Error("[Play Button]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
                    End Try
                End If

                'Record Button (R) -> MP TvProgramInfo aufrufen --Über keychar--
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_KEY_PRESSED Then
                    If action.m_key IsNot Nothing Then
                        If action.m_key.KeyChar = 114 Then
                            If _leftList.IsFocused = True Then LoadTVProgramInfo(Program.Retrieve(_leftList.SelectedListItem.ItemId))
                            If _rightList.IsFocused = True Then LoadTVProgramInfo(Program.Retrieve(_rightList.SelectedListItem.ItemId))
                        End If
                    End If
                End If

                'Menu Button (F9) -> Context Menu open
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_CONTEXT_MENU Then
                    RememberLastFocusedItem()
                    If _leftList.IsFocused = True Then ShowItemsContextMenu(_leftList.SelectedListItem.ItemId)
                    If _rightList.IsFocused = True Then ShowItemsContextMenu(_rightList.SelectedListItem.ItemId)
                End If

                'OSD Info Button (Y) -> Context Menu open (gleiche Fkt. wie Menu Button)
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_KEY_PRESSED Then
                    If action.m_key IsNot Nothing Then
                        If action.m_key.KeyChar = 121 Then
                            RememberLastFocusedItem()

                            If _leftList.IsFocused = True Then ShowItemsContextMenu(_leftList.SelectedListItem.ItemId)
                            If _rightList.IsFocused = True Then ShowItemsContextMenu(_rightList.SelectedListItem.ItemId)
                        End If
                    End If
                End If

                'Remote 5 Button (5) -> zu Heute springen
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.REMOTE_5 Then
                    MyLog.[Debug]("[ItemsGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)

                    _CurrentCounter = 0
                    _PageProgress.Percentage = 0

                    Translator.SetProperty("#CurrentPageLabel", Translation.PageLabel & " " & CShort((_CurrentCounter + 12) / 12) & " / " & CShort((_ItemsCache.Count) / 12 + 0.5))
                    Try

                 
                        If _ClickfinderCategorieView = CategorieView.Now And _idCategorie > 0 Then

                            'Translator.SetProperty("#ItemsLeftListLabel", _Categorie.Name & " " & Translation.von & " " & Format(PeriodeStartTime.Hour, "00") & ":" & Format(PeriodeStartTime.Minute, "00") & " - " & Format(PeriodeEndTime.Hour, "00") & ":" & Format(PeriodeEndTime.Minute, "00"))
                            Dim _string As String = GUIPropertyManager.GetProperty("#ItemsLeftListLabel")
                            Dim _StringList As New ArrayList(Split(_string, " "))
                            Dim _lastUpdate As Date = CDate(_StringList(2))

                            If DateDiff(DateInterval.Minute, Now, _lastUpdate) > 5 Then
                                MsgBox("Refresh: 5min rum")

                                Dim key As New Gentle.Framework.Key(GetType(ClickfinderCategories), True, "idClickfinderCategories", _idCategorie)
                                Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(key)

                                ItemsGuiWindow.SetGuiProperties(CStr(Replace(Replace(_Categorie.SqlString, "#startTime", MySqlDate(PeriodeStartTime.AddMinutes(CDbl(_layer.GetSetting("ClickfinderNowOffset", "-20").Value)))), "#endTime", MySqlDate(PeriodeEndTime))), _Categorie.MinRunTime, _Categorie.sortedBy, _Categorie.idClickfinderCategories)
                                Translator.SetProperty("#ItemsLeftListLabel", _Categorie.Name & " " & Translation.von & " " & Format(PeriodeStartTime.Hour, "00") & ":" & Format(PeriodeStartTime.Minute, "00") & " - " & Format(PeriodeEndTime.Hour, "00") & ":" & Format(PeriodeEndTime.Minute, "00"))
                                GUIWindowManager.ActivateWindow(1656544653)
                            Else
                                If Not _FilterByGroup = _layer.GetSetting("ClickfinderStandardTvGroup", "All Channels").Value Then
                                    _FilterByGroup = _layer.GetSetting("ClickfinderStandardTvGroup", "All Channels").Value
                                    SortFilterList()
                                Else

                                    AbortRunningThreads()

                                    _ThreadLeftList = New Thread(AddressOf FillLeftList)
                                    _ThreadRightList = New Thread(AddressOf FillRightList)
                                    _ThreadLeftList.IsBackground = True
                                    _ThreadRightList.IsBackground = True

                                    _ThreadLeftList.Start()
                                    _ThreadRightList.Start()

                                End If
                            End If
                        Else
                            If Not _FilterByGroup = _layer.GetSetting("ClickfinderStandardTvGroup", "All Channels").Value Then
                                _FilterByGroup = _layer.GetSetting("ClickfinderStandardTvGroup", "All Channels").Value
                                SortFilterList()
                            Else

                                AbortRunningThreads()

                                _ThreadLeftList = New Thread(AddressOf FillLeftList)
                                _ThreadRightList = New Thread(AddressOf FillRightList)
                                _ThreadLeftList.IsBackground = True
                                _ThreadRightList.IsBackground = True

                                _ThreadLeftList.Start()
                                _ThreadRightList.Start()

                            End If
                        End If
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If

                'Remote 7 Button (7) -> Quick Filter 1
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.REMOTE_7 Then
                    MyLog.[Debug]("[CategoriesGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)

                    _FilterByGroup = _layer.GetSetting("ClickfinderQuickTvGroup1", "All Channels").Value

                    SortFilterList()
                End If

                'Remote 9 Button (9) -> Quick Filter 2
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.REMOTE_9 Then
                    MyLog.[Debug]("[CategoriesGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)

                    _FilterByGroup = _layer.GetSetting("ClickfinderQuickTvGroup2", "All Channels").Value

                    SortFilterList()
                End If
            End If

            MyBase.OnAction(action)
        End Sub

        Protected Overrides Sub OnClicked(ByVal controlId As Integer, _
                                  ByVal control As GUIControl, _
                                  ByVal actionType As  _
                                  MediaPortal.GUI.Library.Action.ActionType)

            MyBase.OnClicked(controlId, control, actionType)

            If control Is _btnAllMovies Then
                GuiButtons.AllMovies()
            End If


            If control Is _leftList Or control Is _rightList Then
                If actionType = MediaPortal.GUI.Library.Action.ActionType.ACTION_SELECT_ITEM Then
                    Action_SelectItem()
                End If
            End If

        End Sub

        Private Sub Action_SelectItem()
            RememberLastFocusedItem()

            If _leftList.IsFocused = True Then ListControlClick(_leftList.SelectedListItem.ItemId)
            If _rightList.IsFocused = True Then ListControlClick(_rightList.SelectedListItem.ItemId)
        End Sub


        Protected Overrides Sub OnPreviousWindow()
            MyBase.OnPreviousWindow()
            _LastFocusedIndex = 0
            _LastFocusedControlID = _leftList.GetID
        End Sub


#End Region

#Region "Functions"

        Private Sub GetItemsOnLoad()

            Dim _lastTitle As String = String.Empty
            Dim _ProgramList As List(Of Program)

            Dim _LogLocalSortedBy As String = String.Empty
            Dim _LogLocalMovies As String = String.Empty
            Dim _LogLocalSeries As String = String.Empty
            Dim _LogCategorie As String = String.Empty

            MyLog.Debug("")
            MyLog.Debug("[ItemsGuiWindow] [GetItemsOnLoad]: Thread started")

            _ItemsCache.Clear()
            _ItemsOnLoad.Clear()

            _CurrentCounter = 0


            Translator.SetProperty("#ItemsGroup", _FilterByGroup)

            Select Case _sortedBy
                Case Is = SortMethode.startTime.ToString
                    _LogLocalSortedBy = SortMethode.startTime.ToString
                    _ItemsSqlString = Left(_ItemsSqlString, InStr(_ItemsSqlString, "ORDER BY") - 1) & _
                        Helper.ORDERBYstartTime
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortStartTime)
                Case Is = SortMethode.TvMovieStar.ToString
                    _LogLocalSortedBy = SortMethode.TvMovieStar.ToString
                    _ItemsSqlString = Left(_ItemsSqlString, InStr(_ItemsSqlString, "ORDER BY") - 1) & _
                        Helper.ORDERBYtvMovieBewertung
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortTvMovieStar)
                Case Is = SortMethode.RatingStar.ToString
                    _LogLocalSortedBy = SortMethode.RatingStar.ToString
                    _ItemsSqlString = Left(_ItemsSqlString, InStr(_ItemsSqlString, "ORDER BY") - 1) & _
                        Helper.ORDERBYstarRating
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortRatingStar)
                Case Is = SortMethode.Genre.ToString
                    _LogLocalSortedBy = SortMethode.Genre.ToString
                    _ItemsSqlString = Left(_ItemsSqlString, InStr(_ItemsSqlString, "ORDER BY") - 1) & _
                        Helper.ORDERBYgerne
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortGenre)
                Case Is = SortMethode.parentalRating.ToString
                    _LogLocalSortedBy = SortMethode.parentalRating.ToString
                    _ItemsSqlString = Left(_ItemsSqlString, InStr(_ItemsSqlString, "ORDER BY") - 1) & _
                        Helper.ORDERBYparentalRating
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortparentalRating)
                Case Is = SortMethode.Series.ToString
                    _LogLocalSortedBy = SortMethode.Series.ToString
                    _ItemsSqlString = Left(_ItemsSqlString, InStr(_ItemsSqlString, "ORDER BY") - 1) & _
                        "ORDER BY title ASC, seriesNum ASC, episodeNum ASC, startTime ASC"
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.Episode)
            End Select
            _ItemsSqlString = Replace(_ItemsSqlString, " * ", " Program.IdProgram, Program.Classification, Program.Description, Program.EndTime, Program.EpisodeName, Program.EpisodeNum, Program.EpisodePart, Program.Genre, Program.IdChannel, Program.OriginalAirDate, Program.ParentalRating, Program.SeriesNum, Program.StarRating, Program.StartTime, Program.state, Program.Title ")
            Dim _SQLstate As SqlStatement = Broker.GetStatement(_ItemsSqlString)
            _ProgramList = ObjectFactory.GetCollection(GetType(Program), _SQLstate.Execute())

            'MsgBox("Start: " & _Result.Count)

            If _idCategorie = 0 Then
                _LogCategorie = "none"
            Else
                Dim key As New Gentle.Framework.Key(GetType(ClickfinderCategories), True, "idClickfinderCategories", _idCategorie)
                _LogCategorie = Gentle.Framework.Broker.RetrieveInstance(Of ClickfinderCategories)(key).Name
            End If

            MyLog.Debug("[ItemsGuiWindow] [GetItemsOnLoad]: _result.Count = {0}, sorted by {1}, group = {2}, Categorie = {3}, SQLString: {4}", _
                        _ProgramList.Count, _LogLocalSortedBy, _FilterByGroup, _LogCategorie, _ItemsSqlString)

            If _ProgramList.Count > 0 Then

                'Prüfen ob tvChannel, _CategorieMinRuntime, _FilterByGroup, 
                _ProgramList = _ProgramList.FindAll(Function(p As Program) p.ReferencedChannel.IsTv = True _
                                              And DateDiff(DateInterval.Minute, p.StartTime, p.EndTime) > _CategorieMinRuntime _
                                              And p.ReferencedChannel.GroupNames.Contains(_FilterByGroup))

                'MsgBox("Filter: " & _Result.Count)

                'Wenn Now, beendete programe raus
                If _ClickfinderCategorieView = CategorieView.Now Then
                    _ProgramList = _ProgramList.FindAll(Function(p As Program) p.EndTime > Date.Now)
                    'MsgBox("Now: " & _Result.Count)
                End If

                For Each _program As Program In _ProgramList
                    Try
                        'Dim _Program As Program = Program.Retrieve(_Result.Item(i))

                        'Prüfen ob schon in der Liste vorhanden
                        If String.Equals(_lastTitle, _program.Title & _program.EpisodeName) = False Then

                            'Falls lokale Movies/Videos nicht angezeigt werden sollen -> aus Array entfernen
                            If CBool(_layer.GetSetting("ClickfinderItemsShowLocalMovies", "false").Value) = True Then
                                Try
                                    Dim _TvMovieProgram As TVMovieProgram = TVMovieProgram.Retrieve(_program.IdProgram)
                                    If _TvMovieProgram.local = True And _TvMovieProgram.idSeries = 0 Then
                                        _LogLocalMovies = _LogLocalMovies & _TvMovieProgram.ReferencedProgram.Title & ", "
                                        Continue For
                                    End If
                                Catch ex As Exception
                                End Try
                            End If

                            'Falls lokale Serien nicht angezeigt werden sollen -> aus Array entfernen
                            If CBool(_layer.GetSetting("ClickfinderItemsShowLocalSeries", "false").Value) = True Then
                                Try
                                    Dim _TvMovieProgram As TVMovieProgram = TVMovieProgram.Retrieve(_program.IdProgram)
                                    If _TvMovieProgram.local = True And _TvMovieProgram.idSeries > 0 Then
                                        _LogLocalSeries = _LogLocalSeries & _TvMovieProgram.ReferencedProgram.Title & " - S" & _TvMovieProgram.ReferencedProgram.SeriesNum & "E" & _TvMovieProgram.ReferencedProgram.EpisodeNum & ", "
                                        Continue For
                                    End If
                                Catch ex As Exception
                                End Try
                            End If

                            _ItemsOnLoad.Add(getTvMovieProgram(_program))

                            _lastTitle = _program.Title & _program.EpisodeName
                        End If

                    Catch ex As Exception
                        MyLog.Error("[ItemsGuiWindow] [GetItemsOnLoad]: Loop: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
                    End Try
                Next

            End If

            _ItemsCache = _ItemsOnLoad.FindAll(Function(p As TVMovieProgram) p.ReferencedProgram.ReferencedChannel.GroupNames.Contains(_FilterByGroup))

            'MsgBox("Ende: " & _ItemsCache.Count)
            MyLog.[Debug]("[ItemsGuiWindow] [GetItemsOnLoad]: _ItemsCache= {0}", _ItemsCache.Count)


            If CBool(_layer.GetSetting("ClickfinderItemsShowLocalSeries", "false").Value) = True Then
                MyLog.Debug("[ItemsGuiWindow] [GetItemsOnLoad]: Series Episodes exist local and will not be displayed ({0})", _LogLocalSeries)
            End If
            If CBool(_layer.GetSetting("ClickfinderItemsShowLocalMovies", "false").Value) = True Then
                MyLog.Debug("[ItemsGuiWindow] [GetItemsOnLoad]: Movies exist local and will not be displayed ({0})", _LogLocalMovies)
            End If

            MyLog.Debug("[ItemsGuiWindow] [GetItemsOnLoad]: Thread finished")
            MyLog.Debug("")

            Dim _Thread1 As New Thread(AddressOf FillLeftList)
            Dim _Thread2 As New Thread(AddressOf FillRightList)
            _Thread1.Start()
            _Thread2.Start()
        End Sub

        Private Sub FillLeftList()

            Dim _lastTitle As String = String.Empty
            Dim _ItemCounter As Integer = 0
            Dim _timeLabel As String = String.Empty
            Dim _infoLabel As String = String.Empty
            Dim _startTime As String = String.Empty
            Dim _imagepath As String = String.Empty
            Dim _ForEndCondition As Integer = 0
            Dim _program As Program = Nothing


            MyLog.Debug("")
            MyLog.Debug("[ItemsGuiWindow] [FillLeftList]: Thread started")

            _isAbortException = False


            _leftList.Visible = False
            _leftList.AllocResources()
            _leftList.Clear()

            For i = 1 To 6
                Translator.SetProperty("#ItemsListTvMovieStar" & i, "")
                Translator.SetProperty("#ItemsListImage" & i, "")
                Translator.SetProperty("#ItemsListRatingStar" & i, 0)
            Next

            'Damit der Counter nicht größer wird als _ItemsCache.Count, sonst ERROR Meldung für größere Werte
            If _CurrentCounter + 6 > _ItemsCache.Count Then
                _ForEndCondition = _ItemsCache.Count
            Else
                _ForEndCondition = _CurrentCounter + 6
            End If

            Try

                For i = _CurrentCounter To _ForEndCondition - 1
                    Try

                        'ProgramDaten über idProgram laden
                        _program = _ItemsCache.Item(i).ReferencedProgram

                        'If String.Equals(_lastTitle, _program.Title & _program.EpisodeName) = False Then
                        _ItemCounter = _ItemCounter + 1

                        'TvMovieProgram laden / erstellen wenn nicht vorhanden
                        Dim _TvMovieProgram As TVMovieProgram = _ItemsCache.Item(i)

                        If _TvMovieProgram.idSeries > 0 Then
                            Helper.CheckSeriesLocalStatus(_TvMovieProgram)
                        End If

                        Translator.SetProperty("#ItemsListRatingStar" & _ItemCounter, GuiLayout.ratingStar(_program))
                        Translator.SetProperty("#ItemsListTvMovieStar" & _ItemCounter, GuiLayout.TvMovieStar(_TvMovieProgram))
                        Translator.SetProperty("#ItemsListImage" & _ItemCounter, GuiLayout.Image(_TvMovieProgram))

                        AddListControlItem(_leftList, _program.IdProgram, _program.ReferencedChannel.DisplayName, _program.Title, GuiLayout.TimeLabel(_TvMovieProgram), GuiLayout.InfoLabel(_TvMovieProgram), , , GuiLayout.RecordingStatus(_program))

                        MyLog.Debug("[ItemsGuiWindow] [FillLeftList]: Add ListItem {0} (Title: {1}, Channel: {2}, startTime: {3}, idprogram: {4}, ratingStar: {5}, TvMovieStar: {6}, image: {7})", _
                                            _ItemCounter, _program.Title, _program.ReferencedChannel.DisplayName, _
                                            _program.StartTime, _program.IdProgram, _
                                            GuiLayout.ratingStar(_TvMovieProgram.ReferencedProgram), _
                                            _TvMovieProgram.TVMovieBewertung, GuiLayout.Image(_TvMovieProgram))

                    Catch ex2 As ThreadAbortException
                        MyLog.Debug("[ItemsGuiWindow] [FillLeftList]: --- THREAD ABORTED ---")
                        MyLog.Debug("")
                    Catch ex2 As GentleException
                    Catch ex2 As Exception
                        MyLog.Error("[ItemsGuiWindow] [FillLeftList]: Loop: exception err: {0} stack: {1}", ex2.Message, ex2.StackTrace)
                    End Try
                Next

                _LeftProgressBar.Visible = False
                _leftList.Visible = True

                MyLog.Debug("[ItemsGuiWindow] [FillLeftList]: Thread finished")
                MyLog.Debug("")


                GUIListControl.SelectItemControl(GetID, _LastFocusedControlID, _LastFocusedIndex)
                GUIListControl.FocusControl(GetID, _LastFocusedControlID)


            Catch ex As ThreadAbortException
                MyLog.Debug("[ItemsGuiWindow] [FillLeftList]: --- THREAD ABORTED ---")
                MyLog.Debug("")
            Catch ex As GentleException
            Catch ex As Exception
                MyLog.Error("[ItemsGuiWindow] [FillLeftList]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try
        End Sub
        Private Sub FillRightList()

            Dim _lastTitle As String = String.Empty
            Dim _ItemCounter As Integer = 6
            Dim _timeLabel As String = String.Empty
            Dim _infoLabel As String = String.Empty
            Dim _startTime As String = String.Empty
            Dim _imagepath As String = String.Empty
            Dim _ForEndCondition As Integer = 0
            Dim _program As Program = Nothing

            MyLog.Debug("")
            MyLog.Debug("[ItemsGuiWindow] [FillRightList]: Thread started")

            Translator.SetProperty("#CurrentPageLabel", Translation.PageLabel & " " & CShort((_CurrentCounter + 12) / 12) & " / " & CShort((_ItemsCache.Count) / 12 + 0.5))
            _PageProgress.Percentage = (CShort((_CurrentCounter + 12) / 12)) * 100 / (CShort((_ItemsCache.Count) / 12 + 0.5))

            _isAbortException = False


            _rightList.Visible = False
            _rightList.AllocResources()
            _rightList.Clear()

            For i = 7 To 12
                Translator.SetProperty("#ItemsListTvMovieStar" & i, "")
                Translator.SetProperty("#ItemsListImage" & i, "")
                Translator.SetProperty("#ItemsListRatingStar" & i, 0)
            Next


            'Damit der Counter nicht größer wird als _ItemsCache.Count, sonst ERROR Meldung für größere Werte
            If _CurrentCounter + 12 > _ItemsCache.Count Then
                _ForEndCondition = _ItemsCache.Count
            Else
                _ForEndCondition = _CurrentCounter + 12
            End If

            Try

                For i = _CurrentCounter + 6 To _ForEndCondition - 1
                    Try

                        'ProgramDaten über idProgram laden
                        _program = _ItemsCache.Item(i).ReferencedProgram

                        'If String.Equals(_lastTitle, _program.Title & _program.EpisodeName) = False Then
                        _ItemCounter = _ItemCounter + 1

                        'TvMovieProgram laden / erstellen wenn nicht vorhanden
                        Dim _TvMovieProgram As TVMovieProgram = _ItemsCache.Item(i)

                        If _TvMovieProgram.idSeries > 0 Then
                            Helper.CheckSeriesLocalStatus(_TvMovieProgram)
                        End If

                        Translator.SetProperty("#ItemsListRatingStar" & _ItemCounter, GuiLayout.ratingStar(_program))
                        Translator.SetProperty("#ItemsListTvMovieStar" & _ItemCounter, GuiLayout.TvMovieStar(_TvMovieProgram))
                        Translator.SetProperty("#ItemsListImage" & _ItemCounter, GuiLayout.Image(_TvMovieProgram))

                        AddListControlItem(_rightList, _program.IdProgram, _program.ReferencedChannel.DisplayName, _program.Title, GuiLayout.TimeLabel(_TvMovieProgram), GuiLayout.InfoLabel(_TvMovieProgram), , , GuiLayout.RecordingStatus(_program))

                        MyLog.Debug("[ItemsGuiWindow] [FillRightList]: Add ListItem {0} (Title: {1}, Channel: {2}, startTime: {3}, idprogram: {4}, ratingStar: {5}, TvMovieStar: {6}, image: {7})", _
                                            _ItemCounter, _program.Title, _program.ReferencedChannel.DisplayName, _
                                            _program.StartTime, _program.IdProgram, _
                                            GuiLayout.ratingStar(_TvMovieProgram.ReferencedProgram), _
                                            _TvMovieProgram.TVMovieBewertung, GuiLayout.Image(_TvMovieProgram))

                    Catch ex2 As ThreadAbortException
                        MyLog.Debug("[ItemsGuiWindow] [FillRightList]: --- THREAD ABORTED ---")
                        MyLog.Debug("")
                    Catch ex2 As GentleException
                    Catch ex2 As Exception
                        MyLog.Error("[ItemsGuiWindow] [FillRightList]: Loop: exception err: {0} stack: {1}", ex2.Message, ex2.StackTrace)
                    End Try
                Next

                _RightProgressBar.Visible = False
                _rightList.Visible = True

                MyLog.Debug("[ItemsGuiWindow] [FillRightList]: Thread finished")
                MyLog.Debug("")

                GUIListControl.SelectItemControl(GetID, _LastFocusedControlID, _LastFocusedIndex)
                GUIListControl.FocusControl(GetID, _LastFocusedControlID)


            Catch ex As ThreadAbortException
                MyLog.Debug("[ItemsGuiWindow] [FillRightList]: --- THREAD ABORTED ---")
                MyLog.Debug("")
            Catch ex As GentleException
            Catch ex As Exception
                MyLog.Error("[ItemsGuiWindow] [FillRightList]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try
        End Sub

        'ProgresBar paralell anzeigen
        Private Sub ShowLeftProgressBar()
            _LeftProgressBar.Visible = True
        End Sub
        Private Sub ShowRightProgressBar()
            _RightProgressBar.Visible = True
        End Sub

        Private Sub AbortRunningThreads()

            Try
                If _ThreadLeftList.IsAlive = True Then
                    _ThreadLeftList.Abort()
                    _ThreadRightList.Abort()
                ElseIf _ThreadRightList.IsAlive = True Then
                    _ThreadRightList.Abort()
                    _ThreadLeftList.Abort()
                ElseIf _ThreadLeftList.IsAlive = True And _ThreadRightList.IsAlive = True Then
                    _ThreadLeftList.Abort()
                    _ThreadRightList.Abort()
                End If

            Catch ex3 As Exception ' Ignore this exception
                'Eventuell auftretende Exception abfangen
                ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
            End Try
        End Sub

        Private Sub RememberLastFocusedItem()
            If _leftList.IsFocused Then
                'MsgBox(_MovieList.SelectedListItemIndex)
                _LastFocusedIndex = _leftList.SelectedListItemIndex
                _LastFocusedControlID = _leftList.GetID
            ElseIf _rightList.IsFocused Then
                _LastFocusedIndex = _rightList.SelectedListItemIndex
                _LastFocusedControlID = _rightList.GetID
            Else
                _LastFocusedIndex = 0
                _LastFocusedControlID = _leftList.GetID
            End If
        End Sub

#End Region

#Region "MediaPortal Funktionen / Dialogs"
        Private Sub ShowItemsContextMenu(ByVal idProgram As Integer)
            Dim dlgContext As GUIDialogMenu = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_MENU, Integer)), GUIDialogMenu)
            dlgContext.Reset()

            Dim _Program As Program = Program.Retrieve(idProgram)

            'ContextMenu Layout
            dlgContext.SetHeading(_Program.Title)
            dlgContext.ShowQuickNumbers = True

            'Refresh
            If _ClickfinderCategorieView = CategorieView.Now And _idCategorie > 0 Then
                Dim lItemRefresh As New GUIListItem
                lItemRefresh.Label = Translation.Refresh
                dlgContext.Add(lItemRefresh)
                lItemRefresh.Dispose()
            End If

            'Sort SubMenu
            Dim lItemSort As New GUIListItem
            lItemSort.Label = Translation.Sortby
            dlgContext.Add(lItemSort)
            lItemSort.Dispose()

            'Filter: TvGroup
            Dim lItemGroup As New GUIListItem
            lItemGroup.Label = Translation.Filterby
            dlgContext.Add(lItemGroup)
            lItemGroup.Dispose()

            'Action SubMenu
            Dim lItemActionOn As New GUIListItem
            lItemActionOn.Label = Translation.action
            dlgContext.Add(lItemActionOn)
            lItemActionOn.Dispose()

            dlgContext.DoModal(GUIWindowManager.ActiveWindow)

            Select Case dlgContext.SelectedLabelText
                Case Is = Translation.Sortby
                    ShowSortMenu()
                Case Is = Translation.action
                    ShowActionMenu(_Program)
                Case Is = Translation.Refresh
                    Dim key As New Gentle.Framework.Key(GetType(ClickfinderCategories), True, "idClickfinderCategories", _idCategorie)
                    Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(key)
                    ItemsGuiWindow.SetGuiProperties(CStr(Replace(Replace(_Categorie.SqlString, "#startTime", MySqlDate(PeriodeStartTime.AddMinutes(CDbl(_layer.GetSetting("ClickfinderNowOffset", "-20").Value)))), "#endTime", MySqlDate(PeriodeEndTime))), _Categorie.MinRunTime, _Categorie.sortedBy, _Categorie.idClickfinderCategories)
                    Translator.SetProperty("#ItemsLeftListLabel", _Categorie.Name & " " & Translation.von & " " & Format(PeriodeStartTime.Hour, "00") & ":" & Format(PeriodeStartTime.Minute, "00") & " - " & Format(PeriodeEndTime.Hour, "00") & ":" & Format(PeriodeEndTime.Minute, "00"))
                    GUIWindowManager.ActivateWindow(1656544653)
                Case Is = Translation.Filterby
                    ShowFilterMenu()
            End Select

            dlgContext.Dispose()
            dlgContext.AllocResources()

        End Sub
        Private Sub ShowSortMenu()
            Dim dlgContext As GUIDialogMenu = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_MENU, Integer)), GUIDialogMenu)
            dlgContext.Reset()

            'ContextMenu Layout
            dlgContext.SetHeading(Translation.Sortby)
            dlgContext.ShowQuickNumbers = True

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

            Dim lItemGenre As New GUIListItem
            lItemGenre.Label = Translation.SortGenre
            dlgContext.Add(lItemGenre)
            lItemGenre.Dispose()

            Dim lItemparentalRating As New GUIListItem
            lItemparentalRating.Label = Translation.SortparentalRating
            dlgContext.Add(lItemparentalRating)
            lItemparentalRating.Dispose()

            dlgContext.DoModal(GetID)

            Select Case dlgContext.SelectedLabel
                Case Is = 0
                    _sortedBy = SortMethode.startTime.ToString

                    SortFilterList()
                Case Is = 1
                    _sortedBy = SortMethode.TvMovieStar.ToString

                    SortFilterList()
                Case Is = 2
                    _sortedBy = SortMethode.RatingStar.ToString

                    SortFilterList()
                Case Is = 3
                    _sortedBy = SortMethode.Genre.ToString

                    SortFilterList()
                Case Is = 4
                    _sortedBy = SortMethode.parentalRating.ToString

                    SortFilterList()
            End Select

            dlgContext.Dispose()
            dlgContext.AllocResources()

        End Sub

        Private Sub GuiLayoutLoading()

            _leftList.Visible = False
            _rightList.Visible = False


            Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowLeftProgressBar)
            _ProgressBarThread.Start()

            Dim _ProgressBarThread2 As New Threading.Thread(AddressOf ShowRightProgressBar)
            _ProgressBarThread2.Start()

            Translator.SetProperty("#CurrentPageLabel", Translation.Loading)

            For i = 1 To 12
                Translator.SetProperty("#ItemsListTvMovieStar" & i, "")
                Translator.SetProperty("#ItemsListImage" & i, "")
                Translator.SetProperty("#ItemsListRatingStar" & i, 0)
            Next

        End Sub
        Private Sub SaveSortedByToClickfinderCategories()
            If Not _idCategorie = 0 And CBool(_layer.GetSetting("ClickfinderRemberSortedBy", "true").Value) = True Then
                Dim key As New Gentle.Framework.Key(GetType(ClickfinderCategories), True, "idClickfinderCategories", _idCategorie)
                Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(key)
                _Categorie.sortedBy = _sortedBy
                _Categorie.Persist()
            End If

        End Sub

        Private Sub ShowFilterMenu()
            Dim dlgContext As GUIDialogMenu = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_MENU, Integer)), GUIDialogMenu)
            dlgContext.Reset()

            'ContextMenu Layout
            dlgContext.SetHeading(Translation.Filterby)
            dlgContext.ShowQuickNumbers = True

            Dim _groups As IList(Of ChannelGroup) = ChannelGroup.ListAll

            For i = 0 To _groups.Count - 1
                Dim lItem As New GUIListItem
                lItem.Label = _groups(i).GroupName
                dlgContext.Add(lItem)
                lItem.Dispose()
            Next

            dlgContext.DoModal(GetID)

            If Not String.IsNullOrEmpty(dlgContext.SelectedLabelText) Then

                Dim _Thread1 As New Thread(AddressOf FillLeftList)
                Dim _Thread2 As New Thread(AddressOf FillRightList)

                _FilterByGroup = dlgContext.SelectedLabelText

                SortFilterList()

            End If


            dlgContext.Dispose()
            dlgContext.AllocResources()

        End Sub
        Private Sub SortFilterList()

            GuiLayoutLoading()

            Translator.SetProperty("#ItemsGroup", _FilterByGroup)

            Dim _Thread1 As New Thread(AddressOf FillLeftList)
            Dim _Thread2 As New Thread(AddressOf FillRightList)

            _ItemsCache.Clear()

            'TvFilter
            _ItemsCache = _ItemsOnLoad.FindAll(Function(p As TVMovieProgram) p.ReferencedProgram.ReferencedChannel.GroupNames.Contains(_FilterByGroup))

            Select Case _sortedBy
                Case Is = SortMethode.startTime.ToString
                    'StartZeit
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortStartTime)
                    Dim _StartTime As TVMovieProgram_SortByStartTime = New TVMovieProgram_SortByStartTime
                    _ItemsCache.Sort(_StartTime)

                    '_ItemsCache.OrderBy(Function(y As TVMovieProgram) y.ReferencedProgram.StartTime)

                    '_ItemsCache.Sort(Function(p1 As TVMovieProgram, p2 As TVMovieProgram) p1.ReferencedProgram.StartTime.CompareTo(p2.ReferencedProgram.StartTime))

                Case Is = SortMethode.TvMovieStar.ToString()
                    'TvMovieStar
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortTvMovieStar)
                    Dim _TvMovieBewertung As TVMovieProgram_SortByTvMovieBewertung = New TVMovieProgram_SortByTvMovieBewertung
                    _ItemsCache.Sort(_TvMovieBewertung)

                    '_ItemsCache.Sort(Function(p1 As TVMovieProgram, p2 As TVMovieProgram) p2.TVMovieBewertung.CompareTo(p1.TVMovieBewertung))
                Case Is = SortMethode.RatingStar.ToString
                    'RatingStar
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortRatingStar)
                    Dim _RatingStar As TVMovieProgram_SortByRating = New TVMovieProgram_SortByRating
                    _ItemsCache.Sort(_RatingStar)

                    '_ItemsCache.Sort(Function(p1 As TVMovieProgram, p2 As TVMovieProgram) p2.ReferencedProgram.StarRating.CompareTo(p1.ReferencedProgram.StarRating))
                Case Is = SortMethode.Genre.ToString
                    'Genre
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortGenre)
                    Dim _Genre As TVMovieProgram_SortByGenre = New TVMovieProgram_SortByGenre
                    _ItemsCache.Sort(_Genre)

                    '_ItemsCache.Sort(Function(p1 As TVMovieProgram, p2 As TVMovieProgram) p1.ReferencedProgram.Genre.CompareTo(p2.ReferencedProgram.Genre))
                Case Is = SortMethode.parentalRating.ToString
                    'FSK
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortparentalRating)
                    Dim _ParentalRating As TVMovieProgram_SortByParentalRating = New TVMovieProgram_SortByParentalRating
                    _ItemsCache.Sort(_ParentalRating)

                    '_ItemsCache.Sort(Function(p1 As TVMovieProgram, p2 As TVMovieProgram) p2.ReferencedProgram.ParentalRating.CompareTo(p1.ReferencedProgram.ParentalRating))
            End Select

            _CurrentCounter = 0

            SaveSortedByToClickfinderCategories()

            _Thread1.Start()
            _Thread2.Start()

        End Sub

#End Region

    End Class
End Namespace

