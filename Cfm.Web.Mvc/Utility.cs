using Cfm.Web.Common;
using Cfm.Web.Mvc.Areas.Admin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Cfm.Web.Mvc
{
    public static class Utility
    {
        public static string GetPOLevelName(int POLevel)
        {
            return Repository.POLevel[POLevel];
        }

        public static string GetPostOfficeTreeData()
        {
            List<POTreeViewModel> list = null;
            POTreeViewModel item = null;
            string json = string.Empty;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Helper.APIUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var req = new
                    {
                        Id = 0,
                        ParentId = 0,
                        Code = "",
                        POLevel = "",
                        IsLock = "",
                        Signature = Security.SHA256Hash("0" + "0" + "" + Helper.APIKey)
                    };
                    var res = client.PostAsJsonAsync("api/PostOffice/Search", req).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        var rs = res.Content.ReadAsAsync<ResponseList>().Result;
                        if (rs.Code == "00")
                        {
                            if (rs.ListValue != null)
                            {
                                list = new List<POTreeViewModel>();
                                foreach (dynamic dyn in rs.ListValue)
                                {
                                    item = new POTreeViewModel();
                                    item.text = dyn.Code + " - " + dyn.Name;
                                    item.POLevel = dyn.POLevel;
                                    switch (item.POLevel)
                                    {
                                        case 1:
                                            item.icon = "glyphicon glyphicon-th-list";
                                            break;
                                        case 2:
                                            item.icon = "glyphicon glyphicon-home";
                                            break;
                                        case 3:
                                            item.icon = "glyphicon glyphicon-stats";
                                            break;
                                        default:
                                            item.icon = "glyphicon glyphicon-envelope";
                                            break;
                                    }
                                    item.tags = dyn.Id;
                                    item.Id = dyn.Id;
                                    item.ParentId = dyn.ParentId;
                                    list.Add(item);
                                }
                                if (list.Count > 0)
                                {
                                    json = GetJsonData(list);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogWeb.LogToFile(LogFileType.EXCEPTION, "Utility.Index: " + ex.Message, Helper.LogPath);
                json = string.Empty;
            }
            return json;
        }

        public static List<EmpGroupViewModel> GetEmpGroupByPOId(int poId)
        {
            List<EmpGroupViewModel> list = null;
            EmpGroupViewModel item = null;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Helper.APIUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var req = new
                    {
                        POId = poId,
                        Signature = Security.SHA256Hash(poId.ToString() + "" + "" + Helper.APIKey)
                    };
                    var res = client.PostAsJsonAsync("api/Employee/EmpGroupGetByPOId", req).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        var rs = res.Content.ReadAsAsync<ResponseList>().Result;
                        if (rs.Code == "00")
                        {
                            if (rs.ListValue != null)
                            {
                                list = new List<EmpGroupViewModel>();
                                foreach (dynamic dyn in rs.ListValue)
                                {
                                    item = new EmpGroupViewModel();
                                    item.Id = dyn.Id;
                                    item.Code = dyn.Code;
                                    item.ShortName = dyn.ShortName;
                                    item.Name = dyn.Name;
                                    item.POLevel = dyn.POLevel;
                                    list.Add(item);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogWeb.LogToFile(LogFileType.EXCEPTION, "Utility.GetEmpGroupByPOId: " + ex.Message, Helper.LogPath);
                list = null;
            }
            return list;
        }

        private static string GetJsonData(List<POTreeViewModel> listPO)
        {
            POTreeViewModel root = null;
            string json = string.Empty;

            foreach (POTreeViewModel p in listPO)
            {
                if (p.ParentId == 0)
                {
                    root = p;
                    break;
                }
            }

            if (root != null)
            {
                root = ChildrenOf(root, listPO);
                json = JsonConvert.SerializeObject(new[] { root });
            }

            return json;
        }

        private static POTreeViewModel ChildrenOf(POTreeViewModel parent, List<POTreeViewModel> list)
        {
            foreach (POTreeViewModel child in list.Where(x => x.ParentId == parent.Id))
            {
                parent.Childs.Add(ChildrenOf(child, list));
            }
            return parent;
        }
    }
}