﻿using System;
using System.Globalization;
using PostSharp.Extensibility;
using PostSharp.CodeModel;

namespace SixPack.Validation.PostSharp
{
	/// <summary>
	/// Validates that a parameter is not null.
	/// </summary>
	/// <remarks>
	/// This attribute supports any reference type as well as <see cref="Nullable{T}"/>.
	/// </remarks>
	[Serializable]
	public sealed class NotNullAttribute : SpecificExceptionParameterValidatorAttribute
	{
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

			Type parameterType = parameter.ParameterType.GetSystemType(null, null);

			bool isNullable =
				!parameterType.IsValueType ||
				(parameterType.IsGenericType && parameterType.GetGenericTypeDefinition() == typeof(Nullable<>));

			if (!isNullable)
			{
				messages.Write(new Message(
					SeverityType.Error,
					"NotNullAttribute_TypeNotNullable",
					string.Format(CultureInfo.InvariantCulture, "The type '{0}' is not nullable.", parameterType.Name),
					GetType().FullName
				));
			}
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="target">The object on which the method is being called.</param>
		/// <param name="value">The value of the parameter.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		public override void Validate(object target, object value, string parameterName)
		{
			if(Exception == null)
			{
				if(Message == null)
				{
					NotNull.Validate(value, parameterName);
				}
				else
				{
					NotNull.Validate(value, parameterName, Message);
				}
			}
			else
			{
				NotNull.Validate(value, parameterName, CreateException);
			}
		}
	}
}