using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Mde.Project.Mobile.Core.Data.Seeding
{
    public static class Seed
    {
        // methoden
        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.Memorias.AnyAsync()) return;

            var memorias = new List<Memoria>
            {
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Name = "Eiffel Tower",
                    CreatedOn = DateTime.UtcNow.AddDays(-14),
                    LastEditedOn = DateTime.UtcNow.AddDays(-13),
                    Occation = OccationType.Reis,
                    Description = "Iconische toren en symbool van Parijs.",
                    AddressId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    MemoriaAddress = new Address
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        City = "Paris",
                        Country = "France",
                        Street = "Champ de Mars",
                        HouseNumber = "5",
                        Latitude = 48.8583701,
                        Longitude = 2.2944813,
                    },
                    MediaMaterial = new List<MediaItem>
                    {
                        new MediaItem
                        {
                            Id = Guid.Parse("22222222-2222-2222-2222-222222222220"),
                            Type = MediaType.Photo,
                            FilePath = "france.jpg",
                            CreatedAt = DateTime.UtcNow.AddDays(-14),
                        },
                        new MediaItem
                        {
                            Id = Guid.Parse("22222222-2222-2222-2222-222222222221"),
                            Type = MediaType.Photo,
                            FilePath = "frankrijk.jpg",
                            CreatedAt = DateTime.UtcNow.AddDays(-13)
                        }
                    }
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Name = "Colosseum",
                    CreatedOn = DateTime.UtcNow.AddDays(-13),
                    LastEditedOn = DateTime.UtcNow.AddDays(-12),
                    Occation = OccationType.Reis,
                    Description = "Oud Romeins amfitheater.",
                    AddressId = Guid.Parse("11111111-1111-1111-1111-111111111112"),
                    MemoriaAddress = new Address
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111112"),
                        City = "Rome",
                        Country = "Italy",
                        Street = "Piazza del Colosseo",
                        HouseNumber = "1",
                        Latitude = 41.8902102,
                        Longitude = 12.4922309,
                    },
                    MediaMaterial = new List<MediaItem>(),
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    Name = "Brandenburg Gate",
                    CreatedOn = DateTime.UtcNow.AddDays(-12),
                    LastEditedOn = DateTime.UtcNow.AddDays(-11),
                    Occation = OccationType.Werk,
                    Description = "Historische stadspoort in Berlijn.",
                    AddressId = Guid.Parse("11111111-1111-1111-1111-111111111113"),
                    MemoriaAddress = new Address
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111113"),
                        City = "Berlin",
                        Country = "Germany",
                        Street = "Pariser Platz",
                        HouseNumber = "1",
                        Latitude = 52.5162746,
                        Longitude = 13.3777041,
                    },
                    MediaMaterial = new List<MediaItem>
                    {
                        new MediaItem
                        {
                            Id = Guid.NewGuid(),
                            Type = MediaType.Photo,
                            FilePath = "Assets/SeedImages/berlijn.jpg",
                            CreatedAt = DateTime.UtcNow.AddDays(-12),
                            MemoriaId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                        }
                    }
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    Name = "Sagrada Familia",
                    CreatedOn = DateTime.UtcNow.AddDays(-11),
                    LastEditedOn = DateTime.UtcNow.AddDays(-10),
                    Occation = OccationType.Reis,
                    Description = "Beroemde basiliek ontworpen door Gaudí.",
                    AddressId = Guid.Parse("11111111-1111-1111-1111-111111111114"),
                    MemoriaAddress = new Address
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111114"),
                        City = "Barcelona",
                        Country = "Spain",
                        Street = "Carrer de Mallorca",
                        HouseNumber = "401",
                        Latitude = 41.4036299,
                        Longitude = 2.1743558,
                    },
                    MediaMaterial = new List<MediaItem>(),
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    Name = "Big Ben",
                    CreatedOn = DateTime.UtcNow.AddDays(-10),
                    LastEditedOn = DateTime.UtcNow.AddDays(-9),
                    Occation = OccationType.Ander,
                    Description = "Bekende klokkentoren van Londen.",
                    AddressId = Guid.Parse("11111111-1111-1111-1111-111111111115"),
                    MemoriaAddress = new Address
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111115"),
                        City = "London",
                        Country = "United Kingdom",
                        Street = "Westminster",
                        HouseNumber = "1",
                        Latitude = 51.5007292,
                        Longitude = -0.1246254,
                    },
                    MediaMaterial = new List<MediaItem>(),
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    Name = "Acropolis",
                    CreatedOn = DateTime.UtcNow.AddDays(-9),
                    LastEditedOn = DateTime.UtcNow.AddDays(-8),
                    Occation = OccationType.Uitgaan,
                    Description = "Oude citadel met historische tempels.",
                    AddressId = Guid.Parse("11111111-1111-1111-1111-111111111116"),
                    MemoriaAddress = new Address
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111116"),
                        City = "Athens",
                        Country = "Greece",
                        Street = "Acropolis Hill",
                        HouseNumber = "1",
                        Latitude = 37.971532,
                        Longitude = 23.725749,
                    },
                    MediaMaterial = new List<MediaItem>
                    {
                        new MediaItem
                        {
                            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                            Type = MediaType.Photo,
                            FilePath = "portugal.jpg",
                            CreatedAt = DateTime.UtcNow.AddDays(-8),
                        }
                    }
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                    Name = "Statue of Liberty",
                    CreatedOn = DateTime.UtcNow.AddDays(-8),
                    LastEditedOn = DateTime.UtcNow.AddDays(-7),
                    Occation = OccationType.Uitgaan,
                    Description = "Vrijheidsbeeld in New York.",
                    AddressId = Guid.Parse("11111111-1111-1111-1111-111111111117"),
                    MemoriaAddress = new Address
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111117"),
                        City = "New York",
                        Country = "USA",
                        Street = "Liberty Island",
                        HouseNumber = "1",
                        Latitude = 40.6892494,
                        Longitude = -74.0445004,
                    },
                    MediaMaterial = new List<MediaItem>(),
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    Name = "Christ the Redeemer",
                    CreatedOn = DateTime.UtcNow.AddDays(-7),
                    LastEditedOn = DateTime.UtcNow.AddDays(-6),
                    Occation = OccationType.Werk,
                    Description = "Groot Christusbeeld op de Corcovado.",
                    AddressId = Guid.Parse("11111111-1111-1111-1111-111111111118"),
                    MemoriaAddress = new Address
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111118"),
                        City = "Rio de Janeiro",
                        Country = "Brazil",
                        Street = "Parque Nacional da Tijuca",
                        HouseNumber = "1",
                        Latitude = -22.951916,
                        Longitude = -43.210487,
                    },
                    MediaMaterial = new List<MediaItem>(),

                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                    Name = "Taj Mahal",
                    CreatedOn = DateTime.UtcNow.AddDays(-6),
                    LastEditedOn = DateTime.UtcNow.AddDays(-5),
                    Occation = OccationType.Reis,
                    Description = "Wit marmeren mausoleum.",
                    AddressId = Guid.Parse("11111111-1111-1111-1111-111111111119"),
                    MemoriaAddress = new Address
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111119"),
                        City = "Agra",
                        Country = "India",
                        Street = "Dharmapuri",
                        HouseNumber = "1",
                        Latitude = 27.1751448,
                        Longitude = 78.0421422,
                    },
                    MediaMaterial = new List<MediaItem>(),
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                    Name = "Sydney Opera House",
                    CreatedOn = DateTime.UtcNow.AddDays(-5),
                    LastEditedOn = DateTime.UtcNow.AddDays(-4),
                    Occation = OccationType.Ander,
                    Description = "Iconisch operagebouw.",
                    AddressId = Guid.Parse("11111111-1111-1111-1111-111111111120"),
                    MemoriaAddress = new Address
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111120"),
                        City = "Sydney",
                        Country = "Australia",
                        Street = "Bennelong Point",
                        HouseNumber = "1",
                        Latitude = -33.8567844,
                        Longitude = 151.2152967,
                    },
                    MediaMaterial = new List<MediaItem>(),
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                    Name = "Mount Fuji",
                    CreatedOn = DateTime.UtcNow.AddDays(-4),
                    LastEditedOn = DateTime.UtcNow.AddDays(-3),
                    Occation = OccationType.Werk,
                    Description = "Bekende vulkaan en berg.",
                    AddressId = Guid.Parse("11111111-1111-1111-1111-111111111121"),
                    MemoriaAddress = new Address
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111121"),
                        City = "Fujinomiya",
                        Country = "Japan",
                        Street = "Kitayama",
                        HouseNumber = "1",
                        Latitude = 35.3606255,
                        Longitude = 138.7273634,
                    },
                    MediaMaterial = new List<MediaItem>(),
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                    Name = "Niagara Falls",
                    CreatedOn = DateTime.UtcNow.AddDays(-3),
                    LastEditedOn = DateTime.UtcNow.AddDays(-2),
                    Occation = OccationType.Reis,
                    Description = "Indrukwekkende watervallen.",
                    AddressId = Guid.Parse("11111111-1111-1111-1111-111111111122"),
                    MemoriaAddress = new Address
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111122"),
                        City = "Niagara Falls",
                        Country = "Canada",
                        Street = "Niagara Parkway",
                        HouseNumber = "1",
                        Latitude = 43.0799,
                        Longitude = -79.0747,
                    },
                    MediaMaterial = new List<MediaItem>(),
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                    Name = "Grand Canyon",
                    CreatedOn = DateTime.UtcNow.AddDays(-2),
                    LastEditedOn = DateTime.UtcNow.AddDays(-1),
                    Occation = OccationType.Ander,
                    Description = "Diepe kloof gevormd door de Colorado rivier.",
                    AddressId = Guid.Parse("11111111-1111-1111-1111-111111111123"),
                    MemoriaAddress = new Address
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111123"),
                        City = "Grand Canyon Village",
                        Country = "USA",
                        Street = "Grand Canyon Village",
                        HouseNumber = "1",
                        Latitude = 36.1069652,
                        Longitude = -112.1129972,
                    },
                    MediaMaterial = new List<MediaItem>(),
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                    Name = "Burj Khalifa",
                    CreatedOn = DateTime.UtcNow.AddDays(-1),
                    LastEditedOn = DateTime.UtcNow,
                    Occation = OccationType.Uitgaan,
                    Description = "Hoogste gebouw ter wereld.",
                    AddressId = Guid.Parse("11111111-1111-1111-1111-111111111124"),
                    MemoriaAddress = new Address
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111124"),
                        City = "Dubai",
                        Country = "UAE",
                        Street = "1 Sheikh Mohammed bin Rashid Blvd",
                        HouseNumber = "1",
                        Latitude = 25.197197,
                        Longitude = 55.274376,
                    },
                    MediaMaterial = new List<MediaItem>(),
                },
                new Memoria {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                    Name = "Great Wall",
                    CreatedOn = DateTime.UtcNow,
                    LastEditedOn = DateTime.UtcNow,
                    Occation = OccationType.Ander,
                    Description = "Historische verdedigingsmuur.",
                    AddressId = Guid.Parse("11111111-1111-1111-1111-111111111125"),
                    MemoriaAddress = new Address
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111125"),
                        City = "Beijing",
                        Country = "China",
                        Street = "Badaling",
                        HouseNumber = "1",
                        Latitude = 40.4319077,
                        Longitude = 116.5703749,
                    },
                    MediaMaterial = new List<MediaItem>(),
                }
            };

            try
            {
                context.Memorias.AddRange(memorias);
                await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error seeding data: {ex}");
            }
        }
    }
}
