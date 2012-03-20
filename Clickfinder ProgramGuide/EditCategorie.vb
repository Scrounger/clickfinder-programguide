Imports ClickfinderProgramGuide.TvDatabase
Imports MediaPortal.Configuration
Imports System.Drawing
Imports Gentle.Framework

Public Class EditCategorie

    Private Sub EditCategorie_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not String.IsNullOrEmpty(Me.tbSortOrder.Text) Then
            Dim _Categorie As ClickfinderCategories = ClickfinderCategories.Retrieve(CInt(Me.tbSortOrder.Text))

            Me.tbName.Text = _Categorie.Name
            Me.tbBeschreibung.Text = _Categorie.Beschreibung
            Me.tbSqlString.Text = _Categorie.SqlString
            Me.NumMinDauer.Value = _Categorie.MinRunTime
            Me.NumNowOffset.Value = (-1) * _Categorie.NowOffset

            Try
                Dim imgStream As New IO.MemoryStream
                CreateCloneImage(Config.GetFile(Config.Dir.Thumbs, "Clickfinder ProgramGuide\Categories\") & _Categorie.Image).Save(imgStream, System.Drawing.Imaging.ImageFormat.Png)
                Me.picCategorie.Image = System.Drawing.Image.FromStream(imgStream)
                imgStream.Dispose()
            Catch ex As Exception
                MyLog.Debug("[EditCategorie_Load] Image not found")
            End Try

            Dim _sortedBy As String = String.Empty
            Select Case _Categorie.sortedBy
                Case Is = Helper.SortMethode.startTime.ToString
                    _sortedBy = "Startzeit"
                Case Is = Helper.SortMethode.TvMovieStar.ToString
                    _sortedBy = "TvMovie Bewertung"
                Case Is = Helper.SortMethode.RatingStar.ToString
                    _sortedBy = "Rating Star"
            End Select

            For i = 0 To CBsortedBy.Items.Count - 1
                If CBsortedBy.Items.Item(i) = _sortedBy Then
                    CBsortedBy.Text = CBsortedBy.Items.Item(i)
                End If
            Next

        End If

    End Sub

    Private Function CreateCloneImage(ByVal path As String) As Bitmap
        Dim bmpClone As Bitmap = Nothing 'the clone to be loaded to a PictureBox

        Try
            'Work around: ImageClone erstellen, weil sonst löschen net funzt -> verhindert (... used by other process)

            Dim bmpOriginal As System.Drawing.Image = System.Drawing.Image.FromFile(path)
            bmpClone = New Bitmap(bmpOriginal.Width, bmpOriginal.Height)
            Dim gr As Graphics = Graphics.FromImage(bmpClone) 'get object representing clone's currently empty drawing surface
            gr.SmoothingMode = Drawing2D.SmoothingMode.None
            gr.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
            gr.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighSpeed
            gr.DrawImage(bmpOriginal, 0, 0, bmpOriginal.Width, bmpOriginal.Height) 'copy original image onto this surface
            gr.Dispose()

            bmpOriginal.Dispose()

        Catch ex As Exception
            MyLog.[Warn]("[CreateCloneImage]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
        End Try

        Return bmpClone
    End Function

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub ButtonSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSave.Click
        Try

            If Not (String.IsNullOrEmpty(Me.tbName.Text) Or String.IsNullOrEmpty(Me.tbBeschreibung.Text) Or String.IsNullOrEmpty(Me.tbSqlString.Text)) Then


                Dim _Categorie As ClickfinderCategories

                If Not String.IsNullOrEmpty(Me.tbSortOrder.Text) Then
                    _Categorie = ClickfinderCategories.Retrieve(Me.tbSortOrder.Text)
                Else
                    _Categorie = New ClickfinderCategories(Me.tbName.Text, Me.tbBeschreibung.Text, True, Me.tbNewSortOrder.Text, Me.NumMinDauer.Value, Me.NumNowOffset.Value * (-1))
                End If

                If Not String.IsNullOrEmpty(Me.picCategorie.Tag) Then
                    If Not Me.picCategorie.Tag = Config.GetFile(Config.Dir.Thumbs, "Clickfinder ProgramGuide\Categories\") & Me.tbName.Text & ".png" Then
                        IO.File.Copy(Me.picCategorie.Tag, Config.GetFile(Config.Dir.Thumbs, "Clickfinder ProgramGuide\Categories\") & Me.tbName.Text & ".png", True)
                        _Categorie.Image = Me.tbName.Text & ".png"
                    End If
                End If

                _Categorie.Name = Me.tbName.Text
                _Categorie.Beschreibung = Me.tbBeschreibung.Text
                _Categorie.isVisible = True
                _Categorie.SqlString = Me.tbSqlString.Text
                _Categorie.MinRunTime = Me.NumMinDauer.Value
                _Categorie.NowOffset = Me.NumNowOffset.Value * (-1)

                Select Case CBsortedBy.Text
                    Case Is = "Startzeit"
                        _Categorie.sortedBy = Helper.SortMethode.startTime.ToString
                    Case Is = "Rating Star"
                        _Categorie.sortedBy = Helper.SortMethode.RatingStar.ToString
                    Case Is = "TvMovie Bewertung"
                        _Categorie.sortedBy = Helper.SortMethode.TvMovieStar.ToString
                End Select

                _Categorie.Persist()

                Me.Close()
            Else
                MsgBox("Alle Felder müssen ausgefüllt werden !", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            MyLog.[Error]("[ButtonSave_Click]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
        End Try
    End Sub

    Private Sub ButtonImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonImage.Click
        Try

            Dim _openFileDialog As New System.Windows.Forms.OpenFileDialog

            _openFileDialog.Filter = "Image (*.png) |*.png;"
            _openFileDialog.InitialDirectory = Config.GetFile(Config.Dir.Thumbs, "Clickfinder ProgramGuide\Categories")

            If _openFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then

                Dim imgStream As New IO.MemoryStream
                CreateCloneImage(_openFileDialog.FileName).Save(imgStream, System.Drawing.Imaging.ImageFormat.Png)
                Me.picCategorie.Image = System.Drawing.Image.FromStream(imgStream)
                Me.picCategorie.Tag = _openFileDialog.FileName
                imgStream.Dispose()
            End If
        Catch ex As Exception
            MyLog.[Error]("[ButtonImage_Click]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
        End Try
    End Sub

    Private Sub ButtonTestSQL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTestSQL.Click
        Try
            Dim sqlstring As String
            Dim _result As New ArrayList
            sqlstring = CStr(Replace(Replace(tbSqlString.Text, "#startTime", Helper.MySqlDate(Date.Today.AddHours(20))), "#endTime", Helper.MySqlDate(Date.Now.AddHours(22))))
            _result.AddRange(Broker.Execute(sqlstring).TransposeToFieldList("idProgram", False))

            MsgBox(_result.Count & " Einträge gefunden" & vbNewLine & vbNewLine & "SQL String funktioniert", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox("Fehler !" & vbNewLine & vbNewLine & ex.Message, MsgBoxStyle.Critical, "Warnung ...")
        End Try

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            tbSqlString.Enabled = True
            ButtonTestSQL.Enabled = True
        Else
            tbSqlString.Enabled = False
            ButtonTestSQL.Enabled = False
        End If
    End Sub
End Class