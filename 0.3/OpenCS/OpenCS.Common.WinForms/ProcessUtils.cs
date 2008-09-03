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
    /// ���μ��� ���� ��ƿ Ŭ����
    /// </summary>
    public static class ProcessUtils
    {

        /// <summary>
        /// ���μ����� ��ġ�Ǿ����� �˷��ش�.
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
        /// ���μ����� �����Ѵ�. Win32Exception�� �߻��ϸ� ���� �޽����� ����Ѵ�.
        /// </summary>
        /// <param name="rp">���ҽ� ������</param>
        /// <param name="logger">�ΰ�</param>
        /// <param name="executable">���� ���</param>
        /// <returns>�����ϸ� <c>true</c></returns>
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
        /// ���μ����� �����Ѵ�. Win32Exception�� �߻��ϸ� ���� �޽����� ����Ѵ�.
        /// </summary>
        /// <param name="rp">���ҽ� ������</param>
        /// <param name="executable">���� ���</param>
        /// <returns>�����ϸ� <c>true</c></returns>
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
        /// ���μ����� �����Ѵ�. Win32Exception�� �߻��ϸ� ���� �޽����� ����Ѵ�.
        /// </summary>
        /// <param name="logger">�ΰ�</param>
        /// <param name="executable">���� ���</param>
        /// <returns>�����ϸ� <c>true</c></returns>
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
        /// ���μ����� �����Ѵ�. Win32Exception�� �߻��ϸ� ���� �޽����� ����Ѵ�.
        /// </summary>
        /// <param name="rp">���ҽ� ������</param>
        /// <param name="msgDlg">�޽��� ���̾�α�</param>
        /// <param name="executable">���� ���</param>
        /// <returns>�����ϸ� <c>true</c></returns>
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
        /// ���μ����� �����Ѵ�. Win32Exception�� �߻��ϸ� ���� �޽����� ����Ѵ�.
        /// </summary>
        /// <param name="msgDlg">�޽��� ���̾�α�</param>
        /// <param name="executable">���� ���</param>
        /// <returns>�����ϸ� <c>true</c></returns>
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
        /// ���μ����� �����Ѵ�. Win32Exception�� �߻��ϸ� ���� �޽����� ����Ѵ�.
        /// </summary>
        /// <param name="executable">���� ���</param>
        /// <returns>�����ϸ� <c>true</c></returns>
        public static bool LaunchProcess(string executable)
        {
            Debug.Assert(executable != null);

            try
            {
                Process.Start(executable);

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
