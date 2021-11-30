using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDEProdutos.Models
{
    public class VendasItens
    {
        [BsonRepresentation(BsonType.ObjectId)]

        public string CodigoProduto;

        public string NomeProduto;

        public int valorUnitario;

        public int qtde;

        public decimal ValorTotal;
    }
}
