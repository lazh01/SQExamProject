using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class CompatibilityQueryService : ICompatibilityQueryService
{
    private readonly ICompatibilityService _compatibilityService;
    private readonly IComponentRepository _repository;

    public CompatibilityQueryService(
        ICompatibilityService compatibilityService,
        IComponentRepository repository)
    {
        _compatibilityService = compatibilityService;
        _repository = repository;
    }

    public IEnumerable<Gpu> FindCompatibleGpus(PcConfiguration config) =>
        _repository.GetAllGpus()
            .Where(gpu => _compatibilityService.Validate(config with { Gpu = gpu }).IsCompatible);

    public IEnumerable<Cpu> FindCompatibleCpus(PcConfiguration config) =>
        _repository.GetAllCpus()
            .Where(cpu => _compatibilityService.Validate(config with { Cpu = cpu }).IsCompatible);

    public IEnumerable<Motherboard> FindCompatibleMotherboards(PcConfiguration config) =>
        _repository.GetAllMotherboards()
            .Where(mb => _compatibilityService.Validate(config with { Motherboard = mb }).IsCompatible);

    public IEnumerable<Ram> FindCompatibleRam(PcConfiguration config) =>
        _repository.GetAllRam()
            .Where(ram => _compatibilityService.Validate(
                config with { RamModules = [.. config.RamModules, ram] }).IsCompatible);

    public IEnumerable<Psu> FindCompatiblePsus(PcConfiguration config) =>
        _repository.GetAllPsus()
            .Where(psu => _compatibilityService.Validate(config with { Psu = psu }).IsCompatible);

    public IEnumerable<PcCase> FindCompatibleCases(PcConfiguration config) =>
        _repository.GetAllCases()
            .Where(pcCase => _compatibilityService.Validate(config with { Case = pcCase }).IsCompatible);
}