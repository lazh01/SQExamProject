using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models;

public class CompatibilityResult
{
    public bool IsCompatible { get; set; }
    public List<string> Errors { get; set; } = new();

    public static CompatibilityResult Ok() =>
        new() { IsCompatible = true };

    public static CompatibilityResult Fail(string reason) =>
        new() { IsCompatible = false, Errors = [reason] };
}
