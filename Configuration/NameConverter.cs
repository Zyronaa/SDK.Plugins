using System;
using Zyronaa.SDK.Core.Plugins;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Zyronaa.SDK.Plugins.Configuration
{
	/// <summary>
	/// Yaml converter for <see cref="Name" /> type.
	/// </summary>
	public class NameConverter : IYamlTypeConverter
	{
		/// <summary>
		/// Determines whether this converter supports the specified type.
		/// </summary>
		public bool Accepts(Type type) => type == typeof(Name);

		/// <summary>
		/// Reads an object's state from a Yaml parser.
		/// </summary>
		public object ReadYaml(IParser parser, Type type, ObjectDeserializer deserializer)
		{
			var value = ((Scalar)parser.Current).Value;
			parser.MoveNext();
			return new Name(value);
		}

		/// <summary>
		/// Writes the specified object's state to a Yaml emitter.
		/// </summary>
		public void WriteYaml(IEmitter emitter, object value, Type type, ObjectSerializer serializer)
		{
			emitter.Emit(new Scalar(((Name)value).ToString()));
		}
	}
}
