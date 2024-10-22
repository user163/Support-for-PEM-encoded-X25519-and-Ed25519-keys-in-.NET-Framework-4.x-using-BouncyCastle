using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities.IO.Pem;
using OpenSSL = Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.Crypto.Generators;

internal class Program
{
    static void Main(string[] args)
    {
        // Test 1a: Create X25519 key pair
        (X25519PrivateKeyParameters privateX25519Key, X25519PublicKeyParameters publicX25519Key) = CreateX25519KeyPair();

        // Test 1b: Create Ed25519 key pair
        (Ed25519PrivateKeyParameters privateEd25519Key, Ed25519PublicKeyParameters publicEd25519Key) = CreateEd25519KeyPair();

        // Test 2a: Export PEM encoded private X25519 key in PKCS#8 format, export PEM encoded public X25519 key in SPKI format
        string privateX25519Pkcs8Pem = ExportPrivateAsPkcs8Pem(privateX25519Key);
        string publicX25519SpkiPem = ExportPublicAsSpkiPem(publicX25519Key);
        Console.WriteLine(privateX25519Pkcs8Pem);
        Console.WriteLine(publicX25519SpkiPem);

        // Test 2b: Export PEM encoded private Ed25519 key in PKCS#8 format, export PEM encoded public Ed25519 key in SPKI format
        string privateEd25519Pkcs8Pem = ExportPrivateAsPkcs8Pem(privateEd25519Key);
        string publicEd25519SpkiPem = ExportPublicAsSpkiPem(publicEd25519Key);
        Console.WriteLine(privateEd25519Pkcs8Pem);
        Console.WriteLine(publicEd25519SpkiPem);

        // Test 3a: Import PEM encoded private X25519 key from PKCS#8 format, import PEM encoded public X25519 key from SPKI format
        X25519PrivateKeyParameters privateX25519KeyReloaded1 = (X25519PrivateKeyParameters)ImportPrivateFromPkcs8Pem(privateX25519Pkcs8Pem);
        X25519PublicKeyParameters publicX25519KeyReloaded1 = (X25519PublicKeyParameters)ImportPublicFromSpkiPem(publicX25519SpkiPem);

        // Test 3b: Import PEM encoded private Ed25519 key from PKCS#8 format, import PEM encoded public Ed25519 key from SPKI format
        Ed25519PrivateKeyParameters privateEd25519KeyReloaded1 = (Ed25519PrivateKeyParameters)ImportPrivateFromPkcs8Pem(privateEd25519Pkcs8Pem);
        Ed25519PublicKeyParameters publicEd25519KeyReloaded1 = (Ed25519PublicKeyParameters)ImportPublicFromSpkiPem(publicEd25519SpkiPem);

        // Test 4: X25519 key agreement
        (X25519PrivateKeyParameters privateX25519KeyOtherSide, X25519PublicKeyParameters publicX25519KeyOtherSide) = CreateX25519KeyPair();
        byte[] sharedSecret = CreateX25519SharedSecret(privateX25519KeyReloaded1, publicX25519KeyOtherSide);
        byte[] sharedSecretOtherSide = CreateX25519SharedSecret(privateX25519KeyOtherSide, publicX25519KeyReloaded1);
        Console.WriteLine(Hex.ToHexString(sharedSecret));
        Console.WriteLine(Hex.ToHexString(sharedSecretOtherSide));
        Console.WriteLine();

        // Test 5: Ed25519 signing/verifying
        byte[] message = Encoding.UTF8.GetBytes("The quick brown fox jumps ove rthe lazy dog");
        byte[] signature = Ed25519Sign(message, privateEd25519KeyReloaded1);
        bool verified = Ed25519Verify(message, signature, publicEd25519KeyReloaded1);
        Console.WriteLine(Hex.ToHexString(signature));
        Console.WriteLine(verified);
    }

    public static (X25519PrivateKeyParameters privateX25519, X25519PublicKeyParameters publicX25519) CreateX25519KeyPair()
    {
        X25519KeyPairGenerator x25519KeyPairGenerator = new X25519KeyPairGenerator();
        x25519KeyPairGenerator.Init(new X25519KeyGenerationParameters(new SecureRandom()));
        AsymmetricCipherKeyPair keyPair = x25519KeyPairGenerator.GenerateKeyPair();
        return (keyPair.Private as X25519PrivateKeyParameters, keyPair.Public as X25519PublicKeyParameters);
    }

    public static (Ed25519PrivateKeyParameters privateEd25519, Ed25519PublicKeyParameters publicEd25519) CreateEd25519KeyPair()
    {
        Ed25519KeyPairGenerator ed25519KeyPairGenerator = new Ed25519KeyPairGenerator();
        ed25519KeyPairGenerator.Init(new Ed25519KeyGenerationParameters(new SecureRandom()));
        AsymmetricCipherKeyPair keyPair = ed25519KeyPairGenerator.GenerateKeyPair();
        return (keyPair.Private as Ed25519PrivateKeyParameters, keyPair.Public as Ed25519PublicKeyParameters);
    }

    public static string ExportPrivateAsPkcs8Pem(AsymmetricKeyParameter privateKey)
    {
        OpenSSL.Pkcs8Generator pkcs8Generator = new OpenSSL.Pkcs8Generator(privateKey);
        PemObject pemObjectPkcs8 = pkcs8Generator.Generate();
        OpenSSL.PemWriter pemWriter = new OpenSSL.PemWriter(new StringWriter());
        pemWriter.WriteObject(pemObjectPkcs8);
        return pemWriter.Writer.ToString();
    }

    public static string ExportPublicAsSpkiPem(AsymmetricKeyParameter publicKey)
    {
        OpenSSL.PemWriter pemWriter = new OpenSSL.PemWriter(new StringWriter());
        pemWriter.WriteObject(publicKey);
        return pemWriter.Writer.ToString();
    }

    public static AsymmetricKeyParameter ImportPrivateFromPkcs8Pem(string privatePkcs8Pem)
    {
        OpenSSL.PemReader pemReader = new OpenSSL.PemReader(new StringReader(privatePkcs8Pem));
        return (AsymmetricKeyParameter)pemReader.ReadObject();
    }

    public static AsymmetricKeyParameter ImportPublicFromSpkiPem(string publicSpkiPem)
    {
        OpenSSL.PemReader pemReader = new OpenSSL.PemReader(new StringReader(publicSpkiPem));
        return (AsymmetricKeyParameter)pemReader.ReadObject();
    }

    public static byte[] CreateX25519SharedSecret(X25519PrivateKeyParameters privateX25519Key, X25519PublicKeyParameters publicX25519KeyOtherSide)
    {
        byte[] sharedSecret = new byte[32];
        privateX25519Key.GenerateSecret(publicX25519KeyOtherSide, sharedSecret, 0);
        return sharedSecret;
    }

    private static ISigner Ed25519SignVerify(bool isSigner, byte[] msg, AsymmetricKeyParameter key)
    {
        ISigner signerVerifier = SignerUtilities.GetSigner("Ed25519");
        signerVerifier.Init(isSigner, key);
        signerVerifier.BlockUpdate(msg, 0, msg.Length);
        return signerVerifier;
    }

    public static byte[] Ed25519Sign(byte[] msg, Ed25519PrivateKeyParameters key)
    {
        return Ed25519SignVerify(true, msg, key).GenerateSignature();
    }

    public static bool Ed25519Verify(byte[] msg, byte[] signature, Ed25519PublicKeyParameters key)
    {
        return Ed25519SignVerify(false, msg, key).VerifySignature(signature);
    }
}
