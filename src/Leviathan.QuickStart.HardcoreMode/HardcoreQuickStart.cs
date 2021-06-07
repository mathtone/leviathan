using Leviathan.Services.Core.Hardware;
using Leviathan.Services.Core.QuickStart;
using System;

namespace Leviathan.QuickStart.HardcoreMode {
	public class HardcoreQuickStart : IQuickStartProfile {
		public QuickStartInfo Info { get; } = new QuickStartInfo {
			Id = "I.GOT.THIS",
			Name = "Hardcore Mode",
			Description = "Do nothing.  I will awaken the Leviathan myself"
		};
		public HardcoreQuickStart(IHardwareService hardware) { }

		public void Apply() {
			//throw new NotImplementedException();
		}
	}
}