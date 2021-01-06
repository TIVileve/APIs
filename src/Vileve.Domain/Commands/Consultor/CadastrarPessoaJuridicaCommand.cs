using MediatR;
using Vileve.Domain.Validations.Consultor;

namespace Vileve.Domain.Commands.Consultor
{
    public class CadastrarPessoaJuridicaCommand : ConsultorCommand, IRequest<bool>
    {
        public CadastrarPessoaJuridicaCommand(string codigoConvite, string numeroCelular, string cnpj, string razaoSocial, string nomeFantasia,
            string inscricaoMunicipal, string inscricaoEstadual, string codigoBanco, string agencia, string contaSemDigito,
            string digito, int tipoConta, string contratoSocialBase64, string ultimaAlteracaoBase64)
        {
            CodigoConvite = codigoConvite;
            NumeroCelular = numeroCelular;
            Cnpj = cnpj;
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
            InscricaoMunicipal = inscricaoMunicipal;
            InscricaoEstadual = inscricaoEstadual;
            CodigoBanco = codigoBanco;
            Agencia = agencia;
            ContaSemDigito = contaSemDigito;
            Digito = digito;
            TipoConta = tipoConta;
            ContratoSocialBase64 = contratoSocialBase64;
            UltimaAlteracaoBase64 = ultimaAlteracaoBase64;
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarPessoaJuridicaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}