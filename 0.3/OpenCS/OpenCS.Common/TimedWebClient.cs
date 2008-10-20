using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace OpenCS.Common
{
    /// <summary>
    /// Timeout-enalbed WebClient.
    /// </summary>
    public class TimedWebClient : WebClient
    {
        private int mTimeout;

        /// <summary>
        /// Gets or sets timeout(miliseconds).
        /// </summary>
        public int Timeout
        {
            get { return mTimeout; }
            set { mTimeout = value; }
        }

        /// <summary>
        /// Constructur.
        /// </summary>
        /// <param name="timeout">Miliseconds for timeout.</param>
        public TimedWebClient(int timeout)
        {
            mTimeout = timeout;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest wr =  base.GetWebRequest(address);
            wr.Timeout = Timeout;

            return wr;
        }
    }
}
