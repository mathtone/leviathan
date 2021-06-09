using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leviathan.DataAccess {

	public interface IRepository<T> : IRepository<T, long> { }
	public interface IRepository<T, ID> {
		ID Create(T item);
		T Read(ID id);
		T Update(T item);
		void Delete(ID id);
	}
}