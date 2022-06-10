using HypeLab.RxPatternsResolver.Enums;
using HypeLab.RxPatternsResolver.Interfaces;
using HypeLab.RxPatternsResolver.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        public async Task<EmailCheckerStatus> IsDomainValidAsync(string checkUrl)
        {
            try
            {
                using HttpClient client = new HttpClient();
                string content = await client.GetStringAsync(checkUrl);
                if (!string.IsNullOrEmpty(content))
                    return EmailCheckerStatus.DOMAIN_NOT_VALID;

                return EmailCheckerStatus.DOMAIN_VALID;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
