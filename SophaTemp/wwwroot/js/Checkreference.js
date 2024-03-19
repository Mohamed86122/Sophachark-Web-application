    $(document).ready(function() {
        $('#reference').on('input', function () {
            var reference = $(this).val();
            $.ajax({
                url: '@Url.Action("CheckReferenceExists", "Medicaments")',
                data: { reference: reference },
                method: 'GET',
                success: function (isUnique) {
                    if (!isUnique) {
                        $('#referenceFeedback').show();
                        $('#reference').addClass('is-invalid');
                    } else {
                        $('#referenceFeedback').hide();
                        $('#reference').removeClass('is-invalid');
                    }
                }
            });
        });
    });
