<%@ Page Language="VB" AutoEventWireup="true" CodeBehind="Datenschutz.aspx.vb" Inherits="Arbeitscalc_Web.Datenschutz" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Datenschutzhinweis</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f7f7f7;
            margin: 0;
            padding: 0;
        }
        .container {
            max-width: 800px;
            margin: 50px auto;
            background: #fff;
            padding: 32px;
            border-radius: 8px;
            box-shadow: 0 2px 10px #bbb;
        }
        h2 {
            margin-top: 0;
        }
        p {
            font-size: 15px;
            margin: 12px 0;
        }
        .back-btn {
            margin-top: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Datenschutzhinweis</h2>
            <p>
                Diese Anwendung wird von <strong>Jan Geiger Networking</strong> im deutschen Rechenzentrum <strong>JGN-DC1 (Standort DE-NRW-BSU-1-A)</strong> gehostet.
                Die Verbindung erfolgt ausschließlich über verschlüsselte HTTPS-Tunnel (Cloudflare Zero Trust), direkte HTTP-Verbindungen sind deaktiviert.
            </p>
            <p>
                Es werden keinerlei personenbezogene Daten gespeichert oder verarbeitet.
                Die Nutzung erfolgt vollständig anonymisiert und ohne Einsatz von Cookies, Analysewerkzeugen oder Trackingdiensten.
            </p>
            <p>
                Für Rückfragen zum Datenschutz erreichst du uns unter
                <a href="mailto:support@bsbnet.eu">support@bsbnet.eu</a>.
            </p>

            <asp:Button ID="btnBack" runat="server" Text="Zurück" CssClass="back-btn" />
        </div>
    </form>
</body>
</html>