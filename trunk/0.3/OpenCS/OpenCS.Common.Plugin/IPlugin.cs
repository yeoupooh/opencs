using System;
using System.Collections.Generic;
using System.Text;
using OpenCS.Common.Action;

namespace OpenCS.Common.Plugin
{
    /// <summary>
    /// 플러그인
    /// 플러그인 호스트에서 실행된다.
    /// </summary>
    public interface IPlugin : IActionHandler
    {
        /// <summary>
        /// 플러그인 호스트를 설정한다.
        /// </summary>
        IPluginHost PluginHost
        {
            get;
            set;
        }

        /// <summary>
        /// 제목을 가져온다.
        /// </summary>
        string Title
        {
            get;
        }

        /// <summary>
        /// 버전을 가져온다.
        /// </summary>
        Version Version
        {
            get;
        }

        /// <summary>
        /// 설치된 절대경로를 가져오거나 설정한다. 
        /// 설정은 <c>IPluginHost</c>에서만 하도록 한다.
        /// </summary>
        string InstalledPath
        {
            get;
            set;
        }

        /// <summary>
        /// 플러그인을 초기화한다.
        /// </summary>
        void Init();

        /// <summary>
        /// 플러그인을 해제한다.
        /// </summary>
        void Deinit();
    }
}
