using System;
using System.Text.RegularExpressions;
using Zyronaa.SDK.Core.Configuration;

using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Zyronaa.SDK.Plugins.Configuration
{
	public class SteamIdConverter : IYamlTypeConverter
	{
		private static readonly Regex Steam2Regex = new Regex("^STEAM_0:[0-1]:([0-9]{1,10})$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		private static readonly Regex Steam32Regex = new Regex("^\\[?U:1:([0-9]{1,10})\\]?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		private static readonly Regex Steam64Regex = new Regex("^7656119([0-9]{10})$", RegexOptions.Compiled);

		public bool Accepts(Type type) => type == typeof(SteamId);

		public object ReadYaml(IParser parser, Type type, ObjectDeserializer nestedObjectDeserializer)
		{
			var value = ((Scalar)parser.Current).Value.Trim();
			parser.MoveNext();

			long id;

			if (Steam64Regex.IsMatch(value))
			{
				id = long.Parse(value);
			}
			else if (Steam2Regex.IsMatch(value))
			{
				id = SteamId.FromSteamId2(value);
			}
			else if (Steam32Regex.IsMatch(value))
			{
				id = SteamId.FromSteamId32(value);
			}
			else
			{
				throw new YamlException("YML input not in valid SteamID 2, 32, or 64 format");
			}

			return new SteamId(id);
		}

		public void WriteYaml(IEmitter emitter, object value, Type type, ObjectSerializer nestedObjectSerializer)
		{
			emitter.Emit(new Scalar(value.ToString()));
		}
	}
}
