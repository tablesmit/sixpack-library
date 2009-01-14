﻿using System;
using System.Globalization;
using System.Reflection;
using PostSharp.CodeModel;
using PostSharp.CodeModel.ReflectionWrapper;
using PostSharp.Extensibility;
using System.Web.UI;

namespace SixPack.Web.PostSharp
{
	/// <summary>
	/// Implements an abstract property as a ViewState variable.
	/// </summary>
	[Serializable]
	public sealed class ViewStateVariableAttribute : CollectionVariableAttribute
	{
		private string fieldName;

		/// <summary>
		/// Validates the usage of the attribute on a specific property.
		/// </summary>
		/// <param name="propertyInfo">The property.</param>
		/// <param name="method">The method.</param>
		/// <param name="isGetMethod">if set to <c>true</c> [is get method].</param>
		protected override void CompileTimeValidate(PropertyInfo propertyInfo, MethodBase method, bool isGetMethod)
		{
			if (!typeof(Control).IsAssignableFrom(method.DeclaringType))
			{
				MessageSource.MessageSink.Write(new Message(
					SeverityType.Error,
					"ViewStateVariableAttribute_PropertyDoesNotBelongToControl",
					string.Format(CultureInfo.InvariantCulture, "The property '{0}' does not belong to a System.Web.UI.Control.", propertyInfo.Name),
					GetType().FullName
				));
			}
			if(method.IsStatic)
			{
				MessageSource.MessageSink.Write(new Message(
					SeverityType.Error,
					"ViewStateVariableAttribute_PropertyCannotBeStatic",
					string.Format(CultureInfo.InvariantCulture, "The property '{0}' must not be static.", propertyInfo.Name),
					GetType().FullName
				));
			}

			fieldName = string.Format(CultureInfo.InvariantCulture, "__~~~~{0}", propertyInfo.Name);

			if(isGetMethod)
			{
				MethodDefDeclaration postsharpMethod = ((IReflectionWrapper<MethodDefDeclaration>)method).WrappedObject;

				FieldDefDeclaration propertyField = new FieldDefDeclaration();
				propertyField.Name = fieldName;
				propertyField.FieldType = postsharpMethod.Module.FindType(propertyInfo.PropertyType, BindingOptions.Default);
				propertyField.Attributes = FieldAttributes.Private;

				postsharpMethod.DeclaringType.Fields.Add(propertyField);
			}
		}

		private delegate StateBag GetViewStateDelegate(Control target);

		private static readonly GetViewStateDelegate getViewState = (GetViewStateDelegate)CreatePropertyGetterDelegate(
			typeof(GetViewStateDelegate),
			"ViewState"
		);

		private delegate bool IsViewStateEnabledDelegate(Control target);

		private static readonly IsViewStateEnabledDelegate isViewStateEnabled = (IsViewStateEnabledDelegate)CreatePropertyGetterDelegate(
			typeof(IsViewStateEnabledDelegate),
			"IsViewStateEnabled"
		);


		private static Delegate CreatePropertyGetterDelegate(Type delegateType, string propertyName)
		{
			PropertyInfo isViewStateEnabledProperty = typeof(Control).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic);
			MethodInfo getIsViewStateEnabledMethod = isViewStateEnabledProperty.GetGetMethod(true);
			return Delegate.CreateDelegate(delegateType, getIsViewStateEnabledMethod);
		}

		[NonSerialized]
		private FieldInfo field;

		private FieldInfo GetField(object target)
		{
			if(field == null)
			{
				field = target.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
			}
			return field;
		}

		/// <summary>
		/// Gets the value of the property.
		/// </summary>
		/// <param name="target">The object from which the property should be obtained.</param>
		/// <param name="defaultValue">The default value for the property.</param>
		/// <returns></returns>
		protected override object GetValue(object target, object defaultValue)
		{
			object value;
			Control control = (Control)target;
			if(isViewStateEnabled(control))
			{
				StateBag viewState = getViewState(control);
				value = viewState[Key];
			}
			else
			{
				value = GetField(target).GetValue(target);
			}
			return value ?? defaultValue;
		}

		/// <summary>
		/// Sets the value of the property.
		/// </summary>
		/// <param name="target">The object on which the property should be set.</param>
		/// <param name="value">The new value of the property.</param>
		/// <param name="defaultValue">The default value for the property.</param>
		protected override void SetValue(object target, object value, object defaultValue)
		{
			Control control = (Control)target;
			if (isViewStateEnabled(control))
			{
				StateBag viewState = getViewState(control);
				viewState[Key] = value;
			}
			GetField(target).SetValue(target, value);
		}
	}
}