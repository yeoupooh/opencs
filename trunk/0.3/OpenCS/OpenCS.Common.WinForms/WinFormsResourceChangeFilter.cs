using System;
using System.Collections.Generic;
using System.Text;
using OpenCS.Common.Resource;
using System.Windows.Forms;
using System.ComponentModel;

namespace OpenCS.Common.WinForms
{
    /// <summary>
    /// <c>System.Windows.Forms</c>와 관련된 콘트롤 리소스 변경 필터.
    /// </summary>
    public class WinFormsResourceChangeFilter : IResourceChangeFilter
    {
        #region IResourceChangeFilter 멤버

        /// <summary>
        /// 리소스 변경에 대해 필터링한다.
        /// </summary>
        /// <param name="rc">리소스 변경자</param>
        /// <param name="obj">변경할 객체</param>
        /// <returns>변경이 될 객체이면 <c>true</c></returns>
        public bool Filter(IResourceChanger rc, object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is TreeView)
            {
                rc.Change((obj as TreeView).ContextMenuStrip);

                return true;
            }

            if (obj is ListView)
            {
                foreach (ColumnHeader header in (obj as ListView).Columns)
                {
                    rc.Change(header);
                }

                return true;
            }

            if (obj is TabControl)
            {
                foreach (TabPage tab in (obj as TabControl).TabPages)
                {
                    rc.Change(tab);
                }

                return true;
            }

            if (obj is ToolStripMenuItem)
            {
                rc.Change((obj as ToolStripMenuItem).DropDown);

                return true;
            }

            if (obj is MenuStrip)
            {
                foreach (ToolStripItem item in (obj as MenuStrip).Items)
                {
                    rc.Change(item);
                }

                return true;
            }

            if (obj is ToolStrip)
            {
                foreach (ToolStripItem item in (obj as ToolStrip).Items)
                {
                    rc.Change(item);
                }

                return true;
            }

            if (obj is ToolStripDropDownItem)
            {
                foreach (ToolStripItem item in (obj as ToolStripDropDownItem).DropDownItems)
                {
                    rc.Change(item);
                }

                return true;
            }

            if (obj is ToolStripPanelRow)
            {
                foreach (Control control in (obj as ToolStripPanelRow).Controls)
                {
                    rc.Change(control);
                }

                return true;
            }

            if (obj is ToolStripPanel)
            {
                ToolStripPanel tsp = obj as ToolStripPanel;
                foreach (ToolStripPanelRow row in tsp.Rows)
                {
                    rc.Change(row);
                }

                return true;
            }

            if (obj is ToolStripContainer)
            {
                ToolStripContainer tsc = obj as ToolStripContainer;
                rc.Change(tsc.ContentPanel);
                rc.Change(tsc.LeftToolStripPanel);
                rc.Change(tsc.RightToolStripPanel);
                rc.Change(tsc.TopToolStripPanel);
                rc.Change(tsc.BottomToolStripPanel);

                return true;
            }

            if (obj is Form)
            {
                rc.Change((obj as Form).MainMenuStrip);
            }

            if (obj is Control)
            {
                foreach (Control childControl in (obj as Control).Controls)
                {
                    rc.Change(childControl);
                }

                return true;
            }
            else if (obj is IContainer)
            {
                foreach (Component child in (obj as IContainer).Components)
                {
                    rc.Change(child);
                }

                return true;
            }
            else if (obj is Component)
            {
                if ((obj as Component).Container != null)
                {
                    rc.Change((obj as Component).Container);
                }

                return true;
            }

            return false;
        }

        #endregion
    }
}
