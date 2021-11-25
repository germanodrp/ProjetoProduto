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
using TDEProdutos.Validations;


namespace TDEProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriaController : ControllerBase
    {
        private readonly ProdutoContext context;


        public CategoriaController()
        {
            context = new ProdutoContext();

        }

        ///<summary>
        /// Consultar dados de uma categoria de um produto a partir do Id
        /// Requer uso de token.
        /// </summary>
        /// <param name="Id">Id Produto</param>
        /// <returns>Objeto contendo os dados de um produto.</returns>

        
        [HttpGet("BuscarPorCategoria/{Id}")]

            public ActionResult BuscarPorCategoria(string Id)
            {
            return Ok(context._produtos.Find<Produto>(c => c.Id == Id).FirstOrDefault());
        }

        ///<summary>
        /// Adicionar dados de um produto a partir do Id
        /// Requer uso de token.
        /// </summary>
        /// <param name="Id">Id Produto</param>
        /// <returns>Objeto contendo os dados de um produto.</returns>

        [HttpPost("Adicionar")]
        [ProducesResponseType(typeof(Categoria), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult AdicionarCategoria (Categoria categoria)
        {
            CategoriaVallidation categoriavallidation = new CategoriaVallidation();
            var validacao = categoriavallidation.Validate(categoria);
            if (!validacao.IsValid)
            {
                List<string> erros = new List<string>();
                foreach (var failure in validacao.Errors)
                {
                    erros.Add("Property" + failure.PropertyName +
                        "failed validation. Error Was: "
                        + failure.ErrorMessage);
                }
            }
            return Ok();
        }



        /*

         [HttpPut("desativar{IDcategoria}")]

         public ActionResult Desativar(int IDcategoria)
         {
             var CATEGORIADesativado = ListaCategoria.Where(C => C.IDcategoria == IDcategoria).FirstOrDefault();
             if (CATEGORIADesativado == null) return NotFound("Produto não pode ser desativado, pois codigo não existe");


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


                 mail.Subject = "IDcategoria desativado";
                 mail.Body = "Olá, atenção. O ID " + IDcategoria + " foi desativado do seu catalogo!";
             }

             return Ok("IDcategoria desativo");

         }*/
        ///<summary>
        /// Atualizar dados de uma categoria de um produto a partir do Id
        /// Requer uso de token.
        /// </summary>
        /// <param name="Id">Id Produto</param>
        /// <returns>Objeto contendo os dados de um produto.</returns>

        [HttpPut("Atualizar/{id}")]
        public ActionResult Atualizar(string id, [FromBody] Categoria categoria)
        {

            var pResultado = context._categoria.Find<Categoria>(c => c.Id == id).FirstOrDefault();
            if (pResultado == null) return
                    NotFound("Id não encontrado, atualizacao não realizada!");

            categoria.Id = id;
            context._categoria.ReplaceOne<Categoria>(c => c.Id == id, categoria);

            return NoContent();

        }

        ///<summary>
        /// Remover dados de uma categoria de um produto a partir do Id
        /// Requer uso de token.
        /// </summary>
        /// <param name="Id">Id Produto</param>
        /// 


        [HttpDelete("Remover/{id}")]
        public ActionResult Remova(string id)
        {
            var pResultado = context._categoria.Find<Categoria>(c => c.Id == id).FirstOrDefault();
            if (pResultado == null) return
                    NotFound("Id não encontrado, atualizacao não realizada!");

            context._categoria.DeleteOne<Categoria>(filter => filter.Id == id);
            return NoContent();
        }







    }
}
