using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TDEProdutos.Models;

namespace TDEProdutos.Validations
{
    public class ProdutoValidation: AbstractValidator<Produto>

    {

        public ProdutoValidation()
        {
            RuleFor(Produto => Produto.codigo)
               .NotEmpty().WithMessage("Campo codigo vazio,tente novamente ")
               .NotNull().WithMessage("Campo codigo não informado, tente novamente !")
               .Must(SomenteNumero).WithMessage("codigo invalido");



            RuleFor(Produto => Produto.NomeProduto)
                .NotEmpty().WithMessage("campo nome vazio")
                .NotNull().WithMessage("Campo nome não informado ")
                .MaximumLength(500).WithMessage("tamanho maximo excedido!")
                .Must(SomenteLetras).WithMessage("Somente Letras")
                .MinimumLength(3).WithMessage("minimo 3 caracteres")
                .MaximumLength(250).WithMessage("maximo 250 caracteres");


            RuleFor(Produto => Produto.precoVendas)
                .NotEmpty().WithMessage("campo preço vazio")
                .NotNull().WithMessage("campo preço não informado,Favor inserir !")
                .GreaterThan(0).WithMessage("preço deve ser maior que zero");
           

            RuleFor(Produto => Produto.Ativo)
                .NotEmpty().WithMessage("campo produto ativo ou inativo vazio!")
                .NotNull().WithMessage("campo produto ativo ou inativo não informado,Favor inserir");

          

            RuleFor(Produto => Produto.descricaoProduto)
                .NotEmpty().WithMessage("campo descrição vazio")
                .NotNull().WithMessage("campo descrição não informado")
                .MinimumLength(3).WithMessage("minimo 3 caracteres")
                .MaximumLength(500).WithMessage("maximo 500 caracteres");

            RuleFor(Produto => Produto.precoCusto)
                .GreaterThan(0).WithMessage("preço deve ser maior que zero");

            RuleFor(Produto =>Produto.estoque)
                .GreaterThan(0).WithMessage("a quantidade de estoque tem que ter valor minimo 0");





        }

        public static bool SomenteNumero(string Numeros)
        {
            //logica da receita
            return  Regex.IsMatch(Numeros, @"^\d$");

        }

        public static bool SomenteLetras(string palavra)
        {
            return Regex.IsMatch(palavra, @"^[a-zA-Z]+$");
        }




    }
}
