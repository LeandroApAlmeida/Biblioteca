using System.Xml.Linq;

namespace Library.Services.Authentication {


    public class Argon2Reader(string filePath) {


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