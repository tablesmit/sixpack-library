using System;
using System.Globalization;

namespace SixPack.Validation
{
	/// <summary>
	/// Validates parameters that should not be null.
	/// </summary>
	public static class NotNull
	{
		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		public static void Validate(object value, string parameterName)
		{
			Validate(value, parameterName, defaultMessage => new ArgumentNullException(parameterName, defaultMessage));
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="message">The message of the exception that is thrown when the validation fails.</param>
		public static void Validate(object value, string parameterName, string message)
		{
			Validate(value, parameterName, defaultMessage => new ArgumentNullException(parameterName, message));
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="createException">
		/// A <see cref="CreateExceptionFunction"/> delegate that is used to create the
		/// exception that is thrown when the validation fails.
		/// </param>
		public static void Validate(object value, string parameterName, CreateExceptionFunction createException)
		{
			if (createException == null)
			{
				throw new ArgumentNullException("createException");
			}

			if (value == null)
			{
				throw createException(GetDefaultMessage(parameterName));
			}
		}

		#region Private utility methods
		private static string GetDefaultMessage(string parameterName)
		{
			return string.Format(CultureInfo.InvariantCulture, "The parameter '{0}' is null.", parameterName);
		}
		#endregion
	}
}