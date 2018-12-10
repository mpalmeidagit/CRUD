

$("#btn_incluir").on("click", function (e) {

    e.preventDefault();

    var campoNome = $("#txtNome").val();
    var campoEmail = $("#txtEmail").val();
    var campoCPF = $("#txtCPF").val();
    var campoTelefone = $("#txtTelefone").val();
    var campoCEP = $("#txtCEP").val();
    var campoEstado = $("#txtEstado").val();
    var campoCidade = $("#txtCidade").val();
    var campoBairro = $("#txtBairro").val();
    var campoEndereco = $("#txtEndereco").val();

    if (campoNome.length > 0 && campoEmail.length > 0 && campoCPF.length > 0 && campoTelefone.length > 0 && campoCEP.length > 0 && campoEstado.length > 0 && campoCidade.length > 0 && campoBairro.length > 0 && campoEndereco.length > 0) {
        var objeto = JSON.stringify({
            campoNome: campoNome,
            campoEmail: campoEmail,
            campoCPF: campoCPF,
            campoTelefone: campoTelefone,
            campoCEP: campoCEP,
            campoEstado: campoEstado,
            campoCidade: campoCidade,
            campoBairro: campoBairro,
            campoEndereco: campoEndereco
        });
        $.ajax({
            type: "POST",
            url: url_cadastrar,
            data: objeto,
            contentType: "application/json; charset=utf-8",
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            },

            success: function (data) {

                console.log(data);


            }
        });
    }

});