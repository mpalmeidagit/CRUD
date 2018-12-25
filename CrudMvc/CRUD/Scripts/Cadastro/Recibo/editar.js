function add_anti_forgery_token(data) {
    data.__RequestVerificationToken = $('[name=__RequestVerificationToken]').val();
    return data;
}

function abrir_form(dados) {

    var data = moment(dados.Data).toDate();


    $('#id_cadastro').val(dados.Id);
    $('#txt_data').val(moment(data).format("DD/MM/YYYY"));
    $('#ddl_servico').val(dados.IdServico);
    $('#ddl_cliente').val(dados.IdCliente);
    var modal_cadastro = $('#modal_cadastro');

    $('#msg_mensagem_aviso').empty();
    $('#msg_aviso').hide();
    $('#msg_mensagem_aviso').hide();
    $('#msg_erro').hide();


    bootbox.dialog({
        title: tituloPagina,
        message: modal_cadastro
    })
    .on('shown.bs.modal', function () {
        modal_cadastro.show(0, function () {
            $('#txt_data').focus();
        });
    })
    .on('hidden.bs.modal', function () {
        modal_cadastro.hide().appendTo('body');
    });
}


function criar_linha_grid(dados) {

    var retorno =
        '<tr data-id=' + dados.Id + '>' +
        '<td>' + dados.Id + '</td>' +
        '<td>' + dados.Data + '</td>' +
        '<td>' + dados.Descricao + '</td>' +
        '<td>' + dados.Nome + '</td>' +
        '<td>' + dados.Telefone + '</td>' +       
        //'<td>' + dados.IdPerfil + '</td>' +
        //'<td>' + dados.Senha + '</td>' +
        '<td class="text-center">' +
        '<a class="btn btn-primary btn-alterar" role="button" style="margin-right: 3px"><i class="glyphicon glyphicon-pencil"></i> </a>' +
        '<a class="btn btn-danger btn-excluir" role="button"><i class="glyphicon glyphicon-trash"></i></a>' +
        '</td>' +
        '</tr>';
    return retorno;

}


function formatDate(date) {
    var data = date.replace('/Date(', '');
    data = data.replace(')/', '');
    data = new Date(parseInt(data));
    // console.log(data.format("dd/MM/yyyy"));

}


$(document).on('click', '.btn-alterar', function () {
    $('#txt_senha').attr('readonly', true);
    var btn = $(this),
         id = btn.closest('tr').attr('data-id'),
         url = url_recuperar_recibo,
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
        url = url_remover_recibo,
        param = { 'id': id };

    bootbox.confirm({
        message: "Realmente deseja excluir o recibo?",
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
         url = url_editar_recibo;
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
                        .eq(0).html(param.Data).end()
                        //.eq(1).html().end()
                        .eq(3).html(param.Nome).end()
                    //.eq(3).html(param.IdPerfil).end();
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
            else if (response.Resultado) {
                swal({
                    position: 'top',
                    type: 'error',
                    text: response.Resultado,
                    title: 'Aviso',
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
                    position: 'top',
                    type: 'error',
                    text: response.Resultado,
                    title: 'Aviso',
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
            null,
            null,
            null,
            { "bSortable": false }
        ]
    });
    tabela.fnSetColumnVis(0, false);
});


