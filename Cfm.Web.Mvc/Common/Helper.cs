using Cfm.Web.Common;
using Cfm.Web.Lib;
using Cfm.Web.Log;
using Cfm.Web.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Cfm.Web.Mvc.Common
{
    public static class Helper
    {
        private static string _apiKey = string.Empty;
        private static string _apiUrl = string.Empty;
        private static string _logPath = string.Empty;

        public static string APIKey
        {
            get
            {
                if (string.IsNullOrEmpty(_apiKey))
                    _apiKey = ConfigurationManager.AppSettings["API_KEY"];
                return _apiKey;
            }

            set
            {
                _apiKey = value;
            }
        }
        public static string APIUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_apiUrl))
                    _apiUrl = ConfigurationManager.AppSettings["API_URL"];
                return _apiUrl;
            }

            set
            {
                _apiUrl = value;
            }
        }
        public static string LogPath
        {
            get
            {
                if (string.IsNullOrEmpty(_logPath))
                    _logPath = ConfigurationManager.AppSettings["LOG_PATH"];
                return _logPath;
            }

            set
            {
                _logPath = value;
            }
        }
        public static void LogToFile(LogFileType logFileType, string message)
        {
            try
            {
                LoggerGeneral logger = new LoggerGeneral();
                message = DateTime.Now.ToString("HH:mm:ss") + " " + message;
                FormatterGeneral formatter = new FormatterGeneral(message);
                string logPath = string.Empty;
                logPath = LogPath + @"\" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HH");
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
                switch (logFileType)
                {
                    case LogFileType.TRACE:
                        logPath = logPath + @"\TRACE.log";
                        break;
                    case LogFileType.MESSAGE:
                        logPath = logPath + @"\MESSAGE.log";
                        break;
                    default:
                        logPath = logPath + @"\EXCEPTION.log";
                        break;
                }
                logger.Write(formatter, logPath);
            }
            catch
            {

            }
        }

        public static ResponseObject Invoke(string method, string requestUri, object requestBody)
        {
            ResponseObject res = new ResponseObject();
            HttpResponseMessage response = null;
            try
            {
                using(HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(APIUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("API_KEY", APIKey);
                    client.DefaultRequestHeaders.Add("Authorization", string.Format("Basic {0}", ""));
                    
                    switch (method)
                    {
                        case "GET":
                            response = client.GetAsync(requestUri).Result;
                            break;
                        case "HEAD":                            
                            response = client.GetAsync(requestUri).Result;
                            break;
                        case "POST":
                            response = client.PostAsJsonAsync(requestUri, requestBody).Result;
                            break;
                        case "DELETE":
                            response = client.DeleteAsync(requestUri).Result;
                            break;
                        default:
                            break;
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        res = response.Content.ReadAsAsync<ResponseObject>().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                LogToFile(LogFileType.EXCEPTION, "Helper.Invoke: " + ex.Message);
                res.Code = "99";
                res.Message = "Lỗi hệ thống, vui lòng liên hệ quản trị";
            }
            return res;
        }
    }
}