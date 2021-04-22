using System;
using System.Threading.Tasks;

namespace Leviathan.Alpha {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine("Hello World!");
			var core = new LeviathanCore();
			core.Start();
		}
	}

	public class LeviathanCore {

		bool running;
		object locker = new object();

		public void Start() {
			lock (locker) {

			}
		}

		public void Stop() { }
	}
}