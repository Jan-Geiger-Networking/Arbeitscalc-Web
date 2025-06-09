<%@ Page Language="VB" AutoEventWireup="true" CodeBehind="Rechtliches.aspx.vb" Inherits="Arbeitscalc_Web.Rechtliches" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rechtliches - Arbeitscalc</title>
    <style>
        body { font-family: Arial, sans-serif; background: #f7f7f7; }
        .container { margin: 60px auto; background: #fff; border-radius: 8px; box-shadow: 0 2px 10px #bbb; max-width: 700px; padding: 32px; }
        h2 { margin-top: 0; }
        pre { white-space: pre-wrap; word-wrap: break-word; }
        .back-btn { margin-top: 20px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Rechtliches</h2>
            <pre>
Arbeitscalc – Lizenzinformationen

Copyright (c) 2025 Jan Geiger

Dieses Projekt verwendet den folgenden Lizenzmix:

1. Eigenentwickelter Code:
   Lizenz: MIT License
   Siehe Datei: LICENSE-MIT.txt

2. Eingesetzte Bibliothek:
   - iTextSharp (Version 5.5.13.3) – PDF-Erzeugung
     Lizenz: GNU Affero General Public License (AGPL) v3
     Lizenztext: https://www.gnu.org/licenses/agpl-3.0.de.html

Hinweis: Durch die Verwendung von iTextSharp unterliegt das gesamte Projekt den Bedingungen der AGPLv3. Bei Verbreitung oder öffentlicher Zugänglichmachung des Programms sind Sie verpflichtet, den vollständigen Quellcode offenzulegen.

Dieses Programm dient der unterstützenden Berechnung und Auswertung von Arbeits- und Fahrzeiten anhand strukturierter Textdateien. Trotz sorgfältiger Programmierung kann nicht ausgeschlossen werden, dass das Programm Fehler enthält oder unter bestimmten Bedingungen nicht korrekt funktioniert. Die Verantwortung für die Richtigkeit der berechneten Daten liegt beim Nutzer. Bitte prüfen Sie alle Ergebnisse vor der Weitergabe oder Abrechnung sorgfältig. Der Entwickler übernimmt keine Haftung für Schäden, die durch die Nutzung oder fehlerhafte Auswertung entstehen, es sei denn, diese beruhen auf vorsätzlichem oder grob fahrlässigem Verhalten des Entwicklers.
            </pre>
            <asp:Button ID="btnBack" runat="server" Text="Zurück" CssClass="back-btn" />
        </div>
    </form>
</body>
</html>
