using Leviathan.Components;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

[assembly: LeviathanModule("Remote Channels")]
namespace Leviathan.Hardware.Remote {
	//public class RemoteConnector {

	//}
	public class RemoteChannelData {
		public long PartnerId { get; init; }
		public string PartnerSvcUrl { get; init; }
	}

	public class RemoteChannel<T> : IAsyncInputChannel<T>,IAsyncOutputChannel<T> {
		
		RemoteChannelData _channelData;
		private readonly HttpClient _client = new HttpClient();

		public RemoteChannel(RemoteChannelData data) {
			
		}

		public async Task<T> GetValue() {
			var rslt = await _client.GetStringAsync(_channelData.PartnerSvcUrl);
			return JsonConvert.DeserializeObject<T>(rslt);
		}

		public async Task SetValue(T value) {
			
			var json = JsonConvert.SerializeObject(value);
			var data = new StringContent(json, Encoding.UTF8, "application/json");
			var rslt = await _client.PostAsync(_channelData.PartnerSvcUrl,data);
			if (!rslt.IsSuccessStatusCode) {
				throw new Exception("Error setting remote channel value");
			}
		}
	}
}