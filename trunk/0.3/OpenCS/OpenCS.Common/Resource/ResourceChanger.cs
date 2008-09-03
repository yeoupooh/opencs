using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using OpenCS.Common.Logging;
using OpenCS.Common.Resource;

namespace OpenCS.Common.Resource
{
    /// <summary>
    /// 기본 리소스 변경자
    /// </summary>
    public class ResourceChanger : IResourceChanger
    {
        #region Fields

        private List<IResourceChangeFilter> m_filters = new List<IResourceChangeFilter>();
        private IResourceProvider m_rp;
        private ILogger m_logger;

        #endregion Fields

        #region Properties

        #endregion Properties

        #region Constructors

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="rp">리소스 제공자</param>
        /// <param name="logger">로거</param>
        public ResourceChanger(IResourceProvider rp, ILogger logger)
        {
            m_rp = rp;
            m_logger = logger;
        }

        #endregion Constructors

        #region Private Functions

        #region Initialization

        #endregion Initialization

        #region Event handling

        #region Form

        #endregion Form

        #region Buttons

        #endregion Buttons

        #region Menus

        #endregion Menus

        #endregion Event handling

        #endregion Private Functions

        #region Protected Functions

        /// <summary>
        /// .NET에서 제공하는 기본 형태에 대해서 리소스를 변경한다.
        /// </summary>
        /// <param name="obj">변경할 객체</param>
        /// <returns>성공하면 true</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        protected bool ChangeCommonTypes(object obj)
        {
            if (obj is Control)
            {
                foreach (Control childControl in (obj as Control).Controls)
                {
                    Change(childControl);
                }
                return true;
            }
            else if (obj is IContainer)
            {
                foreach (Component child in (obj as IContainer).Components)
                {
                    Change(child);
                }
                return true;
            }
            else if (obj is Component)
            {
                Component component = obj as Component;
                if (component.Container != null)
                {
                    Change(component.Container);
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// 객체의 문자열 속성을 변경한다.
        /// </summary>
        /// <param name="obj">변경할 객체</param>
        protected void ChangeText(object obj)
        {
            if (obj == null)
            {
                return;
            }

            PropertyInfo[] props = obj.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if ((prop.Name == "Text" || prop.Name == "Caption" || prop.Name == "TabText") &&
                    prop.CanRead == true && prop.CanWrite == true
                    && prop.PropertyType == typeof(string))
                {
                    try
                    {
                        string resourceId = (string)prop.GetValue(obj, new object[] { });
                        if (resourceId.Length > 0)
                        {
                            string newResource = m_rp.GetString(resourceId);
                            if (newResource.Length > 0)
                            {
                                try
                                {
                                    prop.SetValue(obj, newResource, new object[] { });
                                }
                                catch (TargetInvocationException e)
                                {
                                    m_logger.Warn(string.Format(CultureInfo.CurrentCulture, "Can't change resource: obj={0}, prop={1}", obj.ToString(), prop.Name));
                                    m_logger.Debug(e.Message);
                                }
                            }
                        }
                    }
                    catch (TargetInvocationException)
                    {
                        m_logger.Warn(string.Format(CultureInfo.CurrentCulture, "Can't change resource: obj={0}, prop={1}", obj.ToString(), prop.Name));
                    }
                }
            }
        }

        #endregion Protected Functions

        #region Public Functions

        #endregion Public Functions

        #region IResourceChanger 멤버

        /// <summary>
        /// 리소스 제공자를 설정한다.
        /// </summary>
        public IResourceProvider ResourceProvider
        {
            set { m_rp = value; }
        }

        /// <summary>
        /// 객체의 리소스를 바꾼다. 
        /// </summary>
        /// <param name="obj">바꿀 객체</param>
        public void Change(object obj)
        {
            if (obj != null)
            {
                foreach (IResourceChangeFilter filter in m_filters)
                {
                    if (filter.Filter(this, obj) == true)
                    {
#if DEBUG
                        Debug.Print("change obj=" + obj.ToString());
#endif
                        ChangeText(obj);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// IResourceChangeable 객체의 리소스를 바꾼다.
        /// </summary>
        /// <param name="resourceChangeable">IResourceChangeable 객체</param>
        public void Change(IResourceChangeable resourceChangeable)
        {
            // CA1062: 유효성 검사
            if (resourceChangeable == null)
            {
                throw new ArgumentNullException("resourceChangeable");
            }

            resourceChangeable.Text = m_rp.GetString(resourceChangeable.Text);
        }

        /// <summary>
        /// 리소스 변경 필터를 추가한다.
        /// </summary>
        /// <param name="filter">리소스 변경 필터</param>
        public void AddFilter(IResourceChangeFilter filter)
        {
            m_filters.Add(filter);
        }

        #endregion
    }
}
