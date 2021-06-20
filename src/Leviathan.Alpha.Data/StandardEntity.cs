using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.Alpha.Data {
	public record StandardEntity<ID> {
		public ID Id { get; init; }
		public string Name { get; init; }
		public string Description { get; init; }
	}
}