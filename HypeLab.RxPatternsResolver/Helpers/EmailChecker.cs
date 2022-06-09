using HypeLab.RxPatternsResolver.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HypeLab.RxPatternsResolver.Helpers
{
    internal static class EmailChecker
    {
        internal static EmailCheckerStatus CheckDomain(string email)
        {
            return EmailCheckerStatus.INPUT_NULL_OR_EMPTY;
        }
    }
}
