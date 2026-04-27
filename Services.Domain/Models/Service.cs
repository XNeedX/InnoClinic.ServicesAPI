using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Domain.Models;

public class Service
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; } = 0;
    public ServiceStatus Status { get; set; }
    public ServiceCategory Category { get; set; }
}
