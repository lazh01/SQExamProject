using Application.Rules;
using Domain.Entities;
using Domain.Models;

namespace UnitTests;

public class PowerRuleTests
{
    private readonly PowerRule _rule = new();

    [Theory]
    [InlineData(65, 150, 259, true)]   // 1. Alle til stede, over grænsen
    [InlineData(65, 150, 258, true)]   // 2. Alle til stede, præcis på grænsen
    [InlineData(65, 150, 257, false)]  // 3. Alle til stede, under grænsen
    [InlineData(65, 150, 0, false)]    // 13. PSU wattage = 0
    [InlineData(0, 150, 300, false)]   // 14. CPU TDP = 0, ugyldig
    [InlineData(65, 0, 300, false)]    // 15. GPU TDP = 0, ugyldig
    public void Check_PowerCompatibility(int cpuTdp, int gpuTdp, int psuWattage, bool expected)
    {
        var config = new PcConfiguration
        {
            Cpu = new Cpu { Tdp = cpuTdp },
            Gpu = new Gpu { Tdp = gpuTdp },
            Psu = new Psu { Wattage = psuWattage }
        };

        var result = _rule.Check(config);

        Assert.Equal(expected, result.IsCompatible);
    }

    [Theory]
    [InlineData(150, 181, true)]   // 4. CPU null, over grænsen
    [InlineData(150, 180, true)]   // 5. CPU null, præcis på grænsen
    [InlineData(150, 179, false)]  // 6. CPU null, under grænsen
    public void Check_WhenCpuIsNull(int gpuTdp, int psuWattage, bool expected)
    {
        var config = new PcConfiguration
        {
            Cpu = null,
            Gpu = new Gpu { Tdp = gpuTdp },
            Psu = new Psu { Wattage = psuWattage }
        };

        var result = _rule.Check(config);

        Assert.Equal(expected, result.IsCompatible);
    }

    [Theory]
    [InlineData(65, 79, true)]   // 7. GPU null, over grænsen
    [InlineData(65, 78, true)]   // 8. GPU null, præcis på grænsen
    [InlineData(65, 77, false)]  // 9. GPU null, under grænsen
    public void Check_WhenGpuIsNull(int cpuTdp, int psuWattage, bool expected)
    {
        var config = new PcConfiguration
        {
            Cpu = new Cpu { Tdp = cpuTdp },
            Gpu = null,
            Psu = new Psu { Wattage = psuWattage }
        };

        var result = _rule.Check(config);

        Assert.Equal(expected, result.IsCompatible);
    }

    [Theory]
    [InlineData(1, true)]    // 10. Begge null, PSU over 0
    [InlineData(0, false)]   // 11. Begge null, PSU = 0
    public void Check_WhenBothCpuAndGpuAreNull(int psuWattage, bool expected)
    {
        var config = new PcConfiguration
        {
            Cpu = null,
            Gpu = null,
            Psu = new Psu { Wattage = psuWattage }
        };

        var result = _rule.Check(config);

        Assert.Equal(expected, result.IsCompatible);
    }

    [Fact]
    public void Check_WhenPsuIsNull_ReturnsCompatible()
    {
        var config = new PcConfiguration
        {
            Cpu = new Cpu { Tdp = 65 },
            Gpu = new Gpu { Tdp = 150 },
            Psu = null
        };

        Assert.True(_rule.Check(config).IsCompatible);
    }
}