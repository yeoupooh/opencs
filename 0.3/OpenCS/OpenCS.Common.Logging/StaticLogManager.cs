using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Logging
{
    /// <summary>
    /// 로거 관리자를 Static Singleton 클래스로 동작하게 한다
    /// </summary>
    public static class StaticLogManager
    {
        private static LogManager m_instance = null;

        /// <summary>
        /// 로거 관리자를 구한다.
        /// </summary>
        /// <returns>로거 관리자 (Static Singleton)</returns>
        public static LogManager GetLogManager()
        {
            if (m_instance == null)
            {
                m_instance = new LogManager();
            }

            return m_instance;
        }

        /// <summary>
        /// 로거를 추가한다.
        /// </summary>
        /// <param name="logger">로거</param>
        /// <param name="options">로그 레벨 옵션</param>
        public static void AddLogger(ILogger logger, LogLevelOptions options)
        {
            LogManager lm = GetLogManager();
            logger.LevelOptions = options;
            lm.AddLogger(logger);
        }

        /// <summary>
        /// 로거를 삭제한다.
        /// </summary>
        /// <param name="logger">로거</param>
        public static void RemoveLogger(ILogger logger)
        {
            LogManager lm = GetLogManager();
            lm.RemoveLogger(logger);
        }

        /// <summary>
        /// 심각한 오류 메시지를 남긴다.
        /// </summary>
        /// <param name="msg">메시지</param>
        public static void Fatal(string msg)
        {
            LogManager lm = GetLogManager();
            lm.Fatal(msg);
        }

        /// <summary>
        /// 오류 메시지를 남긴다.
        /// </summary>
        /// <param name="msg">메시지</param>
        public static void Error(string msg)
        {
            LogManager lm = GetLogManager();
            lm.Error(msg);
        }

        /// <summary>
        /// 정보 메시지를 남긴다.
        /// </summary>
        /// <param name="msg">메시지</param>
        public static void Info(string msg)
        {
            LogManager lm = GetLogManager();
            lm.Info(msg);
        }

        /// <summary>
        /// 경고 메시지를 남긴다.
        /// </summary>
        /// <param name="msg">메시지</param>
        public static void Warn(string msg)
        {
            LogManager lm = GetLogManager();
            lm.Warn(msg);
        }

        /// <summary>
        /// 디버깅용 메시지를 남긴다.
        /// </summary>
        /// <param name="msg">메시지</param>
        public static void Debug(string msg)
        {
            LogManager lm = GetLogManager();
            lm.Debug(msg);
        }
    }
}
