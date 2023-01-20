using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography;
using System.Text;

namespace PocDataProtection
{
	public class ProtectedString
	{
		private string _cipherText = string.Empty;
		private string _appName = "Default App Name";
		private string _protectorName = "Default Protector Name";
		private IDataProtectionProvider _provider;
		private IDataProtector _protector;

		public ProtectedString(string appName, string protectorName)
		{
			_appName = appName;
			_protectorName = protectorName;
			_provider = DataProtectionProvider.Create(_appName);
			_protector = _provider.CreateProtector(protectorName);
		}

		/// <summary>
		/// Takes a plain Text variable by ref. After the plain text is protected the "plainText" variable
		/// will have its memory wiped for security. Keep this in mind to ensure you can no longer use
		/// this variable and its contents are wiped from memory.
		/// </summary>
		/// <param name="plainText"></param>
		/// <returns></returns>
		public string Protect(ref string plainText)
		{
			_cipherText = _protector.Protect(plainText);
			WipeStringMemory(ref plainText);
			return _cipherText;
		}
		public string Unprotect()
		{
			return _protector.Unprotect(_cipherText);
		}

		/// <summary>
		/// Wipes the Memory of the input using Random Bytes and sets string to empty.
		/// 
		/// This is main for example and is likely a better with using direct memory access
		/// in unsafe mode or perhaps a Span<byte>.
		/// </summary>
		/// <param name="input"></param>
		public void WipeStringMemory(ref string input)
		{
			try
			{
				byte[] randomBytes = new byte[input.Length * sizeof(char)];
				new Random().NextBytes(randomBytes);
				char[] charArray = new char[input.Length];
				Buffer.BlockCopy(randomBytes, 0, charArray, 0, randomBytes.Length);
				input = new string(charArray);
			}
			finally
			{
				input = string.Empty;
			}
		
			// This could be removed if performance is higher priority over security
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}
	}
}