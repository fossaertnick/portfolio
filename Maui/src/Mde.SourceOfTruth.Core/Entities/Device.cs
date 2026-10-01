namespace Mde.SourceOfTruth.Core.Entities
{
    public class Device

    {
        public Guid Id { get; set; }
        public string DeviceIdentifier { get; set; }
        public string DeviceName { get; set; }  
        public DateTime RegisteredOn { get; set; }

        // navigation propertie
        public ICollection<Memoria> Memorias { get; set; } = new List<Memoria>();
    }
}
