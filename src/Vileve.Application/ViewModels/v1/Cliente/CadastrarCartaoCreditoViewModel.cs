using System;

namespace Vileve.Application.ViewModels.v1.Cliente
{
    public class CadastrarCartaoCreditoViewModel
    {
        public string NomeTitularCartao { get; set; }
        public string Cpf { get; set; }
        public string NumeroCartao { get; set; }
        public DateTime Validade { get; set; }
        public string Cvc { get; set; }
    }
}