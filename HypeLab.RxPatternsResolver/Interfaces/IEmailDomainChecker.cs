using HypeLab.RxPatternsResolver.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HypeLab.RxPatternsResolver.Interfaces
{
    internal interface IEmailDomainChecker
    {
        EmailCheckerStatus IsDomainValid(string domain);
    }
}
