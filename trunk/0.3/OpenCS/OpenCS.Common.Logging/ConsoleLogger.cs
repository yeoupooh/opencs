using System;
using System.Collections.Generic;
using System.Text;
using OpenCS.Common;

namespace OpenCS.Common.Logging
{
    /// <summary>
    /// �ܼ� �ΰ�
    /// </summary>
    public class ConsoleLogger : AbstractLogger
    {
        /// <summary>
        /// ������
        /// </summary>
        public ConsoleLogger()
            : base(false)
        {
        }

        /// <summary>
        /// �α� �޽����� �����.
        /// </summary>
        /// <param name="prefix">���ξ�</param>
        /// <param name="msg">�޽���</param>
        protected override void Log(string prefix, string msg)
        {
            Console.WriteLine(StringUtils.GetTimeStampWithTicks() + " [" + prefix + "] " + msg);
        }
    }
}
