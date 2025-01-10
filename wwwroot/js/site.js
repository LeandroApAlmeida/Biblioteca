$(document).ready(function () {

    $('#book-table').DataTable({
        language: {
            "decimal": "",
            "emptyTable": "Nenhum livro encontrado",
            "info": "Livros _START_ a _END_ de _TOTAL_",
            "infoEmpty": "Nenhum livro em exibição",
            "infoFiltered": "",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar: _MENU_ livros",
            "loadingRecords": "Exibindo...",
            "processing": "",
            "search": "Pesquisar: ",
            "zeroRecords": "Nenhum livro encontrado",
            "paginate": {
                "first": "Primeiro",
                "last": "Último",
                "next": "Próximo",
                "previous": "Anterior"
            },
            "aria": {
                "orderable": "Ordenar por esta coluna",
                "orderableReverse": "Reverter a ordem da coluna"
            }
        }
    });

    $('#book-table-2').DataTable({
        language: {
            "decimal": "",
            "emptyTable": "Nenhum livro encontrado",
            "info": "Livros _START_ a _END_ de _TOTAL_",
            "infoEmpty": "Nenhum livro em exibição",
            "infoFiltered": "",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar: _MENU_ livros",
            "loadingRecords": "Exibindo...",
            "processing": "",
            "search": "Pesquisar: ",
            "zeroRecords": "Nenhum livro encontrado",
            "paginate": {
                "first": "Primeiro",
                "last": "Último",
                "next": "Próximo",
                "previous": "Anterior"
            },
            "aria": {
                "orderable": "Ordenar por esta coluna",
                "orderableReverse": "Reverter a ordem da coluna"
            }
        }
    });

    $('#person-table').DataTable({
        language: {
            "decimal": "",
            "emptyTable": "Nenhuma pessoa encontrada",
            "info": "Pessoa _START_ a _END_ de _TOTAL_",
            "infoEmpty": "Nenhuma pessoa em exibição",
            "infoFiltered": "",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar: _MENU_ pessoas",
            "loadingRecords": "Exibindo...",
            "processing": "",
            "search": "Pesquisar: ",
            "zeroRecords": "Nenhuma pessoa encontrada",
            "paginate": {
                "first": "Primeiro",
                "last": "Último",
                "next": "Próximo",
                "previous": "Anterior"
            },
            "aria": {
                "orderable": "Ordenar por esta coluna",
                "orderableReverse": "Reverter a ordem da coluna"
            }
        }
    });

    setTimeout(function () {
        $(".alert").fadeOut("slow", function () {
            $(this).alert('close');
        });
    }, 3000)

});