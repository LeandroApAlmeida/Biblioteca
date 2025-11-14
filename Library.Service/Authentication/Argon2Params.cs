namespace Library.Services.Authentication {


    /// <summary>
    /// Classe que representa os parâmetros para o algoritmo Argon2.
    /// </summary>
    /// <param name="iterations">Número de interações.</param>
    /// <param name="memoryPowOfTwo">Memória alocada.</param>
    /// <param name="parallelism">Número de threads.</param>
    public class Argon2Params(int iterations, int memoryPowOfTwo, int parallelism) {

        /// <summary> Número de interações. </summary>
        public int Iterations { get; } = iterations;

        /// <summary> Memória alocada. </summary>
        public int MemoryPowOfTwo { get; } = memoryPowOfTwo;

        /// <summary> Número de threads. </summary>
        public int Parallelism { get; } = parallelism;

    }


}