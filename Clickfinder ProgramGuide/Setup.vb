Imports System
Imports System.IO

Imports MediaPortal.Profile
Imports MediaPortal.Configuration
Imports MediaPortal.GUI.Library
Imports MediaPortal.Util
Imports MediaPortal.Utils
Imports MySql.Data
Imports MySql.Data.MySqlClient

Imports Gentle.Framework
Imports System.Drawing
Imports System.Threading



Public Class Setup

    Private SettingSQLWhere As String
    Private _TagesCategories As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private _VorschauCategories As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)



    Private Sub Setup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Rating As Integer
        Dim i As Integer
        Dim idGroup As String
        Dim ChannelName As String

        _TagesCategories.Clear()
        _VorschauCategories.Clear()


        _TagesCategories.Add(0, "Movies")
        _TagesCategories.Add(1, "Sky Cinema")
        _TagesCategories.Add(2, "Sky Dokumentationen")
        _TagesCategories.Add(3, "HDTV")
        _TagesCategories.Add(4, "Serien")
        _TagesCategories.Add(5, "Dokumentationen")
        _TagesCategories.Add(6, "Reportagen")
        _TagesCategories.Add(7, "Magazine")
        _TagesCategories.Add(8, "Sport")

        _VorschauCategories.Add(0, "Movies")
        _VorschauCategories.Add(1, "Sky Cinema")
        _VorschauCategories.Add(2, "Sky Dokumentationen")
        _VorschauCategories.Add(3, "Fußball LIVE")


        For i = 0 To _TagesCategories.Count - 1
            lvTagesKategorien.Items.Add(_TagesCategories.Item(i).ToString)

        Next


        ChannelName = "Bitte wählen..."

        Me.tfClickfinderPath.Text = MPSettingRead("config", "ClickfinderPath")
        Rating = CInt(MPSettingRead("config", "ClickfinderRating"))
        idGroup = MPSettingRead("config", "ChannelGroupID")
        MPSettingsWrite("Save", "LastUpdate", "01.01.2011")

        Select Case Rating
            Case Is = 0
                Me.cbRating.Text = Me.cbRating.Items.Item(0)
            Case Is = 1
                Me.cbRating.Text = Me.cbRating.Items.Item(1)
            Case Is = 2
                Me.cbRating.Text = Me.cbRating.Items.Item(2)
            Case Is = 3
                Me.cbRating.Text = Me.cbRating.Items.Item(3)
        End Select

        ReadTvServerDB("Select * from channelgroup")
        While TvServerData.Read

            Me.CBChannelGroup.Items.Add(TvServerData.Item("groupName"))

            If idGroup = TvServerData.Item("idGroup") Then
                ChannelName = TvServerData.Item("groupName")
            End If

        End While

        CloseTvServerDB()

        For i = 0 To CBChannelGroup.Items.Count - 1
            If InStr(CBChannelGroup.Items.Item(i), ChannelName) Then
                CBChannelGroup.Text = CBChannelGroup.Items.Item(i)
            End If

        Next

        For i = 0 To 24
            CBPrimeTimeHour.Items.Add(Format(i, "00"))
            CBLateTimeHour.Items.Add(Format(i, "00"))
        Next
        For i = 0 To 59
            CBPrimeTimeMinute.Items.Add(Format(i, "00"))
            CBLateTimeMinute.Items.Add(Format(i, "00"))
            CBDelayNow.Items.Add(Format(i, "00"))
            CBMinTime.Items.Add(Format(i, "00"))
        Next


        For i = 0 To CBPrimeTimeHour.Items.Count - 1
            If CBPrimeTimeHour.Items.Item(i) = MPSettingRead("config", "PrimeTimeHour") Then CBPrimeTimeHour.Text = MPSettingRead("config", "PrimeTimeHour")
        Next

        For i = 0 To CBPrimeTimeMinute.Items.Count - 1
            If CBPrimeTimeMinute.Items.Item(i) = MPSettingRead("config", "PrimeTimeMinute") Then CBPrimeTimeMinute.Text = MPSettingRead("config", "PrimeTimeMinute")
        Next

        For i = 0 To CBLateTimeHour.Items.Count - 1
            If CBLateTimeHour.Items.Item(i) = MPSettingRead("config", "LateTimeHour") Then CBLateTimeHour.Text = MPSettingRead("config", "LateTimeHour")
        Next
        For i = 0 To CBLateTimeMinute.Items.Count - 1
            If CBLateTimeMinute.Items.Item(i) = MPSettingRead("config", "LateTimeMinute") Then CBLateTimeMinute.Text = MPSettingRead("config", "LateTimeMinute")
        Next
        For i = 0 To CBDelayNow.Items.Count - 1
            If CBDelayNow.Items.Item(i) = MPSettingRead("config", "DelayNow") Then CBDelayNow.Text = MPSettingRead("config", "DelayNow")
        Next
        For i = 0 To CBDelayNow.Items.Count - 1
            If CBMinTime.Items.Item(i) = MPSettingRead("config", "MinTime") Then CBMinTime.Text = MPSettingRead("config", "MinTime")
        Next

        For i = 0 To CBUpdateInterval.Items.Count - 1
            If CBUpdateInterval.Items.Item(i) = MPSettingRead("config", "UpdateInterval") Then CBUpdateInterval.Text = MPSettingRead("config", "UpdateInterval")
        Next

        If MPSettingRead("config", "IgnoreMinTimeSeries") = "true" Then
            CKIgnoreSeries.CheckState = Windows.Forms.CheckState.Checked
        Else
            CKIgnoreSeries.CheckState = Windows.Forms.CheckState.Unchecked
        End If

        If MPSettingRead("config", "useRatingTvLogos") = "true" Then
            CKRatingTVLogos.CheckState = Windows.Forms.CheckState.Checked
        Else
            CKRatingTVLogos.CheckState = Windows.Forms.CheckState.Unchecked
        End If


        If MPSettingRead("config", "LiveCorrection") = "true" Then
            CBLiveCorrection.CheckState = Windows.Forms.CheckState.Checked
        Else
            CBLiveCorrection.CheckState = Windows.Forms.CheckState.Unchecked
        End If

        If MPSettingRead("config", "WdhCorrection") = "true" Then
            CBWdhCorretcion.CheckState = Windows.Forms.CheckState.Checked
        Else
            CBWdhCorretcion.CheckState = Windows.Forms.CheckState.Unchecked
        End If


        rbHeute.Select()


    End Sub

    Public Sub MPSettingsWrite(ByVal section As String, ByVal entry As String, ByVal NewAttribute As String)
        Using xmlReader As New Settings(Config.GetFile(Config.Dir.Config, "ClickfinderPGConfig.xml"))
            xmlReader.SetValue(section, entry, NewAttribute)
        End Using

    End Sub

    Public Function MPSettingRead(ByVal section As String, ByVal entry As String) As String
        Using xmlReader As New Settings(Config.GetFile(Config.Dir.Config, "ClickfinderPGConfig.xml"))
            MPSettingRead = xmlReader.GetValue(section, entry)
        End Using
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        MPSettingsWrite("config", "ClickfinderPath", tfClickfinderPath.Text.ToString)
        MPSettingsWrite("config", "ClickfinderRating", Me.cbRating.Text)
        MPSettingsWrite("config", "PrimeTimeHour", Me.CBPrimeTimeHour.Text)
        MPSettingsWrite("config", "PrimeTimeMinute", Me.CBPrimeTimeMinute.Text)
        MPSettingsWrite("config", "LateTimeHour", Me.CBLateTimeHour.Text)
        MPSettingsWrite("config", "LateTimeMinute", Me.CBLateTimeMinute.Text)
        MPSettingsWrite("config", "DelayNow", Me.CBDelayNow.Text)
        MPSettingsWrite("config", "MinTime", Me.CBMinTime.Text)
        MPSettingsWrite("config", "UpdateInterval", Me.CBUpdateInterval.Text)

        If CKIgnoreSeries.CheckState = Windows.Forms.CheckState.Checked Then
            MPSettingsWrite("config", "IgnoreMinTimeSeries", "true")
        Else
            MPSettingsWrite("config", "IgnoreMinTimeSeries", "false")
        End If

        If CKRatingTVLogos.CheckState = Windows.Forms.CheckState.Checked Then
            MPSettingsWrite("config", "useRatingTvLogos", "true")
        Else
            MPSettingsWrite("config", "useRatingTvLogos", "false")
        End If

        If CBLiveCorrection.CheckState = Windows.Forms.CheckState.Checked Then
            MPSettingsWrite("config", "LiveCorrection", "true")
        Else
            MPSettingsWrite("config", "LiveCorrection", "false")
        End If

        If CBWdhCorretcion.CheckState = Windows.Forms.CheckState.Checked Then
            MPSettingsWrite("config", "WdhCorrection", "true")
        Else
            MPSettingsWrite("config", "WdhCorrection", "false")
        End If

        ReadTvServerDB("Select * from channelgroup Where groupName = '" & CBChannelGroup.Text & "'")
        While TvServerData.Read
            MPSettingsWrite("config", "ChannelGroupID", TvServerData.Item("idGroup"))
        End While
        CloseTvServerDB()

        Me.Close()

    End Sub

