using MediatR;
using VilevePay.Domain.Validations.Endereco;

namespace VilevePay.Domain.Commands.Endereco
{
    public class ObterEnderecoCommand : EnderecoCommand, IRequest<object>
    {
        public ObterEnderecoCommand(string cep)
        {
            Cep = cep;
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterEnderecoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}