<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report04CD.aspx.cs" Inherits="Cfm.Web.Mvc.Areas.CFMReport.ReportView.Report04CD" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mục I - Tổng hợp cân đối thu chi</title>
    <style>
        .div
        {
            background-color: #1a436d;
            height:35px;
            width:80%;     
           margin:auto;
        }
        .div2
        {
            background-color:silver; 
            height:auto;
            width:80%;
            margin:auto;
        }
        .img
        {
            padding-top:2px;
            float:right;
            height:35px;
        }
        .button
        {
            background-color: #1a436d;
            color: #fffafa;
            height:35px;
            padding-left:20px;
            padding-right:20px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font: normal 12px/25px Arial, Helvetica;
            -webkit-transition-duration: 0.4s;
            transition-duration: 0.4s;
            cursor: pointer;
            float:left;
        }
        .button:hover , .button:active
        {
            background-color:#265e98 ;
            color: #fffafa;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="div">
                <asp:Button ID="btnMucI" runat="server" Text="Mục I - Tổng hợp cân đối thu chi" class="button" BorderStyle="None" OnClick="btnMucI_Click" />
                <asp:Button ID="btnMucII" runat="server" Text="Mục II - Chi tiết thông tin các dịch vụ" class="button" BorderStyle="None" OnClick="btnMucII_Click"/>
                <asp:Button ID="btnMucIII" runat="server" Text="Mục III - Một số thông tin chi tiết tham khảo" class="button" BorderStyle="None" OnClick="btnMucIII_Click"/>
                <div class="img">
                    <asp:ImageButton ID="imgbWord" runat="server" ImageUrl="~/Areas/CFMReport/img/img_word.png" OnClick="imgbWord_Click"/>
                </div>
                <div class="img">
                    <asp:ImageButton ID="imgbExcel" runat="server" ImageUrl="~/Areas/CFMReport/img/img_excel.png" OnClick="imgbExcel_Click" />
                </div>
                <div class="img">
                    <asp:ImageButton ID="imgbPDF" runat="server" ImageUrl="~/Areas/CFMReport/img/img_pdf.png" OnClick="imgbPDF_Click" />
                </div>
            </div>
            <div class="div2">
                <table align="center">
                    <tr>
                        <td>
                            <div>
                                <CR:CrystalReportViewer ID="crvReport" runat="server" AutoDataBind="true" HasCrystalLogo="False"
                                        Height="50px" Width="350px" style="margin:auto; padding:inherit;" HasDrilldownTabs="False" EnableTheming="True" HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" SeparatePages="False" ShowAllPageIds="True" ToolPanelView="None" EnableDrillDown="False" HasDrillUpButton="False" HasExportButton="False" HasPrintButton="False"/>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            </div>
    </form>
</body>
</html>
