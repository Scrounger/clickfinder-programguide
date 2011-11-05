Imports System
Imports System.IO
Imports System.Windows.Forms
Imports MediaPortal.GUI.Library
Imports MediaPortal.Dialogs
Imports MediaPortal.Profile
Imports MediaPortal.Configuration
Imports MediaPortal.Utils
Imports MediaPortal.Util


Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Threading



Namespace OurPlugin
    Public Class Class1
        Inherits MediaPortal.GUI.Library.GUIWindow
        Implements MediaPortal.GUI.Library.ISetupForm

        Public Delegate Sub D_Parallel()

#Region "Skin Controls"

        <SkinControlAttribute(2)> Protected btnPrimeTime As GUIButtonControl = Nothing
        <SkinControlAttribute(3)> Protected btnLateTime As GUIButtonControl = Nothing
        <SkinControlAttribute(4)> Protected btnCommingTipps As GUIButtonControl = Nothing

        <SkinControlAttribute(9)> Protected ctlProgressBar As GUIAnimation = Nothing
        <SkinControlAttribute(10)> Protected ctlList As GUIListControl = Nothing

        <SkinControlAttribute(20)> Protected ctlTippImage As GUIImage = Nothing
        <SkinControlAttribute(21)> Protected ctlTippTitel As GUIFadeLabel = Nothing
        <SkinControlAttribute(22)> Protected ctlTippBeschreibung As GUITextScrollUpControl = Nothing
        <SkinControlAttribute(23)> Protected ctlTippKanal As GUIFadeLabel = Nothing
        <SkinControlAttribute(24)> Protected ctlTippZeit As GUILabelControl = Nothing
        <SkinControlAttribute(25)> Protected ctlTippGenre As GUILabelControl = Nothing
        <SkinControlAttribute(26)> Protected ctlTippActors As GUIFadeLabel = Nothing
        <SkinControlAttribute(27)> Protected ctlTippKanalImage As GUIImage = Nothing

        <SkinControlAttribute(30)> Protected ListImage As GUIImage = Nothing
        <SkinControlAttribute(31)> Protected ListTitel As GUIFadeLabel = Nothing
        <SkinControlAttribute(32)> Protected ListBeschreibung As GUITextScrollUpControl = Nothing
        <SkinControlAttribute(33)> Protected ListKanal As GUIFadeLabel = Nothing
        <SkinControlAttribute(34)> Protected ListZeit As GUIFadeLabel = Nothing
        <SkinControlAttribute(35)> Protected ListGenre As GUIFadeLabel = Nothing
        <SkinControlAttribute(36)> Protected ListActors As GUIFadeLabel = Nothing
        <SkinControlAttribute(37)> Protected ListRatingImage As GUIImage = Nothing
        <SkinControlAttribute(38)> Protected ListOrgTitel As GUIFadeLabel = Nothing
        <SkinControlAttribute(39)> Protected ListRegie As GUIFadeLabel = Nothing
        <SkinControlAttribute(40)> Protected ListYearLand As GUIFadeLabel = Nothing
        <SkinControlAttribute(41)> Protected ListDauer As GUIFadeLabel = Nothing
        <SkinControlAttribute(42)> Protected ListKurzKritik As GUIFadeLabel = Nothing
        <SkinControlAttribute(43)> Protected ListBewertungen As GUIFadeLabel = Nothing




