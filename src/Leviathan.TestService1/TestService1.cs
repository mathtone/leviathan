using Leviathan.Services.Sdk;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Leviathan.TestService1 {
	public interface ITestService1: IHostedService {

	}
	[SingletonService(typeof(ITestService1))]
	public class TestService1 : ITestService1 {
		public Task StartAsync(CancellationToken cancellationToken) {
			throw new NotImplementedException();
		}

		public Task StopAsync(CancellationToken cancellationToken) {
			throw new NotImplementedException();
		}
	}
}