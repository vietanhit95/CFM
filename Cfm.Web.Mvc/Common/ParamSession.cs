using Cfm.Web.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfm.Web.Mvc.Common
{
    public class ParamSession : Controller
    {
        private EmployeeViewModel _employee = null;
        private PostOfficeViewModel _postOffice = null;

        public EmployeeViewModel Employee
        {
            get
            {
                if (_employee == null)
                    _employee = (EmployeeViewModel)System.Web.HttpContext.Current.Session[Constant.EMP_SESSION];
                return _employee;
            }

            set
            {
                _employee = value;
            }
        }

        public PostOfficeViewModel PostOffice
        {
            get
            {
                if (_postOffice == null)
                    _postOffice = (PostOfficeViewModel)System.Web.HttpContext.Current.Session[Constant.PO_SESSION];
                return _postOffice;
            }

            set
            {
                _postOffice = value;
            }
        }
    }
}