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


        'Remote Control Items Sort:
        Private Shared m_ItemsRemoteSortAll As New Dictionary(Of Integer, Setting)
        Private Shared m_ItemsRemoteSort1 As Setting
        Private Shared m_ItemsRemoteSort2 As Setting
        Private Shared m_ItemsRemoteSort3 As Setting
        Private Shared m_ItemsRemoteSort4 As Setting
        Private Shared m_ItemsRemoteSort5 As Setting
        Private Shared m_ItemsRemoteSort6 As Setting
        Private Shared m_ItemsRemoteSort7 As Setting
        Private Shared m_ItemsRemoteSort8 As Setting
        Private Shared m_ItemsRemoteSort9 As Setting
        Private Shared m_ItemsRemoteSort0 As Setting

        'Remote Control Items FilterbyGroup:
        Private Shared m_ItemsRemoteTvGroupAll As New Dictionary(Of Integer, Setting)
        Private Shared m_ItemsRemoteTvGroup1 As Setting
        Private Shared m_ItemsRemoteTvGroup2 As Setting
        Private Shared m_ItemsRemoteTvGroup3 As Setting
        Private Shared m_ItemsRemoteTvGroup4 As Setting
        Private Shared m_ItemsRemoteTvGroup5 As Setting
        Private Shared m_ItemsRemoteTvGroup6 As Setting
        Private Shared m_ItemsRemoteTvGroup7 As Setting
        Private Shared m_ItemsRemoteTvGroup8 As Setting
        Private Shared m_ItemsRemoteTvGroup9 As Setting
        Private Shared m_ItemsRemoteTvGroup0 As Setting

        Friend Shared Property OverviewShowMoviesAfter() As Double
            Get
                Return m_OverviewShowMoviesAfter.Value
            End Get
            Set(ByVal value As Double)
                m_OverviewShowMoviesAfter.Value = value
            End Set
        End Property
        Friend Shared Property OverviewShowHighlightsAfter() As Double
            Get
                Return m_OverviewShowHighlightsAfter.Value
            End Get
            Set(ByVal value As Double)
                m_OverviewShowHighlightsAfter.Value = value
            End Set
        End Property
        Friend Shared Property OverviewHighlightsMinRuntime() As Long
            Get
                Return m_OverviewHighlightsMinRuntime.Value
            End Get
            Set(ByVal value As Long)
                m_OverviewHighlightsMinRuntime.Value = value
            End Set
        End Property
        Friend Shared Property OverviewShowLocalMovies() As Boolean
            Get
                Return m_OverviewShowLocalMovies.Value
            End Get
            Set(ByVal value As Boolean)
                m_OverviewShowLocalMovies.Value = value
            End Set
        End Property
        Friend Shared Property OverviewMaxDays() As Double
            Get
                Return m_OverviewMaxDays.Value
            End Get
            Set(ByVal value As Double)
                m_OverviewMaxDays.Value = value
            End Set
        End Property
        Friend Shared Property OverviewShowTagesTipp() As Boolean
            Get
                Return m_OverviewShowTagesTipp.Value
            End Get
            Set(ByVal value As Boolean)
                m_OverviewShowTagesTipp.Value = value
            End Set
        End Property
        Friend Shared Property OverviewMovieSort() As String
            Get
                Return m_OverviewMovieSort.Value
            End Get
            Set(ByVal value As String)
                m_OverviewMovieSort.Value = value
            End Set
        End Property

        Friend Shared Property StandardTvGroup() As String
            Get
                Return m_StandardTvGroup.Value
            End Get
            Set(ByVal value As String)
                m_StandardTvGroup.Value = value
            End Set
        End Property
        Friend Shared Property QuickTvGroup1() As String
            Get
                Return m_QuickTvGroup1.Value
            End Get
            Set(ByVal value As String)
                m_QuickTvGroup1.Value = value
            End Set
        End Property
        Friend Shared Property QuickTvGroup2() As String
            Get
                Return m_QuickTvGroup2.Value
            End Get
            Set(ByVal value As String)
                m_QuickTvGroup2.Value = value
            End Set
        End Property

        
        Friend Shared Property pluginClickfinderProgramGuide() As Boolean
            Get
                Return m_pluginClickfinderProgramGuide.Value
            End Get
            Set(ByVal value As Boolean)
                m_pluginClickfinderProgramGuide.Value = value
            End Set
        End Property

        Friend Shared Property PreviewMaxDays() As Integer
            Get
                Return m_PreviewMaxDays.Value
            End Get
            Set(ByVal value As Integer)
                m_PreviewMaxDays.Value = value
            End Set
        End Property
        Friend Shared Property PreviewMinTvMovieRating() As Integer
            Get
                Return m_PreviewMinTvMovieRating.Value
            End Get
            Set(ByVal value As Integer)
                m_PreviewMinTvMovieRating.Value = value
            End Set
        End Property

        


        Friend Shared Property ItemsShowLocalMovies() As Boolean
            Get
                Return m_ItemsShowLocalMovies.Value
            End Get
            Set(ByVal value As Boolean)
                m_ItemsShowLocalMovies.Value = value
            End Set
        End Property
        Friend Shared Property ItemsShowLocalSeries() As Boolean
            Get
                Return m_ItemsShowLocalSeries.Value
            End Get
            Set(ByVal value As Boolean)
                m_ItemsShowLocalSeries.Value = value
            End Set
        End Property

        Friend Shared Property RemberSortedBy() As Boolean
            Get
                Return m_RemberSortedBy.Value
            End Get
            Set(ByVal value As Boolean)
                m_RemberSortedBy.Value = value
            End Set
        End Property

        Friend Shared Property PrimeTime() As Date
            Get
                Return m_PrimeTime.Value
            End Get
            Set(ByVal value As Date)
                m_PrimeTime.Value = value
            End Set
        End Property
        Friend Shared Property LateTime() As Date
            Get
                Return m_LateTime.Value
            End Get
            Set(ByVal value As Date)
                m_LateTime.Value = value
            End Set
        End Property
        Friend Shared Property UseSportLogos() As Boolean
            Get
                Return m_UseSportLogos.Value
            End Get
            Set(ByVal value As Boolean)
                m_UseSportLogos.Value = value
            End Set
        End Property
        Friend Shared Property EpisodenScanner() As String
            Get
                Return m_EpisodenScanner.Value
            End Get
            Set(ByVal value As String)
                m_EpisodenScanner.Value = value
            End Set
        End Property

        Friend Shared Property ClickfinderDatabasePath() As String
            Get
                Return m_ClickfinderDatabasePath.Value
            End Get
            Set(ByVal value As String)
                m_ClickfinderDatabasePath.Value = value
            End Set
        End Property
        Friend Shared Property ClickfinderImagePath() As String
            Get
                Return m_ClickfinderImagePath.Value
            End Get
            Set(ByVal value As String)
                m_ClickfinderImagePath.Value = value
            End Set
        End Property
        Friend Shared Property ClickfinderPluginName() As String
            Get
                Return m_ClickfinderPluginName.Value
            End Get
            Set(ByVal value As String)
                m_ClickfinderPluginName.Value = value
            End Set
        End Property
        Friend Shared Property ClickfinderDebugMode() As Boolean
            Get
                Return m_ClickfinderDebugMode.Value
            End Get
            Set(ByVal value As Boolean)
                m_ClickfinderDebugMode.Value = value
            End Set
        End Property
        Friend Shared Property ClickfinderStartGui() As String
            Get
                Return m_ClickfinderStartGui.Value
            End Get
            Set(ByVal value As String)
                m_ClickfinderStartGui.Value = value
            End Set
        End Property



        Friend Shared Property CategorieShowLocalMovies() As Boolean
            Get
                Return m_CategorieShowLocalMovies.Value
            End Get
            Set(ByVal value As Boolean)
                m_CategorieShowLocalMovies.Value = value
            End Set
        End Property
        Friend Shared Property CategorieShowLocalSeries() As Boolean
            Get
                Return m_CategorieShowLocalSeries.Value
            End Get
            Set(ByVal value As Boolean)
                m_CategorieShowLocalSeries.Value = value
            End Set
        End Property

        Friend Shared Property DetailUseSeriesDescribtion() As Boolean
            Get
                Return m_DetailUseSeriesDescribtion.Value
            End Get
            Set(ByVal value As Boolean)
                m_DetailUseSeriesDescribtion.Value = value
            End Set
        End Property
        Friend Shared Property DetailSeriesImage() As String
            Get
                Return m_DetailSeriesImage.Value
            End Get
            Set(ByVal value As String)
                m_DetailSeriesImage.Value = value
            End Set
        End Property

        Friend Shared Property OverlayMoviesEnabled() As Boolean
            Get
                Return m_OverlayMoviesEnabled.Value
            End Get
            Set(ByVal value As Boolean)
                m_OverlayMoviesEnabled.Value = value
            End Set
        End Property
        Friend Shared Property OverlayTime() As String
            Get
                Return m_OverlayTime.Value
            End Get
            Set(ByVal value As String)
                m_OverlayTime.Value = value
            End Set
        End Property
        Friend Shared Property OverlaySeriesEnabled() As Boolean
            Get
                Return m_OverlaySeriesEnabled.Value
            End Get
            Set(ByVal value As Boolean)
                m_OverlaySeriesEnabled.Value = value
            End Set
        End Property
        Friend Shared Property OverlayShowTagesTipp() As Boolean
            Get
                Return m_OverlayShowTagesTipp.Value
            End Get
            Set(ByVal value As Boolean)
                m_OverlayShowTagesTipp.Value = value
            End Set
        End Property
        Friend Shared Property OverlayMovieSort() As String
            Get
                Return m_OverlayMovieSort.Value
            End Get
            Set(ByVal value As String)
                m_OverlayMovieSort.Value = value
            End Set
        End Property
        Friend Shared Property OverlayMovieLimit() As Integer
            Get
                Return m_OverlayMovieLimit.Value
            End Get
            Set(ByVal value As Integer)
                m_OverlayMovieLimit.Value = value
            End Set
        End Property
        Friend Shared Property OverlayShowLocalMovies() As Boolean
            Get
                Return m_OverlayShowLocalMovies.Value
            End Get
            Set(ByVal value As Boolean)
                m_OverlayShowLocalMovies.Value = value
            End Set
        End Property
        Friend Shared Property OverlayTvGroup() As String
            Get
                Return m_OverlayTvGroup.Value
            End Get
            Set(ByVal value As String)
                m_OverlayTvGroup.Value = value
            End Set
        End Property
        Friend Shared Property OverlayUpdateTimer() As Integer
            Get
                Return m_OverlayUpdateTimer.Value
            End Get
            Set(ByVal value As Integer)
                m_OverlayUpdateTimer.Value = value
            End Set
        End Property

        'Items RemoteControl
        Friend Shared Property ItemsRemoteSortAll(ByVal index As Integer) As String
            Get
                Return m_ItemsRemoteSortAll(index).Value
            End Get
            Set(ByVal value As String)
                m_ItemsRemoteSortAll(index).Value = value
            End Set
        End Property
        Friend Shared Property ItemsRemoteTvGroupAll(ByVal index As Integer) As String
            Get
                Return m_ItemsRemoteTvGroupAll(index).Value
            End Get
            Set(ByVal value As String)
                m_ItemsRemoteTvGroupAll(index).Value = value
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

        Friend Shared ReadOnly Property TvMovieImportTvSeriesInfos() As Boolean
            Get
                Return m_TvMovieImportTvSeriesInfos.Value
            End Get
        End Property
        Friend Shared ReadOnly Property TvMovieImportVideoDatabaseInfos() As Boolean
            Get
                Return m_TvMovieImportVideoDatabaseInfos.Value
            End Get

        End Property
        Friend Shared ReadOnly Property TvMovieImportMovingPicturesInfos() As Boolean
            Get
                Return m_TvMovieImportMovingPicturesInfos.Value
            End Get

        End Property
        Friend Shared ReadOnly Property TvMovieUseTheTvDb() As Boolean
            Get
                Return m_TvMovieUseTheTvDb.Value
            End Get

        End Property
        Friend Shared ReadOnly Property TvMovieEnabled() As Boolean
            Get
                Return m_TvMovieEnabled.Value
            End Get

        End Property
        Friend Shared ReadOnly Property TvMovieLastUpdate() As Date
            Get
                Return m_TvMovieLastUpdate.Value
            End Get

        End Property
        Friend Shared ReadOnly Property TvMovieImportIsRunning() As Boolean
            Get
                Return m_TvMovieImportIsRunning.Value
            End Get

        End Property
        Friend Shared ReadOnly Property TvMovieMPDatabase() As String
            Get
                Return m_TvMovieMPDatabase.Value
            End Get
        End Property
        Friend Shared Property ClickfinderDataAvailable() As Boolean
            Get
                Return CBool(m_ClickfinderDataAvailable.Value)
            End Get
            Set(ByVal value As Boolean)
                m_ClickfinderDataAvailable.Value = value
            End Set
        End Property
        Friend Shared ReadOnly Property pluginTvWishList() As Boolean
            Get
                Return m_pluginTvWishList.Value
            End Get
        End Property


