using System;
using Zyronaa.SDK.Core.Plugins;
using SemanticVersioning;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Zyronaa.SDK.Plugins.Configuration
{
	/// <inheritdoc />
	/// <summary>
	/// Yaml converter for <see cref="VersionRange" />.
	/// </summary>
	/// <seealso cref="IYamlTypeConverter" />
	public class VersionRangeConverter : IYamlTypeConverter
	{
		/// <inheritdoc />
		/// <summary>
		/// Gets a value indicating whether the current converter supports converting the specified type.
		/// </summary>
		public bool Accepts(Type type) => type == typeof(VersionRange) || type.BaseType == typeof(VersionRange);

		/// <inheritdoc />
		/// <summary>
		/// Reads an object's state from a YAML parser.
		/// </summary>
		public object ReadYaml(IParser parser, Type type, ObjectDeserializer deserializer)
		{
			var value = ((Scalar)parser.Current).Value;
			parser.MoveNext();

			var range = new Range(value);

			return new VersionRange
			{
				Value = range.ToString()
			};
		}

		/// <inheritdoc />
		/// <summary>
		/// Writes the specified object's state to a YAML emitter.
		/// </summary>
		public void WriteYaml(IEmitter emitter, object value, Type type, ObjectSerializer serializer)
		{
			emitter.Emit(new Scalar(((VersionRange)value).Value));
		}
	}
}
