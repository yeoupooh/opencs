using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using OpenCS.Common;
using OpenCS.Common.Resource;

namespace OpenCS.Common.WinForms
{
    /// <summary>
    /// 기본 구현한 메시지 다이얼로그
    /// </summary>
    public class MessageDialog : IMessageDialog
    {
        private IResourceProvider m_rp;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        public MessageDialog(IResourceProvider rp)
        {
            m_rp = rp;
        }

        #region IMessageDialog 멤버

        /// <summary>
        /// 정보 메시지를 표시한다.
        /// </summary>
        /// <param name="msg">메시지</param>
        public void ShowInfo(string msg)
        {
            MessageBox.Show(msg, m_rp.GetString("LABEL_INFO"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 경고 메시지를 표시한다.
        /// </summary>
        /// <param name="msg">메시지</param>
        public void ShowWarn(string msg)
        {
            MessageBox.Show(msg, m_rp.GetString("LABEL_WARN"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 오류 메시지를 표시한다.
        /// </summary>
        /// <param name="msg">메시지</param>
        public void ShowError(string msg)
        {
            MessageBox.Show(msg, m_rp.GetString("LABEL_ERROR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 질문을 메시지를 표시하고, 답변을 받는다.
        /// </summary>
        /// <param name="msg">메시지</param>
        /// <returns>답변</returns>
        public DialogResult ShowQuestion(string msg)
        {
            return ShowQuestion(msg, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// 질문을 메시지를 표시하고, 답변을 받는다.
        /// </summary>
        /// <param name="msg">메시지</param>
        /// <param name="buttons">메시지 버튼 형식</param>
        /// <returns>답변</returns>
        public DialogResult ShowQuestion(string msg, MessageBoxButtons buttons)
        {
            return ShowQuestion(msg, buttons, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// 질문을 메시지를 표시하고, 답변을 받는다.
        /// </summary>
        /// <param name="msg">메시지</param>
        /// <param name="buttons">메시지 버튼 형식</param>
        /// <param name="defaultButton">기본적으로 선택한 버튼</param>
        /// <returns>답변</returns>
        public DialogResult ShowQuestion(string msg, MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton)
        {
            return MessageBox.Show(msg, m_rp.GetString("LABEL_QUESTION"), buttons, MessageBoxIcon.Question, defaultButton);
        }

        #endregion
    }
}
