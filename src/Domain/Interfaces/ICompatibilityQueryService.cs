using Domain.Entities;
using Domain.Models;

namespace Domain.Interfaces;

public interface ICompatibilityQueryService
{
    IEnumerable<Gpu> FindCompatibleGpus(PcConfiguration config);
    IEnumerable<Cpu> FindCompatibleCpus(PcConfiguration config);
    IEnumerable<Motherboard> FindCompatibleMotherboards(PcConfiguration config);
    IEnumerable<Ram> FindCompatibleRam(PcConfiguration config);
    IEnumerable<Psu> FindCompatiblePsus(PcConfiguration config);
    IEnumerable<PcCase> FindCompatibleCases(PcConfiguration config);
}