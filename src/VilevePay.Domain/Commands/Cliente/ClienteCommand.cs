using VilevePay.Domain.Core.Commands;

namespace VilevePay.Domain.Commands.Cliente
{
    public abstract class ClienteCommand : Command
    {
        public string CodigoConvite { get; protected set; }
        public string Cpf { get; protected set; }
        public string Cnpj { get; protected set; }
    }
}