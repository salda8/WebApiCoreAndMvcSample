using System;
using System.IO;
using System.Net;

namespace MvcCore.Api
{
    // The RequestState class passes data across async calls.
    /// <summary>
    /// Class RequestState.
    /// </summary>
    public class RequestState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestState" /> class.
        /// </summary>
        public RequestState()
        {
            Request = null;
            ResponseStream = null;
            WriteStream = null;
            Callback = null;
        }

        /// <summary>
        /// The request
        /// </summary>
        /// <value>The request.</value>
        public WebRequest Request { get; }

        /// <summary>
        /// The response stream
        /// </summary>
        /// <value>The response stream.</value>
        public Stream ResponseStream { get; }

        /// <summary>
        /// The callback
        /// </summary>
        /// <value>The callback.</value>
        public Action Callback { get; }

        /// <summary>
        /// Gets or sets the write stream.
        /// </summary>
        /// <value>The write stream.</value>
        public Stream WriteStream { get; set; }
    }
}