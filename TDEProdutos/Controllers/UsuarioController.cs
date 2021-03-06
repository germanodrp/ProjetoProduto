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
using TDEProdutos.Token;

namespace TDEProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ProdutoContext Context;
        public UsuarioController()
        {
            Context = new ProdutoContext();
        }

        ///<summary>
        /// Efetuar o loguin
        /// Requer uso de token.
        /// </summary>
        /// <param UserName="UserName">loguin usuario</param>
        /// /// <param Password ="Password">senha</param>


       
       
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] Usuario model)
        {
            // Recupera o usuário
            var user = await Context._usuarios.Find<Usuario>(p => p.UserName == model.UserName).FirstOrDefaultAsync();

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            if (user.Password != model.Password)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(user);

            // Oculta a senha
            user.Password = "";

            // Retorna os dados
            return new
            {
                user = user,
                token = token
            };
        }
    }
}
