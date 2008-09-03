using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Logging
{
    /// <summary>
    /// �������� �ΰŸ� ����ؼ� ���ÿ� �α׸� ����� ���ش�. Static Singleton Ŭ�����̴�. <example>
    /// StaticLogManager.AddLogger(new ConsoleLogger(), LogLevelOptions.All);
    /// StaticLogManager.Debug("test");
    /// </example>
    /// </summary>
    public class LogManager : ILogger
    {
        private List<ILogger> m_loggers;
        private bool m_hasUI;
        private LogUIOption m_uiOption;

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
            get { return LogLevelOptions.None; } // �� �α��� ������ ������.
            set { ; }
        }

        /// <summary>
        /// UI�� ���� Ȱ��ȭ ���θ� �������ų� �����Ѵ�.
        /// </summary>
        public LogUIOption UIOption
        {
            get { return m_uiOption; }
            set { m_uiOption = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public LogManager()
        {
            m_loggers = new List<ILogger>();
        }

        /// <summary>
        /// �ΰŸ� �߰��Ѵ�.
        /// </summary>
        /// <param name="logger">�ΰ�</param>
        public void AddLogger(ILogger logger)
        {
            m_loggers.Add(logger);
        }

        /// <summary>
        /// �ΰŸ� �߰��Ѵ�.
        /// </summary>
        /// <param name="logger">�ΰ�</param>
        /// <param name="options">�α� ���� �ɼ�</param>
        public void AddLogger(ILogger logger, LogLevelOptions options)
        {
            logger.LevelOptions = options;
            m_loggers.Add(logger);
        }

        /// <summary>
        /// �ΰŸ� �����Ѵ�.
        /// </summary>
        /// <param name="logger">�ΰ�</param>
        public void RemoveLogger(ILogger logger)
        {
            m_loggers.Remove(logger);
        }

        #region ILogger Members

        /// <summary>
        /// �ɰ��� ���� �޽����� �����.
        /// </summary>
        /// <param name="msg">�޽���</param>
        public void Fatal(string msg)
        {
            foreach (ILogger logger in m_loggers)
            {
                if (m_uiOption == LogUIOption.All ||
                    (m_uiOption == LogUIOption.NoUI && logger.HasUI == false) ||
                    (m_uiOption == LogUIOption.UIOnly && logger.HasUI == true))
                {
                    logger.Fatal(msg);
                }
            }
        }

        /// <summary>
        /// ���� �޽����� �����.
        /// </summary>
        /// <param name="msg">�޽���</param>
        public void Error(string msg)
        {
            foreach (ILogger logger in m_loggers)
            {
                if (m_uiOption == LogUIOption.All ||
                    (m_uiOption == LogUIOption.NoUI && logger.HasUI == false) ||
                    (m_uiOption == LogUIOption.UIOnly && logger.HasUI == true))
                {
                    logger.Error(msg);
                }
            }
        }

        /// <summary>
        /// ���� �޽����� �����.
        /// </summary>
        /// <param name="msg">�޽���</param>
        public void Info(string msg)
        {
            foreach (ILogger logger in m_loggers)
            {
                if (m_uiOption == LogUIOption.All ||
                    (m_uiOption == LogUIOption.NoUI && logger.HasUI == false) ||
                    (m_uiOption == LogUIOption.UIOnly && logger.HasUI == true))
                {
                    logger.Info(msg);
                }
            }
        }

        /// <summary>
        /// ��� �޽����� �����.
        /// </summary>
        /// <param name="msg">�޽���</param>
        public void Warn(string msg)
        {
            foreach (ILogger logger in m_loggers)
            {
                if (m_uiOption == LogUIOption.All ||
                    (m_uiOption == LogUIOption.NoUI && logger.HasUI == false) ||
                    (m_uiOption == LogUIOption.UIOnly && logger.HasUI == true))
                {
                    logger.Warn(msg);
                }
            }
        }

        /// <summary>
        /// ������ �޽����� �����.
        /// </summary>
        /// <param name="msg">�޽���</param>
        public void Debug(string msg)
        {
            foreach (ILogger logger in m_loggers)
            {
                if (m_uiOption == LogUIOption.All ||
                    (m_uiOption == LogUIOption.NoUI && logger.HasUI == false) ||
                    (m_uiOption == LogUIOption.UIOnly && logger.HasUI == true))
                {
                    logger.Debug(msg);
                }
            }
        }

        #endregion
    }
}
