Imports MediaPortal.GUI.Library
Imports TvDatabase
Imports ClickfinderProgramGuide.ClickfinderProgramGuide.HighlightsGuiWindow
Imports Gentle.Framework
Imports enrichEPG.TvDatabase
Imports ClickfinderProgramGuide.Helper
Imports MediaPortal.Configuration
Imports ClickfinderProgramGuide.TvDatabase
Imports MediaPortal.Dialogs
Imports System.Threading
Imports ClickfinderProgramGuide.ClickfinderProgramGuide.CategoriesGuiWindow
Imports Gentle.Common
Imports System.Text

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
        Friend Shared _CurrentCounter As Integer = 0
        Private _ThreadLoadItemsFromDatabase As Threading.Thread
        Private _ThreadLeftList As Threading.Thread
        Private _ThreadRightList As Threading.Thread

        'Friend Shared _idCategorie As Integer = 0
        'Friend Shared _sortedBy As String = String.Empty
        'Private _FilterByGroup As String = String.Empty
        Private _LastFocusedIndex As Integer
        Private _LastFocusedControlID As Integer

        'Friend Shared _SqlStringOnLoad As String = String.Empty



        Private Shared m_SqlString As String = String.Empty
        Private Shared m_StartTime As Date = Nothing
        Private Shared m_EndTime As Date = Nothing
        Private Shared m_MinRuntime As Integer = 0
        Private Shared m_sortedBy As String = String.Empty
        Private Shared m_TvGroupFilter As String = String.Empty
        Private Shared m_RemoveLocalMovies As Boolean
        Private Shared m_RemoveLocalSeries As Boolean
        Private Shared m_LogCategoryName As String = String.Empty
        Private Shared m_idCategorie As Integer = 0
        Private Shared m_CategorieView As CategorieView = CategorieView.none

#End Region

#Region "Constructors"

        Public Sub New()
        End Sub
        'Public Shared Sub SetGuiProperties(ByVal idprogram As Integer, ByVal PeriodeStartTime As Date, ByVal PeriodeEndTime As Date, Optional ByVal Genre As String = "%")

        Friend Shared Sub SetGuiProperties(ByVal SqlString As String, ByVal startTime As Date, ByVal endTime As Date, Optional ByVal sortedBy As String = "startTime", Optional ByVal TvGroupFilter As String = "All Channels", Optional ByVal MinRunTime As Integer = 0, Optional ByVal RemoveLocalMovies As Boolean = False, Optional ByVal RemoveLocalSeries As Boolean = False, Optional ByVal LogCategoryName As String = "none", Optional ByVal CategoryID As Integer = 0, Optional ByVal CategorieView As CategorieView = CategorieView.none)
            m_SqlString = SqlString
            m_LogCategoryName = LogCategoryName
            m_TvGroupFilter = TvGroupFilter
            m_sortedBy = sortedBy
            m_RemoveLocalMovies = RemoveLocalMovies
            m_RemoveLocalSeries = RemoveLocalSeries
            m_idCategorie = CategoryID
            m_StartTime = startTime
            m_EndTime = endTime
            m_MinRuntime = MinRunTime

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
            MyLog.Info("[ItemsGuiWindow] Category: {0} (TvGroup: {1}, sorted by: {2}, ShowLocalMovies: {3}, ShowLocalSeries: {4})", m_LogCategoryName, m_TvGroupFilter, m_sortedBy, Not (m_RemoveLocalMovies), Not (m_RemoveLocalSeries))

            GuiLayoutLoading()

            GuiLayout.SetSettingLastUpdateProperty()

            If _ItemsCache.Count = 0 Then
                _ThreadLoadItemsFromDatabase = New Threading.Thread(AddressOf LoadItemsFromDatabase)
                _ThreadLoadItemsFromDatabase.IsBackground = True
                _ThreadLoadItemsFromDatabase.Start()
            Else
                _ThreadLeftList = New Thread(AddressOf FillLeftList)
                _ThreadRightList = New Thread(AddressOf FillRightList)
                _ThreadLeftList.IsBackground = True
                _ThreadRightList.IsBackground = True
                _ThreadLeftList.Start()
                _ThreadRightList.Start()
            End If

        End Sub

        Protected Overrides Sub OnPageDestroy(ByVal new_windowId As Integer)
            For i = 1 To 12
                Translator.SetProperty("#ItemsListTvMovieStar" & i, "")
                Translator.SetProperty("#ItemsListImage" & i, "")
                Translator.SetProperty("#ItemsListRatingStar" & i, 0)
            Next

            RememberLastFocusedItem()

            AbortRunningThreads()

            MyBase.OnPageDestroy(new_windowId)

            Dispose()
            AllocResources()
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

                    MyLog.Info("")
                    MyLog.Info("[ItemsGuiWindow] [OnAction]: Keypress - Actiontype={0}: page = {1}", action.wID.ToString, CShort((_CurrentCounter + 12) / 12) & " / " & CShort((_ItemsCache.Count) / 12 + 0.5))
                    MyLog.Info("")

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

                    MyLog.Info("")
                    MyLog.Info("[ItemsGuiWindow] [OnAction]: Keypress - Actiontype={0}: page = {1}", action.wID.ToString, CShort((_CurrentCounter + 12) / 12) & " / " & CShort((_ItemsCache.Count) / 12 + 0.5))
                    MyLog.Info("")

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


                'Remote 1
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.REMOTE_1 Then
                    RemoteKeyPressed(1)
                    Return
                End If

                'Remote 2
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.REMOTE_2 Then
                    RemoteKeyPressed(2)
                    Return
                End If

                'Remote 3
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.REMOTE_3 Then
                    RemoteKeyPressed(3)
                    Return
                End If

                'Remote 4
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.REMOTE_4 Then
                    RemoteKeyPressed(4)
                    Return
                End If

                'Remote 5 Button (5) -> zu Heute springen
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.REMOTE_5 Then
                    RemoteKeyPressed(5)
                    Return
                End If

                'Remote 6
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.REMOTE_6 Then
                    RemoteKeyPressed(6)
                    Return
                End If

                'Remote 7
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.REMOTE_7 Then
                    RemoteKeyPressed(7)
                    Return
                End If

                'Remote 8
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.REMOTE_8 Then
                    RemoteKeyPressed(8)
                    Return
                End If

                'Remote 9
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.REMOTE_9 Then
                    RemoteKeyPressed(9)
                    Return
                End If

                'Remote 0 - complet refresh
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.REMOTE_0 Then
                    RemoteKeyPressed(0)
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

        Private Sub RemoteKeyPressed(ByVal RemoteKey As Integer)

            AbortRunningThreads()

            If Not CPGsettings.ItemsRemoteSortAll(RemoteKey) = String.Empty Then
                m_sortedBy = CPGsettings.ItemsRemoteSortAll(RemoteKey)
            End If
            If Not CPGsettings.ItemsRemoteTvGroupAll(RemoteKey) = String.Empty Then
                m_TvGroupFilter = CPGsettings.ItemsRemoteTvGroupAll(RemoteKey)
            End If

            MyLog.Info("")

            _ThreadLoadItemsFromDatabase = New Threading.Thread(AddressOf LoadItemsFromDatabase)
            _ThreadLoadItemsFromDatabase.IsBackground = True
            _ThreadLoadItemsFromDatabase.Start()
        End Sub

