// Configurações do menu do sistema. A seleção do menu é baseada no path
// da página selecionada.


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
} else if (pathname.startsWith("/Home/About")) {
    setMenuOption(8);
} else {
    setMenuOption(1);
}


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

        default:
            relativePath = '~/img/book_icon_24.png';
            menuId = 'menu-1';
            break;

    }

    let favicon = document.querySelector('link[rel="icon"]');

    if (!favicon) {
        favicon = document.createElement('link');
        favicon.setAttribute('rel', 'icon');
        document.head.appendChild(favicon);
    }

    const getAbsolutePath = function(href) {
        const link = document.createElement("a");
        link.href = href;
        return link.href;
    };

    const absolutePath = getAbsolutePath(relativePath.replace('~', ''));

    favicon.setAttribute('type', 'image/png');
    favicon.setAttribute('sizes', '24x24');
    favicon.setAttribute('href', absolutePath);
    
    document.getElementById(menuId).className = "active";

}