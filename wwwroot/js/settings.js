
var discardedTextColorCaption = document.getElementById('discarded-text-color');
var donatedTextColorCaption = document.getElementById('donated-text-color');
var borrowedTextColorCaption = document.getElementById('borrowed-text-color');

var discardedColorPicker = document.getElementById('discarded-color-picker');
var donatedColorPicker = document.getElementById('donated-color-picker');
var borrowedColorPicker = document.getElementById('borrowed-color-picker');

var pdfReportOpt = document.getElementById('pdf-report-opt');
var htmlReportOpt = document.getElementById('html-report-opt');


discardedTextColorCaption.style.color = discardedTextColor;
donatedTextColorCaption.style.color = donatedTextColor;
borrowedTextColorCaption.style.color = borrowedTextColor;

discardedColorPicker.value = discardedTextColor;
donatedColorPicker.value = donatedTextColor;
borrowedColorPicker.value = borrowedTextColor;

discardedColorPicker.addEventListener('change', function () {
    discardedTextColorCaption.style.color = this.value;
    setDiscardedTextColor(this.value);
});

donatedColorPicker.addEventListener('change', function () {
    donatedTextColorCaption.style.color = this.value;
    setDonatedTextColor(this.value);
});

borrowedColorPicker.addEventListener('change', function () {
    borrowedTextColorCaption.style.color = this.value;
    setBorrowedTextColor(this.value);
});


function stringToBool(str) { return str.toLowerCase() === 'true'; }

document.getElementById('discarded-bold').checked = stringToBool(isDiscardedBold);
document.getElementById('discarded-underline').checked = stringToBool(isDiscardedUnderline);
document.getElementById('discarded-italic').checked = stringToBool(isDiscardedItalic);

document.getElementById('donated-bold').checked = stringToBool(isDonatedBold);
document.getElementById('donated-underline').checked = stringToBool(isDonatedUnderline);
document.getElementById('donated-italic').checked = stringToBool(isDonatedItalic);

document.getElementById('borrowed-bold').checked = stringToBool(isBorrowedBold);
document.getElementById('borrowed-underline').checked = stringToBool(isBorrowedUnderline);
document.getElementById('borrowed-italic').checked = stringToBool(isBorrowedItalic);

updateDiscardedFontStyle();
updateDonatedFontStyle();
updateBorrowedFontStyle();


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


function updateDiscardedFontStyle() {
    const bold = document.getElementById('discarded-bold').checked;
    const underline = document.getElementById('discarded-underline').checked;
    const italic = document.getElementById('discarded-italic').checked;
    discardedTextColorCaption.classList.toggle('bold', bold);
    discardedTextColorCaption.classList.toggle('underline', underline);
    discardedTextColorCaption.classList.toggle('italic', italic);
    setDiscardedFontStyle(bold, underline, italic);
}

function updateDonatedFontStyle() {
    const bold = document.getElementById('donated-bold').checked;
    const underline = document.getElementById('donated-underline').checked;
    const italic = document.getElementById('donated-italic').checked;
    donatedTextColorCaption.classList.toggle('bold', bold);
    donatedTextColorCaption.classList.toggle('underline', underline);
    donatedTextColorCaption.classList.toggle('italic', italic);
    setDonatedFontStyle(bold, underline, italic);
}

function updateBorrowedFontStyle() {
    const bold = document.getElementById('borrowed-bold').checked;
    const underline = document.getElementById('borrowed-underline').checked;
    const italic = document.getElementById('borrowed-italic').checked;
    borrowedTextColorCaption.classList.toggle('bold', bold);
    borrowedTextColorCaption.classList.toggle('underline', underline);
    borrowedTextColorCaption.classList.toggle('italic', italic);
    setBorrowedFontStyle(bold, underline, italic);
}


function updateReportFormat() {
    if (document.getElementById('pdf-report-opt').checked) {
        setReportFormat(1);
    } else {
        setReportFormat(2);
    }
}


function setTextColor(endPoint, color) {

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

    fetch(`${origin}/api/SettingsApi/${endPoint}/${encodeURIComponent(color)}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Erro ao salvar a configuração.');
        }
        return true;
    })
    .catch(error => {
        alert(error);
        return false;
    });

}


function setBorrowedTextColor(color) {
    return setTextColor('SetBorrowedTextColor', color);
}

function setDiscardedTextColor(color) {
    return setTextColor('SetDiscardedTextColor', color);
}

function setDonatedTextColor(color) {
    return setTextColor('SetDonatedTextColor', color);
}


function setFontStyle(endPoint, isBold, isUnderline, isItalic) {

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

    fetch(`${origin}/api/SettingsApi/${endPoint}/${encodeURIComponent(isBold)}/${encodeURIComponent(isUnderline)}/${encodeURIComponent(isItalic)}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Erro ao salvar a configuração.');
        }
        return true;
    })
    .catch(error => {
        alert(error);
        return false;
    });

}


function setBorrowedFontStyle(isBold, isUnderline, isItalic) {
    return setFontStyle('SetBorrowedFontStyle', isBold, isUnderline, isItalic);
}

function setDiscardedFontStyle(isBold, isUnderline, isItalic) {
    return setFontStyle('SetDiscardedFontStyle', isBold, isUnderline, isItalic);
}

function setDonatedFontStyle(isBold, isUnderline, isItalic) {
    return setFontStyle('SetDonatedFontStyle', isBold, isUnderline, isItalic);
}


function setReportFormat(format) {

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

    fetch(`${origin}/api/SettingsApi/SetReportFormat/${encodeURIComponent(format)}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Erro ao salvar a configuração.');
        }
        return true;
    })
    .catch(error => {
        alert(error);
        return false;
    });

}