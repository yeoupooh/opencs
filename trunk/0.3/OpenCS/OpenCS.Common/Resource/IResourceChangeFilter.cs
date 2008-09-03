using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Resource
{
    /// <summary>
    /// 리소스 변경 필터
    /// </summary>
    public interface IResourceChangeFilter
    {
        /// <summary>
        /// 리소스 변경에 대해 필터링한다.
        /// </summary>
        /// <param name="rc">리소스 변경자</param>
        /// <param name="obj">변경할 객체</param>
        /// <returns>변경이 될 객체이면 true</returns>
        bool Filter(IResourceChanger rc, object obj);
    }
}
