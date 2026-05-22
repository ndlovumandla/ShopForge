namespace ShopForge.Database.Entities;

public class ProductAttribute
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string AttributeName { get; set; } = string.Empty;
    public string AttributeValue { get; set; } = string.Empty;
    public int DisplayOrder { get; set; } = 0;
    public Product Product { get; set; } = null!;
}
