using System.Text.Json.Serialization;

namespace possession_backend.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Container>? Containers { get; set; }
        [JsonIgnore]
        public ICollection<Item>? Items { get; set; }
    }
}
