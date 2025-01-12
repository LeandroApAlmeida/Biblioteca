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
        },
        "order": []
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
        },
        "order": []
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
        },
        "order": []
    });

    $('#user-table').DataTable({
        language: {
            "decimal": "",
            "emptyTable": "Nenhum usuário encontrado",
            "info": "Usuário _START_ a _END_ de _TOTAL_",
            "infoEmpty": "Nenhum usuário em exibição",
            "infoFiltered": "",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar: _MENU_ usuários",
            "loadingRecords": "Exibindo...",
            "processing": "",
            "search": "Pesquisar: ",
            "zeroRecords": "Nenhum usuário encontrado",
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
        },
        "order": []
    });

    $('#log-table').DataTable({
        language: {
            "decimal": "",
            "emptyTable": "Nenhum log encontrado",
            "info": "Log _START_ a _END_ de _TOTAL_",
            "infoEmpty": "Nenhum log em exibição",
            "infoFiltered": "",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar: _MENU_ logs",
            "loadingRecords": "Exibindo...",
            "processing": "",
            "search": "Pesquisar: ",
            "zeroRecords": "Nenhum log encontrado",
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
        },
        "order": []
    });

    setTimeout(function () {
        $(".alert").fadeOut("slow", function () {
            $(this).alert('close');
        });
    }, 3000)

});