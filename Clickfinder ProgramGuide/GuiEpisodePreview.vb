Imports MediaPortal.GUI.Library
Imports TvDatabase
Imports Gentle.Framework
Imports ClickfinderProgramGuide.Helper
Imports ClickfinderProgramGuide.TvDatabase
Imports MediaPortal.Configuration


Namespace ClickfinderProgramGuide
    Public Class EpisodePreviewGuiWindow
        Inherits GUIWindow


#Region "Skin Controls"

        'Buttons
        <SkinControlAttribute(2)> Protected _btnNow As GUIButtonControl = Nothing
        <SkinControlAttribute(3)> Protected _btnPrimeTime As GUIButtonControl = Nothing
        <SkinControlAttribute(4)> Protected _btnLateTime As GUIButtonControl = Nothing
        <SkinControlAttribute(5)> Protected _btnAllMovies As GUIButtonControl = Nothing
        <SkinControlAttribute(6)> Protected _btnHighlights As GUIButtonControl = Nothing
        <SkinControlAttribute(7)> Protected _btnPreview As GUIButtonControl = Nothing

        'ProgressBar
        <SkinControlAttribute(8)> Protected _SeriesProgressBar As GUIAnimation = Nothing
        <SkinControlAttribute(9)> Protected _RepeatsProgressBar As GUIAnimation = Nothing

        'ListControl
        <SkinControlAttribute(10)> Protected _SeriesList As GUIListControl = Nothing
        <SkinControlAttribute(30)> Protected _RepeatsList As GUIListControl = Nothing
#End Region

#Region "Members"
        Private Shared _layer As New TvBusinessLayer
        Private ThreadSeriesFill As Threading.Thread
#End Region

#Region "Constructors"
        Public Sub New()
        End Sub
#End Region

#Region "GUI Properties"
        Public Overloads Overrides Property GetID() As Integer
            Get
                Return 1656544657
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property


        Public Overloads Overrides Function Init() As Boolean
            'Beim initialisieren des Plugin den Screen laden

            Return Load(GUIGraphicsContext.Skin + "\ClickfinderProgramGuideEpisodePreview.xml")
        End Function

        Public Overrides Function GetModuleName() As String
            Return "Clickfinder ProgramGuide Episode Preview"
        End Function

#End Region

#Region "GUI Events"
        Protected Overrides Sub OnPageLoad()
            MyBase.OnPageLoad()
            GUIWindowManager.NeedRefresh()

            MyLog.Info("")
            MyLog.Info("")
            MyLog.Info("[EpisodePreviewGuiWindow] -------------[OPEN]-------------")

            Translator.SetProperty("#EpisodePreviewLabel", Translation.NewEpisodes & " " & Translation.InTheNext & " " & _layer.GetSetting("ClickfinderPreviewMaxDays", "7").Value & " " & Translation.Day)

            Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowSeriesProgressBar)
            _ProgressBarThread.Start()


            Try
                If ThreadSeriesFill.IsAlive = True Then ThreadSeriesFill.Abort()
            Catch ex As Exception
                'Eventuell auftretende Exception abfangen
                ' http://www.vbarchiv.net/faq/faq_vbnet_threads.html
            End Try

            ThreadSeriesFill = New Threading.Thread(AddressOf FillSeriesList)
            ThreadSeriesFill.IsBackground = True
            ThreadSeriesFill.Start()


        End Sub
        Public Overrides Sub OnAction(ByVal Action As MediaPortal.GUI.Library.Action)
            If GUIWindowManager.ActiveWindow = GetID Then

                'If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_SELECT_ITEM Then

                '    Action_SelectItem()

                'End If

            End If

            MyBase.OnAction(Action)
        End Sub
        Protected Overrides Sub OnPageDestroy(ByVal new_windowId As Integer)

            MyBase.OnPageDestroy(new_windowId)

        End Sub
        Protected Overrides Sub OnClicked(ByVal controlId As Integer, _
                                 ByVal control As GUIControl, _
                                 ByVal actionType As  _
                                 MediaPortal.GUI.Library.Action.ActionType)

            MyBase.OnClicked(controlId, control, actionType)

            If control Is _btnNow Then
                GuiButtons.Now()
            End If

            If control Is _btnPrimeTime Then
                GuiButtons.PrimeTime()
            End If

            If control Is _btnLateTime Then
                GuiButtons.LateTime()
            End If

            If control Is _btnAllMovies Then
                GuiButtons.AllMovies()
            End If

            If control Is _btnHighlights Then
                GuiButtons.Highlights()
            End If

            If control Is _btnPreview Then
                GuiButtons.Preview()
            End If

            If control Is _SeriesList Or control Is _RepeatsList Then
                If actionType = MediaPortal.GUI.Library.Action.ActionType.ACTION_SELECT_ITEM Then
                    Action_SelectItem()
                End If
            End If

        End Sub

        Private Sub Action_SelectItem()

            If _SeriesList.IsFocused = True Then
                FillEpisodesList()
            End If

        End Sub

