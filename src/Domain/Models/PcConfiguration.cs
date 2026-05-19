using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models;

public record PcConfiguration
{
    public Cpu? Cpu { get; set; }
    public Motherboard? Motherboard { get; set; }
    public List<Ram> RamModules { get; set; } = new();
    public Gpu? Gpu { get; set; }
    public Psu? Psu { get; set; }
    public PcCase? Case { get; set; }
}
