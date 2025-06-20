using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System.Text;

namespace Library.Services.Authentication {


    public class Argon2Service : IPasswordService {


        private readonly Argon2Params _argon2Params;

        private const int HASH_LENGTH = 32;


        public Argon2Service(Argon2Params argon2Params) {
            _argon2Params = argon2Params;
        }


        private byte[] GenerateSalt(int length) {
            var random = new SecureRandom();
            var salt = new byte[length];
            random.NextBytes(salt);
            return salt;
        }


        public byte[] GeneratePasswordHash(string password) {

            byte[] salt = GenerateSalt(16);

            int parallelism = _argon2Params.Parallelism;
            int memoryPowOfTwo = _argon2Params.MemoryPowOfTwo;
            int iterations = _argon2Params.Iterations;

            Argon2BytesGenerator argon2 = new Argon2BytesGenerator();
            
            Argon2Parameters.Builder builder = new Argon2Parameters
            .Builder(Argon2Parameters.Argon2id)
            .WithSalt(salt)
            .WithParallelism(parallelism)
            .WithMemoryPowOfTwo(memoryPowOfTwo)
            .WithIterations(iterations); 
            
            Argon2Parameters parameters = builder.Build(); 
            
            argon2.Init(parameters);

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            
            byte[] hash = new byte[HASH_LENGTH];

            argon2.GenerateBytes(passwordBytes, hash);

            byte[] parallelismArray = BitConverter.GetBytes(parallelism);
            byte[] memoryPowOfTwoArray = BitConverter.GetBytes(memoryPowOfTwo);
            byte[] iterationsArray = BitConverter.GetBytes(iterations);

            return [
                .. salt,
                .. parallelismArray,
                .. memoryPowOfTwoArray,
                .. iterationsArray,
                .. hash
            ];
            
        }


        public bool IsTheSamePassword(string password, byte[] passwordHash) {

            byte[] salt = new byte[16];
            byte[] parallelismArray = new byte[4];
            byte[] memoryPowOfTwoArray = new byte[4];
            byte[] iterationsArray = new byte[4];
            byte[] hash = new byte[HASH_LENGTH]; 
            
            Array.Copy(passwordHash, 0, salt, 0, 16);
            Array.Copy(passwordHash, 16, parallelismArray, 0, 4);
            Array.Copy(passwordHash, 20, memoryPowOfTwoArray, 0, 4);
            Array.Copy(passwordHash, 24, iterationsArray, 0, 4);
            Array.Copy(passwordHash, 28, hash, 0, HASH_LENGTH);

            int parallelism = BitConverter.ToInt32(parallelismArray, 0);
            int memoryPowOfTwo = BitConverter.ToInt32(memoryPowOfTwoArray, 0);
            int iterations = BitConverter.ToInt32(iterationsArray, 0);

            Argon2BytesGenerator argon2 = new Argon2BytesGenerator();
            
            Argon2Parameters.Builder builder = new Argon2Parameters
            .Builder(Argon2Parameters.Argon2id)
            .WithSalt(salt)
            .WithParallelism(parallelism)
            .WithMemoryPowOfTwo(memoryPowOfTwo)
            .WithIterations(iterations);

            Argon2Parameters parameters = builder.Build();

            argon2.Init(parameters);

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            byte[] newHash = new byte[HASH_LENGTH];

            argon2.GenerateBytes(passwordBytes, newHash);

            return hash.SequenceEqual(newHash);

        }


    }


}