using Mde.Project.Mobile.Domain.Models.enums;
using Mde.Project.Mobile.Domain.Services.Interfaces;

namespace Mde.Project.Mobile.Domain.Locations.Mock
{
    public class MemoriaService : IMemoriaService
    {
        private readonly List<Memoria> locations;
        private readonly IGeoCodingService _googleGeoCodingService;
        private readonly IMediaService _mediaService;

        // constructor
        public MemoriaService(IGeoCodingService googleGeoCodingService, IMediaService mediaService)
        {
            locations = SeedingMemoria().ToList();
            _googleGeoCodingService = googleGeoCodingService;
            _mediaService = mediaService;
        }

        // seeding
        private IEnumerable<Memoria> SeedingMemoria()
        {
            var seeding = new List<Memoria>
{
    new Memoria {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
        Name = "Eiffel Tower",
        MemoriaAddress = new Models.Address
        {
            City = "Paris",
            Country = "France",
            Street = "Champ de Mars",
            HouseNumber = "5",
        },
        Latitude = 48.8583701,
        Longitude = 2.2944813,
        CreatedOn = DateTime.UtcNow.AddDays(-14),
        LastEditedOn = DateTime.UtcNow.AddDays(-13),
        Occation = OccationType.Reis,
        Description = "Iconische toren en symbool van Parijs."
    },
    new Memoria {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
        Name = "Colosseum",
        MemoriaAddress = new Models.Address
        {
            City = "Rome",
            Country = "Italy",
            Street = "Piazza del Colosseo",
            HouseNumber = "1",
        },
        Latitude = 41.8902102,
        Longitude = 12.4922309,
        CreatedOn = DateTime.UtcNow.AddDays(-13),
        LastEditedOn = DateTime.UtcNow.AddDays(-12),
        Occation = OccationType.Reis,
        Description = "Oud Romeins amfitheater."
    },
    new Memoria {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
        Name = "Brandenburg Gate",
                MemoriaAddress = new Models.Address
        {
        City = "Berlin",
        Country = "Germany",
        Street = "Pariser Platz",
        HouseNumber = "1",
                },
        Latitude = 52.5162746,
        Longitude = 13.3777041,
        CreatedOn = DateTime.UtcNow.AddDays(-12),
        LastEditedOn = DateTime.UtcNow.AddDays(-11),
        Occation = OccationType.Werk,
        Description = "Historische stadspoort in Berlijn."
    },
    new Memoria {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
        Name = "Sagrada Familia",
                MemoriaAddress = new Models.Address
        {
        City = "Barcelona",
        Country = "Spain",
        Street = "Carrer de Mallorca",
        HouseNumber = "401",
                },
        Latitude = 41.4036299,
        Longitude = 2.1743558,
        CreatedOn = DateTime.UtcNow.AddDays(-11),
        LastEditedOn = DateTime.UtcNow.AddDays(-10),
        Occation = OccationType.Reis,
        Description = "Beroemde basiliek ontworpen door Gaudí."
    },
    new Memoria {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
        Name = "Big Ben",
                MemoriaAddress = new Models.Address
        {
        City = "London",
        Country = "United Kingdom",
        Street = "Westminster",
        HouseNumber = "1",
                },
        Latitude = 51.5007292,
        Longitude = -0.1246254,
        CreatedOn = DateTime.UtcNow.AddDays(-10),
        LastEditedOn = DateTime.UtcNow.AddDays(-9),
        Occation = OccationType.Ander,
        Description = "Bekende klokkentoren van Londen."
    },
    new Memoria {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
        Name = "Acropolis",
                MemoriaAddress = new Models.Address
        {
        City = "Athens",
        Country = "Greece",
        Street = "Acropolis Hill",
        HouseNumber = "1",
                },
        Latitude = 37.971532,
        Longitude = 23.7257492,
        CreatedOn = DateTime.UtcNow.AddDays(-9),
        LastEditedOn = DateTime.UtcNow.AddDays(-8),
        Occation = OccationType.Uitgaan,
        Description = "Oude citadel met historische tempels."
    },
    new Memoria {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
        Name = "Statue of Liberty",
                MemoriaAddress = new Models.Address
        {
        City = "New York",
        Country = "USA",
        Street = "Liberty Island",
        HouseNumber = "1",
                },
        Latitude = 40.6892494,
        Longitude = -74.0445004,
        CreatedOn = DateTime.UtcNow.AddDays(-8),
        LastEditedOn = DateTime.UtcNow.AddDays(-7),
        Occation = OccationType.Uitgaan,
        Description = "Vrijheidsbeeld in New York."
    },
    new Memoria {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
        Name = "Christ the Redeemer",
                MemoriaAddress = new Models.Address
        {
        City = "Rio de Janeiro",
        Country = "Brazil",
        Street = "Parque Nacional da Tijuca",
        HouseNumber = "1",
                },
        Latitude = -22.951916,
        Longitude = -43.2104872,
        CreatedOn = DateTime.UtcNow.AddDays(-7),
        LastEditedOn = DateTime.UtcNow.AddDays(-6),
        Occation = OccationType.Werk,
        Description = "Groot Christusbeeld op de Corcovado."
    },
    new Memoria {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
        Name = "Taj Mahal",
                MemoriaAddress = new Models.Address
        {
        City = "Agra",
        Country = "India",
        Street = "Dharmapuri",
        HouseNumber = "1",
                },
        Latitude = 27.1751448,
        Longitude = 78.0421422,
        CreatedOn = DateTime.UtcNow.AddDays(-6),
        LastEditedOn = DateTime.UtcNow.AddDays(-5),
        Occation = OccationType.Reis,
        Description = "Wit marmeren mausoleum."
    },
    new Memoria {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
        Name = "Sydney Opera House",
                MemoriaAddress = new Models.Address
        {
        City = "Sydney",
        Country = "Australia",
        Street = "Bennelong Point",
        HouseNumber = "1",
                },
        Latitude = -33.8567844,
        Longitude = 151.2152967,
        CreatedOn = DateTime.UtcNow.AddDays(-5),
        LastEditedOn = DateTime.UtcNow.AddDays(-4),
        Occation = OccationType.Ander,
        Description = "Iconisch operagebouw."
    },
    new Memoria {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000011"),
        Name = "Mount Fuji",
                MemoriaAddress = new Models.Address
        {
        City = "Fujinomiya",
        Country = "Japan",
        Street = "Kitayama",
        HouseNumber = "0",
                },
        Latitude = 35.360555,
        Longitude = 138.727778,
        CreatedOn = DateTime.UtcNow.AddDays(-4),
        LastEditedOn = DateTime.UtcNow.AddDays(-3),
        Occation = OccationType.Werk,
        Description = "Bekende vulkaan en berg."
    },
    new Memoria {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000012"),
        Name = "Niagara Falls",
                MemoriaAddress = new Models.Address
        {
        City = "Niagara Falls",
        Country = "Canada",
        Street = "Niagara Parkway",
        HouseNumber = "6650",
                },
        Latitude = 43.0962143,
        Longitude = -79.0377388,
        CreatedOn = DateTime.UtcNow.AddDays(-3),
        LastEditedOn = DateTime.UtcNow.AddDays(-2),
        Occation = OccationType.Reis,
        Description = "Indrukwekkende watervallen."
    },
    new Memoria {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000013"),
        Name = "Grand Canyon",
                MemoriaAddress = new Models.Address
        {
        City = "Arizona",
        Country = "USA",
        Street = "Grand Canyon Village",
        HouseNumber = "1",
                },
        Latitude = 36.1069652,
        Longitude = -112.1129972,
        CreatedOn = DateTime.UtcNow.AddDays(-2),
        LastEditedOn = DateTime.UtcNow.AddDays(-1),
        Occation = OccationType.Ander,
        Description = "Diepe kloof gevormd door de Colorado rivier."
    },
    new Memoria {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000014"),
        Name = "Burj Khalifa",
                MemoriaAddress = new Models.Address
        {
        City = "Dubai",
        Country = "UAE",
        Street = "Sheikh Mohammed bin Rashid Blvd",
        HouseNumber = "1",
                },
        Latitude = 25.197197,
        Longitude = 55.2743764,
        CreatedOn = DateTime.UtcNow.AddDays(-1),
        LastEditedOn = DateTime.UtcNow,
        Occation = OccationType.Uitgaan,
        Description = "Hoogste gebouw ter wereld."
    },
    new Memoria {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000015"),
        Name = "Great Wall",
                MemoriaAddress = new Models.Address
        {
        City = "Beijing",
        Country = "China",
        Street = "Huairou District",
        HouseNumber = "1",
                },
        Latitude = 40.4319077,
        Longitude = 116.5703749,
        CreatedOn = DateTime.UtcNow,
        LastEditedOn = DateTime.UtcNow,
        Occation = OccationType.Ander,
        Description = "Historische verdedigingsmuur."
    }
};
            return seeding;
        }

