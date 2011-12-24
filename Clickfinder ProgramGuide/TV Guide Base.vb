#Region "Copyright (C) 2005-2010 Team MediaPortal"

' Copyright (C) 2005-2010 Team MediaPortal
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

#Region "usings"

Imports System.Linq
Imports System.Collections
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Windows.Media.Animation
Imports MediaPortal.Dialogs
Imports MediaPortal.GUI.Library
Imports MediaPortal.GUI.Video
Imports MediaPortal.Player
Imports MediaPortal.Profile
Imports MediaPortal.Util
Imports MediaPortal.Video.Database
Imports TvControl
Imports TvDatabase
Imports Action = MediaPortal.GUI.Library.Action

#End Region

Namespace TvPlugin
	''' <summary>
	''' 
	''' </summary>
	Public Class TvGuideBase
		Inherits GuideBase
		Implements IMDB.IProgress
		#Region "constants"

		' Start for numbering IDs of automaticaly generated TVguide components for channels and programs

		Private _loopDelay As Integer = 100
		' wait at the last item this amount of msec until loop to the first item
		Private Const _skinPropertyPrefix As String = "#TV"

		#End Region

		Protected Overrides ReadOnly Property SkinPropertyPrefix() As String
			Get
				Return _skinPropertyPrefix
			End Get
		End Property

		#Region "enums"

		Private Enum Controls
			PANEL_BACKGROUND = 2
			SPINCONTROL_DAY = 6
			SPINCONTROL_TIME_INTERVAL = 8
			CHANNEL_IMAGE_TEMPLATE = 7
			CHANNEL_LABEL_TEMPLATE = 18
			LABEL_GENRE_TEMPLATE = 23
			LABEL_TITLE_TEMPLATE = 24
			VERTICAL_LINE = 25
			LABEL_TITLE_DARK_TEMPLATE = 26
			LABEL_GENRE_DARK_TEMPLATE = 30
			CHANNEL_TEMPLATE = 20
			' Channel rectangle and row height
			HORZ_SCROLLBAR = 28
			VERT_SCROLLBAR = 29
			LABEL_TIME1 = 40
			' first and template
			IMG_CHAN1 = 50
			IMG_CHAN1_LABEL = 70
			IMG_TIME1 = 90
			' first and template
			IMG_REC_PIN = 31
			SINGLE_CHANNEL_LABEL = 32
			SINGLE_CHANNEL_IMAGE = 33
			LABEL_KEYED_CHANNEL = 34
			BUTTON_PROGRAM_RUNNING = 35
			BUTTON_PROGRAM_NOT_RUNNING = 36
			BUTTON_PROGRAM_NOTIFY = 37
			BUTTON_PROGRAM_RECORD = 38
			BUTTON_PROGRAM_PARTIAL_RECORD = 39

			CHANNEL_GROUP_BUTTON = 100
		End Enum

		#End Region

		#Region "variables"

		Private ReadOnly _recordingsExpectedLock As New Object()
		Private ReadOnly _recordingsExpected As New List(Of Channel)()
		Private _updateTimerRecExpected As DateTime = DateTime.Now
		Private _recordingList As IList(Of Schedule) = New List(Of Schedule)()
		Private _controls As New Dictionary(Of Integer, GUIButton3PartControl)()

		Private _currentTitle As String = [String].Empty
		Private _currentTime As String = [String].Empty
		Private _currentRecOrNotify As Boolean = False
		Private _currentStartTime As Long = 0
		Private _currentEndTime As Long = 0
		Private _currentProgram As Program = Nothing
		Private _needUpdate As Boolean = False
		Private m_dtStartTime As DateTime = DateTime.Now
		Private _colorList As New ArrayList()
		Private _programOffset As Integer = 0
		Private _totalProgramCount As Integer = 0
		Private _server As TvServer = Nothing
		Private _programs As IList(Of Program) = Nothing

		Private _backupCursorX As Integer = 0
		Private _backupCursorY As Integer = 0
		Private _backupChannelOffset As Integer = 0

		Private _keyPressedTimer As DateTime = DateTime.Now
		Private _lineInput As String = [String].Empty

		Private _byIndex As Boolean = False
		Private _showChannelNumber As Boolean = False
		Private _channelNumberMaxLength As Integer = 3
		Private _useNewRecordingButtonColor As Boolean = False
		Private _useNewPartialRecordingButtonColor As Boolean = False
		Private _useNewNotifyButtonColor As Boolean = False
		Private _recalculateProgramOffset As Boolean
		Private _useHdProgramIcon As Boolean = False
		Private _hdtvProgramText As String = [String].Empty
		Private _guideContinuousScroll As Boolean = False

		' current minimum/maximum indexes
		'private int MaxXIndex; // means rows here (channels)
		Private MinYIndex As Integer
		' means cols here (programs/time)
		Protected _lastCommandTime As Double = 0

		''' <summary>
		''' Logic to decide if channel group button is available and visible
		''' </summary>
		Protected ReadOnly Property GroupButtonAvail() As Boolean
			Get
				' show/hide channel group button
				Dim btnChannelGroup As GUIButtonControl = TryCast(GetControl(CInt(Controls.CHANNEL_GROUP_BUTTON)), GUIButtonControl)

				' visible only if more than one group? and not in single channel, and button exists in skin!
				Return (TVHome.Navigator.Groups.Count > 1 AndAlso Not _singleChannelView AndAlso btnChannelGroup IsNot Nothing)
			End Get
		End Property

		#End Region

		#Region "ctor"

		Public Sub New()
			_colorList.Add(Color.Red)
			_colorList.Add(Color.Green)
			_colorList.Add(Color.Blue)
			_colorList.Add(Color.Cyan)
			_colorList.Add(Color.Magenta)
			_colorList.Add(Color.DarkBlue)
			_colorList.Add(Color.Brown)
			_colorList.Add(Color.Fuchsia)
			_colorList.Add(Color.Khaki)
			_colorList.Add(Color.SteelBlue)
			_colorList.Add(Color.SaddleBrown)
			_colorList.Add(Color.Chocolate)
			_colorList.Add(Color.DarkMagenta)
			_colorList.Add(Color.DarkSeaGreen)
			_colorList.Add(Color.Coral)
			_colorList.Add(Color.DarkGray)
			_colorList.Add(Color.DarkOliveGreen)
			_colorList.Add(Color.DarkOrange)
			_colorList.Add(Color.ForestGreen)
			_colorList.Add(Color.Honeydew)
			_colorList.Add(Color.Gray)
			_colorList.Add(Color.Tan)
			_colorList.Add(Color.Silver)
			_colorList.Add(Color.SeaShell)
			_colorList.Add(Color.RosyBrown)
			_colorList.Add(Color.Peru)
			_colorList.Add(Color.OldLace)
			_colorList.Add(Color.PowderBlue)
			_colorList.Add(Color.SpringGreen)
			_colorList.Add(Color.LightSalmon)
		End Sub

		#End Region

		#Region "Serialisation"

		Private Sub LoadSettings()
			Using xmlreader As Settings = New MPSettings()
				Dim channelName As [String] = xmlreader.GetValueAsString("tvguide", "channel", [String].Empty)
				Dim layer As New TvBusinessLayer()
				Dim channels As IList(Of Channel) = layer.GetChannelsByName(channelName)
				If channels IsNot Nothing AndAlso channels.Count > 0 Then
					_currentChannel = channels(0)
				End If
				_cursorX = xmlreader.GetValueAsInt("tvguide", "ypos", 0)
				ChannelOffset = xmlreader.GetValueAsInt("tvguide", "yoffset", 0)
				_byIndex = xmlreader.GetValueAsBool("mytv", "byindex", True)
				_showChannelNumber = xmlreader.GetValueAsBool("mytv", "showchannelnumber", False)
				_channelNumberMaxLength = xmlreader.GetValueAsInt("mytv", "channelnumbermaxlength", 3)
				_timePerBlock = xmlreader.GetValueAsInt("tvguide", "timeperblock", 30)
				_hdtvProgramText = xmlreader.GetValueAsString("mytv", "hdtvProgramText", "(HDTV)")
				_guideContinuousScroll = xmlreader.GetValueAsBool("mytv", "continuousScrollGuide", False)
				_loopDelay = xmlreader.GetValueAsInt("gui", "listLoopDelay", 0)
			End Using
			_useNewRecordingButtonColor = Utils.FileExistsInCache(Path.Combine(GUIGraphicsContext.Skin, "media\tvguide_recButton_Focus_middle.png"))
			_useNewPartialRecordingButtonColor = Utils.FileExistsInCache(Path.Combine(GUIGraphicsContext.Skin, "media\tvguide_partRecButton_Focus_middle.png"))
			_useNewNotifyButtonColor = Utils.FileExistsInCache(Path.Combine(GUIGraphicsContext.Skin, "media\tvguide_notifyButton_Focus_middle.png"))
			_useHdProgramIcon = Utils.FileExistsInCache(Path.Combine(GUIGraphicsContext.Skin, "media\tvguide_hd_program.png"))
		End Sub

		Private Sub SaveSettings()
			Using xmlwriter As Settings = New MPSettings()
				xmlwriter.SetValue("tvguide", "channel", _currentChannel)
				xmlwriter.SetValue("tvguide", "ypos", _cursorX.ToString())
				xmlwriter.SetValue("tvguide", "yoffset", ChannelOffset.ToString())
				xmlwriter.SetValue("tvguide", "timeperblock", _timePerBlock)
			End Using
		End Sub

		#End Region

		#Region "overrides"

		Public Overrides Function GetFocusControlId() As Integer
			Dim focusedId As Integer = MyBase.GetFocusControlId()
			If _cursorX >= 0 OrElse focusedId = CInt(Controls.SPINCONTROL_DAY) OrElse focusedId = CInt(Controls.SPINCONTROL_TIME_INTERVAL) OrElse focusedId = CInt(Controls.CHANNEL_GROUP_BUTTON) Then
				Return focusedId
			Else
				Return -1
			End If
		End Function

		Protected Sub Initialize()
			_server = New TvServer()
		End Sub

		Public Overrides Sub OnAction(action__1 As Action)
			Select Case action__1.wID
				Case Action.ActionType.ACTION_PREVIOUS_MENU
					If _singleChannelView Then
						OnSwitchMode()
							' base.OnAction would close the EPG as well
						Return
					Else
						GUIWindowManager.ShowPreviousWindow()
						Return
					End If

				Case Action.ActionType.ACTION_KEY_PRESSED
					If action__1.m_key IsNot Nothing Then
						OnKeyCode(CChar(action__1.m_key.KeyChar))
					End If
					Exit Select

				Case Action.ActionType.ACTION_RECORD
					If (GetFocusControlId() <> -1) AndAlso (_cursorY > 0) AndAlso (_cursorX >= 0) Then
						OnRecord()
					End If
					Exit Select

				Case Action.ActionType.ACTION_MOUSE_MOVE
					If True Then
						Dim x As Integer = CInt(action__1.fAmount1)
						Dim y As Integer = CInt(action__1.fAmount2)
						For Each control As GUIControl In controlList
							If control.GetID >= CInt(Controls.IMG_CHAN1) + 0 AndAlso control.GetID <= CInt(Controls.IMG_CHAN1) + _channelCount Then
								If x >= control.XPosition AndAlso x < control.XPosition + control.Width Then
									If y >= control.YPosition AndAlso y < control.YPosition + control.Height Then
										UnFocus()
										_cursorX = control.GetID - CInt(Controls.IMG_CHAN1)
										_cursorY = 0

										If _singleChannelNumber <> _cursorX + ChannelOffset Then
											Update(False)
										End If
										UpdateCurrentProgram()
										UpdateHorizontalScrollbar()
										UpdateVerticalScrollbar()
										updateSingleChannelNumber()
										Return
									End If
								End If
							End If
							If control.GetID >= GUIDE_COMPONENTID_START Then
								If x >= control.XPosition AndAlso x < control.XPosition + control.Width Then
									If y >= control.YPosition AndAlso y < control.YPosition + control.Height Then
										Dim iControlId As Integer = control.GetID
										If iControlId >= GUIDE_COMPONENTID_START Then
											iControlId -= GUIDE_COMPONENTID_START
											Dim iCursorY As Integer = (iControlId / RowID)
											iControlId -= iCursorY * RowID
											If iControlId Mod ColID = 0 Then
												Dim iCursorX As Integer = (iControlId / ColID) + 1
												If iCursorY <> _cursorX OrElse iCursorX <> _cursorY Then
													UnFocus()
													_cursorX = iCursorY
													_cursorY = iCursorX
													UpdateCurrentProgram()
													SetFocus()
													UpdateHorizontalScrollbar()
													UpdateVerticalScrollbar()
													updateSingleChannelNumber()
													Return
												End If
												Return
											End If
										End If
									End If
								End If
							End If
						Next
						UnFocus()
						_cursorY = -1
						_cursorX = -1
						MyBase.OnAction(action__1)
					End If
					Exit Select

				Case Action.ActionType.ACTION_TVGUIDE_RESET
					_cursorY = 0
					_viewingTime = DateTime.Now
					Update(False)
					Exit Select


				Case Action.ActionType.ACTION_CONTEXT_MENU
					If True Then
						If _cursorY >= 0 AndAlso _cursorX >= 0 Then
							If _cursorY = 0 Then
								OnSwitchMode()
								Return
							Else
								ShowContextMenu()
							End If
						Else
							action__1.wID = Action.ActionType.ACTION_SELECT_ITEM
							GUIWindowManager.OnAction(action__1)
						End If
					End If
					Exit Select

				Case Action.ActionType.ACTION_PAGE_UP
					OnPageUp()
					updateSingleChannelNumber()
					Exit Select

				Case Action.ActionType.ACTION_PAGE_DOWN
					OnPageDown()
					updateSingleChannelNumber()
					Exit Select

				Case Action.ActionType.ACTION_MOVE_LEFT
					If True Then
						If _cursorX >= 0 Then
							OnLeft()
							updateSingleChannelNumber()
							UpdateHorizontalScrollbar()
							Return
						End If
					End If
					Exit Select
				Case Action.ActionType.ACTION_MOVE_RIGHT
					If True Then
						If _cursorX >= 0 Then
							OnRight()
							UpdateHorizontalScrollbar()
							Return
						End If
					End If
					Exit Select
				Case Action.ActionType.ACTION_MOVE_UP
					If True Then
						If _cursorX >= 0 Then
							OnUp(True, False)
							updateSingleChannelNumber()
							UpdateVerticalScrollbar()
							Return
						End If
					End If
					Exit Select
				Case Action.ActionType.ACTION_MOVE_DOWN
					If True Then
						If _cursorX >= 0 Then
							OnDown(True)
							updateSingleChannelNumber()
							UpdateVerticalScrollbar()
						Else
							_cursorX = 0
							SetFocus()
							updateSingleChannelNumber()
							UpdateVerticalScrollbar()
						End If
						Return
					End If
				'break;
				Case Action.ActionType.ACTION_SHOW_INFO
					If True Then
						ShowContextMenu()
					End If
					Exit Select
				Case Action.ActionType.ACTION_INCREASE_TIMEBLOCK
					If True Then
						_timePerBlock += 15
						If _timePerBlock > 60 Then
							_timePerBlock = 60
						End If
						Dim cntlTimeInterval As GUISpinControl = TryCast(GetControl(CInt(Controls.SPINCONTROL_TIME_INTERVAL)), GUISpinControl)
						cntlTimeInterval.Value = (_timePerBlock / 15) - 1
						Update(False)
						SetFocus()
					End If
					Exit Select

				Case Action.ActionType.ACTION_REWIND, Action.ActionType.ACTION_MUSIC_REWIND
					_viewingTime = _viewingTime.AddHours(-3)
					Update(False)
					SetFocus()
					Exit Select

				Case Action.ActionType.ACTION_FORWARD, Action.ActionType.ACTION_MUSIC_FORWARD
					_viewingTime = _viewingTime.AddHours(3)
					Update(False)
					SetFocus()
					Exit Select

				Case Action.ActionType.ACTION_DECREASE_TIMEBLOCK
					If True Then
						If _timePerBlock > 15 Then
							_timePerBlock -= 15
						End If
						Dim cntlTimeInterval As GUISpinControl = TryCast(GetControl(CInt(Controls.SPINCONTROL_TIME_INTERVAL)), GUISpinControl)
						cntlTimeInterval.Value = (_timePerBlock / 15) - 1
						Update(False)
						SetFocus()
					End If
					Exit Select
				Case Action.ActionType.ACTION_DEFAULT_TIMEBLOCK
					If True Then
						_timePerBlock = 30
						Dim cntlTimeInterval As GUISpinControl = TryCast(GetControl(CInt(Controls.SPINCONTROL_TIME_INTERVAL)), GUISpinControl)
						cntlTimeInterval.Value = (_timePerBlock / 15) - 1
						Update(False)
						SetFocus()
					End If
					Exit Select
				Case Action.ActionType.ACTION_TVGUIDE_INCREASE_DAY
					OnNextDay()
					Exit Select

				Case Action.ActionType.ACTION_TVGUIDE_DECREASE_DAY
					OnPreviousDay()
					Exit Select
				' TV group changing actions
				Case Action.ActionType.ACTION_TVGUIDE_NEXT_GROUP
					OnChangeChannelGroup(1)
					Exit Select

				Case Action.ActionType.ACTION_TVGUIDE_PREV_GROUP
					OnChangeChannelGroup(-1)
					Exit Select
			End Select
			MyBase.OnAction(action__1)
		End Sub

		Protected Sub OnNotify()
			If _currentProgram Is Nothing Then
				Return
			End If

			_currentProgram.Notify = Not _currentProgram.Notify

			' get the right db instance of current prog before we store it
			' currentProgram is not a ref to the real entity
			Dim modifiedProg As Program = Program.RetrieveByTitleTimesAndChannel(_currentProgram.Title, _currentProgram.StartTime, _currentProgram.EndTime, _currentProgram.IdChannel)
			modifiedProg.Notify = _currentProgram.Notify
			modifiedProg.Persist()
			TvNotifyManager.OnNotifiesChanged()
			Update(False)
			SetFocus()
		End Sub

		''' <summary>
		''' changes the current channel group and refreshes guide display
		''' </summary>
		''' <param name="Direction"></param>
		Protected Overridable Sub OnChangeChannelGroup(Direction As Integer)
			' in single channel view there would be errors when changing group
			If _singleChannelView Then
				Return
			End If
			Dim newIndex As Integer, oldIndex As Integer
			Dim countGroups As Integer = TVHome.Navigator.Groups.Count
			' all
			newIndex = InlineAssignHelper(oldIndex, TVHome.Navigator.CurrentGroupIndex)
			If (newIndex >= 1 AndAlso Direction < 0) OrElse (newIndex < countGroups - 1 AndAlso Direction > 0) Then
					' change group
				newIndex += Direction
			' Cycle handling
			ElseIf (newIndex = countGroups - 1) AndAlso Direction > 0 Then
				newIndex = 0
			ElseIf newIndex = 0 AndAlso Direction < 0 Then
				newIndex = countGroups - 1
			End If

			If oldIndex <> newIndex Then
				' update list
				GUIWaitCursor.Show()
				TVHome.Navigator.SetCurrentGroup(newIndex)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.Group", TVHome.Navigator.CurrentGroup.GroupName)

				_cursorY = 1
				' cursor should be on the program guide item
				ChannelOffset = 0
				' reset to top; otherwise focus could be out of screen if new group has less then old position
				_cursorX = 0
				' first channel
				GetChannels(True)
				Update(False)
				SetFocus()
				GUIWaitCursor.Hide()
			End If
		End Sub

		Private Sub UpdateOverlayAllowed()
			If _isOverlayAllowedCondition = 0 Then
				Return
			End If
			Dim bWasAllowed As Boolean = GUIGraphicsContext.Overlay
			_isOverlayAllowed = GUIInfoManager.GetBool(_isOverlayAllowedCondition, GetID)
			If bWasAllowed <> _isOverlayAllowed Then
				GUIGraphicsContext.Overlay = _isOverlayAllowed
			End If
		End Sub


		Public Overrides Function OnMessage(message As GUIMessage) As Boolean
			Try
				Select Case message.Message
					Case GUIMessage.MessageType.GUI_MSG_PERCENTAGE_CHANGED
						If message.SenderControlId = CInt(Controls.HORZ_SCROLLBAR) Then
							_needUpdate = True
							Dim fPercentage As Single = CSng(message.Param1)
							fPercentage /= 100F
							fPercentage *= 24F
							fPercentage *= 60F
							_viewingTime = New DateTime(_viewingTime.Year, _viewingTime.Month, _viewingTime.Day, 0, 0, 0, _
								0)
							_viewingTime = _viewingTime.AddMinutes(CInt(Math.Truncate(fPercentage)))
						End If

						If message.SenderControlId = CInt(Controls.VERT_SCROLLBAR) Then
							_needUpdate = True
							Dim fPercentage As Single = CSng(message.Param1)
							fPercentage /= 100F
							If _singleChannelView Then
								fPercentage *= CSng(_totalProgramCount)
								Dim iChan As Integer = CInt(Math.Truncate(fPercentage))
								ChannelOffset = 0
								_cursorX = 0
								While iChan >= _channelCount
									iChan -= _channelCount
									ChannelOffset += _channelCount
								End While
								_cursorX = iChan
							Else
								fPercentage *= CSng(_channelList.Count)
								Dim iChan As Integer = CInt(Math.Truncate(fPercentage))
								ChannelOffset = 0
								_cursorX = 0
								While iChan >= _channelCount
									iChan -= _channelCount
									ChannelOffset += _channelCount
								End While
								_cursorX = iChan
							End If
						End If
						Exit Select

					Case GUIMessage.MessageType.GUI_MSG_WINDOW_DEINIT
						If True Then
							MyBase.OnMessage(message)
							SaveSettings()
							_recordingList.Clear()

							_controls = New Dictionary(Of Integer, GUIButton3PartControl)()
							_channelList = Nothing
							_recordingList = Nothing
							_currentProgram = Nothing

							Return True
						End If

					Case GUIMessage.MessageType.GUI_MSG_WINDOW_INIT
						If True Then
							TVHome.WaitForGentleConnection()

							GUIPropertyManager.SetProperty("#itemcount", String.Empty)
							GUIPropertyManager.SetProperty("#selecteditem", String.Empty)
							GUIPropertyManager.SetProperty("#selecteditem2", String.Empty)
							GUIPropertyManager.SetProperty("#selectedthumb", String.Empty)

							If _shouldRestore Then
								DoRestoreSkin()
							Else
								LoadSkin()
								AllocResources()
							End If

							InitControls()

							MyBase.OnMessage(message)

							UpdateOverlayAllowed()
							GUIGraphicsContext.Overlay = _isOverlayAllowed

							' set topbar autohide
							Select Case _autoHideTopbarType
								Case AutoHideTopBar.No
									_autoHideTopbar = False
									Exit Select
								Case AutoHideTopBar.Yes
									_autoHideTopbar = True
									Exit Select
								Case Else
									_autoHideTopbar = GUIGraphicsContext.DefaultTopBarHide
									Exit Select
							End Select
							GUIGraphicsContext.AutoHideTopBar = _autoHideTopbar
							GUIGraphicsContext.TopBarHidden = _autoHideTopbar
							GUIGraphicsContext.DisableTopBar = _disableTopBar
							LoadSettings()
							UpdateChannelCount()

							Dim isPreviousWindowTvGuideRelated As Boolean = (message.Param1 = CInt(Window.WINDOW_TV_PROGRAM_INFO) OrElse message.Param1 = CInt(Window.WINDOW_VIDEO_INFO))

							If Not isPreviousWindowTvGuideRelated Then
								UnFocus()
							End If

							GetChannels(True)
							LoadSchedules(True)
							_currentProgram = Nothing
							If Not isPreviousWindowTvGuideRelated Then
								_viewingTime = DateTime.Now
								_cursorY = 0
								_cursorX = 0
								ChannelOffset = 0
								_singleChannelView = False
								_showChannelLogos = False
								If TVHome.Card.IsTimeShifting Then
									_currentChannel = TVHome.Navigator.Channel
									For i As Integer = 0 To _channelList.Count - 1
										Dim chan As Channel = DirectCast(_channelList(i), GuideChannel).channel
										If chan.IdChannel = _currentChannel.IdChannel Then
											_cursorX = i
											Exit For
										End If
									Next
								End If
							End If
							While _cursorX >= _channelCount
								_cursorX -= _channelCount
								ChannelOffset += _channelCount
							End While
							' Mantis 3579: the above lines can lead to too large channeloffset. 
							' Now we check if the offset is too large, and if it is, we reduce it and increase the cursor position accordingly
							If Not _guideContinuousScroll AndAlso (ChannelOffset > _channelList.Count - _channelCount) AndAlso (_channelList.Count - _channelCount > 0) Then
								_cursorX += ChannelOffset - (_channelList.Count - _channelCount)
								ChannelOffset = _channelList.Count - _channelCount
							End If
							Dim cntlDay As GUISpinControl = TryCast(GetControl(CInt(Controls.SPINCONTROL_DAY)), GUISpinControl)
							If cntlDay IsNot Nothing Then
								Dim dtNow As DateTime = DateTime.Now
								cntlDay.Reset()
								cntlDay.SetRange(0, MaxDaysInGuide - 1)
								For iDay As Integer = 0 To MaxDaysInGuide - 1
									Dim dtTemp As DateTime = dtNow.AddDays(iDay)
									Dim day As String
									Select Case dtTemp.DayOfWeek
										Case DayOfWeek.Monday
											day = GUILocalizeStrings.[Get](657)
											Exit Select
										Case DayOfWeek.Tuesday
											day = GUILocalizeStrings.[Get](658)
											Exit Select
										Case DayOfWeek.Wednesday
											day = GUILocalizeStrings.[Get](659)
											Exit Select
										Case DayOfWeek.Thursday
											day = GUILocalizeStrings.[Get](660)
											Exit Select
										Case DayOfWeek.Friday
											day = GUILocalizeStrings.[Get](661)
											Exit Select
										Case DayOfWeek.Saturday
											day = GUILocalizeStrings.[Get](662)
											Exit Select
										Case Else
											day = GUILocalizeStrings.[Get](663)
											Exit Select
									End Select
									day = [String].Format("{0} {1}-{2}", day, dtTemp.Day, dtTemp.Month)
									cntlDay.AddLabel(day, iDay)
								Next
							Else
								Log.Debug("TvGuideBase: SpinControl cntlDay is null!")
							End If

							Dim cntlTimeInterval As GUISpinControl = TryCast(GetControl(CInt(Controls.SPINCONTROL_TIME_INTERVAL)), GUISpinControl)
							If cntlTimeInterval IsNot Nothing Then
								cntlTimeInterval.Reset()
								For i As Integer = 1 To 4
									cntlTimeInterval.AddLabel([String].Empty, i)
								Next
								cntlTimeInterval.Value = (_timePerBlock / 15) - 1
							Else
								Log.Debug("TvGuideBase: SpinControl cntlTimeInterval is null!")
							End If

							If Not isPreviousWindowTvGuideRelated Then
								Update(True)
							Else
								Update(False)
							End If

							SetFocus()

							If _currentProgram IsNot Nothing Then
								m_dtStartTime = _currentProgram.StartTime
							End If
							UpdateCurrentProgram()

							Return True
						End If
					'break;

					Case GUIMessage.MessageType.GUI_MSG_CLICKED
						Dim iControl As Integer = message.SenderControlId
						If iControl = CInt(Controls.SPINCONTROL_DAY) Then
							Dim cntlDay As GUISpinControl = TryCast(GetControl(CInt(Controls.SPINCONTROL_DAY)), GUISpinControl)
							Dim iDay As Integer = cntlDay.Value

							_viewingTime = DateTime.Now
							_viewingTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, _viewingTime.Hour, _viewingTime.Minute, 0, _
								0)
							_viewingTime = _viewingTime.AddDays(iDay)
							_recalculateProgramOffset = True
							Update(False)
							SetFocus()
							Return True
						End If
						If iControl = CInt(Controls.SPINCONTROL_TIME_INTERVAL) Then
							Dim cntlTimeInt As GUISpinControl = TryCast(GetControl(CInt(Controls.SPINCONTROL_TIME_INTERVAL)), GUISpinControl)
							Dim iInterval As Integer = (cntlTimeInt.Value) + 1
							If iInterval > 4 Then
								iInterval = 4
							End If
							_timePerBlock = iInterval * 15
							Update(False)
							SetFocus()
							Return True
						End If
						If iControl = CInt(Controls.CHANNEL_GROUP_BUTTON) Then
							OnSelectChannelGroup()
							Return True
						End If
						If iControl >= GUIDE_COMPONENTID_START Then
							OnSelectItem(True)
							Update(False)
							SetFocus()
						ElseIf _cursorY = 0 Then
							OnSwitchMode()
						End If
						Exit Select
				End Select
			Catch ex As Exception
				Log.Debug("TvGuideBase: {0}", ex)
			End Try
			Return MyBase.OnMessage(message)
			

		End Function

		''' <summary>
		''' Shows channel group selection dialog
		''' </summary>
		Protected Overridable Sub OnSelectChannelGroup()
			' only if more groups present and not in singleChannelView
			If TVHome.Navigator.Groups.Count > 1 AndAlso Not _singleChannelView Then
				Dim prevGroup As Integer = TVHome.Navigator.CurrentGroup.IdGroup

				TVHome.OnSelectGroup()

				If prevGroup <> TVHome.Navigator.CurrentGroup.IdGroup Then
					GUIWaitCursor.Show()
					' button focus should be on tvgroup, so change back to channel name
					'if (_cursorY == -1)
					'	_cursorY = 0;
					_cursorY = 1
					' cursor should be on the program guide item
					ChannelOffset = 0
					' reset to top; otherwise focus could be out of screen if new group has less then old position
					_cursorX = 0
					' set to top, otherwise index could be out of range in new group
					' group has been changed
					GetChannels(True)
					Update(False)

					SetFocus()
					GUIWaitCursor.Hide()
				End If
			End If
		End Sub

		Public Overrides Sub Process()
			TVHome.UpdateProgressPercentageBar()

			OnKeyTimeout()
			UpdateRecStateOnExpectedRecordings()

			If _needUpdate Then
				_needUpdate = False
				Update(False)
				SetFocus()
			End If

			Dim vertLine As GUIImage = TryCast(GetControl(CInt(Controls.VERTICAL_LINE)), GUIImage)
			If vertLine IsNot Nothing Then
				If _singleChannelView Then
					vertLine.IsVisible = False
				Else
					vertLine.IsVisible = True

					Dim dateNow As DateTime = DateTime.Now.[Date]
					Dim datePrev As DateTime = _viewingTime.[Date]
					Dim ts As TimeSpan = dateNow - datePrev
					If ts.TotalDays = 1 Then
						_viewingTime = DateTime.Now
					End If


					If _viewingTime.[Date].Equals(DateTime.Now.[Date]) AndAlso _viewingTime < DateTime.Now Then
						Dim iStartX As Integer = GetControl(CInt(Controls.LABEL_TIME1)).XPosition
						Dim iWidth As Integer = GetControl(CInt(Controls.LABEL_TIME1) + 1).XPosition - iStartX
						iWidth *= 4

						Dim iMin As Integer = _viewingTime.Minute
						Dim iStartTime As Integer = _viewingTime.Hour * 60 + iMin
						Dim iCurTime As Integer = DateTime.Now.Hour * 60 + DateTime.Now.Minute
						If iCurTime >= iStartTime Then
							iCurTime -= iStartTime
						Else
							iCurTime = 24 * 60 + iCurTime - iStartTime
						End If

						Dim iTimeWidth As Integer = (_numberOfBlocks * _timePerBlock)
						Dim fpos As Single = CSng(iCurTime) / CSng(iTimeWidth)
						fpos *= CSng(iWidth)
						fpos += CSng(iStartX)
						Dim width As Integer = vertLine.Width / 2
						vertLine.IsVisible = True
						vertLine.SetPosition(CInt(Math.Truncate(fpos)) - width, vertLine.YPosition)
						vertLine.[Select](0)
						ts = DateTime.Now - _updateTimer
						If ts.TotalMinutes >= 1 Then
							If (DateTime.Now - _viewingTime).TotalMinutes >= iTimeWidth \ 2 Then
								_cursorY = 0
								_viewingTime = DateTime.Now
							End If
							Update(False)
						End If
					Else
						vertLine.IsVisible = False
					End If
				End If
			End If
		End Sub

		Private Sub UpdateRecStateOnExpectedRecordings()
			'if we did a manual rec. on the tvguide directly, then we have to wait for it to start and the update the GUI.
			Dim wasAnyRecordingExpectedStarted As Boolean = False
			SyncLock _recordingsExpectedLock
				If _recordingsExpected.Count > 0 Then
					Dim ts As TimeSpan = DateTime.Now - _updateTimerRecExpected
					If ts.TotalMilliseconds > 1000 Then
						_updateTimerRecExpected = DateTime.Now
						Dim recordingsExpectedToRemove = New List(Of Channel)()
						For Each recordingExpected As Channel In _recordingsExpected
							Dim card As VirtualCard
							If _server.IsRecording(recordingExpected.IdChannel, card) Then
								wasAnyRecordingExpectedStarted = True
								recordingsExpectedToRemove.Add(recordingExpected)
							End If
						Next

						_recordingsExpected.RemoveAll(AddressOf recordingsExpectedToRemove.Contains)
					End If
				End If
			End SyncLock

			If wasAnyRecordingExpectedStarted Then
				GetChannels(True)
				LoadSchedules(True)
				_needUpdate = True
			End If
		End Sub

		Public Overrides Sub Render(timePassed As Single)
			SyncLock Me
				Dim vertLine As GUIImage = TryCast(GetControl(CInt(Controls.VERTICAL_LINE)), GUIImage)
				MyBase.Render(timePassed)
				If vertLine IsNot Nothing Then
					vertLine.Render(timePassed)
				End If
			End SyncLock
		End Sub

		#End Region

		#Region "private members"

		Private Function GetChannelLogo(strChannel As String) As String
			Dim strLogo As String = Utils.GetCoverArt(Thumbs.TVChannel, strChannel)
			If String.IsNullOrEmpty(strLogo) Then
				' Check for a default TV channel logo.
				strLogo = Utils.GetCoverArt(Thumbs.TVChannel, "default")
				If String.IsNullOrEmpty(strLogo) Then
					strLogo = "defaultVideoBig.png"
				End If
			End If
			Return strLogo
		End Function

		Private Sub SetProperties()
			Dim bRecording As Boolean = False

			If _channelList Is Nothing Then
				Return
			End If
			If _channelList.Count = 0 Then
				Return
			End If
			Dim channel As Integer = _cursorX + ChannelOffset
			While channel >= _channelList.Count
				channel -= _channelList.Count
			End While
			If channel < 0 Then
				channel = 0
			End If
			Dim chan As Channel = DirectCast(_channelList(channel).channel, Channel)
			If chan Is Nothing Then
				Return
			End If
			Dim strChannel As String = chan.DisplayName

			If Not _singleChannelView Then
				Dim strLogo As String = GetChannelLogo(strChannel)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.thumb", strLogo)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.ChannelName", strChannel)
				If _showChannelNumber Then
					Dim detail As IList(Of TuningDetail) = chan.ReferringTuningDetail()
					Dim channelNum As Integer = detail(0).ChannelNumber
					GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.ChannelNumber", channelNum & "")
				Else
					GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.ChannelNumber", [String].Empty)
				End If
			End If

			If _cursorY = 0 OrElse _currentProgram Is Nothing Then
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.Title", [String].Empty)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.CompositeTitle", [String].Empty)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.Time", [String].Empty)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.Description", [String].Empty)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.Genre", [String].Empty)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.SubTitle", [String].Empty)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.Episode", [String].Empty)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.EpisodeDetail", [String].Empty)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.Date", [String].Empty)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.StarRating", [String].Empty)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.Classification", [String].Empty)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.Duration", [String].Empty)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.DurationMins", [String].Empty)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.TimeFromNow", [String].Empty)

				_currentStartTime = 0
				_currentEndTime = 0
				_currentTitle = [String].Empty
				_currentTime = [String].Empty
				_currentChannel = chan
				GUIControl.HideControl(GetID, CInt(Controls.IMG_REC_PIN))
			ElseIf _currentProgram IsNot Nothing Then
				Dim strTime As String = [String].Format("{0}-{1}", _currentProgram.StartTime.ToString("t", CultureInfo.CurrentCulture.DateTimeFormat), _currentProgram.EndTime.ToString("t", CultureInfo.CurrentCulture.DateTimeFormat))

				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.Title", _currentProgram.Title)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.CompositeTitle", TVUtil.GetDisplayTitle(_currentProgram))
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.Time", strTime)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.Description", _currentProgram.Description)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.Genre", _currentProgram.Genre)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.Duration", GetDuration(_currentProgram))
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.DurationMins", GetDurationAsMinutes(_currentProgram))
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.TimeFromNow", GetStartTimeFromNow(_currentProgram))
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.Episode", _currentProgram.EpisodeNumber)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.SubTitle", _currentProgram.EpisodeName)

				If _currentProgram.Classification = "" Then
					GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.Classification", "No Rating")
				Else
					GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.Classification", _currentProgram.Classification)
				End If

				_currentStartTime = Utils.datetolong(_currentProgram.StartTime)
				_currentEndTime = Utils.datetolong(_currentProgram.EndTime)
				_currentTitle = _currentProgram.Title
				_currentTime = strTime
				_currentChannel = chan

				Dim bSeries As Boolean = _currentProgram.IsRecordingSeries OrElse _currentProgram.IsRecordingSeriesPending OrElse _currentProgram.IsPartialRecordingSeriesPending
				Dim bConflict As Boolean = _currentProgram.HasConflict
				bRecording = bSeries OrElse (_currentProgram.IsRecording OrElse _currentProgram.IsRecordingOncePending)

				If bRecording Then
					Dim img As GUIImage = DirectCast(GetControl(CInt(Controls.IMG_REC_PIN)), GUIImage)

					Dim bPartialRecording As Boolean = _currentProgram.IsPartialRecordingSeriesPending

					If bConflict Then
						If bSeries Then
							img.SetFileName(If(bPartialRecording, Thumbs.TvConflictPartialRecordingSeriesIcon, Thumbs.TvConflictRecordingSeriesIcon))
						Else
							img.SetFileName(If(bPartialRecording, Thumbs.TvConflictPartialRecordingIcon, Thumbs.TvConflictRecordingIcon))
						End If
					ElseIf bSeries Then
						If bPartialRecording Then
							img.SetFileName(Thumbs.TvPartialRecordingSeriesIcon)
						Else
							img.SetFileName(Thumbs.TvRecordingSeriesIcon)
						End If
					Else
						If bPartialRecording Then
							img.SetFileName(Thumbs.TvPartialRecordingIcon)
						Else
							img.SetFileName(Thumbs.TvRecordingIcon)
						End If
					End If
					GUIControl.ShowControl(GetID, CInt(Controls.IMG_REC_PIN))
				Else
					GUIControl.HideControl(GetID, CInt(Controls.IMG_REC_PIN))
				End If
			End If

			_currentRecOrNotify = bRecording

			If Not _currentRecOrNotify AndAlso _currentProgram IsNot Nothing Then
				If _currentProgram.Notify Then
					_currentRecOrNotify = True
				End If
			End If
		End Sub

		'void SetProperties()

		Protected Overrides Sub RenderSingleChannel(channel As Channel)
			Dim strLogo As String
			Dim chan As Integer = ChannelOffset
			For iChannel As Integer = 0 To _channelCount - 1
				If chan < _channelList.Count Then
					Dim tvChan As Channel = _channelList(chan).channel

					strLogo = GetChannelLogo(tvChan.DisplayName)
					Dim img As GUIButton3PartControl = TryCast(GetControl(iChannel + CInt(Controls.IMG_CHAN1)), GUIButton3PartControl)
					If img IsNot Nothing Then
						If _showChannelLogos Then
							img.TexutureIcon = strLogo
						End If
						img.Label1 = tvChan.DisplayName
						img.Data = tvChan
						img.IsVisible = True
					End If
				End If
				chan += 1
			Next

			Dim channelLabel As GUILabelControl = TryCast(GetControl(CInt(Controls.SINGLE_CHANNEL_LABEL)), GUILabelControl)
			Dim channelImage As GUIImage = TryCast(GetControl(CInt(Controls.SINGLE_CHANNEL_IMAGE)), GUIImage)

			strLogo = GetChannelLogo(channel.DisplayName)
			If channelImage Is Nothing Then
				If strLogo.Length > 0 Then
					channelImage = New GUIImage(GetID, CInt(Controls.SINGLE_CHANNEL_IMAGE), GetControl(CInt(Controls.LABEL_TIME1)).XPosition, GetControl(CInt(Controls.LABEL_TIME1)).YPosition - 15, 40, 40, _
						strLogo, Color.White)
					channelImage.AllocResources()
					Dim temp As GUIControl = DirectCast(channelImage, GUIControl)
					Add(temp)
				End If
			Else
				channelImage.SetFileName(strLogo)
			End If

			If channelLabel Is Nothing Then
				channelLabel = New GUILabelControl(GetID, CInt(Controls.SINGLE_CHANNEL_LABEL), channelImage.XPosition + 44, channelImage.YPosition + 10, 300, 40, _
					"font16", channel.DisplayName, 4294967295UI, GUIControl.Alignment.Left, GUIControl.VAlignment.Top, True, _
					0, 0, &Hff000000UI)
				channelLabel.AllocResources()
				Dim temp As GUIControl = channelLabel
				Add(temp)
			End If

			setSingleChannelLabelVisibility(True)

			channelLabel.Label = channel.DisplayName
			If strLogo.Length > 0 Then
				channelImage.SetFileName(strLogo)
			End If

			If channelLabel IsNot Nothing Then
				channelLabel.Label = channel.DisplayName
			End If
			If _recalculateProgramOffset Then
				_programs = New List(Of Program)()

				Dim dtStart As DateTime = DateTime.Now
				dtStart = dtStart.AddDays(-1)

				Dim dtEnd As DateTime = dtStart.AddDays(30)

				Dim layer As New TvBusinessLayer()
				_programs = layer.GetPrograms(channel, dtStart, dtEnd)

				_totalProgramCount = _programs.Count
				If _totalProgramCount = 0 Then
					_totalProgramCount = _channelCount
				End If

				_recalculateProgramOffset = False
				Dim found As Boolean = False
				For i As Integer = 0 To _programs.Count - 1
					Dim program__1 As Program = DirectCast(_programs(i), Program)
					If program__1.StartTime <= _viewingTime AndAlso program__1.EndTime >= _viewingTime Then
						_programOffset = i
						found = True
						Exit For
					End If
				Next
				If Not found Then
					_programOffset = 0
				End If
			ElseIf _programOffset < _programs.Count Then
				Dim day As Integer = DirectCast(_programs(_programOffset), Program).StartTime.DayOfYear
				Dim changed As Boolean = False
				While day > _viewingTime.DayOfYear
					_viewingTime = _viewingTime.AddDays(1.0)
					changed = True
				End While
				While day < _viewingTime.DayOfYear
					_viewingTime = _viewingTime.AddDays(-1.0)
					changed = True
				End While
				If changed Then
					Dim cntlDay As GUISpinControl = TryCast(GetControl(CInt(Controls.SPINCONTROL_DAY)), GUISpinControl)

					' Find first day in TVGuide and set spincontrol position
					Dim iDay As Integer = CalcDays()
					While iDay < 0
						_viewingTime = _viewingTime.AddDays(1.0)
						iDay += 1
					End While
					While iDay >= MaxDaysInGuide
						_viewingTime = _viewingTime.AddDays(-1.0)
						iDay -= 1
					End While
					cntlDay.Value = iDay
				End If
			End If
			' ichan = number of rows
			For ichan As Integer = 0 To _channelCount - 1
				Dim imgCh As GUIButton3PartControl = TryCast(GetControl(ichan + CInt(Controls.IMG_CHAN1)), GUIButton3PartControl)
				imgCh.TexutureIcon = ""

				Dim iStartXPos As Integer = GetControl(0 + CInt(Controls.LABEL_TIME1)).XPosition
				Dim height As Integer = GetControl(CInt(Controls.IMG_CHAN1) + 1).YPosition
				height -= GetControl(CInt(Controls.IMG_CHAN1)).YPosition
				Dim width As Integer = GetControl(CInt(Controls.LABEL_TIME1) + 1).XPosition
				width -= GetControl(CInt(Controls.LABEL_TIME1)).XPosition

				Dim iTotalWidth As Integer = width * _numberOfBlocks

				Dim program__1 As Program
				Dim offset As Integer = _programOffset
				If offset + ichan < _programs.Count Then
					program__1 = DirectCast(_programs(offset + ichan), Program)
				Else
					' bugfix for 0 items
					If _programs.Count = 0 Then
						program__1 = New Program(channel.IdChannel, _viewingTime, _viewingTime, "-", String.Empty, String.Empty, _
							Program.ProgramState.None, DateTime.MinValue, String.Empty, String.Empty, String.Empty, String.Empty, _
							-1, String.Empty, -1)
					Else
						program__1 = DirectCast(_programs(_programs.Count - 1), Program)
						If program__1.EndTime.DayOfYear = _viewingTime.DayOfYear Then
							program__1 = New Program(channel.IdChannel, program__1.EndTime, program__1.EndTime, "-", "-", "-", _
								Program.ProgramState.None, DateTime.MinValue, String.Empty, String.Empty, String.Empty, String.Empty, _
								-1, String.Empty, -1)
						Else
							program__1 = New Program(channel.IdChannel, _viewingTime, _viewingTime, "-", "-", "-", _
								Program.ProgramState.None, DateTime.MinValue, String.Empty, String.Empty, String.Empty, String.Empty, _
								-1, String.Empty, -1)
						End If
					End If
				End If

				Dim ypos As Integer = GetControl(ichan + CInt(Controls.IMG_CHAN1)).YPosition
				Dim iControlId As Integer = GUIDE_COMPONENTID_START + ichan * RowID + 0 * ColID
				Dim img As GUIButton3PartControl = TryCast(GetControl(iControlId), GUIButton3PartControl)
				Dim buttonTemplate As GUIButton3PartControl = TryCast(GetControl(CInt(Controls.BUTTON_PROGRAM_NOT_RUNNING)), GUIButton3PartControl)

				If img Is Nothing Then
					If buttonTemplate IsNot Nothing Then
						buttonTemplate.IsVisible = False
						img = New GUIButton3PartControl(GetID, iControlId, iStartXPos, ypos, iTotalWidth, height - 2, _
							buttonTemplate.TexutureFocusLeftName, buttonTemplate.TexutureFocusMidName, buttonTemplate.TexutureFocusRightName, buttonTemplate.TexutureNoFocusLeftName, buttonTemplate.TexutureNoFocusMidName, buttonTemplate.TexutureNoFocusRightName, _
							[String].Empty)

						img.TileFillTFL = buttonTemplate.TileFillTFL
						img.TileFillTNFL = buttonTemplate.TileFillTNFL
						img.TileFillTFM = buttonTemplate.TileFillTFM
						img.TileFillTNFM = buttonTemplate.TileFillTNFM
						img.TileFillTFR = buttonTemplate.TileFillTFR
						img.TileFillTNFR = buttonTemplate.TileFillTNFR
					Else
						img = New GUIButton3PartControl(GetID, iControlId, iStartXPos, ypos, iTotalWidth, height - 2, _
							"tvguide_button_selected_left.png", "tvguide_button_selected_middle.png", "tvguide_button_selected_right.png", "tvguide_button_light_left.png", "tvguide_button_light_middle.png", "tvguide_button_light_right.png", _
							[String].Empty)
					End If
					img.AllocResources()
					img.ColourDiffuse = GetColorForGenre(program__1.Genre)
					Dim cntl As GUIControl = DirectCast(img, GUIControl)
					Add(cntl)
				Else
					If buttonTemplate IsNot Nothing Then
						buttonTemplate.IsVisible = False
						img.TexutureFocusLeftName = buttonTemplate.TexutureFocusLeftName
						img.TexutureFocusMidName = buttonTemplate.TexutureFocusMidName
						img.TexutureFocusRightName = buttonTemplate.TexutureFocusRightName
						img.TexutureNoFocusLeftName = buttonTemplate.TexutureNoFocusLeftName
						img.TexutureNoFocusMidName = buttonTemplate.TexutureNoFocusMidName
						img.TexutureNoFocusRightName = buttonTemplate.TexutureNoFocusRightName
						img.TileFillTFL = buttonTemplate.TileFillTFL
						img.TileFillTNFL = buttonTemplate.TileFillTNFL
						img.TileFillTFM = buttonTemplate.TileFillTFM
						img.TileFillTNFM = buttonTemplate.TileFillTNFM
						img.TileFillTFR = buttonTemplate.TileFillTFR
						img.TileFillTNFR = buttonTemplate.TileFillTNFR
					Else
						img.TexutureFocusLeftName = "tvguide_button_selected_left.png"
						img.TexutureFocusMidName = "tvguide_button_selected_middle.png"
						img.TexutureFocusRightName = "tvguide_button_selected_right.png"
						img.TexutureNoFocusLeftName = "tvguide_button_light_left.png"
						img.TexutureNoFocusMidName = "tvguide_button_light_middle.png"
						img.TexutureNoFocusRightName = "tvguide_button_light_right.png"
					End If
					img.Focus = False
					img.SetPosition(iStartXPos, ypos)
					img.Width = iTotalWidth
					img.ColourDiffuse = GetColorForGenre(program__1.Genre)
					img.IsVisible = True
					img.DoUpdate()
				End If
				img.RenderLeft = False
				img.RenderRight = False

				Dim bSeries As Boolean = (program__1.IsRecordingSeries OrElse program__1.IsRecordingSeriesPending OrElse program__1.IsPartialRecordingSeriesPending)
				Dim bConflict As Boolean = program__1.HasConflict
				Dim bRecording As Boolean = bSeries OrElse (program__1.IsRecording OrElse program__1.IsRecordingOncePending)

				img.Data = program__1
				img.ColourDiffuse = GetColorForGenre(program__1.Genre)
				height = height - 10
				height /= 2
				Dim iWidth As Integer = iTotalWidth
				If iWidth > 10 Then
					iWidth -= 10
				Else
					iWidth = 1
				End If

				Dim dt As DateTime = DateTime.Now

				img.TextOffsetX1 = 5
				img.TextOffsetY1 = 5
				img.FontName1 = "font13"
				img.TextColor1 = &HffffffffUI

				img.Label1 = TVUtil.GetDisplayTitle(program__1)

				Dim strTimeSingle As String = [String].Format("{0}", program__1.StartTime.ToString("t", CultureInfo.CurrentCulture.DateTimeFormat))

				If program__1.StartTime.DayOfYear <> _viewingTime.DayOfYear Then
					img.Label1 = [String].Format("{0} {1}", Utils.GetShortDayString(program__1.StartTime), TVUtil.GetDisplayTitle(program__1))
				End If

				Dim labelTemplate As GUILabelControl
				If program__1.IsRunningAt(dt) Then
					labelTemplate = _titleDarkTemplate
				Else
					labelTemplate = _titleTemplate
				End If

				If labelTemplate IsNot Nothing Then
					img.FontName1 = labelTemplate.FontName
					img.TextColor1 = labelTemplate.TextColor
					img.TextOffsetX1 = labelTemplate.XPosition
					img.TextOffsetY1 = labelTemplate.YPosition
					img.SetShadow1(labelTemplate.ShadowAngle, labelTemplate.ShadowDistance, labelTemplate.ShadowColor)
				End If
				img.TextOffsetX2 = 5
				img.TextOffsetY2 = img.Height / 2
				img.FontName2 = "font13"
				img.TextColor2 = &HffffffffUI
				img.Label2 = ""
				If program__1.IsRunningAt(dt) Then
					img.TextColor2 = &Hff101010UI
					labelTemplate = _genreDarkTemplate
				Else
					labelTemplate = _genreTemplate
				End If

				If labelTemplate IsNot Nothing Then
					img.FontName2 = labelTemplate.FontName
					img.TextColor2 = labelTemplate.TextColor
					img.Label2 = program__1.Genre
					img.TextOffsetX2 = labelTemplate.XPosition
					img.TextOffsetY2 = labelTemplate.YPosition
					img.SetShadow2(labelTemplate.ShadowAngle, labelTemplate.ShadowDistance, labelTemplate.ShadowColor)
				End If
				imgCh.Label1 = strTimeSingle
				imgCh.TexutureIcon = ""

				If program__1.IsRunningAt(dt) Then
					Dim buttonRunningTemplate As GUIButton3PartControl = _programRunningTemplate
					If buttonRunningTemplate IsNot Nothing Then
						buttonRunningTemplate.IsVisible = False
						img.TexutureFocusLeftName = buttonRunningTemplate.TexutureFocusLeftName
						img.TexutureFocusMidName = buttonRunningTemplate.TexutureFocusMidName
						img.TexutureFocusRightName = buttonRunningTemplate.TexutureFocusRightName
						img.TexutureNoFocusLeftName = buttonRunningTemplate.TexutureNoFocusLeftName
						img.TexutureNoFocusMidName = buttonRunningTemplate.TexutureNoFocusMidName
						img.TexutureNoFocusRightName = buttonRunningTemplate.TexutureNoFocusRightName
						img.TileFillTFL = buttonRunningTemplate.TileFillTFL
						img.TileFillTNFL = buttonRunningTemplate.TileFillTNFL
						img.TileFillTFM = buttonRunningTemplate.TileFillTFM
						img.TileFillTNFM = buttonRunningTemplate.TileFillTNFM
						img.TileFillTFR = buttonRunningTemplate.TileFillTFR
						img.TileFillTNFR = buttonRunningTemplate.TileFillTNFR
					Else
						img.TexutureFocusLeftName = "tvguide_button_selected_left.png"
						img.TexutureFocusMidName = "tvguide_button_selected_middle.png"
						img.TexutureFocusRightName = "tvguide_button_selected_right.png"
						img.TexutureNoFocusLeftName = "tvguide_button_left.png"
						img.TexutureNoFocusMidName = "tvguide_button_middle.png"
						img.TexutureNoFocusRightName = "tvguide_button_right.png"
					End If
				End If

				img.SetPosition(img.XPosition, img.YPosition)

				img.TexutureIcon = [String].Empty
				If program__1.Notify Then
					Dim buttonNotifyTemplate As GUIButton3PartControl = TryCast(GetControl(CInt(Controls.BUTTON_PROGRAM_NOTIFY)), GUIButton3PartControl)
					If buttonNotifyTemplate IsNot Nothing Then
						buttonNotifyTemplate.IsVisible = False
						img.TexutureFocusLeftName = buttonNotifyTemplate.TexutureFocusLeftName
						img.TexutureFocusMidName = buttonNotifyTemplate.TexutureFocusMidName
						img.TexutureFocusRightName = buttonNotifyTemplate.TexutureFocusRightName
						img.TexutureNoFocusLeftName = buttonNotifyTemplate.TexutureNoFocusLeftName
						img.TexutureNoFocusMidName = buttonNotifyTemplate.TexutureNoFocusMidName
						img.TexutureNoFocusRightName = buttonNotifyTemplate.TexutureNoFocusRightName
						img.TileFillTFL = buttonNotifyTemplate.TileFillTFL
						img.TileFillTNFL = buttonNotifyTemplate.TileFillTNFL
						img.TileFillTFM = buttonNotifyTemplate.TileFillTFM
						img.TileFillTNFM = buttonNotifyTemplate.TileFillTNFM
						img.TileFillTFR = buttonNotifyTemplate.TileFillTFR
						img.TileFillTNFR = buttonNotifyTemplate.TileFillTNFR

						' Use of the button template control implies use of the icon.  Use a blank image if the icon is not desired.
						img.TexutureIcon = Thumbs.TvNotifyIcon
						img.IconOffsetX = buttonNotifyTemplate.IconOffsetX
						img.IconOffsetY = buttonNotifyTemplate.IconOffsetY
						img.IconAlign = buttonNotifyTemplate.IconAlign
						img.IconVAlign = buttonNotifyTemplate.IconVAlign
						img.IconInlineLabel1 = buttonNotifyTemplate.IconInlineLabel1
					Else
						If _useNewNotifyButtonColor Then
							img.TexutureFocusLeftName = "tvguide_notifyButton_Focus_left.png"
							img.TexutureFocusMidName = "tvguide_notifyButton_Focus_middle.png"
							img.TexutureFocusRightName = "tvguide_notifyButton_Focus_right.png"
							img.TexutureNoFocusLeftName = "tvguide_notifyButton_noFocus_left.png"
							img.TexutureNoFocusMidName = "tvguide_notifyButton_noFocus_middle.png"
							img.TexutureNoFocusRightName = "tvguide_notifyButton_noFocus_right.png"
						Else
							img.TexutureIcon = Thumbs.TvNotifyIcon
						End If
					End If
				End If
				If bRecording Then
					Dim bPartialRecording As Boolean = program__1.IsPartialRecordingSeriesPending
					Dim buttonRecordTemplate As GUIButton3PartControl = TryCast(GetControl(CInt(Controls.BUTTON_PROGRAM_RECORD)), GUIButton3PartControl)

					' Select the partial recording template if needed.
					If bPartialRecording Then
						buttonRecordTemplate = TryCast(GetControl(CInt(Controls.BUTTON_PROGRAM_PARTIAL_RECORD)), GUIButton3PartControl)
					End If

					If buttonRecordTemplate IsNot Nothing Then
						buttonRecordTemplate.IsVisible = False
						img.TexutureFocusLeftName = buttonRecordTemplate.TexutureFocusLeftName
						img.TexutureFocusMidName = buttonRecordTemplate.TexutureFocusMidName
						img.TexutureFocusRightName = buttonRecordTemplate.TexutureFocusRightName
						img.TexutureNoFocusLeftName = buttonRecordTemplate.TexutureNoFocusLeftName
						img.TexutureNoFocusMidName = buttonRecordTemplate.TexutureNoFocusMidName
						img.TexutureNoFocusRightName = buttonRecordTemplate.TexutureNoFocusRightName
						img.TileFillTFL = buttonRecordTemplate.TileFillTFL
						img.TileFillTNFL = buttonRecordTemplate.TileFillTNFL
						img.TileFillTFM = buttonRecordTemplate.TileFillTFM
						img.TileFillTNFM = buttonRecordTemplate.TileFillTNFM
						img.TileFillTFR = buttonRecordTemplate.TileFillTFR
						img.TileFillTNFR = buttonRecordTemplate.TileFillTNFR

						' Use of the button template control implies use of the icon.  Use a blank image if the icon is not desired.
						If bConflict Then
							img.TexutureFocusLeftName = "tvguide_recButton_Focus_left.png"
							img.TexutureFocusMidName = "tvguide_recButton_Focus_middle.png"
							img.TexutureFocusRightName = "tvguide_recButton_Focus_right.png"
							img.TexutureNoFocusLeftName = "tvguide_recButton_noFocus_left.png"
							img.TexutureNoFocusMidName = "tvguide_recButton_noFocus_middle.png"
							img.TexutureNoFocusRightName = "tvguide_recButton_noFocus_right.png"
						Else
							If bConflict Then
								img.TexutureIcon = Thumbs.TvConflictRecordingIcon
							ElseIf bSeries Then
								img.TexutureIcon = Thumbs.TvRecordingSeriesIcon
							Else
								img.TexutureIcon = Thumbs.TvRecordingIcon
							End If
						End If
						img.IconOffsetX = buttonRecordTemplate.IconOffsetX
						img.IconOffsetY = buttonRecordTemplate.IconOffsetY
						img.IconAlign = buttonRecordTemplate.IconAlign
						img.IconVAlign = buttonRecordTemplate.IconVAlign
						img.IconInlineLabel1 = buttonRecordTemplate.IconInlineLabel1
					Else
						If bPartialRecording AndAlso _useNewPartialRecordingButtonColor Then
							img.TexutureFocusLeftName = "tvguide_partRecButton_Focus_left.png"
							img.TexutureFocusMidName = "tvguide_partRecButton_Focus_middle.png"
							img.TexutureFocusRightName = "tvguide_partRecButton_Focus_right.png"
							img.TexutureNoFocusLeftName = "tvguide_partRecButton_noFocus_left.png"
							img.TexutureNoFocusMidName = "tvguide_partRecButton_noFocus_middle.png"
							img.TexutureNoFocusRightName = "tvguide_partRecButton_noFocus_right.png"
						Else
							If _useNewRecordingButtonColor Then
								img.TexutureFocusLeftName = "tvguide_recButton_Focus_left.png"
								img.TexutureFocusMidName = "tvguide_recButton_Focus_middle.png"
								img.TexutureFocusRightName = "tvguide_recButton_Focus_right.png"
								img.TexutureNoFocusLeftName = "tvguide_recButton_noFocus_left.png"
								img.TexutureNoFocusMidName = "tvguide_recButton_noFocus_middle.png"
								img.TexutureNoFocusRightName = "tvguide_recButton_noFocus_right.png"
							Else
								If bConflict Then
									img.TexutureIcon = Thumbs.TvConflictRecordingIcon
								ElseIf bSeries Then
									img.TexutureIcon = Thumbs.TvRecordingSeriesIcon
								Else
									img.TexutureIcon = Thumbs.TvRecordingIcon
								End If
							End If
						End If
					End If
				End If
			Next
		End Sub

		Private Function IsRecordingNoEPG(channel As Channel) As Boolean
			Dim vc As VirtualCard = Nothing
			_server.IsRecording(channel.IdChannel, vc)

			If vc IsNot Nothing Then
				Return vc.IsRecording
			End If
			Return False
		End Function

		Protected Overrides Sub RenderChannel(ByRef mapPrograms As Dictionary(Of Integer, List(Of Program)), iChannel As Integer, tvGuideChannel As GuideChannel, iStart As Long, iEnd As Long, selectCurrentShow As Boolean)
			Dim channelNum As Integer = 0
			Dim channel As Channel = tvGuideChannel.channel

			If Not _byIndex Then
				For Each detail As TuningDetail In channel.ReferringTuningDetail()
					channelNum = detail.ChannelNumber
				Next
			Else
				channelNum = _channelList.IndexOf(tvGuideChannel) + 1
			End If

			Dim img As GUIButton3PartControl = TryCast(GetControl(iChannel + CInt(Controls.IMG_CHAN1)), GUIButton3PartControl)
			If img IsNot Nothing Then
				If _showChannelLogos Then
					img.TexutureIcon = tvGuideChannel.strLogo
				End If
				If channelNum > 0 AndAlso _showChannelNumber Then
					img.Label1 = (channelNum & " ") + channel.DisplayName
				Else
					img.Label1 = channel.DisplayName
				End If
				img.Data = channel
				img.IsVisible = True
			End If


			Dim programs As List(Of Program) = Nothing
			If mapPrograms.ContainsKey(channel.IdChannel) Then
				programs = mapPrograms(channel.IdChannel)
			End If

			Dim noEPG As Boolean = (programs Is Nothing OrElse programs.Count = 0)
			If noEPG Then
				Dim dt As DateTime = Utils.longtodate(iEnd)
				Dim iProgEnd As Long = Utils.datetolong(dt)
				Dim prog As New Program(channel.IdChannel, Utils.longtodate(iStart), Utils.longtodate(iProgEnd), GUILocalizeStrings.[Get](736), "", "", _
					Program.ProgramState.None, DateTime.MinValue, String.Empty, String.Empty, String.Empty, String.Empty, _
					-1, String.Empty, -1)
				If programs Is Nothing Then
					programs = New List(Of Program)()
				End If
				programs.Add(prog)
			End If

			Dim iProgram As Integer = 0
			Dim iPreviousEndXPos As Integer = 0

			Dim width As Integer = GetControl(CInt(Controls.LABEL_TIME1) + 1).XPosition
			width -= GetControl(CInt(Controls.LABEL_TIME1)).XPosition

			Dim height As Integer = GetControl(CInt(Controls.IMG_CHAN1) + 1).YPosition
			height -= GetControl(CInt(Controls.IMG_CHAN1)).YPosition

			For Each program__1 As Program In programs
				If Utils.datetolong(program__1.EndTime) <= iStart Then
					Continue For
				End If

				Dim strTitle As String = TVUtil.GetDisplayTitle(program__1)
				Dim bStartsBefore As Boolean = False
				Dim bEndsAfter As Boolean = False

				If Utils.datetolong(program__1.StartTime) < iStart Then
					bStartsBefore = True
				End If

				If Utils.datetolong(program__1.EndTime) > iEnd Then
					bEndsAfter = True
				End If

				Dim dtBlokStart As New DateTime()
				dtBlokStart = _viewingTime
				dtBlokStart = dtBlokStart.AddMilliseconds(-dtBlokStart.Millisecond)
				dtBlokStart = dtBlokStart.AddSeconds(-dtBlokStart.Second)

				Dim bSeries As Boolean = False
				Dim bRecording As Boolean = False
				Dim bConflict As Boolean = False
				Dim bPartialRecording As Boolean = False

				bConflict = program__1.HasConflict
				bSeries = (program__1.IsRecordingSeries OrElse program__1.IsRecordingSeriesPending)
				bRecording = bSeries OrElse (program__1.IsRecording OrElse program__1.IsRecordingOncePending OrElse program__1.IsPartialRecordingSeriesPending)
				bPartialRecording = program__1.IsPartialRecordingSeriesPending
				Dim bManual As Boolean = program__1.IsRecordingManual

				If noEPG AndAlso Not bRecording Then
					bRecording = IsRecordingNoEPG(channel)
				End If

				Dim bProgramIsHD As Boolean = program__1.Description.Contains(_hdtvProgramText)

				Dim iStartXPos As Integer = 0
				Dim iEndXPos As Integer = 0
				For iBlok As Integer = 0 To _numberOfBlocks - 1
					Dim fWidthEnd As Single = CSng(width)
					Dim dtBlokEnd As DateTime = dtBlokStart.AddMinutes(_timePerBlock - 1)
					If program__1.RunningAt(dtBlokStart, dtBlokEnd) Then
						'dtBlokEnd = dtBlokStart.AddSeconds(_timePerBlock * 60);
						If program__1.EndTime <= dtBlokEnd Then
							Dim dtSpan As TimeSpan = dtBlokEnd - program__1.EndTime
							Dim iEndMin As Integer = _timePerBlock - (dtSpan.Minutes)

							fWidthEnd = (CSng(iEndMin) / CSng(_timePerBlock)) * CSng(width)
							If bEndsAfter Then
								fWidthEnd = CSng(width)
							End If
						End If

						If iStartXPos = 0 Then
							Dim ts As TimeSpan = program__1.StartTime - dtBlokStart
							Dim iStartMin As Integer = ts.Hours * 60
							iStartMin += ts.Minutes
							If ts.Seconds = 59 Then
								iStartMin += 1
							End If
							Dim fWidth As Single = (CSng(iStartMin) / CSng(_timePerBlock)) * CSng(width)

							If bStartsBefore Then
								fWidth = 0
							End If

							iStartXPos = GetControl(iBlok + CInt(Controls.LABEL_TIME1)).XPosition
							iStartXPos += CInt(Math.Truncate(fWidth))
							iEndXPos = GetControl(iBlok + CInt(Controls.LABEL_TIME1)).XPosition + CInt(Math.Truncate(fWidthEnd))
						Else
							iEndXPos = GetControl(iBlok + CInt(Controls.LABEL_TIME1)).XPosition + CInt(Math.Truncate(fWidthEnd))
						End If
					End If
					dtBlokStart = dtBlokStart.AddMinutes(_timePerBlock)
				Next

				If iStartXPos >= 0 Then
					If iPreviousEndXPos > iStartXPos Then
						iStartXPos = iPreviousEndXPos
					End If
					If iEndXPos <= iStartXPos + 5 Then
							' at least 1 pixel width
						iEndXPos = iStartXPos + 6
					End If

					Dim ypos As Integer = GetControl(iChannel + CInt(Controls.IMG_CHAN1)).YPosition
					Dim iControlId As Integer = GUIDE_COMPONENTID_START + iChannel * RowID + iProgram * ColID
					Dim iWidth As Integer = iEndXPos - iStartXPos
					If iWidth > 3 Then
						iWidth -= 3
					Else
						iWidth = 1
					End If

					Dim TexutureFocusLeftName As String = "tvguide_button_selected_left.png"
					Dim TexutureFocusMidName As String = "tvguide_button_selected_middle.png"
					Dim TexutureFocusRightName As String = "tvguide_button_selected_right.png"
					Dim TexutureNoFocusLeftName As String = "tvguide_button_light_left.png"
					Dim TexutureNoFocusMidName As String = "tvguide_button_light_middle.png"
					Dim TexutureNoFocusRightName As String = "tvguide_button_light_right.png"

					Dim TileFillTFL As Boolean = False
					Dim TileFillTNFL As Boolean = False
					Dim TileFillTFM As Boolean = False
					Dim TileFillTNFM As Boolean = False
					Dim TileFillTFR As Boolean = False
					Dim TileFillTNFR As Boolean = False

					If _programNotRunningTemplate IsNot Nothing Then
						_programNotRunningTemplate.IsVisible = False
						TexutureFocusLeftName = _programNotRunningTemplate.TexutureFocusLeftName
						TexutureFocusMidName = _programNotRunningTemplate.TexutureFocusMidName
						TexutureFocusRightName = _programNotRunningTemplate.TexutureFocusRightName
						TexutureNoFocusLeftName = _programNotRunningTemplate.TexutureNoFocusLeftName
						TexutureNoFocusMidName = _programNotRunningTemplate.TexutureNoFocusMidName
						TexutureNoFocusRightName = _programNotRunningTemplate.TexutureNoFocusRightName
						TileFillTFL = _programNotRunningTemplate.TileFillTFL
						TileFillTNFL = _programNotRunningTemplate.TileFillTNFL
						TileFillTFM = _programNotRunningTemplate.TileFillTFM
						TileFillTNFM = _programNotRunningTemplate.TileFillTNFM
						TileFillTFR = _programNotRunningTemplate.TileFillTFR
						TileFillTNFR = _programNotRunningTemplate.TileFillTNFR
					End If

					Dim isNew As Boolean = False

					If Not _controls.TryGetValue(CInt(iControlId), img) Then
						img = New GUIButton3PartControl(GetID, iControlId, iStartXPos, ypos, iWidth, height - 2, _
							TexutureFocusLeftName, TexutureFocusMidName, TexutureFocusRightName, TexutureNoFocusLeftName, TexutureNoFocusMidName, TexutureNoFocusRightName, _
							[String].Empty)

						isNew = True
					Else
						img.Focus = False
						img.SetPosition(iStartXPos, ypos)
						img.Width = iWidth
						img.IsVisible = True
						img.DoUpdate()
					End If

					img.RenderLeft = False
					img.RenderRight = False

					img.TexutureIcon = [String].Empty
					If program__1.Notify Then
						If _programNotifyTemplate IsNot Nothing Then
							_programNotifyTemplate.IsVisible = False
							TexutureFocusLeftName = _programNotifyTemplate.TexutureFocusLeftName
							TexutureFocusMidName = _programNotifyTemplate.TexutureFocusMidName
							TexutureFocusRightName = _programNotifyTemplate.TexutureFocusRightName
							TexutureNoFocusLeftName = _programNotifyTemplate.TexutureNoFocusLeftName
							TexutureNoFocusMidName = _programNotifyTemplate.TexutureNoFocusMidName
							TexutureNoFocusRightName = _programNotifyTemplate.TexutureNoFocusRightName
							TileFillTFL = _programNotifyTemplate.TileFillTFL
							TileFillTNFL = _programNotifyTemplate.TileFillTNFL
							TileFillTFM = _programNotifyTemplate.TileFillTFM
							TileFillTNFM = _programNotifyTemplate.TileFillTNFM
							TileFillTFR = _programNotifyTemplate.TileFillTFR
							TileFillTNFR = _programNotifyTemplate.TileFillTNFR

							' Use of the button template control implies use of the icon.  Use a blank image if the icon is not desired.
							img.TexutureIcon = Thumbs.TvNotifyIcon
							img.IconOffsetX = _programNotifyTemplate.IconOffsetX
							img.IconOffsetY = _programNotifyTemplate.IconOffsetY
							img.IconAlign = _programNotifyTemplate.IconAlign
							img.IconVAlign = _programNotifyTemplate.IconVAlign
							img.IconInlineLabel1 = _programNotifyTemplate.IconInlineLabel1
						Else
							If _useNewNotifyButtonColor Then
								TexutureFocusLeftName = "tvguide_notifyButton_Focus_left.png"
								TexutureFocusMidName = "tvguide_notifyButton_Focus_middle.png"
								TexutureFocusRightName = "tvguide_notifyButton_Focus_right.png"
								TexutureNoFocusLeftName = "tvguide_notifyButton_noFocus_left.png"
								TexutureNoFocusMidName = "tvguide_notifyButton_noFocus_middle.png"
								TexutureNoFocusRightName = "tvguide_notifyButton_noFocus_right.png"
							Else
								img.TexutureIcon = Thumbs.TvNotifyIcon
							End If
						End If
					End If
					If bRecording Then
						Dim buttonRecordTemplate As GUIButton3PartControl = _programRecordTemplate

						' Select the partial recording template if needed.
						If bPartialRecording Then
							buttonRecordTemplate = _programPartialRecordTemplate
						End If

						If buttonRecordTemplate IsNot Nothing Then
							buttonRecordTemplate.IsVisible = False
							TexutureFocusLeftName = buttonRecordTemplate.TexutureFocusLeftName
							TexutureFocusMidName = buttonRecordTemplate.TexutureFocusMidName
							TexutureFocusRightName = buttonRecordTemplate.TexutureFocusRightName
							TexutureNoFocusLeftName = buttonRecordTemplate.TexutureNoFocusLeftName
							TexutureNoFocusMidName = buttonRecordTemplate.TexutureNoFocusMidName
							TexutureNoFocusRightName = buttonRecordTemplate.TexutureNoFocusRightName
							TileFillTFL = buttonRecordTemplate.TileFillTFL
							TileFillTNFL = buttonRecordTemplate.TileFillTNFL
							TileFillTFM = buttonRecordTemplate.TileFillTFM
							TileFillTNFM = buttonRecordTemplate.TileFillTNFM
							TileFillTFR = buttonRecordTemplate.TileFillTFR
							TileFillTNFR = buttonRecordTemplate.TileFillTNFR

							' Use of the button template control implies use of the icon.  Use a blank image if the icon is not desired.
							If bConflict Then
								TexutureFocusLeftName = "tvguide_recButton_Focus_left.png"
								TexutureFocusMidName = "tvguide_recButton_Focus_middle.png"
								TexutureFocusRightName = "tvguide_recButton_Focus_right.png"
								TexutureNoFocusLeftName = "tvguide_recButton_noFocus_left.png"
								TexutureNoFocusMidName = "tvguide_recButton_noFocus_middle.png"
								TexutureNoFocusRightName = "tvguide_recButton_noFocus_right.png"
							Else
								If bConflict Then
									img.TexutureIcon = Thumbs.TvConflictRecordingIcon
								ElseIf bSeries Then
									img.TexutureIcon = Thumbs.TvRecordingSeriesIcon
								Else
									img.TexutureIcon = Thumbs.TvRecordingIcon
								End If
							End If
							img.IconOffsetX = buttonRecordTemplate.IconOffsetX
							img.IconOffsetY = buttonRecordTemplate.IconOffsetY
							img.IconAlign = buttonRecordTemplate.IconAlign
							img.IconVAlign = buttonRecordTemplate.IconVAlign
							img.IconInlineLabel1 = buttonRecordTemplate.IconInlineLabel1
						Else
							If bPartialRecording AndAlso _useNewPartialRecordingButtonColor Then
								TexutureFocusLeftName = "tvguide_partRecButton_Focus_left.png"
								TexutureFocusMidName = "tvguide_partRecButton_Focus_middle.png"
								TexutureFocusRightName = "tvguide_partRecButton_Focus_right.png"
								TexutureNoFocusLeftName = "tvguide_partRecButton_noFocus_left.png"
								TexutureNoFocusMidName = "tvguide_partRecButton_noFocus_middle.png"
								TexutureNoFocusRightName = "tvguide_partRecButton_noFocus_right.png"
							Else
								If _useNewRecordingButtonColor Then
									TexutureFocusLeftName = "tvguide_recButton_Focus_left.png"
									TexutureFocusMidName = "tvguide_recButton_Focus_middle.png"
									TexutureFocusRightName = "tvguide_recButton_Focus_right.png"
									TexutureNoFocusLeftName = "tvguide_recButton_noFocus_left.png"
									TexutureNoFocusMidName = "tvguide_recButton_noFocus_middle.png"
									TexutureNoFocusRightName = "tvguide_recButton_noFocus_right.png"
								Else
									If bConflict Then
										img.TexutureIcon = Thumbs.TvConflictRecordingIcon
									ElseIf bSeries Then
										img.TexutureIcon = Thumbs.TvRecordingSeriesIcon
									Else
										img.TexutureIcon = Thumbs.TvRecordingIcon
									End If
								End If
							End If
						End If
					End If

					img.TexutureIcon2 = [String].Empty
					If bProgramIsHD Then
						If _programNotRunningTemplate IsNot Nothing Then
							img.TexutureIcon2 = _programNotRunningTemplate.TexutureIcon2
						Else
							If _useHdProgramIcon Then
								img.TexutureIcon2 = "tvguide_hd_program.png"
							End If
						End If
						img.Icon2InlineLabel1 = True
						img.Icon2VAlign = GUIControl.VAlignment.ALIGN_MIDDLE
						img.Icon2OffsetX = 5
					End If
					img.Data = program__1.Clone()
					img.ColourDiffuse = GetColorForGenre(program__1.Genre)

					iWidth = iEndXPos - iStartXPos
					If iWidth > 10 Then
						iWidth -= 10
					Else
						iWidth = 1
					End If

					Dim dt As DateTime = DateTime.Now

					img.TextOffsetX1 = 5
					img.TextOffsetY1 = 5
					img.FontName1 = "font13"
					img.TextColor1 = &HffffffffUI
					img.Label1 = strTitle
					Dim labelTemplate As GUILabelControl
					If program__1.IsRunningAt(dt) Then
						labelTemplate = _titleDarkTemplate
					Else
						labelTemplate = _titleTemplate
					End If

					If labelTemplate IsNot Nothing Then
						img.FontName1 = labelTemplate.FontName
						img.TextColor1 = labelTemplate.TextColor
						img.TextColor2 = labelTemplate.TextColor
						img.TextOffsetX1 = labelTemplate.XPosition
						img.TextOffsetY1 = labelTemplate.YPosition
						img.SetShadow1(labelTemplate.ShadowAngle, labelTemplate.ShadowDistance, labelTemplate.ShadowColor)

						' This is a legacy behavior check.  Adding labelTemplate.XPosition and labelTemplate.YPosition requires
						' skinners to add these values to the skin xml unless this check exists.  Perform a sanity check on the
						' x,y position to ensure it falls into the bounds of the button.  If it does not then fall back to use the
						' legacy values.  This check is necessary because the x,y position (without skin file changes) will be taken
						' from either the references.xml control template or the controls coded defaults.
						If img.TextOffsetY1 > img.Height Then
							' Set legacy values.
							img.TextOffsetX1 = 5
							img.TextOffsetY1 = 5
						End If
					End If
					img.TextOffsetX2 = 5
					img.TextOffsetY2 = img.Height / 2
					img.FontName2 = "font13"
					img.TextColor2 = &HffffffffUI

					If program__1.IsRunningAt(dt) Then
						labelTemplate = _genreDarkTemplate
					Else
						labelTemplate = _genreTemplate
					End If
					If labelTemplate IsNot Nothing Then
						img.FontName2 = labelTemplate.FontName
						img.TextColor2 = labelTemplate.TextColor
						img.Label2 = program__1.Genre
						img.TextOffsetX2 = labelTemplate.XPosition
						img.TextOffsetY2 = labelTemplate.YPosition
						img.SetShadow2(labelTemplate.ShadowAngle, labelTemplate.ShadowDistance, labelTemplate.ShadowColor)

						' This is a legacy behavior check.  Adding labelTemplate.XPosition and labelTemplate.YPosition requires
						' skinners to add these values to the skin xml unless this check exists.  Perform a sanity check on the
						' x,y position to ensure it falls into the bounds of the button.  If it does not then fall back to use the
						' legacy values.  This check is necessary because the x,y position (without skin file changes) will be taken
						' from either the references.xml control template or the controls coded defaults.
						If img.TextOffsetY2 > img.Height Then
							' Set legacy values.
							img.TextOffsetX2 = 5
							img.TextOffsetY2 = 5
						End If
					End If

					If program__1.IsRunningAt(dt) Then
						Dim buttonRunningTemplate As GUIButton3PartControl = _programRunningTemplate
						If Not bRecording AndAlso Not bPartialRecording AndAlso buttonRunningTemplate IsNot Nothing Then
							buttonRunningTemplate.IsVisible = False
							TexutureFocusLeftName = buttonRunningTemplate.TexutureFocusLeftName
							TexutureFocusMidName = buttonRunningTemplate.TexutureFocusMidName
							TexutureFocusRightName = buttonRunningTemplate.TexutureFocusRightName
							TexutureNoFocusLeftName = buttonRunningTemplate.TexutureNoFocusLeftName
							TexutureNoFocusMidName = buttonRunningTemplate.TexutureNoFocusMidName
							TexutureNoFocusRightName = buttonRunningTemplate.TexutureNoFocusRightName
							TileFillTFL = buttonRunningTemplate.TileFillTFL
							TileFillTNFL = buttonRunningTemplate.TileFillTNFL
							TileFillTFM = buttonRunningTemplate.TileFillTFM
							TileFillTNFM = buttonRunningTemplate.TileFillTNFM
							TileFillTFR = buttonRunningTemplate.TileFillTFR
							TileFillTNFR = buttonRunningTemplate.TileFillTNFR
						ElseIf bRecording AndAlso _useNewRecordingButtonColor Then
							TexutureFocusLeftName = "tvguide_recButton_Focus_left.png"
							TexutureFocusMidName = "tvguide_recButton_Focus_middle.png"
							TexutureFocusRightName = "tvguide_recButton_Focus_right.png"
							TexutureNoFocusLeftName = "tvguide_recButton_noFocus_left.png"
							TexutureNoFocusMidName = "tvguide_recButton_noFocus_middle.png"
							TexutureNoFocusRightName = "tvguide_recButton_noFocus_right.png"
						ElseIf Not (bRecording AndAlso bPartialRecording AndAlso _useNewRecordingButtonColor) Then
							TexutureFocusLeftName = "tvguide_button_selected_left.png"
							TexutureFocusMidName = "tvguide_button_selected_middle.png"
							TexutureFocusRightName = "tvguide_button_selected_right.png"
							TexutureNoFocusLeftName = "tvguide_button_left.png"
							TexutureNoFocusMidName = "tvguide_button_middle.png"
							TexutureNoFocusRightName = "tvguide_button_right.png"
						End If
						If selectCurrentShow AndAlso iChannel = _cursorX Then
							_cursorY = iProgram + 1
							_currentProgram = program__1
							m_dtStartTime = program__1.StartTime
							SetProperties()
						End If
					End If

					If bEndsAfter Then
						img.RenderRight = True

						TexutureFocusRightName = "tvguide_arrow_selected_right.png"
						TexutureNoFocusRightName = "tvguide_arrow_light_right.png"
						If program__1.IsRunningAt(dt) Then
							TexutureNoFocusRightName = "tvguide_arrow_right.png"
						End If
					End If
					If bStartsBefore Then
						img.RenderLeft = True
						TexutureFocusLeftName = "tvguide_arrow_selected_left.png"
						TexutureNoFocusLeftName = "tvguide_arrow_light_left.png"
						If program__1.IsRunningAt(dt) Then
							TexutureNoFocusLeftName = "tvguide_arrow_left.png"
						End If
					End If

					img.TexutureFocusLeftName = TexutureFocusLeftName
					img.TexutureFocusMidName = TexutureFocusMidName
					img.TexutureFocusRightName = TexutureFocusRightName
					img.TexutureNoFocusLeftName = TexutureNoFocusLeftName
					img.TexutureNoFocusMidName = TexutureNoFocusMidName
					img.TexutureNoFocusRightName = TexutureNoFocusRightName

					img.TileFillTFL = TileFillTFL
					img.TileFillTNFL = TileFillTNFL
					img.TileFillTFM = TileFillTFM
					img.TileFillTNFM = TileFillTNFM
					img.TileFillTFR = TileFillTFR
					img.TileFillTNFR = TileFillTNFR

					If isNew Then
						img.AllocResources()
						Dim cntl As GUIControl = DirectCast(img, GUIControl)
						_controls.Add(CInt(iControlId), img)
						Add(cntl)
					Else
						img.DoUpdate()
					End If
					iProgram += 1
				End If
				iPreviousEndXPos = iEndXPos
			Next
		End Sub

		'void RenderChannel(int iChannel,Channel channel, long iStart, long iEnd, bool selectCurrentShow)

		Private Function ProgramCount(iChannel As Integer) As Integer
			Dim iProgramCount As Integer = 0
			For iProgram As Integer = 0 To _numberOfBlocks * 5 - 1
				Dim iControlId As Integer = GUIDE_COMPONENTID_START + iChannel * RowID + iProgram * ColID
				Dim cntl As GUIControl = GetControl(iControlId)
				If cntl IsNot Nothing AndAlso cntl.IsVisible Then
					iProgramCount += 1
				Else
					Return iProgramCount
				End If
			Next
			Return iProgramCount
		End Function

		Private Sub OnDown(updateScreen As Boolean)
			If updateScreen Then
				UnFocus()
			End If
			If _cursorX < 0 Then
				_cursorY = 0
				_cursorX = 0
				If updateScreen Then
					SetFocus()
					GetControl(CInt(Controls.SPINCONTROL_TIME_INTERVAL)).Focus = False
				End If
				Return
			End If

			If _singleChannelView Then
				If _cursorX + 1 < _channelCount Then
					_cursorX += 1
				Else
					If _cursorX + _programOffset + 1 < _totalProgramCount Then
						_programOffset += 1
					End If
				End If
				If updateScreen Then
					Update(False)
					SetFocus()
					UpdateCurrentProgram()
					SetProperties()
				End If
				Return
			End If

			If _cursorY = 0 Then
				MoveDown()

				If updateScreen Then
					Update(False)
					SetFocus()
					SetProperties()
				End If
				Return
			End If

			' not on tvguide button
			If _cursorY > 0 Then
				' if cursor is on a program in guide, try to find the "best time matching" program in new channel
				SetBestMatchingProgram(updateScreen, True)
			End If
		End Sub

		Private Sub MoveDown()
			' Move the cursor only if there are more channels in the view.
			If _cursorX + 1 < Math.Min(_channelList.Count, _channelCount) Then
				_cursorX += 1
				_lastCommandTime = AnimationTimer.TickCount
			Else
				' reached end of screen
				' more channels than rows?
				If _channelList.Count > _channelCount Then
					' Guide may be allowed to loop continuously bottom to top.
					If _guideContinuousScroll Then
						' We're at the bottom of the last page of channels.
						If ChannelOffset >= _channelList.Count Then
							' Position to first channel in guide without moving the cursor (implements continuous loops of channels).
							ChannelOffset = 0
						Else
							' Advance to next channel, wrap around if at end of list.
							ChannelOffset += 1
							If ChannelOffset >= _channelList.Count Then
								ChannelOffset = 0
							End If
						End If
					Else
						' Are we at the bottom of the lst page of channels?
						If ChannelOffset > 0 AndAlso ChannelOffset >= (_channelList.Count - 1) - _cursorX Then
							' We're at the bottom of the last page of channels.
							' Reposition the guide to the top only after the key/button has been released and pressed again.
							If (AnimationTimer.TickCount - _lastCommandTime) > _loopDelay Then
								ChannelOffset = 0
								_cursorX = 0
								_lastCommandTime = AnimationTimer.TickCount
							End If
						Else
							' Advance to next channel.
							ChannelOffset += 1
							_lastCommandTime = AnimationTimer.TickCount
						End If
					End If
				ElseIf (AnimationTimer.TickCount - _lastCommandTime) > _loopDelay Then
					' Move the highlight back to the top of the list only after the key/button has been released and pressed again.
					_cursorX = 0
					_lastCommandTime = AnimationTimer.TickCount
				End If
			End If
		End Sub

		Private Sub OnUp(updateScreen As Boolean, isPaging As Boolean)
			If updateScreen Then
				UnFocus()
			End If

			If _singleChannelView Then
				If _cursorX = 0 AndAlso _cursorY = 0 AndAlso Not isPaging Then
					' Don't focus the control when it is not visible.
					If GetControl(CInt(Controls.SPINCONTROL_DAY)).IsVisible Then
						_cursorX = -1
						GetControl(CInt(Controls.SPINCONTROL_DAY)).Focus = True
					End If
					Return
				End If

				If _cursorX > 0 Then
					_cursorX -= 1
				ElseIf _programOffset > 0 Then
					_programOffset -= 1
				End If

				If updateScreen Then
					Update(False)
					SetFocus()
					UpdateCurrentProgram()
					SetProperties()
				End If
				Return
			Else
				If _cursorY = -1 Then
					_cursorX = -1
					_cursorY = 0
					GetControl(CInt(Controls.CHANNEL_GROUP_BUTTON)).Focus = False
					GetControl(CInt(Controls.SPINCONTROL_DAY)).Focus = True
					Return
				End If

				If _cursorY = 0 AndAlso _cursorX = 0 AndAlso Not isPaging Then
					' Only focus the control if it is visible.
					If GetControl(CInt(Controls.SPINCONTROL_TIME_INTERVAL)).Visible Then
						_cursorX = -1
						GetControl(CInt(Controls.SPINCONTROL_TIME_INTERVAL)).Focus = True
						Return
					End If
				End If
			End If

			If _cursorY = 0 Then
				If _cursorX = 0 Then
					If ChannelOffset > 0 Then
						' Somewhere in the middle of the guide; just scroll up.
						ChannelOffset -= 1
					ElseIf ChannelOffset = 0 Then
						' We're at the top of the first page of channels.
						' Reposition the guide to the bottom only after the key/button has been released and pressed again.
						If (AnimationTimer.TickCount - _lastCommandTime) > _loopDelay Then
							ChannelOffset = _channelList.Count - _channelCount
							_cursorX = _channelCount - 1
						End If
					End If
				ElseIf _cursorX > 0 Then
					_cursorX -= 1
				End If

				If updateScreen Then
					Update(False)
					SetFocus()
					SetProperties()
				End If
			End If

			' not on tvguide button
			If _cursorY > 0 Then
				' if cursor is on a program in guide, try to find the "best time matching" program in new channel
				SetBestMatchingProgram(updateScreen, False)
			End If
		End Sub

		Private Sub MoveUp()
			If _cursorX = 0 Then
				If _guideContinuousScroll Then
					If ChannelOffset = 0 AndAlso _channelList.Count > _channelCount Then
						' We're at the top of the first page of channels.  Position to last channel in guide.
						ChannelOffset = _channelList.Count - 1
					ElseIf ChannelOffset > 0 Then
						' Somewhere in the middle of the guide; just scroll up.
						ChannelOffset -= 1
					End If
				Else
					If ChannelOffset > 0 Then
						' Somewhere in the middle of the guide; just scroll up.
						ChannelOffset -= 1
						_lastCommandTime = AnimationTimer.TickCount
					' Are we at the top of the first page of channels?
					ElseIf ChannelOffset = 0 AndAlso _cursorX = 0 Then
						' We're at the top of the first page of channels.
						' Reposition the guide to the bottom only after the key/button has been released and pressed again.
						If (AnimationTimer.TickCount - _lastCommandTime) > _loopDelay Then
							ChannelOffset = _channelList.Count - _channelCount
							_cursorX = _channelCount - 1
							_lastCommandTime = AnimationTimer.TickCount
						End If
					End If
				End If
			Else
				_cursorX -= 1
				_lastCommandTime = AnimationTimer.TickCount
			End If
		End Sub

		''' <summary>
		''' Sets the best matching program in new guide row
		''' </summary>
		''' <param name="updateScreen"></param>
		Private Sub SetBestMatchingProgram(updateScreen As Boolean, DirectionIsDown As Boolean)
			' if cursor is on a program in guide, try to find the "best time matching" program in new channel
			Dim iCurY As Integer = _cursorX
			Dim iCurOff As Integer = ChannelOffset
			Dim iX1 As Integer, iX2 As Integer
			Dim iControlId As Integer = GUIDE_COMPONENTID_START + _cursorX * RowID + (_cursorY - 1) * ColID
			Dim control As GUIControl = GetControl(iControlId)
			If control Is Nothing Then
				Return
			End If
			iX1 = control.XPosition
			iX2 = control.XPosition + control.Width

			Dim bOK As Boolean = False
			Dim iMaxSearch As Integer = _channelList.Count

			' TODO rewrite the while loop, the code is a little awkward.
			While Not bOK AndAlso (iMaxSearch > 0)
				iMaxSearch -= 1
				If DirectionIsDown = True Then
					MoveDown()
				Else
					' Direction "Up"
					MoveUp()
				End If
				If updateScreen Then
					Update(False)
				End If

				For x As Integer = 1 To ColID - 1
					iControlId = GUIDE_COMPONENTID_START + _cursorX * RowID + (x - 1) * ColID
					control = GetControl(iControlId)
					If control IsNot Nothing Then
						Dim prog As Program = DirectCast(control.Data, Program)

						If _singleChannelView Then
							_cursorY = x
							bOK = True
							Exit For
						End If

						Dim isvalid As Boolean = False
						Dim time As DateTime = DateTime.Now
						If time < prog.EndTime Then
							' present & future
							If m_dtStartTime <= prog.StartTime Then
								isvalid = True
							ElseIf m_dtStartTime >= prog.StartTime AndAlso m_dtStartTime < prog.EndTime Then
								isvalid = True
							ElseIf m_dtStartTime < time Then
								isvalid = True
							End If
						' this one will skip past programs
						ElseIf time > _currentProgram.EndTime Then
							' history
							If prog.EndTime > m_dtStartTime Then
								isvalid = True
							End If
						End If

						If isvalid Then
							_cursorY = x
							bOK = True
							Exit For
						End If
					End If
				Next
			End While
			If Not bOK Then
				_cursorX = iCurY
				ChannelOffset = iCurOff
			End If
			If updateScreen Then
				Correct()
				If iCurOff = ChannelOffset Then
					UpdateCurrentProgram()
					Return
				End If
				SetFocus()
			End If
		End Sub

		Private Sub OnLeft()
			If _cursorX < 0 Then
				Return
			End If
			UnFocus()
			If _cursorY <= 0 Then
				' custom focus handling only if button available
				If MinYIndex = -1 Then
					_cursorY -= 1
					' decrease by 1,
					If _cursorY = -1 Then
						' means tvgroup entered (-1) or moved left (-2)
						SetFocus()
						Return
					End If
				End If
				_viewingTime = _viewingTime.AddMinutes(-_timePerBlock)
				' Check new day
				Dim iDay As Integer = CalcDays()
				If iDay < 0 Then
					_viewingTime = _viewingTime.AddMinutes(+_timePerBlock)
				End If
			Else
				If _cursorY = 1 Then
					_cursorY = 0

					SetFocus()
					SetProperties()
					Return
				End If
				_cursorY -= 1
				Correct()
				UpdateCurrentProgram()
				If _currentProgram IsNot Nothing Then
					m_dtStartTime = _currentProgram.StartTime
				End If
				Return
			End If
			Correct()
			Update(False)
			SetFocus()
			If _currentProgram IsNot Nothing Then
				m_dtStartTime = _currentProgram.StartTime
			End If
		End Sub

		Private Sub UpdateCurrentProgram()
			If _cursorX < 0 Then
				Return
			End If
			If _cursorY < 0 Then
				Return
			End If
			If _cursorY = 0 Then
				SetProperties()
				SetFocus()
				Return
			End If
			Dim iControlId As Integer = GUIDE_COMPONENTID_START + _cursorX * RowID + (_cursorY - 1) * ColID
			Dim img As GUIButton3PartControl = TryCast(GetControl(iControlId), GUIButton3PartControl)
			

			If img IsNot Nothing Then
				SetFocus()
				_currentProgram = DirectCast(img.Data, Program)
				SetProperties()
			End If
		End Sub

		''' <summary>
		''' Show or hide group button
		''' </summary>
		Protected Overrides Sub UpdateGroupButton()
			' text for button
			Dim GroupButtonText As [String] = " "

			' show/hide tvgroup button
			Dim btnTvGroup As GUIButtonControl = TryCast(GetControl(CInt(Controls.CHANNEL_GROUP_BUTTON)), GUIButtonControl)

			If btnTvGroup IsNot Nothing Then
				btnTvGroup.Visible = GroupButtonAvail
			End If

			' set min index for focus handling
			If GroupButtonAvail Then
				MinYIndex = -1
				' allow focus of button
				GroupButtonText = [String].Format("{0}: {1}", GUILocalizeStrings.[Get](971), TVHome.Navigator.CurrentGroup.GroupName)
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.Group", TVHome.Navigator.CurrentGroup.GroupName)
			Else
				GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.Group", TVHome.Navigator.CurrentGroup.GroupName)
				MinYIndex = 0
			End If

			' Set proper text for group change button; Empty string to hide text if only 1 group
			' (split between button and rotated label due to focusing issue of rotated buttons)
			GUIPropertyManager.SetProperty(SkinPropertyPrefix & ".Guide.ChangeGroup", GroupButtonText)
			' existing string "group"
		End Sub

		Private Sub OnRight()
			If _cursorX < 0 Then
				Return
			End If
			UnFocus()
			If _cursorY < ProgramCount(_cursorX) Then
				_cursorY += 1
				Correct()
				UpdateCurrentProgram()
				If _currentProgram IsNot Nothing Then
					m_dtStartTime = _currentProgram.StartTime
				End If
				Return
			Else
				_viewingTime = _viewingTime.AddMinutes(_timePerBlock)
				' Check new day
				Dim iDay As Integer = CalcDays()
				If iDay >= MaxDaysInGuide Then
					_viewingTime = _viewingTime.AddMinutes(-_timePerBlock)
				End If
			End If
			Correct()
			Update(False)
			SetFocus()
			If _currentProgram IsNot Nothing Then
				m_dtStartTime = _currentProgram.StartTime
			End If
		End Sub

		Private Sub updateSingleChannelNumber()
			' update selected channel
			If Not _singleChannelView Then
				_singleChannelNumber = _cursorX + ChannelOffset
				If _singleChannelNumber < 0 Then
					_singleChannelNumber = 0
				End If
				If _singleChannelNumber >= _channelList.Count Then
					_singleChannelNumber -= _channelList.Count
				End If
				' instead of direct casting us "as"; else it fails for other controls!
				Dim img As GUIButton3PartControl = TryCast(GetControl(_cursorX + CInt(Controls.IMG_CHAN1)), GUIButton3PartControl)
				

				If img IsNot Nothing Then
					_currentChannel = DirectCast(img.Data, Channel)
				End If
			End If
		End Sub

		Private Sub UnFocus()
			If _cursorX < 0 Then
				Return
			End If
			If _cursorY = 0 OrElse _cursorY = MinYIndex Then
				' either channel or group button
				Dim controlid As Integer = CInt(Controls.IMG_CHAN1) + _cursorX
				GUIControl.UnfocusControl(GetID, controlid)
			Else
				Correct()
				Dim iControlId As Integer = GUIDE_COMPONENTID_START + _cursorX * RowID + (_cursorY - 1) * ColID
				Dim img As GUIButton3PartControl = TryCast(GetControl(iControlId), GUIButton3PartControl)
				If img IsNot Nothing AndAlso img.IsVisible Then
					If _currentProgram IsNot Nothing Then
						img.ColourDiffuse = GetColorForGenre(_currentProgram.Genre)
					End If
				End If
				GUIControl.UnfocusControl(GetID, iControlId)
			End If
		End Sub

		Private Sub SetFocus()
			If _cursorX < 0 Then
				Return
			End If
			If _cursorY = 0 OrElse _cursorY = MinYIndex Then
				' either channel or group button
				Dim controlid As Integer
				GUIControl.UnfocusControl(GetID, CInt(Controls.SPINCONTROL_DAY))
				GUIControl.UnfocusControl(GetID, CInt(Controls.SPINCONTROL_TIME_INTERVAL))

				If _cursorY = -1 Then
					controlid = CInt(Controls.CHANNEL_GROUP_BUTTON)
				Else
					controlid = CInt(Controls.IMG_CHAN1) + _cursorX
				End If

				GUIControl.FocusControl(GetID, controlid)
			Else
				Correct()
				Dim iControlId As Integer = GUIDE_COMPONENTID_START + _cursorX * RowID + (_cursorY - 1) * ColID
				Dim img As GUIButton3PartControl = TryCast(GetControl(iControlId), GUIButton3PartControl)
				If img IsNot Nothing AndAlso img.IsVisible Then
					img.ColourDiffuse = &HffffffffUI
					_currentProgram = TryCast(img.Data, Program)
					SetProperties()
				End If
				GUIControl.FocusControl(GetID, iControlId)
			End If
		End Sub

		Private Sub Correct()
			Dim iControlId As Integer
			If _cursorY < MinYIndex Then
				' either channel or group button
				_cursorY = MinYIndex
			End If
			If _cursorY > 0 Then
				While _cursorY > 0
					iControlId = GUIDE_COMPONENTID_START + _cursorX * RowID + (_cursorY - 1) * ColID
					Dim cntl As GUIControl = GetControl(iControlId)
					If cntl Is Nothing Then
						_cursorY -= 1
					ElseIf Not cntl.IsVisible Then
						_cursorY -= 1
					Else
						Exit While
					End If
				End While
			End If
			If _cursorX < 0 Then
				_cursorX = 0
			End If
			If Not _singleChannelView Then
				While _cursorX > 0
					iControlId = GUIDE_COMPONENTID_START + _cursorX * RowID + (0) * ColID
					Dim cntl As GUIControl = GetControl(iControlId)
					If cntl Is Nothing Then
						_cursorX -= 1
					ElseIf Not cntl.IsVisible Then
						_cursorX -= 1
					Else
						Exit While
					End If
				End While
			End If
		End Sub

		Private Sub ShowContextMenu()
			Dim dlg As GUIDialogMenu = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_MENU)), GUIDialogMenu)
			If dlg IsNot Nothing Then
				dlg.Reset()
				dlg.SetHeading(GUILocalizeStrings.[Get](924))
				'Menu
				If _currentChannel IsNot Nothing Then
						' View this channel
					dlg.AddLocalizedString(938)
				End If

				If _currentProgram.IdProgram <> 0 Then
						'Upcoming episodes
					dlg.AddLocalizedString(1041)
				End If

				If _currentProgram IsNot Nothing AndAlso _currentProgram.StartTime > DateTime.Now Then
					If _currentProgram.Notify Then
							' cancel reminder
						dlg.AddLocalizedString(1212)
					Else
							' set reminder
						dlg.AddLocalizedString(1040)
					End If
				End If

				dlg.AddLocalizedString(939)
				' Switch mode
				Dim isRecordingNoEPG__1 As Boolean = False

				If _currentProgram IsNot Nothing AndAlso _currentChannel IsNot Nothing AndAlso _currentTitle.Length > 0 Then
					If _currentProgram.IdProgram = 0 Then
						' no EPG program recording., only allow to stop it.
						isRecordingNoEPG__1 = IsRecordingNoEPG(_currentProgram.ReferencedChannel())
						If isRecordingNoEPG__1 Then
								' stop non EPG Recording
							dlg.AddLocalizedString(629)
						Else
								' start non EPG Recording
							dlg.AddLocalizedString(264)
						End If
					ElseIf Not _currentRecOrNotify Then
							' Record
						dlg.AddLocalizedString(264)
					Else

							' Edit Recording
						dlg.AddLocalizedString(637)
					End If
				End If
				'dlg.AddLocalizedString(937);// Reload tvguide

				If TVHome.Navigator.Groups.Count > 1 Then
						' Group
					dlg.AddLocalizedString(971)
				End If

				dlg.AddLocalizedString(368)
				' IMDB
				dlg.DoModal(GetID)
				If dlg.SelectedLabel = -1 Then
					Return
				End If
				Select Case dlg.SelectedId

					Case 1041
						ShowProgramInfo()
						Log.Debug("TVGuide: show episodes or repeatings for current show")
						Exit Select
					Case 368
						' IMDB
						OnGetIMDBInfo()
						Exit Select
					Case 971
						'group
						OnSelectChannelGroup()
						Exit Select
					' set reminder
					Case 1040, 1212
						' cancel reminder
						OnNotify()
						Exit Select

					Case 938
						' view channel
						Log.Debug("viewch channel:{0}", _currentChannel)
						TVHome.ViewChannelAndCheck(_currentProgram.ReferencedChannel())
						If TVHome.Card.IsTimeShifting AndAlso TVHome.Card.IdChannel = _currentProgram.ReferencedChannel().IdChannel Then
							g_Player.ShowFullScreenWindow()
						End If
						Return


					Case 939
						' switch mode
						OnSwitchMode()
						Exit Select
					Case 629
						'stop recording
						Dim schedule__2 As Schedule = Schedule.FindNoEPGSchedule(_currentProgram.ReferencedChannel())
						TVUtil.DeleteRecAndEntireSchedWithPrompt(schedule__2)
						Update(True)
						'remove RED marker
						Exit Select

					' edit recording
					Case 637, 264
						' record
						If _currentProgram.IdProgram = 0 Then
							TVHome.StartRecordingSchedule(_currentProgram.ReferencedChannel(), True)
							_currentProgram.IsRecordingOncePending = True
								'add RED marker
							Update(True)
						Else
							OnRecordContext()
						End If
						Exit Select
				End Select
			End If
		End Sub

		Private Sub OnSwitchMode()
			UnFocus()
			_singleChannelView = Not _singleChannelView
			If _singleChannelView Then
				_backupCursorX = _cursorY
				_backupCursorY = _cursorX
				_backupChannelOffset = ChannelOffset

				_programOffset = InlineAssignHelper(_cursorY, InlineAssignHelper(_cursorX, 0))
				_recalculateProgramOffset = True
			Else
				'focus current channel
				_cursorY = 0
				_cursorX = _backupCursorY
				ChannelOffset = _backupChannelOffset
			End If
			Update(True)
			SetFocus()
		End Sub

		Private Sub ShowProgramInfo()
			If _currentProgram Is Nothing Then
				Return
			End If

			TVProgramInfo.CurrentProgram = _currentProgram
			GUIWindowManager.ActivateWindow(CInt(Window.WINDOW_TV_PROGRAM_INFO))
		End Sub

		Private Sub OnGetIMDBInfo()
			Dim movieDetails As New IMDBMovie()
			movieDetails.SearchString = _currentProgram.Title
			If IMDBFetcher.GetInfoFromIMDB(Me, movieDetails, True, False) Then
				Dim dbLayer As New TvBusinessLayer()

				Dim progs As IList(Of Program) = dbLayer.GetProgramExists(Channel.Retrieve(_currentProgram.IdChannel), _currentProgram.StartTime, _currentProgram.EndTime)
				If progs IsNot Nothing AndAlso progs.Count > 0 Then
					Dim prog As Program = DirectCast(progs(0), Program)
					prog.Description = movieDetails.Plot
					prog.Genre = movieDetails.Genre
					prog.StarRating = CInt(movieDetails.Rating)
					prog.Persist()
				End If
				Dim videoInfo As GUIVideoInfo = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_VIDEO_INFO)), GUIVideoInfo)
				videoInfo.Movie = movieDetails
				Dim btnPlay As GUIButtonControl = DirectCast(videoInfo.GetControl(2), GUIButtonControl)
				btnPlay.Visible = False
				GUIWindowManager.ActivateWindow(CInt(Window.WINDOW_VIDEO_INFO))
			Else
				Log.Info("IMDB Fetcher: Nothing found")
			End If
		End Sub

		Private Sub OnSelectItem(isItemSelected As Boolean)
			If _currentProgram Is Nothing Then
				Return
			End If
			If isItemSelected Then
				If _currentProgram.IsRunningAt(DateTime.Now) OrElse _currentProgram.EndTime <= DateTime.Now Then
					'view this channel
					If g_Player.Playing AndAlso g_Player.IsTVRecording Then
						g_Player.[Stop](True)
					End If
					Try
						Dim fileName As String = ""
						Dim isRec As Boolean = _currentProgram.IsRecording

						Dim rec As Recording = Nothing
						If isRec Then
							rec = Recording.ActiveRecording(_currentProgram.Title, _currentProgram.IdChannel)
						End If


						If rec IsNot Nothing Then
							fileName = rec.FileName
						End If

						If Not String.IsNullOrEmpty(fileName) Then
							'are we really recording ?
							Log.Info("TVGuide: clicked on a currently running recording")
							Dim dlg As GUIDialogMenu = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_MENU)), GUIDialogMenu)
							If dlg Is Nothing Then
								Return
							End If

							dlg.Reset()
							dlg.SetHeading(_currentProgram.Title)
							dlg.AddLocalizedString(979)
							'Play recording from beginning
							dlg.AddLocalizedString(938)
							'View this channel
							dlg.DoModal(GetID)

							If dlg.SelectedLabel = -1 Then
								Return
							End If
							If _recordingList IsNot Nothing Then
								Log.Debug("TVGuide: Found current program {0} in recording list", _currentTitle)
								Select Case dlg.SelectedId
									Case 979
										' Play recording from beginning
										If True Then
											Dim recDB As Recording = Recording.Retrieve(fileName)
											If recDB IsNot Nothing Then
												TVUtil.PlayRecording(recDB)
											End If
										End If
										Return

									Case 938
										' View this channel
										If True Then
											TVHome.ViewChannelAndCheck(_currentProgram.ReferencedChannel())
											If g_Player.Playing Then
												g_Player.ShowFullScreenWindow()
											End If
										End If
										Return
								End Select
							Else
								Log.Info("EPG: _recordingList was not available")
							End If


							If String.IsNullOrEmpty(fileName) Then
								TVHome.ViewChannelAndCheck(_currentProgram.ReferencedChannel())
								If g_Player.Playing Then
									g_Player.ShowFullScreenWindow()
								End If
							End If
						Else
							'not recording
							' clicked the show we're currently watching
							If TVHome.Navigator.Channel IsNot Nothing AndAlso TVHome.Navigator.Channel.IdChannel = _currentChannel.IdChannel AndAlso g_Player.Playing AndAlso g_Player.IsTV Then
								Log.Debug("TVGuide: clicked on a currently running show")
								Dim dlg As GUIDialogMenu = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_MENU)), GUIDialogMenu)
								If dlg Is Nothing Then
									Return
								End If

								dlg.Reset()
								dlg.SetHeading(_currentProgram.Title)
								dlg.AddLocalizedString(938)
								'View this channel
								dlg.AddLocalizedString(1041)
								'Upcoming episodes
								dlg.DoModal(GetID)

								If dlg.SelectedLabel = -1 Then
									Return
								End If

								Select Case dlg.SelectedId
									Case 1041
										ShowProgramInfo()
										Log.Debug("TVGuide: show episodes or repeatings for current show")
										Exit Select
									Case 938
										Log.Debug("TVGuide: switch currently running show to fullscreen")
										GUIWaitCursor.Show()
										TVHome.ViewChannelAndCheck(_currentProgram.ReferencedChannel())
										GUIWaitCursor.Hide()
										If g_Player.Playing Then
											g_Player.ShowFullScreenWindow()
										Else
											Log.Debug("TVGuide: no show currently running to switch to fullscreen")
										End If
										Exit Select
								End Select
							Else
								Dim isPlayingTV As Boolean = (g_Player.FullScreen AndAlso g_Player.IsTV)
								' zap to selected show's channel
								TVHome.UserChannelChanged = True
								' fixing mantis 1874: TV doesn't start when from other playing media to TVGuide & select program
								GUIWaitCursor.Show()
								TVHome.ViewChannelAndCheck(_currentProgram.ReferencedChannel())
								GUIWaitCursor.Hide()
								If g_Player.Playing Then
									If isPlayingTV Then
										GUIWindowManager.CloseCurrentWindow()
									End If
									g_Player.ShowFullScreenWindow()
								End If
							End If
							'end of not recording
						End If
					Finally
						If VMR9Util.g_vmr9 IsNot Nothing Then
							VMR9Util.g_vmr9.Enable(True)
						End If
					End Try

					Return
				End If
				ShowProgramInfo()
				Return
			Else
				ShowProgramInfo()
			End If
		End Sub

		''' <summary>
		''' "Record" via REC button
		''' </summary>
		Private Sub OnRecord()
			If _currentProgram Is Nothing Then
				Return
			End If
			If (_currentProgram.IsRunningAt(DateTime.Now) OrElse (_currentProgram.EndTime <= DateTime.Now)) Then
				'record current programme
				Dim tvHome__1 As GUIWindow = GUIWindowManager.GetWindow(CInt(Window.WINDOW_TV))
				If (tvHome__1 IsNot Nothing) AndAlso (tvHome__1.GetID <> GUIWindowManager.ActiveWindow) Then
					'tvHome.OnAction(new Action(Action.ActionType.ACTION_RECORD, 0, 0));
					Dim didRecStart As Boolean = TVHome.ManualRecord(_currentProgram.ReferencedChannel(), GetID)
					_currentProgram.IsRecordingOncePending = didRecStart
					'refresh view.
					If didRecStart Then
						SyncLock _recordingsExpectedLock
							_recordingsExpected.Add(_currentProgram.ReferencedChannel())
						End SyncLock
					Else
						SyncLock _recordingsExpectedLock
							If _recordingsExpected.Contains(_currentProgram.ReferencedChannel()) Then
								_recordingsExpected.Remove(_currentProgram.ReferencedChannel())
							End If
						End SyncLock
					End If
					_needUpdate = True
				End If
			Else
				ShowProgramInfo()
			End If
		End Sub

		''' <summary>
		''' "Record" entry in context menu
		''' </summary>
		Private Sub OnRecordContext()
			If _currentProgram Is Nothing Then
				Return
			End If
			ShowProgramInfo()
		End Sub

		Private Sub CheckRecordingConflicts()
		End Sub

		Private Sub OnPageUp()
			Dim Steps As Integer
			If _singleChannelView Then
					' all available rows
				Steps = _channelCount
			Else
				If _guideContinuousScroll Then
						' all available rows
					Steps = _channelCount
				Else
					' If we're on the first channel in the guide then allow one step to get back to the end of the guide.
					If ChannelOffset = 0 AndAlso _cursorX = 0 Then
						Steps = 1
					Else
						' only number of additional avail channels
						Steps = Math.Min(ChannelOffset + _cursorX, _channelCount)
					End If
				End If
			End If
			UnFocus()
			For i As Integer = 0 To Steps - 1
				OnUp(False, True)
			Next
			Correct()
			Update(False)
			SetFocus()
		End Sub

		Private Sub OnPageDown()
			Dim Steps As Integer
			If _singleChannelView Then
				Steps = _channelCount
			Else
				' all available rows
				If _guideContinuousScroll Then
						' all available rows
					Steps = _channelCount
				Else
					' If we're on the last channel in the guide then allow one step to get back to top of guide.
					If ChannelOffset + (_cursorX + 1) = _channelList.Count Then
						Steps = 1
					Else
						' only number of additional avail channels
						Steps = Math.Min(_channelList.Count - ChannelOffset - _cursorX - 1, _channelCount)
					End If
				End If
			End If

			UnFocus()
			For i As Integer = 0 To Steps - 1
				OnDown(False)
			Next
			Correct()
			Update(False)
			SetFocus()
		End Sub

		Private Sub OnNextDay()
			_viewingTime = _viewingTime.AddDays(1.0)
			_recalculateProgramOffset = True
			Update(False)
			SetFocus()
		End Sub

		Private Sub OnPreviousDay()
			_viewingTime = _viewingTime.AddDays(-1.0)
			_recalculateProgramOffset = True
			Update(False)
			SetFocus()
		End Sub

		Private Function GetColorForGenre(genre As String) As Long
			'''@
			'
'      if (!_useColorsForGenres) return Color.White.ToArgb();
'      List<string> genres = new List<string>();
'      TVDatabase.GetGenres(ref genres);
'
'      genre = genre.ToLower();
'      for (int i = 0; i < genres.Count; ++i)
'      {
'        if (String.Compare(genre, (string)genres[i], true) == 0)
'        {
'          Color col = (Color)_colorList[i % _colorList.Count];
'          return col.ToArgb();
'        }
'      }

			Return Color.White.ToArgb()
		End Function


		Private Sub OnKeyTimeout()
			If _lineInput.Length = 0 Then
				' Hide label if no keyed channel number to display.
				Dim label As GUILabelControl = TryCast(GetControl(CInt(Controls.LABEL_KEYED_CHANNEL)), GUILabelControl)
				If label IsNot Nothing Then
					label.IsVisible = False
				End If
				Return
			End If
			Dim ts As TimeSpan = DateTime.Now - _keyPressedTimer
			If ts.TotalMilliseconds >= 1000 Then
				' change channel
				Dim iChannel As Integer = Int32.Parse(_lineInput)
				ChangeChannelNr(iChannel)
				_lineInput = [String].Empty
			End If
		End Sub

		Private Sub OnKeyCode(chKey As Char)
			' Don't accept keys when in single channel mode.
			If _singleChannelView Then
				Return
			End If

			If chKey >= "0"C AndAlso chKey <= "9"C Then
				'Make sure it's only for the remote
				Dim ts As TimeSpan = DateTime.Now - _keyPressedTimer
				If _lineInput.Length >= _channelNumberMaxLength OrElse ts.TotalMilliseconds >= 1000 Then
					_lineInput = [String].Empty
				End If
				_keyPressedTimer = DateTime.Now
				_lineInput += chKey

				' give feedback to user that numbers are being entered
				' Check for new standalone label control for keyed in channel numbers.
				Dim label As GUILabelControl
				label = TryCast(GetControl(CInt(Controls.LABEL_KEYED_CHANNEL)), GUILabelControl)
				If label IsNot Nothing Then
					' Show the keyed channel number.
					label.IsVisible = True
				Else
					label = TryCast(GetControl(CInt(Controls.LABEL_TIME1)), GUILabelControl)
				End If
				label.Label = _lineInput

				' Add an underscore "cursor" to visually indicate that more numbers may be entered.
				If _lineInput.Length < _channelNumberMaxLength Then
					label.Label += "_"
				End If

				If _lineInput.Length = _channelNumberMaxLength Then
					' change channel
					Dim iChannel As Integer = Int32.Parse(_lineInput)
					ChangeChannelNr(iChannel)

					' Hide the keyed channel number label.
					Dim labelKeyed As GUILabelControl = TryCast(GetControl(CInt(Controls.LABEL_KEYED_CHANNEL)), GUILabelControl)
					If labelKeyed IsNot Nothing Then
						labelKeyed.IsVisible = False
					End If
				End If
			End If
		End Sub

		Private Sub ChangeChannelNr(iChannelNr As Integer)
			Dim iCounter As Integer = 0
			Dim found As Boolean = False
			Dim searchChannel As Integer = iChannelNr

			Dim chan As Channel
			Dim channelDistance As Integer = 99999

			If _byIndex = False Then
				While iCounter < _channelList.Count AndAlso found = False
					chan = DirectCast(_channelList(iCounter).channel, Channel)
					For Each detail As TuningDetail In chan.ReferringTuningDetail()
						If detail.ChannelNumber = searchChannel Then
							iChannelNr = iCounter
							found = True
						'find closest channel number
						ElseIf CInt(Math.Abs(detail.ChannelNumber - searchChannel)) < channelDistance Then
							channelDistance = CInt(Math.Abs(detail.ChannelNumber - searchChannel))
							iChannelNr = iCounter
						End If
					Next
					iCounter += 1
				End While
			Else
					' offset for indexed channel number
				iChannelNr -= 1
			End If
			If iChannelNr >= 0 AndAlso iChannelNr < _channelList.Count Then
				UnFocus()
				ChannelOffset = 0
				_cursorX = 0

				' Last page adjust (To get a full page channel listing)
				If iChannelNr > _channelList.Count - Math.Min(_channelList.Count, _channelCount) + 1 Then
					' minimum of available channel/max visible channels
					ChannelOffset = _channelList.Count - _channelCount
					iChannelNr = iChannelNr - ChannelOffset
				End If

				While iChannelNr >= Math.Min(_channelList.Count, _channelCount)
					iChannelNr -= Math.Min(_channelList.Count, _channelCount)
					ChannelOffset += Math.Min(_channelList.Count, _channelCount)
				End While
				_cursorX = iChannelNr
			End If
			Update(False)
			SetFocus()
		End Sub

		Protected Overrides Sub LoadSchedules(refresh As Boolean)
			If refresh Then
				_recordingList = Schedule.ListAll()
				Return
			End If
		End Sub

		Protected Overrides Sub GetChannels(refresh As Boolean)
			If refresh OrElse _channelList Is Nothing Then
				_channelList = New List(Of GuideChannel)()
			End If

			If _channelList.Count = 0 Then
				Try
					If TVHome.Navigator.CurrentGroup IsNot Nothing Then
						Dim layer As New TvBusinessLayer()
						Dim channels As IList(Of Channel) = layer.GetTVGuideChannelsForGroup(TVHome.Navigator.CurrentGroup.IdGroup)
						For Each chan As Channel In channels
							Dim tvGuidChannel As New GuideChannel()
							tvGuidChannel.channel = chan

							If tvGuidChannel.channel.VisibleInGuide AndAlso tvGuidChannel.channel.IsTv Then
								If _showChannelNumber Then

									If _byIndex Then
										tvGuidChannel.channelNum = _channelList.Count + 1
									Else
										For Each detail As TuningDetail In tvGuidChannel.channel.ReferringTuningDetail()
											tvGuidChannel.channelNum = detail.ChannelNumber
										Next
									End If
								End If
								tvGuidChannel.strLogo = GetChannelLogo(tvGuidChannel.channel.DisplayName)
								_channelList.Add(tvGuidChannel)
							End If
						Next
					End If
				Catch
				End Try

				If _channelList.Count = 0 Then
					Dim tvGuidChannel As New GuideChannel()
					tvGuidChannel.channel = New Channel(False, True, 0, DateTime.MinValue, False, DateTime.MinValue, _
						0, True, "", GUILocalizeStrings.[Get](911))
					For i As Integer = 0 To 9
						_channelList.Add(tvGuidChannel)
					Next
				End If
			End If
		End Sub

		Protected Overrides Sub UpdateVerticalScrollbar()
			If _channelList Is Nothing OrElse _channelList.Count <= 0 Then
				Return
			End If
			Dim channel As Integer = _cursorX + ChannelOffset
			While channel > 0 AndAlso channel >= _channelList.Count
				channel -= _channelList.Count
			End While
			Dim current As Single = CSng(_cursorX + ChannelOffset)
			Dim total As Single = CSng(_channelList.Count) - 1

			If _singleChannelView Then
				current = CSng(_cursorX + _programOffset)
				total = CSng(_totalProgramCount) - 1
			End If
			If total = 0 Then
				total = _channelCount
			End If

			Dim percentage As Single = (current / total) * 100F
			If percentage < 0 Then
				percentage = 0
			End If
			If percentage > 100 Then
				percentage = 100
			End If
			Dim scrollbar As GUIVerticalScrollbar = TryCast(GetControl(CInt(Controls.VERT_SCROLLBAR)), GUIVerticalScrollbar)
			If scrollbar IsNot Nothing Then
				scrollbar.Percentage = percentage
			End If
		End Sub

		Protected Overrides Sub UpdateHorizontalScrollbar()
			If _channelList Is Nothing Then
				Return
			End If
			Dim scrollbar As GUIHorizontalScrollbar = TryCast(GetControl(CInt(Controls.HORZ_SCROLLBAR)), GUIHorizontalScrollbar)
			If scrollbar IsNot Nothing Then
				Dim percentage As Single = CSng(_viewingTime.Hour) * 60 + _viewingTime.Minute + CSng(_timePerBlock) * (CSng(_viewingTime.Hour) / 24F)
				percentage /= (24F * 60F)
				percentage *= 100F
				If percentage < 0 Then
					percentage = 0
				End If
				If percentage > 100 Then
					percentage = 100
				End If
				If _singleChannelView Then
					percentage = 0
				End If

				If CInt(Math.Truncate(percentage)) <> CInt(scrollbar.Percentage) Then
					scrollbar.Percentage = percentage
				End If
			End If
		End Sub

		Protected Overrides Function CalcDays() As Integer
			Dim iDay As Integer = _viewingTime.DayOfYear - DateTime.Now.DayOfYear
			If _viewingTime.Year > DateTime.Now.Year Then
				iDay += (New DateTime(DateTime.Now.Year, 12, 31)).DayOfYear
			End If
			Return iDay
		End Function

		Protected Function GetKeyboard(ByRef strLine As String) As Boolean
			Dim keyboard As VirtualKeyboard = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_VIRTUAL_KEYBOARD)), VirtualKeyboard)
			If keyboard Is Nothing Then
				Return False
			End If
			keyboard.Reset()
			keyboard.Text = strLine
			keyboard.DoModal(GetID)
			If keyboard.IsConfirmed Then
				strLine = keyboard.Text
				Return True
			End If
			Return False
		End Function

		#Region "TV Database callbacks"

		Protected Sub TVDatabase_On_notifyListChanged()
			''' @
			'
