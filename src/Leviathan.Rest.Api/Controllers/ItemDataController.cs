using Microsoft.AspNetCore.Mvc;

namespace Leviathan.Rest.Api.Controllers {
	public abstract class ItemDataController<DATA> : ControllerBase {
		protected DATA Data { get; }
		
		public ItemDataController(DATA data) {
			this.Data = data; 
		}
	}
}