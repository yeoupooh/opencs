using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OpenCS.Common.Logging.WinForms
{
    /// <summary>
    /// <c>ToolStripItem</c>�� �ΰ�
    /// </summary>
    public class ToolStripItemLogger : AbstractLogger
    {
        private ToolStripItem m_item;

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="item"><c>ToolStripItem</c> ��ü</param>
        public ToolStripItemLogger(ToolStripItem item)
            : base(true)
        {
            m_item = item;
        }

        /// <summary>
        /// �α� �޽����� �����.
        /// </summary>
        /// <param name="prefix">���ξ�</param>
        /// <param name="msg">�޽���</param>
        protected override void Log(string prefix, string msg)
        {
            m_item.Text = msg;
            m_item.Owner.Refresh();
        }
    }
}
