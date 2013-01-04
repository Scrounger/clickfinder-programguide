Imports MediaPortal.GUI.Library
Imports TvDatabase
Imports ClickfinderProgramGuide.ClickfinderProgramGuide.HighlightsGuiWindow
Imports Gentle.Framework
Imports ClickfinderProgramGuide.Helper
Imports MediaPortal.Configuration
Imports ClickfinderProgramGuide.TvDatabase
Imports MediaPortal.Dialogs
Imports System.Threading
Imports Gentle.Common
Imports enrichEPG.TvDatabase
Imports System.Text


Namespace ClickfinderProgramGuide
    Public Class CategoriesGuiWindow
        Inherits GUIWindow

#Region "Skin Controls"

        'Buttons



        <SkinControlAttribute(5)> Protected _btnAllMovies As GUIButtonControl = Nothing



        <SkinControlAttribute(9)> Protected ctlProgressBar As GUIAnimation = Nothing

        <SkinControlAttribute(10)> Protected _CategorieList As GUIListControl = Nothing
        <SkinControlAttribute(30)> Protected _PreviewList As GUIListControl = Nothing

#End Region

#Region "Members"
        Enum CategorieView
            Now = 0
            PrimeTime = 1
            LateTime = 2
            Preview = 3
            Day = 4
            none = 5
        End Enum

        Friend Shared _ClickfinderCategorieView As CategorieView
        Private Shared _Day As Date
        Private _CategoriesList As IList(Of ClickfinderCategories)
        Private _SelectedCategorieMinRunTime As Integer
        Private Shared _SelectedCategorieItemId As Integer
        Private _ThreadPreviewListFill As Threading.Thread
        Private _LastFocusedIndex As Integer
        Private _LastFocusedControlID As Integer

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
#End Region

#Region "Properties"
        Private Shared ReadOnly Property CategorieViewName() As String
            Get
                Select Case _ClickfinderCategorieView
                    Case CategorieView.Now
                        Return Translation.Now
                    Case CategorieView.PrimeTime
                        Return Translation.PrimeTime
                    Case CategorieView.LateTime
                        Return Translation.LateTime
                    Case CategorieView.Preview
                        Return Translation.Preview
                End Select
                Return "Fehler"
            End Get
        End Property
        Friend Shared ReadOnly Property PeriodeStartTime() As Date
            Get
                Dim _PrimeTime As Date = CPGsettings.PrimeTime
                Dim _LateTime As Date = CPGsettings.LateTime

                Select Case _ClickfinderCategorieView
                    Case CategorieView.Now
                        Return Date.Now
                    Case CategorieView.PrimeTime
                        Return Date.Today.AddHours(_PrimeTime.Hour).AddMinutes(_PrimeTime.Minute)
                    Case CategorieView.LateTime
                        Return Date.Today.AddHours(_LateTime.Hour).AddMinutes(_LateTime.Minute)
                    Case CategorieView.Preview
                        Return Date.Now
                    Case CategorieView.Day
                        Return _Day
                End Select
                Return "Fehler"
            End Get
        End Property
        Friend Shared ReadOnly Property PeriodeEndTime() As Date
            Get
                Select Case _ClickfinderCategorieView
                    Case CategorieView.Day
                        Return PeriodeStartTime.AddHours(24)
                    Case CategorieView.Preview
                        Return PeriodeStartTime.AddDays(CPGsettings.PreviewMaxDays)
                    Case Else
                        Return PeriodeStartTime.AddHours(4)
                End Select

            End Get
        End Property
#End Region

#Region "GUI Properties"

        Public Overloads Overrides Property GetID() As Integer
            Get
                Return 1656544654
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property


        Public Overloads Overrides Function Init() As Boolean
            'Beim initialisieren des Plugin den Screen laden

            Return Load(GUIGraphicsContext.Skin + "\ClickfinderProgramGuideCategories.xml")
        End Function

        Public Overrides Function GetModuleName() As String
            Return "Clickfinder ProgramGuide Categories"
        End Function
#End Region

