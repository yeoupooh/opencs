using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OpenCS.Common;

namespace OpenCS.Common.Logging
{
    /// <summary>
    /// �ؽ�Ʈ ���Ϸ� �޽����� �����ϴ� �ΰ�. �α� �Լ��� ȣ��ø��� ������ ���� �ݴ´�.
    /// </summary>
    public class TextFileLogger : AbstractLogger
    {
        private StringBuilder m_queuedMsg = null;
        private string m_filename;

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="filename">���ϸ�</param>
        public TextFileLogger(string filename)
            : base(false)
        {
            m_filename = filename;
        }

        /// <summary>
        /// �α� �޽����� �����.
        /// </summary>
        /// <param name="prefix">���ξ�</param>
        /// <param name="msg">�޽���</param>
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
