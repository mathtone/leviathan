using Leviathan.Services.SDK;
using System.Threading.Tasks;

namespace Leviathan.Channels.SDK {
	public interface IChannelSystemService : ILeviathanService {
		Task<object> GetValueAsync(string id);
		Task SetValueAsync(string id,object value);
	}
}
