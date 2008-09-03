using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Diagnostics;

namespace OpenCS.Common.Http
{
    abstract public class BaseHttpConnection : IHttpConnection
    {
        private string m_url;
        private Encoding m_encoding;
        private string m_userAgent;
        private CookieContainer m_cc;

        public string Url
        {
            get { return m_url; }
            set { m_url = value; }
        }

        public Encoding Encoding
        {
            get { return m_encoding; }
            set { m_encoding = value; }
        }

        public string UserAgent
        {
            get { return m_userAgent; }
            set { m_userAgent = value; }
        }

        public CookieContainer Cc
        {
            get { return m_cc; }
            set { m_cc = value; }
        }

        protected BaseHttpConnection(string url)
        {
            m_url = url;
        }

        protected BaseHttpConnection(string url, CookieContainer cc)
        {
            m_url = url;
            m_cc = cc;
        }

        protected BaseHttpConnection(string url, Encoding encoding, string userAgent)
        {
            m_url = url;
            m_encoding = encoding;
            m_userAgent = userAgent;
        }

        protected BaseHttpConnection(string url, Encoding encoding, string userAgent, CookieContainer cc)
        {
            m_url = url;
            m_encoding = encoding;
            m_userAgent = userAgent;
            m_cc = cc;
        }

        private void ViewCookies(CookieCollection cc)
        {
            // Display the cookies. 
            Console.WriteLine("Number of cookies: " +
                                cc.Count);
            Console.WriteLine("{0,-20}{1,-20}{2}", "Name", "Value", "Domain");

            for (int i = 0; i < cc.Count; i++)
                Console.WriteLine("{0, -20}{1,-20}{2}",
                                   cc[i].Name,
                                   cc[i].Value,
                                   cc[i].Domain
                                   );
        }

        #region IHttpConnection 멤버

        public void AddCookies(CookieContainer cc, Uri uri, HttpWebResponse res)
        {
            ViewCookies(res.Cookies);

            foreach (string header in res.Headers)
            {
                if (header == "Location")
                {
                    Debug.Print("location=" + res.Headers[header]);
                }
                if (header == "Set-Cookie")
                {
                    Debug.Print("set-cookie=" + res.Headers[header]);
                    cc.SetCookies(res.ResponseUri, res.Headers[header]);
                    //cc.Add(res.Cookies);
                }
            }
        }

        #endregion
    }
}
