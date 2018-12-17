function add_anti_forgery_token(data) {
    data.__RequestVerificationToken = $('[name=__RequestVerificationToken]').val();
    return data;
}

var Resultado;

function LimparCampos() {
    var CampoNome = $("#txtNome").val('');
    var CampoEmail = $("#txtEmail").val('');
    var CampoCPF = $("#txtCPF").val('');
    var CampoTelefone = $("#txtTelefone").val('');
    var CampoCEP = $("#txtCEP").val('');
    var CampoEstado = $("#txtEstado").val('');
    var CampoCidade = $("#txtCidade").val('');
    var CampoBairro = $("#txtBairro").val('');
    var CampoEndereco = $("#txtEndereco").val('');

    $('#msg_mensagem_aviso').empty();
    $('#msg_aviso').hide();
    $('#msg_mensagem_aviso').hide();
    $('#msg_erro').hide();
}


$(document).on('click', '#btn_incluir', function () {
    var btn = $(this),
         url = url_cadastrar_cliente,
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
      $("#txtNome").focus();
  });



function formatar_mensagem_aviso(mensagens) {
    var retorno = '';
    for (var i = 0; i < mensagens.length; i++) {
        retorno += '<li>' + mensagens[i] + '</li>';
    }

    return '<ul>' + retorno + '</ul>';
}


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