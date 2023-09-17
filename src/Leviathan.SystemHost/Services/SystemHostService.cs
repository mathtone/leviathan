using Leviathan.SystemHost.Sdk;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.SystemHost.Services {
	public class SystemHostService : ISystemHost, IHostedService {
		public async Task StartAsync(CancellationToken cancellationToken) {
			await Task.CompletedTask;

		}

		public async Task StopAsync(CancellationToken cancellationToken) {
			await Task.CompletedTask;
		}
	}


}