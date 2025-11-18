using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace Library.Services.Cover {


    /// <summary>
    /// Classe para conversão de formato da imagem de capa de um livro.
    /// </summary>
    public class CoverConverter {


        /// <summary>
        /// Criar a miniatura da capa do livro, com 90x120 pixels.
        /// </summary>
        /// <param name="imageBase64">Imagem de capa do livro, no formato string base64.</param>
        /// <returns>Miniatura da capa do livro, no formato string base64, com codificação JPEG.</returns>
        [SupportedOSPlatform("windows")]
        public string CreateThumbnail(string imageBase64) {

            string base64Data = imageBase64.Substring(imageBase64.IndexOf(",") + 1);
            byte[] imageData = Convert.FromBase64String(base64Data);

            using MemoryStream ms = new MemoryStream(imageData);
            using Image image = Image.FromStream(ms);
            using Bitmap thumbnail = new Bitmap(image, new Size(90, 120));
            using MemoryStream thumbStream = new MemoryStream();

            thumbnail.Save(thumbStream, ImageFormat.Jpeg);

            return $"data:image/jpeg;base64,{Convert.ToBase64String(thumbStream.ToArray())}";
        }


        /// <summary>
        /// Codificar a imagem de capa, em formato string base64, para o padrão JPEG, de forma
        /// a otimizar a imagem para o armazenamento no banco de dados.
        /// </summary>
        /// <param name="base64Image">Imagem de capa no formato string base64.</param>
        /// <returns>Imagem de capa no formato string base64 codificada como JPEG.</returns>
        [SupportedOSPlatform("windows")]
        public string ConvertToJpeg(string base64Image) {

            if (!base64Image.StartsWith("data:image/jpeg;base64")) {

                string base64Data = base64Image.Substring(base64Image.IndexOf(",") + 1);
                byte[] imageData = Convert.FromBase64String(base64Data);

                using MemoryStream ms = new MemoryStream(imageData);
                using Image image = Image.FromStream(ms);
                using MemoryStream jpgStream = new MemoryStream();

                image.Save(jpgStream, ImageFormat.Jpeg);

                return $"data:image/jpeg;base64,{Convert.ToBase64String(jpgStream.ToArray())}";

            } else {

                return base64Image;

            }

        }


    }


}