Imports System
Imports System.IO
Imports System.Windows.Forms
Imports MediaPortal.GUI.Library
Imports MediaPortal.Dialogs
Imports MediaPortal.Profile
Imports MediaPortal.Configuration
Imports MediaPortal.Utils
Imports MediaPortal.Util
Imports TvDatabase

Imports Gentle.Common
Imports Gentle.Provider.MySQL
Imports TvPlugin

Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Threading

Imports Action = MediaPortal.GUI.Library.Action
Imports Gentle.Framework
Imports System.Drawing




Namespace ClickfinderProgramGuide
    <PluginIcons("ClickfinderProgramGuide.Config.png", "ClickfinderProgramGuide.Config_disable.png")> _
    Public Class Class1
        Inherits GUIWindow
        Implements ISetupForm



        Public Delegate Sub D_Parallel()

#Region "Skin Controls"

        <SkinControlAttribute(2)> Protected btnNow As GUIButtonControl = Nothing
        <SkinControlAttribute(3)> Protected btnPrimeTime As GUIButtonControl = Nothing
        <SkinControlAttribute(4)> Protected btnLateTime As GUIButtonControl = Nothing
        <SkinControlAttribute(5)> Protected btnPreview As GUIButtonControl = Nothing
        <SkinControlAttribute(6)> Protected btnCommingTipps As GUIButtonControl = Nothing


        <SkinControlAttribute(9)> Protected ctlProgressBar As GUIAnimation = Nothing
        <SkinControlAttribute(10)> Protected ctlList As GUIListControl = Nothing
        <SkinControlAttribute(11)> Protected ctlImportProgress As GUIProgressControl = Nothing




        <SkinControlAttribute(30)> Protected DetailsImage As GUIImage = Nothing
        <SkinControlAttribute(31)> Protected DetailsTitel As GUIFadeLabel = Nothing
        <SkinControlAttribute(32)> Protected DetailsBeschreibung As GUITextScrollUpControl = Nothing
        <SkinControlAttribute(33)> Protected DetailsKanal As GUIFadeLabel = Nothing
        <SkinControlAttribute(34)> Protected DetailsZeit As GUIFadeLabel = Nothing
        <SkinControlAttribute(35)> Protected DetailsGenre As GUIFadeLabel = Nothing
        <SkinControlAttribute(36)> Protected DetailsActors As GUIFadeLabel = Nothing
        <SkinControlAttribute(37)> Protected DetailsRatingImage As GUIImage = Nothing
        <SkinControlAttribute(38)> Protected DetailsOrgTitel As GUIFadeLabel = Nothing
        <SkinControlAttribute(39)> Protected DetailsRegie As GUIFadeLabel = Nothing
        <SkinControlAttribute(40)> Protected DetailsYearLand As GUIFadeLabel = Nothing
        <SkinControlAttribute(41)> Protected DetailsDauer As GUIFadeLabel = Nothing
        <SkinControlAttribute(42)> Protected DetailsKurzKritik As GUIFadeLabel = Nothing
        <SkinControlAttribute(43)> Protected DetailsBewertungen As GUIFadeLabel = Nothing



        <SkinControlAttribute(89)> Protected btnRemember As GUIButtonControl = Nothing
        <SkinControlAttribute(90)> Protected btnBack As GUIButtonControl = Nothing
        <SkinControlAttribute(91)> Protected btnRecord As GUIButtonControl = Nothing

        <SkinControlAttribute(100)> Protected btnTipp0 As GUIButtonControl = Nothing
        <SkinControlAttribute(101)> Protected btnTipp1 As GUIButtonControl = Nothing
        <SkinControlAttribute(102)> Protected btnTipp2 As GUIButtonControl = Nothing
        <SkinControlAttribute(103)> Protected btnTipp3 As GUIButtonControl = Nothing
        <SkinControlAttribute(104)> Protected btnTipp4 As GUIButtonControl = Nothing


        <SkinControlAttribute(110)> Protected FavTitel0 As GUIFadeLabel = Nothing
        <SkinControlAttribute(111)> Protected FavImage0 As GUIImage = Nothing
        <SkinControlAttribute(112)> Protected FavKanal0 As GUIFadeLabel = Nothing
        <SkinControlAttribute(113)> Protected FavGenre0 As GUIFadeLabel = Nothing
        <SkinControlAttribute(114)> Protected FavZeit0 As GUILabelControl = Nothing
        <SkinControlAttribute(115)> Protected FavBewertung0 As GUIFadeLabel = Nothing
        <SkinControlAttribute(116)> Protected FavKritik0 As GUIFadeLabel = Nothing
        <SkinControlAttribute(117)> Protected FavRatingImage0 As GUIImage = Nothing

        <SkinControlAttribute(120)> Protected FavTitel1 As GUIFadeLabel = Nothing
        <SkinControlAttribute(121)> Protected FavImage1 As GUIImage = Nothing
        <SkinControlAttribute(122)> Protected FavKanal1 As GUIFadeLabel = Nothing
        <SkinControlAttribute(123)> Protected FavGenre1 As GUIFadeLabel = Nothing
        <SkinControlAttribute(124)> Protected FavZeit1 As GUILabelControl = Nothing
        <SkinControlAttribute(125)> Protected FavBewertung1 As GUIFadeLabel = Nothing
        <SkinControlAttribute(126)> Protected FavKritik1 As GUIFadeLabel = Nothing
        <SkinControlAttribute(127)> Protected FavRatingImage1 As GUIImage = Nothing

        <SkinControlAttribute(130)> Protected FavTitel2 As GUIFadeLabel = Nothing
        <SkinControlAttribute(131)> Protected FavImage2 As GUIImage = Nothing
        <SkinControlAttribute(132)> Protected FavKanal2 As GUIFadeLabel = Nothing
        <SkinControlAttribute(133)> Protected FavGenre2 As GUIFadeLabel = Nothing
        <SkinControlAttribute(134)> Protected FavZeit2 As GUILabelControl = Nothing
        <SkinControlAttribute(135)> Protected FavBewertung2 As GUIFadeLabel = Nothing
        <SkinControlAttribute(136)> Protected FavKritik2 As GUIFadeLabel = Nothing
        <SkinControlAttribute(137)> Protected FavRatingImage2 As GUIImage = Nothing


        <SkinControlAttribute(140)> Protected FavTitel3 As GUIFadeLabel = Nothing
        <SkinControlAttribute(141)> Protected FavImage3 As GUIImage = Nothing
        <SkinControlAttribute(142)> Protected FavKanal3 As GUIFadeLabel = Nothing
        <SkinControlAttribute(143)> Protected FavGenre3 As GUIFadeLabel = Nothing
        <SkinControlAttribute(144)> Protected FavZeit3 As GUILabelControl = Nothing
        <SkinControlAttribute(145)> Protected FavBewertung3 As GUIFadeLabel = Nothing
        <SkinControlAttribute(146)> Protected FavKritik3 As GUIFadeLabel = Nothing
        <SkinControlAttribute(147)> Protected FavRatingImage3 As GUIImage = Nothing

        <SkinControlAttribute(150)> Protected FavTitel4 As GUIFadeLabel = Nothing
        <SkinControlAttribute(151)> Protected FavImage4 As GUIImage = Nothing
        <SkinControlAttribute(152)> Protected FavKanal4 As GUIFadeLabel = Nothing
        <SkinControlAttribute(153)> Protected FavGenre4 As GUIFadeLabel = Nothing
        <SkinControlAttribute(154)> Protected FavZeit4 As GUILabelControl = Nothing
        <SkinControlAttribute(155)> Protected FavBewertung4 As GUIFadeLabel = Nothing
        <SkinControlAttribute(156)> Protected FavKritik4 As GUIFadeLabel = Nothing
        <SkinControlAttribute(157)> Protected FavRatingImage4 As GUIImage = Nothing



#End Region
#Region "Variablen"
        Public _ShowSQLString As String
        Public _CurrentCategorie As String
        Public _CurrentQuery As String
        Private _TippButtonFocus As Boolean
        Private _TippButtonFocusID As Integer
        Private LiveCorrection As Boolean = CBool(MPSettingRead("config", "LiveCorrection"))
        Private WdhCorrection As Boolean = CBool(MPSettingRead("config", "WdhCorrection"))

        Private _ZeitQueryStart As Date
        Private _ZeitQueryEnde As Date
        Private _RespectInFavGroup As Boolean
        Private _useRatingTvLogos As String = MPSettingRead("config", "useRatingTvLogos")
        Private _SettingIgnoreMinTimeSeries As String = MPSettingRead("config", "IgnoreMinTimeSeries")
        Private _SettingDelayNow As Double = CInt(MPSettingRead("config", "DelayNow"))
        Private _SettingPrimeTimeHour As Double = CDbl(MPSettingRead("config", "PrimeTimeHour"))
        Private _SettingPrimeTimeMinute As Double = CDbl(MPSettingRead("config", "PrimeTimeMinute"))
        Private _SettingLateTimeHour As Double = CDbl(MPSettingRead("config", "LateTimeHour"))
        Private _SettingLateTimeMinute As Double = CDbl(MPSettingRead("config", "LateTimeMinute"))
        Private _GuiImage As Dictionary(Of Integer, GUIImage) = New Dictionary(Of Integer, GUIImage)
        Private _GuiButton As Dictionary(Of Integer, GUIButtonControl) = New Dictionary(Of Integer, GUIButtonControl)    
        Const _idStartCounter As Integer = 110
        Const _idStoppCounter As Integer = 160
        Private _TippClickfinderSendungID0 As Long
        Private _TippClickfinderSendungID1 As Long
        Private _TippClickfinderSendungID2 As Long
        Private _TippClickfinderSendungID3 As Long
        Private _TippClickfinderSendungID4 As Long
        Private _TippClickfinderSendungID As Dictionary(Of Integer, Long) = New Dictionary(Of Integer, Long)
        Private _ShowItemDetailsClickfinderSendungId As String

#End Region


        ' MP SettingMenu
