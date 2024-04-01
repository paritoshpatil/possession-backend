using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace possession_backend.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal OriginalPrice { get; set; }
        public string WarrantyInfo { get; set; }
        public int? CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }
        public int LocationId { get; set; }
        [JsonIgnore]
        public Location? Location { get; set; }
        public int? ContainerId { get; set; }
        [JsonIgnore]
        public Container? Container { get; set; }
    }
}
