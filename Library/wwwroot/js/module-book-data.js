
/*
    Módulo para tratamento de campos ocultos de um livro na página.
*/


/**
 * Atualiza os dados de um livro selecionado nos campos ocultos da página.
 * 
 * @param {any} document página a ser atualizada.
 * @param {any} book dados do livro.
 * @param {any} cover capa do livro.
 */
function setBookData(document, book, cover) {

    document.getElementsByName("Book.Id")[0].value = book.Id;
    document.getElementsByName("Book.Title")[0].value = book.Title;
    document.getElementsByName("Book.Subtitle")[0].value = book.Subtitle;
    document.getElementsByName("Book.Author")[0].value = book.Author;
    document.getElementsByName("Book.Publisher")[0].value = book.Publisher;
    document.getElementsByName("Book.Isbn")[0].value = book.Isbn;
    document.getElementsByName("Book.Edition")[0].value = book.Edition;
    document.getElementsByName("Book.Volume")[0].value = book.Volume;
    document.getElementsByName("Book.NumberOfPages")[0].value = book.NumberOfPages;
    document.getElementsByName("Book.ReleaseYear")[0].value = book.ReleaseYear;
    document.getElementsByName("Book.AcquisitionDate")[0].value = book.AcquisitionDate;
    document.getElementsByName("Book.RegistrationDate")[0].value = book.RegistrationDate;
    document.getElementsByName("Book.LastUpdateDate")[0].value = book.LastUpdateDate;
    document.getElementsByName("Book.Summary")[0].value = book.Summary;
    document.getElementsByName("Book.IsDeleted")[0].value = book.IsDeleted;

    document.getElementsByName("Book.Cover.Id")[0].value = cover.Id;
    document.getElementsByName("Book.Cover.Data")[0].value = cover.Data;

}


export { setBookData };