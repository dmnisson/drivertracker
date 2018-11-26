using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using DriverTracker.Models;

namespace DriverTracker.Domain
{
    /// <summary>
    /// Provides synchronization between addresses and geographic coordinates
    /// </summary>
    public interface IGeocodingDbSync
    {

        /// <summary>
        /// Updates coordinates for all legs (async).
        /// </summary>
        /// <returns><c>true</c>, if all legs were updated, <c>false</c> otherwise.</returns>
        Task<bool> UpdateAllAsync();

        /// <summary>
        /// Updates cooordinates for the given leg (async).
        /// </summary>
        /// <returns><c>true</c>, if the coordinates were updated, <c>false</c> otherwise.</returns>
        /// <param name="id">Leg identifier.</param>
        Task<bool> UpdateLegAsync(int id);

        /// <summary>
        /// Updates coordinates for all legs matching the predicate (async).
        /// </summary>
        /// <returns><c>true</c>, if legs were updated, <c>false</c> otherwise.</returns>
        /// <param name="predicate">Predicate.</param>
        Task<bool> UpdateIfAsync(Expression<Func<Leg, bool>> predicate);

        /// <summary>
        /// Gets the geocoded start and end coordinates of the leg (async).
        /// </summary>
        /// <returns>The start and end coordinates.</returns>
        /// <param name="id">Leg identifier.</param>
        Task<LegCoordinates> GetLegCoordinatesAsync(int id);

        /// <summary>
        /// Lists the geocoded start and end coordinates of each leg (async).
        /// </summary>
        /// <returns>The start and end coordinates.</returns>
        Task<IEnumerable<LegCoordinates>> ListLegCoordinatesAsync();

        /// <summary>
        /// Lists the geocoded start and end coordinates of each leg matching the predicate (async).
        /// </summary>
        /// <returns>The start and end coordinates.</returns>
        /// <param name="predicate">Predicate.</param>
        Task<IEnumerable<LegCoordinates>> ListLegCoordinatesAsync(Expression<Func<Leg, bool>> predicate);

    }
}
