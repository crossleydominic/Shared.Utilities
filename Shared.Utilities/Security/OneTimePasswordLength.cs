using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.Security
{
    /// <summary>
    /// An enum defining a set of supported lengths for generating HOTP codes.
    /// </summary>
    public enum OneTimePasswordLength
    {
        SixDigits = 6,
        SevenDigits = 7,
        EightDigits = 8,
    }
}
