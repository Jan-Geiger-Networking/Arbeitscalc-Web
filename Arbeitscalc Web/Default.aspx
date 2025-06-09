<%@ Page Language="VB" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="Arbeitscalc_Web._Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Willkommen zum Arbeitscalc</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background: #f7f7f7;
            margin: 0;
            padding: 0;
        }

        .centerbox {
            margin: 70px auto;
            background: #fff;
            border-radius: 8px;
            box-shadow: 0 2px 10px #bbb;
            max-width: 460px;
            padding: 32px;
        }

        h1 {
            margin-top: 0;
        }

        .disclaimer {
            color: #a00;
            font-size: 15px;
            margin: 12px 0 28px 0;
        }

        .goto-btn {
             background: #00dd44;
             color: #fff;
             border: none;
             font-size: 18px;
             border-radius: 5px;
             padding: 12px 32px;
             cursor: pointer;
             margin-top: 12px;
             width: 100%;
             transition: background 0.2s;
}

        .goto-btn:hover {
             background: #009933;
}

        .btn-grid {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 10px;
            margin-top: 20px;
        }

        .info-btn {
            width: 100%;
            background: #666;
            color: #fff;
            border: none;
            font-size: 15px;
            border-radius: 5px;
            padding: 10px;
            cursor: pointer;
            transition: background 0.2s;
        }

        .info-btn:hover {
            background: #444;
        }

        .footer {
            text-align: center;
            margin-top: 40px;
            padding: 12px;
            font-size: 13px;
            color: #888;
        }

        .footer img {
            max-height: 40px;
            margin-top: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="centerbox">
            <h1>Willkommen zum Arbeitscalc</h1>
            <div class="disclaimer">
                Hinweis: Dieses Web-Tool befindet sich noch in Entwicklung.<br />
                Es kann noch zu Fehlern oder Abweichungen kommen.<br />
                Für Rückfragen/Fehlermeldungen siehe
                <a href="https://jgnet.eu/arbeitscalc-support" target="_blank">Support</a>.
            </div>

            <asp:Button ID="btnGotoCalc" runat="server" Text="Zum Arbeitszeit-Calculator →" CssClass="goto-btn" />

            <div class="btn-grid">
                <asp:Button ID="btnAbout" runat="server" Text="Über Arbeitscalc" CssClass="info-btn" />
                <asp:Button ID="btnSupport" runat="server" Text="Support" CssClass="info-btn" PostBackUrl="https://jgnet.eu/arbeitscalc-support" />
                <asp:Button ID="btnLegal" runat="server" Text="Rechtliches" CssClass="info-btn" />
                <asp:Button ID="btnTutorial" runat="server" Text="Tutorial anzeigen" CssClass="info-btn" />
                <asp:Button ID="btnHinweise" runat="server" Text="Buchungshinweise" CssClass="info-btn" />
                <asp:Button ID="btnDatenschutz" runat="server" Text="Datenschutz" CssClass="info-btn" />
            </div>
        </div>

        <div class="footer">
            <div>Ein Projekt von Jan Geiger Networking</div>
            <img src="images/jgn-logo.png" alt="JGN Logo" />
        </div>
    </form>
</body>
</html>
