$(document).ready(function () {

    $('#book-table').DataTable({
        language: { url: "/dic/book-table-pt_br.json" },
        order: [],
        ordering: false,
        columnDefs: [{ targets: '_all', className: 'dt-body-left' } ]
    });

    $('#book-table-2').DataTable({
        language: { url: "/dic/book-table-pt_br.json" },
        order: [],
        ordering: false,
        columnDefs: [{ targets: '_all', className: 'dt-body-left' }]
    });

    $('#person-table').DataTable({
        language: { url: "/dic/person-table-pt_br.json" },
        order: [],
        ordering: false,
        columnDefs: [{ targets: '_all', className: 'dt-body-left' }]
    });

    $('#user-table').DataTable({
        language: { url: "/dic/user-table-pt_br.json" },
        order: [],
        ordering: false,
        columnDefs: [{ targets: '_all', className: 'dt-body-left' }]
    });

    $('#log-table').DataTable({
        language: { url: "/dic/log-table-pt_br.json" },
        order: [],
        ordering: false,
        columnDefs: [{ targets: '_all', className: 'dt-body-left' }]
    });

    setTimeout(function () {
        $(".alert").fadeOut("slow", function () {
            $(this).alert('close');
        });
    }, 3000)

});