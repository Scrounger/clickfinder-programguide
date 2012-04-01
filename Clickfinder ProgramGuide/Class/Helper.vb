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

Public Class Helper

    Friend Enum SortMethode
        startTime
        TvMovieStar
        RatingStar
        Genre
        parentalRating
    End Enum
    Friend Shared Sub AddListControlItem(ByVal WindowId As Integer, ByVal Listcontrol As GUIListControl, ByVal idProgram As Integer, ByVal ChannelName As String, ByVal titelLabel As String, Optional ByVal timeLabel As String = "", Optional ByVal infoLabel As String = "", Optional ByVal ImagePath As String = "", Optional ByVal MinRunTime As Integer = 0)

        Dim lItem As New GUIListItem

        'Dim test As New GUIImage(idProgram)
        'test.FileName = ImagePath
        'test.Centered = True
        'test.KeepAspectRatio = True
        'test.Size = New MediaPortal.Drawing.Size(Listcontrol.ImageWidth, Listcontrol.ImageHeight)
        'test.SetPosition(Listcontrol.IconOffsetX, Listcontrol.IconOffsetY)
        'test.Render(1)
        'test.AllocResources()

        lItem.Label = titelLabel
        lItem.Label2 = timeLabel
        lItem.Label3 = infoLabel
        lItem.ItemId = idProgram
        lItem.Path = ChannelName
        lItem.IconImage = ImagePath
        lItem.Duration = MinRunTime

        'If Not String.IsNullOrEmpty(ImagePath) Then
        '    Try

        'GUITextureManager.LoadFromMemory(System.Drawing.Image.FromFile(ImagePath), "bildchen", 1, 20, 20)



        '    Catch ex As Exception
        '        MsgBox("Hallo" & vbNewLine & ex.Message)
        '    End Try
        'End If


        GUIControl.AddListItemControl(WindowId, Listcontrol.GetID, lItem)

        'MyLog.Debug("[AddListControlItem]: ImagePath: " & ImagePath)

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
            Return "ORDER BY starRating DESC, startTime ASC, title ASC, TVMovieBewertung DESC"
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
        DetailGuiWindow.Details_idProgram = idProgram
        GUIWindowManager.ActivateWindow(1656544652)
    End Sub

    Friend Shared Sub LoadTVProgramInfo(ByVal Program As Program)

        Try
            TvPlugin.TVProgramInfo.CurrentProgram = Program
            GUIWindowManager.ActivateWindow(CInt(Window.WINDOW_TV_PROGRAM_INFO))
        Catch ex As Exception
            MyLog.[Error]("[LoadTVProgramInfo]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
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
    Friend Shared Sub StartTv(ByVal Channel As Channel)
        Try

            Dim changeChannel As Channel = DirectCast(Channel, Channel)
            MediaPortal.GUI.Library.GUIWindowManager.ActivateWindow(CInt(MediaPortal.GUI.Library.GUIWindow.Window.WINDOW_TVFULLSCREEN))
            TVHome.ViewChannelAndCheck(changeChannel)

        Catch ex As Exception
            Log.Error("Clickfinder ProgramGuide: [Button_ViewChannel] " & ex.Message)
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
    Friend Shared Function GetHDChannel(ByVal program As Program) As Integer
        'Zunächst prüfen ob HDchannel
        If InStr(program.ReferencedChannel.DisplayName, " HD") = 0 Then
            Dim _stationName As New Gentle.Framework.Key(False, "idChannel", program.ReferencedChannel.IdChannel)
            Try
                'Alle Sender mit gleichem TvMovieMapping laden
                Dim sb As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(TvMovieMapping))
                sb.AddConstraint([Operator].Equals, "stationName", TvMovieMapping.Retrieve(_stationName).StationName)
                Dim stmt As SqlStatement = sb.GetStatement(True)
                Dim _Result As IList(Of TvMovieMapping) = ObjectFactory.GetCollection(GetType(TvMovieMapping), stmt.Execute())

                'Nach Sender mit Endung " HD" suchen
                If _Result.Count > 1 Then
                    For d = 0 To _Result.Count - 1
                        If InStr(_Result(d).ReferencedChannel.DisplayName, " HD") > 0 Then
                            Try
                                Dim _HDprogram As Program = program.RetrieveByTitleTimesAndChannel(program.Title, program.StartTime, program.EndTime, _Result(d).IdChannel)
                                Return _HDprogram.IdProgram
                                Exit Function
                            Catch ex As Exception
                                Return 0
                                MyLog.Debug("TVMovie: [GetHDChannel]: program not found on mapped _HDchannel ({0}, {1}, {2}, {3} -> {4})", program.Title, program.StartTime, program.EndTime, program.ReferencedChannel.DisplayName, _Result(d).IdChannel)
                                Exit Function
                            End Try
                        End If
                    Next
                End If

                Return 0

            Catch ex As Exception
                Return 0
                MyLog.Debug("TVMovie: [GetHDChannel]: {0} not mapped in TvMovieMapping", program.ReferencedChannel.DisplayName)
            End Try
        Else
            Return 0
        End If
    End Function

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
            Dim _newTvMovieProgram As New TVMovieProgram(Program.IdProgram)

            If _ClickfinderDB.Count > 0 Then

                'BildDateiname aus Clickfinder DB holen, sofern vorhanden
                If CBool(_ClickfinderDB(0).KzBilddateiHeruntergeladen) = True And Not String.IsNullOrEmpty(_ClickfinderDB(0).Bilddateiname) Then
                    _newTvMovieProgram.BildDateiname = _ClickfinderDB(0).Bilddateiname
                End If

                'TvMovie Bewertung aus Clickfinder DB holen, sofern vorhanden
                If Not _ClickfinderDB(0).Bewertung = 0 Then
                    _newTvMovieProgram.TVMovieBewertung = _ClickfinderDB(0).Bewertung
                End If

                'KurzKritik aus Clickfinder DB holen, sofern vorhanden
                If Not String.IsNullOrEmpty(_ClickfinderDB(0).Kurzkritik) Then
                    _newTvMovieProgram.KurzKritik = _ClickfinderDB(0).Kurzkritik
                End If

                _newTvMovieProgram.Persist()
                Return _newTvMovieProgram
            Else
                _newTvMovieProgram.Persist()
                MyLog.[Warn]("[ItemsGuiWindow] [FillList]: Program {0} not found in ClickfinderDB (Title: {1}, Channel: {2}, startTime: {3}, starRating: {4})", _
                                        _newTvMovieProgram.ReferencedProgram.IdProgram, _newTvMovieProgram.ReferencedProgram.Title, _newTvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName, _newTvMovieProgram.ReferencedProgram.StartTime, _newTvMovieProgram.ReferencedProgram.StarRating)
                Return _newTvMovieProgram
            End If

        End Try
    End Function

    Friend Shared Sub ShowContextMenu(ByVal idProgram As Integer, ByVal idWindow As Integer)
        Dim dlgContext As GUIDialogMenu = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_MENU, Integer)), GUIDialogMenu)
        dlgContext.Reset()

        Dim _Program As Program = Program.Retrieve(idProgram)
        'ContextMenu Layout
        dlgContext.SetHeading(_Program.Title)
        dlgContext.ShowQuickNumbers = False

        'Kanal einschalten
        Dim lItemOn As New GUIListItem
        lItemOn.Label = Translation.ChannelON
        lItemOn.IconImage = "play_enabled.png"
        dlgContext.Add(lItemOn)
        lItemOn.Dispose()

        'Aufnehmen
        Dim lItemRec As New GUIListItem
        lItemRec.Label = Translation.Record
        lItemRec.IconImage = "tvguide_record_button.png"
        dlgContext.Add(lItemRec)
        lItemOn.Dispose()

        'Erinnern
        Dim lItemRem As New GUIListItem
        lItemRem.Label = Translation.Remember
        lItemRem.IconImage = "tvguide_notify_button.png"
        dlgContext.Add(lItemRem)
        lItemOn.Dispose()

        dlgContext.DoModal(idWindow)

        Select Case dlgContext.SelectedLabel
            Case Is = 0
                StartTv(_Program.ReferencedChannel)
            Case Is = 1
                LoadTVProgramInfo(_Program)
            Case Is = 2
                SetNotify(_Program)
        End Select

    End Sub

    Friend Shared Sub ShowActionMenu(ByVal Program As Program, ByVal idWindow As Integer)
        Dim dlgContext As GUIDialogMenu = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_MENU, Integer)), GUIDialogMenu)
        dlgContext.Reset()

        'ContextMenu Layout
        dlgContext.SetHeading(Program.Title)
        dlgContext.ShowQuickNumbers = True

        'Kanal einschalten
        Dim lItemOn As New GUIListItem
        lItemOn.Label = Translation.ChannelON
        lItemOn.IconImage = "play_enabled.png"
        dlgContext.Add(lItemOn)
        lItemOn.Dispose()

        'Aufnehmen
        Dim lItemRec As New GUIListItem
        lItemRec.Label = Translation.Record
        lItemRec.IconImage = "tvguide_record_button.png"
        dlgContext.Add(lItemRec)
        lItemOn.Dispose()

        'Erinnern
        Dim lItemRem As New GUIListItem
        lItemRem.Label = Translation.Remember
        lItemRem.IconImage = "tvguide_notify_button.png"
        dlgContext.Add(lItemRem)
        lItemOn.Dispose()

        dlgContext.DoModal(idWindow)

        Select Case dlgContext.SelectedLabel
            Case Is = 0
                StartTv(Program.ReferencedChannel)
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
            Case Else
                MyLog.Debug("[ShowActionMenu]: exit")
                MyLog.Debug("")
        End Select
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


End Class