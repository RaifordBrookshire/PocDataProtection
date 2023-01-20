using Microsoft.AspNetCore.DataProtection;

namespace PocDataProtection
{
	public class ProtectedString
	{
		private string _cipherText;
		private string _appName = "My Application Name";
		private string _protectorName = "My Protector";
		private IDataProtectionProvider _provider;
		private IDataProtector _protector;

		public ProtectedString(string appName, string protectorName)
		{
			_appName = appName;
			_protectorName = protectorName;
			_provider = DataProtectionProvider.Create(_appName);
			_protector = _provider.CreateProtector(protectorName);
		}

		public string Protect(string plainText)
		{
			return _cipherText = _protector.Protect(plainText);		
		}
		public string Unprotect()
		{
			return _protector.Unprotect(_cipherText);
		}
	}
}