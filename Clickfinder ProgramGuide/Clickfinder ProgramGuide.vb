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
        <SkinControlAttribute(5)> Protected btnLateTimeRest As GUIButtonControl = Nothing
        <SkinControlAttribute(6)> Protected btnCommingTipps As GUIButtonControl = Nothing


        <SkinControlAttribute(9)> Protected ctlProgressBar As GUIAnimation = Nothing
        <SkinControlAttribute(10)> Protected ctlList As GUIListControl = Nothing



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
        Private _ShowSQLString As String
        Private LiveCorrection As Boolean = False
        Private WdhCorrection As Boolean = False
        Private CurrentQuery As String
        Private _ZeitQueryHour As Double
        Private _ZeitQueryMinute As Double
        Private _RespectInFavGroup As Boolean
        Private _useRatingTvLogos As String = MPSettingRead("config", "useRatingTvLogos")
        Private _SettingIgnoreMinTimeSeries As String = MPSettingRead("config", "IgnoreMinTimeSeries")
        Private _SettingDelayNow As Double = CInt(MPSettingRead("config", "DelayNow"))
        Private _SettingPrimeTimeHour As Double = CDbl(MPSettingRead("config", "PrimeTimeHour"))
        Private _SettingPrimeTimeMinute As Double = CDbl(MPSettingRead("config", "PrimeTimeMinute"))
        Private _SettingLateTimeHour As Double = CDbl(MPSettingRead("config", "LateTimeHour"))
        Private _SettingLateTimeMinute As Double = CDbl(MPSettingRead("config", "LateTimeMinute"))
        Private _GuiImage As Dictionary(Of Integer, GUIImage) = New Dictionary(Of Integer, GUIImage)
        Private _CurrentCategorie As String
        Const _idStartCounter As Integer = 110
        Const _idStoppCounter As Integer = 160


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

#Region "GUI Actions"
        Protected Overrides Sub OnPageLoad()
            MyBase.OnPageLoad()
            GUIWindowManager.NeedRefresh()


            ctlProgressBar.Visibility = Windows.Visibility.Hidden
            DetailsImage.Visibility = Windows.Visibility.Hidden
            btnNow.IsFocused = True



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






            ClickfinderCorrection()

            Button_Now()

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
                _ZeitQueryHour = _SettingPrimeTimeHour
                _ZeitQueryMinute = _SettingPrimeTimeMinute
                CurrentQuery = "PrimeTime"

                ShowCategories()
            End If

            If control Is btnLateTime Then
                _ZeitQueryHour = _SettingLateTimeHour
                _ZeitQueryMinute = _SettingLateTimeMinute
                CurrentQuery = "LateTime"

                ShowCategories()
            End If
            If control Is btnLateTimeRest Then

                ClearTipps()


            End If


            If control Is btnCommingTipps Then
                Button_CommingTipps()
            End If

            If control Is btnBack Then
                DetailsImage.Visible = False
                ctlList.IsFocused = True
                'btnLateTime.IsFocused = False
            End If

            If control Is btnRecord Then

                Button_Record()

            End If

            If control Is btnRemember Then

                Button_Remember()

            End If

        End Sub

        Protected Overrides Sub OnPageDestroy(ByVal new_windowId As Integer)
            MyBase.OnPageDestroy(new_windowId)
            _GuiImage.Clear
            'GUIWindowManager.ResetAllControls()


        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Private Sub Button_Now()
            Dim t As DateTime = DateTime.Now.Subtract(New System.TimeSpan(0, _SettingDelayNow, 0))

            _ZeitQueryHour = t.Hour
            _ZeitQueryMinute = t.Minute
            CurrentQuery = "Now"



            ShowCategories()
        End Sub

        Private Sub Button_CommingTipps()

            StartFillListControlCommingNextTipps()

        End Sub

        Private Sub Button_Record()

            Dim Titel As String

            Dim StartTime As String
            Dim ProofDate As Integer
            Dim SQLDateString As String
            Dim Heute As Date
            Dim AktuelleZeit As Date

            DetailsImage.Visible = True
            ctlList.IsFocused = False
            btnBack.IsFocused = True

            Titel = Replace(ctlList.SelectedListItem.Label.ToString, "'", "''")

            StartTime = Left(ctlList.SelectedListItem.Label2.ToString, InStr(ctlList.SelectedListItem.Label2.ToString, "-") - 2)
            ProofDate = Left(StartTime, InStr(StartTime, ":") - 1)

            AktuelleZeit = Now


            If AktuelleZeit.Hour >= 0 And AktuelleZeit.Hour <= 2 Then

                Heute = Today
                SQLDateString = "'" & Heute.Year & "-" & Format(Heute.Month, "00") & "-" & Format(Heute.Day, "00") & " " & StartTime & ":00'"
            Else
                'Datum.Day +1 ab 0:00h
                If ProofDate >= 0 And ProofDate <= 2 Then
                    Heute = Today.AddDays(1)
                    SQLDateString = "'" & Heute.Year & "-" & Format(Heute.Month, "00") & "-" & Format(Heute.Day, "00") & " " & StartTime & ":00'"
                Else
                    Heute = Today
                    SQLDateString = "'" & Heute.Year & "-" & Format(Heute.Month, "00") & "-" & Format(Heute.Day, "00") & " " & StartTime & ":00'"
                End If
            End If

            ReadTvServerDB("SELECT * from program INNER JOIN channel ON program.idChannel = channel.idChannel where displayName = '" & DetailsKanal.Label & "' AND title = '" & Titel & "' AND startTime =" & SQLDateString)
            While TvServerData.Read

                LoadTVProgramInfo(TvServerData.Item("idChannel"), TvServerData.Item("startTime"), TvServerData.Item("endTime"), TvServerData.Item("title"))
                Exit While
            End While

            CloseTvServerDB()


            btnRecord.IsFocused = False
            btnBack.IsFocused = True

        End Sub
        Private Sub Button_Remember()

            Dim Titel As String

            Dim StartTime As String
            Dim ProofDate As Integer
            Dim SQLDateString As String
            Dim Heute As Date
            Dim AktuelleZeit As Date

            DetailsImage.Visible = True
            ctlList.IsFocused = False
            btnBack.IsFocused = True

            Titel = Replace(ctlList.SelectedListItem.Label.ToString, "'", "''")

            StartTime = Left(ctlList.SelectedListItem.Label2.ToString, InStr(ctlList.SelectedListItem.Label2.ToString, "-") - 2)
            ProofDate = Left(StartTime, InStr(StartTime, ":") - 1)

            AktuelleZeit = Now


            If AktuelleZeit.Hour >= 0 And AktuelleZeit.Hour <= 2 Then

                Heute = Today
                SQLDateString = "'" & Heute.Year & "-" & Format(Heute.Month, "00") & "-" & Format(Heute.Day, "00") & " " & StartTime & ":00'"
            Else
                'Datum.Day +1 ab 0:00h
                If ProofDate >= 0 And ProofDate <= 2 Then
                    Heute = Today.AddDays(1)
                    SQLDateString = "'" & Heute.Year & "-" & Format(Heute.Month, "00") & "-" & Format(Heute.Day, "00") & " " & StartTime & ":00'"
                Else
                    Heute = Today
                    SQLDateString = "'" & Heute.Year & "-" & Format(Heute.Month, "00") & "-" & Format(Heute.Day, "00") & " " & StartTime & ":00'"
                End If
            End If




            ReadTvServerDB("SELECT * from program INNER JOIN channel ON program.idChannel = channel.idChannel where displayName = '" & DetailsKanal.Label & "' AND title = '" & Titel & "' AND startTime =" & SQLDateString)

            If TvServerData.HasRows = True Then
                While TvServerData.Read

                    SetNotify(TvServerData.Item("idChannel"), TvServerData.Item("startTime"), TvServerData.Item("endTime"), TvServerData.Item("title"))
                    Exit While
                End While

            Else
                MsgBox("nix da")
            End If

            CloseTvServerDB()

            btnRemember.IsFocused = False
            btnBack.IsFocused = True

        End Sub


