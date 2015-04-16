using System.Collections.Generic;
using System.Linq;
using EPiServer.Sample.Hotels;

namespace Site.Business.Images
{
    /// <summary>
    /// Hotel image utils
    /// </summary>
    public class HotelImageUtils : IHotelImageUtils
    {
        private const string _imageUrl = "http://hotelassets.episerverdemo.com/";

        /// <summary>
        /// Get hotel image by with and height
        /// </summary>
        /// <param name="images"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public string GetHotelImage(IEnumerable<Image> images, int width, int height)
        {
            var image = images.FirstOrDefault(i => i.Width >= width && i.Height >= width);
            if (image != null)
            {
                return GetImagePath(image);
            }
            else
            {
                return GetImagePath(images.FirstOrDefault());
            }
        }

        /// <summary>
        /// Get the full image path (http://hotelassets.episerverdemo.com/ ..../...)
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public string GetImagePath(Image image)
        {
            if (image != null)
            {
                return string.Format("{0}{1}", _imageUrl, image.Path);
            }
            return string.Empty;
        }
    }
}