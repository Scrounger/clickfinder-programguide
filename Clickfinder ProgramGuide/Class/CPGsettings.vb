Imports MediaPortal.GUI.Library
Imports TvDatabase
Imports TvPlugin
Imports MediaPortal.Dialogs
Imports MediaPortal.GUI.Library.GUIWindow
Imports ClickfinderProgramGuide.ClickfinderProgramGuide
Imports System.Drawing
Imports Gentle.Framework
Imports Gentle.Common
Imports MediaPortal.Configuration
Imports ClickfinderProgramGuide.TvDatabase
Imports MediaPortal.GUI.Home
Imports MediaPortal.GUI.Video
Imports MediaPortal.Player
Imports MediaPortal.Playlists
Imports MediaPortal.TagReader
Imports System.Threading
Imports System.Globalization
Imports ClickfinderProgramGuide.Helper
Imports System.Reflection


Namespace ClickfinderProgramGuide
    <Serializable()> _
    Public Class CPGsettings

        Private Shared _layer As New TvBusinessLayer


#Region "Properties"
#Region "Clickfinder PG settings"
        Private Shared m_OverviewShowMoviesAfter As Setting
        Private Shared m_OverviewShowHighlightsAfter As Setting
        Private Shared m_OverviewShowLocalMovies As Setting
        Private Shared m_OverviewHighlightsMinRuntime As Setting
        Private Shared m_OverviewMaxDays As Setting
        Private Shared m_OverviewShowTagesTipp As Setting
        Private Shared m_OverviewMovieSort As Setting

        Private Shared m_StandardTvGroup As Setting
        Private Shared m_QuickTvGroup1 As Setting
        Private Shared m_QuickTvGroup2 As Setting



        Private Shared m_pluginTvWishList As Setting
        Private Shared m_pluginClickfinderProgramGuide As Setting

        Private Shared m_PreviewMaxDays As Setting
        Private Shared m_PreviewMinTvMovieRating As Setting

        Private Shared m_NowOffset As Setting

        Private Shared m_ItemsShowLocalMovies As Setting
        Private Shared m_ItemsShowLocalSeries As Setting

        Private Shared m_RemberSortedBy As Setting

        Private Shared m_PrimeTime As Setting
        Private Shared m_LateTime As Setting
        Private Shared m_UseSportLogos As Setting
        Private Shared m_EpisodenScanner As Setting

        Private Shared m_ClickfinderDatabasePath As Setting
        Private Shared m_ClickfinderImagePath As Setting
        Private Shared m_ClickfinderPluginName As Setting
        Private Shared m_ClickfinderDebugMode As Setting
        Private Shared m_ClickfinderStartGui As Setting


        Private Shared m_CategorieShowLocalMovies As Setting
        Private Shared m_CategorieShowLocalSeries As Setting



        Private Shared m_DetailUseSeriesDescribtion As Setting
        Private Shared m_DetailSeriesImage As Setting

        Private Shared m_OverlayMoviesEnabled As Setting
        Private Shared m_OverlayTime As Setting
        Private Shared m_OverlaySeriesEnabled As Setting
        Private Shared m_OverlayShowTagesTipp As Setting
        Private Shared m_OverlayMovieSort As Setting
        Private Shared m_OverlayMovieLimit As Setting
        Private Shared m_OverlayShowLocalMovies As Setting
        Private Shared m_OverlayTvGroup As Setting
        Private Shared m_OverlayUpdateTimer As Setting

        Public Shared Property OverviewShowMoviesAfter() As Double
            Get
                Return m_OverviewShowMoviesAfter.Value
            End Get
            Set(ByVal value As Double)
                m_OverviewShowMoviesAfter.Value = value
            End Set
        End Property
        Public Shared Property OverviewShowHighlightsAfter() As Double
            Get
                Return m_OverviewShowHighlightsAfter.Value
            End Get
            Set(ByVal value As Double)
                m_OverviewShowHighlightsAfter.Value = value
            End Set
        End Property
        Public Shared Property OverviewHighlightsMinRuntime() As Long
            Get
                Return m_OverviewHighlightsMinRuntime.Value
            End Get
            Set(ByVal value As Long)
                m_OverviewHighlightsMinRuntime.Value = value
            End Set
        End Property
        Public Shared Property OverviewShowLocalMovies() As Boolean
            Get
                Return m_OverviewShowLocalMovies.Value
            End Get
            Set(ByVal value As Boolean)
                m_OverviewShowLocalMovies.Value = value
            End Set
        End Property
        Public Shared Property OverviewMaxDays() As Double
            Get
                Return m_OverviewMaxDays.Value
            End Get
            Set(ByVal value As Double)
                m_OverviewMaxDays.Value = value
            End Set
        End Property
        Public Shared Property OverviewShowTagesTipp() As Boolean
            Get
                Return m_OverviewShowTagesTipp.Value
            End Get
            Set(ByVal value As Boolean)
                m_OverviewShowTagesTipp.Value = value
            End Set
        End Property
        Public Shared Property OverviewMovieSort() As String
            Get
                Return m_OverviewMovieSort.Value
            End Get
            Set(ByVal value As String)
                m_OverviewMovieSort.Value = value
            End Set
        End Property

        Public Shared Property StandardTvGroup() As String
            Get
                Return m_StandardTvGroup.Value
            End Get
            Set(ByVal value As String)
                m_StandardTvGroup.Value = value
            End Set
        End Property
        Public Shared Property QuickTvGroup1() As String
            Get
                Return m_QuickTvGroup1.Value
            End Get
            Set(ByVal value As String)
                m_QuickTvGroup1.Value = value
            End Set
        End Property
        Public Shared Property QuickTvGroup2() As String
            Get
                Return m_QuickTvGroup2.Value
            End Get
            Set(ByVal value As String)
                m_QuickTvGroup2.Value = value
            End Set
        End Property

        Public Shared Property pluginTvWishList() As Boolean
            Get
                Return m_pluginTvWishList.Value
            End Get
            Set(ByVal value As Boolean)
                m_pluginTvWishList.Value = value
            End Set
        End Property
        Public Shared Property pluginClickfinderProgramGuide() As Boolean
            Get
                Return m_pluginClickfinderProgramGuide.Value
            End Get
            Set(ByVal value As Boolean)
                m_pluginClickfinderProgramGuide.Value = value
            End Set
        End Property

        Public Shared Property PreviewMaxDays() As Integer
            Get
                Return m_PreviewMaxDays.Value
            End Get
            Set(ByVal value As Integer)
                m_PreviewMaxDays.Value = value
            End Set
        End Property
        Public Shared Property PreviewMinTvMovieRating() As Integer
            Get
                Return m_PreviewMinTvMovieRating.Value
            End Get
            Set(ByVal value As Integer)
                m_PreviewMinTvMovieRating.Value = value
            End Set
        End Property

        Public Shared Property NowOffset() As Double
            Get
                Return m_NowOffset.Value
            End Get
            Set(ByVal value As Double)
                m_NowOffset.Value = value
            End Set
        End Property


        Public Shared Property ItemsShowLocalMovies() As Boolean
            Get
                Return m_ItemsShowLocalMovies.Value
            End Get
            Set(ByVal value As Boolean)
                m_ItemsShowLocalMovies.Value = value
            End Set
        End Property
        Public Shared Property ItemsShowLocalSeries() As Boolean
            Get
                Return m_ItemsShowLocalSeries.Value
            End Get
            Set(ByVal value As Boolean)
                m_ItemsShowLocalSeries.Value = value
            End Set
        End Property

        Public Shared Property RemberSortedBy() As Boolean
            Get
                Return m_RemberSortedBy.Value
            End Get
            Set(ByVal value As Boolean)
                m_RemberSortedBy.Value = value
            End Set
        End Property

        Public Shared Property PrimeTime() As Date
            Get
                Return m_PrimeTime.Value
            End Get
            Set(ByVal value As Date)
                m_PrimeTime.Value = value
            End Set
        End Property
        Public Shared Property LateTime() As Date
            Get
                Return m_LateTime.Value
            End Get
            Set(ByVal value As Date)
                m_LateTime.Value = value
            End Set
        End Property
        Public Shared Property UseSportLogos() As Boolean
            Get
                Return m_UseSportLogos.Value
            End Get
            Set(ByVal value As Boolean)
                m_UseSportLogos.Value = value
            End Set
        End Property
        Public Shared Property EpisodenScanner() As String
            Get
                Return m_EpisodenScanner.Value
            End Get
            Set(ByVal value As String)
                m_EpisodenScanner.Value = value
            End Set
        End Property

        Public Shared Property ClickfinderDatabasePath() As String
            Get
                Return m_ClickfinderDatabasePath.Value
            End Get
            Set(ByVal value As String)
                m_ClickfinderDatabasePath.Value = value
            End Set
        End Property
        Public Shared Property ClickfinderImagePath() As String
            Get
                Return m_ClickfinderImagePath.Value
            End Get
            Set(ByVal value As String)
                m_ClickfinderImagePath.Value = value
            End Set
        End Property
        Public Shared Property ClickfinderPluginName() As String
            Get
                Return m_ClickfinderPluginName.Value
            End Get
            Set(ByVal value As String)
                m_ClickfinderPluginName.Value = value
            End Set
        End Property
        Public Shared Property ClickfinderDebugMode() As Boolean
            Get
                Return m_ClickfinderDebugMode.Value
            End Get
            Set(ByVal value As Boolean)
                m_ClickfinderDebugMode.Value = value
            End Set
        End Property
        Public Shared Property ClickfinderStartGui() As String
            Get
                Return m_ClickfinderStartGui.Value
            End Get
            Set(ByVal value As String)
                m_ClickfinderStartGui.Value = value
            End Set
        End Property
        


        Public Shared Property CategorieShowLocalMovies() As Boolean
            Get
                Return m_CategorieShowLocalMovies.Value
            End Get
            Set(ByVal value As Boolean)
                m_CategorieShowLocalMovies.Value = value
            End Set
        End Property
        Public Shared Property CategorieShowLocalSeries() As Boolean
            Get
                Return m_CategorieShowLocalSeries.Value
            End Get
            Set(ByVal value As Boolean)
                m_CategorieShowLocalSeries.Value = value
            End Set
        End Property

        Public Shared Property DetailUseSeriesDescribtion() As Boolean
            Get
                Return m_DetailUseSeriesDescribtion.Value
            End Get
            Set(ByVal value As Boolean)
                m_DetailUseSeriesDescribtion.Value = value
            End Set
        End Property
        Public Shared Property DetailSeriesImage() As String
            Get
                Return m_DetailSeriesImage.Value
            End Get
            Set(ByVal value As String)
                m_DetailSeriesImage.Value = value
            End Set
        End Property

        Public Shared Property OverlayMoviesEnabled() As Boolean
            Get
                Return m_OverlayMoviesEnabled.Value
            End Get
            Set(ByVal value As Boolean)
                m_OverlayMoviesEnabled.Value = value
            End Set
        End Property
        Public Shared Property OverlayTime() As String
            Get
                Return m_OverlayTime.Value
            End Get
            Set(ByVal value As String)
                m_OverlayTime.Value = value
            End Set
        End Property
        Public Shared Property OverlaySeriesEnabled() As Boolean
            Get
                Return m_OverlaySeriesEnabled.Value
            End Get
            Set(ByVal value As Boolean)
                m_OverlaySeriesEnabled.Value = value
            End Set
        End Property
        Public Shared Property OverlayShowTagesTipp() As Boolean
            Get
                Return m_OverlayShowTagesTipp.Value
            End Get
            Set(ByVal value As Boolean)
                m_OverlayShowTagesTipp.Value = value
            End Set
        End Property
        Public Shared Property OverlayMovieSort() As String
            Get
                Return m_OverlayMovieSort.Value
            End Get
            Set(ByVal value As String)
                m_OverlayMovieSort.Value = value
            End Set
        End Property
        Public Shared Property OverlayMovieLimit() As Integer
            Get
                Return m_OverlayMovieLimit.Value
            End Get
            Set(ByVal value As Integer)
                m_OverlayMovieLimit.Value = value
            End Set
        End Property
        Public Shared Property OverlayShowLocalMovies() As Boolean
            Get
                Return m_OverlayShowLocalMovies.Value
            End Get
            Set(ByVal value As Boolean)
                m_OverlayShowLocalMovies.Value = value
            End Set
        End Property
        Public Shared Property OverlayTvGroup() As String
            Get
                Return m_OverlayTvGroup.Value
            End Get
            Set(ByVal value As String)
                m_OverlayTvGroup.Value = value
            End Set
        End Property
        Public Shared Property OverlayUpdateTimer() As Integer
            Get
                Return m_OverlayUpdateTimer.Value
            End Get
            Set(ByVal value As Integer)
                m_OverlayUpdateTimer.Value = value
            End Set
        End Property
