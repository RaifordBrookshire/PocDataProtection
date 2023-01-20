using Microsoft.AspNetCore.DataProtection;

namespace PocDataProtection
{
	internal class Program
	{
		static void Main(string[] args)
		{
			UseDataProtectionNative();
			UseDataProtectionWrapper();

			WriteLine("Hit any key..."); 
			Console.ReadLine();

		}

		private static void UseDataProtectionWrapper()
		{
			WriteLine($"");
			WriteLine($"Using DataProtection with a Wrapper class");
			WriteLine($"-----------------------------------");
			
			const string appName = "My Application Name";
			const string protectorName = "My Protector";
			string plainText = "My Secret in Plain Text";
			string originalText = plainText;

			var protector = new ProtectedString(appName, protectorName);
			
			WriteLine($"Encrypting the plain text: {plainText}");
			var cipherText = protector.Protect(ref plainText);
			WriteLine($"Encrypted to Cipher Text: {cipherText}");
			WriteLine($"Original Plain Text Memory is Wiped: {plainText}");

			// Decrypt the variable
			string secret = protector.Unprotect();
			WriteLine($"Complete: plainText (wiped)={plainText} originalText={originalText} decryptedText: {secret}");

			// Finally Wipe the memory of the secret once your done using it.
			protector.WipeStringMemory(ref secret);
			WriteLine($"Secret has been wiped 'secret': {secret} (this should be empty)");
		}

		private static void UseDataProtectionNative()
		{
			WriteLine($"");
			WriteLine($"Using DataProtection Native .NET");
			WriteLine($"-----------------------------------");


			const string appName = "My Application Name";
			const string plainText = "My Secret in Plain Text";
			const string protectorName = "My Protector";

			var provider = DataProtectionProvider.Create(appName);
			var protector = provider.CreateProtector(protectorName);
		
			WriteLine($"Encrypting the plain text: {plainText}");
			var cipherText = protector.Protect(plainText);
			WriteLine($"Encrypted to Cipher Text: {cipherText}");

			// Decrypt the variable
			string secret = protector.Unprotect(cipherText);
			WriteLine($"Complete: plainText={plainText} decryptedText: {secret}");
		}

		public static void WriteLine(string line)
		{
			Console.WriteLine(line);
		}
	}
}