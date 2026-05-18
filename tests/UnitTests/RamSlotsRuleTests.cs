using Application.Rules;
using Domain.Entities;
using Domain.Models;

namespace UnitTests;

public class RamSlotsRuleTests
{
    private readonly RamSlotsRule _rule = new();

    [Theory]
    [InlineData(new[] { 1 }, 4, true)]
    [InlineData(new[] { 2, 2 }, 4, true)]
    [InlineData(new[] { 2, 2, 2 }, 4, false)]
    [InlineData(new int[] { }, 4, true)]
    [InlineData(new[] { 2 }, 0, false)]
    [InlineData(new int[] { }, 0, true)]
    public void Check_RamSlots(int[] moduleCount, int mbSlots, bool expected)
    {
        var config = new PcConfiguration
        {
            RamModules = moduleCount.Select(m => new Ram { ModuleCount = m }).ToList(),
            Motherboard = new Motherboard { MemorySlots = mbSlots }
        };

        var result = _rule.Check(config);

        Assert.Equal(expected, result.IsCompatible);
    }

    [Fact]
    public void Check_WhenMotherboardIsNull_ReturnsCompatible()
    {
        var config = new PcConfiguration
        {
            RamModules = [new Ram { ModuleCount = 2 }],
            Motherboard = null
        };

        Assert.True(_rule.Check(config).IsCompatible);
    }
}