using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OpenCS.Common.Logging.WinForms
{
    public interface ILogMessageBox
    {
        void Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);
    }
}
