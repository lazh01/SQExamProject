using Domain.Models;

namespace Domain.Interfaces;

public interface ICompatibilityService
{
    CompatibilityResult Validate(PcConfiguration config);
}
