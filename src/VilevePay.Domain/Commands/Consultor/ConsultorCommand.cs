using System;
using VilevePay.Domain.Core.Commands;

namespace VilevePay.Domain.Commands.Consultor
{
    public abstract class ConsultorCommand : Command
    {
        public string CodigoConvite { get; protected set; }
        public Guid EnderecoId { get; protected set; }
        public string Email { get; protected set; }

        // Endereço
        public int TipoEndereco { get; protected set; }
        public string Cep { get; protected set; }
        public string Logradouro { get; protected set; }
        public int Numero { get; protected set; }
        public string Complemento { get; protected set; }
        public string Bairro { get; protected set; }
        public string Cidade { get; protected set; }
        public string Estado { get; protected set; }
        public bool Principal { get; protected set; }
        public string ComprovanteBase64 { get; protected set; }
    }
}