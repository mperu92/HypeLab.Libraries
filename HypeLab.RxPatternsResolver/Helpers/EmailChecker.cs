using HypeLab.RxPatternsResolver.Interfaces;
using HypeLab.RxPatternsResolver.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HypeLab.RxPatternsResolver.Helpers
{
    internal class EmailChecker : IEmailValidityChecker, IEmailDomainChecker, IEmailExistanceChecker
    {
        public bool EmailExists()
        {
            throw new NotImplementedException();
        }

        public bool IsValidEmailAddress(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }

        public EmailCheckerStatus IsDomainValid(string domain)
        {
            throw new NotImplementedException();
        }
    }
}
