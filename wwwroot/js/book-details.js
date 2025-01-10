// Tratador de eventos da página "BookDetails.cshtml". Neste caso, apenas codifica
// a ação de abrir e fechar o diálogo que exibe a sinopse do livro em exibição.


// Diálogo exibido ao clicar no botão "Ver sinopse".
const dialog = document.getElementById("summary-dialog");


// Exibir o diálogo.
function showDialog() {
    dialog.showModal();
}


// Fechar o diálogo.
function closeDialog() {
    dialog.close();
}