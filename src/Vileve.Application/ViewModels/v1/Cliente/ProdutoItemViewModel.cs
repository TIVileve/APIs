using System.Collections.Generic;

namespace Vileve.Application.ViewModels.v1.Cliente
{
    public class ProdutoItemViewModel
    {
        public int CodigoProdutoItem { get; set; }
        public int CodigoProduto { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
        public int QuantidadeMaximaDependente { get; set; }
        public IEnumerable<ProdutoParentescosViewModel> ParentescosAdicionais { get; set; }
    }
}