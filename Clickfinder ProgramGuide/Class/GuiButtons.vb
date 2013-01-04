Imports ClickfinderProgramGuide.ClickfinderProgramGuide
Imports MediaPortal.GUI.Library
Imports TvControl
Imports TvDatabase
Imports TvLibrary.Interfaces
Imports System.Text
Imports ClickfinderProgramGuide.Helper

Public Class GuiButtons

    Friend Shared Sub AllMovies()
        Try

            Dim _SqlStringBuilder As New StringBuilder("Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram ")
            _SqlStringBuilder.Append("WHERE #CPGFilter AND startTime >= #StartTime AND startTime <= #EndTime ")
            _SqlStringBuilder.Append("AND starRating >= 1 AND TvMovieBewertung < 6 ")
            _SqlStringBuilder.Append("AND (genre NOT LIKE '%Serie' OR genre NOT LIKE '%Reihe' OR genre NOT LIKE '%Sitcom%' OR genre NOT LIKE '%Zeichentrick%') ")
            _SqlStringBuilder.Append("#CPGgroupBy ")
            _SqlStringBuilder.Append("#CPGorderBy")

            ItemsGuiWindow.SetGuiProperties(_SqlStringBuilder.ToString, _
                                                        Date.Now.AddMinutes(-60), _
                                                        Date.Now.AddHours(6), _
                                                        SortMethode.startTime.ToString, _
                                                        CPGsettings.StandardTvGroup, _
                                                        85, CPGsettings.ItemsShowLocalMovies)

            Translator.SetProperty("#ItemsLeftListLabel", Translation.allMoviesAt & " " & Format(Date.Now.AddMinutes(-60).Hour, "00") & ":" & Format(Date.Now.AddMinutes(-60).Minute, "00") & " - " & Format(Date.Now.AddHours(6).Hour, "00") & ":" & Format(Date.Now.AddHours(6).Minute, "00"))
            GUIWindowManager.ActivateWindow(1656544653)
        Catch ex As Exception
            MyLog.Error("[Button] [AllMovies]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
        End Try

    End Sub
    Friend Shared Sub Highlights()
        Try
            GUIWindowManager.ActivateWindow(1656544656)
        Catch ex As Exception
            MyLog.Error("[Button] [Highlights]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
        End Try
    End Sub

End Class
