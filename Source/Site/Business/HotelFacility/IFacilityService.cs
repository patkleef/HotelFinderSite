using System.Collections.Generic;
using EPiServer.Sample.Hotels;

namespace Site.Business.HotelFacility
{
    /// <summary>
    /// Facility service
    /// </summary>
    public interface IFacilityService
    {
        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        IEnumerable<Facility> GetAll();

        /// <summary>
        /// Get facility by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Facility GetById(int id);

        /// <summary>
        /// Get facility by id
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        IEnumerable<Facility> GetFacilitiesForHotel(Hotel hotel);
    }
}