using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Resource
{
    /// <summary>
    /// 리소스 제공자를 사용하는 객체
    /// </summary>
    public interface IResourceProvidable
    {
        /// <summary>
        /// 리소스 제공자를 설정한다.
        /// </summary>
        IResourceProvider ResourceProvider
        {
            set;
        }
    }
}
