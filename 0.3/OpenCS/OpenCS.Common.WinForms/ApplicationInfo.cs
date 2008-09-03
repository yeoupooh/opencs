using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml;
using System.Windows.Forms;
using OpenCS.Common.Logging;

namespace OpenCS.Common.WinForms
{
    /// <summary>
    /// ���ø����̼� ������ �����Ѵ�.
    /// </summary>
    public class ApplicationInfo
    {
        private const string UPDATE_INFO_FILE = "autoupdate.txt";
        private const string ELEMENT_APPLICATION = "Application";
        private const string ELEMENT_AUTOUPDATE = "AutoUpdate";
        private const string ATTR_VERSION = "Version";
        private const string ATTR_FILE_VERSION = "FileVersion";
        private const string ATTR_NAME = "Name";
        private const string ATTR_REMOTE_URL = "RemoteUrl";

        private string m_version;
        private string m_name;
        private string m_remoteUrl;
        private string m_tempFolder;
        private Version m_fileVersion;

        private Version m_remoteVersion;
        private string[] m_updateFiles;
        private string m_infoUrl;

        /// <summary>
        /// ���ø����̼��� ���� ���ڿ�(AssemblyVersion)�� �������ų� �����Ѵ�. �ڵ� ������Ʈ �� �� ���� ������ �������� �ʴ´�. <c>FileVersion</c>�� �����Ѵ�.
        /// </summary>
        public string Version
        {
            get { return m_version; }
        }

        /// <summary>
        /// ���ø����̼��� �̸��� �������ų� �����Ѵ�.
        /// </summary>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        /// <summary>
        /// ���ø����̼��� ������Ʈ �ּ�(URL)�� �������ų� �����Ѵ�.
        /// </summary>
        public string RemoteUrl
        {
            get { return m_remoteUrl; }
            set { m_remoteUrl = value; }
        }

        /// <summary>
        /// �ӽ� ���� ��θ� �������ų� �����Ѵ�.
        /// </summary>
        public string TempFolder
        {
            get { return m_tempFolder; }
            set { m_tempFolder = value; }
        }

        /// <summary>
        /// ���ø����̼��� ���� ������ �������ų� �����Ѵ�. �ڵ� ������Ʈ�� �� ���� ������ �����Ѵ�.
        /// </summary>
        public Version FileVersion
        {
            get { return m_fileVersion; }
            set { m_fileVersion = value; }
        }

        /// <summary>
        /// �������� �����ϴ� ���ø����̼� ���� ������ �������ų� �����Ѵ�. �ڵ� ������Ʈ�� �����ȴ�.
        /// </summary>
        public Version RemoteVersion
        {
            get { return m_remoteVersion; }
            set { m_remoteVersion = value; }
        }

        /// <summary>
        /// �ڵ� ������Ʈ�Ǵ� ���� ����� �������ų� �����Ѵ�.
        /// </summary>
        public string[] UpdateFiles
        {
            get { return m_updateFiles; }
            set { m_updateFiles = value; }
        }

        /// <summary>
        /// �ڵ� ������Ʈ�� �����ϴ� �� ����Ʈ �ּҸ� �������ų� �����Ѵ�.
        /// </summary>
        public string InfoUrl
        {
            get { return m_infoUrl; }
            set { m_infoUrl = value; }
        }

        /// <summary>
        /// ������ ���ø����̼� ���� ������ ���Ѵ�.
        /// </summary>
        /// <param name="logger">�ΰ�</param>
        /// <param name="remoteUrl">������Ʈ �ּ�</param>
        /// <param name="tempFolder">�ӽ� ���� ���</param>
        /// <returns>�����ϸ� <c>true</c></returns>
        public bool GetRemoteVersion(ILogger logger, string remoteUrl, string tempFolder)
        {
            m_remoteUrl = remoteUrl;
            m_tempFolder = tempFolder;

            return GetRemoteVersion(logger, tempFolder);
        }

