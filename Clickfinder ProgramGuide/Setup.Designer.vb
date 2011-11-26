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
        Me.cbRating = New System.Windows.Forms.ComboBox
        Me.tfClickfinderPath = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.CBChannelGroup = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.CBPrimeTimeHour = New System.Windows.Forms.ComboBox
        Me.CBPrimeTimeMinute = New System.Windows.Forms.ComboBox
        Me.CBLateTimeMinute = New System.Windows.Forms.ComboBox
        Me.CBLateTimeHour = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.CBDelayNow = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.CBMinTime = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.CKIgnoreSeries = New System.Windows.Forms.CheckBox
        Me.BtnCreateLogos = New System.Windows.Forms.Button
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.CKRatingTVLogos = New System.Windows.Forms.CheckBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.CBUpdateInterval = New System.Windows.Forms.ComboBox
        Me.CBLiveCorrection = New System.Windows.Forms.CheckBox
        Me.CBWdhCorretcion = New System.Windows.Forms.CheckBox
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.rbVorschau = New System.Windows.Forms.RadioButton
        Me.rbHeute = New System.Windows.Forms.RadioButton
        Me.btSave = New System.Windows.Forms.Button
        Me.Label17 = New System.Windows.Forms.Label
        Me.tfSQL = New System.Windows.Forms.TextBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.Label16 = New System.Windows.Forms.Label
        Me.CBCategorie = New System.Windows.Forms.ComboBox
        Me.tfOrderBy = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.tfWhere = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.lvTagesKategorien = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.SuspendLayout()
        '
        'cbRating
        '
        Me.cbRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbRating.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbRating.FormattingEnabled = True
        Me.cbRating.Items.AddRange(New Object() {"0", "1", "2", "3"})
        Me.cbRating.Location = New System.Drawing.Point(154, 11)
        Me.cbRating.Margin = New System.Windows.Forms.Padding(4)
        Me.cbRating.Name = "cbRating"
        Me.cbRating.Size = New System.Drawing.Size(33, 24)
        Me.cbRating.TabIndex = 5
        '
        'tfClickfinderPath
        '
        Me.tfClickfinderPath.Location = New System.Drawing.Point(237, 21)
        Me.tfClickfinderPath.Margin = New System.Windows.Forms.Padding(4)
        Me.tfClickfinderPath.Name = "tfClickfinderPath"
        Me.tfClickfinderPath.Size = New System.Drawing.Size(326, 23)
        Me.tfClickfinderPath.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 24)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(182, 16)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "TV Movie Clickfinder Path:"
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
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.ClickfinderProgramGuide.My.Resources.Resources.SetupIcon
        Me.PictureBox1.Location = New System.Drawing.Point(3, 4)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(75, 62)
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(59, 18)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(486, 25)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Clickfinder Program Guide Configuration"
        '
        'CBChannelGroup
        '
        Me.CBChannelGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBChannelGroup.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBChannelGroup.FormattingEnabled = True
        Me.CBChannelGroup.Location = New System.Drawing.Point(237, 57)
        Me.CBChannelGroup.Margin = New System.Windows.Forms.Padding(4)
        Me.CBChannelGroup.Name = "CBChannelGroup"
        Me.CBChannelGroup.Size = New System.Drawing.Size(326, 24)
        Me.CBChannelGroup.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 95)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 16)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Prime Time:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(9, 135)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 16)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Late Time:"
        '
        'CBPrimeTimeHour
        '
        Me.CBPrimeTimeHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBPrimeTimeHour.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBPrimeTimeHour.FormattingEnabled = True
        Me.CBPrimeTimeHour.Location = New System.Drawing.Point(154, 92)
        Me.CBPrimeTimeHour.Margin = New System.Windows.Forms.Padding(4)
        Me.CBPrimeTimeHour.Name = "CBPrimeTimeHour"
        Me.CBPrimeTimeHour.Size = New System.Drawing.Size(54, 24)
        Me.CBPrimeTimeHour.TabIndex = 14
        '
        'CBPrimeTimeMinute
        '
        Me.CBPrimeTimeMinute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBPrimeTimeMinute.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBPrimeTimeMinute.FormattingEnabled = True
        Me.CBPrimeTimeMinute.Location = New System.Drawing.Point(227, 92)
        Me.CBPrimeTimeMinute.Margin = New System.Windows.Forms.Padding(4)
        Me.CBPrimeTimeMinute.Name = "CBPrimeTimeMinute"
        Me.CBPrimeTimeMinute.Size = New System.Drawing.Size(54, 24)
        Me.CBPrimeTimeMinute.TabIndex = 15
        '
        'CBLateTimeMinute
        '
        Me.CBLateTimeMinute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBLateTimeMinute.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBLateTimeMinute.FormattingEnabled = True
        Me.CBLateTimeMinute.Location = New System.Drawing.Point(227, 132)
        Me.CBLateTimeMinute.Margin = New System.Windows.Forms.Padding(4)
        Me.CBLateTimeMinute.Name = "CBLateTimeMinute"
        Me.CBLateTimeMinute.Size = New System.Drawing.Size(54, 24)
        Me.CBLateTimeMinute.TabIndex = 17
        '
        'CBLateTimeHour
        '
        Me.CBLateTimeHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBLateTimeHour.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBLateTimeHour.FormattingEnabled = True
        Me.CBLateTimeHour.Location = New System.Drawing.Point(154, 132)
        Me.CBLateTimeHour.Margin = New System.Windows.Forms.Padding(4)
        Me.CBLateTimeHour.Name = "CBLateTimeHour"
        Me.CBLateTimeHour.Size = New System.Drawing.Size(54, 24)
        Me.CBLateTimeHour.TabIndex = 16
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(211, 95)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(13, 16)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = ":"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(211, 135)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(13, 16)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = ":"
        '
        'CBDelayNow
        '
        Me.CBDelayNow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBDelayNow.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBDelayNow.FormattingEnabled = True
        Me.CBDelayNow.Location = New System.Drawing.Point(154, 52)
        Me.CBDelayNow.Margin = New System.Windows.Forms.Padding(4)
        Me.CBDelayNow.Name = "CBDelayNow"
        Me.CBDelayNow.Size = New System.Drawing.Size(54, 24)
        Me.CBDelayNow.TabIndex = 20
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(9, 55)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(47, 16)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Jetzt:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(139, 55)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(15, 16)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "-"
        '
        'CBMinTime
        '
        Me.CBMinTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBMinTime.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBMinTime.FormattingEnabled = True
        Me.CBMinTime.Location = New System.Drawing.Point(154, 172)
        Me.CBMinTime.Margin = New System.Windows.Forms.Padding(4)
        Me.CBMinTime.Name = "CBMinTime"
        Me.CBMinTime.Size = New System.Drawing.Size(54, 24)
        Me.CBMinTime.TabIndex = 23
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(9, 175)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(122, 16)
        Me.Label9.TabIndex = 24
        Me.Label9.Text = "mindest Laufzeit:"
        '
        'CKIgnoreSeries
        '
        Me.CKIgnoreSeries.AutoSize = True
        Me.CKIgnoreSeries.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CKIgnoreSeries.Location = New System.Drawing.Point(227, 174)
        Me.CKIgnoreSeries.Margin = New System.Windows.Forms.Padding(4)
        Me.CKIgnoreSeries.Name = "CKIgnoreSeries"
        Me.CKIgnoreSeries.Size = New System.Drawing.Size(161, 20)
        Me.CKIgnoreSeries.TabIndex = 25
        Me.CKIgnoreSeries.Text = "bei Serien ignorieren"
        Me.CKIgnoreSeries.UseVisualStyleBackColor = True
        '
        'BtnCreateLogos
        '
        Me.BtnCreateLogos.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCreateLogos.Location = New System.Drawing.Point(179, 41)
        Me.BtnCreateLogos.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnCreateLogos.Name = "BtnCreateLogos"
        Me.BtnCreateLogos.Size = New System.Drawing.Size(255, 48)
        Me.BtnCreateLogos.TabIndex = 26
        Me.BtnCreateLogos.Text = "TV Rating Logos erstellen"
        Me.BtnCreateLogos.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(82, 107)
        Me.ProgressBar1.Margin = New System.Windows.Forms.Padding(4)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(284, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar1.TabIndex = 28
        Me.ProgressBar1.Visible = False
        '
        'CKRatingTVLogos
        '
        Me.CKRatingTVLogos.AutoSize = True
        Me.CKRatingTVLogos.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CKRatingTVLogos.Location = New System.Drawing.Point(318, 138)
        Me.CKRatingTVLogos.Margin = New System.Windows.Forms.Padding(4)
        Me.CKRatingTVLogos.Name = "CKRatingTVLogos"
        Me.CKRatingTVLogos.Size = New System.Drawing.Size(211, 20)
        Me.CKRatingTVLogos.TabIndex = 29
        Me.CKRatingTVLogos.Text = "TV Rating Logos verwenden" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.CKRatingTVLogos.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(9, 14)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(144, 16)
        Me.Label10.TabIndex = 30
        Me.Label10.Text = "minimale Bewertung:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(15, 60)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(198, 16)
        Me.Label11.TabIndex = 31
        Me.Label11.Text = "Favoriten Tv Channel Group:"
        '
        'CBUpdateInterval
        '
        Me.CBUpdateInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBUpdateInterval.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBUpdateInterval.FormattingEnabled = True
        Me.CBUpdateInterval.Items.AddRange(New Object() {"1", "2", "5", "7", "10"})
        Me.CBUpdateInterval.Location = New System.Drawing.Point(358, 98)
        Me.CBUpdateInterval.Margin = New System.Windows.Forms.Padding(4)
        Me.CBUpdateInterval.Name = "CBUpdateInterval"
        Me.CBUpdateInterval.Size = New System.Drawing.Size(54, 24)
        Me.CBUpdateInterval.TabIndex = 33
        '
        'CBLiveCorrection
        '
        Me.CBLiveCorrection.AutoSize = True
        Me.CBLiveCorrection.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBLiveCorrection.Location = New System.Drawing.Point(18, 98)
        Me.CBLiveCorrection.Margin = New System.Windows.Forms.Padding(4)
        Me.CBLiveCorrection.Name = "CBLiveCorrection"
        Me.CBLiveCorrection.Size = New System.Drawing.Size(113, 20)
        Me.CBLiveCorrection.TabIndex = 34
        Me.CBLiveCorrection.Text = "Titel + (Live)"
        Me.CBLiveCorrection.UseVisualStyleBackColor = True
        '
        'CBWdhCorretcion
        '
        Me.CBWdhCorretcion.AutoSize = True
        Me.CBWdhCorretcion.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBWdhCorretcion.Location = New System.Drawing.Point(18, 130)
        Me.CBWdhCorretcion.Margin = New System.Windows.Forms.Padding(4)
        Me.CBWdhCorretcion.Name = "CBWdhCorretcion"
        Me.CBWdhCorretcion.Size = New System.Drawing.Size(121, 20)
        Me.CBWdhCorretcion.TabIndex = 35
        Me.CBWdhCorretcion.Text = "Titel + (Wdh.)"
        Me.CBWdhCorretcion.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(12, 73)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(604, 452)
        Me.TabControl1.TabIndex = 36
        '
        'TabPage1
        '
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
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(420, 101)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(41, 16)
        Me.Label13.TabIndex = 37
        Me.Label13.Text = "Tage"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(239, 101)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(111, 16)
        Me.Label12.TabIndex = 36
        Me.Label12.Text = "Update Interval"
        '
        'TabPage2
        '
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
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.ProgressBar1)
        Me.TabPage3.Controls.Add(Me.CKRatingTVLogos)
        Me.TabPage3.Controls.Add(Me.BtnCreateLogos)
        Me.TabPage3.Location = New System.Drawing.Point(4, 25)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(596, 423)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Logos"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Label18)
        Me.TabPage4.Controls.Add(Me.rbVorschau)
        Me.TabPage4.Controls.Add(Me.rbHeute)
        Me.TabPage4.Controls.Add(Me.btSave)
        Me.TabPage4.Controls.Add(Me.Label17)
        Me.TabPage4.Controls.Add(Me.tfSQL)
        Me.TabPage4.Controls.Add(Me.Button2)
        Me.TabPage4.Controls.Add(Me.Label16)
        Me.TabPage4.Controls.Add(Me.CBCategorie)
        Me.TabPage4.Controls.Add(Me.tfOrderBy)
        Me.TabPage4.Controls.Add(Me.Label15)
        Me.TabPage4.Controls.Add(Me.tfWhere)
        Me.TabPage4.Controls.Add(Me.Label14)
        Me.TabPage4.Location = New System.Drawing.Point(4, 25)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(596, 423)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "SQL Strings"
        Me.TabPage4.UseVisualStyleBackColor = True
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
        'tfSQL
        '
        Me.tfSQL.Location = New System.Drawing.Point(117, 259)
        Me.tfSQL.Multiline = True
        Me.tfSQL.Name = "tfSQL"
        Me.tfSQL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tfSQL.Size = New System.Drawing.Size(473, 118)
        Me.tfSQL.TabIndex = 46
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
        'tfOrderBy
        '
        Me.tfOrderBy.Enabled = False
        Me.tfOrderBy.Location = New System.Drawing.Point(117, 228)
        Me.tfOrderBy.Margin = New System.Windows.Forms.Padding(4)
        Me.tfOrderBy.Name = "tfOrderBy"
        Me.tfOrderBy.Size = New System.Drawing.Size(472, 23)
        Me.tfOrderBy.TabIndex = 41
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
        'tfWhere
        '
        Me.tfWhere.Enabled = False
        Me.tfWhere.Location = New System.Drawing.Point(117, 193)
        Me.tfWhere.Margin = New System.Windows.Forms.Padding(4)
        Me.tfWhere.Name = "tfWhere"
        Me.tfWhere.Size = New System.Drawing.Size(472, 23)
        Me.tfWhere.TabIndex = 39
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
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(15, 18)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(552, 81)
        Me.Label18.TabIndex = 53
        Me.Label18.Text = resources.GetString("Label18.Text")
        '
        'TabPage5
        '
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
        Me.lvTagesKategorien.Location = New System.Drawing.Point(211, 116)
        Me.lvTagesKategorien.Name = "lvTagesKategorien"
        Me.lvTagesKategorien.Size = New System.Drawing.Size(272, 180)
        Me.lvTagesKategorien.TabIndex = 0
        Me.lvTagesKategorien.UseCompatibleStateImageBehavior = False
        Me.lvTagesKategorien.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Kategorie"
        Me.ColumnHeader1.Width = 196
        '
        'Setup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(621, 605)
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
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbRating As System.Windows.Forms.ComboBox
    Friend WithEvents tfClickfinderPath As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CBChannelGroup As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CBPrimeTimeHour As System.Windows.Forms.ComboBox
    Friend WithEvents CBPrimeTimeMinute As System.Windows.Forms.ComboBox
    Friend WithEvents CBLateTimeMinute As System.Windows.Forms.ComboBox
    Friend WithEvents CBLateTimeHour As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CBDelayNow As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents CBMinTime As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents CKIgnoreSeries As System.Windows.Forms.CheckBox
    Friend WithEvents BtnCreateLogos As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents CKRatingTVLogos As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents CBUpdateInterval As System.Windows.Forms.ComboBox
    Friend WithEvents CBLiveCorrection As System.Windows.Forms.CheckBox
    Friend WithEvents CBWdhCorretcion As System.Windows.Forms.CheckBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents CBCategorie As System.Windows.Forms.ComboBox
    Friend WithEvents tfOrderBy As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents tfWhere As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents tfSQL As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btSave As System.Windows.Forms.Button
    Friend WithEvents rbVorschau As System.Windows.Forms.RadioButton
    Friend WithEvents rbHeute As System.Windows.Forms.RadioButton
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents lvTagesKategorien As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
End Class
