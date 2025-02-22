
document.addEventListener('keydown', function (event) {

    const dialog = document.getElementById('filter-dialog');

    if (!dialog.classList.contains('show')) {

        event.preventDefault();

        if (event.key === 'ArrowRight') {
            document.getElementById('button-next').click();
        } else if (event.key === 'ArrowLeft') {
            document.getElementById('button-previous').click();
        } else if (event.key === 'ArrowUp') {
            document.getElementById('button-first').click();
        } else if (event.key === 'ArrowDown') {
            document.getElementById('button-last').click();
        } else if (event.ctrlKey && event.key.toLowerCase() === 'f') {
            document.getElementById('button-filter').click();
        } else if (event.ctrlKey && event.shiftKey && event.key.toLowerCase() === 'p') {
            document.getElementById('button-print-collection').click();
        } else if (event.ctrlKey && event.key.toLowerCase() === 'p') {
            document.getElementById('button-print').click();
        }

    }

});