
/**
 * Tratador de eventos da página de login (~\Views\Login\Login.cshtml).
 */


document.addEventListener('DOMContentLoaded', function () {


    // Move o foco para o componente usuário.
    document.getElementById('user-name').focus();


    /**
     * Evento disparado ao submeter o form. Neste caso, exibe o spinner para indicar um processo eventualmente
     * longo, pois como é utilizado argon2id para cifrar a senha, pode haver um pequeno atrazo na codificação,
     * dependendo da quantidade de recursos computacionais alocados para o cálculo do hash. A configuração do 
     * spinner é feita no CSS da página.
     */
    document.getElementById('login-form').addEventListener('submit', function (event) {

        // Exibe o spinner.
        document.getElementById('spinner').style.display = 'block';

        // Desabilita os campos da página.
        document.getElementById('user-name').setAttribute('readonly', 'readonly');
        document.getElementById('password').setAttribute('readonly', 'readonly');

        // Desabilita os botões da página.
        document.getElementById('login-button').disabled = true;

    });


});