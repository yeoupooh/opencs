using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Http
{
    /// <summary>
    /// Interface of Getter over HTTP.
    /// </summary>
    public interface IHttpGetter : IHttpConnection
    {
        /// <summary>
        /// Gets data over HTTP
        /// </summary>
        /// <returns>data</returns>
        HttpResult Get();

        /// <summary>
        /// Starts to get data over HTTP
        /// </summary>
        /// <returns>true if data is not null</returns>
        bool BeginGet();

        /// <summary>
        /// Does next part of data over HTTP
        /// </summary>
        /// <returns>true if data is not null</returns>
        bool GetNext();

        /// <summary>
        /// Finishes to get data over HTTP
        /// </summary>
        /// <returns>whole data</returns>
        HttpResult EndGet();
    }
}
