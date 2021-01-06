using Vileve.Domain.Core.Commands;

namespace Vileve.Domain.Commands.Autorizacao
{
    public abstract class AutorizacaoCommand : Command
    {
        public string CodigoConvite { get; protected set; }
        public string NumeroCelular { get; protected set; }
        public string CodigoToken { get; protected set; }
        public string Email { get; protected set; }
        public string Senha { get; protected set; }
        public string ConfirmarSenha { get; protected set; }
        public string FotoBase64 { get; protected set; }
    }
}