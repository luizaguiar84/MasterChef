
$(".idDetalhes").click(function () {

    var id = $(this).attr('id');
    $.ajax({
        type: "Get",
        url: "/Home/GetById",
        data: {
            'id': id
        },
        success: function (data) {

            $('#modalDetalhes').modal('show');
            $('#idTitulo').text(data.title);
            $('#idModoFazer').text(data.wayOfPrepare);
            $('#idimagem').attr("src", 'img/' + data.image);

            $('#tbody').text("");

            var ingredients = data.ingredients;
            $.each(ingredients, function (key, value) {

                $('#tbody').append("<tr> <td>" + value.name + "</td><td>" + value.weight + "</td><td>" + value.description +
                    " - " + value.quantity + "</td>" + "</tr>");

            });
        }
    });

});


$(".deletarIdIngrediente").click(function () {

    var id = $(this).attr('id');

    $.ajax({
        type: "Get",
        url: "/Ingredient/BuscarPorReceitaId",
        data: {
            'id': id
        },
        success: function (data) {

            $('#modalExcluirIngrediente').modal('show');
            $('#hdIdExcluirIngrediente').val(data.id);
            $('#idNomeIngrediente').text(data.name);
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
        url: "/Recipe/BuscarPorId",
        data: {
            'id': id
        },
        success: function (data) {

            $('#modalExcluir').modal('show');
            $('#hdIdExcluir').val(data.id);
            $('#idNome').text(data.title);
            console.log(data)
        }
    });

});

