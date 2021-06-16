using Leviathan.Services;
using System;
using System.Threading.Tasks;

namespace Leviathan.Core {
	public interface ILeviathan : IAsyncInitialize{
		void Start();
		void Stop();
	}
}