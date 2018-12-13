function add_anti_forgery_token(data) {
    data.__RequestVerificationToken = $('[name=__RequestVerificationToken]').val();
    return data;
}

function abrir_form(dados) {
    $('#id_cadastro').val(dados.Id);
    $('#txt_nome').val(dados.Nome);
    $('#txt_email').val(dados.Email);
    $('#txt_login').val(dados.Login);
    $('#txt_senha').val(dados.Senha);

    var modal_cadastro = $('#modal_cadastro');

    $('#msg_mensagem_aviso').empty();
    $('#msg_aviso').hide();
    $('#msg_mensagem_aviso').hide();
    $('#msg_erro').hide();


    bootbox.dialog({
        title: '@ViewBag.Title',
        message: modal_cadastro
    })
    .on('shown.bs.modal', function () {
        modal_cadastro.show(0, function () {
            $('#txt_nome').focus();
        });
    })
    .on('hidden.bs.modal', function () {
        modal_cadastro.hide().appendTo('body');
    });
}


function criar_linha_grid(dados) {
    var retorno =
        '<tr data-id=' + dados.Id + '>' +
        //'<td>' + dados.Id + '</td>' +
        '<td>' + dados.Nome + '</td>' +
        '<td>' + dados.Email + '</td>' +
        '<td>' + dados.Login + '</td>' +
        //'<td>' + dados.Senha + '</td>' +
        '<td class="text-center">' +
        '<a class="btn btn-primary btn-alterar" role="button" style="margin-right: 3px"><i class="glyphicon glyphicon-pencil"></i> </a>' +
        '<a class="btn btn-danger btn-excluir" role="button"><i class="glyphicon glyphicon-trash"></i></a>' +
        '</td>' +
        '</tr>';
    return retorno;

}

$(document).on('click', '.btn-alterar', function () {
    $('#txt_senha').attr('readonly', true);
    var btn = $(this),
         id = btn.closest('tr').attr('data-id'),
         url = url_recuperar_usuarios,
         param = { 'id': id };
    $.post(url, add_anti_forgery_token(param), function (response) {
        if (response) {
            abrir_form(response);
            //set_focus_form();
        }

    });
})

.on('click', '.btn-excluir', function () {
    var btn = $(this),
        tr = btn.closest('tr'),
        id = tr.attr('data-id'),
        url = url_remover_usuario,
        param = { 'id': id };
    bootbox.confirm({
        message: "Realmente deseja excluir o usuário?",
        buttons: {
            confirm: {
                label: 'Sim',
                className: 'btn-success'
            },
            cancel: {
                label: 'Não',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result) {
                $.post(url, add_anti_forgery_token(param), function (response) {
                    if (response) {
                        tr.remove();
                    } swal({
                        position: 'top',
                        type: 'success',
                        text: 'Operação realizada com sucesso.',
                        showConfirmButton: false,
                        timer: 2500
                    });
                });
            }
        }
    });
})
.on('click', '#btn_confirmar', function () {
    var btn = $(this),
         url = url_editar_usuario;
        param = {
            Id: $('#id_cadastro').val(),
            Nome: $('#txt_nome').val(),
            Email: $('#txt_email').val(),
            Login: $('#txt_login').val(),
            Senha: $('#txt_senha').val()
        };
    $.post(url, add_anti_forgery_token(param), function (response) {
        if (response.Resultado == "SUCESSO") {
            if (param.Id == 0) {
                param.Id = response.IdSalvo;
                var table = $('#tbl_usuarios').find('tbody'),
                    linha = criar_linha_grid(param);
                table.append(linha);
            }
            else {
                var linha = $('#tbl_usuarios').find('tr[data-id=' + param.Id + ']').find('td');
                linha
                    //.eq(0).html(param.Id).end()
                    .eq(0).html(param.Nome).end()
                    .eq(1).html(param.Email).end()
                    .eq(2).html(param.Login).end();
                //.eq(3).html(param.Senha).end()
            }
            $('#modal_cadastro').parents('.bootbox').modal('hide');
            swal({
                position: 'top',
                type: 'success',
                text: 'Operação realizada com sucesso.',
                showConfirmButton: false,
                timer: 2500
            });
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
    });
});

function formatar_mensagem_aviso(mensagens) {
    var retorno = '';
    for (var i = 0; i < mensagens.length; i++) {
        retorno += '<li>' + mensagens[i] + '</li>';
    }

    return '<ul>' + retorno + '</ul>';
}



// Inicia o datatable
$(document).ready(function () {
    tabela = $("#tbl_usuarios").DataTable({
        "aaSorting": [[0, 'desc']],
        "bSort": true,
        "aoColumns": [
            null,
            null,
            null,
            null,
            null,
            { "bSortable": false }
        ]
    });
    tabela.fnSetColumnVis(0, false);
    tabela.fnSetColumnVis(4, false);
});
