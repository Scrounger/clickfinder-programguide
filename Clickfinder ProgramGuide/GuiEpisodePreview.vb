Imports MediaPortal.GUI.Library
Imports TvDatabase
Imports Gentle.Framework
Imports ClickfinderProgramGuide.Helper
Imports ClickfinderProgramGuide.TvDatabase
Imports MediaPortal.Configuration
Imports System.Threading
Imports Gentle.Common
Imports MediaPortal.Dialogs
Imports System.Linq
Imports enrichEPG.TvDatabase
Imports enrichEPG.Database
Imports enrichEPG
Imports System.Text

Namespace ClickfinderProgramGuide
    Public Class EpisodePreviewGuiWindow
        Inherits GUIWindow


#Region "Skin Controls"

        'Buttons

        <SkinControlAttribute(5)> Protected _btnAllMovies As GUIButtonControl = Nothing

        'ProgressBar
        <SkinControlAttribute(8)> Protected _SeriesProgressBar As GUIAnimation = Nothing
        <SkinControlAttribute(9)> Protected _RepeatsProgressBar As GUIAnimation = Nothing

        'ListControl
        <SkinControlAttribute(10)> Protected _SeriesList As GUIListControl = Nothing
        <SkinControlAttribute(30)> Protected _RepeatsList As GUIListControl = Nothing

        'Image, sonst geflacker bug ?!
        <SkinControlAttribute(40)> Protected _SeriesPoster As GUIImage = Nothing

#End Region

#Region "Members"
        Private Shared _layer As New TvBusinessLayer
        Private ThreadSeriesFill As Threading.Thread
        Private ThreadRepeatsFill As Threading.Thread
        Private ThreadSeriesInfoFill As Threading.Thread
        Private _selectedListItemIndex As Integer

        Friend Shared _selectedSeries As TVMovieProgram
        Friend Shared _ShowEpisodes As Boolean

        Private _LastFocusedSeriesIndex As Integer
        Private _LastFocusedEpisodeIndex As Integer

        Private _allEpisodesList As New List(Of TVMovieProgram)
        Private _allSeriesList As New List(Of TVMovieProgram)
        Private _SeriesListItems As New List(Of GUIListItem)
        Private _EpisodesListItems As New List(Of GUIListItem)
        Private _EpisodeReapeatsList As New List(Of TVMovieProgram)
        Private _SeriesInfosList As New List(Of EpisodePreview_SeriesItem)
#End Region

#Region "Constructors"
        Public Sub New()
        End Sub
#End Region

#Region "GUI Properties"
        Public Overloads Overrides Property GetID() As Integer
            Get
                Return 1656544657
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property


        Public Overloads Overrides Function Init() As Boolean
            'Beim initialisieren des Plugin den Screen laden

            Return Load(GUIGraphicsContext.Skin + "\ClickfinderProgramGuideEpisodePreview.xml")
        End Function

        Public Overrides Function GetModuleName() As String
            Return "Clickfinder ProgramGuide Episode Preview"
        End Function

#End Region

