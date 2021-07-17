using System;
using System.IO;
using System.Reflection;

namespace Sandbox.TestConsole {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine("Hello World!");

			//List / Load Assemblies
			var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			foreach (var f in Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories)) {
				Console.WriteLine(f);
			}
		}
	}

	public class SystemConfiguration {

	}
}