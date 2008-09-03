using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using OpenCS.Common.Worker;

namespace OpenCS.Common.Http
{
    public class HttpFileGetter : BaseHttpConnection, IHttpGetter
    {
        private IProgressView m_pv;
        private string m_filename;

        public IProgressView ProgressView
        {
            get { return m_pv; }
            set { m_pv = value; }
        }

        public HttpFileGetter(string url, string filename)
            : base(url)
        {
            m_filename = filename;
        }

        #region IHttpGetter 멤버

        public HttpResult Get()
        {
            HttpResult result = new HttpResult();

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(Url);
            if (Cc != null)
            {
                req.CookieContainer = Cc;
            }
            req.Method = "GET";

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream());

            if (Cc != null)
            {
                AddCookies(Cc, req.RequestUri, res);
            }

            // download
            using (HttpWebResponse resDown = req.GetResponse() as HttpWebResponse)
            {
                if (resDown.ContentLength > 0)
                {
                    using (BinaryReader reader = new BinaryReader(resDown.GetResponseStream(), Encoding.UTF8))
                    {
                        using (FileStream fs = new FileStream(m_filename, FileMode.Create))
                        {
                            int downSize = 0;
                            while (true)
                            {
                                byte[] readContents = reader.ReadBytes(2048);

                                downSize += readContents.Length;
                                if (m_pv != null)
                                {
                                    m_pv.SetProgress((int)((float)downSize / resDown.ContentLength * 100));
                                }

                                if (readContents.Length == 0)
                                {
                                    break;
                                }
                                fs.Write(readContents, 0, readContents.Length);
                            }
                        }
                    }
                }
            }


            return result;
        }

        #endregion
    }
}
