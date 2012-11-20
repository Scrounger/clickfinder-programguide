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
        Private _ThreadSortFilterItems As Threading.Thread
        Private _isAbortException As Boolean = False
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

            _FilterByGroup = CPGsettings.StandardTvGroup

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

            If _ThreadSortFilterItems.IsAlive = True Then
                _ThreadSortFilterItems.Abort()
            End If

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

                    Return
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

                    Return
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

                                ItemsGuiWindow.SetGuiProperties(CStr(Replace(Replace(_Categorie.SqlString, "#startTime", MySqlDate(PeriodeStartTime.AddMinutes(CPGsettings.NowOffset))), "#endTime", MySqlDate(PeriodeEndTime))), _Categorie.MinRunTime, _Categorie.sortedBy, _Categorie.idClickfinderCategories)
                                Translator.SetProperty("#ItemsLeftListLabel", _Categorie.Name & " " & Translation.von & " " & Format(PeriodeStartTime.Hour, "00") & ":" & Format(PeriodeStartTime.Minute, "00") & " - " & Format(PeriodeEndTime.Hour, "00") & ":" & Format(PeriodeEndTime.Minute, "00"))
                                GUIWindowManager.ActivateWindow(1656544653)
                            Else
                                If Not _FilterByGroup = CPGsettings.StandardTvGroup Then
                                    _FilterByGroup = CPGsettings.StandardTvGroup
                                    StartSortFilterListThread()
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
                            If Not _FilterByGroup = CPGsettings.StandardTvGroup Then
                                _FilterByGroup = CPGsettings.StandardTvGroup
                                StartSortFilterListThread()
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

                    _FilterByGroup = CPGsettings.QuickTvGroup1

                    StartSortFilterListThread()
                End If

                'Remote 9 Button (9) -> Quick Filter 2
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.REMOTE_9 Then
                    MyLog.[Debug]("[CategoriesGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)

                    _FilterByGroup = CPGsettings.QuickTvGroup2

                    StartSortFilterListThread()
                    Return
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

            Dim _LogLocalSortedBy As String = String.Empty
            Dim _LogCategorie As String = String.Empty

            Dim _timer As Date = Date.Now

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
                Case Is = SortMethode.Title.ToString
                    _LogLocalSortedBy = SortMethode.Series.ToString
                    _ItemsSqlString = Left(_ItemsSqlString, InStr(_ItemsSqlString, "ORDER BY") - 1) & _
                        "ORDER BY title ASC, starRating DESC, startTime ASC"
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.Title)
            End Select

            _ItemsSqlString = Replace(_ItemsSqlString, " * ", " TVMovieProgram.idProgram, TVMovieProgram.Action, TVMovieProgram.Actors, TVMovieProgram.BildDateiname, TVMovieProgram.Country, TVMovieProgram.Cover, TVMovieProgram.Describtion, TVMovieProgram.Dolby, TVMovieProgram.EpisodeImage, TVMovieProgram.Erotic, TVMovieProgram.FanArt, TVMovieProgram.Feelings, TVMovieProgram.FileName, TVMovieProgram.Fun, TVMovieProgram.HDTV, TVMovieProgram.idEpisode, TVMovieProgram.idMovingPictures, TVMovieProgram.idSeries, TVMovieProgram.idTVMovieProgram, TVMovieProgram.idVideo, TVMovieProgram.KurzKritik, TVMovieProgram.local, TVMovieProgram.needsUpdate, TVMovieProgram.Regie, TVMovieProgram.Requirement, TVMovieProgram.SeriesPosterImage, TVMovieProgram.ShortDescribtion, TVMovieProgram.Tension, TVMovieProgram.TVMovieBewertung, TVMovieProgram.Year ")
            Dim _SQLstate As SqlStatement = Broker.GetStatement(_ItemsSqlString)
            _ItemsOnLoad = ObjectFactory.GetCollection(GetType(TVMovieProgram), _SQLstate.Execute())

            If _idCategorie = 0 Then
                _LogCategorie = "none"
            Else
                Dim key As New Gentle.Framework.Key(GetType(ClickfinderCategories), True, "idClickfinderCategories", _idCategorie)
                _LogCategorie = Gentle.Framework.Broker.RetrieveInstance(Of ClickfinderCategories)(key).Name
            End If

            If _ItemsOnLoad.Count > 0 Then

                'Prüfen ob tvChannel, _CategorieMinRuntime 
                _ItemsOnLoad = _ItemsOnLoad.FindAll(Function(p As TVMovieProgram) p.ReferencedProgram.ReferencedChannel.IsTv = True _
                                              And DateDiff(DateInterval.Minute, p.ReferencedProgram.StartTime, p.ReferencedProgram.EndTime) > _CategorieMinRuntime)

                'Wenn Now, beendete programe raus
                If _ClickfinderCategorieView = CategorieView.Now Then
                    _ItemsOnLoad = _ItemsOnLoad.FindAll(Function(p As TVMovieProgram) p.ReferencedProgram.EndTime > Date.Now)
                End If

            End If

            MyLog.Debug("[ItemsGuiWindow] [GetItemsOnLoad]: _ItemsOnLoad.Count = {0}, sorted by {1}, group = {2}, Categorie = {3}", _
                        _ItemsOnLoad.Count, _LogLocalSortedBy, _FilterByGroup, _LogCategorie)

            'Falls lokale Movies/Videos nicht angezeigt werden sollen -> aus Array entfernen
            If CPGsettings.ItemsShowLocalMovies = True Then
                _ItemsOnLoad.RemoveAll(Function(p As TVMovieProgram) p.local = True And p.idSeries = 0)
            End If

            'Falls lokale Serien nicht angezeigt werden sollen -> aus Array entfernen
            If CPGsettings.ItemsShowLocalSeries = True Then
                _ItemsOnLoad.RemoveAll(Function(p As TVMovieProgram) p.local = True And p.idSeries > 0)
            End If

            'Duplicate raus: Title,EpisodeName, Channel, StartZeit gleich
            _ItemsOnLoad = _ItemsOnLoad.Distinct(New TVMovieProgram_GroupByTitleEpisodeNameIdChannelStarTime).ToList

            'Standard TvGruppe
            _ItemsCache = _ItemsOnLoad.FindAll(Function(p As TVMovieProgram) p.ReferencedProgram.ReferencedChannel.GroupNames.Contains(_FilterByGroup))

            MyLog.[Debug]("[ItemsGuiWindow] [GetItemsOnLoad]: _ItemsCache.Count = {0}, time: {1}s", _ItemsCache.Count, DateDiff(DateInterval.Second, _timer, Date.Now))
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
                    ItemsGuiWindow.SetGuiProperties(CStr(Replace(Replace(_Categorie.SqlString, "#startTime", MySqlDate(PeriodeStartTime.AddMinutes(CPGsettings.NowOffset))), "#endTime", MySqlDate(PeriodeEndTime))), _Categorie.MinRunTime, _Categorie.sortedBy, _Categorie.idClickfinderCategories)
                    Translator.SetProperty("#ItemsLeftListLabel", _Categorie.Name & " " & Translation.von & " " & Format(PeriodeStartTime.Hour, "00") & ":" & Format(PeriodeStartTime.Minute, "00") & " - " & Format(PeriodeEndTime.Hour, "00") & ":" & Format(PeriodeEndTime.Minute, "00"))
                    GUIWindowManager.ActivateWindow(1656544653)
                Case Is = Translation.Filterby
                    ShowFilterMenu()
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
            If Not _idCategorie = 0 And CPGsettings.RemberSortedBy = True Then
                Dim key As New Gentle.Framework.Key(GetType(ClickfinderCategories), True, "idClickfinderCategories", _idCategorie)
                Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(key)
                _Categorie.sortedBy = _sortedBy
                _Categorie.Persist()
            End If

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

            Dim lItemparentalTitle As New GUIListItem
            lItemparentalTitle.Label = Translation.SortTitle
            dlgContext.Add(lItemparentalTitle)
            lItemparentalTitle.Dispose()

            Dim lItemparentalRating As New GUIListItem
            lItemparentalRating.Label = Translation.SortparentalRating
            dlgContext.Add(lItemparentalRating)
            lItemparentalRating.Dispose()

            Dim lItemAction As New GUIListItem
            lItemAction.Label = Translation.ActionLabel
            dlgContext.Add(lItemAction)
            lItemAction.Dispose()

            Dim lItemFun As New GUIListItem
            lItemFun.Label = Translation.FunLabel
            dlgContext.Add(lItemFun)
            lItemFun.Dispose()

            Dim lItemErotic As New GUIListItem
            lItemErotic.Label = Translation.EroticLabel
            dlgContext.Add(lItemErotic)
            lItemErotic.Dispose()

            Dim lItemTension As New GUIListItem
            lItemTension.Label = Translation.SuspenseLabel
            dlgContext.Add(lItemTension)
            lItemTension.Dispose()

            Dim lItemFeelings As New GUIListItem
            lItemFeelings.Label = Translation.EmotionsLabel
            dlgContext.Add(lItemFeelings)
            lItemFeelings.Dispose()

            Dim lItemRequirement As New GUIListItem
            lItemRequirement.Label = Translation.LevelLabel
            dlgContext.Add(lItemRequirement)
            lItemRequirement.Dispose()

            dlgContext.DoModal(GetID)

            Select Case dlgContext.SelectedLabel
                Case Is = 0
                    _sortedBy = SortMethode.startTime.ToString

                Case Is = 1
                    _sortedBy = SortMethode.TvMovieStar.ToString

                Case Is = 2
                    _sortedBy = SortMethode.RatingStar.ToString

                Case Is = 3
                    _sortedBy = SortMethode.Genre.ToString

                Case Is = 4
                    _sortedBy = SortMethode.Title.ToString

                Case Is = 5
                    _sortedBy = SortMethode.parentalRating.ToString

                Case Is = 6
                    _sortedBy = SortMethode.Action.ToString

                Case Is = 7
                    _sortedBy = SortMethode.Fun.ToString

                Case Is = 8
                    _sortedBy = SortMethode.Erotic.ToString

                Case Is = 9
                    _sortedBy = SortMethode.Tension.ToString

                Case Is = 10
                    _sortedBy = SortMethode.Feelings.ToString

                Case Is = 11
                    _sortedBy = SortMethode.Requirement.ToString


            End Select

            StartSortFilterListThread()

            dlgContext.Dispose()
            dlgContext.AllocResources()

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

                StartSortFilterListThread()

            End If


            dlgContext.Dispose()
            dlgContext.AllocResources()

        End Sub

        Private Sub StartSortFilterListThread()
            Try

                GuiLayoutLoading()

                MyLog.Debug("[ItemsGuiWindow] [StartSortFilterListThread]: Filter by TvGroup: {0}, sorted by: {1}", _FilterByGroup, _sortedBy)

                _ThreadSortFilterItems = New Threading.Thread(AddressOf SortFilterListThread)
                _ThreadSortFilterItems.Start()

            Catch ex As ThreadAbortException
                MyLog.Debug("[ItemsGuiWindow] [StartSortFilterListThread]: --- THREAD ABORTED ---")
                MyLog.Debug("")
            Catch ex As GentleException
            Catch ex As Exception
                MyLog.Error("[ItemsGuiWindow] [StartSortFilterListThread]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try

        End Sub

        Private Sub SortFilterListThread()
            Try

       
                'Dim _GuiLayoutLoading As New Threading.Thread(AddressOf GuiLayoutLoading)
                '_GuiLayoutLoading.Start()

                Translator.SetProperty("#ItemsGroup", _FilterByGroup)

                _ItemsCache.Clear()

                'TvFilter
                _ItemsCache = _ItemsOnLoad.FindAll(Function(p As TVMovieProgram) p.ReferencedProgram.ReferencedChannel.GroupNames.Contains(_FilterByGroup))

                'Duplicate raus: Title,EpisodeName, Channel, StartZeit
                _ItemsCache = _ItemsCache.Distinct(New TVMovieProgram_GroupByTitleEpisodeNameIdChannelStarTime).ToList

                Select Case _sortedBy
                    Case Is = SortMethode.startTime.ToString
                        'StartZeit
                        Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortStartTime)
                        _ItemsCache.Sort(New TVMovieProgram_SortByStartTime)

                    Case Is = SortMethode.TvMovieStar.ToString()
                        'TvMovieStar
                        Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortTvMovieStar)
                        _ItemsCache.Sort(New TVMovieProgram_SortByTvMovieBewertung)

                    Case Is = SortMethode.RatingStar.ToString
                        'RatingStar
                        Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortRatingStar)
                        _ItemsCache.Sort(New TVMovieProgram_SortByRating)

                    Case Is = SortMethode.Genre.ToString
                        'Genre
                        Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortGenre)
                        _ItemsCache.Sort(New TVMovieProgram_SortByGenre)

                    Case Is = SortMethode.parentalRating.ToString
                        'FSK
                        Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortparentalRating)
                        _ItemsCache.Sort(New TVMovieProgram_SortByParentalRating)

                    Case Is = SortMethode.Title.ToString
                        'Titel
                        Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortTitle)
                        _ItemsCache.Sort(New TVMovieProgram_SortByTitle)

                    Case Is = SortMethode.Action.ToString
                        'Action
                        Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.ActionLabel)
                        _ItemsCache.Sort(New TVMovieProgram_SortByAction)

                    Case Is = SortMethode.Fun.ToString
                        'Spaß
                        Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.FunLabel)
                        _ItemsCache.Sort(New TVMovieProgram_SortByFun)

                    Case Is = SortMethode.Erotic.ToString
                        'Erotik
                        Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.EroticLabel)
                        _ItemsCache.Sort(New TVMovieProgram_SortByErotic)

                    Case Is = SortMethode.Feelings.ToString
                        'Gefühl
                        Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.EmotionsLabel)
                        _ItemsCache.Sort(New TVMovieProgram_SortByFeelings)

                    Case Is = SortMethode.Tension.ToString
                        'Spannung
                        Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SuspenseLabel)
                        _ItemsCache.Sort(New TVMovieProgram_SortByTension)

                    Case Is = SortMethode.Requirement.ToString
                        'Anspruch
                        Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.LevelLabel)
                        _ItemsCache.Sort(New TVMovieProgram_SortByRequirement)

                End Select

                _CurrentCounter = 0

                SaveSortedByToClickfinderCategories()

                _ThreadLeftList = New Thread(AddressOf FillLeftList)
                _ThreadRightList = New Thread(AddressOf FillRightList)

                _ThreadLeftList.Start()
                _ThreadRightList.Start()


            Catch ex As ThreadAbortException
                MyLog.Debug("[ItemsGuiWindow] [SortFilterListThread]: --- THREAD ABORTED ---")
                MyLog.Debug("")
            Catch ex As GentleException
            Catch ex As Exception
                MyLog.Error("[ItemsGuiWindow] [SortFilterListThread]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try

        End Sub

#End Region

    End Class
End Namespace

