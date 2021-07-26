using Leviathan.Alpha.Components;
using Leviathan.Channels.SDK;
using Leviathan.Services.SDK;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Channels {


	[SingletonService(typeof(IChannelSystemService))]
	public class ChannelSystem : LeviathanService, IChannelSystemService {

		ILogger<ChannelSystem> _log;
		IComponentsService _components;
		IServiceProvider _services;
		IDictionary<string, IChannel> _channels;

		public override Task Initialize { get; }

		public IDictionary<string, IChannel> Channels => _channels ?? WhenInitialized(() => _channels).Result;

		public ChannelSystem(ILogger<ChannelSystem> log, IServiceProvider services, IComponentsService components) {
			(_log, _components, _services) = (log, components, services);
			this.Initialize = InitializeAsync();
		}

		async Task InitializeAsync() {

			await base.Initialize;

			_channels = (await GetChannelProviders().ToArrayAsync())
				.SelectMany(p => p.Channels)
				.ToDictionary(c => c.Id);


			_log.LogInformation($"{typeof(ChannelSystem)} Initialized");
		}

		async IAsyncEnumerable<IChannelProvider> GetChannelProviders() {

			var channelProviderTypes = (await _components.AvailableComponents())
				.Where(c => c.AttributeTypes.Contains(typeof(ChannelProviderAttribute)));

			foreach (var t in channelProviderTypes) {

				var svcType = t.Type.GetCustomAttribute<ServiceComponentAttribute>();
				var svc = _services.GetService(svcType.ServiceTypes.First()) as IChannelProvider;
				yield return svc;
			}
		}

		public async Task<object> GetValueAsync(string id) {
			var ch = Channels[id];
			if (ch is IAsyncInputChannel cha) {
				return await cha.GetValueAsync();
			}
			else if (ch is IInputChannel chi) {
				return chi.GetValue();
			}
			throw new NotImplementedException();
		}

		public async Task SetValueAsync(string id, object value) {
			var ch = Channels[id];
			if (ch is IAsyncOutputChannel cha) {
				await cha.SetValueAsync(value);
			}
			else if (ch is IOutputChannel chi) {
				chi.SetValue(value);
			}
			throw new NotImplementedException();
		}
	}
}