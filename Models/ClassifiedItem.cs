using System.ComponentModel.DataAnnotations;

public class ClassifiedItem
{
    public int Id { get; set; }

    [Required]
    public required string Title { get; set; }

    public required string Description { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    public required string Seller { get; set; }
}