#Region "GUI Events"
        Protected Overrides Sub OnPageLoad()
            MyBase.OnPageLoad()
            GUIWindowManager.NeedRefresh()

            Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowSeriesProgressBar)
            _ProgressBarThread.Start()

            Helper.CheckConnectionState()

            MyLog.Info("")
            MyLog.Info("")
            MyLog.Info("[EpisodePreviewGuiWindow] -------------[OPEN]-------------")
            GuiLayout.SetSettingLastUpdateProperty()

            Translator.SetProperty("#EpisodePreviewLabel", "")
            Translator.SetProperty("#EpisodesPreviewSeriesHeaderLabel", "")
            Translator.SetProperty("#EpisodesPreviewEpisodeDescription", "")
            Translator.SetProperty("#EpisodesPreviewLabel1", "")
            Translator.SetProperty("#EpisodesPreviewLabel2", "")
            Translator.SetProperty("#EpisodesPreviewLabel3", "")
            Translator.SetProperty("#EpisodesPreviewSeriesRatingStar", "")
            Translator.SetProperty("#EpisodesPreviewEpisodeDescription", "")
            Translator.SetProperty("#EpisodesPreviewSeriesSummary", "")
            Translator.SetProperty("#EpisodesPreviewTitle", "")
            Translator.SetProperty("#EpisodesPreviewEpisodeDescription", "")

            _SeriesPoster.SetFileName("")

            'Kein load parameter
            If _loadParameter = String.Empty Then
                'Serienansicht
                If _ShowEpisodes = False Then

                    ClearSeriesInfos()
                    LoadAllLists()

                    If _LastFocusedSeriesIndex = 0 Then
                        _selectedListItemIndex = 0
                    End If

                    'erstes Element laden wegen SeriesInfo
                    If _LastFocusedSeriesIndex < 1 Then
                        _selectedSeries = _allSeriesList(0)
                    End If

                    _EpisodesListItems.Clear()

                    AbortRunningThreads()

                    ThreadSeriesFill = New Threading.Thread(AddressOf FillSeriesList)
                    ThreadSeriesFill.IsBackground = True
                    ThreadSeriesFill.Start()

                Else
                    'Epsidoen der Serie anzeigen
                    AbortRunningThreads()

                    '_selectedSeries = TVMovieProgram.Retrieve(_EpisodesListItems(0).ItemId)

                    ThreadSeriesFill = New Threading.Thread(AddressOf FillEpisodesList)
                    ThreadSeriesFill.IsBackground = True
                    ThreadSeriesFill.Start()
                End If

            Else
                'Hyyplerink Parameter
                MyLog.Info("[EpisodePreviewGuiWindow] Hyperlink parameter: {0}", _loadParameter)

                'Hyperlink Parameter: idSeries
                If InStr(_loadParameter, "idSeries: ") > 0 Then

                    LoadAllLists()

                    _EpisodesListItems.Clear()
                    _selectedListItemIndex = 0

                    Dim sb As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(TVMovieProgram))
                    sb.AddConstraint([Operator].Equals, "idSeries", CInt(Replace(_loadParameter, "idSeries: ", "")))
                    sb.AddConstraint([Operator].Equals, "local", False)
                    Dim stmt As SqlStatement = sb.GetStatement(True)
                    Dim _SeriesFoundList As IList(Of TVMovieProgram) = ObjectFactory.GetCollection(GetType(TVMovieProgram), stmt.Execute())

                    'Serie gefunden
                    If _SeriesFoundList.Count > 0 Then
                        _selectedSeries = _SeriesFoundList(0)

                        _ShowEpisodes = True

                        _LastFocusedEpisodeIndex = 0

                        AbortRunningThreads()

                        ThreadSeriesFill = New Threading.Thread(AddressOf FillEpisodesList)
                        ThreadSeriesFill.IsBackground = True
                        ThreadSeriesFill.Start()
                    Else
                        'Series nicht gefunden
                        _SeriesProgressBar.Visible = False
                        _RepeatsProgressBar.Visible = False

                        'TvWish Server plugin aktiv
                        If CPGsettings.pluginTvWishList = True Then

                            Try
                                'Serie über id laden
                                Dim _Series As MyTvSeries = MyTvSeries.Retrieve(CInt(Replace(_loadParameter, "idSeries: ", "")))

                                If InStr(_layer.GetSetting("TvWishList_ListView").Value, "(title like '" & _Series.Title & "' ) AND (description like '%Neue Folge: %')") > 0 Then
                                    Helper.ShowNotify("Keine neuen Episoden im EPG gefunden! TvWish für " & _Series.Title & " wurde bereits angelegt")
                                    GUIWindowManager.ShowPreviousWindow()
                                Else
                                    'tvWish anlegen - fragen
                                    Dim dlgContext As GUIDialogYesNo = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_YES_NO, Integer)), GUIDialogYesNo)
                                    dlgContext.Reset()

                                    dlgContext.SetHeading(_Series.Title)
                                    dlgContext.SetLine(1, "TvWish für neue Episoden anlegen?")
                                    dlgContext.SetLine(2, "")
                                    dlgContext.SetLine(3, "")
                                    dlgContext.DoModal(GUIWindowManager.ActiveWindow)

                                    ' ja anlegen
                                    If dlgContext.IsConfirmed = True Then
                                        GUIWindowManager.ActivateWindow(70943675, "eq(#currentmoduleid,'1656544652'), 'NEWTVWISH//EXPRESSION=(title like '" & _Series.Title & "' ) AND (description like '%Neue Folge: %')//NAME=CPG: " & _Series.Title & ": " & Translation.NewEpisodes, True)
                                    Else
                                        GUIWindowManager.ShowPreviousWindow()
                                    End If
                                End If

                            Catch ex As Exception
                                Helper.ShowNotify("Keine neuen Episoden im EPG gefunden!")
                                GUIWindowManager.ShowPreviousWindow()
                            End Try
                        End If
                    End If
                End If
            End If

        End Sub
        Public Overrides Sub OnAction(ByVal Action As MediaPortal.GUI.Library.Action)
            If GUIWindowManager.ActiveWindow = GetID Then

                'Wenn Previous Window (ESC), erst prüfen ob Episodenliste angezeigt wird -> zurück Serienliste
                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_PREVIOUS_MENU Then

                    If _ShowEpisodes = True Then
                        'zurück zur Serien Übersicht wenn kein load parameter
                        If _loadParameter = String.Empty Then
                            _ShowEpisodes = False

                            _EpisodesListItems.Clear()

                            _LastFocusedEpisodeIndex = 0

                            Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowSeriesProgressBar)
                            _ProgressBarThread.Start()

                            AbortRunningThreads()

                            ThreadSeriesFill = New Threading.Thread(AddressOf FillSeriesList)
                            ThreadSeriesFill.IsBackground = True
                            ThreadSeriesFill.Start()
                            Return
                        End If

                    Else
                        If _loadParameter = String.Empty Then
                            _LastFocusedSeriesIndex = 0
                            _LastFocusedEpisodeIndex = 0
                        End If
                    End If

                End If

                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MOVE_UP Then
                    If _SeriesList.IsFocused = True Then
                        If _ShowEpisodes = True Then

                            If _SeriesList.SelectedListItem.ItemId = _SeriesList.Item(0).ItemId Then
                                _selectedListItemIndex = _SeriesList.Count - 1
                                _selectedSeries = TVMovieProgram.Retrieve(_SeriesList.Item(_selectedListItemIndex).TVTag)
                                _LastFocusedEpisodeIndex = _selectedListItemIndex
                            Else
                                _selectedListItemIndex = _SeriesList.SelectedListItemIndex - 1
                                _selectedSeries = TVMovieProgram.Retrieve(_SeriesList.Item(_selectedListItemIndex).TVTag)
                                _LastFocusedEpisodeIndex = _selectedListItemIndex
                            End If

                            Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowRepeatsProgressBar)
                            _ProgressBarThread.Start()

                            'AbortRunningThreads()

                            ThreadRepeatsFill = New Threading.Thread(AddressOf FillRepeatsList)
                            ThreadRepeatsFill.IsBackground = True
                            ThreadRepeatsFill.Start()

                            '_LastFocusedEpisodeIndex = _selectedListItemIndex

                        Else

                            If _SeriesList.SelectedListItem.ItemId = _SeriesList.Item(0).ItemId Then
                                _selectedListItemIndex = _SeriesList.Count - 1
                                _selectedSeries = TVMovieProgram.Retrieve(_SeriesList.Item(_selectedListItemIndex).TVTag)
                                _LastFocusedSeriesIndex = _selectedListItemIndex
                            Else
                                _selectedListItemIndex = _SeriesList.SelectedListItemIndex - 1
                                _selectedSeries = TVMovieProgram.Retrieve(_SeriesList.Item(_selectedListItemIndex).TVTag)
                                _LastFocusedSeriesIndex = _selectedListItemIndex
                            End If

                            Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowRepeatsProgressBar)
                            _ProgressBarThread.Start()

                            'AbortRunningThreads()

                            ThreadSeriesInfoFill = New Threading.Thread(AddressOf FillSeriesInfos)
                            ThreadSeriesInfoFill.IsBackground = True
                            ThreadSeriesInfoFill.Start()

                            '_LastFocusedIndex = _selectedListItemIndex
                        End If


                        '_LastFocusedControlID = _SeriesList.GetID
                    End If
                End If

                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MOVE_DOWN Then
                    If _SeriesList.IsFocused = True Then
                        If _ShowEpisodes = True Then

                            If _SeriesList.SelectedListItem.ItemId = _SeriesList.Item(_SeriesList.Count - 1).ItemId Then
                                _selectedListItemIndex = 0
                                _selectedSeries = TVMovieProgram.Retrieve(_SeriesList.Item(_selectedListItemIndex).TVTag)
                                _LastFocusedEpisodeIndex = _selectedListItemIndex
                            Else
                                _selectedListItemIndex = _SeriesList.SelectedListItemIndex + 1
                                _selectedSeries = TVMovieProgram.Retrieve(_SeriesList.Item(_selectedListItemIndex).TVTag)
                                _LastFocusedEpisodeIndex = _selectedListItemIndex
                            End If

                            Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowRepeatsProgressBar)
                            _ProgressBarThread.Start()

                            'AbortRunningThreads()

                            ThreadRepeatsFill = New Threading.Thread(AddressOf FillRepeatsList)
                            ThreadRepeatsFill.IsBackground = True
                            ThreadRepeatsFill.Start()

                            '_LastFocusedEpisodeIndex = _selectedListItemIndex

                        Else

                            If _SeriesList.SelectedListItem.ItemId = _SeriesList.Item(_SeriesList.Count - 1).ItemId Then
                                _selectedListItemIndex = 0
                                _selectedSeries = TVMovieProgram.Retrieve(_SeriesList.Item(_selectedListItemIndex).TVTag)
                                _LastFocusedSeriesIndex = _selectedListItemIndex
                            Else
                                _selectedListItemIndex = _SeriesList.SelectedListItemIndex + 1
                                _selectedSeries = TVMovieProgram.Retrieve(_SeriesList.Item(_selectedListItemIndex).TVTag)
                                _LastFocusedSeriesIndex = _selectedListItemIndex
                            End If

                            'FillSeriesInfos()

                            Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowRepeatsProgressBar)
                            _ProgressBarThread.Start()

                            'AbortRunningThreads()

                            ThreadSeriesInfoFill = New Threading.Thread(AddressOf FillSeriesInfos)
                            ThreadSeriesInfoFill.IsBackground = True
                            ThreadSeriesInfoFill.Start()

                            '_LastFocusedIndex = _selectedListItemIndex

                        End If


                        '_LastFocusedControlID = _SeriesList.GetID

                    End If
                End If

                'Play Button (P) -> Start channel
                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_MUSIC_PLAY Then

                    Try
                        If _SeriesList.IsFocused = True Then StartTv(_selectedSeries.ReferencedProgram)
                        If _RepeatsList.IsFocused = True Then StartTv(Program.Retrieve(_RepeatsList.SelectedListItem.ItemId))
                    Catch ex As Exception
                        MyLog.Error("[Play Button]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
                    End Try

                End If

                'Record Button (R) -> MP TvProgramInfo aufrufen --Über keychar--
                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_KEY_PRESSED Then

                    If Action.m_key IsNot Nothing Then
                        If Action.m_key.KeyChar = 114 Then
                            Dim _program As Program = Program.Retrieve(_SeriesList.SelectedListItem.TVTag)
                            If _SeriesList.IsFocused = True Then LoadTVProgramInfo(_program)
                            If _RepeatsList.IsFocused = True Then LoadTVProgramInfo(Program.Retrieve(_RepeatsList.SelectedListItem.TVTag))
                        End If
                    End If

                End If

                'Menu Button (F9) -> Context Menu open
                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_CONTEXT_MENU Then

                    If _SeriesList.IsFocused = True Then DialogMenu(_selectedSeries)
                    If _RepeatsList.IsFocused = True Then DialogMenu(TVMovieProgram.Retrieve(_RepeatsList.SelectedListItem.TVTag))

                End If

                'OSD Info Button (Y) -> Context Menu open (gleiche Fkt. wie Menu Button)
                If Action.wID = MediaPortal.GUI.Library.Action.ActionType.ACTION_KEY_PRESSED Then

                    If Action.m_key IsNot Nothing Then
                        If Action.m_key.KeyChar = 121 Then
                            If _SeriesList.IsFocused = True Then Helper.ShowActionMenu(_selectedSeries.ReferencedProgram)
                            If _RepeatsList.IsFocused = True Then Helper.ShowActionMenu(Program.Retrieve(_RepeatsList.SelectedListItem.TVTag))
                        End If
                    End If
                End If

            End If
            MyBase.OnAction(Action)
        End Sub

        Protected Overrides Sub OnPageDestroy(ByVal new_windowId As Integer)

            AbortRunningThreads()

            MyBase.OnPageDestroy(new_windowId)
            Dispose()
            AllocResources()
        End Sub
        Protected Overrides Sub OnClicked(ByVal controlId As Integer, _
                                 ByVal control As GUIControl, _
                                 ByVal actionType As  _
                                 MediaPortal.GUI.Library.Action.ActionType)

            MyBase.OnClicked(controlId, control, actionType)




            If control Is _btnAllMovies Then
                GuiButtons.AllMovies()
            End If



            If control Is _SeriesList Or control Is _RepeatsList Then
                If actionType = MediaPortal.GUI.Library.Action.ActionType.ACTION_SELECT_ITEM Then
                    Action_SelectItem()
                End If
            End If

        End Sub

        Private Sub Action_SelectItem()

            If _SeriesList.IsFocused = True Then

                If _SeriesList.SelectedListItem.Label = ".." Then
                    'zurück zur Serien Übersicht
                    If _loadParameter = String.Empty Then
                        _ShowEpisodes = False

                        _EpisodesListItems.Clear()

                        _LastFocusedEpisodeIndex = 0

                        Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowSeriesProgressBar)
                        _ProgressBarThread.Start()

                        AbortRunningThreads()

                        ThreadSeriesFill = New Threading.Thread(AddressOf FillSeriesList)
                        ThreadSeriesFill.IsBackground = True
                        ThreadSeriesFill.Start()

                    Else
                        GUIWindowManager.ShowPreviousWindow()
                    End If

                ElseIf _SeriesList.Item(0).ItemId = 0 Then
                    'DetailScreen anzeigen
                    _LastFocusedEpisodeIndex = _SeriesList.SelectedListItemIndex

                    '_selectedSeries = TVMovieProgram.Retrieve(_SeriesList(_LastFocusedEpisodeIndex).TVTag)

                    DetailGuiWindow._DetailGuiWindowList.Clear()
                    DetailGuiWindow._DetailGuiWindowList.AddRange(_SeriesList.ListItems.ConvertAll(Of GUIListItem)(New Converter(Of GUIListItem, GUIListItem)(Function(c As GUIListItem) New GUIListItem() With { _
                                                .ItemId = c.ItemId, _
                                                .ThumbnailImage = Config.GetFile(Config.Dir.Thumbs, _selectedSeries.SeriesPosterImage) _
                                               })))
                    DetailGuiWindow._DetailGuiWindowList.RemoveAt(0)

                    Translator.SetProperty("#DetailCoverflowLabel", GUIPropertyManager.GetProperty("#EpisodesPreviewTitle"))
                    Translator.SetProperty("#DetailCoverflowLabel2", Translation.allNewEpisodes)



                    ListControlClick(_SeriesList.Item(_LastFocusedEpisodeIndex).ItemId)
                Else
                    'Epsidoen der Serie anzeigen
                    _ShowEpisodes = True

                    _LastFocusedEpisodeIndex = 0

                    _selectedSeries = TVMovieProgram.Retrieve(_SeriesList.SelectedListItem.TVTag)

                    _LastFocusedSeriesIndex = _SeriesList.SelectedListItemIndex

                    Dim _ProgressBarThread As New Threading.Thread(AddressOf ShowSeriesProgressBar)
                    _ProgressBarThread.Start()

                    AbortRunningThreads()

                    FillSeriesInfos()

                    ThreadSeriesFill = New Threading.Thread(AddressOf FillEpisodesList)
                    ThreadSeriesFill.IsBackground = True
                    ThreadSeriesFill.Start()
                End If

            End If

        End Sub

