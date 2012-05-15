<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Setup
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Setup))
        Me.ButtonSave = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabAllgemeines = New System.Windows.Forms.TabPage
        Me.Label23 = New System.Windows.Forms.Label
        Me.CBStartGui = New System.Windows.Forms.ComboBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.tbMPDatabasePath = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.tbPluginName = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.CbQuick2 = New System.Windows.Forms.ComboBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.CbQuick1 = New System.Windows.Forms.ComboBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.cbStandardGroup = New System.Windows.Forms.ComboBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.CheckBoxClickfinderPG = New MediaPortal.UserInterface.Controls.MPCheckBox
        Me.CheckBoxVideoDB = New MediaPortal.UserInterface.Controls.MPCheckBox
        Me.CheckBoxMovingPictures = New MediaPortal.UserInterface.Controls.MPCheckBox
        Me.CheckBoxTvSeries = New MediaPortal.UserInterface.Controls.MPCheckBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.tbLateTime = New System.Windows.Forms.MaskedTextBox
        Me.tbPrimeTime = New System.Windows.Forms.MaskedTextBox
        Me.CheckBoxDebugMode = New System.Windows.Forms.CheckBox
        Me.CheckBoxUseSportLogos = New System.Windows.Forms.CheckBox
        Me.ButtonOpenDlgImageFolder = New System.Windows.Forms.Button
        Me.ButtonOpenDlgDatabase = New System.Windows.Forms.Button
        Me.tbClickfinderDatabase = New System.Windows.Forms.TextBox
        Me.tbClickfinderImagePath = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabItems = New System.Windows.Forms.TabPage
        Me.CheckBoxRemberSortedBy = New System.Windows.Forms.CheckBox
        Me.CheckBoxFilterShowLocalSeries = New System.Windows.Forms.CheckBox
        Me.CheckBoxFilterShowLocalMovies = New System.Windows.Forms.CheckBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.TabÜbersicht = New System.Windows.Forms.TabPage
        Me.NumMaxDays = New System.Windows.Forms.NumericUpDown
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.NumHighlightsMinRuntime = New System.Windows.Forms.NumericUpDown
        Me.NumShowHighlightsAfter = New System.Windows.Forms.NumericUpDown
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.RBstartTime = New System.Windows.Forms.RadioButton
        Me.RBTvMovieStar = New System.Windows.Forms.RadioButton
        Me.RBRatingStar = New System.Windows.Forms.RadioButton
        Me.CheckBoxShowTagesTipp = New System.Windows.Forms.CheckBox
        Me.NumShowMoviesAfter = New System.Windows.Forms.NumericUpDown
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.CheckBoxShowLocalMovies = New System.Windows.Forms.CheckBox
        Me.TabCategories = New System.Windows.Forms.TabPage
        Me.CheckBoxShowCategorieLocalSeries = New System.Windows.Forms.CheckBox
        Me.CheckBoxShowCategorieLocalMovies = New System.Windows.Forms.CheckBox
        Me.CheckBoxSelect = New System.Windows.Forms.CheckBox
        Me.ButtonCategoriesDefault = New System.Windows.Forms.Button
        Me.ButtonNewCategorie = New System.Windows.Forms.Button
        Me.dgvCategories = New System.Windows.Forms.DataGridView
        Me.C_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.C_Image = New System.Windows.Forms.DataGridViewImageColumn
        Me.C_visible = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.C_Name = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.C_Beschreibung = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.C_sortOrder = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.C_SqlString = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.C_MinRuntime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.C_NowOffset = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ButtonDown = New System.Windows.Forms.Button
        Me.ButtonUp = New System.Windows.Forms.Button
        Me.TabDetail = New System.Windows.Forms.TabPage
        Me.GroupDetailSeriesImage = New System.Windows.Forms.GroupBox
        Me.RBTvMovieImage = New System.Windows.Forms.RadioButton
        Me.RBEpisodeImage = New System.Windows.Forms.RadioButton
        Me.RBSeriesFanArt = New System.Windows.Forms.RadioButton
        Me.RBSeriesCover = New System.Windows.Forms.RadioButton
        Me.CheckBoxUseSeriesDescribtion = New System.Windows.Forms.CheckBox
        Me.TabOverlay = New System.Windows.Forms.TabPage
        Me.CheckBoxEnableSeriesOverlay = New System.Windows.Forms.CheckBox
        Me.CheckBoxEnableMovieOverlay = New System.Windows.Forms.CheckBox
        Me.GroupBoxMovieOverlay = New System.Windows.Forms.GroupBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.CBOverlayGroup = New System.Windows.Forms.ComboBox
        Me.NumOverlayLimit = New System.Windows.Forms.NumericUpDown
        Me.Label21 = New System.Windows.Forms.Label
        Me.CheckBoxOverlayShowLocalMovies = New System.Windows.Forms.CheckBox
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.RBOverlayStartTime = New System.Windows.Forms.RadioButton
        Me.RBOverlayTvMovieStar = New System.Windows.Forms.RadioButton
        Me.RBOverlayRatingStar = New System.Windows.Forms.RadioButton
        Me.CheckBoxOverlayShowTagesTipp = New System.Windows.Forms.CheckBox
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.RBOverlayLateTime = New System.Windows.Forms.RadioButton
        Me.RBOverlayPrimeTime = New System.Windows.Forms.RadioButton
        Me.RBOverlayNow = New System.Windows.Forms.RadioButton
        Me.RBOverlayHeute = New System.Windows.Forms.RadioButton
        Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.NumPreviewDays = New System.Windows.Forms.NumericUpDown
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.NumPreviewMinTvMovieRating = New System.Windows.Forms.NumericUpDown
        Me.TabControl1.SuspendLayout()
        Me.TabAllgemeines.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TabItems.SuspendLayout()
        Me.TabÜbersicht.SuspendLayout()
        CType(Me.NumMaxDays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.NumHighlightsMinRuntime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumShowHighlightsAfter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.NumShowMoviesAfter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabCategories.SuspendLayout()
        CType(Me.dgvCategories, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabDetail.SuspendLayout()
        Me.GroupDetailSeriesImage.SuspendLayout()
        Me.TabOverlay.SuspendLayout()
        Me.GroupBoxMovieOverlay.SuspendLayout()
        CType(Me.NumOverlayLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumPreviewDays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox8.SuspendLayout()
        CType(Me.NumPreviewMinTvMovieRating, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonSave
        '
        Me.ButtonSave.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSave.Location = New System.Drawing.Point(583, 701)
        Me.ButtonSave.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(79, 39)
        Me.ButtonSave.TabIndex = 8
        Me.ButtonSave.Text = "speichern"
        Me.ButtonSave.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(76, 18)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(486, 25)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Clickfinder Program Guide Configuration"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.Location = New System.Drawing.Point(125, 713)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(194, 14)
        Me.LinkLabel1.TabIndex = 38
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Tag = ""
        Me.LinkLabel1.Text = "Ausführliche Anleitung im Wiki"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabAllgemeines)
        Me.TabControl1.Controls.Add(Me.TabItems)
        Me.TabControl1.Controls.Add(Me.TabÜbersicht)
        Me.TabControl1.Controls.Add(Me.TabCategories)
        Me.TabControl1.Controls.Add(Me.TabDetail)
        Me.TabControl1.Controls.Add(Me.TabOverlay)
        Me.TabControl1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(12, 73)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(654, 617)
        Me.TabControl1.TabIndex = 39
        '
        'TabAllgemeines
        '
        Me.TabAllgemeines.Controls.Add(Me.GroupBox8)
        Me.TabAllgemeines.Controls.Add(Me.Label23)
        Me.TabAllgemeines.Controls.Add(Me.CBStartGui)
        Me.TabAllgemeines.Controls.Add(Me.GroupBox5)
        Me.TabAllgemeines.Controls.Add(Me.tbPluginName)
        Me.TabAllgemeines.Controls.Add(Me.Label20)
        Me.TabAllgemeines.Controls.Add(Me.Label19)
        Me.TabAllgemeines.Controls.Add(Me.CbQuick2)
        Me.TabAllgemeines.Controls.Add(Me.Label18)
        Me.TabAllgemeines.Controls.Add(Me.CbQuick1)
        Me.TabAllgemeines.Controls.Add(Me.Label15)
        Me.TabAllgemeines.Controls.Add(Me.cbStandardGroup)
        Me.TabAllgemeines.Controls.Add(Me.GroupBox4)
        Me.TabAllgemeines.Controls.Add(Me.Label17)
        Me.TabAllgemeines.Controls.Add(Me.Label16)
        Me.TabAllgemeines.Controls.Add(Me.tbLateTime)
        Me.TabAllgemeines.Controls.Add(Me.tbPrimeTime)
        Me.TabAllgemeines.Controls.Add(Me.CheckBoxDebugMode)
        Me.TabAllgemeines.Controls.Add(Me.CheckBoxUseSportLogos)
        Me.TabAllgemeines.Controls.Add(Me.ButtonOpenDlgImageFolder)
        Me.TabAllgemeines.Controls.Add(Me.ButtonOpenDlgDatabase)
        Me.TabAllgemeines.Controls.Add(Me.tbClickfinderDatabase)
        Me.TabAllgemeines.Controls.Add(Me.tbClickfinderImagePath)
        Me.TabAllgemeines.Controls.Add(Me.Label3)
        Me.TabAllgemeines.Controls.Add(Me.Label1)
        Me.TabAllgemeines.Location = New System.Drawing.Point(4, 25)
        Me.TabAllgemeines.Name = "TabAllgemeines"
        Me.TabAllgemeines.Padding = New System.Windows.Forms.Padding(3)
        Me.TabAllgemeines.Size = New System.Drawing.Size(646, 588)
        Me.TabAllgemeines.TabIndex = 0
        Me.TabAllgemeines.Text = "Allgemeines"
        Me.TabAllgemeines.UseVisualStyleBackColor = True
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(9, 141)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(76, 16)
        Me.Label23.TabIndex = 34
        Me.Label23.Text = "Start GUI:"
        '
        'CBStartGui
        '
        Me.CBStartGui.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBStartGui.FormattingEnabled = True
        Me.CBStartGui.Items.AddRange(New Object() {""})
        Me.CBStartGui.Location = New System.Drawing.Point(91, 139)
        Me.CBStartGui.Name = "CBStartGui"
        Me.CBStartGui.Size = New System.Drawing.Size(191, 24)
        Me.CBStartGui.TabIndex = 33
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.tbMPDatabasePath)
        Me.GroupBox5.Controls.Add(Me.Label6)
        Me.GroupBox5.Location = New System.Drawing.Point(12, 346)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(621, 85)
        Me.GroupBox5.TabIndex = 32
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "MediaPortal database path (zur Info)"
        '
        'tbMPDatabasePath
        '
        Me.tbMPDatabasePath.Enabled = False
        Me.tbMPDatabasePath.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbMPDatabasePath.Location = New System.Drawing.Point(16, 51)
        Me.tbMPDatabasePath.Name = "tbMPDatabasePath"
        Me.tbMPDatabasePath.Size = New System.Drawing.Size(588, 23)
        Me.tbMPDatabasePath.TabIndex = 79
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 24)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(433, 16)
        Me.Label6.TabIndex = 78
        Me.Label6.Text = "MediaPortal Datenbank Pfad aus der lokalen MediaPortalDirs.xml "
        '
        'tbPluginName
        '
        Me.tbPluginName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPluginName.Location = New System.Drawing.Point(182, 18)
        Me.tbPluginName.Name = "tbPluginName"
        Me.tbPluginName.Size = New System.Drawing.Size(418, 23)
        Me.tbPluginName.TabIndex = 31
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(6, 21)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(93, 16)
        Me.Label20.TabIndex = 30
        Me.Label20.Text = "Plugin Name:"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(388, 280)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(174, 16)
        Me.Label19.TabIndex = 29
        Me.Label19.Text = "Quick Tv Gruppe Filter 2:"
        '
        'CbQuick2
        '
        Me.CbQuick2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbQuick2.FormattingEnabled = True
        Me.CbQuick2.Location = New System.Drawing.Point(400, 302)
        Me.CbQuick2.Name = "CbQuick2"
        Me.CbQuick2.Size = New System.Drawing.Size(191, 24)
        Me.CbQuick2.TabIndex = 28
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(388, 228)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(174, 16)
        Me.Label18.TabIndex = 27
        Me.Label18.Text = "Quick Tv Gruppe Filter 1:"
        '
        'CbQuick1
        '
        Me.CbQuick1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbQuick1.FormattingEnabled = True
        Me.CbQuick1.Location = New System.Drawing.Point(400, 250)
        Me.CbQuick1.Name = "CbQuick1"
        Me.CbQuick1.Size = New System.Drawing.Size(191, 24)
        Me.CbQuick1.TabIndex = 26
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(388, 176)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(145, 16)
        Me.Label15.TabIndex = 25
        Me.Label15.Text = "standard Tv Gruppe:"
        '
        'cbStandardGroup
        '
        Me.cbStandardGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbStandardGroup.FormattingEnabled = True
        Me.cbStandardGroup.Location = New System.Drawing.Point(400, 198)
        Me.cbStandardGroup.Name = "cbStandardGroup"
        Me.cbStandardGroup.Size = New System.Drawing.Size(191, 24)
        Me.cbStandardGroup.TabIndex = 24
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.CheckBoxClickfinderPG)
        Me.GroupBox4.Controls.Add(Me.CheckBoxVideoDB)
        Me.GroupBox4.Controls.Add(Me.CheckBoxMovingPictures)
        Me.GroupBox4.Controls.Add(Me.CheckBoxTvSeries)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 440)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(621, 115)
        Me.GroupBox4.TabIndex = 12
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Tv Movie EPG Import++ settings (zur Info)"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(13, 29)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(549, 16)
        Me.Label9.TabIndex = 77
        Me.Label9.Text = "Diese Einstellung müssen im Tv Movie EPG Import++ Plugin vorgenommen werden."
        '
        'CheckBoxClickfinderPG
        '
        Me.CheckBoxClickfinderPG.AutoSize = True
        Me.CheckBoxClickfinderPG.Enabled = False
        Me.CheckBoxClickfinderPG.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.CheckBoxClickfinderPG.Location = New System.Drawing.Point(16, 58)
        Me.CheckBoxClickfinderPG.Name = "CheckBoxClickfinderPG"
        Me.CheckBoxClickfinderPG.Size = New System.Drawing.Size(232, 20)
        Me.CheckBoxClickfinderPG.TabIndex = 76
        Me.CheckBoxClickfinderPG.Text = "Clickfinder ProgramGuide import"
        Me.CheckBoxClickfinderPG.UseVisualStyleBackColor = True
        '
        'CheckBoxVideoDB
        '
        Me.CheckBoxVideoDB.AutoSize = True
        Me.CheckBoxVideoDB.Enabled = False
        Me.CheckBoxVideoDB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.CheckBoxVideoDB.Location = New System.Drawing.Point(435, 84)
        Me.CheckBoxVideoDB.Name = "CheckBoxVideoDB"
        Me.CheckBoxVideoDB.Size = New System.Drawing.Size(169, 20)
        Me.CheckBoxVideoDB.TabIndex = 75
        Me.CheckBoxVideoDB.Text = "VideoDatabase import"
        Me.CheckBoxVideoDB.UseVisualStyleBackColor = True
        '
        'CheckBoxMovingPictures
        '
        Me.CheckBoxMovingPictures.AutoSize = True
        Me.CheckBoxMovingPictures.Enabled = False
        Me.CheckBoxMovingPictures.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.CheckBoxMovingPictures.Location = New System.Drawing.Point(214, 84)
        Me.CheckBoxMovingPictures.Name = "CheckBoxMovingPictures"
        Me.CheckBoxMovingPictures.Size = New System.Drawing.Size(170, 20)
        Me.CheckBoxMovingPictures.TabIndex = 74
        Me.CheckBoxMovingPictures.Text = "MovingPictures import"
        Me.CheckBoxMovingPictures.UseVisualStyleBackColor = True
        '
        'CheckBoxTvSeries
        '
        Me.CheckBoxTvSeries.AutoSize = True
        Me.CheckBoxTvSeries.Enabled = False
        Me.CheckBoxTvSeries.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.CheckBoxTvSeries.Location = New System.Drawing.Point(16, 84)
        Me.CheckBoxTvSeries.Name = "CheckBoxTvSeries"
        Me.CheckBoxTvSeries.Size = New System.Drawing.Size(154, 20)
        Me.CheckBoxTvSeries.TabIndex = 73
        Me.CheckBoxTvSeries.Text = "MP-TvSeries import"
        Me.CheckBoxTvSeries.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(48, 176)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(85, 16)
        Me.Label17.TabIndex = 11
        Me.Label17.Text = "Prime Time:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(48, 213)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(79, 16)
        Me.Label16.TabIndex = 10
        Me.Label16.Text = "Late Time:"
        '
        'tbLateTime
        '
        Me.tbLateTime.Location = New System.Drawing.Point(139, 210)
        Me.tbLateTime.Mask = "00:00"
        Me.tbLateTime.Name = "tbLateTime"
        Me.tbLateTime.Size = New System.Drawing.Size(42, 23)
        Me.tbLateTime.TabIndex = 9
        '
        'tbPrimeTime
        '
        Me.tbPrimeTime.Location = New System.Drawing.Point(139, 173)
        Me.tbPrimeTime.Mask = "00:00"
        Me.tbPrimeTime.Name = "tbPrimeTime"
        Me.tbPrimeTime.Size = New System.Drawing.Size(42, 23)
        Me.tbPrimeTime.TabIndex = 8
        '
        'CheckBoxDebugMode
        '
        Me.CheckBoxDebugMode.AutoSize = True
        Me.CheckBoxDebugMode.Location = New System.Drawing.Point(530, 562)
        Me.CheckBoxDebugMode.Name = "CheckBoxDebugMode"
        Me.CheckBoxDebugMode.Size = New System.Drawing.Size(103, 20)
        Me.CheckBoxDebugMode.TabIndex = 7
        Me.CheckBoxDebugMode.Text = "DebugMode"
        Me.CheckBoxDebugMode.UseVisualStyleBackColor = True
        '
        'CheckBoxUseSportLogos
        '
        Me.CheckBoxUseSportLogos.AutoSize = True
        Me.CheckBoxUseSportLogos.Location = New System.Drawing.Point(368, 143)
        Me.CheckBoxUseSportLogos.Name = "CheckBoxUseSportLogos"
        Me.CheckBoxUseSportLogos.Size = New System.Drawing.Size(183, 20)
        Me.CheckBoxUseSportLogos.TabIndex = 6
        Me.CheckBoxUseSportLogos.Text = "Sport Logos verwenden"
        Me.CheckBoxUseSportLogos.UseVisualStyleBackColor = True
        '
        'ButtonOpenDlgImageFolder
        '
        Me.ButtonOpenDlgImageFolder.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonOpenDlgImageFolder.Location = New System.Drawing.Point(608, 99)
        Me.ButtonOpenDlgImageFolder.Name = "ButtonOpenDlgImageFolder"
        Me.ButtonOpenDlgImageFolder.Size = New System.Drawing.Size(23, 23)
        Me.ButtonOpenDlgImageFolder.TabIndex = 5
        Me.ButtonOpenDlgImageFolder.Text = "..."
        Me.ButtonOpenDlgImageFolder.UseVisualStyleBackColor = True
        '
        'ButtonOpenDlgDatabase
        '
        Me.ButtonOpenDlgDatabase.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonOpenDlgDatabase.Location = New System.Drawing.Point(608, 58)
        Me.ButtonOpenDlgDatabase.Name = "ButtonOpenDlgDatabase"
        Me.ButtonOpenDlgDatabase.Size = New System.Drawing.Size(23, 23)
        Me.ButtonOpenDlgDatabase.TabIndex = 4
        Me.ButtonOpenDlgDatabase.Text = "..."
        Me.ButtonOpenDlgDatabase.UseVisualStyleBackColor = True
        '
        'tbClickfinderDatabase
        '
        Me.tbClickfinderDatabase.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbClickfinderDatabase.Location = New System.Drawing.Point(182, 58)
        Me.tbClickfinderDatabase.Name = "tbClickfinderDatabase"
        Me.tbClickfinderDatabase.Size = New System.Drawing.Size(418, 23)
        Me.tbClickfinderDatabase.TabIndex = 3
        '
        'tbClickfinderImagePath
        '
        Me.tbClickfinderImagePath.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbClickfinderImagePath.Location = New System.Drawing.Point(182, 99)
        Me.tbClickfinderImagePath.Name = "tbClickfinderImagePath"
        Me.tbClickfinderImagePath.Size = New System.Drawing.Size(418, 23)
        Me.tbClickfinderImagePath.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 102)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(170, 16)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Clickfinder Bilder Ordner:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 61)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(148, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Clickfinder Database:"
        '
        'TabItems
        '
        Me.TabItems.Controls.Add(Me.CheckBoxRemberSortedBy)
        Me.TabItems.Controls.Add(Me.CheckBoxFilterShowLocalSeries)
        Me.TabItems.Controls.Add(Me.CheckBoxFilterShowLocalMovies)
        Me.TabItems.Controls.Add(Me.Label14)
        Me.TabItems.Location = New System.Drawing.Point(4, 25)
        Me.TabItems.Name = "TabItems"
        Me.TabItems.Padding = New System.Windows.Forms.Padding(3)
        Me.TabItems.Size = New System.Drawing.Size(646, 588)
        Me.TabItems.TabIndex = 3
        Me.TabItems.Text = "GUI Items"
        Me.TabItems.UseVisualStyleBackColor = True
        '
        'CheckBoxRemberSortedBy
        '
        Me.CheckBoxRemberSortedBy.AutoSize = True
        Me.CheckBoxRemberSortedBy.Location = New System.Drawing.Point(43, 133)
        Me.CheckBoxRemberSortedBy.Name = "CheckBoxRemberSortedBy"
        Me.CheckBoxRemberSortedBy.Size = New System.Drawing.Size(345, 20)
        Me.CheckBoxRemberSortedBy.TabIndex = 11
        Me.CheckBoxRemberSortedBy.Text = "merke zuletzt gewählte Sortierung in Kategorien"
        Me.CheckBoxRemberSortedBy.UseVisualStyleBackColor = True
        '
        'CheckBoxFilterShowLocalSeries
        '
        Me.CheckBoxFilterShowLocalSeries.AutoSize = True
        Me.CheckBoxFilterShowLocalSeries.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxFilterShowLocalSeries.Location = New System.Drawing.Point(43, 104)
        Me.CheckBoxFilterShowLocalSeries.Name = "CheckBoxFilterShowLocalSeries"
        Me.CheckBoxFilterShowLocalSeries.Size = New System.Drawing.Size(361, 20)
        Me.CheckBoxFilterShowLocalSeries.TabIndex = 10
        Me.CheckBoxFilterShowLocalSeries.Text = "Keine Serien (Episoden) zeigen, die lokal existieren"
        Me.CheckBoxFilterShowLocalSeries.UseVisualStyleBackColor = True
        '
        'CheckBoxFilterShowLocalMovies
        '
        Me.CheckBoxFilterShowLocalMovies.AutoSize = True
        Me.CheckBoxFilterShowLocalMovies.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxFilterShowLocalMovies.Location = New System.Drawing.Point(43, 74)
        Me.CheckBoxFilterShowLocalMovies.Name = "CheckBoxFilterShowLocalMovies"
        Me.CheckBoxFilterShowLocalMovies.Size = New System.Drawing.Size(278, 20)
        Me.CheckBoxFilterShowLocalMovies.TabIndex = 9
        Me.CheckBoxFilterShowLocalMovies.Text = "Keine Filme zeigen, die lokal existieren"
        Me.CheckBoxFilterShowLocalMovies.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(22, 20)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(543, 38)
        Me.Label14.TabIndex = 3
        Me.Label14.Text = "Mit den verschiedenen Filtereinstellungen könnt ihr die Suchergebnisse eingrenzen" & _
            " und die Zugrifsgeschwindigkeit steigern"
        '
        'TabÜbersicht
        '
        Me.TabÜbersicht.Controls.Add(Me.NumMaxDays)
        Me.TabÜbersicht.Controls.Add(Me.Label12)
        Me.TabÜbersicht.Controls.Add(Me.Label13)
        Me.TabÜbersicht.Controls.Add(Me.GroupBox1)
        Me.TabÜbersicht.Controls.Add(Me.GroupBox2)
        Me.TabÜbersicht.Location = New System.Drawing.Point(4, 25)
        Me.TabÜbersicht.Name = "TabÜbersicht"
        Me.TabÜbersicht.Padding = New System.Windows.Forms.Padding(3)
        Me.TabÜbersicht.Size = New System.Drawing.Size(646, 588)
        Me.TabÜbersicht.TabIndex = 1
        Me.TabÜbersicht.Text = "GUI Highlights"
        Me.TabÜbersicht.UseVisualStyleBackColor = True
        '
        'NumMaxDays
        '
        Me.NumMaxDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumMaxDays.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumMaxDays.Location = New System.Drawing.Point(209, 21)
        Me.NumMaxDays.Maximum = New Decimal(New Integer() {14, 0, 0, 0})
        Me.NumMaxDays.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumMaxDays.Name = "NumMaxDays"
        Me.NumMaxDays.Size = New System.Drawing.Size(42, 23)
        Me.NumMaxDays.TabIndex = 12
        Me.NumMaxDays.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(14, 23)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(195, 16)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "Zeige Übersicht für maximal "
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(257, 23)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(41, 16)
        Me.Label13.TabIndex = 14
        Me.Label13.Text = "Tage"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.NumHighlightsMinRuntime)
        Me.GroupBox1.Controls.Add(Me.NumShowHighlightsAfter)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(17, 252)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(612, 98)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "weitere Highlights"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(240, 68)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 16)
        Me.Label11.TabIndex = 11
        Me.Label11.Text = "Minuten"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(10, 68)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(163, 16)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "Ignoriere wenn Dauer <"
        '
        'NumHighlightsMinRuntime
        '
        Me.NumHighlightsMinRuntime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumHighlightsMinRuntime.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumHighlightsMinRuntime.Location = New System.Drawing.Point(192, 66)
        Me.NumHighlightsMinRuntime.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        Me.NumHighlightsMinRuntime.Name = "NumHighlightsMinRuntime"
        Me.NumHighlightsMinRuntime.Size = New System.Drawing.Size(42, 23)
        Me.NumHighlightsMinRuntime.TabIndex = 9
        '
        'NumShowHighlightsAfter
        '
        Me.NumShowHighlightsAfter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumShowHighlightsAfter.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumShowHighlightsAfter.Location = New System.Drawing.Point(192, 35)
        Me.NumShowHighlightsAfter.Maximum = New Decimal(New Integer() {23, 0, 0, 0})
        Me.NumShowHighlightsAfter.Name = "NumShowHighlightsAfter"
        Me.NumShowHighlightsAfter.Size = New System.Drawing.Size(42, 23)
        Me.NumShowHighlightsAfter.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(240, 37)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(30, 16)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Uhr"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(10, 37)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(164, 16)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Zeige Highlights erst ab"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Controls.Add(Me.CheckBoxShowTagesTipp)
        Me.GroupBox2.Controls.Add(Me.NumShowMoviesAfter)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.CheckBoxShowLocalMovies)
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(17, 60)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(612, 175)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Movie Highlights"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.RBstartTime)
        Me.GroupBox3.Controls.Add(Me.RBTvMovieStar)
        Me.GroupBox3.Controls.Add(Me.RBRatingStar)
        Me.GroupBox3.Location = New System.Drawing.Point(17, 112)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(335, 57)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "sortieren nach:"
        '
        'RBstartTime
        '
        Me.RBstartTime.AutoSize = True
        Me.RBstartTime.Location = New System.Drawing.Point(17, 25)
        Me.RBstartTime.Name = "RBstartTime"
        Me.RBstartTime.Size = New System.Drawing.Size(86, 20)
        Me.RBstartTime.TabIndex = 2
        Me.RBstartTime.TabStop = True
        Me.RBstartTime.Text = "StartZeit"
        Me.RBstartTime.UseVisualStyleBackColor = True
        '
        'RBTvMovieStar
        '
        Me.RBTvMovieStar.AutoSize = True
        Me.RBTvMovieStar.Location = New System.Drawing.Point(109, 25)
        Me.RBTvMovieStar.Name = "RBTvMovieStar"
        Me.RBTvMovieStar.Size = New System.Drawing.Size(114, 20)
        Me.RBTvMovieStar.TabIndex = 1
        Me.RBTvMovieStar.TabStop = True
        Me.RBTvMovieStar.Text = "TvMovie Star"
        Me.RBTvMovieStar.UseVisualStyleBackColor = True
        '
        'RBRatingStar
        '
        Me.RBRatingStar.AutoSize = True
        Me.RBRatingStar.Location = New System.Drawing.Point(229, 25)
        Me.RBRatingStar.Name = "RBRatingStar"
        Me.RBRatingStar.Size = New System.Drawing.Size(95, 20)
        Me.RBRatingStar.TabIndex = 0
        Me.RBRatingStar.TabStop = True
        Me.RBRatingStar.Text = "RatingStar"
        Me.RBRatingStar.UseVisualStyleBackColor = True
        '
        'CheckBoxShowTagesTipp
        '
        Me.CheckBoxShowTagesTipp.AutoSize = True
        Me.CheckBoxShowTagesTipp.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxShowTagesTipp.Location = New System.Drawing.Point(17, 54)
        Me.CheckBoxShowTagesTipp.Name = "CheckBoxShowTagesTipp"
        Me.CheckBoxShowTagesTipp.Size = New System.Drawing.Size(327, 20)
        Me.CheckBoxShowTagesTipp.TabIndex = 6
        Me.CheckBoxShowTagesTipp.Text = "Zeige Tv Movie Tages Tipp als ersten Eintrag"
        Me.CheckBoxShowTagesTipp.UseVisualStyleBackColor = True
        '
        'NumShowMoviesAfter
        '
        Me.NumShowMoviesAfter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumShowMoviesAfter.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumShowMoviesAfter.Location = New System.Drawing.Point(230, 85)
        Me.NumShowMoviesAfter.Maximum = New Decimal(New Integer() {23, 0, 0, 0})
        Me.NumShowMoviesAfter.Name = "NumShowMoviesAfter"
        Me.NumShowMoviesAfter.Size = New System.Drawing.Size(42, 23)
        Me.NumShowMoviesAfter.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 87)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(207, 16)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Zeige Movie Highlights erst ab"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(278, 87)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(30, 16)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Uhr"
        '
        'CheckBoxShowLocalMovies
        '
        Me.CheckBoxShowLocalMovies.AutoSize = True
        Me.CheckBoxShowLocalMovies.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxShowLocalMovies.Location = New System.Drawing.Point(17, 22)
        Me.CheckBoxShowLocalMovies.Name = "CheckBoxShowLocalMovies"
        Me.CheckBoxShowLocalMovies.Size = New System.Drawing.Size(278, 20)
        Me.CheckBoxShowLocalMovies.TabIndex = 0
        Me.CheckBoxShowLocalMovies.Text = "Keine Filme zeigen, die lokal existieren"
        Me.CheckBoxShowLocalMovies.UseVisualStyleBackColor = True
        '
        'TabCategories
        '
        Me.TabCategories.Controls.Add(Me.CheckBoxShowCategorieLocalSeries)
        Me.TabCategories.Controls.Add(Me.CheckBoxShowCategorieLocalMovies)
        Me.TabCategories.Controls.Add(Me.CheckBoxSelect)
        Me.TabCategories.Controls.Add(Me.ButtonCategoriesDefault)
        Me.TabCategories.Controls.Add(Me.ButtonNewCategorie)
        Me.TabCategories.Controls.Add(Me.dgvCategories)
        Me.TabCategories.Controls.Add(Me.ButtonDown)
        Me.TabCategories.Controls.Add(Me.ButtonUp)
        Me.TabCategories.Location = New System.Drawing.Point(4, 25)
        Me.TabCategories.Name = "TabCategories"
        Me.TabCategories.Size = New System.Drawing.Size(646, 588)
        Me.TabCategories.TabIndex = 2
        Me.TabCategories.Text = "GUI Kategorien"
        Me.TabCategories.UseVisualStyleBackColor = True
        '
        'CheckBoxShowCategorieLocalSeries
        '
        Me.CheckBoxShowCategorieLocalSeries.AutoSize = True
        Me.CheckBoxShowCategorieLocalSeries.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxShowCategorieLocalSeries.Location = New System.Drawing.Point(9, 471)
        Me.CheckBoxShowCategorieLocalSeries.Name = "CheckBoxShowCategorieLocalSeries"
        Me.CheckBoxShowCategorieLocalSeries.Size = New System.Drawing.Size(361, 20)
        Me.CheckBoxShowCategorieLocalSeries.TabIndex = 12
        Me.CheckBoxShowCategorieLocalSeries.Text = "Keine Serien (Episoden) zeigen, die lokal existieren"
        Me.CheckBoxShowCategorieLocalSeries.UseVisualStyleBackColor = True
        '
        'CheckBoxShowCategorieLocalMovies
        '
        Me.CheckBoxShowCategorieLocalMovies.AutoSize = True
        Me.CheckBoxShowCategorieLocalMovies.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxShowCategorieLocalMovies.Location = New System.Drawing.Point(9, 445)
        Me.CheckBoxShowCategorieLocalMovies.Name = "CheckBoxShowCategorieLocalMovies"
        Me.CheckBoxShowCategorieLocalMovies.Size = New System.Drawing.Size(278, 20)
        Me.CheckBoxShowCategorieLocalMovies.TabIndex = 11
        Me.CheckBoxShowCategorieLocalMovies.Text = "Keine Filme zeigen, die lokal existieren"
        Me.CheckBoxShowCategorieLocalMovies.UseVisualStyleBackColor = True
        '
        'CheckBoxSelect
        '
        Me.CheckBoxSelect.AutoSize = True
        Me.CheckBoxSelect.Location = New System.Drawing.Point(9, 389)
        Me.CheckBoxSelect.Name = "CheckBoxSelect"
        Me.CheckBoxSelect.Size = New System.Drawing.Size(160, 20)
        Me.CheckBoxSelect.TabIndex = 6
        Me.CheckBoxSelect.Text = "Select / deselect all"
        Me.CheckBoxSelect.UseVisualStyleBackColor = True
        '
        'ButtonCategoriesDefault
        '
        Me.ButtonCategoriesDefault.Location = New System.Drawing.Point(523, 390)
        Me.ButtonCategoriesDefault.Name = "ButtonCategoriesDefault"
        Me.ButtonCategoriesDefault.Size = New System.Drawing.Size(68, 28)
        Me.ButtonCategoriesDefault.TabIndex = 5
        Me.ButtonCategoriesDefault.Text = "Default"
        Me.ButtonCategoriesDefault.UseVisualStyleBackColor = True
        '
        'ButtonNewCategorie
        '
        Me.ButtonNewCategorie.Location = New System.Drawing.Point(449, 390)
        Me.ButtonNewCategorie.Name = "ButtonNewCategorie"
        Me.ButtonNewCategorie.Size = New System.Drawing.Size(68, 28)
        Me.ButtonNewCategorie.TabIndex = 2
        Me.ButtonNewCategorie.Text = "New"
        Me.ButtonNewCategorie.UseVisualStyleBackColor = True
        '
        'dgvCategories
        '
        Me.dgvCategories.AllowUserToAddRows = False
        Me.dgvCategories.AllowUserToDeleteRows = False
        Me.dgvCategories.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgvCategories.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.dgvCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCategories.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.C_id, Me.C_Image, Me.C_visible, Me.C_Name, Me.C_Beschreibung, Me.C_sortOrder, Me.C_SqlString, Me.C_MinRuntime, Me.C_NowOffset})
        Me.dgvCategories.Location = New System.Drawing.Point(9, 8)
        Me.dgvCategories.Name = "dgvCategories"
        Me.dgvCategories.RowHeadersVisible = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.dgvCategories.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvCategories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCategories.Size = New System.Drawing.Size(582, 376)
        Me.dgvCategories.TabIndex = 0
        '
        'C_id
        '
        Me.C_id.FillWeight = 50.0!
        Me.C_id.HeaderText = "id"
        Me.C_id.Name = "C_id"
        Me.C_id.ReadOnly = True
        Me.C_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.C_id.Visible = False
        '
        'C_Image
        '
        Me.C_Image.HeaderText = ""
        Me.C_Image.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.C_Image.Name = "C_Image"
        Me.C_Image.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.C_Image.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.C_Image.Width = 40
        '
        'C_visible
        '
        Me.C_visible.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.C_visible.HeaderText = ""
        Me.C_visible.Name = "C_visible"
        Me.C_visible.Width = 5
        '
        'C_Name
        '
        Me.C_Name.HeaderText = "Name"
        Me.C_Name.Name = "C_Name"
        Me.C_Name.ReadOnly = True
        Me.C_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.C_Name.Width = 200
        '
        'C_Beschreibung
        '
        Me.C_Beschreibung.HeaderText = "Beschreibung"
        Me.C_Beschreibung.Name = "C_Beschreibung"
        Me.C_Beschreibung.ReadOnly = True
        Me.C_Beschreibung.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.C_Beschreibung.Width = 88
        '
        'C_sortOrder
        '
        Me.C_sortOrder.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.C_sortOrder.HeaderText = "sortOrder"
        Me.C_sortOrder.Name = "C_sortOrder"
        Me.C_sortOrder.ReadOnly = True
        Me.C_sortOrder.Visible = False
        '
        'C_SqlString
        '
        Me.C_SqlString.HeaderText = "SqlString"
        Me.C_SqlString.Name = "C_SqlString"
        Me.C_SqlString.Visible = False
        '
        'C_MinRuntime
        '
        Me.C_MinRuntime.HeaderText = "Min"
        Me.C_MinRuntime.Name = "C_MinRuntime"
        Me.C_MinRuntime.Width = 40
        '
        'C_NowOffset
        '
        Me.C_NowOffset.HeaderText = "Offset"
        Me.C_NowOffset.Name = "C_NowOffset"
        Me.C_NowOffset.Width = 52
        '
        'ButtonDown
        '
        Me.ButtonDown.BackgroundImage = Global.ClickfinderProgramGuide.My.Resources.Resources.arrow_Down_48
        Me.ButtonDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ButtonDown.Location = New System.Drawing.Point(605, 206)
        Me.ButtonDown.Name = "ButtonDown"
        Me.ButtonDown.Size = New System.Drawing.Size(28, 61)
        Me.ButtonDown.TabIndex = 4
        Me.ButtonDown.UseVisualStyleBackColor = True
        '
        'ButtonUp
        '
        Me.ButtonUp.BackgroundImage = Global.ClickfinderProgramGuide.My.Resources.Resources.arrow_UP_48
        Me.ButtonUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ButtonUp.Location = New System.Drawing.Point(605, 139)
        Me.ButtonUp.Name = "ButtonUp"
        Me.ButtonUp.Size = New System.Drawing.Size(28, 61)
        Me.ButtonUp.TabIndex = 3
        Me.ButtonUp.UseVisualStyleBackColor = True
        '
        'TabDetail
        '
        Me.TabDetail.Controls.Add(Me.GroupDetailSeriesImage)
        Me.TabDetail.Controls.Add(Me.CheckBoxUseSeriesDescribtion)
        Me.TabDetail.Location = New System.Drawing.Point(4, 25)
        Me.TabDetail.Name = "TabDetail"
        Me.TabDetail.Padding = New System.Windows.Forms.Padding(3)
        Me.TabDetail.Size = New System.Drawing.Size(646, 588)
        Me.TabDetail.TabIndex = 4
        Me.TabDetail.Text = "GUI Details"
        Me.TabDetail.UseVisualStyleBackColor = True
        '
        'GroupDetailSeriesImage
        '
        Me.GroupDetailSeriesImage.Controls.Add(Me.RBTvMovieImage)
        Me.GroupDetailSeriesImage.Controls.Add(Me.RBEpisodeImage)
        Me.GroupDetailSeriesImage.Controls.Add(Me.RBSeriesFanArt)
        Me.GroupDetailSeriesImage.Controls.Add(Me.RBSeriesCover)
        Me.GroupDetailSeriesImage.Location = New System.Drawing.Point(22, 67)
        Me.GroupDetailSeriesImage.Name = "GroupDetailSeriesImage"
        Me.GroupDetailSeriesImage.Size = New System.Drawing.Size(603, 63)
        Me.GroupDetailSeriesImage.TabIndex = 11
        Me.GroupDetailSeriesImage.TabStop = False
        Me.GroupDetailSeriesImage.Text = "Serien Bild:"
        '
        'RBTvMovieImage
        '
        Me.RBTvMovieImage.AutoSize = True
        Me.RBTvMovieImage.Location = New System.Drawing.Point(456, 31)
        Me.RBTvMovieImage.Name = "RBTvMovieImage"
        Me.RBTvMovieImage.Size = New System.Drawing.Size(126, 20)
        Me.RBTvMovieImage.TabIndex = 15
        Me.RBTvMovieImage.TabStop = True
        Me.RBTvMovieImage.Text = "TvMovie Image"
        Me.RBTvMovieImage.UseVisualStyleBackColor = True
        '
        'RBEpisodeImage
        '
        Me.RBEpisodeImage.AutoSize = True
        Me.RBEpisodeImage.Location = New System.Drawing.Point(301, 31)
        Me.RBEpisodeImage.Name = "RBEpisodeImage"
        Me.RBEpisodeImage.Size = New System.Drawing.Size(129, 20)
        Me.RBEpisodeImage.TabIndex = 14
        Me.RBEpisodeImage.TabStop = True
        Me.RBEpisodeImage.Text = "Episoden Image"
        Me.RBEpisodeImage.UseVisualStyleBackColor = True
        '
        'RBSeriesFanArt
        '
        Me.RBSeriesFanArt.AccessibleDescription = "RBSeriesFanArt"
        Me.RBSeriesFanArt.AutoSize = True
        Me.RBSeriesFanArt.Location = New System.Drawing.Point(159, 31)
        Me.RBSeriesFanArt.Name = "RBSeriesFanArt"
        Me.RBSeriesFanArt.Size = New System.Drawing.Size(116, 20)
        Me.RBSeriesFanArt.TabIndex = 13
        Me.RBSeriesFanArt.TabStop = True
        Me.RBSeriesFanArt.Text = "Serien FanArt"
        Me.RBSeriesFanArt.UseVisualStyleBackColor = True
        '
        'RBSeriesCover
        '
        Me.RBSeriesCover.AccessibleDescription = "RBSeriesCover"
        Me.RBSeriesCover.AutoSize = True
        Me.RBSeriesCover.Location = New System.Drawing.Point(23, 31)
        Me.RBSeriesCover.Name = "RBSeriesCover"
        Me.RBSeriesCover.Size = New System.Drawing.Size(110, 20)
        Me.RBSeriesCover.TabIndex = 12
        Me.RBSeriesCover.TabStop = True
        Me.RBSeriesCover.Text = "Serien Cover"
        Me.RBSeriesCover.UseVisualStyleBackColor = True
        '
        'CheckBoxUseSeriesDescribtion
        '
        Me.CheckBoxUseSeriesDescribtion.AutoSize = True
        Me.CheckBoxUseSeriesDescribtion.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxUseSeriesDescribtion.Location = New System.Drawing.Point(24, 28)
        Me.CheckBoxUseSeriesDescribtion.Name = "CheckBoxUseSeriesDescribtion"
        Me.CheckBoxUseSeriesDescribtion.Size = New System.Drawing.Size(522, 20)
        Me.CheckBoxUseSeriesDescribtion.TabIndex = 10
        Me.CheckBoxUseSeriesDescribtion.Text = "Verwende MP-TvSeries Epsioden Beschreibung bei Serien (sofern aktiviert)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.CheckBoxUseSeriesDescribtion.UseVisualStyleBackColor = True
        '
        'TabOverlay
        '
        Me.TabOverlay.Controls.Add(Me.CheckBoxEnableSeriesOverlay)
        Me.TabOverlay.Controls.Add(Me.CheckBoxEnableMovieOverlay)
        Me.TabOverlay.Controls.Add(Me.GroupBoxMovieOverlay)
        Me.TabOverlay.Location = New System.Drawing.Point(4, 25)
        Me.TabOverlay.Name = "TabOverlay"
        Me.TabOverlay.Size = New System.Drawing.Size(646, 588)
        Me.TabOverlay.TabIndex = 5
        Me.TabOverlay.Text = "Overlay"
        Me.TabOverlay.UseVisualStyleBackColor = True
        '
        'CheckBoxEnableSeriesOverlay
        '
        Me.CheckBoxEnableSeriesOverlay.AutoSize = True
        Me.CheckBoxEnableSeriesOverlay.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxEnableSeriesOverlay.Location = New System.Drawing.Point(17, 362)
        Me.CheckBoxEnableSeriesOverlay.Name = "CheckBoxEnableSeriesOverlay"
        Me.CheckBoxEnableSeriesOverlay.Size = New System.Drawing.Size(247, 20)
        Me.CheckBoxEnableSeriesOverlay.TabIndex = 13
        Me.CheckBoxEnableSeriesOverlay.Text = "Neue Episoden Overlay aktivieren"
        Me.CheckBoxEnableSeriesOverlay.UseVisualStyleBackColor = True
        '
        'CheckBoxEnableMovieOverlay
        '
        Me.CheckBoxEnableMovieOverlay.AutoSize = True
        Me.CheckBoxEnableMovieOverlay.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxEnableMovieOverlay.Location = New System.Drawing.Point(17, 19)
        Me.CheckBoxEnableMovieOverlay.Name = "CheckBoxEnableMovieOverlay"
        Me.CheckBoxEnableMovieOverlay.Size = New System.Drawing.Size(184, 20)
        Me.CheckBoxEnableMovieOverlay.TabIndex = 12
        Me.CheckBoxEnableMovieOverlay.Text = "Filme Overlay aktivieren"
        Me.CheckBoxEnableMovieOverlay.UseVisualStyleBackColor = True
        '
        'GroupBoxMovieOverlay
        '
        Me.GroupBoxMovieOverlay.Controls.Add(Me.Label22)
        Me.GroupBoxMovieOverlay.Controls.Add(Me.CBOverlayGroup)
        Me.GroupBoxMovieOverlay.Controls.Add(Me.NumOverlayLimit)
        Me.GroupBoxMovieOverlay.Controls.Add(Me.Label21)
        Me.GroupBoxMovieOverlay.Controls.Add(Me.CheckBoxOverlayShowLocalMovies)
        Me.GroupBoxMovieOverlay.Controls.Add(Me.GroupBox7)
        Me.GroupBoxMovieOverlay.Controls.Add(Me.CheckBoxOverlayShowTagesTipp)
        Me.GroupBoxMovieOverlay.Controls.Add(Me.GroupBox6)
        Me.GroupBoxMovieOverlay.Location = New System.Drawing.Point(13, 48)
        Me.GroupBoxMovieOverlay.Name = "GroupBoxMovieOverlay"
        Me.GroupBoxMovieOverlay.Size = New System.Drawing.Size(620, 293)
        Me.GroupBoxMovieOverlay.TabIndex = 11
        Me.GroupBoxMovieOverlay.TabStop = False
        Me.GroupBoxMovieOverlay.Text = "Filme Overlay"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(19, 115)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(82, 16)
        Me.Label22.TabIndex = 26
        Me.Label22.Text = "Tv Gruppe:"
        '
        'CBOverlayGroup
        '
        Me.CBOverlayGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBOverlayGroup.FormattingEnabled = True
        Me.CBOverlayGroup.Location = New System.Drawing.Point(107, 112)
        Me.CBOverlayGroup.Name = "CBOverlayGroup"
        Me.CBOverlayGroup.Size = New System.Drawing.Size(191, 24)
        Me.CBOverlayGroup.TabIndex = 25
        '
        'NumOverlayLimit
        '
        Me.NumOverlayLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumOverlayLimit.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumOverlayLimit.Location = New System.Drawing.Point(374, 78)
        Me.NumOverlayLimit.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.NumOverlayLimit.Minimum = New Decimal(New Integer() {4, 0, 0, 0})
        Me.NumOverlayLimit.Name = "NumOverlayLimit"
        Me.NumOverlayLimit.Size = New System.Drawing.Size(42, 23)
        Me.NumOverlayLimit.TabIndex = 13
        Me.NumOverlayLimit.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(19, 80)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(349, 16)
        Me.Label21.TabIndex = 14
        Me.Label21.Text = "Limit (max. Einträge die durchsucht werden sollen):"
        '
        'CheckBoxOverlayShowLocalMovies
        '
        Me.CheckBoxOverlayShowLocalMovies.AutoSize = True
        Me.CheckBoxOverlayShowLocalMovies.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxOverlayShowLocalMovies.Location = New System.Drawing.Point(22, 24)
        Me.CheckBoxOverlayShowLocalMovies.Name = "CheckBoxOverlayShowLocalMovies"
        Me.CheckBoxOverlayShowLocalMovies.Size = New System.Drawing.Size(278, 20)
        Me.CheckBoxOverlayShowLocalMovies.TabIndex = 8
        Me.CheckBoxOverlayShowLocalMovies.Text = "Keine Filme zeigen, die lokal existieren"
        Me.CheckBoxOverlayShowLocalMovies.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.RBOverlayStartTime)
        Me.GroupBox7.Controls.Add(Me.RBOverlayTvMovieStar)
        Me.GroupBox7.Controls.Add(Me.RBOverlayRatingStar)
        Me.GroupBox7.Location = New System.Drawing.Point(22, 221)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(579, 57)
        Me.GroupBox7.TabIndex = 10
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "sortieren nach:"
        '
        'RBOverlayStartTime
        '
        Me.RBOverlayStartTime.AutoSize = True
        Me.RBOverlayStartTime.Location = New System.Drawing.Point(18, 25)
        Me.RBOverlayStartTime.Name = "RBOverlayStartTime"
        Me.RBOverlayStartTime.Size = New System.Drawing.Size(86, 20)
        Me.RBOverlayStartTime.TabIndex = 2
        Me.RBOverlayStartTime.TabStop = True
        Me.RBOverlayStartTime.Text = "StartZeit"
        Me.RBOverlayStartTime.UseVisualStyleBackColor = True
        '
        'RBOverlayTvMovieStar
        '
        Me.RBOverlayTvMovieStar.AutoSize = True
        Me.RBOverlayTvMovieStar.Location = New System.Drawing.Point(206, 25)
        Me.RBOverlayTvMovieStar.Name = "RBOverlayTvMovieStar"
        Me.RBOverlayTvMovieStar.Size = New System.Drawing.Size(114, 20)
        Me.RBOverlayTvMovieStar.TabIndex = 1
        Me.RBOverlayTvMovieStar.TabStop = True
        Me.RBOverlayTvMovieStar.Text = "TvMovie Star"
        Me.RBOverlayTvMovieStar.UseVisualStyleBackColor = True
        '
        'RBOverlayRatingStar
        '
        Me.RBOverlayRatingStar.AutoSize = True
        Me.RBOverlayRatingStar.Location = New System.Drawing.Point(422, 25)
        Me.RBOverlayRatingStar.Name = "RBOverlayRatingStar"
        Me.RBOverlayRatingStar.Size = New System.Drawing.Size(95, 20)
        Me.RBOverlayRatingStar.TabIndex = 0
        Me.RBOverlayRatingStar.TabStop = True
        Me.RBOverlayRatingStar.Text = "RatingStar"
        Me.RBOverlayRatingStar.UseVisualStyleBackColor = True
        '
        'CheckBoxOverlayShowTagesTipp
        '
        Me.CheckBoxOverlayShowTagesTipp.AutoSize = True
        Me.CheckBoxOverlayShowTagesTipp.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxOverlayShowTagesTipp.Location = New System.Drawing.Point(22, 50)
        Me.CheckBoxOverlayShowTagesTipp.Name = "CheckBoxOverlayShowTagesTipp"
        Me.CheckBoxOverlayShowTagesTipp.Size = New System.Drawing.Size(327, 20)
        Me.CheckBoxOverlayShowTagesTipp.TabIndex = 7
        Me.CheckBoxOverlayShowTagesTipp.Text = "Zeige Tv Movie Tages Tipp als ersten Eintrag"
        Me.CheckBoxOverlayShowTagesTipp.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.RBOverlayLateTime)
        Me.GroupBox6.Controls.Add(Me.RBOverlayPrimeTime)
        Me.GroupBox6.Controls.Add(Me.RBOverlayNow)
        Me.GroupBox6.Controls.Add(Me.RBOverlayHeute)
        Me.GroupBox6.Location = New System.Drawing.Point(22, 153)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(579, 62)
        Me.GroupBox6.TabIndex = 9
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Zeige Film Empfehlungen:"
        '
        'RBOverlayLateTime
        '
        Me.RBOverlayLateTime.AutoSize = True
        Me.RBOverlayLateTime.Location = New System.Drawing.Point(423, 27)
        Me.RBOverlayLateTime.Name = "RBOverlayLateTime"
        Me.RBOverlayLateTime.Size = New System.Drawing.Size(112, 20)
        Me.RBOverlayLateTime.TabIndex = 3
        Me.RBOverlayLateTime.TabStop = True
        Me.RBOverlayLateTime.Text = "ab Late Time"
        Me.RBOverlayLateTime.UseVisualStyleBackColor = True
        '
        'RBOverlayPrimeTime
        '
        Me.RBOverlayPrimeTime.AutoSize = True
        Me.RBOverlayPrimeTime.Location = New System.Drawing.Point(257, 27)
        Me.RBOverlayPrimeTime.Name = "RBOverlayPrimeTime"
        Me.RBOverlayPrimeTime.Size = New System.Drawing.Size(118, 20)
        Me.RBOverlayPrimeTime.TabIndex = 2
        Me.RBOverlayPrimeTime.TabStop = True
        Me.RBOverlayPrimeTime.Text = "ab Prime Time"
        Me.RBOverlayPrimeTime.UseVisualStyleBackColor = True
        '
        'RBOverlayNow
        '
        Me.RBOverlayNow.AutoSize = True
        Me.RBOverlayNow.Location = New System.Drawing.Point(131, 27)
        Me.RBOverlayNow.Name = "RBOverlayNow"
        Me.RBOverlayNow.Size = New System.Drawing.Size(78, 20)
        Me.RBOverlayNow.TabIndex = 1
        Me.RBOverlayNow.TabStop = True
        Me.RBOverlayNow.Text = "ab jetzt"
        Me.RBOverlayNow.UseVisualStyleBackColor = True
        '
        'RBOverlayHeute
        '
        Me.RBOverlayHeute.AutoSize = True
        Me.RBOverlayHeute.Location = New System.Drawing.Point(18, 27)
        Me.RBOverlayHeute.Name = "RBOverlayHeute"
        Me.RBOverlayHeute.Size = New System.Drawing.Size(65, 20)
        Me.RBOverlayHeute.TabIndex = 0
        Me.RBOverlayHeute.TabStop = True
        Me.RBOverlayHeute.Text = "Heute"
        Me.RBOverlayHeute.UseVisualStyleBackColor = True
        '
        'openFileDialog
        '
        Me.openFileDialog.FileName = "OpenFileDialog1"
        '
        'PictureBox2
        '
        Me.PictureBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox2.Image = Global.ClickfinderProgramGuide.My.Resources.Resources.btn_donate_LG1
        Me.PictureBox2.Location = New System.Drawing.Point(14, 696)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(98, 53)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 40
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.ClickfinderProgramGuide.My.Resources.Resources.SetupIcon
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox1.Location = New System.Drawing.Point(3, 4)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(75, 62)
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'NumPreviewDays
        '
        Me.NumPreviewDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumPreviewDays.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumPreviewDays.Location = New System.Drawing.Point(151, 22)
        Me.NumPreviewDays.Maximum = New Decimal(New Integer() {14, 0, 0, 0})
        Me.NumPreviewDays.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumPreviewDays.Name = "NumPreviewDays"
        Me.NumPreviewDays.Size = New System.Drawing.Size(42, 23)
        Me.NumPreviewDays.TabIndex = 35
        Me.NumPreviewDays.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(17, 24)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(128, 16)
        Me.Label24.TabIndex = 36
        Me.Label24.Text = "Zeige für maximal "
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(199, 24)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(41, 16)
        Me.Label25.TabIndex = 37
        Me.Label25.Text = "Tage"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(17, 55)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(190, 16)
        Me.Label26.TabIndex = 38
        Me.Label26.Text = "Minimal TvMovie Bewertung"
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.NumPreviewMinTvMovieRating)
        Me.GroupBox8.Controls.Add(Me.NumPreviewDays)
        Me.GroupBox8.Controls.Add(Me.Label24)
        Me.GroupBox8.Controls.Add(Me.Label26)
        Me.GroupBox8.Controls.Add(Me.Label25)
        Me.GroupBox8.Location = New System.Drawing.Point(42, 243)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(310, 83)
        Me.GroupBox8.TabIndex = 39
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Vorschau"
        '
        'NumPreviewMinTvMovieRating
        '
        Me.NumPreviewMinTvMovieRating.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumPreviewMinTvMovieRating.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumPreviewMinTvMovieRating.Location = New System.Drawing.Point(213, 53)
        Me.NumPreviewMinTvMovieRating.Maximum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.NumPreviewMinTvMovieRating.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumPreviewMinTvMovieRating.Name = "NumPreviewMinTvMovieRating"
        Me.NumPreviewMinTvMovieRating.Size = New System.Drawing.Size(42, 23)
        Me.NumPreviewMinTvMovieRating.TabIndex = 39
        Me.NumPreviewMinTvMovieRating.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Setup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(678, 751)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.ButtonSave)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Setup"
        Me.Text = "Clickfinder Program Guide Configuration"
        Me.TabControl1.ResumeLayout(False)
        Me.TabAllgemeines.ResumeLayout(False)
        Me.TabAllgemeines.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.TabItems.ResumeLayout(False)
        Me.TabItems.PerformLayout()
        Me.TabÜbersicht.ResumeLayout(False)
        Me.TabÜbersicht.PerformLayout()
        CType(Me.NumMaxDays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.NumHighlightsMinRuntime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumShowHighlightsAfter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.NumShowMoviesAfter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabCategories.ResumeLayout(False)
        Me.TabCategories.PerformLayout()
        CType(Me.dgvCategories, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabDetail.ResumeLayout(False)
        Me.TabDetail.PerformLayout()
        Me.GroupDetailSeriesImage.ResumeLayout(False)
        Me.GroupDetailSeriesImage.PerformLayout()
        Me.TabOverlay.ResumeLayout(False)
        Me.TabOverlay.PerformLayout()
        Me.GroupBoxMovieOverlay.ResumeLayout(False)
        Me.GroupBoxMovieOverlay.PerformLayout()
        CType(Me.NumOverlayLimit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumPreviewDays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        CType(Me.NumPreviewMinTvMovieRating, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonSave As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabAllgemeines As System.Windows.Forms.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabÜbersicht As System.Windows.Forms.TabPage
    Friend WithEvents tbClickfinderDatabase As System.Windows.Forms.TextBox
    Friend WithEvents tbClickfinderImagePath As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ButtonOpenDlgImageFolder As System.Windows.Forms.Button
    Friend WithEvents ButtonOpenDlgDatabase As System.Windows.Forms.Button
    Friend WithEvents openFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents CheckBoxShowLocalMovies As System.Windows.Forms.CheckBox
    Friend WithEvents NumShowMoviesAfter As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents NumShowHighlightsAfter As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBoxShowTagesTipp As System.Windows.Forms.CheckBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents NumHighlightsMinRuntime As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumMaxDays As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxUseSportLogos As System.Windows.Forms.CheckBox
    Friend WithEvents TabCategories As System.Windows.Forms.TabPage
    Friend WithEvents dgvCategories As System.Windows.Forms.DataGridView
    Friend WithEvents ButtonNewCategorie As System.Windows.Forms.Button
    Friend WithEvents ButtonDown As System.Windows.Forms.Button
    Friend WithEvents ButtonUp As System.Windows.Forms.Button
    Friend WithEvents ButtonCategoriesDefault As System.Windows.Forms.Button
    Friend WithEvents CheckBoxSelect As System.Windows.Forms.CheckBox
    Friend WithEvents TabItems As System.Windows.Forms.TabPage
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxFilterShowLocalMovies As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxFilterShowLocalSeries As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents RBstartTime As System.Windows.Forms.RadioButton
    Friend WithEvents RBTvMovieStar As System.Windows.Forms.RadioButton
    Friend WithEvents RBRatingStar As System.Windows.Forms.RadioButton
    Friend WithEvents CheckBoxRemberSortedBy As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxDebugMode As System.Windows.Forms.CheckBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents tbLateTime As System.Windows.Forms.MaskedTextBox
    Friend WithEvents tbPrimeTime As System.Windows.Forms.MaskedTextBox
    Friend WithEvents CheckBoxShowCategorieLocalSeries As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxShowCategorieLocalMovies As System.Windows.Forms.CheckBox
    Friend WithEvents C_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_Image As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents C_visible As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents C_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_Beschreibung As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_sortOrder As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_SqlString As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_MinRuntime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_NowOffset As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Private WithEvents CheckBoxClickfinderPG As MediaPortal.UserInterface.Controls.MPCheckBox
    Private WithEvents CheckBoxVideoDB As MediaPortal.UserInterface.Controls.MPCheckBox
    Private WithEvents CheckBoxMovingPictures As MediaPortal.UserInterface.Controls.MPCheckBox
    Private WithEvents CheckBoxTvSeries As MediaPortal.UserInterface.Controls.MPCheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TabDetail As System.Windows.Forms.TabPage
    Friend WithEvents CheckBoxUseSeriesDescribtion As System.Windows.Forms.CheckBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cbStandardGroup As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents CbQuick2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents CbQuick1 As System.Windows.Forms.ComboBox
    Friend WithEvents tbPluginName As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents tbMPDatabasePath As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupDetailSeriesImage As System.Windows.Forms.GroupBox
    Friend WithEvents RBTvMovieImage As System.Windows.Forms.RadioButton
    Friend WithEvents RBEpisodeImage As System.Windows.Forms.RadioButton
    Friend WithEvents RBSeriesFanArt As System.Windows.Forms.RadioButton
    Friend WithEvents RBSeriesCover As System.Windows.Forms.RadioButton
    Friend WithEvents TabOverlay As System.Windows.Forms.TabPage
    Friend WithEvents CheckBoxOverlayShowTagesTipp As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxOverlayShowLocalMovies As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents RBOverlayLateTime As System.Windows.Forms.RadioButton
    Friend WithEvents RBOverlayPrimeTime As System.Windows.Forms.RadioButton
    Friend WithEvents RBOverlayNow As System.Windows.Forms.RadioButton
    Friend WithEvents RBOverlayHeute As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents RBOverlayStartTime As System.Windows.Forms.RadioButton
    Friend WithEvents RBOverlayTvMovieStar As System.Windows.Forms.RadioButton
    Friend WithEvents RBOverlayRatingStar As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBoxMovieOverlay As System.Windows.Forms.GroupBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents NumOverlayLimit As System.Windows.Forms.NumericUpDown
    Friend WithEvents CBOverlayGroup As System.Windows.Forms.ComboBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxEnableMovieOverlay As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxEnableSeriesOverlay As System.Windows.Forms.CheckBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents CBStartGui As System.Windows.Forms.ComboBox
    Friend WithEvents NumPreviewDays As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents NumPreviewMinTvMovieRating As System.Windows.Forms.NumericUpDown
End Class
