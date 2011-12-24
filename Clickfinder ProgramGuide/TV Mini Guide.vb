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

Imports System.Collections.Generic
Imports System.Data.SqlTypes
Imports System.Diagnostics
Imports System.Text
Imports MediaPortal.Configuration
Imports MediaPortal.Dialogs
Imports MediaPortal.GUI.Library
Imports MediaPortal.Player
Imports MediaPortal.Profile
Imports MediaPortal.Util
Imports TvControl
Imports TvDatabase
Imports TvLibrary.Interfaces
Imports Action = MediaPortal.GUI.Library.Action

#End Region

Namespace TvPlugin
    ''' <summary>
    ''' GUIMiniGuide
    ''' </summary>
    ''' 
    Public Class TvMiniGuide
        Inherits GUIDialogWindow
        ' Member variables                                  
        <SkinControl(34)> _
        Protected cmdExit As GUIButtonControl = Nothing

        <SkinControl(35)> _
        Protected lstChannelsNoStateIcons As GUIListControl = Nothing

        <SkinControl(36)> _
        Protected spinGroup As GUISpinControl = Nothing

        <SkinControl(37)> _
        Protected lstChannelsWithStateIcons As GUIListControl = Nothing

        Protected lstChannels As GUIListControl = Nothing

        Private _canceled As Boolean = False
        '
        '    private bool _running = false;
        '    private int _parentWindowID = 0;
        '    private GUIWindow _parentWindow = null;
        '    

        Private _tvGroupChannelListCache As Dictionary(Of Integer, List(Of Channel)) = Nothing

        Private _channelGroupList As List(Of ChannelGroup) = Nothing
        Private _selectedChannel As Channel
        Private _zap As Boolean = True
        Private benchClock As Stopwatch = Nothing
        Private _channelList As New List(Of Channel)()

        Private _byIndex As Boolean = False
        Private _showChannelNumber As Boolean = False
        Private _channelNumberMaxLength As Integer = 3

        Private _nextEPGupdate As New Dictionary(Of Integer, DateTime)()
        Private _listNowNext As New Dictionary(Of Integer, Dictionary(Of Integer, NowAndNext))()

        Private ReadOnly PathIconNoTune As String = GUIGraphicsContext.Skin + "\Media\remote_blue.png"
        Private ReadOnly PathIconTimeshift As String = GUIGraphicsContext.Skin + "\Media\remote_yellow.png"
        Private ReadOnly PathIconRecord As String = GUIGraphicsContext.Skin + "\Media\remote_red.png"
        ' fetch localized ID's only once from XML file
        Private ReadOnly local736 As String = GUILocalizeStrings.[Get](736)
        ' No data available
        Private ReadOnly local789 As String = GUILocalizeStrings.[Get](789)
        ' Now:
        Private ReadOnly local790 As String = GUILocalizeStrings.[Get](790)
        ' Next:
        Private ReadOnly local1054 As String = GUILocalizeStrings.[Get](1054)
        ' (recording)
        Private ReadOnly local1055 As String = GUILocalizeStrings.[Get](1055)
        ' (timeshifting)
        Private ReadOnly local1056 As String = GUILocalizeStrings.[Get](1056)
        ' (unavailable)    
        Private sb As New StringBuilder()
        Private sbTmp As New StringBuilder()

#Region "Serialisation"

        Private Sub LoadSettings()
            Using xmlreader As Settings = New MPSettings()
                _byIndex = xmlreader.GetValueAsBool("mytv", "byindex", True)
                _showChannelNumber = xmlreader.GetValueAsBool("mytv", "showchannelnumber", False)
                _channelNumberMaxLength = xmlreader.GetValueAsInt("mytv", "channelnumbermaxlength", 3)
            End Using
        End Sub

