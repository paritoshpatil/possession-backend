using System.Text.Json.Serialization;

namespace possession_backend.Models
{
    public class Category
    {
        public int CatrgoryId { get; set; } 
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Item>? Items { get; set; }
    }
}
