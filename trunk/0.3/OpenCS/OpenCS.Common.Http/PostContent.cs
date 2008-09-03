using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OpenCS.Common.Http
{
    public class PostContent
    {
        private string m_contentType;

        public string ContentType
        {
            get { return m_contentType; }
            set { m_contentType = value; }
        }
        private string m_name;

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }
        private string m_value;

        public string Value
        {
            get { return m_value; }
            set { m_value = value; }
        }
        private string m_filename;

        public string Filename
        {
            get { return m_filename; }
            set { m_filename = value; }
        }
        private byte[] m_bytes;

        public byte[] Bytes
        {
            get { return m_bytes; }
        }

        public int Length
        {
            get
            {
                return m_bytes.Length;
            }
        }

        public PostContent(string name, string value, Encoding enc)
        {
            m_name = name;
            m_value = value;

            m_bytes = new byte[enc.GetByteCount(m_value)];
            m_bytes = enc.GetBytes(m_value);
        }

        public PostContent(string name, string contentType, string filename)
        {
            m_name = name;
            m_contentType = contentType;
            m_filename = filename;

            FileInfo fi = new FileInfo(m_filename);
            m_bytes = new byte[fi.Length];
            m_bytes = File.ReadAllBytes(m_filename);
        }
    }
}
