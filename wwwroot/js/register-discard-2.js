import { formatToDatetimeLocal } from "./utils.js"
import { setBookData } from "./data-book.js"


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

            }).then(cover => {

                const imgPreview = document.getElementById("img-preview");
                imgPreview.style.display = "block";
                imgPreview.innerHTML = '<img src="' + cover + '" class="center"/>';
                imgPreview.removeAttribute("hidden");

                setBookData(document, book, cover);

            }).catch(error => {

                alert(error);

            });

            break;

        }

    }

}