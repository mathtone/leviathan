using Leviathan.Services.Core.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.Services.Core.QuickStart {
	public class QuickStartInfo {
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}

	public interface IQuickStartService {
		void RunQuickStart(string id);
		IEnumerable<QuickStartInfo> QuickStartProfiles();
	}

	public interface IQuickStartProfile {
		QuickStartInfo Info { get; }
		void Apply();
	}

	public class QuickStartService : IQuickStartService {
		
		IEnumerable<IQuickStartProfile> profiles;

		public QuickStartService(IEnumerable<IQuickStartProfile> profiles) {
			this.profiles = profiles;
		}

		public IEnumerable<QuickStartInfo> QuickStartProfiles() => profiles.Select(p=>p.Info);

		public void RunQuickStart(string id) {
			this.profiles.Single(p => p.Info.Id == id).Apply();
		}
	}
}