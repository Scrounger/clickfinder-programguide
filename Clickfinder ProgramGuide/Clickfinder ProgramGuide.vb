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



Imports TvPlugin

Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Threading

Imports Action = MediaPortal.GUI.Library.Action
Imports Gentle.Framework




Namespace OurPlugin
    Public Class Class1
        Inherits MediaPortal.GUI.Library.GUIWindow
        Implements MediaPortal.GUI.Library.ISetupForm

        Public Delegate Sub D_Parallel()

#Region "Skin Controls"

        <SkinControlAttribute(2)> Protected btnPrimeTime As GUIButtonControl = Nothing
        <SkinControlAttribute(3)> Protected btnPrimeTimeRest As GUIButtonControl = Nothing
        <SkinControlAttribute(4)> Protected btnLateTime As GUIButtonControl = Nothing
        <SkinControlAttribute(5)> Protected btnLateTimeRest As GUIButtonControl = Nothing
        <SkinControlAttribute(6)> Protected btnCommingTipps As GUIButtonControl = Nothing


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

        <SkinControlAttribute(50)> Protected FavTitel1 As GUIFadeLabel = Nothing
        <SkinControlAttribute(51)> Protected FavImage1 As GUIImage = Nothing

        <SkinControlAttribute(60)> Protected FavTitel2 As GUIFadeLabel = Nothing
        <SkinControlAttribute(61)> Protected FavImage2 As GUIImage = Nothing

        <SkinControlAttribute(70)> Protected FavTitel3 As GUIFadeLabel = Nothing
        <SkinControlAttribute(71)> Protected FavImage3 As GUIImage = Nothing

        <SkinControlAttribute(80)> Protected FavTitel4 As GUIFadeLabel = Nothing
        <SkinControlAttribute(81)> Protected FavImage4 As GUIImage = Nothing

        <SkinControlAttribute(89)> Protected btnRemember As GUIButtonControl = Nothing
        <SkinControlAttribute(90)> Protected btnBack As GUIButtonControl = Nothing
        <SkinControlAttribute(91)> Protected btnRecord As GUIButtonControl = Nothing


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
            DetailsImage.Visible = False


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

            If control Is btnPrimeTime Then
                Button_PrimeTime()
            End If
            If control Is btnPrimeTimeRest Then
                Button_PrimeTimeRest()
            End If
            If control Is btnLateTime Then
                Button_LateTime()
            End If
            If control Is btnLateTimeRest Then
                Button_LateTimeRest()
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


        Private Sub Button_PrimeTime()
            Dim PrimeTimeSQL As String
            Dim EndofDay As Date
            Dim EndofDaySQL As String
            Dim Rating As String


            Rating = MPSettingRead("config", "ClickfinderRating")

            EndofDay = Today.AddDays(1)

            PrimeTimeSQL = "#" & Today.Year & "-" & Format(Today.Month, "00") & "-" & Format(Today.Day, "00") & " 20:15:00#"
            EndofDaySQL = "#" & EndofDay.Year & "-" & Format(EndofDay.Month, "00") & "-" & Format(EndofDay.Day, "00") & " 00:00:00#"


            StartFillListControl_new("Select * from Sendungen where (Beginn Between " & PrimeTimeSQL & " AND " & EndofDaySQL & ") AND Bewertung >= " & Rating & " AND Bewertung <=4 ORDER BY Beginn ASC, Bewertung DESC, Titel")  'AND Bewertung <=4 ORDER BY Beginn ASC, Bewertung DESC"

        End Sub

        Private Sub Button_PrimeTimeRest()
            Dim PrimeTimeSQL As String
            Dim EndofDay As Date
            Dim EndofDaySQL As String
            Dim Rating As String


            Rating = MPSettingRead("config", "ClickfinderRating")

            EndofDay = Today.AddDays(1)

            PrimeTimeSQL = "#" & Today.Year & "-" & Format(Today.Month, "00") & "-" & Format(Today.Day, "00") & " 20:15:00#"
            EndofDaySQL = "#" & EndofDay.Year & "-" & Format(EndofDay.Month, "00") & "-" & Format(EndofDay.Day, "00") & " 00:00:00#"


            StartFillListControl("Select * from Sendungen where (Beginn Between " & PrimeTimeSQL & " AND " & EndofDaySQL & ") AND Bewertung >= 5 AND Bewertung <=6 ORDER BY Beginn ASC, Bewertung DESC, Titel")  'AND Bewertung <=4 ORDER BY Beginn ASC, Bewertung DESC"

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

            StartFillListControl_new("Select * from Sendungen where (Beginn Between " & LateTimeSQL & " AND " & EndofDaySQL & ") AND Bewertung >= " & Rating & " AND Bewertung <=4 ORDER BY Beginn ASC, Bewertung DESC, Titel ASC")


        End Sub


        Private Sub Button_LateTimeRest()

            Dim LateTimeSQL As String
            Dim EndofDay As Date

            Dim EndofDaySQL As String
            Dim Rating As String


            Rating = MPSettingRead("config", "ClickfinderRating")

            EndofDay = Today.AddDays(1)

            LateTimeSQL = "#" & Today.Year & "-" & Format(Today.Month, "00") & "-" & Format(Today.Day, "00") & " 22:00:00#"
            EndofDaySQL = "#" & EndofDay.Year & "-" & Format(EndofDay.Month, "00") & "-" & Format(EndofDay.Day, "00") & " 02:00:00#"

            StartFillListControl("Select * from Sendungen where (Beginn Between " & LateTimeSQL & " AND " & EndofDaySQL & ") AND Bewertung >= 5 AND Bewertung <=6 ORDER BY Beginn ASC, Bewertung DESC, Titel ASC")

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

