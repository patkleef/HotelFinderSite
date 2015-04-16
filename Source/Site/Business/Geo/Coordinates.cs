namespace Site.Business.Geo
{
    /// <summary>
    /// Coordinates class, latitude and longitude
    /// </summary>
    public class Coordinates
    {
        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        public Coordinates(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}