using EPiServer.Sample.Hotels;

namespace Site.Business.Search.Result
{
    public interface ISearchResultItemBuilder
    {
        /// <summary>
        /// Get SearchResultItem from Hotel class
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        SearchResultItem FromHotel(Hotel h);
    }
}