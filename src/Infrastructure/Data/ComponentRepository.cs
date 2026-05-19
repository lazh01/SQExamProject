using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data;

public class ComponentRepository : IComponentRepository
{
    private readonly AppDbContext _context;

    public ComponentRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Cpu> GetAllCpus() => _context.Cpus.ToList();
    public IEnumerable<Motherboard> GetAllMotherboards() => _context.Motherboards.ToList();
    public IEnumerable<Ram> GetAllRam() => _context.Rams.ToList();
    public IEnumerable<Gpu> GetAllGpus() => _context.Gpus.ToList();
    public IEnumerable<Psu> GetAllPsus() => _context.Psus.ToList();
    public IEnumerable<PcCase> GetAllCases() => _context.PcCases.ToList();
}