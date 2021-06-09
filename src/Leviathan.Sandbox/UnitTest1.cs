//using Leviathan.Initialization;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Leviathan.Sandbox {

	public class UnitTest1 {

		[Fact]
		public void Test1() {
			var type = typeof(TestClass);
			var attrs = type.GetCustomAttribute<TestAttributeAttribute>();
			var name = attrs.Name;

			Assert.Equal("TEST", name);
			//Console.WriteLine(TestValues.TestNestedValues.BEAK);
			//var q = new Queries();
			//var c = q.Value1;
			//var npg = new NpgsqlConnection();
			//var x = default(IDbConnection);
			//await npg.OpenAsync();
		}

		[Fact]
		public void Test2() {

			var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			var assys = AppDomain.CurrentDomain.GetAssemblies();
			var dic = new Dictionary<string, Assembly>();
			//var t = typeof(ConfigProfileAttribute);

			foreach (var ass in assys.Where(a => !a.IsDynamic)) {
				if (Path.GetDirectoryName(ass.Location) == path) {
					dic.TryAdd(ass.Location, ass);
				}
			}

			//foreach (var dll in Directory.GetFiles(path, "*.dll")) {
			//	if (!dic.ContainsKey(dll)) {
			//		;
			//	}
			//}

			//var d = assys.Where(a => !a.IsDynamic).ToDictionary(a => a.Location);

			//foreach (var dll in Directory.GetFiles(path, "*.dll")) {

			//	if (dll.EndsWith("Initialization.dll")) {

			//		;
			//	}
			//	//var assy = Assembly.ReflectionOnlyLoadFrom(dll);
			//	////var assy = Assembly.LoadFile(dll);
			//	//foreach (var type in assy.GetTypes()) {
			//	//	if (type.Name.EndsWith("RoboTankProfile")) {
			//	//		var attrs = type.GetCustomAttributes().ToArray()[0];
			//	//		;
			//	//		//}
			//	//	}

			//	//	//var attr = type.GetCustomAttribute<ConfigProfileAttribute>();
			//	//	//if (attr != null) {
			//	//	//	;
			//	//	//}
			//	//}
			//}
		}

		//public interface IAsyncDbConnection : IDbConnection {
		//	ValueTask<DbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default);
		//	ValueTask<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
		//	Task ChangeDatabaseAsync(string databaseName, CancellationToken cancellationToken = default);
		//	Task OpenAsync();
		//	Task CloseAsync();
		//}
	}

	[TestAttribute("TEST")]
	public class TestClass {

	}

	public class TestAttributeAttribute : Attribute {
		public string Name { get; }

		public TestAttributeAttribute(string name) {
			this.Name = name;
		}
	}
}