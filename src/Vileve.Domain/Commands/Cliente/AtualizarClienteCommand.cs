using System;
using MediatR;
using Vileve.Domain.Validations.Cliente;

namespace Vileve.Domain.Commands.Cliente
{
    public class AtualizarClienteCommand : ClienteCommand, IRequest<bool>
    {
        public AtualizarClienteCommand(Guid clienteId, string cpf, string nomeCompleto, DateTime dataNascimento, string email,
            string telefoneFixo, string telefoneCelular, Guid? consultorId,
            long? inssNumeroBeneficio, double? inssSalario, int? inssEspecie, int? outrosDiaPagamento)
        {
            ClienteId = clienteId;
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
            ValidationResult = new AtualizarClienteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}