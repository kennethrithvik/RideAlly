using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Util
{
    
        public class SaltHash
        {

            public static string ComputeHash(string plaintext, string hashalgo, byte[] saltbyte)
            {
                if (saltbyte == null)
                {
                    Random rand = new Random();
                    int saltsize = rand.Next(2, 10);
                    saltbyte = new byte[saltsize];
                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                    rng.GetNonZeroBytes(saltbyte);
                }
                byte[] plaintextbytes = Encoding.UTF8.GetBytes(plaintext);
                byte[] plaintextsaltbytes = new byte[plaintextbytes.Length + saltbyte.Length];
                for (int i = 0; i < plaintextbytes.Length; i++)
                {
                    plaintextsaltbytes[i] = plaintextbytes[i];
                }
                for (int i = 0; i < saltbyte.Length; i++)
                {
                    plaintextsaltbytes[i + plaintextbytes.Length] = saltbyte[i];
                }


                HashAlgorithm hash;
                if (hashalgo == null)
                    hashalgo = "";

                switch (hashalgo.ToUpper())
                {
                    case "SHA1":
                        hash = new SHA1Managed();
                        break;

                    case "SHA256":
                        hash = new SHA256Managed();
                        break;

                    case "SHA384":
                        hash = new SHA384Managed();
                        break;

                    case "SHA512":
                        hash = new SHA512Managed();
                        break;

                    default:
                        hash = new MD5CryptoServiceProvider();
                        break;
                }

                byte[] hashbytes = hash.ComputeHash(plaintextsaltbytes);

                byte[] hashWithSaltBytes = new byte[hashbytes.Length +
                                                    saltbyte.Length];

                for (int i = 0; i < hashbytes.Length; i++)
                    hashWithSaltBytes[i] = hashbytes[i];

                for (int i = 0; i < saltbyte.Length; i++)
                    hashWithSaltBytes[hashbytes.Length + i] = saltbyte[i];

                string hashValue = Convert.ToBase64String(hashWithSaltBytes);

                return hashValue;
            }


            public static bool VerifyHash(string plainText, string hashAlgorithm, string hashValue)
            {
                // Convert base64-encoded hash value into a byte array.
                byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

                // We must know size of hash (without salt).
                int hashSizeInBits, hashSizeInBytes;

                // Make sure that hashing algorithm name is specified.
                if (hashAlgorithm == null)
                    hashAlgorithm = "";

                // Size of hash is based on the specified algorithm.
                switch (hashAlgorithm.ToUpper())
                {
                    case "SHA1":
                        hashSizeInBits = 160;
                        break;

                    case "SHA256":
                        hashSizeInBits = 256;
                        break;

                    case "SHA384":
                        hashSizeInBits = 384;
                        break;

                    case "SHA512":
                        hashSizeInBits = 512;
                        break;

                    default: // Must be MD5
                        hashSizeInBits = 128;
                        break;
                }

                // Convert size of hash from bits to bytes.
                hashSizeInBytes = hashSizeInBits / 8;

                // Make sure that the specified hash value is long enough.
                if (hashWithSaltBytes.Length < hashSizeInBytes)
                    return false;

                // Allocate array to hold original salt bytes retrieved from hash.
                byte[] saltBytes = new byte[hashWithSaltBytes.Length -
                                            hashSizeInBytes];

                // Copy salt from the end of the hash to the new array.
                for (int i = 0; i < saltBytes.Length; i++)
                    saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

                // Compute a new hash string.
                string expectedHashString =
                            ComputeHash(plainText, hashAlgorithm, saltBytes);

                // If the computed hash matches the specified hash,
                // the plain text value must be correct.
                return (hashValue == expectedHashString);
            }
        }
  
}
