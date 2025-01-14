using System.Xml.Linq;

namespace Library.Services.PasswordService {


    public class Argon2FileReader(string filePath) {


        private XDocument xdoc = XDocument.Load(filePath);


        public Argon2Params Read() {

            int iterations = (int) xdoc.Root.Element("Iterations");

            int memoryPowOfTwo = (int) xdoc.Root.Element("MemoryPowOfTwo");

            int parallelism = (int) xdoc.Root.Element("Parallelism");

            return new Argon2Params(iterations, memoryPowOfTwo, parallelism);

        }


    }


}