'      if (_notifyList != null)
'      {
'        _notifyList.Clear();
'        TVDatabase.GetNotifies(_notifyList, false);
'        _needUpdate = true;
'      }
'       

		End Sub

		Protected Sub ConflictManager_OnConflictsUpdated()
			_needUpdate = True
		End Sub

		Protected Sub TVDatabase_OnProgramsChanged()
			_needUpdate = True
		End Sub

		#End Region

		''' <summary>
		''' Calculates the duration of a program and sets the Duration property
		''' </summary>
		Private Function GetDuration(program As Program) As String
			If program.Title = "No TVGuide data available" Then
				Return ""
			End If
			Dim space As String = " "
			Dim progStart As DateTime = program.StartTime
			Dim progEnd As DateTime = program.EndTime
			Dim progDuration As TimeSpan = progEnd.Subtract(progStart)
			Dim duration As String = ""
			Select Case progDuration.Hours
				Case 0
					duration = progDuration.Minutes & space & GUILocalizeStrings.[Get](3004)
					Exit Select
				Case 1
					If progDuration.Minutes = 1 Then
						duration = progDuration.Hours & space & GUILocalizeStrings.[Get](3001) & ", " & progDuration.Minutes & space & GUILocalizeStrings.[Get](3003)
					ElseIf progDuration.Minutes > 1 Then
						duration = progDuration.Hours & space & GUILocalizeStrings.[Get](3001) & ", " & progDuration.Minutes & space & GUILocalizeStrings.[Get](3004)
					Else
						duration = progDuration.Hours & space & GUILocalizeStrings.[Get](3001)
					End If
					Exit Select
				Case Else
					If progDuration.Minutes = 1 Then
						duration = progDuration.Hours & " Hours" & ", " & progDuration.Minutes & space & GUILocalizeStrings.[Get](3003)
					ElseIf progDuration.Minutes > 0 Then
						duration = progDuration.Hours & " Hours" & ", " & progDuration.Minutes & space & GUILocalizeStrings.[Get](3004)
					Else
						duration = progDuration.Hours & space & GUILocalizeStrings.[Get](3002)
					End If
					Exit Select
			End Select
			Return duration
		End Function

		Private Function GetDurationAsMinutes(program As Program) As String
			If program.Title = "No TVGuide data available" Then
				Return ""
			End If
			Dim progStart As DateTime = program.StartTime
			Dim progEnd As DateTime = program.EndTime
			Dim progDuration As TimeSpan = progEnd.Subtract(progStart)
			Return progDuration.TotalMinutes & " " & GUILocalizeStrings.[Get](2998)
		End Function

		''' <summary>
		''' Calculates how long from current time a program starts or started, set the TimeFromNow property
		''' </summary>
		Private Function GetStartTimeFromNow(program As Program) As String
			Dim timeFromNow As String = [String].Empty
			If program.Title = "No TVGuide data available" Then
				Return timeFromNow
			End If
			Dim space As String = " "
			Dim strRemaining As String = [String].Empty
			Dim progStart As DateTime = program.StartTime
			Dim timeRelative As TimeSpan = progStart.Subtract(DateTime.Now)
			If timeRelative.Days = 0 Then
				If timeRelative.Hours >= 0 AndAlso timeRelative.Minutes >= 0 Then
					Select Case timeRelative.Hours
						Case 0
							If timeRelative.Minutes = 1 Then
									' starts in 1 minute
								timeFromNow = GUILocalizeStrings.[Get](3009) & " " & timeRelative.Minutes & space & GUILocalizeStrings.[Get](3003)
							ElseIf timeRelative.Minutes > 1 Then
									'starts in x minutes
								timeFromNow = GUILocalizeStrings.[Get](3009) & " " & timeRelative.Minutes & space & GUILocalizeStrings.[Get](3004)
							Else
								timeFromNow = GUILocalizeStrings.[Get](3013)
							End If
							Exit Select
						Case 1
							If timeRelative.Minutes = 1 Then
									'starts in 1 hour, 1 minute
								timeFromNow = GUILocalizeStrings.[Get](3009) & " " & timeRelative.Hours & space & GUILocalizeStrings.[Get](3001) & ", " & timeRelative.Minutes & space & GUILocalizeStrings.[Get](3003)
							ElseIf timeRelative.Minutes > 1 Then
									'starts in 1 hour, x minutes
								timeFromNow = GUILocalizeStrings.[Get](3009) & " " & timeRelative.Hours & space & GUILocalizeStrings.[Get](3001) & ", " & timeRelative.Minutes & space & GUILocalizeStrings.[Get](3004)
							Else
									'starts in 1 hour
								timeFromNow = GUILocalizeStrings.[Get](3009) & " " & timeRelative.Hours & GUILocalizeStrings.[Get](3001)
							End If
							Exit Select
						Case Else
							If timeRelative.Minutes = 1 Then
									'starts in x hours, 1 minute
								timeFromNow = GUILocalizeStrings.[Get](3009) & " " & timeRelative.Hours & space & GUILocalizeStrings.[Get](3002) & ", " & timeRelative.Minutes & space & GUILocalizeStrings.[Get](3003)
							ElseIf timeRelative.Minutes > 1 Then
									'starts in x hours, x minutes
								timeFromNow = GUILocalizeStrings.[Get](3009) & " " & timeRelative.Hours & space & GUILocalizeStrings.[Get](3002) & ", " & timeRelative.Minutes & space & GUILocalizeStrings.[Get](3004)
							Else
									'starts in x hours
								timeFromNow = GUILocalizeStrings.[Get](3009) & " " & timeRelative.Hours & space & GUILocalizeStrings.[Get](3002)
							End If
							Exit Select
					End Select
				Else
					'already started
					Dim progEnd As DateTime = program.EndTime
					Dim tsRemaining As TimeSpan = DateTime.Now.Subtract(progEnd)
					If tsRemaining.Minutes > 0 Then
						timeFromNow = GUILocalizeStrings.[Get](3016)
						Return timeFromNow
					End If
					Select Case tsRemaining.Hours
						Case 0
							If timeRelative.Minutes = 1 Then
									'(1 Minute Remaining)
								strRemaining = "(" & -tsRemaining.Minutes & space & GUILocalizeStrings.[Get](3018) & ")"
							Else
									'(x Minutes Remaining)
								strRemaining = "(" & -tsRemaining.Minutes & space & GUILocalizeStrings.[Get](3010) & ")"
							End If
							Exit Select
						Case -1
							If timeRelative.Minutes = 1 Then
									'(1 Hour,1 Minute Remaining)
								strRemaining = "(" & -tsRemaining.Hours & space & GUILocalizeStrings.[Get](3001) & ", " & -tsRemaining.Minutes & space & GUILocalizeStrings.[Get](3018) & ")"
							ElseIf timeRelative.Minutes > 1 Then
									'(1 Hour,x Minutes Remaining)
								strRemaining = "(" & -tsRemaining.Hours & space & GUILocalizeStrings.[Get](3001) & ", " & -tsRemaining.Minutes & space & GUILocalizeStrings.[Get](3010) & ")"
							Else
									'(1 Hour Remaining)
								strRemaining = "(" & -tsRemaining.Hours & space & GUILocalizeStrings.[Get](3012) & ")"
							End If
							Exit Select
						Case Else
							If timeRelative.Minutes = 1 Then
									'(x Hours,1 Minute Remaining)
								strRemaining = "(" & -tsRemaining.Hours & space & GUILocalizeStrings.[Get](3002) & ", " & -tsRemaining.Minutes & space & GUILocalizeStrings.[Get](3018) & ")"
							ElseIf timeRelative.Minutes > 1 Then
									'(x Hours,x Minutes Remaining)
								strRemaining = "(" & -tsRemaining.Hours & space & GUILocalizeStrings.[Get](3002) & ", " & -tsRemaining.Minutes & space & GUILocalizeStrings.[Get](3010) & ")"
							Else
									'(x Hours Remaining)
								strRemaining = "(" & -tsRemaining.Hours & space & GUILocalizeStrings.[Get](3012) & ")"
							End If
							Exit Select
					End Select
					Select Case timeRelative.Hours
						Case 0
							If timeRelative.Minutes = -1 Then
									'Started 1 Minute ago
								timeFromNow = GUILocalizeStrings.[Get](3017) + -timeRelative.Minutes & space & GUILocalizeStrings.[Get](3007) & space & strRemaining
							ElseIf timeRelative.Minutes < -1 Then
									'Started x Minutes ago
								timeFromNow = GUILocalizeStrings.[Get](3017) + -timeRelative.Minutes & space & GUILocalizeStrings.[Get](3008) & space & strRemaining
							Else
									'Starting Now
								timeFromNow = GUILocalizeStrings.[Get](3013)
							End If
							Exit Select
						Case -1
							If timeRelative.Minutes = -1 Then
									'Started 1 Hour,1 Minute ago
								timeFromNow = GUILocalizeStrings.[Get](3017) + -timeRelative.Hours & space & GUILocalizeStrings.[Get](3001) & ", " & -timeRelative.Minutes & space & GUILocalizeStrings.[Get](3007) & " " & strRemaining
							ElseIf timeRelative.Minutes < -1 Then
									'Started 1 Hour,x Minutes ago
								timeFromNow = GUILocalizeStrings.[Get](3017) + -timeRelative.Hours & space & GUILocalizeStrings.[Get](3001) & ", " & -timeRelative.Minutes & space & GUILocalizeStrings.[Get](3008) & " " & strRemaining
							Else
									'Started 1 Hour ago
								timeFromNow = GUILocalizeStrings.[Get](3017) + -timeRelative.Hours & space & GUILocalizeStrings.[Get](3005) & space & strRemaining
							End If
							Exit Select
						Case Else
							If timeRelative.Minutes = -1 Then
									'Started x Hours,1 Minute ago
								timeFromNow = GUILocalizeStrings.[Get](3017) + -timeRelative.Hours & space & GUILocalizeStrings.[Get](3006) & ", " & -timeRelative.Minutes & space & GUILocalizeStrings.[Get](3008) & " " & strRemaining
							ElseIf timeRelative.Minutes < -1 Then
									'Started x Hours,x Minutes ago
								timeFromNow = GUILocalizeStrings.[Get](3017) + -timeRelative.Hours & space & GUILocalizeStrings.[Get](3006) & ", " & -timeRelative.Minutes & space & GUILocalizeStrings.[Get](3008) & " " & strRemaining
							Else
									'Started x Hours ago
								timeFromNow = GUILocalizeStrings.[Get](3017) + -timeRelative.Hours & space & GUILocalizeStrings.[Get](3006) & space & strRemaining
							End If
							Exit Select
					End Select
				End If
			Else
				If timeRelative.Days = 1 Then
						'Starts in 1 Day
					timeFromNow = GUILocalizeStrings.[Get](3009) & space & timeRelative.Days & space & GUILocalizeStrings.[Get](3014)
				Else
						'Starts in x Days
					timeFromNow = GUILocalizeStrings.[Get](3009) & space & timeRelative.Days & space & GUILocalizeStrings.[Get](3015)
				End If
			End If
			Return timeFromNow
		End Function

		Protected Overrides Sub setGuideHeadingVisibility(visible As Boolean)
			' can't rely on the heading text control having a unique id, so locate it using the localised heading string.
			' todo: update all skins to have a unique id for this control...?
			For Each control As GUIControl In controlList
				If TypeOf control Is GUILabelControl Then
					If DirectCast(control, GUILabelControl).Label = GUILocalizeStrings.[Get](4) Then
						' TV Guide heading
						control.Visible = visible
					End If
				End If
			Next
		End Sub

		Protected Overrides Sub setSingleChannelLabelVisibility(visible As Boolean)
			Dim channelLabel As GUILabelControl = TryCast(GetControl(CInt(Controls.SINGLE_CHANNEL_LABEL)), GUILabelControl)
			Dim channelImage As GUIImage = TryCast(GetControl(CInt(Controls.SINGLE_CHANNEL_IMAGE)), GUIImage)
			Dim timeInterval As GUISpinControl = TryCast(GetControl(CInt(Controls.SPINCONTROL_TIME_INTERVAL)), GUISpinControl)

			If channelLabel IsNot Nothing Then
				channelLabel.Visible = visible
			End If

			If channelImage IsNot Nothing Then
				channelImage.Visible = visible
			End If

			If timeInterval IsNot Nothing Then
				' If the x position of the control is negative then we assume that the control is not in the viewable area
				' and so it should not be made visible.  Skinners can set the x position negative to effectively remove the control
				' from the window.
				If timeInterval.XPosition < 0 Then
					timeInterval.Visible = False
				Else
					timeInterval.Visible = Not visible
				End If
			End If
		End Sub

		#End Region

		#Region "IMDB.IProgress"

		Public Function OnDisableCancel(fetcher As IMDBFetcher) As Boolean
			Dim pDlgProgress As GUIDialogProgress = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_PROGRESS)), GUIDialogProgress)
			If pDlgProgress.IsInstance(fetcher) Then
				pDlgProgress.DisableCancel(True)
			End If
			Return True
		End Function

		Public Sub OnProgress(line1 As String, line2 As String, line3 As String, percent As Integer)
			If Not GUIWindowManager.IsRouted Then
				Return
			End If
			Dim pDlgProgress As GUIDialogProgress = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_PROGRESS)), GUIDialogProgress)
			pDlgProgress.ShowProgressBar(True)
			pDlgProgress.SetLine(1, line1)
			pDlgProgress.SetLine(2, line2)
			If percent > 0 Then
				pDlgProgress.SetPercentage(percent)
			End If
			pDlgProgress.Progress()
		End Sub

		Public Function OnSearchStarting(fetcher As IMDBFetcher) As Boolean
			Dim pDlgProgress As GUIDialogProgress = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_PROGRESS)), GUIDialogProgress)
			' show dialog that we're busy querying www.imdb.com
			pDlgProgress.Reset()
			pDlgProgress.SetHeading(GUILocalizeStrings.[Get](197))
			pDlgProgress.SetLine(1, fetcher.MovieName)
			pDlgProgress.SetLine(2, String.Empty)
			pDlgProgress.SetObject(fetcher)
			pDlgProgress.StartModal(GUIWindowManager.ActiveWindow)
			Return True
		End Function

		Public Function OnSearchStarted(fetcher As IMDBFetcher) As Boolean
			Dim pDlgProgress As GUIDialogProgress = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_PROGRESS)), GUIDialogProgress)
			pDlgProgress.SetObject(fetcher)
			pDlgProgress.DoModal(GUIWindowManager.ActiveWindow)
			If pDlgProgress.IsCanceled Then
				Return False
			End If
			Return True
		End Function

		Public Function OnSearchEnd(fetcher As IMDBFetcher) As Boolean
			Dim pDlgProgress As GUIDialogProgress = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_PROGRESS)), GUIDialogProgress)
			If (pDlgProgress IsNot Nothing) AndAlso (pDlgProgress.IsInstance(fetcher)) Then
				pDlgProgress.Close()
			End If
			Return True
		End Function

		Public Function OnMovieNotFound(fetcher As IMDBFetcher) As Boolean
			Log.Info("IMDB Fetcher: OnMovieNotFound")
			' show dialog...
			Dim pDlgOK As GUIDialogOK = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_OK)), GUIDialogOK)
			pDlgOK.SetHeading(195)
			pDlgOK.SetLine(1, fetcher.MovieName)
			pDlgOK.SetLine(2, String.Empty)
			pDlgOK.DoModal(GUIWindowManager.ActiveWindow)
			Return True
		End Function

		Public Function OnDetailsStarting(fetcher As IMDBFetcher) As Boolean
			Dim pDlgProgress As GUIDialogProgress = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_PROGRESS)), GUIDialogProgress)
			' show dialog that we're downloading the movie info
			pDlgProgress.Reset()
			pDlgProgress.SetHeading(GUILocalizeStrings.[Get](198))
			'pDlgProgress.SetLine(0, strMovieName);
			pDlgProgress.SetLine(1, fetcher.MovieName)
			pDlgProgress.SetLine(2, String.Empty)
			pDlgProgress.SetObject(fetcher)
			pDlgProgress.StartModal(GUIWindowManager.ActiveWindow)
			Return True
		End Function

		Public Function OnDetailsStarted(fetcher As IMDBFetcher) As Boolean
			Dim pDlgProgress As GUIDialogProgress = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_PROGRESS)), GUIDialogProgress)
			pDlgProgress.SetObject(fetcher)
			pDlgProgress.DoModal(GUIWindowManager.ActiveWindow)
			If pDlgProgress.IsCanceled Then
				Return False
			End If
			Return True
		End Function

		Public Function OnDetailsEnd(fetcher As IMDBFetcher) As Boolean
			Dim pDlgProgress As GUIDialogProgress = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_PROGRESS)), GUIDialogProgress)
			If (pDlgProgress IsNot Nothing) AndAlso (pDlgProgress.IsInstance(fetcher)) Then
				pDlgProgress.Close()
			End If
			Return True
		End Function

		Public Function OnActorsStarting(fetcher As IMDBFetcher) As Boolean
			' won't occure
			Return True
		End Function

		Public Function OnActorsStarted(fetcher As IMDBFetcher) As Boolean
			' won't occure
			Return True
		End Function

		Public Function OnActorsEnd(fetcher As IMDBFetcher) As Boolean
			' won't occure
			Return True
		End Function

		Public Function OnDetailsNotFound(fetcher As IMDBFetcher) As Boolean
			Log.Info("IMDB Fetcher: OnDetailsNotFound")
			' show dialog...
			Dim pDlgOK As GUIDialogOK = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_OK)), GUIDialogOK)
			' show dialog...
			pDlgOK.SetHeading(195)
			pDlgOK.SetLine(1, fetcher.MovieName)
			pDlgOK.SetLine(2, String.Empty)
			pDlgOK.DoModal(GUIWindowManager.ActiveWindow)
			Return False
		End Function

		Public Function OnRequestMovieTitle(fetcher As IMDBFetcher, ByRef movieName As String) As Boolean
			' won't occure
			movieName = ""
			Return True
		End Function

		Public Function OnSelectMovie(fetcher As IMDBFetcher, ByRef selectedMovie As Integer) As Boolean
			' won't occure
			selectedMovie = 0
			Return True
		End Function

		Public Function OnScanStart(total As Integer) As Boolean
			' won't occure
			Return True
		End Function

		Public Function OnScanEnd() As Boolean
			' won't occure
			Return True
		End Function

		Public Function OnScanIterating(count As Integer) As Boolean
			' won't occure
			Return True
		End Function

		Public Function OnScanIterated(count As Integer) As Boolean
			' won't occure
			Return True
		End Function
		Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
			target = value
			Return value
		End Function

		#End Region
	End Class
End Namespace
