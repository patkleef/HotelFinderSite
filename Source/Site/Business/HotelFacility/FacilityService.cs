using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Sample.Hotels;

namespace Site.Business.HotelFacility
{
    /// <summary>
    /// Facility Service
    /// </summary>
    public class FacilityService : IFacilityService
    {
        private IEnumerable<Facility> _facilities;

        private IEnumerable<Facility> Facilities
        {
            get
            {
                if (_facilities == null)
                {
                    GetAll();
                }
                return _facilities;
            }
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Facility> GetAll()
        {
            if (_facilities == null)
            {
                _facilities = new[]
                {
                    new Facility {Id = 1, Title = "TV", Icon = "youtube-play", Value = "Cable / Satellite TV"},
                    new Facility {Id = 2, Title = "Parking", Icon = "car", Value = "Parking is available"},
                    new Facility {Id = 3, Title = "Kids friendly", Icon = "child", Value = "Playground"},
                    new Facility {Id = 4, Title = "Restaurant", Icon = "cutlery", Value = "Restaurant"},
                    new Facility {Id = 5, Title = "Wifi", Icon = "wifi", Value = "Wireless internet on site"},
                    new Facility {Id = 6, Title = "Family Room", Icon = "users", Value = "Family Room"},
                    new Facility {Id = 7, Title = "Non-Smoking Rooms", Icon = "ban", Value = "Designated Smoking Area"},
                    new Facility {Id = 8, Title = "Pet Friendly", Icon = "paw", Value= "Room Service"},
                    new Facility {Id = 9, Title = "Private Bathroom", Icon = "lock", Value = "Private Bathroom"},
                    new Facility {Id = 10, Title = "Air Conditioned", Icon = "sliders", Value = "Pet Friendly"},
                    new Facility {Id = 11, Title = "Bar", Icon = "glass", Value = "Mini Bar"},
                    new Facility {Id = 12, Title = "24 Hour Reception", Icon = "clock-o", Value = "24 Hour Reception"},
                    new Facility {Id = 13, Title = "Wake-up Service", Icon = "bell", Value = "Wake-up Service"},
                    new Facility {Id = 14, Title = "Spa & Wellness Centre", Icon = "smile-o", Value = "Spa & Wellness Centre"}
                };
            }
            return _facilities;
        }

        /// <summary>
        /// Get facility by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Facility GetById(int id)
        {
            return Facilities.FirstOrDefault(f => f.Id == id);
        }

        /// <summary>
        /// Get facility by id
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        public IEnumerable<Facility> GetFacilitiesForHotel(Hotel hotel)
        {
            var facilities = Facilities;
            foreach (var facility in facilities)
            {
                facility.IsSelected =
                    hotel.Features.Any(
                        f => f.Equals(facility.Value, StringComparison.InvariantCultureIgnoreCase));
            }
            return facilities;
        }
    }
}