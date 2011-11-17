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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cbRating
        '
        Me.cbRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbRating.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbRating.FormattingEnabled = True
        Me.cbRating.Items.AddRange(New Object() {"0", "1", "2", "3"})
        Me.cbRating.Location = New System.Drawing.Point(237, 120)
        Me.cbRating.Margin = New System.Windows.Forms.Padding(4)
        Me.cbRating.Name = "cbRating"
        Me.cbRating.Size = New System.Drawing.Size(33, 24)
        Me.cbRating.TabIndex = 5
        '
        'tfClickfinderPath
        '
        Me.tfClickfinderPath.Location = New System.Drawing.Point(237, 77)
        Me.tfClickfinderPath.Margin = New System.Windows.Forms.Padding(4)
        Me.tfClickfinderPath.Name = "tfClickfinderPath"
        Me.tfClickfinderPath.Size = New System.Drawing.Size(326, 23)
        Me.tfClickfinderPath.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 80)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(182, 16)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "TV Movie Clickfinder Path:"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(451, 471)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(112, 73)
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
        Me.CBChannelGroup.Location = New System.Drawing.Point(237, 163)
        Me.CBChannelGroup.Margin = New System.Windows.Forms.Padding(4)
        Me.CBChannelGroup.Name = "CBChannelGroup"
        Me.CBChannelGroup.Size = New System.Drawing.Size(326, 24)
        Me.CBChannelGroup.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(43, 73)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 16)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "PrimeTime:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(43, 113)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 16)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "LateTime:"
        '
        'CBPrimeTimeHour
        '
        Me.CBPrimeTimeHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBPrimeTimeHour.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBPrimeTimeHour.FormattingEnabled = True
        Me.CBPrimeTimeHour.Location = New System.Drawing.Point(188, 70)
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
        Me.CBPrimeTimeMinute.Location = New System.Drawing.Point(261, 70)
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
        Me.CBLateTimeMinute.Location = New System.Drawing.Point(261, 110)
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
        Me.CBLateTimeHour.Location = New System.Drawing.Point(188, 110)
        Me.CBLateTimeHour.Margin = New System.Windows.Forms.Padding(4)
        Me.CBLateTimeHour.Name = "CBLateTimeHour"
        Me.CBLateTimeHour.Size = New System.Drawing.Size(54, 24)
        Me.CBLateTimeHour.TabIndex = 16
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(245, 73)
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
        Me.Label6.Location = New System.Drawing.Point(245, 113)
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
        Me.CBDelayNow.Location = New System.Drawing.Point(188, 30)
        Me.CBDelayNow.Margin = New System.Windows.Forms.Padding(4)
        Me.CBDelayNow.Name = "CBDelayNow"
        Me.CBDelayNow.Size = New System.Drawing.Size(54, 24)
        Me.CBDelayNow.TabIndex = 20
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(43, 33)
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
        Me.Label8.Location = New System.Drawing.Point(173, 33)
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
        Me.CBMinTime.Location = New System.Drawing.Point(188, 150)
        Me.CBMinTime.Margin = New System.Windows.Forms.Padding(4)
        Me.CBMinTime.Name = "CBMinTime"
        Me.CBMinTime.Size = New System.Drawing.Size(54, 24)
        Me.CBMinTime.TabIndex = 23
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(43, 153)
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
        Me.CKIgnoreSeries.Location = New System.Drawing.Point(261, 152)
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
        Me.BtnCreateLogos.Location = New System.Drawing.Point(15, 406)
        Me.BtnCreateLogos.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnCreateLogos.Name = "BtnCreateLogos"
        Me.BtnCreateLogos.Size = New System.Drawing.Size(255, 48)
        Me.BtnCreateLogos.TabIndex = 26
        Me.BtnCreateLogos.Text = "TV Rating Logos erstellen"
        Me.BtnCreateLogos.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(279, 406)
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
        Me.CKRatingTVLogos.Location = New System.Drawing.Point(279, 434)
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
        Me.Label10.Location = New System.Drawing.Point(15, 123)
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
        Me.Label11.Location = New System.Drawing.Point(15, 166)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(198, 16)
        Me.Label11.TabIndex = 31
        Me.Label11.Text = "Favoriten Tv Channel Group:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CBDelayNow)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.CBPrimeTimeHour)
        Me.GroupBox1.Controls.Add(Me.CBPrimeTimeMinute)
        Me.GroupBox1.Controls.Add(Me.CBLateTimeHour)
        Me.GroupBox1.Controls.Add(Me.CKIgnoreSeries)
        Me.GroupBox1.Controls.Add(Me.CBLateTimeMinute)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.CBMinTime)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(18, 211)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(545, 188)
        Me.GroupBox1.TabIndex = 32
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filter Einstellungen"
        '
        'Setup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(580, 560)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.CKRatingTVLogos)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.BtnCreateLogos)
        Me.Controls.Add(Me.CBChannelGroup)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.tfClickfinderPath)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbRating)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Setup"
        Me.Text = "Clickfinder Program Guide Configuration"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
