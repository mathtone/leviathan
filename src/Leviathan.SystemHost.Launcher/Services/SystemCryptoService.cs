using Leviathan.Crypto;
using System.Security.Cryptography;
namespace Leviathan.SystemHost.Launcher.Services {
	class SystemCryptoService(IKeyProvider keyProvider) : CryptoServicebase(keyProvider) {
		protected override RSACryptoServiceProvider CreateServiceProvider() {
			var rtn = base.CreateServiceProvider();
			rtn.ImportPkcs8PrivateKey(KeyProvider.GetKeyBytes("leviathan_private_key"), out _);
			return rtn;
		}
	}
}
