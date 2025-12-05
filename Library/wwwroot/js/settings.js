
/**
 * Tratador de eventos da página de configurações (~\Views\Settings\Manage.cshtml).
 */


let palleteColor = "#feffff";

document.addEventListener('DOMContentLoaded', function () {


    // Configurações da ABA 1 #####################################################################


    // Campo texto de dica para livros descartados.
    var discardedTextColorHint = document.getElementById('discarded-text-color');
    // Campo texto de dica para livros doados.
    var donatedTextColorHint = document.getElementById('donated-text-color');
    // Campo texto de dica para livros emprestados.
    var borrowedTextColorHint = document.getElementById('borrowed-text-color');

    // Atribui a cor de texto de livro descartado ao texto de dica.
    discardedTextColorHint.style.color = discardedTextColor;
    // Atribui a cor de texto de livro doado ao texto de dica.
    donatedTextColorHint.style.color = donatedTextColor;
    // Atribui a cor de texto de livro emprestado ao texto de dica.
    borrowedTextColorHint.style.color = borrowedTextColor;

    // Atribui o estilo negrito ao texto de dica de livros descartados.
    discardedTextColorHint.classList.toggle('bold', stringToBool(isDiscardedBold));
    // Atribui o estilo subrinhado ao texto de dica de livros descartados.
    discardedTextColorHint.classList.toggle('underline', stringToBool(isDiscardedUnderline));
    // Atribui o estilo itálico ao texto de dica de livros descartados.
    discardedTextColorHint.classList.toggle('italic', stringToBool(isDiscardedItalic));

    // Atribui o estilo negrito ao texto de dica de livros doados.
    donatedTextColorHint.classList.toggle('bold', stringToBool(isDonatedBold));
    // Atribui o estilo subrinhado ao texto de dica de livros doados.
    donatedTextColorHint.classList.toggle('underline', stringToBool(isDonatedUnderline));
    // Atribui o estilo itálico ao texto de dica de livros doados.
    donatedTextColorHint.classList.toggle('italic', stringToBool(isDonatedItalic));

    // Atribui o estilo negrito ao texto de dica de livros emprestados.
    borrowedTextColorHint.classList.toggle('bold', stringToBool(isBorrowedBold));
    // Atribui o estilo subrinhado ao texto de dica de livros emprestados.
    borrowedTextColorHint.classList.toggle('underline', stringToBool(isBorrowedUnderline));
    // Atribui o estilo itálico ao texto de dica de livros emprestados.
    borrowedTextColorHint.classList.toggle('italic', stringToBool(isBorrowedItalic));

    // Seleciona a checkbox de negrito para texto de dica de livros descartados.
    document.getElementById('discarded-bold').checked = stringToBool(isDiscardedBold);
    // Seleciona a checkbox de subrinhado para texto de dica de livros descartados.
    document.getElementById('discarded-underline').checked = stringToBool(isDiscardedUnderline);
    // Seleciona a checkbox de itático para texto de dica de livros descartados.
    document.getElementById('discarded-italic').checked = stringToBool(isDiscardedItalic);

    // Seleciona a checkbox de negrito para texto de dica de livros doados.
    document.getElementById('donated-bold').checked = stringToBool(isDonatedBold);
    // Seleciona a checkbox de subrinhado para texto de dica de livros doados.
    document.getElementById('donated-underline').checked = stringToBool(isDonatedUnderline);
    // Seleciona a checkbox de itático para texto de dica de livros doados.
    document.getElementById('donated-italic').checked = stringToBool(isDonatedItalic);

    // Seleciona a checkbox de negrito para texto de dica de livros emprestados.
    document.getElementById('borrowed-bold').checked = stringToBool(isBorrowedBold);
    // Seleciona a checkbox de subrinhado para texto de dica de livros emprestados.
    document.getElementById('borrowed-underline').checked = stringToBool(isBorrowedUnderline);
    // Seleciona a checkbox de itático para texto de dica de livros emprestados.
    document.getElementById('borrowed-italic').checked = stringToBool(isBorrowedItalic);


    // Componente de seleção da cor do texto de dica de livros descartados.
    var discardedColorPicker = document.getElementById('discarded-color-picker');
    // Componente de seleção da cor do texto de dica de livros doados.
    var donatedColorPicker = document.getElementById('donated-color-picker');
    // Componente de seleção da cor do texto de dica de livros emprestados.
    var borrowedColorPicker = document.getElementById('borrowed-color-picker');

    // Atribui a cor de texto de livro descartado ao componente.
    discardedColorPicker.value = discardedTextColor;
    // Atribui a cor de texto de livro doado ao componente.
    donatedColorPicker.value = donatedTextColor;
    // Atribui a cor de texto de livro emprestado ao componente.
    borrowedColorPicker.value = borrowedTextColor;
    

    // Atribui o tratador de evento ao componente de seleção de cor de texto de livro descartado.
    discardedColorPicker.addEventListener('change', function () {
        discardedTextColorHint.style.color = this.value;
        setSetting('SetDiscardedTextColor', this.value);
    });

    // Atribui o tratador de evento ao componente de seleção de cor de texto de livro doado.
    donatedColorPicker.addEventListener('change', function () {
        donatedTextColorHint.style.color = this.value;
        setSetting('SetDonatedTextColor', this.value);
    });

    // Atribui o tratador de evento ao componente de seleção de cor de texto de livro emprestado.
    borrowedColorPicker.addEventListener('change', function () {
        borrowedTextColorHint.style.color = this.value;
        setSetting('SetBorrowedTextColor', this.value);
    });


    // Configurações da ABA 2 #####################################################################


    // Componente radiobox de seleção de relatório em formato PDF.
    var pdfReportOpt = document.getElementById('pdf-report-opt');
    // Componente radiobox de seleção de relatório em formato HTML.
    var htmlReportOpt = document.getElementById('html-report-opt');

    // Seleciona o radiobox de acordo com o valor da configuração passado na página.
    if (reportFormat == 1) {
        // Formato PDF.
        pdfReportOpt.checked = true;
    } else if (reportFormat == 2) {
        // Formato HTML.
        htmlReportOpt.checked = true;
    } else {
        // Formato PDF.
        pdfReportOpt.checked = true;
    }

    // Atribui o tratador de evento ao radiobox formato PDF.
    pdfReportOpt.addEventListener('change', function (event) {
        updateReportFormat();
    });

    // Atribui o tratador de evento ao radiobox formato HTML.
    htmlReportOpt.addEventListener('change', function (event) {
        updateReportFormat();
    });


    // Configurações da ABA 3 #####################################################################


    // Componente de seleção da cor de fundo da página.
    var pageBackgroundColorPicker = document.getElementById('page-background-picker');

    // Atribui a cor de fundo de página ao componente.
    pageBackgroundColorPicker.value = pageBackgroundColor;

    // Atribui o tratador de evento ao componente de seleção de cor de fundo de página.
    pageBackgroundColorPicker.addEventListener('change', function () {
        document.body.style.backgroundColor = this.value;
        setSetting('SetPageBackgroundColor', this.value);
    });


    // Atribui o tratador de evento ao diálogo paleta de cores. Neste caso, ao selecionar uma
    // das cores pré-definidas, aplica a mesma ao fundo da página e ao componente de seleção da
    // cor de fundo da página.
    document.getElementById('color-pallete').addEventListener('change', (event) => {

        const select = event.target;

        if (select.selectedOptions.length > 1) {

            const lastSelected = select.selectedOptions[select.selectedOptions.length - 1];

            Array.from(select.options).forEach(option => {
                option.selected = false;
            });

            lastSelected.selected = true;
        }

        palleteColor = select.value;

        document.getElementById('color-view').style.backgroundColor = palleteColor;

    });


    // Seleciona a checkbox Aplicar estilos às listas.
    document.getElementById('apply-styles-to-lists').checked = stringToBool(isApplyStylesToLists);

    // Seleciona a checkbox Mostrar dica de rodapé.
    document.getElementById('show-footer-caption').checked = stringToBool(isShowFooterCaption);


});


