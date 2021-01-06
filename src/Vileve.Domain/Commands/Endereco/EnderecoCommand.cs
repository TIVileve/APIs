using Vileve.Domain.Core.Commands;

namespace Vileve.Domain.Commands.Endereco
{
    public abstract class EnderecoCommand : Command
    {
        public string Cep { get; protected set; }
    }
}