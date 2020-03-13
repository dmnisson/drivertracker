using static System.Math;
using Accord.Math.Distances;

namespace DriverTracker.Domain
{
    /// <summary>
    /// Geographic distance function.
    /// </summary>
    public class GeographicDistance : IDistance
    {
        public double Distance(double[] x, double[] y)
        {
            return Acos(Sin(x[0] * PI/180) * Sin(y[0] * PI/180) + Cos(x[0] * PI/180) * Cos(y[0] * PI/180) * Cos((y[1] - x[1]) * PI/180)) * 180/PI;
        }
    }
}
