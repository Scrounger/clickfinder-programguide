﻿Imports MediaPortal.GUI.Library
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


Public Class Helper
#Region "Members"
    Private Shared _layer As New TvBusinessLayer
    Private Shared _PlayedFile As TVMovieProgram
    Friend Shared _DbAbgleichRuning As Boolean = False

#End Region

#Region "Properties"
    Friend Shared ReadOnly Property ORDERBYstartTime() As String
        Get
            Return "ORDER BY startTime ASC, starRating DESC, title ASC"
        End Get
    End Property
    Friend Shared ReadOnly Property ORDERBYtvMovieBewertung() As String
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
#End Region

    Friend Enum SortMethode
        startTime
        TvMovieStar
        RatingStar
        Genre
        parentalRating
        Series
    End Enum
    Friend Shared Sub AddListControlItem(ByVal Listcontrol As GUIListControl, ByVal idProgram As Integer, ByVal ChannelName As String, ByVal titelLabel As String, Optional ByVal timeLabel As String = "", Optional ByVal infoLabel As String = "", Optional ByVal ImagePath As String = "", Optional ByVal MinRunTime As Integer = 0, Optional ByVal isRecording As String = "", Optional ByVal tmpInfo As String = "")
        Try

            Dim lItem As New GUIListItem

            lItem.Label = titelLabel
            lItem.Label2 = timeLabel
            lItem.Label3 = infoLabel
            lItem.ItemId = idProgram
            lItem.Path = ChannelName
            lItem.IconImage = ImagePath
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


    Friend Shared Sub ListControlClick(ByVal idProgram As Integer)
        Try
            Dim TvMovieProgram As TVMovieProgram = getTvMovieProgram(Program.Retrieve(idProgram))
            DetailGuiWindow.SetGuiProperties(TvMovieProgram)
            GUIWindowManager.ActivateWindow(1656544652)
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
            Dim _TvMovieprogram As TVMovieProgram = getTvMovieProgram(program)

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

    ''' <summary>
    ''' Holt / erstellt TvMovieProgram
    ''' </summary>
    ''' <returns>TvMovieProgram</returns>
    Friend Shared Function getTvMovieProgram(ByVal Program As Program) As TVMovieProgram
        Try
            Dim _TvMovieProgram As TVMovieProgram = TVMovieProgram.Retrieve(Program.IdProgram)
            Return _TvMovieProgram
        Catch ex As Exception
            Dim _ClickfinderDB As New ClickfinderDB(Program)
            Dim _TvMovieProgram As New TVMovieProgram(Program.IdProgram)
            If _ClickfinderDB.Count > 0 Then

                'nur Informationen die zwigend benötigt werden, anzeige in GuiItems, GuiCategories & GuiHighlights
                '+ zusätzlich Infos zum sortieren & suchen (z.B. TvMovieBewretung, Fun, Action, etc.)

                'BildDateiname aus Clickfinder DB holen, sofern vorhanden
                If CBool(_ClickfinderDB(0).KzBilddateiHeruntergeladen) = True And Not String.IsNullOrEmpty(_ClickfinderDB(0).Bilddateiname) Then
                    _TvMovieProgram.BildDateiname = _ClickfinderDB(0).Bilddateiname
                End If

                'TvMovie Bewertung aus Clickfinder DB holen, sofern vorhanden
                If Not _ClickfinderDB(0).Bewertung = 0 Then
                    _TvMovieProgram.TVMovieBewertung = _ClickfinderDB(0).Bewertung
                End If

                'KurzKritik aus Clickfinder DB holen, sofern vorhanden
                If Not String.IsNullOrEmpty(_ClickfinderDB(0).Kurzkritik) Then
                    _TvMovieProgram.KurzKritik = _ClickfinderDB(0).Kurzkritik
                End If
                _TvMovieProgram.needsUpdate = True
                _TvMovieProgram.Persist()
                Return _TvMovieProgram
            Else
                _TvMovieProgram.Persist()
                MyLog.[Warn]("[getTvMovieProgram]: Program {0} not found in ClickfinderDB (Title: {1}, Channel: {2}, startTime: {3}, starRating: {4})", _
                                        _TvMovieProgram.ReferencedProgram.IdProgram, _TvMovieProgram.ReferencedProgram.Title, _TvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName, _TvMovieProgram.ReferencedProgram.StartTime, _TvMovieProgram.ReferencedProgram.StarRating)
                Return _TvMovieProgram
            End If

        End Try
    End Function

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

            'mit Serien aktualisieren
            Dim lItemSeriesUpdate As New GUIListItem
            lItemSeriesUpdate.Label = Translation.DBRefresh
            dlgContext.Add(lItemSeriesUpdate)
            lItemSeriesUpdate.Dispose()

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

    Private Shared Sub startEnrichEPG()
        Try
            Notify(Translation.DBRefreshStarted)

            _DbAbgleichRuning = True
            GuiLayout.SetSettingLastUpdateProperty()

            Dim enrichEPG As New enrichEPG.EnrichEPG(Config.GetFile(Config.Dir.Database, ""), _
                                                     _layer.GetSetting("TvMovieImportTvSeriesInfos", "false").Value, _
                                                     _layer.GetSetting("TvMovieImportVideoDatabaseInfos", "false").Value, _
                                                     _layer.GetSetting("TvMovieImportMovingPicturesInfos", "false").Value, _
                                                     Date.Now, Global.enrichEPG.EnrichEPG.LogPath.Client, IO.Path.GetDirectoryName(_layer.GetSetting("ClickfinderEpisodenScanner", "").Value), , , "ClickfinderProgramGuide_enrichEPG.log", , , Config.GetFile(Config.Dir.Thumbs, ""))

            Dim _enrichEPGThread As New Thread(AddressOf enrichEPG.start)

            _enrichEPGThread.Start()
            _enrichEPGThread.Join()

            Translator.SetProperty("#SettingLastUpdate", Translation.RefreshOverlays)
            ClickfinderProgramGuide.StartGuiWindow.RefreshOverlays()

            Notify(Translation.DBRefreshFinished)
            _DbAbgleichRuning = False
            GuiLayout.SetSettingLastUpdateProperty()

        Catch ex As Exception
            MyLog.Error("[Helper]: [startEnrichEPG]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Shared Sub ShowSerieLinkContextMenu(ByVal program As Program)

        Try
            Dim dlgContext As GUIDialogSelect2Custom = CType(GUIWindowManager.GetWindow(CType(1656544655, Integer)), GUIDialogSelect2Custom)
            dlgContext.Reset()

            Dim _idSeriesContainer As Dictionary(Of Integer, Integer) = New Dictionary(Of Integer, Integer)

            If CBool(_layer.GetSetting("TvMovieImportTvSeriesInfos", "false").Value) = True Then
                'ContextMenu Layout
                dlgContext.SetHeading(program.Title)

                'Alle Serien aus DB laden
                Dim _TvSeriesDB As New TVSeriesDB
                _TvSeriesDB.LoadAllSeries()

                'Alle Serien + idSeries in CB schreiben
                For i As Integer = 0 To _TvSeriesDB.CountSeries - 1
                    Dim lItemSerie As New GUIListItem
                    lItemSerie.Label = _TvSeriesDB(i).SeriesName

                    Try
                        'Sofern verlinkung vorhanden
                        Dim SeriesMapping As TvMovieSeriesMapping = TvMovieSeriesMapping.Retrieve(_TvSeriesDB(i).SeriesID)
                        lItemSerie.Label3 = Translation.LinkTo & " " & SeriesMapping.EpgTitle
                    Catch ex As Exception
                        'sonst abfangen
                    End Try

                    lItemSerie.IconImage = Config.GetFile(Config.Dir.Thumbs, "MPTVSeriesBanners\") & _TvSeriesDB(i).SeriesPosterImage

                    _idSeriesContainer.Add(i, _TvSeriesDB(i).SeriesID)

                    dlgContext.Add(lItemSerie)
                    lItemSerie.Dispose()

                Next
            End If

            dlgContext.DoModal(GUIWindowManager.ActiveWindow)

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
            lItemAdd.Label = Translation.SeriesMappingAppend
            dlgContext.Add(lItemAdd)
            lItemAdd.Dispose()

            'Verlinkung überschreiben
            Dim lItemOverwrite As New GUIListItem
            lItemOverwrite.Label = Translation.SeriesMappingOverwrite
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
            If IO.File.Exists(ClickfinderDB.DatabasePath) = False Then
                ShowNotify(Translation.ClickfinderDBOffline)
                Return
            End If

            'TvMovie EGP IMport++ plugin nicht aktiviert / installiert
            If CBool(_layer.GetSetting("TvMovieEnabled", "false").Value) = False Then
                ShowNotify(Translation.TvMovieEPGImportNotEnabled)
                Return
            End If

            'Clickfinder ProgramGuide import nicht aktiviert in TvMovie EGP IMport++ plugin
            If CBool(_layer.GetSetting("ClickfinderEnabled", "true").Value) = False Then
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
            If CBool(_layer.GetSetting("TvMovieImportTvSeriesInfos", "false").Value) = True Then
                If TvMovieProgram.idSeries > 0 And TvMovieProgram.local = False Then
                    If String.IsNullOrEmpty(TvMovieProgram.ReferencedProgram.SeriesNum) = False And String.IsNullOrEmpty(TvMovieProgram.ReferencedProgram.EpisodeNum) = False Then
                        Try

                            'prüfen ob inzwischen aufgenommen -> Tv Server table recording
                            Dim sbRecording As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(Recording))
                            sbRecording.AddConstraint([Operator].Equals, "title", TvMovieProgram.ReferencedProgram.Title)
                            sbRecording.AddConstraint([Operator].Equals, "episodeName", TvMovieProgram.ReferencedProgram.EpisodeName)
                            sbRecording.AddConstraint([Operator].Equals, "seriesNum", TvMovieProgram.ReferencedProgram.SeriesNum)
                            sbRecording.AddConstraint([Operator].Equals, "episodeNum", TvMovieProgram.ReferencedProgram.EpisodeNum)
                            Dim stmtRecording As SqlStatement = sbRecording.GetStatement(True)
                            Dim _Recording_found As IList(Of Recording) = ObjectFactory.GetCollection(GetType(Recording), stmtRecording.Execute())

                            If _Recording_found.Count > 0 Then
                                'wenn gefunden -> TvMovieProgram update
                                TvMovieProgram.local = True
                                TvMovieProgram.ReferencedProgram.Description = Replace(TvMovieProgram.ReferencedProgram.Description, "Neue Folge: " & TvMovieProgram.ReferencedProgram.EpisodeName, "Folge: " & TvMovieProgram.ReferencedProgram.EpisodeName)
                                TvMovieProgram.ReferencedProgram.Persist()
                                TvMovieProgram.Persist()
                                MyLog.Info("[Helper]: [CheckSeriesLocalStatus]: Episode found in records: {0} - S{1}E{2}", TvMovieProgram.ReferencedProgram.Title, TvMovieProgram.ReferencedProgram.SeriesNum, TvMovieProgram.ReferencedProgram.EpisodeNum)
                            Else

                                Dim _logSeriesIsLinked As String = String.Empty
                                Dim _SeriesIsLinked As Boolean = False
                                Dim _SeriesName As String = String.Empty

                                _SeriesName = TvMovieProgram.ReferencedProgram.Title

                                ''Prüfen ob EPG-SerienName in TvSeries DB gefunden wird, wenn nicht, schauen ob Verlinkung existiert
                                'Dim _checkSeriesName As New TVSeriesDB
                                'Dim _checkSeriesNameCounter As Boolean = _checkSeriesName.SeriesFound(TvMovieProgram.ReferencedProgram.Title)
                                '_checkSeriesName.Dispose()

                                'If _checkSeriesNameCounter = True Then
                                '    _SeriesName = TvMovieProgram.ReferencedProgram.Title
                                'Else
                                '    'Prüfen ob Serie verlinkt ist
                                '    Dim _SqlString As String = String.Empty
                                '    Dim _SeriesMappingResult As New ArrayList

                                '    _SqlString = "Select * from TvMovieSeriesMapping " & _
                                '                        "WHERE EpgTitle LIKE '%" & TVSeriesDB.allowedSigns(TvMovieProgram.ReferencedProgram.Title) & "%'"

                                '    _SeriesMappingResult.AddRange(Broker.Execute(_SqlString).TransposeToFieldList("idSeries", False))

                                '    'Serie ist verlinkt -> org. SerienName anstatt EPG Name verwenden
                                '    If _SeriesMappingResult.Count > 0 Then

                                '        Dim _TvSeriesName As New TVSeriesDB

                                '        _TvSeriesName.LoadSeriesName(CInt(_SeriesMappingResult.Item(0)))
                                '        _SeriesName = _TvSeriesName(0).SeriesName

                                '        _logSeriesIsLinked = "[Helper]: [CheckSeriesLocalStatus]: manuel mapping found: " & _TvSeriesName(0).SeriesName & " -> " & TvMovieSeriesMapping.Retrieve(_SeriesMappingResult.Item(0)).EpgTitle

                                '        _TvSeriesName.Dispose()

                                '    Else
                                '        'Nicht verlinkt -> EPG Name verwenden
                                '        _SeriesName = TvMovieProgram.ReferencedProgram.Title
                                '    End If
                                'End If

                                Dim _TvSeriesDB As New TVSeriesDB
                                _TvSeriesDB.LoadEpisode(_SeriesName, CInt(TvMovieProgram.ReferencedProgram.SeriesNum), CInt(TvMovieProgram.ReferencedProgram.EpisodeNum))

                                If _TvSeriesDB.EpisodeExistLocal = True Then

                                    TvMovieProgram.local = True
                                    TvMovieProgram.FileName = _TvSeriesDB.EpisodeFilename
                                    TvMovieProgram.ReferencedProgram.Description = Replace(TvMovieProgram.ReferencedProgram.Description, "Neue Folge: " & TvMovieProgram.ReferencedProgram.EpisodeName, "Folge: " & TvMovieProgram.ReferencedProgram.EpisodeName)
                                    TvMovieProgram.ReferencedProgram.Persist()
                                    TvMovieProgram.Persist()

                                    If _SeriesIsLinked = True Then
                                        MyLog.[Info](_logSeriesIsLinked)
                                    End If

                                    MyLog.Info("[Helper]: [CheckSeriesLocalStatus]: Episode found in TvSeries database: {0} - S{1}E{2}", TvMovieProgram.ReferencedProgram.Title, TvMovieProgram.ReferencedProgram.SeriesNum, TvMovieProgram.ReferencedProgram.EpisodeNum)
                                End If
                                _TvSeriesDB.Dispose()

                            End If
                        Catch ex As Exception
                            MyLog.Error("[Helper]: [CheckSeriesLocalStatus]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
                        End Try
                    End If
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
        MyLog.Debug("")
        MyLog.Debug("******* Settings *******")
        MyLog.Debug("[TvMovie++] TvServer provider: {0}", Gentle.Framework.Broker.ProviderName)
        MyLog.Debug("[TvMovie++] MediaPortal database path: {0}", _tvbLayer.GetSetting("TvMovieMPDatabase").Value)
        MyLog.Debug("[TvMovie++] TvMovie database path: {0}", _tvbLayer.GetSetting("TvMoviedatabasepath").Value)
        MyLog.Debug("[TvMovie++] run App after: {0}", _tvbLayer.GetSetting("TvMovieRunAppAfter").Value)
        MyLog.Debug("[TvMovie++] is EpisodenScanner: {0}", _tvbLayer.GetSetting("TvMovieIsEpisodenScanner").Value)
        MyLog.Debug("[TvMovie++] Import Mp-TvSeries Infos: {0}", _tvbLayer.GetSetting("TvMovieImportTvSeriesInfos").Value)
        MyLog.Debug("[TvMovie++] Import MovingPictures Infos: {0}", _tvbLayer.GetSetting("TvMovieImportMovingPicturesInfos").Value)
        MyLog.Debug("[TvMovie++] Import VideoDatabase Infos: {0}", _tvbLayer.GetSetting("TvMovieImportVideoDatabaseInfos").Value)
        MyLog.Debug("[TvMovie++] Import Clickfinder ProgramGuide Infos: {0}", _tvbLayer.GetSetting("ClickfinderDataAvailable").Value)

    End Sub

    Friend Shared Sub UpdateProgramData(ByVal TvMovieProgram As TVMovieProgram)

        Try

            MyLog.Debug("[Helper]: [UpdateProgramData]: program needs update")

            Dim _ClickfinderDB As New ClickfinderDB(TvMovieProgram.ReferencedProgram, True)

            'Wenn HD Sender -> Dolby = true, Hd = true
            If InStr(TvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName, " HD") > 0 Then
                TvMovieProgram.Dolby = True
                TvMovieProgram.HDTV = True
            Else
                'Daten aus ClickfinderDB 
                If _ClickfinderDB.Count > 0 Then
                    TvMovieProgram.Dolby = _ClickfinderDB(0).Dolby
                    TvMovieProgram.HDTV = _ClickfinderDB(0).KzHDTV
                End If
            End If

            'Jahr
            If Not String.IsNullOrEmpty(_ClickfinderDB(0).Herstellungsjahr) Then
                TvMovieProgram.Year = CDate("01.01." & _ClickfinderDB(0).Herstellungsjahr)
            End If

            'Country
            If Not String.IsNullOrEmpty(_ClickfinderDB(0).Herstellungsland) Then
                TvMovieProgram.Country = _ClickfinderDB(0).Herstellungsland
            End If

            'Regie
            If Not String.IsNullOrEmpty(_ClickfinderDB(0).Regie) Then
                TvMovieProgram.Regie = _ClickfinderDB(0).Regie
            End If

            'Describtion
            If TvMovieProgram.idSeries > 0 And CBool(_layer.GetSetting("TvMovieImportTvSeriesInfos", "false").Value) = True And CBool(_layer.GetSetting("ClickfinderDetailUseSeriesDescribtion", "false").Value) = True Then
                'Wenn Serie erkannt dann -> Episoden Beschreibung aus TvSeriesDB laden (sofern aktiviert & vorhanden)
                Dim _Series As New TVSeriesDB
                _Series.LoadEpisodebyEpsiodeID(TvMovieProgram.idSeries, TvMovieProgram.idEpisode)
                If Not String.IsNullOrEmpty(_Series.EpisodeSummary) Then
                    TvMovieProgram.Describtion = _Series.EpisodeSummary & vbNewLine & "(Beschreibung von MP-TvSeries)"
                Else
                    If Not String.IsNullOrEmpty(_ClickfinderDB(0).Beschreibung) Then
                        TvMovieProgram.Describtion = Replace(_ClickfinderDB(0).Beschreibung, "<br>", vbNewLine) & vbNewLine & "(Beschreibung von TvMovie)"
                    ElseIf Not String.IsNullOrEmpty(TvMovieProgram.ReferencedProgram.Description) Then
                        TvMovieProgram.Describtion = TvMovieProgram.ReferencedProgram.Description & vbNewLine & "(Beschreibung aus EPG)"
                    End If
                End If

            Else
                If Not String.IsNullOrEmpty(_ClickfinderDB(0).Beschreibung) Then
                    TvMovieProgram.Describtion = Replace(_ClickfinderDB(0).Beschreibung, "<br>", vbNewLine) & vbNewLine & "(Beschreibung von TvMovie)"
                ElseIf Not String.IsNullOrEmpty(TvMovieProgram.ReferencedProgram.Description) Then
                    TvMovieProgram.Describtion = TvMovieProgram.ReferencedProgram.Description & vbNewLine & "(Beschreibung aus EPG)"
                End If
            End If

            'Short Describtion
            If Not String.IsNullOrEmpty(_ClickfinderDB(0).KurzBeschreibung) Then
                TvMovieProgram.ShortDescribtion = _ClickfinderDB(0).KurzBeschreibung
            End If


            'Bewertungen String aus Clickfinder DB holen, zerlegen, einzel Bewertungen extrahieren
            If Not String.IsNullOrEmpty(_ClickfinderDB(0).Bewertungen) Then
                ' We want to split this input string
                Dim s As String = _ClickfinderDB(0).Bewertungen

                ' Split string based on spaces
                Dim words As String() = s.Split(New Char() {";"c})

                ' Use For Each loop over words and display them
                Dim word As String
                For Each word In words
                    'MsgBox(Left(word, InStr(word, "=") - 1))

                    'MsgBox(CInt(Right(word, word.Length - InStr(word, "="))))

                    Select Case Left(word, InStr(word, "=") - 1)
                        Case Is = "Spaß"
                            TvMovieProgram.Fun = CInt(Right(word, word.Length - InStr(word, "=")))
                        Case Is = "Action"
                            TvMovieProgram.Action = CInt(Right(word, word.Length - InStr(word, "=")))
                        Case Is = "Erotik"
                            TvMovieProgram.Erotic = CInt(Right(word, word.Length - InStr(word, "=")))
                        Case Is = "Spannung"
                            TvMovieProgram.Tension = CInt(Right(word, word.Length - InStr(word, "=")))
                        Case Is = "Anspruch"
                            TvMovieProgram.Requirement = CInt(Right(word, word.Length - InStr(word, "=")))
                        Case Is = "Gefühl"
                            TvMovieProgram.Feelings = CInt(Right(word, word.Length - InStr(word, "=")))
                    End Select
                Next
            End If

            'Actors aus Clickfinder DB holen, sofern vorhanden
            If Not String.IsNullOrEmpty(_ClickfinderDB(0).Darsteller) Then
                TvMovieProgram.Actors = _ClickfinderDB(0).Darsteller
            End If

            TvMovieProgram.needsUpdate = False
            TvMovieProgram.Persist()


        Catch ex As Exception
            MyLog.Error("[Helper]: [UpdateProgramData]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
        End Try

    End Sub

    Private Shared Sub Notify(ByVal message As String)
        Try
            Dim dlgContext As GUIDialogNotify = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_NOTIFY, Integer)), GUIDialogNotify)
            dlgContext.Reset()

            dlgContext.SetHeading("Clickfinder ProgramGuide")
            dlgContext.SetImage(Config.GetFile(Config.Dir.Thumbs, "Clickfinder ProgramGuide\ClickfinderPG_logo.png"))
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
