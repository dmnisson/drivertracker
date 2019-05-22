using Javax.Net.Ssl;

namespace DriverTracker.Mobile.Droid
{
#if DEBUG
    // from https://softwareproduction.eu/2017/08/26/xamarin-on-android-bypass-ssl-verification-with-httpclient/
    internal class BypassHostnameVerifier : Java.Lang.Object, IHostnameVerifier
    {
        public bool Verify(string hostname, ISSLSession session)
        {
            return true;
        }
    }
#endif
}