#Region "iSetupFormImplementation"

        Public Overloads Overrides Property GetID() As Integer
            Get
                Return 165654465
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property
        Public Function PluginName() As String Implements MediaPortal.GUI.Library.ISetupForm.PluginName
            Return "Clickfinder ProgramGuide"
        End Function
        Public Function Description() As String Implements MediaPortal.GUI.Library.ISetupForm.Description
            Return "Describtion"
        End Function
        Public Function Author() As String Implements MediaPortal.GUI.Library.ISetupForm.Author
            Return "Scrounger"
        End Function
        Public Sub ShowPlugin() Implements MediaPortal.GUI.Library.ISetupForm.ShowPlugin
            Dim setup As New Setup
            setup.Show()
        End Sub
        Public Function CanEnable() As Boolean Implements MediaPortal.GUI.Library.ISetupForm.CanEnable
            Return True
        End Function
        Public Function GetWindowId() As Integer _
        Implements MediaPortal.GUI.Library.ISetupForm.GetWindowId

            Return 165654465

        End Function
        Public Function DefaultEnabled() As Boolean Implements MediaPortal.GUI.Library.ISetupForm.DefaultEnabled
            Return True
        End Function
        Public Function HasSetup() As Boolean Implements MediaPortal.GUI.Library.ISetupForm.HasSetup
            Return True
        End Function
        Public Function GetHome(ByRef strButtonText As String, ByRef strButtonImage As String, ByRef strButtonImageFocus As String, ByRef strPictureImage As String) As Boolean Implements MediaPortal.GUI.Library.ISetupForm.GetHome
            strButtonText = "Clickfinder ProgramGuide"

            strButtonImage = String.Empty

            strButtonImageFocus = String.Empty

            strPictureImage = "Config.png"

            Return True
        End Function
        Public Overloads Overrides Function Init() As Boolean
            'Beim initialisieren des Plugin den Screen laden
            'AddHandler GUIGraphicsContext.form.KeyDown, AddressOf MyBase.FormKeyDown
            Return Load(GUIGraphicsContext.Skin + "\ClickfinderProgramGuide.xml")
        End Function


#End Region

#Region "GUI Events"
        Protected Overrides Sub OnPageLoad()
            MyBase.OnPageLoad()
            GUIWindowManager.NeedRefresh()



            Dictonary()

            ctlProgressBar.Visibility = Windows.Visibility.Hidden
            DetailsImage.Visibility = Windows.Visibility.Hidden
            btnNow.IsFocused = True


            'MsgBox(LiveCorrection & " " & WdhCorrection)

            'btnNow.IsFocused = False


            'Screen wird das erste Mal geladen
            If _ShowSQLString = "" Then

                btnNow.IsFocused = True



                Button_Now()
                'Screen wird erneut geladen
            ElseIf _CurrentCategorie = "" Then
                ShowCategories()
                Select Case _CurrentQuery
                    Case Is = "Now"
                        btnNow.IsFocused = True
                    Case Is = "PrimeTime"
                        btnPrimeTime.IsFocused = True
                    Case Is = "LateTime"
                        btnLateTime.IsFocused = True
                    Case Is = "Preview"
                        btnPreview.IsFocused = True
                End Select
            Else
                Dim _ProgressBar As New Thread(AddressOf ShowProgressbar)
                Dim _Threat As New Thread(AddressOf ShowSelectedCategorieItems)
                ShowTipps()
                _ProgressBar.Start()
                _Threat.Start()
                Select Case _CurrentQuery
                    Case Is = "Now"
                        btnNow.IsFocused = True
                    Case Is = "PrimeTime"
                        btnPrimeTime.IsFocused = True
                    Case Is = "LateTime"
                        btnLateTime.IsFocused = True
                    Case Is = "Preview"
                        btnPreview.IsFocused = True
                End Select
            End If


        End Sub
        Public Overrides Sub OnAction(ByVal action As MediaPortal.GUI.Library.Action)
            MyBase.OnAction(action)

            If GUIWindowManager.ActiveWindow = GetID Then
                If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_SELECT_ITEM _
                Or action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MOVE_DOWN _
                Or action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MOVE_UP _
                Or action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_PAGE_UP _
                Or action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_PAGE_DOWN _
                Or action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_HOME _
                Or action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_END Then

                    If ctlList.IsFocused Then

                    End If
                End If


            End If

        End Sub
        

        Protected Overrides Sub OnPageDestroy(ByVal new_windowId As Integer)
            MyBase.OnPageDestroy(new_windowId)
            _GuiImage.Clear()
            _GuiButton.Clear()
            _TippClickfinderSendungID.Clear()

            Log.Debug("Clickfinder ProgramGuide: [OnPageDestroy]: _currentCategorie" & _CurrentCategorie & " - SQLString: " & _ShowSQLString)
            'GUIWindowManager.ResetAllControls()


        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
        Protected Overrides Sub OnClicked(ByVal controlId As Integer, _
                                          ByVal control As GUIControl, _
                                          ByVal actionType As  _
                                          MediaPortal.GUI.Library.Action.ActionType)

            MyBase.OnClicked(controlId, control, actionType)

            If control Is ctlList Then
                ListControlClick()
            End If

            If control Is btnNow Then
                Button_Now()
            End If

            If control Is btnPrimeTime Then

                Button_PrimeTime()
            End If

            If control Is btnLateTime Then

                Button_LateTime()
            End If
            If control Is btnPreview Then

                Button_Preview()
            End If


            If control Is btnCommingTipps Then
                Button_CommingTipps()
            End If

            If control Is btnBack Then

                If _TippButtonFocus = True Then
                    DetailsImage.Visible = False
                    btnBack.IsFocused = False
                    _GuiButton(_TippButtonFocusID).IsFocused = True
                    _TippButtonFocus = False
                Else
                    DetailsImage.Visible = False
                    ctlList.IsFocused = True
                    'btnLateTime.IsFocused = False
                End If
            End If

            If control Is btnRecord Then

                Button_Record()

            End If

            If control Is btnRemember Then

                Button_Remember()

            End If

            If control Is btnTipp0 Then
                Log.Debug("Clickfinder ProgramGuide: [btnTipp0] Call ShowItemDetails: " & FavKanal0.Label & " - " & _TippClickfinderSendungID(110))
                _TippButtonFocusID = btnTipp0.GetID
                _TippButtonFocus = True
                ShowItemDetails(_TippClickfinderSendungID(110), FavKanal0.Label)
            End If

            If control Is btnTipp1 Then
                Log.Debug("Clickfinder ProgramGuide: [btnTipp1] Call ShowItemDetails: " & FavKanal1.Label & " - " & _TippClickfinderSendungID(120))
                _TippButtonFocusID = btnTipp1.GetID
                _TippButtonFocus = True
                ShowItemDetails(_TippClickfinderSendungID(120), FavKanal1.Label)
            End If

            If control Is btnTipp2 Then
                Log.Debug("Clickfinder ProgramGuide: [btnTipp2] Call ShowItemDetails: " & FavKanal2.Label & " - " & _TippClickfinderSendungID(130))
                _TippButtonFocusID = btnTipp2.GetID
                _TippButtonFocus = True
                ShowItemDetails(_TippClickfinderSendungID(130), FavKanal2.Label)
            End If

            If control Is btnTipp3 Then
                Log.Debug("Clickfinder ProgramGuide: [btnTipp3] Call ShowItemDetails: " & FavKanal3.Label & " - " & _TippClickfinderSendungID(140))
                _TippButtonFocusID = btnTipp3.GetID
                _TippButtonFocus = True
                ShowItemDetails(_TippClickfinderSendungID(140), FavKanal3.Label)
            End If

            If control Is btnTipp4 Then
                Log.Debug("Clickfinder ProgramGuide: [btnTipp4] Call ShowItemDetails: " & FavKanal4.Label & " - " & _TippClickfinderSendungID(150))
                _TippButtonFocusID = btnTipp4.GetID
                _TippButtonFocus = True
                ShowItemDetails(_TippClickfinderSendungID(150), FavKanal4.Label)
            End If

        End Sub
#End Region

