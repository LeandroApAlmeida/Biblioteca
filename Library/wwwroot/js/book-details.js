
/**
 * Tratador de eventos da página para Detalhes do Livro (~\Views\Book\Details.cshtml).
 */


document.addEventListener('DOMContentLoaded', function () {


    /**
     * Este evento é disparado quando há o clique no botão "Selecionar Livro" no diálogo
     * "Buscar Livro". Ao fazer isto, carrega a página Detalhes do Livro passando o identificador
     * chave primária do livro selecionado no diálogo.
     */
    document.querySelectorAll(".btn-select-book").forEach(button => {

        button.addEventListener("click", function () {

            // Obtém o identificador chave primária do livro selecionado.
            let itemId = this.getAttribute("data-id");

            // Direciona para a página Detalhes do Livro.
            window.location.href = `/Book/Details?id=${itemId}`;         

        });

    });


    /**
     * Tratador de evento de tecla/Atalho de teclado na página Detalhes do Livro. As teclas tratadas
     * são:
     * 
     * Seta para direita: Clique no botão Ir para o próximo.
     * 
     * Seta para esquerda: Clique no botão Ir para o anterior.
     * 
     * Seta para cima: Clique no botão Ir para o primeiro.
     * 
     * Seta para baixo: Clique no botão Ir para o último.
     * 
     * CTRL + F: Clique no botão Buscar livro.
     * 
     * CTRL + P: Clique no botão Imprimir livro detalhado.
     * 
     * CTRL + SHIFT + P: Clique no botão Imprimir livros cadastrados.
     */
    document.addEventListener('keydown', function (event) {

        const dialog = document.getElementById('book-filter-dialog');

        if (!dialog.classList.contains('show')) {

            if (event.key === 'ArrowRight') {
                // Seta para a direita.
                event.preventDefault();
                document.getElementById('button-next').click();
            } else if (event.key === 'ArrowLeft') {
                // Seta para a esquerda.
                event.preventDefault();
                document.getElementById('button-previous').click();
            } else if (event.key === 'ArrowUp') {
                // Seta para cima.
                event.preventDefault();
                document.getElementById('button-first').click();
            } else if (event.key === 'ArrowDown') {
                // Seta para baixo.
                event.preventDefault();
                document.getElementById('button-last').click();
            } else if (event.ctrlKey && event.key.toLowerCase() === 'f') {
                // CTRL + F.
                event.preventDefault();
                document.getElementById('button-filter').click();
            } else if (event.ctrlKey && event.shiftKey && event.key.toLowerCase() === 'p') {
                // CTRL + SHIFT + P.
                event.preventDefault();
                document.getElementById('button-print-collection').click();
            } else if (event.ctrlKey && event.key.toLowerCase() === 'p') {
                // CTRL + P.
                event.preventDefault();
                document.getElementById('button-print').click();
            }

        }

    });


});