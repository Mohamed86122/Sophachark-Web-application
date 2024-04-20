$(document).ready(function() {
    $('#monTableau').DataTable({
        dom: '<"top" <"dt-buttons"B><"dt-length"l><"dt-search"f>>rt<"bottom"ip><"clear">', // Structure personnalisée du DOM
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ],
        responsive: true,
        orderCellsTop: true,
        initComplete: function() {
            this.api().columns().every(function() {
                var column = this;
                var input = $('<input type="text" placeholder="Rechercher"/>').appendTo($(column.footer()).empty())
                .on('keyup change', function() {
                    var val = $(this).val();
                    column.search(val ? val : '', true, false).draw();
                });
            });
        },
        language: {
            lengthMenu: "Afficher _MENU_ entrées",
            zeroRecords: "Aucun enregistrement trouvé",
            info: "Affichage de la page _PAGE_ sur _PAGES_",
            infoEmpty: "Aucune entrée disponible",
            infoFiltered: "(filtré à partir de _MAX_ enregistrements totaux)",
            search: "Recherche:",
            paginate: {
                first: "Premier",
                last: "Dernier",
                next: ">>",
                previous: "<<"
            }
        }
    });
});
