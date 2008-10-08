using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OpenCS.Common.Logging.WinForms
{
    /// <summary>
    /// <c>ToolStripItem</c>용 로거
    /// </summary>
    public class ToolStripItemLogger : AbstractLogger
    {
        private ToolStripItem m_item;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="item"><c>ToolStripItem</c> 객체</param>
        public ToolStripItemLogger(ToolStripItem item)
            : base(true)
        {
            m_item = item;
        }

        /// <summary>
        /// 로그 메시지를 남긴다.
        /// </summary>
        /// <param name="prefix">접두어</param>
        /// <param name="msg">메시지</param>
        protected override void Log(string prefix, string msg)
        {
            m_item.Text = msg;
            m_item.Owner.Refresh();
        }
    }
}