#End Region

#Region "functions"
        Private Sub FillSeriesList()

            Dim SQLstring As String = String.Empty

            Try
                _SeriesList.Visible = False
                _SeriesList.Clear()

                Dim _Result As New ArrayList
                Dim _DummySeriesResult As New ArrayList
                'Dim _timeLabel As String = String.Empty
                Dim _FoundedNewEpisodes As String = String.Empty

                'Serien mit neuen Episoden laden
                SQLstring = "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                            "WHERE idSeries > 0 " & _
                                            "AND local = 0 " & _
                                            "AND startTime > " & MySqlDate(Date.Now) & " " & _
                                            "AND startTime < " & MySqlDate(Date.Today.AddDays(CDbl(_layer.GetSetting("ClickfinderPreviewMaxDays", "7").Value))) & " " & _
                                            "GROUP BY title ORDER BY startTime ASC"

                _Result.AddRange(Broker.Execute(SQLstring).TransposeToFieldList("idProgram", False))

                For i = 0 To _Result.Count - 1

                    'ProgramDaten über TvMovieProgram laden
                    Dim _Series As TVMovieProgram = TVMovieProgram.Retrieve(_Result.Item(i))

                    'Prüfen ob noch immer lokal nicht vorhanden
                    CheckSeriesLocalStatus(_Series)
                    If _Series.local = False Then

                        If _DummySeriesResult.Contains(_Series.idSeries) = False Then
                            _DummySeriesResult.Add(_Series.idSeries)

                            '_timeLabel = Helper.getTranslatedDayOfWeek(_Series.ReferencedProgram.StartTime) & " " & Format(_Series.ReferencedProgram.StartTime.Day, "00") & "." & Format(_Series.ReferencedProgram.StartTime.Month, "00") & " - " & Format(_Series.ReferencedProgram.StartTime.Hour, "00") & ":" & Format(_Series.ReferencedProgram.StartTime.Minute, "00")

                            'alle neuen episoden zählen
                            Dim _newEpisodesCount As New ArrayList
                            Dim _newEpisodesSQLStringCount As String = String.Empty

                            _newEpisodesSQLStringCount = "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                            "WHERE idSeries > 0 " & _
                                            "AND local = 0 " & _
                                            "AND startTime > " & MySqlDate(Date.Now) & " " & _
                                            "AND startTime < " & MySqlDate(Date.Today.AddDays(CDbl(_layer.GetSetting("ClickfinderPreviewMaxDays", "7").Value) + 1)) & " " & _
                                            "AND title = '" & _Series.ReferencedProgram.Title & "' " & _
                                            "GROUP BY SeriesNum, EpisodeNum ASC"

                            '_newEpisodesSQLStringCount = "Select * from program " & _
                            '                "WHERE SeriesNum IS NOT NULL " & _
                            '                "AND EpisodeNum IS NOT NULL " & _
                            '                "AND startTime > " & MySqlDate(Date.Now) & " " & _
                            '                "AND startTime < " & MySqlDate(Date.Today.AddDays(CDbl(_layer.GetSetting("ClickfinderPreviewMaxDays", "7").Value))) & " " & _
                            '                "AND title = '" & _Series.ReferencedProgram.Title & "' " & _
                            '                "GROUP BY SeriesNum, EpisodeNum ASC"

                            _newEpisodesCount.AddRange(Broker.Execute(_newEpisodesSQLStringCount).TransposeToFieldList("idProgram", False))

                            _FoundedNewEpisodes = _newEpisodesCount.Count & " " & Translation.NewEpisodeFound

                            AddListControlItem(_SeriesList, _Series.idSeries, _Series.ReferencedProgram.ReferencedChannel.DisplayName, _Series.ReferencedProgram.Title, , _FoundedNewEpisodes, Config.GetFile(Config.Dir.Thumbs, "MPTVSeriesBanners\") & _Series.SeriesPosterImage)

                        End If

                    End If

                Next
                _SeriesList.Visible = True
                _SeriesProgressBar.Visible = False

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Sub

        Private Sub FillEpisodesList()


            MsgBox(_SeriesList.SelectedListItem.ItemId & vbNewLine & _SeriesList.SelectedListItem.Label)

            Try

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Sub

        Private Sub ShowSeriesProgressBar()
            _SeriesProgressBar.Visible = True
        End Sub


#End Region


    End Class
End Namespace
