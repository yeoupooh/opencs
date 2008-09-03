using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Http
{
    public interface IHttpGetter : IHttpConnection
    {
        HttpResult Get();
    }
}
