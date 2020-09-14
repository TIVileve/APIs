using System;

namespace VilevePay.Application.Interfaces
{
    public interface IClienteAppService : IDisposable
    {
        void ValidarPessoaFisica(string codigoConvite, string cpf);
        void RegistrarComprovantePessoaFisica(string codigoConvite, string comprovanteBase64);
        void ValidarPessoaJuridica(string codigoConvite, string cnpj);
        void RegistrarComprovantePessoaJuridica(string codigoConvite, string comprovanteBase64);
        void RegistrarEndereco(string codigoConvite, string cep, string logradouro, int numero, string complemento, string bairro, string cidade, string estado);
        void RegistrarComprovanteEndereco(string codigoConvite, string comprovanteEnderecoBase64);
    }
}