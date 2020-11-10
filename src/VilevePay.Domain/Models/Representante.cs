using System;
using System.Collections.Generic;
using VilevePay.Domain.Core.Models;

namespace VilevePay.Domain.Models
{
    public class Representante : Entity
    {
        public Representante(Guid id)
        {
            Id = id;
        }

        // Empty constructor for EF
        protected Representante()
        {
        }

        public string Cpf { get; set; }
        public string NomeCompleto { get; set; }
        public int Sexo { get; set; }
        public int EstadoCivil { get; set; }
        public string Nacionalidade { get; set; }
        public virtual ICollection<RepresentanteEmail> Emails { get; set; }
        public virtual ICollection<RepresentanteTelefone> Telefones { get; set; }
        public string DocumentoFrenteBase64 { get; set; }
        public string DocumentoVersoBase64 { get; set; }

        public virtual Consultor Consultor { get; set; }
        public Guid ConsultorId { get; set; }

        public Representante Update()
        {
            return this;
        }
    }
}