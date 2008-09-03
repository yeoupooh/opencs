using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using OpenCS.Common.Resource;

namespace OpenCS.Common
{
    /// <summary>
    /// 메시지를 표시하는 다이얼로그
    /// </summary>
    public interface IMessageDialog
    {
        /// <summary>
        /// 정보 메시지를 표시한다.
        /// </summary>
        /// <param name="msg">메시지</param>
        void ShowInfo(string msg);

        /// <summary>
        /// 경고 메시지를 표시한다.
        /// </summary>
        /// <param name="msg">메시지</param>
        void ShowWarn(string msg);

        /// <summary>
        /// 오류 메시지를 표시한다.
        /// </summary>
        /// <param name="msg">메시지</param>
        void ShowError(string msg);

        /// <summary>
        /// 질문을 메시지를 표시하고, 답변을 받는다.
        /// </summary>
        /// <param name="msg">메시지</param>
        /// <returns>답변</returns>
        DialogResult ShowQuestion(string msg);

        /// <summary>
        /// 질문을 메시지를 표시하고, 답변을 받는다.
        /// </summary>
        /// <param name="msg">메시지</param>
        /// <param name="buttons">메시지 버튼 형식</param>
        /// <returns>답변</returns>
        DialogResult ShowQuestion(string msg, MessageBoxButtons buttons);

        /// <summary>
        /// 질문을 메시지를 표시하고, 답변을 받는다.
        /// </summary>
        /// <param name="msg">메시지</param>
        /// <param name="buttons">메시지 버튼 형식</param>
        /// <param name="defaultButton">기본적으로 선택한 버튼</param>
        /// <returns>답변</returns>
        DialogResult ShowQuestion(string msg, MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton);
    }
}
