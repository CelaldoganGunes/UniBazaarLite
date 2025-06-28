using System.ComponentModel.DataAnnotations;

public class ClassifiedItem
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = "Unknown Title";

    public string Description { get; set; } = "Unknown Description";

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; } = 1;

    public string Seller { get; set; } = "Unknown Seller";
}
