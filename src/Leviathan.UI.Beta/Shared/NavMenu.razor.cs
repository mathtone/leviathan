using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.UI.Beta.Shared {

	public class NavMenuItem {
		public string Label { get; set; }
		public string IconClass { get; set; }
		public string NavTarget { get; set; }

		public NavMenuItem(string label, string iconClass, string navTarget) {
			this.Label = label;
			this.IconClass = iconClass;
			this.NavTarget = navTarget;
		}
	}

	public partial class NavMenu {
		public IEnumerable<NavMenuItem> Items { get; } = new[] {
			new NavMenuItem("Home","oi oi-home",""),
			new NavMenuItem("Admin","oi oi-wrench","Admin"),
			new NavMenuItem("Setup","bi bi-alarm","Setup")
		};
		public NavMenu() {
			;
		}
	}
}
