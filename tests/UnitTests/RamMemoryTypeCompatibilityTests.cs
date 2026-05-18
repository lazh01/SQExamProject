using Application.Rules;
using Domain.Entities;
using Domain.Models;

namespace UnitTests;

public class RamMemoryTypeCompatibilityRuleTests
{
    private readonly RamMemoryTypeCompatibilityRule _rule = new();

    [Theory]
    [InlineData(new[] { "DDR4" }, "DDR4", true)]
    [InlineData(new[] { "DDR4" }, "DDR5", false)]
    [InlineData(new[] { "DDR4", "DDR4" }, "DDR4", true)]
    [InlineData(new[] { "DDR4", "DDR5" }, "DDR4", false)]
    [InlineData(new string[] { }, "DDR4", true)]
    public void Check_RamMemoryTypeCompatibility(string[] ramTypes, string mbMemoryType, bool expected)
    {
        var config = new PcConfiguration
        {
            RamModules = ramTypes.Select(t => new Ram { RamType = t }).ToList(),
            Motherboard = new Motherboard { MemoryType = mbMemoryType }
        };

        var result = _rule.Check(config);

        Assert.Equal(expected, result.IsCompatible);
    }

    [Fact]
    public void Check_WhenMotherboardIsNull_ReturnsCompatible()
    {
        var config = new PcConfiguration
        {
            RamModules = [new Ram { RamType = "DDR4" }],
            Motherboard = null
        };

        Assert.True(_rule.Check(config).IsCompatible);
    }

    [Fact]
    public void Check_WhenRamModulesIsNull_ReturnsCompatible()
    {
        var config = new PcConfiguration
        {
            RamModules = null,
            Motherboard = new Motherboard { MemoryType = "DDR4" }
        };

        Assert.True(_rule.Check(config).IsCompatible);
    }
}