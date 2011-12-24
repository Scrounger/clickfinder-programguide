Imports System.Reflection
Imports System.Data.OleDb
Imports MediaPortal.Configuration
Imports MediaPortal.Profile


Public Class ClickfinderDB
#Region "Variablen"
    Public Shared _Table As DataTable
    Shared _Index As Integer
    Shared _IndexColumn As Integer
    Shared _ClickfinderDataBasePath As String

#End Region

#Region "Properties"
    Public ReadOnly Property Count() As Integer
        Get
            Return _Table.Rows.Count
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

        Catch
            MsgBox("Can not open connection ! ")
        End Try
    End Sub
    Private Function MPSettingRead(ByVal section As String, ByVal entry As String) As String
        Using xmlReader As New Settings(Config.GetFile(Config.Dir.Config, "ClickfinderPGConfig.xml"))
            MPSettingRead = xmlReader.GetValue(section, entry)
        End Using
    End Function

End Class
