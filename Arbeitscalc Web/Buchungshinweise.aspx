<%@ Page Language="VB" AutoEventWireup="true" CodeBehind="Buchungshinweise.aspx.vb" Inherits="Arbeitscalc_Web.Buchungshinweise" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Buchungshinweise</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f7f7f7;
            margin: 0;
            padding: 0;
        }
        .container {
            max-width: 1000px;
            margin: 40px auto;
            background: #fff;
            padding: 32px;
            border-radius: 8px;
            box-shadow: 0 2px 10px #bbb;
        }
        h1 {
            margin-top: 0;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 30px;
        }
        th, td {
            border: 1px solid #aaa;
            padding: 8px;
            text-align: left;
        }
        th {
            background-color: #dbe8ff;
        }
        .section {
            margin-bottom: 16px;
        }
        .section strong {
            display: inline-block;
            margin-top: 12px;
        }
        .back-btn {
            margin-top: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Buchungshinweise</h1>

            <table>
                <tr>
                    <th>Reihenfolge</th>
                    <th>Buchung</th>
                    <th>Aktion / Was du tust</th>
                    <th>Beispiel</th>
                </tr>
                <tr><td>1</td><td>Anfahrt</td><td>Von der Firma losfahren (Material eingeladen)</td><td>07:00 – Material laden, losfahren</td></tr>
                <tr><td>2</td><td>Arbeitsbeginn</td><td>Auf der Baustelle ankommen und anfangen</td><td>07:15 – erste Arbeiten</td></tr>
                <tr><td>3</td><td>Automatische Pause</td><td>09 Uhr – 0,25 h · 12 Uhr – 0,50 h (automatisch, nichts buchen)</td><td>Wird immer automatisch gebucht</td></tr>
                <tr><td>4</td><td>Abfahrt</td><td>Baustelle verlassen, zurück zur Firma (o. neue Baustelle)</td><td>16:00 – Werkzeug einladen, losfahren</td></tr>
                <tr><td>5</td><td>Arbeitsende</td><td>In der Firma ausladen, Feierabend (unbedingt buchen!)</td><td>16:40 – Bulli abstellen, Feierabend</td></tr>
            </table>

            <div class="section"><strong>Mehrere Baustellen an einem Tag</strong><br />
                Baustelle A verlassen → Abfahrt<br />
                Baustelle B ankommen → Arbeitsbeginn<br />
                Zurück zu A → wieder Abfahrt / Arbeitsbeginn<br />
                <em>Kein „Arbeitsende“ dazwischen – nur am echten Feierabend!</em>
            </div>

            <div class="section"><strong>Fehlende Projektnummer</strong><br />
                Kurz in Bemerkung notieren, z. B.: „Kabel nachliefern Fa. Müller“.
            </div>

            <div class="section"><strong>Berechnung & Rundung</strong><br />
                Es wird minutengenau abgerechnet, keine Auf-/Abrundung auf 0,25 h.<br />
                8 h 07 min bleiben 8,12 h – und werden exakt weitergerechnet.
            </div>

            <div class="section"><strong>Automatische Pause</strong><br />
                Wird automatisch gezogen – kann nicht vergessen oder ergänzt werden.
            </div>

            <div class="section"><strong>Wichtig am Tagesende</strong><br />
                Arbeitsende buchen!<br />
                Sonst läuft die Zeit (z. B. Freitag → Montag früh) automatisch weiter!
            </div>

            <asp:Button ID="btnBack" runat="server" Text="Zurück zur Startseite" CssClass="back-btn" />
        </div>
    </form>
</body>
</html>