#End Region
#Region "Variablen"
        Public ListSQLString As String
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

            strButtonImage = "ClickfinderPG_R3.png"

            strButtonImageFocus = String.Empty

            strPictureImage = String.Empty

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

            ctlProgressBar.Visible = False


            ShowTagesTipp()
            Button_PrimeTime()

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
                        ShowListItemDetails()
                    End If
                End If
            End If


        End Sub


        Protected Overrides Sub OnClicked(ByVal controlId As Integer, _
                                          ByVal control As GUIControl, _
                                          ByVal actionType As  _
                                          MediaPortal.GUI.Library.Action.ActionType)

            MyBase.OnClicked(controlId, control, actionType)

            If control Is btnPrimeTime Then
                Button_PrimeTime()
            End If
            If control Is btnLateTime Then
                Button_LateTime()
            End If
            If control Is ctlList Then

            End If
            If control Is btnCommingTipps Then
                Button_CommingTipps()
            End If

        End Sub


        Private Sub Button_PrimeTime()
            Dim PrimeTimeSQL As String
            Dim EndofDay As Date
            Dim EndofDaySQL As String
            Dim Rating As String


            Rating = MPSettingRead("config", "ClickfinderRating")

            EndofDay = Today.AddDays(1)

            PrimeTimeSQL = "#" & Today.Year & "-" & Format(Today.Month, "00") & "-" & Format(Today.Day, "00") & " 20:15:00#"
            EndofDaySQL = "#" & EndofDay.Year & "-" & Format(EndofDay.Month, "00") & "-" & Format(EndofDay.Day, "00") & " 00:00:00#"


            StartFillListcontrol("Select * from Sendungen where (Beginn Between " & PrimeTimeSQL & " AND " & EndofDaySQL & ") AND Bewertung >= " & Rating & " AND Bewertung <=4 ORDER BY Beginn ASC, Bewertung DESC, Titel")  'AND Bewertung <=4 ORDER BY Beginn ASC, Bewertung DESC"

        End Sub

        Private Sub Button_LateTime()

            Dim LateTimeSQL As String
            Dim EndofDay As Date

            Dim EndofDaySQL As String
            Dim Rating As String


            Rating = MPSettingRead("config", "ClickfinderRating")

            EndofDay = Today.AddDays(1)

            LateTimeSQL = "#" & Today.Year & "-" & Format(Today.Month, "00") & "-" & Format(Today.Day, "00") & " 22:00:00#"
            EndofDaySQL = "#" & EndofDay.Year & "-" & Format(EndofDay.Month, "00") & "-" & Format(EndofDay.Day, "00") & " 02:00:00#"

            StartFillListcontrol("Select * from Sendungen where (Beginn Between " & LateTimeSQL & " AND " & EndofDaySQL & ") AND Bewertung >= " & Rating & " AND Bewertung <=4 ORDER BY Beginn ASC, Bewertung DESC, Titel ASC")

        End Sub

        Private Sub Button_CommingTipps()

            StartFillListControlCommingNextTipps()

        End Sub

#End Region

#Region "TV Movie and TVServer Database Access"

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
            Dim TvServerAdress As String
            Dim TvServerUser As String
            Dim TvServerPW As String

            TvServerAdress = MPSettingRead("config", "TVServerAddress")
            TvServerUser = MPSettingRead("config", "TVServerUser")
            TvServerPW = MPSettingRead("config", "TVServerPW")
            Try

                ConTvServerDBRead.ConnectionString = "server=" & TvServerAdress & ";uid=" & TvServerUser & ";pwd=" & TvServerPW & ";database=mptvdb;"
                ConTvServerDBRead.Open()


                CmdTvServerDBRead = ConTvServerDBRead.CreateCommand
                CmdTvServerDBRead.CommandText = SQLString

                TvServerData = CmdTvServerDBRead.ExecuteReader


            Catch ex As Exception

                MPDialogNotify("Warnung ...", "TV Server nicht gefunden")

                Log.Debug("Clickfinder ProgramGuide: (TvServer DB, read) " & ex.Message)
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


#End Region


