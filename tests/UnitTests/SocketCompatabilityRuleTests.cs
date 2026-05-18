using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Rules;

namespace UnitTests;
public class SocketCompatibilityRuleTests
{
    private readonly SocketCompatibilityRule _rule = new();

    [Theory]
    [InlineData("AM4", "AM4", true)]
    [InlineData("LGA1700", "AM4", false)]
    public void Check_SocketCompatibility(string cpuSocket, string mbSocket, bool expected)
    {
        var config = new PcConfiguration
        {
            Cpu = new Cpu { Socket = cpuSocket },
            Motherboard = new Motherboard { Socket = mbSocket }
        };

        var result = _rule.Check(config);

        Assert.Equal(expected, result.IsCompatible);
    }

    [Fact]
    public void Check_WhenCpuIsNull_ReturnsCompatible()
    {
        var config = new PcConfiguration
        {
            Cpu = null,
            Motherboard = new Motherboard { Socket = "AM4" }
        };

        var result = _rule.Check(config);

        Assert.True(result.IsCompatible);
    }

    [Fact]
    public void Check_WhenMotherboardIsNull_ReturnsCompatible()
    {
        var config = new PcConfiguration
        {
            Cpu = new Cpu { Socket = "AM4" },
            Motherboard = null
        };

        var result = _rule.Check(config);

        Assert.True(result.IsCompatible);
    }

    [Fact]
    public void Check_WhenBothAreNull_ReturnsCompatible()
    {
        var config = new PcConfiguration
        {
            Cpu = null,
            Motherboard = null
        };

        var result = _rule.Check(config);

        Assert.True(result.IsCompatible);
    }
}
