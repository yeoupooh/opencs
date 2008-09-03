using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace OpenCS.Common.Http
{
    public interface IHttpConnection
    {
        void AddCookies(CookieContainer cc, Uri uri, HttpWebResponse res);
    }
}
