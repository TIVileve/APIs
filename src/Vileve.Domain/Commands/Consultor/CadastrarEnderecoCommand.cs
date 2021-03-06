﻿using MediatR;
using Vileve.Domain.Validations.Consultor;

namespace Vileve.Domain.Commands.Consultor
{
    public class CadastrarEnderecoCommand : ConsultorCommand, IRequest<bool>
    {
        public CadastrarEnderecoCommand(string codigoConvite, string numeroCelular, int tipoEndereco, string cep, string logradouro,
            int numero, string complemento, string bairro, string cidade, string estado,
            bool principal, string comprovanteBase64)
        {
            CodigoConvite = codigoConvite;
            NumeroCelular = numeroCelular;
            TipoEndereco = tipoEndereco;
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Principal = principal;
            ComprovanteBase64 = comprovanteBase64;
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarEnderecoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}