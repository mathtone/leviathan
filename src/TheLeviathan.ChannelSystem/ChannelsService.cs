using Leviathan.Channels.Sdk;
using Leviathan.Services.Sdk;
using Leviathan.WebApi.Sdk;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheLeviathan.Components;

namespace TheLeviathan.ChannelSystem{

	public interface IChannelSystemService : IHostedService {
		IChannelData Data { get; }
		IReadOnlyDictionary<int, IChannel> Channels { get; }
	}


	public interface IChannelData {
		IEnumerable<ChannelRecord> List();

	}

	public class ChannelRecord {
		public int Id { get; set; }
		public string TypeName { get; set; }
		public string ChannelData { get; set; }
	}

	[HostedSingletonService(typeof(IChannelSystemService))]
	public class ChannelsService : IChannelSystemService {

		ILogger _log;
		IComponentFactory _components;
		public IChannelData Data { get; }

		public IReadOnlyDictionary<int, IChannel> Channels { get; protected set; }

		public ChannelsService(ILogger<ChannelsService> log, IChannelData data, IComponentFactory components) {
			(_log, Data, _components) = (log, data, components);
			Channels = data
				.List()
				.ToDictionary(c => c.Id, c =>
					(IChannel)components.ActivateInstance(Type.GetType(c.TypeName),
					c.ChannelData
				));
		}

		public Task StartAsync(CancellationToken cancellationToken) {

			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken) {
			return Task.CompletedTask;
		}
	}

	[ApiComponent(ApiModules.Channels)]
	public class ChannelsController : ServiceController<IChannelSystemService> {

		public ChannelsController(IChannelSystemService service) : base(service) {
		}

		[HttpGet, Route("{id}")]
		public async Task<string> GetAsync(int id) {
			var ch = Service.Channels[id];
			if (ch is IAsyncInputChannel chInAsync) {
				return (await chInAsync.GetAsync()).ToString(); ;
			}
			else if (ch is IInputChannel chIn) {
				return chIn.Get().ToString();
			}
			throw new Exception($"Could not read input from channel {id}");
		}

		[HttpGet, Route("[action]")]
		public IEnumerable<ChannelRecord> List() => Service.Data.List();
	}

	//public class UIntRouteConstraint : IRouteConstraint {
	//	public bool Match(HttpContext httpContext, IRouter route, string routeKey,
	//		RouteValueDictionary values, RouteDirection routeDirection) {

	//		if (routeKey == null)
	//			throw new ArgumentNullException(nameof(routeKey));

	//		if (values == null)
	//			throw new ArgumentNullException(nameof(values));

	//		if (values.TryGetValue(routeKey, out object routeValue) && routeValue != null) {
	//			if (routeValue is uint)
	//				return true;

	//			string valueString = Convert.ToString(routeValue, CultureInfo.InvariantCulture);

	//			return UInt32.TryParse(valueString, out uint _);
	//		}

	//		return false;
	//	}
	//}
}