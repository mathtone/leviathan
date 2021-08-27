namespace Leviathan.Data {
	public record NamedItem<ID> : DataItem<ID> {
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
