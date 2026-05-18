using Domain.Interfaces;
using Domain.Models;

namespace Application.Rules;

public class RamSlotsRule : ICompatibilityRule
{
    public CompatibilityResult Check(PcConfiguration config)
    {
        if (config == null)
        {
            return CompatibilityResult.Fail("Configuration cannot be null.");
        }
        if (config.Motherboard == null)
            return CompatibilityResult.Ok();
        if (config.RamModules == null || config.RamModules.Count == 0)
            return CompatibilityResult.Ok();
        if (config.Motherboard.MemorySlots < config.RamModules.Sum(r => r.ModuleCount))
        {
            return CompatibilityResult.Fail($"Total RAM slots required exceeds motherboard limit of {config.Motherboard.MemorySlots}.");
        }

        return CompatibilityResult.Ok();
    }
}