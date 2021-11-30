using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDEProdutos.Models
{
    public class Venda
    {
        [BsonRepresentation(BsonType.ObjectId)]

        public string id { get; set; }

        public int DataVenda{ get; set; }

         public string CpfVendedor{ get; set; }

        public string CpfCliente { get; set; }

        public string NomeClient { get; set; }

        public int ValorTotal { get; set; }

        public List<VendasItens>Itens { get; set; }

    }
}
