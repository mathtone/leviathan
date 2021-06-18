﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.Alpha.UI.Main.Shared {
	public class NavModule {
		public string Name { get; set; }
		public string Target { get; set; }
	}

	public partial class NavMenu {

		public IEnumerable<NavModule> Modules => new[] {
			new NavModule { Name="Home",Target=""},
			new NavModule { Name="Admin",Target="Admin"},
			new NavModule { Name="Startup",Target="Admin/Startup"},
			new NavModule { Name="Equipment",Target="Equipment"},
		};

		protected override async Task OnInitializedAsync() {
			await base.OnInitializedAsync();
		}
	}
}
