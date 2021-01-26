using System;
using System.Collections.Generic;
using Vileve.Domain.Core.Commands;

namespace Vileve.Domain.Commands.Consultor
{
    public abstract class ConsultorCommand : Command
    {
        public string CodigoConvite { get; protected set; }
        private string _numeroCelular;

        public string NumeroCelular
        {
            get => _numeroCelular;
            protected set => _numeroCelular = string.IsNullOrWhiteSpace(value) ? null : value.Contains("+") ? value : $"+{value}";
        }

        public Guid EnderecoId { get; protected set; }
        public string Email { get; protected set; }

        // Pessoa Juridica
        public string Cnpj { get; protected set; }
        public string RazaoSocial { get; protected set; }
        public string NomeFantasia { get; protected set; }
        public string InscricaoMunicipal { get; protected set; }
        public string InscricaoEstadual { get; protected set; }
        public string ContratoSocialBase64 { get; protected set; }
        public string UltimaAlteracaoBase64 { get; protected set; }

        // Dados Bancarios
        public string CodigoBanco { get; protected set; }
        public string Agencia { get; protected set; }
        public string ContaSemDigito { get; protected set; }
        public string Digito { get; protected set; }
        public int TipoConta { get; protected set; }

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

        // Representante
        public string Cpf { get; protected set; }
        public string NomeCompleto { get; protected set; }
        public int Sexo { get; protected set; }
        public int EstadoCivil { get; protected set; }
        public string Nacionalidade { get; protected set; }
        public IEnumerable<object> Emails { get; protected set; }
        public IEnumerable<object> Telefones { get; protected set; }
        public string DocumentoFrenteBase64 { get; protected set; }
        public string DocumentoVersoBase64 { get; protected set; }
    }
}