﻿Imports ClickfinderProgramGuide.ClickfinderProgramGuide
Imports MediaPortal.GUI.Library


Public Class GuiButtons

    Friend Shared Sub Now()
        Try
            CategoriesGuiWindow.SetGuiProperties(CategoriesGuiWindow.CategorieView.Now)
            GUIWindowManager.ActivateWindow(1656544654)
        Catch ex As Exception
            MyLog.Error("[Button] [Now]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
        End Try
    End Sub
    Friend Shared Sub PrimeTime()
        Try
            CategoriesGuiWindow.SetGuiProperties(CategoriesGuiWindow.CategorieView.PrimeTime)
            GUIWindowManager.ActivateWindow(1656544654)
        Catch ex As Exception
            MyLog.Error("[Button] [PrimeTime]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
        End Try
    End Sub
    Friend Shared Sub LateTime()
        Try
            CategoriesGuiWindow.SetGuiProperties(CategoriesGuiWindow.CategorieView.LateTime)
            GUIWindowManager.ActivateWindow(1656544654)
        Catch ex As Exception
            MyLog.Error("[Button] [LateTime]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
        End Try
    End Sub
    Friend Shared Sub AllMovies()
        Try
            Dim _endTime As New Date(Date.Now.Year, Date.Now.Month, Date.Now.AddDays(1).Day)

            ItemsGuiWindow.SetGuiProperties("Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                                    "WHERE startTime >= " & Helper.MySqlDate(Date.Now.AddMinutes(-60)) & " " & _
                                                    "AND startTime <= " & Helper.MySqlDate(New Date(_endTime.Year, _endTime.Month, _endTime.Day)) & " " & _
                                                    "AND TVMovieBewertung > 1 " & _
                                                    "AND NOT TVMovieBewertung = 6 " & _
                                                    "AND starRating > 1 " & _
                                                    "AND genre NOT LIKE '%Serie%' " & _
                                                    "AND genre NOT LIKE '%Sitcom%' " & _
                                                    "AND genre NOT LIKE '%Zeichentrick%' " & _
                                                    Helper.ORDERBYstartTime, 85)

            Translator.SetProperty("#ItemsLeftListLabel", Translation.allMoviesAt & " " & Format(Date.Now.AddMinutes(-60).Hour, "00") & ":" & Format(Date.Now.AddMinutes(-60).Minute, "00") & " - " & Format(_endTime.Hour, "00") & ":" & Format(_endTime.Minute, "00"))
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

    Friend Shared Sub Preview()



    End Sub


End Class