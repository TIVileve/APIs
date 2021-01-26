using Vileve.Domain.Core.Commands;

namespace Vileve.Domain.Commands.Autorizacao
{
    public abstract class AutorizacaoCommand : Command
    {
        public string CodigoConvite { get; protected set; }
        private string _numeroCelular;

        public string NumeroCelular
        {
            get => _numeroCelular;
            protected set => _numeroCelular = string.IsNullOrWhiteSpace(value) ? null : value.Contains("+") ? value : $"+{value}";
        }

        public string CodigoToken { get; protected set; }
        public string Email { get; protected set; }
        public string Senha { get; protected set; }
        public string ConfirmarSenha { get; protected set; }
        public string FotoBase64 { get; protected set; }
    }
}