using Cfm.Web.Mvc.Areas.Admin.Models;
using Cfm.Web.Mvc.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfm.Web.Mvc.Areas.Admin.Controllers
{
    [FilterRoleToAction(Role = new int[] { (int)Constant.POLevel.Center })]
    public class PostOfficeController : BaseController
    {
        // GET: Admin/PostOffice
        public ActionResult Index()
        {
            var model = (PostOfficeViewModel)Session[Constant.PO_SESSION];
            var poList = new List<PostOfficeViewModel>();
            if (model == null)
            {
                return RedirectToAction("Exception", "Error", new { area = "Admin", errorMsg = "Không lấy được thông tin Bưu cục." });
            }
            poList.Add(model);
            ViewBag.POList = poList;
            ViewBag.ParentID = model.ID;
            return View(model);
        }

        public PartialViewResult PODetail()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult POTreeView(List<PostOfficeViewModel> poList, int id)
        {
            string json = string.Empty;
            if (poList == null)
            {
                poList = new List<PostOfficeViewModel>();
            }

            if (id > 0)
            {
                var rs = Helper.Invoke("GET", string.Format("api/PostOffice/GetListPOByParentID?parentID={0}", new object[] { id }), null);
                if (rs != null)
                {
                    foreach (dynamic dyn in rs.ListValue)
                    {
                        var po = new PostOfficeViewModel()
                        {
                            ID = dyn.Id,
                            ParentID = dyn.ParentId,
                            Code = dyn.Code,
                            Name = dyn.Name,
                            POLevel = dyn.POLevel,
                            IsCenter = dyn.IsCenter == "Y" ? true : false,
                            Address = dyn.Address,
                            PhoneNumber = dyn.PhoneNumber,
                            FaxNumber = dyn.FaxNumber,
                            IsOffline = dyn.IsOffline == "Y" ? true : false,
                            CycleDate = dyn.CycleDate,
                            IsLock = dyn.IsLock == "Y" ? true : false
                        };
                        if (!poList.Contains(po))
                        {
                            poList.Add(po);
                        }
                    }
                }
            }
            ViewData["POList"] = poList;
            ViewBag.POTreeViewData = ConvertDataToJson(poList);
            return PartialView();
        }

        private string ConvertDataToJson(List<PostOfficeViewModel> list)
        {
            List<POTreeViewModel> listTree = new List<POTreeViewModel>();
            int minLevel = 10;
            string json = string.Empty;
            try
            {
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (item.POLevel < minLevel)
                        {
                            minLevel = item.POLevel;
                        }

                        listTree.Add(new POTreeViewModel()
                        {
                            text = item.Code + " - " + item.Name,
                            poLevel = item.POLevel,
                            icon = "glyphicon glyphicon-envelope",
                            tags = item.ID.ToString(),
                            href = "",
                            id = item.ID,
                            parentId = item.ParentID
                        });
                    }

                    foreach (var p in listTree)
                    {
                        if (p.poLevel == minLevel)
                        {
                            var parent = p;
                            parent = ChildrenOf(parent, listTree);
                            if (string.IsNullOrEmpty(json))
                                json = JsonConvert.SerializeObject(parent);
                            else
                                json = json + ", " + JsonConvert.SerializeObject(parent);
                        }
                    }
                }

            }
            catch
            {

            }
            return string.Format("[{0}]", json);
        }


        private static POTreeViewModel ChildrenOf(POTreeViewModel parent, List<POTreeViewModel> list)
        {
            try
            {
                foreach (POTreeViewModel child in list.Where(x => x.parentId == parent.id))
                {
                    parent.childs.Add(ChildrenOf(child, list));
                }
            }
            catch
            {

            }
            return parent;
        }

        public JsonResult ListPOByParentID(int parentID)
        {
            var poList = new List<PostOfficeViewModel>();
            var rs = Helper.Invoke("GET", string.Format("api/PostOffice/GetListPOByParentID?parentID={0}", new object[] { parentID }), null);
            if (rs != null)
            {
                if (rs.ListValue != null)
                {
                    foreach (dynamic dyn in rs.ListValue)
                    {
                        var po = new PostOfficeViewModel()
                        {
                            ID = dyn.Id,
                            ParentID = dyn.ParentId,
                            Code = dyn.Code,
                            Name = dyn.Name,
                            POLevel = dyn.POLevel,
                            IsCenter = dyn.IsCenter == "Y" ? true : false,
                            Address = dyn.Address,
                            PhoneNumber = dyn.PhoneNumber,
                            FaxNumber = dyn.FaxNumber,
                            IsOffline = dyn.IsOffline == "Y" ? true : false,
                            CycleDate = dyn.CycleDate,
                            IsLock = dyn.IsLock == "Y" ? true : false
                        };
                        if (!poList.Contains(po))
                        {
                            poList.Add(po);
                        }
                    }
                }
            }
            return Json(ConvertDataToJson(poList));
        }
    }
}