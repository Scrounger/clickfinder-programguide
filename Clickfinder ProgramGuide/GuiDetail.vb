Imports MediaPortal.GUI.Library
Imports TvDatabase
Imports ClickfinderProgramGuide.TvDatabase
Imports MediaPortal.Configuration
Imports MediaPortal.Util



Namespace ClickfinderProgramGuide
    Public Class DetailGuiWindow
        Inherits GUIWindow

#Region "Skin Controls"

        'Buttons
        <SkinControlAttribute(2)> Protected _btnNow As GUIButtonControl = Nothing
        <SkinControlAttribute(3)> Protected _btnPrimeTime As GUIButtonControl = Nothing
        <SkinControlAttribute(4)> Protected _btnLateTime As GUIButtonControl = Nothing
        <SkinControlAttribute(5)> Protected _btnAllMovies As GUIButtonControl = Nothing
        <SkinControlAttribute(6)> Protected _btnHighlights As GUIButtonControl = Nothing
        <SkinControlAttribute(7)> Protected _btnPreview As GUIButtonControl = Nothing


        <SkinControlAttribute(10)> Protected _btnBack As GUIButtonControl = Nothing
        <SkinControlAttribute(11)> Protected _btnPlay As GUIButtonControl = Nothing
        <SkinControlAttribute(12)> Protected _btnRecord As GUIButtonControl = Nothing
        <SkinControlAttribute(13)> Protected _btnRemember As GUIButtonControl = Nothing

#End Region

#Region "Members"
        Friend Shared _DetailTvMovieProgram As TVMovieProgram
        Private _layer As New TvBusinessLayer

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub

        Friend Shared Sub SetGuiProperties(ByVal TvMovieProgram As TVMovieProgram)
            _DetailTvMovieProgram = TvMovieProgram
        End Sub
#End Region

#Region "GUI Properties"
        Public Overloads Overrides Property GetID() As Integer
            Get
                Return 1656544652
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property


        Public Overloads Overrides Function Init() As Boolean
            'Beim initialisieren des Plugin den Screen laden

            Return Load(GUIGraphicsContext.Skin + "\ClickfinderProgramGuideDetail.xml")
        End Function

        Public Overrides Function GetModuleName() As String
            Return "Clickfinder ProgramGuide Details"
        End Function
#End Region

