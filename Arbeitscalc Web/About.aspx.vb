Imports System.Reflection

Public Class About
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim version As String = Assembly.GetExecutingAssembly().GetName().Version.ToString()
            lblVersion.Text = version
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("Default.aspx")
    End Sub
End Class


