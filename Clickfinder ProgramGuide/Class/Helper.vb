Imports MediaPortal.GUI.Library
Imports TvDatabase
Imports TvPlugin
Imports MediaPortal.Dialogs
Imports MediaPortal.GUI.Library.GUIWindow
Imports ClickfinderProgramGuide.ClickfinderProgramGuide
Imports System.Drawing
Imports Gentle.Framework
Imports Gentle.Common
Imports MediaPortal.Configuration
Imports ClickfinderProgramGuide.TvDatabase
Imports MediaPortal.GUI.Home
Imports MediaPortal.GUI.Video
Imports MediaPortal.Player
Imports MediaPortal.Playlists
Imports MediaPortal.TagReader
Imports System.Threading
Imports System.Globalization


Imports enrichEPG
Imports SQLite.NET
Imports MediaPortal.Database
Imports enrichEPG.TvDatabase
Imports enrichEPG.Database
Imports System.Reflection


Public Class Helper
#Region "Members"
    Private Shared _PlayedFile As TVMovieProgram
    Friend Shared _DbAbgleichRuning As Boolean = False

#End Region

#Region "Properties"
    Public Shared ReadOnly Property Version() As String
        Get
            Return Assembly.GetExecutingAssembly().GetName().Version.ToString
        End Get
    End Property


    Friend Shared ReadOnly Property ORDERBYstartTime() As String
        Get
            Return "ORDER BY startTime ASC, starRating DESC, title ASC"
        End Get
    End Property
    Friend Shared ReadOnly Property ORDERBYTvMovieStar() As String
        Get
            Return "ORDER BY TVMovieBewertung DESC, starRating DESC, startTime ASC, title ASC"
        End Get
    End Property
    Friend Shared ReadOnly Property ORDERBYstarRating() As String
        Get
            Return "ORDER BY starRating DESC, TVMovieBewertung DESC, startTime ASC, title ASC"
        End Get
    End Property

    Friend Shared ReadOnly Property ORDERBYgerne() As String
        Get
            Return "ORDER BY genre ASC, starRating DESC, startTime ASC, title ASC, TVMovieBewertung DESC"
        End Get
    End Property
    Friend Shared ReadOnly Property ORDERBYparentalRating() As String
        Get
            Return "ORDER BY parentalRating DESC, starRating DESC, startTime ASC, title ASC, TVMovieBewertung DESC"
        End Get
    End Property

    Friend Shared ReadOnly Property ORDERBYTitle() As String
        Get
            Return "ORDER BY title ASC, starRating DESC, startTime ASC"
        End Get
    End Property
    Friend Shared ReadOnly Property ORDERBYFun() As String
        Get
            Return "ORDER BY fun DESC, starRating DESC, startTime ASC"
        End Get
    End Property

    Friend Shared ReadOnly Property ORDERBYAction() As String
        Get
            Return "ORDER BY Action DESC, starRating DESC, startTime ASC"
        End Get
    End Property

    Friend Shared ReadOnly Property ORDERBYFeelings() As String
        Get
            Return "ORDER BY Feelings DESC, starRating DESC, startTime ASC"
        End Get
    End Property

    Friend Shared ReadOnly Property ORDERBYErotic() As String
        Get
            Return "ORDER BY Erotic DESC, starRating DESC, startTime ASC"
        End Get
    End Property

    Friend Shared ReadOnly Property ORDERBYTension() As String
        Get
            Return "ORDER BY Tension DESC, starRating DESC, startTime ASC"
        End Get
    End Property
    Friend Shared ReadOnly Property ORDERBYRequirement() As String
        Get
            Return "ORDER BY Requirement DESC, starRating DESC, startTime ASC"
        End Get
    End Property
    Friend Shared ReadOnly Property ORDERBYSeries() As String
        Get
            Return "ORDER BY idSeries DESC, starRating DESC, startTime ASC"
        End Get
    End Property

    Friend Shared ReadOnly Property ORDERBYEpisode() As String
        Get
            Return "ORDER BY seriesNum, episodeNum"
        End Get
    End Property

    Friend Shared ReadOnly Property ORDERBYYear() As String
        Get
            Return "ORDER BY OriginalAirDate DESC, starRating DESC, startTime ASC"
        End Get
    End Property

