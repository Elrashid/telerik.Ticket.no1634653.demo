namespace telerik.Ticket.no1634653.demo.Models
{

    public partial class  Location
    {
        public int LocationId { get; set; }
         
        public required string LocationName { get; set; }

        public int? ParentId { get; set; }

     }


}