import { formatToDatetimeLocal } from "./module-datetime.js"


document.addEventListener('DOMContentLoaded', function () {


    var returnDate = document.getElementById("return-date");

    returnDate.focus();

    returnDate.value = formatToDatetimeLocal(new Date());


    document.getElementById('return-book-form').addEventListener('submit', function (event) {

        document.getElementById('spinner').style.display = 'block';
        document.body.style.overflow = 'hidden';

        document.getElementById('img-preview').setAttribute('readonly', 'readonly');
        document.getElementById('return-date').setAttribute('readonly', 'readonly');
        document.getElementById('notes').setAttribute('readonly', 'readonly');

        document.getElementById('save-button').disabled = true;
        document.getElementById('cancel-button').classList.add('disabled');

    });


    document.addEventListener('keydown', function (event) {
        if (event.ctrlKey && event.key.toLowerCase() === 's') {
            event.preventDefault();
            document.getElementById('save-button').click();
        } else if (event.key === 'Escape') {
            event.preventDefault();
            document.getElementById('cancel-button').click();
        }
    });


});