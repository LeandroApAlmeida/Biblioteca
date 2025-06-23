
document.addEventListener('DOMContentLoaded', function () {


    document.getElementById('name').focus();


    document.getElementById('person-form').addEventListener('submit', function (event) {

        document.getElementById('spinner').style.display = 'block';
        document.body.style.overflow = 'hidden';

        document.getElementById('name').setAttribute('readonly', 'readonly');
        document.getElementById('street').setAttribute('readonly', 'readonly');
        document.getElementById('number').setAttribute('readonly', 'readonly');
        document.getElementById('complement').setAttribute('readonly', 'readonly');
        document.getElementById('district').setAttribute('readonly', 'readonly');
        document.getElementById('postal-code').setAttribute('readonly', 'readonly');
        document.getElementById('city').setAttribute('readonly', 'readonly');
        document.getElementById('federal-state').setAttribute('readonly', 'readonly');
        document.getElementById('cowntry').setAttribute('readonly', 'readonly');
        document.getElementById('description').setAttribute('readonly', 'readonly');

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