        // methodes
        public Task<Memoria> GetMemoriaByIdAsync(Guid id)
        {
            return Task.FromResult(locations.SingleOrDefault(l => l.Id == id));
        } // I want only ONE
        public Task<IEnumerable<Memoria>> GetAllMemoriaAsync()
        {
            return Task.FromResult(locations.AsEnumerable());
        } // I want them ALL
        public Task<IEnumerable<Memoria>> GetMemoriaByFilterAsync(string searchTerm)
        {
            return Task.FromResult(locations.Where(l => l.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));
        } // You get what you type
        private async Task UpdateMemoriaAsync(Memoria updateMemoria)
        {
            var memoriaToBeUpdated = await GetMemoriaByIdAsync(updateMemoria.Id);

            if (memoriaToBeUpdated == null) throw new ArgumentException("Niet gevonden");
            else 
            {
                memoriaToBeUpdated.Name = updateMemoria.Name;
                memoriaToBeUpdated.Latitude = updateMemoria.Latitude;
                memoriaToBeUpdated.Longitude = updateMemoria.Longitude;
                memoriaToBeUpdated.Description = updateMemoria.Description;
                memoriaToBeUpdated.Occation = updateMemoria.Occation;

                memoriaToBeUpdated.MemoriaAddress.Country = updateMemoria.MemoriaAddress.Country;
                memoriaToBeUpdated.MemoriaAddress.City = updateMemoria.MemoriaAddress.City;
                memoriaToBeUpdated.MemoriaAddress.Street = updateMemoria.MemoriaAddress.Street;
                memoriaToBeUpdated.MemoriaAddress.HouseNumber = updateMemoria.MemoriaAddress.HouseNumber;

                await _mediaService.SyncMediaFiles(memoriaToBeUpdated, updateMemoria);
            }
        } // Update
        public async Task DeleteMemoriaAsync(Guid memoriaId)
        {
            var memoriaSpecificToId = await GetMemoriaByIdAsync(memoriaId);
            if (memoriaSpecificToId != null)
            {
                await _mediaService.DeletePhotoAsync(memoriaSpecificToId);
                locations.Remove(memoriaSpecificToId);
            }
            else
            {
                throw new ArgumentException("Er ging iets fout bij het verwijderen");
            }
        } // Delete
        public async Task SaveChangesAsync(Memoria saveMemoria)
        {
            if(saveMemoria == null) throw new ArgumentNullException(nameof(saveMemoria));

            var address =
                    $"{saveMemoria.MemoriaAddress.Street} {saveMemoria.MemoriaAddress.HouseNumber}, " +
                    $"{saveMemoria.MemoriaAddress.City}, {saveMemoria.MemoriaAddress.Country}";

            var location = await _googleGeoCodingService.ForwardGeoCodeAsync(address);

            if(location != null)
            {
                saveMemoria.Latitude = location.Latitude;
                saveMemoria.Longitude = location.Longitude;
            }

            if(saveMemoria.Id == Guid.Empty)
            {
                saveMemoria.Id = Guid.NewGuid();
                if(saveMemoria.MediaMaterial != null)
                {
                    foreach(var media in saveMemoria.MediaMaterial)
                    {
                        media.MemoriaId = saveMemoria.Id;
                    }
                }
                locations.Add(saveMemoria);
            }
            else
            {
                await UpdateMemoriaAsync(saveMemoria);
            }
        } // Will we Create Or Update
    }
}
