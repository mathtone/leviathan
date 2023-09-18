namespace Leviathan.Data {

   public interface IConnectionProvider<out CN> {
      CN CreateConnection(string? name = default);
   }
}