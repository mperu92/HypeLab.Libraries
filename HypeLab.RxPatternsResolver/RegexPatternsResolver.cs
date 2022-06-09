using HypeLab.RxPatternsResolver.Helpers;
using HypeLab.RxPatternsResolver.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace HypeLab.RxPatternsResolver
{
	/// <summary>
	/// Class capable of solve collections of regex patterns given an input string. Also equipped with a default patterns set.
	/// </summary>
	public class RegexPatternsResolver
	{
		private Stack<RegexPatternInstance>? _patterns;

		private readonly RegexOptions RegexOption = RegexOptions.None;

		/// <summary>
		/// Class initialization without parameters. Must add patterns before resolve a string.
		/// </summary>
		public RegexPatternsResolver() { }

		/// <summary>
		/// Class initialization with essential parameters.
		/// </summary>
		public RegexPatternsResolver(string pattern, string replacement)
		{
			AddPattern(pattern, replacement);
		}

		/// <summary>
		/// Class initialization with all parameters.
		/// </summary>
		public RegexPatternsResolver(string pattern, string replacement, RegexOptions regexOption)
		{
			RegexOption = regexOption;
			AddPattern(pattern, replacement);
		}

		/// <summary>
		/// Adds a new Regex pattern into patterns collection.
		/// Throws exception if pattern is null.
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		public void AddPattern(string pattern, string replacement, RegexOptions? regexOption = null)
		{
			if (string.IsNullOrWhiteSpace(pattern))
				throw new ArgumentException("Input string cannot be null or empty", nameof(pattern));

			if (_patterns == null)
				_patterns = new Stack<RegexPatternInstance>();

			_patterns!.Push(new RegexPatternInstance()
			{
				Pattern = pattern, Replacement = replacement, RegexOption = regexOption ?? RegexOption
			});
		}

		/// <summary>
		/// Returns input string replaced using patterns previously added.
		/// </summary>
		/// <returns>
		/// Throws exception if patterns collection is null.
		/// Returns just input string if patterns collection is empty.
		/// Otherwise returns the elaborated string.
		/// </returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="Exception"></exception>
		public string ResolveStringWithPatterns(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
				throw new ArgumentException("string to replace cannot be null or empty", nameof(input));

			if (_patterns == null)
				throw new Exception("Patterns collection is null. Do you have added some patterns before resolve?");

			string resolvedString = input;
			try
			{
				if (_patterns!.Count > 0)
				{
					foreach (RegexPatternInstance pattern in _patterns!)
					{
						Regex codeTitleRegex = new Regex (pattern.Pattern, pattern.RegexOption);
						resolvedString = codeTitleRegex.Replace(resolvedString, pattern.Replacement ?? string.Empty);
					}
				}

				return resolvedString;
			}
			catch (ArgumentException argumentException)
			{
				throw new ArgumentException($"[Param: {argumentException.ParamName} - Source: {argumentException.Source}] - {argumentException.Message}", argumentException.InnerException);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex.InnerException);
			}
		}

		/// <summary>
		/// Determines whether the email format is valid for an email address,
		/// check if domain is valid,
		/// check if email address is exists.
		/// ref: https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
		/// </summary>
		/// <param name="email"></param>
		/// <exception cref="RegexMatchTimeoutException"></exception>
		/// <exception cref="Exception"></exception>
		public EmailCheckerResponse IsValidEmail(string email)
        {
			if (string.IsNullOrWhiteSpace(email))
                return new EmailCheckerResponse("input string is null or empty", EmailCheckerStatus.INPUT_NULL_OR_EMPTY);

            try
			{
				EmailChecker emailChecker = new EmailChecker();

				// Normalize the domain
                if (emailChecker.IsValidEmailAddress(email.NormalizeEmailDomain()))
                {
					EmailCheckerStatus checkDomain = emailChecker.IsDomainValid(email);

					if (checkDomain == EmailCheckerStatus.DOMAIN_NOT_VALID)
						return new EmailCheckerResponse($"domain \"{email.GetDomain()}\" is not valid.", checkDomain);

					if (!emailChecker.EmailExists())
						return new EmailCheckerResponse($"\"{email}\" doesn't exists");

					return new EmailCheckerResponse("email is valid");
                }
                else
                {
                    return new EmailCheckerResponse("email address is not valid", EmailCheckerStatus.EMAIL_NOT_VALID);
                }
			}
			catch (RegexMatchTimeoutException e)
			{
				throw new RegexMatchTimeoutException(e.Message, e.InnerException);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message, e.InnerException);
			}
        }
    }
}
