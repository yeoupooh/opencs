using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Action
{
    /// <summary>
    /// 액션 처리 결과
    /// </summary>
    public enum ActionResult
    {
        /// <summary>
        /// 처리 하지 않음
        /// </summary>
        NotHandled,

        /// <summary>
        /// 성공적으로 처리함
        /// </summary>
        Success,

        /// <summary>
        /// 처리에 실패함
        /// </summary>
        Failed
    }
}
