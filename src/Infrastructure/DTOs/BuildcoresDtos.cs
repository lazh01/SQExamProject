using System.Text.Json.Serialization;

namespace Infrastructure.DTOs;

// ─── CPU ──────────────────────────────────────────────────────────────────────

public class CpuDto
{
    [JsonPropertyName("opendb_id")]
    public string OpenDbId { get; set; } = string.Empty;

    [JsonPropertyName("socket")]
    public string? Socket { get; set; }

    [JsonPropertyName("metadata")]
    public ComponentMetadata? Metadata { get; set; }

    [JsonPropertyName("specifications")]
    public CpuSpecifications? Specifications { get; set; }
}

public class CpuSpecifications
{
    [JsonPropertyName("tdp")]
    public int? Tdp { get; set; }

    [JsonPropertyName("memory")]
    public CpuMemorySpec? Memory { get; set; }
}

public class CpuMemorySpec
{
    [JsonPropertyName("types")]
    public List<string>? Types { get; set; }

    [JsonPropertyName("maxSupport")]
    public int? MaxSupport { get; set; }
}

// ─── Motherboard ──────────────────────────────────────────────────────────────

public class MotherboardDto
{
    [JsonPropertyName("opendb_id")]
    public string OpenDbId { get; set; } = string.Empty;

    [JsonPropertyName("socket")]
    public string? Socket { get; set; }

    [JsonPropertyName("form_factor")]
    public string? FormFactor { get; set; }

    [JsonPropertyName("memory")]
    public MotherboardMemory? Memory { get; set; }

    [JsonPropertyName("metadata")]
    public ComponentMetadata? Metadata { get; set; }
}

public class MotherboardMemory
{
    [JsonPropertyName("ram_type")]
    public string? RamType { get; set; }

    [JsonPropertyName("slots")]
    public int? Slots { get; set; }

    [JsonPropertyName("max")]
    public int? Max { get; set; }
}

// ─── RAM ──────────────────────────────────────────────────────────────────────

public class RamDto
{
    [JsonPropertyName("opendb_id")]
    public string OpenDbId { get; set; } = string.Empty;

    [JsonPropertyName("ram_type")]
    public string? RamType { get; set; }

    [JsonPropertyName("form_factor")]
    public string? FormFactor { get; set; }

    [JsonPropertyName("capacity")]
    public int? Capacity { get; set; }

    [JsonPropertyName("speed")]
    public int? Speed { get; set; }

    [JsonPropertyName("modules")]
    public RamModules? Modules { get; set; }

    [JsonPropertyName("metadata")]
    public ComponentMetadata? Metadata { get; set; }
}

public class RamModules
{
    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    [JsonPropertyName("capacity_gb")]
    public int? CapacityGb { get; set; }
}

// ─── GPU ──────────────────────────────────────────────────────────────────────

public class GpuDto
{
    [JsonPropertyName("opendb_id")]
    public string OpenDbId { get; set; } = string.Empty;

    [JsonPropertyName("metadata")]
    public ComponentMetadata? Metadata { get; set; }

    [JsonPropertyName("tdp")]
    public int? Tdp { get; set; }

    [JsonPropertyName("length")]
    public double? Length { get; set; }

    [JsonPropertyName("power_connectors")]
    public GpuPowerConnectors? PowerConnectors { get; set; }
}

public class GpuPowerConnectors
{
    [JsonPropertyName("pcie_8_pin")]
    public int? Pcie8Pin { get; set; }

    [JsonPropertyName("pcie_12VHPWR")]
    public int? Pcie12VHpwr { get; set; }

    [JsonPropertyName("pcie_12V_2x6")]
    public int? Pcie12V2x6 { get; set; }
}

// ─── PSU ──────────────────────────────────────────────────────────────────────

public class PsuDto
{
    [JsonPropertyName("opendb_id")]
    public string OpenDbId { get; set; } = string.Empty;

    [JsonPropertyName("wattage")]
    public int? Wattage { get; set; }

    [JsonPropertyName("form_factor")]
    public string? FormFactor { get; set; }

    [JsonPropertyName("efficiency_rating")]
    public string? EfficiencyRating { get; set; }

    [JsonPropertyName("metadata")]
    public ComponentMetadata? Metadata { get; set; }
}

// ─── PC Case ──────────────────────────────────────────────────────────────────

public class PcCaseDto
{
    [JsonPropertyName("opendb_id")]
    public string OpenDbId { get; set; } = string.Empty;

    [JsonPropertyName("form_factor")]
    public string? FormFactor { get; set; }

    [JsonPropertyName("supported_motherboard_form_factors")]
    public List<string>? SupportedMotherboardFormFactors { get; set; }

    [JsonPropertyName("supported_power_supply_form_factors")]
    public List<string>? SupportedPowerSupplyFormFactors { get; set; }

    [JsonPropertyName("max_video_card_length")]
    public double? MaxVideoCardLength { get; set; }

    [JsonPropertyName("max_cpu_cooler_height")]
    public double? MaxCpuCoolerHeight { get; set; }

    [JsonPropertyName("metadata")]
    public ComponentMetadata? Metadata { get; set; }
}

// ─── Shared ───────────────────────────────────────────────────────────────────

public class ComponentMetadata
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("manufacturer")]
    public string? Manufacturer { get; set; }

    [JsonPropertyName("series")]
    public string? Series { get; set; }
}
