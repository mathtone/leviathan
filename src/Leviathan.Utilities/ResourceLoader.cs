using System;
using System.IO;
using System.Reflection;

namespace Leviathan.Utilities {
	public static class ResourceLoader {

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