#Region "GUI Events"

        Protected Overrides Sub OnPageLoad()
            MyBase.OnPageLoad()
            GUIWindowManager.NeedRefresh()

            Try

                MyLog.Info("")
                MyLog.Info("")
                MyLog.Info("[DetailGuiWindow] -------------[OPEN]-------------")
                MyLog.Debug("[DetailGuiWindow] [OnPageLoad]: {0}, idProgram = {1}, needsUpdate = {2}", _DetailTvMovieProgram.ReferencedProgram.Title, _DetailTvMovieProgram.idProgram, _DetailTvMovieProgram.needsUpdate)

                Helper.CheckConnectionState(GetID)
                ShowDetails()

            Catch ex As Exception
                MyLog.Error("[DetailGuiWindow]: [OnPageLoad]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try

            'MsgBox(Details_idProgram)

        End Sub

        Protected Overrides Sub OnPageDestroy(ByVal new_windowId As Integer)
            'MsgBox("Hallo")

            MyBase.OnPageDestroy(new_windowId)
        End Sub

        Public Overrides Sub OnAction(ByVal action As MediaPortal.GUI.Library.Action)
            MyBase.OnAction(action)

            'Play Button (P) -> Start channel
            If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MUSIC_PLAY Then
                Try
                    MyLog.[Debug]("[DetailGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)
                    Helper.StartTv(_DetailTvMovieProgram.ReferencedProgram.ReferencedChannel)
                Catch ex As Exception
                    MyLog.Error("[Play Button]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
                End Try
            End If

            'Record Button (R) -> MP TvProgramInfo aufrufen --Über keychar--
            If action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_KEY_PRESSED Then
                If action.m_key IsNot Nothing Then
                    If action.m_key.KeyChar = 114 Then
                        MyLog.[Debug]("[DetailGuiWindow] [OnAction]: Keypress - KeyChar={0} ; KeyCode={1} ; Actiontype={2}", action.m_key.KeyChar, action.m_key.KeyCode, action.wID.ToString)

                        Helper.LoadTVProgramInfo(_DetailTvMovieProgram.ReferencedProgram)
                    End If
                End If
            End If

        End Sub

        Protected Overrides Sub OnClicked(ByVal controlId As Integer, _
                                  ByVal control As GUIControl, _
                                  ByVal actionType As  _
                                  MediaPortal.GUI.Library.Action.ActionType)

            MyBase.OnClicked(controlId, control, actionType)

            If control Is _btnNow Then
                CategoriesGuiWindow.SetGuiProperties(CategoriesGuiWindow.CategorieView.Now)
                GUIWindowManager.ActivateWindow(1656544654)
            End If

            If control Is _btnPrimeTime Then
                CategoriesGuiWindow.SetGuiProperties(CategoriesGuiWindow.CategorieView.PrimeTime)
                GUIWindowManager.ActivateWindow(1656544654)
            End If
            If control Is _btnLateTime Then

                CategoriesGuiWindow.SetGuiProperties(CategoriesGuiWindow.CategorieView.LateTime)
                GUIWindowManager.ActivateWindow(1656544654)
            End If

            If control Is _btnHighlights Then

                GUIWindowManager.ActivateWindow(165654465)
            End If

            If control Is _btnPreview Then

                CategoriesGuiWindow.SetGuiProperties(CategoriesGuiWindow.CategorieView.Preview)
                GUIWindowManager.ActivateWindow(1656544654)
            End If

            If control Is _btnBack Then

                GUIWindowManager.ShowPreviousWindow()

            End If

            If control Is _btnRecord Then
                Helper.LoadTVProgramInfo(_DetailTvMovieProgram.ReferencedProgram)
            End If

            If control Is _btnRemember Then
                Helper.SetNotify(_DetailTvMovieProgram.ReferencedProgram)
            End If

            If control Is _btnPlay Then
                Helper.StartTv(_DetailTvMovieProgram.ReferencedProgram.ReferencedChannel)
            End If

        End Sub

        Private Function getRatingpercentage(ByVal TvMovieRating As Integer) As Integer
            Select Case TvMovieRating
                Case Is = 0
                    Return 0
                Case Is = 1
                    Return 5
                Case Is = 2
                    Return 8
                Case Is = 3
                    Return 10
            End Select

        End Function


#End Region

#Region "Functions"
        Private Sub ShowDetails()

            Try

                If _DetailTvMovieProgram.needsUpdate = True Then
                    UpdateProgramData()
                End If

                Translator.SetProperty("#DetailTitle", _DetailTvMovieProgram.ReferencedProgram.Title)

                Translator.SetProperty("#DetailorgTitle", GuiLayout.DetailOrgTitle(_DetailTvMovieProgram))

                'If _DetailTvMovieProgram.idSeries > 0 And CBool(_layer.GetSetting("TvMovieImportTvSeriesInfos", "false").Value) = True Then
                '    Translator.SetProperty("#DetailImage", "")
                'Else
                Translator.SetProperty("#DetailImage", GuiLayout.Image(_DetailTvMovieProgram))
                'End If

                Translator.SetProperty("#DetailTvMovieStar", GuiLayout.TvMovieStar(_DetailTvMovieProgram))
                Translator.SetProperty("#DetailRatingStar", GuiLayout.ratingStar(_DetailTvMovieProgram.ReferencedProgram))
                Translator.SetProperty("#DetailTime", GuiLayout.TimeLabel(_DetailTvMovieProgram))
                Translator.SetProperty("#DetailDuration", DateDiff(DateInterval.Minute, _DetailTvMovieProgram.ReferencedProgram.StartTime, _DetailTvMovieProgram.ReferencedProgram.EndTime) & " " & Translation.MinuteLabel)
                Translator.SetProperty("#DetailChannel", _DetailTvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName)
                Translator.SetProperty("#DetailGenre", _DetailTvMovieProgram.ReferencedProgram.Genre)

                Translator.SetProperty("#DetailRegie", _DetailTvMovieProgram.Regie)
                Translator.SetProperty("#DetailActors", Replace(_DetailTvMovieProgram.Actors, ";", ", "))
                Translator.SetProperty("#DetailKritik", _DetailTvMovieProgram.KurzKritik)
                Translator.SetProperty("#DetailDescription", _DetailTvMovieProgram.Describtion)

                If Not _DetailTvMovieProgram.Year < New Date(1900, 1, 1) Then
                    Translator.SetProperty("#DetailYear", _DetailTvMovieProgram.Year.Year & " ")
                Else
                    Translator.SetProperty("#DetailYear", "")
                End If

                Translator.SetProperty("#DetailCountry", _DetailTvMovieProgram.Country)

                If _DetailTvMovieProgram.Dolby = True Then
                    Translator.SetProperty("#DetailAudioImage", "Logos\ClickfinderPG\dolby.png")
                Else
                    Translator.SetProperty("#DetailAudioImage", "Logos\ClickfinderPG\stereo.png")
                End If

                If _DetailTvMovieProgram.HDTV = True Then
                    Translator.SetProperty("#DetailProgramFormat", "Logos\ClickfinderPG\hd.png")
                Else
                    Translator.SetProperty("#DetailProgramFormat", "Logos\ClickfinderPG\sd.png")
                End If

                If Not String.IsNullOrEmpty(_DetailTvMovieProgram.FanArt) Then
                    Translator.SetProperty("#DetailFanArt", Config.GetFile(Config.Dir.Thumbs, "") & _DetailTvMovieProgram.FanArt)
                Else
                    Translator.SetProperty("#DetailFanArt", "")
                End If

                Translator.SetProperty("#DetailRatingFun", getRatingpercentage(_DetailTvMovieProgram.Fun))
                Translator.SetProperty("#DetailRatingAction", getRatingpercentage(_DetailTvMovieProgram.Action))
                Translator.SetProperty("#DetailRatingErotic", getRatingpercentage(_DetailTvMovieProgram.Erotic))
                Translator.SetProperty("#DetailRatingSuspense", getRatingpercentage(_DetailTvMovieProgram.Tension))
                Translator.SetProperty("#DetailRatingLevel", getRatingpercentage(_DetailTvMovieProgram.Requirement))
                Translator.SetProperty("#DetailRatingEmotions", getRatingpercentage(_DetailTvMovieProgram.Feelings))

            Catch ex As Exception
                MyLog.Error("[DetailGuiWindow]: [ShowDetails]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try
        End Sub

        Private Sub UpdateProgramData()

            Try

                MyLog.Debug("[DetailGuiWindow]: [UpdateProgramData]: program needs update")

                Dim _ClickfinderDB As New ClickfinderDB(_DetailTvMovieProgram.ReferencedProgram, True)

                'Wenn HD Sender -> Dolby = true, Hd = true
                If InStr(_DetailTvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName, " HD") > 0 Then
                    _DetailTvMovieProgram.Dolby = True
                    _DetailTvMovieProgram.HDTV = True
                Else
                    'Daten aus ClickfinderDB 
                    If _ClickfinderDB.Count > 0 Then
                        _DetailTvMovieProgram.Dolby = _ClickfinderDB(0).Dolby
                        _DetailTvMovieProgram.HDTV = _ClickfinderDB(0).KzHDTV
                    End If
                End If

                'Jahr
                If Not String.IsNullOrEmpty(_ClickfinderDB(0).Herstellungsjahr) Then
                    _DetailTvMovieProgram.Year = CDate("01.01." & _ClickfinderDB(0).Herstellungsjahr)
                End If

                'Country
                If Not String.IsNullOrEmpty(_ClickfinderDB(0).Herstellungsland) Then
                    _DetailTvMovieProgram.Country = _ClickfinderDB(0).Herstellungsland
                End If

                'Regie
                If Not String.IsNullOrEmpty(_ClickfinderDB(0).Regie) Then
                    _DetailTvMovieProgram.Regie = _ClickfinderDB(0).Regie
                End If

                'Describtion
                If _DetailTvMovieProgram.idSeries > 0 And CBool(_layer.GetSetting("TvMovieImportTvSeriesInfos", "false").Value) = True And CBool(_layer.GetSetting("ClickfinderDetailUseSeriesDescribtion", "false").Value) = True Then
                    'Wenn Serie erkannt dann -> Episoden Beschreibung aus TvSeriesDB laden (sofern aktiviert & vorhanden)
                    Dim _Series As New TVSeriesDB
                    _Series.LoadEpisodebyEpsiodeID(_DetailTvMovieProgram.idSeries, _DetailTvMovieProgram.idEpisode)
                    _DetailTvMovieProgram.Describtion = "SeriesDescribtion" & vbNewLine & _Series.EpisodeSummary
                Else
                    If Not String.IsNullOrEmpty(_ClickfinderDB(0).Beschreibung) Then
                        _DetailTvMovieProgram.Describtion = "TvMovieDescribtion" & vbNewLine & Replace(_ClickfinderDB(0).Beschreibung, "<br>", vbNewLine)
                    ElseIf Not String.IsNullOrEmpty(_DetailTvMovieProgram.ReferencedProgram.Description) Then
                        _DetailTvMovieProgram.Describtion = "EPGDescribtion" & vbNewLine & _DetailTvMovieProgram.ReferencedProgram.Description
                    End If
                End If

                'Short Describtion
                If Not String.IsNullOrEmpty(_ClickfinderDB(0).KurzBeschreibung) Then
                    _DetailTvMovieProgram.ShortDescribtion = _ClickfinderDB(0).KurzBeschreibung
                End If


                'Bewertungen String aus Clickfinder DB holen, zerlegen, einzel Bewertungen extrahieren
                If Not String.IsNullOrEmpty(_ClickfinderDB(0).Bewertungen) Then
                    ' We want to split this input string
                    Dim s As String = _ClickfinderDB(0).Bewertungen

                    ' Split string based on spaces
                    Dim words As String() = s.Split(New Char() {";"c})

                    ' Use For Each loop over words and display them
                    Dim word As String
                    For Each word In words
                        'MsgBox(Left(word, InStr(word, "=") - 1))

                        'MsgBox(CInt(Right(word, word.Length - InStr(word, "="))))

                        Select Case Left(word, InStr(word, "=") - 1)
                            Case Is = "Spaß"
                                _DetailTvMovieProgram.Fun = CInt(Right(word, word.Length - InStr(word, "=")))
                            Case Is = "Action"
                                _DetailTvMovieProgram.Action = CInt(Right(word, word.Length - InStr(word, "=")))
                            Case Is = "Erotik"
                                _DetailTvMovieProgram.Erotic = CInt(Right(word, word.Length - InStr(word, "=")))
                            Case Is = "Spannung"
                                _DetailTvMovieProgram.Tension = CInt(Right(word, word.Length - InStr(word, "=")))
                            Case Is = "Anspruch"
                                _DetailTvMovieProgram.Requirement = CInt(Right(word, word.Length - InStr(word, "=")))
                            Case Is = "Gefühl"
                                _DetailTvMovieProgram.Feelings = CInt(Right(word, word.Length - InStr(word, "=")))
                        End Select
                    Next
                End If

                'Actors aus Clickfinder DB holen, sofern vorhanden
                If Not String.IsNullOrEmpty(_ClickfinderDB(0).Darsteller) Then
                    _DetailTvMovieProgram.Actors = _ClickfinderDB(0).Darsteller
                End If

                _DetailTvMovieProgram.needsUpdate = False
                _DetailTvMovieProgram.Persist()


            Catch ex As Exception
                MyLog.Error("[DetailGuiWindow]: [UpdateProgramData]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try

        End Sub
#End Region

    End Class
End Namespace
