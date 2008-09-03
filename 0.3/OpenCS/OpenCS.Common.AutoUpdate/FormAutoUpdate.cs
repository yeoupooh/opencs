using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using OpenCS.Common.Logging;
using OpenCS.Common.Resource;
using OpenCS.Common.WinForms;

namespace OpenCS.Common.AutoUpdate
{
    public partial class FormAutoUpdate : Form, ILoggable
    {
        private IResourceProvider m_rp;
        private ILogger m_logger;
        private UpdateResult m_result;
        private WebClient m_webClient = new WebClient();
        private List<string> m_downFiles = new List<string>();

        private ApplicationInfo m_autoUpdate;

        public IResourceProvider ResourceProvider
        {
            get { return m_rp; }
            set
            {
                m_rp = value;
                InitResources();
            }
        }

        public ApplicationInfo AutoUpdate
        {
            get { return m_autoUpdate; }
            set
            {
                m_autoUpdate = value;

                this.Text = string.Format(m_rp.GetString("MSG_NEW_VERSION_HERE"),
                    Application.ProductName, VersionInfo.GetVersionFrom(m_rp, m_autoUpdate.RemoteVersion));

                webBrowserMain.Navigate(m_autoUpdate.InfoUrl);

                foreach (string file in m_autoUpdate.UpdateFiles)
                {
                    m_downFiles.Add(file);
                }

            }
        }

        public UpdateResult Result
        {
            get { return m_result; }
            set { m_result = value; }
        }

        public FormAutoUpdate()
        {
            InitializeComponent();

            m_webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(WebClient_DownloadFileAsyncCompleted);
            m_webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(WebClient_DownloadProgressChanged);
        }

        private void InitResources()
        {
            #region Resources
            this.buttonDownload.Text = m_rp.GetString("LABEL_DOWNLOAD");
            this.buttonCancel.Text = m_rp.GetString("LABEL_CANCEL");
            #endregion
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            buttonDownload.Enabled = false;
            labelMessage.Visible = true;
            progressBarMain.Visible = true;

            // download
            StartDownload();

        }

        private void StartDownload()
        {
            if (m_downFiles.Count > 0)
            {
                if (Directory.Exists(m_autoUpdate.TempFolder) == false)
                {
                    Directory.CreateDirectory(m_autoUpdate.TempFolder);
                }

                string downUrl = m_autoUpdate.RemoteUrl + "/" + m_downFiles[0];
                string downFile = m_autoUpdate.TempFolder + "\\" + m_downFiles[0];
                m_webClient.DownloadFileAsync(new Uri(downUrl), downFile);
            }
            else
            {
                // close
                m_result = UpdateResult.Updated;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void WebClient_DownloadFileAsyncCompleted(object sender, AsyncCompletedEventArgs e)
        {
            timerSkip.Enabled = false;
            labelMessage.Text = m_rp.GetString("MSG_COMPLETED");

            if (e.Error == null)
            {
                m_downFiles.RemoveAt(0);
                StartDownload();
            }
            else
            {
                m_logger.Debug(e.Error.StackTrace);
                m_logger.Fatal(e.Error.Message);
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            timerSkip.Enabled = false;
            if (e.ProgressPercentage >= 0 && e.ProgressPercentage <= 100)
            {
                labelMessage.Text = string.Format(m_rp.GetString("MSG_DOWNLOADED"), Path.GetFileName(m_downFiles[0]), e.ProgressPercentage);
                //m_logger.Debug(string.Format("downloading...{0}%", e.ProgressPercentage.ToString()));

                progressBarMain.Value = e.ProgressPercentage;

                this.Invalidate();
            }
        }

        #region ILoggable 멤버

        public ILogger Logger
        {
            set { m_logger = value; }
        }

        #endregion
    }
}