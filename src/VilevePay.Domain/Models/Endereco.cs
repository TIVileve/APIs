﻿using System;
using VilevePay.Domain.Core.Models;
using VilevePay.Domain.Enums;

namespace VilevePay.Domain.Models
{
    public class Endereco : Entity
    {
        public Endereco(Guid id)
        {
            Id = id;
        }

        // Empty constructor for EF
        protected Endereco()
        {
        }

        public TipoEndereco TipoEndereco { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public bool Principal { get; set; }
        public string ComprovanteBase64 { get; set; }

        public virtual Consultor Consultor { get; set; }
        public Guid ConsultorId { get; set; }

        public Endereco Update()
        {
            return this;
        }
    }
}