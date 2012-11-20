﻿#Region "Copyright (C) 2005-2011 Team MediaPortal"

' Copyright (C) 2005-2011 Team MediaPortal
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

Imports System
Imports System.Collections.Generic
Imports Gentle.Framework
Imports TvLibrary.Log
Imports TvDatabase
Imports System.Reflection

Namespace TvDatabase
    ''' <summary>
    ''' Instances of this class represent the properties and methods of a row in the table <b>TVMovieProgram</b>.
    ''' </summary>
    <TableName("TVMovieProgram")> _
    Public Class TVMovieProgram
        Inherits Persistent

#Region "Members"

        Private m_isChanged As Boolean

        <TableColumn("idTVMovieProgram", NotNull:=True), PrimaryKey(AutoGenerated:=True)> _
        Private m_idTVMovieProgram As Integer

        <TableColumn("idProgram", NotNull:=True)> _
        Private m_idProgram As Integer

        <TableColumn("TVMovieBewertung", NotNull:=True)> _
        Private m_TVMovieBewertung As Integer

        <TableColumn("idSeries", NotNull:=True)> _
        Private m_idSeries As Integer

        <TableColumn("SeriesPosterImage", NotNull:=True)> _
        Private m_SeriesPosterImage As String

        <TableColumn("FanArt", NotNull:=True)> _
        Private m_FanArt As String

        <TableColumn("idEpisode", NotNull:=True)> _
        Private m_idEpisode As String

        <TableColumn("EpisodeImage", NotNull:=True)> _
        Private m_EpisodeImage As String

        <TableColumn("local", NotNull:=True)> _
        Private m_local As Boolean

        <TableColumn("idMovingPictures", NotNull:=True)> _
        Private m_idMovingPictures As Integer

        <TableColumn("idVideo", NotNull:=True)> _
        Private m_idVideo As Integer

        <TableColumn("KurzKritik", NotNull:=True)> _
        Private m_KurzKritik As String

        <TableColumn("BildDateiname", NotNull:=True)> _
        Private m_BildDateiname As String

        <TableColumn("Cover", NotNull:=True)> _
        Private m_Cover As String

        <TableColumn("Fun", NotNull:=True)> _
        Private m_Fun As Integer

        <TableColumn("Action", NotNull:=True)> _
        Private m_Action As Integer

        <TableColumn("Feelings", NotNull:=True)> _
        Private m_Feelings As Integer

        <TableColumn("Erotic", NotNull:=True)> _
        Private m_Erotic As Integer

        <TableColumn("Tension", NotNull:=True)> _
        Private m_Tension As Integer

        <TableColumn("Requirement", NotNull:=True)> _
        Private m_Requirement As Integer

        <TableColumn("needsUpdate", NotNull:=True)> _
        Private m_needsUpdate As Boolean

        <TableColumn("Dolby", NotNull:=True)> _
        Private m_Dolby As Boolean

        <TableColumn("HDTV", NotNull:=True)> _
        Private m_HDTV As Boolean


        <TableColumn("Actors", NotNull:=True)> _
        Private m_Actors As String


        <TableColumn("Country", NotNull:=True)> _
        Private m_Country As String

        <TableColumn("Regie", NotNull:=True)> _
        Private m_Regie As String

        <TableColumn("Year", NotNull:=True)> _
       Private m_Year As Date

        <TableColumn("Describtion", NotNull:=True)> _
       Private m_Describtion As String

        <TableColumn("ShortDescribtion", NotNull:=True)> _
       Private m_ShortDescribtion As String

        <TableColumn("FileName", NotNull:=True)> _
       Private m_FileName As String

        '<TableColumn("RatingString", NotNull:=True)> _
        'Private m_RatingString As String

#End Region

#Region "Constructors"


        'public TVMovieProgram(int idClickfinderMapping, int idProgram, string idSeries, string timeSharingStart, string timeSharingEnd)
        '{
        '  isChanged = true;
        '  this.idClickfinderMapping = idClickfinderMapping;
        '  this.idProgram = idProgram;
        '  this.idSeries = idSeries;
        '  this.timeSharingStart = timeSharingStart;
        '  this.timeSharingEnd = timeSharingEnd;
        '}
        Public Sub New(ByVal idProgram As Integer)
            Me.m_idProgram = idProgram
        End Sub

#End Region

#Region "Public Properties"

        ''' <summary>
        ''' Indicates whether the entity is changed and requires saving or not.
        ''' </summary>
        Public ReadOnly Property IsChanged() As Boolean
            Get
                Return m_isChanged
            End Get
        End Property

        ''' <summary>
        ''' Property relating to database column idTVMovieProgram
        ''' </summary>
        Public Property idTVMovieProgram() As Integer
            Get
                Return m_idTVMovieProgram
            End Get
            Set(ByVal value As Integer)
                m_isChanged = m_isChanged Or m_idTVMovieProgram <> value
                m_idTVMovieProgram = value
            End Set
        End Property

        ''' <summary>
        ''' Property relating to database column TVMovieBewertung
        ''' </summary>
        Public Property TVMovieBewertung() As Integer
            Get
                Return m_TVMovieBewertung
            End Get
            Set(ByVal value As Integer)
                m_isChanged = m_isChanged Or m_TVMovieBewertung <> value
                m_TVMovieBewertung = value
            End Set
        End Property

        ''' <summary>
        ''' Property relating to database column idProgram
        ''' </summary>
        Public Property idProgram() As Integer
            Get
                Return m_idProgram
            End Get
            Set(ByVal value As Integer)
                m_isChanged = m_isChanged Or m_idProgram <> value
                m_idProgram = value
            End Set
        End Property
        ''' <summary>
        ''' Property relating to database column idSeries
        ''' </summary>
        Public Property idSeries() As Integer
            Get
                Return m_idSeries
            End Get
            Set(ByVal value As Integer)
                m_isChanged = m_isChanged Or m_idSeries <> value
                m_idSeries = value
            End Set
        End Property
        Public Property SeriesPosterImage() As String
            Get
                Return m_SeriesPosterImage
            End Get
            Set(ByVal value As String)
                m_isChanged = m_isChanged Or m_SeriesPosterImage <> value
                '"\" am Anfang entfernen sofern vorhanden
                If Not String.IsNullOrEmpty(value) And value(0) = "\" Then
                    value = value.Remove(0, 1)
                End If

                m_SeriesPosterImage = value
            End Set
        End Property

        Public Property FanArt() As String
            Get
                Return m_FanArt
            End Get
            Set(ByVal value As String)
                m_isChanged = m_isChanged Or m_FanArt <> value
                '"\" am Anfang entfernen sofern vorhanden
                If Not String.IsNullOrEmpty(value) And value(0) = "\" Then
                    value = value.Remove(0, 1)
                End If

                m_FanArt = value
            End Set
        End Property
        Public Property idEpisode() As String
            Get
                Return m_idEpisode
            End Get
            Set(ByVal value As String)
                m_isChanged = m_isChanged Or m_idEpisode <> value
                m_idEpisode = value
            End Set
        End Property
        Public Property EpisodeImage() As String
            Get
                Return m_EpisodeImage
            End Get
            Set(ByVal value As String)
                m_isChanged = m_isChanged Or m_EpisodeImage <> value
                '"\" am Anfang entfernen sofern vorhanden
                If Not String.IsNullOrEmpty(value) And value(0) = "\" Then
                    value = value.Remove(0, 1)
                End If

                m_EpisodeImage = value
            End Set
        End Property

        Public Property local() As Boolean
            Get
                Return m_local
            End Get
            Set(ByVal value As Boolean)
                m_isChanged = m_isChanged Or m_local <> value
                m_local = value
            End Set
        End Property
        ''' <summary>
        ''' Property relating to database column idMovingPictures
        ''' </summary>
        Public Property idMovingPictures() As Integer
            Get
                Return m_idMovingPictures
            End Get
            Set(ByVal value As Integer)
                m_isChanged = m_isChanged Or m_idMovingPictures <> value
                m_idMovingPictures = value
            End Set
        End Property

        ''' <summary>
        ''' Property relating to database column idVideo
        ''' </summary>
        Public Property idVideo() As Integer
            Get
                Return m_idVideo
            End Get
            Set(ByVal value As Integer)
                m_isChanged = m_isChanged Or m_idVideo <> value
                m_idVideo = value
            End Set
        End Property
        Public Property KurzKritik() As String
            Get
                Return m_KurzKritik
            End Get
            Set(ByVal value As String)
                m_isChanged = m_isChanged Or m_KurzKritik <> value
                m_KurzKritik = value
            End Set
        End Property
        Public Property BildDateiname() As String
            Get
                Return m_BildDateiname
            End Get
            Set(ByVal value As String)
                m_isChanged = m_isChanged Or m_BildDateiname <> value
                m_BildDateiname = value
            End Set
        End Property
        Public Property Cover() As String
            Get
                Return m_Cover
            End Get
            Set(ByVal value As String)
                m_isChanged = m_isChanged Or m_Cover <> value
                m_Cover = value
            End Set
        End Property



        Public Property Fun() As Integer
            Get
                Return m_Fun
            End Get
            Set(ByVal value As Integer)
                m_isChanged = m_isChanged Or m_Fun <> value
                m_Fun = value
            End Set
        End Property
        Public Property Action() As Integer
            Get
                Return m_Action
            End Get
            Set(ByVal value As Integer)
                m_isChanged = m_isChanged Or m_Action <> value
                m_Action = value
            End Set
        End Property
        Public Property Feelings() As Integer
            Get
                Return m_Feelings
            End Get
            Set(ByVal value As Integer)
                m_isChanged = m_isChanged Or m_Feelings <> value
                m_Feelings = value
            End Set
        End Property
        Public Property Erotic() As Integer
            Get
                Return m_Erotic
            End Get
            Set(ByVal value As Integer)
                m_isChanged = m_isChanged Or m_Erotic <> value
                m_Erotic = value
            End Set
        End Property
        Public Property Tension() As Integer
            Get
                Return m_Tension
            End Get
            Set(ByVal value As Integer)
                m_isChanged = m_isChanged Or m_Tension <> value
                m_Tension = value
            End Set
        End Property

        Public Property Requirement() As Integer
            Get
                Return m_Requirement
            End Get
            Set(ByVal value As Integer)
                m_isChanged = m_isChanged Or m_Requirement <> value
                m_Requirement = value
            End Set
        End Property


        Public Property needsUpdate() As Boolean
            Get
                Return m_needsUpdate
            End Get
            Set(ByVal value As Boolean)
                m_isChanged = m_isChanged Or m_needsUpdate <> value
                m_needsUpdate = value
            End Set
        End Property
        Public Property Dolby() As Boolean
            Get
                Return m_Dolby
            End Get
            Set(ByVal value As Boolean)
                m_isChanged = m_isChanged Or m_Dolby <> value
                m_Dolby = value
            End Set
        End Property
        Public Property HDTV() As Boolean
            Get
                Return m_HDTV
            End Get
            Set(ByVal value As Boolean)
                m_isChanged = m_isChanged Or m_HDTV <> value
                m_HDTV = value
            End Set
        End Property


        Public Property Actors() As String
            Get
                Return m_Actors
            End Get
            Set(ByVal value As String)
                m_isChanged = m_isChanged Or m_Actors <> value
                m_Actors = value
            End Set
        End Property

        Public Property Country() As String
            Get
                Return m_Country
            End Get
            Set(ByVal value As String)
                m_isChanged = m_isChanged Or m_Country <> value
                m_Country = value
            End Set
        End Property

        Public Property Regie() As String
            Get
                Return m_Regie
            End Get
            Set(ByVal value As String)
                m_isChanged = m_isChanged Or m_Regie <> value
                m_Regie = value
            End Set
        End Property

        Public Property Year() As Date
            Get
                Return m_Year
            End Get
            Set(ByVal value As Date)
                m_isChanged = m_isChanged Or m_Year <> value
                m_Year = value
            End Set
        End Property


        Public Property Describtion() As String
            Get
                Return m_Describtion
            End Get
            Set(ByVal value As String)
                m_isChanged = m_isChanged Or m_Describtion <> value
                m_Describtion = value
            End Set
        End Property
        Public Property ShortDescribtion() As String
            Get
                Return m_ShortDescribtion
            End Get
            Set(ByVal value As String)
                m_isChanged = m_isChanged Or m_ShortDescribtion <> value
                m_ShortDescribtion = value
            End Set
        End Property

        Public Property FileName() As String
            Get
                Return m_FileName
            End Get
            Set(ByVal value As String)
                m_isChanged = m_isChanged Or m_FileName <> value
                m_FileName = value
            End Set
        End Property

