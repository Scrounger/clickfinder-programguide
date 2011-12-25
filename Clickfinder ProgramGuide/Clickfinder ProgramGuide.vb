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
Imports MediaPortal.Player

Imports Gentle.Common
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

#Region "Skin Controls"

        <SkinControlAttribute(2)> Protected btnNow As GUIButtonControl = Nothing
        <SkinControlAttribute(3)> Protected btnPrimeTime As GUIButtonControl = Nothing
        <SkinControlAttribute(4)> Protected btnLateTime As GUIButtonControl = Nothing
        <SkinControlAttribute(5)> Protected btnPreview As GUIButtonControl = Nothing


        <SkinControlAttribute(9)> Protected ctlProgressBar As GUIAnimation = Nothing
        <SkinControlAttribute(10)> Protected ctlList As GUIListControl = Nothing
        <SkinControlAttribute(11)> Protected ctlImportProgress As GUIProgressControl = Nothing


        <SkinControlAttribute(20)> Protected AnsichtImage As GUIImage = Nothing
        <SkinControlAttribute(21)> Protected SelectedCategorieLabel As GUIFadeLabel = Nothing

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
        <SkinControlAttribute(44)> Protected DetailsRatingStars As GUIImageList = Nothing

        <SkinControlAttribute(86)> Protected btnTheTvDb As GUIButtonControl = Nothing
        <SkinControlAttribute(87)> Protected btnRemember As GUIButtonControl = Nothing
        <SkinControlAttribute(88)> Protected btnRecord As GUIButtonControl = Nothing
        <SkinControlAttribute(89)> Protected btnViewChannel As GUIButtonControl = Nothing
        <SkinControlAttribute(90)> Protected btnBack As GUIButtonControl = Nothing

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
        <SkinControlAttribute(118)> Protected FavRatingStars0 As GUIImageList = Nothing


        <SkinControlAttribute(120)> Protected FavTitel1 As GUIFadeLabel = Nothing
        <SkinControlAttribute(121)> Protected FavImage1 As GUIImage = Nothing
        <SkinControlAttribute(122)> Protected FavKanal1 As GUIFadeLabel = Nothing
        <SkinControlAttribute(123)> Protected FavGenre1 As GUIFadeLabel = Nothing
        <SkinControlAttribute(124)> Protected FavZeit1 As GUILabelControl = Nothing
        <SkinControlAttribute(125)> Protected FavBewertung1 As GUIFadeLabel = Nothing
        <SkinControlAttribute(126)> Protected FavKritik1 As GUIFadeLabel = Nothing
        <SkinControlAttribute(127)> Protected FavRatingImage1 As GUIImage = Nothing
        <SkinControlAttribute(128)> Protected FavRatingStars1 As GUIImageList = Nothing

        <SkinControlAttribute(130)> Protected FavTitel2 As GUIFadeLabel = Nothing
        <SkinControlAttribute(131)> Protected FavImage2 As GUIImage = Nothing
        <SkinControlAttribute(132)> Protected FavKanal2 As GUIFadeLabel = Nothing
        <SkinControlAttribute(133)> Protected FavGenre2 As GUIFadeLabel = Nothing
        <SkinControlAttribute(134)> Protected FavZeit2 As GUILabelControl = Nothing
        <SkinControlAttribute(135)> Protected FavBewertung2 As GUIFadeLabel = Nothing
        <SkinControlAttribute(136)> Protected FavKritik2 As GUIFadeLabel = Nothing
        <SkinControlAttribute(137)> Protected FavRatingImage2 As GUIImage = Nothing
        <SkinControlAttribute(138)> Protected FavRatingStars2 As GUIImageList = Nothing


        <SkinControlAttribute(140)> Protected FavTitel3 As GUIFadeLabel = Nothing
        <SkinControlAttribute(141)> Protected FavImage3 As GUIImage = Nothing
        <SkinControlAttribute(142)> Protected FavKanal3 As GUIFadeLabel = Nothing
        <SkinControlAttribute(143)> Protected FavGenre3 As GUIFadeLabel = Nothing
        <SkinControlAttribute(144)> Protected FavZeit3 As GUILabelControl = Nothing
        <SkinControlAttribute(145)> Protected FavBewertung3 As GUIFadeLabel = Nothing
        <SkinControlAttribute(146)> Protected FavKritik3 As GUIFadeLabel = Nothing
        <SkinControlAttribute(147)> Protected FavRatingImage3 As GUIImage = Nothing
        <SkinControlAttribute(148)> Protected FavRatingStars3 As GUIImageList = Nothing

        <SkinControlAttribute(150)> Protected FavTitel4 As GUIFadeLabel = Nothing
        <SkinControlAttribute(151)> Protected FavImage4 As GUIImage = Nothing
        <SkinControlAttribute(152)> Protected FavKanal4 As GUIFadeLabel = Nothing
        <SkinControlAttribute(153)> Protected FavGenre4 As GUIFadeLabel = Nothing
        <SkinControlAttribute(154)> Protected FavZeit4 As GUILabelControl = Nothing
        <SkinControlAttribute(155)> Protected FavBewertung4 As GUIFadeLabel = Nothing
        <SkinControlAttribute(156)> Protected FavKritik4 As GUIFadeLabel = Nothing
        <SkinControlAttribute(157)> Protected FavRatingImage4 As GUIImage = Nothing
        <SkinControlAttribute(158)> Protected FavRatingStars4 As GUIImageList = Nothing



#End Region

#Region "Variablen"
        Public _ShowSQLString As String
        Public _CurrentCategorie As String
        Public _CurrentQuery As String
        Public _CurrentDetailsSendungId As String
        Public _CurrentDetailsSendungChannelName As String
        Public _CurrentDetailsImageIsPath As Boolean

        Public _ListStandardOffSetY As Integer
        Public _ListOffSetY As Integer

        Private _TippButtonFocus As Boolean
        Private _TippButtonFocusID As Integer
        

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
        Private _GuiImageList As Dictionary(Of Integer, GUIImageList) = New Dictionary(Of Integer, GUIImageList)
        Const _idStartCounter As Integer = 110
        Const _idStoppCounter As Integer = 160
        Private _TippClickfinderSendungID0 As Long
        Private _TippClickfinderSendungID1 As Long
        Private _TippClickfinderSendungID2 As Long
        Private _TippClickfinderSendungID3 As Long
        Private _TippClickfinderSendungID4 As Long
        Private _TippClickfinderSendungID As Dictionary(Of Integer, Long) = New Dictionary(Of Integer, Long)
        Private _TippClickfinderSendungChannelName0 As String
        Private _TippClickfinderSendungChannelName1 As String
        Private _TippClickfinderSendungChannelName2 As String
        Private _TippClickfinderSendungChannelName3 As String
        Private _TippClickfinderSendungChannelName4 As String
        Private _TippClickfinderSendungChannelName As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)
        'Private MyTVDB As clsTheTVdb = New clsTheTVdb("de")


        Private _TippClickfinderSendungTitel0 As Long
        Private _TippClickfinderSendungTitel1 As Long
        Private _TippClickfinderSendungTitel2 As Long
        Private _TippClickfinderSendungTitel3 As Long
        Private _TippClickfinderSendungTitel4 As Long
        Private _TippClickfinderSendungTitel As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)
