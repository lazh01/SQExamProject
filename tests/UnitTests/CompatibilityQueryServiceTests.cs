using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace UnitTests;

public class CompatibilityQueryServiceTests
{
    private readonly Mock<ICompatibilityService> _compatibilityService = new();
    private readonly Mock<IComponentRepository> _repository = new();
    private readonly CompatibilityQueryService _service;

    public CompatibilityQueryServiceTests()
    {
        _service = new CompatibilityQueryService(
            _compatibilityService.Object,
            _repository.Object);
    }

    [Fact]
    public void FindCompatibleGpus_WhenAllCompatible_ReturnsAll()
    {
        var gpu1 = new Gpu { Name = "RTX 3060" };
        var gpu2 = new Gpu { Name = "RTX 3070" };
        var config = new PcConfiguration();

        _repository.Setup(r => r.GetAllGpus()).Returns([gpu1, gpu2]);
        _compatibilityService
            .Setup(s => s.Validate(It.IsAny<PcConfiguration>()))
            .Returns(CompatibilityResult.Ok());

        var result = _service.FindCompatibleGpus(config).ToList();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void FindCompatibleGpus_WhenSomeCompatible_ReturnsOnlyCompatible()
    {
        var gpu1 = new Gpu { Name = "RTX 3060" };
        var gpu2 = new Gpu { Name = "RTX 4090" };
        var config = new PcConfiguration();

        _repository.Setup(r => r.GetAllGpus()).Returns([gpu1, gpu2]);
        _compatibilityService
            .Setup(s => s.Validate(It.Is<PcConfiguration>(c => c.Gpu == gpu1)))
            .Returns(CompatibilityResult.Ok());
        _compatibilityService
            .Setup(s => s.Validate(It.Is<PcConfiguration>(c => c.Gpu == gpu2)))
            .Returns(CompatibilityResult.Fail("GPU for lang"));

        var result = _service.FindCompatibleGpus(config).ToList();

        Assert.Single(result);
        Assert.Equal("RTX 3060", result[0].Name);
    }

    [Fact]
    public void FindCompatibleGpus_WhenNoneCompatible_ReturnsEmpty()
    {
        var gpu1 = new Gpu { Name = "RTX 4090" };
        var config = new PcConfiguration();

        _repository.Setup(r => r.GetAllGpus()).Returns([gpu1]);
        _compatibilityService
            .Setup(s => s.Validate(It.IsAny<PcConfiguration>()))
            .Returns(CompatibilityResult.Fail("Ingen kompatible"));

        var result = _service.FindCompatibleGpus(config).ToList();

        Assert.Empty(result);
    }

    [Fact]
    public void FindCompatibleGpus_WhenRepositoryIsEmpty_ReturnsEmpty()
    {
        _repository.Setup(r => r.GetAllGpus()).Returns([]);

        var result = _service.FindCompatibleGpus(new PcConfiguration()).ToList();

        Assert.Empty(result);
    }

    [Fact]
    public void FindCompatibleGpus_WhenConfigAlreadyHasGpu_ReplacesWithCandidates()
    {
        var existingGpu = new Gpu { Name = "GTX 1080" };
        var gpu1 = new Gpu { Name = "RTX 3060" };
        var gpu2 = new Gpu { Name = "RTX 4090" };
        var config = new PcConfiguration { Gpu = existingGpu };

        _repository.Setup(r => r.GetAllGpus()).Returns([gpu1, gpu2]);
        _compatibilityService
            .Setup(s => s.Validate(It.Is<PcConfiguration>(c => c.Gpu == gpu1)))
            .Returns(CompatibilityResult.Ok());
        _compatibilityService
            .Setup(s => s.Validate(It.Is<PcConfiguration>(c => c.Gpu == gpu2)))
            .Returns(CompatibilityResult.Fail("GPU for lang"));

        var result = _service.FindCompatibleGpus(config).ToList();

        Assert.Single(result);
        Assert.Equal("RTX 3060", result[0].Name);
    }
}