#End Region

#Region "Storage and Retrieval"

        ''' <summary>
        ''' Static method to retrieve all instances that are stored in the database in one call
        ''' </summary>
        Public Shared Function ListAll() As IList(Of TVMovieProgram)
            Return Gentle.Framework.Broker.RetrieveList(Of TVMovieProgram)()
        End Function

        ''' <summary>
        ''' Retrieves an entity given it's id.
        ''' </summary>
        Public Overloads Shared Function Retrieve(ByVal idProgram As Integer) As TVMovieProgram
            Dim key As New Key(GetType(TVMovieProgram), True, "idProgram", idProgram)
            Return Gentle.Framework.Broker.RetrieveInstance(Of TVMovieProgram)(key)
        End Function

        ''' <summary>
        ''' Retrieves an entity given it's id, using Gentle.Framework.Key class.
        ''' This allows retrieval based on multi-column keys.
        ''' </summary>
        Public Overloads Shared Function Retrieve(ByVal key As Key) As TVMovieProgram
            Return Gentle.Framework.Broker.RetrieveInstance(Of TVMovieProgram)(key)
        End Function

        ''' <summary>
        ''' Persists the entity if it was never persisted or was changed.
        ''' </summary>
        Public Overrides Sub Persist()
            If IsChanged OrElse Not IsPersisted Then
                Try
                    MyBase.Persist()
                Catch ex As Exception
                    MyLog.[Error]("Exception in TVMovieProgram.Persist() with Message {0}", ex.Message)
                    Return
                End Try
                m_isChanged = False
            End If
        End Sub

#End Region

#Region "Relations"

        ''' <summary>
        '''
        ''' </summary>
        <CLSCompliant(False)> _
        Public Function ReferencedProgram() As Program
            Return Program.Retrieve(idProgram)
        End Function

