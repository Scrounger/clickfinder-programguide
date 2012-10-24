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
    Public Class CPGsettings

        Private Shared _layer As New TvBusinessLayer

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
        Private Shared m_ClickfinderEnabled As Setting
        Private Shared m_ClickfinderDatabasePath As Setting
        Private Shared m_ClickfinderImagePath As Setting
        Private Shared m_ClickfinderPluginName As Setting
        Private Shared m_ClickfinderDebugMode As Setting
        Private Shared m_ClickfinderStartGui As Setting
        Private Shared m_ClickfinderDataAvailable As Setting

        Private Shared m_CategorieShowLocalMovies As Setting
        Private Shared m_CategorieShowLocalSeries As Setting

        Private Shared m_TvMovieImportTvSeriesInfos As Setting
        Private Shared m_TvMovieImportVideoDatabaseInfos As Setting
        Private Shared m_TvMovieImportMovingPicturesInfos As Setting
        Private Shared m_TvMovieUseTheTvDb As Setting
        Private Shared m_TvMovieEnabled As Setting
        Private Shared m_TvMovieLastUpdate As Setting
        Private Shared m_TvMovieImportIsRunning As Setting
        Private Shared m_TvMovieMPDatabase As Setting

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

        Public Shared Sub Load()

            m_OverviewShowMoviesAfter = _layer.GetSetting("ClickfinderOverviewShowMoviesAfter", "12")
            m_OverviewShowHighlightsAfter = _layer.GetSetting("ClickfinderOverviewShowHighlightsAfter", "15")
            m_OverviewHighlightsMinRuntime = _layer.GetSetting("ClickfinderOverviewHighlightsMinRuntime", "16")
            m_OverviewShowLocalMovies = _layer.GetSetting("ClickfinderOverviewShowLocalMovies", "false")
            m_OverviewMaxDays = _layer.GetSetting("ClickfinderOverviewMaxDays", "10")
            m_OverviewShowTagesTipp = _layer.GetSetting("ClickfinderOverviewShowTagesTipp")
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
            m_ClickfinderEnabled = _layer.GetSetting("ClickfinderEnabled", "true")
            m_ClickfinderDatabasePath = _layer.GetSetting("ClickfinderDatabasePath")
            m_ClickfinderImagePath = _layer.GetSetting("ClickfinderImagePath")
            m_ClickfinderPluginName = _layer.GetSetting("ClickfinderPluginName", "Clickfinder ProgramGuide")
            m_ClickfinderDebugMode = _layer.GetSetting("ClickfinderDebugMode", "True")
            m_ClickfinderStartGui = _layer.GetSetting("ClickfinderStartGui", "Highlights")
            m_ClickfinderDataAvailable = _layer.GetSetting("ClickfinderDataAvailable", "false")


            m_CategorieShowLocalMovies = _layer.GetSetting("ClickfinderCategorieShowLocalMovies", "false")
            m_CategorieShowLocalSeries = _layer.GetSetting("ClickfinderCategorieShowLocalSeries", "false")

            m_TvMovieImportTvSeriesInfos = _layer.GetSetting("TvMovieImportTvSeriesInfos", "false")
            m_TvMovieImportVideoDatabaseInfos = _layer.GetSetting("TvMovieImportVideoDatabaseInfos", "false")
            m_TvMovieImportMovingPicturesInfos = _layer.GetSetting("TvMovieImportMovingPicturesInfos", "false")
            m_TvMovieUseTheTvDb = _layer.GetSetting("TvMovieUseTheTvDb", "false")
            m_TvMovieEnabled = _layer.GetSetting("TvMovieEnabled", "false")
            m_TvMovieLastUpdate = _layer.GetSetting("TvMovieLastUpdate")
            m_TvMovieImportIsRunning = _layer.GetSetting("TvMovieImportIsRunning", "false")
            m_TvMovieMPDatabase = _layer.GetSetting("TvMovieMPDatabase", "%ProgramData%\Team MediaPortal\MediaPortal\database")

            m_DetailUseSeriesDescribtion = _layer.GetSetting("ClickfinderDetailUseSeriesDescribtion", "false")
            m_DetailSeriesImage = _layer.GetSetting("ClickfinderDetailsSeriesImage", "Cover")

            m_OverlayMoviesEnabled = _layer.GetSetting("ClickfinderOverlayMoviesEnabled", "false")
            m_OverlayTime = _layer.GetSetting("ClickfinderOverlayTime", "PrimeTime")
            m_OverlaySeriesEnabled = _layer.GetSetting("ClickfinderOverlaySeriesEnabled", "false")
            m_OverlayShowTagesTipp = _layer.GetSetting("ClickfinderOverlayShowTagesTipp")
            m_OverlayMovieSort = _layer.GetSetting("ClickfinderOverlayMovieSort", SortMethode.startTime.ToString)
            m_OverlayMovieLimit = _layer.GetSetting("ClickfinderOverlayMovieLimit", "10")
            m_OverlayShowLocalMovies = _layer.GetSetting("ClickfinderOverlayShowLocalMovies", "false")
            m_OverlayTvGroup = _layer.GetSetting("ClickfinderOverlayTvGroup", "All Channels")
            m_OverlayUpdateTimer = _layer.GetSetting("ClickfinderOverlayUpdateTimer", "20")
        End Sub
        Public Shared Sub save()

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
            m_ClickfinderEnabled.Persist()
            m_ClickfinderDatabasePath.Persist()
            m_ClickfinderImagePath.Persist()
            m_ClickfinderPluginName.Persist()
            m_ClickfinderDebugMode.Persist()
            m_ClickfinderStartGui.Persist()
            m_ClickfinderDataAvailable.Persist()

            m_CategorieShowLocalMovies.Persist()
            m_CategorieShowLocalSeries.Persist()

            m_TvMovieImportTvSeriesInfos.Persist()
            m_TvMovieImportVideoDatabaseInfos.Persist()
            m_TvMovieImportMovingPicturesInfos.Persist()
            m_TvMovieUseTheTvDb.Persist()
            m_TvMovieEnabled.Persist()
            m_TvMovieLastUpdate.Persist()
            m_TvMovieImportIsRunning.Persist()

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

        End Sub

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
        Public Shared Property ClickfinderEnabled() As Boolean
            Get
                Return m_ClickfinderEnabled.Value
            End Get
            Set(ByVal value As Boolean)
                m_ClickfinderEnabled.Value = value
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
        Public Shared Property ClickfinderDataAvailable() As Boolean
            Get
                Return m_ClickfinderDataAvailable.Value
            End Get
            Set(ByVal value As Boolean)
                m_ClickfinderDataAvailable.Value = value
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


        Public Shared Property TvMovieImportTvSeriesInfos() As Boolean
            Get
                Return m_TvMovieImportTvSeriesInfos.Value
            End Get
            Set(ByVal value As Boolean)
                m_TvMovieImportTvSeriesInfos.Value = value
            End Set
        End Property
        Public Shared Property TvMovieImportVideoDatabaseInfos() As Boolean
            Get
                Return m_TvMovieImportVideoDatabaseInfos.Value
            End Get
            Set(ByVal value As Boolean)
                m_TvMovieImportVideoDatabaseInfos.Value = value
            End Set
        End Property
        Public Shared Property TvMovieImportMovingPicturesInfos() As Boolean
            Get
                Return m_TvMovieImportMovingPicturesInfos.Value
            End Get
            Set(ByVal value As Boolean)
                m_TvMovieImportMovingPicturesInfos.Value = value
            End Set
        End Property
        Public Shared Property TvMovieUseTheTvDb() As Boolean
            Get
                Return m_TvMovieUseTheTvDb.Value
            End Get
            Set(ByVal value As Boolean)
                m_TvMovieUseTheTvDb.Value = value
            End Set
        End Property
        Public Shared Property TvMovieEnabled() As Boolean
            Get
                Return m_TvMovieEnabled.Value
            End Get
            Set(ByVal value As Boolean)
                m_TvMovieEnabled.Value = value
            End Set
        End Property
        Public Shared Property TvMovieLastUpdate() As Date
            Get
                Return m_TvMovieLastUpdate.Value
            End Get
            Set(ByVal value As Date)
                m_TvMovieLastUpdate.Value = value
            End Set
        End Property
        Public Shared Property TvMovieImportIsRunning() As Boolean
            Get
                Return m_TvMovieImportIsRunning.Value
            End Get
            Set(ByVal value As Boolean)
                m_TvMovieImportIsRunning.Value = value
            End Set
        End Property
        Public Shared Property TvMovieMPDatabase() As String
            Get
                Return m_TvMovieMPDatabase.Value
            End Get
            Set(ByVal value As String)
                m_TvMovieMPDatabase.Value = value
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
    End Class
End Namespace
