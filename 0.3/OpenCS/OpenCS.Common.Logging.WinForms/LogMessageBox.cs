using System;
using System.Collections.Generic;
using System.Text;
using OpenCS.Common.Logging;
using System.Windows.Forms;

namespace OpenCS.Common.Logging.WinForms
{
    public class LogMessageBox : ILogMessageBox
    {
        #region ILogMessageBox 멤버

        public void Show(string text, string caption, System.Windows.Forms.MessageBoxButtons buttons, System.Windows.Forms.MessageBoxIcon icon)
        {
            MessageBox.Show(text, caption, buttons, icon);
        }

        #endregion
    }
}
