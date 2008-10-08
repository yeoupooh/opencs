using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using OpenCS.Common.Resource;

namespace OpenCS.Common.Logging.WinForms
{
    /// <summary>
    /// 로그 메시지를 메시지 상자에 표시해준다.
    /// </summary>
    public static class LogUtils
    {
        private static ILogMessageBox m_msgBox;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="msgBox">로그 메시지 상자</param>
        public static void SetLogMessagebox(ILogMessageBox msgBox)
        {
            m_msgBox = msgBox;
        }

        /// <summary>
        /// 경고 메시지를 표시한다. 호출전에 로그 메시지 상자(<c>ILogMessageBox</c>)를 설정해놓아야 한다.
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <param name="logger">로거</param>
        /// <param name="msg">메시지</param>
        public static void ShowWarning(IResourceProvider rp, ILogger logger, string msg)
        {
            logger.Warn(msg);
            if (m_msgBox != null)
            {
                m_msgBox.Show(msg, LogUtils.LogOptionString(rp, LogLevelOptions.Warn), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 정보 메시지를 표시한다.
        /// </summary>
        /// <param name="logger">로거</param>
        /// <param name="msg">메시지</param>
        public static void ShowError(ILogger logger, string msg)
        {
            logger.Error(msg);
        }

        /// <summary>
        /// 오류 메시지를 표시한다.
        /// </summary>
        /// <param name="logger">로거</param>
        /// <param name="msg">메시지</param>
        /// <param name="e">오류 객체(<c>Exception</c>)</param>
        public static void ShowError(ILogger logger, string msg, Exception e)
        {
            logger.Error(msg);
            logger.Debug(e.StackTrace);
        }

        /// <summary>
        /// 로그 레벨 옵션 문자열을 구한다.
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <param name="option">로그 레벨 옵션</param>
        /// <returns>로그 레벨 옵션 문자열. 없으면 ""을 반환한다.</returns>
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
