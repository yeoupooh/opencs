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
    /// 버전 단계
    /// </summary>
    public enum VersionStage
    {
        /// <summary>
        /// 알파
        /// </summary>
        Alpha = 1,

        /// <summary>
        /// 베타
        /// </summary>
        Beta = 2,

        /// <summary>
        /// 릴리즈 후보
        /// </summary>
        RC = 3,

        /// <summary>
        /// 패치
        /// </summary>
        Patch = 4,

        /// <summary>
        /// 안정
        /// </summary>
        Stable = 5,
    };

    /// <summary>
    /// 버전 정보
    /// </summary>
    public static class VersionInfo
    {
        /// <summary>
        /// 현재 실행 중인 어플리케이션에 대한 퍼블리쉬 빌드 번호 문자열를 구한다. 결과값은 "(빌드#1002)" 같은 형태이다.
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <returns>빌드 번호 문자열</returns>
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
        /// 현재 실행 중인 어플리케이션에 대한 빌드 번호 문자열를 구한다. 결과값은 "(빌드#1002)" 같은 형태이다.
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <returns>빌드 번호 문자열</returns>
        public static string GetBuildNumber(IResourceProvider rp)
        {
            return GetBuildNumberFrom(rp, Application.ProductVersion);
        }

        /// <summary>
        /// 버전 문자열에서 빌드 번호 문자열를 구한다. 결과값은 "(빌드#1002)" 같은 형태이다.
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <param name="versionStr">버전 문자열</param>
        /// <returns>빌드 번호 문자열</returns>
        public static string GetBuildNumberFrom(IResourceProvider rp, string versionStr)
        {
            return GetBuildNumberFrom(rp, new Version(versionStr));
        }

        /// <summary>
        /// 버전 객체에서 빌드 번호 문자열을 구한다. 결과값은 "(빌드#1002)" 같은 형태이다.
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <param name="version">버전 객체</param>
        /// <returns>빌드 번호 문자열</returns>
        public static string GetBuildNumberFrom(IResourceProvider rp, Version version)
        {
            return "(" + rp.GetString("LABEL_BUILD") + "#" + version.Revision.ToString() + ")";
        }

        /// <summary>
        /// 현재 실행 중인 어플리케이션에 대한 버전 문자열("메이저 버전.마이너 버전")을 구한다.
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <returns>버전 문자열("메이저 버전.마이너 버전")</returns>
        public static string GetVersionOnly(IResourceProvider rp)
        {
            return GetVersionOnlyFrom(rp, Application.ProductVersion);
        }

        /// <summary>
        /// 버전 문자열에서 버전 문자열("메이저 버전.마이너 버전")을 구한다.
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <param name="versionStr">버전 문자열</param>
        /// <returns>버전 문자열("메이저 버전.마이너 버전")</returns>
        public static string GetVersionOnlyFrom(IResourceProvider rp, string versionStr)
        {
            return GetVersionFrom(rp, new Version(versionStr));
        }

        /// <summary>
        /// 버전 객체에서 대한 버전 문자열("메이저 버전.마이너 버전")을 구한다.
        /// </summary>
        /// <param name="version">버전 객체</param>
        /// <returns>버전 문자열("메이저 버전.마이너 버전")</returns>
        public static string GetVersionOnlyFrom(Version version)
        {
            return version.Major.ToString() + "."
                + version.Minor.ToString();
        }

        /// <summary>
        /// 현재 실행 중인 어플리케이션에 대한 버전 단계("알파", "베타" 등)를 구한다.
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <returns>버전 단계</returns>
        public static string GetVersionStage(IResourceProvider rp)
        {
            return GetVersionStageFrom(rp, Application.ProductVersion);
        }

        /// <summary>
        /// 버전 문자열에서 버전 단계("알파", "베타" 등)를 구한다.
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <param name="versionStr">버전 문자열</param>
        /// <returns>버전 단계("알파", "베타" 등)</returns>
        public static string GetVersionStageFrom(IResourceProvider rp, string versionStr)
        {
            return GetVersionStageFrom(rp, new Version(versionStr));
        }

        /// <summary>
        /// 버전 객체에서 버전 단계("알파", "베타" 등)를 구한다.
        /// </summary>
        /// <param name="rp">리소스 객체</param>
        /// <param name="version">버전 객체</param>
        /// <returns>버전 단계("알파", "베타" 등)</returns>
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
        /// 현재 실행 중인 어플리케이션에 대한 퍼블리쉬 버전의 버전 단계("알파", "베타" 등)를 구한다.
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <returns>버전 단계("알파", "베타" 등)</returns>
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
        /// 현재 실행 중인 어플리케이션에 대한 퍼블리쉬 버전 문자열 구한다. 퍼블리쉬 버전 문자열의 형태는 "메이저 버전.마이너 버전 알파 (빌드#1002)"와 같다.
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <returns>퍼블리쉬 버전 문자열</returns>
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
        /// 현재 실행 중인 어플리케이션에 대한 제품 버전(ProducetVersion) 문자열 구한다.
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <returns>제품 버전(ProducetVersion) 문자열</returns>
        public static string GetVersion(IResourceProvider rp)
        {
            return GetVersionFrom(rp, Application.ProductVersion);
        }

        /// <summary>
        /// 버전 문자열에서 표시용 버전 문자열을 구한다. 표시용 버전 문자열의 형태는 "메이저 버전.마이너 버전 알파 (빌드#1002)"와 같다.
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <param name="versionStr">버전 문자열</param>
        /// <returns>표시용 버전 문자열</returns>
        public static string GetVersionFrom(IResourceProvider rp, string versionStr)
        {
            return GetVersionFrom(rp, new Version(versionStr));
        }

        /// <summary>
        /// XML 파일에서 표시용 버전 문자열 구한다. 표시용 버전 문자열의 형태는 "메이저 버전.마이너 버전 알파 (빌드#1002)"와 같다.
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <param name="logger">로거</param>
        /// <param name="xmlFile">XML 파일명</param>
        /// <returns>표시용 버전 문자열</returns>
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
        /// 버전 객체에서 표시용 버전 문자열을 구한다. 표시용 버전 문자열의 형태는 "메이저 버전.마이너 버전 알파 (빌드#1002)"와 같다.
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <param name="version">버전 객체</param>
        /// <returns>표시용 버전 문자열</returns>
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
