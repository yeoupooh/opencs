using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Logging
{
    /// <summary>
    /// �⺻������ �ΰ�
    /// </summary>
    public abstract class AbstractLogger : ILogger
    {
        /// <summary>
        /// �α� ���� �ɼ�
        /// </summary>
        protected LogLevelOptions m_levelOptions;

        /// <summary>
        /// UI ������Ʈ�� ����ϴ��� ����
        /// </summary>
        protected bool m_hasUI;

        /// <summary>
        /// UI�� ���� Ȱ��ȭ ����
        /// </summary>
        protected LogUIOption m_uiOption;

        /// <summary>
        /// �ɰ��� ���� �޽���
        /// </summary>
        public static string LOG_PREFIX_FATAL = "FATAL";

        /// <summary>
        /// ���� �޽���
        /// </summary>
        public static string LOG_PREFIX_ERROR = "ERROR";

        /// <summary>
        /// ���� �޽���
        /// </summary>
        public static string LOG_PREFIX_INFO = "INFO ";

        /// <summary>
        /// ��� �޽���
        /// </summary>
        public static string LOG_PREFIX_WARN = "WARN ";

        /// <summary>
        /// ������ �޽���
        /// </summary>
        public static string LOG_PREFIX_DEBUG = "DEBUG";

        /// <summary>
        /// �α� �޽����� �����.
        /// </summary>
        /// <param name="prefix">���ξ�</param>
        /// <param name="msg">�޽���</param>
        abstract protected void Log(string prefix, string msg);

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="hasUI">UI ������Ʈ�� ����ϴ��� ����</param>
        protected AbstractLogger(bool hasUI)
        {
            m_hasUI = hasUI;
        }

        #region ILogger Members

        /// <summary>
        /// �ɰ��� ���� �޽����� �����.
        /// </summary>
        /// <param name="msg">�޽���</param>
        public void Fatal(string msg)
        {
            if ((LevelOptions & LogLevelOptions.Fatal) == LogLevelOptions.Fatal)
            {
                Log(LOG_PREFIX_FATAL, msg);
            }
        }

        /// <summary>
        /// ���� �޽����� �����.
        /// </summary>
        /// <param name="msg">�޽���</param>
        public void Error(string msg)
        {
            if ((LevelOptions & LogLevelOptions.Error) == LogLevelOptions.Error)
            {
                Log(LOG_PREFIX_ERROR, msg);
            }
        }

        /// <summary>
        /// ���� �޽����� �����.
        /// </summary>
        /// <param name="msg">�޽���</param>
        public void Info(string msg)
        {
            if ((LevelOptions & LogLevelOptions.Info) == LogLevelOptions.Info)
            {
                Log(LOG_PREFIX_INFO, msg);
            }
        }

        /// <summary>
        /// ��� �޽����� �����.
        /// </summary>
        /// <param name="msg">�޽���</param>
        public void Warn(string msg)
        {
            if ((LevelOptions & LogLevelOptions.Warn) == LogLevelOptions.Warn)
            {
                Log(LOG_PREFIX_WARN, msg);
            }
        }

        /// <summary>
        /// ������ �޽����� �����.
        /// </summary>
        /// <param name="msg">�޽���</param>
        public void Debug(string msg)
        {
            if ((LevelOptions & LogLevelOptions.Debug) == LogLevelOptions.Debug)
            {
                Log(LOG_PREFIX_DEBUG, msg);
            }
        }

        /// <summary>
        /// UI ������Ʈ�� ����ϴ��� ���θ� �������ų� �����Ѵ�.
        /// </summary>
        public bool HasUI
        {
            get { return m_hasUI; }
            set { m_hasUI = value; }
        }

        /// <summary>
        /// �α� ǥ�� ������ �������ų� �����Ѵ�.
        /// </summary>
        public LogLevelOptions LevelOptions
        {
            get { return m_levelOptions; }
            set { m_levelOptions = value; }
        }

        /// <summary>
        /// UI�� ���� Ȱ��ȭ ���θ� �������ų� �����Ѵ�.
        /// </summary>
        public LogUIOption UIOption
        {
            get { return m_uiOption; }
            set { m_uiOption = value; }
        }

        #endregion
    }
}