#End Region

#Region "functions"
        Private Sub LoadAllLists()
            Try
                _allEpisodesList.Clear()
                _allSeriesList.Clear()
                _SeriesListItems.Clear()
                _SeriesInfosList.Clear()

                'Alle Episoden, local = false laden
                Dim _SQLstring As String = String.Empty
                _SQLstring = "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                                    "WHERE idSeries > 0 " & _
                                                    "AND local = 0 " & _
                                                    "AND startTime > " & MySqlDate(Now.AddMinutes(-1)) & _
                                                    "ORDER BY startTime ASC"

                _SQLstring = Replace(_SQLstring, " * ", " TVMovieProgram.idProgram, TVMovieProgram.Action, TVMovieProgram.Actors, TVMovieProgram.BildDateiname, TVMovieProgram.Country, TVMovieProgram.Cover, TVMovieProgram.Describtion, TVMovieProgram.Dolby, TVMovieProgram.EpisodeImage, TVMovieProgram.Erotic, TVMovieProgram.FanArt, TVMovieProgram.Feelings, TVMovieProgram.FileName, TVMovieProgram.Fun, TVMovieProgram.HDTV, TVMovieProgram.idEpisode, TVMovieProgram.idMovingPictures, TVMovieProgram.idSeries, TVMovieProgram.idVideo, TVMovieProgram.KurzKritik, TVMovieProgram.local, TVMovieProgram.Regie, TVMovieProgram.Requirement, TVMovieProgram.SeriesPosterImage, TVMovieProgram.ShortDescribtion, TVMovieProgram.Tension, TVMovieProgram.TVMovieBewertung ")
                Dim _SQLstate As SqlStatement = Broker.GetStatement(_SQLstring)
                _allEpisodesList = ObjectFactory.GetCollection(GetType(TVMovieProgram), _SQLstate.Execute())

                'Alle Serien laden, orderby startTime ACS
                Dim _idSeries As TVMovieProgram_GroupByIdSeries = New TVMovieProgram_GroupByIdSeries
                _allSeriesList = _allEpisodesList.Distinct(_idSeries).ToList

                'in list(of GuiListItem) umwandeln
                _SeriesListItems = _allSeriesList.ConvertAll(Of GUIListItem)(New Converter(Of TVMovieProgram, GUIListItem)(Function(c As TVMovieProgram) New GUIListItem() With { _
                                    .ItemId = c.idSeries, _
                                    .Path = c.ReferencedProgram.ReferencedChannel.DisplayName, _
                                    .Label = c.ReferencedProgram.Title, _
                                    .Label2 = Helper.getTranslatedDayOfWeek(c.ReferencedProgram.StartTime) & " " & Format(c.ReferencedProgram.StartTime.Day, "00") & "." & Format(c.ReferencedProgram.StartTime.Month, "00") & " - " & Format(c.ReferencedProgram.StartTime.Hour, "00") & ":" & Format(c.ReferencedProgram.StartTime.Minute, "00"), _
                                    .IconImage = Config.GetFile(Config.Dir.Thumbs, c.SeriesPosterImage), _
                                    .TVTag = c.idProgram, _
                                    .MusicTag = Config.GetFile(Config.Dir.Thumbs, "") & c.FanArt _
                                    }))

                'Neue Episoden zählen & an list(of GuiListItem) übergeben
                If _SeriesListItems.Count > 0 Then
                    For Each _ListItem As GUIListItem In _SeriesListItems

                        'alle neuen Episoden der Serie (groupby EpisodeName) zählen
                        Dim _EpisodesCounter As Integer = 0
                        Dim _EpisodesOfSeries As New List(Of TVMovieProgram)

                        Dim _itemID As Integer = _ListItem.ItemId

                        _EpisodesOfSeries = _allEpisodesList.FindAll(Function(x As TVMovieProgram) x.idSeries = _itemID)
                        Dim _episodeName As TVMovieProgram_GroupByEpisodeName = New TVMovieProgram_GroupByEpisodeName
                        _ListItem.Label3 = _EpisodesOfSeries.Distinct(_episodeName).Count & " " & Translation.NewEpisodeFound

                        'Wenn TvWishList aktiviert, dann schaeun ob schon TvWish vorhanden ist
                        Dim _EntryFoundinTvWishList As Boolean = False
                        If CPGsettings.pluginTvWishList = True Then

                            Dim sb As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(Setting))
                            sb.AddConstraint([Operator].Like, "tag", "TvWishList_ListView%")
                            sb.AddConstraint([Operator].Like, "value", "%(title like '" & _ListItem.Label & "' ) AND (description like '%Neue Folge: %')%")
                            sb.SetRowLimit(1)
                            Dim stmt As SqlStatement = sb.GetStatement(True)
                            Dim _TvWishFound As IList(Of Setting) = ObjectFactory.GetCollection(GetType(Setting), stmt.Execute())


                            If _TvWishFound.Count > 0 Then
                                _EntryFoundinTvWishList = True
                            End If
                        End If

                        Dim _tvMovieProgram As TVMovieProgram = TVMovieProgram.Retrieve(_ListItem.TVTag)
                        Dim _TvSeriesItem As New EpisodePreview_SeriesItem _
                            (_ListItem.ItemId, CStr(_ListItem.Label), _ListItem.IconImage, _
                             _ListItem.MusicTag, _tvMovieProgram.ReferencedProgram.Description, _tvMovieProgram.ReferencedProgram.StarRating, "", "", _
                             _EntryFoundinTvWishList)

                        'SerieInfos für FillSeriesInfos laden
                        Dim _status As String = Translation.Continous

                        Try
                            'Sofern Series in MPTvSeries gefunden
                            Dim _Series As MyTvSeries = MyTvSeries.Retrieve(_ListItem.ItemId)

                            If _Series.Status = "Ended" Then
                                _status = Translation.Ended
                            Else
                                _status = Translation.Continous
                            End If

                            _TvSeriesItem = New EpisodePreview_SeriesItem _
                            (_ListItem.ItemId, CStr(_ListItem.Label), _ListItem.IconImage, _
                             _ListItem.MusicTag, _Series.Summary, CInt(_Series.Rating), _Series.Network, _status, _
                             _EntryFoundinTvWishList)

                        Catch ex As Exception

                        End Try

                        _SeriesInfosList.Add(_TvSeriesItem)

                    Next

                End If

            Catch ex As ThreadAbortException
                MyLog.Debug("[EpisodePreviewGuiWindow] [LoadAllLists]: --- THREAD ABORTED ---")
                MyLog.Debug("")
            Catch ex As GentleException
            Catch ex As Exception
                MyLog.Error("[EpisodePreviewGuiWindow] [LoadAllLists]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try

        End Sub

        Private Sub FillSeriesList()

            _SeriesList.AllocResources()
            _SeriesList.Visible = False
            _SeriesList.Clear()
            _RepeatsList.AllocResources()
            _RepeatsList.Visible = False
            _RepeatsList.Clear()

            Translator.SetProperty("#EpisodePreviewLabel", Translation.NewEpisodes & " " & Translation.InTheNext & " " & CPGsettings.PreviewMaxDays & " " & Translation.Day)
            Translator.SetProperty("#EpisodesPreviewSeriesHeaderLabel", Translation.SeriesInfos)
            Translator.SetProperty("#EpisodesPreviewEpisodeDescription", "")

            Try
                If _SeriesListItems.Count > 0 Then
                    For Each _Series As GUIListItem In _SeriesListItems
                        _SeriesList.Add(_Series)
                    Next

                    _SeriesList.Visible = True
                    _SeriesProgressBar.Visible = False

                    GUIListControl.SelectItemControl(GetID, _SeriesList.GetID, _LastFocusedSeriesIndex)
                    GUIListControl.FocusControl(GetID, _SeriesList.GetID)

                    _selectedSeries = TVMovieProgram.Retrieve(_SeriesList.Item(_LastFocusedSeriesIndex).TVTag)

                    FillSeriesInfos()

                End If
            Catch ex As ThreadAbortException
                MyLog.Debug("[EpisodePreviewGuiWindow] [FillSeriesList]: --- THREAD ABORTED ---")
                MyLog.Debug("")
            Catch ex As GentleException
            Catch ex As Exception
                MyLog.Error("[EpisodePreviewGuiWindow] [FillSeriesList]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try
        End Sub

        Private Sub FillEpisodesList()

            Try
                ' _selectedSeries = TVMovieProgram.Retrieve(_SeriesList.Item(_selectedListItemIndex).TVTag)

                Dim _timeLabel As String = String.Empty
                Dim _infoLabel As String = String.Empty
                Dim _description As String = String.Empty

                Translator.SetProperty("#EpisodePreviewLabel", Translation.NewEpisodes & ": " & _selectedSeries.ReferencedProgram.Title)
                Translator.SetProperty("#EpisodesPreviewSeriesHeaderLabel", Translation.Repeats)

                _SeriesList.AllocResources()
                _SeriesList.Visible = False
                _SeriesList.Clear()
                _RepeatsList.AllocResources()
                _RepeatsList.Visible = False
                _RepeatsList.Clear()

                If _EpisodesListItems.Count > 0 Then
                    For Each _lisitem As GUIListItem In _EpisodesListItems
                        _SeriesList.Add(_lisitem)
                    Next
                Else

                    'zurück in Listcontrol einfügen
                    AddListControlItem(_SeriesList, 0, "channel", "..", , , Config.GetFile(Config.Dir.Thumbs, "Clickfinder ProgramGuide\Arrow back.png"), , , _selectedSeries.idProgram)

                    'alle Episoden der Serie laden (startTime ASC) und an list(of t) _EpisodeReapeatsList übergeben
                    _EpisodeReapeatsList = _allEpisodesList.FindAll(Function(x As TVMovieProgram) x.idSeries = _selectedSeries.idSeries)

                    'GroupBy EpisodeName
                    Dim _GroupByEpisode As TVMovieProgram_GroupByEpisodeName = New TVMovieProgram_GroupByEpisodeName
                    Dim tmp As List(Of TVMovieProgram) = _EpisodeReapeatsList.Distinct(_GroupByEpisode).ToList

                    If tmp.Count > 0 Then
                        For Each _Episode As TVMovieProgram In tmp
                            Dim _RecordingStatus As String = String.Empty

                            'Prüfen ob noch immer lokal nicht vorhanden -> Anzeigen
                            CheckSeriesLocalStatus(_Episode)
                            If _Episode.local = False Then

                                _timeLabel = Helper.getTranslatedDayOfWeek(_Episode.ReferencedProgram.StartTime) & " " & Format(_Episode.ReferencedProgram.StartTime.Day, "00") & "." & Format(_Episode.ReferencedProgram.StartTime.Month, "00") & " - " & Format(_Episode.ReferencedProgram.StartTime.Hour, "00") & ":" & Format(_Episode.ReferencedProgram.StartTime.Minute, "00")
                                _description = _Episode.ReferencedProgram.Description

                                'Falls die EPisode als Neu gekeinzeichnet ist, aber keine SxxExx nummer hat -> nicht identifiziert worden
                                If String.IsNullOrEmpty(_Episode.ReferencedProgram.SeriesNum) = True Or String.IsNullOrEmpty(_Episode.ReferencedProgram.EpisodeNum) Then
                                    _infoLabel = Translation.EpisodeNotIdentify
                                Else
                                    _infoLabel = Translation.Season & " " & Format(CInt(_Episode.ReferencedProgram.SeriesNum), "00") & ", " & Translation.Episode & Format(CInt(_Episode.ReferencedProgram.EpisodeNum), "00")

                                    Try
                                        Dim _MyEpisode As MyTvSeries.MyEpisode = MyTvSeries.Search(_Episode.ReferencedProgram.Title).Episode(CInt(_Episode.ReferencedProgram.SeriesNum), CInt(_Episode.ReferencedProgram.EpisodeNum))
                                        If Not String.IsNullOrEmpty(_MyEpisode.EpisodeSummary) Then
                                            _description = _MyEpisode.EpisodeSummary
                                        End If
                                    Catch ex As Exception
                                    End Try
                                End If


                                _RecordingStatus = GuiLayout.RecordingStatus(_Episode.ReferencedProgram)

                                'Prüfen ob Episode an anderem Kanal / Tag / Uhrzeit aufgenommen wird
                                If _RecordingStatus = String.Empty Then

                                    Dim _RepeatsScheduledRecordings As New List(Of TVMovieProgram)

                                    Try
                                        'Alle Wiederholungen dieser Episode laden, die als Aufnahme geplant sind (starTime ASC)
                                        Dim _episodeName As String = _Episode.ReferencedProgram.EpisodeName
                                        _RepeatsScheduledRecordings = _EpisodeReapeatsList.FindAll(Function(x As TVMovieProgram) _
                                                                               x.ReferencedProgram.EpisodeName = _episodeName AndAlso (x.ReferencedProgram.IsRecording = True Or x.ReferencedProgram.IsRecordingManual = True Or x.ReferencedProgram.IsRecordingOnce = True Or x.ReferencedProgram.IsRecordingOncePending = True Or x.ReferencedProgram.IsRecordingSeries = True Or x.ReferencedProgram.IsRecordingSeriesPending = True))

                                        If _RepeatsScheduledRecordings.Count > 0 Then
                                            _RecordingStatus = GuiLayout.RecordingStatus(_RepeatsScheduledRecordings(0).ReferencedProgram)

                                            Select Case (_RecordingStatus)
                                                Case Is = "tvguide_record_button.png"
                                                    _RecordingStatus = "ClickfinderPG_recOnce.png"
                                                Case Is = "tvguide_recordserie_button.png"
                                                    _RecordingStatus = "ClickfinderPG_recSeries.png"
                                            End Select
                                        End If

                                    Catch ex As Exception
                                        MsgBox(ex.Message)
                                    End Try

                                End If

                                AddListControlItem(_SeriesList, _Episode.ReferencedProgram.IdProgram, _Episode.ReferencedProgram.ReferencedChannel.DisplayName, _Episode.ReferencedProgram.EpisodeName, _timeLabel, _infoLabel, Config.GetFile(Config.Dir.Thumbs, "tv\logos\") & Replace(_Episode.ReferencedProgram.ReferencedChannel.DisplayName, "/", "_") & ".png", , _RecordingStatus, _Episode.idProgram, _description)


                            End If
                        Next

                        _EpisodesListItems = _SeriesList.ListItems.ToList

                    End If

                End If
                _SeriesList.Visible = True
                _SeriesProgressBar.Visible = False

                GUIListControl.SelectItemControl(GetID, _SeriesList.GetID, _LastFocusedEpisodeIndex)
                GUIListControl.FocusControl(GetID, _SeriesList.GetID)

                FillRepeatsList()

            Catch ex As ThreadAbortException
                MyLog.Debug("[EpisodePreviewGuiWindow] [FillEpisodesList]: --- THREAD ABORTED ---")
                MyLog.Debug("")
            Catch ex As GentleException
            Catch ex As Exception
                MyLog.Error("[EpisodePreviewGuiWindow] [FillEpisodesList]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try

        End Sub

        Private Sub FillRepeatsList()

            Try
                _RepeatsList.AllocResources()
                _RepeatsList.Visible = False
                _RepeatsList.Clear()

                If Not _SeriesList.SelectedListItem.Label = ".." Then

                    Dim _timeLabel As String = String.Empty
                    Dim _infoLabel As String = String.Empty

                    Dim _EpisodeInfos As TVMovieProgram = TVMovieProgram.Retrieve(_SeriesList.Item(_selectedListItemIndex).TVTag)

                    _SeriesPoster.SetFileName(Config.GetFile(Config.Dir.Thumbs, _selectedSeries.SeriesPosterImage))

                    Translator.SetProperty("#EpisodesPreviewLabel1", Translation.EpisodeNewPrefixLabel & " " & _SeriesList.Item(_selectedListItemIndex).Label)
                    Translator.SetProperty("#EpisodesPreviewLabel2", _SeriesList.Item(_selectedListItemIndex).Label3)
                    Translator.SetProperty("#EpisodesPreviewLabel3", _SeriesList.Item(_selectedListItemIndex).Path & ": " & _SeriesList.Item(_selectedListItemIndex).Label2)
                    Translator.SetProperty("#EpisodesPreviewSeriesRatingStar", GuiLayout.ratingStar(_EpisodeInfos.ReferencedProgram))
                    Translator.SetProperty("#EpisodesPreviewEpisodeDescription", _SeriesList.Item(_selectedListItemIndex).ThumbnailImage)
                    Translator.SetProperty("#EpisodesPreviewSeriesSummary", "")
                    Translator.SetProperty("#EpisodesPreviewTitle", _EpisodeInfos.ReferencedProgram.Title)

                    Dim _EpisodeRepeats As List(Of TVMovieProgram) = _EpisodeReapeatsList.FindAll(Function(x As TVMovieProgram) _
                                            x.idSeries = _EpisodeInfos.idSeries _
                                            AndAlso x.ReferencedProgram.EpisodeName = _EpisodeInfos.ReferencedProgram.EpisodeName _
                                            AndAlso Not x.ReferencedProgram.IdProgram = _EpisodeInfos.ReferencedProgram.IdProgram)

                    '_SeriesPoster.SetFileName(Config.GetFile(Config.Dir.Thumbs, "MPTVSeriesBanners\") & _selectedSeries.SeriesPosterImage)

                    For Each _Repeat As TVMovieProgram In _EpisodeRepeats
                        If Not _Repeat.ReferencedProgram.IdProgram = _SeriesList.Item(_selectedListItemIndex).TVTag Then

                            _timeLabel = Helper.getTranslatedDayOfWeek(_Repeat.ReferencedProgram.StartTime) & " " & Format(_Repeat.ReferencedProgram.StartTime.Day, "00") & "." & Format(_Repeat.ReferencedProgram.StartTime.Month, "00") & " - " & Format(_Repeat.ReferencedProgram.StartTime.Hour, "00") & ":" & Format(_Repeat.ReferencedProgram.StartTime.Minute, "00")
                            _infoLabel = Translation.Season & " " & _Repeat.ReferencedProgram.SeriesNum & ", " & Translation.Episode & " " & _Repeat.ReferencedProgram.EpisodeNum

                            AddListControlItem(_RepeatsList, _Repeat.ReferencedProgram.IdProgram, _Repeat.ReferencedProgram.ReferencedChannel.DisplayName, _Repeat.ReferencedProgram.EpisodeName, , _timeLabel, Config.GetFile(Config.Dir.Thumbs, "tv\logos\") & Replace(_Repeat.ReferencedProgram.ReferencedChannel.DisplayName, "/", "_") & ".png", , GuiLayout.RecordingStatus(_Repeat.ReferencedProgram))
                        End If
                    Next

                    _RepeatsList.Visible = True

                Else
                    FillSeriesInfos()
                End If

                _RepeatsProgressBar.Visible = False

            Catch ex As ThreadAbortException
                MyLog.Debug("[EpisodePreviewGuiWindow] [FillRepeatsList]: --- THREAD ABORTED ---")
                MyLog.Debug("")
            Catch ex As GentleException
            Catch ex As Exception
                MyLog.Error("[EpisodePreviewGuiWindow] [FillRepeatsList]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try

        End Sub

        Private Sub FillSeriesInfos()

            Try

                'MsgBox(_SeriesInfosList(_selectedSeries.idSeries).SeriesName)
                Dim _SeriesItem As EpisodePreview_SeriesItem = _SeriesInfosList.Find(Function(x As EpisodePreview_SeriesItem) x.idSeries = _selectedSeries.idSeries)

                Translator.SetProperty("#EpisodesPreviewTvWishEntryFounded", CStr(_SeriesItem.TvWishFound))

                _SeriesPoster.SetFileName(_SeriesItem.SeriesPosterImage)

                Translator.SetProperty("#EpisodesPreviewTitle", _SeriesItem.SeriesName)
                Translator.SetProperty("#EpisodesPreviewEpisodeDescription", "")
                Translator.SetProperty("#EpisodesPreviewLabel3", "")

                Translator.SetProperty("#EpisodesPreviewSeriesSummary", _SeriesItem.Summary)
                Translator.SetProperty("#EpisodesPreviewSeriesRatingStar", _SeriesItem.Rating)
                Translator.SetProperty("#EpisodesPreviewLabel1", _SeriesItem.Network)
                Translator.SetProperty("#EpisodesPreviewLabel2", _SeriesItem.Status)

                Translator.SetProperty("#EpisodesPreviewFanArt", _SeriesItem.FanArt)

                _RepeatsProgressBar.Visible = False

            Catch ex As ThreadAbortException
                MyLog.Debug("[EpisodePreviewGuiWindow] [FillSeriesInfos]: --- THREAD ABORTED ---")
                MyLog.Debug("")
            Catch ex As GentleException
            Catch ex As Exception
                MyLog.Error("[EpisodePreviewGuiWindow] [FillSeriesInfos]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try

        End Sub

        Private Sub AbortRunningThreads()
            Try
                If ThreadSeriesFill.IsAlive = True Then ThreadSeriesFill.Abort()
            Catch ex As Exception
            End Try

            Try
                If ThreadSeriesInfoFill.IsAlive = True Then ThreadSeriesInfoFill.Abort()
            Catch ex As Exception
            End Try

            Try
                If ThreadRepeatsFill.IsAlive = True Then ThreadRepeatsFill.Abort()
            Catch ex As Exception
            End Try
        End Sub

        Private Sub ShowSeriesProgressBar()
            _SeriesProgressBar.Visible = True
        End Sub

        Private Sub ShowRepeatsProgressBar()
            _RepeatsProgressBar.Visible = True
        End Sub

        Private Sub ClearSeriesInfos()
            Translator.SetProperty("#EpisodesPreviewSeriesPosterImage", "")
            Translator.SetProperty("#EpisodesPreviewTitle", "")
            Translator.SetProperty("#EpisodesPreviewSeriesSummary", "")
            Translator.SetProperty("#EpisodesPreviewSeriesRatingStar", 0)
            Translator.SetProperty("#EpisodesPreviewLabel1", "")
            Translator.SetProperty("#EpisodesPreviewLabel2", "")
            Translator.SetProperty("#EpisodesPreviewLabel3", "")
            Translator.SetProperty("#EpisodesPreviewFanArt", "")
            Translator.SetProperty("#EpisodesPreviewTvWishEntryFounded", "false")
        End Sub

#End Region

#Region "MP Diaglogs"

        Private Sub DialogMenu(ByVal TvMovieProgram As TVMovieProgram)
            Try

                Dim dlgContext As GUIDialogMenu = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_MENU, Integer)), GUIDialogMenu)
                dlgContext.Reset()

                Dim _disabled As Boolean = False
                Dim _minSeriesNum As Integer = 0

                Try
                    _disabled = TvMovieProgram.ReferencedSeries.disabled
                Catch ex As Exception
                    'wenn kein mapping vorhanden
                End Try

                Try
                    _minSeriesNum = TvMovieProgram.ReferencedSeries.minSeasonNum
                Catch ex As Exception
                    'wenn kein mapping vorhanden
                End Try


                If _ShowEpisodes = False Then
                    'Series ContextMenu

                    'ContextMenu Layout
                    dlgContext.SetHeading(TvMovieProgram.ReferencedProgram.Title)
                    dlgContext.ShowQuickNumbers = True

                    'Disable -> Datenbank abgleich erforderlich
                    Dim lItemDisable As New GUIListItem
                    lItemDisable.Label = Translation.NewEpisodes & " ignorieren"
                    lItemDisable.Label2 = _disabled
                    dlgContext.Add(lItemDisable)
                    lItemDisable.Dispose()

                    'min SeriesNum -> gleich ausführen
                    Dim lItemMinSeriesNumber As New GUIListItem
                    lItemMinSeriesNumber.Label = "Seriennummer"
                    lItemMinSeriesNumber.Label2 = ">= " & _minSeriesNum
                    dlgContext.Add(lItemMinSeriesNumber)
                    lItemMinSeriesNumber.Dispose()

                    'Action SubMenu
                    Dim lItemActionOn As New GUIListItem
                    lItemActionOn.Label = Translation.action
                    dlgContext.Add(lItemActionOn)
                    lItemActionOn.Dispose()


                    dlgContext.DoModal(GUIWindowManager.ActiveWindow)

                    Select Case dlgContext.SelectedLabel
                        Case Is = 1
                            ShowMinNumbersMenu(TvMovieProgram)
                    End Select

                Else

                    'ContextMenu Layout
                    dlgContext.SetHeading(TvMovieProgram.ReferencedProgram.EpisodeName)
                    dlgContext.ShowQuickNumbers = True

                    'mit Episode verlinken
                    Dim lItemEpisodeLink As New GUIListItem
                    lItemEpisodeLink.Label = "mit Episode verlinken ..."
                    dlgContext.Add(lItemEpisodeLink)
                    lItemEpisodeLink.Dispose()

                    'alle Episoden im EPG
                    Dim lItemAllEpisode As New GUIListItem
                    lItemAllEpisode.Label = "alle Episoden im EPG"
                    dlgContext.Add(lItemAllEpisode)
                    lItemAllEpisode.Dispose()

                    'Action SubMenu
                    Dim lItemActionOn As New GUIListItem
                    lItemActionOn.Label = Translation.action
                    dlgContext.Add(lItemActionOn)
                    lItemActionOn.Dispose()

                    dlgContext.DoModal(GUIWindowManager.ActiveWindow)

                    Select Case dlgContext.SelectedLabel
                        Case Is = 0
                            If String.IsNullOrEmpty(TvMovieProgram.idEpisode) Then
                                ShowEpisodeLinkContextMenu(TvMovieProgram)
                            Else
                                MPDialogOK("Warnung", TvMovieProgram.ReferencedProgram.Title, TvMovieProgram.ReferencedProgram.EpisodeName & " - S" & TvMovieProgram.ReferencedProgram.SeriesNum & "E" & TvMovieProgram.ReferencedProgram.EpisodeNum, "wurde bereist identifiziert !")
                            End If
                        Case Is = 1
                            Dim _SqlStringBuilder As New StringBuilder("Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram ")
                            _SqlStringBuilder.Append("WHERE #CPGFilter AND startTime >= #StartTime AND startTime <= #EndTime ")
                            _SqlStringBuilder.Append("AND idseries = " & TvMovieProgram.idSeries & " ")
                            _SqlStringBuilder.Append("#CPGgroupBy ")
                            _SqlStringBuilder.Append("#CPGorderBy")

                            ItemsGuiWindow.SetGuiProperties(_SqlStringBuilder.ToString, _
                                                        Today, _
                                                        Today.AddDays(15), _
                                                        SortMethode.Episode.ToString, _
                                                        CPGsettings.StandardTvGroup)

                            Translator.SetProperty("#ItemsLeftListLabel", "alle Episoden im EPG")
                            GUIWindowManager.ActivateWindow(1656544653)

                    End Select
                End If

                dlgContext.Dispose()
                dlgContext.AllocResources()
            Catch ex As Exception
                MyLog.Error("[EpisodePreviewGuiWindow]: error: {0}, stack: {1}", ex.Message, ex.StackTrace)
            End Try
        End Sub

        Private Shared Sub ShowEpisodeLinkContextMenu(ByVal TvMovieProgram As TVMovieProgram)
            MyLog.Info("")
            MyLog.Info("[ShowEpisodeLinkContextMenu]: open")

            Try
                Dim dlgContext As GUIDialogSelect2Custom = CType(GUIWindowManager.GetWindow(CType(1656544655, Integer)), GUIDialogSelect2Custom)
                dlgContext.Reset()

                'ContextMenu Layout
                dlgContext.SetHeading(TvMovieProgram.ReferencedProgram.EpisodeName)

                'Alle Episoden der Serie aus DB laden
                Dim _EpisodeContainer As Dictionary(Of Integer, MyTvSeries.MyEpisode) = New Dictionary(Of Integer, MyTvSeries.MyEpisode)
                Dim _Image As String = Config.GetFile(Config.Dir.Thumbs, TvMovieProgram.SeriesPosterImage)
                Dim _EpisodeList As IList(Of MyTvSeries.MyEpisode) = MyTvSeries.MyEpisode.ListAll(TvMovieProgram.idSeries)

                _EpisodeList = _EpisodeList.Where(Function(x) x.SeriesNum > 0) _
                                .OrderBy(Function(x) x.SeriesNum).ThenBy(Function(x) x.EpisodeNum).ToList

                Dim i As Integer = 0
                For Each _Episode In _EpisodeList
                    Dim _lItemEpisode As New GUIListItem
                    _lItemEpisode.Label = _Episode.EpisodeName
                    _lItemEpisode.Label2 = "S" & Format(_Episode.SeriesNum, "00") & "E" & Format(_Episode.EpisodeNum, "00")
                    _lItemEpisode.IconImage = _Image

                    Try
                        'Sofern verlinkung vorhanden
                        Dim _EpisodeMapping As TVMovieEpisodeMapping = TVMovieEpisodeMapping.Retrieve(_Episode.idEpisode)
                        _lItemEpisode.Label3 = Translation.LinkTo & " " & _EpisodeMapping.EPGEpisodeName
                    Catch ex As Exception
                        'sonst abfangen
                    End Try

                    _EpisodeContainer.Add(i, _Episode)

                    dlgContext.Add(_lItemEpisode)
                    _lItemEpisode.Dispose()

                    i = i + 1
                Next

                dlgContext.DoModal(GUIWindowManager.ActiveWindow)

                Select Case dlgContext.SelectedLabel
                    Case Is = _EpisodeContainer.Keys(dlgContext.SelectedLabel)

                        Dim _Episode As MyTvSeries.MyEpisode = _EpisodeContainer.Item(dlgContext.SelectedLabel)
                        Dim _Series As MyTvSeries = MyTvSeries.Retrieve(_Episode.SeriesID)

                        'EposdeMapping übergeben
                        Dim _EpisodeMapping As TVMovieEpisodeMapping


                        Try
                            _EpisodeMapping = TVMovieEpisodeMapping.Retrieve(_Episode.idEpisode)

                            If String.IsNullOrEmpty(_EpisodeMapping.EPGEpisodeName) = True Then
                                _EpisodeMapping.EPGEpisodeName = TvMovieProgram.ReferencedProgram.EpisodeName
                            Else
                                'Verlinkung schon vorhanden -> neuer Dlg überschreiben oder hinzufügen
                                ShowepisodeLinkManagementContextMenu(_EpisodeMapping, TvMovieProgram.ReferencedProgram.EpisodeName)
                            End If
                        Catch ex As Exception
                            _EpisodeMapping = New TVMovieEpisodeMapping(_Episode.idEpisode, _
                                                                             TvMovieProgram.idSeries)
                            _EpisodeMapping.EPGEpisodeName = TvMovieProgram.ReferencedProgram.EpisodeName
                        End Try

                        _EpisodeMapping.idSeries = _Episode.SeriesID
                        _EpisodeMapping.seriesNum = _Episode.SeriesNum
                        _EpisodeMapping.episodeNum = _Episode.EpisodeNum
                        _EpisodeMapping.Persist()

                        MyLog.Info("[ShowEpisodeLinkContextMenu]: {0} - {1} mapped with {2}", _
                             TvMovieProgram.ReferencedProgram.Title, dlgContext.SelectedLabelText, _
                             TVMovieEpisodeMapping.Retrieve(_Episode.idEpisode).EPGEpisodeName)

                        'Alle Episoden (program & tvmovieprogram) updaten
                        Dim _SQLstring As String = _
                        "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                        "WHERE idSeries = " & _Episode.SeriesID & " " & _
                        "AND episodeName LIKE '" & MyTvSeries.Helper.allowedSigns(TvMovieProgram.ReferencedProgram.EpisodeName) & "'"

                        _SQLstring = Replace(_SQLstring, " * ", " TVMovieProgram.idProgram, TVMovieProgram.Action, TVMovieProgram.Actors, TVMovieProgram.BildDateiname, TVMovieProgram.Country, TVMovieProgram.Cover, TVMovieProgram.Describtion, TVMovieProgram.Dolby, TVMovieProgram.EpisodeImage, TVMovieProgram.Erotic, TVMovieProgram.FanArt, TVMovieProgram.Feelings, TVMovieProgram.FileName, TVMovieProgram.Fun, TVMovieProgram.HDTV, TVMovieProgram.idEpisode, TVMovieProgram.idMovingPictures, TVMovieProgram.idSeries, TVMovieProgram.idVideo, TVMovieProgram.KurzKritik, TVMovieProgram.local, TVMovieProgram.Regie, TVMovieProgram.Requirement, TVMovieProgram.SeriesPosterImage, TVMovieProgram.ShortDescribtion, TVMovieProgram.Tension, TVMovieProgram.TVMovieBewertung ")
                        Dim _SQLstate4 As SqlStatement = Broker.GetStatement(_SQLstring)
                        Dim _EpisodeRepeatsList As IList(Of TVMovieProgram) = ObjectFactory.GetCollection(GetType(TVMovieProgram), _SQLstate4.Execute())

                        If _EpisodeRepeatsList.Count > 0 Then
                            Dim _logNewEpisode As Boolean = False
                            For Each _TvMovieProgram In _EpisodeRepeatsList
                                _logNewEpisode = Not IdentifySeries.UpdateProgramAndTvMovieProgram(_TvMovieProgram.ReferencedProgram, _Series, _Episode, _Episode.ExistLocal, True)
                            Next
                            MyLog.Info("[ShowEpisodeLinkContextMenu]: S{0}E{1} - {2} updated (programList.count = {3}, newEpisode: {4})", _
                                      _Episode.SeriesNum, _Episode.EpisodeNum, TvMovieProgram.ReferencedProgram.EpisodeName, _EpisodeRepeatsList.Count, _logNewEpisode)
                        End If

                    Case Else
                        'Exit dlgcontext
                End Select

                dlgContext.Dispose()
                dlgContext.AllocResources()

                MyLog.Info("")
            Catch ex As Exception
                MyLog.Error("[ShowEpisodeLinkContextMenu]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try

        End Sub
        Private Shared Sub ShowepisodeLinkManagementContextMenu(ByVal Mapping As TVMovieEpisodeMapping, ByVal NewEpisodeMapping As String)
            Try
                Dim dlgContext As GUIDialogMenu = CType(GUIWindowManager.GetWindow(CType(GUIWindow.Window.WINDOW_DIALOG_MENU, Integer)), GUIDialogMenu)
                dlgContext.Reset()

                'ContextMenu Layout
                dlgContext.SetHeading(NewEpisodeMapping)
                dlgContext.ShowQuickNumbers = True

                'Verlinkung hinzufügen
                Dim lItemAdd As New GUIListItem
                lItemAdd.Label = Translation.MappingAppend
                dlgContext.Add(lItemAdd)
                lItemAdd.Dispose()

                'Verlinkung überschreiben
                Dim lItemOverwrite As New GUIListItem
                lItemOverwrite.Label = Translation.MappingOverwrite
                dlgContext.Add(lItemOverwrite)
                lItemOverwrite.Dispose()

                dlgContext.DoModal(GUIWindowManager.ActiveWindow)

                Select Case dlgContext.SelectedLabel
                    Case Is = 0
                        Mapping.EPGEpisodeName = Mapping.EPGEpisodeName & "|" & NewEpisodeMapping
                    Case Is = 1
                        Mapping.EPGEpisodeName = NewEpisodeMapping
                End Select

                dlgContext.Dispose()
                dlgContext.AllocResources()

            Catch ex As Exception
                MyLog.Error("[ShowepisodeLinkManagementContextMenu]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try
        End Sub

        Private Shared Sub ShowMinNumbersMenu(ByVal TvMovieProgram As TVMovieProgram)
            MyLog.Info("")
            MyLog.Info("[ShowMinNumbersMenu]: open")

            Try
                Dim dlgContext As GUIDialogSelect2Custom = CType(GUIWindowManager.GetWindow(CType(1656544655, Integer)), GUIDialogSelect2Custom)
                dlgContext.Reset()

                'ContextMenu Layout
                dlgContext.SetHeading(String.Format("{0}: Neue Episoden anzeigen ab Staffel", TvMovieProgram.ReferencedProgram.Title))


                'Alle Staffeln der Serie aus DB laden
                Dim _Series As MyTvSeries = MyTvSeries.Retrieve(TvMovieProgram.idSeries)

                Dim _activeNum As Integer = 0
                Try
                    Dim _SeriesMapping As TvMovieSeriesMapping = TvMovieProgram.ReferencedSeries
                    _activeNum = _SeriesMapping.minSeasonNum
                Catch ex As Exception

                End Try

                For i = 0 To _Series.SeasonCount
                    Dim _lItemNum As New GUIListItem

                    If i = 0 Then
                        _lItemNum.Label = "deaktivieren"
                    Else
                        _lItemNum.Label = Translation.Season & " " & i
                    End If

                    If _activeNum = i Then
                        _lItemNum.Label2 = "aktiv"
                    End If

                    '_lItemEpisode.Label2 = "S" & Format(_Episode.SeriesNum, "00") & "E" & Format(_Episode.EpisodeNum, "00")
                    _lItemNum.IconImage = Config.GetFile(Config.Dir.Thumbs, TvMovieProgram.SeriesPosterImage)

                    dlgContext.Add(_lItemNum)

                    _lItemNum.Dispose()
                Next


                dlgContext.DoModal(GUIWindowManager.ActiveWindow)


                Select Case dlgContext.SelectedLabel
                    Case Is >= 0
                        'minSeasonNum speichern
                        MyLog.Info("[ShowEpisodeLinkContextMenu]: {0}, only mark as new if season >= {1}", _Series.Title, dlgContext.SelectedLabel)
                        Dim _SeriesMapping As TvMovieSeriesMapping
                        Try
                            _SeriesMapping = TvMovieProgram.ReferencedSeries
                        Catch ex As Exception
                            _SeriesMapping = New TvMovieSeriesMapping(TvMovieProgram.idSeries)
                        End Try

                        _SeriesMapping.TvSeriesTitle = _Series.Title

                        If String.IsNullOrEmpty(_SeriesMapping.EpgTitle) Then _SeriesMapping.EpgTitle = String.Empty

                        _SeriesMapping.minSeasonNum = dlgContext.SelectedLabel
                        _SeriesMapping.Persist()

                        'Alle Episoden der Serie (program & tvmovieprogram) updaten
                        'minSeason prüfen
                        Dim _SQLstring As String = _
                        "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                        "WHERE idSeries = " & _Series.idSeries & " " & _
                        "ORDER BY episodeName"

                        _SQLstring = Replace(_SQLstring, " * ", " TVMovieProgram.idProgram, TVMovieProgram.Action, TVMovieProgram.Actors, TVMovieProgram.BildDateiname, TVMovieProgram.Country, TVMovieProgram.Cover, TVMovieProgram.Describtion, TVMovieProgram.Dolby, TVMovieProgram.EpisodeImage, TVMovieProgram.Erotic, TVMovieProgram.FanArt, TVMovieProgram.Feelings, TVMovieProgram.FileName, TVMovieProgram.Fun, TVMovieProgram.HDTV, TVMovieProgram.idEpisode, TVMovieProgram.idMovingPictures, TVMovieProgram.idSeries, TVMovieProgram.idVideo, TVMovieProgram.KurzKritik, TVMovieProgram.local, TVMovieProgram.Regie, TVMovieProgram.Requirement, TVMovieProgram.SeriesPosterImage, TVMovieProgram.ShortDescribtion, TVMovieProgram.Tension, TVMovieProgram.TVMovieBewertung ")
                        Dim _SQLstate4 As SqlStatement = Broker.GetStatement(_SQLstring)
                        Dim _EpisodeList As IList(Of TVMovieProgram) = ObjectFactory.GetCollection(GetType(TVMovieProgram), _SQLstate4.Execute())

                        If _EpisodeList.Count > 0 Then
                            Dim _logNewEpisode As Boolean = False

                            For Each _Program In _EpisodeList
                                If Not String.IsNullOrEmpty(TvMovieProgram.idEpisode) Then
                                    Dim _Episode As MyTvSeries.MyEpisode = MyTvSeries.MyEpisode.Retrieve(_Program.idEpisode)
                                    _logNewEpisode = Not IdentifySeries.UpdateProgramAndTvMovieProgram(_Program.ReferencedProgram, _Series, _Episode, _Episode.ExistLocal, True)

                                    MyLog.Info("[ShowEpisodeLinkContextMenu]: S{0}E{1} - {2} updated (programList.count = {3}, newEpisode: {4})", _
                                    _Episode.SeriesNum, _Episode.EpisodeNum, _Program.ReferencedProgram.EpisodeName, _EpisodeList.Count, _logNewEpisode)
                                Else
                                    'nicht identifizierte Episoden
                                End If
                            Next
                        End If

                    Case Else
                        'exit
                End Select


                dlgContext.Dispose()
                dlgContext.AllocResources()

                MyLog.Info("")
            Catch ex As Exception
                MyLog.Error("[ShowEpisodeLinkContextMenu]: exception err: {0} stack: {1}", ex.Message, ex.StackTrace)
            End Try

        End Sub
#End Region

        Public Class EpisodePreview_SeriesItem
            Private _mSeriesID As Integer
            Private _mSeriesName As String
            Private _mSeriesPosterImage As String
            Private _mFanArt As String
            Private _mSummary As String
            Private _mRating As Integer
            Private _mNetwork As String
            Private _mStatus As String
            Private _mTvWish As Boolean
            Public Sub New(ByVal idSeries As Integer, ByVal SeriesName As String, ByVal Cover As String, ByVal FanArt As String, ByVal Summary As String, ByVal Rating As Integer, _
                           ByVal Network As String, ByVal Status As String, ByVal TvWishFound As Boolean)
                _mSeriesID = idSeries
                _mSeriesName = SeriesName
                _mSeriesPosterImage = Cover
                _mFanArt = FanArt
                _mSummary = Summary
                _mRating = Rating
                _mNetwork = Network
                _mStatus = Status
                _mTvWish = TvWishFound
            End Sub

            Public Property idSeries() As Integer
                Get
                    Return _mSeriesID
                End Get
                Set(ByVal value As Integer)
                    _mSeriesID = value
                End Set
            End Property
            Public Property SeriesName() As String
                Get
                    Return _mSeriesName
                End Get
                Set(ByVal value As String)
                    _mSeriesName = value
                End Set
            End Property
            Public Property SeriesPosterImage() As String
                Get
                    Return _mSeriesPosterImage
                End Get
                Set(ByVal value As String)
                    _mSeriesPosterImage = value
                End Set
            End Property
            Public Property FanArt() As String
                Get
                    Return _mFanArt
                End Get
                Set(ByVal value As String)
                    _mFanArt = value
                End Set
            End Property
            Public Property Summary() As String
                Get
                    Return _mSummary
                End Get
                Set(ByVal value As String)
                    _mSummary = value
                End Set
            End Property
            Public Property Rating() As Integer
                Get
                    Return _mRating
                End Get
                Set(ByVal value As Integer)
                    _mRating = value
                End Set
            End Property
            Public Property Network() As String
                Get
                    Return _mNetwork
                End Get
                Set(ByVal value As String)
                    _mNetwork = value
                End Set
            End Property
            Public Property Status() As String
                Get
                    Return _mStatus
                End Get
                Set(ByVal value As String)
                    _mStatus = value
                End Set
            End Property
            Public Property TvWishFound() As Boolean
                Get
                    Return _mTvWish
                End Get
                Set(ByVal value As Boolean)
                    _mTvWish = value
                End Set
            End Property
        End Class

    End Class
End Namespace
