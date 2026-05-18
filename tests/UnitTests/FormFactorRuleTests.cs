using Application.Rules;
using Domain.Entities;
using Domain.Models;

namespace UnitTests;

public class FormFactorRuleTests
{
    private readonly FormFactorRule _rule = new();

    [Theory]
    [InlineData("ATX", new[] { "ATX", "Micro ATX", "Mini-ITX" }, true)]
    [InlineData("EATX", new[] { "ATX", "Micro ATX", "Mini-ITX" }, false)]
    [InlineData("ATX", new string[] { }, false)]
    public void Check_FormFactorCompatibility(string mbFormFactor, string[] caseSupportedFormFactors, bool expected)
    {
        var config = new PcConfiguration
        {
            Motherboard = new Motherboard { FormFactor = mbFormFactor },
            Case = new PcCase { SupportedMotherboardFormFactors = new List<string>(caseSupportedFormFactors) }
        };

        var result = _rule.Check(config);

        Assert.Equal(expected, result.IsCompatible);
    }

    [Fact]
    public void Check_WhenMotherboardIsNull_ReturnsCompatible()
    {
        var config = new PcConfiguration
        {
            Motherboard = null,
            Case = new PcCase { SupportedMotherboardFormFactors = ["ATX"] }
        };

        Assert.True(_rule.Check(config).IsCompatible);
    }

    [Fact]
    public void Check_WhenCaseIsNull_ReturnsCompatible()
    {
        var config = new PcConfiguration
        {
            Motherboard = new Motherboard { FormFactor = "ATX" },
            Case = null
        };

        Assert.True(_rule.Check(config).IsCompatible);
    }
}