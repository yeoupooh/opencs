using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace OpenCS.Common.Http
{
    abstract public class BaseHttpPoster : BaseHttpConnection, IHttpPoster
    {
        private List<PostContent> m_contents = new List<PostContent>();

        public List<PostContent> Contents
        {
            get { return m_contents; }
        }

        protected BaseHttpPoster(string url, Encoding encoding, string userAgent)
            : base(url, encoding, userAgent)
        {
        }

        protected BaseHttpPoster(string url, Encoding encoding, string userAgent, CookieContainer cc)
            : base(url, encoding, userAgent, cc)
        {
        }

        public void AddContent(PostContent content)
        {
            m_contents.Add(content);
        }

        public void AddValue(string name, string value)
        {
            PostContent content = new PostContent(name, value, Encoding);
            m_contents.Add(content);
        }

        #region IHttpPoster 멤버

        abstract public int GetContentLength();
        abstract public HttpResult Post();

        public void AddFile(string name, string filename)
        {
            string ext = Path.GetExtension(filename).ToLower();
            string contentType = "application/octet-stream";

            if (ext == ".png")
            {
                contentType = "image/x-png";
            }
            else if (ext == ".zip" || ext == ".mwx" || ext == ".mdk")
            {
                contentType = "application/x-zip-compressed";
            }

            PostContent content = new PostContent(name, contentType, filename);
            m_contents.Add(content);
        }

        #endregion
    }
}