#Region "TV Movie and TVServer Database Access"

        Public ConClickfinderDBRead As New OleDb.OleDbConnection
        Public CmdClickfinderDBRead As New OleDb.OleDbCommand
        Public ClickfinderData As OleDb.OleDbDataReader

        'MP - TVServer Datenbank Variablen  - MYSql
        Public ConTvServerDBRead As New MySqlConnection
        Public CmdTvServerDBRead As New MySqlCommand
        Public TvServerData As MySqlDataReader

        'MP - TVServer Datenbank Variablen  - MYSql
        Public ConTvServerDBRead2 As New MySqlConnection
        Public CmdTvServerDBRead2 As New MySqlCommand
        Public TvServerData2 As MySqlDataReader

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

        Public Sub ReadTvServerDB2(ByVal SQLString As String)
            Dim TvServerAdress As String
            Dim TvServerUser As String
            Dim TvServerPW As String

            TvServerAdress = MPSettingRead("config", "TVServerAddress")
            TvServerUser = MPSettingRead("config", "TVServerUser")
            TvServerPW = MPSettingRead("config", "TVServerPW")
            Try

                ConTvServerDBRead2.ConnectionString = "server=" & TvServerAdress & ";uid=" & TvServerUser & ";pwd=" & TvServerPW & ";database=mptvdb;"
                ConTvServerDBRead2.Open()


                CmdTvServerDBRead2 = ConTvServerDBRead2.CreateCommand
                CmdTvServerDBRead2.CommandText = SQLString

                TvServerData2 = CmdTvServerDBRead.ExecuteReader


            Catch ex As Exception

                MPDialogNotify("Warnung ...", "TV Server nicht gefunden")

                Log.Debug("Clickfinder ProgramGuide: (TvServer DB, read) " & ex.Message)
                MsgBox("Clickfinder ProgramGuide: (TvServer DB, read) " & ex.Message)

                CmdTvServerDBRead2.Dispose()
                ConTvServerDBRead2.Close()
            End Try


        End Sub
        Public Sub CloseTvServerDB2()

            Try
                CmdTvServerDBRead2.Dispose()
                ConTvServerDBRead2.Close()
            Catch ex As Exception
                Log.Debug("Clickfinder ProgramGuide: (TvServer DB, close) " & ex.Message)
                MsgBox("Clickfinder ProgramGuide: (TvServer DB, close) " & ex.Message)
            End Try

        End Sub


#End Region


