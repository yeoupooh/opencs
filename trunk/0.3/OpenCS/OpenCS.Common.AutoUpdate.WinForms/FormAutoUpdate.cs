using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using OpenCS.Common.Logging;
using OpenCS.Common.Resource;

namespace OpenCS.Common.AutoUpdate.WinForms
{
    public partial class FormAutoUpdate : Form, IAutoUpdateDialog
    {
        private WebClient mWC = new WebClient();
        private AutoUpdater mAU;
        private IResourceChanger mRC;

        public FormAutoUpdate()
        {
            InitializeComponent();

            mWC.DownloadFileCompleted += new AsyncCompletedEventHandler(WebClient_DownloadFileAsyncCompleted);
            mWC.DownloadProgressChanged += new DownloadProgressChangedEventHandler(WebClient_DownloadProgressChanged);
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
            if (mAU.Items.Count > 0)
            {
                if (Directory.Exists(mAU.TempFolder) == false)
                {
                    Directory.CreateDirectory(mAU.TempFolder);
                }

                AutoUpateItem item = mAU.Items[0];
                mWC.DownloadFileAsync(new Uri(item.Url), Path.Combine(mAU.TempFolder, item.FileName));
            }
            else
            {
                labelMessage.Text = mAU.ResourceProvider.GetString("MSG_ALL_DOWNLOADED");

                // close
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void WebClient_DownloadFileAsyncCompleted(object sender, AsyncCompletedEventArgs e)
        {
            timerSkip.Enabled = false;

            if (e.Error == null)
            {
                mAU.Items.RemoveAt(0);
                StartDownload();
            }
            else
            {
                mAU.Logger.Debug(e.Error.StackTrace);
                mAU.Logger.Fatal(e.Error.Message);
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            timerSkip.Enabled = false;
            if (e.ProgressPercentage >= 0 && e.ProgressPercentage <= 100)
            {
                labelMessage.Text = string.Format(mAU.ResourceProvider.GetString("FORMAT_DOWNLOAING_FILE"),
                    Path.GetFileName(mAU.Items[0].FileName), e.ProgressPercentage);

                progressBarMain.Value = e.ProgressPercentage;

                this.Invalidate();
            }
        }

        #region IAutoUpdateDialog 멤버

        public AutoUpdater AutoUpdater
        {
            set
            {
                mAU = value;
                webBrowserMain.Navigate(mAU.InformationUrl);
            }
        }

        public IResourceChanger ResourceChanger
        {
            set
            {
                mRC = value;
                mRC.Change(this);
            }
        }

        #endregion
    }
}