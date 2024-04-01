using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace possession_backend.Models
{
    public class Container
    {
        public int ContainerId { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        [JsonIgnore]
        public Location? Location { get; set; }
        [JsonIgnore]
        public ICollection<Item>? Items { get; set; }
    }
}
