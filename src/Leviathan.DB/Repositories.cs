using Leviathan.DataAccess;
using Leviathan.Model;

namespace Leviathan.DB {
	public interface IAccountRepository : IListRepository<Account, int> { }
	public interface IAuthDataRepository : IListRepository<AuthData, int> { }
	public interface IConnectorRepository : IListRepository<Connector, int> { }
	public interface IControllerRepository : IListRepository<Controller, int> { }
}