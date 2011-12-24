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

#Region "Usings"

Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Configuration
Imports System.Diagnostics
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Reflection
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms
Imports MediaPortal
Imports MediaPortal.Configuration
Imports MediaPortal.Dialogs
Imports MediaPortal.GUI.Library
Imports MediaPortal.GUI.Video
Imports MediaPortal.Player
Imports MediaPortal.Profile
Imports MediaPortal.Util
Imports TvControl
Imports TvDatabase
Imports TvLibrary.Implementations.DVB
Imports TvLibrary.Interfaces
Imports Action = MediaPortal.GUI.Library.Action

#End Region

Namespace TvPlugin
	''' <summary>
	''' TV Home screen.
	''' </summary>
	<PluginIcons("TvPlugin.TVPlugin.gif", "TvPlugin.TVPluginDisabled.gif")> _
	Public Class TVHome
		Inherits GUIInternalWindow
		Implements ISetupForm
		Implements IShowPlugin
		Implements IPluginReceiver
		#Region "constants"

		Private Const HEARTBEAT_INTERVAL As Integer = 1
		'seconds
		Private Const MAX_WAIT_FOR_SERVER_CONNECTION As Integer = 10
		'seconds
		Private Const WM_POWERBROADCAST As Integer = &H218
		Private Const WM_QUERYENDSESSION As Integer = &H11
		Private Const PBT_APMQUERYSUSPEND As Integer = &H0
		Private Const PBT_APMQUERYSTANDBY As Integer = &H1
		Private Const PBT_APMQUERYSUSPENDFAILED As Integer = &H2
		Private Const PBT_APMQUERYSTANDBYFAILED As Integer = &H3
		Private Const PBT_APMSUSPEND As Integer = &H4
		Private Const PBT_APMSTANDBY As Integer = &H5
		Private Const PBT_APMRESUMECRITICAL As Integer = &H6
		Private Const PBT_APMRESUMESUSPEND As Integer = &H7
		Private Const PBT_APMRESUMESTANDBY As Integer = &H8
		Private Const PBT_APMRESUMEAUTOMATIC As Integer = &H12
		Private Const PROGRESS_PERCENTAGE_UPDATE_INTERVAL As Integer = 1000
		Private Const PROCESS_UPDATE_INTERVAL As Integer = 1000

		#End Region

		#Region "variables"

		Private Enum Controls
			IMG_REC_CHANNEL = 21
			LABEL_REC_INFO = 22
			IMG_REC_RECTANGLE = 23
		End Enum

		<Flags> _
		Public Enum LiveTvStatus
			WasPlaying = 1
			CardChange = 2
			SeekToEnd = 4
			SeekToEndAfterPlayback = 8
		End Enum

		Private _resumeChannel As Channel = Nothing
		Private heartBeatTransmitterThread As Thread = Nothing
		Private Shared _updateProgressTimer As DateTime = DateTime.MinValue
		Private Shared m_navigator As ChannelNavigator
		Private Shared _util As TVUtil
		Private Shared _card As VirtualCard = Nothing
		Private Shared _updateTimer As DateTime = DateTime.Now
		Private Shared _autoTurnOnTv As Boolean = False
		Private Shared _waitonresume As Integer = 0
		Public Shared settingsLoaded As Boolean = False
		Private _cropManager As New TvCropManager()
		Private _notifyManager As New TvNotifyManager()
		Private Shared _preferredLanguages As List(Of String)
		Private Shared _usertsp As Boolean
		Private Shared _recordingpath As String = ""
		Private Shared _timeshiftingpath As String = ""
		Private Shared _preferAC3 As Boolean = False
		Private Shared _preferAudioTypeOverLang As Boolean = False
		Private Shared _autoFullScreen As Boolean = False
		Private Shared _suspended As Boolean = False
		Private Shared _showlastactivemodule As Boolean = False
		Private Shared _showlastactivemoduleFullscreen As Boolean = False
		Private Shared _playbackStopped As Boolean = False
		Private Shared _onPageLoadDone As Boolean = False
		Private Shared _userChannelChanged As Boolean = False
		Private Shared _showChannelStateIcons As Boolean = True
		Private Shared _doingHandleServerNotConnected As Boolean = False
		Private Shared _doingChannelChange As Boolean = False
		Private Shared _ServerNotConnectedHandled As Boolean = False
		Private Shared _recoverTV As Boolean = False
		Private Shared _connected As Boolean = False
		Private Shared _isAnyCardRecording As Boolean = False
		Protected Shared _server As TvServer

		Private Shared _waitForBlackScreen As ManualResetEvent = Nothing
		Private Shared _waitForVideoReceived As ManualResetEvent = Nothing

		Private Shared FramesBeforeStopRenderBlackImage As Integer = 0
		Private Shared _status As New BitHelper(Of LiveTvStatus)()

		<SkinControl(2)> _
		Protected btnTvGuide As GUIButtonControl = Nothing
		<SkinControl(3)> _
		Protected btnRecord As GUIButtonControl = Nothing
		<SkinControl(7)> _
		Protected btnChannel As GUIButtonControl = Nothing
		<SkinControl(8)> _
		Protected btnTvOnOff As GUIToggleButtonControl = Nothing
		<SkinControl(13)> _
		Protected btnTeletext As GUIButtonControl = Nothing
		<SkinControl(24)> _
		Protected imgRecordingIcon As GUIImage = Nothing
		<SkinControl(99)> _
		Protected videoWindow As GUIVideoControl = Nothing
		<SkinControl(9)> _
		Protected btnActiveStreams As GUIButtonControl = Nothing
		<SkinControl(14)> _
		Protected btnActiveRecordings As GUIButtonControl = Nothing

		' error handling
		Public Class ChannelErrorInfo
			Public FailingChannel As Channel
			Public Result As TvResult
			Public Messages As List(Of [String]) = New List(Of String)()
		End Class

		Public Shared _lastError As New ChannelErrorInfo()

		' CI Menu
		Private Shared ciMenuHandler As CiMenuHandler
		Public Shared dlgCiMenu As GUIDialogCIMenu
		Public Shared _dialogNotify As GUIDialogNotify = Nothing

		Private Shared currentCiMenu As CiMenu = Nothing
		Private Shared CiMenuLock As New Object()
		Private Shared CiMenuActive As Boolean = False

		Private Shared CiMenuList As New List(Of CiMenu)()

		' EPG Channel
		Private Shared _lastTvChannel As Channel = Nothing

		' notification
		Protected Shared _notifyTVTimeout As Integer = 15
		Protected Shared _playNotifyBeep As Boolean = True
		Protected Shared _preNotifyConfig As Integer = 60

		#End Region

		#Region "events & delegates"

		Private Shared Event OnChannelChanged As OnChannelChangedDelegate
		Private Delegate Sub OnChannelChangedDelegate()

		#End Region

		#Region "delegates"

		Private Delegate Sub StopPlayerMainThreadDelegate()

		#End Region

		#Region "ISetupForm Members"

		Public Function CanEnable() As Boolean
			Return True
		End Function

		Public Function PluginName() As String
			Return "TV"
		End Function

		Public Function DefaultEnabled() As Boolean
			Return True
		End Function

		Public Function GetWindowId() As Integer
			Return CInt(Window.WINDOW_TV)
		End Function

		Public Function GetHome(ByRef strButtonText As String, ByRef strButtonImage As String, ByRef strButtonImageFocus As String, ByRef strPictureImage As String) As Boolean
			' TODO:  Add TVHome.GetHome implementation
			strButtonText = GUILocalizeStrings.[Get](605)
			strButtonImage = ""
			strButtonImageFocus = ""
			strPictureImage = "hover_my tv.png"
			Return True
		End Function

		Public Function Author() As String
			Return "Frodo, gemx"
		End Function

		Public Function Description() As String
			Return "Connect to TV service to watch, record and timeshift analog and digital TV"
		End Function

		Public Function HasSetup() As Boolean
			Return False
		End Function

		Public Sub ShowPlugin()
		End Sub

		#End Region

		#Region "IShowPlugin Members"

		Public Function ShowDefaultHome() As Boolean
			Return True
		End Function

		#End Region

		Public Sub New()
			TVUtil.SetGentleConfigFile()

			GetID = CInt(Window.WINDOW_TV)
		End Sub

		#Region "Overrides"

		Public Overrides Function Init() As Boolean
			Return Load(GUIGraphicsContext.Skin + "\mytvhomeServer.xml")
		End Function

		Public Overrides Sub OnAdded()
			Log.Info("TVHome:OnAdded")
			RemoteControl.OnRemotingDisconnected += New RemoteControl.RemotingDisconnectedDelegate(AddressOf RemoteControl_OnRemotingDisconnected)
			RemoteControl.OnRemotingConnected += New RemoteControl.RemotingConnectedDelegate(AddressOf RemoteControl_OnRemotingConnected)

			GUIGraphicsContext.OnBlackImageRendered += New BlackImageRenderedHandler(AddressOf OnBlackImageRendered)
			GUIGraphicsContext.OnVideoReceived += New VideoReceivedHandler(AddressOf OnVideoReceived)

			_waitForBlackScreen = New ManualResetEvent(False)
			_waitForVideoReceived = New ManualResetEvent(False)

			AddHandler Application.ApplicationExit, New EventHandler(AddressOf Application_ApplicationExit)

			g_Player.PlayBackStarted += New g_Player.StartedHandler(AddressOf OnPlayBackStarted)
			g_Player.PlayBackStopped += New g_Player.StoppedHandler(AddressOf OnPlayBackStopped)
			g_Player.AudioTracksReady += New g_Player.AudioTracksReadyHandler(AddressOf OnAudioTracksReady)

			GUIWindowManager.Receivers += New SendMessageHandler(AddressOf OnGlobalMessage)

			' replace g_player's ShowFullScreenWindowTV
			g_Player.ShowFullScreenWindowTV = AddressOf ShowFullScreenWindowTVHandler

			Try
				' Make sure that we have valid hostname for the TV server
				SetRemoteControlHostName()

				' Wake up the TV server, if required
				HandleWakeUpTvServer()
				startHeartBeatThread()

				RemoveHandler TVHome.OnChannelChanged, New OnChannelChangedDelegate(AddressOf ForceUpdates)
				AddHandler TVHome.OnChannelChanged, New OnChannelChangedDelegate(AddressOf ForceUpdates)

				m_navigator = New ChannelNavigator()
				m_navigator.OnZapChannel -= New ChannelNavigator.OnZapChannelDelegate(AddressOf ForceUpdates)
				m_navigator.OnZapChannel += New ChannelNavigator.OnZapChannelDelegate(AddressOf ForceUpdates)
				LoadSettings()

				Dim pluginVersion As String = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion
				Dim tvServerVersion As String = If(Connected, RemoteControl.Instance.GetAssemblyVersion, "Unknown")

				If Connected AndAlso pluginVersion <> tvServerVersion Then
					Dim strLine As String = "TvPlugin and TvServer don't have the same version." & vbCr & vbLf
					strLine += "TvServer Version: " & tvServerVersion & vbCr & vbLf
					strLine += "TvPlugin Version: " & pluginVersion
					Log.[Error](strLine)
				Else
					Log.Info("TVHome V" & pluginVersion & ":ctor")
				End If
			Catch ex As Exception
				Log.[Error]("TVHome: Error occured in Init(): {0}, st {1}", ex.Message, Environment.StackTrace)
			End Try

			_notifyManager.Start()
		End Sub



		''' <summary>
		''' Gets called by the runtime when a  window will be destroyed
		''' Every window window should override this method and cleanup any resources
		''' </summary>
		''' <returns></returns>
		Public Overrides Sub DeInit()
			OnPageDestroy(-1)

			RemoteControl.OnRemotingDisconnected -= New RemoteControl.RemotingDisconnectedDelegate(AddressOf RemoteControl_OnRemotingDisconnected)
			RemoteControl.OnRemotingConnected -= New RemoteControl.RemotingConnectedDelegate(AddressOf RemoteControl_OnRemotingConnected)

			GUIGraphicsContext.OnBlackImageRendered -= New BlackImageRenderedHandler(AddressOf OnBlackImageRendered)
			GUIGraphicsContext.OnVideoReceived -= New VideoReceivedHandler(AddressOf OnVideoReceived)

			RemoveHandler Application.ApplicationExit, New EventHandler(AddressOf Application_ApplicationExit)

			g_Player.PlayBackStarted -= New g_Player.StartedHandler(AddressOf OnPlayBackStarted)
			g_Player.PlayBackStopped -= New g_Player.StoppedHandler(AddressOf OnPlayBackStopped)
			g_Player.AudioTracksReady -= New g_Player.AudioTracksReadyHandler(AddressOf OnAudioTracksReady)

			GUIWindowManager.Receivers -= New SendMessageHandler(AddressOf OnGlobalMessage)
		End Sub

		Public Overrides ReadOnly Property SupportsDelayedLoad() As Boolean
			Get
				Return False
			End Get
		End Property

		Public Overrides Sub OnAction(action__1 As Action)
			Select Case action__1.wID
				Case Action.ActionType.ACTION_RECORD
					' record current program on current channel
					' are we watching tv?                    
					ManualRecord(Navigator.Channel, GetID)
					Exit Select
				Case Action.ActionType.ACTION_PREV_CHANNEL
					OnPreviousChannel()
					Exit Select
				Case Action.ActionType.ACTION_PAGE_DOWN
					OnPreviousChannel()
					Exit Select
				Case Action.ActionType.ACTION_NEXT_CHANNEL
					OnNextChannel()
					Exit Select
				Case Action.ActionType.ACTION_PAGE_UP
					OnNextChannel()
					Exit Select
				Case Action.ActionType.ACTION_LAST_VIEWED_CHANNEL
					OnLastViewedChannel()
					Exit Select
				Case Action.ActionType.ACTION_PREVIOUS_MENU
					If True Then
						' goto home 
						' are we watching tv & doing timeshifting

						' No, then stop viewing... 
						'g_Player.Stop();
						GUIWindowManager.ShowPreviousWindow()
						Return
					End If
				Case Action.ActionType.ACTION_KEY_PRESSED
					If True Then
						If CChar(action__1.m_key.KeyChar) = "0"C Then
							OnLastViewedChannel()
						End If
					End If
					Exit Select
				Case Action.ActionType.ACTION_SHOW_GUI
					If True Then
						' If we are in tvhome and TV is currently off and no fullscreen TV then turn ON TV now!
						If Not g_Player.IsTimeShifting AndAlso Not g_Player.FullScreen Then
								'8=togglebutton
							OnClicked(8, btnTvOnOff, Action.ActionType.ACTION_MOUSE_CLICK)
						End If
						Exit Select
					End If
			End Select
			MyBase.OnAction(action__1)
		End Sub

		Protected Overrides Sub OnPageLoad()
			Log.Info("TVHome:OnPageLoad")

			If GUIWindowManager.GetWindow(GUIWindowManager.ActiveWindow).PreviousWindowId <> CInt(Window.WINDOW_TVFULLSCREEN) Then
				_playbackStopped = False
			End If

			btnActiveStreams.Label = GUILocalizeStrings.[Get](692)

			If Not Connected Then
				If Not _onPageLoadDone Then
					RemoteControl.Clear()
					GUIWindowManager.ActivateWindow(CInt(Window.WINDOW_SETTINGS_TVENGINE))
					Return
				Else
					UpdateStateOfRecButton()
					UpdateProgressPercentageBar()
					UpdateRecordingIndicator()
					Return
				End If
			End If

			Try
				Dim cards As Integer = RemoteControl.Instance.Cards
			Catch generatedExceptionName As Exception
				RemoteControl.Clear()
			End Try

			' stop the old recorder.
			' DatabaseManager.Instance.DefaultQueryStrategy = QueryStrategy.DataSourceOnly;
			Dim msgStopRecorder As New GUIMessage(GUIMessage.MessageType.GUI_MSG_RECORDER_STOP, 0, 0, 0, 0, 0, _
				Nothing)
			GUIWindowManager.SendMessage(msgStopRecorder)

			If Not _onPageLoadDone AndAlso m_navigator IsNot Nothing Then
				m_navigator.ReLoad()
			End If

			If m_navigator Is Nothing Then
					' Create the channel navigator (it will load groups and channels)
				m_navigator = New ChannelNavigator()
			End If

			MyBase.OnPageLoad()

			' set video window position
			If videoWindow IsNot Nothing Then
				GUIGraphicsContext.VideoWindow = New Rectangle(videoWindow.XPosition, videoWindow.YPosition, videoWindow.Width, videoWindow.Height)
			End If

			' start viewing tv... 
			GUIGraphicsContext.IsFullScreenVideo = False
			Dim channel As Channel = Navigator.Channel
			If channel Is Nothing OrElse channel.IsRadio Then
				If Navigator.CurrentGroup IsNot Nothing AndAlso Navigator.Groups.Count > 0 Then
					Navigator.SetCurrentGroup(Navigator.Groups(0).GroupName)
					GUIPropertyManager.SetProperty("#TV.Guide.Group", Navigator.Groups(0).GroupName)
				End If
				If Navigator.CurrentGroup IsNot Nothing Then
					If Navigator.CurrentGroup.ReferringGroupMap().Count > 0 Then
						Dim gm As GroupMap = DirectCast(Navigator.CurrentGroup.ReferringGroupMap()(0), GroupMap)
						channel = gm.ReferencedChannel()
					End If
				End If
			End If

			If channel IsNot Nothing Then
				Log.Info("tv home init:{0}", channel.DisplayName)
				If Not _suspended Then
					AutoTurnOnTv(channel)
				Else
					_resumeChannel = channel
				End If
				GUIPropertyManager.SetProperty("#TV.Guide.Group", Navigator.CurrentGroup.GroupName)
				Log.Info("tv home init:{0} done", channel.DisplayName)
			End If

			If Not _suspended Then
				AutoFullScreenTv()
			End If

			_onPageLoadDone = True
			_suspended = False

			UpdateGUIonPlaybackStateChange()
			UpdateCurrentChannel()
		End Sub

		Private Sub AutoTurnOnTv(channel As Channel)
			If _autoTurnOnTv AndAlso Not _playbackStopped AndAlso Not wasPrevWinTVplugin() Then
				If Not wasPrevWinTVplugin() Then
					_userChannelChanged = False
				End If
				ViewChannelAndCheck(channel)
			End If
		End Sub

		Private Sub AutoFullScreenTv()
			If _autoFullScreen Then
				' if using showlastactivemodule feature and last module is fullscreen while returning from powerstate, then do not set fullscreen here (since this is done by the resume last active module feature)
				' we depend on the onresume method, thats why tvplugin now impl. the IPluginReceiver interface.      
				If Not _suspended Then
					Dim isTvOrRec As Boolean = (g_Player.IsTV OrElse g_Player.IsTVRecording)
					If isTvOrRec Then
						Log.Debug("GUIGraphicsContext.IsFullScreenVideo {0}", GUIGraphicsContext.IsFullScreenVideo)
						Dim wasFullScreenTV As Boolean = (PreviousWindowId = CInt(Window.WINDOW_TVFULLSCREEN))

						If Not wasFullScreenTV Then
							If Not wasPrevWinTVplugin() Then
								Log.Debug("TVHome.AutoFullScreenTv(): setting autoFullScreen")
								Dim showlastActModFS As Boolean = (_showlastactivemodule AndAlso _showlastactivemoduleFullscreen AndAlso Not _suspended AndAlso _autoTurnOnTv)
								If Not showlastActModFS Then
									'if we are resuming from standby with tvhome, we want this in fullscreen, but we need a delay for it to work.
									Dim tvDelayThread__1 As New Thread(AddressOf TvDelayThread)
									tvDelayThread__1.Start()
								Else
									g_Player.ShowFullScreenWindow()
								End If
							Else
								g_Player.ShowFullScreenWindow()
							End If
						End If
					End If
				End If
			End If
		End Sub

		Protected Overrides Sub OnPageDestroy(newWindowId As Integer)
			' if we're switching to another plugin
					'and we're not playing which means we dont timeshift tv
					'g_Player.Stop();
			If Not GUIGraphicsContext.IsTvWindow(newWindowId) Then
			End If
			If Connected Then
				SaveSettings()
			End If
			MyBase.OnPageDestroy(newWindowId)
		End Sub

		Protected Overrides Sub OnClicked(controlId As Integer, control As GUIControl, actionType As Action.ActionType)
			Dim benchClock As Stopwatch = Nothing
			benchClock = Stopwatch.StartNew()

			RefreshConnectionState()

			If Not Connected Then
				UpdateStateOfRecButton()
				UpdateRecordingIndicator()
				UpdateGUIonPlaybackStateChange()
				ShowDlgAsynch()
				Return
			End If

			If control = btnActiveStreams Then
				OnActiveStreams()
			End If

			If control = btnActiveRecordings AndAlso btnActiveRecordings IsNot Nothing Then
				OnActiveRecordings()
			End If

			If control = btnTvOnOff Then
				If Card.IsTimeShifting AndAlso g_Player.IsTV AndAlso g_Player.Playing Then
					' tv off
					g_Player.[Stop]()
					Log.Warn("TVHome.OnClicked(): EndTvOff {0} ms", benchClock.ElapsedMilliseconds.ToString())
					benchClock.[Stop]()
					Return
				Else
					' tv on
					Log.Info("TVHome:turn tv on {0}", Navigator.CurrentChannel)

					' stop playing anything
					If g_Player.Playing Then
								'already playing tv...
						If g_Player.IsTV AndAlso Not g_Player.IsTVRecording Then
						Else
							Log.Warn("TVHome.OnClicked: Stop Called - {0} ms", benchClock.ElapsedMilliseconds.ToString())
							g_Player.[Stop](True)
						End If
					End If
				End If

				' turn tv on/off        
				If Navigator.Channel.IsTv Then
					ViewChannelAndCheck(Navigator.Channel)
				Else
					' current channel seems to be non-tv (radio ?), get latest known tv channel from xml config and use this instead
					Dim xmlreader As Settings = New MPSettings()
					Dim currentchannelName As String = xmlreader.GetValueAsString("mytv", "channel", [String].Empty)
					Dim currentChannel As Channel = Navigator.GetChannel(currentchannelName)
					ViewChannelAndCheck(currentChannel)
				End If

				UpdateStateOfRecButton()
				UpdateGUIonPlaybackStateChange()
				'UpdateProgressPercentageBar();
				benchClock.[Stop]()
				Log.Warn("TVHome.OnClicked(): Total Time - {0} ms", benchClock.ElapsedMilliseconds.ToString())
			End If

			If control = btnTeletext Then
				GUIWindowManager.ActivateWindow(CInt(Window.WINDOW_TELETEXT))
				Return
			End If

			If control = btnRecord Then
				OnRecord()
			End If
			If control = btnChannel Then
				OnSelectChannel()
			End If
			MyBase.OnClicked(controlId, control, actionType)
		End Sub

		Public Overrides Function OnMessage(message As GUIMessage) As Boolean
			Select Case message.Message
				Case GUIMessage.MessageType.PS_ONSTANDBY
					RemoteControl.Clear()
					Exit Select
				Case GUIMessage.MessageType.GUI_MSG_RESUME_TV
					If True Then
						' we only want to resume TV if previous window is NOT a tvplugin based one. (ex. tvguide.)
						If _autoTurnOnTv AndAlso Not wasPrevWinTVplugin() Then
							'restart viewing...  
							Log.Info("tv home msg resume tv:{0}", Navigator.CurrentChannel)
							ViewChannel(Navigator.Channel)
						End If
					End If
					Exit Select
			End Select
			Return MyBase.OnMessage(message)
		End Function

		Private Shared Sub ForceUpdates()
			_updateTimer = DateTime.Now.AddMilliseconds(-1 * (PROCESS_UPDATE_INTERVAL + 1))
			_updateProgressTimer = DateTime.Now.AddMilliseconds(-1 * (PROGRESS_PERCENTAGE_UPDATE_INTERVAL + 1))
		End Sub

		Public Overrides Sub Process()
			Dim ts As TimeSpan = DateTime.Now - _updateTimer
			If Not Connected OrElse _suspended OrElse ts.TotalMilliseconds < PROCESS_UPDATE_INTERVAL Then
				Return
			End If

			Try
				UpdateRecordingIndicator()
				UpdateStateOfRecButton()

				If Not Card.IsTimeShifting Then
					UpdateProgressPercentageBar()
					' mantis #2218 : TV guide information in TV home screen does not update when program changes if TV is not playing           
					Return
				End If

				' BAV, 02.03.08: a channel change should not be delayed by rendering.
				'                by moving thisthe 1 min delays in zapping should be fixed
				' Let the navigator zap channel if needed
				If Navigator.CheckChannelChange() Then
					UpdateGUIonPlaybackStateChange()
				End If

				If GUIGraphicsContext.InVmr9Render Then
					Return
				End If
				ShowCiMenu()
				UpdateCurrentChannel()
			Finally
				_updateTimer = DateTime.Now
			End Try
		End Sub


		Public Overrides ReadOnly Property IsTv() As Boolean
			Get
				Return True
			End Get
		End Property

		#End Region

		#Region "Public static methods"

		Public Shared Sub StartRecordingSchedule(channel As Channel, manual As Boolean)
			Dim layer As New TvBusinessLayer()
			Dim server As New TvServer()
			If manual Then
				' until manual stop
				Dim newSchedule As New Schedule(channel.IdChannel, GUILocalizeStrings.[Get](413) & " (" & Convert.ToString(channel.DisplayName) & ")", DateTime.Now, DateTime.Now.AddDays(1))
				newSchedule.PreRecordInterval = Int32.Parse(layer.GetSetting("preRecordInterval", "5").Value)
				newSchedule.PostRecordInterval = Int32.Parse(layer.GetSetting("postRecordInterval", "5").Value)
				newSchedule.RecommendedCard = Card.Id
				newSchedule.Persist()
				server.OnNewSchedule()
			Else
				' current program
				' lets find any canceled episodes that match this one we want to create, if found uncancel it.
				Dim existingParentSchedule As Schedule = Schedule.RetrieveSeries(channel.IdChannel, channel.CurrentProgram.Title, channel.CurrentProgram.StartTime, channel.CurrentProgram.EndTime)
				If existingParentSchedule IsNot Nothing Then
					For Each cancelSched As CanceledSchedule In existingParentSchedule.ReferringCanceledSchedule()
						If cancelSched.CancelDateTime = channel.CurrentProgram.StartTime Then
							existingParentSchedule.UnCancelSerie(channel.CurrentProgram.StartTime, channel.CurrentProgram.IdChannel)
							server.OnNewSchedule()
							Return
						End If
					Next
				End If

				' ok, no existing schedule found with matching canceled schedules found. proceeding to add the schedule normally
				Dim newSchedule As New Schedule(channel.IdChannel, channel.CurrentProgram.Title, channel.CurrentProgram.StartTime, channel.CurrentProgram.EndTime)
				newSchedule.PreRecordInterval = Int32.Parse(layer.GetSetting("preRecordInterval", "5").Value)
				newSchedule.PostRecordInterval = Int32.Parse(layer.GetSetting("postRecordInterval", "5").Value)
				newSchedule.RecommendedCard = Card.Id
				newSchedule.Persist()
				server.OnNewSchedule()
			End If
		End Sub

		Public Shared Function UseRTSP() As Boolean
			If Not settingsLoaded Then
				LoadSettings()
			End If
			Return _usertsp
		End Function

		Public Shared Function ShowChannelStateIcons() As Boolean
			Return _showChannelStateIcons
		End Function

		Public Shared Function RecordingPath() As String
			Return _recordingpath
		End Function

		Public Shared Function TimeshiftingPath() As String
			Return _timeshiftingpath
		End Function

		Public Shared Function DoingChannelChange() As Boolean
			Return _doingChannelChange
		End Function

		Private Shared Sub StopPlayerMainThread()
			'call g_player.stop only on main thread.
			If GUIGraphicsContext.form.InvokeRequired Then
				Dim d As New StopPlayerMainThreadDelegate(AddressOf StopPlayerMainThread)
				GUIGraphicsContext.form.Invoke(d)
				Return
			End If

			g_Player.[Stop]()
		End Sub

		Private Delegate Sub ShowDlgAsynchDelegate()

		Private Delegate Sub ShowDlgMessageAsynchDelegate(Message As [String])

		Private Shared Sub ShowDlgAsynch()
			'show dialogue only on main thread.
			If GUIGraphicsContext.form.InvokeRequired Then
				Dim d As New ShowDlgAsynchDelegate(AddressOf ShowDlgAsynch)
				GUIGraphicsContext.form.Invoke(d)
				Return
			End If

			_ServerNotConnectedHandled = True
			Dim pDlgOK As GUIDialogOK = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_OK)), GUIDialogOK)

			pDlgOK.Reset()
			pDlgOK.SetHeading(257)
			'error
			If Navigator IsNot Nothing AndAlso Navigator.CurrentChannel IsNot Nothing AndAlso g_Player.IsTV Then
				pDlgOK.SetLine(1, Navigator.CurrentChannel)
			Else
				pDlgOK.SetLine(1, "")
			End If
			pDlgOK.SetLine(2, GUILocalizeStrings.[Get](1510))
			'Connection to TV server lost
			pDlgOK.DoModal(GUIWindowManager.ActiveWindow)
		End Sub

		Public Shared Sub ShowDlgThread()
			Dim guiWindow As GUIWindow = GUIWindowManager.GetWindow(GUIWindowManager.ActiveWindow)

			Dim count As Integer = 0

			While count < 50
				If guiWindow.WindowLoaded Then
					Exit While
				Else
					Thread.Sleep(100)
				End If
				count += 1
			End While

			If guiWindow.WindowLoaded Then
				ShowDlgAsynch()
			End If
		End Sub

		Private Shared Sub RefreshConnectionState()
			Dim iController As IController = RemoteControl.Instance
			'calling instance will make sure the state is refreshed.
		End Sub

		Public Shared Function HandleServerNotConnected() As Boolean
			' _doingHandleServerNotConnected is used to avoid multiple calls to this method.
			' the result could be that the dialogue is not shown.

			If _ServerNotConnectedHandled Then
					'still not connected
				Return True
			End If

			If _doingHandleServerNotConnected Then
					'we assume we are still not connected
				Return False
			End If

			_doingHandleServerNotConnected = True

			Try
				If Not Connected Then
					'Card.User.Name = new User().Name;
					If g_Player.Playing Then
						If g_Player.IsTimeShifting Then
							' live TV or radio must be stopped
							TVHome.StopPlayerMainThread()
						Else
							' playing something else so do not disturb
							Return True
						End If
					End If

					If g_Player.FullScreen Then
						Dim initMsgTV As GUIMessage = Nothing
						initMsgTV = New GUIMessage(GUIMessage.MessageType.GUI_MSG_WINDOW_INIT, CInt(Window.WINDOW_TV), 0, 0, 0, 0, _
							Nothing)
						GUIWindowManager.SendThreadMessage(initMsgTV)
						Return True
					End If
					Dim showDlgThread__1 As New Thread(AddressOf ShowDlgThread)
					showDlgThread__1.IsBackground = True
					' show the dialog asynch.
					' this fixes a hang situation that would happen when resuming TV with showlastactivemodule
					showDlgThread__1.Start()
					Return True
				Else
					Dim gentleConnected As Boolean = WaitForGentleConnection()

					If Not gentleConnected Then
						Return True
					End If
				End If
			Catch e As Exception
				'we assume that server is disconnected.
				Log.[Error]("TVHome.HandleServerNotConnected caused an error {0},{1}", e.Message, e.StackTrace)
				Return True
			Finally
				_doingHandleServerNotConnected = False
			End Try
			Return False
		End Function

		Public Shared Function WaitForGentleConnection() As Boolean
			' lets try one more time - seems like the gentle framework is not properly initialized when coming out of standby/hibernation.                    
			' lets wait 10 secs before giving up.
			Dim success As Boolean = False

			Dim timer As Stopwatch = Stopwatch.StartNew()
			While Not success AndAlso timer.ElapsedMilliseconds < 10000
				'10sec max
				Try
					Dim cards As IList(Of Card) = TvDatabase.Card.ListAll()
					success = True
				Catch generatedExceptionName As Exception
					success = False
					Log.Debug("TVHome: waiting for gentle.net DB connection {0} msec", timer.ElapsedMilliseconds)
					Thread.Sleep(100)
				End Try
			End While

			If Not success Then
				RemoteControl.Clear()
					'GUIWaitCursor.Hide();          
				GUIWindowManager.ActivateWindow(CInt(Window.WINDOW_SETTINGS_TVENGINE))
			End If

			Return success
		End Function

		Public Shared WriteOnly Property PreferredLanguages() As List(Of String)
			Set
				_preferredLanguages = value
			End Set
		End Property

		Public Shared WriteOnly Property PreferAC3() As Boolean
			Set
				_preferAC3 = value
			End Set
		End Property

		Public Shared WriteOnly Property PreferAudioTypeOverLang() As Boolean
			Set
				_preferAudioTypeOverLang = value
			End Set
		End Property

		Public Shared Property UserChannelChanged() As Boolean
			Get
				Return _userChannelChanged
			End Get
			Set
				_userChannelChanged = value
			End Set
		End Property

		Public Shared ReadOnly Property Util() As TVUtil
			Get
				If _util Is Nothing Then
					_util = New TVUtil()
				End If
				Return _util
			End Get
		End Property

		Public Shared ReadOnly Property TvServer() As TvServer
			Get
				If _server Is Nothing Then
					_server = New TvServer()
				End If
				Return _server
			End Get
		End Property

		Public Shared ReadOnly Property IsAnyCardRecording() As Boolean
			Get
				Return _isAnyCardRecording
			End Get
		End Property

		Public Shared Property Connected() As Boolean
			Get
				Return _connected
			End Get
			Set
				_connected = value
			End Set
		End Property

		Public Shared Property Card() As VirtualCard
			Get
				If _card Is Nothing Then
					Dim user As New User()
					_card = TvServer.CardByIndex(user, 0)
				End If
				Return _card
			End Get
			Set
				If _card IsNot Nothing Then
					Dim [stop] As Boolean = True
					If value IsNot Nothing Then
						If value.Id = _card.Id OrElse value.Id = -1 Then
							[stop] = False
						End If
					End If
					If [stop] Then
						_card.User.Name = New User().Name
						_card.StopTimeShifting()
					End If
					_card = value
				End If
			End Set
		End Property

		#End Region

		#Region "Serialisation"

		Private Shared Sub LoadSettings()
			If settingsLoaded Then
				Return
			End If

			Using xmlreader As Settings = New MPSettings()
				m_navigator.LoadSettings(xmlreader)
				_autoTurnOnTv = xmlreader.GetValueAsBool("mytv", "autoturnontv", False)
				_showlastactivemodule = xmlreader.GetValueAsBool("general", "showlastactivemodule", False)
				_showlastactivemoduleFullscreen = xmlreader.GetValueAsBool("general", "lastactivemodulefullscreen", False)

				_waitonresume = xmlreader.GetValueAsInt("tvservice", "waitonresume", 0)

				Dim strValue As String = xmlreader.GetValueAsString("mytv", "defaultar", "Normal")
				GUIGraphicsContext.ARType = Utils.GetAspectRatio(strValue)

				Dim preferredLanguages As String = xmlreader.GetValueAsString("tvservice", "preferredaudiolanguages", "")
				_preferredLanguages = New List(Of String)()
				Log.Debug("TVHome.LoadSettings(): Preferred Audio Languages: " & preferredLanguages)

				Dim st As New StringTokenizer(preferredLanguages, ";")
				While st.HasMore
					Dim lang As String = st.NextToken()
					If lang.Length <> 3 Then
						Log.Warn("Language {0} is not in the correct format!", lang)
					Else
						_preferredLanguages.Add(lang)
						Log.Info("Prefered language {0} is {1}", _preferredLanguages.Count, lang)
					End If
				End While
				_usertsp = xmlreader.GetValueAsBool("tvservice", "usertsp", Not Network.IsSingleSeat())
				_recordingpath = xmlreader.GetValueAsString("tvservice", "recordingpath", "")
				_timeshiftingpath = xmlreader.GetValueAsString("tvservice", "timeshiftingpath", "")

				_preferAC3 = xmlreader.GetValueAsBool("tvservice", "preferac3", False)
				_preferAudioTypeOverLang = xmlreader.GetValueAsBool("tvservice", "preferAudioTypeOverLang", True)
				_autoFullScreen = xmlreader.GetValueAsBool("mytv", "autofullscreen", False)
				_showChannelStateIcons = xmlreader.GetValueAsBool("mytv", "showChannelStateIcons", True)

				_notifyTVTimeout = xmlreader.GetValueAsInt("mytv", "notifyTVTimeout", 15)
				_playNotifyBeep = xmlreader.GetValueAsBool("mytv", "notifybeep", True)
				_preNotifyConfig = xmlreader.GetValueAsInt("mytv", "notifyTVBefore", 300)
			End Using
			settingsLoaded = True
		End Sub

		Private Shared Sub SaveSettings()
			If m_navigator IsNot Nothing Then
				Using xmlwriter As Settings = New MPSettings()
					m_navigator.SaveSettings(xmlwriter)
				End Using
			End If
		End Sub

		#End Region

		#Region "Private methods"

		Private Shared Sub SetRemoteControlHostName()
			Dim hostName As String

			Using xmlreader As Settings = New MPSettings()
				hostName = xmlreader.GetValueAsString("tvservice", "hostname", "")
				If String.IsNullOrEmpty(hostName) OrElse hostName = "localhost" Then
					Try
						hostName = Dns.GetHostName()

						Log.Info("TVHome: No valid hostname specified in mediaportal.xml!")
						xmlreader.SetValue("tvservice", "hostname", hostName)
						hostName = "localhost"
						Settings.SaveCache()
					Catch ex As Exception
						Log.Info("TVHome: Error resolving hostname - {0}", ex.Message)
						Return
					End Try
				End If
			End Using
			RemoteControl.HostName = hostName

			Log.Info("Remote control:master server :{0}", RemoteControl.HostName)
		End Sub

		Private Shared Sub HandleWakeUpTvServer()
			Dim isWakeOnLanEnabled As Boolean
			Dim isAutoMacAddressEnabled As Boolean
			Dim intTimeOut As Integer
			Dim macAddress As [String]
			Dim hwAddress As Byte()

			Using xmlreader As Settings = New MPSettings()
				isWakeOnLanEnabled = xmlreader.GetValueAsBool("tvservice", "isWakeOnLanEnabled", False)
				isAutoMacAddressEnabled = xmlreader.GetValueAsBool("tvservice", "isAutoMacAddressEnabled", False)
				intTimeOut = xmlreader.GetValueAsInt("tvservice", "WOLTimeOut", 10)
			End Using

			If isWakeOnLanEnabled Then
				If Not Network.IsSingleSeat() Then
					Dim wakeOnLanManager As New WakeOnLanManager()

					If isAutoMacAddressEnabled Then
						Dim ipAddress__1 As IPAddress = Nothing

						' Check if we already have a valid IP address stored in RemoteControl.HostName,
						' otherwise try to resolve the IP address
						If Not IPAddress.TryParse(RemoteControl.HostName, ipAddress__1) Then
							' Get IP address of the TV server
							Try
								Dim ips As IPAddress()

								ips = Dns.GetHostAddresses(RemoteControl.HostName)

								Log.Debug("TVHome: WOL - GetHostAddresses({0}) returns:", RemoteControl.HostName)

								For Each ip As IPAddress In ips
									Log.Debug("    {0}", ip)
								Next

								' Use first valid IP address
								ipAddress__1 = ips(0)
							Catch ex As Exception
								Log.[Error]("TVHome: WOL - Failed GetHostAddress - {0}", ex.Message)
							End Try
						End If

						' Check for valid IP address
						If ipAddress__1 IsNot Nothing Then
							' Update the MAC address if possible
							hwAddress = wakeOnLanManager.GetHardwareAddress(ipAddress__1)

							If wakeOnLanManager.IsValidEthernetAddress(hwAddress) Then
								Log.Debug("TVHome: WOL - Valid auto MAC address: {0:x}:{1:x}:{2:x}:{3:x}:{4:x}:{5:x}", hwAddress(0), hwAddress(1), hwAddress(2), hwAddress(3), hwAddress(4), _
									hwAddress(5))

								' Store MAC address
								macAddress = BitConverter.ToString(hwAddress).Replace("-", ":")

								Log.Debug("TVHome: WOL - Store MAC address: {0}", macAddress)

								Using xmlwriter As MediaPortal.Profile.Settings = New MediaPortal.Profile.MPSettings()
									xmlwriter.SetValue("tvservice", "macAddress", macAddress)
								End Using
							End If
						End If
					End If

					' Use stored MAC address
					Using xmlreader As Settings = New MPSettings()
						macAddress = xmlreader.GetValueAsString("tvservice", "macAddress", Nothing)
					End Using

					Log.Debug("TVHome: WOL - Use stored MAC address: {0}", macAddress)

					Try
						hwAddress = wakeOnLanManager.GetHwAddrBytes(macAddress)

						' Finally, start up the TV server
						Log.Info("TVHome: WOL - Start the TV server")

						If wakeOnLanManager.WakeupSystem(hwAddress, RemoteControl.HostName, intTimeOut) Then
							Log.Info("TVHome: WOL - The TV server started successfully!")
						Else
							Log.[Error]("TVHome: WOL - Failed to start the TV server")
						End If
					Catch ex As Exception
						Log.[Error]("TVHome: WOL - Failed to start the TV server - {0}", ex.Message)
					End Try
				End If
			End If
		End Sub

		'''// <summary>
		'''// Register the remoting service and attaching ciMenuHandler for server events
		'''// </summary>
		'public static void RegisterCiMenu(int newCardId)
		'{
		'  if (ciMenuHandler == null)
		'  {
		'    Log.Debug("CiMenu: PrepareCiMenu");
		'    ciMenuHandler = new CiMenuHandler();
		'    // opens remoting and attach local eventhandler to server event, call only once
		'    RemoteControl.RegisterCiMenuCallbacks(ciMenuHandler);
		'  }
		'  // Check if card supports CI menu
		'  if (newCardId != -1 && RemoteControl.Instance.CiMenuSupported(newCardId))
		'  {
		'    // Enable CI menu handling in card
		'    RemoteControl.Instance.SetCiMenuHandler(newCardId, null);
		'    Log.Debug("TvPlugin: CiMenuHandler attached to new card {0}", newCardId);
		'  }
		'}

		Private Shared Sub RemoteControl_OnRemotingConnected()
			If Not Connected Then
				Log.Info("TVHome: OnRemotingConnected, recovered from a disconnection")
			End If
			Connected = True
			_ServerNotConnectedHandled = False
			If _recoverTV Then
				_recoverTV = False
				Dim initMsg As GUIMessage = Nothing
				initMsg = New GUIMessage(GUIMessage.MessageType.GUI_MSG_WINDOW_INIT, CInt(Window.WINDOW_TV_OVERLAY), 0, 0, 0, 0, _
					Nothing)
				GUIWindowManager.SendThreadMessage(initMsg)
			End If
		End Sub

		Private Shared Sub RemoteControl_OnRemotingDisconnected()
			If Connected Then
				Log.Info("TVHome: OnRemotingDisconnected")
			End If
			Connected = False
			HandleServerNotConnected()
		End Sub

		Private Sub Application_ApplicationExit(sender As Object, e As EventArgs)
			Try
				If Card.IsTimeShifting Then
					Card.User.Name = New User().Name
					Card.StopTimeShifting()
				End If
				_notifyManager.[Stop]()
				stopHeartBeatThread()
			Catch generatedExceptionName As Exception
			End Try
		End Sub

		Private Sub HeartBeatTransmitter()
			Dim countToHBLoop As Integer = 5

			While True
				' 1 second loop
				If Connected Then
					_isAnyCardRecording = TvServer.IsAnyCardRecording()
				End If

				' HeartBeat loop (5 seconds)
				If countToHBLoop >= 5 Then
					countToHBLoop = 0
					If Not Connected Then
						' is this needed to update connection status
						RefreshConnectionState()
					End If
					If Connected AndAlso Not _suspended Then
						Dim isTS As Boolean = (Card IsNot Nothing AndAlso Card.IsTimeShifting)
						If Connected AndAlso isTS Then
							' send heartbeat to tv server each 5 sec.
							' this way we signal to the server that we are alive thus avoid being kicked.
							' Log.Debug("TVHome: sending HeartBeat signal to server.");

							' when debugging we want to disable heartbeats
							#If Not DEBUG Then
							Try
								RemoteControl.Instance.HeartBeat(Card.User)
							Catch e As Exception
								Log.[Error]("TVHome: failed sending HeartBeat signal to server. ({0})", e.Message)
								#End If
                            'End Try
						ElseIf Connected AndAlso Not isTS AndAlso Not _playbackStopped AndAlso _onPageLoadDone AndAlso (Not g_Player.IsTVRecording AndAlso (g_Player.IsTV OrElse g_Player.IsRadio)) Then
							' check the possible reason why timeshifting has suddenly stopped
							' maybe the server kicked the client b/c a recording on another transponder was due.

							Dim result As TvStoppedReason = Card.GetTimeshiftStoppedReason
							If result <> TvStoppedReason.UnknownReason Then
								Log.Debug("TVHome: Timeshifting seems to have stopped - TvStoppedReason:{0}", result)
								Dim errMsg As String = ""

								Select Case result
									Case TvStoppedReason.HeartBeatTimeOut
										errMsg = GUILocalizeStrings.[Get](1515)
										Exit Select
									Case TvStoppedReason.KickedByAdmin
										errMsg = GUILocalizeStrings.[Get](1514)
										Exit Select
									Case TvStoppedReason.RecordingStarted
										errMsg = GUILocalizeStrings.[Get](1513)
										Exit Select
									Case TvStoppedReason.OwnerChangedTS
										errMsg = GUILocalizeStrings.[Get](1517)
										Exit Select
									Case Else
										errMsg = GUILocalizeStrings.[Get](1516)
										Exit Select
								End Select
								NotifyUser(errMsg)
							End If
						End If
					End If
				End If
				Thread.Sleep(HEARTBEAT_INTERVAL * 1000)
				'sleep for 1 sec. before sending heartbeat again
				countToHBLoop += 1
			End While
		End Sub

		''' <summary>
		''' Notify the user about the reason of stopped live tv. 
		''' Ensures that the dialog is run in main thread.
		''' </summary>
		''' <param name="errMsg">The error messages</param>
		Private Shared Sub NotifyUser(errMsg As String)
			' show dialogue only on main thread.
			If GUIGraphicsContext.form.InvokeRequired Then
				Dim d As New ShowDlgMessageAsynchDelegate(AddressOf NotifyUser)
				GUIGraphicsContext.form.Invoke(d, errMsg)
				Return
			End If

			Dim pDlgOK As GUIDialogOK = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_OK)), GUIDialogOK)

			If pDlgOK IsNot Nothing Then
				If GUIWindowManager.ActiveWindow = CInt(CInt(Window.WINDOW_TVFULLSCREEN)) Then
					GUIWindowManager.ActivateWindow(CInt(Window.WINDOW_TV), True)
				End If

				pDlgOK.SetHeading(GUILocalizeStrings.[Get](605) & " - " & Convert.ToString(Navigator.CurrentChannel))
				'my tv
				errMsg = errMsg.Replace("\r", vbCr)
				Dim lines As String() = errMsg.Split(ControlChars.Cr)

				For i As Integer = 0 To lines.Length - 1
					Dim line As String = lines(i)
					pDlgOK.SetLine(1 + i, line)
				Next
				pDlgOK.DoModal(GUIWindowManager.ActiveWindowEx)
			End If
			Dim keyAction As New Action(Action.ActionType.ACTION_STOP, 0, 0)
			GUIGraphicsContext.OnAction(keyAction)
			_playbackStopped = True
		End Sub

		Private Sub startHeartBeatThread()
			' setup heartbeat transmitter thread.						
			' thread already running, then leave it.
			If heartBeatTransmitterThread IsNot Nothing Then
				If heartBeatTransmitterThread.IsAlive Then
					Return
				End If
			End If
			Log.Debug("TVHome: HeartBeat Transmitter started.")
			heartBeatTransmitterThread = New Thread(AddressOf HeartBeatTransmitter)
			heartBeatTransmitterThread.IsBackground = True
			heartBeatTransmitterThread.Name = "TvClient-TvHome: HeartBeat transmitter thread"
			heartBeatTransmitterThread.Start()
		End Sub

		Private Sub stopHeartBeatThread()
			If heartBeatTransmitterThread IsNot Nothing Then
				If heartBeatTransmitterThread.IsAlive Then
					Log.Debug("TVHome: HeartBeat Transmitter stopped.")
					heartBeatTransmitterThread.Abort()
				End If
			End If
		End Sub

		#End Region

		Public Shared Sub OnSelectGroup()
			Dim dlg As GUIDialogMenu = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_MENU)), GUIDialogMenu)
			If dlg Is Nothing Then
				Return
			End If
			dlg.Reset()
			dlg.SetHeading(971)
			' group
			Dim selected As Integer = 0

			For i As Integer = 0 To Navigator.Groups.Count - 1
				dlg.Add(Navigator.Groups(i).GroupName)
				If Navigator.Groups(i).GroupName = Navigator.CurrentGroup.GroupName Then
					selected = i
				End If
			Next

			dlg.SelectedLabel = selected
			dlg.DoModal(GUIWindowManager.ActiveWindow)
			If dlg.SelectedLabel < 0 Then
				Return
			End If

			Navigator.SetCurrentGroup(dlg.SelectedLabelText)
			GUIPropertyManager.SetProperty("#TV.Guide.Group", dlg.SelectedLabelText)
		End Sub

		Private Sub OnSelectChannel()
			Dim benchClock As Stopwatch = Nothing
			benchClock = Stopwatch.StartNew()
			Dim miniGuide As TvMiniGuide = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_MINI_GUIDE)), TvMiniGuide)
			miniGuide.AutoZap = False
			miniGuide.SelectedChannel = Navigator.Channel
			miniGuide.DoModal(GetID)

			'Only change the channel if the channel selectd is actually different. 
			'Without this, a ChannelChange might occur even when MiniGuide is canceled. 
			If Not miniGuide.Canceled Then
				ViewChannelAndCheck(miniGuide.SelectedChannel)
				UpdateGUIonPlaybackStateChange()
			End If

			benchClock.[Stop]()
			Log.Debug("TVHome.OnSelecChannel(): Total Time {0} ms", benchClock.ElapsedMilliseconds.ToString())
		End Sub

		Private Sub TvDelayThread()
			'we have to use a small delay before calling tvfullscreen.                                    
			Thread.Sleep(200)

			' wait for timeshifting to complete
			Dim waits As Integer = 0
			While _playbackStopped AndAlso waits < 100
				'Log.Debug("TVHome.OnPageLoad(): waiting for timeshifting to start");
				Thread.Sleep(100)
				waits += 1
			End While

			If Not _playbackStopped Then
				g_Player.ShowFullScreenWindow()
			End If
		End Sub

		Private Sub OnSuspend()
			Log.Debug("TVHome.OnSuspend()")

			RemoteControl.OnRemotingDisconnected -= New RemoteControl.RemotingDisconnectedDelegate(AddressOf RemoteControl_OnRemotingDisconnected)
			RemoteControl.OnRemotingConnected -= New RemoteControl.RemotingConnectedDelegate(AddressOf RemoteControl_OnRemotingConnected)

			Try
				If Card.IsTimeShifting Then
					Card.User.Name = New User().Name
					Card.StopTimeShifting()
				End If
				_notifyManager.[Stop]()
				stopHeartBeatThread()
				'Connected = false;
				_ServerNotConnectedHandled = False
			Catch generatedExceptionName As Exception
			Finally
				_ServerNotConnectedHandled = False
				_suspended = True
			End Try
		End Sub

		Private Sub OnResume()
			Log.Debug("TVHome.OnResume()")
			Try
				Connected = False
				RemoteControl.OnRemotingDisconnected += New RemoteControl.RemotingDisconnectedDelegate(AddressOf RemoteControl_OnRemotingDisconnected)
				RemoteControl.OnRemotingConnected += New RemoteControl.RemotingConnectedDelegate(AddressOf RemoteControl_OnRemotingConnected)
				HandleWakeUpTvServer()
				startHeartBeatThread()
				_notifyManager.Start()
				If _resumeChannel IsNot Nothing Then
					Log.Debug("TVHome.OnResume() - automatically turning on TV: {0}", _resumeChannel.DisplayName)
					AutoTurnOnTv(_resumeChannel)
					AutoFullScreenTv()
					_resumeChannel = Nothing
				End If
			Finally
				_suspended = False
			End Try
		End Sub

		Public Sub Start()
			Log.Debug("TVHome.Start()")
		End Sub

		Public Sub [Stop]()
			Log.Debug("TVHome.Stop()")
		End Sub

		Public Function WndProc(ByRef msg As Message) As Boolean
			If msg.Msg = WM_POWERBROADCAST Then
				Select Case msg.WParam.ToInt32()
					Case PBT_APMSTANDBY
						Log.Info("TVHome.WndProc(): Windows is going to standby")
						OnSuspend()
						Exit Select
					Case PBT_APMSUSPEND
						Log.Info("TVHome.WndProc(): Windows is suspending")
						OnSuspend()
						Exit Select
					Case PBT_APMQUERYSUSPEND, PBT_APMQUERYSTANDBY
						Log.Info("TVHome.WndProc(): Windows is going into powerstate (hibernation/standby)")

						Exit Select
					Case PBT_APMRESUMESUSPEND
						Log.Info("TVHome.WndProc(): Windows has resumed from hibernate mode")
						OnResume()
						Exit Select
					Case PBT_APMRESUMESTANDBY
						Log.Info("TVHome.WndProc(): Windows has resumed from standby mode")
						OnResume()
						Exit Select
				End Select
			End If
			Return False
			' false = all other processes will handle the msg
		End Function

		Private Shared Function wasPrevWinTVplugin() As Boolean
			Dim result As Boolean = False

			Dim act As Integer = GUIWindowManager.ActiveWindow
			Dim prev As Integer = GUIWindowManager.GetWindow(act).PreviousWindowId

			'plz add any newly added ID's to this list.


			result = (prev = CInt(Window.WINDOW_TV_CROP_SETTINGS) OrElse prev = CInt(Window.WINDOW_SETTINGS_SORT_CHANNELS) OrElse prev = CInt(Window.WINDOW_SETTINGS_TV_EPG) OrElse prev = CInt(Window.WINDOW_TVFULLSCREEN) OrElse prev = CInt(Window.WINDOW_TVGUIDE) OrElse prev = CInt(Window.WINDOW_MINI_GUIDE) OrElse prev = CInt(Window.WINDOW_TV_SEARCH) OrElse prev = CInt(Window.WINDOW_TV_SEARCHTYPE) OrElse prev = CInt(Window.WINDOW_TV_SCHEDULER_PRIORITIES) OrElse prev = CInt(Window.WINDOW_TV_PROGRAM_INFO) OrElse prev = CInt(Window.WINDOW_RECORDEDTV) OrElse prev = CInt(Window.WINDOW_TV_RECORDED_INFO) OrElse prev = CInt(Window.WINDOW_SETTINGS_RECORDINGS) OrElse prev = CInt(Window.WINDOW_SCHEDULER) OrElse prev = CInt(Window.WINDOW_SEARCHTV) OrElse prev = CInt(Window.WINDOW_TV_TUNING_DETAILS) OrElse prev = CInt(Window.WINDOW_TV))
			If Not result AndAlso prev = CInt(Window.WINDOW_FULLSCREEN_VIDEO) AndAlso g_Player.IsTVRecording Then
				result = True
			End If
			Return result
		End Function

		Public Shared Sub OnGlobalMessage(message As GUIMessage)
			Select Case message.Message
				Case GUIMessage.MessageType.GUI_MSG_STOP_SERVER_TIMESHIFTING
					If True Then
						Dim user As New User()
						If user.Name = Card.User.Name Then
							Card.StopTimeShifting()
						End If
						

						Exit Select
					End If
				Case GUIMessage.MessageType.GUI_MSG_NOTIFY_REC
					Dim heading As String = message.Label
					Dim text As String = message.Label2
					Dim ch As Channel = TryCast(message.[Object], Channel)
					'Log.Debug("Received rec notify message: {0}, {1}, {2}", heading, text, (ch != null).ToString()); //remove later
					Dim logo As String = String.Empty
					If ch IsNot Nothing AndAlso ch.IsTv Then
						logo = Utils.GetCoverArt(Thumbs.TVChannel, ch.DisplayName)
					ElseIf ch IsNot Nothing AndAlso ch.IsRadio Then
						logo = Utils.GetCoverArt(Thumbs.Radio, ch.DisplayName)
					End If

					Dim pDlgNotify As GUIDialogNotify = DirectCast(GUIWindowManager.GetWindow(CInt(GUIWindow.Window.WINDOW_DIALOG_NOTIFY)), GUIDialogNotify)
					If pDlgNotify IsNot Nothing Then
						pDlgNotify.Reset()
						pDlgNotify.ClearAll()
						pDlgNotify.SetHeading(heading)
						If Not String.IsNullOrEmpty(text) Then
							pDlgNotify.SetText(text)
						End If
						pDlgNotify.SetImage(logo)
						pDlgNotify.TimeOut = 5

						pDlgNotify.DoModal(GUIWindowManager.ActiveWindow)
					End If
					Exit Select
				Case GUIMessage.MessageType.GUI_MSG_NOTIFY_TV_PROGRAM
					If True Then
						Dim tvNotifyDlg As TVNotifyYesNoDialog = DirectCast(GUIWindowManager.GetWindow(CInt(GUIWindow.Window.WINDOW_DIALOG_TVNOTIFYYESNO)), TVNotifyYesNoDialog)

						Dim notify As TVProgramDescription = TryCast(message.[Object], TVProgramDescription)
						If tvNotifyDlg Is Nothing OrElse notify Is Nothing Then
							Return
						End If
						Dim minUntilStart As Integer = _preNotifyConfig \ 60
						If notify.StartTime > DateTime.Now Then
							If minUntilStart > 1 Then
								tvNotifyDlg.SetHeading([String].Format(GUILocalizeStrings.[Get](1018), minUntilStart))
							Else
									' Program is about to begin
								tvNotifyDlg.SetHeading(1019)
							End If
						Else
							tvNotifyDlg.SetHeading([String].Format(GUILocalizeStrings.[Get](1206), (DateTime.Now - notify.StartTime).Minutes.ToString()))
						End If
						tvNotifyDlg.SetLine(1, notify.Title)
						tvNotifyDlg.SetLine(2, notify.Description)
						tvNotifyDlg.SetLine(4, [String].Format(GUILocalizeStrings.[Get](1207), notify.Channel.DisplayName))
						Dim c As Channel = notify.Channel
						Dim strLogo As String = String.Empty
						If c.IsTv Then
							strLogo = MediaPortal.Util.Utils.GetCoverArt(Thumbs.TVChannel, c.DisplayName)
						ElseIf c.IsRadio Then
							strLogo = MediaPortal.Util.Utils.GetCoverArt(Thumbs.Radio, c.DisplayName)
						End If

						tvNotifyDlg.SetImage(strLogo)
						tvNotifyDlg.TimeOut = _notifyTVTimeout
						If _playNotifyBeep Then
							MediaPortal.Util.Utils.PlaySound("notify.wav", False, True)
						End If
						tvNotifyDlg.SetDefaultToYes(False)
						tvNotifyDlg.DoModal(GUIWindowManager.ActiveWindow)

						If tvNotifyDlg.IsConfirmed Then
							Try
								MediaPortal.Player.g_Player.[Stop]()

								If c.IsTv Then
									MediaPortal.GUI.Library.GUIWindowManager.ActivateWindow(CInt(MediaPortal.GUI.Library.GUIWindow.Window.WINDOW_TV))
									TVHome.ViewChannelAndCheck(c)
									If TVHome.Card.IsTimeShifting AndAlso TVHome.Card.IdChannel = c.IdChannel Then
										g_Player.ShowFullScreenWindow()
									End If
								ElseIf c.IsRadio Then
									MediaPortal.GUI.Library.GUIWindowManager.ActivateWindow(CInt(MediaPortal.GUI.Library.GUIWindow.Window.WINDOW_RADIO))
									Radio.CurrentChannel = c
									Radio.Play()
								End If
							Catch e As Exception
								Log.[Error]("TVHome: TVNotification: Error on starting channel {0} after notification: {1} {2} {3}", notify.Channel.DisplayName, e.Message, e.Source, e.StackTrace)

							End Try
						End If
						Exit Select
					End If
			End Select
		End Sub

		Private Sub OnAudioTracksReady()
			Log.Debug("TVHome.OnAudioTracksReady()")

			Dim dualMonoMode As eAudioDualMonoMode = eAudioDualMonoMode.UNSUPPORTED
			Dim prefLangIdx As Integer = GetPreferedAudioStreamIndex(dualMonoMode)
			g_Player.CurrentAudioStream = prefLangIdx

			If dualMonoMode <> eAudioDualMonoMode.UNSUPPORTED Then
				g_Player.SetAudioDualMonoMode(dualMonoMode)
			ElseIf g_Player.GetAudioDualMonoMode() <> eAudioDualMonoMode.UNSUPPORTED Then
				g_Player.SetAudioDualMonoMode(eAudioDualMonoMode.STEREO)
			End If
		End Sub

		Private Sub OnPlayBackStarted(type As g_Player.MediaType, filename As String)
			' when we are watching TV and suddenly decides to watch a audio/video etc., we want to make sure that the TV is stopped on server.
			Dim currentWindow As GUIWindow = GUIWindowManager.GetWindow(GUIWindowManager.ActiveWindow)

			If type = g_Player.MediaType.Radio OrElse type = g_Player.MediaType.TV Then
				UpdateGUIonPlaybackStateChange(True)
			End If

			If currentWindow.IsTv AndAlso type = g_Player.MediaType.TV Then
				Return
			End If
			If GUIWindowManager.ActiveWindow = CInt(Window.WINDOW_RADIO) OrElse GUIWindowManager.ActiveWindow = CInt(Window.WINDOW_RADIO_GUIDE) Then
				Return
			End If

			'gemx: fix for 0001181: Videoplayback does not work if tvservice.exe is not running 
			If Connected Then
				Card.StopTimeShifting()
			End If
		End Sub

		Private Sub OnPlayBackStopped(type As g_Player.MediaType, stoptime As Integer, filename As String)
			If type <> g_Player.MediaType.TV AndAlso type <> g_Player.MediaType.Radio Then
				Return
			End If

			StopPlayback()
			UpdateGUIonPlaybackStateChange(False)
		End Sub

		Private Shared Sub StopPlayback()

			'gemx: fix for 0001181: Videoplayback does not work if tvservice.exe is not running 
			If Not Connected Then
				_recoverTV = True
				Return
			End If
			If Card.IsTimeShifting = False Then
				Return
			End If

			'tv off
			Log.Info("TVHome:turn tv off")
			SaveSettings()
			Card.User.Name = New User().Name
			Card.StopTimeShifting()

			_recoverTV = False
			_playbackStopped = True
		End Sub

		Public Shared Function ManualRecord(channel As Channel, dialogId As Integer) As Boolean
			If GUIWindowManager.ActiveWindowEx = CInt(CInt(Window.WINDOW_TVFULLSCREEN)) Then
				Log.Info("send message to fullscreen tv")
				Dim msg As New GUIMessage(GUIMessage.MessageType.GUI_MSG_RECORD, GUIWindowManager.ActiveWindow, 0, 0, 0, 0, _
					Nothing)
				msg.SendToTargetWindow = True
				msg.TargetWindowId = CInt(CInt(Window.WINDOW_TVFULLSCREEN))
				GUIGraphicsContext.SendMessage(msg)
				Return False
			End If

			Log.Info("TVHome:Record action")
			Dim server = New TvServer()

			Dim card As VirtualCard = Nothing
			Dim prog As Program = channel.CurrentProgram
			Dim isRecording As Boolean
			Dim hasProgram As Boolean = (prog IsNot Nothing)
			If hasProgram Then
				prog.Refresh()
				'refresh the states from db
				isRecording = (prog.IsRecording OrElse prog.IsRecordingOncePending)
			Else
				isRecording = server.IsRecording(channel.IdChannel, card)
			End If

			If Not isRecording Then
				If hasProgram Then
					Dim pDlgOK As GUIDialogMenuBottomRight = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_MENU_BOTTOM_RIGHT)), GUIDialogMenuBottomRight)
					If pDlgOK IsNot Nothing Then
						pDlgOK.Reset()
						pDlgOK.SetHeading(605)
						'my tv
						pDlgOK.AddLocalizedString(875)
						'current program
						pDlgOK.AddLocalizedString(876)
						'till manual stop
						pDlgOK.DoModal(GUIWindowManager.ActiveWindow)
						Select Case pDlgOK.SelectedId
							Case 875
								'record current program                  
								TVProgramInfo.CreateProgram(prog, CInt(ScheduleRecordingType.Once), dialogId)
								Return True

							Case 876
								'manual
								Dim doesManuelScheduleAlreadyExist As Boolean = DoesManualScheduleAlreadyExist(channel)
								If Not doesManuelScheduleAlreadyExist Then
									StartRecordingSchedule(channel, True)
									Return True
								End If
								Exit Select
						End Select
					End If
				Else
					'manual record
					StartRecordingSchedule(channel, True)
					Return True
				End If
			Else
				Dim s As Schedule = Nothing
				Dim idChannel As Integer = 0
				If hasProgram Then
					TVProgramInfo.IsRecordingProgram(prog, s, False)
					If s IsNot Nothing Then
						idChannel = s.ReferencedChannel().IdChannel
					End If
				Else
					s = Schedule.Retrieve(card.RecordingScheduleId)
					idChannel = card.IdChannel
				End If

				If s IsNot Nothing AndAlso idChannel > 0 Then
					TVUtil.DeleteRecAndSchedWithPrompt(s, idChannel)
				End If
			End If
			Return False
		End Function

		Private Shared Function DoesManualScheduleAlreadyExist(channel As Channel) As Boolean
			Dim existingSchedule As Schedule = Schedule.FindNoEPGSchedule(channel)
			Return (existingSchedule IsNot Nothing)
		End Function

		Private Sub UpdateGUIonPlaybackStateChange(playbackStarted As Boolean)
			If btnTvOnOff.Selected <> playbackStarted Then
				btnTvOnOff.Selected = playbackStarted
			End If

			UpdateProgressPercentageBar()

			Dim hasTeletext As Boolean = (Not Connected OrElse Card.HasTeletext) AndAlso (playbackStarted)
			btnTeletext.IsVisible = hasTeletext
		End Sub

		Private Sub UpdateGUIonPlaybackStateChange()
			Dim isTimeShiftingTV As Boolean = (Connected AndAlso Card.IsTimeShifting AndAlso g_Player.IsTV)

			If btnTvOnOff.Selected <> isTimeShiftingTV Then
				btnTvOnOff.Selected = isTimeShiftingTV
			End If

			UpdateProgressPercentageBar()

			Dim hasTeletext As Boolean = (Not Connected OrElse Card.HasTeletext) AndAlso (isTimeShiftingTV)
			btnTeletext.IsVisible = hasTeletext
		End Sub

		Private Sub UpdateCurrentChannel()
			If Not g_Player.Playing Then
				Return
			End If
			Navigator.UpdateCurrentChannel()
			UpdateProgressPercentageBar()

			GUIControl.HideControl(GetID, CInt(Controls.LABEL_REC_INFO))
			GUIControl.HideControl(GetID, CInt(Controls.IMG_REC_RECTANGLE))
			GUIControl.HideControl(GetID, CInt(Controls.IMG_REC_CHANNEL))
		End Sub

		''' <summary>
		''' This function replaces g_player.ShowFullScreenWindowTV
		''' </summary>
		''' <returns></returns>
		Private Shared Function ShowFullScreenWindowTVHandler() As Boolean
			If (g_Player.IsTV AndAlso Card.IsTimeShifting) OrElse g_Player.IsTVRecording Then
				' watching TV
				If GUIWindowManager.ActiveWindow = CInt(Window.WINDOW_TVFULLSCREEN) Then
					Return True
				End If
				Log.Info("TVHome: ShowFullScreenWindow switching to fullscreen tv")
				GUIWindowManager.ActivateWindow(CInt(Window.WINDOW_TVFULLSCREEN))
				GUIGraphicsContext.IsFullScreenVideo = True
				Return True
			End If
			Return g_Player.ShowFullScreenWindowTVDefault()
		End Function

		Public Shared Sub UpdateTimeShift()
		End Sub

		Private Sub OnActiveRecordings()
			Dim ignoreActiveRecordings As IList(Of Recording) = New List(Of Recording)()
			OnActiveRecordings(ignoreActiveRecordings)
		End Sub

		Private Sub OnActiveRecordings(ignoreActiveRecordings As IList(Of Recording))
			Dim dlg As GUIDialogMenu = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_MENU)), GUIDialogMenu)
			If dlg Is Nothing Then
				Return
			End If

			dlg.Reset()
			dlg.SetHeading(200052)
			' Active Recordings      
			Dim activeRecordings As IList(Of Recording) = Recording.ListAllActive()
			If activeRecordings IsNot Nothing AndAlso activeRecordings.Count > 0 Then
				For Each activeRecording As Recording In activeRecordings
					If Not ignoreActiveRecordings.Contains(activeRecording) Then
						Dim item As New GUIListItem()
						Dim channelName As String = activeRecording.ReferencedChannel().DisplayName
						Dim programTitle As String = activeRecording.Title.Trim()
						' default is current EPG info
						item.Label = channelName
						item.Label2 = programTitle

						Dim strLogo As String = Utils.GetCoverArt(Thumbs.TVChannel, channelName)
						If String.IsNullOrEmpty(strLogo) Then
							strLogo = "defaultVideoBig.png"
						End If

						item.IconImage = strLogo
						item.IconImageBig = strLogo
						item.PinImage = ""
						dlg.Add(item)
					End If
				Next

				dlg.SelectedLabel = activeRecordings.Count

				dlg.DoModal(Me.GetID)
				If dlg.SelectedLabel < 0 Then
					Return
				End If

				If dlg.SelectedLabel < 0 OrElse (dlg.SelectedLabel - 1 > activeRecordings.Count) Then
					Return
				End If

				Dim selectedRecording As Recording = activeRecordings(dlg.SelectedLabel)
				Dim parentSchedule As Schedule = selectedRecording.ReferencedSchedule()
				If parentSchedule Is Nothing OrElse parentSchedule.IdSchedule < 1 Then
					Return
				End If
				Dim deleted As Boolean = TVUtil.StopRecAndSchedWithPrompt(parentSchedule, selectedRecording.IdChannel)
				If deleted AndAlso Not ignoreActiveRecordings.Contains(selectedRecording) Then
					ignoreActiveRecordings.Add(selectedRecording)
				End If
					'keep on showing the list until --> 1) user leaves menu, 2) no more active recordings
				OnActiveRecordings(ignoreActiveRecordings)
			Else
				Dim pDlgOK As GUIDialogOK = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_OK)), GUIDialogOK)
				If pDlgOK IsNot Nothing Then
					pDlgOK.SetHeading(200052)
					'my tv
					pDlgOK.SetLine(1, GUILocalizeStrings.[Get](200053))
					' No Active recordings
					pDlgOK.SetLine(2, "")
					pDlgOK.DoModal(Me.GetID)
				End If
			End If
		End Sub

		Private Sub OnActiveStreams()
			Dim dlg As GUIDialogMenu = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_MENU)), GUIDialogMenu)
			If dlg Is Nothing Then
				Return
			End If
			dlg.Reset()
			dlg.SetHeading(692)
			' Active Tv Streams
			Dim selected As Integer = 0

			Dim cards As IList(Of Card) = TvDatabase.Card.ListAll()
			Dim channels As New List(Of Channel)()
			Dim count As Integer = 0
			Dim server As New TvServer()
			Dim _users As New List(Of IUser)()
			For Each card__1 As Card In cards
				If card__1.Enabled = False Then
					Continue For
				End If
				If Not RemoteControl.Instance.CardPresent(card__1.IdCard) Then
					Continue For
				End If
				Dim users As IUser() = RemoteControl.Instance.GetUsersForCard(card__1.IdCard)
				If users Is Nothing Then
					Return
				End If
				For i As Integer = 0 To users.Length - 1
					Dim user As IUser = users(i)
					If card__1.IdCard <> user.CardId Then
						Continue For
					End If
					Dim isRecording As Boolean
					Dim isTimeShifting As Boolean
					Dim tvcard As New VirtualCard(user, RemoteControl.HostName)
					isRecording = tvcard.IsRecording
					isTimeShifting = tvcard.IsTimeShifting
					If isTimeShifting OrElse (isRecording AndAlso Not isTimeShifting) Then
						Dim idChannel As Integer = tvcard.IdChannel
						user = tvcard.User
						Dim ch As Channel = Channel.Retrieve(idChannel)
						channels.Add(ch)
						Dim item As New GUIListItem()
						item.Label = ch.DisplayName
						item.Label2 = user.Name
						Dim strLogo As String = Utils.GetCoverArt(Thumbs.TVChannel, ch.DisplayName)
						If String.IsNullOrEmpty(strLogo) Then
							strLogo = "defaultVideoBig.png"
						End If
						item.IconImage = strLogo
						If isRecording Then
							item.PinImage = Thumbs.TvRecordingIcon
						Else
							item.PinImage = ""
						End If
						dlg.Add(item)
						_users.Add(user)
						If Card IsNot Nothing AndAlso Card.IdChannel = idChannel Then
							selected = count
						End If
						count += 1
					End If
				Next
			Next
			If channels.Count = 0 Then
				Dim pDlgOK As GUIDialogOK = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_OK)), GUIDialogOK)
				If pDlgOK IsNot Nothing Then
					pDlgOK.SetHeading(692)
					'my tv
					pDlgOK.SetLine(1, GUILocalizeStrings.[Get](1511))
					' No Active streams
					pDlgOK.SetLine(2, "")
					pDlgOK.DoModal(Me.GetID)
				End If
				Return
			End If
			dlg.SelectedLabel = selected
			dlg.DoModal(Me.GetID)
			If dlg.SelectedLabel < 0 Then
				Return
			End If

			Dim vCard As New VirtualCard(_users(dlg.SelectedLabel), RemoteControl.HostName)
			Dim channel__2 As Channel = Navigator.GetChannel(vCard.IdChannel)
			ViewChannel(channel__2)
		End Sub

		Private Sub OnRecord()
			ManualRecord(Navigator.Channel, GetID)
			UpdateStateOfRecButton()
		End Sub

		''' <summary>
		''' Update the state of the following buttons    
		''' - record now
		''' </summary>
		Private Sub UpdateStateOfRecButton()
			If Not Connected Then
				btnTvOnOff.Selected = False
				Return
			End If
			Dim isTimeShifting As Boolean = Card.IsTimeShifting

			'are we recording a tv program?      
			If Navigator.Channel IsNot Nothing AndAlso Card IsNot Nothing Then
				Dim label As String
				Dim server As New TvServer()
				Dim vc As VirtualCard
				If server.IsRecording(Navigator.Channel.IdChannel, vc) Then
					If Not isTimeShifting Then
						Card = vc
					End If
					'yes then disable the timeshifting on/off buttons
					'and change the Record Now button into Stop Record
						'stop record
					label = GUILocalizeStrings.[Get](629)
				Else
					'nop. then change the Record Now button
					'to Record Now
						' record
					label = GUILocalizeStrings.[Get](601)
				End If
				If label <> btnRecord.Label Then
					btnRecord.Label = label
				End If
			End If
		End Sub

		Private Sub UpdateRecordingIndicator()
			Dim now As DateTime = DateTime.Now
			'Log.Debug("updaterec: conn:{0}, rec:{1}", Connected, Card.IsRecording);
			' if we're recording tv, update gui with info
			If Connected AndAlso Card.IsRecording Then
				Dim scheduleId As Integer = Card.RecordingScheduleId
				If scheduleId > 0 Then
					Dim schedule__1 As Schedule = Schedule.Retrieve(scheduleId)
					If schedule__1 IsNot Nothing Then
						If schedule__1.ScheduleType = CInt(ScheduleRecordingType.Once) Then
							imgRecordingIcon.SetFileName(Thumbs.TvRecordingIcon)
						Else
							imgRecordingIcon.SetFileName(Thumbs.TvRecordingSeriesIcon)
						End If
					End If
				End If
			Else
				imgRecordingIcon.IsVisible = False
			End If
		End Sub

		''' <summary>
		''' Update the the progressbar in the GUI which shows
		''' how much of the current tv program has elapsed
		''' </summary>
		Public Shared Sub UpdateProgressPercentageBar()
			Dim ts As TimeSpan = DateTime.Now - _updateProgressTimer
			If ts.TotalMilliseconds < PROGRESS_PERCENTAGE_UPDATE_INTERVAL Then
				Return
			End If

			Try
				If Not Connected Then
					Return
				End If

				'set audio video related media info properties.
				Dim currAudio As Integer = g_Player.CurrentAudioStream
				If currAudio > -1 Then
					UpdateAudioProperties(currAudio)
				End If

				' Check for recordings vs liveTv/Radio or Idle
				If g_Player.IsTVRecording Then
					Dim currentPosition As Double = g_Player.CurrentPosition
					Dim duration As Double = g_Player.Duration

					Dim startTime As String = Utils.SecondsToHMSString(CInt(Math.Truncate(currentPosition)))
					Dim endTime As String = Utils.SecondsToHMSString(CInt(Math.Truncate(duration)))

					Dim percentLivePoint As Double = currentPosition / duration
					percentLivePoint *= 100.0

					GUIPropertyManager.SetProperty("#TV.Record.percent1", percentLivePoint.ToString())
					GUIPropertyManager.SetProperty("#TV.Record.percent2", "0")
					GUIPropertyManager.SetProperty("#TV.Record.percent3", "0")

					Dim rec As Recording = TvRecorded.ActiveRecording()
					Dim displayName As String = TvRecorded.GetRecordingDisplayName(rec)

					GUIPropertyManager.SetProperty("#TV.View.channel", displayName & " (" & GUILocalizeStrings.[Get](604) & ")")
					GUIPropertyManager.SetProperty("#TV.View.title", g_Player.currentTitle)
					GUIPropertyManager.SetProperty("#TV.View.compositetitle", g_Player.currentTitle)
					GUIPropertyManager.SetProperty("#TV.View.description", g_Player.currentDescription)

					GUIPropertyManager.SetProperty("#TV.View.start", startTime)
					GUIPropertyManager.SetProperty("#TV.View.stop", endTime)
					If rec IsNot Nothing Then
						GUIPropertyManager.SetProperty("#TV.View.title", rec.Title)
						GUIPropertyManager.SetProperty("#TV.View.compositetitle", TVUtil.GetDisplayTitle(rec))
						GUIPropertyManager.SetProperty("#TV.View.subtitle", rec.EpisodeName)
						GUIPropertyManager.SetProperty("#TV.View.episode", rec.EpisodeNumber)
					End If

					Dim strLogo As String = Utils.GetCoverArt(Thumbs.TVChannel, displayName)
					GUIPropertyManager.SetProperty("#TV.View.thumb", If(String.IsNullOrEmpty(strLogo), "defaultVideoBig.png", strLogo))
					Return
				End If

				' No channel -> no EPG
				If Navigator.Channel Is Nothing Then
					'Log.Debug("UpdateProgressPercentageBar: Navigator.Channel == null");
					Return
				End If

				If g_Player.IsRadio Then
					'Log.Debug("UpdateProgressPercentageBar: g_Player.IsRadio = true");

					'
