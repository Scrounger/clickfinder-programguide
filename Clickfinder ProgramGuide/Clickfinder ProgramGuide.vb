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

Namespace ClickfinderProgramGuide
    <PluginIcons("ClickfinderProgramGuide.Config.png", "ClickfinderProgramGuide.Config_disable.png")> _
    Public Class StartGuiWindow

        Inherits GUIWindow
        Implements ISetupForm

#Region "Skin Controls"

        'Buttons
        <SkinControlAttribute(2)> Protected _btnNow As GUIButtonControl = Nothing
        <SkinControlAttribute(3)> Protected _btnPrimeTime As GUIButtonControl = Nothing
        <SkinControlAttribute(4)> Protected _btnLateTime As GUIButtonControl = Nothing
        <SkinControlAttribute(5)> Protected _btnAllMovies As GUIButtonControl = Nothing
        <SkinControlAttribute(6)> Protected _btnHighlights As GUIButtonControl = Nothing
        <SkinControlAttribute(7)> Protected _btnPreview As GUIButtonControl = Nothing


        'ProgressBar
        <SkinControlAttribute(9)> Protected _HighlightsProgressBar As GUIAnimation = Nothing
        <SkinControlAttribute(8)> Protected _MoviesProgressBar As GUIAnimation = Nothing
        <SkinControlAttribute(11)> Protected _DaysProgress As GUIProgressControl = Nothing

        'ListControl
        <SkinControlAttribute(10)> Protected _MovieList As GUIListControl = Nothing
        <SkinControlAttribute(30)> Protected _HighlightsList As GUIListControl = Nothing

#End Region

