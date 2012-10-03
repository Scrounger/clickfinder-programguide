﻿
''' <summary>
''' These will be loaded with the language files content
''' if the selected lang file is not found, it will first try to load en(us).xml as a backup
''' if that also fails it will use the hardcoded strings as a last resort.
''' </summary>
Public NotInheritable Class Translation

    Private Sub New()
    End Sub

    Public Shared Record As String = "Aufnehmen"
    Public Shared Remember As String = "Erinnern"
    Public Shared ChannelON As String = "Play / Kanal einschalten"
    Public Shared SameGenre As String = "gleiches Genre:"
    Public Shared Season As String = "Staffel"
    Public Shared Episode As String = "Episode"
    Public Shared NewEpisode As String = "Neu Episode !!!"
    Public Shared EpisodePrefixLabel As String = "Folge:"
    Public Shared EpisodeNewPrefixLabel As String = "Neue Folge:"
    Public Shared Now As String = "Jetzt"
    Public Shared PrimeTime As String = "Prime Time"
    Public Shared LateTime As String = "Late Time"
    Public Shared Preview As String = "Vorschau"
    Public Shared CategoriePreviewLabel As String = "ab:"
    Public Shared Highlights As String = "Highlights"
    Public Shared Categorie As String = "Kategorie"
    Public Shared CategorieEdit As String = "bearbeiten"
    Public Shared CategorieRename As String = "umbennen"
    Public Shared CategorieHide As String = "verstecken"
    Public Shared CategorieShow As String = "versteckte Kategorie anzeigen"
    Public Shared CategorieChoose As String = "wähle die Kategorie aus, die angezeigt werden soll"
    Public Shared CategorieHideNotFound As String = "Keine versteckten Kategorien gefunden..."
    Public Shared PageLabel As String = "Seite"
    Public Shared allMoviesAt As String = "Alle Filme von"
    Public Shared allMoviesNow As String = "Alle Filme ab Jetzt"
    Public Shared allHighlightsAt As String = "Alle Highlights von"
    Public Shared allCategoriesAt As String = "Alle Kategorien von"
    Public Shared All As String = "Alle"
    Public Shared TwoDaysAgo As String = "Vorgestern"
    Public Shared at As String = "um"
    Public Shared before As String = "vor"
    Public Shared Days As String = "Tagen"
    Public Shared Loading As String = "Daten werden geladen ..."
    Public Shared existlocal As String = "lokal"
    Public Shared SortStartTime As String = "Startzeit"
    Public Shared SortTvMovieStar As String = "Tv Movie Bewertung"
    Public Shared SortRatingStar As String = "Rating"
    Public Shared SortGenre As String = "Genre"
    Public Shared SortparentalRating As String = "Altersfreigabe"
    Public Shared Sortby As String = "Sortieren nach ..."
    Public Shared SortbyGuiItems As String = "Sortiert nach"
    Public Shared von As String = "von"
    Public Shared action As String = "Aktion ..."
    Public Shared Refresh As String = "Aktualisieren"
    Public Shared ImportIsRunning As String = "Import wird zur Zeit durchgeführt !"
    Public Shared Filterby As String = "Nach Tv-Gruppe filtern ..."
    Public Shared MinuteLabel As String = "min"
    Public Shared DurationLabel As String = "Dauer:"
    Public Shared TimeLabel As String = "Uhrzeit:"
    Public Shared ChannelLabel As String = "Sender:"
    Public Shared LevelLabel As String = "Anspruch"
    Public Shared FunLabel As String = "Spaß"
    Public Shared ActionLabel As String = "Action"
    Public Shared EroticLabel As String = "Erotik"
    Public Shared SuspenseLabel As String = "Spannung"
    Public Shared EmotionsLabel As String = "Gefühl"
    Public Shared SerieLinkLabel As String = "mit Serie verknüpfen ..."
    Public Shared YearLabel As String = "Jahr:"
    Public Shared GenreLabel As String = "Genre:"
    Public Shared RegieLabel As String = "Regie:"
    Public Shared ActorsLabel As String = "Schauspieler:"
    Public Shared Critic As String = "Kritik:"
    Public Shared LinkTo As String = "verknüpft mit:"
    Public Shared NextLabel As String = "Gleich läuft:"
    Public Shared OverlayTitle As String = "Heute im Fernsehen"
    Public Shared Back As String = "Zurück"
    Public Shared Warning As String = "Warnung"
    Public Shared TvServerOffline As String = "Tv Server nicht erreichbar!"
    Public Shared ClickfinderDBOffline = "Clickfinder Datenbank nicht erreichbar!"
    Public Shared TvMovieEPGImportNotEnabled As String = "Tv Movie EPG Import++ nicht aktiviert / installiert!"
    Public Shared ClickfinderImportNotEnabled As String = "Clickfinder ProgramGuide Import im Tv Movie EPG Import++ Plugin nicht aktiviert!"
    Public Shared MyMovies As String = "Meine Filme"
    Public Shared NewEpisodes As String = "Neue Episoden"
    Public Shared Friday As String = "Freitag"
    Public Shared MovieListTitle As String = "Movie Highlights"
    Public Shared HighlightsListTitle As String = "weitere Highlights"
    Public Shared LastUpdate As String = "Letztes Update:"
    Public Shared Monday As String = "Montag"
    Public Shared NewLabel As String = "Neu !!!"
    Public Shared NewEpisodeFound As String = "neue Episode(n) gefunden"
    Public Shared Saturday As String = "Samstag"
    Public Shared Sunday As String = "Sonntag"
    Public Shared Today As String = "Heute"
    Public Shared Tomorrow As String = "Morgen"
    Public Shared Tuesday As String = "Dienstag"
    Public Shared Thursday As String = "Donnerstag"
    Public Shared Wednesday As String = "Mittwoch"
    Public Shared Yesterday As String = "Gestern"
    Public Shared Day As String = "Tage"
    Public Shared [for] As String = "für"
    Public Shared IMDB As String = "IMDb Movie Trailers"
    Public Shared MovingPictures As String = "MovingPictures"
    Public Shared MPtvSeries As String = "MP-TvSeries"
    Public Shared InTheNext As String = "in den nächsten"
    Public Shared Repeats As String = "Wiederholungen"
    Public Shared SeriesInfos As String = "Infos zur Serie"
    Public Shared Ended As String = "beendet"
    Public Shared Continous As String = "wird fortgesetzt"

    Public Shared EpisodeNotIdentify As String = "Episode konnte nicht identifziert werden"
    Public Shared allNewEpisodes As String = "alle neuen Episoden"
    Public Shared episodesNowExistLocal As String = "Episode(n) lokal gefunden"
    Public Shared DataRefreshed As String = "Daten wurden aktualisiert"
    Public Shared SeriesMappingAppend As String = "Verlinkung hinzufügen"
    Public Shared SeriesMappingOverwrite As String = "Verlinkung ersetzen"

    Public Shared DBRefresh As String = "Mit Datenbanken abgleichen..."
    Public Shared DBRefreshStarted As String = "Datenbank Ableich gestartet..."
    Public Shared DBRefreshRunning As String = "Datenbank Abgleich läuft..."
    Public Shared DBRefreshFinished As String = "Datenbank Abgleich erfolgreich!"
    Public Shared RefreshOverlays As String = "Overlays werden aktualisiert..."


End Class

