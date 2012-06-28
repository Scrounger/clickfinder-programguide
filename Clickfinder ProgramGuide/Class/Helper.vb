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

Public Class Helper
#Region "Members"
    Private Shared _layer As New TvBusinessLayer
    Private Shared _PlayedFile As TVMovieProgram
#End Region

    Friend Enum SortMethode
        startTime
        TvMovieStar
        RatingStar
        Genre
        parentalRating
        Series
    End Enum
    Friend Shared Sub AddListControlItem(ByVal Listcontrol As GUIListControl, ByVal idProgram As Integer, ByVal ChannelName As String, ByVal titelLabel As String, Optional ByVal timeLabel As String = "", Optional ByVal infoLabel As String = "", Optional ByVal ImagePath As String = "", Optional ByVal MinRunTime As Integer = 0, Optional ByVal isRecording As String = "")

        Dim lItem As New GUIListItem

        lItem.Label = titelLabel
        lItem.Label2 = timeLabel
        lItem.Label3 = infoLabel
        lItem.ItemId = idProgram
        lItem.Path = ChannelName
        lItem.IconImage = ImagePath
        lItem.Duration = MinRunTime
        lItem.PinImage = isRecording

        GUIControl.AddListItemControl(GUIWindowManager.ActiveWindow, Listcontrol.GetID, lItem)

    End Sub

    Friend Shared Function MySqlDate(ByVal Datum As Date) As String
        Return "'" & Datum.Year & "-" & Format(Datum.Month, "00") & "-" & Format(Datum.Day, "00") & " " & Format(Datum.Hour, "00") & ":" & Format(Datum.Minute, "00") & ":00'"
    End Function

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

    Friend Shared Sub ListControlClick(ByVal idProgram As Integer)
        Try
            Dim TvMovieProgram As TVMovieProgram = getTvMovieProgram(Program.Retrieve(idProgram))
            DetailGuiWindow.SetGuiProperties(TvMovieProgram)
            GUIWindowManager.ActivateWindow(1656544652)
        Catch ex As Exception
            MyLog.Error("[ListControlClick]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
        End Try
    End Sub

    Friend Shared Sub LoadTVProgramInfo(ByVal Program As Program)

        Try
            TvPlugin.TVProgramInfo.CurrentProgram = Program
            GUIWindowManager.ActivateWindow(CInt(Window.WINDOW_TV_PROGRAM_INFO))
        Catch ex As Exception
            MyLog.Error("[LoadTVProgramInfo]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
        End Try

    End Sub
    'MP Notify für Sendung setzen
    Friend Shared Sub SetNotify(ByVal Program As Program)

        Dim Erinnerung As Program = Program.Retrieve(Program.IdProgram)
        Erinnerung.Notify = True
        Erinnerung.Persist()
        TvNotifyManager.OnNotifiesChanged()

        MPDialogOK("Erinnerung:", Erinnerung.Title, Erinnerung.StartTime & " - " & Erinnerung.EndTime, Erinnerung.ReferencedChannel.DisplayName)

    End Sub
    'MP Tv Kanal einschalten
    Friend Shared Sub StartTv(ByVal program As Program)
        Try
            Dim _TvMovieprogram As TVMovieProgram = getTvMovieProgram(program)

            If Not String.IsNullOrEmpty(_TvMovieprogram.FileName) Then
                _PlayedFile = _TvMovieprogram

                'GUIPropertyManager.SetProperty("#Play.Current.Title", _TvMovieprogram.ReferencedProgram.Title)


                'Dim _playlistPlayer As New PlayListPlayer
                '_playlistPlayer.Reset()
                '_playlistPlayer.CurrentPlaylistType = PlayListType.PLAYLIST_VIDEO_TEMP

                'Dim playlist As PlayList = _playlistPlayer.GetPlaylist(PlayListType.PLAYLIST_VIDEO_TEMP)
                'playlist.Clear()

                'Dim itemNew As New PlayListItem()
                'itemNew.FileName = _TvMovieprogram.FileName
                'itemNew.Description = _TvMovieprogram.ReferencedProgram.Description
                'itemNew.Type = PlayListItem.PlayListItemType.Video
                'playlist.Add(itemNew)

                '' play movie...
                '_playlistPlayer.PlayNext(True)


                ' Update OSD (delayed)
                'Dim newThread As New Thread(AddressOf UpdatePlaybackInfo)
                'newThread.Start()

                g_Player.Play(_TvMovieprogram.FileName)
                g_Player.ShowFullScreenWindowVideoDefault()

            Else
                Dim changeChannel As Channel = DirectCast(program.ReferencedChannel, Channel)
                MediaPortal.GUI.Library.GUIWindowManager.ActivateWindow(CInt(MediaPortal.GUI.Library.GUIWindow.Window.WINDOW_TVFULLSCREEN))
                TVHome.ViewChannelAndCheck(changeChannel)
            End If

        Catch ex As Exception
            MyLog.Error("Clickfinder ProgramGuide: [Button_ViewChannel] " & ex.Message)
            ShowNotify(ex.Message)
        End Try
    End Sub

    'MediaPortal Dialoge
    Friend Shared Sub MPDialogOK(ByVal Heading As String, ByVal StringLine1 As String, Optional ByVal StringLine2 As String = "", Optional ByVal StringLine3 As String = "")
        Dim dlg As GUIDialogOK = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_OK, Integer)), GUIDialogOK)
        dlg.SetHeading(Heading)
        dlg.SetLine(1, StringLine1)
        dlg.SetLine(2, StringLine2)
        dlg.SetLine(3, StringLine3)
        dlg.DoModal(GUIWindowManager.ActiveWindow)
    End Sub

    ''' <summary>
    ''' Prüft ob Program auf gleich gemappten (TvMovieMapping) HDSender existiert
    ''' </summary>
    ''' <returns>0 - kein Treffer, idprogram sofern treffer</returns>

    '''' <summary>
    '''' Prüft ob Program auf gleich gemappten (TvMovieMapping) HDSender existiert
    '''' </summary>
    '''' <returns>0 - kein Treffer, idprogram sofern treffer</returns>
    'Friend Shared Function GetHDChannel2(ByVal idprogram As Integer) As Program
    '    Dim _program As Program = Program.Retrieve(idprogram)

    '    'Zunächst prüfen ob HDchannel
    '    If InStr(_program.ReferencedChannel.DisplayName, " HD") = 0 Then
    '        Dim _stationName As New Gentle.Framework.Key(False, "idChannel", _program.ReferencedChannel.IdChannel)
    '        Try
    '            'Alle Sender mit gleichem TvMovieMapping laden
    '            Dim sb As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(TvMovieMapping))
    '            sb.AddConstraint([Operator].Equals, "stationName", TvMovieMapping.Retrieve(_stationName).StationName)
    '            Dim stmt As SqlStatement = sb.GetStatement(True)
    '            Dim _Result As IList(Of TvMovieMapping) = ObjectFactory.GetCollection(GetType(TvMovieMapping), stmt.Execute())

    '            'Nach Sender mit Endung " HD" suchen
    '            If _Result.Count > 1 Then
    '                For d = 0 To _Result.Count - 1
    '                    If InStr(_Result(d).ReferencedChannel.DisplayName, " HD") > 0 Then
    '                        Try
    '                            Dim _HDprogram As Program = Program.RetrieveByTitleTimesAndChannel(_program.Title, _program.StartTime, _program.EndTime, _Result(d).IdChannel)
    '                            Return _HDprogram
    '                            Exit Function
    '                        Catch ex As Exception
    '                            MyLog.Debug("TVMovie: [GetHDChannel]: program not found on mapped _HDchannel ({0}, {1}, {2}, {3} -> {4})", _program.Title, _program.StartTime, _program.EndTime, _program.ReferencedChannel.DisplayName, _Result(d).IdChannel)
    '                            Return _program
    '                        End Try
    '                    End If
    '                Next
    '            End If

    '            Return _program

    '        Catch ex As Exception
    '            MyLog.Debug("TVMovie: [GetHDChannel]: {0} not mapped in TvMovieMapping", _program.ReferencedChannel.DisplayName)
    '            Return _program
    '        End Try
    '    Else
    '        Return _program
    '    End If
    'End Function

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


                ''Bewertungen String aus Clickfinder DB holen, zerlegen, einzel Bewertungen extrahieren
                'If Not String.IsNullOrEmpty(_ClickfinderDB(0).Bewertungen) Then
                '    ' We want to split this input string
                '    Dim s As String = _ClickfinderDB(0).Bewertungen

                '    ' Split string based on spaces
                '    Dim words As String() = s.Split(New Char() {";"c})

                '    ' Use For Each loop over words and display them
                '    Dim word As String
                '    For Each word In words

                '        'MsgBox(Left(word, InStr(word, "=") - 1))

                '        'MsgBox(CInt(Right(word, word.Length - InStr(word, "="))))

                '        Select Case Left(word, InStr(word, "=") - 1)
                '            Case Is = "Spaß"
                '                _TvMovieProgram.Fun = CInt(Right(word, word.Length - InStr(word, "=")))
                '            Case Is = "Action"
                '                _TvMovieProgram.Action = CInt(Right(word, word.Length - InStr(word, "=")))
                '            Case Is = "Erotik"
                '                _TvMovieProgram.Erotic = CInt(Right(word, word.Length - InStr(word, "=")))
                '            Case Is = "Spannung"
                '                _TvMovieProgram.Tension = CInt(Right(word, word.Length - InStr(word, "=")))
                '            Case Is = "Anspruch"
                '                _TvMovieProgram.Requirement = CInt(Right(word, word.Length - InStr(word, "=")))
                '            Case Is = "Gefühl"
                '                _TvMovieProgram.Feelings = CInt(Right(word, word.Length - InStr(word, "=")))
                '        End Select

                '    Next
                'End If

                ''Actors aus Clickfinder DB holen, sofern vorhanden
                'If Not String.IsNullOrEmpty(_ClickfinderDB(0).Darsteller) Then
                '    _TvMovieProgram.Actors = _ClickfinderDB(0).Darsteller
                'End If

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

    Friend Shared Sub ShowContextMenu(ByVal idProgram As Integer)
        Dim dlgContext As GUIDialogMenu = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_MENU, Integer)), GUIDialogMenu)
        dlgContext.Reset()

        Dim _Program As Program = Program.Retrieve(idProgram)
        'ContextMenu Layout
        dlgContext.SetHeading(_Program.Title)
        dlgContext.ShowQuickNumbers = False

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
        lItemOn.Dispose()

        dlgContext.DoModal(GUIWindowManager.ActiveWindow)

        Select Case dlgContext.SelectedLabel
            Case Is = 0
                StartTv(_Program)
            Case Is = 1
                LoadTVProgramInfo(_Program)
            Case Is = 2
                SetNotify(_Program)
        End Select

    End Sub

    Friend Shared Sub ShowActionMenu(ByVal Program As Program)
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
        lItemOn.Dispose()

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
                If PluginManager.IsPlugInEnabled("OnlineVideos") Then
                    GUIWindowManager.ActivateWindow(4755, "site:IMDb Movie Trailers|search:" & Program.Title & "|return:Locked")
                Else
                    ShowNotify("OnlineVideos plugin nicht verfügbar!")
                End If
            Case Is = 4
                ShowSerieLinkContextMenu(Program)
            Case Else
                MyLog.Debug("[ShowActionMenu]: exit")
                MyLog.Debug("")
        End Select
    End Sub

    Private Shared Sub ShowSerieLinkContextMenu(ByVal program As Program)
        Dim dlgContext As GUIDialogSelect2Custom = CType(GUIWindowManager.GetWindow(CType(1656544655, Integer)), GUIDialogSelect2Custom)
        dlgContext.Reset()
        Try

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
                _SeriesMapping.EpgTitle = program.Title
                _SeriesMapping.Persist()
            Catch ex As Exception
                Dim _SeriesMapping As New TvMovieSeriesMapping(_idSeriesContainer.Item(dlgContext.SelectedLabel))
                _SeriesMapping.EpgTitle = program.Title
                _SeriesMapping.Persist()
            End Try

        Catch ex As Exception
            MyLog.Error("[HighlightsGUIWindow] [ShowSeriesContextMenu]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
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

        If Datum = Today Then
            _Result = Translation.Today
        End If

        If Datum = Today.AddDays(1) Then
            _Result = Translation.Tomorrow
        End If

        If Datum = Today.AddDays(-1) Then
            _Result = Translation.Yesterday
        End If

        Return _Result
    End Function

    Friend Shared Sub CheckConnectionState()

        'TvServer nicht verbunden / online
        If TvPlugin.TVHome.Connected = False Then
            ShowNotify(Translation.TvServerOffline)
            Exit Sub
        End If

        'Clickfinder DB nicht gefunden
        If IO.File.Exists(ClickfinderDB.DatabasePath) = False Then
            ShowNotify(Translation.ClickfinderDBOffline)
        End If

        If CBool(_layer.GetSetting("TvMovieEnabled", "false").Value) = False Then
            ShowNotify(Translation.TvMovieEPGImportNotEnabled)
        End If

        If CBool(_layer.GetSetting("ClickfinderEnabled", "true").Value) = False Then
            ShowNotify(Translation.ClickfinderImportNotEnabled)
        End If

    End Sub

    Private Shared Sub ShowNotify(ByVal Message As String)
        Dim dlgContext As GUIDialogOK = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_OK, Integer)), GUIDialogOK)
        dlgContext.Reset()
        dlgContext.SetHeading(Translation.Warning)
        dlgContext.SetLine(1, Message)

        dlgContext.DoModal(GUIWindowManager.ActiveWindow)
        GUIWindowManager.CloseCurrentWindow()

    End Sub

    Friend Shared Sub CheckSeriesLocalStatus(ByVal TvMovieProgram As TVMovieProgram)

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
                            MyLog.Info("[CheckSeriesLocalStatus]: Episode found in records: {0} - S{1}E{2}", TvMovieProgram.ReferencedProgram.Title, TvMovieProgram.ReferencedProgram.SeriesNum, TvMovieProgram.ReferencedProgram.EpisodeNum)
                        Else

                            Dim _logSeriesIsLinked As String = String.Empty
                            Dim _SeriesIsLinked As Boolean = False

                            'Prüfen ob die Serie evtl. verlinkt ist.
                            Dim sbSeries As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(TvMovieSeriesMapping))
                            sbSeries.AddConstraint([Operator].Equals, "EpgTitle", TvMovieProgram.ReferencedProgram.Title)
                            Dim stmtSeries As SqlStatement = sbSeries.GetStatement(True)
                            Dim _TvMovieSeriesMapping As IList(Of TvMovieSeriesMapping) = ObjectFactory.GetCollection(GetType(TvMovieSeriesMapping), stmtSeries.Execute())

                            Dim _SeriesName As String = String.Empty

                            'Serie ist verlinkt -> org. SerienName anstatt EPG Name verwenden
                            If _TvMovieSeriesMapping.Count > 0 Then

                                Dim _TvSeriesName As New TVSeriesDB

                                _TvSeriesName.LoadSeriesName(_TvMovieSeriesMapping(0).idSeries)
                                _SeriesName = _TvSeriesName(0).SeriesName

                                _SeriesIsLinked = True
                                _logSeriesIsLinked = "TVMovie: [CheckSeriesLocalStatus]: manuel mapping found: " & _TvSeriesName(0).SeriesName & " -> " & _TvMovieSeriesMapping(0).EpgTitle

                                _TvSeriesName.Dispose()

                            Else
                                'Nicht verlinkt -> EPG Name verwenden
                                _SeriesName = TvMovieProgram.ReferencedProgram.Title
                            End If

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
                                MyLog.Info("[CheckSeriesLocalStatus]: Episode found in TvSeries database: {0} - S{1}E{2}", TvMovieProgram.ReferencedProgram.Title, TvMovieProgram.ReferencedProgram.SeriesNum, TvMovieProgram.ReferencedProgram.EpisodeNum)
                            End If
                            _TvSeriesDB.Dispose()

                        End If
                    Catch ex As Exception
                        MyLog.Error("[CheckSeriesLocalStatus]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
                    End Try
                End If
            End If
        End If
    End Sub

    'Friend Shared Sub ManualWebBrowser()
    '    Dim webBrowserWindowID As Integer = InfoServiceUtils.GetWebBrowserWindowId(FeedService.Feeds(FeedService.ActiveFeedIndex).Items(_feedListcontrol.SelectedListItemIndex).Url, zoomlevel)
    'End Sub


    ' Updates the movie metadata on the playback screen (for when the user clicks info). 
    ' The delay is necessary because Player tries to use metadata from the MyVideos database.
    ' We want to update this after that happens so the correct info is there.
    Private Shared Sub UpdatePlaybackInfo()
        Thread.Sleep(5000)

        GUIPropertyManager.SetProperty("#Play.Current.Title", "Test")

        'GUIPropertyManager.SetProperty("#Play.Current.Plot", CurrentMovie.Summary)
        GUIPropertyManager.SetProperty("#Play.Current.Thumb", _PlayedFile.Cover)
    

    End Sub


End Class