#Region "ListControl"

        'ListControl mit Daten zur bestimmter Uhrzeit (SQLString) - paralelles Threating
        Private Sub StartFillListControl(ByVal SQLString As String)

            Dim ProgressBar, Fill_Listcontrol As Threading.Thread

            ListSQLString = SQLString

            ProgressBar = New Thread(AddressOf ShowProgressbar)
            Fill_Listcontrol = New Thread(AddressOf FillListControl)

            ProgressBar.Start()
            Fill_Listcontrol.Start()
        End Sub
        Private Sub FillListControl()



            ctlList.ListItems.Clear()

            ReadClickfinderDB(ListSQLString)

            While ClickfinderData.Read
                'MsgBox(ClickfinderData.Item("Titel"))
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

        Private Sub StartFillListControl_new(ByVal SQLString As String)

            Dim ProgressBar, Fill_Listcontrol2 As Threading.Thread

            ListSQLString = SQLString

            ProgressBar = New Thread(AddressOf ShowProgressbar)
            Fill_Listcontrol2 = New Thread(AddressOf FillListControl_new)

            ProgressBar.Start()
            Fill_Listcontrol2.Start()
        End Sub
        Private Sub FillListControl_new()

            Dim _MappingName As String
            Dim _idChannel As String
            Dim _StartZeit As Date
            Dim _EndZeit As Date
            Dim _Titel As String
            Dim _Genre As String
            Dim _lastTitel As String
            Dim _FavCounter As Integer
            Dim _Bewertung As Integer
            Dim _inFav As Boolean = False
            Dim ClickfinderPath As String

            ClickfinderPath = MPSettingRead("config", "ClickfinderPath")
            _lastTitel = Nothing

            _FavCounter = 1

            ctlList.ListItems.Clear()

            ReadClickfinderDB(ListSQLString)

            While ClickfinderData.Read

                _MappingName = ClickfinderData.Item("SenderKennung")
                _StartZeit = CDate(ClickfinderData.Item("Beginn"))
                _EndZeit = CDate(ClickfinderData.Item("Ende"))
                _Titel = ClickfinderData.Item("Titel")
                _Genre = ClickfinderData.Item("Genre")
                _Bewertung = CInt(ClickfinderData.Item("Bewertung"))


                Dim _TVMovieMapping = TvDatabase.TvMovieMapping.ListAll


                For i = 0 To _TVMovieMapping.Count - 1

                    If _TVMovieMapping.Item(i).StationName = _MappingName Then

                        _idChannel = _TVMovieMapping.Item(i).IdChannel

                        Dim Sendung = TvDatabase.Program.RetrieveByTitleTimesAndChannel(_Titel, _StartZeit, _EndZeit, _idChannel)

                        Dim _GroupMap = TvDatabase.GroupMap.ListAll

                        For d = 0 To _GroupMap.Count - 1
                            If _GroupMap.Item(d).IdChannel = CInt(_idChannel) _
                            And _GroupMap.Item(d).IdGroup = CInt(MPSettingRead("config", "ChannelGroupID")) _
                            And Not Sendung.Title = ctlTippTitel.Label Then
                                _inFav = True
                                Exit For
                            End If
                        Next

                        If _inFav = True And _FavCounter <= 4 Then

                            Select Case _FavCounter
                                Case Is = 1
                                    FavTitel1.Label = Sendung.Title
                                    GUIControl.ShowControl(GetID, FavTitel1.GetID)
                                    FavImage1.FileName = ClickfinderPath & "\Hyperlinks\" & ClickfinderData.Item("Bilddateiname")
                                    GUIControl.ShowControl(GetID, FavImage1.GetID)
                                Case Is = 2
                                    FavTitel2.Label = Sendung.Title
                                    GUIControl.ShowControl(GetID, FavTitel2.GetID)
                                    FavImage2.FileName = ClickfinderPath & "\Hyperlinks\" & ClickfinderData.Item("Bilddateiname")
                                    GUIControl.ShowControl(GetID, FavImage2.GetID)
                                Case Is = 3
                                    FavTitel3.Label = Sendung.Title
                                    GUIControl.ShowControl(GetID, FavTitel3.GetID)
                                    FavImage3.FileName = ClickfinderPath & "\Hyperlinks\" & ClickfinderData.Item("Bilddateiname")
                                    GUIControl.ShowControl(GetID, FavImage3.GetID)
                                Case Is = 4
                                    FavTitel4.Label = Sendung.Title
                                    GUIControl.ShowControl(GetID, FavTitel4.GetID)
                                    FavImage4.FileName = ClickfinderPath & "\Hyperlinks\" & ClickfinderData.Item("Bilddateiname")
                                    GUIControl.ShowControl(GetID, FavImage4.GetID)
                            End Select

                            _FavCounter = _FavCounter + 1

                        ElseIf Not Sendung.Title = _lastTitel Then

                            Dim lItem As New GUIListItem

                            lItem.Label = Sendung.Title.ToString
                            lItem.Label2 = Format(CDate(Sendung.StartTime).Hour, "00") & _
                            ":" & Format(CDate(Sendung.StartTime).Minute, "00") & _
                            " - " & Format(CDate(Sendung.EndTime).Hour, "00") & _
                            ":" & Format(CDate(Sendung.EndTime).Minute, "00")
                            lItem.Label3 = _Genre
                            lItem.ItemId = ctlList.ListItems.Count - 1
                            lItem.IconImage = Config.GetFile(Config.Dir.Thumbs, "tv\logos\" & Channel.Retrieve(_idChannel).DisplayName & ".png")
                            GUIControl.AddListItemControl(GetID, ctlList.GetID, lItem)
                        End If

                        _lastTitel = Sendung.Title
                        _inFav = False

                    End If
                Next

            End While

            CloseClickfinderDB()

            ctlProgressBar.Visible = False

        End Sub

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

        'ListControl Click - Details anzeigen
        Private Sub ListControlClick()
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

        End Sub

        'ProgresBar paralell anzeigen
        Private Sub ShowProgressbar()
            ctlProgressBar.Visible = True
        End Sub

#End Region

#Region "Functions and Subs"

        'Funktion um Daten aus der ClickfinderPGConfig.xml abzufragen


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

        Private Function MySQLDateTOstring(ByVal Datum As Date) As String
            MySQLDateTOstring = "'" & Datum.Year & "-" & Format(Datum.Month, "00") & "-" & Format(Datum.Day, "00") & " " & Format(Datum.Hour, "00") & ":" & Format(Datum.Minute, "00") & ":00'"
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







    End Class
End Namespace

