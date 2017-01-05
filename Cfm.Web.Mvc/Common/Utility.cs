using Cfm.Web.Common;
using Cfm.Web.Lib;
using Cfm.Web.Log;
using Cfm.Web.Mvc.Areas.Admin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Cfm.Web.Mvc.Common
{
    public static class Utility
    {
        public static string Page(int intTotal, int intCurrPage, int intPageSize, int intRowPerPage, int intRowFrom, int intRowTo, string function,string refType, bool isShowText = true)
        {
            string strReturn = string.Empty;
            if (intCurrPage == 0)
            {
                intCurrPage = 1;
            }

            intRowPerPage = intRowPerPage == 0 ? 1 : intRowPerPage;

            int intPageNumber = 1,
                i = 1,
                intTotalPage,
                intStart = 0;

            if ((intTotal % intRowPerPage) > 0)
            {
                intTotalPage = (intTotal / intRowPerPage) + 1;
            }
            else
            {
                intTotalPage = intTotal / intRowPerPage;
            }

            if (intRowTo > intTotal) intRowTo = intTotal;

            if (intTotalPage >= 1)
            {
                strReturn += "<div id='pager' class='row mr0 ml0'>";
                
                if(isShowText == true)
                {
                    strReturn += "<div class='col-md-4 col-sm-6 pb0'>";
                    strReturn += "<div class='dataTables_info' id='sample_1_info' role='status' aria-live='polite'>";
                    strReturn += "Hiển thị " + intRowFrom.ToString().Trim() + " tới " + intRowTo.ToString().Trim() + " của " + intTotal.ToString().Trim() + " bản ghi";
                    strReturn += "</div></div>";
                    strReturn += "<div class='col-md-7 col-sm-6 pb0' style='float: right;text-align: right;'>";
                }
                else
                {
                    strReturn += "<div class='col-md-12 col-sm-12' style='float: right;text-align: right;'>";
                }

      
                strReturn += "<div class='dataTables_paginate paging_bootstrap_full_number' id='sample_1_paginate'>";
                strReturn += "<ul class='pagination' style='visibility: visible;'>";

                if (intCurrPage <= intTotalPage)
                {
                    if (intCurrPage == 1)
                    {
                        intPageNumber = intPageSize;
                        if (intPageNumber > intTotalPage)
                        {
                            intPageNumber = intTotalPage;
                        }
                        intStart = 1;
                    }
                    else
                    {
                        strReturn += "<li class='prev'>";
                        strReturn += "<a onclick=" + function + "('" + refType + "',1) href ='javascript:void(0)' id='0' Title='First'>";
                        strReturn += "<i class='fa fa-angle-left'></i><i class='fa fa-angle-left'></i>";
                        strReturn += "</a></li>";
                        strReturn += "<li class='prev'>";
                        strReturn += "<a onclick=" + function + "('"  + refType + "'," + (intCurrPage - 1).ToString().Trim() + ") href ='javascript:void(0)' Title='Prev' id='" + (intCurrPage - 1).ToString().Trim() + "'>";
                        strReturn += "<i class='fa fa-angle-left'></i>";
                        strReturn += "</a></li>";

                        if ((intTotalPage - intCurrPage) < (intPageSize / 2))
                        {
                            intStart = (intTotalPage - intPageSize) + 1;
                            if (intStart <= 0)
                            {
                                intStart = 1;
                            }
                            intPageNumber = intTotalPage;
                        }
                        else
                        {
                            if ((intCurrPage - (intPageSize / 2)) == 0)
                            {
                                intStart = 1;
                                intPageNumber = intCurrPage + (intPageSize / 2) + 1;
                                if (intTotalPage < intPageNumber)
                                {
                                    intPageNumber = intTotalPage;
                                }
                            }
                            else
                            {
                                intStart = intCurrPage - (intPageSize / 2);
                                if (intStart <= 0)
                                {
                                    intStart = 1;
                                }
                                intPageNumber = intCurrPage + (intPageSize / 2);
                                if (intTotalPage < intPageNumber)
                                {
                                    intPageNumber = intTotalPage;
                                }
                                else
                                {
                                    if (intPageNumber < intPageSize)
                                    {
                                        intPageNumber = intPageSize;
                                    }
                                }


                            }
                        }
                    }

                    i = intStart;
                    while (i <= intPageNumber)
                    {
                        if (i == intCurrPage)
                        {
                            strReturn += "<li class='active'><a href='javascript:void(0)'>" + i.ToString().Trim() + "</a></li>";
                        }
                        else
                        {
                            strReturn += "<li><a href='javascript:void(0)' onclick=" + function + "('"  + refType + "'," + i.ToString().Trim() + ") id ='" + i.ToString().Trim() + "'>" + i.ToString().Trim() + "</a></li>";
                        }

                        i++;
                    }

                    if (intCurrPage < intTotalPage)
                    {
                        strReturn += "<li class='next'>";
                        strReturn += "<a href='javascript:void(0)' onclick=" + function + "('"  + refType + "'," + (intCurrPage + 1).ToString().Trim()  + ") Title ='Next' id='" + (intCurrPage + 1).ToString().Trim() + "'>";
                        strReturn += "<i class='fa fa-angle-right'></i></a></li>";
                        strReturn += "<li class='next'>";
                        strReturn += "<a href='javascript:void(0)' onclick=" + function + "('" + refType + "'," + intTotalPage.ToString().Trim() + ") Title ='Last' id='" + intTotalPage.ToString().Trim() + "'>";
                        strReturn += "<i class='fa fa-angle-right'></i><i class='fa fa-angle-right'></i></a></li>";
                    }

                    strReturn += "</ul></div></div></div>";
                }
            }
            return strReturn;
        }

        public static string IntToRoman(int num)
        {
            string[] thou = { "", "M", "MM", "MMM" };
            string[] hun = { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
            string[] ten = { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
            string[] ones = { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };
            string roman = "";
            roman += thou[(int)(num / 1000) % 10];
            roman += hun[(int)(num / 100) % 10];
            roman += ten[(int)(num / 10) % 10];
            roman += ones[num % 10];

            return roman;
        }

        public static bool IsCheckBoxTree(PostOfficeViewModel oPo, bool ChosePo, bool ChoseD, bool ChoseP, bool ChoseT)
        {
            bool bReturn = false;

            if (oPo.POLevel == (int)Constant.POLevel.Counter && ChosePo == true)
            {
                bReturn = true;
            }
            else if (oPo.POLevel == (int)Constant.POLevel.District && ChoseD == true)
            {
                bReturn = true;
            }
            else if (oPo.POLevel == (int)Constant.POLevel.Branch && ChoseP == true)
            {
                bReturn = true;
            }
            else if (oPo.POLevel == (int)Constant.POLevel.Center && ChoseT == true)
            {
                bReturn = true;
            }
            return bReturn;
        }
    }
}