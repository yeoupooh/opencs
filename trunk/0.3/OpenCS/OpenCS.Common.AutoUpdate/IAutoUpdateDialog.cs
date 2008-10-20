using System;
using System.Collections.Generic;
using System.Text;
using OpenCS.Common.Resource;

namespace OpenCS.Common.AutoUpdate
{
    public interface IAutoUpdateDialog : IDialog
    {
        AutoUpdater AutoUpdater
        {
            set;
        }

        IResourceChanger ResourceChanger
        {
            set;
        }
    }
}