/**
 * Definir uma configuração via chamada à API do site.
 * 
 * @param {any} endPoint endpoint da configuração
 * @param {any} value valor da configuração.
 */
function setSetting(endPoint, value) {

    const {
        host,
        hostname,
        href,
        origin,
        pathname,
        port,
        protocol,
        search
    } = window.location;

    fetch(`${origin}/Api/SettingsApi/${endPoint}/${encodeURIComponent(value)}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(response => {
        if (!response.ok) {
            throw new Error('Erro ao salvar a configuração.');
        }
        return true;
    }).catch(error => {
        alert(error);
        return false;
    });

}


/**
 * Converte string em boolean.
 * 
 * @param {any} str string a ser convertida.
 * @returns valor boolean correspondente.
 */
function stringToBool(str) { return str.toLowerCase() === 'true'; }


/**
 * Configura o estilo de negrito para texto de livro descartado.
 */
function updateDiscardedBold() {
    var discardedTextColorInput = document.getElementById('discarded-text-color');
    const bold = document.getElementById('discarded-bold').checked;
    discardedTextColorInput.classList.toggle('bold', bold);
    setSetting('SetDiscardedBold', bold);
}

/**
 * Configura o estilo de subrinhado para texto de livro descartado.
 */
function updateDiscardedUnderline() {
    var discardedTextColorInput = document.getElementById('discarded-text-color');
    const underline = document.getElementById('discarded-underline').checked;
    discardedTextColorInput.classList.toggle('underline', underline);
    setSetting('SetDiscardedUnderline', underline);
}

/**
 * Configura o estilo de itálico para texto de livro descartado.
 */
function updateDiscardedItalic() {
    var discardedTextColorInput = document.getElementById('discarded-text-color');
    const italic = document.getElementById('discarded-italic').checked;
    discardedTextColorInput.classList.toggle('italic', italic);
    setSetting('SetDiscardedItalic', italic);
}


/**
 * Configura o estilo de negrito para texto de livro doado.
 */
function updateDonatedBold() {
    var donatedTextColorInput = document.getElementById('donated-text-color');
    const bold = document.getElementById('donated-bold').checked;
    donatedTextColorInput.classList.toggle('bold', bold);
    setSetting('SetDonatedBold', bold);
}

/**
 * Configura o estilo de subrinhado para texto de livro doado.
 */
function updateDonatedUnderline() {
    var donatedTextColorInput = document.getElementById('donated-text-color');
    const underline = document.getElementById('donated-underline').checked;
    donatedTextColorInput.classList.toggle('underline', underline);
    setSetting('SetDonatedUnderline', underline);
}

/**
 * Configura o estilo de itálico para texto de livro doado.
 */
function updateDonatedItalic() {
    var donatedTextColorInput = document.getElementById('donated-text-color');
    const italic = document.getElementById('donated-italic').checked;
    donatedTextColorInput.classList.toggle('italic', italic);
    setSetting('SetDonatedItalic', italic);
}


/**
 * Configura o estilo de negrito para texto de livro emprestado.
 */
function updateBorrowedBold() {
    var borrowedTextColorInput = document.getElementById('borrowed-text-color');
    const bold = document.getElementById('borrowed-bold').checked;
    borrowedTextColorInput.classList.toggle('bold', bold);
    setSetting('SetBorrowedBold', bold);
}

/**
 * Configura o estilo de subrinhado para texto de livro emprestado.
 */
function updateBorrowedUnderline() {
    var borrowedTextColorInput = document.getElementById('borrowed-text-color');
    const underline = document.getElementById('borrowed-underline').checked;
    borrowedTextColorInput.classList.toggle('underline', underline);
    setSetting('SetBorrowedUnderline', underline);
}

/**
 * Configura o estilo de itálico para texto de livro emprestado.
 */
function updateBorrowedItalic() {
    var borrowedTextColorInput = document.getElementById('borrowed-text-color');
    const italic = document.getElementById('borrowed-italic').checked;
    borrowedTextColorInput.classList.toggle('italic', italic);
    setSetting('SetBorrowedItalic', italic);
}


/**
 * Atualiza o formato do relatório de acordo com o radiobox selecionado.
 */
function updateReportFormat() {
    var format = document.getElementById('pdf-report-opt').checked ? 1 : 2;
    setSetting('SetReportFormat', format);
}


/**
 * Configura a opção de Aplicar estilos às listas, de acordo com o definido no checkbox.
 */
function updateApplyStylesToLists() {
    const apply = document.getElementById('apply-styles-to-lists').checked;
    setSetting('SetApplyStylesToLists', apply);
}


/**
 * Configura a opção de Mostrar dica no rodapé, de acordo com o definido no checkbox.
 */
function updateShowFooterCaption() {
    const show = document.getElementById('show-footer-caption').checked;
    setSetting('SetShowFooterCaption', show);
}


/**
 * Configura a cor de fundo da página, de acordo com o valor definido no componente.
 */
function setPalleteColor() {
    var pageBackgroundColorPicker = document.getElementById('page-background-picker');
    pageBackgroundColorPicker.value = palleteColor;
    document.body.style.backgroundColor = palleteColor;
    setSetting('SetPageBackgroundColor', palleteColor);
}