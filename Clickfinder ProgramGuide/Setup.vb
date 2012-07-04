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



Public Class Setup
#Region "Members"
    Private Shared _layer As New TvBusinessLayer
#End Region

    Private Sub Setup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim _StartGuiList As New ArrayList

            MyLog.Info("")
            MyLog.Info("")
            MyLog.Info("[Setup] load")


            tbPluginName.Text = _layer.GetSetting("ClickfinderPluginName", "Clickfinder ProgramGuide").Value
            tbClickfinderDatabase.Text = _layer.GetSetting("ClickfinderDatabasePath", "").Value
            tbClickfinderImagePath.Text = _layer.GetSetting("ClickfinderImagePath", "").Value
            CheckBoxShowLocalMovies.Checked = _layer.GetSetting("ClickfinderOverviewShowLocalMovies", "false").Value
            CheckBoxShowTagesTipp.Checked = _layer.GetSetting("ClickfinderOverviewShowTagesTipp", "false").Value
            NumShowMoviesAfter.Value = _layer.GetSetting("ClickfinderOverviewShowMoviesAfter", "12").Value
            NumShowHighlightsAfter.Value = _layer.GetSetting("ClickfinderOverviewShowHighlightsAfter", "15").Value
            NumHighlightsMinRuntime.Value = _layer.GetSetting("ClickfinderOverviewHighlightsMinRuntime", "16").Value
            NumMaxDays.Value = _layer.GetSetting("ClickfinderOverviewMaxDays", "10").Value
            'NumNowOffset.Value = _layer.GetSetting("ClickfinderNowOffset", "-20").Value
            tbPrimeTime.Text = _layer.GetSetting("ClickfinderPrimeTime", "20:15").Value
            tbLateTime.Text = _layer.GetSetting("ClickfinderLateTime", "22:00").Value
            CheckBoxFilterShowLocalMovies.Checked = _layer.GetSetting("ClickfinderItemsShowLocalMovies", "false").Value
            CheckBoxFilterShowLocalSeries.Checked = _layer.GetSetting("ClickfinderItemsShowLocalSeries", "false").Value
            CheckBoxUseSportLogos.Checked = _layer.GetSetting("ClickfinderUseSportLogos", "false").Value
            CheckBoxRemberSortedBy.Checked = _layer.GetSetting("ClickfinderRemberSortedBy", "true").Value
            CheckBoxDebugMode.Checked = _layer.GetSetting("ClickfinderDebugMode", "false").Value
            CheckBoxShowCategorieLocalMovies.Checked = _layer.GetSetting("ClickfinderCategorieShowLocalMovies", "false").Value
            CheckBoxShowCategorieLocalSeries.Checked = _layer.GetSetting("ClickfinderCategorieShowLocalSeries", "false").Value
            CheckBoxTvSeries.Checked = _layer.GetSetting("TvMovieImportTvSeriesInfos", "false").Value = "true"
            CheckBoxMovingPictures.Checked = _layer.GetSetting("TvMovieImportMovingPicturesInfos", "false").Value = "true"
            CheckBoxVideoDB.Checked = _layer.GetSetting("TvMovieImportVideoDatabaseInfos", "false").Value = "true"
            CheckBoxClickfinderPG.Checked = _layer.GetSetting("ClickfinderDataAvailable", "false").Value = "true"
            CheckBoxUseSeriesDescribtion.Checked = _layer.GetSetting("ClickfinderDetailUseSeriesDescribtion", "false").Value
            tbMPDatabasePath.Text = Config.GetFile(Config.Dir.Database, "")

            CheckBoxOverlayShowTagesTipp.Checked = _layer.GetSetting("ClickfinderOverlayShowTagesTipp", "false").Value
            CheckBoxOverlayShowLocalMovies.Checked = _layer.GetSetting("ClickfinderOverlayShowLocalMovies", "false").Value
            NumOverlayLimit.Value = _layer.GetSetting("ClickfinderOverlayMovieLimit", "10").Value

            CheckBoxEnableMovieOverlay.Checked = _layer.GetSetting("ClickfinderOverlayMoviesEnabled", "false").Value
            CheckBoxEnableSeriesOverlay.Checked = _layer.GetSetting("ClickfinderOverlaySeriesEnabled", "false").Value

            NumPreviewDays.Value = _layer.GetSetting("ClickfinderPreviewMaxDays", "7").Value
            NumPreviewMinTvMovieRating.Value = _layer.GetSetting("ClickfinderPreviewMinTvMovieRating", "1").Value

            If CheckBoxEnableMovieOverlay.Checked = True Then
                GroupBoxMovieOverlay.Enabled = True
            Else
                GroupBoxMovieOverlay.Enabled = False
            End If

            Select Case _layer.GetSetting("ClickfinderDetailsSeriesImage", "Cover").Value
                Case Is = "Cover"
                    RBSeriesCover.Checked = True
                Case Is = "FanArt"
                    RBSeriesFanArt.Checked = True
                Case Is = "Episode"
                    RBEpisodeImage.Checked = True
                Case Is = "TvMovie"
                    RBTvMovieImage.Checked = True
            End Select


            If CBool(_layer.GetSetting("TvMovieEnabled", "false").Value) = False Or CBool(_layer.GetSetting("ClickfinderEnabled", "true").Value) = False Then
                Dim message As New TvMoviePluginError
                message.ShowDialog()
                Me.Close()
            End If

            If Not _layer.GetSetting("TvMovieImportTvSeriesInfos", "false").Value = "true" Then
                GroupDetailSeriesImage.Enabled = False
            End If

            Select Case (_layer.GetSetting("ClickfinderOverviewMovieSort", "startTime").Value)
                Case Is = Helper.SortMethode.startTime.ToString
                    RBstartTime.Checked = True
                Case Is = Helper.SortMethode.TvMovieStar.ToString
                    RBTvMovieStar.Checked = True
                Case Is = Helper.SortMethode.RatingStar.ToString
                    RBRatingStar.Checked = True
            End Select

            Select Case (_layer.GetSetting("ClickfinderOverlayMovieSort", Helper.SortMethode.RatingStar.ToString).Value)
                Case Is = Helper.SortMethode.startTime.ToString
                    RBOverlayStartTime.Checked = True
                Case Is = Helper.SortMethode.TvMovieStar.ToString
                    RBOverlayTvMovieStar.Checked = True
                Case Is = Helper.SortMethode.RatingStar.ToString
                    RBOverlayRatingStar.Checked = True
            End Select

            Select Case (_layer.GetSetting("ClickfinderOverlayTime", "PrimeTime").Value)
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

                If _groups(i).GroupName = _layer.GetSetting("ClickfinderStandardTvGroup", "All Channels").Value Then
                    cbStandardGroup.Text = _groups(i).GroupName
                End If

                If _groups(i).GroupName = _layer.GetSetting("ClickfinderQuickTvGroup1", "All Channels").Value Then
                    CbQuick1.Text = _groups(i).GroupName
                End If

                If _groups(i).GroupName = _layer.GetSetting("ClickfinderQuickTvGroup2", "All Channels").Value Then
                    CbQuick2.Text = _groups(i).GroupName
                End If

                If _groups(i).GroupName = _layer.GetSetting("ClickfinderOverlayTvGroup", "All Channels").Value Then
                    CBOverlayGroup.Text = _groups(i).GroupName
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
                If _StartGuiList(i) = _layer.GetSetting("ClickfinderStartGui", "Highlights").Value Then
                    CBStartGui.Text = _StartGuiList(i)
                End If
            Next

        Catch ex As Exception
            MyLog.Error("[Setup_Load]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
        End Try

    End Sub

    Private Sub ButtonSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSave.Click
        Try

            Dim setting As Setting = _layer.GetSetting("ClickfinderDatabasePath", "")
            setting.Value = tbClickfinderDatabase.Text
            setting.Persist()


            setting = _layer.GetSetting("ClickfinderImagePath", "false")
            setting.Value = tbClickfinderImagePath.Text
            setting.Persist()

            setting = _layer.GetSetting("ClickfinderOverviewShowMoviesAfter", "12")
            setting.Value = CStr(NumShowMoviesAfter.Value)
            setting.Persist()

            setting = _layer.GetSetting("ClickfinderOverviewShowHighlightsAfter", "15")
            setting.Value = CStr(NumShowHighlightsAfter.Value)
            setting.Persist()

            setting = _layer.GetSetting("ClickfinderOverviewHighlightsMinRuntime", "16")
            setting.Value = CStr(NumHighlightsMinRuntime.Value)
            setting.Persist()


            setting = _layer.GetSetting("ClickfinderOverviewMaxDays", "10")
            setting.Value = CStr(NumMaxDays.Value)
            setting.Persist()

            'setting = _layer.GetSetting("ClickfinderNowOffset", "-20")
            'setting.Value = CStr(NumNowOffset.Value)
            'setting.Persist()

            setting = _layer.GetSetting("ClickfinderPrimeTime", "20:15")
            setting.Value = tbPrimeTime.Text
            setting.Persist()

            setting = _layer.GetSetting("ClickfinderLateTime", "22:00")
            setting.Value = tbLateTime.Text
            setting.Persist()

            setting = _layer.GetSetting("ClickfinderStandardTvGroup", "All Channels")
            setting.Value = cbStandardGroup.Text
            setting.Persist()

            setting = _layer.GetSetting("ClickfinderQuickTvGroup1", "All Channels")
            setting.Value = CbQuick1.Text
            setting.Persist()

            setting = _layer.GetSetting("ClickfinderQuickTvGroup2", "All Channels")
            setting.Value = CbQuick2.Text
            setting.Persist()

            setting = _layer.GetSetting("ClickfinderPluginName", "Clickfinder ProgramGuide")
            setting.Value = tbPluginName.Text
            setting.Persist()

            setting = _layer.GetSetting("ClickfinderOverlayMovieLimit", "10")
            setting.Value = NumOverlayLimit.Value
            setting.Persist()

            setting = _layer.GetSetting("ClickfinderOverlayTvGroup", "All Channels")
            setting.Value = CBOverlayGroup.Text
            setting.Persist()

            setting = _layer.GetSetting("ClickfinderStartGui", "Highlights")
            setting.Value = CBStartGui.Text
            setting.Persist()

            setting = _layer.GetSetting("ClickfinderPreviewMaxDays", "7")
            setting.Value = CStr(NumPreviewDays.Value)
            setting.Persist()

            setting = _layer.GetSetting("ClickfinderPreviewMinTvMovieRating", "1")
            setting.Value = CStr(NumPreviewMinTvMovieRating.Value)
            setting.Persist()

            SaveCategories()



            MyLog.Info("[Setup] close")
            MyLog.Info("")
            MyLog.Info("")

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

        Dim setting As Setting = _layer.GetSetting("ClickfinderOverviewShowLocalMovies", "false")
        setting.Value = CStr(CheckBoxShowLocalMovies.Checked)
        setting.Persist()
    End Sub

    Private Sub CheckBoxShowTagesTipp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxShowTagesTipp.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderOverviewShowTagesTipp", "false")
        setting.Value = CStr(CheckBoxShowTagesTipp.Checked)
        setting.Persist()
    End Sub

    Private Sub CheckBoxUseSportLogos_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxUseSportLogos.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderUseSportLogos", "false")
        setting.Value = CStr(CheckBoxUseSportLogos.Checked)
        setting.Persist()
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

        Try
            Broker.Execute("drop table mptvdb.ClickfinderCategories")
            Broker.Execute("CREATE TABLE mptvdb.ClickfinderCategories ( idClickfinderCategories INT NOT NULL AUTO_INCREMENT , Name VARCHAR(255) , Beschreibung VARCHAR(255) , isVisible BIT(1) NOT NULL DEFAULT 0 , Image VARCHAR(255) , SqlString TEXT , sortOrder INT NOT NULL DEFAULT 0 , MinRunTime INT NOT NULL DEFAULT 0 , NowOffset INT NOT NULL DEFAULT 0 , sortedBy VARCHAR(100) , groupName VARCHAR(100) , PRIMARY KEY (idClickfinderCategories) )")
        Catch ex As Exception
            Broker.Execute("CREATE TABLE mptvdb.ClickfinderCategories ( idClickfinderCategories INT NOT NULL AUTO_INCREMENT , Name VARCHAR(255) , Beschreibung VARCHAR(255) , isVisible BIT(1) NOT NULL DEFAULT 0 , Image VARCHAR(255) , SqlString TEXT , sortOrder INT NOT NULL DEFAULT 0 , MinRunTime INT NOT NULL DEFAULT 0 , NowOffset INT NOT NULL DEFAULT 0 , sortedBy VARCHAR(100) , groupName VARCHAR(100) , PRIMARY KEY (idClickfinderCategories) )")
        End Try

    End Sub

    Private Sub CreateClickfinderCategories()
        Try

            Dim _Categorie As New ClickfinderCategories("Movies", "Alle Filme nach Uhrzeit, Rating, TvMovieRating sortiert", True, 0, 80, 30)
            _Categorie.SqlString = "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram WHERE startTime >= #StartTime AND startTime <= #EndTime AND starRating >= 1 AND TvMovieBewertung < 6 AND (genre NOT LIKE '%Serie' OR genre NOT LIKE '%Reihe' OR genre NOT LIKE '%Sitcom%' OR genre NOT LIKE '%Zeichentrick%') " & Helper.ORDERBYstartTime
            _Categorie.Image = _Categorie.Name & ".png"
            _Categorie.Persist()

            _Categorie = New ClickfinderCategories("Serien", "Alle Serien nach Uhrzeit, Rating und TvMovieRating sortiert", True, 1, 10, 15)
            _Categorie.SqlString = "Select * FROM program LEFT JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram WHERE startTime >= #StartTime AND startTime <= #EndTime AND (Genre LIKE '%Serie' OR genre LIKE '%Reihe' OR genre LIKE '%Sitcom%') " & Helper.ORDERBYstartTime
            _Categorie.Image = _Categorie.Name & ".png"
            _Categorie.Persist()

            _Categorie = New ClickfinderCategories("Dokumentationen", "Alle Dokumentationen nach Uhrzeit und TvMovieRating sortiert", True, 2, 30, 40)
            _Categorie.SqlString = "Select * FROM program LEFT JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram WHERE startTime >= #StartTime AND startTime <= #EndTime AND genre LIKE '%Doku%' " & Helper.ORDERBYstartTime
            _Categorie.Image = _Categorie.Name & ".png"
            _Categorie.Persist()

            _Categorie = New ClickfinderCategories("Sport", "Alle Sport Sendungen nach Uhrzeit und TvMovieRating sortiert", True, 3, 10, 30)
            _Categorie.SqlString = "Select * FROM program LEFT JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram WHERE startTime >= #StartTime AND startTime <= #EndTime AND (title LIKE '%Sport%' OR title LIKE '%Fußball%' OR episodeName LIKE '%Sport%' OR genre LIKE '%Sport%' OR genre LIKE '%football%soccer%' OR genre LIKE '%ball%' OR genre LIKE '%Automagazin%' OR genre LIKE '%Biathlon%' OR genre LIKE '%Billard%' OR genre LIKE '%Bobsport%' OR genre LIKE '%Bowling%' OR genre LIKE '%Boxen%' OR genre LIKE '%Darts%' OR genre LIKE '%Eishockey%' OR genre LIKE '%E-Sport%' OR genre LIKE '%Formel 1%' OR genre LIKE '%Fun- u. Extremsport%' OR genre LIKE '%Golf%' OR genre LIKE '%Leichtathletik%' OR genre LIKE '%Motorrad%' OR genre LIKE '%Nordische Kombination%' OR genre LIKE '%Poker%' OR genre LIKE '%Rennrodeln%' OR genre LIKE '%Segeln%' OR genre LIKE '%Ski%' or genre LIKE '%Snowboard%' OR genre LIKE '%Tennis%' OR genre LIKE '%Wrestling%' OR genre LIKE '%sports (general)%') " & Helper.ORDERBYstartTime
            _Categorie.Image = _Categorie.Name & ".png"
            _Categorie.Persist()

            _Categorie = New ClickfinderCategories("Reportagen", "Alle Reportagen nach Uhrzeit und TvMovieRating sortiert", True, 4, 20, 20)
            _Categorie.SqlString = "Select * FROM program LEFT JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram WHERE startTime >= #StartTime AND startTime <= #EndTime AND genre LIKE '%Report%' " & Helper.ORDERBYstartTime
            _Categorie.Image = _Categorie.Name & ".png"
            _Categorie.Persist()

            _Categorie = New ClickfinderCategories("Magazine", "Alle Magazine nach Uhrzeit und TvMovieRating sortiert", True, 5, 15, 15)
            _Categorie.SqlString = "Select * FROM program LEFT JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram WHERE startTime >= #StartTime AND startTime <= #EndTime AND genre LIKE '%Magazin%' " & Helper.ORDERBYstartTime
            _Categorie.Image = _Categorie.Name & ".png"
            _Categorie.Persist()

            _Categorie = New ClickfinderCategories("HDTV", "Alle HDTV Sendungen nach Uhrzeit und TvMovieRating sortiert", True, 6, 25, 30)
            _Categorie.SqlString = "Select * FROM (program LEFT JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram) INNER JOIN channel ON program.idChannel = channel.idChannel WHERE startTime >= #StartTime AND startTime <= #EndTime AND displayName LIKE '%HD%' " & Helper.ORDERBYstartTime
            _Categorie.Image = _Categorie.Name & ".png"
            _Categorie.Persist()

            _Categorie = New ClickfinderCategories("Sky Cinema", "Alle Sendungen des Packets Film von Sky nach Uhrzeit und TvMovieRating sortiert", True, 7, 80, 30)
            _Categorie.SqlString = "Select * FROM (program LEFT JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram) INNER JOIN channel ON program.idChannel = channel.idChannel WHERE startTime >= #StartTime AND startTime <= #EndTime AND (displayName LIKE '%SKY Cinema%' OR displayName LIKE '%SKY Cinema HD%' OR displayName LIKE '%SKY Action%' OR displayName LIKE '%SKY Action HD%' OR displayName LIKE '%MGM%' OR displayName LIKE '%Disney Cinemagic%' OR displayName LIKE '%Disney Cinemagic HD%' OR displayName LIKE '%SKY Comedy%' OR displayName LIKE '%SKY Emotion%' OR displayName LIKE '%SKY Nostalgie%') " & Helper.ORDERBYstartTime
            _Categorie.Image = _Categorie.Name & ".png"
            _Categorie.Persist()

            _Categorie = New ClickfinderCategories("Sky Dokumentationen", "Alle Dokumentationen von Sky nach Uhrzeit und TvMovieRating sortiert", True, 8, 30, 40)
            _Categorie.SqlString = "Select * FROM (program LEFT JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram) INNER JOIN channel ON program.idChannel = channel.idChannel WHERE startTime >= #StartTime AND startTime <= #EndTime AND (displayName LIKE '%Discovery%' OR displayName LIKE '%History%' OR displayName LIKE '%National Geographic%' OR displayName LIKE '%Spiegel Geschichte%' OR displayName LIKE '%MOTORVISION TV%' OR displayName LIKE '%The Biography Channel%') " & Helper.ORDERBYstartTime
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

    Private Sub ButtonCategoriesDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCategoriesDefault.Click
        Try
            Dim antwort As MsgBoxResult
            antwort = MsgBox("Möchtes du wirklich die Default Kategorien wieder herstellen?" & vbNewLine & _
                             "Alle durchgeführten Änderungen werden dadurch gelöscht !", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Restore Default")

            If antwort = MsgBoxResult.Yes Then
                CreateClickfinderCategoriesTable()
                CreateClickfinderCategories()
                Filldgv()
                MyLog.[Debug]("[ButtonCategoriesDefault_Click]: Restore default Categories")
            End If

        Catch ex As Exception
            MyLog.Error("[ButtonCategoriesDefault_Click]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
        End Try
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

        Dim setting As Setting = _layer.GetSetting("ClickfinderItemsShowLocalMovies", "false")
        setting.Value = CStr(CheckBoxFilterShowLocalMovies.Checked)
        setting.Persist()
    End Sub

    Private Sub CheckBoxFilterShowLocalSeries_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxFilterShowLocalSeries.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderItemsShowLocalSeries", "false")
        setting.Value = CStr(CheckBoxFilterShowLocalSeries.Checked)
        setting.Persist()
    End Sub

    Private Sub RBstartTime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBstartTime.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderOverviewMovieSort", "startTime")
        setting.Value = Helper.SortMethode.startTime.ToString
        setting.Persist()
    End Sub

    Private Sub RBTvMovieStar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBTvMovieStar.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderOverviewMovieSort", "startTime")
        setting.Value = Helper.SortMethode.TvMovieStar.ToString
        setting.Persist()
    End Sub

    Private Sub RBRatingStar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBRatingStar.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderOverviewMovieSort", "startTime")
        setting.Value = Helper.SortMethode.RatingStar.ToString
        setting.Persist()
    End Sub

    Private Sub CheckBoxRemberSortedBy_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxRemberSortedBy.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderRemberSortedBy", "true")
        setting.Value = CStr(CheckBoxRemberSortedBy.Checked)
        setting.Persist()
    End Sub

    Private Sub CheckBoxDebugMode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxDebugMode.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderDebugMode", "false")
        setting.Value = CStr(CheckBoxDebugMode.Checked)
        setting.Persist()
    End Sub


    Private Sub CheckBoxShowCategorieLocalMovies_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxShowCategorieLocalMovies.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderCategorieShowLocalMovies", "false")
        setting.Value = CStr(CheckBoxShowCategorieLocalMovies.Checked)
        setting.Persist()
    End Sub

    Private Sub CheckBoxShowCategorieLocalSeries_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxShowCategorieLocalSeries.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderCategorieShowLocalSeries", "false")
        setting.Value = CStr(CheckBoxShowCategorieLocalSeries.Checked)
        setting.Persist()
    End Sub

    Private Sub CheckBoxUseSeriesDescribtion_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxUseSeriesDescribtion.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderDetailUseSeriesDescribtion", "false")
        setting.Value = CStr(CheckBoxUseSeriesDescribtion.Checked)
        setting.Persist()
    End Sub

    Private Sub RBSeriesCover_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBSeriesCover.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderDetailsSeriesImage", "Cover")
        setting.Value = "Cover"
        setting.Persist()
    End Sub

    Private Sub RBSeriesFanArt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBSeriesFanArt.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderDetailsSeriesImage", "Cover")
        setting.Value = "FanArt"
        setting.Persist()
    End Sub

    Private Sub RBEpisodeImage_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBEpisodeImage.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderDetailsSeriesImage", "Cover")
        setting.Value = "Episode"
        setting.Persist()
    End Sub

    Private Sub RBTvMovieImage_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBTvMovieImage.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderDetailsSeriesImage", "Cover")
        setting.Value = "TvMovie"
        setting.Persist()
    End Sub

    Private Sub CheckBoxOverlayShowTagesTipp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxOverlayShowTagesTipp.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderOverlayShowTagesTipp", "false")
        setting.Value = CStr(CheckBoxOverlayShowTagesTipp.Checked)
        setting.Persist()
    End Sub

    Private Sub CheckBoxOverlayShowLocalMovies_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxOverlayShowLocalMovies.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderOverlayShowLocalMovies", "false")
        setting.Value = CStr(CheckBoxOverlayShowLocalMovies.Checked)
        setting.Persist()
    End Sub

    Private Sub RBOverlayHeute_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOverlayHeute.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderOverlayTime", "PrimeTime")
        setting.Value = "Today"
        setting.Persist()
    End Sub

    Private Sub RBOverlayNow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOverlayNow.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderOverlayTime", "PrimeTime")
        setting.Value = "Now"
        setting.Persist()
    End Sub

    Private Sub RBOverlayPrimeTime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOverlayPrimeTime.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderOverlayTime", "PrimeTime")
        setting.Value = "PrimeTime"
        setting.Persist()
    End Sub

    Private Sub RBOverlayLateTime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOverlayLateTime.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderOverlayTime", "PrimeTime")
        setting.Value = "LateTime"
        setting.Persist()
    End Sub

    Private Sub RBOverlayStartTime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOverlayStartTime.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderOverlayMovieSort", Helper.SortMethode.RatingStar.ToString)
        setting.Value = Helper.SortMethode.startTime.ToString
        setting.Persist()
    End Sub

    Private Sub RBOverlayTvMovieStar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOverlayTvMovieStar.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderOverlayMovieSort", Helper.SortMethode.RatingStar.ToString)
        setting.Value = Helper.SortMethode.TvMovieStar.ToString
        setting.Persist()
    End Sub

    Private Sub RBOverlayRatingStar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOverlayRatingStar.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderOverlayMovieSort", Helper.SortMethode.RatingStar.ToString)
        setting.Value = Helper.SortMethode.RatingStar.ToString
        setting.Persist()
    End Sub

    Private Sub CheckBoxEnableMovieOverlay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxEnableMovieOverlay.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderOverlayMoviesEnabled", "false")
        setting.Value = CStr(CheckBoxEnableMovieOverlay.Checked)
        setting.Persist()

        If CheckBoxEnableMovieOverlay.Checked = True Then
            GroupBoxMovieOverlay.Enabled = True
        Else
            GroupBoxMovieOverlay.Enabled = False
        End If

    End Sub

    Private Sub CheckBoxEnableSeriesOverlay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxEnableSeriesOverlay.CheckedChanged

        Dim setting As Setting = _layer.GetSetting("ClickfinderOverlaySeriesEnabled", "false")
        setting.Value = CStr(CheckBoxEnableSeriesOverlay.Checked)
        setting.Persist()
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=WUTCGQGMATVB4")
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("https://code.google.com/p/clickfinder-programguide/wiki/Manual_de")
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class

