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
        <h3>Tagesdaten</h3>
        <asp:GridView ID="GridViewTagesdaten" runat="server" AutoGenerateColumns="True" CssClass="table" />
        <h3>Summen</h3>
        <asp:GridView ID="GridViewSummen" runat="server" AutoGenerateColumns="True" CssClass="table" />
        <asp:Button ID="btnExportPdf" runat="server" Text="Export PDF" OnClick="btnExportPdf_Click" />
        <asp:Button ID="btnExportCsv" runat="server" Text="Export CSV" OnClick="btnExportCsv_Click" />
    </form>
</body>
</html>