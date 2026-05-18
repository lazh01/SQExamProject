namespace Domain.Entities;

public class Cpu
{
    public int Id { get; set; }
    public string OpenDbId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public string Socket { get; set; } = string.Empty;
    public int Tdp { get; set; }
    public List<string> SupportedMemoryTypes { get; set; } = new();
    public int MaxMemoryGb { get; set; }
}

public class Motherboard
{
    public int Id { get; set; }
    public string OpenDbId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public string Socket { get; set; } = string.Empty;
    public string FormFactor { get; set; } = string.Empty;
    public string MemoryType { get; set; } = string.Empty;
    public int MemorySlots { get; set; }
    public int MaxMemoryGb { get; set; }
}

public class Ram
{
    public int Id { get; set; }
    public string OpenDbId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public string RamType { get; set; } = string.Empty;  // DDR4, DDR5
    public string FormFactor { get; set; } = string.Empty; // 288-pin DIMM etc.
    public int CapacityGb { get; set; }
    public int ModuleCount { get; set; }
    public int Speed { get; set; }
}

public class Gpu
{
    public int Id { get; set; }
    public string OpenDbId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public int Tdp { get; set; }
    public double LengthMm { get; set; }
    public int RequiredPowerConnectors8Pin { get; set; }
}

public class Psu
{
    public int Id { get; set; }
    public string OpenDbId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public int Wattage { get; set; }
    public string FormFactor { get; set; } = string.Empty; // ATX, SFX
    public string EfficiencyRating { get; set; } = string.Empty;
}

public class PcCase
{
    public int Id { get; set; }
    public string OpenDbId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public string FormFactor { get; set; } = string.Empty; // ATX Mid Tower etc.
    public List<string> SupportedMotherboardFormFactors { get; set; } = new();
    public List<string> SupportedPsuFormFactors { get; set; } = new();
    public double MaxGpuLengthMm { get; set; }
    public double MaxCpuCoolerHeightMm { get; set; }
}
