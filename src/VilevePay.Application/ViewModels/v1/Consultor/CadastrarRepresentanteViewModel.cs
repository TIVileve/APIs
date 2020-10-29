using System.Collections.Generic;

namespace VilevePay.Application.ViewModels.v1.Consultor
{
    public class CadastrarRepresentanteViewModel
    {
        public string Cpf { get; set; }
        public string NomeCompleto { get; set; }
        public string Apelido { get; set; }
        public int Sexo { get; set; }
        public int EstadoCivil { get; set; }
        public string Nacionalidade { get; set; }
        public IEnumerable<CadastrarEmailViewModel> Emails { get; set; }
        public IEnumerable<CadastrarTelefoneViewModel> Telefones { get; set; }
        public CadastrarDocumentoViewModel Documento { get; set; }
    }
}