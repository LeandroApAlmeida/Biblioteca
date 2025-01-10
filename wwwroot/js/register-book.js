// Tratador de eventos da página "RegisterBook.cshtml". Neste caso, atribui todas
// as funcionalidades para que a função de carregar uma imagem de capa na página
// esteja disponível.


import { formatToDatetimeLocal } from "./utils.js"


// Caixa de diálogo para localizar o arquivo de capa no navegador.
const chooseFile = document.getElementById("choose-file");

// Atribui o tratador de evento para que quando o arquivo é selecionado, se faça o
// processamento do mesmo.
chooseFile.addEventListener("change", function () {
    setCover(chooseFile.files[0]);
});


// Formata a data atual para opção defaul no campo acquisition-date.
document.getElementById("acquisition-date").value = formatToDatetimeLocal(new Date());



// Definir a capa do livro, de acordo com o arquivo selecionado na caixa de diálogo.
// Assim que o arquivo é selecionado, renderiza o bitmap no campo visível img-preview
// da página e armazena a string em formato base64 com os dados do arquivo no campo
// oculto cover-data. Esta string será gravada no banco de dados assim que o POST da
// página é realizado.
function setCover(file) {

    if (file) {

        const imgPreview = document.getElementById("img-preview");
        const imgData = document.getElementById("cover-data");

        const fileReader = new FileReader();

        fileReader.readAsDataURL(file);

        fileReader.addEventListener("load", function () {

            imgPreview.style.display = "block";

            imgPreview.innerHTML = '<img src="' + this.result + '" class="center"/>';

            imgPreview.removeAttribute("hidden");

            imgData.value = this.result;

        });

    }

}