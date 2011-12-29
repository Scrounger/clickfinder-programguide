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



Public Class Setup

    Private SettingSQLWhere As String
    Private _AvailableTagesCategories As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private _AvailableVorschauCategories As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)

    Private Sub Setup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Rating As Integer
        Dim i As Integer
        Dim idGroup As Integer
        Dim ChannelName As String

        _AvailableTagesCategories.Clear()
        _AvailableVorschauCategories.Clear()


        'Available Tages Kategorien aus ClickfinderPGConfig.xml lesen und in Array packen
        Dim str_AvailableTagesCategories() As String = MPSettingRead("config", "AvailableTagesCategories").ToString.Split(CChar(";"))
        'Array durchlaufen und Kategorie übergeben
        For i = 0 To str_AvailableTagesCategories.Length - 1
            If Not str_AvailableTagesCategories(i) = "" Then
                _AvailableTagesCategories.Add(i, str_AvailableTagesCategories(i))
            End If
        Next

        'Available Vorschau Kategorien aus ClickfinderPGConfig.xml lesen und in Array packen
        Dim str_AvailableVorschauCategories() As String = MPSettingRead("config", "AvailableVorschauCategories").ToString.Split(CChar(";"))
        'Array durchlaufen und Kategorie übergeben
        For i = 0 To str_AvailableVorschauCategories.Length - 1
            If Not str_AvailableVorschauCategories(i) = "" Then
                _AvailableVorschauCategories.Add(i, str_AvailableVorschauCategories(i))
            End If
        Next

        'Gewählte Tages Kategorien aus ClickfinderPGConfig.xml lesen und in Array packen
        Dim str_VisibleTagesCategories() As String = MPSettingRead("config", "VisibleTagesCategories").ToString.Split(CChar(";"))
        'Array durchlaufen und Kategorie übergeben
        For i = 0 To str_VisibleTagesCategories.Length - 1
            If Not str_VisibleTagesCategories(i) = "" Then
                lvTagesCategorieChoosen.Items.Add(str_VisibleTagesCategories(i))
            End If
        Next

        'Gewählte Vorschau Kategorien aus ClickfinderPGConfig.xml lesen und in Array packen
        Dim str_VisibleVorschauCategories() As String = MPSettingRead("config", "VisibleVorschauCategories").ToString.Split(CChar(";"))
        'Array durchlaufen und Kategorie übergeben
        For i = 0 To str_VisibleVorschauCategories.Length - 1
            If Not str_VisibleVorschauCategories(i) = "" Then
                lvVorschauCategorieChoosen.Items.Add(str_VisibleVorschauCategories(i))
            End If
        Next


        For i = 0 To _AvailableTagesCategories.Count - 1
            lvTagesKategorien.Items.Add(_AvailableTagesCategories.Item(i).ToString)
        Next

        For i = 0 To _AvailableVorschauCategories.Count - 1
            lvVorschauKategorien.Items.Add(_AvailableVorschauCategories.Item(i).ToString)
        Next

        tfPluginName.Text = MPSettingRead("config", "PluginName")

        ChannelName = "Bitte wählen..."

        Me.tfClickfinderPath.Text = MPSettingRead("config", "ClickfinderPath")
        Rating = CInt(MPSettingRead("config", "ClickfinderRating"))
        idGroup = CInt(MPSettingRead("config", "ChannelGroupID"))
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


        Dim sb As New SqlBuilder(StatementType.[Select], GetType(ChannelGroup))
        Dim stmt As SqlStatement = sb.GetStatement(True)
        Dim _Result As IList = ObjectFactory.GetCollection(GetType(ChannelGroup), stmt.Execute())


        For i = 0 To _Result.Count - 1
            Dim _ChannelGroup As ChannelGroup = _Result.Item(i)

            Me.CBChannelGroup.Items.Add(_ChannelGroup.GroupName)

            If idGroup = _ChannelGroup.IdGroup Then
                ChannelName = _ChannelGroup.GroupName
            End If

        Next

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

        For i = 0 To 100
            CBListOffsetY.Items.Add(i)
        Next

        For i = 0 To CBListOffsetY.Items.Count - 1
            If CBListOffsetY.Items.Item(i) = MPSettingRead("config", "ListLabelOffsetY") Then CBListOffsetY.Text = MPSettingRead("config", "ListLabelOffsetY")
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

        If MPSettingRead("MPTVSeries", "enable") = "true" Then
            CBTvSeries.CheckState = Windows.Forms.CheckState.Checked
        Else
            CBTvSeries.CheckState = Windows.Forms.CheckState.Unchecked
        End If

        If MPSettingRead("MPTVSeries", "ShowDescribtion") = "true" Then
            CBTvSeriesBeschreibung.CheckState = Windows.Forms.CheckState.Checked
        Else
            CBTvSeriesBeschreibung.CheckState = Windows.Forms.CheckState.Unchecked
        End If

        CBTvSeries_CheckedChanged(sender, e)
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

        Dim _VisibleTagesCategories As String
        Dim _VisibleVorschauCategories As String

        _VisibleTagesCategories = Nothing
        _VisibleVorschauCategories = Nothing

        MPSettingsWrite("config", "PluginName", tfPluginName.Text)
        MPSettingsWrite("config", "ClickfinderPath", tfClickfinderPath.Text.ToString)
        MPSettingsWrite("config", "ClickfinderRating", Me.cbRating.Text)
        MPSettingsWrite("config", "PrimeTimeHour", Me.CBPrimeTimeHour.Text)
        MPSettingsWrite("config", "PrimeTimeMinute", Me.CBPrimeTimeMinute.Text)
        MPSettingsWrite("config", "ListLabelOffsetY", Me.CBListOffsetY.Text)
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

        If CBTvSeries.CheckState = Windows.Forms.CheckState.Checked Then
            MPSettingsWrite("MPTVSeries", "enable", "true")
        Else
            MPSettingsWrite("MPTVSeries", "enable", "false")
        End If

        If CBTvSeriesBeschreibung.CheckState = Windows.Forms.CheckState.Checked Then
            MPSettingsWrite("MPTVSeries", "ShowDescribtion", "true")
        Else
            MPSettingsWrite("MPTVSeries", "ShowDescribtion", "false")
        End If


        Dim sb As New SqlBuilder(StatementType.[Select], GetType(ChannelGroup))
        sb.AddConstraint([Operator].Equals, "groupName", CBChannelGroup.Text)
        Dim stmt As SqlStatement = sb.GetStatement(True)
        Dim _Result As IList = ObjectFactory.GetCollection(GetType(ChannelGroup), stmt.Execute())

        For i = 0 To _Result.Count - 1
            Dim _ChannelGroup As ChannelGroup = _Result.Item(i)


            MPSettingsWrite("config", "ChannelGroupID", _ChannelGroup.IdGroup)
        Next



        For i = 0 To lvTagesCategorieChoosen.Items.Count - 1
            _VisibleTagesCategories = _VisibleTagesCategories & lvTagesCategorieChoosen.Items(i).Text & ";"
        Next
        MPSettingsWrite("config", "VisibleTagesCategories", _VisibleTagesCategories)


        For i = 0 To lvVorschauCategorieChoosen.Items.Count - 1
            _VisibleVorschauCategories = _VisibleVorschauCategories & lvVorschauCategorieChoosen.Items(i).Text & ";"
        Next
        MPSettingsWrite("config", "VisibleVorschauCategories", _VisibleVorschauCategories)


        Me.Close()

    End Sub

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
        ProgressBar1.Value = 0
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

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBCategorie.SelectedIndexChanged




        tfWhere.Text = MPSettingRead(CBCategorie.SelectedItem.ToString, SettingSQLWhere)
        tfOrderBy.Text = MPSettingRead(CBCategorie.SelectedItem.ToString, "OrderBy")

        tfWhere.Enabled = False
        tfOrderBy.Enabled = False
        btSave.Enabled = False

        Dim tmp As String = Replace(Replace(Replace(Replace(Replace(Replace("SELECT * FROM Sendungen INNER JOIN SendungenDetails ON Sendungen.Pos = SendungenDetails.Pos WHERE (Beginn Between #StartTime# AND #EndTime#) " & MPSettingRead(CBCategorie.SelectedItem.ToString, SettingSQLWhere) & " ORDERBY " & MPSettingRead(CBCategorie.SelectedItem.ToString, "OrderBy"), _
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

        For i = 0 To _AvailableTagesCategories.Count - 1
            CBCategorie.Items.Add(_AvailableTagesCategories.Item(i).ToString)
        Next

        tfOrderBy.Text = ""
        tfSQL.Text = ""
        tfWhere.Text = ""



    End Sub

    Private Sub rbVorschau_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbVorschau.CheckedChanged
        SettingSQLWhere = "PreviewWhere"

        CBCategorie.Items.Clear()

        For i = 0 To _AvailableVorschauCategories.Count - 1
            CBCategorie.Items.Add(_AvailableVorschauCategories.Item(i).ToString)
        Next

        tfOrderBy.Text = ""
        tfSQL.Text = ""
        tfWhere.Text = ""

    End Sub

    Private Sub tfWhere_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tfWhere.LostFocus
        Dim tmp As String = Replace(Replace(Replace(Replace(Replace(Replace("SELECT * FROM Sendungen INNER JOIN SendungenDetails ON Sendungen.Pos = SendungenDetails.Pos WHERE (Beginn Between #StartTime# AND #EndTime#) " & tfWhere.Text & " ORDERBY " & tfOrderBy.Text, _
                        "INNER JOIN", vbNewLine & "INNER JOIN"), _
                        "WHERE", vbNewLine & vbNewLine & "WHERE"), _
                        "AND", vbNewLine & "AND"), _
                        "OR", vbNewLine & "OR"), _
                        "ORDERBY", vbNewLine & "ORDERBY"), _
                        "ON", vbNewLine & "ON")

        tfSQL.Text = tmp
    End Sub
    Private Sub tfOrderBy_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tfOrderBy.LostFocus
        Dim tmp As String = Replace(Replace(Replace(Replace(Replace(Replace("SELECT * FROM Sendungen INNER JOIN SendungenDetails ON Sendungen.Pos = SendungenDetails.Pos WHERE (Beginn Between #StartTime# AND #EndTime#) " & tfWhere.Text & " ORDERBY " & tfOrderBy.Text, _
                "INNER JOIN", vbNewLine & "INNER JOIN"), _
                "WHERE", vbNewLine & vbNewLine & "WHERE"), _
                "AND", vbNewLine & "AND"), _
                "OR", vbNewLine & "OR"), _
                "ORDERBY", vbNewLine & "ORDERBY"), _
                "ON", vbNewLine & "ON")

        tfSQL.Text = tmp

    End Sub

    Private Sub lvTagesKategorien_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvTagesKategorien.MouseDoubleClick



        For i = 0 To lvTagesCategorieChoosen.Items.Count - 1
            If lvTagesCategorieChoosen.Items(i).Text = lvTagesKategorien.SelectedItems.Item(0).Text Then
                MsgBox("Kategorie schon vorhanden!", MsgBoxStyle.Information, "Keine doppelten Einträge zulässig")
                Exit Sub
            End If
        Next

        lvTagesCategorieChoosen.Items.Add(lvTagesKategorien.SelectedItems.Item(0).Text)


    End Sub

    Private Sub lvTagesCategorieChoosen_MouseDoubleClick1(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvTagesCategorieChoosen.MouseDoubleClick
        lvTagesCategorieChoosen.SelectedItems.Item(0).Remove()
    End Sub

    Private Sub lvVorschauKategorien_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvVorschauKategorien.MouseDoubleClick

        For i = 0 To lvVorschauCategorieChoosen.Items.Count - 1
            If lvVorschauCategorieChoosen.Items(i).Text = lvVorschauKategorien.SelectedItems.Item(0).Text Then
                MsgBox("Kategorie schon vorhanden!", MsgBoxStyle.Information, "Keine doppelten Einträge zulässig")
                Exit Sub
            End If
        Next

        lvVorschauCategorieChoosen.Items.Add(lvVorschauKategorien.SelectedItems.Item(0).Text)

    End Sub

    Private Sub lvVorschauCategorieChoosen_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvVorschauCategorieChoosen.MouseDoubleClick
        lvVorschauCategorieChoosen.SelectedItems.Item(0).Remove()
    End Sub


    Private Sub MoveSelectedLVWItems(ByVal LVW As ListView, Optional ByVal Down As Boolean = False)
        Dim OldItem As ListViewItem
        Dim OldPos As Integer
        Dim i As Integer
        If LVW.SelectedItems.Count > 0 Then
            LVW.Sorting = SortOrder.None
            If Down = True Then
                If LVW.SelectedItems(LVW.SelectedItems.Count - 1).Index < LVW.Items.Count - 1 Then
                    For i = LVW.SelectedItems.Count - 1 To 0 Step -1
                        OldItem = LVW.Items(LVW.SelectedItems(i).Index + 1)
                        OldPos = LVW.Items(LVW.SelectedItems(i).Index).Index
                        LVW.Items(OldPos + 1) = LVW.SelectedItems(i).Clone
                        LVW.Items(OldPos) = OldItem
                        LVW.Items(OldPos + 1).Selected = True
                    Next
                End If
            Else
                If LVW.SelectedItems(0).Index > 0 Then
                    For i = 0 To LVW.SelectedItems.Count - 1
                        OldItem = LVW.Items(LVW.SelectedItems(i).Index - 1)
                        OldPos = LVW.Items(LVW.SelectedItems(i).Index).Index
                        LVW.Items(OldPos - 1) = LVW.SelectedItems(i).Clone
                        LVW.Items(OldPos) = OldItem
                        LVW.Items(OldPos - 1).Selected = True
                    Next
                End If
            End If
            LVW.Focus()
        End If
    End Sub

    Private Sub btTagesUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btTagesUp.Click
        MoveSelectedLVWItems(lvTagesCategorieChoosen)
    End Sub

    Private Sub btTagesDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btTagesDown.Click
        MoveSelectedLVWItems(lvTagesCategorieChoosen, True)
    End Sub

    Private Sub btVorschauUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btVorschauUp.Click
        MoveSelectedLVWItems(lvVorschauCategorieChoosen)
    End Sub

    Private Sub btVorschauDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btVorschauDown.Click
        MoveSelectedLVWItems(lvVorschauCategorieChoosen, True)
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        System.Diagnostics.Process.Start("https://code.google.com/p/clickfinder-programguide/wiki/Setup")
    End Sub

    Private Sub CBTvSeries_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBTvSeries.CheckedChanged
        If CBTvSeries.CheckState = CheckState.Checked Then
            CBTvSeriesBeschreibung.Enabled = True
            CBTvSeriesTvServerWrite.Enabled = True
        Else
            CBTvSeriesBeschreibung.Enabled = False
            CBTvSeriesTvServerWrite.Enabled = False
        End If
    End Sub

End Class