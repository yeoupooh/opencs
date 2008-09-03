using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OpenCS.Common.Worker
{
    /// <summary>
    /// 일꾼 관리자. 
    /// 일꾼들에게 일을 하게 한다. 비동기는 지원하지 않는다.
    /// </summary>
    public interface IWorkerManager
    {
        /// <summary>
        /// 진행상태 뷰를 설정한다.
        /// </summary>
        IProgressView ProgressView
        {
            set;
        }

        /// <summary>
        /// 일꾼 인자를 가져온다.
        /// 일꾼 관리자 내부에서 일꾼 인자를 초기화한다.
        /// </summary>
        WorkerParams WorkerParams
        {
            get;
        }

        /// <summary>
        /// 일의 결과를 가져온다.
        /// 중간에 어떠한 일이라도 실패이면 중단하고, Failed로 설정한다.
        /// </summary>
        WorkerResultState ResultState
        {
            get;
        }

        /// <summary>
        /// 오류를 가져온다. 실패하거나 오류가 발생하였을 때에 설정된다.
        /// </summary>
        Exception Error
        {
            get;
        }

        /// <summary>
        /// 일꾼을 추가한다.
        /// </summary>
        /// <param name="worker">일꾼</param>
        void AddWorker(IWorker worker);

        /// <summary>
        /// 일을 시작한다.
        /// </summary>
        void Start();
    }
}
