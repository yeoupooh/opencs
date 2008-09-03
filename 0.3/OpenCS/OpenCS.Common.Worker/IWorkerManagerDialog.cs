using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OpenCS.Common.Worker
{
    /// <summary>
    /// 일꾼 관리자 다이얼로그.
    /// 일꾼 관리자 다이얼로그의 진행상태 뷰는 다이얼로그 자체가 된다.
    /// </summary>
    public interface IWorkerManagerDialog : IWorkerManager, IProgressView
    {
        /// <summary>
        /// 일꾼 관리자 다이얼로그 콘트롤러를 가져온다.
        /// </summary>
        IWorkerManagerDialogController Controller
        {
            get;
        }

        /// <summary>
        /// 일꾼 관리자 다이얼로그 모델을 가져온다.
        /// </summary>
        IWorkerManagerDialogModel Model
        {
            get;
        }

        /// <summary>
        /// 다이얼로그가 로딩되었다는 이벤트 핸들러.
        /// 이 벤트 발생후 일꾼 관리자는 일을 시작한다.
        /// </summary>
        event EventHandler Load;

        /// <summary>
        /// 다이얼로그를 모달로 표시한다.
        /// </summary>
        /// <returns></returns>
        DialogResult ShowDialog();

        /// <summary>
        /// 다이얼로그를 닫는다.
        /// </summary>
        void Close();
    }
}
