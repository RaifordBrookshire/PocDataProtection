# DataProtection for .NET Core

This repository contains a proof of concept for using Microsoft.AspNetCore.DataProtection to protect and unprotect strings in a .NET Core application. The proof of concept includes a wrapper class, `ProtectedString`, which encapsulates the lower-level calls to the DataProtection API and makes it easy to use.

## In Memory Data Risk and Threats
It is important to not store secret data, such as passwords or sensitive information, in the memory of an application because it increases the risk of the data being compromised through memory scraping or other malicious means. By using the `ProtectedString` class it can help mitigate this risk by encrypting the data in memory, making it much more difficult for an attacker to access it. Ideally, the encryption keys should also be protected, which adds an extra layer of security. By using the built-in data protection features of .NET C#, developers can easily encrypt and decrypt sensitive information in memory, providing a secure way to handle secret data in their applications.

## Getting Started

1. Clone the repository to your local machine.
2. Open the solution in Visual Studio.
3. Build and run the project.
4. You will see concole output with progress

## Using the Wrapper Class

The `ProtectedString` class has two methods, `Protect(ref string input)` and `Unprotect()`, which can be used to protect and unprotect strings, respectively. The Protect method must take an input as a ref type... This is because the passed in input memory will be Wiped and set to empty to ensure the security of the 
plainText.


Here is an example of how to use the class:

```c#
const string appName = "Default App";
const string protectorName = "Test Protector";			string plainText = "My Secret in Plain Text";

// Create protected string and also unprotect to read back	
var protector = new ProtectedString(appName, protectorName);
var cipherText = protector.Protect(ref plainText);
string secret = protector.Unprotect();

// Clean up the Memory... Wipe it clean
protector.WipeStringMemory(ref secret);

```
