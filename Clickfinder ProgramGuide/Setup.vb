Imports System
Imports System.IO

Imports MediaPortal.Profile
Imports MediaPortal.Configuration
Imports MediaPortal.GUI.Library
Imports MediaPortal.Util
Imports MediaPortal.Utils

Imports Gentle.Framework
Imports System.Drawing
Imports System.Threading
Imports System.Windows.Forms
Imports TvDatabase
Imports ClickfinderProgramGuide.TvDatabase
Imports ClickfinderProgramGuide.ClickfinderProgramGuide

Public Class Setup

    Private Sub Setup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            MyLog.Info("")
            MyLog.Info("")
            MyLog.Info("[Setup] load")

            Dim _layer As New TvBusinessLayer

            If Not _layer.GetSetting("ClickfinderProgramGuideVersion", String.Empty).Value = Helper.Version Then
                Dim setting As Setting = _layer.GetSetting("ClickfinderProgramGuideVersion", String.Empty)
                setting.Value = Helper.Version
                setting.Persist()


                'Hier Änderungen zur Vorgängerversion
                MsgBox("Version: " & Helper.Version & vbNewLine & vbNewLine & _
                       "Neue Version !" & vbNewLine & _
                       "Einstellungen müssen zurück gesetzt werden!", MsgBoxStyle.Information)


                ButtonDefaultSettings_Click(sender, e)

            End If

            LabelVersion.Text = _layer.GetSetting("ClickfinderProgramGuideVersion", String.Empty).Value

            CBOverlayGroup.Items.Clear()
            CbQuick1.Items.Clear()
            CbQuick2.Items.Clear()
            cbStandardGroup.Items.Clear()
            CBStartGui.Items.Clear()

            Dim _StartGuiList As New ArrayList

            MyLog.LogFileName = "ClickfinderProgramGuide_Config.log"
            MyLog.DebugModeOn = True
            MyLog.BackupLogFiles()

            Dim _logErrorSetting As String = String.Empty
            CPGsettings.Load()
            enrichEPG.MySettings.SetSettings(Config.GetFile(Config.Dir.Database, ""), CPGsettings.TvMovieImportTvSeriesInfos, CPGsettings.TvMovieImportVideoDatabaseInfos, CPGsettings.TvMovieImportMovingPicturesInfos, enrichEPG.MySettings.LogPath.Client, "ClickfinderProgramGuide_Config.log", , , , True, , )

            Dim plugin As String = CPGsettings.pluginClickfinderProgramGuide

            CheckBoxDebugMode.Checked = CPGsettings.ClickfinderDebugMode
            tbPluginName.Text = CPGsettings.ClickfinderPluginName
            tbClickfinderDatabase.Text = CPGsettings.ClickfinderDatabasePath
            tbClickfinderImagePath.Text = CPGsettings.ClickfinderImagePath
            tbEpisodenScanner.Text = CPGsettings.EpisodenScanner
            CheckBoxShowLocalMovies.Checked = CPGsettings.OverviewShowLocalMovies
            CheckBoxShowTagesTipp.Checked = CPGsettings.OverviewShowTagesTipp
            NumShowMoviesAfter.Value = CPGsettings.OverviewShowMoviesAfter
            NumShowHighlightsAfter.Value = CPGsettings.OverviewShowHighlightsAfter
            NumHighlightsMinRuntime.Value = CPGsettings.OverviewHighlightsMinRuntime
            NumMaxDays.Value = CPGsettings.OverviewMaxDays
            tbPrimeTime.Text = CPGsettings.PrimeTime
            tbLateTime.Text = CPGsettings.LateTime
            CheckBoxFilterShowLocalMovies.Checked = CPGsettings.ItemsShowLocalMovies
            CheckBoxFilterShowLocalSeries.Checked = CPGsettings.ItemsShowLocalSeries
            CheckBoxUseSportLogos.Checked = CPGsettings.UseSportLogos
            CheckBoxRemberSortedBy.Checked = CPGsettings.RemberSortedBy
            CheckBoxShowCategorieLocalMovies.Checked = CPGsettings.CategorieShowLocalMovies
            CheckBoxShowCategorieLocalSeries.Checked = CPGsettings.CategorieShowLocalSeries
            CheckBoxTvSeries.Checked = CPGsettings.TvMovieImportTvSeriesInfos
            CheckBoxMovingPictures.Checked = CPGsettings.TvMovieImportMovingPicturesInfos
            CheckBoxVideoDB.Checked = CPGsettings.TvMovieImportVideoDatabaseInfos
            CheckBoxUseTheTvDb.Checked = CPGsettings.TvMovieUseTheTvDb
            CheckBoxClickfinderPG.Checked = CPGsettings.ClickfinderDataAvailable
            CheckBoxUseSeriesDescribtion.Checked = CPGsettings.DetailUseSeriesDescribtion
            CheckSaveSettingsLocal.Checked = CPGsettings.ClickfinderSaveSettingsOnClients
            tbMPDatabasePath.Text = Config.GetFile(Config.Dir.Database, "")
            CheckBoxOverlayShowTagesTipp.Checked = CPGsettings.OverlayShowTagesTipp
            CheckBoxOverlayShowLocalMovies.Checked = CPGsettings.OverlayShowLocalMovies
            NumOverlayLimit.Value = CPGsettings.OverlayMovieLimit
            CheckBoxEnableMovieOverlay.Checked = CPGsettings.OverlayMoviesEnabled
            CheckBoxEnableSeriesOverlay.Checked = CPGsettings.OverlaySeriesEnabled
            NumPreviewDays.Value = CPGsettings.PreviewMaxDays
            NumPreviewMinTvMovieRating.Value = CPGsettings.PreviewMinTvMovieRating
            NumUpdateOverlay.Value = CPGsettings.OverlayUpdateTimer


            If CheckBoxEnableMovieOverlay.Checked = True Then
                GroupBoxMovieOverlay.Enabled = True
            Else
                GroupBoxMovieOverlay.Enabled = False
            End If

            Select Case CPGsettings.DetailSeriesImage
                Case Is = "Cover"
                    RBSeriesCover.Checked = True
                Case Is = "FanArt"
                    RBSeriesFanArt.Checked = True
                Case Is = "Episode"
                    RBEpisodeImage.Checked = True
                Case Is = "TvMovie"
                    RBTvMovieImage.Checked = True
            End Select

            If CPGsettings.TvMovieEnabled = False Or CPGsettings.ClickfinderDataAvailable = False Then
                Dim message As New TvMoviePluginError
                message.ShowDialog()
                Me.Close()
            End If

            If Not CPGsettings.TvMovieImportTvSeriesInfos = True Then
                GroupDetailSeriesImage.Enabled = False
            End If

            Select Case CPGsettings.OverviewMovieSort
                Case Is = Helper.SortMethode.startTime.ToString
                    RBstartTime.Checked = True
                Case Is = Helper.SortMethode.TvMovieStar.ToString
                    RBTvMovieStar.Checked = True
                Case Is = Helper.SortMethode.RatingStar.ToString
                    RBRatingStar.Checked = True
            End Select

            Select Case CPGsettings.OverlayMovieSort
                Case Is = Helper.SortMethode.startTime.ToString
                    RBOverlayStartTime.Checked = True
                Case Is = Helper.SortMethode.TvMovieStar.ToString
                    RBOverlayTvMovieStar.Checked = True
                Case Is = Helper.SortMethode.RatingStar.ToString
                    RBOverlayRatingStar.Checked = True
            End Select

            Select Case CPGsettings.OverlayTime
                Case Is = "Today"
                    RBOverlayHeute.Checked = True
                Case Is = "Now"
                    RBOverlayNow.Checked = True
                Case Is = "PrimeTime"
                    RBOverlayPrimeTime.Checked = True
                Case Is = "LateTime"
                    RBOverlayLateTime.Checked = True
            End Select

            ClickfinderCategoriesTable()

            Dim _EditButton As New DataGridViewButtonColumn
            With _EditButton
                .DefaultCellStyle.Padding = New Padding(2, 6, 2, 6)
                .HeaderText = ""
                .Text = "Edit"
                .Name = "Edit"
                '.FlatStyle = FlatStyle.Flat
                .UseColumnTextForButtonValue = True
                '_editbutton.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                .Width = 60
            End With

            Dim _DeleteButton As New DataGridViewButtonColumn

            With _DeleteButton
                .DefaultCellStyle.Padding = New Padding(2, 6, 2, 6)
                .HeaderText = ""
                .Text = "Delete"
                .Name = "Delete"
                '.FlatStyle = FlatStyle.Flat
                .UseColumnTextForButtonValue = True
                '_editbutton.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                .Width = 60
            End With

            Me.dgvCategories.Columns.Add(_EditButton)
            Me.dgvCategories.Columns.Add(_DeleteButton)

            Filldgv()

            Dim _groups As List(Of ChannelGroup) = ChannelGroup.ListAll

            For i = 0 To _groups.Count - 1
                cbStandardGroup.Items.Add(_groups(i).GroupName)
                CbQuick1.Items.Add(_groups(i).GroupName)
                CbQuick2.Items.Add(_groups(i).GroupName)
                CBOverlayGroup.Items.Add(_groups(i).GroupName)

                If _groups(i).GroupName = CPGsettings.StandardTvGroup Then
                    cbStandardGroup.Text = _groups(i).GroupName
                End If

                If _groups(i).GroupName = CPGsettings.QuickTvGroup1 Then
                    CbQuick1.Text = _groups(i).GroupName
                End If

                If _groups(i).GroupName = CPGsettings.QuickTvGroup2 Then
                    CbQuick2.Text = _groups(i).GroupName
                End If

                If _groups(i).GroupName = CPGsettings.OverlayTvGroup Then
                    CBOverlayGroup.Text = _groups(i).GroupName
                End If

            Next

            'Remote Control:
            For Each c As Control In TableItemsPanel.Controls
                Dim cellPos As TableLayoutPanelCellPosition = TableItemsPanel.GetCellPosition(c)
                'CBsort füllen
                If InStr(c.Name, "CBsort") Then
                    Dim CBsort__ As ComboBox = TableItemsPanel.GetControlFromPosition(cellPos.Column, cellPos.Row)
                    CBsort__.Items.Clear()
                    CBsort__.Items.Add("")
                    CBsort__.Items.AddRange([Enum].GetNames(GetType(Helper.SortMethode)))
                    'Wert laden
                    Dim _index As Integer = CInt(Replace(c.Name, "CBSort", ""))
                    CBsort__.Text = CPGsettings.ItemsRemoteSortAll(_index)
                End If
                'CBTvGroup(füllen)
                If InStr(c.Name, "CBTvGroup") Then
                    Try
                        Dim _AllTvGroups As Array = _groups.ConvertAll(Of String)(Function(x As ChannelGroup) x.GroupName.ToString).ToArray
                        Dim CBTvGroup__ As ComboBox = TableItemsPanel.GetControlFromPosition(cellPos.Column, cellPos.Row)
                        CBTvGroup__.Items.Clear()
                        CBTvGroup__.Items.Add("")
                        CBTvGroup__.Items.AddRange(_AllTvGroups)
                        Dim _index As Integer = CInt(Replace(c.Name, "CBTvGroup", ""))
                        CBTvGroup__.Text = CPGsettings.ItemsRemoteTvGroupAll(_index)
                    Catch ex1 As Exception
                        MyLog.Error("[Setup_Load]: RemoteTvGroup fill cbox error!")
                        MyLog.Error("[Setup_Load]: exception err:" & ex1.Message & " stack:" & ex1.StackTrace)
                    End Try
                End If
            Next

            _StartGuiList.Clear()
            CBStartGui.Items.Clear()

            _StartGuiList.Add("Highlights")
            _StartGuiList.Add("Now")
            _StartGuiList.Add("PrimeTime")
            _StartGuiList.Add("LateTime")
            _StartGuiList.Add("PrimeTimeMovies")
            _StartGuiList.Add("LateTimeMovies")

            For i = 0 To _StartGuiList.Count - 1
                CBStartGui.Items.Add(_StartGuiList(i))
                If _StartGuiList(i) = CPGsettings.ClickfinderStartGui Then
                    CBStartGui.Text = _StartGuiList(i)
                End If
            Next

            'Helper.LogSettings()

        Catch ex As Exception
            MsgBox("Ein Fehler beim Laden der Einstellungen ist aufgetreten!", MsgBoxStyle.Critical, "Warnung!")
            MyLog.Error("[Setup_Load]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
            ButtonDefaultSettings_Click(sender, e)
        End Try

    End Sub

    Private Sub ButtonSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSave.Click
        Try
            CPGsettings.ClickfinderDatabasePath = tbClickfinderDatabase.Text
            CPGsettings.ClickfinderImagePath = tbClickfinderImagePath.Text
            CPGsettings.EpisodenScanner = tbEpisodenScanner.Text

            CPGsettings.OverviewShowMoviesAfter = CDbl(NumShowMoviesAfter.Value)
            CPGsettings.OverviewShowHighlightsAfter = CDbl(NumShowHighlightsAfter.Value)
            CPGsettings.OverviewHighlightsMinRuntime = CLng(NumHighlightsMinRuntime.Value)
            CPGsettings.OverviewMaxDays = CDbl(NumMaxDays.Value)

            CPGsettings.PrimeTime = CDate(tbPrimeTime.Text)
            CPGsettings.LateTime = CDate(tbLateTime.Text)

            CPGsettings.StandardTvGroup = cbStandardGroup.Text
            CPGsettings.QuickTvGroup1 = CbQuick1.Text
            CPGsettings.QuickTvGroup2 = CbQuick2.Text

            CPGsettings.pluginClickfinderProgramGuide = True
            CPGsettings.ClickfinderPluginName = tbPluginName.Text
            CPGsettings.ClickfinderStartGui = CBStartGui.Text

            CPGsettings.OverlayMovieLimit = CInt(NumOverlayLimit.Value)
            CPGsettings.OverlayTvGroup = CBOverlayGroup.Text
            CPGsettings.OverlayUpdateTimer = CInt(NumUpdateOverlay.Value)

            CPGsettings.PreviewMaxDays = CInt(NumPreviewDays.Value)
            CPGsettings.PreviewMinTvMovieRating = CInt(NumPreviewMinTvMovieRating.Value)

            For Each c As Control In TableItemsPanel.Controls
                Dim cellPos As TableLayoutPanelCellPosition = TableItemsPanel.GetCellPosition(c)
                'CBsort füllen
                If InStr(c.Name, "CBsort") Then
                    Dim _CBsort__ As ComboBox = TableItemsPanel.GetControlFromPosition(cellPos.Column, cellPos.Row)
                    Dim _index As Integer = CInt(Replace(c.Name, "CBSort", ""))
                    CPGsettings.ItemsRemoteSortAll(_index) = _CBsort__.Text
                End If
                If InStr(c.Name, "CBTvGroup") Then
                    Dim _CBTvGroup__ As ComboBox = TableItemsPanel.GetControlFromPosition(cellPos.Column, cellPos.Row)
                    Dim _index As Integer = CInt(Replace(c.Name, "CBTvGroup", ""))
                    CPGsettings.ItemsRemoteTvGroupAll(_index) = _CBTvGroup__.Text
                End If
            Next

            CPGsettings.pluginClickfinderProgramGuide = True

            CPGsettings.save()

            SaveCategories()

            MyLog.Info("[Setup] close")
            MyLog.Info("")
            MyLog.Info("")

            Me.Dispose()
            Me.Close()
        Catch ex As Exception
            MyLog.Error("[ButtonSave_Click]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
        End Try

    End Sub

    Private Sub ButtonOpenDlgDatabase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOpenDlgDatabase.Click
        Dim openFileDialog As New OpenFileDialog()

        openFileDialog.InitialDirectory = Environment.SpecialFolder.ProgramFiles
        openFileDialog.Filter = "TV Movie Database|*.mdb"
        openFileDialog.FileName = "tvdaten.mdb"

        If openFileDialog.ShowDialog(Me) = DialogResult.OK Then
            tbClickfinderDatabase.Text = openFileDialog.FileName

            If Directory.Exists(IO.Path.GetDirectoryName(openFileDialog.FileName) & "\Hyperlinks") = True Then
                tbClickfinderImagePath.Text = IO.Path.GetDirectoryName(openFileDialog.FileName) & "\Hyperlinks"
            End If

        End If
    End Sub

    Private Sub tbClickfinderDatabase_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbClickfinderDatabase.Leave
        If File.Exists(tbClickfinderDatabase.Text) = True Then
            If Directory.Exists(IO.Path.GetDirectoryName(tbClickfinderDatabase.Text) & "\Hyperlinks") = True Then
                tbClickfinderImagePath.Text = IO.Path.GetDirectoryName(tbClickfinderDatabase.Text) & "\Hyperlinks"
            Else
                MsgBox("Bilder Ordner nicht gefunden!", MsgBoxStyle.Critical, "Warnung ...")
                tbClickfinderImagePath.Text = ""
                tbClickfinderImagePath.Focus()
            End If
        Else
            MsgBox("Datenbank nicht gefunden!", MsgBoxStyle.Critical, "Warnung ...")
            tbClickfinderDatabase.Text = ""
            tbClickfinderDatabase.Focus()
        End If
    End Sub

    Private Sub CheckBoxShowLocalMovies_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxShowLocalMovies.CheckedChanged
        CPGsettings.OverviewShowLocalMovies = CheckBoxShowLocalMovies.Checked
    End Sub

    Private Sub CheckBoxShowTagesTipp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxShowTagesTipp.CheckedChanged
        CPGsettings.OverviewShowTagesTipp = CheckBoxShowTagesTipp.Checked
    End Sub

    Private Sub CheckBoxUseSportLogos_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxUseSportLogos.CheckedChanged
        CPGsettings.UseSportLogos = CheckBoxUseSportLogos.Checked
    End Sub

    Private Sub ClickfinderCategoriesTable()

        Try
            'prüfen ob Einträge vorhanden sind
            Dim bla As IList(Of ClickfinderCategories) = ClickfinderCategories.ListAll

            If bla.Count = 0 Then
                'wenn keine Categorien vorhanden ->  standard Categorien erstellen
                MyLog.Info("[ClickfinderCategoriesTable] No Categroies found -> Create standard categories")
                CreateClickfinderCategories()
            Else
                MyLog.Info("[ClickfinderCategoriesTable] Table and categories found")
            End If

        Catch ex As Exception
            'falls table nicht existiert erstellen + standard Categorien
            MyLog.Info("[ClickfinderCategoriesTable] Not found -> Create table and standard categories")
            CreateClickfinderCategoriesTable()
            CreateClickfinderCategories()

            ClickfinderCategoriesTable()
        End Try
    End Sub

    Private Sub CreateClickfinderCategoriesTable()

        MyLog.Debug("TvServer provider: {0}", Gentle.Framework.Broker.ProviderName)

        If Gentle.Framework.Broker.ProviderName = "MySQL" Then
            Try
                Broker.Execute("drop table mptvdb.ClickfinderCategories")
                Broker.Execute("CREATE TABLE mptvdb.ClickfinderCategories ( idClickfinderCategories INT NOT NULL AUTO_INCREMENT , Name VARCHAR(255) , Beschreibung VARCHAR(255) , isVisible BIT(1) NOT NULL DEFAULT 0 , Image VARCHAR(255) , SqlString TEXT , sortOrder INT NOT NULL DEFAULT 0 , MinRunTime INT NOT NULL DEFAULT 0 , NowOffset INT NOT NULL DEFAULT 0 , sortedBy VARCHAR(100) , groupName VARCHAR(100) , PRIMARY KEY (idClickfinderCategories) )")
            Catch ex As Exception
                Broker.Execute("CREATE TABLE mptvdb.ClickfinderCategories ( idClickfinderCategories INT NOT NULL AUTO_INCREMENT , Name VARCHAR(255) , Beschreibung VARCHAR(255) , isVisible BIT(1) NOT NULL DEFAULT 0 , Image VARCHAR(255) , SqlString TEXT , sortOrder INT NOT NULL DEFAULT 0 , MinRunTime INT NOT NULL DEFAULT 0 , NowOffset INT NOT NULL DEFAULT 0 , sortedBy VARCHAR(100) , groupName VARCHAR(100) , PRIMARY KEY (idClickfinderCategories) )")
            End Try
        Else
            Try
                Broker.Execute("drop table mptvdb.[dbo].ClickfinderCategories")
                Broker.Execute("CREATE TABLE mptvdb.[dbo].ClickfinderCategories ( idClickfinderCategories INT NOT NULL IDENTITY , Name VARCHAR(255) , Beschreibung VARCHAR(255) , isVisible BIT NOT NULL DEFAULT 0 , Image VARCHAR(255) , SqlString TEXT , sortOrder INT NOT NULL DEFAULT 0 , MinRunTime INT NOT NULL DEFAULT 0 , NowOffset INT NOT NULL DEFAULT 0 , sortedBy VARCHAR(100) , groupName VARCHAR(100) , PRIMARY KEY (idClickfinderCategories) )")
            Catch ex As Exception
                Broker.Execute("CREATE TABLE mptvdb.[dbo].ClickfinderCategories ( idClickfinderCategories INT NOT NULL IDENTITY , Name VARCHAR(255) , Beschreibung VARCHAR(255) , isVisible BIT NOT NULL DEFAULT 0 , Image VARCHAR(255) , SqlString TEXT , sortOrder INT NOT NULL DEFAULT 0 , MinRunTime INT NOT NULL DEFAULT 0 , NowOffset INT NOT NULL DEFAULT 0 , sortedBy VARCHAR(100) , groupName VARCHAR(100) , PRIMARY KEY (idClickfinderCategories) )")
            End Try
        End If

    End Sub

    Private Sub CreateClickfinderCategories()
        Try

            Dim _Categorie As New ClickfinderCategories("Movies", "Alle Filme nach Uhrzeit, Rating, TvMovieRating sortiert", True, 0, 80, 30)
            _Categorie.SqlString = "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram WHERE #CPGFilter AND startTime >= #StartTime AND startTime <= #EndTime AND starRating >= 1 AND TvMovieBewertung < 6 AND (genre NOT LIKE '%Serie' OR genre NOT LIKE '%Reihe' OR genre NOT LIKE '%Sitcom%' OR genre NOT LIKE '%Zeichentrick%') #CPGgroupBy #CPGorderBy"
            _Categorie.Image = _Categorie.Name & ".png"
            _Categorie.Persist()

            _Categorie = New ClickfinderCategories("Serien", "Alle Serien nach Uhrzeit, Rating und TvMovieRating sortiert", True, 1, 10, 15)
            _Categorie.SqlString = "Select * FROM program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram WHERE #CPGFilter AND startTime >= #StartTime AND startTime <= #EndTime AND (Genre LIKE '%Serie' OR genre LIKE '%Reihe' OR genre LIKE '%Sitcom%') #CPGgroupBy #CPGorderBy"
            _Categorie.Image = _Categorie.Name & ".png"
            _Categorie.Persist()

            _Categorie = New ClickfinderCategories("Dokumentationen", "Alle Dokumentationen nach Uhrzeit und TvMovieRating sortiert", True, 2, 30, 40)
            _Categorie.SqlString = "Select * FROM program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram WHERE #CPGFilter AND startTime >= #StartTime AND startTime <= #EndTime AND genre LIKE '%Doku%' AND #CPGFilter #CPGgroupBy #CPGorderBy"
            _Categorie.Image = _Categorie.Name & ".png"
            _Categorie.Persist()

            _Categorie = New ClickfinderCategories("Sport", "Alle Sport Sendungen nach Uhrzeit und TvMovieRating sortiert", True, 3, 10, 30)
            _Categorie.SqlString = "Select * FROM program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram WHERE #CPGFilter AND startTime >= #StartTime AND startTime <= #EndTime AND (title LIKE '%Sport%' OR title LIKE '%Fußball%' OR episodeName LIKE '%Sport%' OR genre LIKE '%Sport%' OR genre LIKE '%football%soccer%' OR genre LIKE '%ball%' OR genre LIKE '%Automagazin%' OR genre LIKE '%Biathlon%' OR genre LIKE '%Billard%' OR genre LIKE '%Bobsport%' OR genre LIKE '%Bowling%' OR genre LIKE '%Boxen%' OR genre LIKE '%Darts%' OR genre LIKE '%Eishockey%' OR genre LIKE '%E-Sport%' OR genre LIKE '%Formel 1%' OR genre LIKE '%Fun- u. Extremsport%' OR genre LIKE '%Golf%' OR genre LIKE '%Leichtathletik%' OR genre LIKE '%Motorrad%' OR genre LIKE '%Nordische Kombination%' OR genre LIKE '%Poker%' OR genre LIKE '%Rennrodeln%' OR genre LIKE '%Segeln%' OR genre LIKE '%Ski%' or genre LIKE '%Snowboard%' OR genre LIKE '%Tennis%' OR genre LIKE '%Wrestling%' OR genre LIKE '%sports (general)%') #CPGgroupBy #CPGorderBy"
            _Categorie.Image = _Categorie.Name & ".png"
            _Categorie.Persist()

            _Categorie = New ClickfinderCategories("Reportagen", "Alle Reportagen nach Uhrzeit und TvMovieRating sortiert", True, 4, 20, 20)
            _Categorie.SqlString = "Select * FROM program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram WHERE #CPGFilter AND startTime >= #StartTime AND startTime <= #EndTime AND genre LIKE '%Report%' #CPGgroupBy #CPGorderBy"
            _Categorie.Image = _Categorie.Name & ".png"
            _Categorie.Persist()

            _Categorie = New ClickfinderCategories("Magazine", "Alle Magazine nach Uhrzeit und TvMovieRating sortiert", True, 5, 15, 15)
            _Categorie.SqlString = "Select * FROM program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram WHERE #CPGFilter AND startTime >= #StartTime AND startTime <= #EndTime AND genre LIKE '%Magazin%' #CPGgroupBy #CPGorderBy"
            _Categorie.Image = _Categorie.Name & ".png"
            _Categorie.Persist()

            _Categorie = New ClickfinderCategories("HDTV", "Alle HDTV Sendungen nach Uhrzeit und TvMovieRating sortiert", True, 6, 25, 30)
            _Categorie.SqlString = "Select * FROM (program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram) INNER JOIN channel ON program.idChannel = channel.idChannel WHERE #CPGFilter AND startTime >= #StartTime AND startTime <= #EndTime AND displayName LIKE '%HD%' #CPGgroupBy #CPGorderBy"
            _Categorie.Image = _Categorie.Name & ".png"
            _Categorie.Persist()

            _Categorie = New ClickfinderCategories("Sky Cinema", "Alle Sendungen des Packets Film von Sky nach Uhrzeit und TvMovieRating sortiert", True, 7, 80, 30)
            _Categorie.SqlString = "Select * FROM (program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram) INNER JOIN channel ON program.idChannel = channel.idChannel WHERE #CPGFilter AND startTime >= #StartTime AND startTime <= #EndTime AND (displayName LIKE '%SKY Cinema%' OR displayName LIKE '%SKY Cinema HD%' OR displayName LIKE '%SKY Action%' OR displayName LIKE '%SKY Action HD%' OR displayName LIKE '%MGM%' OR displayName LIKE '%Disney Cinemagic%' OR displayName LIKE '%Disney Cinemagic HD%' OR displayName LIKE '%SKY Comedy%' OR displayName LIKE '%SKY Emotion%' OR displayName LIKE '%SKY Nostalgie%') #CPGgroupBy #CPGorderBy"
            _Categorie.Image = _Categorie.Name & ".png"
            _Categorie.Persist()

            _Categorie = New ClickfinderCategories("Sky Dokumentationen", "Alle Dokumentationen von Sky nach Uhrzeit und TvMovieRating sortiert", True, 8, 30, 40)
            _Categorie.SqlString = "Select * FROM (program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram) INNER JOIN channel ON program.idChannel = channel.idChannel WHERE #CPGFilter AND startTime >= #StartTime AND startTime <= #EndTime AND (displayName LIKE '%Discovery%' OR displayName LIKE '%History%' OR displayName LIKE '%National Geographic%' OR displayName LIKE '%Spiegel Geschichte%' OR displayName LIKE '%MOTORVISION TV%' OR displayName LIKE '%The Biography Channel%') AND #CPGFilter #CPGgroupBy #CPGorderBy"
            _Categorie.Image = _Categorie.Name & ".png"
            _Categorie.Persist()

        Catch ex As Exception
            MyLog.Error("[CreateClickfinderCategories]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
        End Try

    End Sub

    Private Sub Filldgv()

        Dim sb As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(ClickfinderCategories))
        sb.AddOrderByField(True, "sortOrder")
        Dim stmt As SqlStatement = sb.GetStatement(True)
        Dim _Result As IList(Of ClickfinderCategories) = ObjectFactory.GetCollection(GetType(ClickfinderCategories), stmt.Execute())

        dgvCategories.Rows.Clear()

        For i = 0 To _Result.Count - 1

            'id,isvisible,Name,Beschreibung,SortOrder,SqlString
            Me.dgvCategories.Rows.Add(_Result(i).idClickfinderCategories, CreateCloneImage(Config.GetFile(Config.Dir.Thumbs, "Clickfinder ProgramGuide\Categories\") & _Result(i).Image), _Result(i).isVisible, _Result(i).Name, _Result(i).Beschreibung, _Result(i).SortOrder, _Result(i).SqlString, _Result(i).MinRunTime, _Result(i).NowOffset)
            Me.dgvCategories.Item(C_Name.Name, i).Style.Font = New Font(Me.dgvCategories.Font, FontStyle.Bold)

        Next

    End Sub

    Private Sub dgvCategories_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCategories.CellClick
        Try
            Select Case (e.ColumnIndex)
                Case Is = 9

                    SaveCategories()

                    Dim edit As New EditCategorie

                    edit.Text = "Edit Categorie"
                    edit.tbSortOrder.Text = Me.dgvCategories(C_sortOrder.Index, e.RowIndex).Value
                    If Not String.IsNullOrEmpty(Me.dgvCategories(C_Name.Index, e.RowIndex).Value) Then
                        edit.picCategorie.Tag = Config.GetFile(Config.Dir.Thumbs, "Clickfinder ProgramGuide\Categories\") & Me.dgvCategories(C_Name.Index, e.RowIndex).Value & ".png"
                    End If
                    edit.ShowDialog()

                    Filldgv()

                Case Is = 10

                    Dim antwort As MsgBoxResult
                    antwort = MsgBox("Möchtest du die Kategorie wirklich löschen?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Löschen ...")

                    If antwort = MsgBoxResult.Yes Then

                        SaveCategories()

                        Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(Me.dgvCategories(C_sortOrder.Name, e.RowIndex).Value)

                        Me.dgvCategories.Rows.Remove(Me.dgvCategories.Rows(e.RowIndex))
                        _Categorie.Remove()

                        Dim sb As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(ClickfinderCategories))
                        sb.AddOrderByField(True, "sortOrder")
                        Dim stmt As SqlStatement = sb.GetStatement(True)
                        Dim _Result As IList(Of ClickfinderCategories) = ObjectFactory.GetCollection(GetType(ClickfinderCategories), stmt.Execute())

                        For i = 0 To _Result.Count - 1
                            _Result(i).SortOrder = i
                            _Result(i).Persist()
                        Next

                        Filldgv()
                    End If
            End Select
        Catch ex As Exception
            MyLog.Error("[dgvCategories_CellClick]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
        End Try

    End Sub

    Private Sub SaveCategories()
        For i = 0 To Me.dgvCategories.Rows.Count - 1
            Dim _Categories As ClickfinderCategories = ClickfinderCategories.Retrieve(CInt(Me.dgvCategories(C_sortOrder.Index, i).Value))

            _Categories.isVisible = Me.dgvCategories(C_visible.Index, i).Value
            _Categories.Name = Me.dgvCategories(C_Name.Index, i).Value
            _Categories.Image = _Categories.Name & ".png"
            _Categories.Beschreibung = Me.dgvCategories(C_Beschreibung.Index, i).Value
            _Categories.SqlString = Me.dgvCategories(C_SqlString.Index, i).Value
            _Categories.SortOrder = Me.dgvCategories(C_sortOrder.Index, i).Value

            _Categories.Persist()
        Next

    End Sub

    Private Sub ButtonUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonUp.Click
        Dim setfocus As Integer

        If Not dgvCategories.SelectedCells.Item(C_sortOrder.Index).Value = 0 Then
            setfocus = dgvCategories.SelectedCells.Item(C_sortOrder.Index).Value - 1
            dgvCategories.SelectedCells.Item(C_sortOrder.Index).Value = dgvCategories.SelectedCells.Item(C_sortOrder.Index).Value - 1
            dgvCategories(C_sortOrder.Index, dgvCategories.SelectedRows.Item(0).Index - 1).Value = dgvCategories(C_sortOrder.Index, dgvCategories.SelectedRows.Item(0).Index - 1).Value + 1

            dgvCategories.Sort(dgvCategories.Columns(C_sortOrder.Index), System.ComponentModel.ListSortDirection.Ascending)
            dgvCategories.Rows(0).Selected = False
            dgvCategories.Rows(setfocus).Selected = True

        End If

    End Sub

    Private Sub ButtonDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDown.Click
        Dim setfocus As Integer

        If Not dgvCategories.SelectedCells.Item(C_sortOrder.Index).Value = dgvCategories.Rows.Count - 1 Then
            setfocus = dgvCategories.SelectedCells.Item(C_sortOrder.Index).Value + 1
            dgvCategories.SelectedCells.Item(C_sortOrder.Index).Value = dgvCategories.SelectedCells.Item(C_sortOrder.Index).Value + 1
            dgvCategories(C_sortOrder.Index, dgvCategories.SelectedRows.Item(0).Index + 1).Value = dgvCategories(C_sortOrder.Index, dgvCategories.SelectedRows.Item(0).Index + 1).Value - 1

            dgvCategories.Sort(dgvCategories.Columns(C_sortOrder.Index), System.ComponentModel.ListSortDirection.Ascending)
            dgvCategories.Rows(0).Selected = False
            dgvCategories.Rows(setfocus).Selected = True

        End If
    End Sub

    Private Function CreateCloneImage(ByVal path As String) As Bitmap
        Dim bmpClone As Bitmap = Nothing 'the clone to be loaded to a PictureBox

        Try
            'Work around: ImageClone erstellen, weil sonst löschen net funzt -> verhindert (... used by other process)

            Dim bmpOriginal As System.Drawing.Image = System.Drawing.Image.FromFile(path)
            bmpClone = New Bitmap(bmpOriginal.Width, bmpOriginal.Height)
            Dim gr As Graphics = Graphics.FromImage(bmpClone) 'get object representing clone's currently empty drawing surface
            gr.SmoothingMode = Drawing2D.SmoothingMode.None
            gr.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
            gr.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighSpeed
            gr.DrawImage(bmpOriginal, 0, 0, bmpOriginal.Width, bmpOriginal.Height) 'copy original image onto this surface
            gr.Dispose()

            bmpOriginal.Dispose()

        Catch ex As Exception
            MyLog.[Warn]("[CreateCloneImage]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
        End Try

        Return bmpClone
    End Function

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxSelect.CheckedChanged
        For i = 0 To Me.dgvCategories.Rows.Count - 1
            If CheckBoxSelect.Checked = True Then
                Me.dgvCategories(C_visible.Index, i).Value = True
            Else
                Me.dgvCategories(C_visible.Index, i).Value = False
            End If

        Next
    End Sub

    Private Sub ButtonNewCategorie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNewCategorie.Click
        SaveCategories()

        Dim edit As New EditCategorie

        edit.Text = "New Categorie"
        edit.tbNewSortOrder.Text = Me.dgvCategories.Rows.Count
        edit.ShowDialog()
        Filldgv()
    End Sub

    Private Sub CheckBoxFilterShowLocalMovies_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxFilterShowLocalMovies.CheckedChanged
        CPGsettings.ItemsShowLocalMovies = CheckBoxFilterShowLocalMovies.Checked
    End Sub

    Private Sub CheckBoxFilterShowLocalSeries_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxFilterShowLocalSeries.CheckedChanged
        CPGsettings.ItemsShowLocalSeries = CheckBoxFilterShowLocalSeries.Checked
    End Sub

    Private Sub RBstartTime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBstartTime.CheckedChanged
        CPGsettings.OverviewMovieSort = Helper.SortMethode.startTime.ToString
    End Sub

    Private Sub RBTvMovieStar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBTvMovieStar.CheckedChanged
        CPGsettings.OverviewMovieSort = Helper.SortMethode.TvMovieStar.ToString
    End Sub

    Private Sub RBRatingStar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBRatingStar.CheckedChanged
        CPGsettings.OverviewMovieSort = Helper.SortMethode.RatingStar.ToString
    End Sub

    Private Sub CheckBoxRemberSortedBy_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxRemberSortedBy.CheckedChanged
        CPGsettings.RemberSortedBy = CheckBoxRemberSortedBy.Checked
    End Sub

    Private Sub CheckBoxDebugMode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxDebugMode.CheckedChanged
        CPGsettings.ClickfinderDebugMode = CheckBoxDebugMode.Checked
    End Sub

    Private Sub CheckBoxShowCategorieLocalMovies_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxShowCategorieLocalMovies.CheckedChanged
        CPGsettings.CategorieShowLocalMovies = CheckBoxShowCategorieLocalMovies.Checked
    End Sub

    Private Sub CheckBoxShowCategorieLocalSeries_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxShowCategorieLocalSeries.CheckedChanged
        CPGsettings.CategorieShowLocalSeries = CheckBoxShowCategorieLocalSeries.Checked
    End Sub

    Private Sub CheckBoxUseSeriesDescribtion_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxUseSeriesDescribtion.CheckedChanged
        CPGsettings.DetailUseSeriesDescribtion = CheckBoxUseSeriesDescribtion.Checked
    End Sub

    Private Sub RBSeriesCover_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBSeriesCover.CheckedChanged
        CPGsettings.DetailSeriesImage = "Cover"
    End Sub

    Private Sub RBSeriesFanArt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBSeriesFanArt.CheckedChanged
        CPGsettings.DetailSeriesImage = "FanArt"
    End Sub

    Private Sub RBEpisodeImage_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBEpisodeImage.CheckedChanged
        CPGsettings.DetailSeriesImage = "Episode"
    End Sub

    Private Sub RBTvMovieImage_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBTvMovieImage.CheckedChanged
        CPGsettings.DetailSeriesImage = "TvMovie"
    End Sub

    Private Sub CheckBoxOverlayShowTagesTipp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxOverlayShowTagesTipp.CheckedChanged
        CPGsettings.OverlayShowTagesTipp = CheckBoxOverlayShowTagesTipp.Checked
    End Sub

    Private Sub CheckBoxOverlayShowLocalMovies_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxOverlayShowLocalMovies.CheckedChanged
        CPGsettings.OverlayShowLocalMovies = CheckBoxOverlayShowLocalMovies.Checked
    End Sub

    Private Sub RBOverlayHeute_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOverlayHeute.CheckedChanged
        CPGsettings.OverlayTime = "Today"
    End Sub

    Private Sub RBOverlayNow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOverlayNow.CheckedChanged
        CPGsettings.OverlayTime = "Now"
    End Sub

    Private Sub RBOverlayPrimeTime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOverlayPrimeTime.CheckedChanged
        CPGsettings.OverlayTime = "PrimeTime"
    End Sub

    Private Sub RBOverlayLateTime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOverlayLateTime.CheckedChanged
        CPGsettings.OverlayTime = "LateTime"
    End Sub

    Private Sub RBOverlayStartTime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOverlayStartTime.CheckedChanged
        CPGsettings.OverlayMovieSort = Helper.SortMethode.startTime.ToString
    End Sub

    Private Sub RBOverlayTvMovieStar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOverlayTvMovieStar.CheckedChanged
        CPGsettings.OverlayMovieSort = Helper.SortMethode.TvMovieStar.ToString
    End Sub

    Private Sub RBOverlayRatingStar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOverlayRatingStar.CheckedChanged
        CPGsettings.OverlayMovieSort = Helper.SortMethode.RatingStar.ToString
    End Sub

    Private Sub CheckBoxEnableMovieOverlay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxEnableMovieOverlay.CheckedChanged

        CPGsettings.OverlayMoviesEnabled = CheckBoxEnableMovieOverlay.Checked

        If CheckBoxEnableMovieOverlay.Checked = True Then
            GroupBoxMovieOverlay.Enabled = True
        Else
            GroupBoxMovieOverlay.Enabled = False
        End If

    End Sub

    Private Sub CheckBoxEnableSeriesOverlay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxEnableSeriesOverlay.CheckedChanged
        CPGsettings.OverlaySeriesEnabled = CheckBoxEnableSeriesOverlay.Checked
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=WUTCGQGMATVB4")
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("https://code.google.com/p/clickfinder-programguide/wiki/Manual_de")
    End Sub

    Private Sub ButtonOpenDlgEpisodenScanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOpenDlgEpisodenScanner.Click
        Dim openFileDialog As New OpenFileDialog()

        openFileDialog.InitialDirectory = Environment.SpecialFolder.ProgramFiles
        openFileDialog.Filter = "Episodenscanner|*.exe"
        openFileDialog.FileName = "episodescanner.exe"

        If openFileDialog.ShowDialog(Me) = DialogResult.OK Then
            tbEpisodenScanner.Text = openFileDialog.FileName
        End If

    End Sub

    Private Sub ButtonDefaultSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDefaultSettings.Click

        MyLog.Debug("[ButtonDefaultSettings_Click]: pressed")

        Dim antwort As MsgBoxResult
        antwort = MsgBox("Möchtes du wirklich die standard Einstellungen des Clickfinder ProgramGuides wiederherstellen?" & vbNewLine & _
                         "Alle durchgeführten Änderungen werden dadurch gelöscht !" & vbNewLine & vbNewLine & "Das Setup des Clickfinder ProgramGuides muss danach neu aufgerufen werden.", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Restore Default")


        If antwort = MsgBoxResult.Yes Then

            Try

                Dim sb As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(Setting))
                sb.AddConstraint([Operator].Like, "tag", "Clickfinder%")
                sb.AddOrderByField(True, "tag")
                Dim stmt As SqlStatement = sb.GetStatement(True)
                Dim _Result As IList(Of Setting) = ObjectFactory.GetCollection(GetType(Setting), stmt.Execute())

                If _Result.Count > 0 Then
                    For i = 0 To _Result.Count - 1

                        If Not _Result(i).Tag = "ClickfinderDataAvailable" Then
                            MyLog.Debug("[ButtonDefaultSettings_Click] Deleting settings: {0} = {1}", _Result(i).Tag, _Result(i).Value)
                            _Result(i).Remove()
                        End If
                    Next
                End If


                'Default Categorien erzeugen
                Try
                    CreateClickfinderCategoriesTable()
                    CreateClickfinderCategories()
                Catch ex As Exception
                    MyLog.Error("[ButtonDefaultSettings_Click]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
                End Try

                Me.Close()
            Catch ex As Exception
                MyLog.Error("[ButtonDefaultSettings_Click]: error ex: {0}, stack: {1}", ex.Message, ex.StackTrace)
            End Try

        End If

    End Sub

    Private Sub CheckSaveSettingsLocal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckSaveSettingsLocal.CheckedChanged
        Dim _layer As New TvBusinessLayer
        Dim _setting As Setting = _layer.GetSetting("ClickfinderSaveSettingsOnClients", "false")
        _setting.Value = CStr(CheckSaveSettingsLocal.Checked)
        _setting.Persist()
    End Sub

    Private Sub BT_loadfromServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_loadfromServer.Click
        Dim merker As Boolean = CheckSaveSettingsLocal.Checked

        Dim _layer As New TvBusinessLayer
        Dim _setting As Setting = _layer.GetSetting("ClickfinderSaveSettingsOnClients", "false")
        _setting.Value = False
        _setting.Persist()

        Setup_Load(Me, New System.EventArgs)

        _setting.Value = merker
        CheckSaveSettingsLocal.Checked = merker
        _setting.Persist()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Try


        '    MsgBox(CPGsettings.ItemsRemoteSortAll(1))

        '    CPGsettings.ItemsRemoteSortAll(1) = "test"
        '    CPGsettings.ItemsRemoteSortAll(6) = "test"
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

    End Sub

    Private Sub BTsetToStandardTvGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTsetToStandardTvGroup.Click
        'Remote Control:
        For Each c As Control In TableItemsPanel.Controls
            Dim cellPos As TableLayoutPanelCellPosition = TableItemsPanel.GetCellPosition(c)
            If InStr(c.Name, "CBTvGroup") Then
                Dim CBTvGroup__ As ComboBox = TableItemsPanel.GetControlFromPosition(cellPos.Column, cellPos.Row)
                CBTvGroup__.Text = ""
            End If
        Next
    End Sub

    Private Sub BTClearSorting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTClearSorting.Click
        'Remote Control:
        For Each c As Control In TableItemsPanel.Controls
            Dim cellPos As TableLayoutPanelCellPosition = TableItemsPanel.GetCellPosition(c)
            If InStr(c.Name, "CBsort") Then
                Dim CBsort__ As ComboBox = TableItemsPanel.GetControlFromPosition(cellPos.Column, cellPos.Row)
                CBsort__.Text = String.Empty
            End If
        Next
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim _MappingMgt As New enrichEPG.seriesManagement
        _MappingMgt.ShowDialog()

    End Sub
End Class