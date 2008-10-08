using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using OpenCS.Common.Resource;

namespace OpenCS.Common.Logging.WinForms
{
    /// <summary>
    /// �޽��� ���� �ΰ�
    /// </summary>
    public class MessageBoxLogger : AbstractLogger
    {
        private IResourceProvider m_rp;
        private ILogMessageBox m_msgBox;

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="rp">���ҽ� ������</param>
        /// <param name="msgBox">�α� �޽��� ����</param>
        public MessageBoxLogger(IResourceProvider rp, ILogMessageBox msgBox)
            : base(true)
        {
            m_rp = rp;
            m_msgBox = msgBox;
        }

        /// <summary>
        /// �α� �޽����� �����.
        /// </summary>
        /// <param name="prefix">���ξ�</param>
        /// <param name="msg">�޽���</param>
        protected override void Log(string prefix, string msg)
        {
            if (m_msgBox != null)
            {
                if (prefix == LOG_PREFIX_FATAL)
                {
                    m_msgBox.Show(msg, LogUtils.LogOptionString(m_rp, LogLevelOptions.Fatal), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (prefix == LOG_PREFIX_ERROR)
                {
                    m_msgBox.Show(msg, LogUtils.LogOptionString(m_rp, LogLevelOptions.Error), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (prefix == LOG_PREFIX_WARN)
                {
                    m_msgBox.Show(msg, LogUtils.LogOptionString(m_rp, LogLevelOptions.Warn), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (prefix == LOG_PREFIX_INFO)
                {
                    m_msgBox.Show(msg, LogUtils.LogOptionString(m_rp, LogLevelOptions.Info), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
