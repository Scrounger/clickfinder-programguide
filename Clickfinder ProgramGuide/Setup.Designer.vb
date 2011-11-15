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
        Me.Button2 = New System.Windows.Forms.Button
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.CKRatingTVLogos = New System.Windows.Forms.CheckBox
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cbRating
        '
        Me.cbRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbRating.FormattingEnabled = True
        Me.cbRating.Items.AddRange(New Object() {"0", "1", "2", "3"})
        Me.cbRating.Location = New System.Drawing.Point(148, 110)
        Me.cbRating.Name = "cbRating"
        Me.cbRating.Size = New System.Drawing.Size(79, 21)
        Me.cbRating.TabIndex = 5
        '
        'tfClickfinderPath
        '
        Me.tfClickfinderPath.Location = New System.Drawing.Point(148, 84)
        Me.tfClickfinderPath.Name = "tfClickfinderPath"
        Me.tfClickfinderPath.Size = New System.Drawing.Size(200, 20)
        Me.tfClickfinderPath.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 87)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(130, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "TV Movie Clickfinder Path"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(464, 238)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(222, 32)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "speichern"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.ClickfinderProgramGuide.My.Resources.Resources.SetupIcon
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(50, 50)
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(68, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(486, 25)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Clickfinder Program Guide Configuration"
        '
        'CBChannelGroup
        '
        Me.CBChannelGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBChannelGroup.FormattingEnabled = True
        Me.CBChannelGroup.Location = New System.Drawing.Point(464, 143)
        Me.CBChannelGroup.Name = "CBChannelGroup"
        Me.CBChannelGroup.Size = New System.Drawing.Size(193, 21)
        Me.CBChannelGroup.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(115, 217)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "PrimeTime:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(115, 253)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "LateTime:"
        '
        'CBPrimeTimeHour
        '
        Me.CBPrimeTimeHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBPrimeTimeHour.FormattingEnabled = True
        Me.CBPrimeTimeHour.Location = New System.Drawing.Point(180, 214)
        Me.CBPrimeTimeHour.Name = "CBPrimeTimeHour"
        Me.CBPrimeTimeHour.Size = New System.Drawing.Size(37, 21)
        Me.CBPrimeTimeHour.TabIndex = 14
        '
        'CBPrimeTimeMinute
        '
        Me.CBPrimeTimeMinute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBPrimeTimeMinute.FormattingEnabled = True
        Me.CBPrimeTimeMinute.Location = New System.Drawing.Point(240, 214)
        Me.CBPrimeTimeMinute.Name = "CBPrimeTimeMinute"
        Me.CBPrimeTimeMinute.Size = New System.Drawing.Size(37, 21)
        Me.CBPrimeTimeMinute.TabIndex = 15
        '
        'CBLateTimeMinute
        '
        Me.CBLateTimeMinute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBLateTimeMinute.FormattingEnabled = True
        Me.CBLateTimeMinute.Location = New System.Drawing.Point(240, 245)
        Me.CBLateTimeMinute.Name = "CBLateTimeMinute"
        Me.CBLateTimeMinute.Size = New System.Drawing.Size(37, 21)
        Me.CBLateTimeMinute.TabIndex = 17
        '
        'CBLateTimeHour
        '
        Me.CBLateTimeHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBLateTimeHour.FormattingEnabled = True
        Me.CBLateTimeHour.Location = New System.Drawing.Point(180, 245)
        Me.CBLateTimeHour.Name = "CBLateTimeHour"
        Me.CBLateTimeHour.Size = New System.Drawing.Size(37, 21)
        Me.CBLateTimeHour.TabIndex = 16
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(223, 217)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(11, 13)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = ":"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(223, 248)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(11, 13)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = ":"
        '
        'CBDelayNow
        '
        Me.CBDelayNow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBDelayNow.FormattingEnabled = True
        Me.CBDelayNow.Location = New System.Drawing.Point(180, 187)
        Me.CBDelayNow.Name = "CBDelayNow"
        Me.CBDelayNow.Size = New System.Drawing.Size(37, 21)
        Me.CBDelayNow.TabIndex = 20
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(115, 190)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(32, 13)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Jetzt:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(168, 190)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(11, 13)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "-"
        '
        'CBMinTime
        '
        Me.CBMinTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBMinTime.FormattingEnabled = True
        Me.CBMinTime.Location = New System.Drawing.Point(207, 332)
        Me.CBMinTime.Name = "CBMinTime"
        Me.CBMinTime.Size = New System.Drawing.Size(37, 21)
        Me.CBMinTime.TabIndex = 23
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(115, 335)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(86, 13)
        Me.Label9.TabIndex = 24
        Me.Label9.Text = "mindest Laufzeit:"
        '
        'CKIgnoreSeries
        '
        Me.CKIgnoreSeries.AutoSize = True
        Me.CKIgnoreSeries.Location = New System.Drawing.Point(250, 336)
        Me.CKIgnoreSeries.Name = "CKIgnoreSeries"
        Me.CKIgnoreSeries.Size = New System.Drawing.Size(122, 17)
        Me.CKIgnoreSeries.TabIndex = 25
        Me.CKIgnoreSeries.Text = "bei Serien ignorieren"
        Me.CKIgnoreSeries.UseVisualStyleBackColor = True
        '
        'BtnCreateLogos
        '
        Me.BtnCreateLogos.Location = New System.Drawing.Point(486, 354)
        Me.BtnCreateLogos.Name = "BtnCreateLogos"
        Me.BtnCreateLogos.Size = New System.Drawing.Size(170, 39)
        Me.BtnCreateLogos.TabIndex = 26
        Me.BtnCreateLogos.Text = "TV Rating Logos erstellen"
        Me.BtnCreateLogos.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(313, 416)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 27
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(486, 399)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(170, 19)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar1.TabIndex = 28
        Me.ProgressBar1.Visible = False
        '
        'CKRatingTVLogos
        '
        Me.CKRatingTVLogos.AutoSize = True
        Me.CKRatingTVLogos.Location = New System.Drawing.Point(486, 437)
        Me.CKRatingTVLogos.Name = "CKRatingTVLogos"
        Me.CKRatingTVLogos.Size = New System.Drawing.Size(162, 17)
        Me.CKRatingTVLogos.TabIndex = 29
        Me.CKRatingTVLogos.Text = "TV Rating Logos verwenden" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.CKRatingTVLogos.UseVisualStyleBackColor = True
        '
        'Setup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(760, 483)
        Me.Controls.Add(Me.CKRatingTVLogos)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.BtnCreateLogos)
        Me.Controls.Add(Me.CKIgnoreSeries)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.CBMinTime)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.CBDelayNow)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.CBLateTimeMinute)
        Me.Controls.Add(Me.CBLateTimeHour)
        Me.Controls.Add(Me.CBPrimeTimeMinute)
        Me.Controls.Add(Me.CBPrimeTimeHour)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CBChannelGroup)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.tfClickfinderPath)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbRating)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Setup"
        Me.Text = "Clickfinder Program Guide Configuration"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents CKRatingTVLogos As System.Windows.Forms.CheckBox
End Class
