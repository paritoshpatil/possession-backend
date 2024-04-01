namespace possession_backend.Models
{
    public class ItemsFilter
    {
        public int? ItemId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? MaxDate { get; set; }
        public DateTime? MinDate { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public int? CategoryId { get; set; }
        public int? LocationId { get; set; }
        public int? ContainerId { get; set; }
    }
}