#End Region

#Region "TvMovie EPG Import++ settings: Read Only !"
        Private Shared m_TvMovieImportTvSeriesInfos As Setting
        Private Shared m_TvMovieImportVideoDatabaseInfos As Setting
        Private Shared m_TvMovieImportMovingPicturesInfos As Setting
        Private Shared m_TvMovieUseTheTvDb As Setting
        Private Shared m_TvMovieEnabled As Setting
        Private Shared m_TvMovieLastUpdate As Setting
        Private Shared m_TvMovieImportIsRunning As Setting
        Private Shared m_TvMovieMPDatabase As Setting

        Private Shared m_ClickfinderDataAvailable As Setting

        Public Shared ReadOnly Property TvMovieImportTvSeriesInfos() As Boolean
            Get
                Return m_TvMovieImportTvSeriesInfos.Value
            End Get
        End Property
        Public Shared ReadOnly Property TvMovieImportVideoDatabaseInfos() As Boolean
            Get
                Return m_TvMovieImportVideoDatabaseInfos.Value
            End Get

        End Property
        Public Shared ReadOnly Property TvMovieImportMovingPicturesInfos() As Boolean
            Get
                Return m_TvMovieImportMovingPicturesInfos.Value
            End Get

        End Property
        Public Shared ReadOnly Property TvMovieUseTheTvDb() As Boolean
            Get
                Return m_TvMovieUseTheTvDb.Value
            End Get

        End Property
        Public Shared ReadOnly Property TvMovieEnabled() As Boolean
            Get
                Return m_TvMovieEnabled.Value
            End Get

        End Property
        Public Shared ReadOnly Property TvMovieLastUpdate() As Date
            Get
                Return m_TvMovieLastUpdate.Value
            End Get

        End Property
        Public Shared ReadOnly Property TvMovieImportIsRunning() As Boolean
            Get
                Return m_TvMovieImportIsRunning.Value
            End Get

        End Property
        Public Shared ReadOnly Property TvMovieMPDatabase() As String
            Get
                Return m_TvMovieMPDatabase.Value
            End Get
        End Property

        Public Shared ReadOnly Property ClickfinderDataAvailable() As Boolean
            Get
                Return m_ClickfinderDataAvailable.Value
            End Get
        End Property

        Public Shared ReadOnly Property ClickfinderSaveSettingsOnClients() As Boolean
            Get
                Return CBool(_layer.GetSetting("ClickfinderSaveSettingsOnClients", "false").Value)
            End Get
        End Property

