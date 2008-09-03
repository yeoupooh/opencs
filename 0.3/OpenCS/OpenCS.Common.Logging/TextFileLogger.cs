using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OpenCS.Common;

namespace OpenCS.Common.Logging
{
    /// <summary>
    /// 텍스트 파일로 메시지를 저장하는 로거. 로그 함수를 호출시마나 파일을 열고 닫는다.
    /// </summary>
    public class TextFileLogger : AbstractLogger
    {
        private StringBuilder m_queuedMsg = null;
        private string m_filename;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="filename">파일명</param>
        public TextFileLogger(string filename)
            : base(false)
        {
            m_filename = filename;
        }

        /// <summary>
        /// 로그 메시지를 남긴다.
        /// </summary>
        /// <param name="prefix">접두어</param>
        /// <param name="msg">메시지</param>
        protected override void Log(string prefix, string msg)
        {
            // check path exists
            if (Directory.Exists(Path.GetDirectoryName(m_filename)) == false)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(m_filename));
            }

            try
            {
                using (StreamWriter writer = File.AppendText(m_filename))
                {
                    // flush buffer
                    if (m_queuedMsg != null)
                    {
                        writer.WriteLine(m_queuedMsg.ToString());
                    }

                    writer.WriteLine(StringUtils.GetTimeStampWithTicks() + " [" + prefix + "] " + msg);

                    // clear buffer
                    if (m_queuedMsg != null)
                    {
                        m_queuedMsg = null;
                    }
                }
            }
            catch (IOException)
            {
                // add to buffer
                if (m_queuedMsg == null)
                {
                    m_queuedMsg = new StringBuilder();
                }
                m_queuedMsg.Append(StringUtils.GetTimeStampWithTicks() + " [" + prefix + "] " + msg);
            }
        }
    }
}
