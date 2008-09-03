using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Worker
{
    /// <summary>
    /// 진행상태를 표시하는 뷰
    /// </summary>
    public interface IProgressView
    {
        /// <summary>
        /// 진행상태를 표시할 지 여부를 설정한다.
        /// </summary>
        bool ShowProgress
        {
            set;
        }

        /// <summary>
        /// 진행률을 설정한다.
        /// </summary>
        /// <param name="value">진행률 (0~100 사이의 정수)</param>
        void SetProgress(int value);

        /// <summary>
        /// 뷰에 표시할 메시지를 설정한다.
        /// </summary>
        /// <param name="message">메시지</param>
        void SetMessage(string message);
    }
}
