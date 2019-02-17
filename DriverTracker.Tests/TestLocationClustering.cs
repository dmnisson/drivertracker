using System;
using System.Threading.Tasks;
using static System.Math;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;

using DriverTracker.Models;
using DriverTracker.Domain;


namespace DriverTracker.Tests
{
    class LocationClusteringWithMocks
    {
        public LocationClustering LocationClustering { get; set; }
        public Mock<IDriverRepository> DriverRepository { get; set; }
        public Mock<ILegRepository> LegRepository { get; set; }
        public Mock<IGeocodingDbSync> GeocodingDbSync { get; set; }
    }

    public class TestLocationClustering
    {
        private Random rng = new Random();

        private LocationClusteringWithMocks CreateInstance(double[][] startLocations, double[][] endLocations)
        {
            var legRepository = new Mock<ILegRepository>();
            var dbSync = new Mock<IGeocodingDbSync>();
            SetupLegLocationMocks(startLocations, endLocations, legRepository, dbSync);

            Driver driver = new Driver
            {
                DriverID = 1,
                UserIDString = "1",
                LicenseNumber = "123456789ABC0",
                Name = "Testing T. Tester"
            };
            ICollection<Driver> drivers = (new Driver[] { driver }).ToList();

            var driverRepository = new Mock<IDriverRepository>();
            driverRepository.Setup(repo => repo.CountAsync()).ReturnsAsync(1);
            driverRepository.Setup(repo => repo.DriverExists(It.Is<int>(id => drivers.Any(d => d.DriverID == id)))).Returns(true);
            driverRepository.Setup(repo => repo.DriverExists(It.Is<int>(id => !drivers.Any(d => d.DriverID == id)))).Returns(false);
            driverRepository.Setup(repo => repo.GetAsync(It.Is<int>(id => drivers.Any(d => d.DriverID == id)))).ReturnsAsync((int id) => drivers.First(d => d.DriverID == id));
            driverRepository.Setup(repo => repo.ListAsync()).ReturnsAsync(new List<Driver> { driver });
            driverRepository.Setup(repo => repo.AddAsync(It.Is<Driver>(d =>
                !drivers.Any(d2 => d2.DriverID == d.DriverID))))
                .Callback<Driver>(d => drivers.Append(d));
            driverRepository.Setup(repo => repo.AddAsync(It.Is<Driver>(d =>
                drivers.Any(d2 => d2.DriverID == d.DriverID))))
                .ThrowsAsync(new Exception("Driver already exists"));
            driverRepository.Setup(repo => repo.DeleteAsync(It.Is<Driver>(d =>
                drivers.Any(d2 => d2.DriverID == d.DriverID))))
                .Callback<Driver>(d => drivers.Remove(drivers.First(d2 => d2.DriverID == d.DriverID)));
            driverRepository.Setup(repo => repo.DeleteAsync(It.Is<Driver>(d =>
                !drivers.Any(d2 => d2.DriverID == d.DriverID))))
                .ThrowsAsync(new Exception("Driver does not exist"));

            return new LocationClusteringWithMocks
            {
                LocationClustering = new LocationClustering(driverRepository.Object, legRepository.Object,
                dbSync.Object),
                DriverRepository = driverRepository,
                LegRepository = legRepository,
                GeocodingDbSync = dbSync
            };
        }

        private static void SetupLegLocationMocks(double[][] startLocations, double[][] endLocations, LocationClusteringWithMocks locationClustering)
        {
            SetupLegLocationMocks(startLocations, endLocations, locationClustering.LegRepository, locationClustering.GeocodingDbSync);
        }

