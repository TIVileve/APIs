using System;
using System.Collections.Generic;
using VilevePay.Domain.Core.Models;

namespace VilevePay.Domain.Models
{
    public class Consultor : Entity
    {
        public Consultor(Guid id, string cnpj, string razaoSocial, string nomeFantasia, string inscricaoMunicipal,
            string inscricaoEstadual, string contratoSocialBase64, string ultimaAlteracaoBase64, Guid onboardingId)
        {
            Id = id;
            Cnpj = cnpj;
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
            InscricaoMunicipal = inscricaoMunicipal;
            InscricaoEstadual = inscricaoEstadual;
            ContratoSocialBase64 = contratoSocialBase64;
            UltimaAlteracaoBase64 = ultimaAlteracaoBase64;
            OnboardingId = onboardingId;
        }

        // Empty constructor for EF
        protected Consultor()
        {
        }

        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string InscricaoEstadual { get; set; }
        public virtual DadosBancarios DadosBancarios { get; set; }
        public string ContratoSocialBase64 { get; set; }
        public string UltimaAlteracaoBase64 { get; set; }
        public virtual Representante Representante { get; set; }

        public virtual Onboarding Onboarding { get; set; }
        public Guid OnboardingId { get; set; }

        public virtual ICollection<Endereco> Enderecos { get; set; }

        public Consultor Update(string cnpj, string razaoSocial, string nomeFantasia, string inscricaoMunicipal,
            string inscricaoEstadual, string contratoSocialBase64, string ultimaAlteracaoBase64)
        {
            Cnpj = cnpj;
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
            InscricaoMunicipal = inscricaoMunicipal;
            InscricaoEstadual = inscricaoEstadual;
            ContratoSocialBase64 = contratoSocialBase64;
            UltimaAlteracaoBase64 = ultimaAlteracaoBase64;

            return this;
        }
    }
}