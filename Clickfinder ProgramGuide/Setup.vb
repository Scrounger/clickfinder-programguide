﻿Imports System
Imports System.IO

Imports MediaPortal.Profile
Imports MediaPortal.Configuration
Imports MediaPortal.GUI.Library
Imports MediaPortal.Util
Imports MediaPortal.Utils
Imports MySql.Data
Imports MySql.Data.MySqlClient

Imports Gentle.Framework



Public Class Setup

    Private Sub Setup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Rating As Integer
        Dim i As Integer
        Dim idGroup As String
        Dim ChannelName As String

        ChannelName = "Bitte wählen..."

        Me.tfClickfinderPath.Text = MPSettingRead("config", "ClickfinderPath")
        Rating = CInt(MPSettingRead("config", "ClickfinderRating"))
        idGroup = MPSettingRead("config", "ChannelGroupID")


        Select Case Rating
            Case Is = 0
                Me.cbRating.Text = Me.cbRating.Items.Item(0)
            Case Is = 1
                Me.cbRating.Text = Me.cbRating.Items.Item(1)
            Case Is = 2
                Me.cbRating.Text = Me.cbRating.Items.Item(2)
            Case Is = 3
                Me.cbRating.Text = Me.cbRating.Items.Item(3)
        End Select

        ReadTvServerDB("Select * from channelgroup")
        While TvServerData.Read

            Me.CBChannelGroup.Items.Add(TvServerData.Item("groupName"))

            If idGroup = TvServerData.Item("idGroup") Then
                ChannelName = TvServerData.Item("groupName")
            End If

        End While

        CloseTvServerDB()

        For i = 0 To CBChannelGroup.Items.Count - 1
            If InStr(CBChannelGroup.Items.Item(i), ChannelName) Then
                CBChannelGroup.Text = CBChannelGroup.Items.Item(i)
            End If

        Next

        For i = 0 To 24
            CBPrimeTimeHour.Items.Add(Format(i, "00"))
            CBLateTimeHour.Items.Add(Format(i, "00"))
        Next
        For i = 0 To 59
            CBPrimeTimeMinute.Items.Add(Format(i, "00"))
            CBLateTimeMinute.Items.Add(Format(i, "00"))
            CBDelayNow.Items.Add(Format(i, "00"))
        Next


        For i = 0 To CBPrimeTimeHour.Items.Count - 1
            If CBPrimeTimeHour.Items.Item(i) = MPSettingRead("config", "PrimeTimeHour") Then CBPrimeTimeHour.Text = MPSettingRead("config", "PrimeTimeHour")
        Next

        For i = 0 To CBPrimeTimeMinute.Items.Count - 1
            If CBPrimeTimeMinute.Items.Item(i) = MPSettingRead("config", "PrimeTimeMinute") Then CBPrimeTimeMinute.Text = MPSettingRead("config", "PrimeTimeMinute")
        Next

        For i = 0 To CBLateTimeHour.Items.Count - 1
            If CBLateTimeHour.Items.Item(i) = MPSettingRead("config", "LateTimeHour") Then CBLateTimeHour.Text = MPSettingRead("config", "LateTimeHour")
        Next
        For i = 0 To CBLateTimeMinute.Items.Count - 1
            If CBLateTimeMinute.Items.Item(i) = MPSettingRead("config", "LateTimeMinute") Then CBLateTimeMinute.Text = MPSettingRead("config", "LateTimeMinute")
        Next
        For i = 0 To CBDelayNow.Items.Count - 1
            If CBDelayNow.Items.Item(i) = MPSettingRead("config", "DelayNow") Then CBDelayNow.Text = MPSettingRead("config", "DelayNow")
        Next



    End Sub

    Public Sub MPSettingsWrite(ByVal section As String, ByVal entry As String, ByVal NewAttribute As String)
        Using xmlReader As New Settings(Config.GetFile(Config.Dir.Config, "ClickfinderPGConfig.xml"))
            xmlReader.SetValue(section, entry, NewAttribute)
        End Using

    End Sub

    Public Function MPSettingRead(ByVal section As String, ByVal entry As String) As String
        Using xmlReader As New Settings(Config.GetFile(Config.Dir.Config, "ClickfinderPGConfig.xml"))
            MPSettingRead = xmlReader.GetValue(section, entry)
        End Using
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        MPSettingsWrite("config", "ClickfinderPath", tfClickfinderPath.Text.ToString)
        MPSettingsWrite("config", "ClickfinderRating", Me.cbRating.Text)
        MPSettingsWrite("config", "PrimeTimeHour", Me.CBPrimeTimeHour.Text)
        MPSettingsWrite("config", "PrimeTimeMinute", Me.CBPrimeTimeMinute.Text)
        MPSettingsWrite("config", "LateTimeHour", Me.CBLateTimeHour.Text)
        MPSettingsWrite("config", "LateTimeMinute", Me.CBLateTimeMinute.Text)
        MPSettingsWrite("config", "DelayNow", Me.CBDelayNow.Text)

        ReadTvServerDB("Select * from channelgroup Where groupName = '" & CBChannelGroup.Text & "'")
        While TvServerData.Read
            MPSettingsWrite("config", "ChannelGroupID", TvServerData.Item("idGroup"))
        End While
        CloseTvServerDB()

        Me.Close()

    End Sub

#Region "TV Server DB"
    Public ConTvServerDBRead As New MySqlConnection
    Public CmdTvServerDBRead As New MySqlCommand
    Public TvServerData As MySqlDataReader

    Public Sub ReadTvServerDB(ByVal SQLString As String)

        Try

            ConTvServerDBRead.ConnectionString = LeftCut(Replace(Gentle.Framework.GentleSettings.DefaultProviderConnectionString, " ", ""), InStr(Gentle.Framework.GentleSettings.DefaultProviderConnectionString, "charset=utf8") - 3)
            ConTvServerDBRead.Open()


            CmdTvServerDBRead = ConTvServerDBRead.CreateCommand
            CmdTvServerDBRead.CommandText = SQLString

            TvServerData = CmdTvServerDBRead.ExecuteReader


        Catch ex As Exception

            Log.Debug("Clickfinder ProgramGuide: (Config: TvServer DB, read) " & ex.Message)
            MsgBox("Clickfinder ProgramGuide: (Config: TvServer DB, read) " & ex.Message)

            CmdTvServerDBRead.Dispose()
            ConTvServerDBRead.Close()
        End Try


    End Sub
    Public Sub CloseTvServerDB()

        Try
            CmdTvServerDBRead.Dispose()
            ConTvServerDBRead.Close()
        Catch ex As Exception
            Log.Debug("Clickfinder ProgramGuide: (Config: TvServer DB, close) " & ex.Message)
            MsgBox("Clickfinder ProgramGuide: (Config: TvServer DB, close) " & ex.Message)
        End Try

    End Sub

    Public Function LeftCut(ByVal sText As String, _
  ByVal nLen As Integer) As String

        If nLen > sText.Length Then nLen = sText.Length
        Return (sText.Substring(0, nLen))
    End Function
#End Region


End Class