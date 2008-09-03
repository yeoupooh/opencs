using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace OpenCS.Common.Worker
{
    /// <summary>
    /// 일꾼 관리자 다이얼로그 콘트롤러
    /// </summary>
    public class WorkerManagerDialogController : IWorkerManagerDialogController
    {
        /// <summary>
        /// 일꾼 관리자 다이얼로그 뷰
        /// </summary>
        private IWorkerManagerDialog m_view;

        /// <summary>
        /// 일꾼 관리자 다이얼로그 모델
        /// </summary>
        private IWorkerManagerDialogModel m_model;

        private BackgroundWorker m_bgWorker = new BackgroundWorker();

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="view">일꾼 관리자 다이얼로그 뷰</param>
        /// <param name="model">일꾼 관리자 다이얼로그 모델</param>
        public WorkerManagerDialogController(IWorkerManagerDialog view, IWorkerManagerDialogModel model)
        {
            m_view = view;
            m_model = model;

            m_bgWorker.DoWork += new DoWorkEventHandler(m_bgWorker_DoWork);
            m_bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(m_bgWorker_RunWorkerCompleted);

            m_view.Load += new EventHandler(OnLoad);

        }

        void m_bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            m_view.Close();
        }

        void m_bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            m_model.WorkerManager.Start();
        }

        #region IWorkerManagerDialogController 멤버

        /// <summary>
        /// 로딩되었을 때의 처리를 한다.
        /// 이 시점에서 일꾼 관리자를 시작한다.
        /// </summary>
        /// <param name="sender">이벤트 전달자</param>
        /// <param name="e">이벤트 인자</param>
        public void OnLoad(object sender, EventArgs e)
        {
            m_model.WorkerManager.ProgressView = m_view;
            m_view.ShowProgress = m_model.ShowProgress;
            m_bgWorker.RunWorkerAsync();
        }

        #endregion
    }
}
