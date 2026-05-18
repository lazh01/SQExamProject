using Domain.Interfaces;
using Domain.Models;

namespace Application.Rules;

public class RamCapacityRule : ICompatibilityRule
{
    public CompatibilityResult Check(PcConfiguration config)
    {
        if (config == null)
        {
            return CompatibilityResult.Fail("Configuration cannot be null.");
        }
        if (config.RamModules == null || !config.RamModules.Any())
            return CompatibilityResult.Ok();
        if (config.Motherboard == null)
            return CompatibilityResult.Ok();

        if (config.Motherboard.MaxMemoryGb < config.RamModules.Sum(r => r.CapacityGb))
        {
            return CompatibilityResult.Fail($"Total RAM capacity exceeds motherboard limit of {config.Motherboard.MaxMemoryGb} GB.");
        } else
        {
            return CompatibilityResult.Ok();
        }
        throw new NotImplementedException();
    }
}