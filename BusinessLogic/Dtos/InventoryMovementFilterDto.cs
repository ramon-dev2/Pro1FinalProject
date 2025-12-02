namespace BusinessLogic.Dtos
{
    public class InventoryMovementFilterDto
    {
        public int? ProductId { get; set; }
        public string? MovementType { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}

