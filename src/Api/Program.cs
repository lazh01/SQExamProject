using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;
using Infrastructure.Seeding;

// ── DI setup ──────────────────────────────────────────────────────────────────
var services = new ServiceCollection();

services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=pcparts.db"));

var provider = services.BuildServiceProvider();

// ── Migrate & seed ────────────────────────────────────────────────────────────
using var scope = provider.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

context.Database.EnsureCreated();

// Point this at wherever you cloned buildcores-open-db
//var openDbPath = Path.Combine(AppContext.BaseDirectory, "open-db");
var openDbPath = @"C:\Users\Sebastian\Downloads\buildcores-open-db-main\buildcores-open-db-main\open-db";
Console.WriteLine($"Path exists: {Directory.Exists(openDbPath)}");
Console.WriteLine($"Subdirs: {string.Join(", ", Directory.GetDirectories(openDbPath).Select(Path.GetFileName))}");

BuildcoresSeeder.Seed(context, openDbPath);

Console.WriteLine($"CPUs:         {context.Cpus.Count()}");
Console.WriteLine($"Motherboards: {context.Motherboards.Count()}");
Console.WriteLine($"RAM:          {context.Rams.Count()}");
Console.WriteLine($"GPUs:         {context.Gpus.Count()}");
Console.WriteLine($"PSUs:         {context.Psus.Count()}");
Console.WriteLine($"Cases:        {context.PcCases.Count()}");

// Examples

var cpu = context.Cpus.FirstOrDefault();
if (cpu != null)
{
    Console.WriteLine($"CPU: {cpu.Name}, Socket: {cpu.Socket}, TDP: {cpu.Tdp}, MemTypes: {string.Join(", ", cpu.SupportedMemoryTypes)}, MaxMem: {cpu.MaxMemoryGb}GB");
}

var mb = context.Motherboards.FirstOrDefault();
if (mb != null)
{
    Console.WriteLine($"MB: {mb.Name}, Socket: {mb.Socket}, FormFactor: {mb.FormFactor}, MemType: {mb.MemoryType}, Slots: {mb.MemorySlots}, MaxMem: {mb.MaxMemoryGb}GB");
}

var ram = context.Rams.FirstOrDefault();
if (ram != null)
{
    Console.WriteLine($"RAM: {ram.Name}, Type: {ram.RamType}, Capacity: {ram.CapacityGb}GB, Modules: {ram.ModuleCount}, Speed: {ram.Speed}");
}

var gpu = context.Gpus.FirstOrDefault();
if (gpu != null)
{
    Console.WriteLine($"GPU: {gpu.Name}, TDP: {gpu.Tdp}W, Length: {gpu.LengthMm}mm, 8-pin connectors: {gpu.RequiredPowerConnectors8Pin}");
}

var psu = context.Psus.FirstOrDefault();
if (psu != null)
{
    Console.WriteLine($"PSU: {psu.Name}, Wattage: {psu.Wattage}W, FormFactor: {psu.FormFactor}, Efficiency: {psu.EfficiencyRating}");
}

var pcCase = context.PcCases.FirstOrDefault();
if (pcCase != null)
{
    Console.WriteLine($"Case: {pcCase.Name}, FormFactor: {pcCase.FormFactor}, Supported MBs: {string.Join(", ", pcCase.SupportedMotherboardFormFactors)}, MaxGPU: {pcCase.MaxGpuLengthMm}mm");
}


// Socket match: CPU ↔ Motherboard
Console.WriteLine("=== CPU Sockets ===");
context.Cpus.Select(c => c.Socket).Distinct().OrderBy(s => s).ToList()
    .ForEach(Console.WriteLine);

Console.WriteLine("\n=== Motherboard Sockets ===");
context.Motherboards.Select(m => m.Socket).Distinct().OrderBy(s => s).ToList()
    .ForEach(Console.WriteLine);

// Memory type match: RAM ↔ Motherboard
Console.WriteLine("\n=== RAM Types ===");
context.Rams.Select(r => r.RamType).Distinct().OrderBy(s => s).ToList()
    .ForEach(Console.WriteLine);

Console.WriteLine("\n=== Motherboard Memory Types ===");
context.Motherboards.Select(m => m.MemoryType).Distinct().OrderBy(s => s).ToList()
    .ForEach(Console.WriteLine);

// Form factor match: Motherboard ↔ Case
Console.WriteLine("\n=== Motherboard Form Factors ===");
context.Motherboards.Select(m => m.FormFactor).Distinct().OrderBy(s => s).ToList()
    .ForEach(Console.WriteLine);

Console.WriteLine("\n=== Case Supported Motherboard Form Factors ===");
context.PcCases.ToList()
    .SelectMany(c => c.SupportedMotherboardFormFactors).Distinct().OrderBy(s => s)
    .ToList().ForEach(Console.WriteLine);

// GPU length vs Case max
Console.WriteLine("\n=== GPU LengthMm ===");
Console.WriteLine($"min={context.Gpus.Min(g => g.LengthMm)}  max={context.Gpus.Max(g => g.LengthMm)}");

Console.WriteLine("\n=== Case MaxGpuLengthMm ===");
Console.WriteLine($"min={context.PcCases.Min(c => c.MaxGpuLengthMm)}  max={context.PcCases.Max(c => c.MaxGpuLengthMm)}");

// Power: CPU TDP + GPU TDP vs PSU Wattage
Console.WriteLine("\n=== CPU TDP ===");
Console.WriteLine($"min={context.Cpus.Min(c => c.Tdp)}  max={context.Cpus.Max(c => c.Tdp)}");

Console.WriteLine("\n=== GPU TDP ===");
Console.WriteLine($"min={context.Gpus.Min(g => g.Tdp)}  max={context.Gpus.Max(g => g.Tdp)}");

Console.WriteLine("\n=== PSU Wattage ===");
Console.WriteLine($"min={context.Psus.Min(p => p.Wattage)}  max={context.Psus.Max(p => p.Wattage)}");

// Memory capacity vs Motherboard max
Console.WriteLine("\n=== RAM ModuleCount values ===");
context.Rams.Select(r => r.ModuleCount).Distinct().OrderBy(x => x).ToList()
    .ForEach(Console.WriteLine);

Console.WriteLine("\n=== RAM CapacityGb values ===");
context.Rams.Select(r => r.CapacityGb).Distinct().OrderBy(x => x).ToList()
    .ForEach(Console.WriteLine);

Console.WriteLine("\n=== Motherboard MemorySlots values ===");
context.Motherboards.Select(m => m.MemorySlots).Distinct().OrderBy(x => x).ToList()
    .ForEach(Console.WriteLine);

Console.WriteLine("\n=== Motherboard MaxMemoryGb values ===");
context.Motherboards.Select(m => m.MaxMemoryGb).Distinct().OrderBy(x => x).ToList()
    .ForEach(Console.WriteLine);