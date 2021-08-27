using Leviathan.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.Services.Data {
	public record ServiceRecord : DataItem<long> {
		public long ComponentId { get; set; }
	}
}