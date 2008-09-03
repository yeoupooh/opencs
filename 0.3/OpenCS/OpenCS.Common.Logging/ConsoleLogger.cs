using System;
using System.Collections.Generic;
using System.Text;
using OpenCS.Common;

namespace OpenCS.Common.Logging
{
    /// <summary>
    /// 콘솔 로거
    /// </summary>
    public class ConsoleLogger : AbstractLogger
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public ConsoleLogger()
            : base(false)
        {
        }

        /// <summary>
        /// 로그 메시지를 남긴다.
        /// </summary>
        /// <param name="prefix">접두어</param>
        /// <param name="msg">메시지</param>
        protected override void Log(string prefix, string msg)
        {
            Console.WriteLine(StringUtils.GetTimeStampWithTicks() + " [" + prefix + "] " + msg);
        }
    }
}
