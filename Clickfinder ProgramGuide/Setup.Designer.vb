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
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.Label14 = New System.Windows.Forms.Label
        Me.tfWhere = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.tfOrderBy = New System.Windows.Forms.TextBox
        Me.CBCategorie = New System.Windows.Forms.ComboBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button
        Me.tfSQL = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.btSave = New System.Windows.Forms.Button
        Me.rbHeute = New System.Windows.Forms.RadioButton
        Me.rbVorschau = New System.Windows.Forms.RadioButton
        Me.Label18 = New System.Windows.Forms.Label
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.lvTagesKategorien = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.lvTagesCategorieChoosen = New System.Windows.Forms.ListView
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.lvVorschauKategorien = New System.Windows.Forms.ListView
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.lvVorschauCategorieChoosen = New System.Windows.Forms.ListView
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.btTagesUp = New System.Windows.Forms.Button
        Me.btTagesDown = New System.Windows.Forms.Button
        Me.btVorschauUp = New System.Windows.Forms.Button
        Me.btVorschauDown = New System.Windows.Forms.Button
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.Label22 = New System.Windows.Forms.Label
        Me.BtnCreateLogos = New System.Windows.Forms.Button
        Me.CKRatingTVLogos = New System.Windows.Forms.CheckBox
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.PictureBox4 = New System.Windows.Forms.PictureBox
        Me.PictureBox5 = New System.Windows.Forms.PictureBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.CBMinTime = New System.Windows.Forms.ComboBox
        Me.CBLateTimeMinute = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.CBLateTimeHour = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.CBPrimeTimeMinute = New System.Windows.Forms.ComboBox
        Me.CKIgnoreSeries = New System.Windows.Forms.CheckBox
        Me.CBPrimeTimeHour = New System.Windows.Forms.ComboBox
        Me.cbRating = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.CBDelayNow = New System.Windows.Forms.ComboBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.Label11 = New System.Windows.Forms.Label
        Me.CBChannelGroup = New System.Windows.Forms.ComboBox
        Me.CBLiveCorrection = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.CBWdhCorretcion = New System.Windows.Forms.CheckBox
        Me.CBUpdateInterval = New System.Windows.Forms.ComboBox
        Me.tfClickfinderPath = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.CBListOffsetY = New System.Windows.Forms.ComboBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.CBTvSeries = New System.Windows.Forms.CheckBox
        Me.CBTvSeriesBeschreibung = New System.Windows.Forms.CheckBox
        Me.CBTvSeriesTvServerWrite = New System.Windows.Forms.CheckBox
        Me.TabPage6 = New System.Windows.Forms.TabPage
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(529, 553)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(79, 39)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "speichern"
        Me.Button1.UseVisualStyleBackColor = True
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
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.Location = New System.Drawing.Point(13, 565)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(194, 14)
        Me.LinkLabel1.TabIndex = 38
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Tag = ""
        Me.LinkLabel1.Text = "Ausführliche Anleitung im Wiki"
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Label18)
        Me.TabPage4.Controls.Add(Me.rbVorschau)
        Me.TabPage4.Controls.Add(Me.rbHeute)
        Me.TabPage4.Controls.Add(Me.btSave)
        Me.TabPage4.Controls.Add(Me.Label17)
        Me.TabPage4.Controls.Add(Me.tfSQL)
        Me.TabPage4.Controls.Add(Me.tfOrderBy)
        Me.TabPage4.Controls.Add(Me.tfWhere)
        Me.TabPage4.Controls.Add(Me.Button2)
        Me.TabPage4.Controls.Add(Me.Label16)
        Me.TabPage4.Controls.Add(Me.CBCategorie)
        Me.TabPage4.Controls.Add(Me.Label15)
        Me.TabPage4.Controls.Add(Me.Label14)
        Me.TabPage4.Location = New System.Drawing.Point(4, 25)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(596, 423)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "SQL Strings"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(15, 196)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(56, 16)
        Me.Label14.TabIndex = 38
        Me.Label14.Text = "Where:"
        '
        'tfWhere
        '
        Me.tfWhere.Enabled = False
        Me.tfWhere.Location = New System.Drawing.Point(117, 193)
        Me.tfWhere.Margin = New System.Windows.Forms.Padding(4)
        Me.tfWhere.Name = "tfWhere"
        Me.tfWhere.Size = New System.Drawing.Size(472, 23)
        Me.tfWhere.TabIndex = 39
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(15, 231)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(66, 16)
        Me.Label15.TabIndex = 40
        Me.Label15.Text = "OrderBy:"
        '
        'tfOrderBy
        '
        Me.tfOrderBy.Enabled = False
        Me.tfOrderBy.Location = New System.Drawing.Point(117, 228)
        Me.tfOrderBy.Margin = New System.Windows.Forms.Padding(4)
        Me.tfOrderBy.Name = "tfOrderBy"
        Me.tfOrderBy.Size = New System.Drawing.Size(472, 23)
        Me.tfOrderBy.TabIndex = 41
        '
        'CBCategorie
        '
        Me.CBCategorie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBCategorie.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBCategorie.FormattingEnabled = True
        Me.CBCategorie.Location = New System.Drawing.Point(117, 158)
        Me.CBCategorie.Margin = New System.Windows.Forms.Padding(4)
        Me.CBCategorie.Name = "CBCategorie"
        Me.CBCategorie.Size = New System.Drawing.Size(224, 24)
        Me.CBCategorie.TabIndex = 42
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(15, 161)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(76, 16)
        Me.Label16.TabIndex = 43
        Me.Label16.Text = "Kategorie:"
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(452, 158)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(137, 24)
        Me.Button2.TabIndex = 45
        Me.Button2.Text = "bearbeiten"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'tfSQL
        '
        Me.tfSQL.Location = New System.Drawing.Point(117, 259)
        Me.tfSQL.Multiline = True
        Me.tfSQL.Name = "tfSQL"
        Me.tfSQL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tfSQL.Size = New System.Drawing.Size(473, 118)
        Me.tfSQL.TabIndex = 46
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(15, 259)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(85, 16)
        Me.Label17.TabIndex = 47
        Me.Label17.Text = "SQL Befehl:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'btSave
        '
        Me.btSave.Enabled = False
        Me.btSave.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btSave.Location = New System.Drawing.Point(422, 383)
        Me.btSave.Name = "btSave"
        Me.btSave.Size = New System.Drawing.Size(168, 33)
        Me.btSave.TabIndex = 48
        Me.btSave.Text = "SQL Strings speichern"
        Me.btSave.UseVisualStyleBackColor = True
        '
        'rbHeute
        '
        Me.rbHeute.AutoSize = True
        Me.rbHeute.Location = New System.Drawing.Point(18, 122)
        Me.rbHeute.Name = "rbHeute"
        Me.rbHeute.Size = New System.Drawing.Size(251, 20)
        Me.rbHeute.TabIndex = 51
        Me.rbHeute.TabStop = True
        Me.rbHeute.Text = "SQL Strings des Tages bearbeiten"
        Me.rbHeute.UseVisualStyleBackColor = True
        '
        'rbVorschau
        '
        Me.rbVorschau.AutoSize = True
        Me.rbVorschau.Location = New System.Drawing.Point(297, 122)
        Me.rbVorschau.Name = "rbVorschau"
        Me.rbVorschau.Size = New System.Drawing.Size(270, 20)
        Me.rbVorschau.TabIndex = 52
        Me.rbVorschau.TabStop = True
        Me.rbVorschau.Text = "SQL Strings der Vorschau bearbeiten"
        Me.rbVorschau.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(15, 18)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(552, 101)
        Me.Label18.TabIndex = 53
        Me.Label18.Text = resources.GetString("Label18.Text")
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.Label21)
        Me.TabPage5.Controls.Add(Me.Label20)
        Me.TabPage5.Controls.Add(Me.Label19)
        Me.TabPage5.Controls.Add(Me.btVorschauDown)
        Me.TabPage5.Controls.Add(Me.btVorschauUp)
        Me.TabPage5.Controls.Add(Me.btTagesDown)
        Me.TabPage5.Controls.Add(Me.btTagesUp)
        Me.TabPage5.Controls.Add(Me.lvVorschauCategorieChoosen)
        Me.TabPage5.Controls.Add(Me.lvVorschauKategorien)
        Me.TabPage5.Controls.Add(Me.lvTagesCategorieChoosen)
        Me.TabPage5.Controls.Add(Me.lvTagesKategorien)
        Me.TabPage5.Location = New System.Drawing.Point(4, 25)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(596, 423)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Kategorien"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'lvTagesKategorien
        '
        Me.lvTagesKategorien.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.lvTagesKategorien.Location = New System.Drawing.Point(57, 43)
        Me.lvTagesKategorien.Name = "lvTagesKategorien"
        Me.lvTagesKategorien.Size = New System.Drawing.Size(178, 143)
        Me.lvTagesKategorien.TabIndex = 0
        Me.lvTagesKategorien.UseCompatibleStateImageBehavior = False
        Me.lvTagesKategorien.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Kategorien"
        Me.ColumnHeader1.Width = 156
        '
        'lvTagesCategorieChoosen
        '
        Me.lvTagesCategorieChoosen.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2})
        Me.lvTagesCategorieChoosen.Location = New System.Drawing.Point(341, 43)
        Me.lvTagesCategorieChoosen.Name = "lvTagesCategorieChoosen"
        Me.lvTagesCategorieChoosen.Size = New System.Drawing.Size(178, 143)
        Me.lvTagesCategorieChoosen.TabIndex = 1
        Me.lvTagesCategorieChoosen.UseCompatibleStateImageBehavior = False
        Me.lvTagesCategorieChoosen.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "gewählte Kategorien"
        Me.ColumnHeader2.Width = 156
        '
        'lvVorschauKategorien
        '
        Me.lvVorschauKategorien.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3})
        Me.lvVorschauKategorien.Location = New System.Drawing.Point(57, 231)
        Me.lvVorschauKategorien.Name = "lvVorschauKategorien"
        Me.lvVorschauKategorien.Size = New System.Drawing.Size(178, 143)
        Me.lvVorschauKategorien.TabIndex = 2
        Me.lvVorschauKategorien.UseCompatibleStateImageBehavior = False
        Me.lvVorschauKategorien.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Kategorie"
        Me.ColumnHeader3.Width = 156
        '
        'lvVorschauCategorieChoosen
        '
        Me.lvVorschauCategorieChoosen.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader4})
        Me.lvVorschauCategorieChoosen.Location = New System.Drawing.Point(341, 231)
        Me.lvVorschauCategorieChoosen.Name = "lvVorschauCategorieChoosen"
        Me.lvVorschauCategorieChoosen.Size = New System.Drawing.Size(178, 143)
        Me.lvVorschauCategorieChoosen.TabIndex = 3
        Me.lvVorschauCategorieChoosen.UseCompatibleStateImageBehavior = False
        Me.lvVorschauCategorieChoosen.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "gewählte Kategorien"
        Me.ColumnHeader4.Width = 156
        '
        'btTagesUp
        '
        Me.btTagesUp.BackgroundImage = CType(resources.GetObject("btTagesUp.BackgroundImage"), System.Drawing.Image)
        Me.btTagesUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btTagesUp.Location = New System.Drawing.Point(525, 67)
        Me.btTagesUp.Name = "btTagesUp"
        Me.btTagesUp.Size = New System.Drawing.Size(31, 41)
        Me.btTagesUp.TabIndex = 4
        Me.btTagesUp.UseVisualStyleBackColor = True
        '
        'btTagesDown
        '
        Me.btTagesDown.BackgroundImage = Global.ClickfinderProgramGuide.My.Resources.Resources.arrow_Down_48
        Me.btTagesDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btTagesDown.Location = New System.Drawing.Point(525, 114)
        Me.btTagesDown.Name = "btTagesDown"
        Me.btTagesDown.Size = New System.Drawing.Size(31, 41)
        Me.btTagesDown.TabIndex = 5
        Me.btTagesDown.UseVisualStyleBackColor = True
        '
        'btVorschauUp
        '
        Me.btVorschauUp.BackgroundImage = CType(resources.GetObject("btVorschauUp.BackgroundImage"), System.Drawing.Image)
        Me.btVorschauUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btVorschauUp.Location = New System.Drawing.Point(525, 258)
        Me.btVorschauUp.Name = "btVorschauUp"
        Me.btVorschauUp.Size = New System.Drawing.Size(31, 41)
        Me.btVorschauUp.TabIndex = 6
        Me.btVorschauUp.UseVisualStyleBackColor = True
        '
        'btVorschauDown
        '
        Me.btVorschauDown.BackgroundImage = Global.ClickfinderProgramGuide.My.Resources.Resources.arrow_Down_48
        Me.btVorschauDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btVorschauDown.Location = New System.Drawing.Point(525, 305)
        Me.btVorschauDown.Name = "btVorschauDown"
        Me.btVorschauDown.Size = New System.Drawing.Size(31, 41)
        Me.btVorschauDown.TabIndex = 7
        Me.btVorschauDown.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(16, 18)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(349, 16)
        Me.Label19.TabIndex = 31
        Me.Label19.Text = "angezeigte Kategorien in den Ansichten des Tages:"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(16, 206)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(323, 16)
        Me.Label20.TabIndex = 32
        Me.Label20.Text = "angezeigte Kategorien in der Ansicht Vorschau:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(127, 396)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(334, 13)
        Me.Label21.TabIndex = 33
        Me.Label21.Text = "(DoppelKlick auf Eintrag zum Hinzufügen oder Entfernen)"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.PictureBox5)
        Me.TabPage3.Controls.Add(Me.PictureBox4)
        Me.TabPage3.Controls.Add(Me.PictureBox3)
        Me.TabPage3.Controls.Add(Me.PictureBox2)
        Me.TabPage3.Controls.Add(Me.ProgressBar1)
        Me.TabPage3.Controls.Add(Me.CKRatingTVLogos)
        Me.TabPage3.Controls.Add(Me.BtnCreateLogos)
        Me.TabPage3.Controls.Add(Me.Label22)
        Me.TabPage3.Location = New System.Drawing.Point(4, 25)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(596, 423)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Logos"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(21, 136)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(552, 101)
        Me.Label22.TabIndex = 54
        Me.Label22.Text = resources.GetString("Label22.Text")
        '
        'BtnCreateLogos
        '
        Me.BtnCreateLogos.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCreateLogos.Location = New System.Drawing.Point(322, 236)
        Me.BtnCreateLogos.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnCreateLogos.Name = "BtnCreateLogos"
        Me.BtnCreateLogos.Size = New System.Drawing.Size(249, 48)
        Me.BtnCreateLogos.TabIndex = 26
        Me.BtnCreateLogos.Text = "TV Rating Logos erstellen"
        Me.BtnCreateLogos.UseVisualStyleBackColor = True
        '
        'CKRatingTVLogos
        '
        Me.CKRatingTVLogos.AutoSize = True
        Me.CKRatingTVLogos.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CKRatingTVLogos.Location = New System.Drawing.Point(343, 292)
        Me.CKRatingTVLogos.Margin = New System.Windows.Forms.Padding(4)
        Me.CKRatingTVLogos.Name = "CKRatingTVLogos"
        Me.CKRatingTVLogos.Size = New System.Drawing.Size(211, 20)
        Me.CKRatingTVLogos.TabIndex = 29
        Me.CKRatingTVLogos.Text = "TV Rating Logos verwenden" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.CKRatingTVLogos.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(24, 251)
        Me.ProgressBar1.Margin = New System.Windows.Forms.Padding(4)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(249, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar1.TabIndex = 28
        Me.ProgressBar1.Visible = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.ClickfinderProgramGuide.My.Resources.Resources.Das_Erste_HD_3
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox2.Location = New System.Drawing.Point(24, 18)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(100, 100)
        Me.PictureBox2.TabIndex = 55
        Me.PictureBox2.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.BackgroundImage = Global.ClickfinderProgramGuide.My.Resources.Resources.History_2
        Me.PictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox3.Location = New System.Drawing.Point(173, 18)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(100, 100)
        Me.PictureBox3.TabIndex = 56
        Me.PictureBox3.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.BackgroundImage = Global.ClickfinderProgramGuide.My.Resources.Resources.ProSieben_1
        Me.PictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox4.Location = New System.Drawing.Point(471, 18)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(100, 100)
        Me.PictureBox4.TabIndex = 57
        Me.PictureBox4.TabStop = False
        '
        'PictureBox5
        '
        Me.PictureBox5.BackgroundImage = Global.ClickfinderProgramGuide.My.Resources.Resources.Phoenix_1
        Me.PictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox5.Location = New System.Drawing.Point(322, 18)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(100, 100)
        Me.PictureBox5.TabIndex = 58
        Me.PictureBox5.TabStop = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Label23)
        Me.TabPage2.Controls.Add(Me.CBDelayNow)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Label10)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.cbRating)
        Me.TabPage2.Controls.Add(Me.CBPrimeTimeHour)
        Me.TabPage2.Controls.Add(Me.CKIgnoreSeries)
        Me.TabPage2.Controls.Add(Me.CBPrimeTimeMinute)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.CBLateTimeHour)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.CBLateTimeMinute)
        Me.TabPage2.Controls.Add(Me.CBMinTime)
        Me.TabPage2.Controls.Add(Me.Label9)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(596, 423)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Filter"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(241, 162)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(13, 16)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = ":"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(32, 242)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(122, 16)
        Me.Label9.TabIndex = 24
        Me.Label9.Text = "mindest Laufzeit:"
        '
        'CBMinTime
        '
        Me.CBMinTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBMinTime.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBMinTime.FormattingEnabled = True
        Me.CBMinTime.Location = New System.Drawing.Point(184, 239)
        Me.CBMinTime.Margin = New System.Windows.Forms.Padding(4)
        Me.CBMinTime.Name = "CBMinTime"
        Me.CBMinTime.Size = New System.Drawing.Size(54, 24)
        Me.CBMinTime.TabIndex = 23
        '
        'CBLateTimeMinute
        '
        Me.CBLateTimeMinute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBLateTimeMinute.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBLateTimeMinute.FormattingEnabled = True
        Me.CBLateTimeMinute.Location = New System.Drawing.Point(257, 199)
        Me.CBLateTimeMinute.Margin = New System.Windows.Forms.Padding(4)
        Me.CBLateTimeMinute.Name = "CBLateTimeMinute"
        Me.CBLateTimeMinute.Size = New System.Drawing.Size(54, 24)
        Me.CBLateTimeMinute.TabIndex = 17
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(241, 202)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(13, 16)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = ":"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(169, 122)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(15, 16)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "-"
        '
        'CBLateTimeHour
        '
        Me.CBLateTimeHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBLateTimeHour.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBLateTimeHour.FormattingEnabled = True
        Me.CBLateTimeHour.Location = New System.Drawing.Point(184, 199)
        Me.CBLateTimeHour.Margin = New System.Windows.Forms.Padding(4)
        Me.CBLateTimeHour.Name = "CBLateTimeHour"
        Me.CBLateTimeHour.Size = New System.Drawing.Size(54, 24)
        Me.CBLateTimeHour.TabIndex = 16
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(32, 122)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(47, 16)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Jetzt:"
        '
        'CBPrimeTimeMinute
        '
        Me.CBPrimeTimeMinute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBPrimeTimeMinute.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBPrimeTimeMinute.FormattingEnabled = True
        Me.CBPrimeTimeMinute.Location = New System.Drawing.Point(257, 159)
        Me.CBPrimeTimeMinute.Margin = New System.Windows.Forms.Padding(4)
        Me.CBPrimeTimeMinute.Name = "CBPrimeTimeMinute"
        Me.CBPrimeTimeMinute.Size = New System.Drawing.Size(54, 24)
        Me.CBPrimeTimeMinute.TabIndex = 15
        '
        'CKIgnoreSeries
        '
        Me.CKIgnoreSeries.AutoSize = True
        Me.CKIgnoreSeries.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CKIgnoreSeries.Location = New System.Drawing.Point(257, 241)
        Me.CKIgnoreSeries.Margin = New System.Windows.Forms.Padding(4)
        Me.CKIgnoreSeries.Name = "CKIgnoreSeries"
        Me.CKIgnoreSeries.Size = New System.Drawing.Size(161, 20)
        Me.CKIgnoreSeries.TabIndex = 25
        Me.CKIgnoreSeries.Text = "bei Serien ignorieren"
        Me.CKIgnoreSeries.UseVisualStyleBackColor = True
        '
        'CBPrimeTimeHour
        '
        Me.CBPrimeTimeHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBPrimeTimeHour.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBPrimeTimeHour.FormattingEnabled = True
        Me.CBPrimeTimeHour.Location = New System.Drawing.Point(184, 159)
        Me.CBPrimeTimeHour.Margin = New System.Windows.Forms.Padding(4)
        Me.CBPrimeTimeHour.Name = "CBPrimeTimeHour"
        Me.CBPrimeTimeHour.Size = New System.Drawing.Size(54, 24)
        Me.CBPrimeTimeHour.TabIndex = 14
        '
        'cbRating
        '
        Me.cbRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbRating.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbRating.FormattingEnabled = True
        Me.cbRating.Items.AddRange(New Object() {"0", "1", "2", "3"})
        Me.cbRating.Location = New System.Drawing.Point(184, 73)
        Me.cbRating.Margin = New System.Windows.Forms.Padding(4)
        Me.cbRating.Name = "cbRating"
        Me.cbRating.Size = New System.Drawing.Size(33, 24)
        Me.cbRating.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(32, 202)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 16)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Late Time:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(32, 81)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(144, 16)
        Me.Label10.TabIndex = 30
        Me.Label10.Text = "minimale Bewertung:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(32, 162)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 16)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Prime Time:"
        '
        'CBDelayNow
        '
        Me.CBDelayNow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBDelayNow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBDelayNow.FormattingEnabled = True
        Me.CBDelayNow.Location = New System.Drawing.Point(184, 119)
        Me.CBDelayNow.Margin = New System.Windows.Forms.Padding(4)
        Me.CBDelayNow.Name = "CBDelayNow"
        Me.CBDelayNow.Size = New System.Drawing.Size(54, 24)
        Me.CBDelayNow.TabIndex = 20
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(17, 26)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(552, 39)
        Me.Label23.TabIndex = 55
        Me.Label23.Text = "Mit den verschiedenen Filtereinstellungen könnt ihr die Suchergebnisse eingrenzen" & _
            " und die Zugriffsgeschwindigkeit steigern"
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label24)
        Me.TabPage1.Controls.Add(Me.CBListOffsetY)
        Me.TabPage1.Controls.Add(Me.Label13)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Controls.Add(Me.tfClickfinderPath)
        Me.TabPage1.Controls.Add(Me.CBUpdateInterval)
        Me.TabPage1.Controls.Add(Me.CBWdhCorretcion)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.CBLiveCorrection)
        Me.TabPage1.Controls.Add(Me.CBChannelGroup)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(596, 423)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Allgemeines"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(24, 63)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(198, 16)
        Me.Label11.TabIndex = 31
        Me.Label11.Text = "Favoriten Tv Channel Group:"
        '
        'CBChannelGroup
        '
        Me.CBChannelGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBChannelGroup.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBChannelGroup.FormattingEnabled = True
        Me.CBChannelGroup.Location = New System.Drawing.Point(246, 60)
        Me.CBChannelGroup.Margin = New System.Windows.Forms.Padding(4)
        Me.CBChannelGroup.Name = "CBChannelGroup"
        Me.CBChannelGroup.Size = New System.Drawing.Size(326, 24)
        Me.CBChannelGroup.TabIndex = 11
        '
        'CBLiveCorrection
        '
        Me.CBLiveCorrection.AutoSize = True
        Me.CBLiveCorrection.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBLiveCorrection.Location = New System.Drawing.Point(27, 105)
        Me.CBLiveCorrection.Margin = New System.Windows.Forms.Padding(4)
        Me.CBLiveCorrection.Name = "CBLiveCorrection"
        Me.CBLiveCorrection.Size = New System.Drawing.Size(113, 20)
        Me.CBLiveCorrection.TabIndex = 34
        Me.CBLiveCorrection.Text = "Titel + (Live)"
        Me.CBLiveCorrection.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(24, 27)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(182, 16)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "TV Movie Clickfinder Path:"
        '
        'CBWdhCorretcion
        '
        Me.CBWdhCorretcion.AutoSize = True
        Me.CBWdhCorretcion.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBWdhCorretcion.Location = New System.Drawing.Point(27, 137)
        Me.CBWdhCorretcion.Margin = New System.Windows.Forms.Padding(4)
        Me.CBWdhCorretcion.Name = "CBWdhCorretcion"
        Me.CBWdhCorretcion.Size = New System.Drawing.Size(121, 20)
        Me.CBWdhCorretcion.TabIndex = 35
        Me.CBWdhCorretcion.Text = "Titel + (Wdh.)"
        Me.CBWdhCorretcion.UseVisualStyleBackColor = True
        '
        'CBUpdateInterval
        '
        Me.CBUpdateInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBUpdateInterval.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBUpdateInterval.FormattingEnabled = True
        Me.CBUpdateInterval.Items.AddRange(New Object() {"1", "2", "5", "7", "10", "14"})
        Me.CBUpdateInterval.Location = New System.Drawing.Point(395, 99)
        Me.CBUpdateInterval.Margin = New System.Windows.Forms.Padding(4)
        Me.CBUpdateInterval.Name = "CBUpdateInterval"
        Me.CBUpdateInterval.Size = New System.Drawing.Size(54, 24)
        Me.CBUpdateInterval.TabIndex = 33
        '
        'tfClickfinderPath
        '
        Me.tfClickfinderPath.Location = New System.Drawing.Point(246, 24)
        Me.tfClickfinderPath.Margin = New System.Windows.Forms.Padding(4)
        Me.tfClickfinderPath.Name = "tfClickfinderPath"
        Me.tfClickfinderPath.Size = New System.Drawing.Size(326, 23)
        Me.tfClickfinderPath.TabIndex = 7
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(315, 102)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(72, 16)
        Me.Label12.TabIndex = 36
        Me.Label12.Text = "Rating für"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(457, 105)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(115, 16)
        Me.Label13.TabIndex = 37
        Me.Label13.Text = "Tage berechnen"
        '
        'CBListOffsetY
        '
        Me.CBListOffsetY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBListOffsetY.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBListOffsetY.FormattingEnabled = True
        Me.CBListOffsetY.Location = New System.Drawing.Point(246, 196)
        Me.CBListOffsetY.Margin = New System.Windows.Forms.Padding(4)
        Me.CBListOffsetY.Name = "CBListOffsetY"
        Me.CBListOffsetY.Size = New System.Drawing.Size(54, 24)
        Me.CBListOffsetY.TabIndex = 38
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(24, 199)
        Me.Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(185, 16)
        Me.Label24.TabIndex = 39
        Me.Label24.Text = "Kategorien Label Y-Offset:"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(12, 73)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(604, 452)
        Me.TabControl1.TabIndex = 36
        '
        'CBTvSeries
        '
        Me.CBTvSeries.AutoSize = True
        Me.CBTvSeries.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBTvSeries.Location = New System.Drawing.Point(19, 17)
        Me.CBTvSeries.Margin = New System.Windows.Forms.Padding(4)
        Me.CBTvSeries.Name = "CBTvSeries"
        Me.CBTvSeries.Size = New System.Drawing.Size(475, 20)
        Me.CBTvSeries.TabIndex = 30
        Me.CBTvSeries.Text = "Schnittstelle zu MP-TVSeries aktivieren (funktioniert noch nicht !!!)"
        Me.CBTvSeries.UseVisualStyleBackColor = True
        '
        'CBTvSeriesBeschreibung
        '
        Me.CBTvSeriesBeschreibung.AutoSize = True
        Me.CBTvSeriesBeschreibung.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBTvSeriesBeschreibung.Location = New System.Drawing.Point(19, 77)
        Me.CBTvSeriesBeschreibung.Margin = New System.Windows.Forms.Padding(4)
        Me.CBTvSeriesBeschreibung.Name = "CBTvSeriesBeschreibung"
        Me.CBTvSeriesBeschreibung.Size = New System.Drawing.Size(493, 20)
        Me.CBTvSeriesBeschreibung.TabIndex = 31
        Me.CBTvSeriesBeschreibung.Text = "Zeige MP-TVSeries Beschreibung, anstatt der Clickfinder Beschreibung"
        Me.CBTvSeriesBeschreibung.UseVisualStyleBackColor = True
        '
        'CBTvSeriesTvServerWrite
        '
        Me.CBTvSeriesTvServerWrite.AutoSize = True
        Me.CBTvSeriesTvServerWrite.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBTvSeriesTvServerWrite.Location = New System.Drawing.Point(19, 105)
        Me.CBTvSeriesTvServerWrite.Margin = New System.Windows.Forms.Padding(4)
        Me.CBTvSeriesTvServerWrite.Name = "CBTvSeriesTvServerWrite"
        Me.CBTvSeriesTvServerWrite.Size = New System.Drawing.Size(516, 20)
        Me.CBTvSeriesTvServerWrite.TabIndex = 32
        Me.CBTvSeriesTvServerWrite.Text = "Schreibe gefundene Staffel- und Episodennummer ins EPG des TV Servers"
        Me.CBTvSeriesTvServerWrite.UseVisualStyleBackColor = True
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.CBTvSeriesTvServerWrite)
        Me.TabPage6.Controls.Add(Me.CBTvSeriesBeschreibung)
        Me.TabPage6.Controls.Add(Me.CBTvSeries)
        Me.TabPage6.Location = New System.Drawing.Point(4, 25)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(596, 423)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "MP-TVSeries"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'Setup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(621, 605)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Button1)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Setup"
        Me.Text = "Clickfinder Program Guide Configuration"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage6.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents rbVorschau As System.Windows.Forms.RadioButton
    Friend WithEvents rbHeute As System.Windows.Forms.RadioButton
    Friend WithEvents btSave As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents tfSQL As System.Windows.Forms.TextBox
    Friend WithEvents tfOrderBy As System.Windows.Forms.TextBox
    Friend WithEvents tfWhere As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents CBCategorie As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents btVorschauDown As System.Windows.Forms.Button
    Friend WithEvents btVorschauUp As System.Windows.Forms.Button
    Friend WithEvents btTagesDown As System.Windows.Forms.Button
    Friend WithEvents btTagesUp As System.Windows.Forms.Button
    Friend WithEvents lvVorschauCategorieChoosen As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvVorschauKategorien As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvTagesCategorieChoosen As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvTagesKategorien As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents CKRatingTVLogos As System.Windows.Forms.CheckBox
    Friend WithEvents BtnCreateLogos As System.Windows.Forms.Button
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents CBDelayNow As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbRating As System.Windows.Forms.ComboBox
    Friend WithEvents CBPrimeTimeHour As System.Windows.Forms.ComboBox
    Friend WithEvents CKIgnoreSeries As System.Windows.Forms.CheckBox
    Friend WithEvents CBPrimeTimeMinute As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents CBLateTimeHour As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CBLateTimeMinute As System.Windows.Forms.ComboBox
    Friend WithEvents CBMinTime As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents CBListOffsetY As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents tfClickfinderPath As System.Windows.Forms.TextBox
    Friend WithEvents CBUpdateInterval As System.Windows.Forms.ComboBox
    Friend WithEvents CBWdhCorretcion As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CBLiveCorrection As System.Windows.Forms.CheckBox
    Friend WithEvents CBChannelGroup As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents CBTvSeriesTvServerWrite As System.Windows.Forms.CheckBox
    Friend WithEvents CBTvSeriesBeschreibung As System.Windows.Forms.CheckBox
    Friend WithEvents CBTvSeries As System.Windows.Forms.CheckBox
End Class
