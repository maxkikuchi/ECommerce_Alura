class Cadastro {
    salvarCadastro() {
        debugger
        let data = this.getData()
        this.postCadastro(data)
    };

    getData() {
        var nomeCliente = $('#nomeCliente').val()
        var email = $('#email').val()
        var telefone = $('#telefone').val()
        var endereco = $('#endereco').val()
        var complemento = $('#complemento').val()
        var bairro = $('#bairro').val()
        var municipio = $('#municipio').val()
        var cep = $('#cep').val()

        return {
            Nome: nomeCliente,
            Email: email,
            Telefone: telefone,
            Endereco: endereco,
            Complemento: complemento,
            Bairro: bairro,
            Municipio: municipio,
            UF: 'PR',
            CEP: cep
        }
    }

    postCadastro(data) {
        $.ajax({
            url: '/Pedido/SalvarCadastro',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).done(function (response) {


        });
    }
}

var cadastro = new Cadastro();