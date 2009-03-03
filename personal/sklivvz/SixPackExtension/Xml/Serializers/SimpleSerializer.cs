using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;

namespace SixPack.Xml.Serializers
{
	/// <summary>
	/// Serialize instances to XML in a simple way.
	/// </summary>
	public static class SimpleSerializer
	{
		/// <summary>
		/// Serialize an instance to a TextWriter stream
		/// </summary>
		/// <param name="stream">The output stream</param>
		/// <param name="target">The instance to be serialized</param>
		/// <param name="name">The name of the top element of the XML generated</param>
		public static void Serialize(TextWriter stream, object target, string name)
		{
			XmlTextWriter writer = new XmlTextWriter(stream);
			InternalSerialize(writer, target, name);
		}

		private static void InternalSerialize(XmlWriter writer, object target, string name)
		{
			writer.WriteStartElement(name);
			writer.WriteStartAttribute("type");
			writer.WriteString(getTypeName(target.GetType()));
			writer.WriteEndAttribute();
			try
			{
				writer.WriteValue(target);
			}
			catch (InvalidCastException)
			{
				InternalCompositeSerialize(writer, target);
			}
			writer.WriteEndElement();
		}

		private static void InternalCompositeSerialize(XmlWriter writer, object target)
		{
			// I don't know whether there can be more than one numbered property. 
			// Since we are using IEnumerables, only the first numbered property 
			// will give the name to the enumeration.
			bool hack_ReflectNumberedProperty = true;

			foreach (PropertyInfo pi in target.GetType().GetProperties())
			{
				if (pi.CanRead)
				{
					try
					{
						InternalSerialize(writer, pi.GetValue(target, BindingFlags.GetProperty, null, null, null), pi.Name);
					}
					catch (TargetParameterCountException)
					{
						// numbered parameters.
						IEnumerable enumerableTarget = target as IEnumerable;
						if (hack_ReflectNumberedProperty && enumerableTarget != null)
						{
							writer.WriteStartElement(pi.Name + "Array");
							foreach (object o in enumerableTarget)
							{
								InternalSerialize(writer, o, pi.Name);
							}
							writer.WriteEndElement();
							hack_ReflectNumberedProperty = false;
						}
						// else skip
					}
				}
			}
		}

		private static string getTypeName(Type type)
		{
			if (!type.IsGenericType)
			{
				return type.Name;
			}

			Type[] genericArgs = type.GetGenericArguments();
			List<string> names = new List<string>(genericArgs.Length);
			foreach (Type t in genericArgs)
			{
				names.Add(getTypeName(t));
			}

			string genericName = type.Name.Split('`')[0];
			string argsName = string.Join("And", names.ToArray());

			return string.Format(CultureInfo.InvariantCulture, "{0}Of{1}", genericName, argsName);
		}
	}
}