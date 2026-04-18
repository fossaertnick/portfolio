using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Locations.Mock
{
    public class MockMemoriaService : IMemoriaService
    {
        private readonly List<Memoria> locations;

        // constructor
        public MockMemoriaService()
        {
            locations = new List<Memoria>
            {
                new Memoria {Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Eiffel Tower, Paris", Latitude = 48.85837009999999, Longitude = 2.2944813, CreatedOn = DateTime.UtcNow.AddDays(-14), Occation = "Werk" },
                new Memoria {Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Colosseum, Rome", Latitude = 41.8902102, Longitude = 12.4922309, CreatedOn = DateTime.UtcNow.AddDays(-13), Occation = "Reis" },
                new Memoria {Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Brandenburg Gate, Berlin", Latitude = 52.5162746, Longitude = 13.3777041, CreatedOn = DateTime.UtcNow.AddDays(-12), Occation = "Werk" },
                new Memoria {Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Sagrada Familia, Barcelona", Latitude = 41.4036299, Longitude = 2.1743558, CreatedOn = DateTime.UtcNow.AddDays(-11), Occation = "Reis" },
                new Memoria {Id = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "Big Ben, London", Latitude = 51.5007292, Longitude = -0.1246254, CreatedOn = DateTime.UtcNow.AddDays(-10), Occation = "Werk" },
                new Memoria {Id = Guid.Parse("00000000-0000-0000-0000-000000000006"), Name = "Acropolis, Athens", Latitude = 37.971532, Longitude = 23.7257492, CreatedOn = DateTime.UtcNow.AddDays(-9), Occation = "Reis" },
                new Memoria {Id = Guid.Parse("00000000-0000-0000-0000-000000000007"), Name = "Statue of Liberty, New York", Latitude = 40.6892494, Longitude = -74.0445004, CreatedOn = DateTime.UtcNow.AddDays(-8), Occation = "Uitgaan" },
                new Memoria {Id = Guid.Parse("00000000-0000-0000-0000-000000000008"), Name = "Christ the Redeemer, Rio de Janeiro", Latitude = -22.951916, Longitude = -43.2104872, CreatedOn = DateTime.UtcNow.AddDays(-7), Occation = "Uitgaan" },
                new Memoria {Id = Guid.Parse("00000000-0000-0000-0000-000000000009"), Name = "Taj Mahal, Agra", Latitude = 27.1751448, Longitude = 78.0421422, CreatedOn = DateTime.UtcNow.AddDays(-6), Occation = "Reis" },
                new Memoria {Id = Guid.Parse("00000000-0000-0000-0000-000000000010"), Name = "Sydney Opera House, Sydney", Latitude = -33.8567844, Longitude = 151.2152967, CreatedOn = DateTime.UtcNow.AddDays(-5), Occation = "Uitgaan" },
                new Memoria {Id = Guid.Parse("00000000-0000-0000-0000-000000000011"), Name = "Mount Fuji, Japan", Latitude = 35.360555, Longitude = 138.727778, CreatedOn = DateTime.UtcNow.AddDays(-4), Occation = "Reis" },
                new Memoria {Id = Guid.Parse("00000000-0000-0000-0000-000000000012"), Name = "Niagara Falls, Canada", Latitude = 43.0962143, Longitude = -79.0377388, CreatedOn = DateTime.UtcNow.AddDays(-3), Occation = "Reis" },
                new Memoria {Id = Guid.Parse("00000000-0000-0000-0000-000000000013"), Name = "Grand Canyon, Arizona", Latitude = 36.1069652, Longitude = -112.1129972, CreatedOn = DateTime.UtcNow.AddDays(-2), Occation = "Ander" },
                new Memoria {Id = Guid.Parse("00000000-0000-0000-0000-000000000014"), Name = "Burj Khalifa, Dubai", Latitude = 25.197197, Longitude = 55.2743764, CreatedOn = DateTime.UtcNow.AddDays(-1), Occation = "Werk" },
                new Memoria {Id = Guid.Parse("00000000-0000-0000-0000-000000000015"), Name = "Great Wall, China", Latitude = 40.4319077, Longitude = 116.5703749, CreatedOn = DateTime.UtcNow, Occation = "Reis" },
            };
        }
        // methodes
        public Task<Memoria> GetMemoriaById(Guid id)
        {
            return Task.FromResult(locations.SingleOrDefault(l => l.Id == id));
        }
        public Task<IEnumerable<Memoria>> GetMemoriaByFilterAsync(string searchTerm)
        {
            return Task.FromResult(locations.Where(l => l.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));
        }
        public Task<IEnumerable<Memoria>> GetAllMemoriaAsync()
        {
            return Task.FromResult(locations.AsEnumerable());
        }
        public async Task DeleteMemoriaAsync(Memoria location)
        {
            var locationToDelete = await Task.FromResult(locations.SingleOrDefault(l => l.Id == location.Id));
            if (locationToDelete != null)
            {
                locations.Remove(locationToDelete);
            }
            else
            {
                throw new ArgumentException("Er ging iets fout bij het verwijderen");
            }
        }
        public async Task<Memoria> CreateMemoriaAsync(Memoria newAddition)
        {
            if(newAddition != null)
            {
                Memoria newOne = new Memoria
                {
                    Id = Guid.NewGuid(),
                    Name = newAddition.Name,
                    Longitude = newAddition.Longitude,
                    Latitude = newAddition.Latitude,
                    CreatedOn = newAddition.CreatedOn,
                    Occation = newAddition.Occation,
                    Description = newAddition.Description
                };

                locations.Add(newOne);
                return newOne;
            }
            else
            {
                throw new ArgumentException("Er ging iets fout bij het aanmaken");
            }
        }
        public async Task<Memoria> UpdateMemoriaAsync(Memoria updateMemoria)
        {
            var memoriaToBeUpdated = await GetMemoriaById(updateMemoria.Id);
            if(memoriaToBeUpdated != null)
            {
                locations.Remove(memoriaToBeUpdated);
                locations.Add(updateMemoria);
            }
            else
            {
                throw new ArgumentException("Er ging iets fout bij het updaten");
            }
            return memoriaToBeUpdated;
        }
    }
}