#End Region

        ''' <summary>
        ''' Constructor
        ''' </summary>
        Public Sub New()
            GetID = CInt(Window.WINDOW_MINI_GUIDE)
        End Sub

        Public Overrides ReadOnly Property SupportsDelayedLoad() As Boolean
            Get
                Return False
            End Get
        End Property

        ''' <summary>
        ''' Gets a value indicating whether this instance is tv.
        ''' </summary>
        ''' <value><c>true</c> if this instance is tv; otherwise, <c>false</c>.</value>
        Public Overrides ReadOnly Property IsTv() As Boolean
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Gets a value indicating whether the dialog was canceled. 
        ''' </summary>
        ''' <value><c>true</c> if dialog was canceled without a selection</value>
        Public ReadOnly Property Canceled() As Boolean
            Get
                Return _canceled
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the selected channel.
        ''' </summary>
        ''' <value>The selected channel.</value>
        Public Property SelectedChannel() As Channel
            Get
                Return _selectedChannel
            End Get
            Set(ByVal value As Channel)
                _selectedChannel = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a value indicating whether [auto zap].
        ''' </summary>
        ''' <value><c>true</c> if [auto zap]; otherwise, <c>false</c>.</value>
        Public Property AutoZap() As Boolean
            Get
                Return _zap
            End Get
            Set(ByVal value As Boolean)
                _zap = value
            End Set
        End Property

        ''' <summary>
        ''' Init method
        ''' </summary>
        ''' <returns></returns>
        Public Overrides Function Init() As Boolean
            Dim bResult As Boolean = Load(GUIGraphicsContext.Skin + "\TVMiniGuide.xml")

            GetID = CInt(Window.WINDOW_MINI_GUIDE)
            'GUILayerManager.RegisterLayer(this, GUILayerManager.LayerType.MiniEPG);
            _canceled = True
            LoadSettings()
            Return bResult
        End Function

        '
        '    /// <summary>
        '    /// Renderer
        '    /// </summary>
        '    /// <param name="timePassed"></param>
        '    public override void Render(float timePassed)
        '    {
        '      base.Render(timePassed); // render our controls to the screen
        '    }
        '    


        Private Sub GetChannels(ByVal refresh As Boolean)
            If refresh OrElse _channelList Is Nothing Then
                _channelList = New List(Of Channel)()
            End If

            If _channelList.Count = 0 Then
                Try
                    If TVHome.Navigator.CurrentGroup IsNot Nothing Then
                        For Each chan As GroupMap In TVHome.Navigator.CurrentGroup.ReferringGroupMap()
                            Dim ch As Channel = chan.ReferencedChannel()
                            If ch.VisibleInGuide AndAlso ch.IsTv Then
                                _channelList.Add(ch)
                            End If
                        Next
                    End If
                Catch
                End Try

                If _channelList.Count = 0 Then
                    Dim newChannel As New Channel(False, True, 0, DateTime.MinValue, False, DateTime.MinValue, _
                     0, True, "", GUILocalizeStrings.[Get](911))
                    For i As Integer = 0 To 9
                        _channelList.Add(newChannel)
                    Next
                End If
            End If
        End Sub

        '
        '    /// <summary>
        '    /// On close
        '    /// </summary>
        '    private void Close()
        '    {
        '      GUIWindowManager.IsSwitchingToNewWindow = true;
        '      lock (this)
        '      {
        '        GUIMessage msg = new GUIMessage(GUIMessage.MessageType.GUI_MSG_WINDOW_DEINIT, GetID, 0, 0, 0, 0, null);
        '        OnMessage(msg);
        '
        '        GUIWindowManager.UnRoute();
        '        _running = false;
        '        _parentWindow = null;
        '      }
        '      GUIWindowManager.IsSwitchingToNewWindow = false;
        '      GUILayerManager.UnRegisterLayer(this);
        '    }
        '    


        ''' <summary>
        ''' On Message
        ''' </summary>
        ''' <param name="message"></param>
        ''' <returns></returns>
        Public Overrides Function OnMessage(ByVal message As GUIMessage) As Boolean
            Select Case message.Message
                Case GUIMessage.MessageType.GUI_MSG_CLICKED
                    If True Then
                        If message.SenderControlId = 35 OrElse message.SenderControlId = 37 Then
                            ' listbox
                            If CInt(Action.ActionType.ACTION_SELECT_ITEM) = message.Param1 Then
                                ' switching logic
                                SelectedChannel = DirectCast(lstChannels.SelectedListItem.TVTag, Channel)

                                Dim changeChannel As Channel = Nothing
                                If AutoZap Then
                                    If (TVHome.Navigator.Channel.IdChannel <> SelectedChannel.IdChannel) OrElse g_Player.IsTVRecording Then
                                        Dim tvChannelList As List(Of Channel) = GetChannelListByGroup()
                                        If tvChannelList IsNot Nothing Then
                                            changeChannel = DirectCast(tvChannelList(lstChannels.SelectedListItemIndex), Channel)
                                        End If
                                    End If
                                End If
                                _canceled = False
                                PageDestroy()

                                'This one shows the zapOSD when changing channel from mini GUIDE, this is currently unwanted.
                                '
                                '                TvFullScreen TVWindow = (TvFullScreen)GUIWindowManager.GetWindow((int)(int)GUIWindow.Window.WINDOW_TVFULLSCREEN);
                                '                if (TVWindow != null) TVWindow.UpdateOSD(changeChannel.Name);                
                                '                


                                TVHome.UserChannelChanged = True

                                If changeChannel IsNot Nothing Then
                                    TVHome.ViewChannel(changeChannel)
                                End If
                            End If
                        ElseIf message.SenderControlId = 36 Then
                            ' spincontrol
                            ' switch group              
                            OnGroupChanged()
                        ElseIf message.SenderControlId = 34 Then
                            ' exit button
                            ' exit
                            _canceled = True
                            PageDestroy()
                        End If
                        Exit Select
                    End If
            End Select
            Return MyBase.OnMessage(message)
        End Function

        ''' <summary>
        ''' On action
        ''' </summary>
        ''' <param name="action"></param>
        Public Overrides Sub OnAction(ByVal action__1 As Action)
            Select Case action__1.wID
                Case Action.ActionType.ACTION_CONTEXT_MENU
                    '_running = false;
                    PageDestroy()
                    Return
                Case Action.ActionType.ACTION_PREVIOUS_MENU
                    '_running = false;
                    _canceled = True
                    PageDestroy()
                    Return
                Case Action.ActionType.ACTION_MOVE_LEFT, Action.ActionType.ACTION_TVGUIDE_PREV_GROUP
                    ' switch group
                    spinGroup.MoveUp()
                    Return
                Case Action.ActionType.ACTION_MOVE_RIGHT, Action.ActionType.ACTION_TVGUIDE_NEXT_GROUP
                    ' switch group
                    spinGroup.MoveDown()
                    Return
            End Select
            MyBase.OnAction(action__1)
        End Sub

        ''' <summary>
        ''' Page gets destroyed
        ''' </summary>
        ''' <param name="new_windowId"></param>
        Protected Overrides Sub OnPageDestroy(ByVal new_windowId As Integer)
            'Log.Debug("TvMiniGuide: OnPageDestroy");
            MyBase.OnPageDestroy(new_windowId)
            '_running = false;
        End Sub

        ''' <summary>
        ''' Page gets loaded
        ''' </summary>
        Protected Overrides Sub OnPageLoad()
            'Stopwatch bClock = Stopwatch.StartNew();

            'Log.Debug("TvMiniGuide: onpageload");
            ' following line should stay. Problems with OSD not
            ' appearing are already fixed elsewhere
            'GUILayerManager.RegisterLayer(this, GUILayerManager.LayerType.MiniEPG);
            AllocResources()
            ResetAllControls()
            ' make sure the controls are positioned relevant to the OSD Y offset            
            lstChannels = getChannelList()

            If lstChannelsWithStateIcons IsNot Nothing Then
                lstChannelsWithStateIcons.Visible = False
            End If
            lstChannels.Visible = True

            spinGroup.CycleItems = True

            FillChannelList()
            FillGroupList()
            MyBase.OnPageLoad()

            'Log.Debug("TvMiniGuide: onpageload took {0} msec", bClock.ElapsedMilliseconds);
        End Sub

        Private Function getChannelList() As GUIListControl
            Dim lstChannels As GUIListControl = Nothing

            If TVHome.ShowChannelStateIcons() AndAlso lstChannelsWithStateIcons IsNot Nothing Then
                lstChannels = lstChannelsWithStateIcons
            Else
                lstChannels = lstChannelsNoStateIcons
            End If

            Return lstChannels
        End Function

        Private Sub OnGroupChanged()
            Dim bClock As Stopwatch = Stopwatch.StartNew()

            GUIWaitCursor.Show()
            TVHome.Navigator.SetCurrentGroup(spinGroup.Value)
            GUIPropertyManager.SetProperty("#TV.Guide.Group", spinGroup.GetLabel())
            lstChannels.Clear()
            FillChannelList()
            GUIWaitCursor.Hide()

            Log.Debug("OnGroupChanged {0} took {1} msec", spinGroup.Value, bClock.ElapsedMilliseconds)

        End Sub

        ''' <summary>
        ''' Fill up the list with groups
        ''' </summary>
        Public Sub FillGroupList()
            benchClock = Stopwatch.StartNew()

            Dim current As ChannelGroup = Nothing
            _channelGroupList = TVHome.Navigator.Groups
            ' empty list of channels currently in the 
            ' spin control
            spinGroup.Reset()
            ' start to fill them up again
            For i As Integer = 0 To _channelGroupList.Count - 1
                current = _channelGroupList(i)
                spinGroup.AddLabel(current.GroupName, i)
                ' set selected
                If current.GroupName.CompareTo(TVHome.Navigator.CurrentGroup.GroupName) = 0 Then
                    spinGroup.Value = i
                End If
            Next

            If _channelGroupList.Count < 2 Then
                spinGroup.Visible = False
            End If

            benchClock.[Stop]()
            Log.Debug("TvMiniGuide: FillGroupList finished after {0} ms", benchClock.ElapsedMilliseconds.ToString())
        End Sub

        Private Function GetChannelListByGroup() As List(Of Channel)
            Dim idGroup As Integer = TVHome.Navigator.CurrentGroup.IdGroup

            If _tvGroupChannelListCache Is Nothing Then
                _tvGroupChannelListCache = New Dictionary(Of Integer, List(Of Channel))()
            End If

            Dim channels As List(Of Channel) = Nothing
            If _tvGroupChannelListCache.TryGetValue(idGroup, channels) Then
                'already in cache ? then return it.      
                Log.Debug("TvMiniGuide: GetChannelListByGroup returning cached version of channels.")
                Return channels
            Else
                'not in cache, fetch it and update cache, then return.
                Dim layer As New TvBusinessLayer()
                Dim tvChannelList As List(Of Channel) = layer.GetTVGuideChannelsForGroup(idGroup)

                If tvChannelList IsNot Nothing Then
                    Log.Debug("TvMiniGuide: GetChannelListByGroup caching channels from DB.")
                    _tvGroupChannelListCache.Add(idGroup, tvChannelList)
                    Return tvChannelList
                End If
            End If
            Return New List(Of Channel)()
        End Function

        ''' <summary>
        ''' Fill the list with channels
        ''' </summary>
        Public Sub FillChannelList()
            Dim tvChannelList As List(Of Channel) = GetChannelListByGroup()

            benchClock = Stopwatch.StartNew()

            Dim nextEPGupdate As DateTime = GetNextEpgUpdate()
            Dim listNowNext As Dictionary(Of Integer, NowAndNext) = GetNowAndNext(tvChannelList, nextEPGupdate)

            benchClock.[Stop]()
            Log.Debug("TvMiniGuide: FillChannelList retrieved {0} programs for {1} channels in {2} ms", listNowNext.Count, tvChannelList.Count, benchClock.ElapsedMilliseconds.ToString())

            Dim item As GUIListItem = Nothing
            Dim ChannelLogo As String = ""
            'List<int> RecChannels = null;
            'List<int> TSChannels = null;
            Dim SelectedID As Integer = 0
            Dim channelID As Integer = 0
            Dim DisplayStatusInfo As Boolean = True


            Dim tvChannelStatesList As Dictionary(Of Integer, ChannelState) = Nothing

            If TVHome.ShowChannelStateIcons() Then
                benchClock.Reset()
                benchClock.Start()

                If TVHome.Navigator.CurrentGroup.GroupName.Equals(TvConstants.TvGroupNames.AllChannels) OrElse (Not g_Player.IsTV AndAlso Not g_Player.Playing) Then
                    'we have no way of using the cached channelstates on the server in the following situations.
                    ' 1) when the "all channels" group is selected - too many channels.
                    ' 2) when user is not timeshifting - no user object on the server.
                    Dim currentUser As New User()
                    tvChannelStatesList = TVHome.TvServer.GetAllChannelStatesForGroup(TVHome.Navigator.CurrentGroup.IdGroup, currentUser)
                Else
                    ' use the more speedy approach
                    ' ask the server of the cached list of channel states corresponding to the user.
                    tvChannelStatesList = TVHome.TvServer.GetAllChannelStatesCached(TVHome.Card.User)

                    If tvChannelStatesList Is Nothing Then
                        'slow approach.
                        tvChannelStatesList = TVHome.TvServer.GetAllChannelStatesForGroup(TVHome.Navigator.CurrentGroup.IdGroup, TVHome.Card.User)
                    End If
                End If

                benchClock.[Stop]()
                If tvChannelStatesList IsNot Nothing Then
                    Log.Debug("TvMiniGuide: FillChannelList - {0} channel states for group retrieved in {1} ms", Convert.ToString(tvChannelStatesList.Count), benchClock.ElapsedMilliseconds.ToString())
                End If
            End If

            For i As Integer = 0 To tvChannelList.Count - 1
                Dim CurrentChan As Channel = tvChannelList(i)

                If CurrentChan.VisibleInGuide Then
                    Dim CurrentChanState As ChannelState = ChannelState.tunable
                    channelID = CurrentChan.IdChannel
                    If TVHome.ShowChannelStateIcons() Then
                        If Not tvChannelStatesList.TryGetValue(channelID, CurrentChanState) Then
                            CurrentChanState = ChannelState.tunable
                        End If
                    End If

                    'StringBuilder sb = new StringBuilder();
                    sb.Length = 0
                    item = New GUIListItem("")
                    ' store here as it is not needed right now - please beat me later..
                    item.TVTag = CurrentChan

                    sb.Append(CurrentChan.DisplayName)
                    ChannelLogo = Utils.GetCoverArt(Thumbs.TVChannel, CurrentChan.DisplayName)

                    ' if we are watching this channel mark it
                    If TVHome.Navigator IsNot Nothing AndAlso TVHome.Navigator.Channel IsNot Nothing AndAlso TVHome.Navigator.Channel.IdChannel = channelID Then
                        item.IsRemote = True
                        SelectedID = lstChannels.Count
                    End If

                    If Not String.IsNullOrEmpty(ChannelLogo) Then
                        item.IconImageBig = ChannelLogo
                        item.IconImage = ChannelLogo
                    Else
                        item.IconImageBig = String.Empty
                        item.IconImage = String.Empty
                    End If

                    If DisplayStatusInfo Then
                        Dim showChannelStateIcons As Boolean = (TVHome.ShowChannelStateIcons() AndAlso lstChannelsWithStateIcons IsNot Nothing)

                        Select Case CurrentChanState
                            Case ChannelState.nottunable
                                item.IsPlayed = True
                                If showChannelStateIcons Then
                                    item.PinImage = Thumbs.TvIsUnavailableIcon
                                Else
                                    sb.Append(" ")
                                    sb.Append(local1056)
                                End If
                                Exit Select
                            Case ChannelState.timeshifting
                                If showChannelStateIcons Then
                                    item.PinImage = Thumbs.TvIsTimeshiftingIcon
                                Else
                                    sb.Append(" ")
                                    sb.Append(local1055)
                                End If
                                Exit Select
                            Case ChannelState.recording
                                If showChannelStateIcons Then
                                    item.PinImage = Thumbs.TvIsRecordingIcon
                                Else
                                    sb.Append(" ")
                                    sb.Append(local1054)
                                End If
                                Exit Select
                            Case Else
                                item.IsPlayed = False
                                If showChannelStateIcons Then
                                    item.PinImage = Thumbs.TvIsAvailableIcon
                                End If
                                Exit Select
                        End Select
                    End If
                    'StringBuilder sbTmp = new StringBuilder();          
                    sbTmp.Length = 0

                    Dim currentNowAndNext As NowAndNext = Nothing
                    Dim hasNowNext As Boolean = listNowNext.TryGetValue(channelID, currentNowAndNext)

                    If hasNowNext Then
                        If Not String.IsNullOrEmpty(currentNowAndNext.TitleNow) Then
                            TVUtil.TitleDisplay(sbTmp, currentNowAndNext.TitleNow, currentNowAndNext.EpisodeName, currentNowAndNext.SeriesNum, currentNowAndNext.EpisodeNum, currentNowAndNext.EpisodePart)
                        Else
                            sbTmp.Append(local736)
                        End If
                    Else
                        sbTmp.Append(local736)
                    End If

                    item.Label2 = sbTmp.ToString()
                    sbTmp.Insert(0, local789)
                    item.Label3 = sbTmp.ToString()

                    sbTmp.Length = 0

                    If _showChannelNumber = True Then
                        sb.Append(" - ")
                        If Not _byIndex Then
                            For Each detail As TuningDetail In tvChannelList(i).ReferringTuningDetail()
                                sb.Append(detail.ChannelNumber)
                            Next
                        Else
                            sb.Append(i + 1)
                        End If
                    End If

                    If hasNowNext Then
                        ' if the "Now" DB entry is in the future we set MinValue intentionally to avoid wrong percentage calculations
                        Dim startTime As DateTime = currentNowAndNext.NowStartTime
                        If startTime <> SqlDateTime.MinValue.Value Then
                            Dim endTime As DateTime = currentNowAndNext.NowEndTime
                            sb.Append(" - ")
                            sb.Append(CalculateProgress(startTime, endTime).ToString())
                            sb.Append("%")

                            If endTime < nextEPGupdate OrElse nextEPGupdate = DateTime.MinValue Then
                                nextEPGupdate = endTime
                                SetNextEpgUpdate(endTime)
                            End If
                        End If
                    End If



                    If hasNowNext AndAlso listNowNext(channelID).IdProgramNext <> -1 Then
                        TVUtil.TitleDisplay(sbTmp, currentNowAndNext.TitleNext, currentNowAndNext.EpisodeNameNext, currentNowAndNext.SeriesNumNext, currentNowAndNext.EpisodeNumNext, currentNowAndNext.EpisodePartNext)
                    Else
                        sbTmp.Append(local736)
                    End If

                    item.Label2 = sb.ToString()

                    sbTmp.Insert(0, local790)

                    item.Label = sbTmp.ToString()

                    lstChannels.Add(item)
                End If
            Next
            benchClock.[Stop]()
            Log.Debug("TvMiniGuide: State check + filling completed after {0} ms", benchClock.ElapsedMilliseconds.ToString())
            lstChannels.SelectedListItemIndex = SelectedID

            If lstChannels.GetID = 37 Then
                Dim msg As New GUIMessage(GUIMessage.MessageType.GUI_MSG_SETFOCUS, GetID, 0, 37, 0, 0, _
                 Nothing)
                OnMessage(msg)
            End If

            sb.Length = 0
            sbTmp.Length = 0
        End Sub

        Private Function GetNowAndNext(ByVal tvChannelList As List(Of Channel), ByVal nextEPGupdate As DateTime) As Dictionary(Of Integer, NowAndNext)
            Dim getNowAndNext__1 As New Dictionary(Of Integer, NowAndNext)()
            Dim idGroup As Integer = TVHome.Navigator.CurrentGroup.IdGroup


            Dim layer As New TvBusinessLayer()
            If _listNowNext.TryGetValue(idGroup, getNowAndNext__1) Then
                Dim updateNow As Boolean = (DateTime.Now >= nextEPGupdate)
                If updateNow Then
                    getNowAndNext__1 = layer.GetNowAndNext(tvChannelList)
                    _listNowNext(idGroup) = getNowAndNext__1
                End If
            Else
                getNowAndNext__1 = layer.GetNowAndNext(tvChannelList)
                _listNowNext.Add(idGroup, getNowAndNext__1)
            End If
            Return getNowAndNext__1
        End Function

        Private Sub SetNextEpgUpdate(ByVal nextEPGupdate As DateTime)
            Dim idGroup As Integer = TVHome.Navigator.CurrentGroup.IdGroup
            If _nextEPGupdate.ContainsKey(idGroup) Then
                _nextEPGupdate(idGroup) = nextEPGupdate
            Else
                _nextEPGupdate.Add(idGroup, nextEPGupdate)
            End If
        End Sub

        Private Function GetNextEpgUpdate() As DateTime
            Dim nextEPGupdate As DateTime = DateTime.MinValue
            Dim idGroup As Integer = TVHome.Navigator.CurrentGroup.IdGroup

            _nextEPGupdate.TryGetValue(idGroup, nextEPGupdate)
            Return nextEPGupdate
        End Function

        ''' <summary>
        ''' Get current tv program
        ''' </summary>
        ''' <param name="prog"></param>
        ''' <returns></returns>
        Private Function CalculateProgress(ByVal start As DateTime, ByVal [end] As DateTime) As Double
            Dim length As TimeSpan = [end] - start
            Dim passed As TimeSpan = DateTime.Now - start
            Dim fprogress As Double = 0
            If length.TotalMinutes > 0 Then
                fprogress = (passed.TotalMinutes / length.TotalMinutes) * 100
                fprogress = Math.Floor(fprogress)
                If fprogress > 100.0F Then
                    fprogress = 100.0F
                End If
                If fprogress < 1.0F Then
                    fprogress = 0
                End If
            End If
            Return fprogress
        End Function

        '
        '    /// <summary>
        '    /// Do this modal
        '    /// </summary>
        '    /// <param name="dwParentId"></param>
        '    public void DoModal(int dwParentId)
        '    {
        '      //Log.Debug("TvMiniGuide: domodal");
        '      _parentWindowID = dwParentId;
        '      _parentWindow = GUIWindowManager.GetWindow(_parentWindowID);
        '      if (null == _parentWindow)
        '      {
        '        //Log.Debug("TvMiniGuide: parentwindow = null");
        '        _parentWindowID = 0;
        '        return;
        '      }
        '
        '      GUIWindowManager.IsSwitchingToNewWindow = true;
        '      GUIWindowManager.RouteToWindow(GetID);
        '
        '      // activate this window...
        '      GUIMessage msg = new GUIMessage(GUIMessage.MessageType.GUI_MSG_WINDOW_INIT, GetID, 0, 0, -1, 0, null);
        '      OnMessage(msg);
        '
        '      GUIWindowManager.IsSwitchingToNewWindow = false;
        '      _running = true;
        '      GUILayerManager.RegisterLayer(this, GUILayerManager.LayerType.Dialog);
        '      while (_running && GUIGraphicsContext.CurrentState == GUIGraphicsContext.State.RUNNING)
        '      {
        '        GUIWindowManager.Process();
        '      }
        '
        '      Close();
        '    }
        '
        '    // Overlay IRenderLayer members
        '
        '    #region IRenderLayer
        '
        '    public bool ShouldRenderLayer()
        '    {
        '      //TVHome.SendHeartBeat(); //not needed, now sent from tvoverlay.cs
        '      return true;
        '    }
        '
        '    public void RenderLayer(float timePassed)
        '    {
        '      if (_running)
        '      {
        '        Render(timePassed);
        '      }
        '    }
        '
        '    #endregion
        '    

    End Class
End Namespace
