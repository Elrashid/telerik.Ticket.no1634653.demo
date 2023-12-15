namespace telerik.Ticket.no1634653.demo.Models
{
    public partial class FixAsset
    {
        public int AssetId { get; set; }
        public int  LocationId { get; set; }
        public int  IsPlaceholder { get; set; }
        public required String AssetName { get; set; }
        public int Quantity { get; set; }
        public int? ParentId { get; set; }

    }

}