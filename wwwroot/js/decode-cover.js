
/* 

    Decodificador da imagem nas páginas que renderizam a miniatura da capa do livro. Nestas páginas, o
    artifício utilizado é o de ter dois campos para imagem. O campo img-preview vai renderizar o bitmap
    que será exibido na página. O campo cover-data contém a string em formato base64 da imagem recuperada
    do banco de dados que será decodificada para a renderização, e permanecerá oculto na página.

    Quando este javascript é executado, o que ele faz é simplesmente usar a função de decodificação
    de string base64 do próprio HTML para renderizar o conteúdo do campo oculto cover-data no campo
    visível img-preview.

*/


// Campo visível que renderiza a imagem da capa do livro.

const imgPreview = document.getElementById("img-preview");


// Campo oculto que contém a string base64 com os dados da imagem.

const imgData = document.getElementById("cover-data");


// Renderiza a imagem codificada em cover-data em img-preview, usando o decodificador de string base64
// do próprio HTML.

imgPreview.innerHTML = '<img src="' + imgData.value + '" class="center"/>';


// Remove o atributo oculto.

imgPreview.removeAttribute("hidden");