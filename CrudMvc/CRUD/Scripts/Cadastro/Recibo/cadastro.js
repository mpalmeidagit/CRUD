﻿function add_anti_forgery_token(data) {
    data.__RequestVerificationToken = $('[name=__RequestVerificationToken]').val();
    return data;
}


function LimparCampos() {
    var CampoData = $("#txt_data").val('');

    $('#msg_mensagem_aviso').empty();
    $('#msg_aviso').hide();
    $('#msg_mensagem_aviso').hide();
    $('#msg_erro').hide();
}


$(document).on('click', '#btn_incluir', function () {

    var btn = $(this),
         url = url_cadastrar_recibo,
        param = {
            Id: $('#id_cadastro').val(),
            Data: $('#txt_data').val(),      
            IdServico: $('#ddl_servico').val(),
            IdCliente: $('#ddl_cliente').val()
        };
    if (param.Data === "") {
        $('#txt_data').focus();
        swal({
            type: 'error',
            position: 'top',
            title: 'Aviso',
            text: 'Campo data é obrigatório',
        })
    } else if (param.IdServico == "-- Selecione um Clinte --") {
        $('#ddl_servico').focus();
        swal({
            type: 'error',
            position: 'top',
            title: 'Aviso',
            text: 'Campo serviço é obrigatório',
        })
    }
    else {
        $.post(url, add_anti_forgery_token(param), function (response) {
            if (response.Resultado == "SUCESSO") {
                if (param.Id == 0) {
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
            else if (response.Resultado == "ERRO") {
                $('#msg_aviso').hide();
                $('#msg_mensagem_aviso').hide();
                $('#msg_erro').show();
            }
            else if (response.Resultado == "AVISO") {
                $('#msg_mensagem_aviso').html(formatar_mensagem_aviso(response.Mensagens));
                $('#msg_aviso').show();
                $('#msg_mensagem_aviso').show();
                $('#msg_erro').hide();
            }
            else if (response.Resultado) {
                swal({
                    position: 'top',
                    type: 'error',
                    text: response.Resultado,
                    title: 'Aviso',
                });
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