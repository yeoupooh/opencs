using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Logging
{
    /// <summary>
    /// 여러개의 로거를 등록해서 동시에 로그를 남기게 해준다. Static Singleton 클래스이다. <example>
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
        /// UI 콤포넌트를 사용하는지 여부를 가져오거나 설정한다.
        /// </summary>
        public bool HasUI
        {
            get { return m_hasUI; }
            set { m_hasUI = value; }
        }

        /// <summary>
        /// 로그 표시 레벨을 가져오거나 설정한다.
        /// </summary>
        public LogLevelOptions LevelOptions
        {
            get { return LogLevelOptions.None; } // 각 로그의 설정을 따른다.
            set { ; }
        }

        /// <summary>
        /// UI에 따른 활성화 여부를 가져오거나 설정한다.
        /// </summary>
        public LogUIOption UIOption
        {
            get { return m_uiOption; }
            set { m_uiOption = value; }
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public LogManager()
        {
            m_loggers = new List<ILogger>();
        }

        /// <summary>
        /// 로거를 추가한다.
        /// </summary>
        /// <param name="logger">로거</param>
        public void AddLogger(ILogger logger)
        {
            m_loggers.Add(logger);
        }

        /// <summary>
        /// 로거를 추가한다.
        /// </summary>
        /// <param name="logger">로거</param>
        /// <param name="options">로그 레벨 옵션</param>
        public void AddLogger(ILogger logger, LogLevelOptions options)
        {
            logger.LevelOptions = options;
            m_loggers.Add(logger);
        }

        /// <summary>
        /// 로거를 삭제한다.
        /// </summary>
        /// <param name="logger">로거</param>
        public void RemoveLogger(ILogger logger)
        {
            m_loggers.Remove(logger);
        }

        #region ILogger Members

        /// <summary>
        /// 심각한 오류 메시지를 남긴다.
        /// </summary>
        /// <param name="msg">메시지</param>
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
        /// 오류 메시지를 남긴다.
        /// </summary>
        /// <param name="msg">메시지</param>
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
        /// 정보 메시지를 남긴다.
        /// </summary>
        /// <param name="msg">메시지</param>
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
        /// 경고 메시지를 남긴다.
        /// </summary>
        /// <param name="msg">메시지</param>
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
        /// 디버깅용 메시지를 남긴다.
        /// </summary>
        /// <param name="msg">메시지</param>
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
