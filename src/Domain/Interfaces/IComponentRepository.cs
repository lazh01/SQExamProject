using Domain.Entities;

namespace Domain.Interfaces;

public interface IComponentRepository
{
    IEnumerable<Cpu> GetAllCpus();
    IEnumerable<Motherboard> GetAllMotherboards();
    IEnumerable<Ram> GetAllRam();
    IEnumerable<Gpu> GetAllGpus();
    IEnumerable<Psu> GetAllPsus();
    IEnumerable<PcCase> GetAllCases();
}