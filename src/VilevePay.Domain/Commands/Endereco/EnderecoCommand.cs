using VilevePay.Domain.Core.Commands;

namespace VilevePay.Domain.Commands.Endereco
{
    public abstract class EnderecoCommand : Command
    {
        public string Cep { get; protected set; }
    }
}