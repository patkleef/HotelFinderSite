using System.Collections.Generic;
using EPiServer.Sample.Hotels;

namespace Site.Business.Images
{
    public interface IHotelImageUtils
    {
        string GetHotelImage(IEnumerable<Image> images, int width, int height);
        string GetImagePath(Image image);
    }
}