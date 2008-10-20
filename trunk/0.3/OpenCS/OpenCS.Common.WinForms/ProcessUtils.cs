using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using OpenCS.Common.Logging;
using OpenCS.Common.Resource;

namespace OpenCS.Common.WinForms
{
    /// <summary>
    /// 프로세스 관련 유틸 클래스
    /// </summary>
    public static class ProcessUtils
    {

        /// <summary>
        /// 프로세스가 런치되었는지 알려준다.
        /// 
        /// ref: http://www.pcreview.co.uk/forums/thread-1393522.php
        /// </summary>
        static public bool IsThisLaunched(IResourceProvider rp, IMessageDialog msgDlg)
        {
            Process[] ps = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            Console.WriteLine(ps.ToString());
            if (ps.Length > 1)
            {
                msgDlg.ShowError(string.Format(rp.GetString("MSG_ALREADY_RUNNING"), Application.ProductName));

                return true;
            }

            return false;
        }

        #region Launch Process

        /// <summary>
        /// 프로세스를 시작한다. Win32Exception이 발생하면 오류 메시지를 출력한다.
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <param name="logger">로거</param>
        /// <param name="executable">실행 명령</param>
        /// <returns>성공하면 <c>true</c></returns>
        public static bool LaunchProcess(IResourceProvider rp, ILogger logger, string executable)
        {
            Debug.Assert(rp != null);
            Debug.Assert(logger != null);
            Debug.Assert(executable != null);

            try
            {
                Process.Start(executable);

                return true;
            }
            catch (Win32Exception ex)
            {
                logger.Error(rp.GetString("MSG_CANNOT_LAUNCH_PROCESS"));
                logger.Debug(ex.StackTrace);
            }

            return false;
        }

        /// <summary>
        /// 프로세스를 시작한다. Win32Exception이 발생하면 오류 메시지를 출력한다.
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <param name="executable">실행 명령</param>
        /// <returns>성공하면 <c>true</c></returns>
        public static bool LaunchProcess(IResourceProvider rp, string executable)
        {
            Debug.Assert(rp != null);
            Debug.Assert(executable != null);

            try
            {
                Process.Start(executable);

                return true;
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show(rp.GetString("MSG_CANNOT_LAUNCH_PROCESS"), rp.GetString("LABEL_ERROR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.Print(ex.StackTrace);
            }

            return false;
        }

        /// <summary>
        /// 프로세스를 시작한다. Win32Exception이 발생하면 오류 메시지를 출력한다.
        /// </summary>
        /// <param name="logger">로거</param>
        /// <param name="executable">실행 명령</param>
        /// <returns>성공하면 <c>true</c></returns>
        public static bool LaunchProcess(ILogger logger, string executable)
        {
            Debug.Assert(logger != null);
            Debug.Assert(executable != null);

            try
            {
                Process.Start(executable);

                return true;
            }
            catch (Win32Exception ex)
            {
                logger.Error("Can't launch process");
                logger.Debug(ex.StackTrace);
            }

            return false;
        }

        /// <summary>
        /// 프로세스를 시작한다. Win32Exception이 발생하면 오류 메시지를 출력한다.
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <param name="msgDlg">메시지 다이얼로그</param>
        /// <param name="executable">실행 명령</param>
        /// <returns>성공하면 <c>true</c></returns>
        public static bool LaunchProcess(IResourceProvider rp, IMessageDialog msgDlg, string executable)
        {
            Debug.Assert(rp != null);
            Debug.Assert(msgDlg != null);
            Debug.Assert(executable != null);

            // ref: http://msdn2.microsoft.com/ms182303(VS.90).aspx
            try
            {
                Process.Start(executable);

                return true;
            }
            catch (Win32Exception ex)
            {
                msgDlg.ShowError(rp.GetString("MSG_CANNOT_LAUNCH_PROCESS"));
                Debug.Print(ex.StackTrace);
            }

            return false;
        }

        /// <summary>
        /// 프로세스를 시작한다. Win32Exception이 발생하면 오류 메시지를 출력한다.
        /// </summary>
        /// <param name="msgDlg">메시지 다이얼로그</param>
        /// <param name="executable">실행 명령</param>
        /// <returns>성공하면 <c>true</c></returns>
        public static bool LaunchProcess(IMessageDialog msgDlg, string executable)
        {
            Debug.Assert(msgDlg != null);
            Debug.Assert(executable != null);

            try
            {
                Process.Start(executable);

                return true;
            }
            catch (Win32Exception ex)
            {
                msgDlg.ShowError("Can't launch process");
                Debug.Print(ex.StackTrace);
            }

            return false;
        }

        /// <summary>
        /// 프로세스를 시작한다. Win32Exception이 발생하면 오류 메시지를 출력한다.
        /// </summary>
        /// <param name="executable">실행 명령</param>
        /// <returns>성공하면 <c>true</c></returns>
        public static bool LaunchProcess(string executable)
        {
            return LaunchProcess(executable, null);
        }

        /// <summary>
        /// Launch process.
        /// </summary>
        /// <param name="executable">Executable name.</param>
        /// <param name="arguments">Arguments.</param>
        /// <returns>true if launching is succeed.</returns>
        public static bool LaunchProcess(string executable, string arguments)
        {
            Debug.Assert(executable != null);

            try
            {
                Process p = new Process();
                p.StartInfo.FileName = executable;
                p.StartInfo.Arguments = arguments;
                p.Start();

                return true;
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show("Can't launch process", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.Print(ex.StackTrace);
            }

            return false;
        }

        #endregion
    }
}