#End Region

#Region "Show Events"

        Private Sub ShowCategories()

            Dim _ProgressBar As New Thread(AddressOf ShowProgressbar)
            Dim _Threat As New Thread(AddressOf ShowTipps)
            _RespectInFavGroup = False
            _ShowSQLString = SQLQueryAccess(_ZeitQueryHour, _ZeitQueryMinute, "AND Bewertung >= 3 AND Bewertung <= 4 AND KzFilm = true", "Beginn ASC, Bewertung DESC, Titel")

            _ProgressBar.Start()
            _Threat.Start()

            ctlList.ListItems.Clear()
            AddListControlItem("Movies")
            AddListControlItem("Sendungen")
            AddListControlItem("Serien")
            AddListControlItem("Dokumentationen")
            AddListControlItem("Sport")
            AddListControlItem("Bundesliga")
            AddListControlItem("ChampionsLeague")

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

            Dim _lastTitel As String
            Dim _FavCounter As Integer

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

                AddListControlItem("..", , , "defaultFolderBack.png")

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

                            'Program in der TV Server Program DB suchen

                            If ProgramFoundinTvDb(_Titel, _idChannel, _StartZeit, _EndZeit) = True Then
                                Dim Sendung As Program = TvDatabase.Program.RetrieveByTitleTimesAndChannel(_Titel, _StartZeit, _EndZeit, _idChannel)


                                'TVServer GroupMap DB öffen und prüfen ob in FavoritenGruppe 
                                Dim _GroupMap = TvDatabase.GroupMap.ListAll
                                For d = 0 To _GroupMap.Count - 1
                                    If _GroupMap.Item(d).IdChannel = CInt(_idChannel) And _GroupMap.Item(d).IdGroup = CInt(_idGroup) Then
                                        _inFav = True
                                        Exit For
                                    Else
                                        _inFav = False

                                    End If
                                Next


                                'Falls Ja: Detailview erstellen
                                If _inFav = True And _FavCounter <= 4 Then





                                    _FavCounter = _FavCounter + 1

                                ElseIf Not Sendung.Title = _lastTitel Then

                                    If _useRatingTvLogos = "true" Then

                                        _TvLogo = Config.GetFile(Config.Dir.Thumbs, "ClickfinderPG\tv\logos\") & Channel.Retrieve(_idChannel).DisplayName & "_" & _Bewertung & ".png"
                                    Else
                                        _TvLogo = Config.GetFile(Config.Dir.Thumbs, "tv\logos\" & Channel.Retrieve(_idChannel).DisplayName & ".png")
                                    End If



                                    AddListControlItem(Sendung.Title.ToString, _
                                                        Format(CDate(Sendung.StartTime).Hour, "00") & _
                                                        ":" & Format(CDate(Sendung.StartTime).Minute, "00") & _
                                                        " - " & Format(CDate(Sendung.EndTime).Hour, "00") & _
                                                        ":" & Format(CDate(Sendung.EndTime).Minute, "00"), _
                                                        _Genre, _
                                                        _TvLogo)
                                End If



                                _lastTitel = Sendung.Title
                                _inFav = False


                            End If
                        End While

                        CloseTvServerDB()
                    End If

                End While
                CloseClickfinderDB()



                ctlProgressBar.Visible = False
            Catch ex As Exception
                MsgBox(ex.Message)
                GUIWindowManager.GetPreviousActiveWindow()
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

                        ' Clickfinder Titel Korrektur bei "Append (Live) / (Whd.)
                        If LiveCorrection = True And ClickfinderData.Item("KzLive") = "true" Then
                            _Titel = ClickfinderData.Item("Titel") & " (LIVE)"
                        ElseIf WdhCorrection = True And ClickfinderData.Item("KzWiederholung") = "true" Then
                            _Titel = ClickfinderData.Item("Titel") & " (Wdh.)"
                        Else
                            _Titel = ClickfinderData.Item("Titel")
                        End If

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



                                If Not Sendung.Title = _lastTitel And _RespectInFavGroup = True And ChannelFoundInFavGroup(_idChannel) = True Then

                                    'Abbruch wenn alle Tipps gefüllt sind
                                    If _TippsCounter >= _idStoppCounter Then
                                        CloseTvServerDB()
                                        CloseClickfinderDB()
                                        Exit Sub
                                    Else

                                        FillTipps(_TippsCounter, _Titel, _BildDatei, _ChannelName, _StartZeit, _
                                                  _EndZeit, _Genre, _BewertungStr, _Kritik, _
                                                  "ClickfinderPG_R" & CStr(_Bewertung) & ".png")

                                    End If

                                    _TippsCounter = _TippsCounter + 10

                                ElseIf Not Sendung.Title = _lastTitel And _RespectInFavGroup = False Then

                                    'Abbruch wenn alle Tipps gefüllt sind
                                    If _TippsCounter >= _idStoppCounter Then
                                        CloseTvServerDB()
                                        CloseClickfinderDB()
                                        Exit Sub
                                    Else

                                        FillTipps(_TippsCounter, _Titel, _BildDatei, _ChannelName, _StartZeit, _
                                                  _EndZeit, _Genre, _BewertungStr, _Kritik, _
                                                  "ClickfinderPG_R" & CStr(_Bewertung) & ".png")

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
                MsgBox(ex.Message)
            End Try
        End Sub

#End Region



#Region "ListControl Funktionen"

        'ListControl mit Daten zur bestimmter Uhrzeit (SQLString) - paralelles Threating




        'ListControl mit Daten der kommenden TagesTipps füllen - paralleles Threating
        Private Sub StartFillListControlCommingNextTipps()
            Dim ProgressBar, ShowCommingNextTipps As Threading.Thread

            ProgressBar = New Thread(AddressOf ShowProgressbar)
            ShowCommingNextTipps = New Thread(AddressOf FillListControlCommingNextTipps)

            ProgressBar.Start()
            ShowCommingNextTipps.Start()
        End Sub
        Private Sub FillListControlCommingNextTipps()
            Dim LateTimeSQL As String
            Dim EndofDay As Date
            Dim Dauer As String
            Dim EndofDaySQL As String
            Dim Rating As String

            ctlProgressBar.Visible = True


            Rating = MPSettingRead("config", "ClickfinderRating")

            EndofDay = Today.AddMonths(1)

            LateTimeSQL = "#" & Today.Year & "-" & Format(Today.Month, "00") & "-" & Format(Today.Day, "00") & " 00:01:00#"
            EndofDaySQL = "#" & EndofDay.Year & "-" & Format(EndofDay.Month, "00") & "-" & Format(EndofDay.Day, "00") & " 02:00:00#"

            ctlList.ListItems.Clear()

            ReadClickfinderDB("Select * from Sendungen where (Beginn Between " & LateTimeSQL & " AND " & EndofDaySQL & ") AND Bewertung = 4 ORDER BY Beginn ASC")

            While ClickfinderData.Read


                Dauer = Replace(Replace(ClickfinderData.Item("Dauer"), " ", ""), "min", "")

                '110 min
                If CInt(Dauer) > 10 Or CInt(Dauer) < 240 Then

                    Dim lItem As New GUIListItem
                    Dim TagBezeichnung As String

                    TagBezeichnung = CDate(ClickfinderData.Item("Beginn")).DayOfWeek

                    Select Case TagBezeichnung
                        Case Is = "0"
                            TagBezeichnung = "Sonntag"
                        Case Is = "1"
                            TagBezeichnung = "Montag"
                        Case Is = "2"
                            TagBezeichnung = "Dienstag"
                        Case Is = "3"
                            TagBezeichnung = "Mittwoch"
                        Case Is = "4"
                            TagBezeichnung = "Donnerstag"
                        Case Is = "5"
                            TagBezeichnung = "Freitag"
                        Case Is = "6"
                            TagBezeichnung = "Samstag"
                    End Select

                    lItem.Label = ClickfinderData.Item("Titel")
                    lItem.Label2 = TagBezeichnung & " " & Format(CDate(ClickfinderData.Item("Beginn")).Day, "00") & "." & Format(CDate(ClickfinderData.Item("Beginn")).Month, "00") & "." & CDate(ClickfinderData.Item("Beginn")).Year
                    lItem.Label3 = ClickfinderData.Item("Genre").ToString
                    'lItem.Label2 = SeriesReader.Item("ID")
                    lItem.ItemId = ctlList.ListItems.Count - 1
                    'lItem.Label2 = item.StartTime.Date
                    'lItem.Path = Test
                    lItem.IconImage = Config.GetFile(Config.Dir.Thumbs, "tv\logos\" & ClickfinderData.Item("SenderKennung").ToString & ".png")
                    lItem.IsFolder = False
                    GUIControl.AddListItemControl(GetID, ctlList.GetID, lItem)

                End If

            End While

            CloseClickfinderDB()

            ctlProgressBar.Visible = False
        End Sub

        'ListControl Click Action
        Private Sub ListControlClick()
            Dim _Rating As String
            Dim _ProgressBar As New Thread(AddressOf ShowProgressbar)
            Dim _Threat As New Thread(AddressOf ShowSelectedCategorieItems)
            Dim _Threat2 As New Thread(AddressOf ShowTipps)
            Dim i As Integer

            _Rating = MPSettingRead("config", "ClickfinderRating")
            _RespectInFavGroup = True


            Select Case ctlList.SelectedListItem.Label.ToString

                Case ".."
                    Log.Debug("Clickfinder ProgramGuide: [ListControlClick] Call ShowCategories")
                    Try
                        'Dim _Threat As New Thread(AddressOf ClearFavInfo)
                        If ctlProgressBar.IsVisible = False Then
                            ctlList.Clear()
                            ShowCategories()
                        End If
                    Catch ex As Exception
                        Log.Error("Clickfinder ProgramGuide: [ListControlClick] Call ShowCategories: " & ex.Message)
                    End Try


                Case "Movies"
                    Log.Debug("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ctlList.SelectedListItem.Label.ToString & " - " & CurrentQuery.ToString)
                    Try
                        ctlList.Clear()
                        _CurrentCategorie = "Movies"
                        Select Case CurrentQuery.ToString
                            Case Is = "Now"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryHour, _ZeitQueryMinute, "AND Bewertung >= " & _Rating & " AND Bewertung <= 4", "Beginn ASC, Bewertung DESC, Titel")
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "PrimeTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryHour, _ZeitQueryMinute, "AND Bewertung >= " & _Rating & " AND Bewertung <= 4", "Beginn ASC, Bewertung DESC, Titel")
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "LateTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryHour, _ZeitQueryMinute, "AND Bewertung >= " & _Rating & " AND Bewertung <= 4", "Beginn ASC, Bewertung DESC, Titel")
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                        End Select
                    Catch ex As Exception
                        Log.Error("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ex.Message)
                    End Try


                Case "Sendungen"
                    Log.Debug("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ctlList.SelectedListItem.Label.ToString & " - " & CurrentQuery.ToString)
                    Try
                        ctlList.Clear()
                        _CurrentCategorie = "Sendungen"
                        Select Case CurrentQuery.ToString
                            Case Is = "Now"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryHour, _ZeitQueryMinute, "AND Bewertung >= 5 AND Bewertung <= 6", "Beginn ASC, Bewertung DESC, Titel")
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "PrimeTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryHour, _ZeitQueryMinute, "AND Bewertung >= 5 AND Bewertung <= 6", "Beginn ASC, Bewertung DESC, Titel")
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "LateTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryHour, _ZeitQueryMinute, "AND Bewertung >= 5 AND Bewertung <= 6", "Beginn ASC, Bewertung DESC, Titel")
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                        End Select
                    Catch ex As Exception
                        Log.Error("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ex.Message)
                    End Try

                Case "Serien"
                    Log.Debug("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ctlList.SelectedListItem.Label.ToString & " - " & CurrentQuery.ToString)
                    Try
                        ctlList.Clear()
                        _CurrentCategorie = "Serien"
                        Select Case CurrentQuery.ToString
                            Case Is = "Now"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryHour, _ZeitQueryMinute, "AND Keywords LIKE '%Serie%'", "Beginn ASC, Bewertung DESC, Titel")
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "PrimeTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryHour, _ZeitQueryMinute, "AND Keywords LIKE '%Serie%'", "Beginn ASC, Bewertung DESC, Titel")
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "LateTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryHour, _ZeitQueryMinute, "AND Keywords LIKE '%Serie%'", "Beginn ASC, Bewertung DESC, Titel")
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                        End Select
                    Catch ex As Exception
                        Log.Error("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ex.Message)
                    End Try

                Case "Dokumentationen"
                    Log.Debug("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ctlList.SelectedListItem.Label.ToString & " - " & CurrentQuery.ToString)
                    Try
                        ctlList.Clear()
                        _CurrentCategorie = "Dokumentationen"
                        Select Case CurrentQuery.ToString
                            Case Is = "Now"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryHour, _ZeitQueryMinute, "AND (Keywords LIKE '%Dokumentation%' OR Keywords LIKE '%Report%' OR Keywords LIKE '%Reportage%')", "Beginn ASC, Bewertung DESC, Titel")
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "PrimeTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryHour, _ZeitQueryMinute, "AND (Keywords LIKE '%Dokumentation%' OR Keywords LIKE '%Report%' OR Keywords LIKE '%Reportage%')", "Beginn ASC, Bewertung DESC, Titel")
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "LateTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryHour, _ZeitQueryMinute, "AND (Keywords LIKE '%Dokumentation%' OR Keywords LIKE '%Report%' OR Keywords LIKE '%Reportage%')", "Beginn ASC, Bewertung DESC, Titel")
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                        End Select
                    Catch ex As Exception
                        Log.Error("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ex.Message)
                    End Try

                Case "Sport"
                    Log.Debug("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ctlList.SelectedListItem.Label.ToString & " - " & CurrentQuery.ToString)
                    Try
                        ctlList.Clear()
                        _CurrentCategorie = "Sport"
                        Select Case CurrentQuery.ToString
                            Case Is = "Now"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryHour, _ZeitQueryMinute, "AND Keywords LIKE '%Sport%'", "Beginn ASC, Bewertung DESC, Titel")
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "PrimeTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryHour, _ZeitQueryMinute, "AND Keywords LIKE '%Sport%'", "Beginn ASC, Bewertung DESC, Titel")
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                            Case Is = "LateTime"
                                _ShowSQLString = SQLQueryAccess(_ZeitQueryHour, _ZeitQueryMinute, "AND Keywords LIKE '%Sport%'", "Beginn ASC, Bewertung DESC, Titel")
                                ShowTipps()
                                _ProgressBar.Start()
                                _Threat.Start()
                        End Select
                    Catch ex As Exception
                        Log.Error("Clickfinder ProgramGuide: [ListControlClick] Call ShowSelectedCategorieItems: " & ex.Message)
                    End Try

                Case Else

                    Dim Titel As String
                    Dim Kategorie As String
                    Dim StartTime As String
                    Dim ProofDate As Integer
                    Dim SQLDateString As String
                    Dim Heute As Date
                    Dim ClickfinderPath As String
                    Dim RatingImage As String
                    Dim AktuelleZeit As Date

                    DetailsImage.Visible = True
                    ctlList.IsFocused = False
                    btnBack.IsFocused = True


                    Titel = Replace(ctlList.SelectedListItem.Label.ToString, "'", "''")
                    Kategorie = ctlList.SelectedListItem.Label3.ToString
                    StartTime = Left(ctlList.SelectedListItem.Label2.ToString, InStr(ctlList.SelectedListItem.Label2.ToString, "-") - 2)
                    ProofDate = Left(StartTime, InStr(StartTime, ":") - 1)
                    RatingImage = Nothing

                    ClickfinderPath = MPSettingRead("config", "ClickfinderPath")

                    AktuelleZeit = Now

                    'Datum.Day +1 ab 0:00h
                    If ProofDate >= 0 And ProofDate <= 2 Then
                        Heute = Today.AddDays(1)
                        SQLDateString = "#" & Heute.Year & "-" & Format(Heute.Month, "00") & "-" & Format(Heute.Day, "00") & " " & StartTime & ":00#"
                    Else
                        Heute = Today
                        SQLDateString = "#" & Heute.Year & "-" & Format(Heute.Month, "00") & "-" & Format(Heute.Day, "00") & " " & StartTime & ":00#"
                    End If



                    ReadClickfinderDB("Select * from Sendungen Inner Join SendungenDetails on Sendungen.Pos = SendungenDetails.Pos where Beginn = " & SQLDateString & " AND Titel = '" & Titel & "' and Genre = '" & Kategorie & "'")



                    While ClickfinderData.Read

                        DetailsImage.FileName = ClickfinderPath & "\Hyperlinks\" & ClickfinderData.Item("Bilddateiname")
                        'GUIControl.ShowControl(GetID, DetailsImage.GetID)

                        DetailsTitel.Label = ClickfinderData.Item("Titel")
                        GUIControl.ShowControl(GetID, DetailsTitel.GetID)

                        DetailsBeschreibung.Label = ClickfinderData.Item("Beschreibung")
                        GUIControl.ShowControl(GetID, DetailsBeschreibung.GetID)

                        ReadTvServerDB("Select * from tvmoviemapping Inner Join channel on tvmoviemapping.idChannel = channel.idChannel where stationName = '" & ClickfinderData.Item("SenderKennung").ToString & "'")

                        While TvServerData.Read
                            DetailsKanal.Label = TvServerData.Item("displayName")
                            GUIControl.ShowControl(GetID, DetailsKanal.GetID)
                            Exit While
                        End While
                        CloseTvServerDB()



                        DetailsZeit.Label = Format(CDate(ClickfinderData.Item("Beginn")).Hour, "00") & _
                                            ":" & Format(CDate(ClickfinderData.Item("Beginn")).Minute, "00") & _
                                            " - " & Format(CDate(ClickfinderData.Item("Ende")).Hour, "00") & _
                                            ":" & Format(CDate(ClickfinderData.Item("Ende")).Minute, "00")
                        GUIControl.ShowControl(GetID, DetailsZeit.GetID)

                        DetailsGenre.Label = ClickfinderData.Item("Genre")
                        GUIControl.ShowControl(GetID, DetailsGenre.GetID)

                        DetailsActors.Label = ClickfinderData.Item("Darsteller")
                        GUIControl.ShowControl(GetID, DetailsActors.GetID)

                        DetailsOrgTitel.Label = ClickfinderData.Item("Originaltitel")
                        GUIControl.ShowControl(GetID, DetailsOrgTitel.GetID)

                        DetailsRegie.Label = ClickfinderData.Item("Regie")
                        GUIControl.ShowControl(GetID, DetailsRegie.GetID)

                        DetailsYearLand.Label = ClickfinderData.Item("Herstellungsland") & " " & ClickfinderData.Item("Herstellungsjahr")
                        GUIControl.ShowControl(GetID, DetailsYearLand.GetID)

                        DetailsDauer.Label = ClickfinderData.Item("Dauer")
                        GUIControl.ShowControl(GetID, DetailsDauer.GetID)

                        DetailsKurzKritik.Label = ClickfinderData.Item("Kurzkritik")
                        GUIControl.ShowControl(GetID, DetailsKurzKritik.GetID)

                        DetailsBewertungen.Label = Replace(ClickfinderData.Item("Bewertungen"), ";", " ")
                        GUIControl.ShowControl(GetID, DetailsBewertungen.GetID)

                        'Rating Image bereitstellen

                        Select Case ClickfinderData.Item("Bewertung")
                            'Movie Rating
                            Case Is = 4
                                RatingImage = GUIGraphicsContext.Skin & "\Media\ClickfinderPG_R3.png"
                            Case Is = 3
                                RatingImage = GUIGraphicsContext.Skin & "\Media\ClickfinderPG_R3.png"
                            Case Is = 2
                                RatingImage = GUIGraphicsContext.Skin & "\Media\ClickfinderPG_R2.png"
                            Case Is = 1
                                RatingImage = GUIGraphicsContext.Skin & "\Media\ClickfinderPG_R1.png"
                            Case Is = 0
                                RatingImage = Nothing

                                'Rest Rating
                            Case Is = 5
                                RatingImage = GUIGraphicsContext.Skin & "\Media\ClickfinderPG_R2.png"
                            Case Is = 6
                                RatingImage = GUIGraphicsContext.Skin & "\Media\ClickfinderPG_R3.png"
                        End Select

                        DetailsRatingImage.FileName = RatingImage
                        GUIControl.ShowControl(GetID, DetailsRatingImage.GetID)

                    End While

                    CloseClickfinderDB()
            End Select

        End Sub

        'ProgresBar paralell anzeigen
        Private Sub ShowProgressbar()
            ctlProgressBar.Visible = True
        End Sub

#End Region

#Region "Functions and Subs"






        Private Sub ClickfinderCorrection()
            Dim _idChannel As String
            Dim _StartZeit As Date
            Dim _EndZeit As Date
            Dim _StartZeitSQL As String
            Dim _EndZeitSQL As String
            Dim _Titel As String


            Try
                ReadClickfinderDB("SELECT * FROM Sendungen WHERE (KzLive = true AND KzWiederholung = true) OR KzLive = true OR KzWiederholung = true")

                While ClickfinderData.Read

                    _Titel = ClickfinderData.Item("Titel")
                    _StartZeit = CDate(ClickfinderData.Item("Beginn"))
                    _EndZeit = CDate(ClickfinderData.Item("Ende"))

                    _StartZeitSQL = DateTOMySQLstring(_StartZeit)

                    _EndZeitSQL = DateTOMySQLstring(_EndZeit)



                    ReadTvServerDB("Select * from tvmoviemapping Inner Join channel on tvmoviemapping.idChannel = channel.idChannel where stationName = '" & ClickfinderData.Item("SenderKennung").ToString & "'")
                    While TvServerData.Read

                        _idChannel = TvServerData.Item("idChannel")

                        Exit While
                    End While
                    CloseTvServerDB()

                    ReadTvServerDB("SELECT * FROM program WHERE idChannel = '" & _idChannel & "' AND startTime = " & _StartZeitSQL & " AND endTime = " & _EndZeitSQL)
                    While TvServerData.Read

                        If InStr(TvServerData.Item("title"), "(LIVE)") > 0 Then
                            LiveCorrection = True
                            Log.Info("Clickfinder ProgramGuide: [ClickfinderCorrection]: LIVE = true")
                        ElseIf InStr(TvServerData.Item("title"), "(Wdh.)") > 0 Then
                            WdhCorrection = True
                            Log.Info("Clickfinder ProgramGuide: [ClickfinderCorrection]: Wdh. = true")
                        Else
                            Log.Info("Clickfinder ProgramGuide: [ClickfinderCorrection]: nothing")
                        End If
                        Exit While
                    End While
                    CloseTvServerDB()

                    Exit While
                End While
                CloseClickfinderDB()

            Catch ex As Exception
                Log.Debug("Clickfinder ProgramGuide: [ClickfinderCorrection]: " & ex.Message)
            End Try

        End Sub
        Private Sub FillTipps(ByVal StartIdofGroup As Integer, ByVal _Titel As String, ByVal _FavImagePath As String, _
            ByVal _channelName As String, ByVal _StartZeit As Date, ByVal _EndZeit As Date, ByVal _Genre As String, ByVal _BewertungStr As String, _
            ByVal _Kritik As String, ByVal _FavRatingImagePath As String)

            Dim _Bilddatei As String

            _Bilddatei = _FavImagePath

            Try

                'Titel Tipp Labels
                GUIFadeLabel.SetControlLabel(GetID, StartIdofGroup, _Titel)


                'Titel Tipp Image
                If _FavImagePath = "" Then
                    _GuiImage(StartIdofGroup + 1).KeepAspectRatio = True
                    _GuiImage(StartIdofGroup + 1).Centered = True

                    'Premier League
                    _Bilddatei = Config.GetFile(Config.Dir.Thumbs, "tv\logos\" & _channelName & ".png")

                    If InStr(_Titel, "Bundesliga") Then _Bilddatei = Config.GetFile(Config.Dir.Thumbs, "tv\logos\Bundesliga.png")
                    If InStr(_Titel, "2. Bundesliga") Then _Bilddatei = Config.GetFile(Config.Dir.Thumbs, "tv\logos\2. Liga.png")
                    If InStr(_Titel, "DFB-Pokal") Then _Bilddatei = Config.GetFile(Config.Dir.Thumbs, "tv\logos\DFB Konferenz.png")
                    If InStr(_Titel, "Champions League") Then _Bilddatei = Config.GetFile(Config.Dir.Thumbs, "tv\logos\Champ. League.png")
                    If InStr(_Titel, "Europa League") Then _Bilddatei = Config.GetFile(Config.Dir.Thumbs, "tv\logos\Europa League.png")

                    If InStr(_Titel, "Premier League") Then _Bilddatei = Config.GetFile(Config.Dir.Thumbs, "tv\logos\Fußball ENG.png")

                    If InStr(_Titel, "Eishockey: DEL") Then _Bilddatei = Config.GetFile(Config.Dir.Thumbs, "tv\logos\Eishockey DEL.png")
                    If InStr(_Titel, "Formel 1") Then _Bilddatei = Config.GetFile(Config.Dir.Thumbs, "tv\logos\Formel 1.png")

                Else
                    _GuiImage(StartIdofGroup + 1).KeepAspectRatio = False
                    _GuiImage(StartIdofGroup + 1).Centered = False
                    _Bilddatei = _FavImagePath
                End If

                _GuiImage(StartIdofGroup + 1).SetFileName(_Bilddatei)
                GUIImage.ShowControl(GetID, StartIdofGroup + 1)
                'GUIImage.RefreshControl(GetID, StartIdofGroup + 1)

                'Titel Kanal Labels
                GUIFadeLabel.SetControlLabel(GetID, StartIdofGroup + 2, _channelName)


                'Titel Genre Labels
                GUIFadeLabel.SetControlLabel(GetID, StartIdofGroup + 3, _Genre)

                'Titel Zeit Labels
                If _StartZeit = Nothing And _EndZeit = Nothing Then
                    GUILabelControl.SetControlLabel(GetID, StartIdofGroup + 4, "")
                Else
                    GUILabelControl.SetControlLabel(GetID, StartIdofGroup + 4, _
                                                    Format(_StartZeit.Hour, "00") & _
                                                    ":" & Format(_StartZeit.Minute, "00") & _
                                                    " - " & Format(_EndZeit.Hour, "00") & _
                                                    ":" & Format(_EndZeit.Minute, "00"))
                End If



                'Titel Bewertung Labels
                GUIFadeLabel.SetControlLabel(GetID, StartIdofGroup + 5, _BewertungStr)

                'Titel Kritik Labels
                GUIFadeLabel.SetControlLabel(GetID, StartIdofGroup + 6, _Kritik)

                'Titel Rating Image Path
                _GuiImage(StartIdofGroup + 7).SetFileName(_FavRatingImagePath)
                GUIImage.ShowControl(GetID, StartIdofGroup + 7)

                Log.Debug("Clickfinder ProgramGuide: [FillTipps]: " & _Titel & " " & _FavImagePath & " " & _channelName & " " & _Genre & " " & _StartZeit & " " & _EndZeit & " " & _BewertungStr & " " & _Kritik & " " & _FavRatingImagePath)

            Catch ex As Exception
                Log.Debug("Clickfinder ProgramGuide: [FillTipps]: " & ex.Message)
                MsgBox(ex.Message)
            End Try

        End Sub
        Private Sub ClearTipps()

            Dim _TippsCounter As Integer

            _TippsCounter = _idStartCounter

            Try

                Do
                    FillTipps(_TippsCounter, Nothing, "", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "")

                    _TippsCounter = _TippsCounter + 10
                Loop Until _TippsCounter = _idStoppCounter

            Catch ex As Exception
                Log.Debug("Clickfinder ProgramGuide: [ClearTipps]: " & ex.Message)
            End Try

        End Sub








#End Region


#Region "TV Movie and TVServer Database Access and Function"

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
                    Log.Debug("Clickfinder ProgramGuide: (Clickfinder DB, read) " & ex.Message)
                    MsgBox("Clickfinder ProgramGuide: (Clickfinder DB, read) " & ex.Message)
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
                Log.Debug("Clickfinder ProgramGuide: (Clickfinder DB, close) " & ex.Message)
                MsgBox("Clickfinder ProgramGuide: (Clickfinder DB, close) " & ex.Message)
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

                Log.Debug("Clickfinder ProgramGuide: [TvServer DB, read] " & ex.Message)
                MsgBox("Clickfinder ProgramGuide: (TvServer DB, read) " & ex.Message)

                CmdTvServerDBRead.Dispose()
                ConTvServerDBRead.Close()
            End Try


        End Sub
        Public Sub CloseTvServerDB()

            Try
                CmdTvServerDBRead.Dispose()
                ConTvServerDBRead.Close()
            Catch ex As Exception
                Log.Debug("Clickfinder ProgramGuide: (TvServer DB, close) " & ex.Message)
                MsgBox("Clickfinder ProgramGuide: (TvServer DB, close) " & ex.Message)
            End Try

        End Sub

        Private Function ProgramFoundinTvDb(ByVal _Titel As String, ByVal _idChannel As String, ByVal _StartZeit As Date, ByVal _EndZeit As Date) As Boolean
            'MP - TVServer Datenbank Variablen  - MYSql
            Dim Con As New MySqlConnection
            Dim Cmd As New MySqlCommand
            Dim DataCount As Long
            Dim _SQLString As String


            'SELECT COUNT(*) FROM program WHERE Title = 'Tagesschau' AND idChannel = 116 AND startTime =" & DateTOMySQLstring(Start) & " AND endTime =" & DateTOMySQLstring(Ende))

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

        Private Function SQLQueryAccess(ByVal StartZeitHour As Double, ByVal StartZeitMinute As Double, _
                                               Optional ByVal AppendWhereSQLCmd As String = "", _
                                               Optional ByVal OrderBySQLCmd As String = "") As String

            Dim _StartZeit, _EndZeit As Date

            'Umwandeln von Start- & EndZeit in Access Date SQL String
            _StartZeit = Today.AddHours(StartZeitHour).AddMinutes(StartZeitMinute)
            _EndZeit = _StartZeit.AddHours(4)

            OrderBySQLCmd = " ORDER BY " & OrderBySQLCmd

            SQLQueryAccess = "Select * from Sendungen where (Beginn Between " & DateToAccessSQLstring(_StartZeit) & " AND " & DateToAccessSQLstring(_EndZeit) & ") " & AppendWhereSQLCmd & OrderBySQLCmd

        End Function




#End Region

#Region "MediaPortal Funktionen / Dialogs"

        'xml Setting Datei lesen
        Public Function MPSettingRead(ByVal section As String, ByVal entry As String) As String
            Using xmlReader As New Settings(Config.GetFile(Config.Dir.Config, "ClickfinderPGConfig.xml"))
                MPSettingRead = xmlReader.GetValue(section, entry)
            End Using
        End Function

        'MediaPortal Dialoge

        Private Sub AddListControlItem(ByVal Label As String, Optional ByVal label2 As String = "", Optional ByVal label3 As String = "", Optional ByVal ImagePath As String = "")

            Dim lItem As New GUIListItem

            lItem.Label = Label
            lItem.Label2 = label2
            lItem.Label3 = label3
            lItem.ItemId = ctlList.ListItems.Count - 1
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




        Public Shared Function ListByNameStartsWith(ByVal idChannel As Integer) As IList
            Dim sb As SqlBuilder = New SqlBuilder(StatementType.Select, GetType(GroupMap))
            ' note: the partialName parameter must also contain the %'s for the LIKE query!
            sb.AddConstraint([Operator].Like, "idGroup", idChannel)
            ' passing true indicates that we'd like a list of elements, i.e. that no primary key
            ' constraints from the type being retrieved should be added to the statement
            Dim stmt As SqlStatement = sb.GetStatement(StatementType.Select)
            ' execute the statement/query and create a collection of User instances from the result
            Return ObjectFactory.GetCollection(GetType(GroupMap), stmt.Execute)
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


