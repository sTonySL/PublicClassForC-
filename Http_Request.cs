using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace jwc
{
    /// <summary>
    /// 通过传递一个URL属性，可以获得一个get请求和post请求
    /// </summary>
    class Http_Request
    {
        private String url = "";
        /// <summary>
        /// url的get。set方法
        /// </summary>
        public String Url 
        {
            get { return url;}
            set { url = value;}
        }

        private CookieCollection cookies=null;
        public CookieCollection Cookies 
        {
            get { return cookies; }
            set { cookies = value; }
        }

        //创建http请求的对象
        private HttpWebRequest request = null;
        private HttpWebResponse response = null;
        private StreamReader streamreader = null;

        /// <summary>
        /// 构造GET请求
        /// </summary>
        /// <returns>请求成功则返回响应的字符串，请求失败则返回“”</returns>
        public Stream GetRequest() 
        {
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);

                //设置cookie
                if (cookies!=null)
                {
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(cookies);
                }

                //发送get请求
                response = (HttpWebResponse)request.GetResponse();

                return response.GetResponseStream();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }


        /// <summary>
        /// 构造POST请求,返回stream
        /// </summary>
        /// <param name="PostData">POST请求中传递的数据</param>
        /// <param name="contentType">POST请求中传递的数据的类型</param>
        /// <returns></returns>
        public Stream PostRequest(String PostData, String contentType)
        {
            request = (HttpWebRequest)WebRequest.Create(url);
            //对参数进行字节编码
            Byte[] PostData_Byte = UTF8Encoding.UTF8.GetBytes(PostData);
            //构造post请求
            request.Method = "POST";
            request.ContentLength = PostData_Byte.Length;
            request.ContentType = contentType;

            //设置cookie
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }

            //将发送的数据写入请求流中
            request.GetRequestStream().Write(PostData_Byte,0,PostData_Byte.Length);

            //发送请求并得到响应的数据
            try
            {
                //从返回的流中读出所有的字符串
                response = (HttpWebResponse)request.GetResponse();
                //得到返回的cookie
                cookies = response.Cookies;

                return response.GetResponseStream();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            request.Abort();
            response.Close();
            streamreader.Close();
            //强制释放资源
            System.GC.Collect();
        }
    }
}
