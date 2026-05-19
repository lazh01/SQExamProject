using Domain.Interfaces;
using Domain.Models;

namespace Application.Rules;

public class PowerRule : ICompatibilityRule
{
    public CompatibilityResult Check(PcConfiguration config)
    {
        if (config == null) {
            return CompatibilityResult.Fail("Configuration cannot be null.");
        }
        
        if (config.Psu == null)
        {
            return CompatibilityResult.Ok();
        }

        if (config.Psu.Wattage <= 0)
        {
            return CompatibilityResult.Fail("PSU wattage must be greater than zero.");
        }

        if (config.Cpu == null && config.Gpu == null)
        {
            return CompatibilityResult.Ok();
        }

        int totalPower = 0;
        if (config.Cpu != null)
        {
            if (config.Cpu.Tdp <= 0)
            {
                return CompatibilityResult.Fail("CPU TDP must be greater than zero.");
            }
            totalPower += config.Cpu.Tdp;
        }
        if (config.Gpu != null)
        {
            if (config.Gpu.Tdp <= 0)
            {
                return CompatibilityResult.Fail("GPU TDP must be greater than zero.");
            }
            totalPower += config.Gpu.Tdp;
        }

        double buffered_power_demand = totalPower * 1.2; // Add 20% buffer for other components and future upgrades

        if (buffered_power_demand > config.Psu.Wattage)
        {
            return CompatibilityResult.Fail("Buffered power consumption exceeds PSU capacity.");
        }

        return CompatibilityResult.Ok();
    }
}