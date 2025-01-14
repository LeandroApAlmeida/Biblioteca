namespace Library.Services.PasswordService {


    public class Argon2Params(int iterations, int memoryPowOfTwo, int parallelism) {

        public int Iterations { get; } = iterations;

        public int MemoryPowOfTwo { get; } = memoryPowOfTwo;

        public int Parallelism { get; } = parallelism;

    }


}