using System;
using System.Threading.Tasks;
using Leviathan.DataAccess.Npgsql;
using Leviathan.Services.Core;
using Leviathan.Services.Core.Hardware;
using Leviathan.Services.DbInit.Npgsql;
using Leviathan.Services.Hardware.Npgsql.Modules;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Leviathan.Alpha {
	class Program {
		static void Main(string[] args) {

			
			var config = new CoreConfig { DbName = "Leviathan0x00" };
			var dbInit = new DbInitService("Host=poseidonalpha.local;Database=postgres;Username=pi;Password=Digital!2021;");
			var conn = new NpgsqlConnectionProvider($"Host=poseidonalpha.local;Database={config.DbName};Username=pi;Password=Digital!2021;");
			var hardware = new HardwareService(
				new ModuleTypeRepo(conn),
				new ModuleData(conn),
				new ChannelTypeRepo(conn),
				new ChannelData(conn),
				new ChannelControllerTypeRepo(conn),
				new ChannelControllerRepo(conn)
			);
			var core = new LeviathanCore(config, dbInit, hardware, new ConsoleLogger<LeviathanCore>());

			core.Start();
		}

		public abstract class Logger<T> : ILogger<T> {

			public virtual IDisposable BeginScope<TState>(TState state) => state as IDisposable;

			public virtual bool IsEnabled(LogLevel logLevel) => true;

			public abstract void Log<TState1>(LogLevel logLevel, EventId eventId, TState1 state, Exception exception, Func<TState1, Exception, string> formatter);

		}

		public class ConsoleLogger<TState> : Logger<TState> {

			public override void Log<TState1>(LogLevel logLevel, EventId eventId, TState1 state, Exception exception, Func<TState1, Exception, string> formatter) =>
				Console.WriteLine($"{DateTimeOffset.Now} {logLevel}:{formatter(state, exception)}");
		}
	}

	//public class LeviathanCore {

	//	bool running;
	//	object locker = new object();

	//	public void Start() {
	//		lock (locker) {

	//		}
	//	}

	//	public void Stop() { }
	//}
}