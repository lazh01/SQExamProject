using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PcCompatibility.Infrastructure.Data;
using PcCompatibility.Infrastructure.Seeding;

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
