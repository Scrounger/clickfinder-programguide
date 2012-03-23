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
        <SkinControlAttribute(2)> Protected _btnNow As GUIButtonControl = Nothing
        <SkinControlAttribute(3)> Protected _btnPrimeTime As GUIButtonControl = Nothing
        <SkinControlAttribute(4)> Protected _btnLateTime As GUIButtonControl = Nothing
        <SkinControlAttribute(5)> Protected _btnHighlights As GUIButtonControl = Nothing
        <SkinControlAttribute(6)> Protected _btnPreview As GUIButtonControl = Nothing

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
        Private _isAbortException As Boolean = False
        Private Shared _layer As New TvBusinessLayer

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub

        Public Shared Sub SetGuiProperties(ByVal View As CategorieView, Optional ByVal Day As Date = Nothing)
            _ClickfinderCategorieView = View
            _SelectedCategorieItemId = 0
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

            MyLog.Info("")
            MyLog.Info("[CategoriesGuiWindow] {0} load", CategorieViewName)

            For i = 1 To 6
                Translator.SetProperty("#PreviewListImage" & i, "")
                Translator.SetProperty("#PreviewListTvMovieStar" & i, "")
                Translator.SetProperty("#PreviewListRatingStar" & i, 0)
            Next

            If _layer.GetSetting("TvMovieImportIsRunning", "false").Value = "true" Then
                Translator.SetProperty("#SettingLastUpdate", Translation.ImportIsRunning)
                MyLog.Debug("[GuiItems] [OnPageLoad]: _ClickfinderCurrentDate = {0}", "TvMovie++ Import is running !")
            Else
                Translator.SetProperty("#SettingLastUpdate", GuiLayout.LastUpdateLabel)
            End If

            If _ClickfinderCategorieView = CategorieView.Day Then
                Translator.SetProperty("#CategorieView", getTranslatedDayOfWeek(_Day) & " " & Format(_Day, "dd.MM.yyyy"))
            Else
                Translator.SetProperty("#CategorieView", CategorieViewName & " " & Format(PeriodeStartTime.Hour, "00") & ":" & Format(PeriodeStartTime.Minute, "00"))
            End If

            Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(0)
            _SelectedCategorieMinRunTime = _Categorie.MinRunTime

            Dim _Thread1 As New Thread(AddressOf FillCategories)
            Dim _Thread2 As New Thread(AddressOf FillPreviewList)

            _Thread1.Start()
            _Thread2.Start()


            MyBase.OnPageLoad()
        End Sub

        Protected Overrides Sub OnPageDestroy(ByVal new_windowId As Integer)
            'MsgBox("Hallo")
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


                If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_SELECT_ITEM Then
                    If _CategorieList.IsFocused = True Then

                        Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(_SelectedCategorieItemId)
                        If _ClickfinderCategorieView = CategorieView.Now Then
                            ItemsGuiWindow.SetGuiProperties(CStr(Replace(Replace(_Categorie.SqlString, "#startTime", MySqlDate(PeriodeStartTime.AddMinutes(CDbl((-1) * _Categorie.NowOffset)))), "#endTime", MySqlDate(PeriodeEndTime))), _Categorie.MinRunTime, _Categorie.sortedBy, _Categorie.idClickfinderCategories)
                        Else
                            ItemsGuiWindow.SetGuiProperties(CStr(Replace(Replace(_Categorie.SqlString, "#startTime", MySqlDate(PeriodeStartTime)), "#endTime", MySqlDate(PeriodeEndTime))), _Categorie.MinRunTime, _Categorie.sortedBy, _Categorie.idClickfinderCategories)
                        End If


                        Translator.SetProperty("#ItemsLeftListLabel", _Categorie.Name & " " & Translation.von & " " & Format(PeriodeStartTime.Hour, "00") & ":" & Format(PeriodeStartTime.Minute, "00") & " - " & Format(PeriodeEndTime.Hour, "00") & ":" & Format(PeriodeEndTime.Minute, "00"))
                        Translator.SetProperty("#ItemsRightListLabel", Translation.Categorie & ": " & _Categorie.Name)
                        GUIWindowManager.ActivateWindow(1656544653)
                    End If
                End If

                If _CategorieList.IsFocused = True Then

                    If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MOVE_UP Then
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


                        ThreadPreviewListFill = New Threading.Thread(AddressOf FillPreviewList)
                        ThreadPreviewListFill.IsBackground = True
                        ThreadPreviewListFill.Start()
                    End If

                    If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MOVE_DOWN Then
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

                        ThreadPreviewListFill = New Threading.Thread(AddressOf FillPreviewList)
                        ThreadPreviewListFill.IsBackground = True
                        ThreadPreviewListFill.Start()

                    End If


                End If

                'Play Button (P) -> Start channel
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MUSIC_PLAY Then
                    Try
                        If _PreviewList.IsFocused = True Then StartTv(Program.Retrieve(_PreviewList.SelectedListItem.ItemId).ReferencedChannel)
                    Catch ex As Exception
                        MyLog.[Error]("[Play Button]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
                    End Try
                End If

                'Record Button (R) -> MP TvProgramInfo aufrufen --Über keychar--
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_KEY_PRESSED Then
                    If action.m_key IsNot Nothing Then
                        If action.m_key.KeyChar = 114 Then
                            If _PreviewList.IsFocused = True Then LoadTVProgramInfo(Program.Retrieve(_PreviewList.SelectedListItem.ItemId))
                        End If
                    End If
                End If

                'Menu Button (F9) -> Context Menu open
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_CONTEXT_MENU Then
                    If _PreviewList.IsFocused = True Then ShowContextMenu(_PreviewList.SelectedListItem.ItemId, GetID)
                    If _CategorieList.IsFocused = True Then ShowCategoriesContextMenu(_CategorieList.SelectedListItem.ItemId)
                End If

                'OSD Info Button (Y) -> Context Menu open (gleiche Fkt. wie Menu Button)
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_KEY_PRESSED Then
                    If action.m_key IsNot Nothing Then
                        If action.m_key.KeyChar = 121 Then
                            If _PreviewList.IsFocused = True Then ShowContextMenu(_PreviewList.SelectedListItem.ItemId, GetID)
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

                CategoriesGuiWindow.SetGuiProperties(CategoriesGuiWindow.CategorieView.Now)
                GUIWindowManager.ActivateWindow(1656544654)
            End If

            If control Is _btnPrimeTime Then

                CategoriesGuiWindow.SetGuiProperties(CategoriesGuiWindow.CategorieView.PrimeTime)
                GUIWindowManager.ActivateWindow(1656544654)
            End If
            If control Is _btnLateTime Then

                CategoriesGuiWindow.SetGuiProperties(CategoriesGuiWindow.CategorieView.LateTime)
                GUIWindowManager.ActivateWindow(1656544654)
            End If

            If control Is _btnHighlights Then

                CategoriesGuiWindow.SetGuiProperties(CategoriesGuiWindow.CategorieView.Preview)
                GUIWindowManager.ActivateWindow(165654465)
            End If

            If control Is _btnPreview Then

                CategoriesGuiWindow.SetGuiProperties(CategoriesGuiWindow.CategorieView.Preview)
                GUIWindowManager.ActivateWindow(1656544654)
            End If

        End Sub
#End Region

#Region "Functions"

        Private Sub FillCategories()
            Dim sb As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(ClickfinderCategories))
            sb.AddOrderByField(True, "sortOrder")
            Dim stmt As SqlStatement = sb.GetStatement(True)
            _Categories = ObjectFactory.GetCollection(GetType(ClickfinderCategories), stmt.Execute())

            For i = 0 To _Categories.Count - 1

                If _Categories(i).isVisible = True Then
                    AddListControlItem(GetID, _CategorieList, _Categories(i).SortOrder, String.Empty, _Categories(i).Name, , , Config.GetFile(Config.Dir.Thumbs, "Clickfinder ProgramGuide\Categories\") & _Categories(i).Name & ".png", _Categories(i).MinRunTime)
                End If

            Next
        End Sub

        Private Sub FillPreviewList()
            Dim _SqlString As String = String.Empty
            Dim _lastTitle As String = String.Empty
            Dim _ItemCounter As Integer = 1
            Dim _timeLabel As String = String.Empty
            Dim _infoLabel As String = String.Empty
            Dim _imagepath As String = String.Empty
            Dim _program As Program = Nothing
            Dim _startTime As Date = Nothing
            Dim _endTime As Date = Nothing

            _isAbortException = False

            Try
                _PreviewList.Visible = False
                _PreviewList.Clear()

                For i = 1 To 6
                    Translator.SetProperty("#PreviewListImage" & i, "")
                    Translator.SetProperty("#PreviewListTvMovieStar" & i, "")
                    Translator.SetProperty("#PreviewListRatingStar" & i, 0)
                Next

                Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(_SelectedCategorieItemId)

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

                _SqlString = _SqlString & " LIMIT 30"

                Dim _result As New ArrayList
                _result.AddRange(Broker.Execute(_SqlString).TransposeToFieldList("idProgram", False))

                For i = 0 To _result.Count - 1
                    Try
                        Dim SaveChanges As Boolean = False

                        'ProgramDaten über idProgram laden
                        Dim _ResultProgram As Program = Program.Retrieve(_result.Item(i))

                        Dim _TvMovieProgram As TVMovieProgram = getTvMovieProgram(_ResultProgram)

                        'Falls lokale Movies/Videos nicht angezeigt werden sollen -> aus Array entfernen
                        If CBool(_layer.GetSetting("ClickfinderShowLocalMovies", "false").Value) = True Then
                            If _TvMovieProgram.local = True And _TvMovieProgram.idSeries = 0 Then
                                MyLog.Debug("[CategoriesGuiWindow] [FillPreviewList]: {0} exist local and will not be displayed", _TvMovieProgram.ReferencedProgram.Title)
                                Continue For
                            End If
                        End If

                        'Falls lokale Serien nicht angezeigt werden sollen -> aus Array entfernen
                        If CBool(_layer.GetSetting("ClickfinderShowLocalSeries", "false").Value) = True Then
                            If _TvMovieProgram.local = True And _TvMovieProgram.idSeries > 0 Then
                                MyLog.Debug("[CategoriesGuiWindow] [SetGuiProperties]: {0} exist local and will not be displayed", _TvMovieProgram.ReferencedProgram.Title & " - " & _TvMovieProgram.ReferencedProgram.EpisodeName)
                                Continue For
                            End If
                        End If

                        If DateDiff(DateInterval.Minute, _ResultProgram.StartTime, _ResultProgram.EndTime) > _SelectedCategorieMinRunTime Then

                            If String.Equals(_lastTitle, _ResultProgram.Title & _ResultProgram.EpisodeName) = False And _ResultProgram.ReferencedChannel.IsTv = True Then

                                'Prüfen ob gleiches Program evtl. auf HDSender (mapping gleich) auch läuft
                                Dim _idHDchannelProgram As Integer = GetHDChannel(_ResultProgram)
                                If _idHDchannelProgram > 0 Then
                                    _program = Program.Retrieve(_idHDchannelProgram)
                                Else
                                    _program = _ResultProgram
                                End If

                                Translator.SetProperty("#PreviewListRatingStar" & _ItemCounter, GuiLayout.ratingStar(_program))
                                Translator.SetProperty("#PreviewListTvMovieStar" & _ItemCounter, GuiLayout.TvMovieStar(_TvMovieProgram))
                                Translator.SetProperty("#PreviewListImage" & _ItemCounter, GuiLayout.Image(_TvMovieProgram))

                                AddListControlItem(GetID, _PreviewList, _program.IdProgram, _program.ReferencedChannel.DisplayName, _program.Title, GuiLayout.TimeLabel(_TvMovieProgram), GuiLayout.InfoLabel(_TvMovieProgram))

                                _ItemCounter = _ItemCounter + 1
                                _lastTitle = _program.Title & _program.EpisodeName
                            End If
                        End If

                        If _ItemCounter > 6 Then Exit For

                        'log Ausgabe abfangen, falls der Thread abgebrochen wird
                    Catch ex As ThreadAbortException ' Ignore this exception
                        _isAbortException = True
                    Catch ex As GentleException
                    Catch ex As Exception
                        If _isAbortException = False Then
                            MyLog.[Error]("[CategoriesGuiWindow] [FillPreviewList]: Loop exception err:" & ex.Message & " stack:" & ex.StackTrace)
                        Else
                            _isAbortException = False
                        End If
                    End Try

                Next

                ctlProgressBar.Visible = False
                _PreviewList.Visible = True

                'log Ausgabe abfangen, falls der Thread abgebrochen wird
            Catch ex2 As ThreadAbortException ' Ignore this exception
                _isAbortException = True
            Catch ex As GentleException
            Catch ex2 As Exception
                If _isAbortException = False Then
                    MyLog.[Error]("[CategoriesGuiWindow] [FillPreviewList]: exception err:" & ex2.Message & " stack:" & ex2.StackTrace)
                Else
                    _isAbortException = False
                End If
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

                'Categorie umbennenen
                Dim lItemRename As New GUIListItem
                lItemRename.Label = Translation.CategorieRename
                'lItemOn.IconImage = "play_enabled.png"
                dlgContext.Add(lItemRename)
                lItemRename.Dispose()

                'Categorie verstecken
                Dim lItemHide As New GUIListItem
                lItemHide.Label = Translation.CategorieHide
                'lItemOn.IconImage = "play_enabled.png"
                dlgContext.Add(lItemHide)
                lItemHide.Dispose()

                'versteckte Categorie anzeigen
                Dim lItemShow As New GUIListItem
                lItemShow.Label = Translation.CategorieShow
                'lItemOn.IconImage = "play_enabled.png"
                dlgContext.Add(lItemShow)
                lItemShow.Dispose()

                dlgContext.DoModal(GetID)


                Select Case dlgContext.SelectedLabel
                    Case Is = 0
                        'Categorie umbennen

                    Case Is = 1
                        'Categorie verstecken
                        _Categorie.isVisible = False
                        _Categorie.Persist()

                        CategoriesGuiWindow.SetGuiProperties(_ClickfinderCategorieView)
                        GUIWindowManager.ActivateWindow(1656544654)

                    Case Is = 2
                        'versteckte Categorie anzeigen
                        showNotVisibleCategories()

                End Select


            Catch ex As Exception
                MyLog.[Error]("[CategoriesGuiWindow] [ShowCategoriesContextMenu]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
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

                    'MsgBox(_idCategorieContainer(dlgContext.SelectedLabel))

                    Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(_idCategorieContainer(dlgContext.SelectedLabel))
                    _Categorie.isVisible = True
                    _Categorie.Persist()
                    CategoriesGuiWindow.SetGuiProperties(_ClickfinderCategorieView)
                    GUIWindowManager.ActivateWindow(1656544654)
                Else
                    'dlgContext.ShowQuickNumbers = False
                    Dim lItem As New GUIListItem
                    lItem.Label = Translation.CategorieHideNotFound
                    dlgContext.Add(lItem)
                    lItem.Dispose()
                    dlgContext.DoModal(GetID)

                End If
                _idCategorieContainer.Clear()

            Catch ex As Exception
                MyLog.[Error]("[CategoriesGuiWindow] [showNotVisibleCategories]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try
        End Sub


#End Region

    End Class
End Namespace
