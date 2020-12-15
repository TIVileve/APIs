namespace VilevePay.Application.ViewModels.v1.Cliente
{
    public class CadastrarPagamentoViewModel
    {
        public int TipoPagamento { get; set; }
        public CadastrarCartaoCreditoViewModel CartaoCredito { get; set; }
        public CadastrarBoletoBancarioViewModel BoletoBancario { get; set; }
        public CadastrarDebitoContaViewModel DebitoConta { get; set; }
        public CadastrarConvenioViewModel Convenio { get; set; }
    }
}