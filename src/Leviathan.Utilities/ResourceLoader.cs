using System.IO;
using System.Reflection;

namespace Leviathan.Utilities {
	public static class ResourceLoader {
		public static string LoadLocalResource(string resourceName) {
			var assembly = Assembly.GetCallingAssembly();
			return LoadResource(assembly, assembly.GetName().Name + "." + resourceName);
		}

		public static string LoadResource(string resourceName) =>
			LoadResource(Assembly.GetCallingAssembly(), resourceName);

		public static string LoadResource(Assembly assembly, string resourceName) {
			using var stream = assembly.GetManifestResourceStream(resourceName);
			using var reader = new StreamReader(stream);
			return reader.ReadToEnd();
		}
	}
}