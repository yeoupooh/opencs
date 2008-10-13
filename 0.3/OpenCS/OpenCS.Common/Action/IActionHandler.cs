using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Action
{
    /// <summary>
    /// Interface of action handler. 액션 처리기
    /// </summary>
    public interface IActionHandler 
    {
        /// <summary>
        /// Handle action. 액션을 처리한다.
        /// </summary>
        /// <param name="action">Action. 액션</param>
        /// <returns>Result of handling action. 처리 결과</returns>
        ActionResult HandleAction(IAction action);
    }
}
