using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System.Text;

namespace Library.Services.PasswordService {


    public class PasswordService : IPasswordService {


        private const int PARALLELISM = 8;
        
        private const int MEMORY_POW_OF_TWO = 13;
        
        private const int ITERATIONS = 4;
        
        private const int HASH_LENGTH = 32;
         

        public static byte[] GenerateSalt(int length) {
            var random = new SecureRandom();
            var salt = new byte[length];
            random.NextBytes(salt);
            return salt;
        }


        public byte[] GeneratePasswordHash(string password) {

            byte[] salt = GenerateSalt(16);

            Argon2BytesGenerator argon2 = new Argon2BytesGenerator();
            Argon2Parameters.Builder builder = new Argon2Parameters
            .Builder(Argon2Parameters.Argon2id)
            .WithSalt(salt)
            .WithParallelism(PARALLELISM)
            .WithMemoryPowOfTwo(MEMORY_POW_OF_TWO)
            .WithIterations(ITERATIONS); 
            
            Argon2Parameters parameters = builder.Build(); 
            
            argon2.Init(parameters);

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            
            byte[] hash = new byte[HASH_LENGTH];

            argon2.GenerateBytes(passwordBytes, hash);

            return [.. salt, .. hash];
            
        }

        public bool IsItTheSamePassword(string password, byte[] passwordHash) {

            byte[] salt = passwordHash.Take(16).ToArray();
            byte[] hash = passwordHash.Skip(16).Take(32).ToArray();


            Argon2BytesGenerator argon2 = new Argon2BytesGenerator();
            Argon2Parameters.Builder builder = new Argon2Parameters
            .Builder(Argon2Parameters.Argon2id)
            .WithSalt(salt)
            .WithParallelism(PARALLELISM)
            .WithMemoryPowOfTwo(MEMORY_POW_OF_TWO)
            .WithIterations(ITERATIONS);

            Argon2Parameters parameters = builder.Build();

            argon2.Init(parameters);

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            byte[] newHash = new byte[HASH_LENGTH];

            argon2.GenerateBytes(passwordBytes, newHash);

            return hash.SequenceEqual(newHash);

        }

    }


}