using VilevePay.Domain.Commands.Cliente;

namespace VilevePay.Domain.Validations.Cliente
{
    public class RegistrarEnderecoCommandValidation : ClienteValidation<RegistrarEnderecoCommand>
    {
        public RegistrarEnderecoCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateCep();
            ValidateLogradouro();
            ValidateNumero();
            ValidateComplemento();
            ValidateBairro();
            ValidateCidade();
            ValidateEstado();
        }
    }
}