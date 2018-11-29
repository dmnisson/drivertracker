

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

using Geocoding;

using DriverTracker.Models;

namespace DriverTracker.Domain
{
    /// <summary>
    /// Provides synchronization between addresses and geographic coordinates
    /// </summary>
    public class GeocodingDbSync : IGeocodingDbSync
    {
        private MvcDriverContext _context;
        private IGeocoder _geocoder;

        public GeocodingDbSync(IConfiguration configuration, MvcDriverContext context)
        {
            _context = context;
            _geocoder = GeocoderFactory.GetGeocoder(configuration);
        }

        public async Task<bool> UpdateAllAsync()
        {
            var creationTasks = _context.Legs.Where(leg => leg.LegCoordinates == null)
                                        .Select(leg => CreateLegCoordinatesAsync(leg));
            var updateTasks = _context.Legs.Where(leg => leg.LegCoordinates != null 
                                                  && leg.LegCoordinates.DateModified > DateTime.Now)
                                      .Select(leg => UpdateLegCoordinatesAsync(leg));

            await Task.WhenAll(creationTasks);
            await Task.WhenAll(updateTasks);

            return (creationTasks.Count() + updateTasks.Count() == 0) 
                || (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateLegAsync(int id)
        {
            Leg leg = await _context.Legs.FirstOrDefaultAsync(l => l.LegID == id);
            if (leg.LegCoordinates == null)
            {
                await CreateLegCoordinatesAsync(leg);
            }
            else
            {
                await UpdateLegCoordinatesAsync(leg);
            }

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateLegAsync(Leg leg)
        {
            return await UpdateLegAsync(leg.LegID);
        }

        public async Task<bool> UpdateIfAsync(Expression<Func<Leg, bool>> predicate)
        {
            var creationTasks = _context.Legs.Where(leg => leg.LegCoordinates == null)
                                        .Where(predicate)
                                        .Select(leg => CreateLegCoordinatesAsync(leg));
            var updateTasks = _context.Legs.Where(leg => leg.LegCoordinates != null
                                                 && leg.LegCoordinates.DateModified > DateTime.Now)
                                      .Where(predicate)
                                      .Select(leg => UpdateLegCoordinatesAsync(leg));

            await Task.WhenAll(creationTasks);
            await Task.WhenAll(updateTasks);

            return (creationTasks.Count() + updateTasks.Count() == 0)
                || (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<LegCoordinates> GetLegCoordinatesAsync(int id)
        {
            return await _context.LegCoordinates.FirstOrDefaultAsync(c => c.LegID == id);
        }

        public async Task<IEnumerable<LegCoordinates>> ListLegCoordinatesAsync() 
        {
            return await _context.LegCoordinates.ToListAsync();
        }

        public async Task<IEnumerable<LegCoordinates>> ListLegCoordinatesAsync(Expression<Func<Leg, bool>> predicate)
        {
            return await _context.Legs.Where(predicate).Select(leg => leg.LegCoordinates).ToListAsync();
        }

        private async Task<LegCoordinates> GeocodeLeg(Leg leg) {
            IEnumerable<Address> startAddressList = await _geocoder.GeocodeAsync(leg.StartAddress);
            IEnumerable<Address> endAddressList = await _geocoder.GeocodeAsync(leg.DestinationAddress);
            return new LegCoordinates
            {
                LegID = leg.LegID,
                StartLatitude = startAddressList.Average(
                    address => Convert.ToDecimal(address.Coordinates.Latitude)),
                StartLongitude = startAddressList.Average(
                    address => Convert.ToDecimal(address.Coordinates.Longitude)),
                DestLatitude = endAddressList.Average(
                    address => Convert.ToDecimal(address.Coordinates.Latitude)),
                DestLongitude = endAddressList.Average(
                    address => Convert.ToDecimal(address.Coordinates.Longitude)),

                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
        }

        private async Task CreateLegCoordinatesAsync(Leg leg)
        {
            LegCoordinates legCoordinates = await GeocodeLeg(leg);

            _context.LegCoordinates.Add(legCoordinates);
        }

        private async Task UpdateLegCoordinatesAsync(Leg leg)
        {
            LegCoordinates legCoordinates = await GeocodeLeg(leg);

            _context.LegCoordinates.Update(legCoordinates);
        }
    }
}
