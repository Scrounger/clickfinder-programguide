Imports MediaPortal.GUI.Library
Imports TvDatabase

Namespace ClickfinderProgramGuide
    Public Class DetailGuiWindow
        Inherits GUIWindow

#Region "Skin Controls"


#End Region

#Region "Members"
        Public Shared Details_idProgram As Integer

        Private _layer As New TvBusinessLayer

#End Region

#Region "Constructors"
        Public Sub New()

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
            Translator.SetProperty("#SettingLastUpdate", Details_idProgram)

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
