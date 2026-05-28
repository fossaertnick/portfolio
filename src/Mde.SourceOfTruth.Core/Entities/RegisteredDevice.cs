using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.SourceOfTruth.Core.Entities
{
    public class RegisteredDevice
    {
        public Guid Id { get; set; }
        public string DeviceId { get; set; } = string.Empty;
        public string DeviceName { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        // navigation
        public ICollection<Memoria> memorias { get; set; } = new List<Memoria>();
    }
}
