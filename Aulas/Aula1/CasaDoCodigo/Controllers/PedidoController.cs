using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CasaDoCodigo.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IPedidoRepository pedidoRepository;
        private readonly IItemPedidoRepository itemPedidoRepository;

        public PedidoController(IProdutoRepository produtoRepository, 
            IPedidoRepository pedidoRepository, 
            IItemPedidoRepository itemPedidoRepository)
        {
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
            this.itemPedidoRepository = itemPedidoRepository;
        }

        // GET: /<controller>/
        public IActionResult Carrossel()
        {
            var listaProdutos = produtoRepository.GetProdutos();

            return View(listaProdutos);
        }

        public IActionResult Carrinho(string codigoProduto)
        {
            if (!string.IsNullOrEmpty(codigoProduto))
            {
                pedidoRepository.AddItem(codigoProduto);
            }

            Pedido pedido = pedidoRepository.GetPedido();
            CarrinhoViewModel carrinhoViewModel = new CarrinhoViewModel(pedido.Itens);

            return View(carrinhoViewModel);
        }

        public IActionResult Cadastro()
        {
            var pedido = pedidoRepository.GetPedido();

            if (pedido == null)
            {
                return RedirectToAction("Carrossel");
            }

            return View(pedido.Cadastro);
        }

        [HttpPost]
        public void SalvarCadastro([FromBody] Cadastro cadastro)
        {

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Resumo(Cadastro cadastro)
        {
            if (ModelState.IsValid)
            {
                Pedido pedido = pedidoRepository.UpdateCadastro(cadastro);
                return View(pedido);
            }
            
            return RedirectToAction("Cadastro");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public UpdateQuantidadeResponse UpdateQuantidade([FromBody]ItemPedido itemPedido)
        {
            return pedidoRepository.UpdateQuantidade(itemPedido);
        }
    }
}
