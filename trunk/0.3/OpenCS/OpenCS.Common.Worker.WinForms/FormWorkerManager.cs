using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using OpenCS.Common.Resource;
using OpenCS.Common;

namespace OpenCS.Common.Worker.WinForms
{
    public partial class FormWorkerManager : Form, IWorkerManagerDialog
    {
        private IWorkerManagerDialogController m_controller;
        private IWorkerManagerDialogModel m_model;

        public FormWorkerManager()
        {
            InitializeComponent();

            if (Singleton<ResourceManager>.Instance.ResourceChanger != null)
            {
                Singleton<ResourceManager>.Instance.ResourceChanger.Change(this);
            }

            m_model = new WorkerManagerDialogModel();
            m_controller = new WorkerManagerDialogController(this, m_model);

            this.StartPosition = FormStartPosition.CenterParent;
            this.ControlBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
        }

        private delegate void ShowProgressDelegate(bool showProgress);
        private void ShowProgressInternal(bool showProgress)
        {
            progressBar1.Style = (showProgress == true) ? ProgressBarStyle.Continuous : ProgressBarStyle.Marquee;
        }

        private delegate void SetProgressDelegate(int value);
        private void SetProgressInternal(int value)
        {
            progressBar1.Value = value;
        }

        private delegate void SetMessageDelegate(string message);
        private void SetMessageInternal(string message)
        {
            label1.Text = message;
        }

        #region IWorkerManagerDialog 멤버

        public IWorkerManagerDialogController Controller
        {
            get { return m_controller; }
        }

        public IWorkerManagerDialogModel Model
        {
            get { return m_model; }
        }

        #endregion

        #region IProgressView 멤버

        public bool ShowProgress
        {
            set
            {
                Invoke(new ShowProgressDelegate(ShowProgressInternal), new object[] { value });
            }
        }

        public void SetProgress(int value)
        {
            Invoke(new SetProgressDelegate(SetProgressInternal), new object[] { value });
        }

        public void SetMessage(string message)
        {
            Invoke(new SetMessageDelegate(SetMessageInternal), new object[] { message });
        }

        public WorkerResultState ResultState
        {
            get
            {
                return m_model.WorkerManager.ResultState;
            }
        }

        #endregion

        #region IWorkerManager 멤버

        public IProgressView ProgressView
        {
            set { ; }
        }

        public WorkerParams WorkerParams
        {
            get
            {
                return m_model.WorkerManager.WorkerParams;
            }
        }

        public void AddWorker(IWorker worker)
        {
            m_model.WorkerManager.AddWorker(worker);
        }

        public void Start()
        {
            Debug.Fail("Use ShowDialog()");
        }

        public Exception Error
        {
            get { return m_model.WorkerManager.Error; }
        }

        #endregion
    }
}