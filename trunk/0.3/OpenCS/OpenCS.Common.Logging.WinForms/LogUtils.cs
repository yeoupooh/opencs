using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using OpenCS.Common.Resource;

namespace OpenCS.Common.Logging.WinForms
{
    /// <summary>
    /// �α� �޽����� �޽��� ���ڿ� ǥ�����ش�.
    /// </summary>
    public static class LogUtils
    {
        private static ILogMessageBox m_msgBox;

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="msgBox">�α� �޽��� ����</param>
        public static void SetLogMessagebox(ILogMessageBox msgBox)
        {
            m_msgBox = msgBox;
        }

        /// <summary>
        /// ��� �޽����� ǥ���Ѵ�. ȣ������ �α� �޽��� ����(<c>ILogMessageBox</c>)�� �����س��ƾ� �Ѵ�.
        /// </summary>
        /// <param name="rp">���ҽ� ������</param>
        /// <param name="logger">�ΰ�</param>
        /// <param name="msg">�޽���</param>
        public static void ShowWarning(IResourceProvider rp, ILogger logger, string msg)
        {
            logger.Warn(msg);
            if (m_msgBox != null)
            {
                m_msgBox.Show(msg, LogUtils.LogOptionString(rp, LogLevelOptions.Warn), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// ���� �޽����� ǥ���Ѵ�.
        /// </summary>
        /// <param name="logger">�ΰ�</param>
        /// <param name="msg">�޽���</param>
        public static void ShowError(ILogger logger, string msg)
        {
            logger.Error(msg);
        }

        /// <summary>
        /// ���� �޽����� ǥ���Ѵ�.
        /// </summary>
        /// <param name="logger">�ΰ�</param>
        /// <param name="msg">�޽���</param>
        /// <param name="e">���� ��ü(<c>Exception</c>)</param>
        public static void ShowError(ILogger logger, string msg, Exception e)
        {
            logger.Error(msg);
            logger.Debug(e.StackTrace);
        }

        /// <summary>
        /// �α� ���� �ɼ� ���ڿ��� ���Ѵ�.
        /// </summary>
        /// <param name="rp">���ҽ� ������</param>
        /// <param name="option">�α� ���� �ɼ�</param>
        /// <returns>�α� ���� �ɼ� ���ڿ�. ������ ""�� ��ȯ�Ѵ�.</returns>
        public static string LogOptionString(IResourceProvider rp, LogLevelOptions option)
        {
            switch (option)
            {
                case LogLevelOptions.Debug: return rp.GetString("LABEL_DEBUG");
                case LogLevelOptions.Info: return rp.GetString("LABEL_INFO");
                case LogLevelOptions.Warn: return rp.GetString("LABEL_WARN");
                case LogLevelOptions.Error: return rp.GetString("LABEL_ERROR");
                case LogLevelOptions.Fatal: return rp.GetString("LABEL_FATAL");
            }

            return "";
        }
    }
}
