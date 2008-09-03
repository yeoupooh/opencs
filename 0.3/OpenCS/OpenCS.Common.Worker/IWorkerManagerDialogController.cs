using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Worker
{
    /// <summary>
    /// 일꾼 관리자 다이얼로그 콘트롤러
    /// </summary>
    public interface IWorkerManagerDialogController
    {
        /// <summary>
        /// 로딩되었을 때의 처리를 한다.
        /// 이 시점에서 일꾼 관리자를 시작한다.
        /// </summary>
        /// <param name="sender">이벤트 전달자</param>
        /// <param name="e">이벤트 인자</param>
        void OnLoad(object sender, EventArgs e);
    }
}
