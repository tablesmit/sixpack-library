using System;
using System.Globalization;

namespace SixPack.Validation
{
	/// <summary>
	/// Validates parameters that should not be null.
	/// </summary>
	public static class Interval
	{
		#region IComparable validation
		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="min">The min value that the parameter can have.</param>
		/// <param name="max">The maximum value that the parameter can have.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		public static void Validate(IComparable value, object min, object max, string parameterName)
		{
			Validate(value, min, BoundaryMode.Inclusive, max, BoundaryMode.Inclusive, parameterName);
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="min">The min value that the parameter can have.</param>
		/// <param name="minMode">Indicates whether the minimum value is inclusive or exclusive.</param>
		/// <param name="max">The maximum value that the parameter can have.</param>
		/// <param name="maxMode">Indicates whether the maximum value is inclusive or exclusive.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		public static void Validate(IComparable value, object min, BoundaryMode minMode, object max, BoundaryMode maxMode, string parameterName)
		{
			Validate(value, min, minMode, max, maxMode, parameterName, defaultMessage => new ArgumentOutOfRangeException(parameterName, defaultMessage));
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="min">The min value that the parameter can have.</param>
		/// <param name="max">The maximum value that the parameter can have.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="message">The message of the exception that is thrown when the validation fails.</param>
		public static void Validate(IComparable value, object min, object max, string parameterName, string message)
		{
			Validate(value, min, BoundaryMode.Inclusive, max, BoundaryMode.Inclusive, parameterName, message);
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="min">The min value that the parameter can have.</param>
		/// <param name="minMode">Indicates whether the minimum value is inclusive or exclusive.</param>
		/// <param name="max">The maximum value that the parameter can have.</param>
		/// <param name="maxMode">Indicates whether the maximum value is inclusive or exclusive.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="message">The message of the exception that is thrown when the validation fails.</param>
		public static void Validate(IComparable value, object min, BoundaryMode minMode, object max, BoundaryMode maxMode, string parameterName, string message)
		{
			Validate(value, min, minMode, max, maxMode, parameterName, defaultMessage => new ArgumentOutOfRangeException(parameterName, message));
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="min">The min value that the parameter can have.</param>
		/// <param name="max">The maximum value that the parameter can have.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="createException">A <see cref="CreateExceptionFunction"/> delegate that is used to create the
		/// exception that is thrown when the validation fails.</param>
		public static void Validate(IComparable value, object min, object max, string parameterName, CreateExceptionFunction createException)
		{
			Validate(value, min, BoundaryMode.Inclusive, max, BoundaryMode.Inclusive, parameterName, createException);
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="min">The min value that the parameter can have.</param>
		/// <param name="minMode">Indicates whether the minimum value is inclusive or exclusive.</param>
		/// <param name="max">The maximum value that the parameter can have.</param>
		/// <param name="maxMode">Indicates whether the maximum value is inclusive or exclusive.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="createException">A <see cref="CreateExceptionFunction"/> delegate that is used to create the
		/// exception that is thrown when the validation fails.</param>
		public static void Validate(IComparable value, object min, BoundaryMode minMode, object max, BoundaryMode maxMode, string parameterName, CreateExceptionFunction createException)
		{
			if (min == null)
			{
				throw new ArgumentNullException("min");
			}

			if (max == null)
			{
				throw new ArgumentNullException("max");
			}

			if (createException == null)
			{
				throw new ArgumentNullException("createException");
			}

			if (value != null)
			{
				bool isValid =
					ValidateBoundary(minMode, value.CompareTo(min)) &&
					ValidateBoundary(maxMode, -value.CompareTo(max));

				if (!isValid)
				{
					throw createException(GetDefaultMessage(min, minMode, max, maxMode, parameterName));
				}
			}
		}
		#endregion

		#region IComparable<T> validation
		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="min">The min value that the parameter can have.</param>
		/// <param name="max">The maximum value that the parameter can have.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		public static void Validate<T>(IComparable<T> value, T min, T max, string parameterName)
		{
			Validate(value, min, BoundaryMode.Inclusive, max, BoundaryMode.Inclusive, parameterName);
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="min">The min value that the parameter can have.</param>
		/// <param name="minMode">Indicates whether the minimum value is inclusive or exclusive.</param>
		/// <param name="max">The maximum value that the parameter can have.</param>
		/// <param name="maxMode">Indicates whether the maximum value is inclusive or exclusive.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		public static void Validate<T>(IComparable<T> value, T min, BoundaryMode minMode, T max, BoundaryMode maxMode, string parameterName)
		{
			Validate(value, min, minMode, max, maxMode, parameterName, defaultMessage => new ArgumentOutOfRangeException(parameterName, defaultMessage));
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="min">The min value that the parameter can have.</param>
		/// <param name="max">The maximum value that the parameter can have.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="message">The message of the exception that is thrown when the validation fails.</param>
		public static void Validate<T>(IComparable<T> value, T min, T max, string parameterName, string message)
		{
			Validate(value, min, BoundaryMode.Inclusive, max, BoundaryMode.Inclusive, parameterName, message);
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="min">The min value that the parameter can have.</param>
		/// <param name="minMode">Indicates whether the minimum value is inclusive or exclusive.</param>
		/// <param name="max">The maximum value that the parameter can have.</param>
		/// <param name="maxMode">Indicates whether the maximum value is inclusive or exclusive.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="message">The message of the exception that is thrown when the validation fails.</param>
		public static void Validate<T>(IComparable<T> value, T min, BoundaryMode minMode, T max, BoundaryMode maxMode, string parameterName, string message)
		{
			Validate(value, min, minMode, max, maxMode, parameterName, defaultMessage => new ArgumentOutOfRangeException(parameterName, message));
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="min">The min value that the parameter can have.</param>
		/// <param name="max">The maximum value that the parameter can have.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="createException">A <see cref="CreateExceptionFunction"/> delegate that is used to create the
		/// exception that is thrown when the validation fails.</param>
		public static void Validate<T>(IComparable<T> value, T min, T max, string parameterName, CreateExceptionFunction createException)
		{
			Validate(value, min, BoundaryMode.Inclusive, max, BoundaryMode.Inclusive, parameterName, createException);
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="min">The min value that the parameter can have.</param>
		/// <param name="minMode">Indicates whether the minimum value is inclusive or exclusive.</param>
		/// <param name="max">The maximum value that the parameter can have.</param>
		/// <param name="maxMode">Indicates whether the maximum value is inclusive or exclusive.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="createException">A <see cref="CreateExceptionFunction"/> delegate that is used to create the
		/// exception that is thrown when the validation fails.</param>
		public static void Validate<T>(IComparable<T> value, T min, BoundaryMode minMode, T max, BoundaryMode maxMode, string parameterName, CreateExceptionFunction createException)
		{
			// We can safely compare with null because it will have the expected behaviour.
			// ReSharper disable CompareNonConstrainedGenericWithNull
			if (min == null)
			{
				throw new ArgumentNullException("min");
			}

			if (max == null)
			{
				throw new ArgumentNullException("max");
			}
			// ReSharper restore CompareNonConstrainedGenericWithNull

			if (createException == null)
			{
				throw new ArgumentNullException("createException");
			}

			if (value != null)
			{
				bool isValid =
					ValidateBoundary(minMode, value.CompareTo(min)) &&
					ValidateBoundary(maxMode, -value.CompareTo(max));

				if (!isValid)
				{
					throw createException(GetDefaultMessage(min, minMode, max, maxMode, parameterName));
				}
			}
		}
		#endregion

		#region Private utility methods
		private static string GetDefaultMessage(object min, BoundaryMode minMode, object max, BoundaryMode maxMode, string parameterName)
		{
			return string.Format(
				CultureInfo.InvariantCulture,
				"The parameter '{4}' must be in the interval {0}{1}, {2}{3}",
				minMode == BoundaryMode.Inclusive ? '[' : ']',
				min,
				max,
				maxMode == BoundaryMode.Inclusive ? ']' : '[',
				parameterName
			);
		}

		private static bool ValidateBoundary(BoundaryMode comparisonMode, int comparisonResult)
		{
			return !(comparisonResult < 0 || (comparisonResult == 0 && comparisonMode == BoundaryMode.Exclusive));
		}
		#endregion
	}
}