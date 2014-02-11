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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Setup))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ButtonSave = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabAllgemeines = New System.Windows.Forms.TabPage()
        Me.Panel31 = New System.Windows.Forms.Panel()
        Me.ButtonDefaultSettings = New System.Windows.Forms.Button()
        Me.CheckBoxDebugMode = New System.Windows.Forms.CheckBox()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Panel37 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel36 = New System.Windows.Forms.Panel()
        Me.CheckBoxUseTheTvDb = New MediaPortal.UserInterface.Controls.MPCheckBox()
        Me.CheckBoxVideoDB = New MediaPortal.UserInterface.Controls.MPCheckBox()
        Me.CheckBoxMovingPictures = New MediaPortal.UserInterface.Controls.MPCheckBox()
        Me.CheckBoxTvSeries = New MediaPortal.UserInterface.Controls.MPCheckBox()
        Me.CheckBoxClickfinderPG = New MediaPortal.UserInterface.Controls.MPCheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.tbMPDatabasePath = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.CbQuick2 = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.CbQuick1 = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cbStandardGroup = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.CheckBoxUseSportLogos = New System.Windows.Forms.CheckBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.NumPreviewMinTvMovieRating = New System.Windows.Forms.NumericUpDown()
        Me.NumPreviewDays = New System.Windows.Forms.NumericUpDown()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.tbLateTime = New System.Windows.Forms.MaskedTextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.tbPrimeTime = New System.Windows.Forms.MaskedTextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.CBStartGui = New System.Windows.Forms.ComboBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Panel29 = New System.Windows.Forms.Panel()
        Me.tbEpisodenScanner = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.ButtonOpenDlgEpisodenScanner = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.tbClickfinderImagePath = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ButtonOpenDlgImageFolder = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.tbClickfinderDatabase = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ButtonOpenDlgDatabase = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.tbPluginName = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.TabItems = New System.Windows.Forms.TabPage()
        Me.Panel33 = New System.Windows.Forms.Panel()
        Me.Panel32 = New System.Windows.Forms.Panel()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.Panel34 = New System.Windows.Forms.Panel()
        Me.TableItemsPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.PictureBox12 = New System.Windows.Forms.PictureBox()
        Me.CBTvGroup0 = New System.Windows.Forms.ComboBox()
        Me.CBsort0 = New System.Windows.Forms.ComboBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.CBTvGroup9 = New System.Windows.Forms.ComboBox()
        Me.CBsort9 = New System.Windows.Forms.ComboBox()
        Me.CBTvGroup8 = New System.Windows.Forms.ComboBox()
        Me.CBsort8 = New System.Windows.Forms.ComboBox()
        Me.CBsort7 = New System.Windows.Forms.ComboBox()
        Me.CBTvGroup7 = New System.Windows.Forms.ComboBox()
        Me.CBsort6 = New System.Windows.Forms.ComboBox()
        Me.CBTvGroup6 = New System.Windows.Forms.ComboBox()
        Me.CBsort5 = New System.Windows.Forms.ComboBox()
        Me.CBTvGroup5 = New System.Windows.Forms.ComboBox()
        Me.CBsort4 = New System.Windows.Forms.ComboBox()
        Me.CBTvGroup4 = New System.Windows.Forms.ComboBox()
        Me.CBsort3 = New System.Windows.Forms.ComboBox()
        Me.CBTvGroup3 = New System.Windows.Forms.ComboBox()
        Me.CBsort2 = New System.Windows.Forms.ComboBox()
        Me.CBTvGroup2 = New System.Windows.Forms.ComboBox()
        Me.CBsort1 = New System.Windows.Forms.ComboBox()
        Me.CBTvGroup1 = New System.Windows.Forms.ComboBox()
        Me.PictureBox11 = New System.Windows.Forms.PictureBox()
        Me.PictureBox10 = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.BTClearSorting = New System.Windows.Forms.Button()
        Me.BTsetToStandardTvGroup = New System.Windows.Forms.Button()
        Me.CheckBoxRemberSortedBy = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFilterShowLocalSeries = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFilterShowLocalMovies = New System.Windows.Forms.CheckBox()
        Me.TabÜbersicht = New System.Windows.Forms.TabPage()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel20 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.NumHighlightsMinRuntime = New System.Windows.Forms.NumericUpDown()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.NumShowHighlightsAfter = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.RBstartTime = New System.Windows.Forms.RadioButton()
        Me.RBTvMovieStar = New System.Windows.Forms.RadioButton()
        Me.RBRatingStar = New System.Windows.Forms.RadioButton()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.NumShowMoviesAfter = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CheckBoxShowTagesTipp = New System.Windows.Forms.CheckBox()
        Me.CheckBoxShowLocalMovies = New System.Windows.Forms.CheckBox()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.NumMaxDays = New System.Windows.Forms.NumericUpDown()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TabCategories = New System.Windows.Forms.TabPage()
        Me.CheckBoxShowCategorieLocalSeries = New System.Windows.Forms.CheckBox()
        Me.CheckBoxShowCategorieLocalMovies = New System.Windows.Forms.CheckBox()
        Me.Panel27 = New System.Windows.Forms.Panel()
        Me.ButtonNewCategorie = New System.Windows.Forms.Button()
        Me.CheckBoxSelect = New System.Windows.Forms.CheckBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.dgvCategories = New System.Windows.Forms.DataGridView()
        Me.C_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C_Image = New System.Windows.Forms.DataGridViewImageColumn()
        Me.C_visible = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.C_Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C_Beschreibung = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C_sortOrder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C_SqlString = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C_MinRuntime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C_NowOffset = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel28 = New System.Windows.Forms.Panel()
        Me.ButtonDown = New System.Windows.Forms.Button()
        Me.ButtonUp = New System.Windows.Forms.Button()
        Me.TabDetail = New System.Windows.Forms.TabPage()
        Me.Panel21 = New System.Windows.Forms.Panel()
        Me.GroupDetailSeriesImage = New System.Windows.Forms.GroupBox()
        Me.RBTvMovieImage = New System.Windows.Forms.RadioButton()
        Me.RBEpisodeImage = New System.Windows.Forms.RadioButton()
        Me.RBSeriesFanArt = New System.Windows.Forms.RadioButton()
        Me.RBSeriesCover = New System.Windows.Forms.RadioButton()
        Me.CheckBoxUseSeriesDescribtion = New System.Windows.Forms.CheckBox()
        Me.TabOverlay = New System.Windows.Forms.TabPage()
        Me.Panel22 = New System.Windows.Forms.Panel()
        Me.GroupBoxMovieOverlay = New System.Windows.Forms.GroupBox()
        Me.Panel26 = New System.Windows.Forms.Panel()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.RBOverlayStartTime = New System.Windows.Forms.RadioButton()
        Me.RBOverlayTvMovieStar = New System.Windows.Forms.RadioButton()
        Me.RBOverlayRatingStar = New System.Windows.Forms.RadioButton()
        Me.Panel25 = New System.Windows.Forms.Panel()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.RBOverlayLateTime = New System.Windows.Forms.RadioButton()
        Me.RBOverlayPrimeTime = New System.Windows.Forms.RadioButton()
        Me.RBOverlayNow = New System.Windows.Forms.RadioButton()
        Me.RBOverlayHeute = New System.Windows.Forms.RadioButton()
        Me.Panel24 = New System.Windows.Forms.Panel()
        Me.CBOverlayGroup = New System.Windows.Forms.ComboBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Panel23 = New System.Windows.Forms.Panel()
        Me.NumOverlayLimit = New System.Windows.Forms.NumericUpDown()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.CheckBoxOverlayShowLocalMovies = New System.Windows.Forms.CheckBox()
        Me.CheckBoxOverlayShowTagesTipp = New System.Windows.Forms.CheckBox()
        Me.Panel30 = New System.Windows.Forms.Panel()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.NumUpdateOverlay = New System.Windows.Forms.NumericUpDown()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.CheckBoxEnableSeriesOverlay = New System.Windows.Forms.CheckBox()
        Me.CheckBoxEnableMovieOverlay = New System.Windows.Forms.CheckBox()
        Me.openFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.LabelVersion = New System.Windows.Forms.Label()
        Me.Panel35 = New System.Windows.Forms.Panel()
        Me.TabControl1.SuspendLayout()
        Me.TabAllgemeines.SuspendLayout()
        Me.Panel31.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.Panel37.SuspendLayout()
        Me.Panel36.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        CType(Me.NumPreviewMinTvMovieRating, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumPreviewDays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel10.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel29.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TabItems.SuspendLayout()
        Me.Panel33.SuspendLayout()
        Me.Panel32.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.Panel34.SuspendLayout()
        Me.TableItemsPanel.SuspendLayout()
        CType(Me.PictureBox12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabÜbersicht.SuspendLayout()
        Me.Panel18.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel20.SuspendLayout()
        CType(Me.NumHighlightsMinRuntime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel19.SuspendLayout()
        CType(Me.NumShowHighlightsAfter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel15.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel17.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel16.SuspendLayout()
        CType(Me.NumShowMoviesAfter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel14.SuspendLayout()
        CType(Me.NumMaxDays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabCategories.SuspendLayout()
        Me.Panel27.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dgvCategories, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel28.SuspendLayout()
        Me.TabDetail.SuspendLayout()
        Me.Panel21.SuspendLayout()
        Me.GroupDetailSeriesImage.SuspendLayout()
        Me.TabOverlay.SuspendLayout()
        Me.Panel22.SuspendLayout()
        Me.GroupBoxMovieOverlay.SuspendLayout()
        Me.Panel26.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.Panel25.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.Panel24.SuspendLayout()
        Me.Panel23.SuspendLayout()
        CType(Me.NumOverlayLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel30.SuspendLayout()
        CType(Me.NumUpdateOverlay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel35.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonSave
        '
        Me.ButtonSave.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ButtonSave.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSave.Location = New System.Drawing.Point(0, 829)
        Me.ButtonSave.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(813, 39)
        Me.ButtonSave.TabIndex = 8
        Me.ButtonSave.Text = "speichern"
        Me.ButtonSave.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Verdana", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(75, 0)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(10, 10, 0, 0)
        Me.Label2.Size = New System.Drawing.Size(496, 35)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Clickfinder Program Guide Configuration"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.LinkLabel1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.Location = New System.Drawing.Point(94, 2)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.LinkLabel1.Size = New System.Drawing.Size(231, 25)
        Me.LinkLabel1.TabIndex = 38
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Tag = ""
        Me.LinkLabel1.Text = "Ausführliche Anleitung im Wiki"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabAllgemeines)
        Me.TabControl1.Controls.Add(Me.TabItems)
        Me.TabControl1.Controls.Add(Me.TabÜbersicht)
        Me.TabControl1.Controls.Add(Me.TabCategories)
        Me.TabControl1.Controls.Add(Me.TabDetail)
        Me.TabControl1.Controls.Add(Me.TabOverlay)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(10, 10)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(793, 731)
        Me.TabControl1.TabIndex = 39
        '
        'TabAllgemeines
        '
        Me.TabAllgemeines.AutoScroll = True
        Me.TabAllgemeines.Controls.Add(Me.Panel31)
        Me.TabAllgemeines.Controls.Add(Me.Panel13)
        Me.TabAllgemeines.Controls.Add(Me.Panel12)
        Me.TabAllgemeines.Controls.Add(Me.Panel11)
        Me.TabAllgemeines.Controls.Add(Me.Panel29)
        Me.TabAllgemeines.Controls.Add(Me.Panel5)
        Me.TabAllgemeines.Controls.Add(Me.Panel4)
        Me.TabAllgemeines.Controls.Add(Me.Panel3)
        Me.TabAllgemeines.Location = New System.Drawing.Point(4, 25)
        Me.TabAllgemeines.Name = "TabAllgemeines"
        Me.TabAllgemeines.Padding = New System.Windows.Forms.Padding(3)
        Me.TabAllgemeines.Size = New System.Drawing.Size(785, 702)
        Me.TabAllgemeines.TabIndex = 0
        Me.TabAllgemeines.Text = "Allgemeines"
        Me.TabAllgemeines.UseVisualStyleBackColor = True
        '
        'Panel31
        '
        Me.Panel31.Controls.Add(Me.ButtonDefaultSettings)
        Me.Panel31.Controls.Add(Me.CheckBoxDebugMode)
        Me.Panel31.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel31.Location = New System.Drawing.Point(3, 669)
        Me.Panel31.Name = "Panel31"
        Me.Panel31.Size = New System.Drawing.Size(779, 23)
        Me.Panel31.TabIndex = 81
        '
        'ButtonDefaultSettings
        '
        Me.ButtonDefaultSettings.Dock = System.Windows.Forms.DockStyle.Left
        Me.ButtonDefaultSettings.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonDefaultSettings.Location = New System.Drawing.Point(0, 0)
        Me.ButtonDefaultSettings.Name = "ButtonDefaultSettings"
        Me.ButtonDefaultSettings.Size = New System.Drawing.Size(211, 23)
        Me.ButtonDefaultSettings.TabIndex = 44
        Me.ButtonDefaultSettings.Text = "alle Einstellungen zurücksetzen"
        Me.ButtonDefaultSettings.UseVisualStyleBackColor = True
        '
        'CheckBoxDebugMode
        '
        Me.CheckBoxDebugMode.AutoSize = True
        Me.CheckBoxDebugMode.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxDebugMode.Dock = System.Windows.Forms.DockStyle.Right
        Me.CheckBoxDebugMode.Location = New System.Drawing.Point(581, 0)
        Me.CheckBoxDebugMode.Name = "CheckBoxDebugMode"
        Me.CheckBoxDebugMode.Padding = New System.Windows.Forms.Padding(95, 0, 0, 0)
        Me.CheckBoxDebugMode.Size = New System.Drawing.Size(198, 23)
        Me.CheckBoxDebugMode.TabIndex = 7
        Me.CheckBoxDebugMode.Text = "DebugMode"
        Me.CheckBoxDebugMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxDebugMode.UseVisualStyleBackColor = True
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.GroupBox4)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel13.Location = New System.Drawing.Point(3, 527)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Padding = New System.Windows.Forms.Padding(10, 5, 10, 10)
        Me.Panel13.Size = New System.Drawing.Size(779, 142)
        Me.Panel13.TabIndex = 79
        '
        'GroupBox4
        '
        Me.GroupBox4.AutoSize = True
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.Controls.Add(Me.Panel37)
        Me.GroupBox4.Controls.Add(Me.Panel36)
        Me.GroupBox4.Controls.Add(Me.CheckBoxClickfinderPG)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox4.Location = New System.Drawing.Point(10, 5)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(10)
        Me.GroupBox4.Size = New System.Drawing.Size(759, 127)
        Me.GroupBox4.TabIndex = 12
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Tv Movie EPG Import++ settings (zur Info)"
        '
        'Panel37
        '
        Me.Panel37.Controls.Add(Me.Button1)
        Me.Panel37.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel37.Location = New System.Drawing.Point(10, 98)
        Me.Panel37.Name = "Panel37"
        Me.Panel37.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.Panel37.Size = New System.Drawing.Size(739, 23)
        Me.Panel37.TabIndex = 80
        '
        'Button1
        '
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button1.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Button1.Location = New System.Drawing.Point(521, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(218, 21)
        Me.Button1.TabIndex = 43
        Me.Button1.Text = "Mapping Management"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Panel36
        '
        Me.Panel36.Controls.Add(Me.CheckBoxUseTheTvDb)
        Me.Panel36.Controls.Add(Me.CheckBoxVideoDB)
        Me.Panel36.Controls.Add(Me.CheckBoxMovingPictures)
        Me.Panel36.Controls.Add(Me.CheckBoxTvSeries)
        Me.Panel36.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel36.Location = New System.Drawing.Point(10, 74)
        Me.Panel36.Name = "Panel36"
        Me.Panel36.Size = New System.Drawing.Size(739, 24)
        Me.Panel36.TabIndex = 79
        '
        'CheckBoxUseTheTvDb
        '
        Me.CheckBoxUseTheTvDb.AutoSize = True
        Me.CheckBoxUseTheTvDb.Dock = System.Windows.Forms.DockStyle.Left
        Me.CheckBoxUseTheTvDb.Enabled = False
        Me.CheckBoxUseTheTvDb.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.CheckBoxUseTheTvDb.Location = New System.Drawing.Point(532, 0)
        Me.CheckBoxUseTheTvDb.Name = "CheckBoxUseTheTvDb"
        Me.CheckBoxUseTheTvDb.Padding = New System.Windows.Forms.Padding(10, 3, 3, 3)
        Me.CheckBoxUseTheTvDb.Size = New System.Drawing.Size(120, 24)
        Me.CheckBoxUseTheTvDb.TabIndex = 78
        Me.CheckBoxUseTheTvDb.Text = "useTheTvDb"
        Me.CheckBoxUseTheTvDb.UseVisualStyleBackColor = True
        '
        'CheckBoxVideoDB
        '
        Me.CheckBoxVideoDB.AutoSize = True
        Me.CheckBoxVideoDB.Dock = System.Windows.Forms.DockStyle.Left
        Me.CheckBoxVideoDB.Enabled = False
        Me.CheckBoxVideoDB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.CheckBoxVideoDB.Location = New System.Drawing.Point(350, 0)
        Me.CheckBoxVideoDB.Name = "CheckBoxVideoDB"
        Me.CheckBoxVideoDB.Padding = New System.Windows.Forms.Padding(10, 3, 3, 3)
        Me.CheckBoxVideoDB.Size = New System.Drawing.Size(182, 24)
        Me.CheckBoxVideoDB.TabIndex = 75
        Me.CheckBoxVideoDB.Text = "VideoDatabase import"
        Me.CheckBoxVideoDB.UseVisualStyleBackColor = True
        '
        'CheckBoxMovingPictures
        '
        Me.CheckBoxMovingPictures.AutoSize = True
        Me.CheckBoxMovingPictures.Dock = System.Windows.Forms.DockStyle.Left
        Me.CheckBoxMovingPictures.Enabled = False
        Me.CheckBoxMovingPictures.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.CheckBoxMovingPictures.Location = New System.Drawing.Point(167, 0)
        Me.CheckBoxMovingPictures.Name = "CheckBoxMovingPictures"
        Me.CheckBoxMovingPictures.Padding = New System.Windows.Forms.Padding(10, 3, 3, 3)
        Me.CheckBoxMovingPictures.Size = New System.Drawing.Size(183, 24)
        Me.CheckBoxMovingPictures.TabIndex = 74
        Me.CheckBoxMovingPictures.Text = "MovingPictures import"
        Me.CheckBoxMovingPictures.UseVisualStyleBackColor = True
        '
        'CheckBoxTvSeries
        '
        Me.CheckBoxTvSeries.AutoSize = True
        Me.CheckBoxTvSeries.Dock = System.Windows.Forms.DockStyle.Left
        Me.CheckBoxTvSeries.Enabled = False
        Me.CheckBoxTvSeries.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.CheckBoxTvSeries.Location = New System.Drawing.Point(0, 0)
        Me.CheckBoxTvSeries.Name = "CheckBoxTvSeries"
        Me.CheckBoxTvSeries.Padding = New System.Windows.Forms.Padding(10, 3, 3, 3)
        Me.CheckBoxTvSeries.Size = New System.Drawing.Size(167, 24)
        Me.CheckBoxTvSeries.TabIndex = 73
        Me.CheckBoxTvSeries.Text = "MP-TvSeries import"
        Me.CheckBoxTvSeries.UseVisualStyleBackColor = True
        '
        'CheckBoxClickfinderPG
        '
        Me.CheckBoxClickfinderPG.AutoSize = True
        Me.CheckBoxClickfinderPG.Dock = System.Windows.Forms.DockStyle.Top
        Me.CheckBoxClickfinderPG.Enabled = False
        Me.CheckBoxClickfinderPG.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.CheckBoxClickfinderPG.Location = New System.Drawing.Point(10, 48)
        Me.CheckBoxClickfinderPG.Name = "CheckBoxClickfinderPG"
        Me.CheckBoxClickfinderPG.Padding = New System.Windows.Forms.Padding(10, 3, 3, 3)
        Me.CheckBoxClickfinderPG.Size = New System.Drawing.Size(739, 26)
        Me.CheckBoxClickfinderPG.TabIndex = 76
        Me.CheckBoxClickfinderPG.Text = "Clickfinder ProgramGuide import"
        Me.CheckBoxClickfinderPG.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Location = New System.Drawing.Point(10, 26)
        Me.Label9.Name = "Label9"
        Me.Label9.Padding = New System.Windows.Forms.Padding(3)
        Me.Label9.Size = New System.Drawing.Size(555, 22)
        Me.Label9.TabIndex = 77
        Me.Label9.Text = "Diese Einstellung müssen im Tv Movie EPG Import++ Plugin vorgenommen werden."
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.GroupBox5)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel12.Location = New System.Drawing.Point(3, 428)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Padding = New System.Windows.Forms.Padding(10, 10, 10, 5)
        Me.Panel12.Size = New System.Drawing.Size(779, 99)
        Me.Panel12.TabIndex = 78
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.tbMPDatabasePath)
        Me.GroupBox5.Controls.Add(Me.Label6)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox5.Location = New System.Drawing.Point(10, 10)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(10)
        Me.GroupBox5.Size = New System.Drawing.Size(759, 84)
        Me.GroupBox5.TabIndex = 32
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "MediaPortal database path (zur Info)"
        '
        'tbMPDatabasePath
        '
        Me.tbMPDatabasePath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbMPDatabasePath.Enabled = False
        Me.tbMPDatabasePath.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbMPDatabasePath.Location = New System.Drawing.Point(10, 50)
        Me.tbMPDatabasePath.Name = "tbMPDatabasePath"
        Me.tbMPDatabasePath.Size = New System.Drawing.Size(739, 23)
        Me.tbMPDatabasePath.TabIndex = 79
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Location = New System.Drawing.Point(10, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Padding = New System.Windows.Forms.Padding(0, 0, 0, 8)
        Me.Label6.Size = New System.Drawing.Size(433, 24)
        Me.Label6.TabIndex = 78
        Me.Label6.Text = "MediaPortal Datenbank Pfad aus der lokalen MediaPortalDirs.xml "
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.Panel6)
        Me.Panel11.Controls.Add(Me.Panel7)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Location = New System.Drawing.Point(3, 170)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(779, 258)
        Me.Panel11.TabIndex = 78
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.CbQuick2)
        Me.Panel6.Controls.Add(Me.Label19)
        Me.Panel6.Controls.Add(Me.CbQuick1)
        Me.Panel6.Controls.Add(Me.Label18)
        Me.Panel6.Controls.Add(Me.cbStandardGroup)
        Me.Panel6.Controls.Add(Me.Label15)
        Me.Panel6.Controls.Add(Me.CheckBoxUseSportLogos)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(391, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel6.Size = New System.Drawing.Size(388, 258)
        Me.Panel6.TabIndex = 43
        '
        'CbQuick2
        '
        Me.CbQuick2.Dock = System.Windows.Forms.DockStyle.Top
        Me.CbQuick2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbQuick2.FormattingEnabled = True
        Me.CbQuick2.Location = New System.Drawing.Point(10, 178)
        Me.CbQuick2.Name = "CbQuick2"
        Me.CbQuick2.Size = New System.Drawing.Size(368, 24)
        Me.CbQuick2.TabIndex = 28
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label19.Location = New System.Drawing.Point(10, 148)
        Me.Label19.Name = "Label19"
        Me.Label19.Padding = New System.Windows.Forms.Padding(0, 10, 0, 4)
        Me.Label19.Size = New System.Drawing.Size(174, 30)
        Me.Label19.TabIndex = 29
        Me.Label19.Text = "Quick Tv Gruppe Filter 2:"
        '
        'CbQuick1
        '
        Me.CbQuick1.Dock = System.Windows.Forms.DockStyle.Top
        Me.CbQuick1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbQuick1.FormattingEnabled = True
        Me.CbQuick1.Location = New System.Drawing.Point(10, 124)
        Me.CbQuick1.Name = "CbQuick1"
        Me.CbQuick1.Size = New System.Drawing.Size(368, 24)
        Me.CbQuick1.TabIndex = 26
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label18.Location = New System.Drawing.Point(10, 94)
        Me.Label18.Name = "Label18"
        Me.Label18.Padding = New System.Windows.Forms.Padding(0, 10, 0, 4)
        Me.Label18.Size = New System.Drawing.Size(174, 30)
        Me.Label18.TabIndex = 27
        Me.Label18.Text = "Quick Tv Gruppe Filter 1:"
        '
        'cbStandardGroup
        '
        Me.cbStandardGroup.Dock = System.Windows.Forms.DockStyle.Top
        Me.cbStandardGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbStandardGroup.FormattingEnabled = True
        Me.cbStandardGroup.Location = New System.Drawing.Point(10, 70)
        Me.cbStandardGroup.Name = "cbStandardGroup"
        Me.cbStandardGroup.Size = New System.Drawing.Size(368, 24)
        Me.cbStandardGroup.TabIndex = 24
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Location = New System.Drawing.Point(10, 40)
        Me.Label15.Name = "Label15"
        Me.Label15.Padding = New System.Windows.Forms.Padding(0, 10, 0, 4)
        Me.Label15.Size = New System.Drawing.Size(145, 30)
        Me.Label15.TabIndex = 25
        Me.Label15.Text = "standard Tv Gruppe:"
        '
        'CheckBoxUseSportLogos
        '
        Me.CheckBoxUseSportLogos.AutoSize = True
        Me.CheckBoxUseSportLogos.Dock = System.Windows.Forms.DockStyle.Top
        Me.CheckBoxUseSportLogos.Location = New System.Drawing.Point(10, 10)
        Me.CheckBoxUseSportLogos.Name = "CheckBoxUseSportLogos"
        Me.CheckBoxUseSportLogos.Padding = New System.Windows.Forms.Padding(0, 0, 0, 10)
        Me.CheckBoxUseSportLogos.Size = New System.Drawing.Size(368, 30)
        Me.CheckBoxUseSportLogos.TabIndex = 6
        Me.CheckBoxUseSportLogos.Text = "Sport Logos verwenden"
        Me.CheckBoxUseSportLogos.UseVisualStyleBackColor = True
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.GroupBox8)
        Me.Panel7.Controls.Add(Me.Panel10)
        Me.Panel7.Controls.Add(Me.Panel8)
        Me.Panel7.Controls.Add(Me.Panel9)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel7.Size = New System.Drawing.Size(391, 258)
        Me.Panel7.TabIndex = 44
        '
        'GroupBox8
        '
        Me.GroupBox8.AutoSize = True
        Me.GroupBox8.Controls.Add(Me.NumPreviewMinTvMovieRating)
        Me.GroupBox8.Controls.Add(Me.NumPreviewDays)
        Me.GroupBox8.Controls.Add(Me.Label24)
        Me.GroupBox8.Controls.Add(Me.Label26)
        Me.GroupBox8.Controls.Add(Me.Label25)
        Me.GroupBox8.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox8.Location = New System.Drawing.Point(10, 140)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(371, 98)
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
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.tbLateTime)
        Me.Panel10.Controls.Add(Me.Label16)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Location = New System.Drawing.Point(10, 95)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel10.Size = New System.Drawing.Size(371, 45)
        Me.Panel10.TabIndex = 42
        '
        'tbLateTime
        '
        Me.tbLateTime.Dock = System.Windows.Forms.DockStyle.Left
        Me.tbLateTime.Location = New System.Drawing.Point(105, 10)
        Me.tbLateTime.Mask = "00:00"
        Me.tbLateTime.Name = "tbLateTime"
        Me.tbLateTime.Size = New System.Drawing.Size(46, 23)
        Me.tbLateTime.TabIndex = 9
        '
        'Label16
        '
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Location = New System.Drawing.Point(10, 10)
        Me.Label16.Name = "Label16"
        Me.Label16.Padding = New System.Windows.Forms.Padding(0, 3, 6, 0)
        Me.Label16.Size = New System.Drawing.Size(95, 25)
        Me.Label16.TabIndex = 10
        Me.Label16.Text = "Late Time:"
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.tbPrimeTime)
        Me.Panel8.Controls.Add(Me.Label17)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(10, 54)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel8.Size = New System.Drawing.Size(371, 41)
        Me.Panel8.TabIndex = 40
        '
        'tbPrimeTime
        '
        Me.tbPrimeTime.Dock = System.Windows.Forms.DockStyle.Left
        Me.tbPrimeTime.Location = New System.Drawing.Point(105, 10)
        Me.tbPrimeTime.Mask = "00:00"
        Me.tbPrimeTime.Name = "tbPrimeTime"
        Me.tbPrimeTime.Size = New System.Drawing.Size(46, 23)
        Me.tbPrimeTime.TabIndex = 8
        '
        'Label17
        '
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label17.Location = New System.Drawing.Point(10, 10)
        Me.Label17.Name = "Label17"
        Me.Label17.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Label17.Size = New System.Drawing.Size(95, 21)
        Me.Label17.TabIndex = 11
        Me.Label17.Text = "Prime Time:"
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.CBStartGui)
        Me.Panel9.Controls.Add(Me.Label23)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(10, 10)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel9.Size = New System.Drawing.Size(371, 44)
        Me.Panel9.TabIndex = 41
        '
        'CBStartGui
        '
        Me.CBStartGui.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CBStartGui.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBStartGui.FormattingEnabled = True
        Me.CBStartGui.Items.AddRange(New Object() {""})
        Me.CBStartGui.Location = New System.Drawing.Point(86, 10)
        Me.CBStartGui.Name = "CBStartGui"
        Me.CBStartGui.Size = New System.Drawing.Size(275, 24)
        Me.CBStartGui.TabIndex = 33
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label23.Location = New System.Drawing.Point(10, 10)
        Me.Label23.Name = "Label23"
        Me.Label23.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Label23.Size = New System.Drawing.Size(76, 19)
        Me.Label23.TabIndex = 34
        Me.Label23.Text = "Start GUI:"
        '
        'Panel29
        '
        Me.Panel29.Controls.Add(Me.tbEpisodenScanner)
        Me.Panel29.Controls.Add(Me.Label30)
        Me.Panel29.Controls.Add(Me.Label31)
        Me.Panel29.Controls.Add(Me.ButtonOpenDlgEpisodenScanner)
        Me.Panel29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel29.Location = New System.Drawing.Point(3, 128)
        Me.Panel29.Name = "Panel29"
        Me.Panel29.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel29.Size = New System.Drawing.Size(779, 42)
        Me.Panel29.TabIndex = 80
        '
        'tbEpisodenScanner
        '
        Me.tbEpisodenScanner.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbEpisodenScanner.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbEpisodenScanner.Location = New System.Drawing.Point(203, 10)
        Me.tbEpisodenScanner.Name = "tbEpisodenScanner"
        Me.tbEpisodenScanner.Size = New System.Drawing.Size(523, 23)
        Me.tbEpisodenScanner.TabIndex = 2
        '
        'Label30
        '
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label30.Location = New System.Drawing.Point(726, 10)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(10, 22)
        Me.Label30.TabIndex = 43
        '
        'Label31
        '
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label31.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(10, 10)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(193, 22)
        Me.Label31.TabIndex = 1
        Me.Label31.Text = "Episodenscanner:"
        '
        'ButtonOpenDlgEpisodenScanner
        '
        Me.ButtonOpenDlgEpisodenScanner.AutoSize = True
        Me.ButtonOpenDlgEpisodenScanner.Dock = System.Windows.Forms.DockStyle.Right
        Me.ButtonOpenDlgEpisodenScanner.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonOpenDlgEpisodenScanner.Location = New System.Drawing.Point(736, 10)
        Me.ButtonOpenDlgEpisodenScanner.Name = "ButtonOpenDlgEpisodenScanner"
        Me.ButtonOpenDlgEpisodenScanner.Size = New System.Drawing.Size(33, 22)
        Me.ButtonOpenDlgEpisodenScanner.TabIndex = 5
        Me.ButtonOpenDlgEpisodenScanner.Text = "..."
        Me.ButtonOpenDlgEpisodenScanner.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.tbClickfinderImagePath)
        Me.Panel5.Controls.Add(Me.Label27)
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Controls.Add(Me.ButtonOpenDlgImageFolder)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(3, 86)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel5.Size = New System.Drawing.Size(779, 42)
        Me.Panel5.TabIndex = 42
        '
        'tbClickfinderImagePath
        '
        Me.tbClickfinderImagePath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbClickfinderImagePath.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbClickfinderImagePath.Location = New System.Drawing.Point(203, 10)
        Me.tbClickfinderImagePath.Name = "tbClickfinderImagePath"
        Me.tbClickfinderImagePath.Size = New System.Drawing.Size(523, 23)
        Me.tbClickfinderImagePath.TabIndex = 2
        '
        'Label27
        '
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label27.Location = New System.Drawing.Point(726, 10)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(10, 22)
        Me.Label27.TabIndex = 43
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(10, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(193, 22)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Clickfinder Bilder Ordner:"
        '
        'ButtonOpenDlgImageFolder
        '
        Me.ButtonOpenDlgImageFolder.AutoSize = True
        Me.ButtonOpenDlgImageFolder.Dock = System.Windows.Forms.DockStyle.Right
        Me.ButtonOpenDlgImageFolder.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonOpenDlgImageFolder.Location = New System.Drawing.Point(736, 10)
        Me.ButtonOpenDlgImageFolder.Name = "ButtonOpenDlgImageFolder"
        Me.ButtonOpenDlgImageFolder.Size = New System.Drawing.Size(33, 22)
        Me.ButtonOpenDlgImageFolder.TabIndex = 5
        Me.ButtonOpenDlgImageFolder.Text = "..."
        Me.ButtonOpenDlgImageFolder.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.tbClickfinderDatabase)
        Me.Panel4.Controls.Add(Me.Label28)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.ButtonOpenDlgDatabase)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(3, 44)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel4.Size = New System.Drawing.Size(779, 42)
        Me.Panel4.TabIndex = 41
        '
        'tbClickfinderDatabase
        '
        Me.tbClickfinderDatabase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbClickfinderDatabase.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbClickfinderDatabase.Location = New System.Drawing.Point(203, 10)
        Me.tbClickfinderDatabase.Name = "tbClickfinderDatabase"
        Me.tbClickfinderDatabase.Size = New System.Drawing.Size(523, 23)
        Me.tbClickfinderDatabase.TabIndex = 3
        '
        'Label28
        '
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label28.Location = New System.Drawing.Point(726, 10)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(10, 22)
        Me.Label28.TabIndex = 44
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(193, 22)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Clickfinder Database:"
        '
        'ButtonOpenDlgDatabase
        '
        Me.ButtonOpenDlgDatabase.AutoSize = True
        Me.ButtonOpenDlgDatabase.Dock = System.Windows.Forms.DockStyle.Right
        Me.ButtonOpenDlgDatabase.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonOpenDlgDatabase.Location = New System.Drawing.Point(736, 10)
        Me.ButtonOpenDlgDatabase.Name = "ButtonOpenDlgDatabase"
        Me.ButtonOpenDlgDatabase.Size = New System.Drawing.Size(33, 22)
        Me.ButtonOpenDlgDatabase.TabIndex = 4
        Me.ButtonOpenDlgDatabase.Text = "..."
        Me.ButtonOpenDlgDatabase.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.tbPluginName)
        Me.Panel3.Controls.Add(Me.Label29)
        Me.Panel3.Controls.Add(Me.Label20)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(3, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel3.Size = New System.Drawing.Size(779, 41)
        Me.Panel3.TabIndex = 40
        '
        'tbPluginName
        '
        Me.tbPluginName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbPluginName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPluginName.Location = New System.Drawing.Point(203, 10)
        Me.tbPluginName.Name = "tbPluginName"
        Me.tbPluginName.Size = New System.Drawing.Size(523, 23)
        Me.tbPluginName.TabIndex = 31
        '
        'Label29
        '
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label29.Location = New System.Drawing.Point(726, 10)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(43, 21)
        Me.Label29.TabIndex = 45
        '
        'Label20
        '
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label20.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(10, 10)
        Me.Label20.Name = "Label20"
        Me.Label20.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Label20.Size = New System.Drawing.Size(193, 21)
        Me.Label20.TabIndex = 30
        Me.Label20.Text = "Plugin Name:"
        '
        'TabItems
        '
        Me.TabItems.AutoScroll = True
        Me.TabItems.Controls.Add(Me.Panel33)
        Me.TabItems.Location = New System.Drawing.Point(4, 25)
        Me.TabItems.Name = "TabItems"
        Me.TabItems.Padding = New System.Windows.Forms.Padding(3)
        Me.TabItems.Size = New System.Drawing.Size(785, 702)
        Me.TabItems.TabIndex = 3
        Me.TabItems.Text = "GUI Items"
        Me.TabItems.UseVisualStyleBackColor = True
        '
        'Panel33
        '
        Me.Panel33.AutoScroll = True
        Me.Panel33.Controls.Add(Me.Panel32)
        Me.Panel33.Controls.Add(Me.CheckBoxRemberSortedBy)
        Me.Panel33.Controls.Add(Me.CheckBoxFilterShowLocalSeries)
        Me.Panel33.Controls.Add(Me.CheckBoxFilterShowLocalMovies)
        Me.Panel33.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel33.Location = New System.Drawing.Point(3, 3)
        Me.Panel33.Name = "Panel33"
        Me.Panel33.Size = New System.Drawing.Size(779, 696)
        Me.Panel33.TabIndex = 25
        '
        'Panel32
        '
        Me.Panel32.Controls.Add(Me.GroupBox9)
        Me.Panel32.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel32.Location = New System.Drawing.Point(0, 110)
        Me.Panel32.Name = "Panel32"
        Me.Panel32.Padding = New System.Windows.Forms.Padding(26, 10, 26, 10)
        Me.Panel32.Size = New System.Drawing.Size(779, 543)
        Me.Panel32.TabIndex = 24
        '
        'GroupBox9
        '
        Me.GroupBox9.AutoSize = True
        Me.GroupBox9.Controls.Add(Me.Panel34)
        Me.GroupBox9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox9.Location = New System.Drawing.Point(26, 10)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Padding = New System.Windows.Forms.Padding(10)
        Me.GroupBox9.Size = New System.Drawing.Size(727, 523)
        Me.GroupBox9.TabIndex = 23
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Remote control"
        '
        'Panel34
        '
        Me.Panel34.AutoScroll = True
        Me.Panel34.AutoSize = True
        Me.Panel34.Controls.Add(Me.TableItemsPanel)
        Me.Panel34.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel34.Location = New System.Drawing.Point(10, 26)
        Me.Panel34.Name = "Panel34"
        Me.Panel34.Size = New System.Drawing.Size(474, 487)
        Me.Panel34.TabIndex = 23
        '
        'TableItemsPanel
        '
        Me.TableItemsPanel.AutoSize = True
        Me.TableItemsPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableItemsPanel.ColumnCount = 3
        Me.TableItemsPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableItemsPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableItemsPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230.0!))
        Me.TableItemsPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableItemsPanel.Controls.Add(Me.PictureBox12, 0, 10)
        Me.TableItemsPanel.Controls.Add(Me.CBTvGroup0, 1, 10)
        Me.TableItemsPanel.Controls.Add(Me.CBsort0, 2, 10)
        Me.TableItemsPanel.Controls.Add(Me.Label36, 0, 0)
        Me.TableItemsPanel.Controls.Add(Me.Label35, 2, 0)
        Me.TableItemsPanel.Controls.Add(Me.Label34, 1, 0)
        Me.TableItemsPanel.Controls.Add(Me.CBTvGroup9, 1, 9)
        Me.TableItemsPanel.Controls.Add(Me.CBsort9, 2, 9)
        Me.TableItemsPanel.Controls.Add(Me.CBTvGroup8, 1, 8)
        Me.TableItemsPanel.Controls.Add(Me.CBsort8, 2, 8)
        Me.TableItemsPanel.Controls.Add(Me.CBsort7, 2, 7)
        Me.TableItemsPanel.Controls.Add(Me.CBTvGroup7, 1, 7)
        Me.TableItemsPanel.Controls.Add(Me.CBsort6, 2, 6)
        Me.TableItemsPanel.Controls.Add(Me.CBTvGroup6, 1, 6)
        Me.TableItemsPanel.Controls.Add(Me.CBsort5, 2, 5)
        Me.TableItemsPanel.Controls.Add(Me.CBTvGroup5, 1, 5)
        Me.TableItemsPanel.Controls.Add(Me.CBsort4, 2, 4)
        Me.TableItemsPanel.Controls.Add(Me.CBTvGroup4, 1, 4)
        Me.TableItemsPanel.Controls.Add(Me.CBsort3, 2, 3)
        Me.TableItemsPanel.Controls.Add(Me.CBTvGroup3, 1, 3)
        Me.TableItemsPanel.Controls.Add(Me.CBsort2, 2, 2)
        Me.TableItemsPanel.Controls.Add(Me.CBTvGroup2, 1, 2)
        Me.TableItemsPanel.Controls.Add(Me.CBsort1, 2, 1)
        Me.TableItemsPanel.Controls.Add(Me.CBTvGroup1, 1, 1)
        Me.TableItemsPanel.Controls.Add(Me.PictureBox11, 0, 9)
        Me.TableItemsPanel.Controls.Add(Me.PictureBox10, 0, 8)
        Me.TableItemsPanel.Controls.Add(Me.PictureBox4, 0, 7)
        Me.TableItemsPanel.Controls.Add(Me.PictureBox5, 0, 6)
        Me.TableItemsPanel.Controls.Add(Me.PictureBox6, 0, 5)
        Me.TableItemsPanel.Controls.Add(Me.PictureBox7, 0, 4)
        Me.TableItemsPanel.Controls.Add(Me.PictureBox8, 0, 3)
        Me.TableItemsPanel.Controls.Add(Me.PictureBox9, 0, 2)
        Me.TableItemsPanel.Controls.Add(Me.PictureBox3, 0, 1)
        Me.TableItemsPanel.Controls.Add(Me.BTClearSorting, 2, 11)
        Me.TableItemsPanel.Controls.Add(Me.BTsetToStandardTvGroup, 1, 11)
        Me.TableItemsPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableItemsPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize
        Me.TableItemsPanel.Location = New System.Drawing.Point(0, 0)
        Me.TableItemsPanel.Name = "TableItemsPanel"
        Me.TableItemsPanel.RowCount = 12
        Me.TableItemsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableItemsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableItemsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableItemsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableItemsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableItemsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableItemsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableItemsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableItemsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableItemsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableItemsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableItemsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableItemsPanel.Size = New System.Drawing.Size(474, 487)
        Me.TableItemsPanel.TabIndex = 22
        '
        'PictureBox12
        '
        Me.PictureBox12.BackgroundImage = CType(resources.GetObject("PictureBox12.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox12.Location = New System.Drawing.Point(4, 390)
        Me.PictureBox12.Name = "PictureBox12"
        Me.PictureBox12.Size = New System.Drawing.Size(34, 34)
        Me.PictureBox12.TabIndex = 52
        Me.PictureBox12.TabStop = False
        '
        'CBTvGroup0
        '
        Me.CBTvGroup0.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBTvGroup0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBTvGroup0.FormattingEnabled = True
        Me.CBTvGroup0.Location = New System.Drawing.Point(45, 395)
        Me.CBTvGroup0.Name = "CBTvGroup0"
        Me.CBTvGroup0.Size = New System.Drawing.Size(194, 24)
        Me.CBTvGroup0.TabIndex = 51
        '
        'CBsort0
        '
        Me.CBsort0.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBsort0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBsort0.FormattingEnabled = True
        Me.CBsort0.Location = New System.Drawing.Point(246, 395)
        Me.CBsort0.Name = "CBsort0"
        Me.CBsort0.Size = New System.Drawing.Size(224, 24)
        Me.CBsort0.TabIndex = 52
        '
        'Label36
        '
        Me.Label36.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(4, 1)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(34, 16)
        Me.Label36.TabIndex = 24
        Me.Label36.Text = "key"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label35
        '
        Me.Label35.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(246, 1)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(224, 16)
        Me.Label35.TabIndex = 24
        Me.Label35.Text = "sortiert nach"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label34
        '
        Me.Label34.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(45, 1)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(194, 16)
        Me.Label34.TabIndex = 23
        Me.Label34.Text = "Tv Gruppe"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CBTvGroup9
        '
        Me.CBTvGroup9.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBTvGroup9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBTvGroup9.FormattingEnabled = True
        Me.CBTvGroup9.Location = New System.Drawing.Point(45, 354)
        Me.CBTvGroup9.Name = "CBTvGroup9"
        Me.CBTvGroup9.Size = New System.Drawing.Size(194, 24)
        Me.CBTvGroup9.TabIndex = 37
        '
        'CBsort9
        '
        Me.CBsort9.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBsort9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBsort9.FormattingEnabled = True
        Me.CBsort9.Location = New System.Drawing.Point(246, 354)
        Me.CBsort9.Name = "CBsort9"
        Me.CBsort9.Size = New System.Drawing.Size(224, 24)
        Me.CBsort9.TabIndex = 38
        '
        'CBTvGroup8
        '
        Me.CBTvGroup8.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBTvGroup8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBTvGroup8.FormattingEnabled = True
        Me.CBTvGroup8.Location = New System.Drawing.Point(45, 313)
        Me.CBTvGroup8.Name = "CBTvGroup8"
        Me.CBTvGroup8.Size = New System.Drawing.Size(194, 24)
        Me.CBTvGroup8.TabIndex = 35
        '
        'CBsort8
        '
        Me.CBsort8.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBsort8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBsort8.FormattingEnabled = True
        Me.CBsort8.Location = New System.Drawing.Point(246, 313)
        Me.CBsort8.Name = "CBsort8"
        Me.CBsort8.Size = New System.Drawing.Size(224, 24)
        Me.CBsort8.TabIndex = 36
        '
        'CBsort7
        '
        Me.CBsort7.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBsort7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBsort7.FormattingEnabled = True
        Me.CBsort7.Location = New System.Drawing.Point(246, 272)
        Me.CBsort7.Name = "CBsort7"
        Me.CBsort7.Size = New System.Drawing.Size(224, 24)
        Me.CBsort7.TabIndex = 34
        '
        'CBTvGroup7
        '
        Me.CBTvGroup7.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBTvGroup7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBTvGroup7.FormattingEnabled = True
        Me.CBTvGroup7.Location = New System.Drawing.Point(45, 272)
        Me.CBTvGroup7.Name = "CBTvGroup7"
        Me.CBTvGroup7.Size = New System.Drawing.Size(194, 24)
        Me.CBTvGroup7.TabIndex = 33
        '
        'CBsort6
        '
        Me.CBsort6.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBsort6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBsort6.FormattingEnabled = True
        Me.CBsort6.Location = New System.Drawing.Point(246, 231)
        Me.CBsort6.Name = "CBsort6"
        Me.CBsort6.Size = New System.Drawing.Size(224, 24)
        Me.CBsort6.TabIndex = 32
        '
        'CBTvGroup6
        '
        Me.CBTvGroup6.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBTvGroup6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBTvGroup6.FormattingEnabled = True
        Me.CBTvGroup6.Location = New System.Drawing.Point(45, 231)
        Me.CBTvGroup6.Name = "CBTvGroup6"
        Me.CBTvGroup6.Size = New System.Drawing.Size(194, 24)
        Me.CBTvGroup6.TabIndex = 31
        '
        'CBsort5
        '
        Me.CBsort5.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBsort5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBsort5.FormattingEnabled = True
        Me.CBsort5.Location = New System.Drawing.Point(246, 190)
        Me.CBsort5.Name = "CBsort5"
        Me.CBsort5.Size = New System.Drawing.Size(224, 24)
        Me.CBsort5.TabIndex = 30
        '
        'CBTvGroup5
        '
        Me.CBTvGroup5.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBTvGroup5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBTvGroup5.FormattingEnabled = True
        Me.CBTvGroup5.Location = New System.Drawing.Point(45, 190)
        Me.CBTvGroup5.Name = "CBTvGroup5"
        Me.CBTvGroup5.Size = New System.Drawing.Size(194, 24)
        Me.CBTvGroup5.TabIndex = 29
        '
        'CBsort4
        '
        Me.CBsort4.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBsort4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBsort4.FormattingEnabled = True
        Me.CBsort4.Location = New System.Drawing.Point(246, 149)
        Me.CBsort4.Name = "CBsort4"
        Me.CBsort4.Size = New System.Drawing.Size(224, 24)
        Me.CBsort4.TabIndex = 28
        '
        'CBTvGroup4
        '
        Me.CBTvGroup4.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBTvGroup4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBTvGroup4.FormattingEnabled = True
        Me.CBTvGroup4.Location = New System.Drawing.Point(45, 149)
        Me.CBTvGroup4.Name = "CBTvGroup4"
        Me.CBTvGroup4.Size = New System.Drawing.Size(194, 24)
        Me.CBTvGroup4.TabIndex = 27
        '
        'CBsort3
        '
        Me.CBsort3.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBsort3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBsort3.FormattingEnabled = True
        Me.CBsort3.Location = New System.Drawing.Point(246, 108)
        Me.CBsort3.Name = "CBsort3"
        Me.CBsort3.Size = New System.Drawing.Size(224, 24)
        Me.CBsort3.TabIndex = 26
        '
        'CBTvGroup3
        '
        Me.CBTvGroup3.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBTvGroup3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBTvGroup3.FormattingEnabled = True
        Me.CBTvGroup3.Location = New System.Drawing.Point(45, 108)
        Me.CBTvGroup3.Name = "CBTvGroup3"
        Me.CBTvGroup3.Size = New System.Drawing.Size(194, 24)
        Me.CBTvGroup3.TabIndex = 25
        '
        'CBsort2
        '
        Me.CBsort2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBsort2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBsort2.FormattingEnabled = True
        Me.CBsort2.Location = New System.Drawing.Point(246, 67)
        Me.CBsort2.Name = "CBsort2"
        Me.CBsort2.Size = New System.Drawing.Size(224, 24)
        Me.CBsort2.TabIndex = 24
        '
        'CBTvGroup2
        '
        Me.CBTvGroup2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBTvGroup2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBTvGroup2.FormattingEnabled = True
        Me.CBTvGroup2.Location = New System.Drawing.Point(45, 67)
        Me.CBTvGroup2.Name = "CBTvGroup2"
        Me.CBTvGroup2.Size = New System.Drawing.Size(194, 24)
        Me.CBTvGroup2.TabIndex = 23
        '
        'CBsort1
        '
        Me.CBsort1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBsort1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBsort1.FormattingEnabled = True
        Me.CBsort1.Location = New System.Drawing.Point(246, 26)
        Me.CBsort1.Name = "CBsort1"
        Me.CBsort1.Size = New System.Drawing.Size(224, 24)
        Me.CBsort1.TabIndex = 22
        '
        'CBTvGroup1
        '
        Me.CBTvGroup1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBTvGroup1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBTvGroup1.FormattingEnabled = True
        Me.CBTvGroup1.Location = New System.Drawing.Point(45, 26)
        Me.CBTvGroup1.Name = "CBTvGroup1"
        Me.CBTvGroup1.Size = New System.Drawing.Size(194, 24)
        Me.CBTvGroup1.TabIndex = 21
        '
        'PictureBox11
        '
        Me.PictureBox11.BackgroundImage = CType(resources.GetObject("PictureBox11.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox11.Location = New System.Drawing.Point(4, 349)
        Me.PictureBox11.Name = "PictureBox11"
        Me.PictureBox11.Size = New System.Drawing.Size(34, 34)
        Me.PictureBox11.TabIndex = 20
        Me.PictureBox11.TabStop = False
        '
        'PictureBox10
        '
        Me.PictureBox10.BackgroundImage = CType(resources.GetObject("PictureBox10.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox10.Location = New System.Drawing.Point(4, 308)
        Me.PictureBox10.Name = "PictureBox10"
        Me.PictureBox10.Size = New System.Drawing.Size(34, 34)
        Me.PictureBox10.TabIndex = 19
        Me.PictureBox10.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.BackgroundImage = CType(resources.GetObject("PictureBox4.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox4.Location = New System.Drawing.Point(4, 267)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(34, 34)
        Me.PictureBox4.TabIndex = 13
        Me.PictureBox4.TabStop = False
        '
        'PictureBox5
        '
        Me.PictureBox5.BackgroundImage = CType(resources.GetObject("PictureBox5.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox5.Location = New System.Drawing.Point(4, 226)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(34, 34)
        Me.PictureBox5.TabIndex = 14
        Me.PictureBox5.TabStop = False
        '
        'PictureBox6
        '
        Me.PictureBox6.BackgroundImage = CType(resources.GetObject("PictureBox6.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox6.Location = New System.Drawing.Point(4, 185)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(34, 34)
        Me.PictureBox6.TabIndex = 15
        Me.PictureBox6.TabStop = False
        '
        'PictureBox7
        '
        Me.PictureBox7.BackgroundImage = CType(resources.GetObject("PictureBox7.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox7.Location = New System.Drawing.Point(4, 144)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(34, 34)
        Me.PictureBox7.TabIndex = 16
        Me.PictureBox7.TabStop = False
        '
        'PictureBox8
        '
        Me.PictureBox8.BackgroundImage = CType(resources.GetObject("PictureBox8.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox8.Location = New System.Drawing.Point(4, 103)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(34, 34)
        Me.PictureBox8.TabIndex = 17
        Me.PictureBox8.TabStop = False
        '
        'PictureBox9
        '
        Me.PictureBox9.BackgroundImage = CType(resources.GetObject("PictureBox9.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox9.Location = New System.Drawing.Point(4, 62)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(34, 34)
        Me.PictureBox9.TabIndex = 18
        Me.PictureBox9.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.BackgroundImage = CType(resources.GetObject("PictureBox3.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox3.Location = New System.Drawing.Point(4, 21)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(34, 34)
        Me.PictureBox3.TabIndex = 12
        Me.PictureBox3.TabStop = False
        '
        'BTClearSorting
        '
        Me.BTClearSorting.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BTClearSorting.Location = New System.Drawing.Point(246, 445)
        Me.BTClearSorting.Name = "BTClearSorting"
        Me.BTClearSorting.Size = New System.Drawing.Size(224, 24)
        Me.BTClearSorting.TabIndex = 40
        Me.BTClearSorting.Text = "alle löschen"
        Me.BTClearSorting.UseVisualStyleBackColor = True
        '
        'BTsetToStandardTvGroup
        '
        Me.BTsetToStandardTvGroup.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BTsetToStandardTvGroup.Location = New System.Drawing.Point(45, 445)
        Me.BTsetToStandardTvGroup.Name = "BTsetToStandardTvGroup"
        Me.BTsetToStandardTvGroup.Size = New System.Drawing.Size(194, 24)
        Me.BTsetToStandardTvGroup.TabIndex = 39
        Me.BTsetToStandardTvGroup.Text = "alle löschen"
        Me.BTsetToStandardTvGroup.UseVisualStyleBackColor = True
        '
        'CheckBoxRemberSortedBy
        '
        Me.CheckBoxRemberSortedBy.AutoSize = True
        Me.CheckBoxRemberSortedBy.Dock = System.Windows.Forms.DockStyle.Top
        Me.CheckBoxRemberSortedBy.Location = New System.Drawing.Point(0, 70)
        Me.CheckBoxRemberSortedBy.Name = "CheckBoxRemberSortedBy"
        Me.CheckBoxRemberSortedBy.Padding = New System.Windows.Forms.Padding(30, 10, 30, 10)
        Me.CheckBoxRemberSortedBy.Size = New System.Drawing.Size(779, 40)
        Me.CheckBoxRemberSortedBy.TabIndex = 11
        Me.CheckBoxRemberSortedBy.Text = "merke zuletzt gewählte Sortierung in Kategorien"
        Me.CheckBoxRemberSortedBy.UseVisualStyleBackColor = True
        '
        'CheckBoxFilterShowLocalSeries
        '
        Me.CheckBoxFilterShowLocalSeries.AutoSize = True
        Me.CheckBoxFilterShowLocalSeries.Dock = System.Windows.Forms.DockStyle.Top
        Me.CheckBoxFilterShowLocalSeries.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxFilterShowLocalSeries.Location = New System.Drawing.Point(0, 35)
        Me.CheckBoxFilterShowLocalSeries.Name = "CheckBoxFilterShowLocalSeries"
        Me.CheckBoxFilterShowLocalSeries.Padding = New System.Windows.Forms.Padding(30, 10, 30, 5)
        Me.CheckBoxFilterShowLocalSeries.Size = New System.Drawing.Size(779, 35)
        Me.CheckBoxFilterShowLocalSeries.TabIndex = 10
        Me.CheckBoxFilterShowLocalSeries.Text = "Keine Serien (Episoden) zeigen, die lokal existieren"
        Me.CheckBoxFilterShowLocalSeries.UseVisualStyleBackColor = True
        '
        'CheckBoxFilterShowLocalMovies
        '
        Me.CheckBoxFilterShowLocalMovies.AutoSize = True
        Me.CheckBoxFilterShowLocalMovies.Dock = System.Windows.Forms.DockStyle.Top
        Me.CheckBoxFilterShowLocalMovies.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxFilterShowLocalMovies.Location = New System.Drawing.Point(0, 0)
        Me.CheckBoxFilterShowLocalMovies.Name = "CheckBoxFilterShowLocalMovies"
        Me.CheckBoxFilterShowLocalMovies.Padding = New System.Windows.Forms.Padding(30, 10, 30, 5)
        Me.CheckBoxFilterShowLocalMovies.Size = New System.Drawing.Size(779, 35)
        Me.CheckBoxFilterShowLocalMovies.TabIndex = 9
        Me.CheckBoxFilterShowLocalMovies.Text = "Keine Filme zeigen, die lokal existieren"
        Me.CheckBoxFilterShowLocalMovies.UseVisualStyleBackColor = True
        '
        'TabÜbersicht
        '
        Me.TabÜbersicht.AutoScroll = True
        Me.TabÜbersicht.Controls.Add(Me.Panel18)
        Me.TabÜbersicht.Controls.Add(Me.Panel15)
        Me.TabÜbersicht.Controls.Add(Me.Panel14)
        Me.TabÜbersicht.Location = New System.Drawing.Point(4, 25)
        Me.TabÜbersicht.Name = "TabÜbersicht"
        Me.TabÜbersicht.Padding = New System.Windows.Forms.Padding(3)
        Me.TabÜbersicht.Size = New System.Drawing.Size(785, 702)
        Me.TabÜbersicht.TabIndex = 1
        Me.TabÜbersicht.Text = "GUI Highlights"
        Me.TabÜbersicht.UseVisualStyleBackColor = True
        '
        'Panel18
        '
        Me.Panel18.Controls.Add(Me.GroupBox1)
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel18.Location = New System.Drawing.Point(3, 295)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel18.Size = New System.Drawing.Size(779, 127)
        Me.Panel18.TabIndex = 17
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel20)
        Me.GroupBox1.Controls.Add(Me.Panel19)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(10, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(10)
        Me.GroupBox1.Size = New System.Drawing.Size(759, 107)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "weitere Highlights"
        '
        'Panel20
        '
        Me.Panel20.Controls.Add(Me.Label11)
        Me.Panel20.Controls.Add(Me.NumHighlightsMinRuntime)
        Me.Panel20.Controls.Add(Me.Label10)
        Me.Panel20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel20.Location = New System.Drawing.Point(10, 60)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Padding = New System.Windows.Forms.Padding(4)
        Me.Panel20.Size = New System.Drawing.Size(739, 35)
        Me.Panel20.TabIndex = 13
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(214, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Padding = New System.Windows.Forms.Padding(5, 3, 0, 0)
        Me.Label11.Size = New System.Drawing.Size(65, 19)
        Me.Label11.TabIndex = 11
        Me.Label11.Text = "Minuten"
        '
        'NumHighlightsMinRuntime
        '
        Me.NumHighlightsMinRuntime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumHighlightsMinRuntime.Dock = System.Windows.Forms.DockStyle.Left
        Me.NumHighlightsMinRuntime.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumHighlightsMinRuntime.Location = New System.Drawing.Point(172, 4)
        Me.NumHighlightsMinRuntime.Maximum = New Decimal(New Integer() {59, 0, 0, 0})
        Me.NumHighlightsMinRuntime.Name = "NumHighlightsMinRuntime"
        Me.NumHighlightsMinRuntime.Size = New System.Drawing.Size(42, 23)
        Me.NumHighlightsMinRuntime.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(4, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Padding = New System.Windows.Forms.Padding(0, 3, 5, 0)
        Me.Label10.Size = New System.Drawing.Size(168, 19)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "Ignoriere wenn Dauer <"
        '
        'Panel19
        '
        Me.Panel19.Controls.Add(Me.Label7)
        Me.Panel19.Controls.Add(Me.NumShowHighlightsAfter)
        Me.Panel19.Controls.Add(Me.Label8)
        Me.Panel19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel19.Location = New System.Drawing.Point(10, 26)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Padding = New System.Windows.Forms.Padding(4)
        Me.Panel19.Size = New System.Drawing.Size(739, 34)
        Me.Panel19.TabIndex = 12
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(215, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Padding = New System.Windows.Forms.Padding(5, 3, 0, 0)
        Me.Label7.Size = New System.Drawing.Size(35, 19)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Uhr"
        '
        'NumShowHighlightsAfter
        '
        Me.NumShowHighlightsAfter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumShowHighlightsAfter.Dock = System.Windows.Forms.DockStyle.Left
        Me.NumShowHighlightsAfter.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumShowHighlightsAfter.Location = New System.Drawing.Point(173, 4)
        Me.NumShowHighlightsAfter.Maximum = New Decimal(New Integer() {23, 0, 0, 0})
        Me.NumShowHighlightsAfter.Name = "NumShowHighlightsAfter"
        Me.NumShowHighlightsAfter.Size = New System.Drawing.Size(42, 23)
        Me.NumShowHighlightsAfter.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(4, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Padding = New System.Windows.Forms.Padding(0, 3, 5, 0)
        Me.Label8.Size = New System.Drawing.Size(169, 19)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Zeige Highlights erst ab"
        '
        'Panel15
        '
        Me.Panel15.Controls.Add(Me.GroupBox2)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel15.Location = New System.Drawing.Point(3, 61)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel15.Size = New System.Drawing.Size(779, 234)
        Me.Panel15.TabIndex = 16
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Panel17)
        Me.GroupBox2.Controls.Add(Me.Panel16)
        Me.GroupBox2.Controls.Add(Me.CheckBoxShowTagesTipp)
        Me.GroupBox2.Controls.Add(Me.CheckBoxShowLocalMovies)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(10, 10)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(10)
        Me.GroupBox2.Size = New System.Drawing.Size(759, 214)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Movie Highlights"
        '
        'Panel17
        '
        Me.Panel17.Controls.Add(Me.GroupBox3)
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel17.Location = New System.Drawing.Point(10, 125)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel17.Size = New System.Drawing.Size(739, 76)
        Me.Panel17.TabIndex = 9
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.RBstartTime)
        Me.GroupBox3.Controls.Add(Me.RBTvMovieStar)
        Me.GroupBox3.Controls.Add(Me.RBRatingStar)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(10, 10)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(719, 56)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "sortieren nach:"
        '
        'RBstartTime
        '
        Me.RBstartTime.AutoSize = True
        Me.RBstartTime.Dock = System.Windows.Forms.DockStyle.Left
        Me.RBstartTime.Location = New System.Drawing.Point(232, 19)
        Me.RBstartTime.Name = "RBstartTime"
        Me.RBstartTime.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.RBstartTime.Size = New System.Drawing.Size(96, 34)
        Me.RBstartTime.TabIndex = 2
        Me.RBstartTime.TabStop = True
        Me.RBstartTime.Text = "StartZeit"
        Me.RBstartTime.UseVisualStyleBackColor = True
        '
        'RBTvMovieStar
        '
        Me.RBTvMovieStar.AutoSize = True
        Me.RBTvMovieStar.Dock = System.Windows.Forms.DockStyle.Left
        Me.RBTvMovieStar.Location = New System.Drawing.Point(108, 19)
        Me.RBTvMovieStar.Name = "RBTvMovieStar"
        Me.RBTvMovieStar.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.RBTvMovieStar.Size = New System.Drawing.Size(124, 34)
        Me.RBTvMovieStar.TabIndex = 1
        Me.RBTvMovieStar.TabStop = True
        Me.RBTvMovieStar.Text = "TvMovie Star"
        Me.RBTvMovieStar.UseVisualStyleBackColor = True
        '
        'RBRatingStar
        '
        Me.RBRatingStar.AutoSize = True
        Me.RBRatingStar.Dock = System.Windows.Forms.DockStyle.Left
        Me.RBRatingStar.Location = New System.Drawing.Point(3, 19)
        Me.RBRatingStar.Name = "RBRatingStar"
        Me.RBRatingStar.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.RBRatingStar.Size = New System.Drawing.Size(105, 34)
        Me.RBRatingStar.TabIndex = 0
        Me.RBRatingStar.TabStop = True
        Me.RBRatingStar.Text = "RatingStar"
        Me.RBRatingStar.UseVisualStyleBackColor = True
        '
        'Panel16
        '
        Me.Panel16.Controls.Add(Me.Label5)
        Me.Panel16.Controls.Add(Me.NumShowMoviesAfter)
        Me.Panel16.Controls.Add(Me.Label4)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel16.Location = New System.Drawing.Point(10, 86)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Padding = New System.Windows.Forms.Padding(10, 10, 10, 5)
        Me.Panel16.Size = New System.Drawing.Size(739, 39)
        Me.Panel16.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(259, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Label5.Size = New System.Drawing.Size(30, 19)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Uhr"
        '
        'NumShowMoviesAfter
        '
        Me.NumShowMoviesAfter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumShowMoviesAfter.Dock = System.Windows.Forms.DockStyle.Left
        Me.NumShowMoviesAfter.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumShowMoviesAfter.Location = New System.Drawing.Point(217, 10)
        Me.NumShowMoviesAfter.Maximum = New Decimal(New Integer() {23, 0, 0, 0})
        Me.NumShowMoviesAfter.Name = "NumShowMoviesAfter"
        Me.NumShowMoviesAfter.Size = New System.Drawing.Size(42, 23)
        Me.NumShowMoviesAfter.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(10, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Label4.Size = New System.Drawing.Size(207, 19)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Zeige Movie Highlights erst ab"
        '
        'CheckBoxShowTagesTipp
        '
        Me.CheckBoxShowTagesTipp.AutoSize = True
        Me.CheckBoxShowTagesTipp.Dock = System.Windows.Forms.DockStyle.Top
        Me.CheckBoxShowTagesTipp.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxShowTagesTipp.Location = New System.Drawing.Point(10, 56)
        Me.CheckBoxShowTagesTipp.Name = "CheckBoxShowTagesTipp"
        Me.CheckBoxShowTagesTipp.Padding = New System.Windows.Forms.Padding(10, 5, 10, 5)
        Me.CheckBoxShowTagesTipp.Size = New System.Drawing.Size(739, 30)
        Me.CheckBoxShowTagesTipp.TabIndex = 6
        Me.CheckBoxShowTagesTipp.Text = "Zeige Tv Movie Tages Tipp als ersten Eintrag"
        Me.CheckBoxShowTagesTipp.UseVisualStyleBackColor = True
        '
        'CheckBoxShowLocalMovies
        '
        Me.CheckBoxShowLocalMovies.AutoSize = True
        Me.CheckBoxShowLocalMovies.Dock = System.Windows.Forms.DockStyle.Top
        Me.CheckBoxShowLocalMovies.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxShowLocalMovies.Location = New System.Drawing.Point(10, 26)
        Me.CheckBoxShowLocalMovies.Name = "CheckBoxShowLocalMovies"
        Me.CheckBoxShowLocalMovies.Padding = New System.Windows.Forms.Padding(10, 5, 10, 5)
        Me.CheckBoxShowLocalMovies.Size = New System.Drawing.Size(739, 30)
        Me.CheckBoxShowLocalMovies.TabIndex = 0
        Me.CheckBoxShowLocalMovies.Text = "Keine Filme zeigen, die lokal existieren"
        Me.CheckBoxShowLocalMovies.UseVisualStyleBackColor = True
        '
        'Panel14
        '
        Me.Panel14.Controls.Add(Me.Label13)
        Me.Panel14.Controls.Add(Me.NumMaxDays)
        Me.Panel14.Controls.Add(Me.Label12)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel14.Location = New System.Drawing.Point(3, 3)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Padding = New System.Windows.Forms.Padding(20, 20, 20, 10)
        Me.Panel14.Size = New System.Drawing.Size(779, 58)
        Me.Panel14.TabIndex = 15
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(262, 20)
        Me.Label13.Name = "Label13"
        Me.Label13.Padding = New System.Windows.Forms.Padding(5, 3, 0, 0)
        Me.Label13.Size = New System.Drawing.Size(46, 19)
        Me.Label13.TabIndex = 14
        Me.Label13.Text = "Tage"
        '
        'NumMaxDays
        '
        Me.NumMaxDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumMaxDays.Dock = System.Windows.Forms.DockStyle.Left
        Me.NumMaxDays.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumMaxDays.Location = New System.Drawing.Point(220, 20)
        Me.NumMaxDays.Maximum = New Decimal(New Integer() {14, 0, 0, 0})
        Me.NumMaxDays.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumMaxDays.Name = "NumMaxDays"
        Me.NumMaxDays.Size = New System.Drawing.Size(42, 23)
        Me.NumMaxDays.TabIndex = 12
        Me.NumMaxDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumMaxDays.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(20, 20)
        Me.Label12.Name = "Label12"
        Me.Label12.Padding = New System.Windows.Forms.Padding(0, 3, 5, 0)
        Me.Label12.Size = New System.Drawing.Size(200, 19)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "Zeige Übersicht für maximal "
        '
        'TabCategories
        '
        Me.TabCategories.AutoScroll = True
        Me.TabCategories.Controls.Add(Me.CheckBoxShowCategorieLocalSeries)
        Me.TabCategories.Controls.Add(Me.CheckBoxShowCategorieLocalMovies)
        Me.TabCategories.Controls.Add(Me.Panel27)
        Me.TabCategories.Controls.Add(Me.SplitContainer1)
        Me.TabCategories.Location = New System.Drawing.Point(4, 25)
        Me.TabCategories.Name = "TabCategories"
        Me.TabCategories.Size = New System.Drawing.Size(785, 702)
        Me.TabCategories.TabIndex = 2
        Me.TabCategories.Text = "GUI Kategorien"
        Me.TabCategories.UseVisualStyleBackColor = True
        '
        'CheckBoxShowCategorieLocalSeries
        '
        Me.CheckBoxShowCategorieLocalSeries.AutoSize = True
        Me.CheckBoxShowCategorieLocalSeries.Dock = System.Windows.Forms.DockStyle.Top
        Me.CheckBoxShowCategorieLocalSeries.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxShowCategorieLocalSeries.Location = New System.Drawing.Point(0, 475)
        Me.CheckBoxShowCategorieLocalSeries.Name = "CheckBoxShowCategorieLocalSeries"
        Me.CheckBoxShowCategorieLocalSeries.Padding = New System.Windows.Forms.Padding(20, 5, 40, 10)
        Me.CheckBoxShowCategorieLocalSeries.Size = New System.Drawing.Size(785, 35)
        Me.CheckBoxShowCategorieLocalSeries.TabIndex = 12
        Me.CheckBoxShowCategorieLocalSeries.Text = "Keine Serien (Episoden) zeigen, die lokal existieren"
        Me.CheckBoxShowCategorieLocalSeries.UseVisualStyleBackColor = True
        '
        'CheckBoxShowCategorieLocalMovies
        '
        Me.CheckBoxShowCategorieLocalMovies.AutoSize = True
        Me.CheckBoxShowCategorieLocalMovies.Dock = System.Windows.Forms.DockStyle.Top
        Me.CheckBoxShowCategorieLocalMovies.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxShowCategorieLocalMovies.Location = New System.Drawing.Point(0, 430)
        Me.CheckBoxShowCategorieLocalMovies.Name = "CheckBoxShowCategorieLocalMovies"
        Me.CheckBoxShowCategorieLocalMovies.Padding = New System.Windows.Forms.Padding(20, 20, 40, 5)
        Me.CheckBoxShowCategorieLocalMovies.Size = New System.Drawing.Size(785, 45)
        Me.CheckBoxShowCategorieLocalMovies.TabIndex = 11
        Me.CheckBoxShowCategorieLocalMovies.Text = "Keine Filme zeigen, die lokal existieren"
        Me.CheckBoxShowCategorieLocalMovies.UseVisualStyleBackColor = True
        '
        'Panel27
        '
        Me.Panel27.Controls.Add(Me.ButtonNewCategorie)
        Me.Panel27.Controls.Add(Me.CheckBoxSelect)
        Me.Panel27.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel27.Location = New System.Drawing.Point(0, 383)
        Me.Panel27.Name = "Panel27"
        Me.Panel27.Padding = New System.Windows.Forms.Padding(20, 10, 40, 10)
        Me.Panel27.Size = New System.Drawing.Size(785, 47)
        Me.Panel27.TabIndex = 15
        '
        'ButtonNewCategorie
        '
        Me.ButtonNewCategorie.Dock = System.Windows.Forms.DockStyle.Right
        Me.ButtonNewCategorie.Location = New System.Drawing.Point(677, 10)
        Me.ButtonNewCategorie.Name = "ButtonNewCategorie"
        Me.ButtonNewCategorie.Size = New System.Drawing.Size(68, 27)
        Me.ButtonNewCategorie.TabIndex = 2
        Me.ButtonNewCategorie.Text = "New"
        Me.ButtonNewCategorie.UseVisualStyleBackColor = True
        '
        'CheckBoxSelect
        '
        Me.CheckBoxSelect.AutoSize = True
        Me.CheckBoxSelect.Dock = System.Windows.Forms.DockStyle.Left
        Me.CheckBoxSelect.Location = New System.Drawing.Point(20, 10)
        Me.CheckBoxSelect.Name = "CheckBoxSelect"
        Me.CheckBoxSelect.Size = New System.Drawing.Size(88, 27)
        Me.CheckBoxSelect.TabIndex = 6
        Me.CheckBoxSelect.Text = "Select all"
        Me.CheckBoxSelect.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Top
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvCategories)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(10, 10, 2, 10)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel28)
        Me.SplitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.SplitContainer1.Size = New System.Drawing.Size(785, 383)
        Me.SplitContainer1.SplitterDistance = 749
        Me.SplitContainer1.SplitterWidth = 1
        Me.SplitContainer1.TabIndex = 14
        '
        'dgvCategories
        '
        Me.dgvCategories.AllowUserToAddRows = False
        Me.dgvCategories.AllowUserToDeleteRows = False
        Me.dgvCategories.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgvCategories.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.dgvCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCategories.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.C_id, Me.C_Image, Me.C_visible, Me.C_Name, Me.C_Beschreibung, Me.C_sortOrder, Me.C_SqlString, Me.C_MinRuntime, Me.C_NowOffset})
        Me.dgvCategories.Cursor = System.Windows.Forms.Cursors.Hand
        Me.dgvCategories.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvCategories.Location = New System.Drawing.Point(10, 10)
        Me.dgvCategories.Name = "dgvCategories"
        Me.dgvCategories.RowHeadersVisible = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.dgvCategories.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvCategories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCategories.Size = New System.Drawing.Size(737, 363)
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
        Me.C_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.C_Name.HeaderText = "Name"
        Me.C_Name.Name = "C_Name"
        Me.C_Name.ReadOnly = True
        Me.C_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.C_Name.Width = 50
        '
        'C_Beschreibung
        '
        Me.C_Beschreibung.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.C_Beschreibung.HeaderText = "Beschreibung"
        Me.C_Beschreibung.Name = "C_Beschreibung"
        Me.C_Beschreibung.ReadOnly = True
        Me.C_Beschreibung.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
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
        'Panel28
        '
        Me.Panel28.Controls.Add(Me.ButtonDown)
        Me.Panel28.Controls.Add(Me.ButtonUp)
        Me.Panel28.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel28.Location = New System.Drawing.Point(2, 0)
        Me.Panel28.Name = "Panel28"
        Me.Panel28.Padding = New System.Windows.Forms.Padding(2, 130, 2, 2)
        Me.Panel28.Size = New System.Drawing.Size(23, 383)
        Me.Panel28.TabIndex = 14
        '
        'ButtonDown
        '
        Me.ButtonDown.BackgroundImage = Global.ClickfinderProgramGuide.My.Resources.Resources.arrow_Down_48
        Me.ButtonDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ButtonDown.Dock = System.Windows.Forms.DockStyle.Top
        Me.ButtonDown.Location = New System.Drawing.Point(2, 191)
        Me.ButtonDown.Name = "ButtonDown"
        Me.ButtonDown.Padding = New System.Windows.Forms.Padding(10)
        Me.ButtonDown.Size = New System.Drawing.Size(19, 61)
        Me.ButtonDown.TabIndex = 4
        Me.ButtonDown.UseVisualStyleBackColor = True
        '
        'ButtonUp
        '
        Me.ButtonUp.BackgroundImage = Global.ClickfinderProgramGuide.My.Resources.Resources.arrow_UP_48
        Me.ButtonUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ButtonUp.Dock = System.Windows.Forms.DockStyle.Top
        Me.ButtonUp.Location = New System.Drawing.Point(2, 130)
        Me.ButtonUp.Name = "ButtonUp"
        Me.ButtonUp.Padding = New System.Windows.Forms.Padding(10)
        Me.ButtonUp.Size = New System.Drawing.Size(19, 61)
        Me.ButtonUp.TabIndex = 3
        Me.ButtonUp.UseVisualStyleBackColor = True
        '
        'TabDetail
        '
        Me.TabDetail.AutoScroll = True
        Me.TabDetail.Controls.Add(Me.Panel21)
        Me.TabDetail.Controls.Add(Me.CheckBoxUseSeriesDescribtion)
        Me.TabDetail.Location = New System.Drawing.Point(4, 25)
        Me.TabDetail.Name = "TabDetail"
        Me.TabDetail.Padding = New System.Windows.Forms.Padding(3)
        Me.TabDetail.Size = New System.Drawing.Size(785, 702)
        Me.TabDetail.TabIndex = 4
        Me.TabDetail.Text = "GUI Details"
        Me.TabDetail.UseVisualStyleBackColor = True
        '
        'Panel21
        '
        Me.Panel21.Controls.Add(Me.GroupDetailSeriesImage)
        Me.Panel21.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel21.Location = New System.Drawing.Point(3, 53)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel21.Size = New System.Drawing.Size(779, 83)
        Me.Panel21.TabIndex = 12
        '
        'GroupDetailSeriesImage
        '
        Me.GroupDetailSeriesImage.Controls.Add(Me.RBTvMovieImage)
        Me.GroupDetailSeriesImage.Controls.Add(Me.RBEpisodeImage)
        Me.GroupDetailSeriesImage.Controls.Add(Me.RBSeriesFanArt)
        Me.GroupDetailSeriesImage.Controls.Add(Me.RBSeriesCover)
        Me.GroupDetailSeriesImage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupDetailSeriesImage.Location = New System.Drawing.Point(10, 10)
        Me.GroupDetailSeriesImage.Name = "GroupDetailSeriesImage"
        Me.GroupDetailSeriesImage.Padding = New System.Windows.Forms.Padding(10)
        Me.GroupDetailSeriesImage.Size = New System.Drawing.Size(759, 63)
        Me.GroupDetailSeriesImage.TabIndex = 11
        Me.GroupDetailSeriesImage.TabStop = False
        Me.GroupDetailSeriesImage.Text = "Serien Bild:"
        '
        'RBTvMovieImage
        '
        Me.RBTvMovieImage.AutoSize = True
        Me.RBTvMovieImage.Dock = System.Windows.Forms.DockStyle.Left
        Me.RBTvMovieImage.Location = New System.Drawing.Point(395, 26)
        Me.RBTvMovieImage.Name = "RBTvMovieImage"
        Me.RBTvMovieImage.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.RBTvMovieImage.Size = New System.Drawing.Size(136, 27)
        Me.RBTvMovieImage.TabIndex = 15
        Me.RBTvMovieImage.TabStop = True
        Me.RBTvMovieImage.Text = "TvMovie Image"
        Me.RBTvMovieImage.UseVisualStyleBackColor = True
        '
        'RBEpisodeImage
        '
        Me.RBEpisodeImage.AutoSize = True
        Me.RBEpisodeImage.Dock = System.Windows.Forms.DockStyle.Left
        Me.RBEpisodeImage.Location = New System.Drawing.Point(256, 26)
        Me.RBEpisodeImage.Name = "RBEpisodeImage"
        Me.RBEpisodeImage.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.RBEpisodeImage.Size = New System.Drawing.Size(139, 27)
        Me.RBEpisodeImage.TabIndex = 14
        Me.RBEpisodeImage.TabStop = True
        Me.RBEpisodeImage.Text = "Episoden Image"
        Me.RBEpisodeImage.UseVisualStyleBackColor = True
        '
        'RBSeriesFanArt
        '
        Me.RBSeriesFanArt.AccessibleDescription = "RBSeriesFanArt"
        Me.RBSeriesFanArt.AutoSize = True
        Me.RBSeriesFanArt.Dock = System.Windows.Forms.DockStyle.Left
        Me.RBSeriesFanArt.Location = New System.Drawing.Point(130, 26)
        Me.RBSeriesFanArt.Name = "RBSeriesFanArt"
        Me.RBSeriesFanArt.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.RBSeriesFanArt.Size = New System.Drawing.Size(126, 27)
        Me.RBSeriesFanArt.TabIndex = 13
        Me.RBSeriesFanArt.TabStop = True
        Me.RBSeriesFanArt.Text = "Serien FanArt"
        Me.RBSeriesFanArt.UseVisualStyleBackColor = True
        '
        'RBSeriesCover
        '
        Me.RBSeriesCover.AccessibleDescription = "RBSeriesCover"
        Me.RBSeriesCover.AutoSize = True
        Me.RBSeriesCover.Dock = System.Windows.Forms.DockStyle.Left
        Me.RBSeriesCover.Location = New System.Drawing.Point(10, 26)
        Me.RBSeriesCover.Name = "RBSeriesCover"
        Me.RBSeriesCover.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.RBSeriesCover.Size = New System.Drawing.Size(120, 27)
        Me.RBSeriesCover.TabIndex = 12
        Me.RBSeriesCover.TabStop = True
        Me.RBSeriesCover.Text = "Serien Cover"
        Me.RBSeriesCover.UseVisualStyleBackColor = True
        '
        'CheckBoxUseSeriesDescribtion
        '
        Me.CheckBoxUseSeriesDescribtion.AutoSize = True
        Me.CheckBoxUseSeriesDescribtion.Dock = System.Windows.Forms.DockStyle.Top
        Me.CheckBoxUseSeriesDescribtion.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxUseSeriesDescribtion.Location = New System.Drawing.Point(3, 3)
        Me.CheckBoxUseSeriesDescribtion.Name = "CheckBoxUseSeriesDescribtion"
        Me.CheckBoxUseSeriesDescribtion.Padding = New System.Windows.Forms.Padding(20, 20, 20, 10)
        Me.CheckBoxUseSeriesDescribtion.Size = New System.Drawing.Size(779, 50)
        Me.CheckBoxUseSeriesDescribtion.TabIndex = 10
        Me.CheckBoxUseSeriesDescribtion.Text = "Verwende MP-TvSeries Epsioden Beschreibung bei Serien (sofern aktiviert)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.CheckBoxUseSeriesDescribtion.UseVisualStyleBackColor = True
        '
        'TabOverlay
        '
        Me.TabOverlay.AutoScroll = True
        Me.TabOverlay.Controls.Add(Me.Panel22)
        Me.TabOverlay.Controls.Add(Me.Panel30)
        Me.TabOverlay.Controls.Add(Me.CheckBoxEnableSeriesOverlay)
        Me.TabOverlay.Controls.Add(Me.CheckBoxEnableMovieOverlay)
        Me.TabOverlay.Location = New System.Drawing.Point(4, 25)
        Me.TabOverlay.Name = "TabOverlay"
        Me.TabOverlay.Padding = New System.Windows.Forms.Padding(3)
        Me.TabOverlay.Size = New System.Drawing.Size(785, 702)
        Me.TabOverlay.TabIndex = 5
        Me.TabOverlay.Text = "Overlay"
        Me.TabOverlay.UseVisualStyleBackColor = True
        '
        'Panel22
        '
        Me.Panel22.Controls.Add(Me.GroupBoxMovieOverlay)
        Me.Panel22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel22.Location = New System.Drawing.Point(3, 137)
        Me.Panel22.Name = "Panel22"
        Me.Panel22.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel22.Size = New System.Drawing.Size(779, 344)
        Me.Panel22.TabIndex = 14
        '
        'GroupBoxMovieOverlay
        '
        Me.GroupBoxMovieOverlay.Controls.Add(Me.Panel26)
        Me.GroupBoxMovieOverlay.Controls.Add(Me.Panel25)
        Me.GroupBoxMovieOverlay.Controls.Add(Me.Panel24)
        Me.GroupBoxMovieOverlay.Controls.Add(Me.Panel23)
        Me.GroupBoxMovieOverlay.Controls.Add(Me.CheckBoxOverlayShowLocalMovies)
        Me.GroupBoxMovieOverlay.Controls.Add(Me.CheckBoxOverlayShowTagesTipp)
        Me.GroupBoxMovieOverlay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBoxMovieOverlay.Location = New System.Drawing.Point(10, 10)
        Me.GroupBoxMovieOverlay.Name = "GroupBoxMovieOverlay"
        Me.GroupBoxMovieOverlay.Padding = New System.Windows.Forms.Padding(10)
        Me.GroupBoxMovieOverlay.Size = New System.Drawing.Size(759, 324)
        Me.GroupBoxMovieOverlay.TabIndex = 11
        Me.GroupBoxMovieOverlay.TabStop = False
        Me.GroupBoxMovieOverlay.Text = "Filme Overlay"
        '
        'Panel26
        '
        Me.Panel26.Controls.Add(Me.GroupBox7)
        Me.Panel26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel26.Location = New System.Drawing.Point(10, 241)
        Me.Panel26.Name = "Panel26"
        Me.Panel26.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel26.Size = New System.Drawing.Size(739, 70)
        Me.Panel26.TabIndex = 30
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.RBOverlayStartTime)
        Me.GroupBox7.Controls.Add(Me.RBOverlayTvMovieStar)
        Me.GroupBox7.Controls.Add(Me.RBOverlayRatingStar)
        Me.GroupBox7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox7.Location = New System.Drawing.Point(10, 10)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(719, 50)
        Me.GroupBox7.TabIndex = 10
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "sortieren nach:"
        '
        'RBOverlayStartTime
        '
        Me.RBOverlayStartTime.AutoSize = True
        Me.RBOverlayStartTime.Dock = System.Windows.Forms.DockStyle.Left
        Me.RBOverlayStartTime.Location = New System.Drawing.Point(232, 19)
        Me.RBOverlayStartTime.Name = "RBOverlayStartTime"
        Me.RBOverlayStartTime.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.RBOverlayStartTime.Size = New System.Drawing.Size(96, 28)
        Me.RBOverlayStartTime.TabIndex = 2
        Me.RBOverlayStartTime.TabStop = True
        Me.RBOverlayStartTime.Text = "StartZeit"
        Me.RBOverlayStartTime.UseVisualStyleBackColor = True
        '
        'RBOverlayTvMovieStar
        '
        Me.RBOverlayTvMovieStar.AutoSize = True
        Me.RBOverlayTvMovieStar.Dock = System.Windows.Forms.DockStyle.Left
        Me.RBOverlayTvMovieStar.Location = New System.Drawing.Point(108, 19)
        Me.RBOverlayTvMovieStar.Name = "RBOverlayTvMovieStar"
        Me.RBOverlayTvMovieStar.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.RBOverlayTvMovieStar.Size = New System.Drawing.Size(124, 28)
        Me.RBOverlayTvMovieStar.TabIndex = 1
        Me.RBOverlayTvMovieStar.TabStop = True
        Me.RBOverlayTvMovieStar.Text = "TvMovie Star"
        Me.RBOverlayTvMovieStar.UseVisualStyleBackColor = True
        '
        'RBOverlayRatingStar
        '
        Me.RBOverlayRatingStar.AutoSize = True
        Me.RBOverlayRatingStar.Dock = System.Windows.Forms.DockStyle.Left
        Me.RBOverlayRatingStar.Location = New System.Drawing.Point(3, 19)
        Me.RBOverlayRatingStar.Name = "RBOverlayRatingStar"
        Me.RBOverlayRatingStar.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.RBOverlayRatingStar.Size = New System.Drawing.Size(105, 28)
        Me.RBOverlayRatingStar.TabIndex = 0
        Me.RBOverlayRatingStar.TabStop = True
        Me.RBOverlayRatingStar.Text = "RatingStar"
        Me.RBOverlayRatingStar.UseVisualStyleBackColor = True
        '
        'Panel25
        '
        Me.Panel25.Controls.Add(Me.GroupBox6)
        Me.Panel25.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel25.Location = New System.Drawing.Point(10, 171)
        Me.Panel25.Name = "Panel25"
        Me.Panel25.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel25.Size = New System.Drawing.Size(739, 70)
        Me.Panel25.TabIndex = 29
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.RBOverlayLateTime)
        Me.GroupBox6.Controls.Add(Me.RBOverlayPrimeTime)
        Me.GroupBox6.Controls.Add(Me.RBOverlayNow)
        Me.GroupBox6.Controls.Add(Me.RBOverlayHeute)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox6.Location = New System.Drawing.Point(10, 10)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(719, 50)
        Me.GroupBox6.TabIndex = 9
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Zeige Film Empfehlungen:"
        '
        'RBOverlayLateTime
        '
        Me.RBOverlayLateTime.AutoSize = True
        Me.RBOverlayLateTime.Dock = System.Windows.Forms.DockStyle.Left
        Me.RBOverlayLateTime.Location = New System.Drawing.Point(294, 19)
        Me.RBOverlayLateTime.Name = "RBOverlayLateTime"
        Me.RBOverlayLateTime.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.RBOverlayLateTime.Size = New System.Drawing.Size(122, 28)
        Me.RBOverlayLateTime.TabIndex = 3
        Me.RBOverlayLateTime.TabStop = True
        Me.RBOverlayLateTime.Text = "ab Late Time"
        Me.RBOverlayLateTime.UseVisualStyleBackColor = True
        '
        'RBOverlayPrimeTime
        '
        Me.RBOverlayPrimeTime.AutoSize = True
        Me.RBOverlayPrimeTime.Dock = System.Windows.Forms.DockStyle.Left
        Me.RBOverlayPrimeTime.Location = New System.Drawing.Point(166, 19)
        Me.RBOverlayPrimeTime.Name = "RBOverlayPrimeTime"
        Me.RBOverlayPrimeTime.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.RBOverlayPrimeTime.Size = New System.Drawing.Size(128, 28)
        Me.RBOverlayPrimeTime.TabIndex = 2
        Me.RBOverlayPrimeTime.TabStop = True
        Me.RBOverlayPrimeTime.Text = "ab Prime Time"
        Me.RBOverlayPrimeTime.UseVisualStyleBackColor = True
        '
        'RBOverlayNow
        '
        Me.RBOverlayNow.AutoSize = True
        Me.RBOverlayNow.Dock = System.Windows.Forms.DockStyle.Left
        Me.RBOverlayNow.Location = New System.Drawing.Point(78, 19)
        Me.RBOverlayNow.Name = "RBOverlayNow"
        Me.RBOverlayNow.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.RBOverlayNow.Size = New System.Drawing.Size(88, 28)
        Me.RBOverlayNow.TabIndex = 1
        Me.RBOverlayNow.TabStop = True
        Me.RBOverlayNow.Text = "ab jetzt"
        Me.RBOverlayNow.UseVisualStyleBackColor = True
        '
        'RBOverlayHeute
        '
        Me.RBOverlayHeute.AutoSize = True
        Me.RBOverlayHeute.Dock = System.Windows.Forms.DockStyle.Left
        Me.RBOverlayHeute.Location = New System.Drawing.Point(3, 19)
        Me.RBOverlayHeute.Name = "RBOverlayHeute"
        Me.RBOverlayHeute.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.RBOverlayHeute.Size = New System.Drawing.Size(75, 28)
        Me.RBOverlayHeute.TabIndex = 0
        Me.RBOverlayHeute.TabStop = True
        Me.RBOverlayHeute.Text = "Heute"
        Me.RBOverlayHeute.UseVisualStyleBackColor = True
        '
        'Panel24
        '
        Me.Panel24.Controls.Add(Me.CBOverlayGroup)
        Me.Panel24.Controls.Add(Me.Label22)
        Me.Panel24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel24.Location = New System.Drawing.Point(10, 127)
        Me.Panel24.Name = "Panel24"
        Me.Panel24.Padding = New System.Windows.Forms.Padding(10, 10, 10, 5)
        Me.Panel24.Size = New System.Drawing.Size(739, 44)
        Me.Panel24.TabIndex = 28
        '
        'CBOverlayGroup
        '
        Me.CBOverlayGroup.Dock = System.Windows.Forms.DockStyle.Left
        Me.CBOverlayGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBOverlayGroup.FormattingEnabled = True
        Me.CBOverlayGroup.Location = New System.Drawing.Point(97, 10)
        Me.CBOverlayGroup.Name = "CBOverlayGroup"
        Me.CBOverlayGroup.Size = New System.Drawing.Size(191, 24)
        Me.CBOverlayGroup.TabIndex = 25
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label22.Location = New System.Drawing.Point(10, 10)
        Me.Label22.Name = "Label22"
        Me.Label22.Padding = New System.Windows.Forms.Padding(0, 3, 5, 0)
        Me.Label22.Size = New System.Drawing.Size(87, 19)
        Me.Label22.TabIndex = 26
        Me.Label22.Text = "Tv Gruppe:"
        '
        'Panel23
        '
        Me.Panel23.Controls.Add(Me.NumOverlayLimit)
        Me.Panel23.Controls.Add(Me.Label21)
        Me.Panel23.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel23.Location = New System.Drawing.Point(10, 86)
        Me.Panel23.Name = "Panel23"
        Me.Panel23.Padding = New System.Windows.Forms.Padding(10, 10, 10, 5)
        Me.Panel23.Size = New System.Drawing.Size(739, 41)
        Me.Panel23.TabIndex = 27
        '
        'NumOverlayLimit
        '
        Me.NumOverlayLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumOverlayLimit.Dock = System.Windows.Forms.DockStyle.Left
        Me.NumOverlayLimit.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumOverlayLimit.Location = New System.Drawing.Point(359, 10)
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
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label21.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(10, 10)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(349, 16)
        Me.Label21.TabIndex = 14
        Me.Label21.Text = "Limit (max. Einträge die durchsucht werden sollen):"
        '
        'CheckBoxOverlayShowLocalMovies
        '
        Me.CheckBoxOverlayShowLocalMovies.AutoSize = True
        Me.CheckBoxOverlayShowLocalMovies.Dock = System.Windows.Forms.DockStyle.Top
        Me.CheckBoxOverlayShowLocalMovies.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxOverlayShowLocalMovies.Location = New System.Drawing.Point(10, 56)
        Me.CheckBoxOverlayShowLocalMovies.Name = "CheckBoxOverlayShowLocalMovies"
        Me.CheckBoxOverlayShowLocalMovies.Padding = New System.Windows.Forms.Padding(10, 5, 10, 5)
        Me.CheckBoxOverlayShowLocalMovies.Size = New System.Drawing.Size(739, 30)
        Me.CheckBoxOverlayShowLocalMovies.TabIndex = 8
        Me.CheckBoxOverlayShowLocalMovies.Text = "Keine Filme zeigen, die lokal existieren"
        Me.CheckBoxOverlayShowLocalMovies.UseVisualStyleBackColor = True
        '
        'CheckBoxOverlayShowTagesTipp
        '
        Me.CheckBoxOverlayShowTagesTipp.AutoSize = True
        Me.CheckBoxOverlayShowTagesTipp.Dock = System.Windows.Forms.DockStyle.Top
        Me.CheckBoxOverlayShowTagesTipp.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxOverlayShowTagesTipp.Location = New System.Drawing.Point(10, 26)
        Me.CheckBoxOverlayShowTagesTipp.Name = "CheckBoxOverlayShowTagesTipp"
        Me.CheckBoxOverlayShowTagesTipp.Padding = New System.Windows.Forms.Padding(10, 5, 10, 5)
        Me.CheckBoxOverlayShowTagesTipp.Size = New System.Drawing.Size(739, 30)
        Me.CheckBoxOverlayShowTagesTipp.TabIndex = 7
        Me.CheckBoxOverlayShowTagesTipp.Text = "Zeige Tv Movie Tages Tipp als ersten Eintrag"
        Me.CheckBoxOverlayShowTagesTipp.UseVisualStyleBackColor = True
        '
        'Panel30
        '
        Me.Panel30.Controls.Add(Me.Label33)
        Me.Panel30.Controls.Add(Me.NumUpdateOverlay)
        Me.Panel30.Controls.Add(Me.Label32)
        Me.Panel30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel30.Location = New System.Drawing.Point(3, 93)
        Me.Panel30.Name = "Panel30"
        Me.Panel30.Padding = New System.Windows.Forms.Padding(20, 10, 20, 10)
        Me.Panel30.Size = New System.Drawing.Size(779, 44)
        Me.Panel30.TabIndex = 16
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label33.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(177, 10)
        Me.Label33.Name = "Label33"
        Me.Label33.Padding = New System.Windows.Forms.Padding(2, 4, 10, 0)
        Me.Label33.Size = New System.Drawing.Size(129, 20)
        Me.Label33.TabIndex = 17
        Me.Label33.Text = "min aktualisieren"
        '
        'NumUpdateOverlay
        '
        Me.NumUpdateOverlay.Dock = System.Windows.Forms.DockStyle.Left
        Me.NumUpdateOverlay.Location = New System.Drawing.Point(107, 10)
        Me.NumUpdateOverlay.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        Me.NumUpdateOverlay.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NumUpdateOverlay.Name = "NumUpdateOverlay"
        Me.NumUpdateOverlay.Size = New System.Drawing.Size(70, 23)
        Me.NumUpdateOverlay.TabIndex = 15
        Me.NumUpdateOverlay.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label32.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(20, 10)
        Me.Label32.Name = "Label32"
        Me.Label32.Padding = New System.Windows.Forms.Padding(0, 4, 2, 10)
        Me.Label32.Size = New System.Drawing.Size(87, 30)
        Me.Label32.TabIndex = 16
        Me.Label32.Text = "Overlay alle"
        '
        'CheckBoxEnableSeriesOverlay
        '
        Me.CheckBoxEnableSeriesOverlay.AutoSize = True
        Me.CheckBoxEnableSeriesOverlay.Dock = System.Windows.Forms.DockStyle.Top
        Me.CheckBoxEnableSeriesOverlay.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxEnableSeriesOverlay.Location = New System.Drawing.Point(3, 53)
        Me.CheckBoxEnableSeriesOverlay.Name = "CheckBoxEnableSeriesOverlay"
        Me.CheckBoxEnableSeriesOverlay.Padding = New System.Windows.Forms.Padding(20, 10, 20, 10)
        Me.CheckBoxEnableSeriesOverlay.Size = New System.Drawing.Size(779, 40)
        Me.CheckBoxEnableSeriesOverlay.TabIndex = 13
        Me.CheckBoxEnableSeriesOverlay.Text = "Episoden Overlay aktivieren"
        Me.CheckBoxEnableSeriesOverlay.UseVisualStyleBackColor = True
        '
        'CheckBoxEnableMovieOverlay
        '
        Me.CheckBoxEnableMovieOverlay.AutoSize = True
        Me.CheckBoxEnableMovieOverlay.Dock = System.Windows.Forms.DockStyle.Top
        Me.CheckBoxEnableMovieOverlay.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxEnableMovieOverlay.Location = New System.Drawing.Point(3, 3)
        Me.CheckBoxEnableMovieOverlay.Name = "CheckBoxEnableMovieOverlay"
        Me.CheckBoxEnableMovieOverlay.Padding = New System.Windows.Forms.Padding(20, 20, 20, 10)
        Me.CheckBoxEnableMovieOverlay.Size = New System.Drawing.Size(779, 50)
        Me.CheckBoxEnableMovieOverlay.TabIndex = 12
        Me.CheckBoxEnableMovieOverlay.Text = "Filme Overlay aktivieren"
        Me.CheckBoxEnableMovieOverlay.UseVisualStyleBackColor = True
        '
        'openFileDialog
        '
        Me.openFileDialog.FileName = "OpenFileDialog1"
        '
        'PictureBox2
        '
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox2.Image = Global.ClickfinderProgramGuide.My.Resources.Resources.btn_donate_LG
        Me.PictureBox2.Location = New System.Drawing.Point(2, 2)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(92, 25)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 40
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.ClickfinderProgramGuide.My.Resources.Resources.SetupIcon
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(75, 49)
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.LinkLabel1)
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 800)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel1.Size = New System.Drawing.Size(813, 29)
        Me.Panel1.TabIndex = 41
        '
        'Panel2
        '
        Me.Panel2.AutoScroll = True
        Me.Panel2.Controls.Add(Me.TabControl1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 49)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel2.Size = New System.Drawing.Size(813, 751)
        Me.Panel2.TabIndex = 42
        '
        'LabelVersion
        '
        Me.LabelVersion.AutoSize = True
        Me.LabelVersion.Dock = System.Windows.Forms.DockStyle.Left
        Me.LabelVersion.Location = New System.Drawing.Point(571, 0)
        Me.LabelVersion.Name = "LabelVersion"
        Me.LabelVersion.Padding = New System.Windows.Forms.Padding(0, 18, 0, 0)
        Me.LabelVersion.Size = New System.Drawing.Size(74, 34)
        Me.LabelVersion.TabIndex = 44
        Me.LabelVersion.Text = "#Version"
        Me.LabelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel35
        '
        Me.Panel35.Controls.Add(Me.LabelVersion)
        Me.Panel35.Controls.Add(Me.Label2)
        Me.Panel35.Controls.Add(Me.PictureBox1)
        Me.Panel35.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel35.Location = New System.Drawing.Point(0, 0)
        Me.Panel35.Name = "Panel35"
        Me.Panel35.Size = New System.Drawing.Size(813, 49)
        Me.Panel35.TabIndex = 45
        '
        'Setup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(813, 868)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ButtonSave)
        Me.Controls.Add(Me.Panel35)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Setup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Clickfinder Program Guide Configuration"
        Me.TabControl1.ResumeLayout(False)
        Me.TabAllgemeines.ResumeLayout(False)
        Me.Panel31.ResumeLayout(False)
        Me.Panel31.PerformLayout()
        Me.Panel13.ResumeLayout(False)
        Me.Panel13.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.Panel37.ResumeLayout(False)
        Me.Panel36.ResumeLayout(False)
        Me.Panel36.PerformLayout()
        Me.Panel12.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.Panel11.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        CType(Me.NumPreviewMinTvMovieRating, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumPreviewDays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.Panel29.ResumeLayout(False)
        Me.Panel29.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.TabItems.ResumeLayout(False)
        Me.Panel33.ResumeLayout(False)
        Me.Panel33.PerformLayout()
        Me.Panel32.ResumeLayout(False)
        Me.Panel32.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.Panel34.ResumeLayout(False)
        Me.Panel34.PerformLayout()
        Me.TableItemsPanel.ResumeLayout(False)
        Me.TableItemsPanel.PerformLayout()
        CType(Me.PictureBox12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabÜbersicht.ResumeLayout(False)
        Me.Panel18.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel20.ResumeLayout(False)
        Me.Panel20.PerformLayout()
        CType(Me.NumHighlightsMinRuntime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel19.ResumeLayout(False)
        Me.Panel19.PerformLayout()
        CType(Me.NumShowHighlightsAfter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel15.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel17.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.Panel16.ResumeLayout(False)
        Me.Panel16.PerformLayout()
        CType(Me.NumShowMoviesAfter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel14.ResumeLayout(False)
        Me.Panel14.PerformLayout()
        CType(Me.NumMaxDays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabCategories.ResumeLayout(False)
        Me.TabCategories.PerformLayout()
        Me.Panel27.ResumeLayout(False)
        Me.Panel27.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dgvCategories, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel28.ResumeLayout(False)
        Me.TabDetail.ResumeLayout(False)
        Me.TabDetail.PerformLayout()
        Me.Panel21.ResumeLayout(False)
        Me.GroupDetailSeriesImage.ResumeLayout(False)
        Me.GroupDetailSeriesImage.PerformLayout()
        Me.TabOverlay.ResumeLayout(False)
        Me.TabOverlay.PerformLayout()
        Me.Panel22.ResumeLayout(False)
        Me.GroupBoxMovieOverlay.ResumeLayout(False)
        Me.GroupBoxMovieOverlay.PerformLayout()
        Me.Panel26.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.Panel25.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.Panel24.ResumeLayout(False)
        Me.Panel24.PerformLayout()
        Me.Panel23.ResumeLayout(False)
        Me.Panel23.PerformLayout()
        CType(Me.NumOverlayLimit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel30.ResumeLayout(False)
        Me.Panel30.PerformLayout()
        CType(Me.NumUpdateOverlay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel35.ResumeLayout(False)
        Me.Panel35.PerformLayout()
        Me.ResumeLayout(False)

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
    Friend WithEvents CheckBoxSelect As System.Windows.Forms.CheckBox
    Friend WithEvents TabItems As System.Windows.Forms.TabPage
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents Panel18 As System.Windows.Forms.Panel
    Friend WithEvents Panel20 As System.Windows.Forms.Panel
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Friend WithEvents Panel21 As System.Windows.Forms.Panel
    Friend WithEvents Panel22 As System.Windows.Forms.Panel
    Friend WithEvents Panel23 As System.Windows.Forms.Panel
    Friend WithEvents Panel26 As System.Windows.Forms.Panel
    Friend WithEvents Panel25 As System.Windows.Forms.Panel
    Friend WithEvents Panel24 As System.Windows.Forms.Panel
    Friend WithEvents C_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_Image As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents C_visible As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents C_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_Beschreibung As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_sortOrder As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_SqlString As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_MinRuntime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_NowOffset As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel28 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel27 As System.Windows.Forms.Panel
    Friend WithEvents Panel29 As System.Windows.Forms.Panel
    Friend WithEvents tbEpisodenScanner As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents ButtonOpenDlgEpisodenScanner As System.Windows.Forms.Button
    Friend WithEvents Panel30 As System.Windows.Forms.Panel
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents NumUpdateOverlay As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents CheckBoxUseTheTvDb As MediaPortal.UserInterface.Controls.MPCheckBox
    Friend WithEvents Panel31 As System.Windows.Forms.Panel
    Friend WithEvents ButtonDefaultSettings As System.Windows.Forms.Button
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox11 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox10 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox9 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox8 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox7 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents TableItemsPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel32 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents CBsort9 As System.Windows.Forms.ComboBox
    Friend WithEvents CBTvGroup9 As System.Windows.Forms.ComboBox
    Friend WithEvents CBsort8 As System.Windows.Forms.ComboBox
    Friend WithEvents CBTvGroup8 As System.Windows.Forms.ComboBox
    Friend WithEvents CBsort7 As System.Windows.Forms.ComboBox
    Friend WithEvents CBTvGroup7 As System.Windows.Forms.ComboBox
    Friend WithEvents CBsort6 As System.Windows.Forms.ComboBox
    Friend WithEvents CBTvGroup6 As System.Windows.Forms.ComboBox
    Friend WithEvents CBsort5 As System.Windows.Forms.ComboBox
    Friend WithEvents CBTvGroup5 As System.Windows.Forms.ComboBox
    Friend WithEvents CBsort4 As System.Windows.Forms.ComboBox
    Friend WithEvents CBTvGroup4 As System.Windows.Forms.ComboBox
    Friend WithEvents CBsort3 As System.Windows.Forms.ComboBox
    Friend WithEvents CBTvGroup3 As System.Windows.Forms.ComboBox
    Friend WithEvents CBsort2 As System.Windows.Forms.ComboBox
    Friend WithEvents CBTvGroup2 As System.Windows.Forms.ComboBox
    Friend WithEvents CBsort1 As System.Windows.Forms.ComboBox
    Friend WithEvents CBTvGroup1 As System.Windows.Forms.ComboBox
    Friend WithEvents Panel33 As System.Windows.Forms.Panel
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents BTsetToStandardTvGroup As System.Windows.Forms.Button
    Friend WithEvents BTClearSorting As System.Windows.Forms.Button
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Panel34 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox12 As System.Windows.Forms.PictureBox
    Friend WithEvents CBTvGroup0 As System.Windows.Forms.ComboBox
    Friend WithEvents CBsort0 As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents LabelVersion As System.Windows.Forms.Label
    Friend WithEvents Panel35 As System.Windows.Forms.Panel
    Friend WithEvents Panel37 As System.Windows.Forms.Panel
    Friend WithEvents Panel36 As System.Windows.Forms.Panel
End Class
