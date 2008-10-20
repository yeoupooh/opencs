using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.AutoUpdate
{
    public class AutoUpateItem
    {
        private string mId;
        private string mUrl;
        private string mFileName;

        public string Id
        {
            get { return mId; }
            set { mId = value; }
        }

        public string Url
        {
            get { return mUrl; }
            set { mUrl = value; }
        }

        public string FileName
        {
            get { return mFileName; }
            set { mFileName = value; }
        }

        public AutoUpateItem(string id, string url, string fileName)
        {
            mId = id;
            mUrl = url;
            mFileName = fileName;
        }
    }
}
