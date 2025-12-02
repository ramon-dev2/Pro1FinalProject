namespace BusinessLogic.Dtos
{
    public class ProductFilterDto
    {
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinStock { get; set; }
        public string? Name { get; set; }
    }
}

