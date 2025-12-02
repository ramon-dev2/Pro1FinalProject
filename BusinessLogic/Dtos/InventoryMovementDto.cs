namespace BusinessLogic.Dtos
{
    public class InventoryMovementDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string MovementType { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string? Reason { get; set; }
        public string? Reference { get; set; }
        public DateTime MovementDate { get; set; }
        public string? CreatedBy { get; set; }
    }
}

