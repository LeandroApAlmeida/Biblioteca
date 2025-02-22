
/*
    Configuração da barra de menu das páginas de layout. O menu é utilizado pelos layouts
    ~\Views\Shared\_Layout.cshtml e ~\Views\Shared\_LayoutAdmin.cshtml. O layout de login
    ~\Views\Shared\_LoginLayout.cshtml não tem menu.
*/


document.addEventListener('DOMContentLoaded', function () {

    //Propriedades da página ativa.

    const {
        host,
        hostname,
        href,
        origin,
        pathname,
        port,
        protocol,
        search
    } = window.location

    // Identifica a que item de menu a página atual está relacionada e aciona a função
    // para a configuração do menu.

    if (pathname.startsWith("/Book/Details") || pathname.startsWith("/Book/NoBook")) {
        setMenuOption(1);
    } else if (pathname.startsWith("/Book")) {
        setMenuOption(2);
    } else if (pathname.startsWith("/Discard")) {
        setMenuOption(3);
    } else if (pathname.startsWith("/Donation")) {
        setMenuOption(4);
    } else if (pathname.startsWith("/Loan")) {
        setMenuOption(5);
    } else if (pathname.startsWith("/Person")) {
        setMenuOption(6);
    } else if (pathname.startsWith("/User")) {
        setMenuOption(7);
    } else if (pathname.startsWith("/Session")) {
        setMenuOption(9);
    } else if (pathname.startsWith("/Settings")) {
        setMenuOption(10);
    } else if (pathname.startsWith("/Home/About")) {
        setMenuOption(8);
    } else {
        setMenuOption(1);
    }

});


/**
 * Configura o menu relacionado com a página em exibição. A função executa duas modificações:
 * 
 * 1. Configura o ícone na aba do navegador.
 * 
 * 2. Ativa o item de menu para criar a linha horizontal de destaque do menu, configurada via CSS.
 * 
 * O banner da página não recebe esta linha, conduzindo para a página de detalhes de um livro, a
 * qual será configurada no menu.
 * 
 * @param {any} menuIndex índice do menu.
 */
function setMenuOption(menuIndex) {

    var relativePath;
    var menuId;

    switch (menuIndex) {

        case 1:
            relativePath = '~/img/book_icon_24.png';
            menuId = 'menu-1';
            break;

        case 2:
            relativePath = '~/img/library_icon_24.png';
            menuId = 'menu-2';
            break;

        case 3:
            relativePath = '~/img/recycle_icon.png';
            menuId = 'menu-3';
            break;

        case 4:
            relativePath = '~/img/donation_icon.png';
            menuId = 'menu-4';
            break;

        case 5:
            relativePath = '~/img/book_borrow_icon_24.png';
            menuId = 'menu-5';
            break;

        case 6:
            relativePath = '~/img/face_icon_24.png';
            menuId = 'menu-6';
            break;

        case 7:
            relativePath = '~/img/person_icon.png';
            menuId = 'menu-7';
            break;

        case 8:
            relativePath = '~/img/about_icon_24.png';
            menuId = 'menu-8';
            break;

        case 9:
            relativePath = '~/img/report_icon_24.png';
            menuId = 'menu-9';
            break;

        case 10:
            relativePath = '~/img/settings_icon_24.png';
            menuId = 'menu-10';
            break;

        default:
            relativePath = '~/img/book_icon_24.png';
            menuId = 'menu-1';
            break;

    }

    let icon = document.querySelector('link[rel="icon"]');

    if (!icon) {
        icon = document.createElement('link');
        icon.setAttribute('rel', 'icon');
        document.head.appendChild(icon);
    }

    const getAbsolutePath = function(href) {
        const link = document.createElement("a");
        link.href = href;
        return link.href;
    };

    const absolutePath = getAbsolutePath(relativePath.replace('~', ''));

    // Atribui o ícone à aba do navegador.

    icon.setAttribute('type', 'image/png');
    icon.setAttribute('sizes', '24x24');
    icon.setAttribute('href', absolutePath);

    // Ativa o item de menu para receber a linha de horizontal

    if (document.getElementById(menuId) != null) {
        document.getElementById(menuId).className = "active";
    }

}