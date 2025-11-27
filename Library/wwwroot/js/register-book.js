
/**
 * Tratador de eventos da página de cadastro de um livro (~\Views\Book\Register.cshtml).
 */


import { formatToDatetimeLocal } from "./module-datetime.js"
import { setCoverData } from "./module-cover-data.js"


document.addEventListener('DOMContentLoaded', function () {


    // Mover o foco para o componente Título.
    document.getElementById('title').focus();


    // Atribui valores default nos campos da página.
    document.getElementById("acquisition-date").value = formatToDatetimeLocal(new Date());
    document.getElementById('edition').value = 1;
    document.getElementById('volume').value = 1;
    document.getElementById('release-year').value = 2000;
    document.getElementById('num-of-pages').value = 1;


    /**
     * Evento disparado ao selecionar o arquivo da capa na página.
     */
    document.getElementById("choose-file").addEventListener("change", function (event) {

        // Campo de exibição da imagem.
        const imgPreview = document.getElementById("img-preview");

        // Campo oculto.
        const imgData = document.getElementById("cover-data");

        // Converte o arquivo da capa em string base 64 e atribui aos campos da página.
        setCoverData(event.target.files[0], imgPreview, imgData);

    });


    /**
     * Evento disparado ao submeter o form. Neste caso, desabilita os campos de edição, os
     * botões e exibe o spinner, mostrando que há um processo em execução.
     */
    document.getElementById('book-form').addEventListener('submit', function (event) {

        // Exibe o spinner.
        document.getElementById('spinner').style.display = 'block';
        document.body.style.overflow = 'hidden';

        // Desabilita os campos da página.
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

        // Desabilita os botões da página.
        document.getElementById('save-button').disabled = true;
        document.getElementById('cancel-button').classList.add('disabled');

    });


    /**
     * Evento disparado ao pressionar alguma tecla/atalho de teclado. As teclas tratadas são:
     * 
     * CTRL + S: Salvar os campos da página.
     * 
     * ESC: Cancelar a edição.
     */
    document.addEventListener('keydown', function (event) {

        if (event.ctrlKey && event.key.toLowerCase() === 's') {
            // CTRL + S
            event.preventDefault();
            document.getElementById('save-button').click();
        } else if (event.key === 'Escape') {
            // ESC
            event.preventDefault();
            document.getElementById('cancel-button').click();
        }

    });


});