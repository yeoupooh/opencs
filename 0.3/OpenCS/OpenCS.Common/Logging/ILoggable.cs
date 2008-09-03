using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Logging
{
    /// <summary>
    /// 로그를 남기는 객체
    /// </summary>
    public interface ILoggable
    {
        /// <summary>
        /// 로거를 설정한다.
        /// </summary>
        ILogger Logger
        {
            set;
        }
    }
}
