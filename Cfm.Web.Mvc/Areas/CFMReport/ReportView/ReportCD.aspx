<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportCD.aspx.cs" Inherits="Cfm.Web.Mvc.Areas.CFMReport.ReportView.ReportCD" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
     #menu
        {
             width: 100%;
             margin: 0;
             padding: 10px 0 0 0;
             list-style: none;
             background: #1a436d;
             background: -moz-linear-gradient(#444, #1a436d);
             background: -webkit-gradient(linear,left bottom,left top,color-stop(0, #1a436d),color-stop(1, #444));
             background: -webkit-linear-gradient(#444, #1a436d);
             background: -o-linear-gradient(#444, #1a436d);
             background: -ms-linear-gradient(#444, #1a436d);
             background: linear-gradient(#444, #1a436d);
             -moz-border-radius: 50px;
             border-radius: 50px;
             -moz-box-shadow: 0 2px 1px #9c9c9c;
             -webkit-box-shadow: 0 2px 1px #9c9c9c;
             box-shadow: 0 2px 1px #9c9c9c;
        }
    #menu li
    {
         float: left;
         padding: 0 0 10px 0;
         position: relative;
         line-height: 0;
    }
    #menu a
    {
         float: left;
         height: 25px;
         padding: 0 25px;
         color: #fafafa;
         /*text-transform: uppercase;*/
         font: bold 12px/25px Arial, Helvetica;
         text-decoration: none;
         text-shadow: 0 1px 0 #000;
    }
    #menu li:hover > a
    {
        color: #fafafa;
    }
    #menu li a:hover, #menu li a:active /* IE6 */
    {
        color: #e2bb5d;
        background: #1a436d;
        background: -moz-linear-gradient(#04acec,  #1a436d);
        background: -webkit-gradient(linear, left top, left bottom, from(#04acec), to(#1a436d));
        background: -webkit-linear-gradient(#04acec,  #1a436d);
        background: -o-linear-gradient(#04acec,  #1a436d);
        background: -ms-linear-gradient(#04acec,  #1a436d);
        background: linear-gradient(#04acec,  #1a436d);
    }
    #menu li:hover > ul
    {
        display: block;
    }

    #menu ul
    {
        list-style: none;
        margin: 0;
        padding: 0;
        display: none;
        position: absolute;
        top: 35px;
        left: 0;
        z-index: 99999;
        background: #444;
        background: -moz-linear-gradient(#444, #1a436d);
        background: -webkit-gradient(linear,left bottom,left top,color-stop(0, #1a436d),color-stop(1, #444));
        background: -webkit-linear-gradient(#444, #1a436d);
        background: -o-linear-gradient(#444, #1a436d);
        background: -ms-linear-gradient(#444, #1a436d);
        background: linear-gradient(#444, #1a436d);
        -moz-box-shadow: 0 0 2px rgba(255,255,255,.5);
        -webkit-box-shadow: 0 0 2px rgba(255,255,255,.5);
        box-shadow: 0 0 2px rgba(255,255,255,.5);
        -moz-border-radius: 5px;
        border-radius: 5px;
    }
    #menu ul ul
    {
        top: 0;
        left: 150px;
    }
    #menu ul li
    {
        float: none;
        margin: 0;
        padding: 0;
        display: block;
        -moz-box-shadow: 0 1px 0 #111111, 0 2px 0 #777777;
        -webkit-box-shadow: 0 1px 0 #111111, 0 2px 0 #777777;
        box-shadow: 0 1px 0 #111111, 0 2px 0 #777777;
    }
    #menu ul li:last-child
    {
        -moz-box-shadow: none;
        -webkit-box-shadow: none;
        box-shadow: none;
    }
    #menu ul a
    {
        padding: 10px;
        height: 10px;
        width: 130px;
        height: auto;
        line-height: 1;
        display: block;
        white-space: nowrap;
        float: none;
        text-transform: none;
    }
    #menu ul a /* IE6 */
    {
        height: 10px;
    }
    #menu ul a /* IE7 */
    {
        height: 10px;
    }
    #menu ul a:hover
    {
        background: #1a436d;
        background: -moz-linear-gradient(#04acec,  #1a436d);
        background: -webkit-gradient(linear, left top, left bottom, from(#04acec), to(#1a436d));
        background: -webkit-linear-gradient(#04acec,  #1a436d);
        background: -o-linear-gradient(#04acec,  #1a436d);
        background: -ms-linear-gradient(#04acec,  #1a436d);
        background: linear-gradient(#04acec,  #1a436d);
    }
    #menu ul li:first-child > a
    {
        -moz-border-radius: 5px 5px 0 0;
        border-radius: 5px 5px 0 0;
    }
    #menu ul li:first-child > a:after
    {
        content: '';
        position: absolute;
        left: 30px;
        top: -8px;
        width: 0;
        height: 0;
        border-left: 5px solid transparent;
        border-right: 5px solid transparent;
        border-bottom: 8px solid #444;
    }
    #menu ul ul li:first-child a:after
    {
        left: -8px;
        top: 12px;
        width: 0;
        height: 0;
        border-left: 0;
        border-bottom: 5px solid transparent;
        border-top: 5px solid transparent;
        border-right: 8px solid #444;
    }
    #menu ul li:first-child a:hover:after
    {
        border-bottom-color: #04acec;
    }
    #menu ul ul li:first-child a:hover:after
    {
        border-right-color: #04acec;
        border-bottom-color: transparent;
    }

    #menu ul li:last-child > a
    {
        -moz-border-radius: 0 0 5px 5px;
        border-radius: 0 0 5px 5px;
    }
    #menu:after
    {
        visibility: hidden;
        display: block;
        font-size: 0;
        content: " ";
        clear: both;
        height: 0;
    }
    #menu             { zoom: 1; } /* IE6 */
    #menu { zoom: 1; } /* IE7 */
    a:active{
        background-color:white;
    }
    
    </style>   
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ul id="menu">
                <li>
                    <a href="#" >Mục I - Tổng hợp cân đối thu chi</a>
                </li>
                <li>
                    <a href="#" onclick="">Mục II - Chi tiết thông tin các dịch vụ</a>
                </li>
                <li>
                    <a href="#" onclick="">Mục III - Một số thông tin chi tiết tham khảo</a>
                </li>            
                <li style="width:20px;float:right;height:25px;"></li>
                <li style="float:right;height:25px;">
                    <asp:ImageButton ID="imgbWord" runat="server" ImageUrl="~/Areas/CFMReport/img/img_word.png" OnClick="imgbWord_Click" />
                </li>
                <li style="float:right;height:25px;">
                    <asp:ImageButton ID="imgbExcel" runat="server" ImageUrl="~/Areas/CFMReport/img/img_excel.png" OnClick="imgbExcel_Click" />
                </li>
                <li style="float:right;height:25px;">
                    <asp:ImageButton ID="imgbPDF" runat="server" ImageUrl="~/Areas/CFMReport/img/img_pdf.png" OnClick="imgbPDF_Click" />
                </li>
            </ul>
        </div>
        <div>
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
    </form>
</body>
</html>
