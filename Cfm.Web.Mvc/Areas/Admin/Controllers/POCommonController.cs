using Cfm.Web.Mvc.Areas.Admin.Models;
using Cfm.Web.Mvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfm.Web.Mvc.Areas.Admin.Controllers
{
    [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Center , (int)Constant.POLevel.Branch , (int)Constant.POLevel.District , (int)Constant.POLevel.Counter })]
    public class POCommonController : BaseController
    {
        // GET: Admin/POCommon
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ListPO(
            string func,
            int isLoadParent = 0, 
            bool ChosePo = false, 
            bool ChoseD = false, 
            bool ChoseP = false, 
            bool ChoseT = false,
            bool IsSubtract = false)
        {
            PostOfficeViewModel po = PoCurrent();

            if (po != null)
            {
                List<PostOfficeViewModel> ListPo = new List<PostOfficeViewModel>();

                if(isLoadParent == 1)
                {
                    var ParentPo = Repository.GetPOByID(po.ParentID);
                    ListPo.Add(ParentPo);
                }
                else if (isLoadParent == 2)
                {
                    var ParentPo1 = Repository.GetPOByID(po.ParentID);
                    var ParentPo2 = Repository.GetPOByID(ParentPo1.ParentID);
                    ListPo.Add(ParentPo2);
                    ListPo.Add(ParentPo1);
                }

                ListPo.Add(po);
                ViewBag.isLoadParent = isLoadParent;
                ViewBag.ChosePo = ChosePo;
                ViewBag.ChoseD = ChoseD;
                ViewBag.ChoseP = ChoseP;
                ViewBag.ChoseT = ChoseT;
                ViewBag.func = func;
                ViewBag.Subtract = IsSubtract;
                return PartialView(ListPo);
            }
            else
            {
                return null;
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ListPoChild(int ParentId, 
            bool ChosePo = false,
            bool ChoseD = false,
            bool ChoseP = false,
            bool ChoseT = false,
            bool IsSubtract = false
            )
        {
            var rs = Helper.Invoke("GET", string.Format("api/PostOffice/GetListPOByParentID?parentID={0}", new object[] { ParentId }), null);

            List<PostOfficeViewModel> ListPo = new List<PostOfficeViewModel>();
            if (rs != null && rs.Code == "00" && rs.ListValue != null)
            {
                foreach (dynamic dyn in rs.ListValue)
                {
                    PostOfficeViewModel oItem = new PostOfficeViewModel();

                    oItem.ID = int.Parse((dyn.Id ?? "0").ToString());
                    oItem.ParentID = int.Parse((dyn.ParentId ?? "0").ToString());
                    oItem.Code = (dyn.Code ?? "").ToString();
                    oItem.Name = (dyn.Name ?? "").ToString();
                    oItem.POLevel = int.Parse((dyn.POLevel ?? "0").ToString());
                    oItem.IsCenter = (dyn.IsCenter ?? "N").ToString() == "Y" ? true : false;
                    oItem.Address = (dyn.Address ?? "").ToString();
                    oItem.PhoneNumber = (dyn.PhoneNumber ?? "").ToString();
                    oItem.FaxNumber = (dyn.FaxNumber ?? "").ToString();
                    oItem.IsOffline = (dyn.IsOffline ?? "N").ToString() == "Y" ? true : false;
                    oItem.CycleDate = int.Parse((dyn.CycleDate ?? "0").ToString());
                    oItem.IsLock = (dyn.IsLock ?? "Y").ToString() == "Y" ? true : false;
                    ListPo.Add(oItem);
                }
            }
            ViewBag.ChosePo = ChosePo;
            ViewBag.ChoseD = ChoseD;
            ViewBag.ChoseP = ChoseP;
            ViewBag.ChoseT = ChoseT;
            ViewBag.IsSubtract = IsSubtract;
            return PartialView(ListPo);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult TreeItem(PostOfficeViewModel po, bool Check, bool IsChild = false)
        {
            ViewBag.Check = Check;
            ViewBag.IsChild = IsChild;
            return PartialView(po);
        }
    }
}