#Region "ListControl mit Daten füttern"

        'List Controll mit Daten zur bestimmter Uhrzeit (SQLString) - paralelles Threating
        Private Sub StartFillListControl(ByVal SQLString As String)

            Dim ProgressBar, Fill_Listcontrol As Threading.Thread

            ListSQLString = SQLString

            ProgressBar = New Thread(AddressOf ShowProgressbar)
            Fill_Listcontrol = New Thread(AddressOf FillListcontrol)

            ProgressBar.Start()
            Fill_Listcontrol.Start()
        End Sub
        Private Sub FillListControl()
            ctlList.ListItems.Clear()

            ReadClickfinderDB(ListSQLString)

            While ClickfinderData.Read

                Dim lItem As New GUIListItem
                lItem.Label = ClickfinderData.Item("Titel")
                lItem.Label2 = Format(CDate(ClickfinderData.Item("Beginn")).Hour, "00") & _
                ":" & Format(CDate(ClickfinderData.Item("Beginn")).Minute, "00") & _
                " - " & Format(CDate(ClickfinderData.Item("Ende")).Hour, "00") & _
                ":" & Format(CDate(ClickfinderData.Item("Ende")).Minute, "00")
                lItem.Label3 = ClickfinderData.Item("Genre").ToString
                lItem.ItemId = ctlList.ListItems.Count - 1

                ReadTvServerDB("Select * from tvmoviemapping Inner Join channel on tvmoviemapping.idChannel = channel.idChannel where stationName = '" & ClickfinderData.Item("SenderKennung").ToString & "'")

                While TvServerData.Read

                    lItem.IconImage = Config.GetFile(Config.Dir.Thumbs, "tv\logos\" & TvServerData.Item("displayName").ToString & ".png")
                    GUIControl.AddListItemControl(GetID, ctlList.GetID, lItem)
                    Exit While
                End While
                CloseTvServerDB()


            End While

            CloseClickfinderDB()

            ctlProgressBar.Visible = False

        End Sub

        'List Controll mit Daten der kommenden TagesTipps füllen - paralleles Threating
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
            Application.DoEvents()


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

        'ProgresBar paralell anzeigen
        Private Sub ShowProgressbar()
            ctlProgressBar.Visible = True
        End Sub

#End Region

#Region "Functions and Subs"

        'Funktion um Daten aus der ClickfinderPGConfig.xml abzufragen
        Public Function MPSettingRead(ByVal section As String, ByVal entry As String) As String
            Using xmlReader As New Settings(Config.GetFile(Config.Dir.Config, "ClickfinderPGConfig.xml"))
                MPSettingRead = xmlReader.GetValue(section, entry)
            End Using
        End Function

        'Parameter des Tagestipps anzeigen
        Private Sub ShowTagesTipp()
            Dim DayStart As String
            Dim DayEnd As String
            Dim ClickfinderPath As String


            DayStart = "#" & Today.Year & "-" & Format(Today.Month, "00") & "-" & Format(Today.Day, "00") & " 00:01:00#"
            DayEnd = "#" & Today.Year & "-" & Format(Today.Month, "00") & "-" & Format(Today.Day, "00") & " 23:59:00#"

            ClickfinderPath = MPSettingRead("config", "ClickfinderPath")


            ReadClickfinderDB("Select * from Sendungen Inner Join SendungenDetails on Sendungen.Pos = SendungenDetails.Pos where (Beginn Between " & DayStart & " AND " & DayEnd & ") AND Bewertung = 4 ORDER BY Beginn")


            While ClickfinderData.Read

                ctlTippImage.FileName = ClickfinderPath & "\Hyperlinks\" & ClickfinderData.Item("Bilddateiname")
                GUIControl.ShowControl(GetID, ctlTippImage.GetID)

                ctlTippTitel.Label = ClickfinderData.Item("Titel")
                GUIControl.ShowControl(GetID, ctlTippTitel.GetID)

                ctlTippBeschreibung.Label = ClickfinderData.Item("Beschreibung")
                GUIControl.ShowControl(GetID, ctlTippBeschreibung.GetID)

                ctlTippKanal.Label = ClickfinderData.Item("SenderKennung")
                GUIControl.ShowControl(GetID, ctlTippKanal.GetID)

                ctlTippZeit.Label = Format(CDate(ClickfinderData.Item("Beginn")).Hour, "00") & _
                    ":" & Format(CDate(ClickfinderData.Item("Beginn")).Minute, "00") & _
                    " - " & Format(CDate(ClickfinderData.Item("Ende")).Hour, "00") & _
                    ":" & Format(CDate(ClickfinderData.Item("Ende")).Minute, "00")
                GUIControl.ShowControl(GetID, ctlTippZeit.GetID)

                ctlTippGenre.Label = ClickfinderData.Item("Genre")
                GUIControl.ShowControl(GetID, ctlTippGenre.GetID)

                ctlTippActors.Label = ClickfinderData.Item("Darsteller")
                GUIControl.ShowControl(GetID, ctlTippActors.GetID)

                ctlTippKanalImage.FileName = Config.GetFile(Config.Dir.Thumbs, "tv\logos\" & ClickfinderData.Item("SenderKennung").ToString & ".png")
                GUIControl.ShowControl(GetID, ctlTippKanalImage.GetID)

            End While

            CloseClickfinderDB()

        End Sub


        Private Sub ShowListItemDetails()
            Dim Titel As String
            Dim Kategorie As String
            Dim StartTime As String
            Dim ProofDate As Integer
            Dim SQLDateString As String
            Dim Heute As Date
            Dim ClickfinderPath As String
            Dim RatingImage As String
            Dim AktuelleZeit As Date




            Titel = Replace(ctlList.SelectedListItem.Label.ToString, "'", "''")
            Kategorie = ctlList.SelectedListItem.Label3.ToString
            StartTime = Left(ctlList.SelectedListItem.Label2.ToString, InStr(ctlList.SelectedListItem.Label2.ToString, "-") - 2)
            ProofDate = Left(StartTime, InStr(StartTime, ":") - 1)
            RatingImage = Nothing

            ClickfinderPath = MPSettingRead("config", "ClickfinderPath")

            AktuelleZeit = Now


            If AktuelleZeit.Hour >= 0 And AktuelleZeit.Hour <= 2 Then

                Heute = Today
                SQLDateString = "#" & Heute.Year & "-" & Format(Heute.Month, "00") & "-" & Format(Heute.Day, "00") & " " & StartTime & ":00#"
            Else
                'Datum.Day +1 ab 0:00h
                If ProofDate >= 0 And ProofDate <= 2 Then
                    Heute = Today.AddDays(1)
                    SQLDateString = "#" & Heute.Year & "-" & Format(Heute.Month, "00") & "-" & Format(Heute.Day, "00") & " " & StartTime & ":00#"
                Else
                    Heute = Today
                    SQLDateString = "#" & Heute.Year & "-" & Format(Heute.Month, "00") & "-" & Format(Heute.Day, "00") & " " & StartTime & ":00#"
                End If
            End If


            ReadClickfinderDB("Select * from Sendungen Inner Join SendungenDetails on Sendungen.Pos = SendungenDetails.Pos where Beginn = " & SQLDateString & " AND Titel = '" & Titel & "' and Genre = '" & Kategorie & "'")

            While ClickfinderData.Read

                ListImage.FileName = ClickfinderPath & "\Hyperlinks\" & ClickfinderData.Item("Bilddateiname")
                GUIControl.ShowControl(GetID, ListImage.GetID)

                ListTitel.Label = ClickfinderData.Item("Titel")
                GUIControl.ShowControl(GetID, ListTitel.GetID)

                ListBeschreibung.Label = ClickfinderData.Item("Beschreibung")
                GUIControl.ShowControl(GetID, ListBeschreibung.GetID)

                ReadTvServerDB("Select * from tvmoviemapping Inner Join channel on tvmoviemapping.idChannel = channel.idChannel where stationName = '" & ClickfinderData.Item("SenderKennung").ToString & "'")

                While TvServerData.Read
                    ListKanal.Label = TvServerData.Item("displayName")
                    GUIControl.ShowControl(GetID, ListKanal.GetID)
                    Exit While
                End While
                CloseTvServerDB()



                ListZeit.Label = Format(CDate(ClickfinderData.Item("Beginn")).Hour, "00") & _
                                    ":" & Format(CDate(ClickfinderData.Item("Beginn")).Minute, "00") & _
                                    " - " & Format(CDate(ClickfinderData.Item("Ende")).Hour, "00") & _
                                    ":" & Format(CDate(ClickfinderData.Item("Ende")).Minute, "00")
                GUIControl.ShowControl(GetID, ListZeit.GetID)

                ListGenre.Label = ClickfinderData.Item("Genre")
                GUIControl.ShowControl(GetID, ListGenre.GetID)

                ListActors.Label = ClickfinderData.Item("Darsteller")
                GUIControl.ShowControl(GetID, ListActors.GetID)

                ListOrgTitel.Label = ClickfinderData.Item("Originaltitel")
                GUIControl.ShowControl(GetID, ListOrgTitel.GetID)

                ListRegie.Label = ClickfinderData.Item("Regie")
                GUIControl.ShowControl(GetID, ListRegie.GetID)

                ListYearLand.Label = ClickfinderData.Item("Herstellungsland") & " " & ClickfinderData.Item("Herstellungsjahr")
                GUIControl.ShowControl(GetID, ListYearLand.GetID)

                ListDauer.Label = ClickfinderData.Item("Dauer")
                GUIControl.ShowControl(GetID, ListDauer.GetID)

                ListKurzKritik.Label = ClickfinderData.Item("Kurzkritik")
                GUIControl.ShowControl(GetID, ListKurzKritik.GetID)

                ListBewertungen.Label = Replace(ClickfinderData.Item("Bewertungen"), ";", " ")
                GUIControl.ShowControl(GetID, ListBewertungen.GetID)

                'Rating Image bereitstellen

                Select Case ClickfinderData.Item("Bewertung")
                    Case Is = 3
                        RatingImage = GUIGraphicsContext.Skin & "\Media\ClickfinderPG_R3.png"
                    Case Is = 2
                        RatingImage = GUIGraphicsContext.Skin & "\Media\ClickfinderPG_R2.png"
                    Case Is = 1
                        RatingImage = GUIGraphicsContext.Skin & "\Media\ClickfinderPG_R1.png"
                    Case Is = 0
                        RatingImage = Nothing
                End Select

                ListRatingImage.FileName = RatingImage
                GUIControl.ShowControl(GetID, ListRatingImage.GetID)

            End While

            CloseClickfinderDB()

        End Sub







#End Region

#Region "MediaPortal Funktionen / Dialogs"

        'MediaPortal Dialoge
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
#End Region







    End Class
End Namespace

