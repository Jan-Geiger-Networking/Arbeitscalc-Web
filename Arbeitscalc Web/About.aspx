<%@ Page Language="VB" AutoEventWireup="true" CodeBehind="About.aspx.vb" Inherits="Arbeitscalc_Web.About" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Über Arbeitscalc</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f7f7f7;
            margin: 0;
            padding: 0;
        }
        .container {
            max-width: 700px;
            margin: 50px auto;
            background: #fff;
            padding: 32px;
            border-radius: 8px;
            box-shadow: 0 2px 10px #bbb;
            text-align: center;
        }
        .logo {
            max-height: 60px;
            margin-bottom: 20px;
        }
        h1 {
            margin-top: 0;
        }
        p {
            font-size: 16px;
            margin: 12px 0;
        }
        .back-btn {
            margin-top: 20px;
        }
        a {
            color: #2869bf;
            text-decoration: none;
        }
        a:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <img src="images/jgn-logo.png" alt="JGN Logo" class="logo" />
            <h1>Über Arbeitscalc</h1>
            <p><strong>Arbeitscalc Web-Version:</strong> <asp:Label ID="lblVersion" runat="server" Text="..." /></p>
            <p>
                Der Arbeitscalc dient zur einfachen und übersichtlichen Berechnung von Arbeits- und Fahrzeiten
                auf Basis strukturierter Textdateien.
            </p>
            <p>
                Entwickelt von Jan Geiger. Mehr Informationen finden Sie auf der
                <a href="https://jgnet.eu/arbeitscalc-support" target="_blank">Support-Seite</a>.
            </p>
            <asp:Button ID="btnBack" runat="server" Text="Zurück" CssClass="back-btn" />
        </div>
    </form>
</body>
</html>