#End Region

    Friend Enum SortMethode
        startTime
        TvMovieStar
        RatingStar
        Genre
        Year
        parentalRating
        Series
        Episode
        Title
        Action
        Fun
        Feelings
        Tension
        Erotic
        Requirement
    End Enum

    Friend Shared Sub AddListControlItem(ByVal Listcontrol As GUIListControl, ByVal idProgram As Integer, ByVal ChannelName As String, ByVal titelLabel As String, Optional ByVal timeLabel As String = "", Optional ByVal infoLabel As String = "", Optional ByVal ImagePath As String = "", Optional ByVal MinRunTime As Integer = 0, Optional ByVal isRecording As String = "", Optional ByVal tmpInfo As String = "", Optional ByVal Thumbnail As String = "")
        Try

            Dim lItem As New GUIListItem

            lItem.Label = titelLabel
            lItem.Label2 = timeLabel
            lItem.Label3 = infoLabel
            lItem.ItemId = idProgram
            lItem.Path = ChannelName
            lItem.IconImage = ImagePath
            lItem.ThumbnailImage = Thumbnail
            lItem.Duration = MinRunTime
            lItem.PinImage = isRecording
            lItem.TVTag = tmpInfo

            GUIControl.AddListItemControl(GUIWindowManager.ActiveWindow, Listcontrol.GetID, lItem)


            lItem.Dispose()

        Catch ex As Exception
            MyLog.Error("[Helper]: [AddListControlItem]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
        End Try

    End Sub

    Friend Shared Function MySqlDate(ByVal Datum As Date) As String
        Try
            If Gentle.Framework.Broker.ProviderName = "MySQL" Then
                Return "'" & Datum.Year & "-" & Format(Datum.Month, "00") & "-" & Format(Datum.Day, "00") & " " & Format(Datum.Hour, "00") & ":" & Format(Datum.Minute, "00") & ":00'"
            Else
                Return "'" & Datum.Year & Format(Datum.Month, "00") & Format(Datum.Day, "00") & " " & Format(Datum.Hour, "00") & ":" & Format(Datum.Minute, "00") & ":" & Format(Datum.Second, "00") & "'"
            End If

        Catch ex As Exception
            MyLog.Error("[Helper]: [MySqlDate]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            Return ""
        End Try
    End Function

    Friend Shared Function AppendSqlLimit(ByVal sqlstring As String, ByVal limit As Integer) As String
        Try
            If Gentle.Framework.Broker.ProviderName = "MySQL" Then
                Return sqlstring & " LIMIT " & limit
            Else
                Return Replace(sqlstring, "Select ", "Select top " & limit & " ")
            End If
        Catch ex As Exception
            MyLog.Error("[Helper]: [AppendSqlLimit]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            Return ""
        End Try
    End Function

    'Friend Shared Function AllowedSignsSQL(ByVal expression As String)
    '    Return Replace(System.Text.RegularExpressions.Regex.Replace(expression, "[\?,.!-*]", "_"), "'", "''")
    'End Function

    Friend Shared Sub ListControlClick(ByVal idProgram As Integer)
        Try
            Dim TvMovieProgram As TVMovieProgram = TvMovieProgram.Retrieve(idProgram)
            GUIWindowManager.ActivateWindow(1656544652, "list: " & TvMovieProgram.idProgram)
        Catch ex As Exception
            MyLog.Error("[Helper]: [ListControlClick]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Friend Shared Sub LoadTVProgramInfo(ByVal Program As Program)

        Try
            TvPlugin.TVProgramInfo.CurrentProgram = Program
            GUIWindowManager.ActivateWindow(CInt(Window.WINDOW_TV_PROGRAM_INFO))
        Catch ex As Exception
            MyLog.Error("[Helper]: [LoadTVProgramInfo]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
        End Try

    End Sub
    'MP Notify für Sendung setzen
    Friend Shared Sub SetNotify(ByVal Program As Program)
        Try
            Dim Erinnerung As Program = Program.Retrieve(Program.IdProgram)
            Erinnerung.Notify = True
            Erinnerung.Persist()
            TvNotifyManager.OnNotifiesChanged()

            MPDialogOK("Erinnerung:", Erinnerung.Title, Erinnerung.StartTime & " - " & Erinnerung.EndTime, Erinnerung.ReferencedChannel.DisplayName)

        Catch ex As Exception
            MyLog.Error("[Helper]: [SetNotify]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            ShowNotify(ex.Message)
        End Try
    End Sub
    'MP Tv Kanal einschalten
    Friend Shared Sub StartTv(ByVal program As Program)
        Try
            Dim _TvMovieprogram As TVMovieProgram = TVMovieProgram.Retrieve(program.IdProgram)

            If Not String.IsNullOrEmpty(_TvMovieprogram.FileName) Then
                _PlayedFile = _TvMovieprogram

                g_Player.Play(_TvMovieprogram.FileName)
                g_Player.ShowFullScreenWindowVideoDefault()

            Else
                Dim changeChannel As Channel = DirectCast(program.ReferencedChannel, Channel)
                MediaPortal.GUI.Library.GUIWindowManager.ActivateWindow(CInt(MediaPortal.GUI.Library.GUIWindow.Window.WINDOW_TVFULLSCREEN))
                TVHome.ViewChannelAndCheck(changeChannel)
            End If

        Catch ex As Exception
            MyLog.Error("[Helper]: [StartTv]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            ShowNotify(ex.Message)
        End Try
    End Sub

    'MediaPortal Dialoge
    Friend Shared Sub MPDialogOK(ByVal Heading As String, ByVal StringLine1 As String, Optional ByVal StringLine2 As String = "", Optional ByVal StringLine3 As String = "")
        Try
            Dim dlg As GUIDialogOK = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_OK, Integer)), GUIDialogOK)

            dlg.SetHeading(Heading)
            dlg.SetLine(1, StringLine1)
            dlg.SetLine(2, StringLine2)
            dlg.SetLine(3, StringLine3)
            dlg.DoModal(GUIWindowManager.ActiveWindow)
            dlg.Dispose()
            dlg.AllocResources()
        Catch ex As Exception
            MyLog.Error("[Helper]: [StartTv]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
        End Try

    End Sub

    Friend Shared Sub ShowActionMenu(ByVal Program As Program)

        Try

            Dim dlgContext As GUIDialogMenu = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_MENU, Integer)), GUIDialogMenu)
            dlgContext.Reset()

            'ContextMenu Layout
            dlgContext.SetHeading(Program.Title)
            dlgContext.ShowQuickNumbers = True

            'Kanal einschalten
            Dim lItemOn As New GUIListItem
            lItemOn.Label = Translation.ChannelON
            dlgContext.Add(lItemOn)
            lItemOn.Dispose()

            'Aufnehmen
            Dim lItemRec As New GUIListItem
            lItemRec.Label = Translation.Record
            dlgContext.Add(lItemRec)
            lItemOn.Dispose()

            'Erinnern
            Dim lItemRem As New GUIListItem
            lItemRem.Label = Translation.Remember
            dlgContext.Add(lItemRem)
            lItemRec.Dispose()

            'IMDB OnlineVideos
            Dim lItemIMDB As New GUIListItem
            lItemIMDB.Label = Translation.IMDB
            dlgContext.Add(lItemIMDB)
            lItemIMDB.Dispose()

            'mit Serie verlinken
            Dim lItemSerieLink As New GUIListItem
            lItemSerieLink.Label = Translation.SerieLinkLabel
            dlgContext.Add(lItemSerieLink)
            lItemSerieLink.Dispose()

            'mit DB aktualisieren
            Dim lItemDBUpdate As New GUIListItem
            lItemDBUpdate.Label = Translation.DBRefresh
            dlgContext.Add(lItemDBUpdate)
            lItemDBUpdate.Dispose()

            dlgContext.DoModal(GUIWindowManager.ActiveWindow)

            Select Case dlgContext.SelectedLabel
                Case Is = 0
                    StartTv(Program)
                    MyLog.Debug("[ShowActionMenu]: selected -> start tv (channel)")
                    MyLog.Debug("")
                Case Is = 1
                    LoadTVProgramInfo(Program)
                    MyLog.Debug("[ShowActionMenu]: selected -> open TvProgramInfo Gui")
                    MyLog.Debug("")
                Case Is = 2
                    SetNotify(Program)
                    MyLog.Debug("[ShowActionMenu]: selected -> set notify")
                    MyLog.Debug("")
                Case Is = 3
                    If PluginManager.IsPlugInEnabled(4755) = True Then
                        GUIWindowManager.ActivateWindow(4755, "site:IMDb Movie Trailers|search:" & Program.Title & "|return:Locked")
                    Else
                        ShowNotify("OnlineVideos plugin nicht verfügbar!")
                    End If
                Case Is = 4
                    ShowSerieLinkContextMenu(Program)
                Case Is = 5

                    If _DbAbgleichRuning = True Then

                        Notify(Translation.DBRefreshRunning)
                    Else
                        Dim tmp As New Thread(AddressOf startEnrichEPG)
                        tmp.IsBackground = True
                        tmp.Start()
                    End If

                Case Else
                    MyLog.Debug("[ShowActionMenu]: exit")
                    MyLog.Debug("")
            End Select

            dlgContext.Dispose()
            dlgContext.AllocResources()

        Catch ex As Exception
            MyLog.Error("[Helper]: [ShowActionMenu]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Friend Shared Sub startEnrichEPG()
        Try
            MyLog.Debug("[ShowActionMenu]: enrichEPG started")

            Notify(Translation.DBRefreshStarted)

            _DbAbgleichRuning = True
            GuiLayout.SetSettingLastUpdateProperty()

            Dim _EpisodenScannerPath As String = String.Empty

            If Not String.IsNullOrEmpty(CPGsettings.EpisodenScanner) Then
                _EpisodenScannerPath = IO.Path.GetDirectoryName(CPGsettings.EpisodenScanner)
            End If

            Dim enrichEPG As New enrichEPG.EnrichEPG(Config.GetFile(Config.Dir.Database, ""), _
                                                     CPGsettings.TvMovieImportTvSeriesInfos, _
                                                     CPGsettings.TvMovieImportVideoDatabaseInfos, _
                                                     CPGsettings.TvMovieImportMovingPicturesInfos, _
                                                     Date.Now, MySettings.LogPath.Client, _EpisodenScannerPath, , , _
                                                     "ClickfinderProgramGuide_enrichEPG.log", CPGsettings.TvMovieUseTheTvDb, , _
                                                     Config.GetFile(Config.Dir.Thumbs, ""))


            Dim _enrichEPGThread As New Thread(AddressOf enrichEPG.start)

            _enrichEPGThread.Start()
            _enrichEPGThread.Join()

            Translator.SetProperty("#SettingLastUpdate", Translation.RefreshOverlays)
            ClickfinderProgramGuide.StartGuiWindow.RefreshOverlays()

            Notify(Translation.DBRefreshFinished)
            _DbAbgleichRuning = False
            GuiLayout.SetSettingLastUpdateProperty()

        Catch ex As Exception
            Notify("Error by starting enrichEPG !!!")
            MyLog.Error("[Helper]: [startEnrichEPG]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Friend Shared Sub ShowSerieLinkContextMenu(ByVal program As Program)

        MyLog.Info("")
        MyLog.Info("[Helper] [ShowSerieLinkContextMenu]: open")
        MyLog.Info("")

        Try
            Dim dlgContext As GUIDialogSelect2Custom = CType(GUIWindowManager.GetWindow(CType(1656544655, Integer)), GUIDialogSelect2Custom)
            dlgContext.Reset()

            Dim _idSeriesContainer As Dictionary(Of Integer, Integer) = New Dictionary(Of Integer, Integer)

            If CPGsettings.TvMovieImportTvSeriesInfos = True Then
                'ContextMenu Layout
                dlgContext.SetHeading(program.Title)

                Dim _SeriesList As List(Of MyTvSeries) = MyTvSeries.ListAll
                Dim _counter As Integer = 0

                ''Alle Serien aus DB laden
                'Dim _TvSeriesDB As New TVSeriesDB
                '_TvSeriesDB.LoadAllSeries()

                'Alle Serien + idSeries in CB schreiben
                For Each _Series In _SeriesList
                    Dim lItemSerie As New GUIListItem
                    lItemSerie.Label = _Series.Title

                    Try
                        'Sofern verlinkung vorhanden
                        Dim SeriesMapping As TvMovieSeriesMapping = TvMovieSeriesMapping.Retrieve(_Series.idSeries)
                        lItemSerie.Label3 = Translation.LinkTo & " " & SeriesMapping.EpgTitle
                    Catch ex As Exception
                        'sonst abfangen
                    End Try

                    lItemSerie.IconImage = Config.GetFile(Config.Dir.Thumbs, "MPTVSeriesBanners\") & _Series.PosterImage

                    _idSeriesContainer.Add(_counter, _Series.idSeries)
                    _counter = _counter + 1

                    dlgContext.Add(lItemSerie)
                    lItemSerie.Dispose()

                Next
            End If

            dlgContext.DoModal(GUIWindowManager.ActiveWindow)


            Select Case dlgContext.SelectedLabel
                Case Is = _idSeriesContainer.Keys(dlgContext.SelectedLabel)

                    Try
                        Dim _SeriesMapping As TvMovieSeriesMapping = TvMovieSeriesMapping.Retrieve(_idSeriesContainer.Item(dlgContext.SelectedLabel))

                        If String.IsNullOrEmpty(_SeriesMapping.EpgTitle) = True Then
                            _SeriesMapping.EpgTitle = program.Title
                            _SeriesMapping.Persist()
                        Else
                            'Verlinkung schon vorhanden -> neuer Dlg überschreiben oder hinzufügen
                            ShowSeriesLinkManagementContextMenu(_SeriesMapping, program.Title)
                        End If

                    Catch ex As Exception
                        Dim _SeriesMapping As New TvMovieSeriesMapping(_idSeriesContainer.Item(dlgContext.SelectedLabel))
                        _SeriesMapping.EpgTitle = program.Title
                        _SeriesMapping.Persist()
                    End Try

                    dlgContext.Dispose()
                    dlgContext.AllocResources()
                Case Else
                    'Exit dlgcontext
            End Select

        Catch ex As Exception
            MyLog.Error("[Helper] [ShowSerieLinkContextMenu]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Shared Sub ShowSeriesLinkManagementContextMenu(ByVal Mapping As TvMovieSeriesMapping, ByVal NewSeriesMapping As String)

        Try
            Dim dlgContext As GUIDialogMenu = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_MENU, Integer)), GUIDialogMenu)
            dlgContext.Reset()

            'ContextMenu Layout
            dlgContext.SetHeading(NewSeriesMapping)
            dlgContext.ShowQuickNumbers = True

            'Verlinkung hinzufügen
            Dim lItemAdd As New GUIListItem
            lItemAdd.Label = Translation.MappingAppend
            dlgContext.Add(lItemAdd)
            lItemAdd.Dispose()

            'Verlinkung überschreiben
            Dim lItemOverwrite As New GUIListItem
            lItemOverwrite.Label = Translation.MappingOverwrite
            dlgContext.Add(lItemOverwrite)
            lItemOverwrite.Dispose()

            dlgContext.DoModal(GUIWindowManager.ActiveWindow)

            Select Case dlgContext.SelectedLabel
                Case Is = 0
                    Mapping.EpgTitle = Mapping.EpgTitle & "|" & NewSeriesMapping
                    Mapping.Persist()
                Case Is = 1
                    Mapping.EpgTitle = NewSeriesMapping
                    Mapping.Persist()
            End Select

            dlgContext.Dispose()
            dlgContext.AllocResources()

        Catch ex As Exception
            MyLog.Error("[Helper] [ShowSeriesLinkManagementContextMenu]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Friend Shared Function UseSportLogos(ByVal title As String, ByVal imagePathFallback As String) As String
        If title.Contains("Fußball: Bundesliga") Then
            Return Config.GetFile(Config.Dir.Thumbs, "tv\logos\") & "Bundesliga" & ".png"
        ElseIf title.Contains("Fußball: 2. Bundesliga") Then
            Return Config.GetFile(Config.Dir.Thumbs, "tv\logos\") & "Bundesliga" & ".png"
        ElseIf title.Contains("Fußball: UEFA Champions League") Then
            Return Config.GetFile(Config.Dir.Thumbs, "tv\logos\") & "Champ. League" & ".png"
        ElseIf title.Contains("Fußball: Premier League") Then
            Return Config.GetFile(Config.Dir.Thumbs, "tv\logos\") & "Fußball ENG" & ".png"
        ElseIf title.Contains("Fußball: UEFA Europa League") Then
            Return Config.GetFile(Config.Dir.Thumbs, "tv\logos\") & "Europa League" & ".png"
        ElseIf title.Contains("Fußball: DFB-Pokal") Then
            Return Config.GetFile(Config.Dir.Thumbs, "tv\logos\") & "DFB-Konf." & ".png"
        Else
            Return imagePathFallback
        End If

    End Function

    Friend Shared Function getTranslatedDayOfWeek(ByVal Datum As Date) As String
        Try
            Dim _Result As String = String.Empty

            Select Case Datum.DayOfWeek
                Case DayOfWeek.Monday
                    _Result = Translation.Monday
                Case DayOfWeek.Tuesday
                    _Result = Translation.Tuesday
                Case DayOfWeek.Wednesday
                    _Result = Translation.Wednesday
                Case DayOfWeek.Thursday
                    _Result = Translation.Thursday
                Case DayOfWeek.Friday
                    _Result = Translation.Friday
                Case DayOfWeek.Saturday
                    _Result = Translation.Saturday
                Case DayOfWeek.Sunday
                    _Result = Translation.Sunday
            End Select

            If Datum.Date = Today Then
                _Result = Translation.Today
            End If

            If Datum.Date = Today.AddDays(1) Then
                _Result = Translation.Tomorrow
            End If

            If Datum.Date = Today.AddDays(-1) Then
                _Result = Translation.Yesterday
            End If

            Return _Result
        Catch ex As Exception
            MyLog.Error("[Helper] [getTranslatedDayOfWeek]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            Return ""
        End Try

    End Function

    Friend Shared Sub CheckConnectionState()
        Try
            'TvServer nicht verbunden / online
            If Helper.TvServerConnected = False Then
                ShowNotify(Translation.TvServerOffline)
                Return
            End If

            'Clickfinder DB nicht gefunden
            If IO.File.Exists(CPGsettings.ClickfinderDatabasePath) = False Then
                ShowNotify(Translation.ClickfinderDBOffline)
                Return
            End If

            'TvMovie EGP IMport++ plugin nicht aktiviert / installiert
            If CPGsettings.TvMovieEnabled = False Then
                ShowNotify(Translation.TvMovieEPGImportNotEnabled)
                Return
            End If

            'Clickfinder ProgramGuide import nicht aktiviert in TvMovie EGP IMport++ plugin
            If CPGsettings.ClickfinderDataAvailable = False Then
                ShowNotify(Translation.ClickfinderImportNotEnabled)
                Return
            End If
        Catch ex As Exception
            MyLog.Error("[Helper] [CheckConnectionState]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
        End Try


    End Sub

    Public Shared Sub ShowNotify(ByVal Message As String)
        Try
            Dim dlgContext As GUIDialogOK = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_OK, Integer)), GUIDialogOK)
            dlgContext.Reset()
            dlgContext.SetHeading(Translation.Warning)
            dlgContext.SetLine(1, Message)

            dlgContext.DoModal(GUIWindowManager.ActiveWindow)
            GUIWindowManager.CloseCurrentWindow()

            dlgContext.Dispose()
            dlgContext.AllocResources()
        Catch ex As Exception
            MyLog.Error("[Helper]: [ShowNotify]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Friend Shared Sub CheckSeriesLocalStatus(ByVal TvMovieProgram As TVMovieProgram)
        Try
            If CPGsettings.TvMovieImportTvSeriesInfos = True Then
                If String.IsNullOrEmpty(TvMovieProgram.ReferencedProgram.SeriesNum) = False And String.IsNullOrEmpty(TvMovieProgram.ReferencedProgram.EpisodeNum) = False Then

                    'prüfen ob in Records vorhanden
                    Dim _RecList As List(Of Recording)
                    Dim _SQLstring As String = "SELECT * FROM Recording " & _
                                                "WHERE title LIKE '" & MyTvSeries.Helper.allowedSigns(TvMovieProgram.ReferencedProgram.Title) & "' " & _
                                                "AND episodeName LIKE '" & MyTvSeries.Helper.allowedSigns(TvMovieProgram.ReferencedProgram.EpisodeName) & "' " & _
                                                "AND seriesNum = '" & TvMovieProgram.ReferencedProgram.SeriesNum & "' " & _
                                                "AND episodeNum = '" & TvMovieProgram.ReferencedProgram.EpisodeNum & "'"

                    _SQLstring = Helper.AppendSqlLimit(_SQLstring, 1)
                    Dim _SQLstate1 As SqlStatement = Broker.GetStatement(_SQLstring)
                    _RecList = ObjectFactory.GetCollection(GetType(Recording), _SQLstate1.Execute())

                    'Serien laden und nach update schauen, sofern in MP-TvSeries (neu serien aufnahme verurschat Ex)
                    Try
                        Dim _Series As MyTvSeries = MyTvSeries.Retrieve(TvMovieProgram.idSeries)
                        Dim _Episode As MyTvSeries.MyEpisode = MyTvSeries.MyEpisode.Retrieve(_Series.idSeries, TvMovieProgram.ReferencedProgram.SeriesNum, TvMovieProgram.ReferencedProgram.EpisodeNum)

                        If _RecList.Count > 0 Then
                            _Episode.ExistLocal = True
                        End If

                        'Falls local dann Datensätz updaten
                        If _Episode.ExistLocal = True Then

                            'Alle Episoden (program & tvmovieprogram) updaten
                            _SQLstring = _
                            "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                            "WHERE idSeries = " & _Episode.SeriesID & " " & _
                            "AND episodeName LIKE '" & MyTvSeries.Helper.allowedSigns(TvMovieProgram.ReferencedProgram.EpisodeName) & "'"

                            _SQLstring = Replace(_SQLstring, " * ", " TVMovieProgram.idProgram, TVMovieProgram.Action, TVMovieProgram.Actors, TVMovieProgram.BildDateiname, TVMovieProgram.Country, TVMovieProgram.Cover, TVMovieProgram.Describtion, TVMovieProgram.Dolby, TVMovieProgram.EpisodeImage, TVMovieProgram.Erotic, TVMovieProgram.FanArt, TVMovieProgram.Feelings, TVMovieProgram.FileName, TVMovieProgram.Fun, TVMovieProgram.HDTV, TVMovieProgram.idEpisode, TVMovieProgram.idMovingPictures, TVMovieProgram.idSeries, TVMovieProgram.idVideo, TVMovieProgram.KurzKritik, TVMovieProgram.local, TVMovieProgram.Regie, TVMovieProgram.Requirement, TVMovieProgram.SeriesPosterImage, TVMovieProgram.ShortDescribtion, TVMovieProgram.Tension, TVMovieProgram.TVMovieBewertung ")
                            Dim _SQLstate4 As SqlStatement = Broker.GetStatement(_SQLstring)
                            Dim _EpisodeRepeatsList As IList(Of TVMovieProgram) = ObjectFactory.GetCollection(GetType(TVMovieProgram), _SQLstate4.Execute())

                            If _EpisodeRepeatsList.Count > 0 Then
                                Dim _logNewEpisode As Boolean = False
                                For Each _TvMovieProgram In _EpisodeRepeatsList
                                    _logNewEpisode = Not IdentifySeries.UpdateProgramAndTvMovieProgram(_TvMovieProgram.ReferencedProgram, _Series, _Episode, _Episode.ExistLocal, True)
                                Next
                                MyLog.Info("[CheckSeriesLocalStatus]: S{0}E{1} - {2} updated (programList.count = {3}, newEpisode: {4})", _
                                          _Episode.SeriesNum, _Episode.EpisodeNum, TvMovieProgram.ReferencedProgram.EpisodeName, _EpisodeRepeatsList.Count, _logNewEpisode)
                            End If
                        End If
                    Catch ex As Exception
                    End Try
                End If
            End If

        Catch ex As Exception
            MyLog.Error("[Helper]: [CheckSeriesLocalStatus]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Friend Shared Function TvServerConnected() As Boolean
        'Prüfen ob TvServer online ist
        Try
            Dim _server As IList(Of Server) = Server.ListAll
            MyLog.Debug("TvServer found: {0}", _server(0).HostName)
            Return True
        Catch ex As Exception
            MyLog.Error("Server not found")
            Return False
        End Try
    End Function

    Friend Shared Sub LogSettings()
        Dim _tvbLayer As New TvBusinessLayer

        Dim sb As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(Setting))
        sb.AddConstraint([Operator].Like, "tag", "Clickfinder%")
        sb.AddOrderByField(True, "tag")
        Dim stmt As SqlStatement = sb.GetStatement(True)
        Dim _Result As IList(Of Setting) = ObjectFactory.GetCollection(GetType(Setting), stmt.Execute())

        MyLog.Debug("******* Settings ClickfinderProgramGuide *******")
        If _Result.Count > 0 Then
            For i = 0 To _Result.Count - 1
                MyLog.Debug("[ClickfinderProgramGuide]: {0} = {1}", _Result(i).Tag, _Result(i).Value)
            Next
        Else
            MyLog.Error("[ClickfinderProgramGuide]: Keine settings gefunden !!!")
        End If

        MyLog.Debug("")
        MyLog.Debug("******* Settings TvMovie EPG Import++ plugin (TvServer) *******")
        Dim sb2 As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(Setting))
        sb2.AddConstraint([Operator].Like, "tag", "TvMovie%")
        sb2.AddOrderByField(True, "tag")
        Dim stmt2 As SqlStatement = sb2.GetStatement(True)
        Dim _Result2 As IList(Of Setting) = ObjectFactory.GetCollection(GetType(Setting), stmt2.Execute())

        MyLog.Debug("[TvMovie++]: TvServer provider = {0}", Gentle.Framework.Broker.ProviderName)
        If _Result2.Count > 0 Then
            For i = 0 To _Result2.Count - 1
                MyLog.Debug("[TvMovie++]: {0} = {1}", _Result2(i).Tag, _Result2(i).Value)
            Next
        Else
            MyLog.Error("[TvMovie++]: Keine settings gefunden !!!")
        End If
    End Sub



    Friend Shared Sub Notify(ByVal message As String, Optional ByVal image As String = "")
        Try
            Dim dlgContext As GUIDialogNotify = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_NOTIFY, Integer)), GUIDialogNotify)
            dlgContext.Reset()

            dlgContext.SetHeading("Clickfinder ProgramGuide")
            If String.IsNullOrEmpty(image) Then
                dlgContext.SetImage(Config.GetFile(Config.Dir.Thumbs, "Clickfinder ProgramGuide\ClickfinderPG_logo.png"))
            Else
                dlgContext.SetImage(image)
            End If

            dlgContext.SetText(message)
            dlgContext.TimeOut = 5

            dlgContext.DoModal(GUIWindowManager.ActiveWindow)
            dlgContext.Dispose()
            dlgContext.AllocResources()
        Catch ex As Exception
            MyLog.Error("[Helper]: [Notify]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
        End Try
    End Sub
End Class
