using Leviathan.Channels.SDK;
using System;
using System.Threading.Tasks;

namespace Leviathan.PCA9685 {
	public class PCA9685Channel : IAsyncInputChannel<double>, IAsyncOutputChannel<double> {

		public Task<double> GetValue() {
			throw new NotImplementedException();
		}

		public Task SetValue(double value) {
			throw new NotImplementedException();
		}
	}
}