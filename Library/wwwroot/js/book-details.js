
/*
    Tratador de eventos da página para Detalhes do Livro (~\Views\Book\Details.cshtml).
*/


document.addEventListener('DOMContentLoaded', function () {

    document.addEventListener('keydown', function (event) {

        const dialog = document.getElementById('filter-dialog');

        if (!dialog.classList.contains('show')) {

            if (event.key === 'ArrowRight') {
                event.preventDefault();
                document.getElementById('button-next').click();
            } else if (event.key === 'ArrowLeft') {
                event.preventDefault();
                document.getElementById('button-previous').click();
            } else if (event.key === 'ArrowUp') {
                event.preventDefault();
                document.getElementById('button-first').click();
            } else if (event.key === 'ArrowDown') {
                event.preventDefault();
                document.getElementById('button-last').click();
            } else if (event.ctrlKey && event.key.toLowerCase() === 'f') {
                event.preventDefault();
                document.getElementById('button-filter').click();
            } else if (event.ctrlKey && event.shiftKey && event.key.toLowerCase() === 'p') {
                event.preventDefault();
                document.getElementById('button-print-collection').click();
            } else if (event.ctrlKey && event.key.toLowerCase() === 'p') {
                event.preventDefault();
                document.getElementById('button-print').click();
            }

        }

    });

});