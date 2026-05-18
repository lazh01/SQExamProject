using Domain.Interfaces;
using Domain.Models;

namespace Application.Rules;

public class FormFactorRule : ICompatibilityRule
{
    public CompatibilityResult Check(PcConfiguration config)
    {
        if (config == null)
            return CompatibilityResult.Fail("Configuration cannot be null.");
        if (config.Motherboard == null)
            return CompatibilityResult.Ok();
        if (config.Case == null)
            return CompatibilityResult.Ok();
        if (config.Case.SupportedMotherboardFormFactors.Contains(config.Motherboard.FormFactor))
        {
            return CompatibilityResult.Ok();
        }
        else
        {
            return CompatibilityResult.Fail($"Case does not support the motherboard form factor {config.Motherboard.FormFactor}.");
        }
        throw new NotImplementedException();
    }
}