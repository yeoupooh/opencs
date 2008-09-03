using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Http
{
    public interface IHttpPoster : IHttpConnection
    {
        int GetContentLength();
        void AddContent(PostContent content);
        void AddValue(string name, string value);
        void AddFile(string name, string filename);
        HttpResult Post();

    }
}
