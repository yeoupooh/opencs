using System;
using System.Collections.Generic;
using System.Text;
using OpenCS.Common.Action;

namespace OpenCS.Common.Plugin
{
    /// <summary>
    /// Interface of plugin host. 플러그인 호스트
    /// 플러그인들 관리하는 주체.
    /// </summary>
    public interface IPluginHost : IActionHandler
    {
        /// <summary>
        /// Gets or sets path of executing plugin host.
        /// 플러그인 호스트(보통 어플리케이션)가 실행 중인 절대경로를 가져오거나 설정한다. 
        /// 설정은 <c>IPluginHost</c>를 사용하는 객체나 어플리케이션에서 하도록 한다.
        /// </summary>
        string ExecutingPath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets list of plugins. 플러그인 목록을 가져온다.
        /// </summary>
        List<IPlugin> Plugins
        {
            get;
        }
        
        /// <summary>
        /// Load all plugins. 플러그인들을 로딩한다.
        /// </summary>
        /// <param name="baseFolder">Base folder which is location of plugins. 플러그인들이 위치한 기본 폴더</param>
        void LoadPlugins(string baseFolder);

        /// <summary>
        /// Unload all plugins. 플러그인들을 해제한다.
        /// </summary>
        void UnloadPlugins();
    }
}
