
function add_anti_forgery_token(data) {
    data.__RequestVerificationToken = $('[name=__RequestVerificationToken]').val();
    return data;
}

function abrir_form(dados) {
    $('#id_cadastro').val(dados.Id);
    $('#txtNome').val(dados.Nome);
    $('#txtEmail').val(dados.Email);
    $('#txtCPF').val(dados.CPF);
    $('#txtTelefone').val(dados.Telefone);
    $('#txtCEP').val(dados.CEP);
    $('#txtEstado').val(dados.Estado);
    $('#txtCidade').val(dados.Cidade);
    $('#txtBairro').val(dados.Bairro);
    $('#txtEndereco').val(dados.Endereco);


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
        '<td>' + dados.CPF + '</td>' +
        '<td>' + dados.Telefone + '</td>' +
       // '<td>' + dados.CEP + '</td>' +
        '<td>' + dados.Estado + '</td>' +
        //'<td>' + dados.Cidade + '</td>' +
        //'<td>' + dados.Bairro + '</td>' +
        //'<td>' + dados.Endereco + '</td>' +
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
         url = url_recuperar_cliente,
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
        url = url_remover_cliente,
        param = { 'id': id };

    bootbox.confirm({
        message: "Realmente deseja excluir o cliente?",
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
         url = url_editar_cliente;
    param = {
        Id: $('#id_cadastro').val(),
        Nome: $('#txtNome').val(),
        Email: $('#txtEmail').val(),
        CPF: $('#txtCPF').val(),
        Telefone: $('#txtTelefone').val(),
        CEP: $('#txtCEP').val(),
        Estado: $('#txtEstado').val(),
        Cidade: $('#txtCidade').val(),
        Bairro: $('#txtBairro').val(),
        Endereco: $('#txtEndereco').val()
    };
    if (param.Nome === "") {
        $('#txtNome').focus();
        swal({
            type: 'error',
            position: 'top',
            title: 'Aviso',
            text: 'Campo nome é obrigatório',
        })
    } else if (param.Email === "") {
        $('#txtEmail').focus();
        swal({
            type: 'error',
            position: 'top',
            title: 'Aviso',
            text: 'Campo email é obrigatório',
        })
    } else if (!ValidarEmail(param.Email)) {
        $('#txtEmail').focus();
        swal({
            type: 'info',
            position: 'top',
            title: 'Aviso',
            text: 'E-mail informado é invalido',
        })
    } else if (param.CPF === "") {
        $('#txtCPF').focus();
        swal({
            type: 'error',
            position: 'top',
            title: 'Aviso',
            text: 'Campo cpf é obrigatório',
        })
    }
    else if (param.Telefone === "") {
        $('#txtTelefone').focus();
        swal({
            type: 'error',
            position: 'top',
            title: 'Aviso',
            text: 'Campo telefone é obrigatório',
        })
    } else if (param.CEP === "") {
        $('#txtCEP').focus();
        swal({
            type: 'error',
            position: 'top',
            title: 'Aviso',
            text: 'Campo cep é obrigatório',
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
                        .eq(0).html(param.Nome).end()
                        .eq(1).html(param.Email).end()
                        .eq(2).html(param.CPF).end()
                        .eq(3).html(param.Telefone).end()
                        //.eq(5).html(param.CEP).end()
                        .eq(4).html(param.Estado).end()
                        //.eq(7).html(param.Cidade).end()
                        //.eq(8).html(param.Bairro).end()
                        //.eq(9).html(param.Endereco).end()
                   
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
            null,
            null,
            null,
            null,
            { "bSortable": false }
        ]
    });
    tabela.fnSetColumnVis(0, false);
    tabela.fnSetColumnVis(5, false);
    tabela.fnSetColumnVis(7, false);
    tabela.fnSetColumnVis(8, false);
    tabela.fnSetColumnVis(9, false);
});


// Preencher campos através do CEP
$(function ($) {
    $("#txtCEP").change(function () {
        var cep_code = $(this).val();
        if (cep_code.length <= 0) return;
        $.get("http://apps.widenet.com.br/busca-cep/api/cep.json", { code: cep_code },
        function (result) {
            if (result.status != 1) {
                alert(result.message || "Houve um erro desconhecido");
                return;
            }
            $("input#txtCEP").val(result.code);
            $("input#txtEstado").val(result.state);
            $("input#txtCidade").val(result.city);
            $("input#txtBairro").val(result.district);
            $("input#txtEndereco").val(result.address);
        });
    });
});


// Validar e-mail
function ValidarEmail($email) {
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    return emailReg.test($email);
}