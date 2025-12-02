namespace BusinessLogic.Dtos
{
    public class OrderFilterDto
    {
        public int? CustomerId { get; set; }
        public int? StatusId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public decimal? MinTotal { get; set; }
        public decimal? MaxTotal { get; set; }
    }
}

