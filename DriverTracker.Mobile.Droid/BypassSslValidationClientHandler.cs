﻿using Javax.Net.Ssl;
using Xamarin.Android.Net;
using Android.Net;

namespace DriverTracker.Mobile.Droid
{
#if DEBUG

    internal class BypassSslValidationClientHandler : AndroidClientHandler
    {
        protected override SSLSocketFactory ConfigureCustomSSLSocketFactory(HttpsURLConnection connection)
        {
            return SSLCertificateSocketFactory.GetInsecure(1000, null);
        }

        protected override IHostnameVerifier GetSSLHostnameVerifier(HttpsURLConnection connection)
        {
            return new BypassHostnameVerifier();
        }
    }
#endif
}
