using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Address : IEquatable<Address>
    {
        /// <summary>
        /// Three digit country code See: https://countrycode.org/
        /// </summary>
        public string CountryCode { get; set; }
        public string StateProvinceTerritory { get; set; }
        public string PostalCode { get; set; }
        public string CityTownVilla { get; set; }
        public string Locality { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public Address()
        {

        }

        public Address(string streetAddress1, string streetAddress2, string cityTownVilla, string stateProvinceTerritory, string postalCode)
        {
            CountryCode = "USA";
            StateProvinceTerritory = stateProvinceTerritory;
            PostalCode = postalCode;
            CityTownVilla = cityTownVilla;
            StreetAddress1 = streetAddress1;
            StreetAddress2 = streetAddress2;
        }

        public Address(string streetAddress1, string streetAddress2, string cityTownVilla, string stateProvinceTerritory, string postalCode, decimal? latitude, decimal? longitude)
        {
            CountryCode = "USA";
            StateProvinceTerritory = stateProvinceTerritory;
            PostalCode = postalCode;
            CityTownVilla = cityTownVilla;
            StreetAddress1 = streetAddress1;
            StreetAddress2 = streetAddress2;
            Latitude = latitude;
            Longitude = longitude;
        }

        public Address(string streetAddress1, string streetAddress2, string cityTownVilla, string stateProvinceTerritory, string postalCode, string countryCode)
        {
            CountryCode = countryCode;
            StateProvinceTerritory = stateProvinceTerritory;
            PostalCode = postalCode;
            CityTownVilla = cityTownVilla;
            StreetAddress1 = streetAddress1;
            StreetAddress2 = streetAddress2;
        }

        public Address(string countryCode, string stateProvinceTerritory, string postalCode, string cityTownVilla, string locality, string streetAddress1, string streetAddress2, decimal? latitude, decimal? longitude)
        {
            CountryCode = countryCode;
            StateProvinceTerritory = stateProvinceTerritory;
            PostalCode = postalCode;
            CityTownVilla = cityTownVilla;
            Locality = locality;
            StreetAddress1 = streetAddress1;
            StreetAddress2 = streetAddress2;
            Latitude = latitude;
            Longitude = longitude;
        }

        public bool Equals(Address other)
        {
            return string.Equals(CountryCode, other.CountryCode)
                && string.Equals(StateProvinceTerritory, other.StateProvinceTerritory)
                && string.Equals(PostalCode, other.PostalCode)
                && string.Equals(CityTownVilla, other.CityTownVilla)
                && string.Equals(Locality, other.Locality)
                && string.Equals(StreetAddress1, other.StreetAddress1)
                && string.Equals(StreetAddress2, other.StreetAddress2)
                && Latitude == other.Latitude
                && Longitude == other.Longitude;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            var addr = obj as Address;
            return Equals(addr);
        }

        public override string ToString()
        {
            return string.Format("{0} {1}, {2} {3}", this.StreetAddress1, this.CityTownVilla, this.StateProvinceTerritory, this.PostalCode);
        }
    }
}
