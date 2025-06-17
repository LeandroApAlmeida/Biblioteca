
// Tratador de eventos da página ~\Views\User\Register.cshtml.


document.addEventListener('DOMContentLoaded', function () {

    document.getElementById('name').focus();

    /**
     * Exibe o spinner para indicar um processo eventualmente longo, pois como é utilizado argon2id
     * para cifrar a senha, pode haver um pequeno atrazo na codificação, dependendo da quantidade de 
     * recursos computacionais alocados para o cálculo do hash. A configuração do spinner é feita no
     * CSS da página.
     */

    document.getElementById('user-form').addEventListener('submit', function (event) {

        document.getElementById('spinner').style.display = 'block';
        document.body.style.overflow = 'hidden';

        document.getElementById('name').setAttribute('readonly', 'readonly');
        document.getElementById('user-name').setAttribute('readonly', 'readonly');
        document.getElementById('password').setAttribute('readonly', 'readonly');
        document.getElementById('conf-password').setAttribute('readonly', 'readonly');

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