        /// <summary>
        /// ������ ���ø����̼� ���� ������ ���Ѵ�. �� �Լ��� ȣ���ϱ� ���� ������Ʈ �ּҰ� �����Ǿ� �־�� �Ѵ�.
        /// </summary>
        /// <param name="logger">�ΰ�</param>
        /// <param name="tempFolder">�ӽ� ���� ���</param>
        /// <returns>�����ϸ� <c>true</c></returns>
        public bool GetRemoteVersion(ILogger logger, string tempFolder)
        {
            if (m_remoteUrl != null)
            {
                m_tempFolder = tempFolder;

                // get remote version
                WebClient web = new WebClient();
                try
                {
                    string downStr = web.DownloadString(m_remoteUrl + "/" + UPDATE_INFO_FILE);
                    string[] downStrs = downStr.Split('|');
                    string remoteVersionStr = downStrs[0];
                    m_remoteVersion = new Version(remoteVersionStr);
                    m_updateFiles = downStrs[1].Split(',');
                    m_infoUrl = downStrs[2];

                    return true;
                }
                catch (ArgumentException e)
                {
                    // network�� ���� ��� downStr�� �� html�� ���� ��찡 �ִ�.
                    logger.Debug(e.Message);
                    logger.Debug(e.StackTrace);
                }
                catch (WebException e)
                {
                    // �� ������ ó������ �ʴ´�.
                    logger.Debug(e.Message);
                    logger.Debug(e.StackTrace);
                }
            }

            return false;
        }

        /// <summary>
        /// ���ø����̼� ������ XML ���Ͽ��� �о�´�.
        /// </summary>
        /// <param name="logger">�ΰ�</param>
        /// <param name="xmlFile">XML ����</param>
        /// <returns>�����ϸ� <c>true</c></returns>
        public bool LoadFromXml(ILogger logger, string xmlFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFile);

            // version check
            XmlNode xnApplication = xmlDoc.SelectSingleNode(string.Format("/{0}", ELEMENT_APPLICATION));
            m_version = xnApplication.Attributes[ATTR_VERSION].Value;
            if (m_version != null && m_version == "1.0")
            {
                // get remote url
                XmlNode xnAutoUpdate = xmlDoc.SelectSingleNode(string.Format("/{0}/{1}", ELEMENT_APPLICATION, ELEMENT_AUTOUPDATE));
                m_remoteUrl = xnAutoUpdate.Attributes[ATTR_REMOTE_URL].Value;

                // get application info
                m_name = xnApplication.Attributes[ATTR_NAME].Value;
                string localVersionStr = xnApplication.Attributes[ATTR_FILE_VERSION].Value;
                m_fileVersion = new Version(localVersionStr);

                return true;
            }
            else
            {
                logger.Error("Unknown AutoUpdate.xml version.");
            }

            return false;
        }

        /// <summary>
        /// ���� ���� ���� ���ø����̼� ������ ���Ѵ�.
        /// </summary>
        /// <returns>�����ϸ� <c>true</c></returns>
        public bool Load()
        {
            // get local version
            string localVersionStr = System.Windows.Forms.Application.ProductVersion;
            m_fileVersion = new Version(localVersionStr);

            return true;
        }

        /// <summary>
        /// ���� ���� ���� ���ø����̼��� ���� ��θ� �����´�. ���� �ּҴ� http://www.thescarms.com/dotNet/apppath.aspx �̴�.
        /// </summary>
        public static string Path
        {
            get
            {
                string path;

                // ref: http://www.thescarms.com/dotNet/apppath.aspx
                // �Ʒ��� �������� ����� ��� �ٸ��� ���� �� �ִ�.
                //return System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
                path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                // ref: http://msdn2.microsoft.com/en-us/library/aa457089.aspx
                // uri ���·� return�ȴ�. file://
                //return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

                return path;
            }
        }
    }

}
