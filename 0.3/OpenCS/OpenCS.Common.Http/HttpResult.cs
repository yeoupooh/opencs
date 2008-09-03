using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Http
{
    public class HttpResult
    {
        private Uri m_redirect;

        public Uri Redirect
        {
            get { return m_redirect; }
            set { m_redirect = value; }
        }
        private string m_content;

        public string Content
        {
            get { return m_content; }
            set { m_content = value; }
        }
    }
}