#End Region

#Region "Functions"

        Friend Shared Function GetSqlCPGFilterString(ByVal TvGroupFilter As String, ByVal ShowLocalMovies As Boolean, ByVal ShowLocalSeries As Boolean, ByVal MinRuntime As Integer) As String
            '#CPGFilter vorbereiten
            Dim _CPGFilter As New StringBuilder("(SELECT groupmap.idgroup FROM mptvdb.groupmap Inner join mptvdb.channelgroup ON groupmap.idgroup = channelgroup.idGroup WHERE groupmap.idchannel = program.idchannel and channelgroup.groupName = '#FilterTvGroup')")

            'TvGroup Filter setzen
            _CPGFilter.Replace("#FilterTvGroup", TvGroupFilter)

            'Categorie MinRuntime
            If MinRuntime > 0 Then _CPGFilter.Append(String.Format(" AND TIMESTAMPDIFF(MINUTE,program.starttime, program.endtime) > {0}", _
                                                              MinRuntime))

            'Falls lokale Movies/Videos nicht angezeigt werden sollen -> aus Array entfernen
            If ShowLocalMovies = True Then _CPGFilter.Append(" AND NOT (TvMovieProgram.local = 1 And TvMovieProgram.idSeries = 0)")

            'Falls lokale Serien nicht angezeigt werden sollen -> aus Array entfernen
            If ShowLocalSeries = True Then _CPGFilter.Append(" AND NOT (TvMovieProgram.local = 1 And TvMovieProgram.idSeries > 0)")

            'Wenn Now, beendete programe raus
            If m_CategorieView = CategorieView.Now Then _CPGFilter.Append(" AND program.endTime > NOW()")

            Return _CPGFilter.ToString

        End Function

        Private Shared Function GetSqlCPGOrderByString(ByVal sort As String)

            Select Case sort

                Case Is = SortMethode.startTime.ToString
                    'StartZeit
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortStartTime)
                    Return Helper.ORDERBYstartTime

                Case Is = SortMethode.TvMovieStar.ToString()
                    'TvMovieStar
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortTvMovieStar)
                    Return Helper.ORDERBYTvMovieStar

                Case Is = SortMethode.RatingStar.ToString
                    'RatingStar
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortRatingStar)
                    Return Helper.ORDERBYstarRating

                Case Is = SortMethode.Genre.ToString
                    'Genre
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortGenre)
                    Return Helper.ORDERBYgerne

                Case Is = SortMethode.Year.ToString
                    'Jahr
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.Year)
                    Return Helper.ORDERBYYear

                Case Is = SortMethode.parentalRating.ToString
                    'FSK
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortparentalRating)
                    Return Helper.ORDERBYparentalRating

                Case Is = SortMethode.Title.ToString
                    'Titel
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortTitle)
                    Return Helper.ORDERBYTitle

                Case Is = SortMethode.Action.ToString
                    'Action
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.ActionLabel)
                    Return Helper.ORDERBYAction

                Case Is = SortMethode.Fun.ToString
                    'Spaß
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.FunLabel)
                    Return Helper.ORDERBYFun

                Case Is = SortMethode.Erotic.ToString
                    'Erotik
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.EroticLabel)
                    Return Helper.ORDERBYErotic

                Case Is = SortMethode.Feelings.ToString
                    'Gefühl
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.EmotionsLabel)
                    Return Helper.ORDERBYFeelings

                Case Is = SortMethode.Tension.ToString
                    'Spannung
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SuspenseLabel)
                    Return Helper.ORDERBYTension

                Case Is = SortMethode.Requirement.ToString
                    'Anspruch
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.LevelLabel)
                    Return Helper.ORDERBYRequirement

                Case Is = SortMethode.Series
                    'Serien
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " Series")
                    Return Helper.ORDERBYSeries

                Case Is = SortMethode.Episode
                    'Episoden
                    Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.Episode)
                    Return Helper.ORDERBYEpisode
                Case Else
                    Return Helper.ORDERBYstartTime
            End Select
        End Function

        Private Sub LoadItemsFromDatabase()
            Try

                MyLog.Info("")
                MyLog.Info("[ItemsGuiWindow] [LoadItemsFromDatabase]: Thread started...")
                Dim _timer As Date = Date.Now
                Dim _totaltimer As Date = Date.Now

                GuiLayoutLoading()

                _ItemsCache.Clear()

                _CurrentCounter = 0

                Translator.SetProperty("#ItemsGroup", m_TvGroupFilter)


                'SQL String bauen
                Dim _SqlStringBuilder As New StringBuilder(m_SqlString)
                _SqlStringBuilder.Replace("#StartTime", MySqlDate(m_startTime))
                _SqlStringBuilder.Replace("#EndTime", MySqlDate(m_endTime))
                _SqlStringBuilder.Replace("#CPGFilter", GetSqlCPGFilterString(m_TvGroupFilter, m_RemoveLocalMovies, m_RemoveLocalSeries, m_MinRunTime))

                Select Case m_CategorieView
                    Case Is = CategorieView.Preview
                        _SqlStringBuilder.Replace("#CPGgroupBy", "GROUP BY program.title, program.episodeName")
                    Case Is = CategorieView.none
                        _SqlStringBuilder.Replace("#CPGgroupBy", "GROUP BY program.title, program.episodeName")
                    Case Is = CategorieView.Day
                        _SqlStringBuilder.Replace("#CPGgroupBy", "GROUP BY program.title, program.episodeName")
                    Case Else
                        'Now, PrimeTime, LateTime
                        If m_sortedBy = SortMethode.startTime.ToString Then
                            _SqlStringBuilder.Replace("#CPGgroupBy", "GROUP BY program.title, program.episodeName, program.idChannel, program.startTime")
                        Else
                            _SqlStringBuilder.Replace("#CPGgroupBy", "GROUP BY program.title, program.episodeName, program.idChannel")
                        End If
                End Select

                'If m_sortedBy = SortMethode.startTime.ToString And Not m_CategorieView = CategorieView.Preview Then
                '    _SqlStringBuilder.Replace("#CPGgroupBy", "GROUP BY program.title, program.episodeName, program.idChannel, program.startTime")
                'ElseIf m_CategorieView = CategorieView.Preview Then
                '    _SqlStringBuilder.Replace("#CPGgroupBy", "GROUP BY program.title, program.episodeName")
                'Else
                '    _SqlStringBuilder.Replace("#CPGgroupBy", "GROUP BY program.title, program.episodeName, program.idChannel")
                'End If

                _SqlStringBuilder.Replace("#CPGorderBy", GetSqlCPGOrderByString(m_sortedBy))
                _SqlStringBuilder.Replace(" * ", " TVMovieProgram.idProgram, TVMovieProgram.Action, TVMovieProgram.Actors, TVMovieProgram.BildDateiname, TVMovieProgram.Country, TVMovieProgram.Cover, TVMovieProgram.Describtion, TVMovieProgram.Dolby, TVMovieProgram.EpisodeImage, TVMovieProgram.Erotic, TVMovieProgram.FanArt, TVMovieProgram.Feelings, TVMovieProgram.FileName, TVMovieProgram.Fun, TVMovieProgram.HDTV, TVMovieProgram.idEpisode, TVMovieProgram.idMovingPictures, TVMovieProgram.idSeries, TVMovieProgram.idVideo, TVMovieProgram.KurzKritik, TVMovieProgram.local, TVMovieProgram.Regie, TVMovieProgram.Requirement, TVMovieProgram.SeriesPosterImage, TVMovieProgram.ShortDescribtion, TVMovieProgram.Tension, TVMovieProgram.TVMovieBewertung ")

                MyLog.Debug("")
                MyLog.Debug("[ItemsGuiWindow] [SetGuiProperties]: startTime = {0}, endTime = {1}", m_StartTime, m_EndTime)
                MyLog.Debug("[ItemsGuiWindow] [SetGuiProperties]: SQLstring = {0}", _SqlStringBuilder.ToString)
                MyLog.Debug("")

                Dim _SQLstate As SqlStatement = Broker.GetStatement(_SqlStringBuilder.ToString)
                Dim _ItemsOnLoad As List(Of TVMovieProgram) = ObjectFactory.GetCollection(GetType(TVMovieProgram), _SQLstate.Execute())

                MyLog.Info("[ItemsGuiWindow] [LoadItemsFromDatabase]: {0} entries readed from database in {1}s", _ItemsOnLoad.Count, (DateTime.Now - _timer).TotalSeconds)

                If _ItemsOnLoad.Count > 0 Then

                    _ItemsCache = _ItemsOnLoad

                    _ThreadLeftList = New Thread(AddressOf FillLeftList)
                    _ThreadRightList = New Thread(AddressOf FillRightList)
                    _ThreadLeftList.IsBackground = True
                    _ThreadRightList.IsBackground = True
                    _ThreadLeftList.Start()
                    _ThreadRightList.Start()
                Else
                    Helper.Notify("Nichts gefunden !")
                End If

                MyLog.Info("[ItemsGuiWindow] [LoadItemsFromDatabase]: Thread finished in {0}s", (DateTime.Now - _totaltimer).TotalSeconds)

            Catch ex As ThreadAbortException
                MyLog.Info("[ItemsGuiWindow] [LoadItemsFromDatabase]: --- THREAD ABORTED ---")
            Catch ex As GentleException
            Catch ex As Exception
                MyLog.Error("[ItemsGuiWindow] [LoadItemsFromDatabase]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try
        End Sub

        Private Sub FillLeftList()

            Dim _timer As Date = Date.Now
            Dim _ItemCounter As Integer = 0
            Dim _ForEndCondition As Integer = 0

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
                        'If String.Equals(_lastTitle, _program.Title & _program.EpisodeName) = False Then
                        _ItemCounter = _ItemCounter + 1

                        'TvMovieProgram laden / erstellen wenn nicht vorhanden
                        Dim _TvMovieProgram As TVMovieProgram = _ItemsCache.Item(i)

                        If _TvMovieProgram.idSeries > 0 Then
                            Helper.CheckSeriesLocalStatus(_TvMovieProgram)
                        End If

                        Translator.SetProperty("#ItemsListRatingStar" & _ItemCounter, GuiLayout.ratingStar(_TvMovieProgram.ReferencedProgram))
                        Translator.SetProperty("#ItemsListTvMovieStar" & _ItemCounter, GuiLayout.TvMovieStar(_TvMovieProgram))
                        Translator.SetProperty("#ItemsListImage" & _ItemCounter, GuiLayout.Image(_TvMovieProgram))

                        AddListControlItem(_leftList, _TvMovieProgram.idProgram, _TvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName, _TvMovieProgram.ReferencedProgram.Title, GuiLayout.TimeLabel(_TvMovieProgram), GuiLayout.InfoLabel(_TvMovieProgram), , , GuiLayout.RecordingStatus(_TvMovieProgram.ReferencedProgram))

                    Catch ex2 As ThreadAbortException
                        MyLog.Info("[ItemsGuiWindow] [FillLeftList]: --- THREAD ABORTED ---")
                    Catch ex2 As GentleException
                    Catch ex2 As Exception
                        MyLog.Error("[ItemsGuiWindow] [FillLeftList]: Loop: exception err: {0} stack: {1}", ex2.Message, ex2.StackTrace)
                    End Try
                Next

                _LeftProgressBar.Visible = False
                _leftList.Visible = True

                MyLog.Info("[ItemsGuiWindow] [FillLeftList]: Thread finished in {0}s", (DateTime.Now - _timer).TotalSeconds)

                GUIListControl.SelectItemControl(GetID, _LastFocusedControlID, _LastFocusedIndex)
                GUIListControl.FocusControl(GetID, _LastFocusedControlID)


            Catch ex As ThreadAbortException
                MyLog.Info("[ItemsGuiWindow] [FillLeftList]: --- THREAD ABORTED ---")
            Catch ex As GentleException
            Catch ex As Exception
                MyLog.Error("[ItemsGuiWindow] [FillLeftList]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try
        End Sub
        Private Sub FillRightList()

            Dim _timer As Date = Date.Now
            Dim _ItemCounter As Integer = 6
            Dim _ForEndCondition As Integer = 0

            Translator.SetProperty("#CurrentPageLabel", Translation.PageLabel & " " & CShort((_CurrentCounter + 12) / 12) & " / " & CShort((_ItemsCache.Count) / 12 + 0.5))
            _PageProgress.Percentage = (CShort((_CurrentCounter + 12) / 12)) * 100 / (CShort((_ItemsCache.Count) / 12 + 0.5))

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

                        'If String.Equals(_lastTitle, _program.Title & _program.EpisodeName) = False Then
                        _ItemCounter = _ItemCounter + 1

                        'TvMovieProgram laden / erstellen wenn nicht vorhanden
                        Dim _TvMovieProgram As TVMovieProgram = _ItemsCache.Item(i)

                        If _TvMovieProgram.idSeries > 0 Then
                            Helper.CheckSeriesLocalStatus(_TvMovieProgram)
                        End If

                        Translator.SetProperty("#ItemsListRatingStar" & _ItemCounter, GuiLayout.ratingStar(_TvMovieProgram.ReferencedProgram))
                        Translator.SetProperty("#ItemsListTvMovieStar" & _ItemCounter, GuiLayout.TvMovieStar(_TvMovieProgram))
                        Translator.SetProperty("#ItemsListImage" & _ItemCounter, GuiLayout.Image(_TvMovieProgram))

                        AddListControlItem(_rightList, _TvMovieProgram.idProgram, _TvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName, _TvMovieProgram.ReferencedProgram.Title, GuiLayout.TimeLabel(_TvMovieProgram), GuiLayout.InfoLabel(_TvMovieProgram), , , GuiLayout.RecordingStatus(_TvMovieProgram.ReferencedProgram))

                    Catch ex2 As ThreadAbortException
                        MyLog.Info("[ItemsGuiWindow] [FillRightList]: --- THREAD ABORTED ---")
                    Catch ex2 As GentleException
                    Catch ex2 As Exception
                        MyLog.Error("[ItemsGuiWindow] [FillRightList]: Loop: exception err: {0} stack: {1}", ex2.Message, ex2.StackTrace)
                    End Try
                Next

                _RightProgressBar.Visible = False
                _rightList.Visible = True

                MyLog.Info("[ItemsGuiWindow] [FillRightList]: Thread finished in {0}s", (DateTime.Now - _timer).TotalSeconds)

                GUIListControl.SelectItemControl(GetID, _LastFocusedControlID, _LastFocusedIndex)
                GUIListControl.FocusControl(GetID, _LastFocusedControlID)

            Catch ex As ThreadAbortException
                MyLog.Info("[ItemsGuiWindow] [FillRightList]: --- THREAD ABORTED ---")
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
                End If
            Catch ex As Exception
            End Try

            Try
                If _ThreadRightList.IsAlive = True Then
                    _ThreadRightList.Abort()
                End If
            Catch ex As Exception
            End Try

            Try
                If _ThreadLoadItemsFromDatabase.IsAlive = True Then
                    _ThreadLoadItemsFromDatabase.Abort()
                End If
            Catch ex As Exception
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

        'Private Sub StartSortFilterListThread()
        '    Try

        '        GuiLayoutLoading()

        '        _ThreadSortFilterItems = New Threading.Thread(AddressOf SortFilterListThread)
        '        _ThreadSortFilterItems.Start()

        '    Catch ex As ThreadAbortException
        '        MyLog.Debug("[ItemsGuiWindow] [StartSortFilterListThread]: --- THREAD ABORTED ---")
        '    Catch ex As GentleException
        '    Catch ex As Exception
        '        MyLog.Error("")
        '        MyLog.Error("[ItemsGuiWindow] [StartSortFilterListThread]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
        '    End Try

        'End Sub

        'Private Sub SortFilterListThread()
        '    Try
        '        Dim _timer As Date = Date.Now

        '        MyLog.Info("")

        '        Translator.SetProperty("#ItemsGroup", _FilterByGroup)

        '        'TvFilter: inkl. sortieren da aus _ItemsOnLoad
        '        If Not _LastFilteredBy = _FilterByGroup Then
        '            _CurrentCounter = 0
        '            MyLog.Info("[ItemsGuiWindow] [SortFilterListThread]: Thread started... (Filter by TvGroup: {0}, sorted by: {1}, ItemsOnLoad.Count = {2})", _FilterByGroup, _sortedBy, _ItemsOnLoad.Count)
        '            _ItemsCache.Clear()
        '            _LastFilteredBy = _FilterByGroup
        '            _ItemsCache = _ItemsOnLoad.Where(Function(x) x.ReferencedProgram.ReferencedChannel.GroupNames.Contains(_FilterByGroup)).ToList


        '            '_ItemsCache = _ItemsOnLoad.FindAll(Function(p As TVMovieProgram) p.ReferencedProgram.ReferencedChannel.GroupNames.Contains(_FilterByGroup))
        '            MyLog.Info("[ItemsGuiWindow] [SortFilterListThread]: {0} entries filtered by TvGroup: {1} ({2}s)", _ItemsCache.Count, _FilterByGroup, (DateTime.Now - _timer).TotalSeconds)

        '            SortList()
        '            MyLog.Info("[ItemsGuiWindow] [SortFilterListThread]: Thread finished in {0}s (_ItemsCache.Count = {1})", (DateTime.Now - _timer).TotalSeconds, _ItemsCache.Count)
        '        ElseIf Not _LastSortedBy = _sortedBy Then
        '            'Sort: nur sortieren
        '            _CurrentCounter = 0
        '            MyLog.Info("[ItemsGuiWindow] [SortFilterListThread]: Thread started... (sorted by: {0}, ItemsCache.Count = {1})", _sortedBy, _ItemsCache.Count)
        '            _LastSortedBy = _sortedBy
        '            SortList()
        '            MyLog.Info("[ItemsGuiWindow] [SortFilterListThread]: Thread finished in {0}s (_ItemsCache.Count = {1})", (DateTime.Now - _timer).TotalSeconds, _ItemsCache.Count)
        '        Else
        '            MyLog.Info("[ItemsGuiWindow] [SortFilterListThread]: Nothing to do...")
        '        End If

        '        SaveSortedByToClickfinderCategories()

        '        _ThreadLeftList = New Thread(AddressOf FillLeftList)
        '        _ThreadRightList = New Thread(AddressOf FillRightList)
        '        _ThreadLeftList.IsBackground = True
        '        _ThreadRightList.IsBackground = True
        '        _ThreadLeftList.Start()
        '        _ThreadRightList.Start()

        '    Catch ex As ThreadAbortException
        '        MyLog.Debug("[ItemsGuiWindow] [SortFilterListThread]: --- THREAD ABORTED ---")
        '    Catch ex As GentleException
        '    Catch ex As Exception
        '        MyLog.Error("")
        '        MyLog.Error("[ItemsGuiWindow] [SortFilterListThread]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
        '    End Try

        'End Sub

        'Private Sub SortList()
        '    Dim _timer As Date = Now

        '    Select Case _sortedBy
        '        Case Is = SortMethode.startTime.ToString
        '            'StartZeit
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortStartTime)
        '            _ItemsCache = _ItemsCache.OrderBy(Function(x) x.ReferencedProgram.StartTime).ThenByDescending(Function(x) x.ReferencedProgram.StarRating).ToList

        '        Case Is = SortMethode.TvMovieStar.ToString()
        '            'TvMovieStar
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortTvMovieStar)
        '            _ItemsCache = _ItemsCache.OrderByDescending(Function(x) x.TVMovieBewertung).ThenByDescending(Function(x) x.ReferencedProgram.StarRating).ToList

        '        Case Is = SortMethode.RatingStar.ToString
        '            'RatingStar
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortRatingStar)
        '            _ItemsCache = _ItemsCache.OrderByDescending(Function(x) x.ReferencedProgram.StarRating).ThenBy(Function(x) x.ReferencedProgram.StartTime.Date).ToList

        '        Case Is = SortMethode.Genre.ToString
        '            'Genre
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortGenre)
        '            _ItemsCache = _ItemsCache.OrderBy(Function(x) x.ReferencedProgram.Genre).ThenByDescending(Function(x) x.ReferencedProgram.StarRating).ToList

        '        Case Is = SortMethode.Year.ToString
        '            'Year
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.Year)
        '            _ItemsCache = _ItemsCache.OrderByDescending(Function(x) x.ReferencedProgram.OriginalAirDate).ThenByDescending(Function(x) x.ReferencedProgram.StarRating).ToList

        '        Case Is = SortMethode.parentalRating.ToString
        '            'FSK
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortparentalRating)
        '            _ItemsCache = _ItemsCache.OrderByDescending(Function(x) x.ReferencedProgram.ParentalRating).ThenByDescending(Function(x) x.ReferencedProgram.StarRating).ToList

        '        Case Is = SortMethode.Title.ToString
        '            'Titel
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortTitle)
        '            _ItemsCache = _ItemsCache.OrderBy(Function(x) x.ReferencedProgram.Title).ThenByDescending(Function(x) x.ReferencedProgram.StarRating).ToList

        '        Case Is = SortMethode.Action.ToString
        '            'Action
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.ActionLabel)
        '            _ItemsCache = _ItemsCache.OrderByDescending(Function(x) x.Action).ThenByDescending(Function(x) x.ReferencedProgram.StarRating).ToList

        '        Case Is = SortMethode.Fun.ToString
        '            'Spaß
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.FunLabel)
        '            _ItemsCache = _ItemsCache.OrderByDescending(Function(x) x.Fun).ThenByDescending(Function(x) x.ReferencedProgram.StarRating).ToList

        '        Case Is = SortMethode.Erotic.ToString
        '            'Erotik
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.EroticLabel)
        '            _ItemsCache = _ItemsCache.OrderByDescending(Function(x) x.Erotic).ThenByDescending(Function(x) x.ReferencedProgram.StarRating).ToList

        '        Case Is = SortMethode.Feelings.ToString
        '            'Gefühl
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.EmotionsLabel)
        '            _ItemsCache = _ItemsCache.OrderByDescending(Function(x) x.Feelings).ThenByDescending(Function(x) x.ReferencedProgram.StarRating).ToList

        '        Case Is = SortMethode.Tension.ToString
        '            'Spannung
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SuspenseLabel)
        '            _ItemsCache = _ItemsCache.OrderByDescending(Function(x) x.Tension).ThenByDescending(Function(x) x.ReferencedProgram.StarRating).ToList

        '        Case Is = SortMethode.Requirement.ToString
        '            'Anspruch
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.LevelLabel)
        '            _ItemsCache = _ItemsCache.OrderByDescending(Function(x) x.Requirement).ThenByDescending(Function(x) x.ReferencedProgram.StarRating).ToList
        '    End Select
        '    MyLog.Info("[ItemsGuiWindow] [SortFilterListThread]: {0} entries sorted by: {1} ({2}s)", _ItemsCache.Count, _sortedBy, (DateTime.Now - _timer).TotalSeconds)
        'End Sub

        'Private Function GetSqlOrderBySortClause(ByVal SQLstring As String) As String

        '    'Vorhandene OrderByClause entfernen, sofern vorhanden
        '    SQLstring = Left(SQLstring, InStr(SQLstring, "ORDER BY") - 1)

        '    Select Case _sortedBy
        '        Case Is = SortMethode.startTime.ToString
        '            'StartZeit
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortStartTime)
        '            SQLstring = SQLstring & Helper.ORDERBYstartTime

        '        Case Is = SortMethode.TvMovieStar.ToString()
        '            'TvMovieStar
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortTvMovieStar)
        '            SQLstring = SQLstring & Helper.ORDERBYTvMovieStar

        '        Case Is = SortMethode.RatingStar.ToString
        '            'RatingStar
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortRatingStar)
        '            SQLstring = SQLstring & Helper.ORDERBYstarRating

        '        Case Is = SortMethode.Genre.ToString
        '            'Genre
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortGenre)
        '            SQLstring = SQLstring & Helper.ORDERBYgerne

        '        Case Is = SortMethode.Year.ToString
        '            'Jahr
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.Year)
        '            SQLstring = SQLstring & Helper.ORDERBYYear

        '        Case Is = SortMethode.parentalRating.ToString
        '            'FSK
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortparentalRating)
        '            SQLstring = SQLstring & Helper.ORDERBYparentalRating

        '        Case Is = SortMethode.Title.ToString
        '            'Titel
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SortTitle)
        '            SQLstring = SQLstring & Helper.ORDERBYTitle

        '        Case Is = SortMethode.Action.ToString
        '            'Action
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.ActionLabel)
        '            SQLstring = SQLstring & Helper.ORDERBYAction

        '        Case Is = SortMethode.Fun.ToString
        '            'Spaß
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.FunLabel)
        '            SQLstring = SQLstring & Helper.ORDERBYFun

        '        Case Is = SortMethode.Erotic.ToString
        '            'Erotik
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.EroticLabel)
        '            SQLstring = SQLstring & Helper.ORDERBYErotic

        '        Case Is = SortMethode.Feelings.ToString
        '            'Gefühl
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.EmotionsLabel)
        '            SQLstring = SQLstring & Helper.ORDERBYFeelings

        '        Case Is = SortMethode.Tension.ToString
        '            'Spannung
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.SuspenseLabel)
        '            SQLstring = SQLstring & Helper.ORDERBYTension

        '        Case Is = SortMethode.Requirement.ToString
        '            'Anspruch
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.LevelLabel)
        '            SQLstring = SQLstring & Helper.ORDERBYRequirement

        '        Case Is = SortMethode.Series
        '            'Serien
        '            Translator.SetProperty("#ItemsRightListLabel", Translation.SortbyGuiItems & " " & Translation.Episode)
        '            SQLstring = SQLstring & Helper.ORDERBYSeries
        '    End Select


        '    Return SQLstring

        'End Function

