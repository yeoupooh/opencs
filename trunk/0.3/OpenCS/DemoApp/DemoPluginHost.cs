using System;
using System.Collections.Generic;
using System.Text;
using OpenCS.Common.Action;
using OpenCS.Common.Plugin;

namespace DemoApp
{
    public class DemoPluginHost : BasePluginHost
    {
        public override ActionResult HandleAction(IAction action)
        {
            return ActionResult.NotHandled;
        }
    }
}
