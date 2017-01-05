<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="Cfm.Web.Mvc.Areas.CFMReport.ReportView.ReportViewer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hiển thị báo cáo</title>
</head>
<body>
    <form id="form1" runat="server">        
        <div>
            <table align="center">    
                <tr>
                    <td>
                        <div style="margin-left:auto;margin-right:auto;margin-top:0px;padding-bottom:24px;">
                            <table align="right">
                                <tr>
                                    <td>
                                        <div>
                                            <asp:ImageButton ID="imgbPDF" runat="server" ImageUrl="~/Areas/CFMReport/img/img_pdf.png" OnClick="imgbPDF_Click" />
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:ImageButton ID="imgbExcel" runat="server" ImageUrl="~/Areas/CFMReport/img/img_excel.png" OnClick="imgbExcel_Click" />
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:ImageButton ID="imgbWord" runat="server" ImageUrl="~/Areas/CFMReport/img/img_word.png" OnClick="imgbWord_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <CR:CrystalReportViewer ID="crvReport" runat="server" AutoDataBind="true" HasCrystalLogo="False"
                                Height="50px" Width="350px" style="margin:auto; padding:inherit;" HasDrilldownTabs="False" EnableTheming="True" HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" SeparatePages="False" ShowAllPageIds="True" ToolPanelView="None" EnableDrillDown="False" HasDrillUpButton="False" HasExportButton="False" HasPrintButton="False"/>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
