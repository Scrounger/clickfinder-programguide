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
            Translator.SetProperty("#DetailTitle", _tvMovieProgram.ReferencedProgram.Title)
            Translator.SetProperty("#DetailorgTitle", _tvMovieProgram.ReferencedProgram.EpisodeName)
            Translator.SetProperty("#DetailImage", GuiLayout.Image(_tvMovieProgram))
            Translator.SetProperty("#DetailTvMovieStar", GuiLayout.TvMovieStar(_tvMovieProgram))
            Translator.SetProperty("#DetailRatingStar", GuiLayout.ratingStar(_tvMovieProgram.ReferencedProgram))
            Translator.SetProperty("#DetailTime", GuiLayout.TimeLabel(_tvMovieProgram))
            Translator.SetProperty("#DetailDuration", DateDiff(DateInterval.Minute, _tvMovieProgram.ReferencedProgram.StartTime, _tvMovieProgram.ReferencedProgram.EndTime) & " " & Translation.MinuteLabel)
            Translator.SetProperty("#DetailChannel", _tvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName)

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
#End Region

    End Class
End Namespace
