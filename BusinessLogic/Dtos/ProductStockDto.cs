namespace BusinessLogic.Dtos
{
    public class ProductStockDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? CategoryName { get; set; }
        public int CurrentStock { get; set; }
        public decimal Price { get; set; }
    }
}

