﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditCategorie
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
        Me.tbName = New System.Windows.Forms.TextBox
        Me.tbBeschreibung = New System.Windows.Forms.TextBox
        Me.tbSortOrder = New System.Windows.Forms.TextBox
        Me.tbSqlString = New System.Windows.Forms.TextBox
        Me.picCategorie = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.ButtonImage = New System.Windows.Forms.Button
        Me.ButtonSave = New System.Windows.Forms.Button
        Me.tbNewSortOrder = New System.Windows.Forms.TextBox
        Me.NumMinDauer = New System.Windows.Forms.NumericUpDown
        Me.Label18 = New System.Windows.Forms.Label
        Me.CBsortedBy = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.ButtonTestSQL = New System.Windows.Forms.Button
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.NumNowOffset = New System.Windows.Forms.NumericUpDown
        Me.Label4 = New System.Windows.Forms.Label
        Me.cbGroup = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        CType(Me.picCategorie, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumMinDauer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumNowOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbName
        '
        Me.tbName.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbName.Location = New System.Drawing.Point(130, 32)
        Me.tbName.Name = "tbName"
        Me.tbName.Size = New System.Drawing.Size(389, 29)
        Me.tbName.TabIndex = 0
        '
        'tbBeschreibung
        '
        Me.tbBeschreibung.Location = New System.Drawing.Point(130, 80)
        Me.tbBeschreibung.Multiline = True
        Me.tbBeschreibung.Name = "tbBeschreibung"
        Me.tbBeschreibung.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tbBeschreibung.Size = New System.Drawing.Size(389, 92)
        Me.tbBeschreibung.TabIndex = 1
        '
        'tbSortOrder
        '
        Me.tbSortOrder.Enabled = False
        Me.tbSortOrder.Location = New System.Drawing.Point(313, 523)
        Me.tbSortOrder.Name = "tbSortOrder"
        Me.tbSortOrder.Size = New System.Drawing.Size(74, 20)
        Me.tbSortOrder.TabIndex = 2
        Me.tbSortOrder.Visible = False
        '
        'tbSqlString
        '
        Me.tbSqlString.Enabled = False
        Me.tbSqlString.Location = New System.Drawing.Point(15, 53)
        Me.tbSqlString.Multiline = True
        Me.tbSqlString.Name = "tbSqlString"
        Me.tbSqlString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tbSqlString.Size = New System.Drawing.Size(504, 117)
        Me.tbSqlString.TabIndex = 3
        '
        'picCategorie
        '
        Me.picCategorie.Location = New System.Drawing.Point(15, 32)
        Me.picCategorie.Name = "picCategorie"
        Me.picCategorie.Size = New System.Drawing.Size(100, 100)
        Me.picCategorie.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picCategorie.TabIndex = 8
        Me.picCategorie.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(127, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Beschreibung:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "SQL String:"
        '
        'ButtonImage
        '
        Me.ButtonImage.Location = New System.Drawing.Point(15, 146)
        Me.ButtonImage.Name = "ButtonImage"
        Me.ButtonImage.Size = New System.Drawing.Size(100, 26)
        Me.ButtonImage.TabIndex = 11
        Me.ButtonImage.Text = "Image"
        Me.ButtonImage.UseVisualStyleBackColor = True
        '
        'ButtonSave
        '
        Me.ButtonSave.Location = New System.Drawing.Point(403, 510)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(140, 44)
        Me.ButtonSave.TabIndex = 13
        Me.ButtonSave.Text = "Save"
        Me.ButtonSave.UseVisualStyleBackColor = True
        '
        'tbNewSortOrder
        '
        Me.tbNewSortOrder.Enabled = False
        Me.tbNewSortOrder.Location = New System.Drawing.Point(233, 523)
        Me.tbNewSortOrder.Name = "tbNewSortOrder"
        Me.tbNewSortOrder.Size = New System.Drawing.Size(74, 20)
        Me.tbNewSortOrder.TabIndex = 14
        Me.tbNewSortOrder.Visible = False
        '
        'NumMinDauer
        '
        Me.NumMinDauer.Location = New System.Drawing.Point(74, 23)
        Me.NumMinDauer.Name = "NumMinDauer"
        Me.NumMinDauer.Size = New System.Drawing.Size(42, 20)
        Me.NumMinDauer.TabIndex = 15
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(13, 25)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(59, 13)
        Me.Label18.TabIndex = 16
        Me.Label18.Text = "Laufzeit:  >"
        '
        'CBsortedBy
        '
        Me.CBsortedBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBsortedBy.FormattingEnabled = True
        Me.CBsortedBy.Items.AddRange(New Object() {"Startzeit", "Rating Star", "TvMovie Bewertung"})
        Me.CBsortedBy.Location = New System.Drawing.Point(155, 39)
        Me.CBsortedBy.Name = "CBsortedBy"
        Me.CBsortedBy.Size = New System.Drawing.Size(100, 21)
        Me.CBsortedBy.TabIndex = 17
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(152, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 13)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "sortieren nach:"
        '
        'ButtonTestSQL
        '
        Me.ButtonTestSQL.Enabled = False
        Me.ButtonTestSQL.Location = New System.Drawing.Point(411, 176)
        Me.ButtonTestSQL.Name = "ButtonTestSQL"
        Me.ButtonTestSQL.Size = New System.Drawing.Size(108, 26)
        Me.ButtonTestSQL.TabIndex = 19
        Me.ButtonTestSQL.Text = "Test SQL String"
        Me.ButtonTestSQL.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(79, 30)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(72, 17)
        Me.CheckBox1.TabIndex = 20
        Me.CheckBox1.Text = "aktivieren"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'NumNowOffset
        '
        Me.NumNowOffset.Location = New System.Drawing.Point(74, 49)
        Me.NumNowOffset.Maximum = New Decimal(New Integer() {0, 0, 0, 0})
        Me.NumNowOffset.Minimum = New Decimal(New Integer() {59, 0, 0, -2147483648})
        Me.NumNowOffset.Name = "NumNowOffset"
        Me.NumNowOffset.Size = New System.Drawing.Size(42, 20)
        Me.NumNowOffset.TabIndex = 21
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Jetzt:"
        '
        'cbGroup
        '
        Me.cbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGroup.FormattingEnabled = True
        Me.cbGroup.Location = New System.Drawing.Point(107, 39)
        Me.cbGroup.Name = "cbGroup"
        Me.cbGroup.Size = New System.Drawing.Size(100, 21)
        Me.cbGroup.TabIndex = 23
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(37, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 13)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "Group Filter:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.NumMinDauer)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.NumNowOffset)
        Me.GroupBox1.Controls.Add(Me.CBsortedBy)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 205)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(278, 84)
        Me.GroupBox1.TabIndex = 25
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Einstellungen die nur für die GUI Items gilt"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.tbBeschreibung)
        Me.GroupBox2.Controls.Add(Me.tbName)
        Me.GroupBox2.Controls.Add(Me.picCategorie)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.ButtonImage)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(531, 187)
        Me.GroupBox2.TabIndex = 26
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Allgemeine Einstellungen der Kategorie"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.CheckBox1)
        Me.GroupBox3.Controls.Add(Me.tbSqlString)
        Me.GroupBox3.Controls.Add(Me.ButtonTestSQL)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 295)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(531, 209)
        Me.GroupBox3.TabIndex = 27
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "SQL Befehl der Kategorie"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.cbGroup)
        Me.GroupBox4.Location = New System.Drawing.Point(296, 205)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(247, 84)
        Me.GroupBox4.TabIndex = 28
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Einstellungen die nur für die GUI Kategorien gilt"
        '
        'EditCategorie
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(555, 565)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.tbNewSortOrder)
        Me.Controls.Add(Me.ButtonSave)
        Me.Controls.Add(Me.tbSortOrder)
        Me.Name = "EditCategorie"
        Me.Text = "NewCategorie"
        CType(Me.picCategorie, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumMinDauer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumNowOffset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbName As System.Windows.Forms.TextBox
    Friend WithEvents tbBeschreibung As System.Windows.Forms.TextBox
    Friend WithEvents tbSortOrder As System.Windows.Forms.TextBox
    Friend WithEvents tbSqlString As System.Windows.Forms.TextBox
    Public WithEvents picCategorie As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ButtonImage As System.Windows.Forms.Button
    Friend WithEvents ButtonSave As System.Windows.Forms.Button
    Friend WithEvents tbNewSortOrder As System.Windows.Forms.TextBox
    Friend WithEvents NumMinDauer As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents CBsortedBy As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ButtonTestSQL As System.Windows.Forms.Button
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents NumNowOffset As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbGroup As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
End Class
