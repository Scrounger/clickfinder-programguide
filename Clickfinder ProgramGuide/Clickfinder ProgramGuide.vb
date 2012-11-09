Imports System
Imports System.IO
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Runtime.CompilerServices
Imports ClickfinderProgramGuide.Helper

Imports MediaPortal.GUI.Library
Imports MediaPortal.Dialogs
Imports MediaPortal.Profile
Imports MediaPortal.Configuration
Imports MediaPortal.Utils
Imports MediaPortal.Util
Imports TvDatabase
Imports MediaPortal.Player

Imports Gentle.Common
Imports TvPlugin

Imports MediaPortal.Database
Imports SQLite.NET

Imports System.Threading

Imports Action = MediaPortal.GUI.Library.Action
Imports Gentle.Framework
Imports System.Drawing
Imports MediaPortal.Playlists

Imports ClickfinderProgramGuide.TvDatabase
Imports System.Timers

Namespace ClickfinderProgramGuide
    <PluginIcons("ClickfinderProgramGuide.Config.png", "ClickfinderProgramGuide.Config_disable.png")> _
    Public Class StartGuiWindow

        Inherits GUIWindow
        Implements ISetupForm

#Region "Skin Controls"

#End Region

#Region "Members"
        Private _stateTimer As System.Timers.Timer
        Friend Shared _OverlayStartupLoaded As Boolean = False
#End Region

#Region "iSetupFormImplementation"

        Public Overloads Overrides Property GetID() As Integer
            Get
                Return 165654465
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property
        Public Function PluginName() As String Implements MediaPortal.GUI.Library.ISetupForm.PluginName
            Return "Clickfinder ProgramGuide"
        End Function
        Public Function Description() As String Implements MediaPortal.GUI.Library.ISetupForm.Description
            Return "Describtion"
        End Function
        Public Function Author() As String Implements MediaPortal.GUI.Library.ISetupForm.Author
            Return "Scrounger"
        End Function
        Public Sub ShowPlugin() Implements MediaPortal.GUI.Library.ISetupForm.ShowPlugin
            Dim setup As New Setup
            setup.ShowDialog()
        End Sub
        Public Function CanEnable() As Boolean Implements MediaPortal.GUI.Library.ISetupForm.CanEnable
            Return True
        End Function
        Public Function GetWindowId() As Integer _
        Implements MediaPortal.GUI.Library.ISetupForm.GetWindowId

            Return 165654465

        End Function
        Public Function DefaultEnabled() As Boolean Implements MediaPortal.GUI.Library.ISetupForm.DefaultEnabled
            Return True
        End Function
        Public Function HasSetup() As Boolean Implements MediaPortal.GUI.Library.ISetupForm.HasSetup
            Return True
        End Function
        Public Function GetHome(ByRef strButtonText As String, ByRef strButtonImage As String, ByRef strButtonImageFocus As String, ByRef strPictureImage As String) As Boolean Implements MediaPortal.GUI.Library.ISetupForm.GetHome
            Dim _layer As New TvBusinessLayer
            strButtonText = _layer.GetSetting("ClickfinderPluginName", "Clickfinder ProgramGuide").Value

            strButtonImage = String.Empty

            strButtonImageFocus = String.Empty

            strPictureImage = "Config.png"

            Return True
        End Function
        Public Overloads Overrides Function Init() As Boolean
            'Beim initialisieren des Plugin den Screen laden
            'AddHandler GUIGraphicsContext.form.KeyDown, AddressOf MyBase.FormKeyDown
            Return Load(GUIGraphicsContext.Skin + "\ClickfinderProgramGuide.xml")
        End Function

#End Region

