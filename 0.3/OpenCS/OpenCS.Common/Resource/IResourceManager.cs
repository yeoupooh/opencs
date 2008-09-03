using System;
using System.Collections.Generic;
using System.Text;
using OpenCS.Common.Logging;

namespace OpenCS.Common.Resource
{
    /// <summary>
    /// 리소스 관리자 (= 리소스 제공자 + 리소스 변경자)
    /// </summary>
    public interface IResourceManager
    {
        /// <summary>
        /// 리소스 제공자를 가져온다.
        /// </summary>
        IResourceProvider ResourceProvider
        {
            get;
        }

        /// <summary>
        /// 리소스 변경자를 가져온다.
        /// </summary>
        IResourceChanger ResourceChanger
        {
            get;
        }

        /// <summary>
        /// 리소스 관리자를 초기화한다.
        /// 리소스 관리자를 사용하기 전에 반드시 초기화해야 한다.
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <param name="logger">로거</param>
        void Init(IResourceProvider rp, ILogger logger);
    }
}
