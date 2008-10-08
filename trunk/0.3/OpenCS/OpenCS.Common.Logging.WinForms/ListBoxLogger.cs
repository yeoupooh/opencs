using System;
using System.Collections.Generic;
using System.Text;
using OpenCS.Common.Logging;
using System.Windows.Forms;

namespace OpenCS.Common.Logging.WinForms
{
    public class ListBoxLogger : AbstractLogger
    {
        private ListBox m_listBox;

        public ListBoxLogger(ListBox listBox)
            : base(true)
        {
            m_listBox = listBox;
        }

        protected override void Log(string prefix, string msg)
        {
            m_listBox.Items.Add("[" + prefix + "] " + msg);
            m_listBox.SelectedIndex = m_listBox.Items.Count - 1;
        }
    }
}