#Region "GUI Events"
        Public Overrides Sub PreInit()

            CPGsettings.Load()
            Try
                MyLog.DebugModeOn = CPGsettings.ClickfinderDebugMode
                MyLog.LogFileName = "ClickfinderProgramGuide.log"

                If Helper.TvServerConnected = True Then

                    'nötig weil sonst doppel ausgeführt wird bei MP Start
                    If _OverlayStartupLoaded = False Then
                        MyLog.Info("[PreInit] [BasicHomeOverlay]: ****************** MediaPortal Startup ******************")
                        MyLog.Debug("")
                        MyLog.BackupLogFiles()
                        Helper.LogSettings()

                        Translator.TranslateSkin()

                        RefreshOverlays()
                        _OverlayStartupLoaded = True

                        'Overlays Timer starten
                        StartStopTimer(True)
                    End If

                Else

                    For i = 1 To 4
                        Translator.SetProperty("#ClickfinderPG.Movie" & i & ".Title", "")
                    Next

                    For i = 1 To 4
                        Translator.SetProperty("#ClickfinderPG.Series" & i & ".Title", "")
                    Next

                    MyLog.Warn("[PreInit] [BasicHomeOverlay]: TvServer not online")

                End If

                MyLog.Info("")

            Catch ex As Exception
                MyLog.Error("[StartGuiWindow] [PreInit]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try

            MyBase.PreInit()
        End Sub
        Protected Overrides Sub OnPageLoad()
            MyBase.OnPageLoad()
            GUIWindowManager.NeedRefresh()

            Try
                'CPGsettings.Load()
                'Start GUI
                Select Case CPGsettings.ClickfinderStartGui
                    Case Is = "Highlights"
                        GUIWindowManager.ActivateWindow(1656544656, True)
                    Case Is = "Now"
                        GUIWindowManager.ActivateWindow(1656544654, "CPG.Now", True)
                    Case Is = "PrimeTime"
                        GUIWindowManager.ActivateWindow(1656544654, "CPG.PrimeTime", True)
                    Case Is = "LateTime"
                        GUIWindowManager.ActivateWindow(1656544654, "CPG.LateTime", True)
                    Case Is = "PrimeTimeMovies"

                        Dim _PrimeTime As Date = CPGsettings.PrimeTime
                        Dim _startTime As Date = Today.AddHours(_PrimeTime.Hour).AddMinutes(_PrimeTime.Minute)

                        ItemsGuiWindow.SetGuiProperties("Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                                        "WHERE startTime >= " & Helper.MySqlDate(_startTime) & " " & _
                                                        "AND startTime <= " & Helper.MySqlDate(_startTime.AddHours(4)) & " " & _
                                                        "AND TVMovieBewertung > 1 " & _
                                                        "AND NOT TVMovieBewertung = 6 " & _
                                                        "AND starRating > 1 " & _
                                                        "AND genre NOT LIKE '%Serie%' " & _
                                                        "AND genre NOT LIKE '%Sitcom%' " & _
                                                        "AND genre NOT LIKE '%Zeichentrick%' " & _
                                                        Helper.ORDERBYstartTime, 85)

                        Translator.SetProperty("#ItemsLeftListLabel", Translation.allMoviesAt & " " & Format(_startTime.Hour, "00") & ":" & Format(_startTime.Minute, "00") & " - " & Format(_startTime.AddHours(4).Hour, "00") & ":" & Format(_startTime.AddHours(4).Minute, "00"))
                        GUIWindowManager.ActivateWindow(1656544653, True)

                    Case Is = "LateTimeMovies"

                        Dim _LateTime As Date = CPGsettings.LateTime
                        Dim _startTime As Date = Today.AddHours(_LateTime.Hour).AddMinutes(_LateTime.Minute)

                        ItemsGuiWindow.SetGuiProperties("Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                                        "WHERE startTime >= " & Helper.MySqlDate(_startTime) & " " & _
                                                        "AND startTime <= " & Helper.MySqlDate(_startTime.AddHours(4)) & " " & _
                                                        "AND TVMovieBewertung > 1 " & _
                                                        "AND NOT TVMovieBewertung = 6 " & _
                                                        "AND starRating > 1 " & _
                                                        "AND genre NOT LIKE '%Serie%' " & _
                                                        "AND genre NOT LIKE '%Sitcom%' " & _
                                                        "AND genre NOT LIKE '%Zeichentrick%' " & _
                                                        Helper.ORDERBYstartTime, 85)

                        Translator.SetProperty("#ItemsLeftListLabel", Translation.allMoviesAt & " " & Format(_startTime.Hour, "00") & ":" & Format(_startTime.Minute, "00") & " - " & Format(_startTime.AddHours(4).Hour, "00") & ":" & Format(_startTime.AddHours(4).Minute, "00"))
                        GUIWindowManager.ActivateWindow(1656544653, True)
                End Select

            Catch ex As Exception
                MyLog.Error("[StartGuiWindow] [OnPageLoad]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try

        End Sub
        Public Overrides Sub OnAction(ByVal Action As MediaPortal.GUI.Library.Action)
            MyBase.OnAction(Action)
        End Sub
        Protected Overrides Sub OnPageDestroy(ByVal new_windowId As Integer)

            'GC.Collect()
            MyBase.OnPageDestroy(new_windowId)

        End Sub
        Protected Overrides Sub Finalize()


            MyBase.Finalize()
        End Sub
        Protected Overrides Sub OnClicked(ByVal controlId As Integer, _
                                          ByVal control As GUIControl, _
                                          ByVal actionType As  _
                                          MediaPortal.GUI.Library.Action.ActionType)

            MyBase.OnClicked(controlId, control, actionType)

        End Sub
#End Region

#Region "Functions"

        Friend Shared Sub RefreshOverlays()

            'Movie Overlay aktualisieren
            'MyLog.Debug("[PreInit] [RefreshOverlays]: Thread started")
            If CPGsettings.OverlayMoviesEnabled = True Then
                If _OverlayStartupLoaded = False Then
                    'MP startup
                    ClickfinderProgramGuideOverlayMovies()

                Else
                    'Mp running
                    'Movie Overlay nur aktualisieren wenn 'ab Jetzt' eingestellt ist
                    If CPGsettings.OverlayTime = "Now" Then
                        ClickfinderProgramGuideOverlayMovies()
                    End If
                End If
            End If

            'Serien Overlay werden immer aktualisiert
            If CPGsettings.OverlaySeriesEnabled = True Then
                ClickfinderProgramGuideOverlaySeries()
            End If

        End Sub

        Friend Shared Sub ClickfinderProgramGuideOverlayMovies()

            Dim _PrimeTime As Date = CPGsettings.PrimeTime
            Dim _LateTime As Date = CPGsettings.LateTime

            MyLog.Info("")
            MyLog.Info("[BasicHomeOverlay]: [ClickfinderProgramGuideOverlayMovies]: started ({0},{1})", CPGsettings.OverlayTime, CPGsettings.OverlayTvGroup)

            Try
                Dim _time As Date = Now
                Dim _startTime As Date = Nothing
                Dim _endTime As Date = Nothing

                Dim _OverLayMoviesList As New List(Of TVMovieProgram)
                Dim _ShowTagesTipp As Boolean = False
                Dim _ItemCounter As Integer = 0

                For i = 1 To 4
                    Translator.SetProperty("#ClickfinderPG.Movie" & i & ".Title", "")
                Next

                Select Case (CPGsettings.OverlayTime)
                    Case Is = "Today"
                        _startTime = Date.Today
                        _endTime = _startTime.AddDays(1).AddHours(0).AddMinutes(0)
                    Case Is = "Now"
                        _startTime = Date.Now
                        _endTime = _startTime.AddDays(1).AddHours(0).AddMinutes(0)
                    Case Is = "PrimeTime"
                        _startTime = Date.Today.AddHours(_PrimeTime.Hour).AddMinutes(_PrimeTime.Minute).AddMinutes(-1)
                        _endTime = _startTime.AddHours(4)
                    Case Is = "LateTime"
                        _startTime = Date.Today.AddHours(_LateTime.Hour).AddMinutes(_LateTime.Minute).AddMinutes(-1)
                        _endTime = _startTime.AddHours(4)
                End Select

                'SQLstring: Alle Movies (2 > TvMovieBewertung < 6), inkl. TagesTipps des Tages laden
                Dim _SQLstring As String = _
                    "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                    "WHERE TvMovieBewertung > 2 AND TvMovieBewertung < 6 " & _
                    "AND startTime > " & MySqlDate(_startTime) & " " & _
                    "AND startTime < " & MySqlDate(_endTime) & " " & _
                    "AND genre NOT LIKE '%Serie%' " & _
                    "AND genre NOT LIKE '%Sitcom%' " & _
                    "AND genre NOT LIKE '%Zeichentrick%'"

                'SqlString: Orderby TvMovieBewertung & Limit
                _SQLstring = Helper.AppendSqlLimit(_SQLstring & Helper.ORDERBYtvMovieBewertung, 25)

                'List: Daten laden
                _SQLstring = Replace(_SQLstring, " * ", " TVMovieProgram.idProgram, TVMovieProgram.Action, TVMovieProgram.Actors, TVMovieProgram.BildDateiname, TVMovieProgram.Country, TVMovieProgram.Cover, TVMovieProgram.Describtion, TVMovieProgram.Dolby, TVMovieProgram.EpisodeImage, TVMovieProgram.Erotic, TVMovieProgram.FanArt, TVMovieProgram.Feelings, TVMovieProgram.FileName, TVMovieProgram.Fun, TVMovieProgram.HDTV, TVMovieProgram.idEpisode, TVMovieProgram.idMovingPictures, TVMovieProgram.idSeries, TVMovieProgram.idTVMovieProgram, TVMovieProgram.idVideo, TVMovieProgram.KurzKritik, TVMovieProgram.local, TVMovieProgram.needsUpdate, TVMovieProgram.Regie, TVMovieProgram.Requirement, TVMovieProgram.SeriesPosterImage, TVMovieProgram.ShortDescribtion, TVMovieProgram.Tension, TVMovieProgram.TVMovieBewertung, TVMovieProgram.Year ")
                Dim _SQLstate1 As SqlStatement = Broker.GetStatement(_SQLstring)
                _OverLayMoviesList = ObjectFactory.GetCollection(GetType(TVMovieProgram), _SQLstate1.Execute())

                MyLog.Debug("[BasicHomeOverlay] [ClickfinderProgramGuideOverlayMovies]: {0} Movies loaded from Database (setting = {1})", _OverLayMoviesList.Count, CPGsettings.OverlayTime)

                'List: TvGruppe filter
                _OverLayMoviesList = _OverLayMoviesList.FindAll(Function(x As TVMovieProgram) x.ReferencedProgram.ReferencedChannel.GroupNames.Contains(CPGsettings.OverlayTvGroup))

                'List: nicht local = 0 -> sofern in Settings
                If CPGsettings.OverlayShowLocalMovies = True Then
                    _OverLayMoviesList = _OverLayMoviesList.FindAll(Function(p As TVMovieProgram) p.local = False)
                End If

                MyLog.Debug("[BasicHomeOverlay] [ClickfinderProgramGuideOverlayMovies]: {0} Movies filtered by '{1}' TvGroup and not local = {2}", _OverLayMoviesList.Count, CPGsettings.OverlayTvGroup, CInt(CPGsettings.OverlayShowLocalMovies) * -1)

                'List: sortieren
                Select Case (CPGsettings.OverlayMovieSort)
                    Case Is = SortMethode.startTime.ToString
                        _OverLayMoviesList.Sort(New TVMovieProgram_SortByStartTime)
                    Case Is = SortMethode.TvMovieStar.ToString
                        _OverLayMoviesList.Sort(New TVMovieProgram_SortByTvMovieBewertung)
                    Case Is = SortMethode.RatingStar.ToString
                        _OverLayMoviesList.Sort(New TVMovieProgram_SortByRating)
                End Select

                MyLog.Debug("[BasicHomeOverlay] [ClickfinderProgramGuideOverlayMovies]: {0} Movies sorted by {1}", _OverLayMoviesList.Count, CPGsettings.OverlayMovieSort)

                'GroupBy: Title And EpisodeName (doppelte Einträge raus)
                _OverLayMoviesList = _OverLayMoviesList.Distinct(New TVMovieProgram_GroupByTitleAndEpisodeName()).ToList


                'List: TagesTipp laden
                Dim _TagesTippList As New List(Of TVMovieProgram)
                If CPGsettings.OverlayShowTagesTipp = True Then
                    _SQLstring = _
                        "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                        "WHERE TvMovieBewertung = 4 " & _
                        "AND startTime > " & MySqlDate(Today) & " " & _
                        "AND startTime < " & MySqlDate(Today.AddDays(1)) & " " & _
                        "AND genre NOT LIKE '%Serie%' " & _
                        "AND genre NOT LIKE '%Sitcom%' " & _
                        "AND genre NOT LIKE '%Zeichentrick%'"

                    _SQLstring = Replace(_SQLstring, " * ", " TVMovieProgram.idProgram, TVMovieProgram.Action, TVMovieProgram.Actors, TVMovieProgram.BildDateiname, TVMovieProgram.Country, TVMovieProgram.Cover, TVMovieProgram.Describtion, TVMovieProgram.Dolby, TVMovieProgram.EpisodeImage, TVMovieProgram.Erotic, TVMovieProgram.FanArt, TVMovieProgram.Feelings, TVMovieProgram.FileName, TVMovieProgram.Fun, TVMovieProgram.HDTV, TVMovieProgram.idEpisode, TVMovieProgram.idMovingPictures, TVMovieProgram.idSeries, TVMovieProgram.idTVMovieProgram, TVMovieProgram.idVideo, TVMovieProgram.KurzKritik, TVMovieProgram.local, TVMovieProgram.needsUpdate, TVMovieProgram.Regie, TVMovieProgram.Requirement, TVMovieProgram.SeriesPosterImage, TVMovieProgram.ShortDescribtion, TVMovieProgram.Tension, TVMovieProgram.TVMovieBewertung, TVMovieProgram.Year ")
                    Dim _SQLstate2 As SqlStatement = Broker.GetStatement(_SQLstring)
                    _TagesTippList = ObjectFactory.GetCollection(GetType(TVMovieProgram), _SQLstate2.Execute())
                    _TagesTippList = _TagesTippList.GetRange(0, 1)

                    'TagesTipp vorhanden -> Listcontrol
                    If _TagesTippList.Count > 0 Then
                        _ShowTagesTipp = True

                        _OverLayMoviesList.RemoveAll(Function(x As TVMovieProgram) x.TVMovieBewertung = 4)

                        MyLog.Debug("[BasicHomeOverlay] [ClickfinderProgramGuideOverlayMovies]: TagesTipp added to cache ({0} ,{1} ,{2})", _TagesTippList(0).ReferencedProgram.Title, _TagesTippList(0).ReferencedProgram.ReferencedChannel.DisplayName, _TagesTippList(0).ReferencedProgram.StartTime)
                    Else
                        MyLog.Warn("[BasicHomeOverlay] [ClickfinderProgramGuideOverlayMovies]: No TagesTipp - {0}", Today)
                    End If
                End If

                'TagesTip evtl. hinzufügen
                If _ShowTagesTipp = True Then
                    _OverLayMoviesList = _OverLayMoviesList.GetRange(0, 3)
                    _OverLayMoviesList.Insert(0, _TagesTippList(0))
                Else
                    _OverLayMoviesList = _OverLayMoviesList.GetRange(0, 4)
                End If

                'Overlay properties übergeben
                For Each _TvMovieProgram In _OverLayMoviesList
                    _ItemCounter = _ItemCounter + 1

                    Translator.SetProperty("#ClickfinderPG.Movie" & _ItemCounter & ".Title", _TvMovieProgram.ReferencedProgram.Title)
                    Translator.SetProperty("#ClickfinderPG.Movie" & _ItemCounter & ".RatingStar", _TvMovieProgram.ReferencedProgram.StarRating)
                    Translator.SetProperty("#ClickfinderPG.Movie" & _ItemCounter & ".Time", GuiLayout.TimeLabel(_TvMovieProgram))
                    Translator.SetProperty("#ClickfinderPG.Movie" & _ItemCounter & ".Channel", _TvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName)
                    Translator.SetProperty("#ClickfinderPG.Movie" & _ItemCounter & ".Image", GuiLayout.Image(_TvMovieProgram))
                    Translator.SetProperty("#ClickfinderPG.Movie" & _ItemCounter & ".TvMovieStar", GuiLayout.TvMovieStar(_TvMovieProgram))
                    Translator.SetProperty("#ClickfinderPG.Movie" & _ItemCounter & ".Genre", _TvMovieProgram.ReferencedProgram.Genre)
                Next

                MyLog.Info("[BasicHomeOverlay]: [ClickfinderProgramGuideOverlayMovies]: finished in {0} s", DateDiff(DateInterval.Second, _time, Now))

            Catch ex2 As Exception
                Log.Error("[BasicHomeOverlay] [ClickfinderProgramGuideOverlayMovies]: exception err:" & ex2.Message & " stack:" & ex2.StackTrace)
            End Try
        End Sub
        Friend Shared Sub ClickfinderProgramGuideOverlaySeries()
            Try
                Dim _ItemCounter As Integer = 1
                Dim _time As Date = Now

                For i = 1 To 4
                    Translator.SetProperty("#ClickfinderPG.Series" & i & ".Title", "")
                Next

                MyLog.Info("")
                MyLog.Info("[BasicHomeOverlay]: [ClickfinderProgramGuideOverlaySeries]: started")

                Dim _NewEpisodesList As New List(Of TVMovieProgram)
                Dim _AllNewEpisodesList As New List(Of TVMovieProgram)
                Dim _RepeatsScheduledRecordings As New List(Of Program)

                'Alle neuen Episoden des Tages laden, sortiert nach State
                Dim _SQLstring As String = "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                            "WHERE idSeries > 0 " & _
                                            "AND local = 0 " & _
                                            "AND startTime > " & MySqlDate(Date.Today.AddHours(0)) & " " & _
                                            "AND startTime < " & MySqlDate(Date.Today.AddHours(24).AddHours(0).AddMinutes(0)) & " " & _
                                            "Order BY state DESC"

                _SQLstring = Replace(_SQLstring, " * ", " TVMovieProgram.idProgram, TVMovieProgram.Action, TVMovieProgram.Actors, TVMovieProgram.BildDateiname, TVMovieProgram.Country, TVMovieProgram.Cover, TVMovieProgram.Describtion, TVMovieProgram.Dolby, TVMovieProgram.EpisodeImage, TVMovieProgram.Erotic, TVMovieProgram.FanArt, TVMovieProgram.Feelings, TVMovieProgram.FileName, TVMovieProgram.Fun, TVMovieProgram.HDTV, TVMovieProgram.idEpisode, TVMovieProgram.idMovingPictures, TVMovieProgram.idSeries, TVMovieProgram.idTVMovieProgram, TVMovieProgram.idVideo, TVMovieProgram.KurzKritik, TVMovieProgram.local, TVMovieProgram.needsUpdate, TVMovieProgram.Regie, TVMovieProgram.Requirement, TVMovieProgram.SeriesPosterImage, TVMovieProgram.ShortDescribtion, TVMovieProgram.Tension, TVMovieProgram.TVMovieBewertung, TVMovieProgram.Year ")
                Dim _SQLstate As SqlStatement = Broker.GetStatement(_SQLstring)
                _AllNewEpisodesList = ObjectFactory.GetCollection(GetType(TVMovieProgram), _SQLstate.Execute())

                'Alle doppelten Episoden raus
                _AllNewEpisodesList = _AllNewEpisodesList.Distinct(New TVMovieProgram_GroupByTitleAndEpisodeName).ToList

                MyLog.Debug("[BasicHomeOverlay] [ClickfinderProgramGuideOverlaySeries]: {0} new Episodes today found", _AllNewEpisodesList.Count)

                'prüfen ob inzwischen local vorhanden
                For Each _NewEpisode In _AllNewEpisodesList
                    CheckSeriesLocalStatus(_NewEpisode)

                    If _NewEpisode.local = False Then
                        _NewEpisodesList.Add(_NewEpisode)
                    End If
                Next

                MyLog.Debug("[BasicHomeOverlay] [ClickfinderProgramGuideOverlaySeries]: {0} new Episodes today found, after checking local status", _NewEpisodesList.Count)

                Dim tmp As List(Of TVMovieProgram) = _NewEpisodesList.Distinct(New TVMovieProgram_GroupByIdSeries).ToList
                tmp.Sort(New TVMovieProgram_SortByStartTime)

                'Nur vier Serien anzeigen, sofern mehr vorhanden
                If tmp.Count > 4 Then
                    tmp = tmp.GetRange(0, 4)
                End If

                For Each _NewEpisode In tmp

                    Translator.SetProperty("#ClickfinderPG.Series" & _ItemCounter & ".Image", Config.GetFile(Config.Dir.Thumbs, "MPTVSeriesBanners\") & _NewEpisode.SeriesPosterImage)
                    Translator.SetProperty("#ClickfinderPG.Series" & _ItemCounter & ".Title", _NewEpisode.ReferencedProgram.Title)
                    Translator.SetProperty("#ClickfinderPG.Series" & _ItemCounter & ".RatingStar", _NewEpisode.ReferencedProgram.StarRating)
                    Translator.SetProperty("#ClickfinderPG.Series" & _ItemCounter & ".CountNewEpisodes", _NewEpisodesList.FindAll(Function(x As TVMovieProgram) x.idSeries = _NewEpisode.idSeries).Count & " " & Translation.NewEpisodeFound)
                    Translator.SetProperty("#ClickfinderPG.Series" & _ItemCounter & ".Time", _NewEpisode.ReferencedProgram.ReferencedChannel.DisplayName & " - " & CStr(Format(_NewEpisode.ReferencedProgram.StartTime.Hour, "00") & ":" & Format(_NewEpisode.ReferencedProgram.StartTime.Minute, "00")))

                    Dim _RecordingStatus As String = GuiLayout.RecordingStatus(_NewEpisode.ReferencedProgram)

                    'Falls nicht als Aufnahme geplant, prüfen ob an anderem Tag / Uhrzeit
                    If _RecordingStatus = String.Empty Then
                        _SQLstring = _
                                "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                "WHERE idSeries = " & _NewEpisode.idSeries & " " & _
                                "AND local = 0 " & _
                                "AND episodeName = '" & TVSeriesDB.allowedSigns(_NewEpisode.ReferencedProgram.EpisodeName) & "' " & _
                                "ORDER BY state DESC"

                        _SQLstring = Helper.AppendSqlLimit(_SQLstring, 1)

                        _SQLstring = Replace(_SQLstring, " * ", " Program.IdProgram, Program.Classification, Program.Description, Program.EndTime, Program.EpisodeName, Program.EpisodeNum, Program.EpisodePart, Program.Genre, Program.IdChannel, Program.OriginalAirDate, Program.ParentalRating, Program.SeriesNum, Program.StarRating, Program.StartTime, Program.state, Program.Title ")
                        Dim _SQLstate4 As SqlStatement = Broker.GetStatement(_SQLstring)
                        _RepeatsScheduledRecordings = ObjectFactory.GetCollection(GetType(Program), _SQLstate4.Execute())

                        If _RepeatsScheduledRecordings.Count > 0 Then
                            _RecordingStatus = GuiLayout.RecordingStatus(_RepeatsScheduledRecordings.Item(0))

                            'MsgBox("episode: " & _Episode.ReferencedProgram.Title & " - " & _Episode.ReferencedProgram.EpisodeName & vbNewLine & _
                            '       "recState: " & _RecordingStatus)

                            Select Case (_RecordingStatus)
                                Case Is = "tvguide_record_button.png"
                                    _RecordingStatus = "ClickfinderPG_recOnce.png"
                                Case Is = "tvguide_recordserie_button.png"
                                    _RecordingStatus = "ClickfinderPG_recSeries.png"
                                Case Else
                                    _RecordingStatus = String.Empty
                            End Select
                        End If
                    End If

                    Translator.SetProperty("#ClickfinderPG.Series" & _ItemCounter & ".RecordingState", _RecordingStatus)

                    _ItemCounter = _ItemCounter + 1
                Next


                MyLog.Info("[BasicHomeOverlay]: [ClickfinderProgramGuideOverlayMovies]: finished in {0} s", DateDiff(DateInterval.Second, _time, Now))

            Catch ex2 As Exception
                Log.Error("[StartGuiWindow] [ClickfinderProgramGuideOverlaySeries]: exception err:" & ex2.Message & " stack:" & ex2.StackTrace)
            End Try

        End Sub

        Private Sub StartStopTimer(ByVal startNow As Boolean)

            If startNow Then
                If _stateTimer Is Nothing Then
                    _stateTimer = New System.Timers.Timer()
                    AddHandler _stateTimer.Elapsed, New ElapsedEventHandler(AddressOf RefreshOverlays)
                    _stateTimer.Interval = CLng(CPGsettings.OverlayUpdateTimer * 60 * 1000)
                    _stateTimer.AutoReset = True

                    GC.KeepAlive(_stateTimer)
                End If
                _stateTimer.Start()
                _stateTimer.Enabled = True

            Else
                _stateTimer.Enabled = False
                _stateTimer.[Stop]()
                MyLog.Debug("background timer stopped")
            End If
        End Sub


#End Region



    End Class
End Namespace
