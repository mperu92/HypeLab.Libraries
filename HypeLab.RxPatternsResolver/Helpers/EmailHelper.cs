using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace HypeLab.RxPatternsResolver.Helpers
{
    internal static class EmailHelper
    {
        internal static string GetDomain(this string email)
        {
            return new MailAddress(email).Host;
        }
    }
}
