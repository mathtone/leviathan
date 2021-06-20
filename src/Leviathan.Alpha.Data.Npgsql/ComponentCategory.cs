namespace Leviathan.Alpha.Data.Npgsql {
	public record ComponentCategoryRecord : StandardEntity<long> {

	}

	public class ComponentCategoryRepo : AlphaDbDataRepo<ComponentCategoryRecord> { }
}