'              * Team decision was not to hide TV's last channel EPG while Radio plays
'              * 
'            UpdateCurrentEpgProperties(null);
'            UpdateNextEpgProperties(null);
'            

					Return
				End If

				Dim infoChannel As Channel
				If Navigator.Channel.IsTv Then
					infoChannel = Navigator.Channel
				Else
					infoChannel = _lastTvChannel
				End If
				UpdateCurrentEpgProperties(infoChannel)
				UpdateNextEpgProperties(infoChannel)
				'Update lastTvChannel with current
				_lastTvChannel = infoChannel
			Finally
				_updateProgressTimer = DateTime.Now
			End Try
		End Sub

		Private Shared Sub UpdateAudioProperties(currAudio As Integer)
			Dim streamType As String = g_Player.AudioType(currAudio)

			GUIPropertyManager.SetProperty("#TV.View.IsAC3", String.Empty)
			GUIPropertyManager.SetProperty("#TV.View.IsMP1A", String.Empty)
			GUIPropertyManager.SetProperty("#TV.View.IsMP2A", String.Empty)
			GUIPropertyManager.SetProperty("#TV.View.IsAAC", String.Empty)
			GUIPropertyManager.SetProperty("#TV.View.IsLATMAAC", String.Empty)

			Select Case streamType
				Case "AC3", "AC3plus"
					' just for the time being use the same icon for AC3 & AC3plus
					GUIPropertyManager.SetProperty("#TV.View.IsAC3", String.Format("{0}{1}{2}", GUIGraphicsContext.Skin, "\Media\Logos\", "ac3.png"))
					Exit Select

				Case "Mpeg1"
					GUIPropertyManager.SetProperty("#TV.View.IsMP1A", String.Format("{0}{1}{2}", GUIGraphicsContext.Skin, "\Media\Logos\", "mp1a.png"))
					Exit Select

				Case "Mpeg2"
					GUIPropertyManager.SetProperty("#TV.View.IsMP2A", String.Format("{0}{1}{2}", GUIGraphicsContext.Skin, "\Media\Logos\", "mp2a.png"))
					Exit Select

				Case "AAC"
					GUIPropertyManager.SetProperty("#TV.View.IsAAC", String.Format("{0}{1}{2}", GUIGraphicsContext.Skin, "\Media\Logos\", "aac.png"))
					Exit Select

				Case "LATMAAC"
					GUIPropertyManager.SetProperty("#TV.View.IsLATMAAC", String.Format("{0}{1}{2}", GUIGraphicsContext.Skin, "\Media\Logos\", "latmaac3.png"))
					Exit Select
			End Select
		End Sub

		Private Shared Sub UpdateCurrentEpgProperties(ch As Channel)

			Dim hasChannel As Boolean = (ch IsNot Nothing)
			Dim current As Program = Nothing
			If hasChannel Then
				current = ch.CurrentProgram
			End If
			Dim hasCurrentEPG As Boolean = hasChannel AndAlso current IsNot Nothing

			If Not hasChannel OrElse Not hasCurrentEPG Then
				GUIPropertyManager.SetProperty("#TV.View.title", GUILocalizeStrings.[Get](736))
				' no epg for this channel
				GUIPropertyManager.SetProperty("#TV.View.compositetitle", GUILocalizeStrings.[Get](736))
				' no epg for this channel
				GUIPropertyManager.SetProperty("#TV.View.start", [String].Empty)
				GUIPropertyManager.SetProperty("#TV.View.stop", [String].Empty)
				GUIPropertyManager.SetProperty("#TV.View.description", [String].Empty)
				GUIPropertyManager.SetProperty("#TV.View.subtitle", [String].Empty)
				GUIPropertyManager.SetProperty("#TV.View.episode", [String].Empty)
				GUIPropertyManager.SetProperty("#TV.View.genre", [String].Empty)
				GUIPropertyManager.SetProperty("#TV.View.Percentage", "0")
				GUIPropertyManager.SetProperty("#TV.Record.percent1", "0")
				GUIPropertyManager.SetProperty("#TV.Record.percent2", "0")
				GUIPropertyManager.SetProperty("#TV.Record.percent3", "0")
				GUIPropertyManager.SetProperty("#TV.View.remaining", [String].Empty)
				GUIPropertyManager.SetProperty("#TV.View.thumb", [String].Empty)

				If Not hasChannel Then
					GUIPropertyManager.SetProperty("#TV.View.channel", [String].Empty)
					Log.Debug("UpdateCurrentEpgProperties: no channel, returning")
				End If

				If Not hasCurrentEPG Then
					Log.Debug("UpdateCurrentEpgProperties: no EPG data, returning")
				End If
			Else
				GUIPropertyManager.SetProperty("#TV.View.channel", ch.DisplayName)
				GUIPropertyManager.SetProperty("#TV.View.title", current.Title)
				GUIPropertyManager.SetProperty("#TV.View.compositetitle", TVUtil.GetDisplayTitle(current))
				GUIPropertyManager.SetProperty("#TV.View.start", current.StartTime.ToString("t", CultureInfo.CurrentCulture.DateTimeFormat))
				GUIPropertyManager.SetProperty("#TV.View.stop", current.EndTime.ToString("t", CultureInfo.CurrentCulture.DateTimeFormat))
				GUIPropertyManager.SetProperty("#TV.View.description", current.Description)
				GUIPropertyManager.SetProperty("#TV.View.subtitle", current.EpisodeName)
				GUIPropertyManager.SetProperty("#TV.View.episode", current.EpisodeNumber)
				GUIPropertyManager.SetProperty("#TV.View.genre", current.Genre)
				GUIPropertyManager.SetProperty("#TV.View.remaining", Utils.SecondsToHMSString(current.EndTime - current.StartTime))

				Dim strLogo As String = Utils.GetCoverArt(Thumbs.TVChannel, ch.DisplayName)
				If String.IsNullOrEmpty(strLogo) Then
					strLogo = "defaultVideoBig.png"
				End If
				GUIPropertyManager.SetProperty("#TV.View.thumb", strLogo)

				Dim ts As TimeSpan = current.EndTime - current.StartTime

				If ts.TotalSeconds > 0 Then
					' calculate total duration of the current program
					Dim programDuration As Double = ts.TotalSeconds

					'calculate where the program is at this time
					ts = (DateTime.Now - current.StartTime)
					Dim livePoint As Double = ts.TotalSeconds

					'calculate when timeshifting was started
					Dim timeShiftStartPoint As Double = livePoint - g_Player.Duration
					Dim playingPoint As Double = timeShiftStartPoint + g_Player.CurrentPosition
					If timeShiftStartPoint < 0 Then
						timeShiftStartPoint = 0
					End If

					Dim timeShiftStartPointPercent As Double = timeShiftStartPoint / programDuration
					timeShiftStartPointPercent *= 100.0
					GUIPropertyManager.SetProperty("#TV.Record.percent1", timeShiftStartPointPercent.ToString())

					Dim playingPointPercent As Double = playingPoint / programDuration
					playingPointPercent *= 100.0
					GUIPropertyManager.SetProperty("#TV.Record.percent2", playingPointPercent.ToString())

					Dim percentLivePoint As Double = livePoint / programDuration
					percentLivePoint *= 100.0
					GUIPropertyManager.SetProperty("#TV.View.Percentage", percentLivePoint.ToString())
					GUIPropertyManager.SetProperty("#TV.Record.percent3", percentLivePoint.ToString())
				End If
			End If


		End Sub

		Private Shared Sub UpdateNextEpgProperties(ch As Channel)
			Dim [next] As Program = Nothing
			If ch Is Nothing Then
				Log.Debug("UpdateNextEpgProperties: no channel, returning")
			Else
				[next] = ch.NextProgram
				If [next] Is Nothing Then
					Log.Debug("UpdateNextEpgProperties: no EPG data, returning")
				End If
			End If

			If [next] IsNot Nothing Then
				GUIPropertyManager.SetProperty("#TV.Next.title", [next].Title)
				GUIPropertyManager.SetProperty("#TV.Next.compositetitle", TVUtil.GetDisplayTitle([next]))
				GUIPropertyManager.SetProperty("#TV.Next.start", [next].StartTime.ToString("t", CultureInfo.CurrentCulture.DateTimeFormat))
				GUIPropertyManager.SetProperty("#TV.Next.stop", [next].EndTime.ToString("t", CultureInfo.CurrentCulture.DateTimeFormat))
				GUIPropertyManager.SetProperty("#TV.Next.description", [next].Description)
				GUIPropertyManager.SetProperty("#TV.Next.subtitle", [next].EpisodeName)
				GUIPropertyManager.SetProperty("#TV.Next.episode", [next].EpisodeNumber)
				GUIPropertyManager.SetProperty("#TV.Next.genre", [next].Genre)
				GUIPropertyManager.SetProperty("#TV.Next.remaining", Utils.SecondsToHMSString([next].EndTime - [next].StartTime))
			Else
				GUIPropertyManager.SetProperty("#TV.Next.title", GUILocalizeStrings.[Get](736))
				' no epg for this channel
				GUIPropertyManager.SetProperty("#TV.Next.compositetitle", GUILocalizeStrings.[Get](736))
				' no epg for this channel
				GUIPropertyManager.SetProperty("#TV.Next.start", [String].Empty)
				GUIPropertyManager.SetProperty("#TV.Next.stop", [String].Empty)
				GUIPropertyManager.SetProperty("#TV.Next.description", [String].Empty)
				GUIPropertyManager.SetProperty("#TV.Next.subtitle", [String].Empty)
				GUIPropertyManager.SetProperty("#TV.Next.episode", [String].Empty)
				GUIPropertyManager.SetProperty("#TV.Next.genre", [String].Empty)
			End If
		End Sub

		''' <summary>
		''' When called this method will switch to the previous TV channel
		''' </summary>
		Public Shared Sub OnPreviousChannel()
			Log.Info("TVHome:OnPreviousChannel()")
			If GUIGraphicsContext.IsFullScreenVideo Then
				' where in fullscreen so delayzap channel instead of immediatly tune..
				Dim TVWindow As TvFullScreen = DirectCast(GUIWindowManager.GetWindow(CInt(CInt(Window.WINDOW_TVFULLSCREEN))), TvFullScreen)
				If TVWindow IsNot Nothing Then
					TVWindow.ZapPreviousChannel()
				End If
				Return
			End If

			' Zap to previous channel immediately
			Navigator.ZapToPreviousChannel(False)
		End Sub

		#Region "audio selection section"

		''' <summary>
		''' unit test enabled method. please respect this.
		''' run and/or modify the unit tests accordingly.
		''' </summary>
		Public Shared Function GetPreferedAudioStreamIndex(ByRef dualMonoMode As eAudioDualMonoMode) As Integer
		' also used from tvrecorded class
			Dim idxFirstAc3 As Integer = -1
			' the index of the first avail. ac3 found
			Dim idxFirstmpeg As Integer = -1
			' the index of the first avail. mpg found
			Dim idxStreamIndexAc3 As Integer = -1
			' the streamindex of ac3 found based on lang. pref
			Dim idxStreamIndexmpeg As Integer = -1
			' the streamindex of mpg found based on lang. pref   
			Dim idx As Integer = -1
			' the chosen audio index we return
			Dim idxLangPriAc3 As Integer = -1
			' the lang priority of ac3 found based on lang. pref
			Dim idxLangPrimpeg As Integer = -1
			' the lang priority of mpg found based on lang. pref         
			Dim ac3BasedOnLang As String = ""
			' for debugging, what lang. in prefs. where used to choose the ac3 audio track ?
			Dim mpegBasedOnLang As String = ""
			' for debugging, what lang. in prefs. where used to choose the mpeg audio track ?      

			dualMonoMode = eAudioDualMonoMode.UNSUPPORTED

			Dim streams As IAudioStream() = GetStreamsList()

			If IsPreferredAudioLanguageAvailable() Then
				Log.Debug("TVHome.GetPreferedAudioStreamIndex(): preferred LANG(s):{0} preferAC3:{1} preferAudioTypeOverLang:{2}", [String].Join(";", _preferredLanguages.ToArray()), _preferAC3, _preferAudioTypeOverLang)
			Else
				Log.Debug("TVHome.GetPreferedAudioStreamIndex(): preferred LANG(s):{0} preferAC3:{1} _preferAudioTypeOverLang:{2}", "n/a", _preferAC3, _preferAudioTypeOverLang)
			End If
			Log.Debug("Audio streams avail: {0}", streams.Length)
			Dim dualMonoModeEnabled As Boolean = (g_Player.GetAudioDualMonoMode() <> eAudioDualMonoMode.UNSUPPORTED)

			If streams.Length = 1 AndAlso Not ShouldApplyDualMonoMode(streams(0).Language) Then
				Log.Info("Audio stream: switching to preferred AC3/MPEG audio stream 0 (only 1 track avail.)")
				Return 0
			End If

			Dim priority As Integer = Integer.MaxValue
			idxFirstAc3 = GetFirstAC3Index(streams)
			idxFirstmpeg = GetFirstMpegIndex(streams)

			UpdateAudioStreamIndexesAndPrioritiesBasedOnLanguage(streams, priority, idxStreamIndexmpeg, mpegBasedOnLang, idxStreamIndexAc3, idxLangPriAc3, _
				idxLangPrimpeg, ac3BasedOnLang, dualMonoMode)
			idx = GetAC3AudioStreamIndex(idxStreamIndexmpeg, idxStreamIndexAc3, ac3BasedOnLang, idx, idxFirstAc3)

			If idx = -1 AndAlso _preferAC3 Then
				Log.Info("Audio stream: no preferred AC3 audio stream found, trying mpeg instead.")
			End If

			If idx = -1 OrElse Not _preferAC3 Then
				' we end up here if ac3 selection didnt happen (no ac3 avail.) or if preferac3 is disabled.
				If IsPreferredAudioLanguageAvailable() Then
					'did we find a mpeg track that matches our LANG prefs ?
					idx = GetMpegAudioStreamIndexBasedOnLanguage(idxStreamIndexmpeg, mpegBasedOnLang, idxStreamIndexAc3, idx, idxFirstmpeg)
				Else
					idx = idxFirstmpeg
					Log.Info("Audio stream: switching to preferred MPEG audio stream {0}, NOT based on LANG", idx)
				End If
			End If

			If idx = -1 Then
				idx = 0
				Log.Info("Audio stream: no preferred stream found - switching to audio stream 0")
			End If

			Return idx
		End Function

		Private Shared Function GetAC3AudioStreamIndex(idxStreamIndexmpeg As Integer, idxStreamIndexAc3 As Integer, ac3BasedOnLang As String, idx As Integer, idxFirstAc3 As Integer) As Integer
			If _preferAC3 Then
				If IsPreferredAudioLanguageAvailable() Then
					'did we find an ac3 track that matches our LANG prefs ?
						'if not then proceed with mpeg lang. selection below.
					idx = GetAC3AudioStreamIndexBasedOnLanguage(idxStreamIndexmpeg, idxStreamIndexAc3, ac3BasedOnLang, idx, idxFirstAc3)
				Else
					'did we find an ac3 track ?
					If idxFirstAc3 > -1 Then
						idx = idxFirstAc3
						Log.Info("Audio stream: switching to preferred AC3 audio stream {0}, NOT based on LANG", idx)
						'if not then proceed with mpeg lang. selection below.
					End If
				End If
			End If
			Return idx
		End Function

		Private Shared Sub UpdateAudioStreamIndexesAndPrioritiesBasedOnLanguage(streams As IAudioStream(), priority As Integer, ByRef idxStreamIndexmpeg As Integer, ByRef mpegBasedOnLang As String, ByRef idxStreamIndexAc3 As Integer, idxLangPriAc3 As Integer, _
			idxLangPrimpeg As Integer, ByRef ac3BasedOnLang As String, ByRef dualMonoMode As eAudioDualMonoMode)
			dualMonoMode = eAudioDualMonoMode.UNSUPPORTED
			If IsPreferredAudioLanguageAvailable() Then
				For i As Integer = 0 To streams.Length - 1
					'now find the ones based on LANG prefs.        
					If ShouldApplyDualMonoMode(streams(i).Language) Then
						dualMonoMode = GetDualMonoMode(streams, i, priority, idxStreamIndexmpeg, mpegBasedOnLang)
						If dualMonoMode <> eAudioDualMonoMode.UNSUPPORTED Then
							Exit For
						End If
					Else
						' lower value means higher priority
						UpdateAudioStreamIndexesBasedOnLang(streams, i, idxStreamIndexmpeg, idxStreamIndexAc3, mpegBasedOnLang, idxLangPriAc3, _
							idxLangPrimpeg, ac3BasedOnLang)
					End If
				Next
			End If
		End Sub

		Private Shared Function GetMpegAudioStreamIndexBasedOnLanguage(idxStreamIndexmpeg As Integer, mpegBasedOnLang As String, idxStreamIndexAc3 As Integer, idx As Integer, idxFirstmpeg As Integer) As Integer
			If idxStreamIndexmpeg > -1 Then
				idx = idxStreamIndexmpeg
				Log.Info("Audio stream: switching to preferred MPEG audio stream {0}, based on LANG {1}", idx, mpegBasedOnLang)
			'if not, did we even find a mpeg track ?
			ElseIf idxFirstmpeg > -1 Then
				'we did find a AC3 track, but not based on LANG - should we choose this or the mpeg track which is based on LANG.
				If _preferAudioTypeOverLang OrElse (idxStreamIndexAc3 = -1 AndAlso _preferAudioTypeOverLang) Then
					idx = idxFirstmpeg
					Log.Info("Audio stream: switching to preferred MPEG audio stream {0}, NOT based on LANG (none avail. matching {1})", idx, mpegBasedOnLang)
				ElseIf idxStreamIndexAc3 > -1 Then
					idx = idxStreamIndexAc3
					Log.Info("Audio stream: ignoring MPEG audio stream {0}", idx)
				End If
			End If
			Return idx
		End Function

		Private Shared Function GetAC3AudioStreamIndexBasedOnLanguage(idxStreamIndexmpeg As Integer, idxStreamIndexAc3 As Integer, ac3BasedOnLang As String, idx As Integer, idxFirstAc3 As Integer) As Integer
			If idxStreamIndexAc3 > -1 Then
				idx = idxStreamIndexAc3
				Log.Info("Audio stream: switching to preferred AC3 audio stream {0}, based on LANG {1}", idx, ac3BasedOnLang)
			'if not, did we even find an ac3 track ?
			ElseIf idxFirstAc3 > -1 Then
				'we did find an AC3 track, but not based on LANG - should we choose this or the mpeg track which is based on LANG.
				If _preferAudioTypeOverLang OrElse idxStreamIndexmpeg = -1 Then
					idx = idxFirstAc3
					Log.Info("Audio stream: switching to preferred AC3 audio stream {0}, NOT based on LANG (none avail. matching {1})", idx, ac3BasedOnLang)
				Else
					Log.Info("Audio stream: ignoring AC3 audio stream {0}", idxFirstAc3)
				End If
			End If
			Return idx
		End Function

		Private Shared Sub UpdateAudioStreamIndexesBasedOnLang(streams As IAudioStream(), i As Integer, ByRef idxStreamIndexmpeg As Integer, ByRef idxStreamIndexAc3 As Integer, ByRef mpegBasedOnLang As String, ByRef idxLangPriAc3 As Integer, _
			ByRef idxLangPrimpeg As Integer, ByRef ac3BasedOnLang As String)
			Dim langPriority As Integer = _preferredLanguages.IndexOf(streams(i).Language)
			Dim langSel As String = streams(i).Language
			Log.Debug("Stream {0} lang {1}, lang priority index {2}", i, langSel, langPriority)

			' is the stream language preferred?
			If langPriority >= 0 Then
				' has the stream a higher priority than an old one or is this the first AC3 stream with lang pri (idxLangPriAc3 == -1) (AC3)
				Dim isAC3 As Boolean = IsStreamAC3(streams(i))
				If isAC3 Then
					If idxLangPriAc3 = -1 OrElse langPriority < idxLangPriAc3 Then
						Log.Debug("Setting AC3 pref")
						idxStreamIndexAc3 = i
						idxLangPriAc3 = langPriority
						ac3BasedOnLang = langSel
					End If
				Else
					'not AC3
					' has the stream a higher priority than an old one or is this the first mpeg stream with lang pri (idxLangPrimpeg == -1) (mpeg)
					If idxLangPrimpeg = -1 OrElse langPriority < idxLangPrimpeg Then
						Log.Debug("Setting mpeg pref")
						idxStreamIndexmpeg = i
						idxLangPrimpeg = langPriority
						mpegBasedOnLang = langSel
					End If
				End If
			End If
		End Sub

		Private Shared Function IsStreamAC3(stream As IAudioStream) As Boolean
			Return (stream.StreamType = AudioStreamType.AC3 OrElse stream.StreamType = AudioStreamType.EAC3)
		End Function

		Private Shared Function ShouldApplyDualMonoMode(language As String) As Boolean
			Dim dualMonoModeEnabled As Boolean = (g_Player.GetAudioDualMonoMode() <> eAudioDualMonoMode.UNSUPPORTED)
			Return (dualMonoModeEnabled AndAlso language.Length = 6)
		End Function

		Private Shared Function GetFirstAC3Index(streams As IAudioStream()) As Integer
			Dim idxFirstAc3 As Integer = -1

			For i As Integer = 0 To streams.Length - 1
				If IsStreamAC3(streams(i)) Then
					idxFirstAc3 = i
					Exit For
				End If
			Next
			Return idxFirstAc3
		End Function

		Private Shared Function GetFirstMpegIndex(streams As IAudioStream()) As Integer
			Dim idxFirstMpeg As Integer = -1

			For i As Integer = 0 To streams.Length - 1
				If Not IsStreamAC3(streams(i)) Then
					idxFirstMpeg = i
					Exit For
				End If
			Next
			Return idxFirstMpeg
		End Function

		Private Shared Function GetDualMonoMode(streams As IAudioStream(), currentIndex As Integer, ByRef priority As Integer, ByRef idxStreamIndexmpeg As Integer, ByRef mpegBasedOnLang As String) As eAudioDualMonoMode
			Dim dualMonoMode As eAudioDualMonoMode = eAudioDualMonoMode.UNSUPPORTED
			Dim leftAudioLang As String = streams(currentIndex).Language.Substring(0, 3)
			Dim rightAudioLang As String = streams(currentIndex).Language.Substring(3, 3)

			Dim indexLeft As Integer = _preferredLanguages.IndexOf(leftAudioLang)
			If indexLeft >= 0 AndAlso indexLeft < priority Then
				dualMonoMode = eAudioDualMonoMode.LEFT_MONO
				mpegBasedOnLang = leftAudioLang
				idxStreamIndexmpeg = currentIndex
				priority = indexLeft
			End If

			Dim indexRight As Integer = _preferredLanguages.IndexOf(rightAudioLang)
			If indexRight >= 0 AndAlso indexRight < priority Then
				dualMonoMode = eAudioDualMonoMode.RIGHT_MONO
				mpegBasedOnLang = rightAudioLang
				idxStreamIndexmpeg = currentIndex
				priority = indexRight
			End If
			Return dualMonoMode
		End Function

		Private Shared Function IsPreferredAudioLanguageAvailable() As Boolean
			Return (_preferredLanguages IsNot Nothing AndAlso _preferredLanguages.Count > 0)
		End Function

		Private Shared Function GetStreamsList() As IAudioStream()
			Dim streamsList As New List(Of IAudioStream)()
			For i As Integer = 0 To g_Player.AudioStreams - 1
				Dim stream As New DVBAudioStream()

				Dim streamType As String = g_Player.AudioType(i)

				Select Case streamType
					Case "AC3"
						stream.StreamType = AudioStreamType.AC3
						Exit Select
					Case "AC3plus"
						stream.StreamType = AudioStreamType.EAC3
						Exit Select
					Case "Mpeg1"
						stream.StreamType = AudioStreamType.Mpeg1
						Exit Select
					Case "Mpeg2"
						stream.StreamType = AudioStreamType.Mpeg2
						Exit Select
					Case "AAC"
						stream.StreamType = AudioStreamType.AAC
						Exit Select
					Case "LATMAAC"
						stream.StreamType = AudioStreamType.LATMAAC
						Exit Select
					Case Else
						stream.StreamType = AudioStreamType.Unknown
						Exit Select
				End Select

				stream.Language = g_Player.AudioLanguage(i)
				Dim lang As String() = stream.Language.Split("("C)
				If lang.Length > 1 Then
					stream.Language = lang(1).Substring(0, lang(1).Length - 1)
				End If
				streamsList.Add(stream)
			Next
			Return streamsList.ToArray()
		End Function

		#End Region

		Private Shared Sub ChannelTuneFailedNotifyUser(succeeded As TvResult, wasPlaying As Boolean, channel As Channel)
			GUIGraphicsContext.RenderBlackImage = False

			_lastError.Result = succeeded
			_lastError.FailingChannel = channel
			_lastError.Messages.Clear()

			' reset the last channel, so you can switch back after error
			TVHome.Navigator.SetFailingChannel(_lastError.FailingChannel)

			Dim TextID As Integer = 0
			_lastError.Messages.Add(GUILocalizeStrings.[Get](1500))
			Select Case succeeded
				Case TvResult.NoPmtFound
					TextID = 1498
					Exit Select
				Case TvResult.NoSignalDetected
					TextID = 1499
					Exit Select
				Case TvResult.CardIsDisabled
					TextID = 1501
					Exit Select
				Case TvResult.AllCardsBusy
					TextID = 1502
					Exit Select
				Case TvResult.ChannelIsScrambled
					TextID = 1503
					Exit Select
				Case TvResult.NoVideoAudioDetected
					TextID = 1504
					Exit Select
				Case TvResult.UnableToStartGraph
					TextID = 1505
					Exit Select
				Case TvResult.UnknownError
					' this error can also happen if we have no connection to the server.
					If Not Connected Then
						' || !IsRemotingConnected())
						TextID = 1510
					Else
						TextID = 1506
					End If
					Exit Select
				Case TvResult.UnknownChannel
					TextID = 1507
					Exit Select
				Case TvResult.ChannelNotMappedToAnyCard
					TextID = 1508
					Exit Select
				Case TvResult.NoTuningDetails
					TextID = 1509
					Exit Select
				Case TvResult.GraphBuildingFailed
					TextID = 1518
					Exit Select
				Case TvResult.SWEncoderMissing
					TextID = 1519
					Exit Select
				Case TvResult.NoFreeDiskSpace
					TextID = 1520
					Exit Select
				Case Else
					' this error can also happen if we have no connection to the server.
					If Not Connected Then
						' || !IsRemotingConnected())
						TextID = 1510
					Else
						TextID = 1506
					End If
					Exit Select
			End Select

			If TextID <> 0 Then
				_lastError.Messages.Add(GUILocalizeStrings.[Get](TextID))
			End If

			Dim pDlgNotify As GUIDialogNotify = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_NOTIFY)), GUIDialogNotify)
			Dim caption As String = GUILocalizeStrings.[Get](605) & " - " & Convert.ToString(channel.DisplayName)
			' +GUILocalizeStrings.Get(1512); ("tune last?")
			pDlgNotify.SetHeading(caption)
			'my tv
			Dim sbMessage As New StringBuilder()
			' ignore the "unable to start timeshift" line to avoid scrolling, because NotifyDLG has very few space available.
			For idx As Integer = 1 To _lastError.Messages.Count - 1
				sbMessage.AppendFormat(vbLf & "{0}", _lastError.Messages(idx))
			Next
			pDlgNotify.SetText(sbMessage.ToString())

			' Fullscreen shows the TVZapOSD to handle error messages
			If GUIWindowManager.ActiveWindow = CInt(CInt(Window.WINDOW_TVFULLSCREEN)) Then
				' If failed and wasPlaying TV, left screen as it is and show osd with error message 
				Log.Info("send message to fullscreen tv")
				Dim msg As New GUIMessage(GUIMessage.MessageType.GUI_MSG_TV_ERROR_NOTIFY, GUIWindowManager.ActiveWindow, 0, 0, 0, 0, _
					Nothing)
				msg.SendToTargetWindow = True
				msg.TargetWindowId = CInt(CInt(Window.WINDOW_TVFULLSCREEN))
				msg.[Object] = _lastError
				' forward error info object
				msg.Param1 = 3
				' sec timeout
				GUIGraphicsContext.SendMessage(msg)
				Return
			Else
				' show notify dialog 
				pDlgNotify.DoModal(CInt(GUIWindowManager.ActiveWindowEx))
			End If
		End Sub

		Private Shared Sub OnBlackImageRendered()
			If GUIGraphicsContext.RenderBlackImage Then
				'MediaPortal.GUI.Library.Log.Debug("TvHome.OnBlackImageRendered()");
				_waitForBlackScreen.[Set]()
			End If
		End Sub

		Private Shared Sub OnVideoReceived()
			If GUIGraphicsContext.RenderBlackImage Then
				Log.Debug("TvHome.OnVideoReceived() {0}", FramesBeforeStopRenderBlackImage)
				If FramesBeforeStopRenderBlackImage <> 0 Then
					FramesBeforeStopRenderBlackImage -= 1
					If FramesBeforeStopRenderBlackImage = 0 Then
						GUIGraphicsContext.RenderBlackImage = False
						Log.Debug("TvHome.StopRenderBlackImage()")
					End If
				End If
			End If
		End Sub

		Private Shared Sub StopRenderBlackImage()
			If GUIGraphicsContext.RenderBlackImage Then
					' Ambass : we need to wait the 3rd frame to avoid persistance of previous channel....Why ?????
					' Morpheus: number of frames depends on hardware, from 1..5 or higher might be needed! 
					'           Probably the faster the graphics card is, the more frames required???
				FramesBeforeStopRenderBlackImage = 3
			End If
		End Sub

		Private Shared Sub RenderBlackImage()
			If GUIGraphicsContext.RenderBlackImage = False Then
				Log.Debug("TvHome.RenderBlackImage()")
				_waitForBlackScreen.Reset()
				GUIGraphicsContext.RenderBlackImage = True
				_waitForBlackScreen.WaitOne(1000, False)
			End If
		End Sub

		''' <summary>
		''' Pre-tune checks "outsourced" to reduce code complexity
		''' </summary>
		''' <param name="channel">the channel to tune</param>
		''' <param name="doContinue">indicate to continue</param>
		''' <returns>return value when not continuing</returns>
		Private Shared Function PreTuneChecks(channel As Channel, ByRef doContinue As Boolean) As Boolean
			doContinue = False
			If _suspended AndAlso _waitonresume > 0 Then
				Log.Info("TVHome.ViewChannelAndCheck(): system just woke up...waiting {0} ms., suspended {2}", _waitonresume, _suspended)
				Thread.Sleep(_waitonresume)
			End If

			_waitForVideoReceived.Reset()

			If channel Is Nothing Then
				Log.Info("TVHome.ViewChannelAndCheck(): channel==null")
				Return False
			End If
			Log.Info("TVHome.ViewChannelAndCheck(): View channel={0}", channel.DisplayName)

			'if a channel is untunable, then there is no reason to carry on or even stop playback.
			Dim CurrentChanState As ChannelState = TvServer.GetChannelState(channel.IdChannel, Card.User)
			If CurrentChanState = ChannelState.nottunable Then
				ChannelTuneFailedNotifyUser(TvResult.AllCardsBusy, False, channel)
				Return False
			End If

			'BAV: fixing mantis bug 1263: TV starts with no video if Radio is previously ON & channel selected from TV guide
			If (Not channel.IsRadio AndAlso g_Player.IsRadio) OrElse (channel.IsRadio AndAlso Not g_Player.IsRadio) Then
				Log.Info("TVHome.ViewChannelAndCheck(): Stop g_Player")
				g_Player.[Stop](True)
			End If
			' do we stop the player when changing channel ?
			' _userChannelChanged is true if user did interactively change the channel, like with mini ch. list. etc.
			If Not _userChannelChanged Then
				If g_Player.IsTVRecording Then
					Return True
				End If
				If Not _autoTurnOnTv Then
					'respect the autoturnontv setting.
					If g_Player.IsVideo OrElse g_Player.IsDVD OrElse g_Player.IsMusic Then
						Return True
					End If
				Else
					If g_Player.IsVideo OrElse g_Player.IsDVD OrElse g_Player.IsMusic OrElse g_Player.IsCDA Then
						' || g_Player.IsRadio)
							' tell that we are zapping so exclusive mode is not going to be disabled
						g_Player.[Stop](True)
					End If
				End If
			ElseIf g_Player.IsTVRecording AndAlso _userChannelChanged Then
				'we are watching a recording, we have now issued a ch. change..stop the player.
				_userChannelChanged = False
				g_Player.[Stop](True)
			ElseIf (channel.IsTv AndAlso g_Player.IsRadio) OrElse (channel.IsRadio AndAlso g_Player.IsTV) OrElse g_Player.IsCDA OrElse g_Player.IsMusic OrElse g_Player.IsVideo Then
				g_Player.[Stop](True)
			End If

			If Card IsNot Nothing Then
				If g_Player.Playing AndAlso g_Player.IsTV AndAlso Not g_Player.IsTVRecording Then
					'modified by joboehl. Avoids other video being played instead of TV. 
					'if we're already watching this channel, then simply return
					If Card.IsTimeShifting = True AndAlso Card.IdChannel = channel.IdChannel Then
						Return True
					End If
				End If
			End If

			' if all checks passed then we won't return
			doContinue = True
			Return True
			' will be ignored
		End Function

		''' <summary>
		''' Tunes to a new channel
		''' </summary>
		''' <param name="channel"></param>
		''' <returns></returns>
		Public Shared Function ViewChannelAndCheck(channel As Channel) As Boolean
			Dim checkResult As Boolean
			Dim doContinue As Boolean

			If Not Connected Then
				Return False
			End If

			_status.Clear()

			_doingChannelChange = False

			Try
				checkResult = PreTuneChecks(channel, doContinue)
				If doContinue = False Then
					Return checkResult
				End If

				_doingChannelChange = True
				Dim succeeded As TvResult


				Dim user As IUser = New User()
				If Card IsNot Nothing Then
					user.CardId = Card.Id
				End If

				If (g_Player.Playing AndAlso g_Player.IsTimeShifting AndAlso Not g_Player.Stopped) AndAlso (g_Player.IsTV OrElse g_Player.IsRadio) Then
					_status.[Set](LiveTvStatus.WasPlaying)
				End If

				'Start timeshifting the new tv channel
				Dim server As New TvServer()
				Dim card__1 As VirtualCard
				Dim newCardId As Integer = -1

				' check which card will be used
				newCardId = server.TimeShiftingWouldUseCard(user, channel.IdChannel)

				'Added by joboehl - If any major related to the timeshifting changed during the start, restart the player.           
				If newCardId <> -1 AndAlso Card.Id <> newCardId Then
					_status.[Set](LiveTvStatus.CardChange)
					RegisterCiMenu(newCardId)
				End If

				' we need to stop player HERE if card has changed.        
				If _status.AllSet(LiveTvStatus.WasPlaying Or LiveTvStatus.CardChange) Then
					Log.Debug("TVHome.ViewChannelAndCheck(): Stopping player. CardId:{0}/{1}, RTSP:{2}", Card.Id, newCardId, Card.RTSPUrl)
					Log.Debug("TVHome.ViewChannelAndCheck(): Stopping player. Timeshifting:{0}", Card.TimeShiftFileName)
					Log.Debug("TVHome.ViewChannelAndCheck(): rebuilding graph (card changed) - timeshifting continueing.")
				End If
				If _status.IsSet(LiveTvStatus.WasPlaying) Then
					RenderBlackImage()
					g_Player.PauseGraph()
				Else
					' if CI menu is not attached due to card change, do it if graph was not playing 
					' (some handlers use polling threads that get stopped on graph stop)
					If _status.IsNotSet(LiveTvStatus.CardChange) Then
						RegisterCiMenu(newCardId)
					End If
				End If

				' if card was not changed
				If _status.IsNotSet(LiveTvStatus.CardChange) Then
						' Setup Zapping for TsReader, requesting new PAT from stream
					g_Player.OnZapping(&H80)
				End If
				Dim cardChanged As Boolean = False
				succeeded = server.StartTimeShifting(user, channel.IdChannel, card__1, cardChanged)

				If _status.IsSet(LiveTvStatus.WasPlaying) Then
					If card__1 IsNot Nothing Then
						g_Player.OnZapping(CInt(card__1.Type))
					Else
						g_Player.OnZapping(-1)
					End If
				End If


				If succeeded <> TvResult.Succeeded Then
					'timeshifting new channel failed. 
					g_Player.[Stop]()

					' ensure right channel name, even if not watchable:Navigator.Channel = channel; 
					ChannelTuneFailedNotifyUser(succeeded, _status.IsSet(LiveTvStatus.WasPlaying), channel)

					_doingChannelChange = True
					' keep fullscreen false;
						' "success"
					Return True
				End If

				If card__1 IsNot Nothing AndAlso card__1.NrOfOtherUsersTimeshiftingOnCard > 0 Then
					_status.[Set](LiveTvStatus.SeekToEndAfterPlayback)
				End If

				If cardChanged Then
					_status.[Set](LiveTvStatus.CardChange)
					If card__1 IsNot Nothing Then
						RegisterCiMenu(card__1.Id)
					End If
					_status.Reset(LiveTvStatus.WasPlaying)
				Else
					_status.Reset(LiveTvStatus.CardChange)
					_status.[Set](LiveTvStatus.SeekToEnd)
				End If

				' Update channel navigator
				If Navigator.Channel IsNot Nothing AndAlso (channel.IdChannel <> Navigator.Channel.IdChannel OrElse (Navigator.LastViewedChannel Is Nothing)) Then
					Navigator.LastViewedChannel = Navigator.Channel
				End If
				Log.Info("succeeded:{0} {1}", succeeded, card__1)
				Card = card__1
				'Moved by joboehl - Only touch the card if starttimeshifting succeeded. 
				' if needed seek to end
				If _status.IsSet(LiveTvStatus.SeekToEnd) Then
					SeekToEnd(True)
				End If

				' continue graph
				g_Player.ContinueGraph()
				If Not g_Player.Playing OrElse _status.IsSet(LiveTvStatus.CardChange) OrElse (g_Player.Playing AndAlso Not (g_Player.IsTV OrElse g_Player.IsRadio)) Then
					StartPlay()

					' if needed seek to end
					If _status.IsSet(LiveTvStatus.SeekToEndAfterPlayback) Then
						Dim dTime As Double = g_Player.Duration - 5
						g_Player.SeekAbsolute(dTime)
					End If
				End If
				Try

					TvTimeShiftPositionWatcher.SetNewChannel(channel.IdChannel)
						'ignore, error already logged
				Catch
				End Try

				_playbackStopped = False
				_doingChannelChange = False
				_ServerNotConnectedHandled = False
				Return True
			Catch ex As Exception
				Log.Debug("TvPlugin:ViewChannelandCheckV2 Exception {0}", ex.ToString())
				_doingChannelChange = False
				Card.User.Name = New User().Name
				g_Player.[Stop]()
				Card.StopTimeShifting()
				Return False
			Finally
				StopRenderBlackImage()
				_userChannelChanged = False
				FireOnChannelChangedEvent()
				Navigator.UpdateCurrentChannel()
			End Try
		End Function

		Private Shared Sub FireOnChannelChangedEvent()
			RaiseEvent OnChannelChanged()
		End Sub

		Public Shared Sub ViewChannel(channel As Channel)
			ViewChannelAndCheck(channel)
			UpdateProgressPercentageBar()
			Return
		End Sub

		''' <summary>
		''' When called this method will switch to the next TV channel
		''' </summary>
		Public Shared Sub OnNextChannel()
			Log.Info("TVHome:OnNextChannel()")
			If GUIGraphicsContext.IsFullScreenVideo Then
				' where in fullscreen so delayzap channel instead of immediatly tune..
				Dim TVWindow As TvFullScreen = DirectCast(GUIWindowManager.GetWindow(CInt(CInt(Window.WINDOW_TVFULLSCREEN))), TvFullScreen)
				If TVWindow IsNot Nothing Then
					TVWindow.ZapNextChannel()
				End If
				Return
			End If

			' Zap to next channel immediately
			Navigator.ZapToNextChannel(False)
		End Sub

		''' <summary>
		''' When called this method will switch to the last viewed TV channel   // mPod
		''' </summary>
		Public Shared Sub OnLastViewedChannel()
			Navigator.ZapToLastViewedChannel()
		End Sub

		''' <summary>
		''' Returns true if the specified window belongs to the my tv plugin
		''' </summary>
		''' <param name="windowId">id of window</param>
		''' <returns>
		''' true: belongs to the my tv plugin
		''' false: does not belong to the my tv plugin</returns>
		Public Shared Function IsTVWindow(windowId As Integer) As Boolean
			If windowId = CInt(Window.WINDOW_TV) Then
				Return True
			End If

			Return False
		End Function

		''' <summary>
		''' Gets the channel navigator that can be used for channel zapping.
		''' </summary>
		Public Shared ReadOnly Property Navigator() As ChannelNavigator
			Get
				Return m_navigator
			End Get
		End Property

		Private Shared Sub StartPlay()
			Dim benchClock As Stopwatch = Nothing
			benchClock = Stopwatch.StartNew()
			If Card Is Nothing Then
				Log.Info("tvhome:startplay card=null")
				Return
			End If
			If Card.IsScrambled Then
				Log.Info("tvhome:startplay scrambled")
				Return
			End If
			Log.Info("tvhome:startplay")
			Dim timeshiftFileName As String = Card.TimeShiftFileName
			Log.Info("tvhome:file:{0}", timeshiftFileName)

			Dim channel As TvLibrary.Interfaces.IChannel = Card.Channel
			If channel Is Nothing Then
				Log.Info("tvhome:startplay channel=null")
				Return
			End If
			Dim mediaType As g_Player.MediaType = g_Player.MediaType.TV
			If channel.IsRadio Then
				mediaType = g_Player.MediaType.Radio
			End If

			benchClock.[Stop]()
			Log.Warn("tvhome:startplay.  Phase 1 - {0} ms - Done method initialization", benchClock.ElapsedMilliseconds.ToString())
			benchClock.Reset()
			benchClock.Start()

			timeshiftFileName = TVUtil.GetFileNameForTimeshifting()
			Dim useRTSP__1 As Boolean = UseRTSP()

			Log.Info("tvhome:startplay:{0} - using rtsp mode:{1}", timeshiftFileName, useRTSP__1)

			If Not useRTSP__1 Then
				Dim tsFileExists As Boolean = False
				Dim timeout As Integer = 0
				While Not tsFileExists AndAlso timeout < 50
					tsFileExists = File.Exists(timeshiftFileName)
					If Not tsFileExists Then
						Log.Info("tvhome:startplay: waiting for TS file {0}", timeshiftFileName)
						timeout += 1
						Thread.Sleep(10)
					End If
				End While
			End If

			If Not g_Player.Play(timeshiftFileName, mediaType) Then
				StopPlayback()
			End If

			benchClock.[Stop]()
			Log.Warn("tvhome:startplay.  Phase 2 - {0} ms - Done starting g_Player.Play()", benchClock.ElapsedMilliseconds.ToString())
			benchClock.Reset()
			'benchClock.Start();
			'SeekToEnd(true);
			'Log.Warn("tvhome:startplay.  Phase 3 - {0} ms - Done seeking.", benchClock.ElapsedMilliseconds.ToString());
			'SeekToEnd(true);

			benchClock.[Stop]()
		End Sub

		Private Shared Sub SeekToEnd(zapping As Boolean)
			Dim duration As Double = g_Player.Duration
			Dim position As Double = g_Player.CurrentPosition

			Dim useRtsp__1 As Boolean = UseRTSP()

			Log.Info("tvhome:SeektoEnd({0}/{1}),{2},rtsp={3}", position, duration, zapping, useRtsp__1)
			If duration > 0 OrElse position > 0 Then
				Try
					'singleseat or  multiseat rtsp streaming....
					If Not useRtsp__1 OrElse (useRtsp__1 AndAlso zapping) Then
						g_Player.SeekAbsolute(duration)
					End If
				Catch e As Exception
					Log.[Error]("tvhome:SeektoEnd({0}, rtsp={1} exception: {2}", zapping, useRtsp__1, e.Message)
					g_Player.[Stop]()
				End Try
			End If
		End Sub

		#Region "CI Menu"

		''' <summary>
		''' Register the remoting service and attaching ciMenuHandler for server events
		''' </summary>
		Public Shared Sub RegisterCiMenu(newCardId As Integer)
			If ciMenuHandler Is Nothing Then
				Log.Debug("CiMenu: PrepareCiMenu")
				ciMenuHandler = New CiMenuHandler()
			End If
			' Check if card supports CI menu
			If newCardId <> -1 AndAlso RemoteControl.Instance.CiMenuSupported(newCardId) Then
				' opens remoting and attach local eventhandler to server event, call only once
				RemoteControl.RegisterCiMenuCallbacks(ciMenuHandler)

				' Enable CI menu handling in card
				RemoteControl.Instance.SetCiMenuHandler(newCardId, Nothing)
				Log.Debug("TvPlugin: CiMenuHandler attached to new card {0}", newCardId)
			End If
		End Sub

		''' <summary>
		''' Keyboard input for ci menu
		''' </summary>
		''' <param name="title"></param>
		''' <param name="maxLength"></param>
		''' <param name="bPassword"></param>
		''' <param name="strLine"></param>
		''' <returns></returns>
		Protected Shared Function GetKeyboard(title As String, maxLength As Integer, bPassword As Boolean, ByRef strLine As String) As Boolean
			Dim keyboard As StandardKeyboard = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_VIRTUAL_KEYBOARD)), StandardKeyboard)
			If keyboard Is Nothing Then
				Return False
			End If
			keyboard.Password = bPassword
			'keyboard.Title = title;
			keyboard.SetMaxLength(maxLength)
			keyboard.Reset()
			keyboard.Text = strLine
			keyboard.DoModal(GUIWindowManager.ActiveWindowEx)
			If keyboard.IsConfirmed Then
				strLine = keyboard.Text
				Return True
			End If
			Return False
		End Function

		''' <summary>
		''' Pass the CiMenu to TvHome so that Process can handle it in own thread
		''' </summary>
		''' <param name="Menu"></param>
		Public Shared Sub ProcessCiMenu(Menu As CiMenu)
			SyncLock CiMenuLock
				CiMenuList.Add(Menu)
				If CiMenuActive Then
					' Just suppose if a new menu is coming from CAM, last one can be trashed.
					dlgCiMenu.Reset()
				End If
				Log.Debug("ProcessCiMenu {0} {1} ", Menu, CiMenuList.Count)
			End SyncLock
		End Sub

		''' <summary>
		''' Handles all CiMenu actions from callback
		''' </summary>
		Public Shared Sub ShowCiMenu()
			SyncLock CiMenuLock
				If CiMenuActive OrElse CiMenuList.Count = 0 Then
					Return
				End If
				currentCiMenu = CiMenuList(0)
				CiMenuList.RemoveAt(0)
					' avoid re-entrance from process()
				CiMenuActive = True
			End SyncLock

			If dlgCiMenu Is Nothing Then
				dlgCiMenu = DirectCast(GUIWindowManager.GetWindow(CInt(MediaPortal.GUI.Library.GUIWindow.Window.WINDOW_DIALOG_CIMENU)), GUIDialogCIMenu)
			End If

			Select Case currentCiMenu.State
				' choices available, so show them
				Case TvLibrary.Interfaces.CiMenuState.Ready
					dlgCiMenu.Reset()
					dlgCiMenu.SetHeading(currentCiMenu.Title, currentCiMenu.Subtitle, currentCiMenu.BottomText)
					' CI Menu
					For i As Integer = 0 To currentCiMenu.NumChoices - 1
						' CI Menu Entries
						dlgCiMenu.Add(currentCiMenu.MenuEntries(i).Message)
					Next
					' take only message, numbers come from dialog
					' show dialog and wait for result       
					dlgCiMenu.DoModal(GUIWindowManager.ActiveWindow)
					If currentCiMenu.State <> TvLibrary.Interfaces.CiMenuState.[Error] Then
						If dlgCiMenu.SelectedId <> -1 Then
							TVHome.Card.SelectCiMenu(Convert.ToByte(dlgCiMenu.SelectedId))
						Else
							If CiMenuList.Count = 0 Then
								' Another menu is pending, do not answer...
								TVHome.Card.SelectCiMenu(0)
								' 0 means "back"
							End If
						End If
					Else
							' in case of error close the menu
						TVHome.Card.CloseMenu()
					End If
					Exit Select

				' errors and menu options with no choices
				Case TvLibrary.Interfaces.CiMenuState.[Error], TvLibrary.Interfaces.CiMenuState.NoChoices

					If _dialogNotify Is Nothing Then
						_dialogNotify = DirectCast(GUIWindowManager.GetWindow(CInt(Window.WINDOW_DIALOG_NOTIFY)), GUIDialogNotify)
					End If
					If _dialogNotify IsNot Nothing Then
						_dialogNotify.Reset()
						_dialogNotify.ClearAll()
						_dialogNotify.SetHeading(currentCiMenu.Title)
						_dialogNotify.SetText([String].Format("{0}" & vbCr & vbLf & "{1}", currentCiMenu.Subtitle, currentCiMenu.BottomText))
						_dialogNotify.TimeOut = 2
						' seconds
						_dialogNotify.DoModal(GUIWindowManager.ActiveWindow)
					End If
					Exit Select

				' requests require users input so open keyboard
				Case TvLibrary.Interfaces.CiMenuState.Request
					Dim result As [String] = ""
					If GetKeyboard(currentCiMenu.RequestText, currentCiMenu.AnswerLength, currentCiMenu.Password, result) = True Then
							' send answer, cancel=false
						TVHome.Card.SendMenuAnswer(False, result)
					Else
							' cancel request 
						TVHome.Card.SendMenuAnswer(True, Nothing)
					End If
					Exit Select
				Case CiMenuState.Close
					If _dialogNotify IsNot Nothing Then
						Dim msg As New GUIMessage(GUIMessage.MessageType.GUI_MSG_WINDOW_DEINIT, _dialogNotify.GetID, 0, 0, 0, 0, _
							Nothing)
							' Send a de-init msg to hide the current notify dialog
						_dialogNotify.OnMessage(msg)
					End If
					If dlgCiMenu IsNot Nothing Then
						Dim msg As New GUIMessage(GUIMessage.MessageType.GUI_MSG_WINDOW_DEINIT, dlgCiMenu.GetID, 0, 0, 0, 0, _
							Nothing)
							' Send a de-init msg to hide the current CI menu dialog
						dlgCiMenu.OnMessage(msg)
					End If
					Exit Select
			End Select

			CiMenuActive = False
			' finished
			currentCiMenu = Nothing
			' reset menu
		End Sub

		#End Region
	End Class
End Namespace

#Region "CI Menu"

''' <summary>
''' Handler class for gui interactions of ci menu
''' </summary>
Public Class CiMenuHandler
	Inherits CiMenuCallbackSink
	''' <summary>
	''' eventhandler to show CI Menu dialog
	''' </summary>
	''' <param name="Menu"></param>
	Protected Overrides Sub CiMenuCallback(Menu As CiMenu)
		Try
			Log.Debug("Callback from tvserver {0}", Menu.Title)

			' pass menu to calling dialog
			TvPlugin.TVHome.ProcessCiMenu(Menu)
		Catch
			Menu = New CiMenu("Remoting Exception", "Communication with server failed", Nothing, TvLibrary.Interfaces.CiMenuState.[Error])
			' pass menu to calling dialog
			TvPlugin.TVHome.ProcessCiMenu(Menu)
		End Try
	End Sub
End Class

#End Region
