using Mde.SourceOfTruth.Core.Entities;
using Mde.SourceOfTruth.Core.Entities.enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.SourceOfTruth.Core.Data.Seeding
{
    public class Seeder
    {
        public static void Seed(ModelBuilder builder)
        {
            var memorias = new List<Memoria>
            {
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Name = "Eiffel Tower",
                    EventDate = new DateTime(2025, 01, 03),
                    CreatedOn = new DateTime(2025, 01, 03),
                    LastEditedOn = new DateTime(2025, 01, 13),
                    Occation = OccationType.Reis,
                    Description = "Iconische toren en symbool van Parijs.",
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Name = "Colosseum",
                    EventDate = new DateTime(2025, 01, 01),
                    CreatedOn = new DateTime(2025, 01, 11),
                    LastEditedOn = new DateTime(2025, 01, 11),
                    Occation = OccationType.Reis,
                    Description = "Oud Romeins amfitheater.",
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    Name = "Brandenburg Gate",
                    EventDate = new DateTime(2025, 01, 19),
                    CreatedOn = new DateTime(2025, 01, 19),
                    LastEditedOn = new DateTime(2025, 01, 21),
                    Occation = OccationType.Werk,
                    Description = "Historische stadspoort in Berlijn.",
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    Name = "Sagrada Familia",
                    EventDate = new DateTime(2025, 01, 27),
                    CreatedOn = new DateTime(2025, 01, 27),
                    LastEditedOn = new DateTime(2025, 01, 27),
                    Occation = OccationType.Reis,
                    Description = "Beroemde basiliek ontworpen door Gaudí.",
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    Name = "Big Ben",
                    EventDate = new DateTime(2025, 02, 04),
                    CreatedOn = new DateTime(2025, 02, 04),
                    LastEditedOn = new DateTime(2025, 02, 04),
                    Occation = OccationType.Ander,
                    Description = "Bekende klokkentoren van Londen.",
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    Name = "Acropolis",
                    EventDate = new DateTime(2025, 01, 21),
                    CreatedOn = new DateTime(2025, 01, 27),
                    LastEditedOn = new DateTime(2025, 01, 29),
                    Occation = OccationType.Uitgaan,
                    Description = "Oude citadel met historische tempels.",
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                    Name = "Statue of Liberty",
                    EventDate = new DateTime(2025, 01, 27),
                    CreatedOn = new DateTime(2025, 01, 27),
                    LastEditedOn = new DateTime(2025, 01, 27),
                    Occation = OccationType.Uitgaan,
                    Description = "Vrijheidsbeeld in New York.",
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    Name = "Christ the Redeemer",
                    EventDate = new DateTime(2025, 01, 17),
                    CreatedOn = new DateTime(2025, 01, 27),
                    LastEditedOn = new DateTime(2025, 01, 27),
                    Occation = OccationType.Werk,
                    Description = "Groot Christusbeeld op de Corcovado.",
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                    Name = "Taj Mahal",
                    EventDate = new DateTime(2025, 01, 27),
                    CreatedOn = new DateTime(2025, 01, 27),
                    LastEditedOn = new DateTime(2025, 01, 27),
                    Occation = OccationType.Reis,
                    Description = "Wit marmeren mausoleum.",
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                    Name = "Sydney Opera House",
                    EventDate = new DateTime(2025, 01, 27),
                    CreatedOn = new DateTime(2025, 01, 27),
                    LastEditedOn = new DateTime(2025, 01, 27),
                    Occation = OccationType.Ander,
                    Description = "Iconisch operagebouw.",
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                    Name = "Mount Fuji",
                    EventDate = new DateTime(2025, 01, 27),
                    CreatedOn = new DateTime(2025, 01, 27),
                    LastEditedOn = new DateTime(2025, 01, 27),
                    Occation = OccationType.Werk,
                    Description = "Bekende vulkaan en berg.",
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                    Name = "Niagara Falls",
                    EventDate = new DateTime(2025, 01, 27),
                    CreatedOn = new DateTime(2025, 01, 27),
                    LastEditedOn = new DateTime(2025, 01, 27),
                    Occation = OccationType.Reis,
                    Description = "Indrukwekkende watervallen.",
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                    Name = "Grand Canyon",
                    EventDate = new DateTime(2025, 01, 27),
                    CreatedOn = new DateTime(2025, 01, 27),
                    LastEditedOn = new DateTime(2025, 01, 27),
                    Occation = OccationType.Ander,
                    Description = "Diepe kloof gevormd door de Colorado rivier.",
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                    Name = "Burj Khalifa",
                    EventDate = new DateTime(2025, 01, 27),
                    CreatedOn = new DateTime(2025, 01, 27),
                    LastEditedOn = new DateTime(2025, 01, 27),
                    Occation = OccationType.Uitgaan,
                    Description = "Hoogste gebouw ter wereld.",
                },
                new Memoria 
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                    Name = "Great Wall",
                    EventDate = new DateTime(2025, 01, 30),
                    CreatedOn = new DateTime(2025, 02, 12),
                    LastEditedOn = new DateTime(2025, 02, 13),
                    Occation = OccationType.Ander,
                    Description = "Historische verdedigingsmuur.",
                }
            };
            var addresses = new List<Address>
            {
                new Address
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    MemoriaId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    City = "Paris",
                    Country = "France",
                    Street = "Champ de Mars",
                    HouseNumber = "5",
                    Latitude = 48.8583701,
                    Longitude = 2.2944813,
                },
                new Address
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111112"),
                    MemoriaId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    City = "Rome",
                    Country = "Italy",
                    Street = "Piazza del Colosseo",
                    HouseNumber = "1",
                    Latitude = 41.8902102,
                    Longitude = 12.4922309,
                },
                new Address
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111113"),
                    MemoriaId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    City = "Berlin",
                    Country = "Germany",
                    Street = "Pariser Platz",
                    HouseNumber = "1",
                    Latitude = 52.5162746,
                    Longitude = 13.3777041,
                },
                new Address
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111114"),
                    MemoriaId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    City = "Barcelona",
                    Country = "Spain",
                    Street = "Carrer de Mallorca",
                    HouseNumber = "401",
                    Latitude = 41.4036299,
                    Longitude = 2.1743558,
                },
                new Address
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111115"),
                    MemoriaId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    City = "London",
                    Country = "United Kingdom",
                    Street = "Westminster",
                    HouseNumber = "1",
                    Latitude = 51.5007292,
                    Longitude = -0.1246254,
                },
                new Address
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111116"),
                    MemoriaId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    City = "Athens",
                    Country = "Greece",
                    Street = "Acropolis Hill",
                    HouseNumber = "1",
                    Latitude = 37.971532,
                    Longitude = 23.725749,
                },
                new Address
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111117"),
                    MemoriaId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                    City = "New York",
                    Country = "USA",
                    Street = "Liberty Island",
                    HouseNumber = "1",
                    Latitude = 40.6892494,
                    Longitude = -74.0445004,
                },
                new Address
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111118"),
                    MemoriaId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    City = "Rio de Janeiro",
                    Country = "Brazil",
                    Street = "Parque Nacional da Tijuca",
                    HouseNumber = "1",
                    Latitude = -22.951916,
                    Longitude = -43.210487,
                },
                new Address
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111119"),
                    MemoriaId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                    City = "Agra",
                    Country = "India",
                    Street = "Dharmapuri",
                    HouseNumber = "1",
                    Latitude = 27.1751448,
                    Longitude = 78.0421422,
                },
                new Address
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111120"),
                    MemoriaId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                    City = "Sydney",
                    Country = "Australia",
                    Street = "Bennelong Point",
                    HouseNumber = "1",
                    Latitude = -33.8567844,
                    Longitude = 151.2152967,
                },
                new Address
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111121"),
                    MemoriaId = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                    City = "Fujinomiya",
                    Country = "Japan",
                    Street = "Kitayama",
                    HouseNumber = "1",
                    Latitude = 35.3606255,
                    Longitude = 138.7273634,
                },
                new Address
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111122"),
                    MemoriaId = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                    City = "Niagara Falls",
                    Country = "Canada",
                    Street = "Niagara Parkway",
                    HouseNumber = "1",
                    Latitude = 43.0799,
                    Longitude = -79.0747,
                },
                new Address
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111123"),
                    MemoriaId = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                    City = "Grand Canyon Village",
                    Country = "USA",
                    Street = "Grand Canyon Village",
                    HouseNumber = "1",
                    Latitude = 36.1069652,
                    Longitude = -112.1129972,
                },
                new Address
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111124"),
                    MemoriaId = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                    City = "Dubai",
                    Country = "UAE",
                    Street = "1 Sheikh Mohammed bin Rashid Blvd",
                    HouseNumber = "1",
                    Latitude = 25.197197,
                    Longitude = 55.274376,
                },
                new Address
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111125"),
                    MemoriaId = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                    City = "Beijing",
                    Country = "China",
                    Street = "Badaling",
                    HouseNumber = "1",
                    Latitude = 40.4319077,
                    Longitude = 116.5703749,
                },
            };
            var images = new List<MediaItem>
            {
                new MediaItem
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222220"),
                    Type = MediaType.Photo,
                    FilePath = "https://06dfrpsm-44338.brs.devtunnels.ms/img/france.jpg",
                    CreatedAt = new DateTime(2025, 01, 03),
                    MemoriaId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                },
                new MediaItem
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222221"),
                    Type = MediaType.Photo,
                    FilePath = "https://06dfrpsm-44338.brs.devtunnels.ms/img/frankrijk.jpg",
                    CreatedAt = new DateTime(2025, 01, 13),
                    MemoriaId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                },
                new MediaItem
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Type = MediaType.Photo,
                    FilePath = "https://06dfrpsm-44338.brs.devtunnels.ms/img/athens.jpg",
                    CreatedAt = new DateTime(2025, 01, 29),
                    MemoriaId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                }
            };

            builder.Entity<Memoria>().HasData(memorias);
            builder.Entity<Address>().HasData(addresses);
            builder.Entity<MediaItem>().HasData(images);
        }
    }
}
