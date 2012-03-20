
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
    Public Shared ChannelON As String = "Kanal einschalten"
    Public Shared SameGenre As String = "gleiches Genre:"


    Public Shared Season As String = "Staffel"
    Public Shared Episode As String = "Episode"
    Public Shared NewEpisode As String = "Neu Episode !!!"
    Public Shared EpisodePrefixLabel As String = "Folge:"
    Public Shared EpisodeNewPrefixLabel As String = "Neue Folge:"
    Public Shared Now As String = "Jetzt"
    Public Shared PrimeTime As String = "Prime Time"
    Public Shared LateTime As String = "Late Time"
    Public Shared Preview As String = "Preview"
    Public Shared CategoriePreviewLabel = "ab:"
    Public Shared Highlights = "Highlights"
    Public Shared Categorie = "Kategorie"
    Public Shared CategorieEdit = "bearbeiten"
    Public Shared CategorieRename = "umbennen"
    Public Shared CategorieHide = "verstecken"
    Public Shared CategorieShow = "versteckte Kategorie anzeigen"
    Public Shared CategorieChoose = "wähle die Kategorie aus, die angezeigt werden soll"
    Public Shared CategorieHideNotFound = "Keine versteckten Kategorien gefunden..."
    Public Shared PageLabel = "Seite"
    Public Shared allMoviesAt = "Alle Filme von"
    Public Shared allHighlightsAt = "Alle Highlights von"
    Public Shared TwoDaysAgo = "Vorgestern"
    Public Shared at = "um"
    Public Shared before = "vor"
    Public Shared Days = "Tagen"
    Public Shared Loading = "Daten werden geladen ..."
    Public Shared existlocal = "lokal"
    Public Shared SortStartTime = "nach Startzeit sortieren"
    Public Shared SortTvMovieStar = "nach Tv Movie Bewertung sortieren"
    Public Shared SortRatingStar = "nach Rating sortieren"
    Public Shared Sortby = "sortieren nach ..."
    Public Shared von = "von"
    Public Shared action = "Aktion ..."
    Public Shared Refresh = "Aktualisieren"

    ' A

    ' B

    ' C

    ' D

    ' E

    ' F
    Public Shared Friday As String = "Freitag"

    ' G
    Public Shared MovieListTitle As String = "Movie Higlights"
    Public Shared HighlightsListTitle As String = "weitere Higlights"



    ' H


    ' I


    ' L
    Public Shared LastUpdate As String = "Letztes Update:"

    ' M
    Public Shared Monday As String = "Montag"

    ' N
    Public Shared NewLabel As String = "Neu !!!"
    Public Shared NewEpisodeFound As String = "Neu Episode(n) gefunden"


    ' O


    ' P


    ' R


    ' S
    Public Shared Saturday As String = "Samstag"
    Public Shared Sunday As String = "Sonntag"

    ' T
    Public Shared Today As String = "Heute"
    Public Shared Tomorrow As String = "Morgen"
    Public Shared Tuesday As String = "Dienstag"
    Public Shared Thursday As String = "Donnerstag"

    ' U
   

    ' V


    ' W
    Public Shared Wednesday As String = "Mittwoch"

    ' X

    ' Y
    Public Shared Yesterday As String = "Gestern"

    ' Z
End Class