#End Region

        Public Sub Delete()
            Dim list As IList(Of TVMovieProgram) = ListAll()
            For Each map As TVMovieProgram In list
                map.Remove()
            Next
            'Remove()
        End Sub

    End Class

#Region "Sort Methods"
    Public Class TVMovieProgram_SortByStartTime
        'Sortieren nach Genre ASC & StarRating DESC
        Implements IComparer(Of TVMovieProgram)
        Public Function Compare(ByVal x As TVMovieProgram, ByVal y As TVMovieProgram) As Integer Implements System.Collections.Generic.IComparer(Of TVMovieProgram).Compare
            If x.ReferencedProgram.StartTime = y.ReferencedProgram.StartTime AndAlso y.ReferencedProgram.StarRating = x.ReferencedProgram.StarRating Then
                Return 0
            ElseIf x.ReferencedProgram.StartTime > y.ReferencedProgram.StartTime Then
                Return 1
            ElseIf x.ReferencedProgram.StartTime = y.ReferencedProgram.StartTime AndAlso y.ReferencedProgram.StarRating > x.ReferencedProgram.StarRating Then
                Return 1
            Else
                Return -1
            End If
        End Function
    End Class
    Public Class TVMovieProgram_SortByTvMovieBewertung
        'Sortieren nach TvMovieBewertung DESC & StarRating DESC
        Implements IComparer(Of TVMovieProgram)
        Public Function Compare(ByVal x As TVMovieProgram, ByVal y As TVMovieProgram) As Integer Implements System.Collections.Generic.IComparer(Of TVMovieProgram).Compare
            If y.TVMovieBewertung = x.TVMovieBewertung AndAlso y.ReferencedProgram.StarRating = x.ReferencedProgram.StarRating Then
                Return 0
            ElseIf y.TVMovieBewertung > x.TVMovieBewertung Then
                Return 1
            ElseIf y.TVMovieBewertung = x.TVMovieBewertung AndAlso y.ReferencedProgram.StarRating > x.ReferencedProgram.StarRating Then
                Return 1
            Else
                Return -1
            End If
        End Function
    End Class
    Public Class TVMovieProgram_SortByRating
        'Sortieren nach TvMovieBewertung DESC & StarRating DESC
        Implements IComparer(Of TVMovieProgram)
        Public Function Compare(ByVal x As TVMovieProgram, ByVal y As TVMovieProgram) As Integer Implements System.Collections.Generic.IComparer(Of TVMovieProgram).Compare
            If y.ReferencedProgram.StarRating = x.ReferencedProgram.StarRating AndAlso x.ReferencedProgram.StartTime = y.ReferencedProgram.StartTime Then
                Return 0
            ElseIf y.ReferencedProgram.StarRating > x.ReferencedProgram.StarRating Then
                Return 1
            ElseIf y.ReferencedProgram.StarRating = x.ReferencedProgram.StarRating AndAlso x.ReferencedProgram.StartTime > y.ReferencedProgram.StartTime Then
                Return 1
            Else
                Return -1
            End If
        End Function
    End Class
    Public Class TVMovieProgram_SortByGenre
        'Sortieren nach Genre ASC & StarRating DESC
        Implements IComparer(Of TVMovieProgram)
        Public Function Compare(ByVal x As TVMovieProgram, ByVal y As TVMovieProgram) As Integer Implements System.Collections.Generic.IComparer(Of TVMovieProgram).Compare
            If x.ReferencedProgram.Genre = y.ReferencedProgram.Genre AndAlso y.ReferencedProgram.StarRating = x.ReferencedProgram.StarRating Then
                Return 0
            ElseIf x.ReferencedProgram.Genre > y.ReferencedProgram.Genre Then
                Return 1
            ElseIf x.ReferencedProgram.Genre = y.ReferencedProgram.Genre AndAlso y.ReferencedProgram.StarRating > x.ReferencedProgram.StarRating Then
                Return 1
            Else
                Return -1
            End If
        End Function
    End Class
    Public Class TVMovieProgram_SortByParentalRating
        'Sortieren nach TvMovieBewertung DESC & StarRating DESC
        Implements IComparer(Of TVMovieProgram)
        Public Function Compare(ByVal x As TVMovieProgram, ByVal y As TVMovieProgram) As Integer Implements System.Collections.Generic.IComparer(Of TVMovieProgram).Compare
            If y.ReferencedProgram.ParentalRating = x.ReferencedProgram.ParentalRating AndAlso y.ReferencedProgram.StarRating = x.ReferencedProgram.StarRating Then
                Return 0
            ElseIf y.ReferencedProgram.ParentalRating > x.ReferencedProgram.ParentalRating Then
                Return 1
            ElseIf y.ReferencedProgram.ParentalRating = x.ReferencedProgram.ParentalRating AndAlso y.ReferencedProgram.StarRating > x.ReferencedProgram.StarRating Then
                Return 1
            Else
                Return -1
            End If
        End Function
    End Class
    Public Class TVMovieProgram_SortByTitle
        'Sortieren nach Genre ASC & StarRating DESC
        Implements IComparer(Of TVMovieProgram)
        Public Function Compare(ByVal x As TVMovieProgram, ByVal y As TVMovieProgram) As Integer Implements System.Collections.Generic.IComparer(Of TVMovieProgram).Compare
            If x.ReferencedProgram.Title = y.ReferencedProgram.Title AndAlso y.ReferencedProgram.StarRating = x.ReferencedProgram.StarRating Then
                Return 0
            ElseIf x.ReferencedProgram.Title > y.ReferencedProgram.Title Then
                Return 1
            ElseIf x.ReferencedProgram.Title = y.ReferencedProgram.Title AndAlso y.ReferencedProgram.StarRating > x.ReferencedProgram.StarRating Then
                Return 1
            Else
                Return -1
            End If
        End Function
    End Class

    Public Class TVMovieProgram_SortByAllRecordingStates
        'Sortieren nach TvMovieBewertung DESC & StarRating DESC
        Implements IComparer(Of TVMovieProgram)
        Public Function Compare(ByVal x As TVMovieProgram, ByVal y As TVMovieProgram) As Integer Implements System.Collections.Generic.IComparer(Of TVMovieProgram).Compare
            If x.ReferencedProgram.IsPartialRecordingSeriesPending > y.ReferencedProgram.IsPartialRecordingSeriesPending _
               Or x.ReferencedProgram.IsRecording > y.ReferencedProgram.IsRecording _
               Or x.ReferencedProgram.IsRecordingManual > y.ReferencedProgram.IsRecordingManual _
               Or x.ReferencedProgram.IsRecordingOnce > y.ReferencedProgram.IsRecordingOnce _
               Or x.ReferencedProgram.IsRecordingOncePending > y.ReferencedProgram.IsRecordingOncePending _
               Or x.ReferencedProgram.IsRecordingSeries > y.ReferencedProgram.IsRecordingSeries _
               Or x.ReferencedProgram.IsRecordingSeriesPending > y.ReferencedProgram.IsRecordingSeriesPending Then
                Return 1
            ElseIf x.ReferencedProgram.IsPartialRecordingSeriesPending < y.ReferencedProgram.IsPartialRecordingSeriesPending _
                Or x.ReferencedProgram.IsRecording < y.ReferencedProgram.IsRecording _
                Or x.ReferencedProgram.IsRecordingManual < y.ReferencedProgram.IsRecordingManual _
                Or x.ReferencedProgram.IsRecordingOnce < y.ReferencedProgram.IsRecordingOnce _
                Or x.ReferencedProgram.IsRecordingOncePending < y.ReferencedProgram.IsRecordingOncePending _
                Or x.ReferencedProgram.IsRecordingSeries < y.ReferencedProgram.IsRecordingSeries _
                Or x.ReferencedProgram.IsRecordingSeriesPending < y.ReferencedProgram.IsRecordingSeriesPending Then
                Return -1
            Else
                Return 0
            End If
        End Function
    End Class


    Public Class TVMovieProgram_SortByAction
        'Sortieren nach TvMovieBewertung DESC & StarRating DESC
        Implements IComparer(Of TVMovieProgram)
        Public Function Compare(ByVal x As TVMovieProgram, ByVal y As TVMovieProgram) As Integer Implements System.Collections.Generic.IComparer(Of TVMovieProgram).Compare
            If y.Action = x.Action AndAlso y.ReferencedProgram.StarRating = x.ReferencedProgram.StarRating Then
                Return 0
            ElseIf y.Action > x.Action Then
                Return 1
            ElseIf y.Action = x.Action AndAlso y.ReferencedProgram.StarRating > x.ReferencedProgram.StarRating Then
                Return 1
            Else
                Return -1
            End If
        End Function
    End Class
    Public Class TVMovieProgram_SortByTension
        'Sortieren nach TvMovieBewertung DESC & StarRating DESC
        Implements IComparer(Of TVMovieProgram)
        Public Function Compare(ByVal x As TVMovieProgram, ByVal y As TVMovieProgram) As Integer Implements System.Collections.Generic.IComparer(Of TVMovieProgram).Compare
            If y.Tension = x.Tension AndAlso y.ReferencedProgram.StarRating = x.ReferencedProgram.StarRating Then
                Return 0
            ElseIf y.Tension > x.Tension Then
                Return 1
            ElseIf y.Tension = x.Tension AndAlso y.ReferencedProgram.StarRating > x.ReferencedProgram.StarRating Then
                Return 1
            Else
                Return -1
            End If
        End Function
    End Class
    Public Class TVMovieProgram_SortByFun
        'Sortieren nach TvMovieBewertung DESC & StarRating DESC
        Implements IComparer(Of TVMovieProgram)
        Public Function Compare(ByVal x As TVMovieProgram, ByVal y As TVMovieProgram) As Integer Implements System.Collections.Generic.IComparer(Of TVMovieProgram).Compare
            If y.Fun = x.Fun AndAlso y.ReferencedProgram.StarRating = x.ReferencedProgram.StarRating Then
                Return 0
            ElseIf y.Fun > x.Fun Then
                Return 1
            ElseIf y.Fun = x.Fun AndAlso y.ReferencedProgram.StarRating > x.ReferencedProgram.StarRating Then
                Return 1
            Else
                Return -1
            End If
        End Function
    End Class
    Public Class TVMovieProgram_SortByErotic
        'Sortieren nach TvMovieBewertung DESC & StarRating DESC
        Implements IComparer(Of TVMovieProgram)
        Public Function Compare(ByVal x As TVMovieProgram, ByVal y As TVMovieProgram) As Integer Implements System.Collections.Generic.IComparer(Of TVMovieProgram).Compare
            If y.Erotic = x.Erotic AndAlso y.ReferencedProgram.StarRating = x.ReferencedProgram.StarRating Then
                Return 0
            ElseIf y.Erotic > x.Erotic Then
                Return 1
            ElseIf y.Erotic = x.Erotic AndAlso y.ReferencedProgram.StarRating > x.ReferencedProgram.StarRating Then
                Return 1
            Else
                Return -1
            End If
        End Function
    End Class
    Public Class TVMovieProgram_SortByFeelings
        'Sortieren nach TvMovieBewertung DESC & StarRating DESC
        Implements IComparer(Of TVMovieProgram)
        Public Function Compare(ByVal x As TVMovieProgram, ByVal y As TVMovieProgram) As Integer Implements System.Collections.Generic.IComparer(Of TVMovieProgram).Compare
            If y.Feelings = x.Feelings AndAlso y.ReferencedProgram.StarRating = x.ReferencedProgram.StarRating Then
                Return 0
            ElseIf y.Feelings > x.Feelings Then
                Return 1
            ElseIf y.Feelings = x.Feelings AndAlso y.ReferencedProgram.StarRating > x.ReferencedProgram.StarRating Then
                Return 1
            Else
                Return -1
            End If
        End Function
    End Class
    Public Class TVMovieProgram_SortByRequirement
        'Sortieren nach TvMovieBewertung DESC & StarRating DESC
        Implements IComparer(Of TVMovieProgram)
        Public Function Compare(ByVal x As TVMovieProgram, ByVal y As TVMovieProgram) As Integer Implements System.Collections.Generic.IComparer(Of TVMovieProgram).Compare
            If y.Requirement = x.Requirement AndAlso y.ReferencedProgram.StarRating = x.ReferencedProgram.StarRating Then
                Return 0
            ElseIf y.Requirement > x.Requirement Then
                Return 1
            ElseIf y.Requirement = x.Requirement AndAlso y.ReferencedProgram.StarRating > x.ReferencedProgram.StarRating Then
                Return 1
            Else
                Return -1
            End If
        End Function
    End Class
