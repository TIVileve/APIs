using System.ComponentModel;

namespace Vileve.Domain.Enums
{
    public enum TipoDocumento
    {
        [Description("Comprovante de endereço")]
        ComprovanteEndereco = 0,

        [Description("Identidade ou CNH")]
        IdentidadeOuCnh = 1,

        [Description("Selfie")]
        Selfie = 2
    }
}