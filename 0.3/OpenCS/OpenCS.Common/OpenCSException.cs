using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCS.Common
{
    /// <summary>
    /// General exception in OpenC# library.
    /// </summary>
    public class OpenCSException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Message.</param>
        public OpenCSException(string message)
            : base(message)
        {
        }
    }
}
