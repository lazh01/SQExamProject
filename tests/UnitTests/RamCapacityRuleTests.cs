using Application.Rules;
using Domain.Entities;
using Domain.Models;

namespace UnitTests;

public class RamCapacityRuleTests
{
    private readonly RamCapacityRule _rule = new();

    [Theory]
    [InlineData(new[] { 64 }, 128, true)]
    [InlineData(new[] { 64, 64 }, 128, true)]
    [InlineData(new[] { 64, 64, 64 }, 128, false)]
    [InlineData(new int[] { }, 128, true)]
    [InlineData(new[] { 64 }, 0, false)]
    [InlineData(new int[] { }, 0, true)]
    public void Check_RamCapacity(int[] ramCapacities, int mbMaxMemory, bool expected)
    {
        var config = new PcConfiguration
        {
            RamModules = ramCapacities.Select(c => new Ram { CapacityGb = c }).ToList(),
            Motherboard = new Motherboard { MaxMemoryGb = mbMaxMemory }
        };

        var result = _rule.Check(config);

        Assert.Equal(expected, result.IsCompatible);
    }

    [Fact]
    public void Check_WhenMotherboardIsNull_ReturnsCompatible()
    {
        var config = new PcConfiguration
        {
            RamModules = [new Ram { CapacityGb = 64 }],
            Motherboard = null
        };

        Assert.True(_rule.Check(config).IsCompatible);
    }
}