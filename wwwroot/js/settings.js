
let palleteColor = "#feffff";

document.addEventListener('DOMContentLoaded', function () {

    var discardedTextColorHint = document.getElementById('discarded-text-color');
    var donatedTextColorHint = document.getElementById('donated-text-color');
    var borrowedTextColorHint = document.getElementById('borrowed-text-color');
    
    discardedTextColorHint.style.color = discardedTextColor;
    donatedTextColorHint.style.color = donatedTextColor;
    borrowedTextColorHint.style.color = borrowedTextColor;

    discardedTextColorHint.classList.toggle('bold', stringToBool(isDiscardedBold));
    discardedTextColorHint.classList.toggle('underline', stringToBool(isDiscardedUnderline));
    discardedTextColorHint.classList.toggle('italic', stringToBool(isDiscardedItalic));

    donatedTextColorHint.classList.toggle('bold', stringToBool(isDonatedBold));
    donatedTextColorHint.classList.toggle('underline', stringToBool(isDonatedUnderline));
    donatedTextColorHint.classList.toggle('italic', stringToBool(isDonatedItalic));

    borrowedTextColorHint.classList.toggle('bold', stringToBool(isBorrowedBold));
    borrowedTextColorHint.classList.toggle('underline', stringToBool(isBorrowedUnderline));
    borrowedTextColorHint.classList.toggle('italic', stringToBool(isBorrowedItalic));


    var discardedColorPicker = document.getElementById('discarded-color-picker');
    var donatedColorPicker = document.getElementById('donated-color-picker');
    var borrowedColorPicker = document.getElementById('borrowed-color-picker');
    var pageBackgroundColorPicker = document.getElementById('page-background-picker');

    discardedColorPicker.value = discardedTextColor;
    donatedColorPicker.value = donatedTextColor;
    borrowedColorPicker.value = borrowedTextColor;
    pageBackgroundColorPicker.value = pageBackgroundColor;

    discardedColorPicker.addEventListener('change', function () {
        discardedTextColorHint.style.color = this.value;
        setSetting('SetDiscardedTextColor', this.value);
    });

    donatedColorPicker.addEventListener('change', function () {
        donatedTextColorHint.style.color = this.value;
        setSetting('SetDonatedTextColor', this.value);
    });

    borrowedColorPicker.addEventListener('change', function () {
        borrowedTextColorHint.style.color = this.value;
        setSetting('SetBorrowedTextColor', this.value);
    });

    pageBackgroundColorPicker.addEventListener('change', function () {
        document.body.style.backgroundColor = this.value;
        setSetting('SetPageBackgroundColor', this.value);
    });


    var pdfReportOpt = document.getElementById('pdf-report-opt');
    var htmlReportOpt = document.getElementById('html-report-opt');

    if (reportFormat == 1) {
        pdfReportOpt.checked = true;
    } else if (reportFormat == 2) {
        htmlReportOpt.checked = true;
    } else {
        pdfReportOpt.checked = true;
    }

    pdfReportOpt.addEventListener('change', function (event) {
        updateReportFormat();
    });

    htmlReportOpt.addEventListener('change', function (event) {
        updateReportFormat();
    });


    document.getElementById('discarded-bold').checked = stringToBool(isDiscardedBold);
    document.getElementById('discarded-underline').checked = stringToBool(isDiscardedUnderline);
    document.getElementById('discarded-italic').checked = stringToBool(isDiscardedItalic);

    document.getElementById('donated-bold').checked = stringToBool(isDonatedBold);
    document.getElementById('donated-underline').checked = stringToBool(isDonatedUnderline);
    document.getElementById('donated-italic').checked = stringToBool(isDonatedItalic);

    document.getElementById('borrowed-bold').checked = stringToBool(isBorrowedBold);
    document.getElementById('borrowed-underline').checked = stringToBool(isBorrowedUnderline);
    document.getElementById('borrowed-italic').checked = stringToBool(isBorrowedItalic);

    document.getElementById('apply-styles-to-lists').checked = stringToBool(isApplyStylesToLists);
    document.getElementById('show-footer-caption').checked = stringToBool(isShowFooterCaption);

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


});


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


function stringToBool(str) { return str.toLowerCase() === 'true'; }


function updateDiscardedBold() {
    var discardedTextColorInput = document.getElementById('discarded-text-color');
    const bold = document.getElementById('discarded-bold').checked;
    discardedTextColorInput.classList.toggle('bold', bold);
    setSetting('SetDiscardedBold', bold);
}

function updateDiscardedUnderline() {
    var discardedTextColorInput = document.getElementById('discarded-text-color');
    const underline = document.getElementById('discarded-underline').checked;
    discardedTextColorInput.classList.toggle('underline', underline);
    setSetting('SetDiscardedUnderline', underline);
}

function updateDiscardedItalic() {
    var discardedTextColorInput = document.getElementById('discarded-text-color');
    const italic = document.getElementById('discarded-italic').checked;
    discardedTextColorInput.classList.toggle('italic', italic);
    setSetting('SetDiscardedItalic', italic);
}


function updateDonatedBold() {
    var donatedTextColorInput = document.getElementById('donated-text-color');
    const bold = document.getElementById('donated-bold').checked;
    donatedTextColorInput.classList.toggle('bold', bold);
    setSetting('SetDonatedBold', bold);
}

function updateDonatedUnderline() {
    var donatedTextColorInput = document.getElementById('donated-text-color');
    const underline = document.getElementById('donated-underline').checked;
    donatedTextColorInput.classList.toggle('underline', underline);
    setSetting('SetDonatedUnderline', underline);
}

function updateDonatedItalic() {
    var donatedTextColorInput = document.getElementById('donated-text-color');
    const italic = document.getElementById('donated-italic').checked;
    donatedTextColorInput.classList.toggle('italic', italic);
    setSetting('SetDonatedItalic', italic);
}


function updateBorrowedBold() {
    var borrowedTextColorInput = document.getElementById('borrowed-text-color');
    const bold = document.getElementById('borrowed-bold').checked;
    borrowedTextColorInput.classList.toggle('bold', bold);
    setSetting('SetBorrowedBold', bold);
}

function updateBorrowedUnderline() {
    var borrowedTextColorInput = document.getElementById('borrowed-text-color');
    const underline = document.getElementById('borrowed-underline').checked;
    borrowedTextColorInput.classList.toggle('underline', underline);
    setSetting('SetBorrowedUnderline', underline);
}

function updateBorrowedItalic() {
    var borrowedTextColorInput = document.getElementById('borrowed-text-color');
    const italic = document.getElementById('borrowed-italic').checked;
    borrowedTextColorInput.classList.toggle('italic', italic);
    setSetting('SetBorrowedItalic', italic);
}


function updateReportFormat() {
    var format = document.getElementById('pdf-report-opt').checked ? 1 : 2;
    setSetting('SetReportFormat', format);
}


function updateApplyStylesToLists() {
    const apply = document.getElementById('apply-styles-to-lists').checked;
    setSetting('SetApplyStylesToLists', apply);
}


function updateShowFooterCaption() {
    const show = document.getElementById('show-footer-caption').checked;
    setSetting('SetShowFooterCaption', show);
}


function setPalleteColor() {
    var pageBackgroundColorPicker = document.getElementById('page-background-picker');
    pageBackgroundColorPicker.value = palleteColor;
    document.body.style.backgroundColor = palleteColor;
    setSetting('SetPageBackgroundColor', palleteColor);
}