using Application.Services;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace UnitTests;

public class CompatibilityServiceTests
{
    [Fact]
    public void Validate_WhenAllRulesPass_ReturnsCompatible()
    {
        var rule1 = new Mock<ICompatibilityRule>();
        var rule2 = new Mock<ICompatibilityRule>();
        rule1.Setup(r => r.Check(It.IsAny<PcConfiguration>())).Returns(CompatibilityResult.Ok());
        rule2.Setup(r => r.Check(It.IsAny<PcConfiguration>())).Returns(CompatibilityResult.Ok());

        var service = new CompatibilityService([rule1.Object, rule2.Object]);
        var result = service.Validate(new PcConfiguration());

        Assert.True(result.IsCompatible);
    }

    [Fact]
    public void Validate_WhenOneRuleFails_ReturnsIncompatible()
    {
        var rule1 = new Mock<ICompatibilityRule>();
        var rule2 = new Mock<ICompatibilityRule>();
        rule1.Setup(r => r.Check(It.IsAny<PcConfiguration>())).Returns(CompatibilityResult.Ok());
        rule2.Setup(r => r.Check(It.IsAny<PcConfiguration>())).Returns(CompatibilityResult.Fail("fejl"));

        var service = new CompatibilityService([rule1.Object, rule2.Object]);
        var result = service.Validate(new PcConfiguration());

        Assert.False(result.IsCompatible);
        Assert.Single(result.Errors);
    }

    [Fact]
    public void Validate_WhenMultipleRulesFail_ReturnsAllErrors()
    {
        var rule1 = new Mock<ICompatibilityRule>();
        var rule2 = new Mock<ICompatibilityRule>();
        rule1.Setup(r => r.Check(It.IsAny<PcConfiguration>())).Returns(CompatibilityResult.Fail("fejl 1"));
        rule2.Setup(r => r.Check(It.IsAny<PcConfiguration>())).Returns(CompatibilityResult.Fail("fejl 2"));

        var service = new CompatibilityService([rule1.Object, rule2.Object]);
        var result = service.Validate(new PcConfiguration());

        Assert.False(result.IsCompatible);
        Assert.Equal(2, result.Errors.Count);
    }

    [Fact]
    public void Validate_WhenNoRules_ReturnsCompatible()
    {
        var service = new CompatibilityService([]);
        var result = service.Validate(new PcConfiguration());

        Assert.True(result.IsCompatible);
    }
}