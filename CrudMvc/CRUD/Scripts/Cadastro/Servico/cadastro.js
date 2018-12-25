function add_anti_forgery_token(data) {
    data.__RequestVerificationToken = $('[name=__RequestVerificationToken]').val(); 
    return data;
}
var Resultado;

function LimparCampos() {
    var campoDescricao = $("#txtDescricao").val('');
    var campoValor = $("#txtValor").val('');

    $('#msg_aviso').empty();
    $('#msg_email').hide();
    $('#msg_erro').hide();
    $('#msg_mensagem_aviso').hide();
    $('#msg_mensagem_aviso').empty();
}

$(document).on('click', '#btn_incluir', function () {
    var btn = $(this),
        url = url_cadastrar_servico,
        param = {
            //Id: $('#id_cadastro').val(),
            Descricao: $('#txtDescricao').val(),
            Valor: $('#txtValor').val()
        };
    if (param.Descricao === "") {
        $('#txtDescricao').focus();
        swal({
            type: 'error',
            position: 'top',
            title: 'Aviso',
            text: 'Campo descrição é obrigatório'
        })
    } else if (param.Valor === "") {
        $('#txtValor').focus();
        swal({
            type: 'error',
            position: 'top',
            title: 'Aviso',
            text: 'Campo valor é obrigatório',
        })
    } else {
        $.post(url, add_anti_forgery_token(param), function (response) {
            if (response.Resultado == "SUCESSO") {
                if (param.Id == 0) {
                    param.Id = response.IdSalvo;
                }
                swal({
                    type: 'success',
                    position: 'top',
                    text: 'Operação realizada com sucesso',
                    showConfirmButton: false,
                    timer: 2500
                });
                LimparCampos();
            } else if (response.Resultado === "ERRO") {
                $('#msg_aviso').hide();
                $('#msg_erro').show();
                $('#msg_mensagem_aviso').hide();
            }
            else if (response.Resultado === "AVISO") {
                $('#msg_aviso').show();
                $('#msg_erro').hide();
                $('#msg_mensagem_aviso').show();
                $('#msg_mensagem_aviso').html(formatar_mensagem_aviso(response.Mensagens));
            } else if (response.Resultado) {
                swal({
                    type: 'error',
                    position: 'top',
                    text: response.Resultado,
                    title: 'Aviso',
                });
            }
        });
    }
}).on('click', '#btn_cancelar', function () {
    LimparCampos();
    $("#txtDescricao").focus();
});

function formatar_mensagem_aviso(mensgens) {
    var retorno = '';
    for (var i = 0; i < mensgens.length; i++) {
        retorno += '<li>' + mensgens[i] + '</li>';
    }

    return '<ul>' + retorno + '</ul>';
}


