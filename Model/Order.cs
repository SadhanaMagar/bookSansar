namespace Application.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = default!;

        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Pending"; 

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
