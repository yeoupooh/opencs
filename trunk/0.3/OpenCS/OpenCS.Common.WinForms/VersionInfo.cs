using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Deployment.Application;
using OpenCS.Common.Resource;
using OpenCS.Common.Logging;

namespace OpenCS.Common.WinForms
{
    /// <summary>
    /// ���� �ܰ�
    /// </summary>
    public enum VersionStage
    {
        /// <summary>
        /// ����
        /// </summary>
        Alpha = 1,

        /// <summary>
        /// ��Ÿ
        /// </summary>
        Beta = 2,

        /// <summary>
        /// ������ �ĺ�
        /// </summary>
        RC = 3,

        /// <summary>
        /// ��ġ
        /// </summary>
        Patch = 4,

        /// <summary>
        /// ����
        /// </summary>
        Stable = 5,
    };

    /// <summary>
    /// ���� ����
    /// </summary>
    public static class VersionInfo
    {
        /// <summary>
        /// ���� ���� ���� ���ø����̼ǿ� ���� �ۺ��� ���� ��ȣ ���ڿ��� ���Ѵ�. ������� "(����#1002)" ���� �����̴�.
        /// </summary>
        /// <param name="rp">���ҽ� ������</param>
        /// <returns>���� ��ȣ ���ڿ�</returns>
        public static string GetPublishBuildNumber(IResourceProvider rp)
        {
            string buildNumber = "";

            try
            {
                System.Deployment.Application.ApplicationDeployment deploy = System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
                buildNumber = "(" + rp.GetString("LABEL_BUILD") + "#" + deploy.CurrentVersion.Revision.ToString() + ")";
            }
            catch (InvalidDeploymentException ide)
            {
                Console.WriteLine("getversion: " + ide.Message);
                buildNumber = "";
            }

            return buildNumber;
        }

        /// <summary>
        /// ���� ���� ���� ���ø����̼ǿ� ���� ���� ��ȣ ���ڿ��� ���Ѵ�. ������� "(����#1002)" ���� �����̴�.
        /// </summary>
        /// <param name="rp">���ҽ� ������</param>
        /// <returns>���� ��ȣ ���ڿ�</returns>
        public static string GetBuildNumber(IResourceProvider rp)
        {
            return GetBuildNumberFrom(rp, Application.ProductVersion);
        }

        /// <summary>
        /// ���� ���ڿ����� ���� ��ȣ ���ڿ��� ���Ѵ�. ������� "(����#1002)" ���� �����̴�.
        /// </summary>
        /// <param name="rp">���ҽ� ������</param>
        /// <param name="versionStr">���� ���ڿ�</param>
        /// <returns>���� ��ȣ ���ڿ�</returns>
        public static string GetBuildNumberFrom(IResourceProvider rp, string versionStr)
        {
            return GetBuildNumberFrom(rp, new Version(versionStr));
        }

        /// <summary>
        /// ���� ��ü���� ���� ��ȣ ���ڿ��� ���Ѵ�. ������� "(����#1002)" ���� �����̴�.
        /// </summary>
        /// <param name="rp">���ҽ� ������</param>
        /// <param name="version">���� ��ü</param>
        /// <returns>���� ��ȣ ���ڿ�</returns>
        public static string GetBuildNumberFrom(IResourceProvider rp, Version version)
        {
            return "(" + rp.GetString("LABEL_BUILD") + "#" + version.Revision.ToString() + ")";
        }

        /// <summary>
        /// ���� ���� ���� ���ø����̼ǿ� ���� ���� ���ڿ�("������ ����.���̳� ����")�� ���Ѵ�.
        /// </summary>
        /// <param name="rp">���ҽ� ������</param>
        /// <returns>���� ���ڿ�("������ ����.���̳� ����")</returns>
        public static string GetVersionOnly(IResourceProvider rp)
        {
            return GetVersionOnlyFrom(rp, Application.ProductVersion);
        }

