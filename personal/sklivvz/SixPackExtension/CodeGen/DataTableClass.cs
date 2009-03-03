using System;
using System.Data;
using System.Globalization;
using System.Runtime.Serialization;
using SixPack.Text;

namespace SixPack.CodeGen
{
	/// <summary>
	/// A class that creates a data transfer object representation from a datatable.
	/// </summary>
	// the class should remain sealed because it is using virtual members in the constructor!
	public sealed class DataTableClass: SimpleClass
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DataTableClass"/> class.
		/// </summary>
		/// <param name="dataTable">The data table.</param>
		/// <param name="@namespace">The namespace.</param>
		/// <param name="prefix">The prefix.</param>
		public DataTableClass(DataTable dataTable, AbstractNamespace @namespace, string prefix)
		{
			if (string.IsNullOrEmpty(dataTable.TableName))
				throw new ArgumentNullException("dataTable", "The data table must have a TableName.");

			Name = TextUtilities2.NormalizeForCode(dataTable.TableName, TextNormalizationType.Class);
			NamespaceDefinition = @namespace;
			Postfix = "ISerializable";
			Prefix = "[Serializable]\n\t" + prefix;
			UsingClauses.Add(new SimpleUsingClause("System"));
			UsingClauses.Add(new SimpleUsingClause("System.Data"));
			UsingClauses.Add(new SimpleUsingClause("System.Runtime.Serialization"));
			UsingClauses.Add(new SimpleUsingClause("System.Security.Permissions"));

			// ISerializable pattern
			SimpleConstructor serializationConstructor = new SimpleConstructor(Name, "protected", null);
			serializationConstructor.Parameters.Add(new SimpleParameter("information", typeof (SerializationInfo), null));
			serializationConstructor.Parameters.Add(new SimpleParameter("context", typeof (StreamingContext), null));

			SimpleMethod serializationMethod = new SimpleMethod("GetObjectData",
			                                                    "[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]\n\t\tpublic",
			                                                    null);
			serializationMethod.Parameters.Add(new SimpleParameter("info", typeof (SerializationInfo), null));
			serializationMethod.Parameters.Add(new SimpleParameter("context", typeof (StreamingContext), null));

			// datarow constructor
			SimpleConstructor datarowConstructor = new SimpleConstructor(Name, "public", null);
			datarowConstructor.Parameters.Add(new SimpleParameter("dataRow", typeof (DataRow), null));

			foreach (DataColumn column in dataTable.Columns)
			{
				string fieldName = TextUtilities2.NormalizeForCode(column.ColumnName, TextNormalizationType.Field);
				string typeName = column.DataType.Name;

				Fields.Add(new SimpleField(fieldName, column.DataType, "private readonly"));

				SimpleGetter getter = new SimpleGetter();
				getter.Body.Add(string.Format(CultureInfo.InvariantCulture, "return {0};", fieldName));

				Properties.Add(new SimpleProperty(
				               	TextUtilities2.NormalizeForCode(column.ColumnName, TextNormalizationType.Property), column.DataType,
				               	"public", getter, null));

				serializationConstructor.Body.Add(string.Format(CultureInfo.InvariantCulture,
				                                                "{0} = ({1}) information.GetValue(\"{0}\", typeof ({1}));",
				                                                fieldName, typeName));

				serializationMethod.Body.Add(string.Format(CultureInfo.InvariantCulture, "info.AddValue(\"{0}\", {1});", fieldName,
				                                           fieldName));

				datarowConstructor.Body.Add(string.Format(CultureInfo.InvariantCulture, "{0} = ({1})dataRow[\"{2}\"];", fieldName,
				                                          typeName, column.ColumnName));
			}

			Constructors.Add(serializationConstructor);
			Methods.Add(serializationMethod);

			Constructors.Add(datarowConstructor);
		}
	}
}