using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common.AutoUpdate
{
    /// <summary>
    /// Exception while auto updating.
    /// </summary>
    public class AutoUpdateException : OpenCSException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Message</param>
        public AutoUpdateException(string message)
            : base(message)
        {
        }
    }
}
