Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub btnGotoCalc_Click(sender As Object, e As EventArgs) Handles btnGotoCalc.Click
        Response.Redirect("ArbeitscalcMain.aspx")
    End Sub

    Protected Sub btnAbout_Click(sender As Object, e As EventArgs) Handles btnAbout.Click
        Response.Redirect("About.aspx")
    End Sub

    Protected Sub btnLegal_Click(sender As Object, e As EventArgs) Handles btnLegal.Click
        Response.Redirect("Rechtliches.aspx")
    End Sub
    Protected Sub btnTutorial_Click(sender As Object, e As EventArgs) Handles btnTutorial.Click
        Response.Redirect("Tutorial.aspx")
    End Sub
    Protected Sub btnHinweise_Click(sender As Object, e As EventArgs) Handles btnHinweise.Click
        Response.Redirect("Buchungshinweise.aspx")
    End Sub

End Class
