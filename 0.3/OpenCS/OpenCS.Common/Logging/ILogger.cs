using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Logging
{
    /// <summary>
    /// 로그를 남기는 객체
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// UI 콤포넌트를 사용하는지 여부를 가져오거나 설정한다.
        /// </summary>
        bool HasUI
        {
            get;
            set;
        }

        /// <summary>
        /// 로그 표시 레벨을 가져오거나 설정한다.
        /// </summary>
        LogLevelOptions LevelOptions
        {
            get;
            set;
        }

        /// <summary>
        /// UI에 따른 활성화 여부를 가져오거나 설정한다.
        /// </summary>
        LogUIOption UIOption
        {
            get;
            set;
        }

        /// <summary>
        /// 심각한 오류 메시지를 남긴다.
        /// </summary>
        /// <param name="msg">메시지</param>
        void Fatal(string msg);

        /// <summary>
        /// 오류 메시지를 남긴다.
        /// </summary>
        /// <param name="msg">메시지</param>
        void Error(string msg);

        /// <summary>
        /// 정보 메시지를 남긴다.
        /// </summary>
        /// <param name="msg">메시지</param>
        void Info(string msg);

        /// <summary>
        /// 경고 메시지를 남긴다.
        /// </summary>
        /// <param name="msg">메시지</param>
        void Warn(string msg);

        /// <summary>
        /// 디버깅용 메시지를 남긴다.
        /// </summary>
        /// <param name="msg">메시지</param>
        void Debug(string msg);

    }

    /// <summary>
    /// 로그 표시 레벨 옵션 플래그
    /// </summary>
    [Flags]
    public enum LogLevelOptions
    {
        /// <summary>
        /// 아무것도 남기지 않음
        /// </summary>
        None = 0x0,

        /// <summary>
        /// 심각한 오류
        /// </summary>
        Fatal = 0x1,

        /// <summary>
        /// 일반 오류
        /// </summary>
        Error = 0x2,

        /// <summary>
        /// 정보
        /// </summary>
        Info = 0x4,

        /// <summary>
        /// 경고
        /// </summary>
        Warn = 0x8,

        /// <summary>
        /// 디버깅용 정보
        /// </summary>
        Debug = 0x10,

        /// <summary>
        /// 모두 남김
        /// </summary>
        All = 0xff
    };

    /// <summary>
    /// 로거의 UI에 따른 활성화 옵션
    /// </summary>
    public enum LogUIOption
    {
        /// <summary>
        /// 모두 활성화
        /// </summary>
        All,

        /// <summary>
        /// UI를 가지지 않은 로거만 활성화
        /// </summary>
        NoUI,

        /// <summary>
        /// UI를 가진 로거만 활성화
        /// </summary>
        UIOnly
    }
}
