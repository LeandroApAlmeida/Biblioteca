$(document).ready(function () {

    $('#book-table').DataTable({
        language: { url: "/dic/book-table-pt_br.json" },
        order: [],
        ordering: false,
        columnDefs: [{ targets: '_all', className: 'dt-body-left' }],
        lengthMenu: [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "Todos"]],
        pageLength: 5
    });

    $('#book-table-2').DataTable({
        language: { url: "/dic/book-table-pt_br.json" },
        order: [],
        ordering: false,
        columnDefs: [{ targets: '_all', className: 'dt-body-left' }],
        lengthMenu: [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "Todos"]],
        pageLength: 5
    });

    $('#filter-table').DataTable({
        language: {
            url: "/dic/book-table-pt_br.json",
        },
        order: [],
        ordering: false,
        columnDefs: [{ targets: '_all', className: 'dt-body-left' }],
        lengthMenu: [[-1], ["Todos"]],
        pageLength: -1
    });

    $('#person-table').DataTable({
        language: { url: "/dic/person-table-pt_br.json" },
        order: [],
        ordering: false,
        columnDefs: [{ targets: '_all', className: 'dt-body-left' }],
        lengthMenu: [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "Todos"]],
        pageLength: 5
    });

    $('#user-table').DataTable({
        language: { url: "/dic/user-table-pt_br.json" },
        order: [],
        ordering: false,
        columnDefs: [{ targets: '_all', className: 'dt-body-left' }],
        lengthMenu: [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "Todos"]],
        pageLength: 5
    });

    $('#log-table').DataTable({
        language: { url: "/dic/log-table-pt_br.json" },
        order: [],
        ordering: false,
        columnDefs: [{ targets: '_all', className: 'dt-body-left' }],
        lengthMenu: [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "Todos"]],
        pageLength: 5
    });

    setTimeout(function () {
        $(".alert").fadeOut("slow", function () {
            $(this).alert('close');
        });
    }, 3000)

});