#End Region
#End Region

#Region "Functions"
        Public Shared Sub Load()
            Try
                'TvMovie++ Settings und settings die auf dem TvServer gespeichert werden müssen (ob auf Clients gespeichert werden soll)
                m_TvMovieImportTvSeriesInfos = _layer.GetSetting("TvMovieImportTvSeriesInfos", "false")
                m_TvMovieImportVideoDatabaseInfos = _layer.GetSetting("TvMovieImportVideoDatabaseInfos", "false")
                m_TvMovieImportMovingPicturesInfos = _layer.GetSetting("TvMovieImportMovingPicturesInfos", "false")
                m_TvMovieUseTheTvDb = _layer.GetSetting("TvMovieUseTheTvDb", "false")
                m_TvMovieEnabled = _layer.GetSetting("TvMovieEnabled", "false")
                m_TvMovieLastUpdate = _layer.GetSetting("TvMovieLastUpdate")
                m_TvMovieImportIsRunning = _layer.GetSetting("TvMovieImportIsRunning", "false")
                m_TvMovieMPDatabase = _layer.GetSetting("TvMovieMPDatabase", "%ProgramData%\Team MediaPortal\MediaPortal\database")

                m_ClickfinderDataAvailable = _layer.GetSetting("ClickfinderDataAvailable", "false")
            Catch ex As Exception
                MyLog.Error("[CPGsettings]: [Load]: nessacary TvServer settings: exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try

            If ClickfinderSaveSettingsOnClients = False Then
                MyLog.Debug("[CPGsettings]: [Load]: load settings from TvServer")
                LoadFromTvServer()
            Else
                MyLog.Debug("[CPGsettings]: [Load]: load settings from Client")
                LoadFromClient()
            End If

        End Sub
        Public Shared Sub save()
            If ClickfinderSaveSettingsOnClients = False Then
                MyLog.Debug("[CPGsettings]: [Save]: save settings on TvServer")
                SaveToTvServer()
                MsgBox("Save to Server")
            Else
                MyLog.Debug("[CPGsettings]: [Save]: save settings on Clients")
                SaveToClient()
                MsgBox("Save to Client")
            End If
        End Sub

#Region "TvServer"
        Private Shared Sub LoadFromTvServer()
            Try
                m_OverviewShowMoviesAfter = _layer.GetSetting("ClickfinderOverviewShowMoviesAfter", "12")
                m_OverviewShowHighlightsAfter = _layer.GetSetting("ClickfinderOverviewShowHighlightsAfter", "15")
                m_OverviewHighlightsMinRuntime = _layer.GetSetting("ClickfinderOverviewHighlightsMinRuntime", "16")
                m_OverviewShowLocalMovies = _layer.GetSetting("ClickfinderOverviewShowLocalMovies", "false")
                m_OverviewMaxDays = _layer.GetSetting("ClickfinderOverviewMaxDays", "10")
                m_OverviewShowTagesTipp = _layer.GetSetting("ClickfinderOverviewShowTagesTipp", "false")
                m_OverviewMovieSort = _layer.GetSetting("ClickfinderOverviewMovieSort", SortMethode.startTime.ToString)

                m_StandardTvGroup = _layer.GetSetting("ClickfinderStandardTvGroup", "All Channels")
                m_QuickTvGroup1 = _layer.GetSetting("ClickfinderQuickTvGroup1", "All Channels")
                m_QuickTvGroup2 = _layer.GetSetting("ClickfinderQuickTvGroup2", "All Channels")


                m_pluginTvWishList = _layer.GetSetting("pluginTvWishList", "false")
                m_pluginClickfinderProgramGuide = _layer.GetSetting("pluginClickfinder ProgramGuide", "false")

                m_PreviewMaxDays = _layer.GetSetting("ClickfinderPreviewMaxDays", "7")
                m_PreviewMinTvMovieRating = _layer.GetSetting("ClickfinderPreviewMinTvMovieRating", "1")

                m_NowOffset = _layer.GetSetting("ClickfinderNowOffset", "-20")

                m_ItemsShowLocalMovies = _layer.GetSetting("ClickfinderItemsShowLocalMovies", "false")
                m_ItemsShowLocalSeries = _layer.GetSetting("ClickfinderItemsShowLocalSeries", "false")

                m_RemberSortedBy = _layer.GetSetting("ClickfinderRemberSortedBy", "true")

                m_PrimeTime = _layer.GetSetting("ClickfinderPrimeTime", "20:15")
                m_LateTime = _layer.GetSetting("ClickfinderLateTime", "22:00")
                m_UseSportLogos = _layer.GetSetting("ClickfinderUseSportLogos", "false")
                m_EpisodenScanner = _layer.GetSetting("ClickfinderEpisodenScanner", "")

                m_ClickfinderDatabasePath = _layer.GetSetting("ClickfinderDatabasePath", "")
                m_ClickfinderImagePath = _layer.GetSetting("ClickfinderImagePath", "")
                m_ClickfinderPluginName = _layer.GetSetting("ClickfinderPluginName", "Clickfinder ProgramGuide")
                m_ClickfinderDebugMode = _layer.GetSetting("ClickfinderDebugMode", "True")
                m_ClickfinderStartGui = _layer.GetSetting("ClickfinderStartGui", "Highlights")

                m_CategorieShowLocalMovies = _layer.GetSetting("ClickfinderCategorieShowLocalMovies", "false")
                m_CategorieShowLocalSeries = _layer.GetSetting("ClickfinderCategorieShowLocalSeries", "false")

                m_DetailUseSeriesDescribtion = _layer.GetSetting("ClickfinderDetailUseSeriesDescribtion", "false")
                m_DetailSeriesImage = _layer.GetSetting("ClickfinderDetailsSeriesImage", "Cover")

                m_OverlayMoviesEnabled = _layer.GetSetting("ClickfinderOverlayMoviesEnabled", "false")
                m_OverlayTime = _layer.GetSetting("ClickfinderOverlayTime", "PrimeTime")
                m_OverlaySeriesEnabled = _layer.GetSetting("ClickfinderOverlaySeriesEnabled", "false")
                m_OverlayShowTagesTipp = _layer.GetSetting("ClickfinderOverlayShowTagesTipp", "false")
                m_OverlayMovieSort = _layer.GetSetting("ClickfinderOverlayMovieSort", SortMethode.startTime.ToString)
                m_OverlayMovieLimit = _layer.GetSetting("ClickfinderOverlayMovieLimit", "10")
                m_OverlayShowLocalMovies = _layer.GetSetting("ClickfinderOverlayShowLocalMovies", "false")
                m_OverlayTvGroup = _layer.GetSetting("ClickfinderOverlayTvGroup", "All Channels")
                m_OverlayUpdateTimer = _layer.GetSetting("ClickfinderOverlayUpdateTimer", "20")
            Catch ex As Exception
                MyLog.Error("[CPGsettings]: [LoadFromTvServer]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try
        End Sub
        Private Shared Sub SaveToTvServer()
            Try
                m_OverviewShowMoviesAfter.Persist()
                m_OverviewShowHighlightsAfter.Persist()
                m_OverviewShowLocalMovies.Persist()
                m_OverviewHighlightsMinRuntime.Persist()
                m_OverviewMaxDays.Persist()
                m_OverviewShowTagesTipp.Persist()
                m_OverviewMovieSort.Persist()

                m_StandardTvGroup.Persist()
                m_QuickTvGroup1.Persist()
                m_QuickTvGroup2.Persist()

                m_pluginTvWishList.Persist()
                m_pluginClickfinderProgramGuide.Persist()

                m_PreviewMaxDays.Persist()
                m_PreviewMinTvMovieRating.Persist()

                m_NowOffset.Persist()

                m_ItemsShowLocalMovies.Persist()
                m_ItemsShowLocalSeries.Persist()

                m_RemberSortedBy.Persist()

                m_PrimeTime.Persist()
                m_LateTime.Persist()
                m_UseSportLogos.Persist()
                m_EpisodenScanner.Persist()
                m_ClickfinderDatabasePath.Persist()
                m_ClickfinderImagePath.Persist()
                m_ClickfinderPluginName.Persist()
                m_ClickfinderDebugMode.Persist()
                m_ClickfinderStartGui.Persist()
                m_ClickfinderDataAvailable.Persist()

                m_CategorieShowLocalMovies.Persist()
                m_CategorieShowLocalSeries.Persist()

                m_DetailUseSeriesDescribtion.Persist()
                m_DetailSeriesImage.Persist()

                m_OverlayMoviesEnabled.Persist()
                m_OverlayTime.Persist()
                m_OverlaySeriesEnabled.Persist()
                m_OverlayShowTagesTipp.Persist()
                m_OverlayMovieSort.Persist()
                m_OverlayMovieLimit.Persist()
                m_OverlayShowLocalMovies.Persist()
                m_OverlayTvGroup.Persist()
                m_OverlayUpdateTimer.Persist()
            Catch ex As Exception
                MyLog.Error("[CPGsettings]: [SaveToTvServer]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try
        End Sub

#End Region

#Region "Client"
        Private Shared Sub LoadFromClient()
            Try

                m_OverviewShowMoviesAfter = loadFromXml("ClickfinderOverviewShowMoviesAfter", "12")
                m_OverviewShowHighlightsAfter = loadFromXml("ClickfinderOverviewShowHighlightsAfter", "15")
                m_OverviewHighlightsMinRuntime = loadFromXml("ClickfinderOverviewHighlightsMinRuntime", "16")
                m_OverviewShowLocalMovies = loadFromXml("ClickfinderOverviewShowLocalMovies", "false")
                m_OverviewMaxDays = loadFromXml("ClickfinderOverviewMaxDays", "10")
                m_OverviewShowTagesTipp = loadFromXml("ClickfinderOverviewShowTagesTipp", "false")
                m_OverviewMovieSort = loadFromXml("ClickfinderOverviewMovieSort", SortMethode.startTime.ToString)

                m_StandardTvGroup = loadFromXml("ClickfinderStandardTvGroup", "All Channels")
                m_QuickTvGroup1 = loadFromXml("ClickfinderQuickTvGroup1", "All Channels")
                m_QuickTvGroup2 = loadFromXml("ClickfinderQuickTvGroup2", "All Channels")


                m_pluginTvWishList = loadFromXml("pluginTvWishList", "false")
                m_pluginClickfinderProgramGuide = loadFromXml("pluginClickfinder ProgramGuide", "false")

                m_PreviewMaxDays = loadFromXml("ClickfinderPreviewMaxDays", "7")
                m_PreviewMinTvMovieRating = loadFromXml("ClickfinderPreviewMinTvMovieRating", "1")

                m_NowOffset = loadFromXml("ClickfinderNowOffset", "-20")

                m_ItemsShowLocalMovies = loadFromXml("ClickfinderItemsShowLocalMovies", "false")
                m_ItemsShowLocalSeries = loadFromXml("ClickfinderItemsShowLocalSeries", "false")

                m_RemberSortedBy = loadFromXml("ClickfinderRemberSortedBy", "true")

                m_PrimeTime = loadFromXml("ClickfinderPrimeTime", "20:15")
                m_LateTime = loadFromXml("ClickfinderLateTime", "22:00")
                m_UseSportLogos = loadFromXml("ClickfinderUseSportLogos", "false")
                m_EpisodenScanner = loadFromXml("ClickfinderEpisodenScanner", "")

                m_ClickfinderDatabasePath = loadFromXml("ClickfinderDatabasePath", "")
                m_ClickfinderImagePath = loadFromXml("ClickfinderImagePath", "")
                m_ClickfinderPluginName = loadFromXml("ClickfinderPluginName", "Clickfinder ProgramGuide")
                m_ClickfinderDebugMode = loadFromXml("ClickfinderDebugMode", "True")
                m_ClickfinderStartGui = loadFromXml("ClickfinderStartGui", "Highlights")

                m_CategorieShowLocalMovies = loadFromXml("ClickfinderCategorieShowLocalMovies", "false")
                m_CategorieShowLocalSeries = loadFromXml("ClickfinderCategorieShowLocalSeries", "false")

                m_DetailUseSeriesDescribtion = loadFromXml("ClickfinderDetailUseSeriesDescribtion", "false")
                m_DetailSeriesImage = loadFromXml("ClickfinderDetailsSeriesImage", "Cover")

                m_OverlayMoviesEnabled = loadFromXml("ClickfinderOverlayMoviesEnabled", "false")
                m_OverlayTime = loadFromXml("ClickfinderOverlayTime", "PrimeTime")
                m_OverlaySeriesEnabled = loadFromXml("ClickfinderOverlaySeriesEnabled", "false")
                m_OverlayShowTagesTipp = loadFromXml("ClickfinderOverlayShowTagesTipp", "false")
                m_OverlayMovieSort = loadFromXml("ClickfinderOverlayMovieSort", SortMethode.startTime.ToString)
                m_OverlayMovieLimit = loadFromXml("ClickfinderOverlayMovieLimit", "10")
                m_OverlayShowLocalMovies = loadFromXml("ClickfinderOverlayShowLocalMovies", "false")
                m_OverlayTvGroup = loadFromXml("ClickfinderOverlayTvGroup", "All Channels")
                m_OverlayUpdateTimer = loadFromXml("ClickfinderOverlayUpdateTimer", "20")


            Catch ex As Exception
                MyLog.Error("[CPGsettings]: [LoadFromClient]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try
        End Sub
        Private Shared Sub SaveToClient()
            Try

           
                saveToXml(m_OverviewShowMoviesAfter)
                saveToXml(m_OverviewShowHighlightsAfter)
                saveToXml(m_OverviewShowLocalMovies)
                saveToXml(m_OverviewHighlightsMinRuntime)
                saveToXml(m_OverviewMaxDays)
                saveToXml(m_OverviewShowTagesTipp)
                saveToXml(m_OverviewMovieSort)

                saveToXml(m_StandardTvGroup)
                saveToXml(m_QuickTvGroup1)
                saveToXml(m_QuickTvGroup2)



                saveToXml(m_pluginTvWishList)
                saveToXml(m_pluginClickfinderProgramGuide)

                saveToXml(m_PreviewMaxDays)
                saveToXml(m_PreviewMinTvMovieRating)

                saveToXml(m_NowOffset)

                saveToXml(m_ItemsShowLocalMovies)
                saveToXml(m_ItemsShowLocalSeries)

                saveToXml(m_RemberSortedBy)

                saveToXml(m_PrimeTime)
                saveToXml(m_LateTime)
                saveToXml(m_UseSportLogos)
                saveToXml(m_EpisodenScanner)

                saveToXml(m_ClickfinderDatabasePath)
                saveToXml(m_ClickfinderImagePath)
                saveToXml(m_ClickfinderPluginName)
                saveToXml(m_ClickfinderDebugMode)
                saveToXml(m_ClickfinderStartGui)
                saveToXml(m_ClickfinderDataAvailable)

                saveToXml(m_CategorieShowLocalMovies)
                saveToXml(m_CategorieShowLocalSeries)



                saveToXml(m_DetailUseSeriesDescribtion)
                saveToXml(m_DetailSeriesImage)

                saveToXml(m_OverlayMoviesEnabled)
                saveToXml(m_OverlayTime)
                saveToXml(m_OverlaySeriesEnabled)
                saveToXml(m_OverlayShowTagesTipp)
                saveToXml(m_OverlayMovieSort)
                saveToXml(m_OverlayMovieLimit)
                saveToXml(m_OverlayShowLocalMovies)
                saveToXml(m_OverlayTvGroup)
                saveToXml(m_OverlayUpdateTimer)

            Catch ex As Exception
                MyLog.Error("[CPGsettings]: [SaveToClient]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try

        End Sub

#End Region

#Region "XML handler"
        Private Shared Function loadFromXml(ByVal Tag As String, ByVal Value As String) As Setting
            Try

                Dim _path As String = Config.GetFile(Config.Dir.Config, "ClickfinderProgramGuide.xml")
                Dim sReturn As Setting = New Setting(Tag, Value)
                Dim dsSettings As New DataSet
                If System.IO.File.Exists(_path) Then
                    dsSettings.ReadXml(_path)
                Else
                    dsSettings.Tables.Add("Settings")
                    dsSettings.Tables(0).Columns.Add("Tag", GetType(String))
                    dsSettings.Tables(0).Columns.Add("Value", GetType(String))
                End If

                Dim dr() As DataRow = dsSettings.Tables("Settings").Select("Tag = '" & Tag & "'")
                If dr.Length = 1 Then
                    sReturn.Tag = dr(0)("Tag").ToString
                    sReturn.Value = dr(0)("Value").ToString
                Else
                    saveToXml(sReturn)
                End If

                Return sReturn
            Catch ex As Exception
                MyLog.Error("[CPGsettings]: [loadFromXml]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try
        End Function
        Private Shared Sub saveToXml(ByVal CPGSetting As Setting)
            Try

                Dim _dsSettings As New DataSet("localSettings")
                Dim _path As String = Config.GetFile(Config.Dir.Config, "ClickfinderProgramGuide.xml")

                If System.IO.File.Exists(_path) Then
                    _dsSettings.ReadXml(_path)
                Else
                    _dsSettings.Tables.Add("Settings")
                    _dsSettings.Tables(0).Columns.Add("Tag", GetType(String))
                    _dsSettings.Tables(0).Columns.Add("Value", GetType(String))
                End If

                Dim dr() As DataRow = _dsSettings.Tables(0).Select("Tag = '" & CPGSetting.Tag & "'")
                If dr.Length = 1 Then
                    dr(0)("Value") = CPGSetting.Value
                Else
                    Dim drSetting As DataRow = _dsSettings.Tables("Settings").NewRow
                    drSetting("Tag") = CPGSetting.Tag
                    drSetting("Value") = CPGSetting.Value
                    _dsSettings.Tables("Settings").Rows.Add(drSetting)


                End If
                _dsSettings.WriteXml(_path)

            Catch ex As Exception
                MyLog.Error("[CPGsettings]: [saveToXml]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try
        End Sub
#End Region
#End Region

    End Class
End Namespace