#End Region
#End Region

#Region "Functions"
        Friend Shared Sub Load()
            Try
                'ReadOnly vom tvServer (TvMovie++, TvWishList, CPG Data available)
                m_TvMovieImportTvSeriesInfos = _layer.GetSetting("TvMovieImportTvSeriesInfos", "false")
                m_TvMovieImportVideoDatabaseInfos = _layer.GetSetting("TvMovieImportVideoDatabaseInfos", "false")
                m_TvMovieImportMovingPicturesInfos = _layer.GetSetting("TvMovieImportMovingPicturesInfos", "false")
                m_TvMovieUseTheTvDb = _layer.GetSetting("TvMovieUseTheTvDb", "false")
                m_TvMovieEnabled = _layer.GetSetting("TvMovieEnabled", "false")
                m_TvMovieLastUpdate = _layer.GetSetting("TvMovieLastUpdate")
                m_TvMovieImportIsRunning = _layer.GetSetting("TvMovieImportIsRunning", "false")
                m_TvMovieMPDatabase = _layer.GetSetting("TvMovieMPDatabase", "%ProgramData%\Team MediaPortal\MediaPortal\database")
                m_pluginTvWishList = _layer.GetSetting("pluginTvWishList", "false")

                m_ClickfinderDataAvailable = _layer.GetSetting("ClickfinderDataAvailable", "false")

                m_pluginClickfinderProgramGuide = _layer.GetSetting("pluginClickfinderProgramGuide", "false")



                MyLog.Info("[CPGsettings]: [Load]: nessacary TvServer settings loaded")
                MyLog.Info("[CPGsettings]: [Load]: CPG Data Available: {0}, TvMovie++: {1}, TvWishList: {2}", m_ClickfinderDataAvailable.Value, m_TvMovieEnabled.Value, m_pluginTvWishList.Value)
                MyLog.Info("")
            Catch ex As Exception
                MyLog.Error("[CPGsettings]: [Load]: nessacary TvServer settings: exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try

            'Remote Control Dictionaries clear, sofern gefüllt
            Try
                m_ItemsRemoteSortAll.Clear()
                m_ItemsRemoteTvGroupAll.Clear()
            Catch ex As Exception
            End Try

            'If ClickfinderSaveSettingsOnClients = False Then

            LoadFromTvServer()
            'Else
            '    MyLog.Debug("[CPGsettings]: [Load]: load settings from Client")
            '    LoadFromClient()
            'End If

        End Sub
        Friend Shared Sub save()

            MyLog.Debug("[CPGsettings]: [Save]: save settings on TvServer")
            SaveToTvServer()
        End Sub

#Region "TvServer"
        Private Shared Sub LoadFromTvServer()
            Try
                MyLog.Info("[CPGsettings]: [LoadFromTvServer]: load settings from TvServer...")

                'Tab HighLights Settings
                m_OverviewShowMoviesAfter = _layer.GetSetting("ClickfinderOverviewShowMoviesAfter", "20")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_OverviewShowMoviesAfter.Tag, m_OverviewShowMoviesAfter.Value)
                m_OverviewShowHighlightsAfter = _layer.GetSetting("ClickfinderOverviewShowHighlightsAfter", "15")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_OverviewShowHighlightsAfter.Tag, m_OverviewShowHighlightsAfter.Value)
                m_OverviewHighlightsMinRuntime = _layer.GetSetting("ClickfinderOverviewHighlightsMinRuntime", "16")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_OverviewHighlightsMinRuntime.Tag, m_OverviewHighlightsMinRuntime.Value)
                m_OverviewShowLocalMovies = _layer.GetSetting("ClickfinderOverviewShowLocalMovies", "true")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_OverviewShowLocalMovies.Tag, m_OverviewShowLocalMovies.Value)
                m_OverviewMaxDays = _layer.GetSetting("ClickfinderOverviewMaxDays", "14")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_OverviewMaxDays.Tag, m_OverviewMaxDays.Value)
                m_OverviewShowTagesTipp = _layer.GetSetting("ClickfinderOverviewShowTagesTipp", "true")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_OverviewShowTagesTipp.Tag, m_OverviewShowTagesTipp.Value)
                m_OverviewMovieSort = _layer.GetSetting("ClickfinderOverviewMovieSort", SortMethode.RatingStar.ToString)
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_OverviewMovieSort.Tag, m_OverviewMovieSort.Value)


                'Tab Allgemeines
                m_StandardTvGroup = _layer.GetSetting("ClickfinderStandardTvGroup", "All Channels")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_StandardTvGroup.Tag, m_StandardTvGroup.Value)
                m_QuickTvGroup1 = _layer.GetSetting("ClickfinderQuickTvGroup1", "All Channels")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_QuickTvGroup1.Tag, m_QuickTvGroup1.Value)
                m_QuickTvGroup2 = _layer.GetSetting("ClickfinderQuickTvGroup2", "All Channels")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_QuickTvGroup2.Tag, m_QuickTvGroup2.Value)
                m_PrimeTime = _layer.GetSetting("ClickfinderPrimeTime", "20:15")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_PrimeTime.Tag, m_PrimeTime.Value)
                m_LateTime = _layer.GetSetting("ClickfinderLateTime", "22:00")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_LateTime.Tag, m_LateTime.Value)
                m_UseSportLogos = _layer.GetSetting("ClickfinderUseSportLogos", "true")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_UseSportLogos.Tag, m_UseSportLogos.Value)
                m_EpisodenScanner = _layer.GetSetting("ClickfinderEpisodenScanner", "")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_EpisodenScanner.Tag, m_EpisodenScanner.Value)
                m_ClickfinderDatabasePath = _layer.GetSetting("ClickfinderDatabasePath", "")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_ClickfinderDatabasePath.Tag, m_ClickfinderDatabasePath.Value)
                m_ClickfinderImagePath = _layer.GetSetting("ClickfinderImagePath", "")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_ClickfinderImagePath.Tag, m_ClickfinderImagePath.Value)
                m_ClickfinderPluginName = _layer.GetSetting("ClickfinderPluginName", "Clickfinder ProgramGuide")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_ClickfinderPluginName.Tag, m_ClickfinderPluginName.Value)
                m_ClickfinderDebugMode = _layer.GetSetting("ClickfinderDebugMode", "True")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_ClickfinderDebugMode.Tag, m_ClickfinderDebugMode.Value)
                m_ClickfinderStartGui = _layer.GetSetting("ClickfinderStartGui", "Highlights")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_ClickfinderStartGui.Tag, m_ClickfinderStartGui.Value)
                
                'Tab Allgemeines: Vorschau
                m_PreviewMaxDays = _layer.GetSetting("ClickfinderPreviewMaxDays", "14")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_PreviewMaxDays.Tag, m_PreviewMaxDays.Value)
                m_PreviewMinTvMovieRating = _layer.GetSetting("ClickfinderPreviewMinTvMovieRating", "1")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_PreviewMinTvMovieRating.Tag, m_PreviewMinTvMovieRating.Value)


                'Tab Items
                m_ItemsShowLocalMovies = _layer.GetSetting("ClickfinderItemsShowLocalMovies", "false")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_ItemsShowLocalMovies.Tag, m_ItemsShowLocalMovies.Value)

                m_ItemsShowLocalSeries = _layer.GetSetting("ClickfinderItemsShowLocalSeries", "true")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_ItemsShowLocalSeries.Tag, m_ItemsShowLocalSeries.Value)

                m_RemberSortedBy = _layer.GetSetting("ClickfinderRemberSortedBy", "false")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_RemberSortedBy.Tag, m_RemberSortedBy.Value)


             
                'Tab Categories
                m_CategorieShowLocalMovies = _layer.GetSetting("ClickfinderCategorieShowLocalMovies", "true")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_CategorieShowLocalMovies.Tag, m_CategorieShowLocalMovies.Value)
                m_CategorieShowLocalSeries = _layer.GetSetting("ClickfinderCategorieShowLocalSeries", "true")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_CategorieShowLocalSeries.Tag, m_CategorieShowLocalSeries.Value)


                'Tab Detail
                m_DetailUseSeriesDescribtion = _layer.GetSetting("ClickfinderDetailUseSeriesDescribtion", "false")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_DetailUseSeriesDescribtion.Tag, m_DetailUseSeriesDescribtion.Value)

                m_DetailSeriesImage = _layer.GetSetting("ClickfinderDetailsSeriesImage", "Cover")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_DetailSeriesImage.Tag, m_DetailSeriesImage.Value)

                'Tab Overlay
                m_OverlayMoviesEnabled = _layer.GetSetting("ClickfinderOverlayMoviesEnabled", "true")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_OverlayMoviesEnabled.Tag, m_OverlayMoviesEnabled.Value)

                m_OverlayTime = _layer.GetSetting("ClickfinderOverlayTime", "PrimeTime")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_OverlayTime.Tag, m_OverlayTime.Value)

                m_OverlaySeriesEnabled = _layer.GetSetting("ClickfinderOverlaySeriesEnabled", "false")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_OverlaySeriesEnabled.Tag, m_OverlaySeriesEnabled.Value)

                m_OverlayShowTagesTipp = _layer.GetSetting("ClickfinderOverlayShowTagesTipp", "true")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_OverlayShowTagesTipp.Tag, m_OverlayShowTagesTipp.Value)

                m_OverlayMovieSort = _layer.GetSetting("ClickfinderOverlayMovieSort", SortMethode.startTime.ToString)
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_OverlayMovieSort.Tag, m_OverlayMovieSort.Value)


                m_OverlayMovieLimit = _layer.GetSetting("ClickfinderOverlayMovieLimit", "25")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_OverlayMovieLimit.Tag, m_OverlayMovieLimit.Value)

                m_OverlayShowLocalMovies = _layer.GetSetting("ClickfinderOverlayShowLocalMovies", "true")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_OverlayShowLocalMovies.Tag, m_OverlayShowLocalMovies.Value)

                m_OverlayTvGroup = _layer.GetSetting("ClickfinderOverlayTvGroup", "All Channels")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_OverlayTvGroup.Tag, m_OverlayTvGroup.Value)

                m_OverlayUpdateTimer = _layer.GetSetting("ClickfinderOverlayUpdateTimer", "5")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_OverlayUpdateTimer.Tag, m_OverlayUpdateTimer.Value)


                m_pluginClickfinderProgramGuide = _layer.GetSetting("pluginClickfinder ProgramGuide", "false")
                MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", m_pluginClickfinderProgramGuide.Tag, m_pluginClickfinderProgramGuide.Value)


                'Remote Control Sort + Dictionary füllen
                m_ItemsRemoteSort1 = _layer.GetSetting("ClickfinderItemsRemoteSort1", "")
                m_ItemsRemoteSort2 = _layer.GetSetting("ClickfinderItemsRemoteSort2", "")
                m_ItemsRemoteSort3 = _layer.GetSetting("ClickfinderItemsRemoteSort3", "")
                m_ItemsRemoteSort4 = _layer.GetSetting("ClickfinderItemsRemoteSort4", "")
                m_ItemsRemoteSort5 = _layer.GetSetting("ClickfinderItemsRemoteSort5", "")
                m_ItemsRemoteSort6 = _layer.GetSetting("ClickfinderItemsRemoteSort6", "")
                m_ItemsRemoteSort7 = _layer.GetSetting("ClickfinderItemsRemoteSort7", "")
                m_ItemsRemoteSort8 = _layer.GetSetting("ClickfinderItemsRemoteSort8", "")
                m_ItemsRemoteSort9 = _layer.GetSetting("ClickfinderItemsRemoteSort9", "")
                m_ItemsRemoteSort0 = _layer.GetSetting("ClickfinderItemsRemoteSort0", "")

                m_ItemsRemoteSortAll.Add(1, m_ItemsRemoteSort1)
                m_ItemsRemoteSortAll.Add(2, m_ItemsRemoteSort2)
                m_ItemsRemoteSortAll.Add(3, m_ItemsRemoteSort3)
                m_ItemsRemoteSortAll.Add(4, m_ItemsRemoteSort4)
                m_ItemsRemoteSortAll.Add(5, m_ItemsRemoteSort5)
                m_ItemsRemoteSortAll.Add(6, m_ItemsRemoteSort6)
                m_ItemsRemoteSortAll.Add(7, m_ItemsRemoteSort7)
                m_ItemsRemoteSortAll.Add(8, m_ItemsRemoteSort8)
                m_ItemsRemoteSortAll.Add(9, m_ItemsRemoteSort9)
                m_ItemsRemoteSortAll.Add(0, m_ItemsRemoteSort0)

                For Each _setting In m_ItemsRemoteSortAll
                    MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", _setting.Value.Tag, _setting.Value.Value)
                Next

                'Remote Control TvGroup + Dictionary füllen
                m_ItemsRemoteTvGroup1 = _layer.GetSetting("ClickfinderItemsRemoteTvGroup1", "")
                m_ItemsRemoteTvGroup2 = _layer.GetSetting("ClickfinderItemsRemoteTvGroup2", "")
                m_ItemsRemoteTvGroup3 = _layer.GetSetting("ClickfinderItemsRemoteTvGroup3", "")
                m_ItemsRemoteTvGroup4 = _layer.GetSetting("ClickfinderItemsRemoteTvGroup4", "")
                m_ItemsRemoteTvGroup5 = _layer.GetSetting("ClickfinderItemsRemoteTvGroup5", "")
                m_ItemsRemoteTvGroup6 = _layer.GetSetting("ClickfinderItemsRemoteTvGroup6", "")
                m_ItemsRemoteTvGroup7 = _layer.GetSetting("ClickfinderItemsRemoteTvGroup7", "")
                m_ItemsRemoteTvGroup8 = _layer.GetSetting("ClickfinderItemsRemoteTvGroup8", "")
                m_ItemsRemoteTvGroup9 = _layer.GetSetting("ClickfinderItemsRemoteTvGroup9", "")
                m_ItemsRemoteTvGroup0 = _layer.GetSetting("ClickfinderItemsRemoteTvGroup0", "")

                m_ItemsRemoteTvGroupAll.Add(1, m_ItemsRemoteTvGroup1)
                m_ItemsRemoteTvGroupAll.Add(2, m_ItemsRemoteTvGroup2)
                m_ItemsRemoteTvGroupAll.Add(3, m_ItemsRemoteTvGroup3)
                m_ItemsRemoteTvGroupAll.Add(4, m_ItemsRemoteTvGroup4)
                m_ItemsRemoteTvGroupAll.Add(5, m_ItemsRemoteTvGroup5)
                m_ItemsRemoteTvGroupAll.Add(6, m_ItemsRemoteTvGroup6)
                m_ItemsRemoteTvGroupAll.Add(7, m_ItemsRemoteTvGroup7)
                m_ItemsRemoteTvGroupAll.Add(8, m_ItemsRemoteTvGroup8)
                m_ItemsRemoteTvGroupAll.Add(9, m_ItemsRemoteTvGroup9)
                m_ItemsRemoteTvGroupAll.Add(0, m_ItemsRemoteTvGroup0)

                For Each _setting In m_ItemsRemoteTvGroupAll
                    MyLog.Debug("[CPGsettings]: [LoadFromTvServer]: {0} = {1}", _setting.Value.Tag, _setting.Value.Value)
                Next

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

                For Each Setting In m_ItemsRemoteSortAll
                    Setting.Value.Persist()
                Next

                For Each Setting In m_ItemsRemoteTvGroupAll
                    Setting.Value.Persist()
                Next


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
            Dim sReturn As Setting = New Setting(Tag, Value)
            Try

                Dim _path As String = Config.GetFile(Config.Dir.Config, "ClickfinderProgramGuide.xml")
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


            Catch ex As Exception
                MyLog.Error("[CPGsettings]: [loadFromXml]: exception err:" & ex.Message & " stack:" & ex.StackTrace)
            End Try
            Return sReturn
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
