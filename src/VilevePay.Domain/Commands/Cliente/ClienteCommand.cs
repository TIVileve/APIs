using VilevePay.Domain.Core.Commands;

namespace VilevePay.Domain.Commands.Cliente
{
    public abstract class ClienteCommand : Command
    {
        public string CodigoConvite { get; protected set; }
        public string Cpf { get; protected set; }
        public string Cnpj { get; protected set; }
        public string ComprovanteBase64 { get; protected set; }
        public string Cep { get; private set; }
        public string Logradouro { get; private set; }
        public int Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
    }
}