using System.IO;
using System.Reflection;
using Leviathan.DB.Npgsql;
using Npgsql;
using NUnit.Framework;

namespace Sandbox {
	public class Tests {
		[SetUp]
		public void Setup() {
		}

		[Test]
		public void Test1() {
			using var cn = new NpgsqlConnection("Host=poseidonalpha.local;Database=Leviathan0x01;Username=pi;Password=Digital!2021;Persist Security Info=True");
			cn.Open();
			var repo = new AccountRepo(cn);
			var l = repo.List();
			var r = repo.Read(6);
			var u = repo.Update(r);
		}

		[Test]
		public void Test2() {

			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = "Sandbox.TestResources.TestResource.txt";

			using var stream = assembly.GetManifestResourceStream(resourceName);
			using var reader = new StreamReader(stream); string result = reader.ReadToEnd();
		}

		[Test]
		public void Test3() {

			var settings = new InitializationSettings {
				InstanceId = 0,
				DbName = "Leviathan0x00",
				DropDb = false,
				CreateDb = false,
				CreateObjects = true,
				HostName = "poseidonalpha.local",
			};

			using var cn = new NpgsqlConnection($"Host={settings.HostName};Database=postgres;Username=pi;Password=Digital!2021;");
			cn.Open();

			var data = new InitData(cn);
			//var exists = data.LocateDatabase(settings.DbName);
			data.InitDb(settings);
		}
	}
	//public abstract class HardwareController {	}

	////public class GpioController : HardwareController {
	////}

	//public class PCA9865Controller : HardwareController {
	//}
}