#Region "Members"
        Private _layer As New TvBusinessLayer
        Friend Shared _DebugModeOn As Boolean = False
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
            setup.Show()
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
            MyBase.PreInit()

            If CBool(_layer.GetSetting("ClickfinderOverlayMoviesEnabled", "false").Value) = True Then
                ClickfinderProgramGuideOverlayMovies()
            End If

            If CBool(_layer.GetSetting("ClickfinderOverlaySeriesEnabled", "false").Value) = True Then
                ClickfinderProgramGuideOverlaySeries()
            End If

        End Sub
        Protected Overrides Sub OnPageLoad()
            MyBase.OnPageLoad()
            GUIWindowManager.NeedRefresh()

            Dim _Thread4 As New Thread(AddressOf Translator.TranslateSkin)
            _Thread4.Start()

            CheckConnectionState(GetID)

            If _layer.GetSetting("TvMovieImportIsRunning", "false").Value = "true" Then
                Translator.SetProperty("#SettingLastUpdate", Translation.ImportIsRunning)
                MyLog.Debug("[HighlightsGuiWindow] [OnPageLoad]: {0}", "TvMovie++ Import is running !")
            Else
                Translator.SetProperty("#SettingLastUpdate", GuiLayout.LastUpdateLabel)
            End If

            If CBool(_layer.GetSetting("ClickfinderDebugMode").Value) = True Then
                _DebugModeOn = True
            End If


            Select Case _layer.GetSetting("ClickfinderStartGui", "Highlights").Value
                Case Is = "Highlights"
                    GUIWindowManager.ReplaceWindow(1656544656)
                    GuiButtons.Highlights()
                Case Is = "Now"
                    GUIWindowManager.ReplaceWindow(1656544654)
                    GuiButtons.Now()
                Case Is = "PrimeTime"
                    GUIWindowManager.ReplaceWindow(1656544654)
                    GuiButtons.PrimeTime()
                Case Is = "LateTime"
                    GUIWindowManager.ReplaceWindow(1656544654)
                    GuiButtons.LateTime()
                Case Is = "PrimeTimeMovies"
                    GUIWindowManager.ReplaceWindow(1656544653)
                    Dim _PrimeTime As Date = CDate(_layer.GetSetting("ClickfinderPrimeTime", "22:00").Value)
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
                    GUIWindowManager.ActivateWindow(1656544653)

                Case Is = "LateTimeMovies"
                    GUIWindowManager.ReplaceWindow(1656544653)

                    Dim _LateTime As Date = CDate(_layer.GetSetting("ClickfinderLateTime", "22:00").Value)
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
                    GUIWindowManager.ActivateWindow(1656544653)
            End Select

            'CategoriesGuiWindow.SetGuiProperties(CategoriesGuiWindow.CategorieView.Now)
            'GUIWindowManager.ReplaceWindow(1656544656)
            'GUIWindowManager.ActivateWindow(1656544656)

        End Sub
        Public Overrides Sub OnAction(ByVal Action As MediaPortal.GUI.Library.Action)
            MyBase.OnAction(Action)
        End Sub
        Protected Overrides Sub OnPageDestroy(ByVal new_windowId As Integer)

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
        Private Sub ClickfinderProgramGuideOverlayMovies()
            Dim _lastTitle As String = String.Empty
            Dim _ItemCounter As Integer = 0
            Dim _timeLabel As String = String.Empty
            Dim _infoLabel As String = String.Empty
            Dim _imagepath As String = String.Empty
            Dim _SQLstring As String = String.Empty
            Dim _idProgramTagesTipp As Integer = 0
            Dim _LogLocalMovies As String = String.Empty
            Dim _LogLocalSortedBy As String = String.Empty
            Dim _logShowTagesTipp As String = "false"
            Dim _startTime As Date = Nothing
            Dim _endTime As Date = Nothing

            Dim _layer As New TvBusinessLayer

            Dim _PrimeTime As Date = CDate(_layer.GetSetting("ClickfinderPrimeTime", "20:15").Value)
            Dim _LateTime As Date = CDate(_layer.GetSetting("ClickfinderLateTime", "22:00").Value)

            Log.Debug("")
            Log.Debug("[PreInit] [BasicHomeOverlay]: Thread started")


            Try

                For i = 1 To 4
                    Translator.SetProperty("#ClickfinderPG.Movie" & i & ".Title", "")
                    Translator.SetProperty("#ClickfinderPG.Movie" & i & ".RatingStar", "")
                    Translator.SetProperty("#ClickfinderPG.Movie" & i & ".Time", "")
                    Translator.SetProperty("#ClickfinderPG.Movie" & i & ".Channel", "")
                    Translator.SetProperty("#ClickfinderPG.Movie" & i & ".Image", "")
                    Translator.SetProperty("#ClickfinderPG.Movie" & i & ".TvMovieStar", "")
                    Translator.SetProperty("#ClickfinderPG.Movie" & i & ".Genre", "")
                Next

                Dim _Result As New ArrayList

                '[Optional] TvMovie Tages Tipp immer als erstes anzeigen
                If CBool(_layer.GetSetting("ClickfinderOverlayShowTagesTipp").Value) = True Then
                    _SQLstring = _
                    "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                    "WHERE TvMovieBewertung = 4 " & _
                    "AND program.startTime > " & MySqlDate(Date.Today) & " " & _
                    "AND program.startTime < " & MySqlDate(Date.Today.AddHours(24))

                    Dim _TvMovieTagesTipps As New ArrayList(Broker.Execute(_SQLstring).TransposeToFieldList("idProgram", False))

                    If _TvMovieTagesTipps.Count > 0 Then

                        For i = 0 To _TvMovieTagesTipps.Count - 1
                            Dim _TvMovieProgram As TVMovieProgram = TVMovieProgram.Retrieve(_TvMovieTagesTipps.Item(i))

                            ''Prüfen ob in Standard Tv Gruppe
                            'Dim key As New Gentle.Framework.Key(GetType(ChannelGroup), True, "groupName", _layer.GetSetting("ClickfinderOverlayTvGroup", "All Channels").Value)
                            'Dim _Group As ChannelGroup = Gentle.Framework.Broker.RetrieveInstance(Of ChannelGroup)(key)

                            ''Alle Gruppen des _program laden
                            'Dim sb As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(GroupMap))
                            'sb.AddConstraint([Operator].Equals, "idgroup", _Group.IdGroup)
                            'sb.AddConstraint([Operator].Equals, "idChannel", _TvMovieProgram.ReferencedProgram.IdChannel)
                            'Dim stmt As SqlStatement = sb.GetStatement(True)
                            'Dim _isInFavGroup As IList(Of GroupMap) = ObjectFactory.GetCollection(GetType(GroupMap), stmt.Execute())

                            'If _isInFavGroup.Count = 0 Then
                            '    Continue For
                            'End If

                            _Result.Add(_TvMovieTagesTipps.Item(i))

                            _idProgramTagesTipp = _TvMovieTagesTipps.Item(i)

                        Next
                        _logShowTagesTipp = "true"
                    Else
                        _logShowTagesTipp = "not found"
                    End If

                End If

                Select Case (_layer.GetSetting("ClickfinderOverlayTime", "PrimeTime").Value)
                    Case Is = "Today"
                        _startTime = Date.Today
                        _endTime = _startTime.AddHours(24)
                    Case Is = "Now"
                        _startTime = Date.Now
                        _endTime = _startTime.AddHours(4)
                    Case Is = "PrimeTime"
                        _startTime = Date.Today.AddHours(_PrimeTime.Hour).AddMinutes(_PrimeTime.Minute)
                        _endTime = _startTime.AddHours(4)
                    Case Is = "LateTime"
                        _startTime = Date.Today.AddHours(_LateTime.Hour).AddMinutes(_LateTime.Minute)
                        _endTime = _startTime.AddHours(4)
                End Select

                _SQLstring = _
                   "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                   "WHERE starRating > 1 " & _
                   "AND startTime >= " & MySqlDate(_startTime) & " " & _
                   "AND startTime <= " & MySqlDate(_endTime) & " " & _
                   "AND genre NOT LIKE '%Serie%' " & _
                   "AND genre NOT LIKE '%Sitcom%' " & _
                   "AND genre NOT LIKE '%Zeichentrick%' "

                Select Case (_layer.GetSetting("ClickfinderOverlayMovieSort", SortMethode.startTime.ToString).Value)
                    Case Is = SortMethode.startTime.ToString
                        _LogLocalSortedBy = SortMethode.startTime.ToString
                        _SQLstring = _SQLstring & Helper.ORDERBYstartTime

                    Case Is = SortMethode.TvMovieStar.ToString
                        _LogLocalSortedBy = SortMethode.TvMovieStar.ToString
                        _SQLstring = _SQLstring & Helper.ORDERBYtvMovieBewertung

                    Case Is = SortMethode.RatingStar.ToString
                        _LogLocalSortedBy = SortMethode.RatingStar.ToString
                        _SQLstring = _SQLstring & Helper.ORDERBYstarRating
                End Select

                _SQLstring = _SQLstring & " LIMIT " & _layer.GetSetting("ClickfinderOverlayMovieLimit", "10").Value

                Log.Debug("[PreInit] [BasicHomeOverlay]: sorted by {0}, show TvMovie TagesTipp = {1}, SQLString: {2}", _LogLocalSortedBy, _logShowTagesTipp, _SQLstring)

                _Result.AddRange(Broker.Execute(_SQLstring).TransposeToFieldList("idProgram", False))

                Log.Debug("[PreInit] [BasicHomeOverlay]: {0} program found", _Result.Count)

                If _Result.Count > 0 Then

                    For i = 0 To _Result.Count - 1
                        Try

                            'ProgramDaten über TvMovieProgram laden
                            Dim _TvMovieProgram As TVMovieProgram = TVMovieProgram.Retrieve(_Result.Item(i))

                            '[Option] Falls Datei lokal existiert, dann nicht anzeigen (-> Continue For)
                            'Wenn TagesTipp aktiviert, dann nicht ausführen
                            If CBool(_layer.GetSetting("ClickfinderOverlayShowLocalMovies", "false").Value) = True And Not _TvMovieProgram.idProgram = _idProgramTagesTipp Then
                                If _TvMovieProgram.local = True Then
                                    _LogLocalMovies = _LogLocalMovies & _TvMovieProgram.ReferencedProgram.Title & ", "
                                    Continue For
                                End If
                            End If

                            'Wenn TagesTipp immer anzeigen aktiviert, prüfen ob TagesTipp, dann weiter
                            If _TvMovieProgram.idProgram = _idProgramTagesTipp And _ItemCounter > 1 Then
                                Continue For
                            End If

                            'Prüfen ob in Standard Tv Gruppe, nur wenn nicht Tagestipp
                            If Not _TvMovieProgram.idProgram = _idProgramTagesTipp Then
                                Dim key As New Gentle.Framework.Key(GetType(ChannelGroup), True, "groupName", _layer.GetSetting("ClickfinderOverlayTvGroup", "All Channels").Value)
                                Dim _Group As ChannelGroup = Gentle.Framework.Broker.RetrieveInstance(Of ChannelGroup)(key)

                                'Alle Gruppen des _program laden
                                Dim sb As New SqlBuilder(Gentle.Framework.StatementType.Select, GetType(GroupMap))
                                sb.AddConstraint([Operator].Equals, "idgroup", _Group.IdGroup)
                                sb.AddConstraint([Operator].Equals, "idChannel", _TvMovieProgram.ReferencedProgram.IdChannel)
                                Dim stmt As SqlStatement = sb.GetStatement(True)
                                Dim _isInFavGroup As IList(Of GroupMap) = ObjectFactory.GetCollection(GetType(GroupMap), stmt.Execute())

                                If _isInFavGroup.Count = 0 Then
                                    Continue For
                                End If
                            End If


                            'Prüfen ob schon in der Liste vorhanden
                            If String.Equals(_lastTitle, _TvMovieProgram.ReferencedProgram.Title & _TvMovieProgram.ReferencedProgram.EpisodeName) = False Then

                                _ItemCounter = _ItemCounter + 1

                                Translator.SetProperty("#ClickfinderPG.Movie" & _ItemCounter & ".Title", _TvMovieProgram.ReferencedProgram.Title)
                                Translator.SetProperty("#ClickfinderPG.Movie" & _ItemCounter & ".RatingStar", _TvMovieProgram.ReferencedProgram.StarRating)
                                Translator.SetProperty("#ClickfinderPG.Movie" & _ItemCounter & ".Time", GuiLayout.TimeLabel(_TvMovieProgram))
                                Translator.SetProperty("#ClickfinderPG.Movie" & _ItemCounter & ".Channel", _TvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName)
                                Translator.SetProperty("#ClickfinderPG.Movie" & _ItemCounter & ".Image", GuiLayout.Image(_TvMovieProgram))
                                Translator.SetProperty("#ClickfinderPG.Movie" & _ItemCounter & ".TvMovieStar", GuiLayout.TvMovieStar(_TvMovieProgram))
                                Translator.SetProperty("#ClickfinderPG.Movie" & _ItemCounter & ".Genre", _TvMovieProgram.ReferencedProgram.Genre)

                                Log.Debug("[PreInit] [BasicHomeOverlay]: Load global SkinProperties {0} (Title: {1}, Channel: {2}, startTime: {3}, idprogram: {4}, ratingStar: {5}, TvMovieStar: {6}, image: {7})", _
                                            _ItemCounter, _TvMovieProgram.ReferencedProgram.Title, _TvMovieProgram.ReferencedProgram.ReferencedChannel.DisplayName, _
                                            _TvMovieProgram.ReferencedProgram.StartTime, _TvMovieProgram.ReferencedProgram.IdProgram, _
                                            GuiLayout.ratingStar(_TvMovieProgram.ReferencedProgram), _
                                            _TvMovieProgram.TVMovieBewertung, GuiLayout.Image(_TvMovieProgram))

                                _lastTitle = _TvMovieProgram.ReferencedProgram.Title & _TvMovieProgram.ReferencedProgram.EpisodeName

                            End If

                            If _ItemCounter = 4 Then Exit For


                            'log Ausgabe abfangen, falls der Thread abgebrochen wird
                        Catch ex As ThreadAbortException ' Ignore this exception
                            '_isAbortException = True
                            Log.Debug("[PreInit] [BasicHomeOverlay]: --- THREAD ABORTED ---")
                            Log.Debug("")
                        Catch ex As GentleException
                        Catch ex As Exception
                            Log.Error("[PreInit] [BasicHomeOverlay]: Loop exception err:" & ex.Message & " stack:" & ex.StackTrace)
                        End Try
                    Next
                End If

                If CBool(_layer.GetSetting("ClickfinderOverlayShowLocalMovies", "false").Value) = True Then
                    Log.Debug("[PreInit] [BasicHomeOverlay]: Movies exist local and will not be displayed ({0})", _LogLocalMovies)
                End If

                Log.Debug("[PreInit] [BasicHomeOverlay]: Thread finished")
                Log.Debug("")
                'log Ausgabe abfangen, falls der Thread abgebrochen wird
            Catch ex2 As ThreadAbortException ' Ignore this exception
                Log.Debug("[PreInit] [BasicHomeOverlay]: --- THREAD ABORTED ---")
                Log.Debug("")
            Catch ex2 As GentleException
            Catch ex2 As Exception
                Log.Error("[PreInit] [BasicHomeOverlay]: exception err:" & ex2.Message & " stack:" & ex2.StackTrace)
            End Try
        End Sub
        Private Sub ClickfinderProgramGuideOverlaySeries()

            Try

                Dim _ItemCounter As Integer = 0
                Dim SQLstring As String = String.Empty
                Dim _Result As New ArrayList

                For i = 1 To 4
                    Translator.SetProperty("#ClickfinderPG.Series" & i & ".Image", "")
                    Translator.SetProperty("#ClickfinderPG.Series" & i & ".Title", "")
                Next

                If CBool(_layer.GetSetting("TvMovieImportTvSeriesInfos", "false").Value) = True Then

                    SQLstring = "Select * from program INNER JOIN TvMovieProgram ON program.idprogram = TvMovieProgram.idProgram " & _
                                        "WHERE idSeries > 0 " & _
                                        "AND local = 0 " & _
                                        "AND startTime > " & MySqlDate(Today.AddHours(0)) & " " & _
                                        "AND startTime < " & MySqlDate(Today.AddHours(24)) & " " & _
                                        "GROUP BY title " & _
                                        Helper.ORDERBYstartTime
                    _Result.AddRange(Broker.Execute(SQLstring).TransposeToFieldList("idProgram", True))
                End If

                For i = 0 To _Result.Count - 1

                    _ItemCounter = _ItemCounter + 1

                    'ProgramDaten über TvMovieProgram laden
                    Dim _TvMovieProgram As TVMovieProgram = TVMovieProgram.Retrieve(_Result.Item(i))

                    Translator.SetProperty("#ClickfinderPG.Series" & _ItemCounter & ".Image", Config.GetFile(Config.Dir.Thumbs, "MPTVSeriesBanners\") & _TvMovieProgram.SeriesPosterImage)
                    Translator.SetProperty("#ClickfinderPG.Series" & _ItemCounter & ".Title", _TvMovieProgram.ReferencedProgram.Title)
                    Translator.SetProperty("#ClickfinderPG.Series" & _ItemCounter & ".RatingStar", _TvMovieProgram.ReferencedProgram.StarRating)

                    If _ItemCounter = 4 Then Exit For
                Next

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Sub
#End Region



    End Class
End Namespace
