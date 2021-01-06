using System.Collections.Generic;
using MediatR;
using Vileve.Domain.Validations.Consultor;

namespace Vileve.Domain.Commands.Consultor
{
    public class CadastrarRepresentanteCommand : ConsultorCommand, IRequest<bool>
    {
        public CadastrarRepresentanteCommand(string codigoConvite, string numeroCelular, string cpf, string nomeCompleto, int sexo,
            int estadoCivil, string nacionalidade, IEnumerable<object> emails, IEnumerable<object> telefones, string documentoFrenteBase64,
            string documentoVersoBase64)
        {
            CodigoConvite = codigoConvite;
            NumeroCelular = numeroCelular;
            Cpf = cpf;
            NomeCompleto = nomeCompleto;
            Sexo = sexo;
            EstadoCivil = estadoCivil;
            Nacionalidade = nacionalidade;
            Emails = emails;
            Telefones = telefones;
            DocumentoFrenteBase64 = documentoFrenteBase64;
            DocumentoVersoBase64 = documentoVersoBase64;
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarRepresentanteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}