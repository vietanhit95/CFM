<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report03TH.aspx.cs" Inherits="Cfm.Web.Mvc.Areas.CFMReport.ReportView.Report03TH" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hiển thị báo cáo 03TH</title>
    <style type="text/css">
        .Initial
        {
            display: block;
            padding: 4px 18px 4px 18px;
            float: left;
            background:#1F568B;
            color: white;
            font-weight: bold;
        }
        .Initial:hover
        {            
            background-color:white;
            color:burlywood;
        }
        .Clicked
        {
            float: left;
            display: block;
            background-color:lightgray;
            padding: 4px 18px 4px 18px;
            color: Black;
            border-style:groove;
        }
    </style>
    <script>
        
    </script>
</head>
<body style="font-family: tahoma">
    <form id="form1" runat="server">
        <div id="menu" style ="background: #1F568B; list-style-type: none;  text-align: left; height:28px; ">
            <asp:Button ID="btnPhanI" runat="server" Text="Phần I" Width="120px" Height="28px" BorderStyle="None" Font-Bold="True" OnClick="btnPhanI_Click"/>
            <asp:Button ID="btnPhanII" runat="server" Text="Phần II" Width="120px" Height="28px" BorderStyle="None" Font-Bold="True" OnClick="btnPhanII_Click"/>
            <asp:Button ID="btnPhanIII" runat="server" Text="Phần III" Width="120px" Height="28px" BorderStyle="None" Font-Bold="True" OnClick="btnPhanIII_Click"/>
        </div>       
        <div>
            <asp:MultiView ID="MainView" runat="server">
                <asp:View ID="View1" runat="server">
                    <div id="subMenu" style ="background: #1F568B; list-style-type: none;  text-align: left; height:28px; border:solid 0.5px; border-color:white; ">
                        <asp:Button ID="btnPage1" runat="server" Text="Trang 1" Width="120px" Height="28px" BorderStyle="None" Font-Bold="False" OnClick="btnPage1_Click"/>
                        <asp:Button ID="btnPage2" runat="server" Text="Trang 2" Width="120px" Height="28px" BorderStyle="None" Font-Bold="False" OnClick="btnPage2_Click" />
                        <asp:Button ID="btnPage3" runat="server" Text="Trang 3" Width="120px" Height="28px" BorderStyle="None" Font-Bold="False" OnClick="btnPage3_Click" />
                     </div>
                    <asp:MultiView ID="SubMain" runat="server">
                        <asp:View ID="View4" runat="server">
                            <CR:CrystalReportViewer ID="crvPage1" runat="server" AutoDataBind="true" />
                        </asp:View>
                        <asp:View ID="View5" runat="server">
                            <CR:CrystalReportViewer ID="crvPage2" runat="server" AutoDataBind="true" />
                        </asp:View>
                        <asp:View ID="View6" runat="server">
                            <CR:CrystalReportViewer ID="crvPage3" runat="server" AutoDataBind="true" />
                        </asp:View>
                    </asp:MultiView>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <CR:CrystalReportViewer ID="crvPhanII" runat="server" AutoDataBind="true" />
                </asp:View>
                <asp:View ID="View3" runat="server">
                    <CR:CrystalReportViewer ID="crvPhanIII" runat="server" AutoDataBind="true" />                    
                </asp:View>
            </asp:MultiView>
        </div>
    </form>
</body>
</html>
