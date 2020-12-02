using System.ComponentModel;

namespace VilevePay.Domain.Enums
{
    public enum StatusOnboarding
    {
        [Description("Validação do token")]
        ValidacaoToken = 0,

        [Description("Validação e-mail")]
        ValidacaoEmail = 1,

        [Description("Validação da senha")]
        ValidacaoSenha = 2,

        [Description("Contrato social")]
        ContratoSocial = 3,

        [Description("Endereço do CNPJ")]
        EnderecoCnpj = 4,

        [Description("Dados do representante")]
        DadosRepresentante = 5,

        [Description("Endereço do representante")]
        EnderecoRepresentante = 6
    }
}