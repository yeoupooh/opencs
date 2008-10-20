using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OpenCS.Common
{
    /// <summary>
    /// 다이얼로그
    /// </summary>
    public interface IDialog : IDisposable
    {
        /// <summary>
        /// 다이얼로그 결과를 가져오거나 설정한다.
        /// </summary>
        DialogResult DialogResult
        {
            get;
            set;
        }

        /// <summary>
        /// 다이얼로그 모달로 표시한다.
        /// </summary>
        /// <returns>다이얼로그 결과</returns>
        DialogResult ShowDialog();

        /// <summary>
        /// 창을 닫는다.
        /// </summary>
        void Close();
    }
}
