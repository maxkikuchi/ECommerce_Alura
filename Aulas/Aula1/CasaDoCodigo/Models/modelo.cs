using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models
{
    [DataContract]
    public class BaseModel
    {
        [DataMember]
        public int Id { get; protected set; }
    }

    public class Produto : BaseModel
    {
        public Produto()
        {

        }

        [Required]
        public string Codigo { get; private set; }
        [Required]
        public string Nome { get; private set; }
        [Required]
        public decimal Preco { get; private set; }

        public Produto(string codigo, string nome, decimal preco)
        {
            this.Codigo = codigo;
            this.Nome = nome;
            this.Preco = preco;
        }
    }

    [DataContract]
    public class Cadastro : BaseModel
    {
        public Cadastro()
        {
        }

        public virtual Pedido Pedido { get; set; }
        [MinLength(5, ErrorMessage = "Nome deve ter no m�nimo 5 caracteres")]
        [MaxLength(50, ErrorMessage = "Nome deve ter no m�ximo 50 caracteres")]
        [Required(ErrorMessage ="Nome � obrigat�rio")]
        [DataMember]
        public string Nome { get; set; } = "";
        [EmailAddress(ErrorMessage = "Email est� inv�lido")]
        [Required(ErrorMessage = "Email � obrigat�rio")]
        [DataMember]
        public string Email { get; set; } = "";
        [Required(ErrorMessage = "Telefone � obrigat�rio")]
        [DataMember]
        public string Telefone { get; set; } = "";
        [Required(ErrorMessage = "Endere�o � obrigat�rio")]
        [DataMember]
        public string Endereco { get; set; } = "";
        [Required(ErrorMessage = "Complemento � obrigat�rio")]
        [DataMember]
        public string Complemento { get; set; } = "";
        [Required(ErrorMessage = "Bairro � obrigat�rio")]
        [DataMember]
        public string Bairro { get; set; } = "";
        [Required(ErrorMessage = "Munic�pio � obrigat�rio")]
        [DataMember]
        public string Municipio { get; set; } = "";
        [Required(ErrorMessage = "UF � obrigat�rio")]
        [DataMember]
        public string UF { get; set; } = "";
        [Required(ErrorMessage = "CEP � obrigat�rio")]
        [DataMember]
        public string CEP { get; set; } = "";

        internal void Update(Cadastro novoCadastro)
        {
            this.Nome = novoCadastro.Nome;
            this.Email = novoCadastro.Email;
            this.Telefone = novoCadastro.Telefone;
            this.Endereco = novoCadastro.Endereco;
            this.Complemento = novoCadastro.Complemento;
            this.Bairro = novoCadastro.Bairro;
            this.Municipio = novoCadastro.Municipio;
            this.UF = novoCadastro.UF;
            this.CEP = novoCadastro.CEP;
        }
    }

    [DataContract]
    public class ItemPedido : BaseModel
    {   
        [Required]
        public Pedido Pedido { get; private set; }
        [Required]
        public Produto Produto { get; private set; }
        [Required]
        [DataMember]
        public int Quantidade { get; private set; }
        [Required]
        public decimal PrecoUnitario { get; private set; }
        [DataMember]
        public decimal Subtotal => Quantidade * PrecoUnitario;

        public ItemPedido()
        {

        }

        public ItemPedido(Pedido pedido, Produto produto, int quantidade, decimal precoUnitario)
        {
            Pedido = pedido;
            Produto = produto;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }

        internal void AtualizaQuantidade(int quantidade)
        {
            Quantidade = quantidade;
        }
    }

    public class Pedido : BaseModel
    {
        public Pedido()
        {
            Cadastro = new Cadastro();
        }

        public Pedido(Cadastro cadastro)
        {
            Cadastro = cadastro;
        }

        public List<ItemPedido> Itens { get; private set; } = new List<ItemPedido>();
        [Required]
        public virtual Cadastro Cadastro { get; private set; }
    }
}
