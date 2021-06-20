using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Leviathan.SDK {
	public static class ResourceLoader {

		public static async Task<string> LoadLocalAsync(string resourceName) {
			var assembly = Assembly.GetCallingAssembly();
			return await LoadAsync(assembly, assembly.GetName().Name + "." + resourceName);
		}

		public static async Task<string> LoadAsync(Assembly assembly, string resourceName) {
			using var stream = assembly.GetManifestResourceStream(resourceName);
			using var reader = new StreamReader(stream);
			return await reader.ReadToEndAsync();
		}

		public static string LoadLocal(string resourceName) {
			var assembly = Assembly.GetCallingAssembly();
			return Load(assembly, assembly.GetName().Name + "." + resourceName);
		}

		public static string Load(Assembly assembly, string resourceName) {
			using var stream = assembly.GetManifestResourceStream(resourceName);
			using var reader = new StreamReader(stream);
			return reader.ReadToEnd();
		}
	}
}
