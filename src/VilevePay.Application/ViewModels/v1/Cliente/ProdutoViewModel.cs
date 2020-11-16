using System.Collections.Generic;

namespace VilevePay.Application.ViewModels.v1.Cliente
{
    public class ProdutoViewModel
    {
        public int CodigoProduto { get; set; }
        public string Nome { get; set; }
        public IEnumerable<ProdutoItemViewModel> Itens { get; set; }
    }
}