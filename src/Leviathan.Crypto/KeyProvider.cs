using System.Collections.Concurrent;

namespace Leviathan.Crypto {
	public class KeyProvider(Dictionary<string, Func<string, string>> keySelectors) : IKeyProvider {

		readonly ConcurrentDictionary<string, string> keys = new();

		public string GetKey(string name) => keys.GetOrAdd(name, LoadKey);

		public ReadOnlySpan<byte> GetKeyBytes(string name)=> new(Convert.FromBase64String(GetKey(name)));

      string LoadKey(string name) => keySelectors[name](name);
	}
}