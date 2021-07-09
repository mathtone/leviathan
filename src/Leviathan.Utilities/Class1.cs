using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Leviathan.Utilities {
	public class AssemblyLoader {
		private static List<Assembly> _loadedAssemblies = new();

		public static List<Assembly> GetLoadedAssemblies(params string[] scanAssembliesStartsWith) {
			if (_loadedAssemblies.Any()) {
				return _loadedAssemblies;
			}

			LoadAssemblies(scanAssembliesStartsWith);
			return _loadedAssemblies;
		}

		private static void LoadAssemblies(params string[] scanAssembliesStartsWith) {
			HashSet<Assembly> loadedAssemblies = new();
			List<string> assembliesToBeLoaded = new();
			string appDllsDirectory = AppDomain.CurrentDomain.BaseDirectory;

			if (scanAssembliesStartsWith?.Any() == true) {
				if (scanAssembliesStartsWith.Length == 1) {
					var searchPattern = $"{scanAssembliesStartsWith.First()}*.dll";
					var assemblyPaths = Directory.GetFiles(appDllsDirectory, searchPattern, SearchOption.AllDirectories);
					assembliesToBeLoaded.AddRange(assemblyPaths);
				}

				if (scanAssembliesStartsWith.Length > 1) {
					foreach (string starsWith in scanAssembliesStartsWith) {
						var searchPattern = $"{starsWith}*.dll";
						var assemblyPaths = Directory.GetFiles(appDllsDirectory, searchPattern, SearchOption.AllDirectories);
						assembliesToBeLoaded.AddRange(assemblyPaths);
					}
				}
			}
			else {
				var assemblyPaths = Directory.GetFiles(appDllsDirectory, "*.dll");
				assembliesToBeLoaded.AddRange(assemblyPaths);
			}

			foreach (var path in assembliesToBeLoaded) {
				try {
					loadedAssemblies.Add(Assembly.LoadFrom(path));
				}
				catch (Exception) {
					continue;
				}
			}

			_loadedAssemblies = loadedAssemblies.ToList();
		}
	}
}