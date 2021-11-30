using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDEProdutos.context;
using TDEProdutos.Models;

namespace TDEProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        private readonly ProdutoContext context;
        public VendaController()
        {
            context = new ProdutoContext();

        }


        [Authorize]
        [HttpPost("RegistrarVenda")]
        public ActionResult RegistrarVenda(Venda venda)
        {

            foreach (var item in venda.Itens)
            {
                var resultado = context._produtos.Find<Produto>(p => p.codigo == item.CodigoProduto).FirstOrDefault();
                if (resultado == null)
                {
                    return NotFound($"O produto {item.CodigoProduto} não existe na base de dados, venda nao pde ser feita");
                }

                if (resultado.EstoqueAtual < item.qtde)
                {
                    return BadRequest($"O produto {item.qtde} nao pode ter venda relizada, Venda maior que o estoque atual!");
                }

                resultado.EstoqueAtual = resultado.EstoqueAtual - item.qtde;

                context._produtos.ReplaceOne<Produto>(p => p.Id == resultado.Id, resultado);

            }
            context._VendaProduto.InsertOne(venda);
            return Ok(venda);
           
        }
        
    }
}

           
        

