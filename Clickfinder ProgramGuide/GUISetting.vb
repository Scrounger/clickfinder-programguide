Imports MediaPortal.GUI.Library
Imports TvDatabase

Namespace ClickfinderProgramGuide
    Public Class SettingGuiWindow
        Inherits GUIWindow


#Region "Skin Controls"

        <SkinControlAttribute(2)> Protected btnNow As GUIButtonControl = Nothing
        <SkinControlAttribute(3)> Protected btnPrimeTime As GUIButtonControl = Nothing
        <SkinControlAttribute(4)> Protected btnLateTime As GUIButtonControl = Nothing

        <SkinControlAttribute(5)> Protected TvSeries As GUICheckButton = Nothing


#End Region

        Public Shared Details_idProgram As Integer

        Private _TvSetting As New TvBusinessLayer

        Public Sub New()

        End Sub

        Public Overloads Overrides Property GetID() As Integer
            Get
                Return 1656544651
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property


        Public Overloads Overrides Function Init() As Boolean
            'Beim initialisieren des Plugin den Screen laden

            Return Load(GUIGraphicsContext.Skin + "\ClickfinderProgramGuideSetting.xml")
        End Function

        Public Overrides Function GetModuleName() As String
            Return "Clickfinder ProgramGuide Setting"
        End Function

        Protected Overrides Sub OnPageLoad()
            MyBase.OnPageLoad()

            Translator.SetProperty("#SettingLastUpdate", _TvSetting.GetSetting("TvMovieLastUpdate").Value)
            TvSeries.Selected = CBool(_TvSetting.GetSetting("TvMovieImportTvSeriesInfos").Value)

            'MsgBox(Details_idProgram)

        End Sub

        Protected Overrides Sub OnPageDestroy(ByVal new_windowId As Integer)
            MsgBox("Hallo")

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

            If control Is btnNow Then
                Translator.TranslateSkin()
            End If

            If control Is btnPrimeTime Then

            End If

        End Sub



    End Class
End Namespace
