
/**
 * Módulo para conversão do arquivo de imagem de capa selecionado na página em sua representação
 * no formato string base64, que será gravado no banco de dados.
 */


/**
 * Definir a capa do livro, de acordo com o arquivo selecionado na caixa de diálogo. Assim que o 
 * arquivo é selecionado, renderiza o bitmap no campo visível "imgPreview" da página e armazena a
 * string em formato base64 com os dados do arquivo no campo oculto "imgData". Esta string será 
 * gravada no banco de dados assim que o POST da página é realizado.
 * 
 * @param {any} file arquivo de capa selecionado.
 * @param {any} imgPreview componente para a renderização da imagem.
 * @param {any} imgData componente oculto com a string base64 da imagem.
 */
function setCoverData(file, imgPreview, imgData) {

    if (file) {

        const fileReader = new FileReader();

        fileReader.addEventListener("load", function () {

            imgPreview.style.display = "block";

            imgPreview.innerHTML = '<img src="' + this.result + '" class="center"/>';

            imgPreview.removeAttribute("hidden");

            imgData.value = this.result;

        });

        fileReader.readAsDataURL(file);

    }

}


export { setCoverData };