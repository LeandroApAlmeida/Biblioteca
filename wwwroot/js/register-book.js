// Tratador de eventos da página "RegisterBook.cshtml". Neste caso, atribui todas
// as funcionalidades para que a função de carregar uma imagem de capa na página
// esteja disponível.


import { formatToDatetimeLocal } from "./utils.js"
import { setCoverData } from "./data-cover.js"


// Formata a data atual para opção defaul no campo acquisition-date.
document.getElementById("acquisition-date").value = formatToDatetimeLocal(new Date());

document.getElementById('edition').value = 1;
document.getElementById('volume').value = 1;
document.getElementById('release-year').value = 2000;
document.getElementById('num-of-pages').value = 1;

// Caixa de diálogo para localizar o arquivo de capa no navegador.
const chooseFile = document.getElementById("choose-file");


// Atribui o tratador de evento para que quando o arquivo é selecionado, se faça o
// processamento do mesmo.
chooseFile.addEventListener("change", function () {

    const imgPreview = document.getElementById("img-preview");
    const imgData = document.getElementById("cover-data");

    setCoverData(chooseFile.files[0], imgPreview, imgData);

});


document.getElementById('book-form').addEventListener('submit', function (event) {

    document.getElementById('title').setAttribute('readonly', 'readonly');
    document.getElementById('subtitle').setAttribute('readonly', 'readonly');
    document.getElementById('author').setAttribute('readonly', 'readonly');
    document.getElementById('publisher').setAttribute('readonly', 'readonly');
    document.getElementById('isbn').setAttribute('readonly', 'readonly');
    document.getElementById('edition').setAttribute('readonly', 'readonly');
    document.getElementById('volume').setAttribute('readonly', 'readonly');
    document.getElementById('release-year').setAttribute('readonly', 'readonly');
    document.getElementById('num-of-pages').setAttribute('readonly', 'readonly');
    document.getElementById('acquisition-date').setAttribute('readonly', 'readonly');
    document.getElementById('summary').setAttribute('readonly', 'readonly');
    document.getElementById('choose-file').disabled = true;

    document.getElementById('save-button').disabled = true;
    document.getElementById('cancel-button').classList.add('disabled');

});