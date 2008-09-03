using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Worker
{
    /// <summary>
    /// 기본 구현한 일꾼 관리자 다이얼로그 모델.
    /// </summary>
    public class WorkerManagerDialogModel : IWorkerManagerDialogModel
    {
        /// <summary>
        /// 진행상태 표시 여부
        /// </summary>
        private bool m_showProgress;

        /// <summary>
        /// 일꾼 관리자
        /// 내부적으로 초기화한다.
        /// </summary>
        private IWorkerManager m_wm = new WorkerManager();

        #region IWorkerManagerDialogModel 멤버

        /// <summary>
        /// 진행상태를 표시할지 여부를 가져오거나 설정한다.
        /// </summary>
        public bool ShowProgress
        {
            get
            {
                return m_showProgress;
            }
            set
            {
                m_showProgress = value;
            }
        }

        /// <summary>
        /// 일꾼 관리자를 가져오거나 설정한다.
        /// </summary>
        public IWorkerManager WorkerManager
        {
            get
            {
                return m_wm;
            }
            set
            {
                m_wm = value;
            }
        }

        #endregion
    }
}
