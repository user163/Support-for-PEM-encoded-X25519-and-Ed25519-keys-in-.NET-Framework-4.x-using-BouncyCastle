# Support-for-PEM-encoded-X25519-and-Ed25519-keys-in-.NET-Framework-using-BouncyCastle
PEM support for .NET Framework 

Generation of an X25519 and Ed25519 key pair, export and import of PEM encoded private (in PKCS#8 format) and public keys (in SPKI format), key agreement and signing/verifying using the BouncyCastle classes. 

Tested with .NET Framework 4.8 and Portable.BouncyCastle 1.9.0

Sample output:

```none
-----BEGIN PRIVATE KEY-----
MFECAQEwBQYDK2VuBCIEINCrXnYv85qCdGddocOsvVkqIM4tm3zhkMKCIBN+rWpe
gSEAmjUI4cbSKotAqepSvlh1A+d7RQpSxhgUyePRMXt4/Xw=
-----END PRIVATE KEY-----

-----BEGIN PUBLIC KEY-----
MCowBQYDK2VuAyEAmjUI4cbSKotAqepSvlh1A+d7RQpSxhgUyePRMXt4/Xw=
-----END PUBLIC KEY-----

-----BEGIN PRIVATE KEY-----
MFECAQEwBQYDK2VwBCIEIH/HlVvRZShDe62bBvP4r6HeydO4YguplYYwjJoGVFV+
gSEAKrziIbhf49n3H91u8odnacUo7bdKzRMOqLQ2ApWvkLU=
-----END PRIVATE KEY-----

-----BEGIN PUBLIC KEY-----
MCowBQYDK2VwAyEAKrziIbhf49n3H91u8odnacUo7bdKzRMOqLQ2ApWvkLU=
-----END PUBLIC KEY-----

-----BEGIN PUBLIC KEY-----
MCowBQYDK2VuAyEAmjUI4cbSKotAqepSvlh1A+d7RQpSxhgUyePRMXt4/Xw=
-----END PUBLIC KEY-----

-----BEGIN PUBLIC KEY-----
MCowBQYDK2VwAyEAKrziIbhf49n3H91u8odnacUo7bdKzRMOqLQ2ApWvkLU=
-----END PUBLIC KEY-----

a8f8134dfef761feae9ca7a1feccd4da9fb4a6f693df489f91e5c5b767a33610
a8f8134dfef761feae9ca7a1feccd4da9fb4a6f693df489f91e5c5b767a33610

0ecdcf35cab2bc2caed580b0e2f4a4614349b05354350a16e8a8decea1902c3a16ef0ad3a860f1d47e41d53fa96b275bebb3af8a0f89a740dc099d6a9f662109
True
```
