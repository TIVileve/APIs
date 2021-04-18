using System;
using Vileve.Domain.Core.Models;
using Vileve.Domain.Enums;

namespace Vileve.Domain.Models
{
    public class ClienteDocumento : Entity
    {
        public ClienteDocumento(Guid id, string frenteBase64, string versoBase64, TipoDocumento tipoDocumento, Guid clienteId)
        {
            Id = id;
            FrenteBase64 = frenteBase64;
            VersoBase64 = versoBase64;
            TipoDocumento = tipoDocumento;
            ClienteId = clienteId;
        }

        // Empty constructor for EF
        protected ClienteDocumento()
        {
        }

        public string FrenteBase64 { get; }
        public string VersoBase64 { get; }
        public TipoDocumento TipoDocumento { get; }

        public virtual Cliente Cliente { get; }
        public Guid ClienteId { get; }
    }
}