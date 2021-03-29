using Vileve.Domain.Core.Commands;
using Vileve.Domain.ExtensionMethods;

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
        private string _senha;

        public string Senha
        {
            get => _senha;
            protected set => _senha = string.IsNullOrWhiteSpace(value) ? null : value.CreateMd5();
        }

        private string _confirmarSenha;

        public string ConfirmarSenha
        {
            get => _confirmarSenha;
            protected set => _confirmarSenha = string.IsNullOrWhiteSpace(value) ? null : value.CreateMd5();
        }

        public string SenhaOriginal { get; protected set; }
        public string FotoBase64 { get; protected set; }
    }
}