#End Region

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

            Log.Debug("")
            Log.Debug("")

            If _ListOffSetY = 0 Then
                _ListStandardOffSetY = ctlList.TextOffsetY
                _ListOffSetY = ctlList.TextOffsetY + CInt(MPSettingRead("config", "ListLabelOffsetY"))
            End If

            Dictonary()

            ctlProgressBar.Visibility = Windows.Visibility.Hidden
            DetailsImage.Visibility = Windows.Visibility.Hidden

            btnNow.IsFocused = True
            btnNow.IsFocused = False
            btnPrimeTime.IsFocused = False
            btnLateTime.IsFocused = False
            btnPreview.IsFocused = False


            If _ShowSQLString = "" Then
                'Screen wird das erste Mal geladen, kein SQL String existiert -> Tages Ansicht Now
                Log.Debug("")
                Log.Debug("Clickfinder ProgramGuide: [OnPageLoad]: Load First Time")
                Log.Debug("Clickfinder ProgramGuide: [OnPageLoad]: Ansicht Now - Categorie")

                ctlList.IsFocused = True
                Button_Now()

                'Screen wird erneut geladen, germkte Ansichten zeigen   
            ElseIf _CurrentCategorie = "" Then
                'Keine Kategorie gespeichert
                Log.Debug("")
                Log.Debug("Clickfinder ProgramGuide: [OnPageLoad]: Load again")

                If Not _CurrentDetailsSendungId = Nothing Then
                    'SendungId vorhanden -> Ansicht Item Details anzeigen
                    Log.Debug("Clickfinder ProgramGuide: [OnPageLoad]: Detail Ansicht " & _CurrentQuery & ", " & _CurrentDetailsSendungId)


                    ShowCategories()
                    System.Threading.Thread.Sleep(1000)
                    ShowItemDetails(_CurrentDetailsSendungId, _CurrentDetailsSendungChannelName, _CurrentDetailsImageIsPath)



                    ctlList.IsFocused = False
                    btnBack.IsFocused = True
                

                Else
                    'SendungId nicht vorhanden -> Ansicht Kategorie anzeigen
                    Log.Debug("Clickfinder ProgramGuide: [OnPageLoad]: Ansicht " & _CurrentQuery)
                    ShowCategories()
                    ctlList.IsFocused = True
                End If

            Else
                'Kategorie vorhanden -> SelctedKategorie Items Ansicht
                Log.Debug("")
                Log.Debug("Clickfinder ProgramGuide: [OnPageLoad]: Load again")
                If Not _CurrentDetailsSendungId = Nothing Then
                    Log.Debug("Clickfinder ProgramGuide: [OnPageLoad]: SelectedCategorieItem Detail Ansicht " & _CurrentQuery & " - " & _CurrentCategorie & ", " & _CurrentDetailsSendungId)
                Else
                    Log.Debug("Clickfinder ProgramGuide: [OnPageLoad]: SelectedCategorieItem Ansicht " & _CurrentQuery & " - " & _CurrentCategorie)
                End If

                Dim _ProgressBar As New Thread(AddressOf ShowProgressbar)
                Dim _Threat As New Thread(AddressOf ShowSelectedCategorieItems)
                ShowTipps()
                _ProgressBar.Start()
                _Threat.Start()

                ctlList.IsFocused = True

                'Wenn Sendung ID vorhanden, dann DetailsView anzeigen
                If Not _CurrentDetailsSendungId = Nothing Then
                    _Threat.Join()
                    ShowItemDetails(_CurrentDetailsSendungId, _CurrentDetailsSendungChannelName, _CurrentDetailsImageIsPath)                    
                End If
            End If

            Log.Debug("")
            SelectedCategorieLabel.Label = _CurrentCategorie
            AnsichtImage.FileName = Config.GetFile(Config.Dir.Thumbs, "ClickfinderPG\Categories\") & _CurrentQuery & ".png"

        End Sub
        'Public Overrides Sub OnAction(ByVal action As MediaPortal.GUI.Library.Action)
        '    MyBase.OnAction(action)

        '    If GUIWindowManager.ActiveWindow = GetID Then
        '        If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_SELECT_ITEM _
        '        Or action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MOVE_DOWN _
        '        Or action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MOVE_UP _
        '        Or action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_PAGE_UP _
        '        Or action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_PAGE_DOWN _
        '        Or action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_HOME _
        '        Or action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_END Then

        '            If ctlList.IsFocused Then

        '            End If
        '        End If


        '    End If

        'End Sub
        Protected Overrides Sub OnPageDestroy(ByVal new_windowId As Integer)
            MyBase.OnPageDestroy(new_windowId)
            _GuiImage.Clear()
            _GuiButton.Clear()
            _GuiImageList.Clear()
            _TippClickfinderSendungID.Clear()
            _TippClickfinderSendungTitel.Clear()
            _TippClickfinderSendungChannelName.Clear()
            'MyTVDB.TheTVdbHandler.ClearCache()
            'MyTVDB.TheTVdbHandler.CloseCache()

            ctlList.SetTextOffsets(ctlList.TextOffsetX, _ListStandardOffSetY, ctlList.TextOffsetX2, ctlList.TextOffsetY2, ctlList.TextOffsetX3, ctlList.TextOffsetY3)
            _ListStandardOffSetY = Nothing
            _ListOffSetY = Nothing

            Directory.Delete(Config.GetFolder(Config.Dir.Cache) & "\ClickfinderPG", True)

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


            If control Is btnBack Then

                DetailsRatingStars.Visible = False

                If _TippButtonFocus = True Then
                    DetailsImage.Visible = False
                    btnBack.IsFocused = False
                    _GuiButton(_TippButtonFocusID).IsFocused = True
                    _TippButtonFocus = False
                Else
                    DetailsImage.Visible = False
                    btnBack.IsFocused = False
                    ctlList.IsFocused = True
                    'btnLateTime.IsFocused = False
                End If

                RatingStarsVisble()

                _CurrentDetailsSendungId = Nothing
                _CurrentDetailsSendungChannelName = Nothing

            End If

            If control Is btnViewChannel Then
                Button_ViewChannel()

            End If

            If control Is btnRecord Then

                Button_Record()

            End If

            If control Is btnRemember Then

                Button_Remember()

            End If

            If control Is btnTheTvDb Then



            End If

            If control Is btnTipp0 Then
                Log.Debug("")
                Log.Debug("Clickfinder ProgramGuide: [btnTipp0] Call ShowItemDetails: " & FavKanal0.Label & " - " & _TippClickfinderSendungID(110))
                Log.Debug("")
                _TippButtonFocusID = btnTipp0.GetID
                _TippButtonFocus = True
                ShowItemDetails(_TippClickfinderSendungID(110), _TippClickfinderSendungChannelName(110))
            End If

            If control Is btnTipp1 Then
                Log.Debug("")
                Log.Debug("Clickfinder ProgramGuide: [btnTipp1] Call ShowItemDetails: " & FavKanal1.Label & " - " & _TippClickfinderSendungID(120))
                Log.Debug("")
                _TippButtonFocusID = btnTipp1.GetID
                _TippButtonFocus = True
                ShowItemDetails(_TippClickfinderSendungID(120), _TippClickfinderSendungChannelName(120))
            End If

            If control Is btnTipp2 Then
                Log.Debug("")
                Log.Debug("Clickfinder ProgramGuide: [btnTipp2] Call ShowItemDetails: " & FavKanal2.Label & " - " & _TippClickfinderSendungID(130))
                Log.Debug("")
                _TippButtonFocusID = btnTipp2.GetID
                _TippButtonFocus = True
                ShowItemDetails(_TippClickfinderSendungID(130), _TippClickfinderSendungChannelName(130))
            End If

            If control Is btnTipp3 Then
                Log.Debug("")
                Log.Debug("Clickfinder ProgramGuide: [btnTipp3] Call ShowItemDetails: " & FavKanal3.Label & " - " & _TippClickfinderSendungID(140))
                Log.Debug("")
                _TippButtonFocusID = btnTipp3.GetID
                _TippButtonFocus = True
                ShowItemDetails(_TippClickfinderSendungID(140), _TippClickfinderSendungChannelName(140))
            End If

            If control Is btnTipp4 Then
                Log.Debug("")
                Log.Debug("Clickfinder ProgramGuide: [btnTipp4] Call ShowItemDetails: " & FavKanal4.Label & " - " & _TippClickfinderSendungID(150))
                Log.Debug("")
                _TippButtonFocusID = btnTipp4.GetID
                _TippButtonFocus = True
                ShowItemDetails(_TippClickfinderSendungID(150), _TippClickfinderSendungChannelName(150))
            End If

        End Sub
#End Region

#Region "Click Events"

        Private Sub Button_Now()
            Log.Debug("")
            Log.Debug("Clickfinder ProgramGuide: [Button_Now]: Clicked")
            Log.Debug("")

            Dim t As DateTime = DateTime.Now.Subtract(New System.TimeSpan(0, _SettingDelayNow, 0))

            _ZeitQueryStart = Today.AddHours(t.Hour).AddMinutes(t.Minute)
            _ZeitQueryEnde = _ZeitQueryStart.AddHours(4)
            _CurrentQuery = "Now"

            ShowCategories()


        End Sub

        Private Sub Button_PrimeTime()
            Log.Debug("")
            Log.Debug("Clickfinder ProgramGuide: [Button_PrimeTime]: Clicked")
            Log.Debug("")
            _ZeitQueryStart = Today.AddHours(_SettingPrimeTimeHour).AddMinutes(_SettingPrimeTimeMinute)
            _ZeitQueryEnde = _ZeitQueryStart.AddHours(4)
            _CurrentQuery = "PrimeTime"

            ShowCategories()
        End Sub
        Private Sub Button_LateTime()
            Log.Debug("")
            Log.Debug("Clickfinder ProgramGuide: [Button_LateTime]: Clicked")
            Log.Debug("")
            _ZeitQueryStart = Today.AddHours(_SettingLateTimeHour).AddMinutes(_SettingLateTimeMinute)
            _ZeitQueryEnde = _ZeitQueryStart.AddHours(4)
            _CurrentQuery = "LateTime"

            ShowCategories()
        End Sub

        Private Sub Button_Preview()
            Log.Debug("")
            Log.Debug("Clickfinder ProgramGuide: [Button_Preview]: Clicked")
            Log.Debug("")

            _ZeitQueryStart = Today
            _ZeitQueryEnde = _ZeitQueryStart.AddDays(20)
            _CurrentQuery = "Preview"

            ShowCategories()
        End Sub

        Private Sub Button_ViewChannel()
            Dim _Channel As Channel
            ReadTvServerDB("SELECT * FROM channel WHERE displayName = '" & DetailsKanal.Label & "'")

            While TvServerData.Read
                _Channel = Channel.Retrieve(CInt(TvServerData("idChannel")))
                Exit While
            End While
            CloseTvServerDB()

            Dim changeChannel As Channel = DirectCast(_Channel, Channel)
            MediaPortal.GUI.Library.GUIWindowManager.ActivateWindow(CInt(MediaPortal.GUI.Library.GUIWindow.Window.WINDOW_TVFULLSCREEN))
            TVHome.ViewChannelAndCheck(changeChannel)
        End Sub
        Private Sub Button_Record()

            Try
                Log.Debug("")
                Log.Debug("Clickfinder ProgramGuide: [Button_Record]: Clicked")
                Log.Debug("")

                DetailsImage.Visible = True
                ctlList.IsFocused = False
                btnBack.IsFocused = True

                Dim _ClickfinderDB As New ClickfinderDB("SELECT * FROM Sendungen WHERE SendungID = '" & _CurrentDetailsSendungId & "'")
                For i As Integer = 0 To _ClickfinderDB.Count - 1

                    Dim _idChannel As Integer

                    ReadTvServerDB("SELECT * FROM channel WHERE displayName = '" & DetailsKanal.Label & "'")
                    While TvServerData.Read
                        _idChannel = CInt(TvServerData.Item("idChannel"))
                    End While
                    CloseTvServerDB()



                    Log.Debug("Clickfinder ProgramGuide: [Button_Record]: LoadTVProgramInfo(" & _idChannel & "," & _ClickfinderDB(i).Beginn & "," & _ClickfinderDB(i).Ende & "," & _ClickfinderDB(i).Titel & ")")
                    LoadTVProgramInfo(_idChannel, _ClickfinderDB(i).Beginn, _ClickfinderDB(i).Ende, _ClickfinderDB(i).Titel)

                    Exit For
                Next

                btnRecord.IsFocused = False
                btnBack.IsFocused = True

            Catch ex As Exception
                Log.Error("Clickfinder ProgramGuide: [Button_Record]: " & ex.Message)
            End Try

        End Sub
        Private Sub Button_Remember()
            Try
                Log.Debug("")
                Log.Debug("Clickfinder ProgramGuide: [Button_Record]: Clicked")
                Log.Debug("")

                DetailsImage.Visible = True
                ctlList.IsFocused = False
                btnBack.IsFocused = True

                Dim _ClickfinderDB As New ClickfinderDB("SELECT * FROM Sendungen WHERE SendungID = '" & _CurrentDetailsSendungId & "'")
                For i As Integer = 0 To _ClickfinderDB.Count - 1

                    Dim _idChannel As Integer

                    ReadTvServerDB("SELECT * FROM channel WHERE displayName = '" & DetailsKanal.Label & "'")
                    While TvServerData.Read
                        _idChannel = CInt(TvServerData.Item("idChannel"))
                    End While
                    CloseTvServerDB()

                    Log.Debug("Clickfinder ProgramGuide: [Button_Remember]: SetNotify(" & _idChannel & "," & _ClickfinderDB(i).Beginn & "," & _ClickfinderDB(i).Ende & "," & _ClickfinderDB(i).Titel & ")")
                    SetNotify(_idChannel, _ClickfinderDB(i).Beginn, _ClickfinderDB(i).Ende, _ClickfinderDB(i).Titel)

                    Exit For
                Next

                btnRemember.IsFocused = False
                btnBack.IsFocused = True

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
            Dim _Categorie As String = Nothing
            Dim _Log As String = Nothing

            'Dictionary mit allen Kategorien füllen
            Dim _AvailableCategories As Dictionary(Of String, String) = New Dictionary(Of String, String)
            _AvailableCategories.Clear()

            'AvailableTagesCategories Array durchlaufen und Kategorie an Dictionary übergeben
            Dim str_AvailableTagesCategories() As String = MPSettingRead("config", "AvailableTagesCategories").ToString.Split(CChar(";"))
            For i = 0 To str_AvailableTagesCategories.Length - 1
                If Not str_AvailableTagesCategories(i) = "" Then
                    _AvailableCategories.Add(str_AvailableTagesCategories(i), str_AvailableTagesCategories(i))
                    _Log = _Log & str_AvailableTagesCategories(i) & "; "
                End If
            Next

            'AvailableVorschauCategories Array durchlaufen und Kategorie an Dictionary übergebe, sofern nicht schon vorhanden
            Dim str_AvailableVorschauCategories() As String = MPSettingRead("config", "AvailableVorschauCategories").ToString.Split(CChar(";"))
            For i = 0 To str_AvailableVorschauCategories.Length - 1
                If _AvailableCategories.ContainsValue(str_AvailableVorschauCategories(i)) = False Then
                    _AvailableCategories.Add(str_AvailableVorschauCategories(i), str_AvailableVorschauCategories(i))
                    '_Log = _Log & str_AvailableVorschauCategories(i) & "; "
                End If
            Next

            ctlList.SetTextOffsets(ctlList.TextOffsetX, _ListStandardOffSetY, ctlList.TextOffsetX2, ctlList.TextOffsetY2, ctlList.TextOffsetX3, ctlList.TextOffsetY3)

            _Rating = MPSettingRead("config", "ClickfinderRating")
            _RespectInFavGroup = True

            'Sofern Categorie vorhanden im Dicitionary -> Categorie Items anzeigen
            If _AvailableCategories.ContainsValue(ctlList.SelectedListItem.Label.ToString) = True Then
                _Categorie = ctlList.SelectedListItem.Label.ToString
            End If


            'Aufruf der Fkt. beim click auf ein ListConrtol Item
            Select Case ctlList.SelectedListItem.Label.ToString
                Case ""
                    'Listcontrol Item = .. -> eine Ebende zurück in der Listcontrol
                    Log.Debug("")
                    Log.Debug("Clickfinder ProgramGuide: [ListControlClick] Call ShowCategories")
                    Log.Debug("")
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


                Case Is = _Categorie
                    'Listcontrol Item = Categorie: Categorie Items anzeigen
                    Log.Debug("")
                    Log.Debug("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ctlList.SelectedListItem.Label.ToString & " - " & _CurrentQuery.ToString)
                    Log.Debug("")
                    Try
                        _CurrentCategorie = _AvailableCategories.Item(ctlList.SelectedListItem.Label.ToString)
                        SelectedCategorieLabel.Label = _CurrentCategorie
                        ctlList.Clear()

                        _SQLWhereAdd = Replace(MPSettingRead(_CurrentCategorie, "Where"), "Bewertung >= #Rating#", "Bewertung >= " & _Rating)
                        _SQLWhereAddPreview = Replace(MPSettingRead(_CurrentCategorie, "PreviewWhere"), "Bewertung >= #Rating#", "Bewertung >= " & _Rating)
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


                Case Else
                    'Listcontrol Item = Sendung: Details der Sendung anzeigen
                    Log.Debug("")
                    Log.Debug("Clickfinder ProgramGuide: [ListControlClick] Call ShowItemDetails: " & ctlList.SelectedListItem.Label & " " & ctlList.SelectedListItem.Label3 & " " & ctlList.SelectedListItem.Label2 & " " & ctlList.SelectedListItem.Icon.FileName & " - true")
                    Log.Debug("")
                    ShowItemDetails(ctlList.SelectedListItem.ItemId, ctlList.SelectedListItem.Icon.FileName, True)
            End Select

        End Sub

#End Region

#Region "Show Events"

        Private Sub ShowCategories()

            Dim _ProgressBar As New Thread(AddressOf ShowProgressbar)

            Try
                Log.Debug("Clickfinder ProgramGuide: [ShowCategories]: _CurrentQuery" & _CurrentQuery)
                Log.Debug("")

                _CurrentCategorie = ""
                _RespectInFavGroup = False
                ctlList.ListItems.Clear()

                ctlList.SetTextOffsets(ctlList.TextOffsetX, _ListOffSetY, ctlList.TextOffsetX2, ctlList.TextOffsetY2, ctlList.TextOffsetX3, ctlList.TextOffsetY3)

                If _CurrentQuery = "Preview" Then
                    _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, "AND Bewertung >= 4 AND KzFilm = true", "Beginn ASC, SendungenDetails.Rating DESC, Bewertung DESC")

                    'Gewählte Kategorien aus ClickfinderPGConfig.xml lesen und in Array packen
                    Dim str_VisiblePreviewCategories() As String = MPSettingRead("config", "VisibleVorschauCategories").ToString.Split(CChar(";"))
                    'Array durchlaufen und Kategorie an ListControlübergeben
                    For i = 0 To str_VisiblePreviewCategories.Length - 1
                        If Not str_VisiblePreviewCategories(i) = "" Then
                            Log.Debug("Clickfinder ProgramGuide: [ShowCategories]: Add PreviewCategorie: " & str_VisiblePreviewCategories(i))
                            AddListControlItem(ctlList.ListItems.Count - 1, str_VisiblePreviewCategories(i), , , Config.GetFile(Config.Dir.Thumbs, "ClickfinderPG\Categories\") & str_VisiblePreviewCategories(i) & ".png")
                            Log.Debug("")
                        End If
                    Next

                Else
                    _ShowSQLString = SQLQueryAccess(_ZeitQueryStart, _ZeitQueryEnde, "AND KzFilm = true", "Beginn ASC, SendungenDetails.Rating DESC")

                    'Gewählte Kategorien aus ClickfinderPGConfig.xml lesen und in Array packen
                    Dim str_VisibleTagesCategories() As String = MPSettingRead("config", "VisibleTagesCategories").ToString.Split(CChar(";"))
                    'Array durchlaufen und Kategorie an ListControlübergeben
                    For i = 0 To str_VisibleTagesCategories.Length - 1
                        If Not str_VisibleTagesCategories(i) = "" Then
                            Log.Debug("Clickfinder ProgramGuide: [ShowCategories]: Add Categorie: " & str_VisibleTagesCategories(i))
                            AddListControlItem(ctlList.ListItems.Count - 1, str_VisibleTagesCategories(i), , , Config.GetFile(Config.Dir.Thumbs, "ClickfinderPG\Categories\") & str_VisibleTagesCategories(i) & ".png")
                            Log.Debug("")
                        End If
                    Next

                End If

                'Clickfinder Rating prüfen ob vorhanden und anschließend Tipps anzeigen
                Dim _Threat2 As New Thread(AddressOf CreateClickfinderRatingTable)
                _Threat2.Start()

                SelectedCategorieLabel.Label = _CurrentCategorie
                AnsichtImage.FileName = Config.GetFile(Config.Dir.Thumbs, "ClickfinderPG\Categories\") & _CurrentQuery & ".png"

                ctlList.Focus = True
                btnNow.Focus = False
                btnPrimeTime.Focus = False
                btnLateTime.Focus = False
                btnPreview.Focus = False

            Catch ex As Exception
                Log.Error("Clickfinder ProgramGuide: [ShowCategories]: " & ex.Message)
            End Try

        End Sub

        Private Sub ShowSelectedCategorieItems()

            Try
                Log.Debug("")
                Log.Debug("Clickfinder ProgramGuide: [ShowSelectedCategorieItems] Categorie: " & _CurrentCategorie)
                Log.Debug("")

                Dim _idGroup As String = MPSettingRead("config", "ChannelGroupID")
                Dim _ClickfinderPath As String = MPSettingRead("config", "ClickfinderPath")
                Dim _SettingMinTime As Integer = CInt(MPSettingRead("config", "MinTime"))
                Dim _lastTitel As String = Nothing
                Dim _lastorgTitel As String = Nothing
                Dim _FavCounter As Integer = 0

                _RespectInFavGroup = True

                If _CurrentCategorie = "Serien" And MPSettingRead("config", "IgnoreMinTimeSeries") = "true" _
                Then _SettingMinTime = 0

                ctlList.ListItems.Clear()
                ctlList.Clear()

                'Zurück ListItem hinzufügen
                AddListControlItem(ctlList.ListItems.Count - 1, "", , , "defaultFolderBack.png")

                'Clickfinder Datenbank öffnen & Daten einlesen
                Dim _ClickfinderDB As New ClickfinderDB(_ShowSQLString)
                For i As Integer = 0 To _ClickfinderDB.Count - 1

                    If _ClickfinderDB(i).Dauer >= _SettingMinTime Then

                        'Tv Server öffnen und TVMovieMapping idChannel ermitteln
                        ReadTvServerDB("Select * from tvmoviemapping Inner Join channel on tvmoviemapping.idChannel = channel.idChannel where stationName = '" & _ClickfinderDB(i).SenderKennung & "'")

                        While TvServerData.Read
                            Dim _idChannel As String = TvServerData.Item("idChannel")
                            Dim _ChannelName As String = TvServerData.Item("displayName")

                            'Prüfen ob Program in der TV Server Program DB ist
                            If ProgramFoundinTvDb(_ClickfinderDB(i).Titel, _idChannel, _ClickfinderDB(i).Beginn, _ClickfinderDB(i).Ende) = True Then
                                Dim Sendung As Program = TvDatabase.Program.RetrieveByTitleTimesAndChannel(_ClickfinderDB(i).Titel, _ClickfinderDB(i).Beginn, _ClickfinderDB(i).Ende, _idChannel)

                                Dim _SeriesNum As String = Sendung.SeriesNum
                                Dim _EpisodeNum As String = Sendung.EpisodeNum

                                Select Case _ClickfinderDB(i).Titel & _ClickfinderDB(i).Originaltitel
                                    Case Is = _lastTitel
                                        Exit Select
                                    Case Else

                                        For Each item In _TippClickfinderSendungTitel
                                            If _ClickfinderDB(i).Titel & _ClickfinderDB(i).Originaltitel = _TippClickfinderSendungTitel.Item(item.Key) Then
                                                MsgBox(_TippClickfinderSendungTitel.Item(item.Key))
                                                Exit Select
                                            End If
                                        Next

                                        Dim _TvLogo As String
                                        If _useRatingTvLogos = "true" Then
                                            _TvLogo = Config.GetFile(Config.Dir.Thumbs, "ClickfinderPG\tv\logos\") & Channel.Retrieve(_idChannel).DisplayName & "_" & _ClickfinderDB(i).Bewertung & ".png"
                                        Else
                                            _TvLogo = Config.GetFile(Config.Dir.Thumbs, "tv\logos\" & Channel.Retrieve(_idChannel).DisplayName & ".png")
                                        End If

                                        'ListItemInfoLabel formatieren
                                        Dim _ListItemInfoLabel As String
                                        Select Case _CurrentCategorie
                                            Case Is = "Serien"
                                                _ListItemInfoLabel = _ClickfinderDB(i).Originaltitel
                                                If Not _SeriesNum = "" Or Not _EpisodeNum = "" Then
                                                    _ListItemInfoLabel = "Folge: " & _ClickfinderDB(i).Originaltitel _
                                                    & vbNewLine & "Staffel " & _SeriesNum _
                                                     & ", Episode " & _EpisodeNum
                                                Else
                                                    _ListItemInfoLabel = _ClickfinderDB(i).Originaltitel _
                                                    & vbNewLine & _ClickfinderDB(i).Genre
                                                End If

                                            Case Is = "Fußball LIVE"
                                                _ListItemInfoLabel = _ClickfinderDB(i).Originaltitel

                                            Case Else
                                                If _ClickfinderDB(i).Rating > 0 Then
                                                    _ListItemInfoLabel = _ClickfinderDB(i).Genre _
                                                                & vbNewLine & "Bewertung: " & CStr(_ClickfinderDB(i).Rating) _
                                                                & vbNewLine & _ClickfinderDB(i).Kurzkritik
                                                ElseIf Not _ClickfinderDB(i).Originaltitel = "" Then
                                                    _ListItemInfoLabel = _ClickfinderDB(i).Originaltitel _
                                                                        & vbNewLine & _ClickfinderDB(i).Genre
                                                Else
                                                    _ListItemInfoLabel = _ClickfinderDB(i).Genre
                                                End If
                                        End Select

                                        AddListControlItem(_ClickfinderDB(i).SendungID, Sendung.Title.ToString, _
                                                           FormatTimeLabel(_ClickfinderDB(i).Beginn, _ClickfinderDB(i).Ende), _
                                                            _ListItemInfoLabel, _
                                                            _TvLogo)

                                End Select

                                _lastTitel = _ClickfinderDB(i).Titel & _ClickfinderDB(i).Originaltitel
                            End If
                        End While

                        CloseTvServerDB()
                    End If

                Next

                RatingStarsVisble()
                ctlProgressBar.Visible = False
                ctlList.Focus = True

            Catch ex As Exception
                Log.Error("Clickfinder ProgramGuide: [ShowSelectedCategorieItems]: " & ex.Message)
            End Try

        End Sub
        Private Sub ShowTipps()

            Dim _ProgressBar As New Thread(AddressOf ShowProgressbar)
            _ProgressBar.Start()

            ClearTipps()

            'Einstellungen aus ClickfinderPGConfig.xml laden & Variablen übergeben / definieren
            Dim _idGroup As String = MPSettingRead("config", "ChannelGroupID")

            Dim _lastTitel As String = Nothing
            Dim _TippsCounter As Integer = _idStartCounter
            Dim _SettingMinTime As Integer = CInt(MPSettingRead("config", "MinTime"))

            Log.Debug("Clickfinder ProgramGuide: [ShowTipps]: Query: " & _CurrentQuery & ", Categorie:" & _CurrentCategorie)

            If _CurrentCategorie = "Serien" And _SettingIgnoreMinTimeSeries = "true" _
                    Then _SettingMinTime = 0

            Try
                'Clickfinder Datenbank öffnen & Daten einlesen
                Dim _ClickfinderDB As New ClickfinderDB(_ShowSQLString)
                For i As Integer = 0 To _ClickfinderDB.Count - 1

                    If _ClickfinderDB(i).Dauer >= _SettingMinTime Then
                        'Daten aus Clickfinder DB lesen und übergeben
                        Dim _BildDatei As String = _ClickfinderDB(i).Bilddateiname



                        If _ClickfinderDB(i).KzBilddateiHeruntergeladen = False Then
                            _BildDatei = ""
                        End If

                        'TvMovieMapping in TvServerDB auslesen & idChannel + displayName übergeben
                        ReadTvServerDB("Select * from tvmoviemapping Inner Join channel on tvmoviemapping.idChannel = channel.idChannel where stationName = '" & _ClickfinderDB(i).SenderKennung & "'")

                        While TvServerData.Read
                            Dim _idChannel As String = TvServerData.Item("idChannel")
                            Dim _ChannelName As String = TvServerData.Item("displayName")

                            'Prüfen ob Sendung in der TV DB vorhanden ist - Ja ->
                            If ProgramFoundinTvDb(_ClickfinderDB(i).Titel, _idChannel, _ClickfinderDB(i).Beginn, _ClickfinderDB(i).Ende) = True Then
                                Dim Sendung As Program = TvDatabase.Program.RetrieveByTitleTimesAndChannel(_ClickfinderDB(i).Titel, _ClickfinderDB(i).Beginn, _ClickfinderDB(i).Ende, _idChannel)

                                Dim _SeriesNum As String = Sendung.SeriesNum
                                Dim _EpisodeNum As String = Sendung.EpisodeNum


                                If Not _lastTitel = _ClickfinderDB(i).Titel & _ClickfinderDB(i).Originaltitel And _RespectInFavGroup = True And ChannelFoundInFavGroup(_idChannel) = True Then

                                    'Abbruch wenn alle Tipps gefüllt sind
                                    If _TippsCounter >= _idStoppCounter Then
                                        CloseTvServerDB()
                                        ctlProgressBar.Visible = False
                                        RatingStarsVisble()
                                        Exit Sub
                                    Else
                                        _TippClickfinderSendungID(_TippsCounter) = _ClickfinderDB(i).SendungID
                                        _TippClickfinderSendungChannelName(_TippsCounter) = _ChannelName
                                        _TippClickfinderSendungTitel(_TippsCounter) = _ClickfinderDB(i).Titel & _ClickfinderDB(i).Originaltitel
                                        FillTipps(_TippsCounter, Sendung.Title, _BildDatei, _ChannelName, _ClickfinderDB(i).Beginn, _
                                                  _ClickfinderDB(i).Ende, _ClickfinderDB(i).Genre, _ClickfinderDB(i).Bewertungen, _ClickfinderDB(i).Kurzkritik, _
                                                  "ClickfinderPG_R" & CStr(_ClickfinderDB(i).Bewertung) & ".png", _ClickfinderDB(i).Originaltitel, _SeriesNum, _EpisodeNum, _ClickfinderDB(i).Bewertung, _ClickfinderDB(i).Rating)

                                    End If

                                    _TippsCounter = _TippsCounter + 10

                                ElseIf Not _lastTitel = _ClickfinderDB(i).Titel & _ClickfinderDB(i).Originaltitel And _RespectInFavGroup = False Then

                                    'Abbruch wenn alle Tipps gefüllt sind
                                    If _TippsCounter >= _idStoppCounter Then
                                        CloseTvServerDB()
                                        ctlProgressBar.Visible = False
                                        RatingStarsVisble()
                                        Exit Sub
                                    Else
                                        _TippClickfinderSendungID(_TippsCounter) = _ClickfinderDB(i).SendungID
                                        _TippClickfinderSendungChannelName(_TippsCounter) = _ChannelName
                                        _TippClickfinderSendungTitel(_TippsCounter) = _ClickfinderDB(i).Titel & _ClickfinderDB(i).Originaltitel
                                        FillTipps(_TippsCounter, Sendung.Title, _BildDatei, _ChannelName, _ClickfinderDB(i).Beginn, _
                                                  _ClickfinderDB(i).Ende, _ClickfinderDB(i).Genre, _ClickfinderDB(i).Bewertungen, _ClickfinderDB(i).Kurzkritik, _
                                                  "ClickfinderPG_R" & CStr(_ClickfinderDB(i).Bewertung) & ".png", _ClickfinderDB(i).Originaltitel, _SeriesNum, _EpisodeNum, _ClickfinderDB(i).Bewertung, _ClickfinderDB(i).Rating)

                                    End If
                                    _TippsCounter = _TippsCounter + 10
                                End If
                                _lastTitel = _ClickfinderDB(i).Titel & _ClickfinderDB(i).Originaltitel
                            End If
                        End While
                        CloseTvServerDB()
                    End If

                    'End While
                Next

                ctlProgressBar.Visible = False
                RatingStarsVisble()

            Catch ex As Exception
                Log.Error("Clickfinder ProgramGuide: [ShowTipps]: " & ex.Message)
            End Try
        End Sub

        Private Sub ShowItemDetails(ByVal ClickfinderSendungID As String, ByVal ChannelName As String, Optional ByVal ChannelNameIsImagePath As Boolean = False)

            _CurrentDetailsSendungId = ClickfinderSendungID
            _CurrentDetailsSendungChannelName = ChannelName
            _CurrentDetailsImageIsPath = ChannelNameIsImagePath

            ClearDetails()

            'Detail Info's zeigen - GUIWindow
            DetailsImage.Visible = True
            ctlList.IsFocused = False
            btnTipp0.Focus = False
            btnTipp1.Focus = False
            btnTipp2.Focus = False
            btnTipp3.Focus = False
            btnTipp4.Focus = False
            btnBack.IsFocused = True

            For Each _item In _GuiImageList
                _GuiImageList(_item.Key).Visible = False
            Next

            Dim _ChannelName As String = ChannelName

            If ChannelNameIsImagePath = True Then
                'ChannelName aus Logo extrahieren - wird benötigt um das Clickfinder Mapping im Tv Server zu ermitteln
                If _useRatingTvLogos = "true" Then
                    _ChannelName = Replace(Left(_ChannelName, InStr(_ChannelName, "_") - 1), Config.GetFile(Config.Dir.Thumbs, "ClickfinderPG\tv\logos\"), "")
                Else
                    _ChannelName = Replace(Replace(_ChannelName, ".png", ""), Config.GetFile(Config.Dir.Thumbs, "tv\logos\"), "")
                End If
            End If

            'Clickfinder Datenbank öffnen & Daten einlesen
            Dim _ClickfinderDB As New ClickfinderDB("Select * from Sendungen Inner Join SendungenDetails on Sendungen.Pos = SendungenDetails.Pos where Sendungen.SendungID = '" & ClickfinderSendungID & "'")
            For i As Integer = 0 To _ClickfinderDB.Count - 1
                Try

                    DetailsTitel.Label = _ClickfinderDB(i).Titel

                    If Not _ClickfinderDB(i).Beschreibung = "" Then
                        If _ClickfinderDB(i).KurzBeschreibung = "" Or _ClickfinderDB(i).KurzBeschreibung = "HDTV" Then
                            DetailsBeschreibung.Label = Replace(_ClickfinderDB(i).Beschreibung, "<br><br>", vbNewLine)
                        Else
                            DetailsBeschreibung.Label = Replace(_ClickfinderDB(i).KurzBeschreibung, "HDTV,", "") & vbNewLine & vbNewLine & Replace(_ClickfinderDB(i).Beschreibung, "<br><br>", vbNewLine)
                        End If
                    End If

                    DetailsKanal.Label = _ChannelName
                    DetailsZeit.Label = Format(_ClickfinderDB(i).Beginn.Hour, "00") & _
                                        ":" & Format(_ClickfinderDB(i).Beginn.Minute, "00") & _
                                            " - " & Format(_ClickfinderDB(i).Ende.Hour, "00") & _
                                            ":" & Format(_ClickfinderDB(i).Ende.Minute, "00")

                    DetailsGenre.Label = _ClickfinderDB(i).Genre
                    DetailsActors.Label = _ClickfinderDB(i).Darsteller
                    DetailsOrgTitel.Label = _ClickfinderDB(i).Originaltitel
                    DetailsRegie.Label = _ClickfinderDB(i).Regie
                    DetailsYearLand.Label = _ClickfinderDB(i).Herstellungsland & " " & _ClickfinderDB(i).Herstellungsjahr
                    DetailsDauer.Label = CStr(_ClickfinderDB(i).Dauer) & " min"
                    DetailsKurzKritik.Label = _ClickfinderDB(i).Kurzkritik
                    DetailsBewertungen.Label = Replace(_ClickfinderDB(i).Bewertungen, ";", " ")

                    If _ClickfinderDB(i).Rating > 0 Then
                        DetailsRatingStars.Percentage = _ClickfinderDB(i).Rating * 10
                        DetailsRatingStars.Visible = True
                    Else
                        DetailsRatingStars.Percentage = 0
                        DetailsRatingStars.Visible = False
                    End If

                    Dim _BildDatei As String
                    If _ClickfinderDB(i).KzBilddateiHeruntergeladen = False Then
                        DetailsImage.KeepAspectRatio = True
                        DetailsImage.Centered = True

                        'Premier League
                        _BildDatei = UseSportLogos(_ClickfinderDB(i).Titel, _ChannelName)

                    Else
                        DetailsImage.KeepAspectRatio = False
                        DetailsImage.Centered = False
                        _BildDatei = _ClickfinderDB(i).Bilddateiname
                    End If

                    DetailsImage.FileName = _BildDatei
                    DetailsRatingImage.FileName = "ClickfinderPG_R" & CStr(_ClickfinderDB(i).Bewertung) & ".png"

                    Log.Debug("Clickfinder ProgramGuide: [ShowItemDetails]: ClkID: " & _CurrentDetailsSendungId)
                    Log.Debug("Clickfinder ProgramGuide: [ShowItemDetails]: Titel: " & _ClickfinderDB(i).Titel)
                    Log.Debug("Clickfinder ProgramGuide: [ShowItemDetails]: Channel: " & _ChannelName)
                    Log.Debug("Clickfinder ProgramGuide: [ShowItemDetails]: Time: " & (_ClickfinderDB(i).Beginn & " - " & (_ClickfinderDB(i).Ende)))

                Catch ex As Exception
                    Log.Error("Clickfinder ProgramGuide: [ShowItemDetails]: " & ex.Message)
                End Try
            Next

        End Sub
        'Private Sub ShowTheTvDBDetails()
        '    Dim _EpisodeFound As Boolean = False
        '    Dim _SeriesID As Integer = 0
        '    Dim _SeriesName As String = DetailsTitel.Label
        '    Dim _EpisodeName As String = DetailsOrgTitel.Label

        '    'Serie suchen und in Liste packen
        '    Dim _SearchSeriesResult As List(Of TvdbLib.Data.TvdbSearchResult) _
        '            = MyTVDB.TheTVdbHandler.SearchSeries(_SeriesName)



        '    Dim dlg As GUIDialogMenu = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_MENU, Integer)), GUIDialogMenu)
        '    Dim bla As GUIListItem = New GUIListItem


        '    'SerienId auf TheTvDB suchen mit SerienName
        '    If _SearchSeriesResult.Count > 0 Then
        '        For i = 0 To _SearchSeriesResult.Count - 1

        '            bla.Label = _SearchSeriesResult(i).SeriesName.ToString
        '            dlg.Add(bla)
        '            'If UCase(_SearchSeriesResult(i).SeriesName) = UCase(_SeriesName) Then
        '            '    _SeriesID = _SearchSeriesResult(i).Id
        '            '    Exit For
        '            'End If
        '        Next

        '        dlg.DoModal(GetID)



        '        ''kompletten Serien Inhalt laden, sofern SeriesId gefunden
        '        'If _SeriesID > 0 Then
        '        '    Dim _Serie As TvdbLib.Data.TvdbSeries = MyTVDB.TheTVdbHandler.GetFullSeries(_SeriesID, MyTVDB.DBLanguage, True)

        '        '    If _Serie.Episodes.Count > 0 Then
        '        '        For i = 0 To _Serie.Episodes.Count - 1
        '        '            If UCase(_Serie.Episodes(i).EpisodeName) = UCase(_EpisodeName) Then
        '        '                _EpisodeFound = True

        '        '                Dim _SeasonID As Integer = _Serie.Episodes(i).SeasonNumber


        '        '                '_Serie.Episodes(i).Banner.LoadThumb()

        '        '                '_Serie.PosterBanners(0).LoadThumb()

        '        '                DetailsOrgTitel.Label = _Serie.Episodes(i).EpisodeName _
        '        '                & " (Staffel " & _Serie.Episodes(i).SeasonNumber _
        '        '                & ", Episode " & _Serie.Episodes(i).EpisodeNumber _
        '        '                & ")"

        '        '                DetailsGenre.Label = Replace(_Serie.GenreString, "|", "")
        '        '                DetailsYearLand.Label = _Serie.Episodes(i).FirstAired
        '        '                DetailsRegie.Label = Replace(_Serie.Episodes(i).DirectorsString, "|", "")



        '        '                'MsgBox(Config.GetFile(Config.Dir.Cache, "ClickfinderPG\" & _SeasonID & "\" & Replace(Replace(_Serie.PosterBanners(0).ThumbPath, "_cache", "thumb"), "/", "_")))
        '        '                DetailsImage.KeepAspectRatio = True
        '        '                DetailsImage.Centered = True
        '        '                _Serie.PosterBanners(0).LoadThumb()
        '        '                DetailsImage.FileName = Config.GetFile(Config.Dir.Cache, "ClickfinderPG\" & _SeriesID.ToString & "\" & Replace(Replace(_Serie.PosterBanners(0).ThumbPath, "_cache", "thumb"), "/", "_"))


        '        '            End If
        '        '        Next

        '        '    End If
        '        'End If

        '        'If _EpisodeFound = False Then
        '        '    MPDialogOK("Information", "Episode nicht gefunden!")
        '        'End If

        '    Else
        '        MPDialogOK("Information", "Serie nicht gefunden!")
        '    End If






        '    ctlProgressBar.Visible = False

        'End Sub


#End Region

#Region "ProgressBar"

        'ProgresBar paralell anzeigen
        Private Sub ShowProgressbar()
            For Each _item In _GuiImageList
                _GuiImageList(_item.Key).Visible = False
            Next
            ctlProgressBar.Visible = True
        End Sub
        Private Sub ShowImportProgressbar()
            For Each _item In _GuiImageList
                _GuiImageList(_item.Key).Visible = False
            Next
            ctlImportProgress.Visible = True

        End Sub

#End Region

#Region "Functions and Subs"
        Private Sub FillTipps(ByVal StartIdofGroup As Integer, ByVal _Titel As String, ByVal _FavImagePath As String, _
            ByVal _channelName As String, ByVal _StartZeit As Date, ByVal _EndZeit As Date, ByVal _Genre As String, ByVal _BewertungStr As String, _
            ByVal _Kritik As String, ByVal _FavRatingImagePath As String, ByVal _EpisodenName As String, ByVal _SeriesNum As String, ByVal _EpisodeNum As String, ByVal _Bewertung As Integer, ByVal _Rating As Integer)

            Dim _Bilddatei As String = _FavImagePath

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
                If _Bewertung = 0 Then

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

                If _Rating > 0 Then
                    _GuiImageList(StartIdofGroup + 8).Percentage = _Rating * 10
                Else
                    _GuiImageList(StartIdofGroup + 8).Percentage = 0
                End If

                'Kritik 
                GUIFadeLabel.SetControlLabel(GetID, StartIdofGroup + 6, _Kritik)

                'Rating Image Path
                _GuiImage(StartIdofGroup + 7).SetFileName(_FavRatingImagePath)

                If Not _Titel = "" Then
                    Log.Debug("")
                    Log.Debug("Clickfinder ProgramGuide: [FillTipps: " & StartIdofGroup & "]: ClickfinderID: " & _TippClickfinderSendungID(StartIdofGroup))
                    Log.Debug("Clickfinder ProgramGuide: [FillTipps: " & StartIdofGroup & "]: Titel: " & _Titel & " " & _FavImagePath & " " & _channelName & " " & _Genre & " " & _StartZeit & " " & _EndZeit & " " & _BewertungStr & " " & _Kritik & " " & _FavRatingImagePath)
                    Log.Debug("Clickfinder ProgramGuide: [FillTipps: " & StartIdofGroup & "]: Channel: " & _channelName)
                    Log.Debug("Clickfinder ProgramGuide: [FillTipps: " & StartIdofGroup & "]: Time: " & _StartZeit & " - " & _EndZeit)
                    Log.Debug("Clickfinder ProgramGuide: [FillTipps: " & StartIdofGroup & "]: ImagePath: " & _Bilddatei)
                    Log.Debug("Clickfinder ProgramGuide: [FillTipps: " & StartIdofGroup & "]: RatingImagePath: " & _FavRatingImagePath)
                End If

            Catch ex As Exception
                Log.Error("Clickfinder ProgramGuide: [FillTipps]: " & ex.Message)
            End Try
        End Sub

        Private Sub ClearTipps()

            Dim _TippsCounter As Integer = _idStartCounter

            Try

                Do
                    FillTipps(_TippsCounter, Nothing, "", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "", "", "", "", 0, 0)

                    _TippsCounter = _TippsCounter + 10
                Loop Until _TippsCounter = _idStoppCounter
                Log.Debug("")
                Log.Debug("Clickfinder ProgramGuide: [ClearTipps]: Called")
                Log.Debug("")
            Catch ex As Exception
                Log.Error("Clickfinder ProgramGuide: [ClearTipps]: " & ex.Message)
            End Try

        End Sub

        Private Sub RatingStarsVisble()

            For Each _item In _GuiImageList
                If _GuiImageList(_item.Key).Percentage > 0 Then
                    _GuiImageList(_item.Key).Visible = True
                Else
                    _GuiImageList(_item.Key).Visible = False
                End If
            Next

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

        Private Function UseSportLogos(ByVal _Titel As String, ByVal _channelname As String) As String

            Dim _BildDatei As String = Config.GetFile(Config.Dir.Thumbs, "tv\logos\" & _channelname & ".png")

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
            DetailsYearLand.Label = ""
            DetailsDauer.Label = ""
            DetailsKurzKritik.Label = ""
            DetailsBewertungen.Label = ""
            DetailsImage.FileName = ""
            DetailsRatingImage.FileName = ""

        End Sub

        Private Sub Dictonary()

            'Dictionary Tipp Image & ImagePaths  Variablen        
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

            'Dictionary Tipp FavRatingStars Image Variablen 
            _GuiImageList.Add(118, FavRatingStars0)
            _GuiImageList.Add(128, FavRatingStars1)
            _GuiImageList.Add(138, FavRatingStars2)
            _GuiImageList.Add(148, FavRatingStars3)
            _GuiImageList.Add(158, FavRatingStars4)

            'Dictionary Tipp btnTipp Button Variablen 
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

            _TippClickfinderSendungTitel.Add(110, _TippClickfinderSendungTitel0)
            _TippClickfinderSendungTitel.Add(120, _TippClickfinderSendungTitel1)
            _TippClickfinderSendungTitel.Add(130, _TippClickfinderSendungTitel2)
            _TippClickfinderSendungTitel.Add(140, _TippClickfinderSendungTitel3)
            _TippClickfinderSendungTitel.Add(150, _TippClickfinderSendungTitel4)

            _TippClickfinderSendungChannelName.Add(110, _TippClickfinderSendungChannelName0)
            _TippClickfinderSendungChannelName.Add(120, _TippClickfinderSendungChannelName1)
            _TippClickfinderSendungChannelName.Add(130, _TippClickfinderSendungChannelName2)
            _TippClickfinderSendungChannelName.Add(140, _TippClickfinderSendungChannelName3)
            _TippClickfinderSendungChannelName.Add(150, _TippClickfinderSendungChannelName4)

        End Sub
#End Region

#Region "TV Movie and TVServer Database Access and Function"

        'MP - TVServer Datenbank Variablen  - MYSql
        Public ConTvServerDBRead As New MySqlConnection
        Public CmdTvServerDBRead As New MySqlCommand
        Public TvServerData As MySqlDataReader

        Public Count As Long

        Private Sub ReadTvServerDB(ByVal SQLString As String)

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
        Private Sub CloseTvServerDB()

            Try
                CmdTvServerDBRead.Dispose()
                ConTvServerDBRead.Close()
            Catch ex As Exception
                Log.Error("Clickfinder ProgramGuide: [CloseTvServerDB]: " & ex.Message)
            End Try

        End Sub

        Private Function ProgramFoundinTvDb(ByVal _Titel As String, ByVal _idChannel As String, ByVal _StartZeit As Date, ByVal _EndZeit As Date) As Boolean

            Dim Con As New MySqlConnection
            Dim Cmd As New MySqlCommand

            Dim _SQLString As String = ("SELECT COUNT(*) FROM program WHERE Title = '" & _Titel & "' AND idChannel = " & _idChannel & " AND startTime =" & DateTOMySQLstring(_StartZeit) & " AND endTime =" & DateTOMySQLstring(_EndZeit))
            Try

                Con.ConnectionString = Left(Replace(Gentle.Framework.GentleSettings.DefaultProviderConnectionString, " ", ""), InStr(Gentle.Framework.GentleSettings.DefaultProviderConnectionString, "charset=utf8") - 3)
                Con.Open()

                Cmd = Con.CreateCommand
                Cmd.CommandText = _SQLString

                Dim DataCount As Long = CLng(Cmd.ExecuteScalar)

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

            Dim Con As New MySqlConnection
            Dim Cmd As New MySqlCommand

            Dim _idGroup As String = MPSettingRead("config", "ChannelGroupID")
            Dim _SQLString As String = ("SELECT COUNT(*) FROM groupMap WHERE idChannel = " & _idchannel & " AND idGroup = " & _idGroup)

            Try

                Con.ConnectionString = Left(Replace(Gentle.Framework.GentleSettings.DefaultProviderConnectionString, " ", ""), InStr(Gentle.Framework.GentleSettings.DefaultProviderConnectionString, "charset=utf8") - 3)
                Con.Open()

                Cmd = Con.CreateCommand
                Cmd.CommandText = _SQLString

                Dim DataCount As Long = CLng(Cmd.ExecuteScalar)

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
            Dim _Counter As Integer = 0


            For Each _item In _GuiImageList
                _GuiImageList(_item.Key).Visible = False

            Next
            ctlProgressBar.Visible = True

            'Prüfen ob in der Tabelle SendungenDetails die Spalte "Rating" existiert
            If DoesFieldExist("SendungenDetails", _NewColumn, "Select * FROM SendungenDetails") = False Then

                'Falls nicht -> erstellen
                Try
                    Dim dbConn As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;" & "Data Source=" & _ClickfinderPath & "\tvdaten.mdb")
                    dbConn.Open()

                    Dim dbCmd As New OleDb.OleDbCommand("ALTER TABLE SendungenDetails ADD COLUMN " & _NewColumn & " int DEFAULT 0", dbConn)
                    dbCmd.ExecuteNonQuery()

                    Log.Info("Clickfinder ProgramGuide: [CreateClickfinderRatingTable]: RatingField in SendungenDetails created")

                    dbCmd.Dispose()
                    dbConn.Close()

                Catch ex As Exception
                    Log.Error("Clickfinder ProgramGuide: [CreateClickfinderRatingTable]: " & ex.Message)
                End Try
            End If


            Dim _TestStartZeit As Date = Today
            Dim _TestEndZeit As Date = Today.AddHours(6)
            Dim _RatingField As New ClickfinderDB(Replace(SQLQueryAccess(_TestStartZeit, _TestEndZeit, "AND Bewertung >= 1 AND Rating >= 1 AND KzFilm = true"), "*", "DISTINCT Rating"))

            'MsgBox(_RatingField.Count)
            'Prüfen ob Rating für die kommenden Stunden existiert - ClickfinderDB Update erkennen
            If _RatingField.Count <= 5 Then
                Log.Info("Clickfinder ProgramGuide: [CreateClickfinderRatingTable]: Start Calculate & write Ratings")

                Try
                    Dim _StartZeit As Date = Today
                    Dim _EndZeit As Date = Today.AddDays(CDbl(MPSettingRead("config", "UpdateInterval")))
                    Dim _SQLString As String = SQLQueryAccess(_StartZeit, _EndZeit, "AND Bewertung >= 1 AND Bewertungen LIKE '%Spann%'")
                    Dim _ClickfinderDB As New ClickfinderDB(_SQLString)
                    Dim Max As Integer = _ClickfinderDB.Count

                    ctlImportProgress.Percentage = 0
                    ctlProgressBar.Visible = False
                    ctlImportProgress.IsVisible = True

                    'Connection DB öffnen fürs schreiben
                    Dim dbConn As New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;" & "Data Source=" & _ClickfinderPath & "\tvdaten.mdb")
                    Dim dbCmd As New OleDb.OleDbCommand
                    dbCmd.Connection = dbConn
                    dbConn.Open()

                    For i As Integer = 0 To _ClickfinderDB.Count - 1

                        ctlImportProgress.Percentage = (i + 1) / Max * 100

                        _BewertungenStrNumber = Replace(Replace(Replace(Replace(Replace(Replace(Replace(_ClickfinderDB(i).Bewertungen, ";", " "), "Spaß=", ""), "Action=", ""), "Erotik=", ""), "Spannung=", ""), "Gefühl=", ""), "Anspruch=", "")
                        _Rating = 0

                        For d = 1 To _BewertungenStrNumber.Length
                            If IsNumeric(Mid$(_BewertungenStrNumber, d, 1)) Then
                                _Rating = _Rating + CInt(Mid$(_BewertungenStrNumber, d, 1))
                            End If
                        Next

                        If Not _ClickfinderDB(i).Rating = _Rating Then
                            dbCmd.CommandText = ("UPDATE SendungenDetails SET Rating = '" & _Rating & "' WHERE Pos = " & _ClickfinderDB(i).Pos)
                            dbCmd.ExecuteNonQuery()
                        End If

                    Next
                    dbConn.Close()
                    ctlProgressBar.Visible = True
                    ctlImportProgress.Visible = False

                Catch ex As Exception
                    Log.Error("Clickfinder ProgramGuide: [CreateClickfinderRatingTable]: " & ex.Message)
                End Try

            End If

            ShowTipps()

        End Sub

        Private Function DoesFieldExist(ByVal tblName As String, _
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
        Private Function MPSettingRead(ByVal section As String, ByVal entry As String) As String
            Using xmlReader As New Settings(Config.GetFile(Config.Dir.Config, "ClickfinderPGConfig.xml"))
                MPSettingRead = xmlReader.GetValue(section, entry)
            End Using
        End Function

        Private Sub MPSettingsWrite(ByVal section As String, ByVal entry As String, ByVal NewAttribute As String)
            Using xmlReader As New Settings(Config.GetFile(Config.Dir.Config, "ClickfinderPGConfig.xml"))
                xmlReader.SetValue(section, entry, NewAttribute)
            End Using

        End Sub

        Private Sub AddListControlItem(ByVal SendungID As String, ByVal Label As String, Optional ByVal label2 As String = "", Optional ByVal label3 As String = "", Optional ByVal ImagePath As String = "")

            Dim lItem As New GUIListItem

            lItem.Label = Label
            lItem.Label2 = label2
            lItem.Label3 = label3
            lItem.ItemId = CInt(SendungID)
            lItem.IconImage = ImagePath
            GUIControl.AddListItemControl(GetID, ctlList.GetID, lItem)

            Log.Debug("Clickfinder ProgramGuide: [AddListControlItem]: ClickfinderID: " & SendungID)
            Log.Debug("Clickfinder ProgramGuide: [AddListControlItem]: ImagePath: " & ImagePath)


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

        'MP Program Info aufrufen
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

        'MP Notify für Sendung setzen
        Private Sub SetNotify(ByVal idChannel As Integer, ByVal StartTime As Date, ByVal EndTime As Date, ByVal Titel As String)

            Dim Kanal As Channel = Channel.Retrieve(idChannel)

            Dim Erinnerung As Program = Program.RetrieveByTitleTimesAndChannel(Titel, StartTime, EndTime, idChannel)
            Erinnerung.Notify = True
            Erinnerung.Persist()
            TvNotifyManager.OnNotifiesChanged()

            MPDialogOK("Erinnerung:", Titel, StartTime & " - " & EndTime, Kanal.DisplayName)


        End Sub
#End Region

    End Class
End Namespace