#Region "GUI Events"
        Protected Overrides Sub OnPageLoad()
            MyBase.OnPageLoad()
            GUIWindowManager.NeedRefresh()

            Helper.CheckConnectionState()

            MyLog.Info("")
            MyLog.Info("")
            MyLog.Info("[CategoriesGuiWindow] -------------[OPEN]-------------")

            GuiLayout.SetSettingLastUpdateProperty()

            Try

                If Not _loadParameter = String.Empty Then

                    Select Case (_loadParameter)
                        Case Is = "CPG.Now"
                            _ClickfinderCategorieView = CategorieView.Now
                        Case Is = "CPG.PrimeTime"
                            _ClickfinderCategorieView = CategorieView.PrimeTime
                        Case Is = "CPG.LateTime"
                            _ClickfinderCategorieView = CategorieView.LateTime
                        Case Is = "CPG.Preview"
                            _ClickfinderCategorieView = CategorieView.Preview
                        Case Else
                            If InStr(_loadParameter, "CPG.Day:") > 0 Then
                                _ClickfinderCategorieView = CategorieView.Day
                                _Day = CDate(Replace(_loadParameter, "CPG.Day:", ""))
                            Else
                                Helper.ShowNotify("CPG Error: parameter")
                                MyLog.Error("[CategoriesGuiWindow] [OnPageLoad]: parameter: {0}", _loadParameter)
                                Return
                            End If
                    End Select

                End If

                MyLog.Info("[CategoriesGuiWindow] [OnPageLoad]: parameter: {0} ({1})", _ClickfinderCategorieView.ToString, _loadParameter)
                MyLog.Info("[CategoriesGuiWindow] [OnPageLoad]: PeriodeStartTime = {0}, PeriodeEndTime = {1}", _
                            getTranslatedDayOfWeek(PeriodeStartTime.Date) & " " & Format(PeriodeStartTime, "dd.MM.yyyy") & " " & Format(PeriodeStartTime.Hour, "00") & ":" & Format(PeriodeStartTime.Minute, "00"), Format(PeriodeEndTime.Hour, "00") & ":" & Format(PeriodeEndTime.Minute, "00"))

                For i = 1 To 6
                    Translator.SetProperty("#PreviewListImage" & i, "")
                    Translator.SetProperty("#PreviewListTvMovieStar" & i, "")
                    Translator.SetProperty("#PreviewListRatingStar" & i, 0)
                Next

                If _ClickfinderCategorieView = CategorieView.Day Then
                    Translator.SetProperty("#CategorieView", getTranslatedDayOfWeek(_Day) & " " & Format(_Day, "dd.MM.yyyy"))
                ElseIf _ClickfinderCategorieView = CategorieView.Preview Then
                    Translator.SetProperty("#CategorieView", Translation.Preview & " " & Translation.for & " " & DateDiff(DateInterval.Day, PeriodeStartTime, PeriodeEndTime) & " " & Translation.Day)
                Else
                    Translator.SetProperty("#CategorieView", CategorieViewName & " " & Format(PeriodeStartTime.Hour, "00") & ":" & Format(PeriodeStartTime.Minute, "00"))
                End If

                If Not _SelectedCategorieItemId >= 0 Then
                    _SelectedCategorieItemId = _CategorieList.SelectedListItemIndex
                End If

                Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(_SelectedCategorieItemId)
                _SelectedCategorieMinRunTime = _Categorie.MinRunTime


                Dim _Thread1 As New Thread(AddressOf FillCategories)
                _ThreadPreviewListFill = New Thread(AddressOf FillPreviewList)

                _Thread1.Start()
                _ThreadPreviewListFill.Start()

            Catch ex As Exception
                MyLog.Error("[CategoriesGuiWindow] [OnPageLoad]: Loop exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try
        End Sub

        Protected Overrides Sub OnPageDestroy(ByVal new_windowId As Integer)

            For i = 1 To 6
                Translator.SetProperty("#PreviewListImage" & i, "")
                Translator.SetProperty("#PreviewListTvMovieStar" & i, "")
                Translator.SetProperty("#PreviewListRatingStar" & i, 0)
            Next

            RememberLastFocusedItem()

            Try
                If _ThreadPreviewListFill.IsAlive = True Then _ThreadPreviewListFill.Abort()
            Catch ex As Exception
                'Eventuell auftretende Exception abfangen
                ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
            End Try

            MyBase.OnPageDestroy(new_windowId)
            Dispose()
            AllocResources()
        End Sub

        Public Overrides Sub OnAction(ByVal action As MediaPortal.GUI.Library.Action)
            If GUIWindowManager.ActiveWindow = GetID Then
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MOVE_UP Then
                    If _CategorieList.IsFocused = True Then

                        Try
                            If _ThreadPreviewListFill.IsAlive = True Then _ThreadPreviewListFill.Abort()
                        Catch ex As Exception
                            'Eventuell auftretende Exception abfangen
                            ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
                        End Try

                        Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowProgressbar)
                        _ProgressBarThread.Start()

                        If _CategorieList.SelectedListItem.ItemId = _CategorieList.Item(0).ItemId Then
                            _SelectedCategorieItemId = _CategorieList.Item(_CategorieList.Count - 1).ItemId
                            _SelectedCategorieMinRunTime = _CategorieList.Item(_CategorieList.Count - 1).Duration
                        Else
                            _SelectedCategorieItemId = _CategorieList.Item(_CategorieList.SelectedListItemIndex - 1).ItemId
                            _SelectedCategorieMinRunTime = _CategorieList.Item(_CategorieList.SelectedListItemIndex - 1).Duration
                        End If

                        MyLog.Info("")
                        MyLog.Info("[CategoriesGuiWindow] [OnAction]: Keypress - Actiontype={0}: _SelectedCategorieItemId = {1}", action.wID.ToString, _SelectedCategorieItemId)
                        MyLog.Info("")

                        _LastFocusedIndex = _SelectedCategorieItemId
                        _LastFocusedControlID = _CategorieList.GetID

                        _ThreadPreviewListFill = New Threading.Thread(AddressOf FillPreviewList)
                        _ThreadPreviewListFill.IsBackground = True
                        _ThreadPreviewListFill.Start()
                    End If
                End If

                If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MOVE_DOWN Then
                    If _CategorieList.IsFocused = True Then

                        Try
                            If _ThreadPreviewListFill.IsAlive = True Then _ThreadPreviewListFill.Abort()
                        Catch ex As Exception
                            'Eventuell auftretende Exception abfangen
                            ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
                        End Try

                        Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowProgressbar)
                        _ProgressBarThread.Start()

                        If _CategorieList.SelectedListItem.ItemId = _CategorieList.Item(_CategorieList.Count - 1).ItemId Then
                            _SelectedCategorieItemId = _CategorieList.Item(0).ItemId
                            _SelectedCategorieMinRunTime = _CategorieList.Item(0).Duration
                        Else
                            _SelectedCategorieItemId = _CategorieList.Item(_CategorieList.SelectedListItemIndex + 1).ItemId
                            _SelectedCategorieMinRunTime = _CategorieList.Item(_CategorieList.SelectedListItemIndex + 1).Duration
                        End If

                        MyLog.Info("")
                        MyLog.Info("[CategoriesGuiWindow] [OnAction]: Keypress - Actiontype={0}: _SelectedCategorieItemId = {1}", action.wID.ToString, _SelectedCategorieItemId)
                        MyLog.Info("")

                        _LastFocusedIndex = _SelectedCategorieItemId
                        _LastFocusedControlID = _CategorieList.GetID

                        _ThreadPreviewListFill = New Threading.Thread(AddressOf FillPreviewList)
                        _ThreadPreviewListFill.IsBackground = True
                        _ThreadPreviewListFill.Start()
                        EndInit()
                    End If

                End If

                'Remote 5 Button (5) -> Reset Filter Settings of selcted categorie-> AllChannels
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.REMOTE_5 Then
                    MyLog.[Debug]("[CategoriesGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)

                    Try
                        If _ThreadPreviewListFill.IsAlive = True Then _ThreadPreviewListFill.Abort()
                    Catch ex As Exception
                        'Eventuell auftretende Exception abfangen
                        ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
                    End Try

                    Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowProgressbar)
                    _ProgressBarThread.Start()

                    Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(_CategorieList.SelectedListItem.ItemId)
                    _Categorie.groupName = CPGsettings.StandardTvGroup
                    _Categorie.Persist()

                    MyLog.Info("")
                    MyLog.Info("[CategoriesGuiWindow] [OnAction]: Keypress - Actiontype={0}: _SelectedCategorieItemId = {1}, TvGroup: {2}", action.wID.ToString, _SelectedCategorieItemId, _Categorie.groupName)
                    MyLog.Info("")

                    _ThreadPreviewListFill = New Threading.Thread(AddressOf FillPreviewList)
                    _ThreadPreviewListFill.IsBackground = True
                    _ThreadPreviewListFill.Start()

                End If

                'Remote 7 Button (7) -> Quick Filter 1
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.REMOTE_7 Then
                    MyLog.[Debug]("[CategoriesGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)

                    Try
                        If _ThreadPreviewListFill.IsAlive = True Then _ThreadPreviewListFill.Abort()
                    Catch ex As Exception
                        'Eventuell auftretende Exception abfangen
                        ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
                    End Try

                    Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowProgressbar)
                    _ProgressBarThread.Start()

                    Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(_CategorieList.SelectedListItem.ItemId)
                    _Categorie.groupName = CPGsettings.QuickTvGroup1
                    _Categorie.Persist()

                    MyLog.Info("")
                    MyLog.Info("[CategoriesGuiWindow] [OnAction]: Keypress - Actiontype={0}: _SelectedCategorieItemId = {1}, TvGroup: {2}", action.wID.ToString, _SelectedCategorieItemId, _Categorie.groupName)
                    MyLog.Info("")

                    _ThreadPreviewListFill = New Threading.Thread(AddressOf FillPreviewList)
                    _ThreadPreviewListFill.IsBackground = True
                    _ThreadPreviewListFill.Start()

                End If

                'Remote 9 Button (9) -> Quick Filter 2
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.REMOTE_9 Then
                    MyLog.[Debug]("[CategoriesGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)

                    Try
                        If _ThreadPreviewListFill.IsAlive = True Then _ThreadPreviewListFill.Abort()
                    Catch ex As Exception
                        'Eventuell auftretende Exception abfangen
                        ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
                    End Try

                    Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowProgressbar)
                    _ProgressBarThread.Start()

                    Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(_CategorieList.SelectedListItem.ItemId)
                    _Categorie.groupName = CPGsettings.QuickTvGroup2
                    _Categorie.Persist()

                    MyLog.Info("")
                    MyLog.Info("[CategoriesGuiWindow] [OnAction]: Keypress - Actiontype={0}: _SelectedCategorieItemId = {1}, TvGroup: {2}", action.wID.ToString, _SelectedCategorieItemId, _Categorie.groupName)
                    MyLog.Info("")

                    _ThreadPreviewListFill = New Threading.Thread(AddressOf FillPreviewList)
                    _ThreadPreviewListFill.IsBackground = True
                    _ThreadPreviewListFill.Start()

                End If

                'Play Button (P) -> Start channel
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MUSIC_PLAY Then
                    MyLog.[Debug]("[CategoriesGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)
                    Try
                        If _PreviewList.IsFocused = True Then StartTv(Program.Retrieve(_PreviewList.SelectedListItem.ItemId))
                    Catch ex As Exception
                        MyLog.Error("[Play Button]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
                    End Try
                End If

                'Record Button (R) -> MP TvProgramInfo aufrufen --Über keychar--
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_KEY_PRESSED Then
                    If action.m_key IsNot Nothing Then
                        If action.m_key.KeyChar = 114 Then
                            MyLog.[Debug]("[CategoriesGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)
                            If _PreviewList.IsFocused = True Then LoadTVProgramInfo(Program.Retrieve(_PreviewList.SelectedListItem.ItemId))
                        End If
                    End If
                End If

                'Menu Button (F9) -> Context Menu open
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_CONTEXT_MENU Then
                    MyLog.[Debug]("[CategoriesGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)

                    RememberLastFocusedItem()

                    If _PreviewList.IsFocused = True Then
                        Dim _program As Program = Program.Retrieve(_PreviewList.SelectedListItem.ItemId)
                        Helper.ShowActionMenu(_program)
                    End If
                    If _CategorieList.IsFocused = True Then ShowCategoriesContextMenu(_CategorieList.SelectedListItem.ItemId)
                End If

                'OSD Info Button (Y) -> Context Menu open (gleiche Fkt. wie Menu Button)
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_KEY_PRESSED Then
                    If action.m_key IsNot Nothing Then
                        If action.m_key.KeyChar = 121 Then

                            RememberLastFocusedItem()

                            MyLog.[Debug]("[CategoriesGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)
                            If _PreviewList.IsFocused = True Then
                                Dim _program As Program = Program.Retrieve(_PreviewList.SelectedListItem.ItemId)
                                Helper.ShowActionMenu(_program)
                            End If
                            If _CategorieList.IsFocused = True Then ShowCategoriesContextMenu(_CategorieList.SelectedListItem.ItemId)
                        End If
                    End If
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


            If control Is _CategorieList Or control Is _PreviewList Then
                If actionType = MediaPortal.GUI.Library.Action.ActionType.ACTION_SELECT_ITEM Then
                    Action_SelectItem()
                End If
            End If


        End Sub

        Protected Overrides Sub OnPreviousWindow()
            MyBase.OnPreviousWindow()
            _LastFocusedIndex = 0
            _LastFocusedControlID = _CategorieList.GetID
        End Sub

        Private Sub Action_SelectItem()

            RememberLastFocusedItem()

            If _CategorieList.IsFocused = True Then

                'ItemsGuiWindow._LastFocusedItemsIndex = 0
                'ItemsGuiWindow._LastFocusedItemsControlID = 10

                Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(_CategorieList.SelectedListItem.ItemId)

                Select Case _ClickfinderCategorieView
                    Case Is = CategorieView.Now
                        ItemsGuiWindow.SetGuiProperties(_Categorie.SqlString, _
                                                        PeriodeStartTime.AddMinutes(CDbl((-1) * _Categorie.NowOffset)), _
                                                        PeriodeEndTime, _
                                                        _Categorie.sortedBy, _
                                                        CPGsettings.StandardTvGroup, _
                                                        _Categorie.MinRunTime, _
                                                        CPGsettings.ItemsShowLocalMovies, _
                                                        CPGsettings.ItemsShowLocalSeries, _
                                                        _Categorie.Name, _
                                                        _Categorie.idClickfinderCategories, _ClickfinderCategorieView)


                    Case Is = CategorieView.Preview
                        ItemsGuiWindow.SetGuiProperties(Replace(_Categorie.SqlString, "WHERE", "WHERE TVMovieBewertung >= " & CPGsettings.PreviewMinTvMovieRating & " AND "), _
                                                        PeriodeStartTime, _
                                                        PeriodeEndTime, _
                                                        _Categorie.sortedBy, _
                                                        CPGsettings.StandardTvGroup, _
                                                        _Categorie.MinRunTime, _
                                                        CPGsettings.ItemsShowLocalMovies, _
                                                        CPGsettings.ItemsShowLocalSeries, _
                                                        _Categorie.Name, _
                                                        _Categorie.idClickfinderCategories, _ClickfinderCategorieView)

                    Case Else
                        ItemsGuiWindow.SetGuiProperties(_Categorie.SqlString, _
                                                        PeriodeStartTime, _
                                                        PeriodeEndTime, _
                                                        _Categorie.sortedBy, _
                                                        CPGsettings.StandardTvGroup, _
                                                        _Categorie.MinRunTime, _
                                                        CPGsettings.ItemsShowLocalMovies, _
                                                        CPGsettings.ItemsShowLocalSeries, _
                                                        _Categorie.Name, _
                                                        _Categorie.idClickfinderCategories, _ClickfinderCategorieView)
                End Select


                If _ClickfinderCategorieView = CategorieView.Day Then
                    Translator.SetProperty("#ItemsLeftListLabel", Translation.All & " " & _Categorie.Name & " " & Translation.von & " " & getTranslatedDayOfWeek(_Day) & " " & Format(_Day.AddMinutes(-1), "dd.MM.yyyy"))
                ElseIf _ClickfinderCategorieView = CategorieView.Preview Then
                    Translator.SetProperty("#ItemsLeftListLabel", _Categorie.Name & " " & Translation.Preview & " " & Translation.for & " " & DateDiff(DateInterval.Day, PeriodeStartTime, PeriodeEndTime) & " " & Translation.Day)
                Else
                    Translator.SetProperty("#ItemsLeftListLabel", _Categorie.Name & " " & Translation.von & " " & Format(PeriodeStartTime.Hour, "00") & ":" & Format(PeriodeStartTime.Minute, "00") & " - " & Format(PeriodeEndTime.Hour, "00") & ":" & Format(PeriodeEndTime.Minute, "00"))
                End If

                GUIWindowManager.ActivateWindow(1656544653)
            End If

            If _PreviewList.IsFocused = True Then ListControlClick(_PreviewList.SelectedListItem.ItemId)

        End Sub

        Private Sub RememberLastFocusedItem()
            If _CategorieList.IsFocused Then
                'MsgBox(_MovieList.SelectedListItemIndex)
                _LastFocusedIndex = _CategorieList.SelectedListItemIndex
                _LastFocusedControlID = _CategorieList.GetID
                _SelectedCategorieItemId = _CategorieList.SelectedListItemIndex
            ElseIf _PreviewList.IsFocused Then
                _LastFocusedIndex = _PreviewList.SelectedListItemIndex
                _LastFocusedControlID = _PreviewList.GetID
                _SelectedCategorieItemId = _CategorieList.SelectedListItemIndex
            Else
                _LastFocusedIndex = 0
                _LastFocusedControlID = _CategorieList.GetID
            End If
        End Sub

#End Region

#Region "Functions"

        Private Sub FillCategories()
            Try

                MyLog.Info("[CategoriesGuiWindow] [FillCategories]: Thread started...")

                Dim sb As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(ClickfinderCategories))
                sb.AddOrderByField(True, "sortOrder")
                Dim stmt As SqlStatement = sb.GetStatement(True)
                _CategoriesList = ObjectFactory.GetCollection(GetType(ClickfinderCategories), stmt.Execute())

                For Each _Categorie In _CategoriesList
                    Try
                        If _Categorie.isVisible = True Then
                            AddListControlItem(_CategorieList, _Categorie.SortOrder, String.Empty, _Categorie.Name, , , Config.GetFile(Config.Dir.Thumbs, "Clickfinder ProgramGuide\Categories\") & _Categorie.Name & ".png", _Categorie.MinRunTime)
                        End If
                    Catch ex As ThreadAbortException ' Ignore this exception
                        MyLog.Info("[CategoriesGuiWindow] [FillCategories]: --- THREAD ABORTED ---")
                    Catch ex As GentleException
                    Catch ex As Exception
                        MyLog.Error("[CategoriesGuiWindow] [FillCategories]: exception err: {0}, stack: {1}", ex.Message, ex.StackTrace)
                    End Try
                Next

                If _ThreadPreviewListFill.IsAlive = False Then
                    GUIListControl.SelectItemControl(GetID, _LastFocusedControlID, _LastFocusedIndex)
                    GUIListControl.FocusControl(GetID, _LastFocusedControlID)
                End If

                MyLog.Info("[CategoriesGuiWindow] [FillCategories]: {0} Categories loaded", _CategoriesList.Count)
                MyLog.Info("[CategoriesGuiWindow] [FillCategories]: Thread finished")

            Catch ex1 As ThreadAbortException ' Ignore this exception
                MyLog.Info("[CategoriesGuiWindow] [FillCategories]: --- THREAD ABORTED ---")
            Catch ex1 As GentleException
            Catch ex1 As Exception
                MyLog.Error("[CategoriesGuiWindow] [FillCategories]: exception err: {0}, stack: {1}", ex1.Message, ex1.StackTrace)
            End Try
        End Sub

        Private Sub FillPreviewList()
            Dim _ItemCounter As Integer = 1
            Dim _startTime As Date = Nothing
            Dim _endTime As Date = Nothing
            Dim _timer As Date = Date.Now
            Dim _TotalTimer As Date = Date.Now

            Try
                _PreviewList.Visible = False
                _PreviewList.AllocResources()
                _PreviewList.Clear()

                Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowProgressbar)
                _ProgressBarThread.Start()

                MyLog.Info("[CategoriesGuiWindow] [FillPreviewList]: Thread started...")

                For i = 1 To 6
                    Translator.SetProperty("#PreviewListImage" & i, "")
                    Translator.SetProperty("#PreviewListTvMovieStar" & i, "")
                    Translator.SetProperty("#PreviewListRatingStar" & i, 0)
                Next

                Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(_SelectedCategorieItemId)

                Translator.SetProperty("#CategorieGroup", _Categorie.groupName)

                'SqlString StartZeit anpassen, je nachdem von wo aus GuiCategories aufgerufen wird
                If _ClickfinderCategorieView = CategorieView.Day And _Day.Date = Date.Today.Date Then
                    'Alle Kategorien (GuiHighlightsMenu): Heute -> PreviewList: Jetzt anzeigen
                    _startTime = Date.Now
                    _endTime = Date.Now.AddHours(4)

                ElseIf _ClickfinderCategorieView = CategorieView.Day And Not _Day.Date = Date.Today.Date Then
                    'Alle Kategorien (GuiHighlightsMenu): außer Heute -> PreviewList: PrimeTime anzeigen, sortiert nach StarRating
                    _startTime = PeriodeStartTime.Date.AddHours(20).AddMinutes(15)
                    _endTime = _startTime.AddHours(4)
                    
                Else
                    'Jetzt,PrimeTime,LateTime über Button / Remote keys
                    _startTime = PeriodeStartTime
                    _endTime = PeriodeEndTime
                End If


                'SQL String bauen
                Dim _SqlStringBuilder As New StringBuilder(_Categorie.SqlString)
                _SqlStringBuilder.Replace("#StartTime", MySqlDate(_startTime))
                _SqlStringBuilder.Replace("#EndTime", MySqlDate(_endTime))
                _SqlStringBuilder.Replace("#CPGFilter", ItemsGuiWindow.GetSqlCPGFilterString(_Categorie.groupName, CPGsettings.CategorieShowLocalMovies, CPGsettings.CategorieShowLocalSeries, _Categorie.MinRunTime))
                _SqlStringBuilder.Replace("#CPGgroupBy", "GROUP BY program.title, program.episodeName")
                _SqlStringBuilder.Replace("#CPGorderBy", Helper.ORDERBYstartTime)
                _SqlStringBuilder.Replace(" * ", " TVMovieProgram.idProgram, TVMovieProgram.Action, TVMovieProgram.Actors, TVMovieProgram.BildDateiname, TVMovieProgram.Country, TVMovieProgram.Cover, TVMovieProgram.Describtion, TVMovieProgram.Dolby, TVMovieProgram.EpisodeImage, TVMovieProgram.Erotic, TVMovieProgram.FanArt, TVMovieProgram.Feelings, TVMovieProgram.FileName, TVMovieProgram.Fun, TVMovieProgram.HDTV, TVMovieProgram.idEpisode, TVMovieProgram.idMovingPictures, TVMovieProgram.idSeries, TVMovieProgram.idVideo, TVMovieProgram.KurzKritik, TVMovieProgram.local, TVMovieProgram.Regie, TVMovieProgram.Requirement, TVMovieProgram.SeriesPosterImage, TVMovieProgram.ShortDescribtion, TVMovieProgram.Tension, TVMovieProgram.TVMovieBewertung ")


                'List: Daten laden
                '_SqlString = Replace(_SqlString, " * ", " TVMovieProgram.idProgram, TVMovieProgram.Action, TVMovieProgram.Actors, TVMovieProgram.BildDateiname, TVMovieProgram.Country, TVMovieProgram.Cover, TVMovieProgram.Describtion, TVMovieProgram.Dolby, TVMovieProgram.EpisodeImage, TVMovieProgram.Erotic, TVMovieProgram.FanArt, TVMovieProgram.Feelings, TVMovieProgram.FileName, TVMovieProgram.Fun, TVMovieProgram.HDTV, TVMovieProgram.idEpisode, TVMovieProgram.idMovingPictures, TVMovieProgram.idSeries, TVMovieProgram.idVideo, TVMovieProgram.KurzKritik, TVMovieProgram.local, TVMovieProgram.Regie, TVMovieProgram.Requirement, TVMovieProgram.SeriesPosterImage, TVMovieProgram.ShortDescribtion, TVMovieProgram.Tension, TVMovieProgram.TVMovieBewertung ")
                Dim _SQLstate As SqlStatement = Broker.GetStatement(AppendSqlLimit(_SqlStringBuilder.ToString, 6))
                Dim _ResultList As List(Of TVMovieProgram) = ObjectFactory.GetCollection(GetType(TVMovieProgram), _SQLstate.Execute())

                MyLog.Info("[CategoriesGuiWindow] [FillPreviewList]: {0} Previews loaded from Database (max = 20, {1}s)", _ResultList.Count, (DateTime.Now - _timer).TotalSeconds)

                If _ResultList.Count > 0 Then

                    For Each _TvMovieProgram In _ResultList
                        Translator.SetProperty("#PreviewListRatingStar" & _ItemCounter, GuiLayout.ratingStar(_TvMovieProgram.ReferencedProgram))
                        Translator.SetProperty("#PreviewListTvMovieStar" & _ItemCounter, GuiLayout.TvMovieStar(_TvMovieProgram))
                        Translator.SetProperty("#PreviewListImage" & _ItemCounter, GuiLayout.Image(_TvMovieProgram))

                        AddListControlItem(_PreviewList, _TvMovieProgram.ReferencedProgram.IdProgram, _TvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName, _TvMovieProgram.ReferencedProgram.Title, GuiLayout.TimeLabel(_TvMovieProgram), GuiLayout.InfoLabel(_TvMovieProgram), , , , GuiLayout.RecordingStatus(_TvMovieProgram.ReferencedProgram))

                        _ItemCounter = _ItemCounter + 1
                        If _ItemCounter > 6 Then Exit For
                    Next

                    ctlProgressBar.Visible = False
                    _PreviewList.Visible = True

                    GUIListControl.SelectItemControl(GetID, _LastFocusedControlID, _LastFocusedIndex)
                    GUIListControl.FocusControl(GetID, _LastFocusedControlID)
                Else
                    ctlProgressBar.Visible = False
                    _PreviewList.Visible = False
                    MyLog.Warn("[CategoriesGuiWindow] [FillPreviewList]: nothing found (_ResultList.Count = 0)")
                End If

                MyLog.Info("[CategoriesGuiWindow] [FillPreviewList]: Thread finished in {0}s", (DateTime.Now - _TotalTimer).TotalSeconds)

                'log Ausgabe abfangen, falls der Thread abgebrochen wird
            Catch ex3 As ThreadAbortException ' Ignore this exception
                MyLog.Info("[CategoriesGuiWindow] [FillPreviewList]: --- THREAD ABORTED ---")
            Catch ex3 As GentleException
            Catch ex3 As Exception
                MyLog.Error("[CategoriesGuiWindow] [FillPreviewList]: exception err:" & ex3.Message & " stack:" & ex3.StackTrace)
            End Try
        End Sub

        Private Sub ShowProgressbar()
            ctlProgressBar.Visible = True
        End Sub

#End Region

#Region "MediaPortal Funktionen / Dialogs"
        Private Sub ShowCategoriesContextMenu(ByVal idCategorie As Integer)
            Try
                Dim dlgContext As GUIDialogMenu = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_MENU, Integer)), GUIDialogMenu)
                dlgContext.Reset()

                Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(idCategorie)
                dlgContext.SetHeading(Translation.Categorie & ": " & _Categorie.Name & " " & Translation.CategorieEdit)
                dlgContext.ShowQuickNumbers = True

                MyLog.Debug("[CategoriesGuiWindow] [ShowCategoriesContextMenu]: idCategorie = {0}, title = {1}", _Categorie.SortOrder, _Categorie.Name)

                'Filter: TvGroup
                Dim lItemGroup As New GUIListItem
                lItemGroup.Label = Translation.Filterby
                dlgContext.Add(lItemGroup)
                lItemGroup.Dispose()

                'Categorie umbennenen
                Dim lItemRename As New GUIListItem
                lItemRename.Label = Translation.CategorieRename
                dlgContext.Add(lItemRename)
                lItemRename.Dispose()

                'Categorie verstecken
                Dim lItemHide As New GUIListItem
                lItemHide.Label = Translation.CategorieHide
                dlgContext.Add(lItemHide)
                lItemHide.Dispose()

                'versteckte Categorie anzeigen
                Dim lItemShow As New GUIListItem
                lItemShow.Label = Translation.CategorieShow
                dlgContext.Add(lItemShow)
                lItemShow.Dispose()

                dlgContext.DoModal(GetID)


                Select Case dlgContext.SelectedLabelText
                    Case Is = Translation.CategorieRename
                        MyLog.Debug("[CategoriesGuiWindow] [ShowCategoriesContextMenu]: selected -> rename categorie: {0}", _Categorie.Name)
                        MyLog.Debug("")
                        'Categorie umbennen

                    Case Is = Translation.CategorieHide
                        'Categorie verstecken
                        MyLog.Debug("[CategoriesGuiWindow] [ShowCategoriesContextMenu]: selected -> hide categorie: {0}", _Categorie.Name)
                        MyLog.Debug("")
                        _Categorie.isVisible = False
                        _Categorie.Persist()

                        GUIWindowManager.ActivateWindow(1656544654, "CPG." & _ClickfinderCategorieView.ToString)

                    Case Is = Translation.CategorieShow
                        'versteckte Categorie anzeigen
                        MyLog.Debug("[CategoriesGuiWindow] [ShowCategoriesContextMenu]: selected -> hidden Categories menu")
                        showNotVisibleCategories()

                    Case Is = Translation.Filterby
                        MyLog.Debug("[CategoriesGuiWindow] [ShowCategoriesContextMenu]: selected -> filter menu")
                        ShowFilterMenu(_Categorie)
                    Case Else
                        MyLog.Debug("[CategoriesGuiWindow] [ShowCategoriesContextMenu]: exit")
                        MyLog.Debug("")
                End Select


                dlgContext.Dispose()
                dlgContext.AllocResources()

            Catch ex As Exception
                MyLog.Error("[CategoriesGuiWindow] [ShowCategoriesContextMenu]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try
        End Sub

        Private Sub showNotVisibleCategories()
            Try
                Dim dlgContext As GUIDialogSelect2Custom = CType(GUIWindowManager.GetWindow(CType(1656544655, Integer)), GUIDialogSelect2Custom)
                dlgContext.Reset()

                dlgContext.SetHeading(Translation.CategorieChoose)
                'dlgContext.ShowQuickNumbers = True

                Dim _idCategorieContainer As Dictionary(Of Integer, Integer) = New Dictionary(Of Integer, Integer)

                'Alle versteckten Categorien laden, sortiert nach SortOrder
                Dim sb As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(ClickfinderCategories))
                sb.AddConstraint([Operator].Equals, "isVisible", 0)
                sb.AddOrderByField(True, "sortOrder")
                Dim stmt As SqlStatement = sb.GetStatement(True)
                Dim _Result As IList(Of ClickfinderCategories) = ObjectFactory.GetCollection(GetType(ClickfinderCategories), stmt.Execute())

                If _Result.Count > 0 Then

                    For i = 0 To _Result.Count - 1

                        Dim lItem As New GUIListItem
                        lItem.Label = _Result(i).Name
                        lItem.Label3 = _Result(i).Beschreibung
                        lItem.IconImage = Config.GetFile(Config.Dir.Thumbs, "Clickfinder ProgramGuide\Categories\") & _CategoriesList(i).Name & ".png"
                        dlgContext.Add(lItem)
                        _idCategorieContainer.Add(i, _Result(i).SortOrder)
                        lItem.Dispose()
                    Next

                    dlgContext.DoModal(GetID)

                    Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(_idCategorieContainer(dlgContext.SelectedLabel))
                    MyLog.Debug("[CategoriesGuiWindow] [showNotVisibleCategories]: selected -> set Categorie = {0} visible", _Categorie.Name)
                    MyLog.Debug("")
                    _Categorie.isVisible = True
                    _Categorie.Persist()
                    GUIWindowManager.ActivateWindow(1656544654, "CPG." & _ClickfinderCategorieView.ToString)

                Else
                    'dlgContext.ShowQuickNumbers = False
                    MyLog.Debug("[CategoriesGuiWindow] [showNotVisibleCategories]: no hidden categories found -> exit")
                    MyLog.Debug("")
                    Dim lItem As New GUIListItem
                    lItem.Label = Translation.CategorieHideNotFound
                    dlgContext.Add(lItem)
                    lItem.Dispose()
                    dlgContext.DoModal(GetID)

                End If
                _idCategorieContainer.Clear()

                dlgContext.Dispose()
                dlgContext.AllocResources()

            Catch ex As Exception
                MyLog.Error("[CategoriesGuiWindow] [showNotVisibleCategories]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try
        End Sub

        Private Sub ShowFilterMenu(ByVal Categorie As ClickfinderCategories)
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
                MyLog.Debug("[CategoriesGuiWindow] [ShowFilterMenu]: selected -> fliter by groug: {0}", dlgContext.SelectedLabelText)
                MyLog.Debug("")
                Categorie.groupName = dlgContext.SelectedLabelText
                Categorie.Persist()

                'Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowProgressbar)
                '_ProgressBarThread.Start()

                _ThreadPreviewListFill = New Threading.Thread(AddressOf FillPreviewList)
                _ThreadPreviewListFill.IsBackground = True
                _ThreadPreviewListFill.Start()

            Else
                MyLog.Debug("[CategoriesGuiWindow] [showNotVisibleCategories]: selected -> exit")
            End If

            dlgContext.Dispose()
            dlgContext.AllocResources()

        End Sub

#End Region

    End Class
End Namespace