#End Region

#Region "GroupBy Methods"
    Public Class TVMovieProgram_GroupByIdSeries
        Implements IEqualityComparer(Of TVMovieProgram)
        Private _PropertyInfo As PropertyInfo
        Public Function Equals1(ByVal x As TVMovieProgram, ByVal y As TVMovieProgram) As Boolean Implements System.Collections.Generic.IEqualityComparer(Of TVMovieProgram).Equals
            If x.idSeries = y.idSeries Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetHashCode1(ByVal obj As TVMovieProgram) As Integer Implements System.Collections.Generic.IEqualityComparer(Of TVMovieProgram).GetHashCode
            Return obj.idSeries.GetHashCode()
        End Function
    End Class
    Public Class TVMovieProgram_GroupByEpisodeName
        Implements IEqualityComparer(Of TVMovieProgram)
        Private _PropertyInfo As PropertyInfo
        Public Function Equals1(ByVal x As TVMovieProgram, ByVal y As TVMovieProgram) As Boolean Implements System.Collections.Generic.IEqualityComparer(Of TVMovieProgram).Equals
            If x.ReferencedProgram.EpisodeName = y.ReferencedProgram.EpisodeName Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetHashCode1(ByVal obj As TVMovieProgram) As Integer Implements System.Collections.Generic.IEqualityComparer(Of TVMovieProgram).GetHashCode
            Return obj.ReferencedProgram.EpisodeName.GetHashCode()
        End Function
    End Class
    Public Class TVMovieProgram_GroupByTitle
        Implements IEqualityComparer(Of TVMovieProgram)
        Private _PropertyInfo As PropertyInfo
        Public Function Equals1(ByVal x As TVMovieProgram, ByVal y As TVMovieProgram) As Boolean Implements System.Collections.Generic.IEqualityComparer(Of TVMovieProgram).Equals
            If x.ReferencedProgram.Title = y.ReferencedProgram.Title Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetHashCode1(ByVal obj As TVMovieProgram) As Integer Implements System.Collections.Generic.IEqualityComparer(Of TVMovieProgram).GetHashCode
            Return obj.ReferencedProgram.Title.GetHashCode()
        End Function
    End Class

    Public Class TVMovieProgram_GroupByTitleAndEpisodeName
        Implements IEqualityComparer(Of TVMovieProgram)

        Public Function Equals2(ByVal x As TVMovieProgram, ByVal y As TVMovieProgram) As Boolean Implements System.Collections.Generic.IEqualityComparer(Of TVMovieProgram).Equals
            ' Check whether the compared objects reference the same data.
            If [Object].ReferenceEquals(x, y) Then
                Return True
            End If

            ' Check whether any of the compared objects is null.
            If [Object].ReferenceEquals(x, Nothing) OrElse [Object].ReferenceEquals(y, Nothing) Then
                Return False
            End If

            ' Check whether the cars' properties are equal.
            Return x.ReferencedProgram.Title = y.ReferencedProgram.Title AndAlso x.ReferencedProgram.EpisodeName = y.ReferencedProgram.EpisodeName
        End Function

        Public Function GetHashCode2(ByVal obj As TVMovieProgram) As Integer Implements System.Collections.Generic.IEqualityComparer(Of TVMovieProgram).GetHashCode
            ' Check whether the object is null.
            If [Object].ReferenceEquals(obj, Nothing) Then
                Return 0
            End If

            ' Get the hash code for the ModelName field if it is not null.
            Dim hashTitle As Integer = If(obj.ReferencedProgram.Title Is Nothing, 0, obj.ReferencedProgram.Title.GetHashCode())

            ' Get the hash code for the price field.
            Dim hashEpisodeName As Integer = obj.ReferencedProgram.EpisodeName.GetHashCode()

            ' Calculate the hash code for the product.
            Return hashTitle Xor hashEpisodeName
        End Function
    End Class

    Public Class TVMovieProgram_GroupByTitleEpisodeNameIdChannelStarTime
        Implements IEqualityComparer(Of TVMovieProgram)

        Public Function Equals1(ByVal x As TVMovieProgram, ByVal y As TVMovieProgram) As Boolean Implements System.Collections.Generic.IEqualityComparer(Of TVMovieProgram).Equals
            ' Check whether the compared objects reference the same data.
            If [Object].ReferenceEquals(x, y) Then
                Return True
            End If

            ' Check whether any of the compared objects is null.
            If [Object].ReferenceEquals(x, Nothing) OrElse [Object].ReferenceEquals(y, Nothing) Then
                Return False
            End If

            ' Check whether the cars' properties are equal.
            Return x.ReferencedProgram.Title = y.ReferencedProgram.Title AndAlso x.ReferencedProgram.EpisodeName = y.ReferencedProgram.EpisodeName AndAlso x.ReferencedProgram.IdChannel = y.ReferencedProgram.IdChannel AndAlso x.ReferencedProgram.StartTime = y.ReferencedProgram.StartTime
        End Function

        Public Function GetHashCode1(ByVal obj As TVMovieProgram) As Integer Implements System.Collections.Generic.IEqualityComparer(Of TVMovieProgram).GetHashCode
            ' Check whether the object is null.
            If [Object].ReferenceEquals(obj, Nothing) Then
                Return 0
            End If

            ' Get the hash code for the ModelName field if it is not null.
            Dim hashTitle As Integer = If(obj.ReferencedProgram.Title Is Nothing, 0, obj.ReferencedProgram.Title.GetHashCode())

            ' Get the hash code for the price field.
            Dim hashEpisodeName As Integer = obj.ReferencedProgram.EpisodeName.GetHashCode()

            Dim hashIdChannel As Integer = obj.ReferencedProgram.IdChannel.GetHashCode

            Dim hashStartTime As Integer = obj.ReferencedProgram.StartTime.GetHashCode

            ' Calculate the hash code for the product.
            Return hashTitle Xor hashEpisodeName Xor hashIdChannel Xor hashStartTime
        End Function
    End Class
#End Region

End Namespace