#Region "Click Events"

        Private Sub Button_Now()
            Dim t As DateTime = DateTime.Now.Subtract(New System.TimeSpan(0, _SettingDelayNow, 0))

            _ZeitQueryStart = Today.AddHours(t.Hour).AddMinutes(t.Minute)
            _ZeitQueryEnde = _ZeitQueryStart.AddHours(4)
            _CurrentQuery = "Now"



            ShowCategories()


        End Sub

        Private Sub Button_PrimeTime()
            _ZeitQueryStart = Today.AddHours(_SettingPrimeTimeHour).AddMinutes(_SettingPrimeTimeMinute)
            _ZeitQueryEnde = _ZeitQueryStart.AddHours(4)
            _CurrentQuery = "PrimeTime"


            ShowCategories()
        End Sub
        Private Sub Button_LateTime()
            _ZeitQueryStart = Today.AddHours(_SettingLateTimeHour).AddMinutes(_SettingLateTimeMinute)
            _ZeitQueryEnde = _ZeitQueryStart.AddHours(4)
            _CurrentQuery = "LateTime"


            ShowCategories()
        End Sub

        Private Sub Button_Preview()
            _ZeitQueryStart = Today
            _ZeitQueryEnde = _ZeitQueryStart.AddDays(20)
            _CurrentQuery = "Preview"


            ShowCategories()
        End Sub


        Private Sub Button_CommingTipps()

            Dim _Threat1 As New Thread(AddressOf ShowProgressbar)
            Dim _Threat2 As New Thread(AddressOf CreateClickfinderRatingTable)

            _Threat1.Start()
            _Threat2.Start()




            '_Threat2.Join()


            'Dim sb As New SqlBuilder(StatementType.[Select], GetType(Program))
            'sb.AddConstraint([Operator].GreaterThan, "starRating", CInt(0))
            'sb.AddConstraint([Operator].LessThanOrEquals, "starRating", CInt(10))
            'Dim stmt As SqlStatement = sb.GetStatement(True)
            'Dim apps As IList = ObjectFactory.GetCollection(GetType(Program), stmt.Execute())

            'MsgBox(apps.Count - 1)

            'For i = 0 To apps.Count - 1
            '    Dim _program As Program = apps.Item(i)
            '    Dim bla As Channel = Channel.Retrieve(_program.IdChannel)

            '    MsgBox(bla.DisplayName)

            'Next


            'Dim sb As New SqlBuilder(StatementType.[Select], GetType(GroupMap))
            'sb.AddConstraint([Operator].Equals, "idGroup", CInt(2))
            'Dim stmt As SqlStatement = sb.GetStatement(True)
            'Dim apps As IList = ObjectFactory.GetCollection(GetType(GroupMap), stmt.Execute())



            'For I = 0 To apps.Count - 1
            '    Dim bla As GroupMap = apps.Item(I)
            '    Dim ChannelName As Channel = Channel.Retrieve(bla.IdChannel)

            '    MsgBox(ChannelName.DisplayName)
            '    'MsgBox(bla.IdChannel)

            'Next


        End Sub


        Private Sub Button_Record()
            Dim _ClickfinderChannelName As String
            Dim _idChannel As Integer
            Dim _StartZeit As String
            Dim _EndZeit As String
            Dim _Titel As String

            Try



                DetailsImage.Visible = True
                ctlList.IsFocused = False
                btnBack.IsFocused = True

                ReadClickfinderDB("SELECT * FROM Sendungen WHERE SendungID = '" & _ShowItemDetailsClickfinderSendungId & "'")


                While ClickfinderData.Read
                    _ClickfinderChannelName = ClickfinderData.Item("SenderKennung")
                    _Titel = ClickfinderData.Item("Titel")
                    _StartZeit = CDate(ClickfinderData.Item("Beginn"))
                    _EndZeit = CDate(ClickfinderData.Item("Ende"))

                    ReadTvServerDB("SELECT * FROM channel WHERE displayName = '" & DetailsKanal.Label & "'")
                    While TvServerData.Read
                        _idChannel = CInt(TvServerData.Item("idChannel"))
                    End While
                    CloseTvServerDB()


                    If LiveCorrection = True And ClickfinderData.Item("KzLive") = "true" Then
                        _Titel = ClickfinderData.Item("Titel") & " (LIVE)"
                    ElseIf WdhCorrection = True And ClickfinderData.Item("KzWiederholung") = "true" Then
                        _Titel = ClickfinderData.Item("Titel") & " (Wdh.)"
                    Else
                        _Titel = ClickfinderData.Item("Titel")
                    End If

                    LoadTVProgramInfo(_idChannel, _StartZeit, _EndZeit, _Titel)

                    Exit While
                End While


                CloseClickfinderDB()

                btnRecord.IsFocused = False
                btnBack.IsFocused = True

            Catch ex As Exception
                Log.Error("Clickfinder ProgramGuide: [Button_Record]: " & ex.Message)
            End Try

        End Sub
        Private Sub Button_Remember()
            Dim _ClickfinderChannelName As String
            Dim _idChannel As Integer
            Dim _StartZeit As String
            Dim _EndZeit As String
            Dim _Titel As String

            Try



                DetailsImage.Visible = True
                ctlList.IsFocused = False
                btnBack.IsFocused = True

                ReadClickfinderDB("SELECT * FROM Sendungen WHERE SendungID = '" & _ShowItemDetailsClickfinderSendungId & "'")


                While ClickfinderData.Read
                    _ClickfinderChannelName = ClickfinderData.Item("SenderKennung")
                    _Titel = ClickfinderData.Item("Titel")
                    _StartZeit = CDate(ClickfinderData.Item("Beginn"))
                    _EndZeit = CDate(ClickfinderData.Item("Ende"))

                    ReadTvServerDB("SELECT * FROM channel WHERE displayName = '" & DetailsKanal.Label & "'")
                    While TvServerData.Read
                        _idChannel = CInt(TvServerData.Item("idChannel"))
                    End While
                    CloseTvServerDB()


                    If LiveCorrection = True And ClickfinderData.Item("KzLive") = "true" Then
                        _Titel = ClickfinderData.Item("Titel") & " (LIVE)"
                    ElseIf WdhCorrection = True And ClickfinderData.Item("KzWiederholung") = "true" Then
                        _Titel = ClickfinderData.Item("Titel") & " (Wdh.)"
                    Else
                        _Titel = ClickfinderData.Item("Titel")
                    End If

                    btnRemember.IsFocused = False
                    btnBack.IsFocused = True

                    SetNotify(_idChannel, _StartZeit, _EndZeit, _Titel)

                    Exit While
                End While


                CloseClickfinderDB()



            Catch ex As Exception
                Log.Error("Clickfinder ProgramGuide: [Button_Remember]: " & ex.Message)
            End Try

        End Sub

        'ListControl Click Action

        Private Sub ListControlClick()
            Dim _Rating As String
            Dim _ProgressBar As New Thread(AddressOf ShowProgressbar)
            Dim _Threat As New Thread(AddressOf ShowSelectedCategorieItems)
            Dim _SQLWhereAdd As String
            Dim _SQLWhereAddPreview As String
            Dim _SQLOrderBy As String



            _Rating = MPSettingRead("config", "ClickfinderRating")
            _RespectInFavGroup = True


            Select Case ctlList.SelectedListItem.Label.ToString

                Case ".."
                    Log.Debug("Clickfinder ProgramGuide: [ListControlClick] Call ShowCategories")
                    Try
                        'Dim _Threat As New Thread(AddressOf ClearFavInfo)
                        If ctlProgressBar.IsVisible = False Then
                            _CurrentCategorie = ""
                            ctlList.Clear()
                            ShowCategories()
                        End If
                    Catch ex As Exception
                        Log.Error("Clickfinder ProgramGuide: [ListControlClick] Call ShowCategories: " & ex.Message)
                    End Try


                Case "Movies"
                    Log.Debug("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ctlList.SelectedListItem.Label.ToString & " - " & _CurrentQuery.ToString)
                    Try
                        ctlList.Clear()
                        _CurrentCategorie = "Movies"

                        _SQLWhereAdd = "AND Bewertung >= " & _Rating & " AND " & MPSettingRead(_CurrentCategorie, "Where")
                        _SQLWhereAddPreview = "AND " & MPSettingRead(_CurrentCategorie, "PreviewWhere")
                        _SQLOrderBy = MPSettingRead(_CurrentCategorie, "OrderBy")

                        Select Case _CurrentQuery.ToString
                            Case Is = "Now"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "PrimeTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "LateTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "Preview"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAddPreview, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                        End Select
                    Catch ex As Exception
                        Log.Error("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ex.Message)
                    End Try

                Case "Sky Cinema"
                    Log.Debug("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ctlList.SelectedListItem.Label.ToString & " - " & _CurrentQuery.ToString)
                    Try
                        ctlList.Clear()
                        _CurrentCategorie = "Sky Cinema"

                        _SQLWhereAdd = "AND Bewertung >= " & _Rating & " AND " & MPSettingRead(_CurrentCategorie, "Where")
                        _SQLWhereAddPreview = "AND " & MPSettingRead(_CurrentCategorie, "PreviewWhere")
                        _SQLOrderBy = MPSettingRead(_CurrentCategorie, "OrderBy")


                        Select Case _CurrentQuery.ToString
                            Case Is = "Now"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "PrimeTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "LateTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "Preview"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAddPreview, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                        End Select
                    Catch ex As Exception
                        Log.Error("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ex.Message)
                    End Try

                Case "Sky Dokumentationen"
                    Log.Debug("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ctlList.SelectedListItem.Label.ToString & " - " & _CurrentQuery.ToString)
                    Try
                        ctlList.Clear()
                        _CurrentCategorie = "Sky Dokumentationen"

                        _SQLWhereAdd = "AND " & MPSettingRead(_CurrentCategorie, "Where")
                        _SQLWhereAddPreview = "AND " & MPSettingRead(_CurrentCategorie, "PreviewWhere")
                        _SQLOrderBy = MPSettingRead(_CurrentCategorie, "OrderBy")

                        Select Case _CurrentQuery.ToString
                            Case Is = "Now"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "PrimeTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "LateTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "Preview"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAddPreview, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                        End Select
                    Catch ex As Exception
                        Log.Error("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ex.Message)
                    End Try



                Case "HDTV"
                    Log.Debug("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ctlList.SelectedListItem.Label.ToString & " - " & _CurrentQuery.ToString)
                    Try
                        ctlList.Clear()
                        _CurrentCategorie = "HDTV"

                        _SQLWhereAdd = "AND " & MPSettingRead(_CurrentCategorie, "Where")
                        _SQLOrderBy = MPSettingRead(_CurrentCategorie, "OrderBy")


                        Select Case _CurrentQuery.ToString
                            Case Is = "Now"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "PrimeTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "LateTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                        End Select
                    Catch ex As Exception
                        Log.Error("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ex.Message)
                    End Try


                Case "Serien"
                    Log.Debug("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ctlList.SelectedListItem.Label.ToString & " - " & _CurrentQuery.ToString)
                    Try
                        ctlList.Clear()
                        _CurrentCategorie = "Serien"

                        _SQLWhereAdd = "AND " & MPSettingRead(_CurrentCategorie, "Where")
                        _SQLOrderBy = MPSettingRead(_CurrentCategorie, "OrderBy")


                        Select Case _CurrentQuery.ToString
                            Case Is = "Now"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "PrimeTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "LateTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                        End Select
                    Catch ex As Exception
                        Log.Error("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ex.Message)
                    End Try

                Case "Dokumentationen"
                    Log.Debug("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ctlList.SelectedListItem.Label.ToString & " - " & _CurrentQuery.ToString)
                    Try
                        ctlList.Clear()
                        _CurrentCategorie = "Dokumentationen"

                        _SQLWhereAdd = "AND " & MPSettingRead(_CurrentCategorie, "Where")
                        _SQLOrderBy = MPSettingRead(_CurrentCategorie, "OrderBy")

                        Select Case _CurrentQuery.ToString
                            Case Is = "Now"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "PrimeTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "LateTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                        End Select
                    Catch ex As Exception
                        Log.Error("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ex.Message)
                    End Try

                Case "Reportagen"
                    Log.Debug("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ctlList.SelectedListItem.Label.ToString & " - " & _CurrentQuery.ToString)
                    Try
                        ctlList.Clear()
                        _CurrentCategorie = "Reportagen"

                        _SQLWhereAdd = "AND " & MPSettingRead(_CurrentCategorie, "Where")
                        _SQLOrderBy = MPSettingRead(_CurrentCategorie, "OrderBy")


                        Select Case _CurrentQuery.ToString
                            Case Is = "Now"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "PrimeTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "LateTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                        End Select
                    Catch ex As Exception
                        Log.Error("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ex.Message)
                    End Try

                Case "Magazine"
                    Log.Debug("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ctlList.SelectedListItem.Label.ToString & " - " & _CurrentQuery.ToString)
                    Try
                        ctlList.Clear()
                        _CurrentCategorie = "Magazine"

                        _SQLWhereAdd = "AND " & MPSettingRead(_CurrentCategorie, "Where")
                        _SQLOrderBy = MPSettingRead(_CurrentCategorie, "OrderBy")


                        Select Case _CurrentQuery.ToString
                            Case Is = "Now"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "PrimeTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "LateTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                        End Select
                    Catch ex As Exception
                        Log.Error("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ex.Message)
                    End Try

                Case "Sport"
                    Log.Debug("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ctlList.SelectedListItem.Label.ToString & " - " & _CurrentQuery.ToString)

                    Try
                        ctlList.Clear()
                        _CurrentCategorie = "Sport"

                        _SQLWhereAdd = "AND " & MPSettingRead(_CurrentCategorie, "Where")
                        _SQLOrderBy = MPSettingRead(_CurrentCategorie, "OrderBy")

                        Select Case _CurrentQuery.ToString
                            Case Is = "Now"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "PrimeTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "LateTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAdd, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                        End Select
                    Catch ex As Exception
                        Log.Error("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ex.Message)
                    End Try

                Case "Fußball LIVE"
                    Log.Debug("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ctlList.SelectedListItem.Label.ToString & " - " & _CurrentQuery.ToString)

                    Try
                        ctlList.Clear()
                        _CurrentCategorie = "Fußball LIVE"

                        _SQLWhereAddPreview = "AND " & MPSettingRead(_CurrentCategorie, "PreviewWhere")
                        _SQLOrderBy = MPSettingRead(_CurrentCategorie, "OrderBy")

                        Select Case _CurrentQuery.ToString
                            Case Is = "Preview"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, _SQLWhereAddPreview, _SQLOrderBy)
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                        End Select
                    Catch ex As Exception
                        Log.Error("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ex.Message)
                    End Try

                Case Else
                    Log.Debug("Clickfinder ProgramGuide: [ListControlClick] Call ShowItemDetails: " & ctlList.SelectedListItem.Label & " " & ctlList.SelectedListItem.Label3 & " " & ctlList.SelectedListItem.Label2 & " " & ctlList.SelectedListItem.Icon.FileName & " - true")
                    ShowItemDetails(ctlList.SelectedListItem.ItemId, ctlList.SelectedListItem.Icon.FileName, True)

            End Select

        End Sub

#End Region

#Region "Show Events"

        Private Sub ShowCategories()

            Dim _ProgressBar As New Thread(AddressOf ShowProgressbar)
            Dim _Threat As New Thread(AddressOf ShowTipps)

            Try
                Log.Debug("Clickfinder ProgramGuide: [ShowCategories]: _CurrentQuery" & _CurrentQuery)
                _CurrentCategorie = ""
                _RespectInFavGroup = False
                ctlList.ListItems.Clear()


                If _CurrentQuery = "Preview" Then
                    _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, "AND Bewertung = 4", "Beginn ASC, Bewertung DESC, Titel")
                    AddListControlItem(ctlList.ListItems.Count - 1, "Movies", , , "ClickfinderPG_Movies.png")
                    AddListControlItem(ctlList.ListItems.Count - 1, "Sky Cinema")
                    AddListControlItem(ctlList.ListItems.Count - 1, "Sky Dokumentationen")
                    AddListControlItem(ctlList.ListItems.Count - 1, "Fußball LIVE")

                Else
                    _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, "AND KzFilm = true", "Beginn ASC, SendungenDetails.Rating DESC, Titel")
                    AddListControlItem(ctlList.ListItems.Count - 1, "Movies", , , "ClickfinderPG_Movies.png")
                    AddListControlItem(ctlList.ListItems.Count - 1, "Sky Cinema")
                    AddListControlItem(ctlList.ListItems.Count - 1, "Sky Dokumentationen")
                    AddListControlItem(ctlList.ListItems.Count - 1, "HDTV")
                    AddListControlItem(ctlList.ListItems.Count - 1, "Serien", , , "ClickfinderPG_TvSeries.png")
                    AddListControlItem(ctlList.ListItems.Count - 1, "Dokumentationen", , , "ClickfinderPG_Doku.png")
                    AddListControlItem(ctlList.ListItems.Count - 1, "Reportagen")
                    AddListControlItem(ctlList.ListItems.Count - 1, "Magazine")
                    AddListControlItem(ctlList.ListItems.Count - 1, "Sport", , , "ClickfinderPG_Sport.png")
                End If



                'AddListControlItem("Bundesliga")
                'AddListControlItem("ChampionsLeague")

                'Clickfinder Rating prüfen ob vorhanden und anschließend Tipps anzeigen
                Dim _Threat2 As New Thread(AddressOf CreateClickfinderRatingTable)
                _Threat2.Start()

            Catch ex As Exception
                Log.Error("Clickfinder ProgramGuide: [ShowCategories]: " & ex.Message)
            End Try

        End Sub

        Private Sub ShowSelectedCategorieItems()
            Dim _Titel As String
            Dim _Genre As String
            Dim _idChannel As String
            Dim _ClickfinderPath As String
            Dim _Bewertung As Integer
            Dim _BewertungStr As String
            Dim _Kritik As String
            Dim _MappingName As String
            Dim _idGroup As String
            Dim _StartZeit As Date
            Dim _EndZeit As Date
            Dim _ChannelName As String
            Dim _EpisodenName As String
            Dim _lastTitel As String
            Dim _FavCounter As Integer

            Dim _SeriesNum As String
            Dim _EpisodeNum As String

            Dim _inFav As Boolean = False
            Dim _MinTime As String
            Dim _SettingMinTime As Integer = CInt(MPSettingRead("config", "MinTime"))
            Dim _TvLogo As String



            Try

                _RespectInFavGroup = True

                Log.Debug("Clickfinder ProgramGuide: [FillListControl] Start")

                _idGroup = MPSettingRead("config", "ChannelGroupID")
                _ClickfinderPath = MPSettingRead("config", "ClickfinderPath")
                _lastTitel = Nothing

                _FavCounter = 0

                If _CurrentCategorie = "Serien" And MPSettingRead("config", "IgnoreMinTimeSeries") = "true" _
                Then _SettingMinTime = 0

                ctlList.ListItems.Clear()
                ctlList.Clear()

                'MsgBox("Categorie: " & _CurrentCategorie & " " & _SettingIgnoreMinTimeSeries & " " & _SettingMinTime)

                AddListControlItem(ctlList.ListItems.Count - 1, "..", , , "defaultFolderBack.png")

                'Clickfinder Datenbank öffnen & Daten einlesen
                ReadClickfinderDB(_ShowSQLString)

                While ClickfinderData.Read

                    _MinTime = Replace(ClickfinderData.Item("Dauer"), " min", "")

                    If CInt(_MinTime) >= _SettingMinTime Then
                        _MappingName = ClickfinderData.Item("SenderKennung")
                        _Genre = ClickfinderData.Item("Genre")
                        _Bewertung = CInt(ClickfinderData.Item("Bewertung"))
                        _BewertungStr = Replace(ClickfinderData.Item("Bewertungen"), ";", " ")
                        _Kritik = ClickfinderData.Item("Kurzkritik")
                        _StartZeit = CDate(ClickfinderData.Item("Beginn"))
                        _EndZeit = CDate(ClickfinderData.Item("Ende"))
                        _EpisodenName = ClickfinderData.Item("Originaltitel")

                        ' Clickfinder Titel Korrektur bei "Append (Live) / (Whd.)
                        If LiveCorrection = True And ClickfinderData.Item("KzLive") = "true" Then
                            _Titel = ClickfinderData.Item("Titel") & " (LIVE)"
                        ElseIf WdhCorrection = True And ClickfinderData.Item("KzWiederholung") = "true" Then
                            _Titel = ClickfinderData.Item("Titel") & " (Wdh.)"
                        Else
                            _Titel = ClickfinderData.Item("Titel")
                        End If



                        'Tv Server öffnen und TVMovieMapping idChannel ermitteln
                        ReadTvServerDB("Select * from tvmoviemapping Inner Join channel on tvmoviemapping.idChannel = channel.idChannel where stationName = '" & ClickfinderData.Item("SenderKennung").ToString & "'")

                        While TvServerData.Read
                            _idChannel = TvServerData.Item("idChannel")
                            _ChannelName = TvServerData.Item("displayName")

                            Log.Debug("Clickfinder ProgramGuide:" & _StartZeit & " " & _Titel & " " & _idChannel)

                            'Prüfen ob Program in der TV Server Program DB ist
                            If ProgramFoundinTvDb(_Titel, _idChannel, _StartZeit, _EndZeit) = True Then
                                Dim Sendung As Program = TvDatabase.Program.RetrieveByTitleTimesAndChannel(_Titel, _StartZeit, _EndZeit, _idChannel)

                                _SeriesNum = Sendung.SeriesNum
                                _EpisodeNum = Sendung.EpisodeNum

                                'angeziegte Tipps und doppelte Einträge abfangen
                                Select Case Sendung.Title
                                    Case Is = _lastTitel
                                        Exit Select
                                    Case Is = FavTitel0.Label
                                        Exit Select
                                    Case Is = FavTitel1.Label
                                        Exit Select
                                    Case Is = FavTitel2.Label
                                        Exit Select
                                    Case Is = FavTitel3.Label
                                        Exit Select
                                    Case Is = FavTitel4.Label
                                        Exit Select
                                    Case Else

                                        If _useRatingTvLogos = "true" Then
                                            _TvLogo = Config.GetFile(Config.Dir.Thumbs, "ClickfinderPG\tv\logos\") & Channel.Retrieve(_idChannel).DisplayName & "_" & _Bewertung & ".png"
                                        Else
                                            _TvLogo = Config.GetFile(Config.Dir.Thumbs, "tv\logos\" & Channel.Retrieve(_idChannel).DisplayName & ".png")
                                        End If

                                        Select Case _CurrentCategorie

                                            Case Is = "Serien"
                                                _Genre = _EpisodenName
                                                If Not _SeriesNum = "" Or Not _EpisodeNum = "" Then
                                                    _Genre = "S" & Format(CInt(_SeriesNum), "00") & "E" & Format(CInt(_EpisodeNum), "00") & " - " & _EpisodenName
                                                End If
                                            Case Is = "Fußball LIVE"
                                                _Genre = _EpisodenName
                                        End Select


                                        AddListControlItem(ClickfinderData.Item("SendungID"), Sendung.Title.ToString, _
                                                           FormatTimeLabel(_StartZeit, _EndZeit), _
                                                            _Genre, _
                                                            _TvLogo)

                                End Select

                                        _lastTitel = Sendung.Title


                            End If
                        End While

                        CloseTvServerDB()
                    End If

                End While
                CloseClickfinderDB()



                ctlProgressBar.Visible = False
            Catch ex As Exception
                Log.Error("Clickfinder ProgramGuide: [ShowSelectedCategorieItems]: " & ex.Message)
            End Try
        End Sub
        Private Sub ShowTipps()

            Dim _Titel As String
            Dim _Genre As String
            Dim _idChannel As String
            Dim _ChannelName As String
            Dim _ClickfinderPath As String
            Dim _Bewertung As Integer
            Dim _BewertungStr As String
            Dim _Kritik As String
            Dim _MappingName As String
            Dim _idGroup As String
            Dim _StartZeit As Date
            Dim _EndZeit As Date
            Dim _TippsCounter As Integer
            Dim _ProgressBar As New Thread(AddressOf ShowProgressbar)
            Dim _BildDatei As String
            Dim _MinTime As String
            Dim _SettingMinTime As Integer = CInt(MPSettingRead("config", "MinTime"))
            Dim _EpisodenName As String
            Dim _SeriesNum As String
            Dim _EpisodeNum As String

            _TippsCounter = _idStartCounter

            Dim _lastTitel As String

            Dim _inFav As Boolean = False

            _ProgressBar.Start()

            ClearTipps()

            'Einstellungen aus ClickfinderPGConfig.xml laden & Variablen übergeben / definieren
            _idGroup = MPSettingRead("config", "ChannelGroupID")
            _ClickfinderPath = MPSettingRead("config", "ClickfinderPath")
            _lastTitel = Nothing


            If _CurrentCategorie = "Serien" And _SettingIgnoreMinTimeSeries = "true" _
                    Then _SettingMinTime = 0

            Try
                'MsgBox("Tipps: " & _CurrentCategorie & " " & _SettingIgnoreMinTimeSeries & " " & _SettingMinTime)
                'MsgBox(_SettingMinTime & " " & _SettingIgnoreMinTimeSeries)

                'Clickfinder Datenbank öffnen & Daten einlesen
                ReadClickfinderDB(_ShowSQLString)

                While ClickfinderData.Read



                    _MinTime = Replace(ClickfinderData.Item("Dauer"), " min", "")

                    If CInt(_MinTime) >= _SettingMinTime Then
                        'Daten aus Clickfinder DB lesen und übergeben
                        _MappingName = ClickfinderData.Item("SenderKennung")
                        _Genre = ClickfinderData.Item("Genre")
                        _Bewertung = CInt(ClickfinderData.Item("Bewertung"))
                        _BewertungStr = Replace(ClickfinderData.Item("Bewertungen"), ";", " ")
                        _Kritik = ClickfinderData.Item("Kurzkritik")
                        _StartZeit = CDate(ClickfinderData.Item("Beginn"))
                        _EndZeit = CDate(ClickfinderData.Item("Ende"))
                        _BildDatei = _ClickfinderPath & "\Hyperlinks\" & ClickfinderData.Item("Bilddateiname")
                        _EpisodenName = ClickfinderData.Item("Originaltitel")


                        ' Clickfinder Titel Korrektur bei "Append (Live) / (Whd.)
                        If LiveCorrection = True And ClickfinderData.Item("KzLive") = "true" Then
                            _Titel = ClickfinderData.Item("Titel") & " (LIVE)"
                        ElseIf WdhCorrection = True And ClickfinderData.Item("KzWiederholung") = "true" Then
                            _Titel = ClickfinderData.Item("Titel") & " (Wdh.)"
                        Else
                            _Titel = ClickfinderData.Item("Titel")
                        End If

                        'MsgBox(_Titel)

                        If ClickfinderData.Item("KzBilddateiHeruntergeladen") = True Then
                            _BildDatei = _ClickfinderPath & "\Hyperlinks\" & ClickfinderData.Item("Bilddateiname")
                        Else
                            _BildDatei = ""
                        End If

                        'TvMovieMapping in TvServerDB auslesen & idChannel + displayName übergeben
                        ReadTvServerDB("Select * from tvmoviemapping Inner Join channel on tvmoviemapping.idChannel = channel.idChannel where stationName = '" & ClickfinderData.Item("SenderKennung").ToString & "'")

                        While TvServerData.Read
                            _idChannel = TvServerData.Item("idChannel")
                            _ChannelName = TvServerData.Item("displayName")


                            'Prüfen ob Sendung in der TV DB vorhanden ist - Ja ->
                            If ProgramFoundinTvDb(_Titel, _idChannel, _StartZeit, _EndZeit) = True Then
                                Dim Sendung As Program = TvDatabase.Program.RetrieveByTitleTimesAndChannel(_Titel, _StartZeit, _EndZeit, _idChannel)

                                _SeriesNum = Sendung.SeriesNum
                                _EpisodeNum = Sendung.EpisodeNum


                                If Not Sendung.Title = _lastTitel And _RespectInFavGroup = True And ChannelFoundInFavGroup(_idChannel) = True Then

                                    'Abbruch wenn alle Tipps gefüllt sind
                                    If _TippsCounter >= _idStoppCounter Then
                                        CloseTvServerDB()
                                        CloseClickfinderDB()
                                        'ctlProgressBar.Visible = False
                                        Exit Sub
                                    Else
                                        _TippClickfinderSendungID(_TippsCounter) = CLng(ClickfinderData.Item("SendungID"))
                                        FillTipps(_TippsCounter, Sendung.Title, _BildDatei, _ChannelName, _StartZeit, _
                                                  _EndZeit, _Genre, _BewertungStr, _Kritik, _
                                                  "ClickfinderPG_R" & CStr(_Bewertung) & ".png", _EpisodenName, _SeriesNum, _EpisodeNum)

                                    End If

                                    _TippsCounter = _TippsCounter + 10

                                ElseIf Not Sendung.Title = _lastTitel And _RespectInFavGroup = False Then

                                    'Abbruch wenn alle Tipps gefüllt sind
                                    If _TippsCounter >= _idStoppCounter Then
                                        CloseTvServerDB()
                                        CloseClickfinderDB()
                                        Exit Sub
                                    Else
                                        _TippClickfinderSendungID(_TippsCounter) = CLng(ClickfinderData.Item("SendungID"))
                                        FillTipps(_TippsCounter, Sendung.Title, _BildDatei, _ChannelName, _StartZeit, _
                                                  _EndZeit, _Genre, _BewertungStr, _Kritik, _
                                                  "ClickfinderPG_R" & CStr(_Bewertung) & ".png", _EpisodenName, _SeriesNum, _EpisodeNum)

                                    End If

                                    _TippsCounter = _TippsCounter + 10


                                End If
                                _lastTitel = Sendung.Title


                            End If

                            ctlProgressBar.Visible = False

                        End While
                        CloseTvServerDB()
                    End If


                End While
                CloseClickfinderDB()
            Catch ex As Exception
                Log.Error("Clickfinder ProgramGuide: [ShowTipps]: " & ex.Message)
            End Try
        End Sub

        Private Sub ShowItemDetails(ByVal ClickfinderSendungID As String, ByVal ChannelName As String, Optional ByVal ChannelNameIsImagePath As Boolean = False)

            Dim _Titel As String
            Dim _ClickfinderPath As String
            Dim _ChannelName As String
            Dim _ClickfinderChannelName As String
            Dim _BildDatei As String
            Dim _Bewertung As Integer

            _ShowItemDetailsClickfinderSendungId = ClickfinderSendungID

            Try

                'ClearDetails()

                'MsgBox(ClickfinderPosID)
                'Detail Info's zeigen - GUIWindow
                DetailsImage.Visible = True
                ctlList.IsFocused = False
                btnTipp0.Focus = False
                btnTipp1.Focus = False
                btnTipp2.Focus = False
                btnTipp3.Focus = False
                btnTipp4.Focus = False
                btnBack.IsFocused = True

                _ChannelName = ChannelName
                _ClickfinderPath = MPSettingRead("config", "ClickfinderPath")

                If ChannelNameIsImagePath = True Then
                    'ChannelName aus Logo extrahieren - wird benötigt um das Clickfinder Mapping im Tv Server zu ermitteln
                    If _useRatingTvLogos = "true" Then
                        _ChannelName = Replace(Left(_ChannelName, InStr(_ChannelName, "_") - 1), Config.GetFile(Config.Dir.Thumbs, "ClickfinderPG\tv\logos\"), "")
                    Else
                        _ChannelName = Replace(Replace(_ChannelName, ".png", ""), Config.GetFile(Config.Dir.Thumbs, "tv\logos\"), "")
                    End If
                End If

                'TV Movie Mapping ChannelName aus TV Server DB lesen
                ReadTvServerDB("Select * from tvmoviemapping Inner Join channel on tvmoviemapping.idChannel = channel.idChannel where displayName = '" & _ChannelName & "'")
                While TvServerData.Read
                    _ClickfinderChannelName = TvServerData.Item("stationName")
                    _ChannelName = TvServerData.Item("displayName")
                End While
                CloseTvServerDB()


                ReadClickfinderDB("Select * from Sendungen Inner Join SendungenDetails on Sendungen.Pos = SendungenDetails.Pos where SendungID = '" & ClickfinderSendungID & "'")

                While ClickfinderData.Read

                    _Titel = ClickfinderData.Item("Titel")

                    'MsgBox(_Titel)

                    DetailsTitel.Label = ClickfinderData.Item("Titel")
                    DetailsBeschreibung.Label = ClickfinderData.Item("Beschreibung")
                    DetailsKanal.Label = _ChannelName
                    _Bewertung = CInt(ClickfinderData.Item("Bewertung"))

                    DetailsZeit.Label = Format(CDate(ClickfinderData.Item("Beginn")).Hour, "00") & _
                                        ":" & Format(CDate(ClickfinderData.Item("Beginn")).Minute, "00") & _
                                            " - " & Format(CDate(ClickfinderData.Item("Ende")).Hour, "00") & _
                                            ":" & Format(CDate(ClickfinderData.Item("Ende")).Minute, "00")

                    DetailsGenre.Label = ClickfinderData.Item("Genre")
                    DetailsActors.Label = ClickfinderData.Item("Darsteller")
                    DetailsOrgTitel.Label = ClickfinderData.Item("Originaltitel")
                    DetailsRegie.Label = ClickfinderData.Item("Regie")
                    DetailsYearLand.Label = ClickfinderData.Item("Herstellungsland") & " " & ClickfinderData.Item("Herstellungsjahr")
                    DetailsDauer.Label = ClickfinderData.Item("Dauer")
                    DetailsKurzKritik.Label = ClickfinderData.Item("Kurzkritik")
                    DetailsBewertungen.Label = Replace(ClickfinderData.Item("Bewertungen"), ";", " ")

                    If ClickfinderData.Item("KzBilddateiHeruntergeladen") = "false" Then
                        DetailsImage.KeepAspectRatio = True
                        DetailsImage.Centered = True

                        'Premier League
                        _BildDatei = UseSportLogos(_Titel, _ChannelName)

                    Else
                        DetailsImage.KeepAspectRatio = False
                        DetailsImage.Centered = False
                        _BildDatei = _ClickfinderPath & "\Hyperlinks\" & ClickfinderData.Item("Bilddateiname")
                    End If

                    DetailsImage.FileName = _BildDatei
                    DetailsRatingImage.FileName = "ClickfinderPG_R" & CStr(_Bewertung) & ".png"

                End While

                CloseClickfinderDB()

            Catch ex As Exception
                Log.Error("Clickfinder ProgramGuide: [ShowItemDetails]: " & ex.Message)
            End Try

        End Sub



#End Region



#Region "ListControl Funktionen"

        'ListControl mit Daten der kommenden TagesTipps füllen - paralleles Threating



        'ProgresBar paralell anzeigen
        Private Sub ShowProgressbar()
            ctlProgressBar.Visible = True
        End Sub
        Private Sub ShowImportProgressbar()
            ctlImportProgress.Visible = True
        End Sub

#End Region

#Region "Functions and Subs"
        ' Log + Error Handling - fertig


        Private Sub FillTipps(ByVal StartIdofGroup As Integer, ByVal _Titel As String, ByVal _FavImagePath As String, _
            ByVal _channelName As String, ByVal _StartZeit As Date, ByVal _EndZeit As Date, ByVal _Genre As String, ByVal _BewertungStr As String, _
            ByVal _Kritik As String, ByVal _FavRatingImagePath As String, ByVal _EpisodenName As String, ByVal _SeriesNum As String, ByVal _EpisodeNum As String)


            Dim _Bilddatei As String

            _Bilddatei = _FavImagePath

            Try
                'Titel
                GUIFadeLabel.SetControlLabel(GetID, StartIdofGroup, _Titel)

                'Image
                If _FavImagePath = "" Then
                    _GuiImage(StartIdofGroup + 1).KeepAspectRatio = True
                    _GuiImage(StartIdofGroup + 1).Centered = True


                    _Bilddatei = UseSportLogos(_Titel, _channelName)
                Else
                    _GuiImage(StartIdofGroup + 1).KeepAspectRatio = False
                    _GuiImage(StartIdofGroup + 1).Centered = False
                    _Bilddatei = _FavImagePath
                End If

                _GuiImage(StartIdofGroup + 1).SetFileName(_Bilddatei)

                'Kanal 
                GUIFadeLabel.SetControlLabel(GetID, StartIdofGroup + 2, _channelName)

                'Genre
                GUIFadeLabel.SetControlLabel(GetID, StartIdofGroup + 3, _Genre)

                'Zeit 
                GUILabelControl.SetControlLabel(GetID, StartIdofGroup + 4, FormatTimeLabel(_StartZeit, _EndZeit))

                If Not _SeriesNum = "" Or Not _EpisodeNum = "" Then
                    _EpisodenName = "S" & Format(CInt(_SeriesNum), "00") & "E" & Format(CInt(_EpisodeNum), "00") & " - " & _EpisodenName
                End If

                'Wenn keine Bewertung vorhanden ist, Genre = EpisodenName, Kanal = Genre, Bewertung = Kanal
                If _BewertungStr = "" Then

                    If _EpisodenName = "" Then
                        GUIFadeLabel.SetControlLabel(GetID, StartIdofGroup + 2, _channelName)
                        GUIFadeLabel.SetControlLabel(GetID, StartIdofGroup + 3, _Genre)
                        GUIFadeLabel.SetControlLabel(GetID, StartIdofGroup + 5, "")
                    Else
                        GUIFadeLabel.SetControlLabel(GetID, StartIdofGroup + 2, _Genre)
                        GUIFadeLabel.SetControlLabel(GetID, StartIdofGroup + 3, _EpisodenName)
                        GUIFadeLabel.SetControlLabel(GetID, StartIdofGroup + 5, _channelName)
                    End If

                Else
                    GUIFadeLabel.SetControlLabel(GetID, StartIdofGroup + 2, _channelName)
                    GUIFadeLabel.SetControlLabel(GetID, StartIdofGroup + 3, _Genre)
                    GUIFadeLabel.SetControlLabel(GetID, StartIdofGroup + 5, _BewertungStr)
                End If


                'Kritik 
                GUIFadeLabel.SetControlLabel(GetID, StartIdofGroup + 6, _Kritik)

                'Rating Image Path
                _GuiImage(StartIdofGroup + 7).SetFileName(_FavRatingImagePath)

                Log.Debug("Clickfinder ProgramGuide: [FillTipps]: " & _Titel & " " & _FavImagePath & " " & _channelName & " " & _Genre & " " & _StartZeit & " " & _EndZeit & " " & _BewertungStr & " " & _Kritik & " " & _FavRatingImagePath)

            Catch ex As Exception
                Log.Error("Clickfinder ProgramGuide: [FillTipps]: " & ex.Message)
            End Try

        End Sub

        Private Sub ClearTipps()

            Dim _TippsCounter As Integer

            _TippsCounter = _idStartCounter

            Try

                Do
                    FillTipps(_TippsCounter, Nothing, "", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "", "", "", "")

                    _TippsCounter = _TippsCounter + 10
                Loop Until _TippsCounter = _idStoppCounter

            Catch ex As Exception
                Log.Error("Clickfinder ProgramGuide: [ClearTipps]: " & ex.Message)
            End Try

        End Sub
        Private Function FormatTimeLabel(ByVal _StartZeit As Date, ByVal _EndZeit As Date)

            If _StartZeit = Nothing And _EndZeit = Nothing Then
                FormatTimeLabel = ""
            Else

                If _CurrentQuery = "Preview" Then
                    Dim oCulture As New System.Globalization.CultureInfo("de-DE")
                    Dim oFormatInfo As System.Globalization.DateTimeFormatInfo = oCulture.DateTimeFormat


                    FormatTimeLabel = oFormatInfo.GetDayName(_StartZeit.DayOfWeek) & _
                                                " " & Format(_StartZeit.Hour, "00") & _
                                                ":" & Format(_StartZeit.Minute, "00")

                Else
                    FormatTimeLabel = Format(_StartZeit.Hour, "00") & _
                            ":" & Format(_StartZeit.Minute, "00") & _
                            " - " & Format(_EndZeit.Hour, "00") & _
                            ":" & Format(_EndZeit.Minute, "00")
                End If
            End If

        End Function
        Public Function UseSportLogos(ByVal _Titel As String, ByVal _channelname As String) As String
            Dim _BildDatei As String

            _BildDatei = Config.GetFile(Config.Dir.Thumbs, "tv\logos\" & _channelname & ".png")

            If InStr(_Titel, "Bundesliga") Then _BildDatei = Config.GetFile(Config.Dir.Thumbs, "tv\logos\Bundesliga.png")
            If InStr(_Titel, "2. Bundesliga") Then _BildDatei = Config.GetFile(Config.Dir.Thumbs, "tv\logos\2. Liga.png")
            If InStr(_Titel, "DFB-Pokal") Then _BildDatei = Config.GetFile(Config.Dir.Thumbs, "tv\logos\DFB Konferenz.png")
            If InStr(_Titel, "Champions League") Then _BildDatei = Config.GetFile(Config.Dir.Thumbs, "tv\logos\Champ. League.png")
            If InStr(_Titel, "Europa League") Then _BildDatei = Config.GetFile(Config.Dir.Thumbs, "tv\logos\Europa League.png")

            If InStr(_Titel, "Premier League") Then _BildDatei = Config.GetFile(Config.Dir.Thumbs, "tv\logos\Fußball ENG.png")

            If InStr(_Titel, "Eishockey: DEL") Then _BildDatei = Config.GetFile(Config.Dir.Thumbs, "tv\logos\Eishockey DEL.png")
            If InStr(_Titel, "Formel 1") Then _BildDatei = Config.GetFile(Config.Dir.Thumbs, "tv\logos\Formel 1.png")

            UseSportLogos = _BildDatei

        End Function
        Private Sub ClearDetails()

            DetailsTitel.Label = ""
            DetailsBeschreibung.Label = ""
            DetailsKanal.Label = ""


            DetailsZeit.Label = ""

            DetailsGenre.Label = ""
            DetailsActors.Label = ""
            DetailsOrgTitel.Label = ""
            DetailsRegie.Label = ""
            DetailsYearLand.Label = ClickfinderData.Item("Herstellungsland") & " " & ClickfinderData.Item("Herstellungsjahr")
            DetailsDauer.Label = ""
            DetailsKurzKritik.Label = ""
            DetailsBewertungen.Label = ""

            DetailsImage.FileName = ""
            DetailsRatingImage.FileName = ""
        End Sub

        Private Sub Dictonary()
            'Dictionary füllen für später variable Zuweisung der Tipp ImagePaths          
            _GuiImage.Add(111, FavImage0)
            _GuiImage.Add(117, FavRatingImage0)
            _GuiImage.Add(121, FavImage1)
            _GuiImage.Add(127, FavRatingImage1)
            _GuiImage.Add(131, FavImage2)
            _GuiImage.Add(137, FavRatingImage2)
            _GuiImage.Add(141, FavImage3)
            _GuiImage.Add(147, FavRatingImage3)
            _GuiImage.Add(151, FavImage4)
            _GuiImage.Add(157, FavRatingImage4)

            _GuiButton.Add(100, btnTipp0)
            _GuiButton.Add(101, btnTipp1)
            _GuiButton.Add(102, btnTipp2)
            _GuiButton.Add(103, btnTipp3)
            _GuiButton.Add(104, btnTipp4)

            _TippClickfinderSendungID.Add(110, _TippClickfinderSendungID0)
            _TippClickfinderSendungID.Add(120, _TippClickfinderSendungID1)
            _TippClickfinderSendungID.Add(130, _TippClickfinderSendungID2)
            _TippClickfinderSendungID.Add(140, _TippClickfinderSendungID3)
            _TippClickfinderSendungID.Add(150, _TippClickfinderSendungID4)

        End Sub
#End Region


#Region "TV Movie and TVServer Database Access and Function"
        'Log und Error Handling - fertig

        Public ConClickfinderDBRead As New OleDb.OleDbConnection
        Public CmdClickfinderDBRead As New OleDb.OleDbCommand
        Public ClickfinderData As OleDb.OleDbDataReader

        'MP - TVServer Datenbank Variablen  - MYSql
        Public ConTvServerDBRead As New MySqlConnection
        Public CmdTvServerDBRead As New MySqlCommand
        Public TvServerData As MySqlDataReader



        Public Count As Long



        Public Sub ReadClickfinderDB(ByVal SQLString As String)
            Dim ClickfinderPath As String

            Log.Debug("TV Movie ProgramGuide: Open TV Movie Clickfinder Database")

            ClickfinderPath = MPSettingRead("config", "ClickfinderPath")


            Try
                ConClickfinderDBRead.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" & "Data Source=" & ClickfinderPath & "\tvdaten.mdb"
                ConClickfinderDBRead.Open()
                'Provider=Microsoft.Jet.OLEDB.4.0;
                'Provider=Microsoft.ACE.OLEDB.12.0;
                CmdClickfinderDBRead = ConClickfinderDBRead.CreateCommand
                CmdClickfinderDBRead.CommandText = SQLString

                ClickfinderData = CmdClickfinderDBRead.ExecuteReader


            Catch ex As Exception

                If IO.File.Exists(ClickfinderPath & "\tvdaten.mdb") Then
                    Log.Error("Clickfinder ProgramGuide: [ReadClickfinderDB]: " & ex.Message)
                Else
                    MPDialogNotify("Warnung ...", "Clickfinder Datenbank nicht gefunden !")
                End If

                CmdClickfinderDBRead.Dispose()
                ConClickfinderDBRead.Close()

            End Try
        End Sub

        Public Sub CloseClickfinderDB()

            Try
                CmdClickfinderDBRead.Dispose()
                ConClickfinderDBRead.Close()

            Catch ex As Exception
                Log.Error("Clickfinder ProgramGuide: [CloseClickfinderDB]: " & ex.Message)

            End Try
        End Sub


        Public Sub ReadTvServerDB(ByVal SQLString As String)



            Try

                ConTvServerDBRead.ConnectionString = Left(Replace(Gentle.Framework.GentleSettings.DefaultProviderConnectionString, " ", ""), InStr(Gentle.Framework.GentleSettings.DefaultProviderConnectionString, "charset=utf8") - 3)
                ConTvServerDBRead.Open()


                CmdTvServerDBRead = ConTvServerDBRead.CreateCommand
                CmdTvServerDBRead.CommandText = SQLString

                TvServerData = CmdTvServerDBRead.ExecuteReader


            Catch ex As Exception

                MPDialogNotify("Warnung ...", "TV Server nicht gefunden")

                Log.Error("Clickfinder ProgramGuide: [ReadTvServerDB]: " & ex.Message)

                CmdTvServerDBRead.Dispose()
                ConTvServerDBRead.Close()
            End Try


        End Sub
        Public Sub CloseTvServerDB()

            Try
                CmdTvServerDBRead.Dispose()
                ConTvServerDBRead.Close()
            Catch ex As Exception
                Log.Error("Clickfinder ProgramGuide: [CloseTvServerDB]: " & ex.Message)
            End Try

        End Sub

        Private Function ProgramFoundinTvDb(ByVal _Titel As String, ByVal _idChannel As String, ByVal _StartZeit As Date, ByVal _EndZeit As Date) As Boolean
            'MP - TVServer Datenbank Variablen  - MYSql
            Dim Con As New MySqlConnection
            Dim Cmd As New MySqlCommand
            Dim DataCount As Long
            Dim _SQLString As String

            _SQLString = ("SELECT COUNT(*) FROM program WHERE Title = '" & _Titel & "' AND idChannel = " & _idChannel & " AND startTime =" & DateTOMySQLstring(_StartZeit) & " AND endTime =" & DateTOMySQLstring(_EndZeit))

            Try

                Con.ConnectionString = Left(Replace(Gentle.Framework.GentleSettings.DefaultProviderConnectionString, " ", ""), InStr(Gentle.Framework.GentleSettings.DefaultProviderConnectionString, "charset=utf8") - 3)
                Con.Open()


                Cmd = Con.CreateCommand
                Cmd.CommandText = _SQLString

                DataCount = CLng(Cmd.ExecuteScalar)


                If DataCount > 0 Then
                    ProgramFoundinTvDb = True
                Else
                    ProgramFoundinTvDb = False
                    Log.Warn("Clickfinder ProgramGuide: [ProgramFoundinTvDb] = False (" & _Titel & ", " & _idChannel & ", " & _StartZeit & ", " & _EndZeit & ")")
                End If

                Cmd.Dispose()
                Con.Close()

            Catch ex As Exception
                Log.Error("Clickfinder ProgramGuide: [ProgramFoundinTvDb] " & ex.Message)
            End Try



        End Function

        Private Function ChannelFoundInFavGroup(ByVal _idchannel As String) As Boolean
            'MP - TVServer Datenbank Variablen  - MYSql
            Dim Con As New MySqlConnection
            Dim Cmd As New MySqlCommand
            Dim DataCount As Long
            Dim _SQLString As String
            Dim _idGroup As String

            _idGroup = MPSettingRead("config", "ChannelGroupID")
            _SQLString = ("SELECT COUNT(*) FROM groupMap WHERE idChannel = " & _idchannel & " AND idGroup = " & _idGroup)

            Try

                Con.ConnectionString = Left(Replace(Gentle.Framework.GentleSettings.DefaultProviderConnectionString, " ", ""), InStr(Gentle.Framework.GentleSettings.DefaultProviderConnectionString, "charset=utf8") - 3)
                Con.Open()


                Cmd = Con.CreateCommand
                Cmd.CommandText = _SQLString

                DataCount = CLng(Cmd.ExecuteScalar)


                If DataCount > 0 Then
                    ChannelFoundInFavGroup = True
                Else
                    ChannelFoundInFavGroup = False
                End If

                Cmd.Dispose()
                Con.Close()

            Catch ex As Exception
                Log.Error("Clickfinder ProgramGuide: [ChannelFoundInFavGroup] " & ex.Message)
            End Try

        End Function

        Private Function DateTOMySQLstring(ByVal Datum As Date) As String
            DateTOMySQLstring = "'" & Datum.Year & "-" & Format(Datum.Month, "00") & "-" & Format(Datum.Day, "00") & " " & Format(Datum.Hour, "00") & ":" & Format(Datum.Minute, "00") & ":00'"
        End Function
        Private Function DateToAccessSQLstring(ByVal Datum As Date) As String
            DateToAccessSQLstring = "#" & Datum.Year & "-" & Format(Datum.Month, "00") & "-" & Format(Datum.Day, "00") & " " & Format(Datum.Hour, "00") & ":" & Format(Datum.Minute, "00") & ":" & Format(Datum.Millisecond, "00") & "#"
        End Function

        Private Function SQLQueryAccess(ByVal _StartZeit As Date, ByVal _EndZeit As Date, _
                                               Optional ByVal AppendWhereSQLCmd As String = "", _
                                               Optional ByVal OrderBySQLCmd As String = "") As String


            'Umwandeln von Start- & EndZeit in Access Date SQL String

            If Not OrderBySQLCmd = "" Then OrderBySQLCmd = " ORDER BY " & OrderBySQLCmd

            SQLQueryAccess = "Select * from Sendungen Inner Join SendungenDetails on Sendungen.Pos = SendungenDetails.Pos WHERE (Beginn Between " & DateToAccessSQLstring(_StartZeit) & " AND " & DateToAccessSQLstring(_EndZeit) & ") " & AppendWhereSQLCmd & OrderBySQLCmd

        End Function

        Private Sub CreateClickfinderRatingTable()


            Dim _ClickfinderPath As String = MPSettingRead("config", "ClickfinderPath")
            Dim _NewColumn As String = "Rating"
            Dim _BewertungenStrNumber As String
            Dim _Rating As Integer
            Dim _LastUpdate As Date = CDate(MPSettingRead("Save", "LastUpdate"))
            Dim _UpdateInterval As Double = CDbl(MPSettingRead("config", "UpdateInterval"))
            Dim _Counter As Integer = 0
            Dim _StartZeit As Date = Today
            Dim _EndZeit As Date = Today.AddDays(_UpdateInterval)


            If _LastUpdate.AddDays(_UpdateInterval) < Now.Subtract(New System.TimeSpan(4, 5, 0)) Then
                ctlImportProgress.IsVisible = True
                ctlImportProgress.Percentage = 0


                If DoesFieldExist("SendungenDetails", _NewColumn, "Select * FROM SendungenDetails") = False Then

                    Try
                        Dim dbConn As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;" & "Data Source=" & _ClickfinderPath & "\tvdaten.mdb")
                        dbConn.Open()

                        Dim dbCmd As New OleDb.OleDbCommand("ALTER TABLE SendungenDetails ADD COLUMN " & _NewColumn & " int DEFAULT 0", dbConn)
                        dbCmd.ExecuteNonQuery()

                        Log.Info("Clickfinder ProgramGuide: [CreateClickfinderRatingTable]: RatingField in SendungenDetails created")

                        dbCmd.Dispose()
                        dbConn.Close()

                    Catch ex As Exception
                        MsgBox(ex.Message)
                        Log.Error("Clickfinder ProgramGuide: [CreateClickfinderRatingTable]: " & ex.Message)
                    End Try
                End If


                Try
                    Log.Info("Clickfinder ProgramGuide: [CreateClickfinderRatingTable]: Start Calculate & write Ratings")

                    Dim _SQLString As String = SQLQueryAccess(_StartZeit, _EndZeit, "AND Bewertung >= 1 AND Bewertungen LIKE '%Spann%'")

                    Dim Con As New OleDb.OleDbConnection
                    Dim Cmd As New OleDb.OleDbCommand
                    Dim DataCount As Long

                    Con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" & "Data Source=" & _ClickfinderPath & "\tvdaten.mdb"
                    Con.Open()

                    Cmd = Con.CreateCommand
                    Cmd.CommandText = Replace(_SQLString, "Select *", "SELECT Count (*) As Anzahl")
                    DataCount = CLng(Cmd.ExecuteScalar)

                    Cmd.Dispose()
                    Con.Close()




                    ReadClickfinderDB(_SQLString)
                    While ClickfinderData.Read
                        _Counter = _Counter + 1

                        ctlImportProgress.Percentage = _Counter / DataCount * 100

                        _BewertungenStrNumber = Replace(Replace(Replace(Replace(Replace(Replace(Replace(ClickfinderData.Item("Bewertungen"), ";", " "), "Spaß=", ""), "Action=", ""), "Erotik=", ""), "Spannung=", ""), "Gefühl=", ""), "Anspruch=", "")
                        _Rating = 0

                        For i = 1 To _BewertungenStrNumber.Length
                            If IsNumeric(Mid$(_BewertungenStrNumber, i, 1)) Then
                                _Rating = _Rating + CInt(Mid$(_BewertungenStrNumber, i, 1))

                            End If

                        Next

                        'MsgBox(_Rating)
                        'If CInt(ClickfinderData.Item("Bewertung")) >= 1 And CInt(ClickfinderData.Item("Bewertung")) <= 4 Then
                        '    _Rating = _Rating + CInt(ClickfinderData.Item("Bewertung"))
                        'ElseIf ClickfinderData.Item("Bewertung") = 5 Then
                        '    _Rating = _Rating + 0
                        'End If



                        If Not CInt(ClickfinderData.Item("Sendungen.Pos")) = _Rating Then
                            DBWrite("UPDATE SendungenDetails SET Rating = '" & _Rating & "' WHERE Pos = " & ClickfinderData.Item("Sendungen.Pos"))
                        End If

                    End While
                    CloseClickfinderDB()

                    MPSettingsWrite("Save", "LastUpdate", Now)

                    Log.Info("Clickfinder ProgramGuide: [CreateClickfinderRatingTable]: Calculate & write Ratings - success")

                Catch ex As Exception
                    Log.Error("Clickfinder ProgramGuide: [CreateClickfinderRatingTable]: " & ex.Message)
                End Try

                ctlImportProgress.Visible = False
            End If

            ShowTipps()

        End Sub

        Public Sub DBWrite(ByVal SQLString As String)
            Dim _ClickfinderPath As String = MPSettingRead("config", "ClickfinderPath")

            'Connection zur DB herstellen
            Dim dbConn As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;" & "Data Source=" & _ClickfinderPath & "\tvdaten.mdb")
            Dim dbCmd As New OleDb.OleDbCommand


            dbCmd.Connection = dbConn

            Try
                'Reader mit SQL Anfrage füllen
                dbConn.Open()
                dbCmd.CommandText = SQLString
                dbCmd.ExecuteNonQuery()
                dbConn.Close()

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Fehler...")
            End Try
        End Sub

        Public Function DoesFieldExist(ByVal tblName As String, _
                               ByVal fldName As String, _
                               ByVal cnnStr As String) As Boolean

            Try

                Dim _ClickfinderPath As String = MPSettingRead("config", "ClickfinderPath")
                Dim dbConn As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;" & "Data Source=" & _ClickfinderPath & "\tvdaten.mdb")
                dbConn.Open()
                Dim dbTbl As New DataTable

                Dim strSql As String = "Select TOP 1 * from " & tblName
                Dim dbAdapater As New OleDb.OleDbDataAdapter(strSql, dbConn)
                dbAdapater.Fill(dbTbl)

                ' Get the index of the field name
                Dim i As Integer = dbTbl.Columns.IndexOf(fldName)

                If i = -1 Then
                    'Field is missing
                    DoesFieldExist = False

                Else
                    'Field is there
                    DoesFieldExist = True
                End If

                Log.Info("Clickfinder ProgramGuide: [DoesFieldExist]: SendungenDetails - RatingField = " & DoesFieldExist)

                dbTbl.Dispose()
                dbConn.Close()
                dbConn.Dispose()

            Catch ex As Exception
                Log.Error("Clickfinder ProgramGuide: [DoesFieldExist]: " & ex.Message)
            End Try

        End Function

#End Region

#Region "MediaPortal Funktionen / Dialogs"


        'xml Setting Datei lesen
        Public Function MPSettingRead(ByVal section As String, ByVal entry As String) As String
            Using xmlReader As New Settings(Config.GetFile(Config.Dir.Config, "ClickfinderPGConfig.xml"))
                MPSettingRead = xmlReader.GetValue(section, entry)
            End Using
        End Function

        Public Sub MPSettingsWrite(ByVal section As String, ByVal entry As String, ByVal NewAttribute As String)
            Using xmlReader As New Settings(Config.GetFile(Config.Dir.Config, "ClickfinderPGConfig.xml"))
                xmlReader.SetValue(section, entry, NewAttribute)
            End Using

        End Sub
        'MediaPortal Dialoge

        Private Sub AddListControlItem(ByVal SendungID As String, ByVal Label As String, Optional ByVal label2 As String = "", Optional ByVal label3 As String = "", Optional ByVal ImagePath As String = "")

            Dim lItem As New GUIListItem

            lItem.Label = Label
            lItem.Label2 = label2
            lItem.Label3 = label3
            lItem.ItemId = CInt(SendungID)
            lItem.IconImage = ImagePath
            GUIControl.AddListItemControl(GetID, ctlList.GetID, lItem)

        End Sub
        Private Sub MPDialogOK(ByVal Heading As String, ByVal StringLine1 As String, Optional ByVal StringLine2 As String = "", Optional ByVal StringLine3 As String = "")
            Dim dlg As GUIDialogOK = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_OK, Integer)), GUIDialogOK)
            dlg.SetHeading(Heading)
            dlg.SetLine(1, StringLine1)
            dlg.SetLine(2, StringLine2)
            dlg.SetLine(3, StringLine3)
            dlg.DoModal(GUIWindowManager.ActiveWindow)
        End Sub
        Private Sub MPDialogNotify(ByVal Heading As String, ByVal StringLine1 As String)
            Dim dlg As GUIDialogNotify = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_NOTIFY, Integer)), GUIDialogNotify)
            dlg.SetHeading(Heading)
            dlg.SetText(StringLine1)
            dlg.DoModal(GUIWindowManager.ActiveWindow)
        End Sub

        Private Sub LoadTVProgramInfo(ByVal idChannel As Integer, ByVal StartTime As Date, ByVal EndTime As Date, ByVal Titel As String)


            Dim Sendung = New Program(idChannel, StartTime, EndTime, Titel, String.Empty, String.Empty, _
                                Program.ProgramState.None, DateTime.MinValue, String.Empty, String.Empty, String.Empty, String.Empty, _
                                -1, String.Empty, -1)

            If Sendung Is Nothing Then
                Return
            End If

            TvPlugin.TVProgramInfo.CurrentProgram = Sendung
            GUIWindowManager.ActivateWindow(CInt(Window.WINDOW_TV_PROGRAM_INFO))

        End Sub

        Private Sub SetNotify(ByVal idChannel As Integer, ByVal StartTime As Date, ByVal EndTime As Date, ByVal Titel As String)

            Dim Kanal As Channel = Channel.Retrieve(idChannel)

            Dim Erinnerung As Program = Program.RetrieveByTitleTimesAndChannel(Titel, StartTime, EndTime, idChannel)
            Erinnerung.Notify = True
            Erinnerung.Persist()
            TvNotifyManager.OnNotifiesChanged()

            MPDialogOK("Erinnerung:", Titel, StartTime & " - " & EndTime, Kanal.DisplayName)


        End Sub
#End Region


        Public Shared Function ListByNameStartsWith(ByVal DisplayName As String) As IList


            Dim sb As SqlBuilder = New SqlBuilder(StatementType.Select, GetType(Channel))
            ' note: the partialName parameter must also contain the %'s for the LIKE query!
            sb.AddConstraint([Operator].Like, "displayName", DisplayName)
            ' passing true indicates that we'd like a list of elements, i.e. that no primary key
            ' constraints from the type being retrieved should be added to the statement
            Dim stmt As SqlStatement = sb.GetStatement(StatementType.Select)
            ' execute the statement/query and create a collection of User instances from the result
            Return ObjectFactory.GetCollection(GetType(Channel), stmt.Execute)
        End Function







        'Public Sub test()
        '    Dim idGroup As String

        '    Try

        '        Dim SQL As SqlBuilder

        '        SQL.SetStatementType(StatementType.Select)
        '        SQL.SetTable(GetType(GroupMap))




        '    Catch ex As Exception
        '        MsgBox(ex.Message)
        '    End Try

        'End Sub

        'Public Shared Function ListByNameStartsWith(ByVal partialName As String) As IList

        '    Try

        '        Dim sb As SqlBuilder = New SqlBuilder(StatementType.Select, GetType(GroupMap))
        '        ' note: the partialName parameter must also contain the %'s for the LIKE query!
        '        sb.AddConstraint([Operator].Like, "idGroup", partialName)
        '        ' passing true indicates that we'd like a list of elements, i.e. that no primary key
        '        ' constraints from the type being retrieved should be added to the statement
        '        Dim stmt As SqlStatement = sb.GetStatement(StatementType.Select)
        '        ' execute the statement/query and create a collection of User instances from the result

        '        ListByNameStartsWith = (ObjectFactory.GetCollection(GetType(GroupMap), stmt.Execute))

        '    Catch ex As Exception
        '        MsgBox(ex.Message)
        '    End Try
        'End Function


    End Class
End Namespace


