using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace OpenCS.Common.Logging
{
    /// <summary>
    /// 콘솔로 메시지를 남기는 로거
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        private LogLevelOptions mLevelOptions;
        private LogUIOption mUIOption;

        #region ILogger 멤버

        /// <summary>
        /// 심각한 오류 메시지를 남긴다.
        /// </summary>
        /// <param name="message">메시지</param>
        public void Fatal(string message)
        {
            System.Diagnostics.Debug.Print("[FATAL] " + message);
        }

        /// <summary>
        /// 오류 메시지를 남긴다.
        /// </summary>
        /// <param name="message">메시지</param>
        public void Error(string message)
        {
            System.Diagnostics.Debug.Print("[ERROR] " + message);
        }

        /// <summary>
        /// 경고 메시지를 남긴다.
        /// </summary>
        /// <param name="message">메시지</param>
        public void Warn(string message)
        {
            System.Diagnostics.Debug.Print("[WARN ] " + message);
        }

        /// <summary>
        /// 정보 메시지를 남긴다.
        /// </summary>
        /// <param name="message">메시지</param>
        public void Info(string message)
        {
            System.Diagnostics.Debug.Print("[INFO ] " + message);
        }

        /// <summary>
        /// 디버깅 메시지를 남긴다.
        /// </summary>
        /// <param name="message">메시지</param>
        public void Debug(string message)
        {
            System.Diagnostics.Debug.Print("[DEBUG] " + message);
        }

        #endregion

        #region ILogger 멤버

        public bool HasUI
        {
            get { return false; }
            set { ; }
        }

        public LogLevelOptions LevelOptions
        {
            get { return mLevelOptions; }
            set { mLevelOptions = value; }
        }

        public LogUIOption UIOption
        {
            get { return mUIOption; }
            set { mUIOption = value; }
        }

        #endregion
    }
}
