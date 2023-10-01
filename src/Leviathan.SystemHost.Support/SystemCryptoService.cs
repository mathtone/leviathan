using Leviathan.Security;
using System.Security.Cryptography;

namespace Leviathan.SystemHost.Support {
	public class SystemCryptoService(IKeyProvider keyProvider) : CryptoServiceBase(keyProvider) {
		protected override RSACryptoServiceProvider CreateServiceProvider() {
			var rtn = base.CreateServiceProvider();
			rtn.ImportPkcs8PrivateKey(KeyProvider.GetKeyBytes("leviathan_private_key"), out _);
			//rtn.im(KeyProvider.GetKeyBytes("leviathan_public_key"), out _);
			return rtn;
		}
	}
}