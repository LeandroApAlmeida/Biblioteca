import { formatToDatetimeLocal } from "./utils.js"

var selector = document.getElementById('select-book');
selector.addEventListener('change', onSelectedBook);

document.getElementById("discard-date").value = formatToDatetimeLocal(new Date());

onSelectedBook();

function onSelectedBook() {

    var jsonData = window.booksData;

    for (var i = 0; i < jsonData.length; i++) {

        var book = jsonData[i];

        if (book.Id == selector.value) {

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

            const url = `${origin}/api/CollectionApi/cover/${book.Id}`;
            fetch(url).then(response => {

                if (!response.ok) { throw new Error('Erro ao buscar os dados'); }

                return response.json();

            }).then(data => {

                const imgPreview = document.getElementById("img-preview");
                imgPreview.style.display = "block";
                imgPreview.innerHTML = '<img src="' + data + '" class="center"/>';
                imgPreview.removeAttribute("hidden");

                document.getElementsByName("Book.Cover")[0].value = data;
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

            }).catch(error => {

                alert(error);

            });

            break;

        }

    }

}