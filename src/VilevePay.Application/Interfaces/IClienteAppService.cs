using System;

namespace VilevePay.Application.Interfaces
{
    public interface IClienteAppService : IDisposable
    {
        void ValidarPessoaFisicaCommand(string codigoConvite, string cpf);
        void RegistrarComprovantePessoaFisicaCommand(string codigoConvite, string comprovanteBase64);
        void ValidarPessoaJuridicaCommand(string codigoConvite, string cnpj);
        void RegistrarComprovantePessoaJuridicaCommand(string codigoConvite, string comprovanteBase64);
        void RegistrarEnderecoCommand(string codigoConvite, string cep, string logradouro, int numero, string complemento, string bairro, string cidade, string estado);
        void RegistrarComprovanteEnderecoCommand(string codigoConvite, string comprovanteEnderecoBase64);
    }
}