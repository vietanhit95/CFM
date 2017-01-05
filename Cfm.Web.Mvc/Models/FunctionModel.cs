using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc.Models
{
    public class FunctionModel
    {
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }
        public int Area { get; set; }
        public int ParentId { get; set; }
    }

    public class FunctionList
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public int EmpGroupId { get; set; }
        public int PoId { get; set; }
        public int MenuId { get; set; }
        public int AreaId { get; set; }
        public bool Checked { get; set; }
    }
}