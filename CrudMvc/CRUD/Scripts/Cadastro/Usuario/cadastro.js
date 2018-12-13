function add_anti_forgery_token(data) {
    data.__RequestVerificationToken = $('[name=__RequestVerificationToken]').val();
    return data;
}


function LimparCampos() {
    var CampoNome = $("#txt_nome").val('');
    var CampoEmail = $("#txt_email").val('');
    var CampoLogin = $("#txt_login").val('');
    var CampoSenha = $("#txt_senha").val('');
}


$(document).on('click', '#btn_incluir', function () {
    var btn = $(this),
         url = url_cadastrar_usuario,
        param = {
            Id: $('#id_cadastro').val(),
            Nome: $('#txt_nome').val(),
            Email: $('#txt_email').val(),
            Login: $('#txt_login').val(),
            Senha: $('#txt_senha').val()
        };
    if (param.Nome === "") {
        $('#txt_nome').focus();
        swal({
            type: 'error',
            position: 'top',
            title: 'Aviso',
            text: 'Campo nome é obrigatório',
        })
    } else if (param.Email === "") {
        $('#txt_email').focus();
        swal({
            type: 'error',
            position: 'top',
            title: 'Aviso',
            text: 'Campo email é obrigatório',
        })
    } else if (param.Login === "") {
        $('#txt_login').focus();
        swal({
            type: 'error',
            position: 'top',
            title: 'Aviso',
            text: 'Campo login é obrigatório',
        })
    }
    else if (param.Senha === "") {
        $('#txt_senha').focus();
        swal({
            type: 'error',
            position: 'top',
            title: 'Aviso',
            text: 'Campo senha é obrigatório',
        })
    } else {
        $.post(url, add_anti_forgery_token(param), function (response) {
            if (response.Resultado === "SUCESSO") {
                if (param.Id === 0) {
                    param.Id = response.IdSalvo;
                }
                swal({
                    position: 'top',
                    type: 'success',
                    text: 'Operação realizada com sucesso.',
                    showConfirmButton: false,
                    timer: 2500
                });
                LimparCampos();
                $('#msg_mensagem_aviso').empty();
                $('#msg_aviso').hide();
                $('#msg_mensagem_aviso').hide();
                $('#msg_erro').hide();
            }
            else if (response.Resultado === "ERRO") {
                $('#msg_aviso').hide();
                $('#msg_mensagem_aviso').hide();
                $('#msg_erro').show();
            }
            else if (response.Resultado === "AVISO") {
                $('#msg_mensagem_aviso').html(formatar_mensagem_aviso(response.Mensagens));
                $('#msg_aviso').show();
                $('#msg_mensagem_aviso').show();
                $('#msg_erro').hide();
            }
            else if (response.Resultado === "Retorno") {
                $('#msg_mensagem_aviso').html(formatar_mensagem_aviso(response.Mensagens));
                $('#msg_aviso').show();
                $('#msg_mensagem_aviso').show();
                $('#msg_erro').hide();
            }
        });
    }
})
  .on('click', '#btn_cancelar', function () {
      LimparCampos();
      $("#txt_nome").focus();
  });



function formatar_mensagem_aviso(mensagens) {
    var retorno = '';
    for (var i = 0; i < mensagens.length; i++) {
        retorno += '<li>' + mensagens[i] + '</li>';
    }

    return '<ul>' + retorno + '</ul>';
}
