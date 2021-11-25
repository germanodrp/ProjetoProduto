using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDEProdutos.Models;

namespace TDEProdutos.Validations
{
    public class CategoriaVallidation : AbstractValidator<Categoria>
    {
        public CategoriaVallidation()
        {
            RuleFor(Categoria => Categoria.IDcategoria)
              .NotEmpty().WithMessage("Campo categoria vazio,tente novamente ")
              .NotNull().WithMessage("Campo categoria não informado, tente novamente !");


        }
    }
}