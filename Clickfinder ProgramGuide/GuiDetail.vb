Imports MediaPortal.GUI.Library
Imports TvDatabase
Imports ClickfinderProgramGuide.TvDatabase


Namespace ClickfinderProgramGuide
    Public Class DetailGuiWindow
        Inherits GUIWindow

#Region "Skin Controls"


#End Region

#Region "Members"
        Friend Shared _tvMovieProgram As TVMovieProgram
        Private _layer As New TvBusinessLayer

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub

        Friend Shared Sub SetGuiProperties(ByVal TvMovieProgram As tvmovieprogram)
            _tvMovieProgram = TvMovieProgram
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


            MyLog.Info("")
            MyLog.Info("")
            MyLog.Info("[DetailGuiWindow] -------------[OPEN]-------------")
            MyLog.Debug("[DetailGuiWindow] [OnPageLoad]: {0}, idProgram = {1}, needsUpdate = {2}", _tvMovieProgram.ReferencedProgram.Title, _tvMovieProgram.idProgram, _tvMovieProgram.needsUpdate)

            If _tvMovieProgram.needsUpdate = True Then
                Dim _ClickfinderDB As New ClickfinderDB(_tvMovieProgram.ReferencedProgram)

                'Wenn HD Sender -> Dolby = true, Hd = true
                If InStr(_tvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName, " HD") > 0 Then
                    _tvMovieProgram.Dolby = True
                    _tvMovieProgram.HDTV = True
                Else
                    'Daten aus ClickfinderDB 
                    If _ClickfinderDB.Count > 0 Then
                        _tvMovieProgram.Dolby = _ClickfinderDB(0).Dolby
                        _tvMovieProgram.HDTV = _ClickfinderDB(0).KzHDTV
                    End If
                End If

                If Not String.IsNullOrEmpty(_ClickfinderDB(0).Herstellungsjahr) Then
                    _tvMovieProgram.Year = CDate("01.01." & _ClickfinderDB(0).Herstellungsjahr)
                End If

                If Not String.IsNullOrEmpty(_ClickfinderDB(0).Herstellungsland) Then
                    _tvMovieProgram.Country = _ClickfinderDB(0).Herstellungsland
                End If

                If Not String.IsNullOrEmpty(_ClickfinderDB(0).Regie) Then
                    _tvMovieProgram.Regie = _ClickfinderDB(0).Regie
                End If

                _tvMovieProgram.needsUpdate = False
                _tvMovieProgram.Persist()
            End If

            Translator.SetProperty("#DetailTitle", _tvMovieProgram.ReferencedProgram.Title)
            Translator.SetProperty("#DetailorgTitle", _tvMovieProgram.ReferencedProgram.EpisodeName)
            Translator.SetProperty("#DetailImage", GuiLayout.Image(_tvMovieProgram))
            Translator.SetProperty("#DetailTvMovieStar", GuiLayout.TvMovieStar(_tvMovieProgram))
            Translator.SetProperty("#DetailRatingStar", GuiLayout.ratingStar(_tvMovieProgram.ReferencedProgram))
            Translator.SetProperty("#DetailTime", GuiLayout.TimeLabel(_tvMovieProgram))
            Translator.SetProperty("#DetailDuration", DateDiff(DateInterval.Minute, _tvMovieProgram.ReferencedProgram.StartTime, _tvMovieProgram.ReferencedProgram.EndTime) & " " & Translation.MinuteLabel)
            Translator.SetProperty("#DetailChannel", _tvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName)
            Translator.SetProperty("#DetailGenre", _tvMovieProgram.ReferencedProgram.Genre)
            Translator.SetProperty("#DetailYear", _tvMovieProgram.Year.Year & " ")
            Translator.SetProperty("#DetailCountry", _tvMovieProgram.Country)
            Translator.SetProperty("#DetailRegie", _tvMovieProgram.Regie)
            Translator.SetProperty("#DetailActors", Replace(_tvMovieProgram.Actors, ";", ", "))
            Translator.SetProperty("#DetailKritik", _tvMovieProgram.KurzKritik)

            If _tvMovieProgram.Dolby = True Then
                Translator.SetProperty("#DetailAudioImage", "Logos\ac-3 dolby digital.png")
            Else
                Translator.SetProperty("#DetailAudioImage", "Logos\stereo.png")
            End If

            If _tvMovieProgram.HDTV = True Then
                Translator.SetProperty("#DetailProgramFormat", "Logos\hd.png")
            Else
                Translator.SetProperty("#DetailProgramFormat", "Logos\sd.png")
            End If

            Translator.SetProperty("#DetailRatingFun", getRatingpercentage(_tvMovieProgram.Fun))
            Translator.SetProperty("#DetailRatingAction", getRatingpercentage(_tvMovieProgram.Action))
            Translator.SetProperty("#DetailRatingErotic", getRatingpercentage(_tvMovieProgram.Erotic))
            Translator.SetProperty("#DetailRatingSpannung", getRatingpercentage(_tvMovieProgram.Tension))
            Translator.SetProperty("#DetailRatingAnspruch", getRatingpercentage(_tvMovieProgram.Requirement))
            Translator.SetProperty("#DetailRatingFeelings", getRatingpercentage(_tvMovieProgram.Feelings))

            'MsgBox(Details_idProgram)
            MyBase.OnPageLoad()
        End Sub

        Protected Overrides Sub OnPageDestroy(ByVal new_windowId As Integer)
            'MsgBox("Hallo")

            MyBase.OnPageDestroy(new_windowId)
        End Sub

        Public Overrides Sub OnAction(ByVal action As MediaPortal.GUI.Library.Action)

            MyBase.OnAction(action)
        End Sub

        Protected Overrides Sub OnClicked(ByVal controlId As Integer, _
                                  ByVal control As GUIControl, _
                                  ByVal actionType As  _
                                  MediaPortal.GUI.Library.Action.ActionType)

            MyBase.OnClicked(controlId, control, actionType)

            'If control Is ctlList Then
            '    ListControlClick()
            'End If


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

    End Class
End Namespace
