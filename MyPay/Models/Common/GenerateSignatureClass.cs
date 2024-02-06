using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System; 
using System.IO; 
using System.Security.Cryptography;
using System.Text; 

public class GenerateSignatureClass
{

	public static void main()
	{
		string messageToSign = "Payload JSON";
		string privateKeyString = "--- MERCHANT PRIVATE KEY  ---";
		RSACryptoServiceProvider privateKeyParameter = ImportPrivateKey(privateKeyString);
		RSAParameters privateKey = privateKeyParameter.ExportParameters(true);
		string signedMessage = SignData(messageToSign, privateKey);

	}

	public static RSACryptoServiceProvider ImportPrivateKey(string pem)
	{
		PemReader pr = new PemReader(new StringReader(pem));
		AsymmetricCipherKeyPair KeyPair = (AsymmetricCipherKeyPair)pr.ReadObject();
		RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)KeyPair.Private);

		RSACryptoServiceProvider csp = new RSACryptoServiceProvider();// cspParams);
		csp.ImportParameters(rsaParams);
		return csp;
	}


	public static string SignData(string message, RSAParameters privateKey)
	{
		//// The array to store the signed message in bytes
		byte[] signedBytes;
		using (var rsa = new RSACryptoServiceProvider())
		{
			//// Write the message to a byte array using UTF8 as the encoding.
			var encoder = new UTF8Encoding();
			byte[] originalData = encoder.GetBytes(message);

			try
			{
				//// Import the private key used for signing the message
				rsa.ImportParameters(privateKey);

				//// Sign the data, using SHA256 as the hashing algorithm 
				signedBytes = rsa.SignData(originalData, CryptoConfig.MapNameToOID("SHA256"));
			}
			catch (CryptographicException e)
			{
				Console.WriteLine(e.Message);
				return null;
			}
			finally
			{
				//// Set the keycontainer to be cleared when rsa is garbage collected.
				rsa.PersistKeyInCsp = false;
			}
		}
		//// Convert the a base64 string before returning
		return Convert.ToBase64String(signedBytes);
	}

}