$(document).on("change", '#EstadoID', function (e) {
    var estadoID = $(this).find(":selected").val();
    GetCidades(estadoID, '#CidadeID')
});

function GetCidades(estado, campoCidade) {
    $(campoCidade).find("option").remove();
    var optionCampoCidade = campoCidade + 'option';
    if (estado.length > 0) {
        $(campoCidade).remove(optionCampoCidade);
        var url = $(campoCidade).data('url');
        $.getJSON(url, { estadoID: estado }, function (cidade) {
            for (i = 0; i < cidade.length; i++) {
                $(campoCidade).append('<option value="' + cidade[i].CidadeID + '">' + cidade[i].Nome + '</option>');
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            debugger;
            alert('Erro de conexão', 'Erro obtendo cidades');
        });
    } else {
        $(optionCampoCidade).remove();
        $(campoCidade).append('<option value=""></option');
    }
}