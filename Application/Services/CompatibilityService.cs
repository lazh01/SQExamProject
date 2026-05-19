using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class CompatibilityService : ICompatibilityService
{
    private readonly IEnumerable<ICompatibilityRule> _rules;

    public CompatibilityService(IEnumerable<ICompatibilityRule> rules)
    {
        _rules = rules;
    }

    public CompatibilityResult Validate(PcConfiguration config)
    {
        List<string> errors = new List<string>();
        foreach (var rule in _rules)
        {
            var result = rule.Check(config);
            
            if (!result.IsCompatible)
            {
                errors.AddRange(result.Errors);
            }
        }

        if (errors.Count > 0)
        {
            return CompatibilityResult.Fail(errors);
        }

        return CompatibilityResult.Ok();
    }
}