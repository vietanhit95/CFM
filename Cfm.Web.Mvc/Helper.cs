using Cfm.Web.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Cfm.Web.Mvc
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
                    _apiKey = Security.DecryptString(ConfigurationManager.AppSettings["API_KEY"]);
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
    }
}