<%@ Page Language="VB" AutoEventWireup="true" CodeBehind="Tutorial.aspx.vb" Inherits="Arbeitscalc_Web.Tutorial" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tutorial – CSV-Export</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f7f7f7;
            margin: 0;
            padding: 0;
        }
        .container {
            max-width: 900px;
            margin: 40px auto;
            background: #fff;
            padding: 32px;
            border-radius: 8px;
            box-shadow: 0 2px 10px #bbb;
        }
        h1 {
            margin-top: 0;
        }
        h2 {
            margin-top: 32px;
            font-size: 20px;
        }
        p {
            font-size: 15px;
        }
        .tutorial-image {
            display: block;
            margin: 16px auto;
            max-width: 100%;
            width: 100%;
            max-height: 600px;
            object-fit: contain;
        }
        @media (min-width: 600px) {
            .tutorial-image {
                width: 450px;
            }
        }
        .back-btn {
            margin-top: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>CSV-Export aus der pds App – Anleitung</h1>

            <h2>Schritt 1: Zeit-Menü öffnen</h2>
            <p>Tippe unten im Menü auf „Zeit“.</p>
            <img src="images/tutorial1.png" alt="Schritt 1" class="tutorial-image" />

            <h2>Schritt 2: Protokoll öffnen</h2>
            <p>Tippe auf „Protokoll“ im Bereich „Zeit“.</p>
            <img src="images/tutorial2.png" alt="Schritt 2" class="tutorial-image" />

            <h2>Schritt 3: Export starten</h2>
            <p>Tippe oben rechts auf das Export-Symbol.</p>
            <img src="images/tutorial3.png" alt="Schritt 3" class="tutorial-image" />

            <h2>Schritt 4: Zeitraum wählen und CSV exportieren</h2>
            <p>Wähle den Zeitraum vom 1. bis zum letzten Tag des Monats und tippe auf „CSV-Datei exportieren“.</p>
            <img src="images/tutorial4.png" alt="Schritt 4" class="tutorial-image" />

            <h2>Schritt 5: CSV-Datei im Web laden</h2>
            <p>Wähle zuerst eine CSV-Datei über <strong>„Choose File“</strong> aus und klicke dann auf <strong>„CSV laden“</strong>, um die Daten zu importieren.</p>
            <img src="images/tutorial5.1.png" alt="Schritt 5" class="tutorial-image" />

            <asp:Button ID="btnBack" runat="server" Text="Zurück zur Startseite" CssClass="back-btn" />
        </div>
    </form>
</body>
</html>
