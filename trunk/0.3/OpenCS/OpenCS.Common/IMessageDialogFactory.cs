using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common
{
    /// <summary>
    /// 메시지 다이얼로그(IMessageDialogFactory) 공장
    /// </summary>
    public interface IMessageDialogFactory
    {
        /// <summary>
        /// 메시지 다이얼로그를 생성한다.
        /// </summary>
        /// <returns>생성된 메시지 다이얼로그</returns>
        IMessageDialog CreateMessageDialog();
    }
}
