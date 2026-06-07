using Mde.Project.Mobile.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Core.Entities
{
    public class MemoriaList
    {
        // propreties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public OccationType OccationType { get; set; }
        public int TotalPhotos { get; set; }
        public int TotalVideos { get; set; }
        public string Country { get; set; }
    }
}
