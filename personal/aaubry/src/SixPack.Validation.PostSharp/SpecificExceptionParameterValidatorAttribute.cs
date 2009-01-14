using System;
using System.Reflection;
using PostSharp.CodeModel;
using PostSharp.Extensibility;
using System.Globalization;

namespace SixPack.Validation.PostSharp
{
	/// <summary>
	/// Base attribute for validating method parameters that throws a user-specified exception when
	/// the validation fails.
	/// </summary>
	[Serializable]
	public abstract class SpecificExceptionParameterValidatorAttribute : ParameterValidatorAttribute
	{
		private Type exception;

		/// <summary>
		/// Gets the type of the exception that is thrown when the validation fails.
		/// </summary>
		public Type Exception
		{
			get
			{
				return exception;
			}
			set
			{
				exception = value;
			}
		}

		private string message;

		/// <summary>
		/// Gets or sets the exception message.
		/// </summary>
		public string Message
		{
			get
			{
				return message;
			}
			set
			{
				message = value;
			}
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

			if(exception != null)
			{
				if(!ValidateExceptionTypeIsException(messages))
				{
					return;
				}
				if(!ValidateExceptionTypeCanBeInstantiated(messages))
				{
					return;
				}
			}
		}

		private bool ValidateExceptionTypeIsException(IMessageSink messages)
		{
			bool isValid = typeof(Exception).IsAssignableFrom(exception);
			if(!isValid)
			{
				messages.Write(new Message(
					SeverityType.Error,
					"SpecificExceptionParameterValidatorAttribute_InvalidExceptionType",
					string.Format(CultureInfo.InvariantCulture, "The type '{0}' is not an exception type.", exception.Name),
					GetType().FullName
				));
			}
			return isValid;
		}

		private bool ValidateExceptionTypeCanBeInstantiated(IMessageSink messages)
		{
			ConstructorInfo constructor = exception.GetConstructor(
				BindingFlags.Public | BindingFlags.Instance,
				null,
				new[] {typeof(string)},
				null
			);

			bool isValid = constructor != null;
			if(!isValid)
			{
				messages.Write(new Message(
					SeverityType.Error,
					"SpecificExceptionParameterValidatorAttribute_ExceptionTypeCanotBeInstantiated",
					string.Format(CultureInfo.InvariantCulture, "The exception '{0}' does not have a public constructor that takes a string as parameter.", exception.Name),
					GetType().FullName
				));
			}
			return isValid;
		}

		/// <summary>
		/// Creates an exception of the type specified inthe <see cref="Exception"/> property.
		/// </summary>
		/// <param name="defaultMessage">The default message of the exception.</param>
		/// <returns></returns>
		protected Exception CreateException(string defaultMessage)
		{
			return (Exception)Activator.CreateInstance(exception, message ?? defaultMessage);
		}
	}
}