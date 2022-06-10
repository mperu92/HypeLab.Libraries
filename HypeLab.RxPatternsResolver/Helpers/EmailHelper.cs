using HypeLab.RxPatternsResolver.Enums;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace HypeLab.RxPatternsResolver.Helpers
{
    internal static class EmailHelper
    {
        internal const string requestUrl = "https://dns.google.com/resolve?name={0}&type={1}";

        internal static string GetDomain(this string email)
        {
            return new MailAddress(email).Host;
        }

        internal static string RetrieveRequestUrlWithGivenDomain(string domain, DnsDomainType domainType = DnsDomainType.MX)
        {
            return string.Format(requestUrl, domain, domainType);
        }
    }
}
