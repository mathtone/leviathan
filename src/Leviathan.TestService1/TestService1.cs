using Leviathan.Services.Sdk;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Leviathan.TestService1 {
	public interface ITestService1 {

	}
	[TransientService(typeof(ITestService1))]
	public class TestService1 : ITestService1 {

	}
}