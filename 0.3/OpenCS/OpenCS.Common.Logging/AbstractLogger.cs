using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Logging
{
    /// <summary>
    /// 기본구현한 로거
    /// </summary>
    public abstract class AbstractLogger : ILogger
    {
        /// <summary>
        /// 로그 레벨 옵션
        /// </summary>
        protected LogLevelOptions m_levelOptions;

        /// <summary>
        /// UI 콤포넌트를 사용하는지 여부
        /// </summary>
        protected bool m_hasUI;

        /// <summary>
        /// UI에 따른 활성화 여부
        /// </summary>
        protected LogUIOption m_uiOption;

        /// <summary>
        /// 심각한 오류 메시지
        /// </summary>
        public static string LOG_PREFIX_FATAL = "FATAL";

        /// <summary>
        /// 오류 메시지
        /// </summary>
        public static string LOG_PREFIX_ERROR = "ERROR";

        /// <summary>
        /// 정보 메시지
        /// </summary>
        public static string LOG_PREFIX_INFO = "INFO ";

        /// <summary>
        /// 경고 메시지
        /// </summary>
        public static string LOG_PREFIX_WARN = "WARN ";

        /// <summary>
        /// 디버깅용 메시지
        /// </summary>
        public static string LOG_PREFIX_DEBUG = "DEBUG";

        /// <summary>
        /// 로그 메시지를 남긴다.
        /// </summary>
        /// <param name="prefix">접두어</param>
        /// <param name="msg">메시지</param>
        abstract protected void Log(string prefix, string msg);

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="hasUI">UI 콤포넌트를 사용하는지 여부</param>
        protected AbstractLogger(bool hasUI)
        {
            m_hasUI = hasUI;
        }

        #region ILogger Members

        /// <summary>
        /// 심각한 오류 메시지를 남긴다.
        /// </summary>
        /// <param name="msg">메시지</param>
        public void Fatal(string msg)
        {
            if ((LevelOptions & LogLevelOptions.Fatal) == LogLevelOptions.Fatal)
            {
                Log(LOG_PREFIX_FATAL, msg);
            }
        }

        /// <summary>
        /// 오류 메시지를 남긴다.
        /// </summary>
        /// <param name="msg">메시지</param>
        public void Error(string msg)
        {
            if ((LevelOptions & LogLevelOptions.Error) == LogLevelOptions.Error)
            {
                Log(LOG_PREFIX_ERROR, msg);
            }
        }

        /// <summary>
        /// 정보 메시지를 남긴다.
        /// </summary>
        /// <param name="msg">메시지</param>
        public void Info(string msg)
        {
            if ((LevelOptions & LogLevelOptions.Info) == LogLevelOptions.Info)
            {
                Log(LOG_PREFIX_INFO, msg);
            }
        }

        /// <summary>
        /// 경고 메시지를 남긴다.
        /// </summary>
        /// <param name="msg">메시지</param>
        public void Warn(string msg)
        {
            if ((LevelOptions & LogLevelOptions.Warn) == LogLevelOptions.Warn)
            {
                Log(LOG_PREFIX_WARN, msg);
            }
        }

        /// <summary>
        /// 디버깅용 메시지를 남긴다.
        /// </summary>
        /// <param name="msg">메시지</param>
        public void Debug(string msg)
        {
            if ((LevelOptions & LogLevelOptions.Debug) == LogLevelOptions.Debug)
            {
                Log(LOG_PREFIX_DEBUG, msg);
            }
        }

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
            get { return m_levelOptions; }
            set { m_levelOptions = value; }
        }

        /// <summary>
        /// UI에 따른 활성화 여부를 가져오거나 설정한다.
        /// </summary>
        public LogUIOption UIOption
        {
            get { return m_uiOption; }
            set { m_uiOption = value; }
        }

        #endregion
    }
}
