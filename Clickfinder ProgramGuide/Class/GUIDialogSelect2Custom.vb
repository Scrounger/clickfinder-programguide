#Region "Copyright (C) 2005-2011 Team MediaPortal"

' Copyright (C) 2005-2011 Team MediaPortal
' http://www.team-mediaportal.com
' 
' MediaPortal is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 2 of the License, or
' (at your option) any later version.
' 
' MediaPortal is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
' GNU General Public License for more details.
' 
' You should have received a copy of the GNU General Public License
' along with MediaPortal. If not, see <http://www.gnu.org/licenses/>.

#End Region


Imports System.Collections
Imports MediaPortal.GUI.Library
Imports MediaPortal.ExtensionMethods

Imports MediaPortal.Dialogs
Imports MediaPortal.GUI.Library.GUIWindow

Namespace ClickfinderProgramGuide
    ''' <summary>
    ''' 
    ''' </summary>
    Public Class GUIDialogSelect2Custom
        Inherits GUIDialogWindow

        Private Enum Controls
            CONTROL_BACKGROUND = 1
            CONTROL_LIST = 3
            CONTROL_HEADING = 4
            CONTROL_BACKGROUNDDLG = 6
        End Enum

        Private m_strSelected As String = ""
        Private m_vecList As New ArrayList()

        Public Sub New()
            GetID = 1656544655
        End Sub

        Public Overloads Overrides Function Init() As Boolean
            Return Load(GUIGraphicsContext.Skin + "\ClickfinderProgramGuideDialogSelect2.xml")
        End Function

        Public Overloads Overrides Function OnMessage(ByVal message As GUIMessage) As Boolean
            '      needRefresh = true;
            Select Case message.Message
                Case GUIMessage.MessageType.GUI_MSG_WINDOW_DEINIT
                    If True Then
                        SetControlLabel(GetID, CInt(Controls.CONTROL_HEADING), String.Empty)
                        MyBase.OnMessage(message)
                        Return True
                    End If

                Case GUIMessage.MessageType.GUI_MSG_WINDOW_INIT
                    If True Then
                        MyBase.OnMessage(message)
                        ClearControl(GetID, CInt(Controls.CONTROL_LIST))

                        For i As Integer = 0 To m_vecList.Count - 1
                            Dim pItem As GUIListItem = DirectCast(m_vecList(i), GUIListItem)
                            AddListItemControl(GetID, CInt(Controls.CONTROL_LIST), pItem)
                        Next

                        If _selectedLabel >= 0 Then
                            Dim msg As New GUIMessage(GUIMessage.MessageType.GUI_MSG_ITEM_SELECT, GetID, 0, CInt(Controls.CONTROL_LIST), _selectedLabel, 0, _
                             Nothing)
                            OnMessage(msg)
                        End If
                        _selectedLabel = -1
                    End If
                    Return True

                Case GUIMessage.MessageType.GUI_MSG_CLICKED
                    If True Then
                        Dim iControl As Integer = message.SenderControlId
                        If CInt(Controls.CONTROL_LIST) = iControl Then
                            _selectedLabel = GetSelectedItemNo()
                            m_strSelected = GetSelectedItem().Label
                            PageDestroy()
                        End If
                    End If
                    Exit Select
            End Select

            Return MyBase.OnMessage(message)
        End Function

        Public Overloads Overrides Sub Reset()
            MyBase.Reset()
            m_vecList.DisposeAndClearList()
        End Sub

        Public Overloads Sub Add(ByVal strLabel As String)
            Dim pItem As New GUIListItem(strLabel)
            m_vecList.Add(pItem)
        End Sub

        Public Overloads Sub Add(ByVal pItem As GUIListItem)
            m_vecList.Add(pItem)
        End Sub

        Public ReadOnly Property SelectedLabelText() As String
            Get
                Return m_strSelected
            End Get
        End Property

        Public Sub SetHeading(ByVal strLine As String)
            Reset()
            'LoadSkin();
            AllocResources()
            InitControls()

            SetControlLabel(GetID, CInt(Controls.CONTROL_HEADING), strLine)
        End Sub


        Public Sub SetHeading(ByVal iString As Integer)
            SetHeading(GUILocalizeStrings.[Get](iString))
        End Sub

        Private Function GetSelectedItem() As GUIListItem
            Dim msg As New GUIMessage(GUIMessage.MessageType.GUI_MSG_GET_SELECTED_ITEM, GetID, 0, CInt(Controls.CONTROL_LIST), 0, 0, _
             Nothing)
            OnMessage(msg)
            Return DirectCast(msg.[Object], GUIListItem)
        End Function

        Private Function GetItem(ByVal iItem As Integer) As GUIListItem
            Dim msg As New GUIMessage(GUIMessage.MessageType.GUI_MSG_GET_ITEM, GetID, 0, CInt(Controls.CONTROL_LIST), iItem, 0, _
             Nothing)
            OnMessage(msg)
            Return DirectCast(msg.[Object], GUIListItem)
        End Function

        Private Function GetSelectedItemNo() As Integer
            Dim msg As New GUIMessage(GUIMessage.MessageType.GUI_MSG_ITEM_SELECTED, GetID, 0, CInt(Controls.CONTROL_LIST), 0, 0, _
             Nothing)
            OnMessage(msg)
            Dim iItem As Integer = msg.Param1
            Return iItem
        End Function

        Private Function GetItemCount() As Integer
            Dim msg As New GUIMessage(GUIMessage.MessageType.GUI_MSG_ITEMS, GetID, 0, CInt(Controls.CONTROL_LIST), 0, 0, _
             Nothing)
            OnMessage(msg)
            Return msg.Param1
        End Function

        Private Sub ClearControl(ByVal iWindowId As Integer, ByVal iControlId As Integer)
            Dim msg As New GUIMessage(GUIMessage.MessageType.GUI_MSG_LABEL_RESET, iWindowId, 0, iControlId, 0, 0, _
             Nothing)
            OnMessage(msg)
        End Sub

        Private Sub AddListItemControl(ByVal iWindowId As Integer, ByVal iControlId As Integer, ByVal item As GUIListItem)
            Dim msg As New GUIMessage(GUIMessage.MessageType.GUI_MSG_LABEL_ADD, iWindowId, 0, iControlId, 0, 0, _
             item)
            OnMessage(msg)
        End Sub
    End Class
End Namespace
