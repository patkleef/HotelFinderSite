using EPiServer.Sample.Hotels;

namespace Site.Business.Images
{
    /// <summary>
    /// Hotel image helper
    /// </summary>
    public static class HotelImageHelper
    {
        private static readonly IHotelImageUtils _hotelImageUtils = new HotelImageUtils();

        /// <summary>
        /// Image url path
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string ImageUrlPath(this Image image)
        {
            return _hotelImageUtils.GetImagePath(image);
        }
    }
}