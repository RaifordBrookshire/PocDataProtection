using Microsoft.AspNetCore.DataProtection;

namespace PocDataProtection
{
	internal class Program
	{
		static void Main(string[] args)
		{
			UseDataProtectionNative();
			UseDataProtectionWrapper();

			Console.WriteLine("Hit any key..."); 
			Console.ReadLine();

		}

		private static void UseDataProtectionWrapper()
		{
			Console.WriteLine($"");
			Console.WriteLine($"Using DataProtection with a Wrapper class");
			Console.WriteLine($"-----------------------------------");
			
			const string appName = "My Application Name";
			const string protectorName = "My Protector";
			const string plainText = "My Secret in Plain Text";
			
			var protector = new ProtectedString(appName, protectorName);
			
			Console.WriteLine($"Encrypting the plain text: {plainText}");
			var cipherText = protector.Protect(plainText);
			Console.WriteLine($"Encrypted to Cipher Text: {cipherText}");

			// Decrypt the variable
			string secret = protector.Unprotect();
			Console.WriteLine($"Complete: plainText={plainText} decryptedText: {secret}");
		}

		private static void UseDataProtectionNative()
		{
			Console.WriteLine($"");
			Console.WriteLine($"Using DataProtection Native .NET");
			Console.WriteLine($"-----------------------------------");


			const string appName = "My Application Name";
			const string plainText = "My Secret in Plain Text";
			const string protectorName = "My Protector";

			var provider = DataProtectionProvider.Create(appName);
			var protector = provider.CreateProtector(protectorName);
		
			Console.WriteLine($"Encrypting the plain text: {plainText}");
			var cipherText = protector.Protect(plainText);
			Console.WriteLine($"Encrypted to Cipher Text: {cipherText}");

			// Decrypt the variable
			string secret = protector.Unprotect(cipherText);
			Console.WriteLine($"Complete: plainText={plainText} decryptedText: {secret}");
		}
	}
}