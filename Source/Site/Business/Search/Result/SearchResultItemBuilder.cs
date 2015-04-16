using EPiServer.Sample.Hotels;
using Site.Business.Images;

namespace Site.Business.Search.Result
{
    public class SearchResultItemBuilder : ISearchResultItemBuilder
    {
        private const int _searchResultsImageWidth = 370;
        private const int _searchResultsImageHeight = 370;

        private readonly IHotelImageUtils _hotelImageUtils;

        /// <summary>
        /// Public constructor
        /// </summary>
        public SearchResultItemBuilder()
        {
            _hotelImageUtils = new HotelImageUtils();
        }

        /// <summary>
        /// Get SearchResultItem from Hotel class
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        public SearchResultItem FromHotel(Hotel h)
        {
            var shortLocation = h.FullAddress;
            // Just use a short location (not the ShortLocation property because it sometimes only shows the street and number)
            // description, unfortunately some times the Location object don't have city but a Province.
            if (h.Location != null) 
            {
                shortLocation = string.Format("{0}, {1}", h.Location.Title, h.Location.Country.Title);
            }

            return new SearchResultItem
            {
                Id = h.Id,
                Title = h.Title,
                Location = shortLocation,
                Price = h.PriceUSD,
                StarRating = h.StarRating,
                Image = _hotelImageUtils.GetHotelImage(h.Images, _searchResultsImageWidth, _searchResultsImageHeight)
            };
            
        }
    }
}