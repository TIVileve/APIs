﻿using MediatR;
using VilevePay.Domain.Validations.Autorizacao;

namespace VilevePay.Domain.Commands.Autorizacao
{
    public class CadastrarSenhaCommand : AutorizacaoCommand, IRequest<bool>
    {
        public CadastrarSenhaCommand(string codigoConvite, string numeroCelular, string email, string senha,
            string confirmarSenha)
        {
            CodigoConvite = codigoConvite;
            NumeroCelular = numeroCelular;
            Email = email;
            Senha = senha;
            ConfirmarSenha = confirmarSenha;
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarSenhaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}