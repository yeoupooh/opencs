using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.Resource
{
    /// <summary>
    /// Dummy implementation of <c>IResourceProvider</c>.
    /// </summary>
    public class DummyResourceProvider : IResourceProvider
    {
        #region IResourceProvider 멤버

        /// <summary>
        /// Gets string resource.
        /// </summary>
        /// <param name="resourceId">Resource Id.</param>
        /// <returns>String resource.</returns>
        /// <summary>
        public string GetString(string resourceId)
        {
            return resourceId;
        }

        #endregion
    }
}
