using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Action
{
    /// <summary>
    /// 액션 처리기
    /// </summary>
    public interface IActionHandler 
    {
        /// <summary>
        /// 액션을 처리한다.
        /// </summary>
        /// <param name="action">액션</param>
        /// <returns>처리 결과</returns>
        ActionResult HandleAction(IAction action);
    }
}
