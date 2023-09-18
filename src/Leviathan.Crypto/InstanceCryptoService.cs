using System.Security.Cryptography;

namespace Leviathan.Crypto {
	public class InstanceCryptoService(IKeyProvider keyProvider) : CryptoServicebase(keyProvider){
		protected override RSACryptoServiceProvider CreateServiceProvider() {
			var rtn = base.CreateServiceProvider();
			rtn.ImportPkcs8PrivateKey(KeyProvider.GetKeyBytes("leviathan_public_key"), out _);
			return rtn;
		}
	}
}