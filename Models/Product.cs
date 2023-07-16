using System.ComponentModel.DataAnnotations;

namespace RestfulApiExample.Models
{
public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ürün adi zorunludur.")]
    public string Name { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }
}


}