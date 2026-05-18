using System;
using System.Collections.Generic;
using System.Text;
using Domain.Interfaces;
using Domain.Models;
namespace Application.Rules
{
    public class SocketCompatibilityRule : ICompatibilityRule
    {
        public CompatibilityResult Check(PcConfiguration config)
        {
            if (config == null)
            {
                return CompatibilityResult.Fail("Configuration cannot be null.");
            }
            if (config.Cpu == null || config.Motherboard == null)
            {
                return CompatibilityResult.Ok();
            }

            if (!config.Cpu.Socket.Equals(config.Motherboard.Socket))
            {
                return CompatibilityResult.Fail("CPU and Motherboard sockets are not compatible.");
            }

            return CompatibilityResult.Ok();

            throw new NotImplementedException("Socket compatibility check is not implemented yet.");
        }
    }
}
