using Leviathan.Services.Sdk;
using System;

namespace TheLeviathan.SetupSystem {
	public interface ISetupService {

	}

	[SingletonService(typeof(ISetupService))]
	public class SetupService : ISetupService {

	}
}