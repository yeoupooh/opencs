using System;
using System.Collections.Generic;
using System.Text;
using OpenCS.Common.Action;

namespace OpenCS.Common.Plugin
{
    /// <summary>
    /// 기본 구현 플러그인
    /// </summary>
    abstract public class BasePlugin : IPlugin
    {
        /// <summary>
        /// 플러그인 호스트
        /// </summary>
        private IPluginHost mHost;

        private string mInstalledPath;

        /// <summary>
        /// 생성자
        /// </summary>
        protected BasePlugin()
        {
        }

        #region IActionHandler 멤버

        /// <summary>
        /// 액션을 처리한다.
        /// </summary>
        /// <param name="action">액션</param>
        /// <returns>처리 결과</returns>
        abstract public ActionResult HandleAction(IAction action);

        #endregion

        #region IPlugin 멤버

        /// <summary>
        /// 플러그인 호스트를 설정한다.
        /// </summary>
        public IPluginHost PluginHost
        {
            get { return mHost; }
            set { mHost = value; }
        }

        /// <summary>
        /// 제목을 가져온다.
        /// </summary>
        abstract public string Title
        {
            get;
        }

        /// <summary>
        /// 버전을 가져온다.
        /// </summary>
        abstract public Version Version
        {
            get;
        }

        /// <summary>
        /// 설치된 경로를 가져오거나 설정한다. 
        /// 설정은 <c>IPluginHost</c>에서만 하도록 한다.
        /// </summary>
        public string InstalledPath
        {
            get { return mInstalledPath; }

            set { mInstalledPath = value; }
        }

        /// <summary>
        /// 플러그인을 초기화한다.
        /// </summary>
        abstract public void Init();

        /// <summary>
        /// 플러그인을 해제한다.
        /// </summary>
        abstract public void Deinit();

        #endregion
    }
}
