
$(".idDetalhes").click(function () {

    var id = $(this).attr('id');
    $.ajax({
        type: "Get",
        url: "/Home/BuscarPorId",
        data: {
            'id': id
        },
        success: function (data) {

            $('#modalDetalhes').modal('show');
            $('#idTitulo').text(data.titulo);
            $('#idModoFazer').text(data.modoFazer);
            $('#idimagem').attr("src", 'img/' + data.imagem);

            var ingredientes = data.ingredientes;
            $.each(ingredientes, function (key, value) {

                $('#tbody > tr').append("<td>" + value.nome + "</td><td>" + value.peso + "</td><td>" + value.descricao +
                    " - " + value.quantidade + "</td>" + "<br />");

            });
        }
    });

});


$(".deletarIdIngrediente").click(function () {

    var id = $(this).attr('id');

    $.ajax({
        type: "Get",
        url: "/Ingrediente/BuscarPorId",
        data: {
            'id': id
        },
        success: function (data) {

            $('#modalExcluirIngrediente').modal('show');
            $('#hdIdExcluirIngrediente').val(data.id);
            $('#idNomeIngrediente').text(data.nome);
            console.log(data)
        }
    });

});


$(".btn-close").click(function () {

    $('#modalDetalhes').modal('hide');
});



$(".deletarId").click(function () {

    var id = $(this).attr('id');

    console.log(id);
    $.ajax({
        type: "Get",
        url: "/Receita/BuscarPorId",
        data: {
            'id': id
        },
        success: function (data) {

            $('#modalExcluir').modal('show');
            $('#hdIdExcluir').val(data.id);
            $('#idNome').text(data.titulo);
            console.log(data)
        }
    });

});

