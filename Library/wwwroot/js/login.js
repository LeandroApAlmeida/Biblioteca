
/*
    Tratador de eventos da página de login (~\Views\Login\Login.cshtml).
*/


document.addEventListener('DOMContentLoaded', function () {

    document.getElementById('user-name').focus();

    // Exibe o spinner para indicar um processo eventualmente longo, pois como é utilizado argon2id
    // para cifrar a senha, pode haver um pequeno atrazo na codificação, dependendo da quantidade de
    // recursos computacionais alocados para o cálculo do hash. A configuração do spinner é feita no
    // CSS da página.

    document.getElementById('login-form').addEventListener('submit', function (event) {

        document.getElementById('spinner').style.display = 'block';

        document.getElementById('user-name').setAttribute('readonly', 'readonly');
        document.getElementById('password').setAttribute('readonly', 'readonly');
        document.getElementById('login-button').disabled = true;

    });

});