using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Xunit;
using static System.Guid;
namespace Sandbox.Tests {
	public class UnitTest1 {

		[Fact]
		public void Test1() {
			using IHost host = CreateHostBuilder()
				.Build();
			
			var services = host.Services.GetServices<object>();
			host.RunAsync();
		}

		static IHostBuilder CreateHostBuilder() => Host.CreateDefaultBuilder();

		//static void ExemplifyScoping(IServiceProvider services, string scope) {
		//	using IServiceScope serviceScope = services.CreateScope();
		//	IServiceProvider provider = serviceScope.ServiceProvider;

		//	OperationLogger logger = provider.GetRequiredService<OperationLogger>();
		//	logger.LogOperations($"{scope}-Call 1 .GetRequiredService<OperationLogger>()");

		//	Console.WriteLine("...");

		//	logger = provider.GetRequiredService<OperationLogger>();
		//	logger.LogOperations($"{scope}-Call 2 .GetRequiredService<OperationLogger>()");

		//	Console.WriteLine();
		//}
	}

	//public interface IOperation {
	//	string OperationId { get; }
	//}
	//public interface ITransientOperation : IOperation { }
	//public interface IScopedOperation : IOperation { }
	//public interface ISingletonOperation : IOperation { }

	//public class DefaultOperation :
	//   ITransientOperation,
	//   IScopedOperation,
	//   ISingletonOperation {
	//	public string OperationId { get; } = NewGuid().ToString()[^4..];
	//}

	//public class OperationLogger {

	//	private readonly ITransientOperation _transientOperation;
	//	private readonly IScopedOperation _scopedOperation;
	//	private readonly ISingletonOperation _singletonOperation;

	//	public OperationLogger(ITransientOperation transientOperation, IScopedOperation scopedOperation, ISingletonOperation singletonOperation) =>
	//		(_transientOperation, _scopedOperation, _singletonOperation) = (transientOperation, scopedOperation, singletonOperation);

	//	public void LogOperations(string scope) {
	//		LogOperation(_transientOperation, scope, "Always different");
	//		LogOperation(_scopedOperation, scope, "Changes only with scope");
	//		LogOperation(_singletonOperation, scope, "Always the same");
	//	}

	//	private static void LogOperation<T>(T operation, string scope, string message)
	//		where T : IOperation =>
	//		Console.WriteLine(
	//			$"{scope}: {typeof(T).Name,-19} [ {operation.OperationId}...{message,-23} ]");
	//}
}