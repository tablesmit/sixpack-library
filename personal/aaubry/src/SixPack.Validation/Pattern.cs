using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SixPack.Validation
{
	/// <summary>
	/// Validates parameters that should not be null.
	/// </summary>
	public static class Pattern
	{
		#region String pattern
		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="pattern">The pattern.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		public static void Validate(string value, string pattern, string parameterName)
		{
			Validate(value, pattern, RegexOptions.None, parameterName, defaultMessage => new ArgumentNullException(parameterName, defaultMessage));
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="pattern">The pattern.</param>
		/// <param name="options">The regular expression options.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		public static void Validate(string value, string pattern, RegexOptions options, string parameterName)
		{
			Validate(value, pattern, options, parameterName, defaultMessage => new ArgumentNullException(parameterName, defaultMessage));
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="pattern">The pattern.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="message">The message of the exception that is thrown when the validation fails.</param>
		public static void Validate(string value, string pattern, string parameterName, string message)
		{
			Validate(value, pattern, RegexOptions.None, parameterName, defaultMessage => new ArgumentNullException(parameterName, message));
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="pattern">The pattern.</param>
		/// <param name="options">The regular expression options.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="message">The message of the exception that is thrown when the validation fails.</param>
		public static void Validate(string value, string pattern, RegexOptions options, string parameterName, string message)
		{
			Validate(value, pattern, options, parameterName, defaultMessage => new ArgumentNullException(parameterName, message));
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="pattern">The pattern.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="createException">A <see cref="CreateExceptionFunction"/> delegate that is used to create the
		/// exception that is thrown when the validation fails.</param>
		public static void Validate(string value, string pattern, string parameterName, CreateExceptionFunction createException)
		{
			Validate(value, pattern, RegexOptions.None, parameterName, createException);
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="pattern">The pattern.</param>
		/// <param name="options">The regular expression options.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="createException">A <see cref="CreateExceptionFunction"/> delegate that is used to create the
		/// exception that is thrown when the validation fails.</param>
		public static void Validate(string value, string pattern, RegexOptions options, string parameterName, CreateExceptionFunction createException)
		{
			if(string.IsNullOrEmpty(pattern))
			{
				throw new ArgumentNullException("pattern");
			}

			if (createException == null)
			{
				throw new ArgumentNullException("createException");
			}

			if (value != null && !Regex.IsMatch(value, pattern, options))
			{
				throw createException(GetDefaultMessage(parameterName));
			}
		}
		#endregion

		#region Regex pattern
		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="pattern">The pattern.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		public static void Validate(string value, Regex pattern, string parameterName)
		{
			Validate(value, pattern, parameterName, defaultMessage => new ArgumentException(defaultMessage, parameterName));
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="pattern">The pattern.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="message">The message of the exception that is thrown when the validation fails.</param>
		public static void Validate(string value, Regex pattern, string parameterName, string message)
		{
			Validate(value, pattern, parameterName, defaultMessage => new ArgumentException(message, parameterName));
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="pattern">The pattern.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="createException">A <see cref="CreateExceptionFunction"/> delegate that is used to create the
		/// exception that is thrown when the validation fails.</param>
		public static void Validate(string value, Regex pattern, string parameterName, CreateExceptionFunction createException)
		{
			if (pattern == null)
			{
				throw new ArgumentNullException("pattern");
			}

			if (createException == null)
			{
				throw new ArgumentNullException("createException");
			}

			if (value != null && !pattern.IsMatch(value))
			{
				throw createException(GetDefaultMessage(parameterName));
			}
		}
		#endregion

		#region Private utility methods
		private static string GetDefaultMessage(string parameterName)
		{
			return string.Format(CultureInfo.InvariantCulture, "The parameter '{0}' does not match the defined pattern.", parameterName);
		}
		#endregion
	}
}