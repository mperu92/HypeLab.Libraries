using HypeLab.RxPatternsResolver.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HypeLab.RxPatternsResolver.Interfaces
{
    public interface IEmailValidator
    {
        Task<EmailCheckerResponse> IsValidEmail(string email);
    }
}
