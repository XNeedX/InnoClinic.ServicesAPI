using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Domain.Models;

public class Service : Entity
{
    public string Name { get; set; }
    public decimal Price { get; set; } = 0;  
    public Category Category { get; set; }
    public Guid SpecializationId { get; set; }
    public Specialization Specialization { get; set; }
}
