Imports System.Reflection
Imports System.Data.OleDb
Imports MediaPortal.Configuration
Imports MediaPortal.Profile
Imports Gentle.Common
Imports Gentle.Framework
Imports TvDatabase


Public Class ClickfinderDB
#Region "Variablen"
    Private Shared _Table As DataTable
    Private Shared _TvServerTable As DataTable
    Private Shared _Index As Integer
    Private Shared _IndexColumn As Integer
    Private Shared _ClickfinderDataBasePath As String

#End Region

#Region "Properties"
    Public ReadOnly Property Count() As Integer
        Get
            Return _Table.Rows.Count
        End Get
    End Property
    Public ReadOnly Property Datatable() As DataTable
        Get
            Return _Table
        End Get
    End Property

    'Get DBFields over Index
    Private _Item As New DataBaseItem
    Default Public ReadOnly Property Item(ByVal Index As String) As DataBaseItem
        Get
            _Index = Index
            Return _Item
        End Get
    End Property
    Public Class DataBaseItem





        'SubProperties for _item

        Public ReadOnly Property TvServer_idchannel() As Integer
            Get

                Try

                    Dim _result As IList = TvServerMapping(_Table.Rows(_Index).Item("SenderKennung"))

                    If _result.Count = 1 Then
                        Dim _Mapping As TvMovieMapping = _result.Item(0)
                        Dim _channel As Channel = Channel.Retrieve(_Mapping.IdChannel)

                        Return _channel.IdChannel

                    ElseIf _result.Count > 1 Then
                        Dim _idchannel As String = ""

                        For i = 0 To _result.Count - 1
                            Dim _Mapping As TvMovieMapping = _result.Item(i)
                            Dim _channel As Channel = Channel.Retrieve(_Mapping.IdChannel)

                            If InStr(_channel.DisplayName, "HD") Then
                                _idchannel = _channel.IdChannel
                                Exit For
                            Else
                                _idchannel = _channel.IdChannel
                            End If

                        Next

                        Return _idchannel
                    Else
                        Return 0
                    End If


                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Get
        End Property

        Public ReadOnly Property TvServer_displayName() As String
            Get
                Dim _Result As IList = TvServerMapping(_Table.Rows(_Index).Item("SenderKennung"))

                If _Result.Count = 1 Then
                    Dim _Mapping As TvMovieMapping = _Result.Item(0)
                    Dim _channel As Channel = Channel.Retrieve(_Mapping.IdChannel)

                    Return _channel.DisplayName

                ElseIf _Result.Count > 1 Then
                    Dim _displayName As String = ""

                    For i = 0 To _Result.Count - 1
                        Dim _Mapping As TvMovieMapping = _Result.Item(i)
                        Dim _channel As Channel = Channel.Retrieve(_Mapping.IdChannel)

                        If InStr(_channel.DisplayName, "HD") Then
                            _displayName = _channel.DisplayName
                            Exit For
                        Else
                            _displayName = _channel.DisplayName
                        End If

                    Next

                    Return _displayName

                Else
                    Return ""
                End If


            End Get
        End Property

        Public ReadOnly Property Titel() As String
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("Titel")) Then
                    Return _Table.Rows(_Index).Item("Titel")
                Else
                    Return ""
                End If
            End Get
        End Property
        Public ReadOnly Property SendungID() As Integer
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("SendungID")) Then
                    Return _Table.Rows(_Index).Item("SendungID")
                Else
                    Return 0
                End If

            End Get
        End Property
        Public ReadOnly Property Pos() As Integer
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("Sendungen.Pos")) Then
                    Return _Table.Rows(_Index).Item("Sendungen.Pos")
                Else
                    Return 0
                End If

            End Get
        End Property
        Public ReadOnly Property Beginn() As Date
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("Beginn")) Then
                    Return _Table.Rows(_Index).Item("Beginn")
                Else
                    Return Nothing
                End If
            End Get
        End Property
        Public ReadOnly Property Ende() As Date
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("Ende")) Then
                    Return _Table.Rows(_Index).Item("Ende")
                Else
                    Return Nothing
                End If
            End Get
        End Property
        Public ReadOnly Property SenderKennung() As String
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("SenderKennung")) Then
                    Return _Table.Rows(_Index).Item("SenderKennung")
                Else
                    Return ""
                End If
            End Get
        End Property
        Public ReadOnly Property Genre() As String
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("Genre")) Then
                    Return _Table.Rows(_Index).Item("Genre")
                Else
                    Return ""
                End If
            End Get
        End Property
        Public ReadOnly Property Bewertung() As Integer
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("Bewertung")) Then
                    Return _Table.Rows(_Index).Item("Bewertung")
                Else
                    Return 0
                End If
            End Get
        End Property
        Public ReadOnly Property Bewertungen() As String
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("Bewertungen")) Then
                    Return Replace(_Table.Rows(_Index).Item("Bewertungen"), ";", " ")
                Else
                    Return ""
                End If
            End Get
        End Property
        Public ReadOnly Property Kurzkritik() As String
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("Kurzkritik")) Then
                    Return _Table.Rows(_Index).Item("Kurzkritik")
                Else
                    Return ""
                End If
            End Get
        End Property
        Public ReadOnly Property Bilddateiname() As String
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("Bilddateiname")) Then
                    Return _ClickfinderDataBasePath & "\Hyperlinks\" & _Table.Rows(_Index).Item("Bilddateiname")
                Else
                    Return ""
                End If
            End Get
        End Property
        Public ReadOnly Property Originaltitel() As String
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("Originaltitel")) Then
                    Return _Table.Rows(_Index).Item("Originaltitel")
                Else
                    Return ""
                End If
            End Get
        End Property
        Public ReadOnly Property Rating() As Integer
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("Rating")) Then
                    Return _Table.Rows(_Index).Item("Rating")
                Else
                    Return 0
                End If
            End Get
        End Property
        Public ReadOnly Property KzLive() As Boolean
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("KzLive")) Then
                    Return _Table.Rows(_Index).Item("KzLive")
                Else
                    Return False
                End If
            End Get
        End Property
        Public ReadOnly Property KzWiederholung() As Boolean
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("KzWiederholung")) Then
                    Return _Table.Rows(_Index).Item("KzWiederholung")
                Else
                    Return False
                End If
            End Get
        End Property
        Public ReadOnly Property KzBilddateiHeruntergeladen() As Boolean
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("KzBilddateiHeruntergeladen")) Then
                    Return _Table.Rows(_Index).Item("KzBilddateiHeruntergeladen")
                Else
                    Return False
                End If
            End Get
        End Property
        Public ReadOnly Property Dauer() As Integer
            Get
                Return CInt(Replace(_Table.Rows(_Index).Item("Dauer"), " min", ""))
                If Not IsDBNull(_Table.Rows(_Index).Item("Dauer")) Then
                    Return CInt(Replace(_Table.Rows(_Index).Item("Dauer"), " min", ""))
                Else
                    Return 0
                End If
            End Get
        End Property
        Public ReadOnly Property Beschreibung() As String
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("Beschreibung")) Then
                    Return Replace(_Table.Rows(_Index).Item("Beschreibung"), ";", " ")
                Else
                    Return ""
                End If
            End Get
        End Property
        Public ReadOnly Property KurzBeschreibung() As String
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("KurzBeschreibung")) Then
                    Return Replace(_Table.Rows(_Index).Item("KurzBeschreibung"), ";", " ")
                Else
                    Return ""
                End If
            End Get
        End Property
        Public ReadOnly Property Darsteller() As String
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("Darsteller")) Then
                    Return Replace(_Table.Rows(_Index).Item("Darsteller"), ";", " ")
                Else
                    Return ""
                End If
            End Get
        End Property
        Public ReadOnly Property Regie() As String
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("Regie")) Then
                    Return Replace(_Table.Rows(_Index).Item("Regie"), ";", " ")
                Else
                    Return ""
                End If
            End Get
        End Property
        Public ReadOnly Property Herstellungsland() As String
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("Herstellungsland")) Then
                    Return Replace(_Table.Rows(_Index).Item("Herstellungsland"), ";", " ")
                Else
                    Return ""
                End If
            End Get
        End Property
        Public ReadOnly Property Herstellungsjahr() As String
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("Herstellungsjahr")) Then
                    Return Replace(_Table.Rows(_Index).Item("Herstellungsjahr"), ";", " ")
                Else
                    Return ""
                End If
            End Get
        End Property

    End Class


    'Get DBColumns over Index
    Private _Columns As New DataBaseColumns
    Public ReadOnly Property Columns(Optional ByVal Index As String = Nothing) As DataBaseColumns
        Get
            _IndexColumn = Index
            Return _Columns
        End Get
    End Property
    Public Class DataBaseColumns
        'SubProperties for _Columns
        Public ReadOnly Property Name() As String
            Get
                Return _Table.Columns(_IndexColumn).ColumnName
            End Get
        End Property
        Public ReadOnly Property Count() As Integer
            Get
                Return _Table.Columns.Count
            End Get
        End Property
    End Class

