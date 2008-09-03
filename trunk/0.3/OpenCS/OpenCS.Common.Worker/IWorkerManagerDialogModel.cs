using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Worker
{
    /// <summary>
    /// 일꾼 관리자 다이얼로그 모델
    /// </summary>
    public interface IWorkerManagerDialogModel
    {
        /// <summary>
        /// 진행상태를 표시할지 여부를 가져오거나 설정한다.
        /// </summary>
        bool ShowProgress
        {
            get;
            set;
        }

        /// <summary>
        /// 일꾼 관리자를 가져오거나 설정한다.
        /// </summary>
        IWorkerManager WorkerManager
        {
            get;
            set;
        }

    }
}
