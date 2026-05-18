using Application.Rules;
using Domain.Entities;
using Domain.Models;
using Infrastructure.DTOs;

namespace UnitTests;

public class MemoryTypeCompatibilityRuleTests
{
    private readonly MemoryTypeCompatibilityRule _rule = new();

    [Theory]
    [InlineData(new[] { "DDR4" }, "DDR4", true)]
    [InlineData(new[] { "DDR4" }, "DDR5", false)]
    [InlineData(new[] { "DDR4", "DDR5" }, "DDR5", true)]
    [InlineData(new[] { "DDR4", "DDR5" }, "DDR3", false)]
    [InlineData(new string[] { }, "DDR4", false)]
    public void Check_MemoryTypeCompatibility(string[] cpuMemoryTypes, string mbMemoryType, bool expected)
    {
        var config = new PcConfiguration
        {
            Cpu = new Cpu { SupportedMemoryTypes = new List<string>(cpuMemoryTypes) },
            Motherboard = new Motherboard { MemoryType = mbMemoryType }
        };

        var result = _rule.Check(config);

        Assert.Equal(expected, result.IsCompatible);
    }

    [Fact]
    public void Check_WhenCpuIsNull_ReturnsCompatible()
    {
        var config = new PcConfiguration { Cpu = null, Motherboard = new Motherboard { MemoryType = "DDR4" } };
        Assert.True(_rule.Check(config).IsCompatible);
    }

    [Fact]
    public void Check_WhenMotherboardIsNull_ReturnsCompatible()
    {
        var config = new PcConfiguration { Cpu = new Cpu { SupportedMemoryTypes = ["DDR4"] }, Motherboard = null };
        Assert.True(_rule.Check(config).IsCompatible);
    }
}
