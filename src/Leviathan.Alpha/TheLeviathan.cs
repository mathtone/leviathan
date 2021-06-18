using Leviathan.Alpha.FactoryReset;
using Leviathan.Core;
using Leviathan.System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Leviathan.Alpha {

	public class TheLeviathan : ILeviathan {

		IFactoryResetService factoryReset;
		IHostApplicationLifetime AppLifetime;

		public TheLeviathan(IFactoryResetService factoryReset, IHostApplicationLifetime AppLifetime) : base() {
			this.factoryReset = factoryReset;
			this.factoryReset.SelfDestructBegun += FactoryReset_SelfDestructBegun;
			this.factoryReset.SelfDestructComplete += FactoryReset_SelfDestructComplete;
		}

		private void FactoryReset_SelfDestructBegun(object sender, EventArgs e) {
			//throw new NotImplementedException();
		}

		private void FactoryReset_SelfDestructComplete(object sender, EventArgs e) {
			//throw new NotImplementedException();
			
		}


		public void Start() {

		}

		public void Stop() {

		}
	}
}