using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenCS.Common.AutoUpdate;
using OpenCS.Common.AutoUpdate.WinForms;
using OpenCS.Common.Logging;
using OpenCS.Common.Resource;
using OpenCS.Common.WinForms;
using System.IO;

namespace DemoApp
{
    static class Program
    {
        public static string AppTempPath = @"C:\Temp";
        // ref: http://msdn2.microsoft.com/en-us/library/ms229654.aspx
        public static string AppPath = ApplicationInfo.Path;
        public static string AppMetaFileName = AppPath + @"\AutoUpdateConsumer.xml";

        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AutoUpdater au = new AutoUpdater(AppMetaFileName, AppTempPath);
            au.ResourceProvider = new DummyResourceProvider();
            try
            {
                if (au.CheckNewVersion() == true)
                {
                    if (au.Download(new FormAutoUpdate()) == true)
                    {
                        if (ProcessUtils.LaunchProcess(Path.Combine(au.TempFolder, au.ExecuteItem.FileName)) == true)
                        {
                            return;
                        }
                    }
                }
                Application.Run(new Form1());
            }
            catch (AutoUpdateException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}