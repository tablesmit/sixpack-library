using System;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;

namespace SixPack.Validation
{
	/// <summary>
	/// Validates parameters that should not be null.
	/// </summary>
	public static class NotEmpty
	{
		#region String validation
		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		public static void Validate(string value, string parameterName)
		{
			Validate(value, parameterName, defaultMessage => new ArgumentException(defaultMessage, parameterName));
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="message">The message of the exception that is thrown when the validation fails.</param>
		public static void Validate(string value, string parameterName, string message)
		{
			Validate(value, parameterName, defaultMessage => new ArgumentException(message, parameterName));
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="createException">A <see cref="CreateExceptionFunction"/> delegate that is used to create the
		/// exception that is thrown when the validation fails.</param>
		public static void Validate(string value, string parameterName, CreateExceptionFunction createException)
		{
			if (createException == null)
			{
				throw new ArgumentNullException("createException");
			}

			if (value != null && value.Length == 0)
			{
				throw createException(GetDefaultMessage(parameterName));
			}
		}
		#endregion

		#region ICollection validation
		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		public static void Validate(ICollection value, string parameterName)
		{
			Validate(value, parameterName, defaultMessage => new ArgumentException(defaultMessage, parameterName));
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="message">The message of the exception that is thrown when the validation fails.</param>
		public static void Validate(ICollection value, string parameterName, string message)
		{
			Validate(value, parameterName, defaultMessage => new ArgumentException(message, parameterName));
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="createException">A <see cref="CreateExceptionFunction"/> delegate that is used to create the
		/// exception that is thrown when the validation fails.</param>
		public static void Validate(ICollection value, string parameterName, CreateExceptionFunction createException)
		{
			if (createException == null)
			{
				throw new ArgumentNullException("createException");
			}

			if (value != null && value.Count == 0)
			{
				throw createException(GetDefaultMessage(parameterName));
			}
		}
		#endregion

		#region ICollection<T> validation
		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		public static void Validate<T>(ICollection<T> value, string parameterName)
		{
			Validate(value, parameterName, defaultMessage => new ArgumentException(defaultMessage, parameterName));
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="message">The message of the exception that is thrown when the validation fails.</param>
		public static void Validate<T>(ICollection<T> value, string parameterName, string message)
		{
			Validate(value, parameterName, defaultMessage => new ArgumentException(message, parameterName));
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="createException">A <see cref="CreateExceptionFunction"/> delegate that is used to create the
		/// exception that is thrown when the validation fails.</param>
		public static void Validate<T>(ICollection<T> value, string parameterName, CreateExceptionFunction createException)
		{
			if (createException == null)
			{
				throw new ArgumentNullException("createException");
			}

			if (value != null && value.Count == 0)
			{
				throw createException(GetDefaultMessage(parameterName));
			}
		}
		#endregion

		#region Private utility methods
		private static string GetDefaultMessage(string parameterName)
		{
			return string.Format(CultureInfo.InvariantCulture, "The parameter '{0}' is empty", parameterName);
		}
		#endregion
	}
}