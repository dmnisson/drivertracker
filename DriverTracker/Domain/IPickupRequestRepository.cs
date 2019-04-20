using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DriverTracker.Models;

namespace DriverTracker.Domain
{
    public interface IPickupRequestRepository
    {
        /// <summary>
        /// List all pickup requests
        /// </summary>
        /// <returns>The list of pickup requests.</returns>
        IEnumerable<PickupRequest> List();


        /// <summary>
        /// List all pickup requests matching a predicate
        /// </summary>
        /// <param name="predicate">The predicate</param>
        /// <returns>The list of pickup requests matching the given <paramref name="predicate"/>.</returns>
        IEnumerable<PickupRequest> List(Expression<Func<PickupRequest, bool>> predicate);

        /// <summary>
        /// Lists all pickup requests assigned to a driver by the pickup request system.
        /// </summary>
        /// <returns>The driver's assigned requests.</returns>
        /// <param name="driver">The driver.</param>
        IEnumerable<PickupRequest> ListForDriver(Driver driver);

        /// <summary>
        /// Count the number of pickup requests in a database.
        /// </summary>
        /// <returns>The number of pickup requests.</returns>
        int Count();

        /// <summary>
        /// Count the number of pickup requests in the database matching a predicate.
        /// </summary>
        /// <returns>The number of pickup requests matching the given <paramref name="predicate"/>.</returns>
        /// <param name="predicate">Predicate.</param>
        int Count(Expression<Func<PickupRequest, bool>> predicate);

        /// <summary>
        /// Counts pickup requests assigned to a driver by the pickup request system.
        /// </summary>
        /// <returns>The number of requests assigned to the driver.</returns>
        /// <param name="driver">The driver.</param>
        int CountForDriver(Driver driver);

        /// <summary>
        /// Get the pickup request with the specified id.
        /// </summary>
        /// <returns>The pickup request.</returns>
        /// <param name="id">Identifier.</param>
        PickupRequest Get(int id);

        /// <summary>
        /// True if the pickup request is answered
        /// </summary>
        /// <returns><c>true</c>, if answered, <c>false</c> otherwise.</returns>
        /// <param name="id">Identifier.</param>
        bool IsAnswered(int id);

        /// <summary>
        /// Get the leg that was used to complete the pickup request.
        /// </summary>
        /// <returns>The leg.</returns>
        /// <param name="id">Identifier.</param>
        Leg GetAnswerLeg(int id);

        /// <summary>
        /// Checks if the given leg is a response to a pickup request.
        /// </summary>
        /// <returns><c>true</c>, if response to request, <c>false</c> otherwise.</returns>
        /// <param name="leg">Leg.</param>
        bool IsResponseToRequest(Leg leg);

        /// <summary>
        /// Gets the pickup request to which this leg was an answer.
        /// </summary>
        /// <returns>The leg.</returns>
        /// <param name="leg">Leg.</param>
        PickupRequest GetForLeg(Leg leg);

        /// <summary>
        /// Gets the assigned driver.
        /// </summary>
        /// <returns>The assigned driver.</returns>
        /// <param name="id">Identifier.</param>
        Driver GetAssignedDriver(int id);

        /// <summary>
        /// Add the specified request.
        /// </summary>
        /// <param name="request">Request.</param>
        void Add(PickupRequest request);

        /// <summary>
        /// Edit the specified request.
        /// </summary>
        /// <param name="request">Request.</param>
        void Edit(PickupRequest request);

        /// <summary>
        /// Cancel the specified request.
        /// </summary>
        /// <param name="request">Request.</param>
        void Cancel(PickupRequest request);

        /// <summary>
        /// Answer the specified request with the specified leg.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <param name="leg">Leg.</param>
        void Answer(PickupRequest request, Leg leg);

        /// <summary>
        /// Assign the specified request to the specified driver.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <param name="driver">Driver.</param>
        void Assign(PickupRequest request, Driver driver);
    }
}
