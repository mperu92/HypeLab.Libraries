using HypeLab.RxPatternsResolver.Enums;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace HypeLab.RxPatternsResolver.Helpers
{
    internal static partial class EmailHelper
    {
        internal static string GetDomain(this string email)
        {
            return new MailAddress(email).Host;
        }

        internal static string RetrieveRequestUrlWIthGivenDomain(string domain, DnsDomainType domainType = DnsDomainType.MX)
        {
            return $"https://dns.google.com/resolve?name={domain}&type={domainType}";
        }
    }
}
