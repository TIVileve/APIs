using MediatR;
using VilevePay.Domain.Validations.Cliente;

namespace VilevePay.Domain.Commands.Cliente
{
    public class RegistrarEnderecoCommand : ClienteCommand, IRequest<bool>
    {
        public RegistrarEnderecoCommand(string codigoConvite, string cep, string logradouro, int numero, string complemento, string bairro, string cidade, string estado)
        {
            CodigoConvite = codigoConvite;
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegistrarEnderecoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}