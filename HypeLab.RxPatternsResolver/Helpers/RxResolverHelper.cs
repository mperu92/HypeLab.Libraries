using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace HypeLab.RxPatternsResolver.Helpers
{
    internal static class RxResolverHelper
    {
        internal static string DomainMapper(Match match)
        {
            // Use IdnMapping class to convert Unicode domain names.
            IdnMapping idn = new IdnMapping();

            // Pull out and process domain name (throws ArgumentException on invalid)
            string domainName = idn.GetAscii(match.Groups[2].Value);

            return match.Groups[1].Value + domainName;
        }
    }
}
