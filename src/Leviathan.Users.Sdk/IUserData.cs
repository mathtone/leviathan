using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathtone.Sdk.Patterns;

namespace Leviathan.Users {
   public interface IUserData : IAsyncListRepository<int, UserRecord> {
      Task<UserRecord> ReadByLogin(string login);
   }

   public record UserRecord(long Id, string Login) {
      public string PasswordHash { get; init; }
   }
}