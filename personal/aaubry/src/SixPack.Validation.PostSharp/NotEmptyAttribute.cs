using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using PostSharp.CodeModel;
using PostSharp.Extensibility;

namespace SixPack.Validation.PostSharp
{
	/// <summary>
	/// Validates that a parameter is not empty.
	/// </summary>
	/// <remarks>
	/// This attributes supports the following types: <see cref="String"/>, <see cref="ICollection"/> and <see cref="ICollection{T}"/>
	/// </remarks>
	[Serializable]
	public sealed class NotEmptyAttribute : SpecificExceptionParameterValidatorAttribute
	{
		private enum ParameterKind
		{
			String,
			ICollection,
			ICollectionOfT
		}

		private ParameterKind parameterKind;

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

			if (parameterType == typeof(string))
			{
				parameterKind = ParameterKind.String;
				return;
			}

			if(typeof(ICollection).IsAssignableFrom(parameterType))
			{
				parameterKind = ParameterKind.ICollection;
				return;
			}

			if (TypeIsICollectionOfT(parameterType))
			{
				parameterKind = ParameterKind.ICollectionOfT;
				//getCount = CreateGetCountDelegate(parameter.ParameterType.GetSystemType(null, null));

				Type interfaceType = parameter.ParameterType.GetSystemType(null, null);
				if(Exception != null)
				{
					validateMethodWithException = CreateValidateMethodWithException(interfaceType);
				}
				else if(Message != null)
				{
					validateMethodWithMessage = CreateValidateMethodWithMessage(interfaceType);
				}
				else
				{
					validateMethod = CreateValidateMethod(interfaceType);
				}

				return;
			}

			messages.Write(new Message(
				SeverityType.Error,
				"NotEmptyAttribute_TypeNotSupported",
				string.Format(CultureInfo.InvariantCulture, "The type '{0}' is not supported.", parameterType.Name),
				GetType().FullName
			));
		}

		private static bool TypeIsICollectionOfT(Type type)
		{
			if (type.IsInterface && InterfaceIsICollectionOfT(type))
			{
				return true;
			}

			foreach (var interfaceType in type.GetInterfaces())
			{
				if (InterfaceIsICollectionOfT(interfaceType))
				{
					return true;
				}
			}
			return false;
		}

		private static bool InterfaceIsICollectionOfT(Type type)
		{
			return
				type.Assembly == typeof(ICollection<>).Assembly &&
				type.Namespace == typeof(ICollection<>).Namespace &&
				type.Name == typeof(ICollection<>).Name;
		}

		private delegate void ValidateMethod(object value, string parameterName);
		private delegate void ValidateMethodWithMessage(object value, string parameterName, string message);
		private delegate void ValidateMethodWithException(object value, string parameterName, CreateExceptionFunction createException);

		// This methods are called through reflection.
		// ReSharper disable UnusedPrivateMember
		private static ValidateMethod MakeValidateMethod<T>()
		{
			return (value, parameterName) => NotEmpty.Validate((ICollection<T>)value, parameterName);
		}

		private static ValidateMethodWithMessage MakeValidateMethodWithMessage<T>()
		{
			return (value, parameterName, message) => NotEmpty.Validate((ICollection<T>)value, parameterName, message);
		}

		private static ValidateMethodWithException MakeValidateMethodWithException<T>()
		{
			return (value, parameterName, createException) => NotEmpty.Validate((ICollection<T>)value, parameterName, createException);
		}
		// ReSharper restore UnusedPrivateMember

		private static ValidateMethod CreateValidateMethod(Type interfaceType)
		{
			Type itemType = interfaceType.GetGenericArguments()[0];

			MethodInfo openValidateMethod = typeof(NotEmptyAttribute).GetMethod("MakeValidateMethod", BindingFlags.Static | BindingFlags.NonPublic);
			MethodInfo makeValidateMethod = openValidateMethod.MakeGenericMethod(itemType);

			return (ValidateMethod)makeValidateMethod.Invoke(null, new object[0]);
		}

		private static ValidateMethodWithMessage CreateValidateMethodWithMessage(Type interfaceType)
		{
			Type itemType = interfaceType.GetGenericArguments()[0];

			MethodInfo openValidateMethodWithMessage = typeof(NotEmptyAttribute).GetMethod("MakeValidateMethod", BindingFlags.Static | BindingFlags.NonPublic);
			MethodInfo makeValidateMethodWithMessage = openValidateMethodWithMessage.MakeGenericMethod(itemType);

			return (ValidateMethodWithMessage)makeValidateMethodWithMessage.Invoke(null, new object[0]);
		}

		private static ValidateMethodWithException CreateValidateMethodWithException(Type interfaceType)
		{
			Type itemType = interfaceType.GetGenericArguments()[0];

			MethodInfo openValidateMethodWithException = typeof(NotEmptyAttribute).GetMethod("MakeValidateMethod", BindingFlags.Static | BindingFlags.NonPublic);
			MethodInfo makeValidateMethodWithException = openValidateMethodWithException.MakeGenericMethod(itemType);

			return (ValidateMethodWithException)makeValidateMethodWithException.Invoke(null, new object[0]);
		}

		private ValidateMethod validateMethod;
		private ValidateMethodWithMessage validateMethodWithMessage;
		private ValidateMethodWithException validateMethodWithException;

		/// <summary>
		/// Validates the parameter.
		/// </summary>
		/// <param name="target">The object on which the method is being invoked.</param>
		/// <param name="value">The value of the parameter.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		public override void Validate(object target, object value, string parameterName)
		{
			if (value != null)
			{
				switch(parameterKind)
				{
					case ParameterKind.String:
						ValidateString((string)value, parameterName);
						break;
					
					case ParameterKind.ICollection:
						ValidateCollection((ICollection)value, parameterName);
						break;
		
					case ParameterKind.ICollectionOfT:
						ValidateCollectionOfT(value, parameterName);
						break;

					default:
						throw new InvalidOperationException("An invalid ParameterKind has been detected.");
				}
			}
		}

		private void ValidateCollectionOfT(object parameterValue, string parameterName)
		{
			if (Exception == null)
			{
				if (Message == null)
				{
					validateMethod(parameterValue, parameterName);
				}
				else
				{
					validateMethodWithMessage(parameterValue, parameterName, Message);
				}
			}
			else
			{
				validateMethodWithException(parameterValue, parameterName, CreateException);
			}
		}

		private void ValidateCollection(ICollection parameterValue, string parameterName)
		{
			if (Exception == null)
			{
				if (Message == null)
				{
					NotEmpty.Validate(parameterValue, parameterName);
				}
				else
				{
					NotEmpty.Validate(parameterValue, parameterName, Message);
				}
			}
			else
			{
				NotEmpty.Validate(parameterValue, parameterName, CreateException);
			}
		}

		private void ValidateString(string parameterValue, string parameterName)
		{
			if (Exception == null)
			{
				if (Message == null)
				{
					NotEmpty.Validate(parameterValue, parameterName);
				}
				else
				{
					NotEmpty.Validate(parameterValue, parameterName, Message);
				}
			}
			else
			{
				NotEmpty.Validate(parameterValue, parameterName, CreateException);
			}
		}
	}
}