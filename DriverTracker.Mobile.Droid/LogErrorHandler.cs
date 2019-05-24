using System;
using Android.Content.Res;
using Android.Util;

namespace DriverTracker.Mobile.Droid
{
    public class LogErrorHandler : IErrorHandler
    {
        /// <summary>
        /// Gets or sets the tag for logging the exception stacktrace.
        /// </summary>
        /// <value>The tag.</value>
        public string Tag { get; set; }

        /// <summary>
        /// Initializes a new <see cref="T:DriverTracker.Mobile.Droid.LogErrorHandler"/> class.
        /// </summary>
        /// <param name="tag">The tag to initialize with.</param>
        public LogErrorHandler(string tag)
        {
            Tag = tag;
        }

        public void HandleError(Exception ex)
        {
            Log.Debug(Tag, ex.StackTrace);
        }
    }
}