        /// <summary>
        /// ���� ���ڿ����� ���� ���ڿ�("������ ����.���̳� ����")�� ���Ѵ�.
        /// </summary>
        /// <param name="rp">���ҽ� ������</param>
        /// <param name="versionStr">���� ���ڿ�</param>
        /// <returns>���� ���ڿ�("������ ����.���̳� ����")</returns>
        public static string GetVersionOnlyFrom(IResourceProvider rp, string versionStr)
        {
            return GetVersionFrom(rp, new Version(versionStr));
        }

        /// <summary>
        /// ���� ��ü���� ���� ���� ���ڿ�("������ ����.���̳� ����")�� ���Ѵ�.
        /// </summary>
        /// <param name="version">���� ��ü</param>
        /// <returns>���� ���ڿ�("������ ����.���̳� ����")</returns>
        public static string GetVersionOnlyFrom(Version version)
        {
            return version.Major.ToString() + "."
                + version.Minor.ToString();
        }

        /// <summary>
        /// ���� ���� ���� ���ø����̼ǿ� ���� ���� �ܰ�("����", "��Ÿ" ��)�� ���Ѵ�.
        /// </summary>
        /// <param name="rp">���ҽ� ������</param>
        /// <returns>���� �ܰ�</returns>
        public static string GetVersionStage(IResourceProvider rp)
        {
            return GetVersionStageFrom(rp, Application.ProductVersion);
        }

        /// <summary>
        /// ���� ���ڿ����� ���� �ܰ�("����", "��Ÿ" ��)�� ���Ѵ�.
        /// </summary>
        /// <param name="rp">���ҽ� ������</param>
        /// <param name="versionStr">���� ���ڿ�</param>
        /// <returns>���� �ܰ�("����", "��Ÿ" ��)</returns>
        public static string GetVersionStageFrom(IResourceProvider rp, string versionStr)
        {
            return GetVersionStageFrom(rp, new Version(versionStr));
        }

        /// <summary>
        /// ���� ��ü���� ���� �ܰ�("����", "��Ÿ" ��)�� ���Ѵ�.
        /// </summary>
        /// <param name="rp">���ҽ� ��ü</param>
        /// <param name="version">���� ��ü</param>
        /// <returns>���� �ܰ�("����", "��Ÿ" ��)</returns>
        public static string GetVersionStageFrom(IResourceProvider rp, Version version)
        {
            string stageStr = "";
            VersionStage stage = (VersionStage)version.Build;
            switch (stage)
            {
                case VersionStage.Stable:
                    break;

                case VersionStage.Alpha:
                    stageStr = rp.GetString("LABEL_ALPHA");
                    break;

                case VersionStage.Beta:
                    stageStr = rp.GetString("LABEL_BETA");
                    break;

                case VersionStage.RC:
                    stageStr = rp.GetString("LABEL_RC");
                    break;

                case VersionStage.Patch:
                    stageStr = rp.GetString("LABEL_PATCH");
                    break;
            }

            return stageStr;
        }

        /// <summary>
        /// ���� ���� ���� ���ø����̼ǿ� ���� �ۺ��� ������ ���� �ܰ�("����", "��Ÿ" ��)�� ���Ѵ�.
        /// </summary>
        /// <param name="rp">���ҽ� ������</param>
        /// <returns>���� �ܰ�("����", "��Ÿ" ��)</returns>
        public static string GetPublishVersionStage(IResourceProvider rp)
        {
            Version appVersion = new Version(Application.ProductVersion);
            string stageStr = "";
            VersionStage stage = (VersionStage)appVersion.Build;
            switch (stage)
            {
                case VersionStage.Stable:
                    break;

                case VersionStage.Alpha:
                    stageStr = rp.GetString("LABEL_ALPHA");
                    break;

                case VersionStage.Beta:
                    stageStr = rp.GetString("LABEL_BETA");
                    break;

                case VersionStage.RC:
                    stageStr = rp.GetString("LABEL_RC");
                    break;

                case VersionStage.Patch:
                    stageStr = rp.GetString("LABEL_PATCH");
                    break;
            }

            return stageStr + appVersion.Revision.ToString();
        }