#End Region

    Public Sub New(ByVal SQLString As String)
        'Fill Dataset with SQLString Query
        Dim DataAdapter As OleDb.OleDbDataAdapter
        Dim Data As New DataSet
        _ClickfinderDataBasePath = MPSettingRead("config", "ClickfinderPath")
        Try
            Dim Con As New OleDb.OleDbConnection
            Dim Cmd As New OleDb.OleDbCommand

            Con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" & "Data Source=" & _ClickfinderDataBasePath & "\tvdaten.mdb"
            Con.Open()

            DataAdapter = New OleDbDataAdapter(SQLString, Con)
            DataAdapter.Fill(Data, "ClickfinderDB")

            DataAdapter.Dispose()
            Con.Close()

            _Table = Data.Tables("ClickfinderDB")

            'PrimärSchlüssel für Datatable festlegen, damit gesucht werden kann.
            _Table.PrimaryKey = New DataColumn() {_Table.Columns("Sendungen.Pos")}

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Shared Function TvServerMapping(ByVal SenderKennung As String) As IList
        'TvMovieMapping Table nach idchannel durchsuchen
        Dim sb As New SqlBuilder(StatementType.[Select], GetType(TvMovieMapping))
        sb.AddConstraint([Operator].Equals, "stationName", SenderKennung)
        'sb.AddOrderByField(False, "stationName")
        Dim stmt As SqlStatement = sb.GetStatement(True)
        Dim _Result As IList = ObjectFactory.GetCollection(GetType(TvMovieMapping), stmt.Execute())

        Return _Result
    End Function
    Private Function MPSettingRead(ByVal section As String, ByVal entry As String) As String
        Using xmlReader As New Settings(Config.GetFile(Config.Dir.Config, "ClickfinderPGConfig.xml"))
            MPSettingRead = xmlReader.GetValue(section, entry)
        End Using
    End Function

End Class
