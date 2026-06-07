using Mde.SourceOfTruth.Core.Entities;
using Mde.SourceOfTruth.Core.Entities.enums;
using Microsoft.EntityFrameworkCore;

namespace Mde.SourceOfTruth.Core.Data.Seeding
{
    public class Seeder
    {
        public static void Seed(ModelBuilder builder)
        {
            var devices = new List<Device>
            {
                new Device
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    DeviceIdentifier = "device-001",
                    DeviceName = "John's Android",
                    RegisteredOn = new DateTime(2025, 01, 01),
                },
                new Device
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333334"),
                    DeviceIdentifier = "device-002",
                    DeviceName = "Jack's Windows",
                    RegisteredOn = new DateTime(2025, 01, 05),
                },
            };

            var memorias = new List<Memoria>
            {
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Name = "Eiffel Tower",
                    EventDate = new DateTime(2025, 01, 03),
                    CreatedOn = new DateTime(2025, 01, 03),
                    LastEditedOn = new DateTime(2025, 01, 13),
                    Occation = OccationType.Travel,
                    Description = "Iconische toren en symbool van Parijs.",
                    DeviceId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Name = "Colosseum",
                    EventDate = new DateTime(2025, 01, 01),
                    CreatedOn = new DateTime(2025, 01, 11),
                    LastEditedOn = new DateTime(2025, 01, 11),
                    Occation = OccationType.Travel,
                    Description = "Oud Romeins amfitheater.",
                    DeviceId = Guid.Parse("33333333-3333-3333-3333-333333333334"),
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    Name = "Brandenburg Gate",
                    EventDate = new DateTime(2025, 01, 19),
                    CreatedOn = new DateTime(2025, 01, 19),
                    LastEditedOn = new DateTime(2025, 01, 21),
                    Occation = OccationType.Work,
                    Description = "Historische stadspoort in Berlijn.",
                    DeviceId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    Name = "Sagrada Familia",
                    EventDate = new DateTime(2025, 01, 27),
                    CreatedOn = new DateTime(2025, 01, 27),
                    LastEditedOn = new DateTime(2025, 01, 27),
                    Occation = OccationType.Travel,
                    Description = "Beroemde basiliek ontworpen door Gaudí.",
                    DeviceId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                },
                new Memoria
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    Name = "Big Ben",
                    EventDate = new DateTime(2025, 02, 04),
                    CreatedOn = new DateTime(2025, 02, 04),
                    LastEditedOn = new DateTime(2025, 02, 04),
                    Occation = OccationType.Other,
                    Description = "Bekende klokkentoren van Londen.",
                    DeviceId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                },
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
            };
            var images = new List<MediaItem>
            { // fotos voor parijs
                new MediaItem
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222221"),
                    Type = MediaType.Photo,
                    FilePath = "https://06dfrpsm-44338.brs.devtunnels.ms/img/paris1.jpg",
                    CreatedAt = new DateTime(2025, 01, 03),
                    MemoriaId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                },
            };

            builder.Entity<Device>().HasData(devices);
            builder.Entity<Memoria>().HasData(memorias);
            builder.Entity<Address>().HasData(addresses);
            builder.Entity<MediaItem>().HasData(images);
        }
    }
}
