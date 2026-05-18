using System;
using System.Collections.Generic;
using System.Text;
using Domain.Interfaces;
using Domain.Models;
namespace Application.Rules
{
    public class RamMemoryTypeCompatibilityRule : ICompatibilityRule
    {
        public CompatibilityResult Check(PcConfiguration config)
        {
            if (config == null)
                return CompatibilityResult.Fail("Configuration cannot be null.");
            if (config.Motherboard == null)
                return CompatibilityResult.Ok();
            if (config.RamModules == null || config.RamModules.Count == 0)
                return CompatibilityResult.Ok();

            foreach (var ram in config.RamModules)
            {
                if (!config.Motherboard.MemoryType.Equals(ram.RamType))
                    return CompatibilityResult.Fail("Incompatible RAM memory types.");
            }

            return CompatibilityResult.Ok();
        }
    }
}
