using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Leviathan.SystemHost.Services {
	public class FileKeyProvider : IKeyProvider {

		readonly ConcurrentDictionary<string, string> _keys = new();

		public string? GetKey(string keyName) =>
			_keys.GetOrAdd(keyName, GetKeyFromFile);

		static string GetKeyFromFile(string name) => File
			.ReadAllText($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!}/{name}.pem")
			.Replace("-----BEGIN PRIVATE KEY-----", "")
			.Replace("-----END PRIVATE KEY-----", "")
			.Trim();
	}

	public interface IKeyProvider {
		public string? GetKey(string keyName);
	}
}