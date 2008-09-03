using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OpenCS.Common.Resource
{
    /// <summary>
    /// 리소스 변경자.
    /// </summary>
    public interface IResourceChanger
    {
        /// <summary>
        /// 리소스 제공자를 설정한다.
        /// </summary>
        IResourceProvider ResourceProvider
        {
            set;
        }

        /// <summary>
        /// 리소스 변경 필터를 추가한다.
        /// </summary>
        /// <param name="filter">리소스 변경 필터</param>
        void AddFilter(IResourceChangeFilter filter);

        /// <summary>
        /// 객체의 리소스를 바꾼다. 
        /// </summary>
        /// <param name="obj">바꿀 객체</param>
        void Change(object obj);

        /// <summary>
        /// IResourceChangeable 객체의 리소스를 바꾼다.
        /// </summary>
        /// <param name="resourceChangeable">IResourceChangeable 객체</param>
        void Change(IResourceChangeable resourceChangeable);
    }
}
