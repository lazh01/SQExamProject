using System.Text.Json.Serialization;

namespace PcCompatibility.Infrastructure.DTOs;

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

    [JsonPropertyName("memory_type")]
    public string? MemoryType { get; set; }

    [JsonPropertyName("memory_slots")]
    public int? MemorySlots { get; set; }

    [JsonPropertyName("max_memory")]
    public int? MaxMemory { get; set; }

    [JsonPropertyName("metadata")]
    public ComponentMetadata? Metadata { get; set; }
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

    [JsonPropertyName("specifications")]
    public GpuSpecifications? Specifications { get; set; }
}

public class GpuSpecifications
{
    [JsonPropertyName("tdp")]
    public int? Tdp { get; set; }

    [JsonPropertyName("length_mm")]
    public double? LengthMm { get; set; }

    [JsonPropertyName("power_connectors_8pin")]
    public int? PowerConnectors8Pin { get; set; }
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
