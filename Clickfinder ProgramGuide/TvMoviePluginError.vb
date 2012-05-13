Imports System
Imports System.IO

Imports MediaPortal.Profile
Imports MediaPortal.Configuration
Imports MediaPortal.GUI.Library
Imports MediaPortal.Util
Imports MediaPortal.Utils

Imports Gentle.Framework
Imports System.Drawing
Imports System.Threading
Imports System.Windows.Forms
Imports TvDatabase


Public Class TvMoviePluginError
#Region "Members"
    Private Shared _layer As New TvBusinessLayer
#End Region

    Private Sub TvMoviePluginError_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        If CBool(_layer.GetSetting("TvMovieEnabled", "false").Value) = False Then
            Label1.Visible = True
            LinkClickfinderPG.Visible = True
            LinkLabel1.Visible = True

            LinkLabel2.Visible = False
            LinkLabel3.Visible = False
            Label2.Visible = False

        Else
            Label1.Visible = False
            LinkClickfinderPG.Visible = False
            LinkLabel1.Visible = False

            LinkLabel2.Visible = True
            LinkLabel3.Visible = True
            Label2.Visible = True


        End If
    End Sub

    Private Sub ButtonOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOK.Click
        Me.Close()
    End Sub

    Private Sub LinkClickfinderPG_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkClickfinderPG.LinkClicked
        Process.Start("http://code.google.com/p/tv-movie-epg-import-plusplus/downloads/list")
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("http://code.google.com/p/tv-movie-epg-import-plusplus/w/list")
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Process.Start("http://code.google.com/p/tv-movie-epg-import-plusplus/downloads/list")
    End Sub

    Private Sub LinkLabel3_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Process.Start("http://code.google.com/p/tv-movie-epg-import-plusplus/w/list")
    End Sub
End Class