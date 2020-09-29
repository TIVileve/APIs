using VilevePay.Domain.Core.Commands;

namespace VilevePay.Domain.Commands.Consultor
{
    public abstract class ConsultorCommand : Command
    {
        public string CodigoConvite { get; protected set; }
    }
}