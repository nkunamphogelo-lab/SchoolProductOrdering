// Models/CartItem.cs
public class CartItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    // Takealot style calculation
    public decimal Total => Price * Quantity;
}