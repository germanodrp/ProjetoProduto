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
    public class EstoqueController : ControllerBase
    {
        private readonly ProdutoContext context;


        public EstoqueController()
        {
            context = new ProdutoContext();

        }

        ///<summary>
        /// Atualizar estoque de um produto a partir do Id
        /// se a quantidade for maior ou igual a quantidade de estoque
        /// Requer uso de token.
        /// </summary>
        /// <param name="Id">Id Produto</param>
        /// <returns>Objeto contendo os dados de um produto atualizado.</returns>
        [Authorize]
        [HttpPost("DebitarEstoque")]
        public ActionResult DebitarEstoque(ProdutoDebito produtoDebito)
        {
            var resultado = context._produtos.Find<Produto>(p => p.codigo == produtoDebito.codigo).FirstOrDefault();
            if (resultado == null)
            {
                return NotFound("O produto não existe na base de dados");
            }

            if (produtoDebito.qtde > resultado.EstoqueAtual)
            {
                return BadRequest("O produto não tem estoque suficiente");
            }

            resultado.EstoqueAtual = resultado.EstoqueAtual - produtoDebito.qtde;
            
            context._produtos.ReplaceOne<Produto>(p => p.Id == resultado.Id, resultado);

            return Ok("estoque debitado com sucesso");
            /*
                        if(resultado.EstoqueAtual < resultado.EstoqueMinimo)
                        {
                            using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient())
                            {
                                smtp.Host = "smtp.gmail.com";
                                smtp.Port = 587;
                                smtp.EnableSsl = true;
                                smtp.UseDefaultCredentials = false;
                                smtp.Credentials = new System.Net.NetworkCredential("germanodrp18@gamil.com", "SUASENHAdoEmail");
                            }

                            using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
                            {
                                mail.From = new System.Net.Mail.MailAddress("germanodrp18@gmail.com");
                                mail.To.Add(new System.Net.Mail.MailAddress("germanodrp18@gamil.com"));


                                mail.Subject = "estoque minimo";
                                mail.Body = "Olá, atenção. O estoque " +resultado.EstoqueAtual  + " ta baixo!";
                            }
                        }

                        return Ok("Produto debitado do estoque com sucesso!");

                    }*/

        }
    }
}