using Domain.Interfaces;
using Domain.Models;

namespace Application.Rules;

public class GpuLengthRule : ICompatibilityRule
{
    public CompatibilityResult Check(PcConfiguration config)
    {
        if (config == null)
            return CompatibilityResult.Fail("Configuration cannot be null.");
        if (config.Gpu == null)
            return CompatibilityResult.Ok();
        if (config.Case == null)
            return CompatibilityResult.Ok();
        
        if (config.Case.MaxGpuLengthMm <= 0)
            return CompatibilityResult.Fail("Case maximum GPU length is 0 or less."); // No limit specified, assume compatible

        if (config.Gpu.LengthMm > config.Case.MaxGpuLengthMm)
            return CompatibilityResult.Fail($"GPU length ({config.Gpu.LengthMm}mm) exceeds case maximum ({config.Case.MaxGpuLengthMm}mm).");

        return CompatibilityResult.Ok();
    }
}