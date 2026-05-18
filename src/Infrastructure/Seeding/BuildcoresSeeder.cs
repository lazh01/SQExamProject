using System.Text.Json;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.DTOs;

namespace Infrastructure.Seeding;

public static class BuildcoresSeeder
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static void Seed(AppDbContext context, string openDbPath)
    {
        SeedCpus(context, Path.Combine(openDbPath, "CPU"));
        SeedMotherboards(context, Path.Combine(openDbPath, "Motherboard"));
        SeedRam(context, Path.Combine(openDbPath, "RAM"));
        SeedGpus(context, Path.Combine(openDbPath, "GPU"));
        SeedPsus(context, Path.Combine(openDbPath, "PSU"));
        SeedCases(context, Path.Combine(openDbPath, "PCCase"));
        context.SaveChanges();
    }

    private static void SeedCpus(AppDbContext context, string path)
    {
        if (!Directory.Exists(path)) return;
        if (context.Cpus.Any()) return;

        foreach (var file in Directory.GetFiles(path, "*.json"))
        {
            var dto = Deserialize<CpuDto>(file);
            if (dto is null) continue;

            var entity = new Cpu
            {
                OpenDbId        = dto.OpenDbId,
                Name            = dto.Metadata?.Name ?? Path.GetFileNameWithoutExtension(file),
                Manufacturer    = dto.Metadata?.Manufacturer ?? string.Empty,
                Socket          = dto.Socket ?? string.Empty,
                Tdp             = dto.Specifications?.Tdp ?? 0,
                SupportedMemoryTypes = dto.Specifications?.Memory?.Types ?? new List<string>(),
                MaxMemoryGb     = dto.Specifications?.Memory?.MaxSupport ?? 0,
            };

            context.Cpus.Add(entity);
        }
    }

    private static void SeedMotherboards(AppDbContext context, string path)
    {
        if (!Directory.Exists(path)) return;
        if (context.Motherboards.Any()) return;

        foreach (var file in Directory.GetFiles(path, "*.json"))
        {
            var dto = Deserialize<MotherboardDto>(file);
            if (dto is null) continue;

            context.Motherboards.Add(new Motherboard
            {
                OpenDbId = dto.OpenDbId,
                Name = dto.Metadata?.Name ?? Path.GetFileNameWithoutExtension(file),
                Manufacturer = dto.Metadata?.Manufacturer ?? string.Empty,
                Socket = dto.Socket ?? string.Empty,
                FormFactor = dto.FormFactor ?? string.Empty,
                MemoryType = dto.Memory?.RamType ?? string.Empty,
                MemorySlots = dto.Memory?.Slots ?? 0,
                MaxMemoryGb = dto.Memory?.Max ?? 0,
            });
        }
    }

    private static void SeedRam(AppDbContext context, string path)
    {
        if (!Directory.Exists(path)) return;
        if (context.Rams.Any()) return;

        foreach (var file in Directory.GetFiles(path, "*.json"))
        {
            var dto = Deserialize<RamDto>(file);
            if (dto is null) continue;

            context.Rams.Add(new Ram
            {
                OpenDbId     = dto.OpenDbId,
                Name         = dto.Metadata?.Name ?? Path.GetFileNameWithoutExtension(file),
                Manufacturer = dto.Metadata?.Manufacturer ?? string.Empty,
                RamType      = dto.RamType ?? string.Empty,
                FormFactor   = dto.FormFactor ?? string.Empty,
                CapacityGb   = dto.Capacity ?? 0,
                ModuleCount  = dto.Modules?.Quantity ?? 1,
                Speed        = dto.Speed ?? 0,
            });
        }
    }

    private static void SeedGpus(AppDbContext context, string path)
    {
        if (!Directory.Exists(path)) return;
        if (context.Gpus.Any()) return;

        foreach (var file in Directory.GetFiles(path, "*.json"))
        {
            var dto = Deserialize<GpuDto>(file);
            if (dto is null) continue;

            context.Gpus.Add(new Gpu
            {
                OpenDbId = dto.OpenDbId,
                Name = dto.Metadata?.Name ?? Path.GetFileNameWithoutExtension(file),
                Manufacturer = dto.Metadata?.Manufacturer ?? string.Empty,
                Tdp = dto.Tdp ?? 0,
                LengthMm = dto.Length ?? 0,
                RequiredPowerConnectors8Pin = dto.PowerConnectors?.Pcie8Pin ?? 0,
            });
        }
    }

    private static void SeedPsus(AppDbContext context, string path)
    {
        if (!Directory.Exists(path)) return;
        if (context.Psus.Any()) return;

        foreach (var file in Directory.GetFiles(path, "*.json"))
        {
            var dto = Deserialize<PsuDto>(file);
            if (dto is null) continue;

            context.Psus.Add(new Psu
            {
                OpenDbId         = dto.OpenDbId,
                Name             = dto.Metadata?.Name ?? Path.GetFileNameWithoutExtension(file),
                Manufacturer     = dto.Metadata?.Manufacturer ?? string.Empty,
                Wattage          = dto.Wattage ?? 0,
                FormFactor       = dto.FormFactor ?? string.Empty,
                EfficiencyRating = dto.EfficiencyRating ?? string.Empty,
            });
        }
    }

    private static void SeedCases(AppDbContext context, string path)
    {
        if (!Directory.Exists(path)) return;
        if (context.PcCases.Any()) return;

        foreach (var file in Directory.GetFiles(path, "*.json"))
        {
            var dto = Deserialize<PcCaseDto>(file);
            if (dto is null) continue;

            context.PcCases.Add(new PcCase
            {
                OpenDbId                        = dto.OpenDbId,
                Name                            = dto.Metadata?.Name ?? Path.GetFileNameWithoutExtension(file),
                Manufacturer                    = dto.Metadata?.Manufacturer ?? string.Empty,
                FormFactor                      = dto.FormFactor ?? string.Empty,
                SupportedMotherboardFormFactors = dto.SupportedMotherboardFormFactors ?? new List<string>(),
                SupportedPsuFormFactors         = dto.SupportedPowerSupplyFormFactors ?? new List<string>(),
                MaxGpuLengthMm                  = dto.MaxVideoCardLength ?? 0,
                MaxCpuCoolerHeightMm            = dto.MaxCpuCoolerHeight ?? 0,
            });
        }
    }

    private static T? Deserialize<T>(string filePath)
    {
        try
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(json, JsonOptions);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Seeder] Skipping {Path.GetFileName(filePath)}: {ex.Message}");
            return default;
        }
    }
}
