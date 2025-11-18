using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System.Text;

namespace Library.Services.Authentication {


    /// <summary>
    /// Classe para geração de hash de senha com base no algoritmo Argon2.
    /// </summary>
    /// <remarks>
    /// Constructor da classe.
    /// </remarks>
    /// <param name="argon2Params">Parâmetros para o algoritmo Argon2.</param>
    public class Argon2Service(Argon2Params argon2Params) : IPasswordService {


        /// <summary> Parâmetros para o algoritmo Argon2. </summary>
        private readonly Argon2Params _argon2Params = argon2Params;

        /// <summary> Comprimento do hash gerado. </summary>
        private const int HASH_LENGTH = 32;


        /// <summary>
        /// Gerar o SALT para o algoritmo Argon2.
        /// </summary>
        /// <param name="length">comprimento do SALT a ser gerado.</param>
        /// <returns>SALT com o comprimento definido</returns>
        private byte[] GenerateSalt(int length) {
            var random = new SecureRandom();
            var salt = new byte[length];
            random.NextBytes(salt);
            return salt;
        }


        /// <summary>
        /// Gerar o hash da senha passada como parâmetro. Neste caso, o arranjo retornado é
        /// composto pelos seguintes campos:
        /// 
        /// <br/><br/>
        /// 
        /// 1. SALT: Salt gerado para a codificação da senha.
        /// 
        /// <br/><br/>
        /// 
        /// 2. PARALLELISM: número de threads.
        /// 
        /// <br/><br/>
        /// 
        /// 3. MEMORY: valor de memória alocada.
        /// 
        /// <br/><br/>
        /// 
        /// 4. ITERATIONS: número de interações.
        /// 
        /// <br/><br/>
        /// 
        /// 3. HASH: bytes calculados do hash.
        /// 
        /// </summary>
        /// <param name="password">Senha a ter o hash gerado</param>
        /// <returns>Arranjo contendo o SALT, PARALLELISM, MEMORY, ITERATIONS e HASH da
        /// senha, nesta ordem.</returns>
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
                .. salt,                // 16 bytes
                .. parallelismArray,    // 4 bytes
                .. memoryPowOfTwoArray, // 4 bytes
                .. iterationsArray,     // 4 bytes
                .. hash                 // 32 bytes
            ];
            
        }


        /// <summary>
        /// Verificar se a senha passada é a mesma que foi gerado o hash gravado no banco
        /// de dados.
        /// </summary>
        /// <param name="password">Senha a ser comparada.</param>
        /// <param name="passwordHash">Arranjo contendo o SALT, PARALLELISM, MEMORY, ITERATIONS
        /// e HASH da senha, nesta ordem.</param>
        /// <returns>True, caso a senha seja a mesma que gerou o hash. False, caso o contrário</returns>
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