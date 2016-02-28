using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Jxglxt
{
    class JxglxtRequest
    {
        #region 公共变量
        public CookieCollection Cookies = new CookieCollection();
        private HttpWebRequest Request = null;
        private HttpWebResponse Response = null;
        #endregion  

        /// <summary>
        /// 登录请求，属于Post请求，不带Cookie请求，保存返回的Cookies
        /// </summary>
        /// <param name="Url">向该地址传递登录请求</param>
        /// <param name="Data">登录过程中的参数</param>
        /// <returns></returns>
        public String LoginJxglxt(String Url, String Data)
        {
            Request = (HttpWebRequest)WebRequest.Create(Url);
            Byte[] bt = UTF8Encoding.UTF8.GetBytes(Data);

            Request.Method = "POST";
            Request.CookieContainer = new CookieContainer();
            Request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            //Request.Referer = "http://222.72.92.106/eams/index.do?isShowLogin=true";
            Request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.73 Safari/537.36";
            Request.ContentLength = bt.Length;
            Request.ContentType = "application/x-www-form-urlencoded";
            //Request.AllowAutoRedirect = false;//禁止自动重定向
            Request.GetRequestStream().Write(bt, 0, bt.Length);

            try
            {
                Response = (HttpWebResponse)Request.GetResponse();
                Cookies = Response.Cookies;
                using (System.IO.StreamReader Sr = new System.IO.StreamReader(Response.GetResponseStream()))
                {
                    return Sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="Url">向该地址发送Get请求</param>
        /// <returns>返回字符串</returns>
        public String GetRequest(String Url)
        {
            Request = (HttpWebRequest)WebRequest.Create(Url);

            Request.CookieContainer = new CookieContainer();
            Request.CookieContainer.Add(Cookies);
            Request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            Request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.73 Safari/537.36";
            // Request.Referer = "http://222.72.92.106/eams/defaultHome.do?method=moduleList&parentCode=";
            //Request.AllowAutoRedirect = false;//禁止自动重定向
            Response = (HttpWebResponse)Request.GetResponse();
            using (System.IO.StreamReader Sr = new System.IO.StreamReader(Response.GetResponseStream()))
            {
                var fs = Sr.ReadToEnd();
                return fs;
            }
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="Url">向改地址发送Post请求</param>
        /// <param name="Data">传递的参数</param>
        /// <returns></returns>
        public String PostRequest(String Url, String Data)
        {
            Request = (HttpWebRequest)WebRequest.Create(Url);
            Byte[] bt = UTF8Encoding.UTF8.GetBytes(Data);

            Request.Method = "POST";
            Request.CookieContainer = new CookieContainer();
            Request.CookieContainer.Add(Cookies);
            //Request.AllowAutoRedirect = false;//禁止自动重定向
            Request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            //Request.Referer = "http://222.72.92.106/eams/index.do?isShowLogin=true";
            Request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.73 Safari/537.36";
            Request.ContentLength = bt.Length;
            Request.ContentType = "application/x-www-form-urlencoded";
            Request.AllowAutoRedirect = false;
            Request.GetRequestStream().Write(bt, 0, bt.Length);

            Response = (HttpWebResponse)Request.GetResponse();
            using (System.IO.StreamReader Sr = new System.IO.StreamReader(Response.GetResponseStream()))
            {
                return Sr.ReadToEnd();
            }
        }

        /// <summary>
        ///垃圾回收
        /// </summary>
        public void Close()
        {
            if (Request != null)
            {
                Request.Abort();
            }
            if (Response != null)
            {
                Response.Close();
            }
        }
    }
}
