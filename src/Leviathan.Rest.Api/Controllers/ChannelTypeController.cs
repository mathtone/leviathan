using Leviathan.Hardware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.Rest.Api.Controllers {

	[ApiController]
	[Route("[controller]/[action]")]
	public class ChannelType : ItemDataController<IChannelTypeData> {

		public ChannelType(IChannelTypeData data) : base(data) { }

		[HttpGet]
		public IEnumerable<TypeInfo> Catalog() => Data.Catalog();
	}
}