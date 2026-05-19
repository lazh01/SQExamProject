using Application.Rules;
using Domain.Entities;
using Domain.Models;

namespace UnitTests;

public class GpuLengthRuleTests
{
    private readonly GpuLengthRule _rule = new();

    [Theory]
    [InlineData(280, 360, true)]
    [InlineData(360, 360, true)]
    [InlineData(361, 360, false)]
    [InlineData(280, 0, false)]
    [InlineData(0, 0, false)]
    public void Check_GpuLength(double gpuLength, double caseMaxLength, bool expected)
    {
        var config = new PcConfiguration
        {
            Gpu = new Gpu { LengthMm = gpuLength },
            Case = new PcCase { MaxGpuLengthMm = caseMaxLength }
        };

        var result = _rule.Check(config);

        Assert.Equal(expected, result.IsCompatible);
    }

    [Fact]
    public void Check_WhenGpuIsNull_ReturnsCompatible()
    {
        var config = new PcConfiguration
        {
            Gpu = null,
            Case = new PcCase { MaxGpuLengthMm = 360 }
        };

        Assert.True(_rule.Check(config).IsCompatible);
    }

    [Fact]
    public void Check_WhenCaseIsNull_ReturnsCompatible()
    {
        var config = new PcConfiguration
        {
            Gpu = new Gpu { LengthMm = 280 },
            Case = null
        };

        Assert.True(_rule.Check(config).IsCompatible);
    }
}