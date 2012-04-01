﻿Imports ClickfinderProgramGuide.TvDatabase
Imports TvDatabase
Imports MediaPortal.Configuration

Namespace ClickfinderProgramGuide

    Public Class GuiLayout
        Friend Shared ReadOnly Property ratingStar(ByVal Program As Program) As Integer
            Get
                If Program.StarRating > 0 Then
                    Return Program.StarRating
                Else
                    Return 0
                End If
            End Get
        End Property
        Friend Shared ReadOnly Property TvMovieStar(ByVal TvMovieProgram As TVMovieProgram) As String
            Get
                If TvMovieProgram.TVMovieBewertung > 0 Then
                    Return "ClickfinderPG_R" & TvMovieProgram.TVMovieBewertung & ".png"
                Else
                    Return ""
                End If
            End Get
        End Property
        Friend Shared ReadOnly Property TimeLabel(ByVal TvMovieProgram As TVMovieProgram) As String
            Get
                Dim _percent100 As Long
                Dim _percentX As Long

                _percent100 = DateDiff(DateInterval.Minute, TvMovieProgram.ReferencedProgram.StartTime, TvMovieProgram.ReferencedProgram.EndTime)
                _percentX = DateDiff(DateInterval.Minute, TvMovieProgram.ReferencedProgram.StartTime, Date.Now)

                Select Case TvMovieProgram.idSeries
                    Case Is = 0
                        If TvMovieProgram.local = True Then
                            'Movie/Video existiert lokal
                            Return Translation.existlocal
                        Else
                            If TvMovieProgram.ReferencedProgram.StartTime < Date.Now And TvMovieProgram.ReferencedProgram.EndTime > Date.Now Then
                                'läuft gerade
                                Return CStr(CInt(_percentX * 100 / _percent100) & "%") '& _
                                'CStr(Format(TvMovieProgram.ReferencedProgram.StartTime.Hour, "00") & ":" & Format(TvMovieProgram.ReferencedProgram.StartTime.Minute, "00"))
                            Else
                                'Keine Serie/Movie/Video -> Nur TimeLabel
                                Return CStr(Format(TvMovieProgram.ReferencedProgram.StartTime.Hour, "00") & ":" & Format(TvMovieProgram.ReferencedProgram.StartTime.Minute, "00"))
                            End If
                        End If
                    Case Is > 0
                        If TvMovieProgram.local = True Then
                            'Serie existiert lokal
                            Return Translation.existlocal
                        Else
                            If TvMovieProgram.ReferencedProgram.StartTime < Date.Now And TvMovieProgram.ReferencedProgram.EndTime > Date.Now Then
                                'Neue Episode - läuft gerade
                                Return Translation.NewLabel & " " & CStr(CInt(_percentX * 100 / _percent100) & "%")
                            Else
                                'Neue Episode
                                Return Translation.NewLabel & " " & CStr(Format(TvMovieProgram.ReferencedProgram.StartTime.Hour, "00") & ":" & Format(TvMovieProgram.ReferencedProgram.StartTime.Minute, "00"))
                            End If
                        End If
                    Case Else
                        Return "Error !"
                End Select
            End Get
        End Property

        Friend Shared ReadOnly Property Image(ByVal TvMovieProgram As TVMovieProgram) As String
            Get

                Dim _TvSeriesPoster As String = Config.GetFile(Config.Dir.Thumbs, "MPTVSeriesBanners\") & TvMovieProgram.SeriesPosterImage
                Dim _Cover As String = Config.GetFile(Config.Dir.Thumbs, "") & TvMovieProgram.Cover
                Dim _ChannelLogo As String = Config.GetFile(Config.Dir.Thumbs, "tv\logos\") & Replace(TvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName, "/", "_") & ".png"
                Dim _ClickfinderImage As String = ClickfinderDB.ImagePath & "\" & TvMovieProgram.BildDateiname
                'Dim _ClickfinderImage As String = _ChannelLogo

                'Image zunächst auf SenderLogo festlegen
                Select Case TvMovieProgram.idSeries
                    Case Is = 0
                        If TvMovieProgram.local = True Then
                            If Not String.IsNullOrEmpty(TvMovieProgram.Cover) Then
                                'Movie/Video existiert lokal -> Cover zeigen
                                Return _Cover

                            Else
                                'Kein Cover existiert -> SenderLogo / Clickfinder Bild anzeigen
                                If Not String.IsNullOrEmpty(TvMovieProgram.BildDateiname) Then
                                    Return _ClickfinderImage
                                Else
                                    Return _ChannelLogo
                                End If

                            End If
                        Else
                            'Keine Serie/Movie/Video -> SenderLogo anzeigen
                            If Not String.IsNullOrEmpty(TvMovieProgram.BildDateiname) Then
                                Return _ClickfinderImage
                            Else
                                Return _ChannelLogo
                            End If
                        End If
                    Case Is > 0
                        If Not String.IsNullOrEmpty(TvMovieProgram.SeriesPosterImage) Then
                            If Not String.IsNullOrEmpty(TvMovieProgram.SeriesPosterImage) Then
                                'Serie erkannt -> Series Poster anzeigen
                                Return _TvSeriesPoster
                            Else
                                'Kein Cover existiert -> SenderLogo / Clickfinder Bild anzeigen
                                If Not String.IsNullOrEmpty(TvMovieProgram.BildDateiname) Then
                                    Return _ClickfinderImage
                                Else
                                    Return _ChannelLogo
                                End If
                            End If
                        Else
                            'Kein Cover existiert -> SenderLogo / Clickfinder Bild anzeigen
                            If Not String.IsNullOrEmpty(TvMovieProgram.BildDateiname) Then
                                Return _ClickfinderImage
                            Else
                                Return _ChannelLogo
                            End If
                        End If
                    Case Else
                        MyLog.Warn("[Layout] [Image]: No Image found")
                        Return ""

                End Select
            End Get
        End Property

        Friend Shared ReadOnly Property InfoLabel(ByVal TvMovieProgram As TVMovieProgram) As String
            Get
                Dim _infoLabel As String = String.Empty

                Select TvMovieProgram.idSeries
                    Case Is = 0
                        'infoLabel format
                        If String.IsNullOrEmpty(TvMovieProgram.ReferencedProgram.EpisodeName) Then
                            _infoLabel = TvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName & vbNewLine & TvMovieProgram.ReferencedProgram.Genre
                        Else

                            'If TvMovieProgram.ReferencedProgram.EpisodeName = TvMovieProgram.ReferencedProgram.Title Then
                            _infoLabel = TvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName & vbNewLine & TvMovieProgram.ReferencedProgram.Genre
                            'Else
                            '_infoLabel = TvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName & vbNewLine & TvMovieProgram.ReferencedProgram.EpisodeName
                            'End If
                        End If

                        If Not String.IsNullOrEmpty(TvMovieProgram.KurzKritik) Then
                            _infoLabel = _infoLabel & vbNewLine & TvMovieProgram.KurzKritik
                        Else
                            If Not TvMovieProgram.ReferencedProgram.EpisodeName = TvMovieProgram.ReferencedProgram.Title Then
                                _infoLabel = _infoLabel & vbNewLine & Translation.EpisodePrefixLabel & " " & TvMovieProgram.ReferencedProgram.EpisodeName
                            End If
                        End If



                        Return _infoLabel

                    Case Is > 0
                        'Serie gefunden -> Serien formatierung
                        If TvMovieProgram.local = True Then
                            Return Translation.EpisodePrefixLabel & " " & TvMovieProgram.ReferencedProgram.EpisodeName & vbNewLine & _
                                                    Translation.Season & " " & TvMovieProgram.ReferencedProgram.SeriesNum & " " & Translation.Episode & " " & TvMovieProgram.ReferencedProgram.EpisodeNum
                        Else
                            Return Translation.EpisodeNewPrefixLabel & " " & TvMovieProgram.ReferencedProgram.EpisodeName & vbNewLine & _
                                                    Translation.Season & " " & TvMovieProgram.ReferencedProgram.SeriesNum & " " & Translation.Episode & " " & TvMovieProgram.ReferencedProgram.EpisodeNum
                        End If
                    Case Else
                        Return "Error !"
                End Select
            End Get
        End Property

        Friend Shared ReadOnly Property LastUpdateLabel() As String
            Get
                Dim _layer As New TvBusinessLayer
                Dim LastUpdate As Date = _layer.GetSetting("TvMovieLastUpdate").Value

                Select Case (DateDiff(DateInterval.Day, Date.Now, LastUpdate))
                    Case Is = 0
                        If LastUpdate.Day = Date.Now.Day Then
                            Return Translation.LastUpdate & " " & Translation.Today & " " & Translation.at & " " & _
                                Format(LastUpdate.Hour, "00") & ":" & Format(LastUpdate.Minute, "00")
                        Else
                            Return Translation.LastUpdate & " " & Translation.Yesterday & " " & Translation.at & " " & _
                                Format(LastUpdate.Hour, "00") & ":" & Format(LastUpdate.Minute, "00")
                        End If
                    Case Is = -1
                        Return Translation.LastUpdate & " " & Translation.Yesterday & " " & Translation.at & " " & _
                                Format(LastUpdate.Hour, "00") & ":" & Format(LastUpdate.Minute, "00")
                    Case Is = -2
                        Return Translation.LastUpdate & " " & Translation.TwoDaysAgo & " " & Translation.at & " " & _
                                Format(LastUpdate.Hour, "00") & ":" & Format(LastUpdate.Minute, "00")
                    Case Is < -2
                        Return Translation.LastUpdate & " " & Translation.before & " " & (DateDiff(DateInterval.Day, Date.Now, LastUpdate) * -1) & " " & Translation.Days
                End Select

                Return CStr(DateDiff(DateInterval.Day, Date.Now, LastUpdate))
            End Get
        End Property

    End Class

End Namespace
