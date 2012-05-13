Imports System.Reflection
Imports System.Data.OleDb
Imports MediaPortal.UserInterface.Controls
Imports MediaPortal.Configuration
Imports MediaPortal.Common
Imports Gentle.Common
Imports Gentle.Framework
Imports TvDatabase


Public Class ClickfinderDB
#Region "Members"
    Private Shared _Table As DataTable
    Private Shared _TvServerTable As DataTable
    Private Shared _Index As Integer
    Private Shared _IndexColumn As Integer
    Private Shared _ClickfinderDataBaseFolder As String
    Private Shared _layer As New TvBusinessLayer
#End Region

#Region "Properties"

    Public Shared ReadOnly Property DatabasePath() As String
        Get


            Return _layer.GetSetting("ClickfinderDatabasePath").Value
        End Get
    End Property
    Public Shared ReadOnly Property ImagePath() As String
        Get

            Return _layer.GetSetting("ClickfinderImagePath").Value
        End Get
    End Property
    Public ReadOnly Property Count() As Integer
        Get
            Return _Table.Rows.Count
        End Get
    End Property
    Public ReadOnly Property DatensatzTabelle() As DataTable
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
        Public ReadOnly Property SenderLogo() As String
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("Zeichen")) Then
                    Return _Table.Rows(_Index).Item("Zeichen")
                Else
                    Return ""
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
        Public ReadOnly Property SenderBezeichnung() As String
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("Bezeichnung")) Then
                    Return _Table.Rows(_Index).Item("Bezeichnung")
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
        Public ReadOnly Property SenderID() As Integer
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("ID")) Then
                    Return _Table.Rows(_Index).Item("ID")
                Else
                    Return 0
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
                    Return _Table.Rows(_Index).Item("Bilddateiname")
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
        Public ReadOnly Property Dolby() As Boolean
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("KzDolby")) Or Not IsDBNull(_Table.Rows(_Index).Item("KzDolbyDigital")) Or Not IsDBNull(_Table.Rows(_Index).Item("KzDolbySurround")) Then
                    If CBool(_Table.Rows(_Index).Item("KzDolby")) = True Or CBool(_Table.Rows(_Index).Item("KzDolbyDigital")) = True Or CBool(_Table.Rows(_Index).Item("KzDolbySurround")) = True Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            End Get
        End Property
        Public ReadOnly Property KzHDTV() As Boolean
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("KzHDTV")) Then
                    Return CBool(_Table.Rows(_Index).Item("KzHDTV"))
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
                    Return _Table.Rows(_Index).Item("Darsteller")
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
        Public ReadOnly Property Bewertungen() As String
            Get
                If Not IsDBNull(_Table.Rows(_Index).Item("Bewertungen")) Then
                    Return CStr(_Table.Rows(_Index).Item("Bewertungen"))
                Else
                    Return ""
                End If
            End Get
        End Property
    End Class

    Public ReadOnly Property DataTable() As DataTable
        Get
            Return _Table
        End Get
    End Property

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

        Dim layer As New TvBusinessLayer()

        _ClickfinderDataBaseFolder = System.IO.Path.GetDirectoryName(DatabasePath)

        Try

            Dim Con As New OleDb.OleDbConnection
            Dim Cmd As New OleDb.OleDbCommand

            Con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" & "Data Source=" & ClickfinderDB.DatabasePath
            Con.Open()

            DataAdapter = New OleDbDataAdapter(SQLString, Con)
            DataAdapter.Fill(Data, "ClickfinderDB")

            DataAdapter.Dispose()
            Con.Close()

            _Table = Data.Tables("ClickfinderDB")

            'PrimärSchlüssel für Datatable festlegen, damit gesucht werden kann.
            _Table.PrimaryKey = New DataColumn() {_Table.Columns("Sendungen.Pos")}

        Catch ex As Exception
            MyLog.Error("[ClickfinderDB]: Loop exception err:" & ex.Message & " stack:" & ex.StackTrace)
        End Try

    End Sub

    Public Sub New(ByVal _TvServerProgram As Program, Optional ByVal InnerJoinSendungenDetails As Boolean = False)
        Dim DataAdapter As OleDb.OleDbDataAdapter
        Dim Data As New DataSet
        Dim _SenderKennung As String = String.Empty
        Dim layer As New TvBusinessLayer()
        Dim SQLString As String = String.Empty
        Try
            '

            Dim _channel As New Gentle.Framework.Key(False, "idChannel", _TvServerProgram.ReferencedChannel.IdChannel)
            _SenderKennung = TvMovieMapping.Retrieve(_channel).StationName

        Catch ex As Exception
            MyLog.Warn("[ClickfinderDB]: Channel not mapped in TvMovieMapping: {0}", _TvServerProgram.ReferencedChannel.DisplayName)
            _SenderKennung = String.Empty
        End Try

        If InnerJoinSendungenDetails = False Then
            SQLString = "Select * FROM Sendungen"
        Else
            SQLString = "Select * FROM Sendungen INNER JOIN SendungenDetails ON Sendungen.Pos = SendungenDetails.Pos"
        End If

        SQLString = SQLString & _
        " WHERE SenderKennung = '" & _SenderKennung & "'" & _
        " AND (Beginn BETWEEN " & DateToAccessSQLstring(_TvServerProgram.StartTime.AddMinutes(-1)) & " AND " & DateToAccessSQLstring(_TvServerProgram.StartTime.AddMinutes(1)) & ")" & _
        " AND Titel = '" & Replace(Replace(Replace(_TvServerProgram.Title, "'", "''"), " (LIVE)", ""), " (Wdh.)", "") & "'"

        _ClickfinderDataBaseFolder = System.IO.Path.GetDirectoryName(DatabasePath)

        Try

            Dim Con As New OleDb.OleDbConnection
            Dim Cmd As New OleDb.OleDbCommand

            Con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" & "Data Source=" & ClickfinderDB.DatabasePath
            Con.Open()

            DataAdapter = New OleDbDataAdapter(SQLString, Con)
            DataAdapter.Fill(Data, "ClickfinderDB")

            DataAdapter.Dispose()
            Con.Close()

            _Table = Data.Tables("ClickfinderDB")

            'PrimärSchlüssel für Datatable festlegen, damit gesucht werden kann.
            _Table.PrimaryKey = New DataColumn() {_Table.Columns("Sendungen.Pos")}

        Catch ex As Exception
            MyLog.Error("[ClickfinderDB]: Loop exception err:" & ex.Message & " stack:" & ex.StackTrace)
        End Try
    End Sub

    Private Function DateToAccessSQLstring(ByVal Datum As Date) As String
        DateToAccessSQLstring = "#" & Datum.Year & "-" & Format(Datum.Month, "00") & "-" & Format(Datum.Day, "00") & " " & Format(Datum.Hour, "00") & ":" & Format(Datum.Minute, "00") & ":" & Format("00", "00") & "#"
    End Function

    Shared Function TvServerMapping(ByVal SenderKennung As String) As IList
        'TvMovieMapping Table nach idchannel durchsuchen
        Dim sb As New SqlBuilder(StatementType.[Select], GetType(TvMovieMapping))
        sb.AddConstraint([Operator].Equals, "stationName", SenderKennung)
        'sb.AddOrderByField(False, "stationName")
        Dim stmt As SqlStatement = sb.GetStatement(True)
        Dim _Result As IList = ObjectFactory.GetCollection(GetType(TvMovieMapping), stmt.Execute())

        Return _Result
    End Function
    'Private Function MPSettingRead(ByVal section As String, ByVal entry As String) As String
    '    Using xmlReader As New Settings(Config.GetFile(Config.Dir.Config, "ClickfinderPGConfig.xml"))
    '        MPSettingRead = xmlReader.GetValue(section, entry)
    '    End Using
    'End Function

End Class
