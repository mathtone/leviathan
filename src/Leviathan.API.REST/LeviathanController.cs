using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.API.REST {
	
	public class LeviathanController : ControllerBase {	}

	public class LeviathanDataController<T> : LeviathanController {
		
		protected T Data { get; }
		
		public LeviathanDataController(T data) {
			this.Data = data;
		}
	}
}
