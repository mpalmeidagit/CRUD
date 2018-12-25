function add_anti_forgery_token(data) {
    data.__RequestVerificationToken = $('[name=__RequestVerificationToken]').val();
    return data;
}

function abrir_form(dados) {
    $('#id_cadastro').val(dados.Id);
    $('#txtDescricao').val(dados.Descricao);
    $('#txtValor').val(dados.Valor);

    var modal_cadastro = $('#modal_cadastro');

    $('#msg_aviso').empty();
    $('#msg_email').hide();
    $('#msg_erro').hide();
    $('#msg_mensagem_aviso').hide();
    $('#msg_mensagem_aviso').empty();

    bootbox.dialog({
        title: tituloPagina,
        message: modal_cadastro
    }).on('shown.bs.modal', function () {
        modal_cadastro.show(0, function () {
            $('#txtDescricao').focus();
        });
    }).on('hidden.bs.modal', function () {
        modal_cadastro.hide().appendTo('body');
    });

}

function criar_linha_grid(dados) {
    var retorno =
        '<tr data-id=' + dados.Id + '>' +
        '<td>' + dados.Id + '</td>' +
        '<td>' + dados.Descricao + '</td>' +
        '<td>' + dados.Valor + '</td>' +
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
         url = url_recuperar_servico,
         param = { 'id': id };
    $.post(url, add_anti_forgery_token(param), function (response) {
        if (response) {
            abrir_form(response);
            //set_focus_form();
        }
    });
}).on('click', '.btn-excluir', function () {
    var btn = $(this),
        tr = btn.closest('tr'),
        id = tr.attr('data-id'),
        url = url_remover_servico,
        param = { 'id': id };

    bootbox.confirm({
        message: "Realmente deseja excluir o serviço?",
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
         url = url_editar_servico;
    param = {
        Id: $('#id_cadastro').val(),
        Descricao: $('#txtDescricao').val(),
        Valor: $('#txtValor').val()
    };
    if (param.Descricao === "") {
        $('#txtDescricao').focus();
        swal({
            type: 'error',
            position: 'top',
            title: 'Aviso',
            text: 'Campo descrição é obrigatório',
        })
    } else if (param.Valor === "") {
        $('#txtValor').focus();
        swal({
            type: 'error',
            position: 'top',
            title: 'Aviso',
            text: 'Campo email é obrigatório',
        })
    } else {
        $.post(url, add_anti_forgery_token(param), function (response) {
            if (response.Resultado == "SUCESSO") {
                if (param.Id == 0) {
                    param.Id = response.IdSalvo;
                    var table = $('#tbl_dados').find('tbody'),
                        linha = criar_linha_grid(param);
                    table.append(linha);
                }
                else {
                    var linha = $('#tbl_dados').find('tr[data-id=' + param.Id + ']').find('td');
                    linha
                        //.eq(0).html(param.Id).end()
                        .eq(0).html(param.Descricao).end()
                        .eq(1).html(param.Valor).end()
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
            else if (response.Resultado) {
                swal({
                    type: 'error',
                    position: 'top',
                    title: 'Aviso',
                    text: response.Resultado,
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
            else if (response.Resultado) {
                swal({
                    type: 'error',
                    position: 'top',                   
                    title: 'Aviso',
                    text: response.Resultado,                    
                });
            }
        });
    }
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
    tabela = $("#tbl_dados").DataTable({
        "aaSorting": [[0, 'desc']],
        "bSort": true,
        "aoColumns": [
            null,
            null,
            null,
            { "bSortable": false }
        ]
    });
    tabela.fnSetColumnVis(0, false);
});

