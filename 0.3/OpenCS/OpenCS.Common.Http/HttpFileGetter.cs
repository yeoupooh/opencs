using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using OpenCS.Common.Worker;

namespace OpenCS.Common.Http
{
    public class HttpFileGetter : BaseHttpConnection, IHttpGetter
    {
        private IProgressView mPv;
        private string mFileName;

        private HttpResult mResult;
        private HttpWebRequest mReq;
        private HttpWebResponse mRes;
        private StreamReader mSr;
        private HttpWebResponse mResDown;
        private BinaryReader mReader;
        private FileStream mFs;
        private long mContentLength;
        private long mDownSize = 0;

        public IProgressView ProgressView
        {
            get { return mPv; }
            set { mPv = value; }
        }

        public HttpFileGetter(string url, string filename)
            : base(url)
        {
            mFileName = filename;
        }

        #region IHttpGetter 멤버

        public HttpResult Get()
        {
            if (BeginGet() == true)
            {
                while (GetNext())
                {
                }
            }
            mResult = EndGet();

            return mResult;
        }

        #endregion

        #region IHttpGetter 멤버

        public bool BeginGet()
        {
            mReq = (HttpWebRequest)HttpWebRequest.Create(Url);
            if (Cc != null)
            {
                mReq.CookieContainer = Cc;
            }
            mReq.Method = "GET";

            mRes = (HttpWebResponse)mReq.GetResponse();
            mSr = new StreamReader(mRes.GetResponseStream());

            if (Cc != null)
            {
                AddCookies(Cc, mReq.RequestUri, mRes);
            }

            // download
            mResDown = mReq.GetResponse() as HttpWebResponse;
            mContentLength = mResDown.ContentLength;
            if (mContentLength > 0)
            {
                mReader = new BinaryReader(mResDown.GetResponseStream(), Encoding.UTF8);
                mFs = new FileStream(mFileName, FileMode.Create);
            }

            mDownSize = 0;

            return (mContentLength > 0) ? true : false;
        }

        public bool GetNext()
        {
            byte[] readContents = mReader.ReadBytes(2048);

            mDownSize += readContents.Length;
            if (mPv != null)
            {
                mPv.SetProgress((int)((float)mDownSize / mContentLength * 100));
            }

            if (readContents.Length == 0)
            {
                return false;
            }

            mFs.Write(readContents, 0, readContents.Length);

            return (mDownSize > 0) ? true : false;
        }

        public HttpResult EndGet()
        {
            if (mFs != null)
            {
                mFs.Close();
            }

            if (mReader != null)
            {
                mReader.Close();
            }

            if (mSr != null)
            {
                mSr.Close();
            }

            return mResult;
        }

        #endregion
    }
}
