using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Worker
{
    /// <summary>
    /// 기본 구현한 추상 일꾼
    /// </summary>
    abstract public class BaseWorker : IWorker
    {
        /// <summary>
        /// 일꾼 인자
        /// </summary>
        private WorkerParams m_wps;

        /// <summary>
        /// 진행상태 뷰
        /// </summary>
        private IProgressView m_pv;

        /// <summary>
        /// 일 결과
        /// </summary>
        private WorkerResultState m_rs;

        #region IWorker 멤버

        /// <summary>
        /// 일꾼 인자를 가져오거나 설정한다.
        /// 이 객체는 일꾼 관리자로부터 전달 받고, 다른 일꾼에게도 전달되기도 한다.
        /// </summary>
        public WorkerParams WorkerParams
        {
            get
            {
                return m_wps;
            }
            set
            {
                m_wps = value;
            }
        }

        /// <summary>
        /// 진행상태를 표시하는 뷰를 설정한다.
        /// </summary>
        public IProgressView ProgressView
        {
            get { return m_pv; }
            set { m_pv = value; }
        }

        /// <summary>
        /// 진행상태를 표시할지 여부를 가져온다.
        /// false이면 표시하지 않거나 marquee 형태로 표시하게 된다.
        /// </summary>
        abstract public bool ShowProgress
        {
            get;
        }

        /// <summary>
        /// 일꾼의 일 제목을 가져온다.
        /// 진행상태를 표시할 때 기본으로 표시된다.
        /// </summary>
        abstract public string Title
        {
            get;
        }

        /// <summary>
        /// 일을 마친 상태를 가져온다.
        /// </summary>
        public WorkerResultState ResultState
        {
            get { return m_rs; }
            set { m_rs = value; }
        }

        /// <summary>
        /// 일을 한다.
        /// </summary>
        abstract public void DoWork();

        #endregion
    }
}
