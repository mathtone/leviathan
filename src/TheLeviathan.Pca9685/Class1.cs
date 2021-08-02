using Leviathan.Channels.Sdk;
using System;
using System.Threading.Tasks;

namespace TheLeviathan.Pca9685 {

	public class Pca9685Channel : IAsyncInOutChannel<double> {

		public Task<double> GetAsync() {
			throw new NotImplementedException();
		}

		public Task SetAsync(double value) {
			throw new NotImplementedException();
		}

		public Task SetAsync(object value) {
			throw new NotImplementedException();
		}

		Task<string> IAsyncInputChannel.GetAsync() {
			throw new NotImplementedException();
		}
	}
}