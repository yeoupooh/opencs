﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using OpenCS.Common.Action;

namespace OpenCS.Common.Plugin
{
    /// <summary>
    /// Base plugin host implementation.
    /// </summary>
    abstract public class BasePluginHost : IPluginHost
    {
        private string mExecutingPath;
        private List<IPlugin> mPlugins = new List<IPlugin>();

        #region IActionHandler 멤버

        /// <summary>
        /// Handle action. 
        /// </summary>
        /// <param name="action">Action.</param>
        /// <returns>Result of handling action.</returns>
        abstract public ActionResult HandleAction(IAction action);

        #endregion

        #region IPluginHost 멤버

        /// <summary>
        /// Gets or sets path of executing plugin host.
        /// </summary>
        public string ExecutingPath
        {
            get { return mExecutingPath; }
            set { mExecutingPath = value; }
        }

        /// <summary>
        /// Gets list of plugins.
        /// </summary>
        public List<IPlugin> Plugins
        {
            get { return mPlugins; }
        }

        /// <summary>
        /// Load all plugins. 
        /// </summary>
        /// <param name="baseFolder">Base folder which is location of plugins. </param>
        public void LoadPlugins(string baseFolder)
        {
            if (Directory.Exists(baseFolder) == false)
            {
                //mLogger.Warn("Plugins folder is not found.");
                return;
            }

            foreach (string pluginFolder in Directory.GetDirectories(baseFolder))
            {
                foreach (string file in Directory.GetFiles(pluginFolder, "*Plugin.dll"))
                {
                    //if (file.EndsWith("Plugin.dll") == true)
                    {
                        Assembly asm = Assembly.LoadFrom(file);
                        try
                        {
                            foreach (Type type in asm.GetTypes())
                            {
                                //mLogger.Debug(type.ToString());
                                Type ifType = type.GetInterface(typeof(IPlugin).FullName, false);
                                if (ifType != null && type.IsAbstract == false)
                                {
                                    //mLogger.Info("Found plugin: " + type.FullName);
                                    object obj = asm.CreateInstance(type.FullName);
                                    if (obj != null && obj is IPlugin)
                                    {
                                        IPlugin plugin = obj as IPlugin;
                                        //mLogger.Info("Plugin: " + plugin.Title);
                                        plugin.PluginHost = this;

                                        plugin.InstalledPath = Path.GetDirectoryName(file);
                                        plugin.Init();
                                        //mLogger.Info("Plugin Inited: " + plugin.Title);

                                        mPlugins.Add(plugin);
                                        //mDCPlugins.AddPlugin(plugin);
                                    }
                                }
                            }
                        }
                        catch (ReflectionTypeLoadException)
                        {
                            //mLogger.Debug(ex.Message);
                            //mLogger.Warn(ex.Message);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Unload all plugins.
        /// </summary>
        public void UnloadPlugins()
        {
            foreach (IPlugin plugin in mPlugins)
            {
                plugin.Deinit();
                //mLogger.Info("Plugin Deinited: " + plugin.Title);
            }
        }

        #endregion

    }
}
