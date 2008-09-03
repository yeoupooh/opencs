using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Worker
{
    /// <summary>
    /// 어떤 일을 하는 일꾼
    /// 주로 시간이 많이 걸리는 일을 한다.
    /// </summary>
    public interface IWorker
    {
        /// <summary>
        /// 일꾼 인자를 가져오거나 설정한다.
        /// 이 객체는 일꾼 관리자로부터 전달 받고, 다른 일꾼에게도 전달되기도 한다.
        /// </summary>
        WorkerParams WorkerParams
        {
            get;
            set;
        }

        /// <summary>
        /// 진행상태를 표시하는 뷰를 가져오거나 설정한다.
        /// </summary>
        IProgressView ProgressView
        {
            get;
            set;
        }

        /// <summary>
        /// 진행상태를 표시할지 여부를 가져온다.
        /// false이면 표시하지 않거나 marquee 형태로 표시하게 된다.
        /// </summary>
        bool ShowProgress
        {
            get;
        }

        /// <summary>
        /// 일꾼의 일 제목을 가져온다.
        /// 진행상태를 표시할 때 기본으로 표시된다.
        /// </summary>
        string Title
        {
            get;
        }

        /// <summary>
        /// 일을 마친 상태를 가져오거나 설정한다.
        /// </summary>
        WorkerResultState ResultState
        {
            get;
            set;
        }

        /// <summary>
        /// 일을 한다.
        /// </summary>
        void DoWork();
    }
}
