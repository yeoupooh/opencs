using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using OpenCS.Common.Http;
using OpenCS.Common.Worker;

namespace OpenCS.Common.Http
{
    public class HttpMultipartPoster : BaseHttpPoster
    {
        private const string STR_CR = "\r\n";
        private const string STR_BOUNDARY_LINE = "--";
        private const string FORMAT_CONENT = "Content-Disposition: form-data; name=\"{0}\"";
        private const string FORMAT_CONENT_FILE = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"";
        private const string FORMAT_CONENT_TYPE = "Content-Type: {0}";

        private string m_boundary;

        private IProgressView m_pv;

        public IProgressView ProgressView
        {
            get { return m_pv; }
            set { m_pv = value; }
        }

        public HttpMultipartPoster(string url, Encoding encoding, string userAgent)
            : base(url, encoding, userAgent)
        {
            InitBoundary();
        }

        public HttpMultipartPoster(string url, Encoding encoding, string userAgent, CookieContainer cc)
            : base(url, encoding, userAgent, cc)
        {
            InitBoundary();
        }

        private void InitBoundary()
        {
            m_boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
        }

        protected string MakeContentString(PostContent content)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(STR_BOUNDARY_LINE);
            sb.Append(m_boundary);
            sb.Append(STR_CR);
            sb.Append(string.Format(FORMAT_CONENT, content.Name));
            sb.Append(STR_CR);
            sb.Append(STR_CR);
            sb.Append(content.Value);
            sb.Append(STR_CR);

            return sb.ToString();
        }

        private string MakeContentFileString(PostContent content)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(STR_BOUNDARY_LINE);
            sb.Append(m_boundary);
            sb.Append(STR_CR);
            sb.Append(string.Format(FORMAT_CONENT_FILE, content.Name, content.Filename));
            sb.Append(STR_CR);
            sb.Append(string.Format(FORMAT_CONENT_TYPE, content.ContentType));
            sb.Append(STR_CR);
            sb.Append(STR_CR);

            return sb.ToString();
        }

        private string MakeContentFinisherString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(STR_BOUNDARY_LINE);
            sb.Append(m_boundary);
            sb.Append(STR_BOUNDARY_LINE);
            sb.Append(STR_CR);

            return sb.ToString();
        }

        public override int GetContentLength()
        {
            // -----------------------------7d83393138029c
            // Content-Disposition: form-data; name="null"
            // 
            // 
            // -----------------------------7d83393138029c
            // Content-Disposition: form-data; name="wr_subject"
            //
            // 제목
            // -----------------------------7d83393138029c
            // Content-Disposition: form-data; name="bf_file[]"; filename="C:\Users\mio\Pictures\IMG_0896-190x190.png"
            // Content-Type: image/x-png
            // 
            // (png 파일 바이너리)
            // -----------------------------7d83393138029c
            // Content-Disposition: form-data; name="bf_file[]"; filename="D:\Test\test48.mwx"
            // Content-Type: application/x-zip-compressed
            //
            // (mwx 파일 바이너리)
            // -----------------------------7d83393138029c
            // Content-Disposition: form-data; name="wr_trackback"
            //
            //
            // -----------------------------7d83393138029c--

            int totalLen = 0;
            foreach (PostContent content in Contents)
            {
                if (content.Value != null)
                {
                    totalLen = totalLen + Encoding.GetByteCount(MakeContentString(content));
                }
                else
                {
                    totalLen = totalLen + Encoding.GetByteCount(MakeContentFileString(content));
                    totalLen = totalLen + content.Length + Encoding.GetByteCount(STR_CR);
                }
            }

            totalLen = totalLen + Encoding.GetByteCount(MakeContentFinisherString());

            return totalLen;
        }

        public override HttpResult Post()
        {
            HttpResult result = new HttpResult();

            // header
            HttpWebRequest reqUp = (HttpWebRequest)WebRequest.Create(Url);
            reqUp.CookieContainer = Cc;
            reqUp.ContentType = "multipart/form-data; boundary=" + m_boundary;
            reqUp.Method = "POST";
            reqUp.KeepAlive = true;
            reqUp.UserAgent = UserAgent;
            reqUp.AllowAutoRedirect = false;
            reqUp.ContentLength = GetContentLength();
            reqUp.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-ms-application, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-ms-xbap, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-silverlight, application/x-silverlight-2-b1, */*";

            // body
            int totalLen = GetContentLength();
            int postLen = 0;
            using (Stream reqStream = reqUp.GetRequestStream())
            {
                string data;
                foreach (PostContent content in Contents)
                {
                    if (content.Value != null)
                    {
                        data = MakeContentString(content);
                        reqStream.Write(Encoding.GetBytes(data), 0, Encoding.GetByteCount(data));
                        postLen = postLen + Encoding.GetByteCount(data);
                    }
                    else
                    {
                        data = MakeContentFileString(content);
                        reqStream.Write(Encoding.GetBytes(data), 0, Encoding.GetByteCount(data));
                        postLen = postLen + Encoding.GetByteCount(data);

                        // 파일을 한방에 올린다. 
                        //reqStream.Write(content.Bytes, 0, content.Length);
                        //postLen = postLen + content.Length;

                        using (FileStream fs = new FileStream(content.Filename, FileMode.Open))
                        {
                            byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)fs.Length))];
                            int bytesRead = 0;
                            while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                reqStream.Write(buffer, 0, bytesRead);
                                postLen += bytesRead;

                                if (m_pv != null)
                                {
                                    m_pv.SetProgress((int)((float)postLen / totalLen * 100));
                                }
                            }
                        }

                        reqStream.Write(Encoding.GetBytes(STR_CR), 0, Encoding.GetByteCount(STR_CR));
                        postLen = postLen + Encoding.GetByteCount(STR_CR);
                    }

                    if (m_pv != null)
                    {
                        m_pv.SetProgress((int)((float)postLen / totalLen * 100));
                    }
                }

                data = MakeContentFinisherString();
                reqStream.Write(Encoding.GetBytes(data), 0, Encoding.GetByteCount(data));
                postLen = postLen + Encoding.GetByteCount(data);

                if (m_pv != null)
                {
                    m_pv.SetProgress((int)((float)postLen / totalLen * 100));
                }
            }

            // get response
            using (HttpWebResponse res = reqUp.GetResponse() as HttpWebResponse)
            {
                using (Stream resStream = res.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(resStream))
                    {
                        result.Content = sr.ReadToEnd();
                    }
                }
            }

            return result;
        }
    }
}
