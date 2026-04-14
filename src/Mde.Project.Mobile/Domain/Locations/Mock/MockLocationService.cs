using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Locations.Mock
{
    public class MockLocationService : IKnownLocationService
    {
        // properties
        private List<KnownLocation> locations = new List<KnownLocation>()
        {
            new KnownLocation { Name = "Eiffel Tower, Paris", Latitude = 48.85837009999999, Longitude = 2.2944813, CreatedOn = DateTime.UtcNow.AddDays(-14), Occation = "Werk" },
            new KnownLocation { Name = "Colosseum, Rome", Latitude = 41.8902102, Longitude = 12.4922309, CreatedOn = DateTime.UtcNow.AddDays(-13), Occation = "Reis" },
            new KnownLocation { Name = "Brandenburg Gate, Berlin", Latitude = 52.5162746, Longitude = 13.3777041, CreatedOn = DateTime.UtcNow.AddDays(-12), Occation = "Werk" },
            new KnownLocation { Name = "Sagrada Familia, Barcelona", Latitude = 41.4036299, Longitude = 2.1743558, CreatedOn = DateTime.UtcNow.AddDays(-11), Occation = "Reis" },
            new KnownLocation { Name = "Big Ben, London", Latitude = 51.5007292, Longitude = -0.1246254, CreatedOn = DateTime.UtcNow.AddDays(-10), Occation = "Werk" },
            new KnownLocation { Name = "Acropolis, Athens", Latitude = 37.971532, Longitude = 23.7257492, CreatedOn = DateTime.UtcNow.AddDays(-9), Occation = "Reis" },
            new KnownLocation { Name = "Statue of Liberty, New York", Latitude = 40.6892494, Longitude = -74.0445004, CreatedOn = DateTime.UtcNow.AddDays(-8), Occation = "Uitgaan" },
            new KnownLocation { Name = "Christ the Redeemer, Rio de Janeiro", Latitude = -22.951916, Longitude = -43.2104872, CreatedOn = DateTime.UtcNow.AddDays(-7), Occation = "Uitgaan" },
            new KnownLocation { Name = "Taj Mahal, Agra", Latitude = 27.1751448, Longitude = 78.0421422, CreatedOn = DateTime.UtcNow.AddDays(-6), Occation = "Reis" },
            new KnownLocation { Name = "Sydney Opera House, Sydney", Latitude = -33.8567844, Longitude = 151.2152967, CreatedOn = DateTime.UtcNow.AddDays(-5), Occation = "Uitgaan" },
            new KnownLocation { Name = "Mount Fuji, Japan", Latitude = 35.360555, Longitude = 138.727778, CreatedOn = DateTime.UtcNow.AddDays(-4), Occation = "Reis" },
            new KnownLocation { Name = "Niagara Falls, Canada", Latitude = 43.0962143, Longitude = -79.0377388, CreatedOn = DateTime.UtcNow.AddDays(-3), Occation = "Reis" },
            new KnownLocation { Name = "Grand Canyon, Arizona", Latitude = 36.1069652, Longitude = -112.1129972, CreatedOn = DateTime.UtcNow.AddDays(-2), Occation = "Ander" },
            new KnownLocation { Name = "Burj Khalifa, Dubai", Latitude = 25.197197, Longitude = 55.2743764, CreatedOn = DateTime.UtcNow.AddDays(-1), Occation = "Werk" },
            new KnownLocation { Name = "Great Wall, China", Latitude = 40.4319077, Longitude = 116.5703749, CreatedOn = DateTime.UtcNow, Occation = "Reis" },
        };

        // methodes
        public Task<IEnumerable<KnownLocation>> GetKnownLocationsAsync(string searchTerm)
        {
            return Task.FromResult(locations.Where(l => l.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));
        }
        public Task<IEnumerable<KnownLocation>> GetAllKnownLocationsAsync()
        {
            return Task.FromResult(locations.AsEnumerable());
        }
    }
}
