$(document).ready(function () {
    $("#searchForm").on("submit", function (e) {
        e.preventDefault();

        var searchTerm = $("#searchInput").val();

        $.ajax({
            url: '@Url.Action("SearchMedicament", "Search")',
            type: 'GET',
            data: { search: searchTerm },
            success: function (response) {
                if (response.success) {
                    var results = response.data;
                    var resultsContainer = $("#searchResults");
                    resultsContainer.empty();

                    if (results.length > 0) {
                        results.forEach(function (item) {
                            resultsContainer.append("<div class='search-result-item'>" +
                                "<img src='/images/" + item.Medicament.Image + "' alt='" + item.Medicament.Nom + "' class='img-thumbnail' style='width:50px;height:50px;' />" +
                                "<strong>" + item.Medicament.Nom + "</strong><br />" +
                                "<p>" + item.Medicament.Description + "</p>" +
                                "<p>Prix: " + item.Prix + " MAD</p>" +
                                "<p>Quantité: " + item.Quantite + "</p>" +
                                "</div>");
                        });
                    } else {
                        resultsContainer.append("<div>Aucun résultat trouvé</div>");
                    }
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("Erreur lors de la recherche");
            }
        });
    });
});