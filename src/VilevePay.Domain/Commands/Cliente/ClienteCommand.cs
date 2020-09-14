using VilevePay.Domain.Core.Commands;

namespace VilevePay.Domain.Commands.Cliente
{
    public abstract class ClienteCommand : Command
    {
        public string CodigoConvite { get; protected set; }
        public string Cpf { get; protected set; }
        public string Cnpj { get; protected set; }
        public string ComprovanteBase64 { get; protected set; }
        public string Cep { get; protected set; }
        public string Logradouro { get; protected set; }
        public int Numero { get; protected set; }
        public string Complemento { get; protected set; }
        public string Bairro { get; protected set; }
        public string Cidade { get; protected set; }
        public string Estado { get; protected set; }
    }
}