#Region "TV Server DB"
    Public ConTvServerDBRead As New MySqlConnection
    Public CmdTvServerDBRead As New MySqlCommand
    Public TvServerData As MySqlDataReader

    Public Sub ReadTvServerDB(ByVal SQLString As String)

        Try

            ConTvServerDBRead.ConnectionString = LeftCut(Replace(Gentle.Framework.GentleSettings.DefaultProviderConnectionString, " ", ""), InStr(Gentle.Framework.GentleSettings.DefaultProviderConnectionString, "charset=utf8") - 3)
            ConTvServerDBRead.Open()


            CmdTvServerDBRead = ConTvServerDBRead.CreateCommand
            CmdTvServerDBRead.CommandText = SQLString

            TvServerData = CmdTvServerDBRead.ExecuteReader


        Catch ex As Exception

            Log.Error("Clickfinder ProgramGuide: (Config: TvServer DB, read) " & ex.Message)
            MsgBox("Der TV Server wurde nicht gefunden!" & vbNewLine & "Bitte überprüfen Sie die Einstellungen in der Gentle.config Datei")

            CmdTvServerDBRead.Dispose()
            ConTvServerDBRead.Close()

        End Try


    End Sub
    Public Sub CloseTvServerDB()

        Try
            CmdTvServerDBRead.Dispose()
            ConTvServerDBRead.Close()
        Catch ex As Exception
            Log.Error("Clickfinder ProgramGuide: (Config: TvServer DB, close) " & ex.Message)
            MsgBox("Clickfinder ProgramGuide: (Config: TvServer DB, close) " & ex.Message)
        End Try

    End Sub

    Public Function LeftCut(ByVal sText As String, _
  ByVal nLen As Integer) As String

        If nLen > sText.Length Then nLen = sText.Length
        Return (sText.Substring(0, nLen))
    End Function
