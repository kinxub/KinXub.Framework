using System;
using System.Web;

namespace KinXub.Framework
{
    public class IPHelper
    {
        /// <summary>
        /// 客戶端IP
        /// </summary>
        public static string GetClientIp
        {
            get
            {
                string ip = string.Empty;
                try
                {

                    if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                    {
                        ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                    }
                    else
                    {
                        ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    }
                    if (ip == "::1")
                    {
                        ip = "127.0.0.1";
                    }
                }
                catch (Exception ex)
                {
                }
                return ip;
            }
        }
    }
}
