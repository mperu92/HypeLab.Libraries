using HypeLab.RxPatternsResolver.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HypeLab.RxPatternsResolver
{
	/// <summary>
	/// Class capable of solve collections of regex patterns given an input string. Also equipped with a default patterns set.
	/// </summary>
	public class RegexPatternsResolver
	{
		private IDictionary<int, RegexPatternInstance>? _patterns;
		private int _index;

		private readonly RegexOptions RegexOption = RegexOptions.None;

		/// <summary>
		/// Class initialization without parameters. Must add patterns before resolve a string.
		/// </summary>
		public RegexPatternsResolver() { }

		/// <summary>
		/// Class initialization with essential parameters.
		/// </summary>
		/// <param name="pattern"></param>
		/// <param name="replacement"></param>
		public RegexPatternsResolver(string pattern, string replacement)
		{
			AddPattern(pattern, replacement);
		}

		/// <summary>
		/// Class initialization with all parameters.
		/// </summary>
		/// <param name="pattern"></param>
		/// <param name="replacement"></param>
		/// <param name="regexOption"></param>
		public RegexPatternsResolver(string pattern, string replacement, RegexOptions regexOption)
		{
			RegexOption = regexOption;
			AddPattern(pattern, replacement);
		}

		/// <summary>
		/// Adds a new Regex pattern into patterns collection.
		/// Throws exception if pattern is null.
		/// </summary>
		/// <param name="pattern"></param>
		/// <param name="replacement"></param>
		/// <param name="regexOption"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public void AddPattern(string pattern, string replacement, RegexOptions? regexOption = null)
		{
			if (string.IsNullOrWhiteSpace(pattern))
				throw new ArgumentNullException(nameof(pattern));

			(_patterns ??= new Dictionary<int, RegexPatternInstance>())
				.Add(_index, new RegexPatternInstance() { Pattern = pattern, Replacement = replacement, RegexOption = regexOption ?? RegexOption });

			_index++;
		}

		/// <summary>
		/// Returns input string replaced using patterns previously added.
		/// </summary>
		/// <param name="input"></param>
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
				throw new ArgumentException("string to replace is null", nameof(input));

			if (_patterns == null)
				throw new Exception("Class parameter [patterns] is null. Do you have added some patterns before resolve?");

			string resolvedString = input;
			try
			{
				if (_patterns.Count > 0)
				{
					foreach (KeyValuePair<int, RegexPatternInstance> pattern in _patterns)
					{
						Regex codeTitleRegex = new Regex (pattern.Value.Pattern, pattern.Value.RegexOption);
						resolvedString = codeTitleRegex.Replace(resolvedString, pattern.Value.Replacement ?? string.Empty);
					}
				}

				return resolvedString;
			}
			catch (ArgumentException argumentException)
			{
				throw new Exception($"[Param: {argumentException.ParamName} - Source: {argumentException.Source}] - {argumentException.Message}", argumentException.InnerException);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex.InnerException);
			}
		}
	}
}
