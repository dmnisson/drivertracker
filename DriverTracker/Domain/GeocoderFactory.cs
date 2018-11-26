using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Geocoding;
using Geocoding.Google;
using Geocoding.MapQuest;
using Geocoding.Microsoft;
using Geocoding.Yahoo;

namespace DriverTracker.Domain
{
    public static class GeocoderFactory
    {

        public static bool IsGeocoderEnabled(IConfiguration configuration) {
            string[] providerNames = { "Google", "MapQuest", "OpenStreetMap", "Microsoft", "Yahoo" };
            foreach (string providerName in providerNames) {
                if (configuration["GeocodingProviders:Provider"].Equals(providerName))
                {
                    return true;
                }
            }
            return false;
        }

        public static IGeocoder GetGeocoder(IConfiguration configuration) {
            switch (configuration["GeocodingProviders:Provider"])
            {
                case "Google":
                    return new GoogleGeocoder() { ApiKey = configuration["GeocodingProviders:ApiKey"] };
                case "MapQuest":
                    return new MapQuestGeocoder(configuration["GeocodingProviders:Key"]) { };
                case "OpenStreetMap":
                    return new MapQuestGeocoder(configuration["GeocodingProviders:Key"])
                    {
                        UseOSM = true
                    };
                case "Microsoft":
                    return new BingMapsGeocoder(configuration["GeocodingProviders:BingKey"]) { };
                case "Yahoo":
                    return new YahooGeocoder(
                        configuration["GeocodingProviders:ConsumerKey"],
                        configuration["GeocodingProviders:ConsumerSecret"])
                    { };
                default:
                    return null;
            }
        }
    }
}
