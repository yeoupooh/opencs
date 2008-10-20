using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using OpenCS.Common.Logging;
using OpenCS.Common.Resource;
using OpenCS.Common.Xml;

namespace OpenCS.Common.AutoUpdate
{
    public class AutoUpdater
    {
        private string mConsumerFile;
        private string mTempFolder;

        private ILogger mLogger;
        private IResourceProvider mRP;

        private Version mConsumerVersion;
        private string mProviderUrl;

        private Version mProviderVersion;
        private string mInformationUrl;

        private List<AutoUpateItem> mItems = new List<AutoUpateItem>();
        private AutoUpateItem mExecuteItem;

        public ILogger Logger
        {
            get { return mLogger; }
            set { mLogger = value; }
        }

        public IResourceProvider ResourceProvider
        {
            get { return mRP; }
            set { mRP = value; }
        }

        public string ConsumerFile
        {
            get { return mConsumerFile; }
            set { mConsumerFile = value; }
        }

        public string TempFolder
        {
            get { return mTempFolder; }
            set { mTempFolder = value; }
        }

        public string InformationUrl
        {
            get { return mInformationUrl; }
            set { mInformationUrl = value; }
        }

        public List<AutoUpateItem> Items
        {
            get { return mItems; }
            set { mItems = value; }
        }

        public AutoUpateItem ExecuteItem
        {
            get { return mExecuteItem; }
            set { mExecuteItem = value; }
        }

        public AutoUpdater(string consumerFile, string tempFolder)
        {
            mConsumerFile = consumerFile;
            mTempFolder = tempFolder;

            LoadConfig();
        }

        public AutoUpdater(string consumerFile, string tempFolder, ILogger logger, IResourceProvider rp)
        {
            mConsumerFile = consumerFile;
            mTempFolder = tempFolder;
            mLogger = logger;
            mRP = rp;

            LoadConfig();
        }

        public void LoadConfig()
        {
            XmlDocumenter xd = new XmlDocumenter();
            try
            {
                xd.Document.Load(mConsumerFile);
                string version = xd.RootElement.Attributes.GetNamedItem(AutoUpdateConstants.TagVersion).Value;
                string type = xd.RootElement.Attributes.GetNamedItem(AutoUpdateConstants.TagType).Value;
                Debug.Print(version);
                Debug.Print(type);
                if (version == AutoUpdateConstants.CurrentVersion && type == AutoUpdateConstants.TagConsumer)
                {
                    LoadConsumerConfig11(xd);
                }
                else
                {
                    throw new AutoUpdateException("Can't support this auto update consumer.");
                }
            }
            catch (XmlException ex)
            {
                throw new AutoUpdateException("Error in reading auto update consumer profile.");
            }
        }

        private void LoadConsumerConfig11(XmlDocumenter xd)
        {
            try
            {
                mConsumerVersion = new Version(xd.RootElement.SelectSingleNode(AutoUpdateConstants.TagVersion).InnerText);
                XmlNode provider = xd.RootElement.SelectSingleNode(AutoUpdateConstants.TagProvider);
                mProviderUrl = provider.Attributes.GetNamedItem(AutoUpdateConstants.TagUrl).Value;
            }
            catch (NullReferenceException ex)
            {
                throw new AutoUpdateException("Error in reading auto update consumer profile.");
            }
            catch (XmlException ex)
            {
                throw new AutoUpdateException("Error in reading auto update consumer profile.");
            }
        }

        public bool CheckNewVersion()
        {
            return CheckNewVersion(5000);
        }

        public bool CheckNewVersion(int timeout)
        {
            TimedWebClient wc = new TimedWebClient(timeout);
            try
            {
                XmlDocumenter xd = new XmlDocumenter();
                xd.Document.InnerXml = wc.DownloadString(mProviderUrl);
                string version = xd.RootElement.Attributes.GetNamedItem(AutoUpdateConstants.TagVersion).InnerText;
                string type = xd.RootElement.Attributes.GetNamedItem(AutoUpdateConstants.TagType).Value;
                Debug.Print(version);
                Debug.Print(type);
                if (version == AutoUpdateConstants.CurrentVersion && type == AutoUpdateConstants.TagProvider)
                {
                    LoadProviderConfig11(xd);
                }
                else
                {
                    throw new AutoUpdateException("Can't support this auto update consumer.");
                }

                return mProviderVersion.CompareTo(mConsumerVersion) > 0 ? true : false;
            }
            catch (NullReferenceException ex)
            {
                throw new AutoUpdateException("Error in reading auto update provider profile.");
            }
            catch (XmlException ex)
            {
                throw new AutoUpdateException("Error in reading auto update provider profile.");
            }
            catch (WebException ex)
            {
                throw new AutoUpdateException("Error in reading auto update provider profile.");
            }
            catch (NotSupportedException ex)
            {
                throw new AutoUpdateException("Error in reading auto update provider profile.");
            }

            return true;
        }

        private void LoadProviderConfig11(XmlDocumenter xd)
        {
            try
            {
                mProviderVersion = new Version(xd.RootElement.SelectSingleNode(AutoUpdateConstants.TagVersion).InnerText);
                mInformationUrl = xd.RootElement.SelectSingleNode(AutoUpdateConstants.TagInformation).Attributes.GetNamedItem(AutoUpdateConstants.TagUrl).Value;
                string executeFileId = xd.RootElement.SelectSingleNode(AutoUpdateConstants.TagExcute).Attributes.GetNamedItem(AutoUpdateConstants.TagFileId).Value;
                mExecuteItem = null;
                mItems.Clear();
                foreach (XmlNode node in xd.RootElement.SelectNodes(
                    string.Format("{0}/{1}", AutoUpdateConstants.TagFiles, AutoUpdateConstants.TagFile)))
                {
                    AutoUpateItem item = new AutoUpateItem(
                        node.Attributes.GetNamedItem(AutoUpdateConstants.TagId).Value,
                        node.Attributes.GetNamedItem(AutoUpdateConstants.TagUrl).Value,
                        node.Attributes.GetNamedItem(AutoUpdateConstants.TagName).Value);
                    mItems.Add(item);
                    if (item.Id == executeFileId)
                    {
                        mExecuteItem = item;
                    }
                }
                Debug.Print(mExecuteItem.FileName);
            }
            catch (NullReferenceException ex)
            {
                throw new AutoUpdateException("Error in reading auto update provider profile.");
            }
            catch (XmlException ex)
            {
                throw new AutoUpdateException("Error in reading auto update provider profile.");
            }
        }

        public bool Download(IAutoUpdateDialog dlg)
        {
            using (dlg)
            {
                dlg.AutoUpdater = this;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
