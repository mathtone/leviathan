using System;
using System.Threading.Tasks;

namespace Leviathan.Core.SDK {
	public interface ITheLeviathan {
		Task Start();
		Task Stop();
	}
}