#End Region

#Region "MediaPortal Funktionen / Dialogs"
        Private Sub ShowItemsContextMenu(ByVal idProgram As Integer)
            MyLog.Info("")
            MyLog.Info("[ItemsGuiWindow] [ShowItemsContextMenu]: open")
            MyLog.Info("")
            Dim dlgContext As GUIDialogMenu = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_MENU, Integer)), GUIDialogMenu)
            dlgContext.Reset()

            Dim _Program As Program = Program.Retrieve(idProgram)

            'ContextMenu Layout
            dlgContext.SetHeading(_Program.Title)
            dlgContext.ShowQuickNumbers = True

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
                Case Is = Translation.Filterby
                    ShowFilterMenu()
            End Select

            dlgContext.Dispose()
            dlgContext.AllocResources()

        End Sub

        Private Sub ShowSortMenu()
            MyLog.Info("")
            MyLog.Info("[ItemsGuiWindow] [ShowSortMenu]: open")
            MyLog.Info("")

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

            Dim lItemYear As New GUIListItem
            lItemYear.Label = Translation.Year
            dlgContext.Add(lItemYear)
            lItemYear.Dispose()

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
                    m_sortedBy = SortMethode.startTime.ToString

                Case Is = 1
                    m_sortedBy = SortMethode.TvMovieStar.ToString

                Case Is = 2
                    m_sortedBy = SortMethode.RatingStar.ToString

                Case Is = 3
                    m_sortedBy = SortMethode.Genre.ToString

                Case Is = 4
                    m_sortedBy = SortMethode.Year.ToString

                Case Is = 5
                    m_sortedBy = SortMethode.Title.ToString

                Case Is = 6
                    m_sortedBy = SortMethode.parentalRating.ToString

                Case Is = 7
                    m_sortedBy = SortMethode.Action.ToString

                Case Is = 8
                    m_sortedBy = SortMethode.Fun.ToString

                Case Is = 9
                    m_sortedBy = SortMethode.Erotic.ToString

                Case Is = 10
                    m_sortedBy = SortMethode.Tension.ToString

                Case Is = 11
                    m_sortedBy = SortMethode.Feelings.ToString

                Case Is = 12
                    m_sortedBy = SortMethode.Requirement.ToString

            End Select

            _ThreadLoadItemsFromDatabase = New Threading.Thread(AddressOf LoadItemsFromDatabase)
            _ThreadLoadItemsFromDatabase.IsBackground = True
            _ThreadLoadItemsFromDatabase.Start()

            dlgContext.Dispose()
            dlgContext.AllocResources()

        End Sub
        Private Sub ShowFilterMenu()

            MyLog.Info("")
            MyLog.Info("[ItemsGuiWindow] [ShowFilterMenu]: open")
            MyLog.Info("")

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

                m_TvGroupFilter = dlgContext.SelectedLabelText

                _ThreadLoadItemsFromDatabase = New Threading.Thread(AddressOf LoadItemsFromDatabase)
                _ThreadLoadItemsFromDatabase.IsBackground = True
                _ThreadLoadItemsFromDatabase.Start()

            End If

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
            If Not m_idCategorie = 0 And CPGsettings.RemberSortedBy = True Then
                Dim key As New Gentle.Framework.Key(GetType(ClickfinderCategories), True, "idClickfinderCategories", m_idCategorie)
                Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(key)
                _Categorie.sortedBy = m_sortedBy
                _Categorie.Persist()
            End If

        End Sub


#End Region

    End Class
End Namespace

