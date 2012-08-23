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

Namespace ClickfinderProgramGuide
    Public Class CategoriesGuiWindow
        Inherits GUIWindow

#Region "Skin Controls"

        'Buttons
        <SkinControlAttribute(2)> Protected _btnNow As GUIButtonControl = Nothing
        <SkinControlAttribute(3)> Protected _btnPrimeTime As GUIButtonControl = Nothing
        <SkinControlAttribute(4)> Protected _btnLateTime As GUIButtonControl = Nothing
        <SkinControlAttribute(5)> Protected _btnAllMovies As GUIButtonControl = Nothing
        <SkinControlAttribute(6)> Protected _btnHighlights As GUIButtonControl = Nothing
        <SkinControlAttribute(7)> Protected _btnPreview As GUIButtonControl = Nothing

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
            Highlights = 4
            Day = 5
        End Enum

        Friend Shared _ClickfinderCategorieView As CategorieView
        Private Shared _Day As Date
        Private _Categories As IList(Of ClickfinderCategories)
        Private _SelectedCategorieMinRunTime As Integer
        Private Shared _SelectedCategorieItemId As Integer
        Private ThreadPreviewListFill As Threading.Thread
        Private Shared _layer As New TvBusinessLayer
        Private _CategorieFilterByGroup As String
        Private _LastFocusedIndex As Integer
        Private _LastFocusedControlID As Integer

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub

        Public Shared Sub SetGuiProperties(ByVal View As CategorieView, Optional ByVal Day As Date = Nothing)
            _ClickfinderCategorieView = View
            _Day = Day

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
                Dim _PrimeTime As Date = CDate(_layer.GetSetting("ClickfinderPrimeTime", "20:15").Value)
                Dim _LateTime As Date = CDate(_layer.GetSetting("ClickfinderLateTime", "22:00").Value)

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
                        Return PeriodeStartTime.AddDays(CDbl(_layer.GetSetting("ClickfinderPreviewMaxDays", "7").Value))
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

            MyLog.Info("")
            MyLog.Info("")
            MyLog.Info("[CategoriesGuiWindow] -------------[OPEN]-------------")

            Helper.CheckConnectionState()

            MyLog.Debug("[CategoriesGuiWindow] [OnPageLoad]: PeriodeStartTime = {0}, PeriodeEndTime = {1}", _
                        getTranslatedDayOfWeek(PeriodeStartTime.Date) & " " & Format(PeriodeStartTime, "dd.MM.yyyy") & " " & Format(PeriodeStartTime.Hour, "00") & ":" & Format(PeriodeStartTime.Minute, "00"), Format(PeriodeEndTime.Hour, "00") & ":" & Format(PeriodeEndTime.Minute, "00"))

            For i = 1 To 6
                Translator.SetProperty("#PreviewListImage" & i, "")
                Translator.SetProperty("#PreviewListTvMovieStar" & i, "")
                Translator.SetProperty("#PreviewListRatingStar" & i, 0)
            Next

            If _layer.GetSetting("TvMovieImportIsRunning", "false").Value = "true" Then
                Translator.SetProperty("#SettingLastUpdate", Translation.ImportIsRunning)
                MyLog.Debug("[CategoriesGuiWindow] [OnPageLoad]: _ClickfinderCurrentDate = {0}", "TvMovie++ Import is running !")
            Else
                Translator.SetProperty("#SettingLastUpdate", GuiLayout.LastUpdateLabel)
            End If

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
            Dim _Thread2 As New Thread(AddressOf FillPreviewList)

            _Thread1.Start()
            _Thread2.Start()


        End Sub

        Protected Overrides Sub OnPageDestroy(ByVal new_windowId As Integer)

            For i = 1 To 6
                Translator.SetProperty("#PreviewListImage" & i, "")
                Translator.SetProperty("#PreviewListTvMovieStar" & i, "")
                Translator.SetProperty("#PreviewListRatingStar" & i, 0)
            Next

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

            Try
                If ThreadPreviewListFill.IsAlive = True Then ThreadPreviewListFill.Abort()
            Catch ex As Exception
                'Eventuell auftretende Exception abfangen
                ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
            End Try

            MyBase.OnPageDestroy(new_windowId)
        End Sub

        Public Overrides Sub OnAction(ByVal action As MediaPortal.GUI.Library.Action)
            If GUIWindowManager.ActiveWindow = GetID Then
                'Try
                '    MyLog.[Debug]("[OnAction] Keypress KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)
                'Catch
                '    MyLog.[Debug]("[OnAction] Keypress KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)
                'End Try

                'Select Item (Enter) -> MP TvProgramInfo aufrufen --Über keychar--
                'If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_SELECT_ITEM Then
                '    MyLog.[Debug]("[CategoriesGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)

                '    Action_SelectItem()

                'End If

                If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MOVE_UP Then
                    If _CategorieList.IsFocused = True Then

                        MyLog.[Debug]("[CategoriesGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)
                        Try
                            If ThreadPreviewListFill.IsAlive = True Then ThreadPreviewListFill.Abort()
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

                        _LastFocusedIndex = _SelectedCategorieItemId
                        _LastFocusedControlID = _CategorieList.GetID

                        ThreadPreviewListFill = New Threading.Thread(AddressOf FillPreviewList)
                        ThreadPreviewListFill.IsBackground = True
                        ThreadPreviewListFill.Start()
                    End If
                End If

                If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MOVE_DOWN Then
                    If _CategorieList.IsFocused = True Then

                        MyLog.[Debug]("[CategoriesGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)
                        Try
                            If ThreadPreviewListFill.IsAlive = True Then ThreadPreviewListFill.Abort()
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

                        _LastFocusedIndex = _SelectedCategorieItemId
                        _LastFocusedControlID = _CategorieList.GetID

                        ThreadPreviewListFill = New Threading.Thread(AddressOf FillPreviewList)
                        ThreadPreviewListFill.IsBackground = True
                        ThreadPreviewListFill.Start()
                        EndInit()
                    End If

                End If

                'Remote 5 Button (5) -> Reset Filter Settings of selcted categorie-> AllChannels
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.REMOTE_5 Then
                    MyLog.[Debug]("[CategoriesGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)

                    Try
                        If ThreadPreviewListFill.IsAlive = True Then ThreadPreviewListFill.Abort()
                    Catch ex As Exception
                        'Eventuell auftretende Exception abfangen
                        ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
                    End Try

                    Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(_CategorieList.SelectedListItem.ItemId)
                    _Categorie.groupName = _layer.GetSetting("ClickfinderStandardTvGroup", "All Channels").Value
                    _Categorie.Persist()

                    ThreadPreviewListFill = New Threading.Thread(AddressOf FillPreviewList)
                    ThreadPreviewListFill.IsBackground = True
                    ThreadPreviewListFill.Start()

                End If

                'Remote 7 Button (7) -> Quick Filter 1
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.REMOTE_7 Then
                    MyLog.[Debug]("[CategoriesGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)

                    Try
                        If ThreadPreviewListFill.IsAlive = True Then ThreadPreviewListFill.Abort()
                    Catch ex As Exception
                        'Eventuell auftretende Exception abfangen
                        ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
                    End Try

                    Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(_CategorieList.SelectedListItem.ItemId)
                    _Categorie.groupName = _layer.GetSetting("ClickfinderQuickTvGroup1", "All Channels").Value
                    _Categorie.Persist()

                    ThreadPreviewListFill = New Threading.Thread(AddressOf FillPreviewList)
                    ThreadPreviewListFill.IsBackground = True
                    ThreadPreviewListFill.Start()

                End If

                'Remote 9 Button (9) -> Quick Filter 2
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.REMOTE_9 Then
                    MyLog.[Debug]("[CategoriesGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)

                    Try
                        If ThreadPreviewListFill.IsAlive = True Then ThreadPreviewListFill.Abort()
                    Catch ex As Exception
                        'Eventuell auftretende Exception abfangen
                        ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
                    End Try

                    Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(_CategorieList.SelectedListItem.ItemId)
                    _Categorie.groupName = _layer.GetSetting("ClickfinderQuickTvGroup2", "All Channels").Value
                    _Categorie.Persist()

                    ThreadPreviewListFill = New Threading.Thread(AddressOf FillPreviewList)
                    ThreadPreviewListFill.IsBackground = True
                    ThreadPreviewListFill.Start()

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
                            MyLog.[Debug]("[CategoriesGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)
                            If _PreviewList.IsFocused = True Then ShowContextMenu(_PreviewList.SelectedListItem.ItemId)
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

        'Actions die für Listcontrolclick (mouse) & Action events benötigt werder

        'categorie aufrufen (GuiItems)
        Private Sub Action_SelectItem()

            If _CategorieList.IsFocused = True Then

                'ItemsGuiWindow._LastFocusedItemsIndex = 0
                'ItemsGuiWindow._LastFocusedItemsControlID = 10

                Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(_CategorieList.SelectedListItem.ItemId)

                If _ClickfinderCategorieView = CategorieView.Now Then
                    ItemsGuiWindow.SetGuiProperties(CStr(Replace(Replace(_Categorie.SqlString, "#startTime", MySqlDate(PeriodeStartTime.AddMinutes(CDbl((-1) * _Categorie.NowOffset)))), "#endTime", MySqlDate(PeriodeEndTime))), _Categorie.MinRunTime, _Categorie.sortedBy, _Categorie.idClickfinderCategories)
                ElseIf _ClickfinderCategorieView = CategorieView.Preview Then
                    ItemsGuiWindow.SetGuiProperties(CStr(Replace(Replace(Replace(_Categorie.SqlString, "#startTime", MySqlDate(PeriodeStartTime)), "#endTime", MySqlDate(PeriodeEndTime)), "WHERE", "WHERE TVMovieBewertung >= " & CInt(_layer.GetSetting("ClickfinderPreviewMinTvMovieRating", "1").Value) & " AND ")), _Categorie.MinRunTime, _Categorie.sortedBy, _Categorie.idClickfinderCategories)
                Else
                    ItemsGuiWindow.SetGuiProperties(CStr(Replace(Replace(_Categorie.SqlString, "#startTime", MySqlDate(PeriodeStartTime)), "#endTime", MySqlDate(PeriodeEndTime))), _Categorie.MinRunTime, _Categorie.sortedBy, _Categorie.idClickfinderCategories)
                End If

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


#End Region

#Region "Functions"

        Private Sub FillCategories()
            Try

                Dim _logCategories As String = String.Empty
                Dim _logHiddenCategories As String = String.Empty

                MyLog.Debug("")
                MyLog.Debug("[CategoriesGuiWindow] [FillCategories]: Thread started")

                Dim sb As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(ClickfinderCategories))
                sb.AddOrderByField(True, "sortOrder")
                Dim stmt As SqlStatement = sb.GetStatement(True)
                _Categories = ObjectFactory.GetCollection(GetType(ClickfinderCategories), stmt.Execute())

                For i = 0 To _Categories.Count - 1
                    If _Categories(i).isVisible = True Then
                        AddListControlItem(_CategorieList, _Categories(i).SortOrder, String.Empty, _Categories(i).Name, , , Config.GetFile(Config.Dir.Thumbs, "Clickfinder ProgramGuide\Categories\") & _Categories(i).Name & ".png", _Categories(i).MinRunTime)
                        _logCategories = _logCategories & _Categories(i).Name & ", "
                    Else
                        _logHiddenCategories = _logHiddenCategories & _Categories(i).Name & ", "
                    End If
                Next

                If ThreadPreviewListFill.IsAlive = False Then
                    GUIListControl.SelectItemControl(GetID, _LastFocusedControlID, _LastFocusedIndex)
                    GUIListControl.FocusControl(GetID, _LastFocusedControlID)
                End If

                MyLog.Debug("[CategoriesGuiWindow] [FillCategories]: categories ({0})", _logCategories)
                MyLog.Debug("[CategoriesGuiWindow] [FillCategories]: hidden categories ({0})", _logHiddenCategories)
                MyLog.Debug("[CategoriesGuiWindow] [FillCategories]: Thread finished")
                MyLog.Debug("")

            Catch ex As Exception
                MyLog.Error("[CategoriesGuiWindow] [FillCategories]: Loop exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try

        End Sub

        Private Sub FillPreviewList()
            Dim _SqlString As String = String.Empty
            Dim _lastTitle As String = String.Empty
            Dim _ItemCounter As Integer = 1
            Dim _timeLabel As String = String.Empty
            Dim _infoLabel As String = String.Empty
            Dim _imagepath As String = String.Empty
            Dim _startTime As Date = Nothing
            Dim _endTime As Date = Nothing
            Dim _LogLocalMovies As String = Nothing
            Dim _LogLocalSeries As String = Nothing

            Try
                _PreviewList.Visible = False
                _PreviewList.Clear()

                Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowProgressbar)
                _ProgressBarThread.Start()

                MyLog.Debug("")
                MyLog.Debug("[CategoriesGuiWindow] [FillPreviewList]: Thread started")

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
                    _SqlString = CStr(Replace(Replace(_Categorie.SqlString, "#startTime", MySqlDate(Date.Now)), "#endTime", MySqlDate(Date.Now.AddHours(2))))

                ElseIf _ClickfinderCategorieView = CategorieView.Day And Not _Day.Date = Date.Today.Date Then

                    'Alle Kategorien (GuiHighlightsMenu): außer Heute -> PreviewList: PrimeTime anzeigen, sortiert nach StarRating
                    _startTime = PeriodeStartTime.Date.AddHours(20).AddMinutes(15)
                    _SqlString = CStr(Replace(Replace(_Categorie.SqlString, "#startTime", MySqlDate(PeriodeStartTime.Date.AddHours(20).AddMinutes(15))), "#endTime", MySqlDate(PeriodeStartTime.Date.AddHours(22).AddMinutes(30))))
                    _SqlString = Left(_SqlString, InStr(_SqlString, "ORDER BY") - 1) & _
                        Helper.ORDERBYstarRating
                Else
                    'Jetzt,PrimeTime,LateTime über Button / Remote keys
                    _SqlString = CStr(Replace(Replace(_Categorie.SqlString, "#startTime", MySqlDate(PeriodeStartTime)), "#endTime", MySqlDate(PeriodeStartTime.AddHours(2))))
                End If


                _SqlString = AppendSqlLimit(_SqlString, 25)


                Dim _result As New ArrayList
                _result.AddRange(Broker.Execute(_SqlString).TransposeToFieldList("idProgram", False))

                MyLog.Debug("[CategoriesGuiWindow] [FillPreviewList]: {0} program found, Categorie = {1}, group = {2}, SQLString: {3}", _result.Count, _Categorie.Name, _Categorie.groupName, _SqlString)

                For i = 0 To _result.Count - 1
                    Try
                        Dim SaveChanges As Boolean = False

                        'ProgramDaten über idProgram laden
                        Dim _Program As Program = Program.Retrieve(_result.Item(i))

                        If Not _Categorie.groupName = _layer.GetSetting("ClickfinderStandardTvGroup", "All Channels").Value Then
                            'Alle Gruppen des _resultprogram laden
                            Dim sb As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(GroupMap))
                            sb.AddConstraint([Operator].Equals, "idgroup", _Categorie.Referencedgroup.IdGroup)
                            sb.AddConstraint([Operator].Equals, "idChannel", _Program.IdChannel)
                            Dim stmt As SqlStatement = sb.GetStatement(True)
                            Dim _isInFavGroup As IList(Of GroupMap) = ObjectFactory.GetCollection(GetType(GroupMap), stmt.Execute())

                            If _isInFavGroup.Count = 0 Then
                                Continue For
                            End If
                        End If

                        Dim _TvMovieProgram As TVMovieProgram = getTvMovieProgram(_Program)

                        If _TvMovieProgram.idSeries > 0 Then
                            Helper.CheckSeriesLocalStatus(_TvMovieProgram)
                        End If

                        'Falls lokale Movies/Videos nicht angezeigt werden sollen -> aus Array entfernen
                        If CBool(_layer.GetSetting("ClickfinderCategorieShowLocalMovies", "false").Value) = True Then
                            If _TvMovieProgram.local = True And _TvMovieProgram.idSeries = 0 Then
                                _LogLocalMovies = _LogLocalMovies & _TvMovieProgram.ReferencedProgram.Title & ", "
                                Continue For
                            End If
                        End If

                        'Falls lokale Serien nicht angezeigt werden sollen -> aus Array entfernen
                        If CBool(_layer.GetSetting("ClickfinderCategorieShowLocalSeries", "false").Value) = True Then
                            If _TvMovieProgram.local = True And _TvMovieProgram.idSeries > 0 Then
                                _LogLocalSeries = _LogLocalSeries & _TvMovieProgram.ReferencedProgram.Title & " - S" & _TvMovieProgram.ReferencedProgram.SeriesNum & "E" & _TvMovieProgram.ReferencedProgram.EpisodeNum & ", "
                                Continue For
                            End If
                        End If

                        If DateDiff(DateInterval.Minute, _Program.StartTime, _Program.EndTime) > _SelectedCategorieMinRunTime Then

                            If String.Equals(_lastTitle, _Program.Title & _Program.EpisodeName) = False And _Program.ReferencedChannel.IsTv = True Then


                                Translator.SetProperty("#PreviewListRatingStar" & _ItemCounter, GuiLayout.ratingStar(_Program))
                                Translator.SetProperty("#PreviewListTvMovieStar" & _ItemCounter, GuiLayout.TvMovieStar(_TvMovieProgram))
                                Translator.SetProperty("#PreviewListImage" & _ItemCounter, GuiLayout.Image(_TvMovieProgram))

                                AddListControlItem(_PreviewList, _Program.IdProgram, _Program.ReferencedChannel.DisplayName, _Program.Title, GuiLayout.TimeLabel(_TvMovieProgram), GuiLayout.InfoLabel(_TvMovieProgram), , , GuiLayout.RecordingStatus(_Program))

                                MyLog.Debug("[CategoriesGuiWindow] [FillPreviewList]: Add ListItem {0} (Title: {1}, Channel: {2}, startTime: {3}, idprogram: {4}, ratingStar: {5}, TvMovieStar: {6}, image: {7})", _
                                            _ItemCounter, _Program.Title, _Program.ReferencedChannel.DisplayName, _
                                            _Program.StartTime, _Program.IdProgram, _
                                            GuiLayout.ratingStar(_TvMovieProgram.ReferencedProgram), _
                                            _TvMovieProgram.TVMovieBewertung, GuiLayout.Image(_TvMovieProgram))

                                _ItemCounter = _ItemCounter + 1
                                _lastTitle = _Program.Title & _Program.EpisodeName
                            End If
                        End If

                        If _ItemCounter > 6 Then Exit For

                        'log Ausgabe abfangen, falls der Thread abgebrochen wird
                    Catch ex As ThreadAbortException ' Ignore this exception
                        MyLog.Debug("[CategoriesGuiWindow] [FillPreviewList]: --- THREAD ABORTED ---")
                        MyLog.Debug("")
                    Catch ex As GentleException
                    Catch ex As Exception
                        MyLog.Error("[CategoriesGuiWindow] [FillPreviewList]: Loop exception err:" & ex.Message & " stack:" & ex.StackTrace)
                    End Try

                Next

                ctlProgressBar.Visible = False
                _PreviewList.Visible = True

                If CBool(_layer.GetSetting("ClickfinderCategorieShowLocalSeries", "false").Value) = True Then
                    MyLog.Debug("[CategoriesGuiWindow] [FillPreviewList]: Series Episodes exist local and will not be displayed ({0})", _LogLocalSeries)
                End If
                If CBool(_layer.GetSetting("ClickfinderCategorieShowLocalMovies", "false").Value) = True Then
                    MyLog.Debug("[CategoriesGuiWindow] [FillPreviewList]: Movies exist local and will not be displayed ({0})", _LogLocalMovies)
                End If

                MyLog.Debug("[CategoriesGuiWindow] [FillPreviewList]: Thread finished")
                MyLog.Debug("")

                GUIListControl.SelectItemControl(GetID, _LastFocusedControlID, _LastFocusedIndex)
                GUIListControl.FocusControl(GetID, _LastFocusedControlID)


                'log Ausgabe abfangen, falls der Thread abgebrochen wird
            Catch ex2 As ThreadAbortException ' Ignore this exception
                MyLog.Debug("[CategoriesGuiWindow] [FillPreviewList]: --- THREAD ABORTED ---")
                MyLog.Debug("")
            Catch ex2 As GentleException
            Catch ex2 As Exception
                MyLog.Error("[CategoriesGuiWindow] [FillPreviewList]: exception err:" & ex2.Message & " stack:" & ex2.StackTrace)
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

                        CategoriesGuiWindow.SetGuiProperties(_ClickfinderCategorieView)
                        GUIWindowManager.ActivateWindow(1656544654)

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
                        lItem.IconImage = Config.GetFile(Config.Dir.Thumbs, "Clickfinder ProgramGuide\Categories\") & _Categories(i).Name & ".png"
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
                    CategoriesGuiWindow.SetGuiProperties(_ClickfinderCategorieView)
                    GUIWindowManager.ActivateWindow(1656544654)
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

                ThreadPreviewListFill = New Threading.Thread(AddressOf FillPreviewList)
                ThreadPreviewListFill.IsBackground = True
                ThreadPreviewListFill.Start()

            Else
                MyLog.Debug("[CategoriesGuiWindow] [showNotVisibleCategories]: selected -> exit")
            End If

        End Sub

#End Region

    End Class
End Namespace
