using System;
using VilevePay.Domain.Core.Commands;

namespace VilevePay.Domain.Commands.Consultor
{
    public abstract class ConsultorCommand : Command
    {
        public string CodigoConvite { get; protected set; }
        public Guid EnderecoId { get; protected set; }
        public string Email { get; protected set; }

        // Pessoa Juridica
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string InscricaoEstadual { get; set; }
        public string ContratoSocialBase64 { get; set; }
        public string UltimaAlteracaoBase64 { get; set; }

        // Dados Bancarios
        public string CodigoBanco { get; set; }
        public string Agencia { get; set; }
        public string ContaSemDigito { get; set; }
        public string Digito { get; set; }
        public int TipoConta { get; set; }

        // Endereco
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