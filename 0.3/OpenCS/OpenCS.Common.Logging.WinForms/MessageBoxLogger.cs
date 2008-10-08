using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using OpenCS.Common.Resource;

namespace OpenCS.Common.Logging.WinForms
{
    /// <summary>
    /// 메시지 상자 로거
    /// </summary>
    public class MessageBoxLogger : AbstractLogger
    {
        private IResourceProvider m_rp;
        private ILogMessageBox m_msgBox;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <param name="msgBox">로그 메시지 상자</param>
        public MessageBoxLogger(IResourceProvider rp, ILogMessageBox msgBox)
            : base(true)
        {
            m_rp = rp;
            m_msgBox = msgBox;
        }

        /// <summary>
        /// 로그 메시지를 남긴다.
        /// </summary>
        /// <param name="prefix">접두어</param>
        /// <param name="msg">메시지</param>
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
