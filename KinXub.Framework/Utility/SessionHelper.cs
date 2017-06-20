using System.Web;

namespace KinXub.Framework
{
    public class SessionHelper
    {
        /// <summary>
        /// 根據session名獲取session對象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetSession(string name)
        {
            return HttpContext.Current.Session[name];
        }

        /// <summary>
        /// 設置session
        /// </summary>
        /// <param name="name">session 名</param>
        /// <param name="val">session 值</param>
        public static void SetSession(string name, object val)
        {
            HttpContext.Current.Session.Remove(name);
            HttpContext.Current.Session.Add(name, val);
        }

        /// <summary>
        /// 讀取某個Session對象值
        /// </summary>
        /// <param name="name">Session對象名稱</param>
        /// <returns>Session对象值</returns>
        public static string Get(string name)
        {
            if (HttpContext.Current.Session[name] == null)
            {
                return null;
            }
            else
            {
                return HttpContext.Current.Session[name].ToString();
            }
        }

        /// <summary>
        /// 刪除某個Session對象
        /// </summary>
        /// <param name="name">Session對象名稱</param>
        public static void Del(string name)
        {
            HttpContext.Current.Session[name] = null;
        }
    }
}
