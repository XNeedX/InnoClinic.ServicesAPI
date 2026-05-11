namespace Services.Domain.Models;

public class Specialization : Entity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Category Category { get; set; }
}
