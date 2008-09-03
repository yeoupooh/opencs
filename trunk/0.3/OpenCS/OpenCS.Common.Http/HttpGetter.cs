using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace OpenCS.Common.Http
{
    public class HttpGetter : BaseHttpConnection, IHttpGetter
    {
        public HttpGetter(string url, CookieContainer cc)
            : base(url, cc)
        {
        }

        public HttpResult Get()
        {
            HttpResult result = new HttpResult();

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(Url);
            req.CookieContainer = Cc;
            req.Method = "GET";

            using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
            {
                using (StreamReader sr = new StreamReader(res.GetResponseStream()))
                {
                    AddCookies(Cc, req.RequestUri, res);
                }
            }

            return result;
        }
    }
}
