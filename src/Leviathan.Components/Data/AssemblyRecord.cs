using Leviathan.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.Components.Data {
	public record AssemblyRecord : NamedItem<long> {
		public string Location { get; set; }
	}
}
