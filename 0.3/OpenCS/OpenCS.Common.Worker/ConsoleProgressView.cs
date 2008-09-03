using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace OpenCS.Common.Worker
{
    /// <summary>
    /// 콘솔로 진행상태를 표시하는 뷰.
    /// 진행상태 뷰를 설정하지 않는 경우에 자동으로 설정된다.
    /// </summary>
    public class ConsoleProgressView : IProgressView
    {
        #region IProgressView 멤버

        /// <summary>
        /// 진행상태를 표시할 지 여부를 설정한다.
        /// </summary>
        public bool ShowProgress
        {
            set { ;}
        }

        /// <summary>
        /// 진행률을 설정한다.
        /// </summary>
        /// <param name="value">진행률 (0~100 사이의 정수)</param>
        public void SetProgress(int value)
        {
            Debug.Print("DummyProgressView: progress=" + value.ToString());
        }

        /// <summary>
        /// 뷰에 표시할 메시지를 설정한다.
        /// </summary>
        /// <param name="message">메시지</param>
        public void SetMessage(string message)
        {
            Debug.Print("DummyProgressView: message=" + message);
        }

        #endregion
    }
}
