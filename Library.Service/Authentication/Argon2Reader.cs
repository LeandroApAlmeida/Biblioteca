using System.Xml.Linq;

namespace Library.Services.Authentication {

    /// <summary>
    /// Classe para leitura dos parâmetros do algoritmo Argon2 a partir do arquivo XML.
    /// </summary>
    /// <param name="filePath">Path do arquivo XML com as configurações do Argon2.</param>
    public class Argon2Reader(string filePath) {


        /// <summary>
        /// Ler o arquivo XML para obtenção dos parâmetros para o algoritmo Argon2.
        /// </summary>
        /// <param name="defaultIterations">Número default de interações.</param>
        /// <param name="defaultMemoryPowOfTwo">Valor default de memória alocada.</param>
        /// <param name="defaultParallelism">Número default de threads.</param>
        /// <returns>Parâmetros para configuração do algoritmo Argon2.</returns>
        public Argon2Params Read(int defaultIterations, int defaultMemoryPowOfTwo,
        int defaultParallelism) {

            try {

                XDocument xDocument = XDocument.Load(filePath);

                XElement? xRoot = xDocument.Root;

                if (xRoot != null) {

                    XElement? xIterations = xRoot.Element("Iterations");
                    XElement? xMemPowOfTwo = xRoot.Element("MemoryPowOfTwo");
                    XElement? xParallelism = xRoot.Element("Parallelism");

                    int iterations = xIterations != null ? (int) xIterations : defaultIterations;
                    int memoryPowOfTwo = xMemPowOfTwo != null ? (int) xMemPowOfTwo : defaultMemoryPowOfTwo;
                    int parallelism = xParallelism != null ? (int) xParallelism : defaultParallelism;

                    return new Argon2Params(
                        iterations,
                        memoryPowOfTwo,
                        parallelism
                    );

                } else {

                    return new Argon2Params(
                        defaultIterations,
                        defaultMemoryPowOfTwo,
                        defaultParallelism
                    );

                }

            } catch {

                return new Argon2Params(
                    defaultIterations,
                    defaultMemoryPowOfTwo,
                    defaultParallelism
                );

            }

        }


    }


}