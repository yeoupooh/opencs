using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Xml;
using OpenCS.Common.Logging;
using OpenCS.Common.WinForms;
using OpenCS.Common.Resource;

namespace OpenCS.Common.AutoUpdate
{
    public enum UpdateResult { NoNewVersion, Updated, Canceled };

    public static class AutoUpdaterV2
    {
        private static UpdateResult UpdateInternal(IResourceProvider rp, ILogger logger, ApplicationInfo au)
        {
            // remote >  local: 1
            // remote == local: 0
            // remote <  local: -1
            if (au.RemoteVersion.CompareTo(au.FileVersion) <= 0)
            {
                return UpdateResult.NoNewVersion;
            }

            using (FormAutoUpdate dlg = new FormAutoUpdate())
            {
                dlg.ResourceProvider = rp;
                dlg.AutoUpdate = au;
                dlg.Logger = logger;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ProcessUtils.LaunchProcess(rp, logger, au.TempFolder + "\\" + au.UpdateFiles[0]);

                    return UpdateResult.Updated;
                }
            }

            return UpdateResult.Canceled;
        }

        public static UpdateResult Update(IResourceProvider rp, ILogger logger, string updateUrl, string tempFolder)
        {
            ApplicationInfo au = new ApplicationInfo();
            if (au.Load() == true)
            {
                if (au.GetRemoteVersion(logger, updateUrl, tempFolder) == true)
                {
                    return UpdateInternal(rp, logger, au);
                }
            }

            return UpdateResult.NoNewVersion;
        }

        public static UpdateResult UpdateFromXml(IResourceProvider rp, ILogger logger, string xmlFile, string tempFolder)
        {
            ApplicationInfo au = new ApplicationInfo();
            if (au.LoadFromXml(logger, xmlFile) == true)
            {
                if (au.GetRemoteVersion(logger, tempFolder) == true)
                {
                    return UpdateInternal(rp, logger, au);
                }
            }

            return UpdateResult.NoNewVersion;
        }

    }
}
