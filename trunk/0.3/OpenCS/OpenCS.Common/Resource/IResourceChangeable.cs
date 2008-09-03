using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Resource
{
    /// <summary>
    /// 리소스가 변경가능한 객체
    /// </summary>
    public interface IResourceChangeable
    {
        /// <summary>
        /// 텍스트를 가져오거나 설정한다.
        /// </summary>
        string Text
        {
            get;
            set;
        }
    }
}
