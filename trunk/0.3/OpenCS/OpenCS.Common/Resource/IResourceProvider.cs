using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Resource
{
    /// <summary>
    /// 리소스 제공자
    /// </summary>
    public interface IResourceProvider
    {
        /// <summary>
        /// 문자열 리소스를 가져온다.
        /// </summary>
        /// <param name="resourceId">리소스 아이디</param>
        /// <returns>가져온 문자열 리소스</returns>
        string GetString(string resourceId);
    }
}
