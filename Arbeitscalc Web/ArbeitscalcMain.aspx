<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="ArbeitscalcMain.aspx.vb" Inherits="Arbeitscalc_Web.ArbeitscalcMain" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Arbeitscalc Web</title>
    <style>
        body { font-family:Segoe UI,Arial,sans-serif;font-size:14px;padding:24px; }
        .csvupload { margin-bottom:16px; }
        .csvupload label { margin-right:12px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="csvupload">
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="btnImportCSV" runat="server" Text="CSV laden" OnClick="btnImportCSV_Click" />
            <asp:Label ID="lblStatus" runat="server" ForeColor="Red" />
        </div>
        <div style="background: #ffeaea; border: 2px solid #ff4444; color: #a00; font-weight: bold; border-radius: 6px; padding: 12px 18px; margin-bottom: 18px;">
    ⚠️ <u>Achtung:</u> Zeiten dürfen <b>nur</b> im Format <b>HH:MM</b> eingegeben werden – z.&nbsp;B. <b>07:15</b>.<br />
    <b>Niemals</b> einstellige Stunden schreiben (<b>falsch:</b> 7:15, <b>richtig:</b> 07:15)!
</div>
        <h3>Tagesdaten</h3>
        <asp:GridView ID="GridViewTagesdaten" runat="server" AutoGenerateColumns="False"
              OnRowEditing="GridViewTagesdaten_RowEditing"
              OnRowUpdating="GridViewTagesdaten_RowUpdating"
              OnRowCancelingEdit="GridViewTagesdaten_RowCancelingEdit"
              DataKeyNames="Datum,Baustelle">
    <Columns>
        <asp:BoundField DataField="Tag" HeaderText="Tag" ReadOnly="True" />
        <asp:BoundField DataField="Datum" HeaderText="Datum" ReadOnly="True" />
        <asp:BoundField DataField="Baustelle" HeaderText="Baustelle" ReadOnly="True" />
        <asp:BoundField DataField="Bemerkung" HeaderText="Bemerkung" />
        <asp:BoundField DataField="Fahrzeit-Zeitraum" HeaderText="Fahrzeit-Zeitraum" />
        <asp:BoundField DataField="Arbeitszeit-Zeitraum" HeaderText="Arbeitszeit-Zeitraum" />
        <asp:BoundField DataField="Pausenzeit (h)" HeaderText="Pausenzeit (h)" />
        <asp:BoundField DataField="Arbeitszeit (h)" HeaderText="Arbeitszeit (h)" ReadOnly="True" />
        <asp:BoundField DataField="Fahrzeit (h)" HeaderText="Fahrzeit (h)" ReadOnly="True" />
        <asp:BoundField DataField="Überstunden (h)" HeaderText="Überstunden (h)" ReadOnly="True" />
        <asp:BoundField DataField="Überstd. 25% (h)" HeaderText="Überstd. 25% (h)" ReadOnly="True" />
        <asp:BoundField DataField="Überstd. 50% (h)" HeaderText="Überstd. 50% (h)" ReadOnly="True" />
        <asp:BoundField DataField="Vergütete Arbeitszeit (h)" HeaderText="Vergütete Arbeitszeit (h)" ReadOnly="True" />
        <asp:BoundField DataField="Datenintegrität" HeaderText="Datenintegrität" ReadOnly="True" />
        <asp:CommandField ShowEditButton="True" />
    </Columns>
</asp:GridView>
        <h3>Summen</h3>
        <asp:GridView ID="GridViewSummen" runat="server" AutoGenerateColumns="True" CssClass="table" />
        <asp:Button ID="btnExportPdf" runat="server" Text="Export PDF" OnClick="btnExportPdf_Click" />
        <asp:Button ID="btnExportCsv" runat="server" Text="Export CSV" OnClick="btnExportCsv_Click" />
    </form>
</body>
</html>