#End Region


    Private Sub BtnCreateLogos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCreateLogos.Click

        ProgressBar1.Visible = True

        ' Verzeichnis, dessen Dateien ermittelt werden sollen
        Dim TVLogoPath As String = Config.GetFile(Config.Dir.Thumbs, "tv\logos")
        'MsgBox("TVLogoPath: " & TVLogoPath)

        ' ggf. abschließenden Backslash entfernen
        If TVLogoPath.EndsWith("\") And TVLogoPath.Length > 3 Then
            TVLogoPath = TVLogoPath.Substring(0, TVLogoPath.Length - 1)
        End If

        ' Directory-Object erstellen
        Dim oDir As New System.IO.DirectoryInfo(TVLogoPath)

        ' alle Dateien des Ordners
        Dim oFiles As System.IO.FileInfo() = oDir.GetFiles()

        ' Datei-Array durchlaufen
        Dim oFile As System.IO.FileInfo
        'MsgBox("Directory Exist? " & Directory.Exists(Config.GetFile(Config.Dir.Thumbs, "ClickfinderPG\tv\logos")))
        If Not Directory.Exists(Config.GetFile(Config.Dir.Thumbs, "ClickfinderPG\tv\logos")) Then
            Directory.CreateDirectory(Config.GetFile(Config.Dir.Thumbs, "ClickfinderPG\tv\logos"))
        End If
        'MsgBox("RatingImage: " & Config.GetFile(Config.Dir.Skin, "DefaultWide\Media\ClickfinderPG_R3.png"))

        ProgressBar1.Maximum = oFiles.Length * 7
        ProgressBar1.Step = 1
        For i = 0 To 6


            For Each oFile In oFiles
                'msgbox(oFile.FullName)
                ProgressBar1.PerformStep()
                If Not i = 0 Then
                    Dim TVLogo As Image = Bitmap.FromFile(oFile.FullName)
                    Dim RatingImg As Image = Bitmap.FromFile(Config.GetFile(Config.Dir.Skin, "DefaultWide\Media\ClickfinderPG_R" & i & ".png"))
                    Dim b As New Bitmap(200, 200)
                    Dim g As Graphics = Graphics.FromImage(b)
                    g.DrawImage(TVLogo, 10, 0, 190, 190)
                    g.DrawImage(RatingImg, 0, 130, 70, 70)
                    b.Save(Config.GetFile(Config.Dir.Thumbs, "ClickfinderPG\tv\logos\") & Path.GetFileNameWithoutExtension(oFile.FullName) & "_" & i & ".png")
                Else
                    Dim TVLogo As Image = Bitmap.FromFile(oFile.FullName)
                    Dim b As New Bitmap(200, 200)
                    Dim g As Graphics = Graphics.FromImage(b)
                    g.DrawImage(TVLogo, 0, 0, 200, 200)
                    b.Save(Config.GetFile(Config.Dir.Thumbs, "ClickfinderPG\tv\logos\") & Path.GetFileNameWithoutExtension(oFile.FullName) & "_" & i & ".png")

                End If






            Next
        Next

        ProgressBar1.Visible = False

        CKRatingTVLogos.CheckState = Windows.Forms.CheckState.Checked

        'MsgBox("Output: " & Config.GetFile(Config.Dir.Thumbs, "ClickfinderPG\tv\logos\") & Path.GetFileNameWithoutExtension(oFile.FullName) & "_3.png")
        'For Each oFile In oFiles
        '    'msgbox(oFile.FullName)
        '    Dim img1 As Image = Bitmap.FromFile(oFile.FullName)
        '    Dim img2 As Image = Bitmap.FromFile("C:\Stuff\ImageAssembling\TestOrdner\ClickfinderPG_R2.png")
        '    Dim b As New Bitmap(210, 210)
        '    Dim g As Graphics = Graphics.FromImage(b)
        '    g.DrawImage(img1, 10, 0, 200, 200)
        '    g.DrawImage(img2, 0, 146, 64, 64)

        '    If Not Directory.Exists("C:\Stuff\ImageAssembling\TestOrdner\thumbs\Clickfinder\tv\logos") Then
        '        Directory.CreateDirectory("C:\Stuff\ImageAssembling\TestOrdner\thumbs\Clickfinder\tv\logos")
        '    End If

        '    b.Save("C:\Stuff\ImageAssembling\TestOrdner\thumbs\Clickfinder\tv\logos\" & Path.GetFileNameWithoutExtension(oFile.FullName) & "_2.png")
        'Next

        'For Each oFile In oFiles
        '    'msgbox(oFile.FullName)
        '    Dim img1 As Image = Bitmap.FromFile(oFile.FullName)
        '    Dim img2 As Image = Bitmap.FromFile("C:\Stuff\ImageAssembling\TestOrdner\ClickfinderPG_R1.png")
        '    Dim b As New Bitmap(210, 210)
        '    Dim g As Graphics = Graphics.FromImage(b)
        '    g.DrawImage(img1, 10, 0, 200, 200)
        '    g.DrawImage(img2, 0, 146, 64, 64)

        '    If Not Directory.Exists("C:\Stuff\ImageAssembling\TestOrdner\thumbs\Clickfinder\tv\logos") Then
        '        Directory.CreateDirectory("C:\Stuff\ImageAssembling\TestOrdner\thumbs\Clickfinder\tv\logos")
        '    End If

        '    b.Save("C:\Stuff\ImageAssembling\TestOrdner\thumbs\Clickfinder\tv\logos\" & Path.GetFileNameWithoutExtension(oFile.FullName) & "_1.png")
        'Next

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ShowProgressBar()
        ProgressBar1.Visible = True
        ProgressBar1.Style = Windows.Forms.ProgressBarStyle.Marquee

    End Sub

    'Private Sub ClickfinderCorrection()

    '    LiveCorrection = ClickfinderCorrectionFunction("KzLive", " (LIVE)")
    '    WdhCorrection = ClickfinderCorrectionFunction("KzLive", " (Wdh.)")
    '    MsgBox(LiveCorrection, WdhCorrection)

    'End Sub

    'Private Function ClickfinderCorrectionFunction(ByVal _SQLWhereString As String, ByVal _ProofSting As String) As Boolean
    '    Dim _idChannel As String
    '    Dim _StartZeit As Date
    '    Dim _EndZeit As Date
    '    Dim _StartZeitSQL As String
    '    Dim _EndZeitSQL As String
    '    Dim _Titel As String



    '    Try
    '        ReadClickfinderDB("SELECT Titel, Beginn, Ende, SenderKennung FROM Sendungen WHERE " & _SQLWhereString & " = true")

    '        While ClickfinderData.Read

    '            _Titel = ClickfinderData.Item("Titel")
    '            _StartZeit = CDate(ClickfinderData.Item("Beginn"))
    '            _EndZeit = CDate(ClickfinderData.Item("Ende"))

    '            _StartZeitSQL = DateTOMySQLstring(_StartZeit)

    '            _EndZeitSQL = DateTOMySQLstring(_EndZeit)



    '            ReadTvServerDB("Select * from tvmoviemapping WHERE stationName = '" & ClickfinderData.Item("SenderKennung").ToString & "'")
    '            While TvServerData.Read

    '                _idChannel = TvServerData.Item("idChannel")

    '                Exit While
    '            End While
    '            CloseTvServerDB()

    '            If ProgramFoundinTvDb(_Titel & _ProofSting, _idChannel, _StartZeit, _EndZeit) = True Then
    '                ClickfinderCorrectionFunction = True
    '                Exit While
    '                Log.Info("Clickfinder ProgramGuide: [ClickfinderCorrection]: " & _ProofSting & " = true")
    '            End If


    '            Log.Debug("Clickfinder ProgramGuide: [ClickfinderCorrection]: nothing")

    '        End While
    '        CloseClickfinderDB()

    '    Catch ex As Exception
    '        Log.Error("Clickfinder ProgramGuide: [ClickfinderCorrection]: " & ex.Message)
    '    End Try

    'End Function

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBCategorie.SelectedIndexChanged
      



        tfWhere.Text = MPSettingRead(CBCategorie.SelectedItem.ToString, SettingSQLWhere)
        tfOrderBy.Text = MPSettingRead(CBCategorie.SelectedItem.ToString, "OrderBy")

        tfWhere.Enabled = False
        tfOrderBy.Enabled = False
        btSave.Enabled = False

        Dim tmp As String = Replace(Replace(Replace(Replace(Replace(Replace("SELECT * FROM Sendungen INNER JOIN SendungenDetails ON Sendungen.Pos = SendungenDetails.Pos WHERE (Beginn Between #StartTime AND #EndTime) AND " & MPSettingRead(CBCategorie.SelectedItem.ToString, SettingSQLWhere) & " ORDERBY " & MPSettingRead(CBCategorie.SelectedItem.ToString, "OrderBy"), _
                                "INNER JOIN", vbNewLine & "INNER JOIN"), _
                                "WHERE", vbNewLine & vbNewLine & "WHERE"), _
                                "AND", vbNewLine & "AND"), _
                                "OR", vbNewLine & "OR"), _
                                "ORDERBY", vbNewLine & "ORDERBY"), _
                                "ON", vbNewLine & "ON")

        tfSQL.Text = tmp

    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        tfWhere.Enabled = True
        tfOrderBy.Enabled = True
        btSave.Enabled = True
    End Sub

    Private Sub btSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSave.Click
        MPSettingsWrite(CBCategorie.SelectedItem.ToString, SettingSQLWhere, tfWhere.Text)
        MPSettingsWrite(CBCategorie.SelectedItem.ToString, "OrderBy", tfOrderBy.Text)
        tfWhere.Enabled = False
        tfOrderBy.Enabled = False
        btSave.Enabled = False

        tfOrderBy.Text = ""
        tfSQL.Text = ""
        tfWhere.Text = ""
    End Sub

    Private Sub rbHeute_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbHeute.CheckedChanged
        SettingSQLWhere = "Where"

        CBCategorie.Items.Clear()

        For i = 0 To _TagesCategories.Count - 1
            CBCategorie.Items.Add(_TagesCategories.Item(i).ToString)
        Next

        tfOrderBy.Text = ""
        tfSQL.Text = ""
        tfWhere.Text = ""



    End Sub

    Private Sub rbVorschau_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbVorschau.CheckedChanged
        SettingSQLWhere = "PreviewWhere"

        CBCategorie.Items.Clear()

        For i = 0 To _VorschauCategories.Count - 1
            CBCategorie.Items.Add(_VorschauCategories.Item(i).ToString)
        Next

        tfOrderBy.Text = ""
        tfSQL.Text = ""
        tfWhere.Text = ""

    End Sub

    Private Sub tfWhere_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tfWhere.LostFocus
        Dim tmp As String = Replace(Replace(Replace(Replace(Replace(Replace("SELECT * FROM Sendungen INNER JOIN SendungenDetails ON Sendungen.Pos = SendungenDetails.Pos WHERE (Beginn Between #StartTime AND #EndTime) AND " & tfWhere.Text & " ORDERBY " & tfOrderBy.Text, _
                        "INNER JOIN", vbNewLine & "INNER JOIN"), _
                        "WHERE", vbNewLine & vbNewLine & "WHERE"), _
                        "AND", vbNewLine & "AND"), _
                        "OR", vbNewLine & "OR"), _
                        "ORDERBY", vbNewLine & "ORDERBY"), _
                        "ON", vbNewLine & "ON")

        tfSQL.Text = tmp
    End Sub
    Private Sub tfOrderBy_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tfOrderBy.LostFocus
        Dim tmp As String = Replace(Replace(Replace(Replace(Replace(Replace("SELECT * FROM Sendungen INNER JOIN SendungenDetails ON Sendungen.Pos = SendungenDetails.Pos WHERE (Beginn Between #StartTime AND #EndTime) AND " & tfWhere.Text & " ORDERBY " & tfOrderBy.Text, _
                "INNER JOIN", vbNewLine & "INNER JOIN"), _
                "WHERE", vbNewLine & vbNewLine & "WHERE"), _
                "AND", vbNewLine & "AND"), _
                "OR", vbNewLine & "OR"), _
                "ORDERBY", vbNewLine & "ORDERBY"), _
                "ON", vbNewLine & "ON")

        tfSQL.Text = tmp

    End Sub


End Class