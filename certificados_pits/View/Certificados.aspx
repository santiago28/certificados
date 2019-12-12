<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Certificados.aspx.cs" Inherits="certificados_pits.View.Certificados" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>

<!DOCTYPE html>

<html>
<head runat="server">
    <%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />--%>
    <title></title>
    <style>
        table div {
            text-align: justify !important;
        }
    </style>
</head>
<body>
    <div align="center">
        <h1>Certificado Contractual</h1>
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <rsweb:ReportViewer ID="ReporteCertificado" runat="server" Height="778px" Width="60%" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <%--<LocalReport ReportPath="View\Reportes\CertificadoLaboral.rdlc">
                 
             </LocalReport>--%>
            </rsweb:ReportViewer>
        </form>
    </div>

</body>
</html>
