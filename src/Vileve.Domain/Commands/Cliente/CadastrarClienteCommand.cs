using System;
using MediatR;
using Vileve.Domain.Validations.Cliente;

namespace Vileve.Domain.Commands.Cliente
{
    public class CadastrarClienteCommand : ClienteCommand, IRequest<object>
    {
        public CadastrarClienteCommand(string cpf, string nomeCompleto, DateTime dataNascimento, string email,
            string telefoneFixo, string telefoneCelular, Guid? consultorId,
            long? inssNumeroBeneficio, double? inssSalario, int? inssEspecie, int? outrosDiaPagamento)
        {
            Cpf = cpf;
            NomeCompleto = nomeCompleto;
            DataNascimento = dataNascimento;
            Email = email;
            TelefoneFixo = telefoneFixo;
            TelefoneCelular = telefoneCelular;
            ConsultorId = consultorId;
            InssNumeroBeneficio = inssNumeroBeneficio;
            InssSalario = inssSalario;
            InssEspecie = inssEspecie;
            OutrosDiaPagamento = outrosDiaPagamento;
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarClienteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}