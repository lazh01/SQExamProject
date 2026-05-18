using Domain.Interfaces;
using Domain.Models;

namespace Application.Rules;

public class MemoryTypeCompatibilityRule : ICompatibilityRule
{
    public CompatibilityResult Check(PcConfiguration config)
    {
        if (config == null)
            return CompatibilityResult.Fail("Configuration cannot be null.");
        if (config.Cpu == null || config.Motherboard == null)
            return CompatibilityResult.Ok();
        if (config.Cpu.SupportedMemoryTypes.Contains(config.Motherboard.MemoryType))
            return CompatibilityResult.Ok();
        return CompatibilityResult.Fail("Incompatible memory types.");
    }
}
