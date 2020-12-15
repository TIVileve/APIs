namespace VilevePay.Application.ViewModels.v1.Cliente
{
    public class CadastrarDebitoContaViewModel
    {
        public int CodigoBancoConvencionado { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public int DataCobranca { get; set; }
    }
}