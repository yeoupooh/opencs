using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;

namespace OpenCS.Common.Worker
{
    /// <summary>
    /// 기본 구현한 일꾼 관리자
    /// </summary>
    public class WorkerManager : IWorkerManager
    {
        private List<IWorker> m_workers = new List<IWorker>();
        private IProgressView m_pv;
        private WorkerParams m_wps = new WorkerParams();
        private WorkerResultState m_rs;
        private Exception m_error;

        #region IWorkerManager 멤버

        /// <summary>
        /// 진행상태 뷰를 설정한다.
        /// </summary>
        public IProgressView ProgressView
        {
            set { m_pv = value; }
        }

        /// <summary>
        /// 일꾼 인자를 가져온다.
        /// 일꾼 관리자 내부에서 일꾼 인자를 초기화한다.
        /// </summary>
        public WorkerParams WorkerParams
        {
            get
            {
                return m_wps;
            }
        }

        /// <summary>
        /// 일의 결과를 가져온다.
        /// 중간에 어떠한 일이라도 실패이면 중단하고, Failed로 설정한다.
        /// </summary>
        public WorkerResultState ResultState
        {
            get
            {
                return m_rs;
            }
        }

        /// <summary>
        /// 오류를 가져온다. 실패하거나 오류가 발생하였을 때에 설정된다.
        /// </summary>
        public Exception Error
        {
            get { return m_error; }
        }

        /// <summary>
        /// 일꾼을 추가한다.
        /// </summary>
        /// <param name="worker">일꾼</param>
        public void AddWorker(IWorker worker)
        {
            m_workers.Add(worker);
        }

        /// <summary>
        /// 일을 시작한다.
        /// </summary>
        public void Start()
        {
            try
            {
                try
                {
                    foreach (IWorker worker in m_workers)
                    {
                        worker.WorkerParams = m_wps;
                        if (m_pv != null)
                        {
                            m_pv.ShowProgress = worker.ShowProgress;
                            m_pv.SetMessage(worker.Title);
                            worker.ProgressView = m_pv;
                        }
                        else
                        {
                            Debug.Print("ProgressView is not set");
                            worker.ProgressView = new ConsoleProgressView();
                        }
                        worker.DoWork();

                        if (worker.ResultState == WorkerResultState.Failed)
                        {
                            m_rs = WorkerResultState.Failed;
                            return;
                        }
                    }

                    m_rs = WorkerResultState.Success;
                    return;
                }
                catch (Exception ex)
                {
                    m_error = ex;
                }

                m_rs = WorkerResultState.Failed;
            }
            finally
            {
                // 일을 마치면 초기화한다.
                m_workers.Clear();
            }
        }

        #endregion
    }
}
