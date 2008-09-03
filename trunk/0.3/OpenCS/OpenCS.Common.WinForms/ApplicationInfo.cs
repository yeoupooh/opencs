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
    /// 어플리케이션 정보를 제공한다.
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
        /// 어플리케이션의 버전 문자열(AssemblyVersion)을 가져오거나 설정한다. 자동 업데이트 시 이 버전 정보를 참고하지 않는다. <c>FileVersion</c>을 참고한다.
        /// </summary>
        public string Version
        {
            get { return m_version; }
        }

        /// <summary>
        /// 어플리케이션의 이름을 가져오거나 설정한다.
        /// </summary>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        /// <summary>
        /// 어플리케이션의 업데이트 주소(URL)를 가져오거나 설정한다.
        /// </summary>
        public string RemoteUrl
        {
            get { return m_remoteUrl; }
            set { m_remoteUrl = value; }
        }

        /// <summary>
        /// 임시 저장 경로를 가져오거나 설정한다.
        /// </summary>
        public string TempFolder
        {
            get { return m_tempFolder; }
            set { m_tempFolder = value; }
        }

        /// <summary>
        /// 어플리케이션의 파일 버전을 가져오거나 설정한다. 자동 업데이트시 이 버전 정보를 참고한다.
        /// </summary>
        public Version FileVersion
        {
            get { return m_fileVersion; }
            set { m_fileVersion = value; }
        }

        /// <summary>
        /// 서버에서 제공하는 어플리케이션 파일 버전을 가져오거나 설정한다. 자동 업데이트시 설정된다.
        /// </summary>
        public Version RemoteVersion
        {
            get { return m_remoteVersion; }
            set { m_remoteVersion = value; }
        }

        /// <summary>
        /// 자동 업데이트되는 파일 목록을 가져오거나 설정한다.
        /// </summary>
        public string[] UpdateFiles
        {
            get { return m_updateFiles; }
            set { m_updateFiles = value; }
        }

        /// <summary>
        /// 자동 업데이트시 접속하는 웹 사이트 주소를 가져오거나 설정한다.
        /// </summary>
        public string InfoUrl
        {
            get { return m_infoUrl; }
            set { m_infoUrl = value; }
        }

        /// <summary>
        /// 서버의 어플리케이션 파일 버전을 구한다.
        /// </summary>
        /// <param name="logger">로거</param>
        /// <param name="remoteUrl">업데이트 주소</param>
        /// <param name="tempFolder">임시 저장 경로</param>
        /// <returns>성공하면 <c>true</c></returns>
        public bool GetRemoteVersion(ILogger logger, string remoteUrl, string tempFolder)
        {
            m_remoteUrl = remoteUrl;
            m_tempFolder = tempFolder;

            return GetRemoteVersion(logger, tempFolder);
        }

        /// <summary>
        /// 서버의 어플리케이션 파일 버전을 구한다. 이 함수를 호출하기 전에 업데이트 주소가 설정되어 있어야 한다.
        /// </summary>
        /// <param name="logger">로거</param>
        /// <param name="tempFolder">임시 저장 경로</param>
        /// <returns>성공하면 <c>true</c></returns>
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
                    // network이 끊긴 경우 downStr에 긴 html이 들어가는 경우가 있다.
                    logger.Debug(e.Message);
                    logger.Debug(e.StackTrace);
                }
                catch (WebException e)
                {
                    // 이 에러는 처리하지 않는다.
                    logger.Debug(e.Message);
                    logger.Debug(e.StackTrace);
                }
            }

            return false;
        }

        /// <summary>
        /// 어플리케이션 정보를 XML 파일에서 읽어온다.
        /// </summary>
        /// <param name="logger">로거</param>
        /// <param name="xmlFile">XML 파일</param>
        /// <returns>성공하면 <c>true</c></returns>
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
        /// 현재 실행 중인 어플리케이션 정보를 구한다.
        /// </summary>
        /// <returns>성공하면 <c>true</c></returns>
        public bool Load()
        {
            // get local version
            string localVersionStr = System.Windows.Forms.Application.ProductVersion;
            m_fileVersion = new Version(localVersionStr);

            return true;
        }

        /// <summary>
        /// 현재 실행 중인 어플리케이션의 실행 경로를 가져온다. 참고 주소는 http://www.thescarms.com/dotNet/apppath.aspx 이다.
        /// </summary>
        public static string Path
        {
            get
            {
                string path;

                // ref: http://www.thescarms.com/dotNet/apppath.aspx
                // 아래는 여러개의 모듈일 경우 다르게 나올 수 있다.
                //return System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
                path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                // ref: http://msdn2.microsoft.com/en-us/library/aa457089.aspx
                // uri 형태로 return된다. file://
                //return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

                return path;
            }
        }
    }

}
