namespace VilevePay.Application.ViewModels.v1.Cliente
{
    public class ProdutoItemViewModel
    {
        public int CodigoProdutoItem { get; set; }
        public int CodigoProduto { get; set; }
        public string Nome { get; set; }
        public string Adicional => "Pais, sogros, irmãos, netos, tios, sobrinhos, genros e noras.";
        public int Dependentes => 10;
    }
}