        /// <summary>
        /// ���� ���� ���� ���ø����̼ǿ� ���� �ۺ��� ���� ���ڿ� ���Ѵ�. �ۺ��� ���� ���ڿ��� ���´� "������ ����.���̳� ���� ���� (����#1002)"�� ����.
        /// </summary>
        /// <param name="rp">���ҽ� ������</param>
        /// <returns>�ۺ��� ���� ���ڿ�</returns>
        public static string GetPublishVersion(IResourceProvider rp)
        {
            Version appVersion = new Version(Application.ProductVersion);
            VersionStage stage = (VersionStage)appVersion.Build;

            string appFullVersion = GetVersionOnly(rp);
            if (stage != VersionStage.Stable)
            {
                appFullVersion = appFullVersion + " " + GetVersionStage(rp);
            }

            if (stage != VersionStage.Stable)
            {
                appFullVersion = appFullVersion + " " + GetBuildNumber(rp);
            }

            return appFullVersion;
        }

        /// <summary>
        /// ���� ���� ���� ���ø����̼ǿ� ���� ��ǰ ����(ProducetVersion) ���ڿ� ���Ѵ�.
        /// </summary>
        /// <param name="rp">���ҽ� ������</param>
        /// <returns>��ǰ ����(ProducetVersion) ���ڿ�</returns>
        public static string GetVersion(IResourceProvider rp)
        {
            return GetVersionFrom(rp, Application.ProductVersion);
        }

        /// <summary>
        /// ���� ���ڿ����� ǥ�ÿ� ���� ���ڿ��� ���Ѵ�. ǥ�ÿ� ���� ���ڿ��� ���´� "������ ����.���̳� ���� ���� (����#1002)"�� ����.
        /// </summary>
        /// <param name="rp">���ҽ� ������</param>
        /// <param name="versionStr">���� ���ڿ�</param>
        /// <returns>ǥ�ÿ� ���� ���ڿ�</returns>
        public static string GetVersionFrom(IResourceProvider rp, string versionStr)
        {
            return GetVersionFrom(rp, new Version(versionStr));
        }

        /// <summary>
        /// XML ���Ͽ��� ǥ�ÿ� ���� ���ڿ� ���Ѵ�. ǥ�ÿ� ���� ���ڿ��� ���´� "������ ����.���̳� ���� ���� (����#1002)"�� ����.
        /// </summary>
        /// <param name="rp">���ҽ� ������</param>
        /// <param name="logger">�ΰ�</param>
        /// <param name="xmlFile">XML ���ϸ�</param>
        /// <returns>ǥ�ÿ� ���� ���ڿ�</returns>
        public static string GetVersionFromXml(IResourceProvider rp, ILogger logger, string xmlFile)
        {
            if (File.Exists(xmlFile) == true)
            {
                ApplicationInfo app = new ApplicationInfo();
                if (app.LoadFromXml(logger, xmlFile) == true)
                {
                    return GetVersionFrom(rp, app.FileVersion);
                }
            }

            throw new FileNotFoundException("Can't found version info file.");
        }

        /// <summary>
        /// ���� ��ü���� ǥ�ÿ� ���� ���ڿ��� ���Ѵ�. ǥ�ÿ� ���� ���ڿ��� ���´� "������ ����.���̳� ���� ���� (����#1002)"�� ����.
        /// </summary>
        /// <param name="rp">���ҽ� ������</param>
        /// <param name="version">���� ��ü</param>
        /// <returns>ǥ�ÿ� ���� ���ڿ�</returns>
        public static string GetVersionFrom(IResourceProvider rp, Version version)
        {
            VersionStage stage = (VersionStage)version.Build;

            string appFullVersion = GetVersionOnlyFrom(version);
            if (!String.IsNullOrEmpty(GetVersionStageFrom(rp, version)))
            {
                appFullVersion = appFullVersion + " " + GetVersionStageFrom(rp, version) + " " + GetBuildNumberFrom(rp, version);
            }
            else
            {
                appFullVersion = appFullVersion + " " + GetBuildNumberFrom(rp, version);
            }

            return appFullVersion;
        }

    }
}