        private static void SetupLegLocationMocks(double[][] startLocations, double[][] endLocations, Mock<ILegRepository> legRepository, Mock<IGeocodingDbSync> dbSync)
        {
            ICollection<LegCoordinates> legCoordinates = startLocations.AsEnumerable().Select((loc, i) => new LegCoordinates
            {
                LegID = i + 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                StartLatitude = Convert.ToDecimal(loc[0]),
                StartLongitude = Convert.ToDecimal(loc[1]),
                DestLatitude = Convert.ToDecimal(endLocations[i][0]),
                DestLongitude = Convert.ToDecimal(endLocations[i][1])
            }).ToList();

            ICollection<Leg> legs = startLocations.AsEnumerable().Select((loc, i) => new Leg
            {
                LegID = i + 1,
                DriverID = 1,
                NumOfPassengersAboard = 1,
                NumOfPassengersPickedUp = 1,

                LegCoordinates = legCoordinates.ElementAt(i),
            }
            ).ToList();

            legRepository.Setup(repo => repo.CountAsync()).ReturnsAsync(legs.Count());
            legRepository.Setup(repo => repo.CountDriverLegsAsync(It.Is<int>(id => legs.Any(l => l.DriverID == id))))
                .ReturnsAsync((int id) => legs.Count(l => l.DriverID == id));
            legRepository.Setup(repo => repo.CountDriverLegsAsync(It.Is<int>(id => !legs.Any(l => l.DriverID == id))))
                .ReturnsAsync(0);
            legRepository.Setup(repo => repo.Get(It.IsInRange(1, legs.Count(), Range.Inclusive)))
                .ReturnsAsync((int id) => legs.ElementAt(id - 1));
            legRepository.Setup(repo => repo.ListForDriverAsync(It.Is<int>(id => legs.Any(l => l.DriverID == id))))
                .ReturnsAsync((int id) => legs.Where(l => l.DriverID == id));
            legRepository.Setup(repo => repo.ListAsync()).ReturnsAsync(legs);
            legRepository.Setup(repo => repo.AddAsync(It.Is<Leg>(l => !legs.Any(l2 => l2.LegID == l.LegID))))
                .Callback<Leg>(l =>
                {
                    legs.Append(l);
                });
            legRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Leg>()))
                .Callback<Leg>(l => legs.Remove(l));


            dbSync.Setup(s => s.GetLegCoordinatesAsync(It.IsInRange(1, legs.Count(), Range.Inclusive)))
                .ReturnsAsync((int id) => legCoordinates.ElementAt(id - 1));
            dbSync.Setup(s => s.ListLegCoordinatesAsync())
                .ReturnsAsync(legCoordinates);
        }

        private double[][] GenerateCluster(double minLon, double minLat, double maxLon, double maxLat, int n)
        {
            double[][] cluster = new double[n][];
            for (int i = 0; i < n; i++)
            {
                cluster[i] = new double[2];
                cluster[i][0] = rng.NextDouble() * (maxLat - minLat) + minLat;
                cluster[i][1] = rng.NextDouble() * (maxLon - minLon) + minLon;
            }

            return cluster;
        }

        // Generate an exponentially distributed endpoint starting from the given coordinates
        private double[] GenerateEndpoint(double startLat, double startLon, double distanceConstantDeg)
        {
            double[] endpoint = new double[2];
            double a = PI * 2 * rng.NextDouble(); // azimuth angle
            double d = distanceConstantDeg / rng.NextDouble();
            endpoint[0] = (Asin(Sin(startLat * PI / 180) * Cos(d)) + Cos(startLat * PI / 180) * Sin(d) * Cos(a)) * 180 / PI;
            endpoint[1] = Asin(Sin(a) * Sin(d) / Cos(endpoint[0])) + startLon *PI/180;

            return endpoint;
        }

        [Fact]
        public async Task CheckNumberingChanges()
        {
            // initialize with a single region of uniformly distributed start coordinates and exponentially
            // distributed geographic distances
            // Coordinates of Davis, CA: 38.553889, -121.738056
            int clusterSize = 100;
            double[][] startLocations = GenerateCluster(-121.738556, 38.553389, -121.737556, 38.554389, clusterSize);
            double[][] endLocations = new double[clusterSize][];
            for (int i = 0; i < clusterSize; i++)
            {
                endLocations[i] = GenerateEndpoint(startLocations[i][0], startLocations[i][1], 0.0002);
            }

            // perform initial cluster analysis and check k
            LocationClusteringWithMocks clustering = CreateInstance(startLocations, endLocations);
            await clustering.LocationClustering.RenumberAsync();
            int k1 = clustering.LocationClustering.NumberOfClusters;

            // add another region of legs
            // Coordinates of San Francisco, CA: 37.783333, -122.416667
            int clusterSize2 = 50;
            double[][] startLocations2 = GenerateCluster(-122.417167, 37.782833, -122.416167, 37.783833, clusterSize2);
            double[][] endLocations2 = new double[clusterSize2][];
            for (int i = 0; i < clusterSize2; i++)
            {
                endLocations2[i] = GenerateEndpoint(startLocations2[i][0], startLocations2[i][1], 0.0003);
            }

            Array.Resize(ref startLocations, startLocations.Length + startLocations2.Length);
            Array.Copy(startLocations2, 0, startLocations, startLocations.Length - startLocations2.Length, startLocations2.Length);
            Array.Resize(ref endLocations, endLocations.Length + endLocations2.Length);
            Array.Copy(endLocations2, 0, endLocations, endLocations.Length - endLocations2.Length, endLocations2.Length);

            SetupLegLocationMocks(startLocations, endLocations, clustering);
            await clustering.LocationClustering.RenumberAsync();
            int k2 = clustering.LocationClustering.NumberOfClusters;

            Assert.NotEqual<int>(k2, k1);
        }
    }
}
