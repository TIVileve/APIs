using VilevePay.Domain.Core.Commands;

namespace VilevePay.Domain.Commands.Autorizacao
{
    public abstract class AutorizacaoCommand : Command
    {
        public string CodigoConvite { get; protected set; }
        public string NumeroCelular { get; protected set; }
        public string CodigoToken { get; protected set; }
        public string Email { get; protected set; }
    }
}