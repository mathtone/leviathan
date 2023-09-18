using System.Data;
using System.Data.Common;

namespace Leviathan.Data {
   public static class ConnectionProviderExtensions {
      public static CN Open<CN>(this IConnectionProvider<CN> provider, string? name = default)
        where CN : IDbConnection {

         var cn = provider.CreateConnection(name);
         cn.Open();
         return cn;
      }

      public static void Execute<CN>(this IConnectionProvider<CN> provider, Action<CN> action)
       where CN : IDbConnection =>
         provider.Execute(default, action);

      public static void Execute<CN>(this IConnectionProvider<CN> provider, string? name, Action<CN> action)
         where CN : IDbConnection {

         using var cn = provider.Open(name);
         action(cn);
         cn.Close();
      }

      public static T Execute<CN, T>(this IConnectionProvider<CN> provider, string? name, Func<CN, T> action)
         where CN : IDbConnection {

         using var cn = provider.Open(name);
         try {
            return action(cn);
         }
         finally {
            cn.Close();
         }
      }

      public static IEnumerable<T> Consume<CN, T>(this IConnectionProvider<CN> provider, string? name, Func<CN, IEnumerable<T>> action)
         where CN : DbConnection {

         using var cn = provider.Open(name);
         try {
            foreach (var item in action(cn))
               yield return item;
         }
         finally {
            cn.Close();
         }
      }

   }
}