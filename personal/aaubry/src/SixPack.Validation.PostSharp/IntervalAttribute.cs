using System;
using System.Globalization;
using PostSharp.Extensibility;
using PostSharp.CodeModel;

namespace SixPack.Validation.PostSharp
{
	/// <summary>
	/// Validates that a parameter is in the specified interval
	/// </summary>
	[Serializable]
	public sealed class IntervalAttribute : SpecificExceptionParameterValidatorAttribute
	{
		private readonly object min;

		/// <summary>
		/// Gets the minimum value that the parameter can have.
		/// </summary>
		public object Min
		{
			get
			{
				return min;
			}
		}

		private BoundaryMode minMode;

		/// <summary>
		/// Gets or sets a value indicating whether the minimum value is inclusive or exclusive.
		/// </summary>
		public BoundaryMode MinMode
		{
			get
			{
				return minMode;
			}
			set
			{
				minMode = value;
			}
		}

		private readonly object max;

		/// <summary>
		/// Gets the maximum value that the parameter can have.
		/// </summary>
		public object Max
		{
			get
			{
				return max;
			}
		}

		private BoundaryMode maxMode;

		/// <summary>
		/// Gets or sets a value indicating whether the maximum value is inclusive or exclusive.
		/// </summary>
		public BoundaryMode MaxMode
		{
			get
			{
				return maxMode;
			}
			set
			{
				maxMode = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IntervalAttribute"/> class.
		/// </summary>
		/// <param name="min">The minimum value that the parameter can have.</param>
		/// <param name="max">The maximum value that the parameter can have.</param>
		public IntervalAttribute(object min, object max)
		{
			this.min = min;
			this.max = max;
		}

		/// <summary>
		/// Method called at compile-time to validate the application of this
		/// custom attribute on a specific parameter.
		/// </summary>
		/// <param name="parameter">The parameter on which the attribute is applied.</param>
		/// <param name="messages">A <see cref="IMessageSink"/> where to write error messages.</param>
		/// <remarks>
		/// This method should use <paramref name="messages"/> to report any error encountered
		/// instead of throwing an exception.
		/// </remarks>
		[CLSCompliant(false)]
		public override void CompileTimeValidate(ParameterDeclaration parameter, IMessageSink messages)
		{
			base.CompileTimeValidate(parameter, messages);

			Type parameterType = GetSystemParameter(parameter).ParameterType;
			if (!typeof(IComparable).IsAssignableFrom(parameterType))
			{
				messages.Write(new Message(
					SeverityType.Error,
					"IntervalAttribute_TypeNotSupported",
					string.Format(CultureInfo.InvariantCulture, "The type '{0}' does not implement IComparable.", parameterType.Name),
					GetType().FullName
				));
			}
		}

		/// <summary>
		/// Validates the parameter.
		/// </summary>
		/// <param name="target">The object on which the method is being invoked.</param>
		/// <param name="value">The value of the parameter.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		public override void Validate(object target, object value, string parameterName)
		{
			IComparable comparable = (IComparable)value;
			if (Exception == null)
			{
				if (Message == null)
				{
					Interval.Validate(comparable, min, minMode, max, maxMode, parameterName);
				}
				else
				{
					Interval.Validate(comparable, min, minMode, max, maxMode, parameterName, Message);
				}
			}
			else
			{
				Interval.Validate(comparable, min, minMode, max, maxMode, parameterName, CreateException);
			}
		}
	}
}