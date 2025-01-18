
// Tratador de eventos da página ~\Views\User\RegisterAdm.cshtml.


/**
 * Exibe o spinner para indicar um processo eventualmente longo, pois como é utilizado argon2id
 * para cifrar a senha, pode haver um pequeno atrazo na codificação, dependendo da quantidade de 
 * recursos computacionais alocados para o cálculo do hash. A configuração do spinner é feita no
 * CSS da página.
 */

document.getElementById('adm-form').addEventListener('submit', function (event) {

    document.getElementById('name').setAttribute('readonly', 'readonly');
    document.getElementById('user-name').setAttribute('readonly', 'readonly');
    document.getElementById('password').setAttribute('readonly', 'readonly');
    document.getElementById('conf-password').setAttribute('readonly', 'readonly');
    document.getElementById('save-button').disabled = true;
    document.getElementById('cancel-button').classList.add('disabled');

    var spinner = document.getElementById('spinner');
    spinner.style.display = 'block';

});