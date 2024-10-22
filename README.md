# Support-for-PEM-encoded-X25519-and-Ed25519-keys-in-.NET-Framework-using-BouncyCastle
PEM support for .NET Framework 

Generation of an X25519 and Ed25519 key pair, export and import of PEM encoded private (in PKCS#8 format) and public keys (in SPKI format), key agreement and signing/verifying using the BouncyCastle classes. 

Tested with .NET Framework 4.8 and Portable.BouncyCastle 1.9.0

Sample output:

```none
-----BEGIN PRIVATE KEY-----
MFECAQEwBQYDK2VuBCIEIDi3T0yS32G/0CA4B1nd6pj7c2fb2Rlm6K6GRz7LorZi
gSEAwE4bzGUDkikAzWmvluG103JGX703wF6j/CkNDqPdQQA=
-----END PRIVATE KEY-----

-----BEGIN PUBLIC KEY-----
MCowBQYDK2VuAyEAwE4bzGUDkikAzWmvluG103JGX703wF6j/CkNDqPdQQA=
-----END PUBLIC KEY-----

-----BEGIN PRIVATE KEY-----
MFECAQEwBQYDK2VwBCIEILTAs44LQCK7ycl0kiO400fVAd8bDhT7zcPxwAfrcsUk
gSEA066agTAwVN/I8P05U0NSvWjeLKQ1mlvKMqvDBrQHe5c=
-----END PRIVATE KEY-----

-----BEGIN PUBLIC KEY-----
MCowBQYDK2VwAyEA066agTAwVN/I8P05U0NSvWjeLKQ1mlvKMqvDBrQHe5c=
-----END PUBLIC KEY-----

4f1cb5b361870670b29069e2bb17d03fa18b9512243c6dce51d23a384e2e6036
4f1cb5b361870670b29069e2bb17d03fa18b9512243c6dce51d23a384e2e6036

18c7dac1487d1a42abbda10ae9cdc36dfa7e7d2cf740a9896f6e792caf91079a62f51ef8a8e6c6a315410b8f88afe36faef90f875a557c8795e54e974af49105
True
```
