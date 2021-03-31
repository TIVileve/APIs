using System.Linq;
using AutoMapper;
using Vileve.Application.ViewModels.v1.Autorizacao;
using Vileve.Application.ViewModels.v1.Cliente;
using Vileve.Application.ViewModels.v1.Consultor;
using Vileve.Application.ViewModels.v1.Parametrizacao;
using Vileve.Domain.Enums;
using Vileve.Domain.Models;
using Vileve.Domain.Responses;
using EnderecoViewModel = Vileve.Application.ViewModels.v1.Consultor.EnderecoViewModel;

namespace Vileve.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Token, TokenViewModel>();

            CreateMap<Onboarding, StatusOnboardingViewModel>()
                .ForMember(dest => dest.StatusOnboardingDescricao, opt => opt.MapFrom(src => Enumerations.GetEnumDescription(src.StatusOnboarding)))
                .ForMember(dest => dest.StatusOnboarding, opt => opt.MapFrom(src => (int)src.StatusOnboarding));

            CreateMap<Endereco, EnderecoViewModel>()
                .ForMember(dest => dest.TipoEnderecoDescricao, opt => opt.MapFrom(src => Enumerations.GetEnumDescription(src.TipoEndereco)))
                .ForMember(dest => dest.TipoEndereco, opt => opt.MapFrom(src => (int)src.TipoEndereco));

            CreateMap<Endereco, EnderecoPorIdViewModel>()
                .ForMember(dest => dest.TipoEnderecoDescricao, opt => opt.MapFrom(src => Enumerations.GetEnumDescription(src.TipoEndereco)))
                .ForMember(dest => dest.TipoEndereco, opt => opt.MapFrom(src => (int)src.TipoEndereco));

            CreateMap<EnderecoCep, ViewModels.v1.Endereco.EnderecoViewModel>()
                .ForMember(dest => dest.CodigoCidade, opt => opt.MapFrom(src => src.CodigoCidade))
                .ForMember(dest => dest.CodigoUf, opt => opt.MapFrom(src => src.CodigoUf))
                .ForMember(dest => dest.IbgeMunicipio, opt => opt.MapFrom(src => src.IbgeMunicipio))
                .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.Cidade))
                .ForMember(dest => dest.Logradouro, opt => opt.MapFrom(src => src.Logradouro))
                .ForMember(dest => dest.TipoLogradouro, opt => opt.MapFrom(src => src.TipoLogradouro))
                .ForMember(dest => dest.Bairro, opt => opt.MapFrom(src => src.Bairro))
                .ForMember(dest => dest.Uf, opt => opt.MapFrom(src => src.Uf))
                .ForMember(dest => dest.Resultado, opt => opt.MapFrom(src => src.Resultado));

            CreateMap<ParametrizacaoEstadoCivil, EstadoCivilViewModel>()
                .ForMember(dest => dest.CodigoEstadoCivil, opt => opt.MapFrom(src => src.CodigoEstadoCivil))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<ParametrizacaoNacionalidade, NacionalidadeViewModel>()
                .ForMember(dest => dest.CodigoNacionalidade, opt => opt.MapFrom(src => src.CodigoNacionalidade))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.PrefixoPais, opt => opt.MapFrom(src => src.PrefixoPais));

            CreateMap<ParametrizacaoPerfilUsuario, PerfilUsuarioViewModel>()
                .ForMember(dest => dest.CodigoPerfil, opt => opt.MapFrom(src => src.CodigoPerfil))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<ParametrizacaoTipoTelefone, TipoTelefoneViewModel>()
                .ForMember(dest => dest.TipoTelefone, opt => opt.MapFrom(src => src.TipoTelefone))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<ParametrizacaoTipoEmail, TipoEmailViewModel>()
                .ForMember(dest => dest.TipoEmail, opt => opt.MapFrom(src => src.TipoEmail))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<ParametrizacaoTipoEndereco, TipoEnderecoViewModel>()
                .ForMember(dest => dest.TipoEndereco, opt => opt.MapFrom(src => src.TipoEndereco))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<ParametrizacaoBanco, BancoViewModel>()
                .ForMember(dest => dest.CodigoBanco, opt => opt.MapFrom(src => src.CodigoBanco))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<ParametrizacaoOperacaoBancaria, OperacaoBancariaViewModel>()
                .ForMember(dest => dest.CodigoOperacao, opt => opt.MapFrom(src => src.CodigoOperacao))
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Name));

            CreateMap<ParametrizacaoSexo, SexoViewModel>()
                .ForMember(dest => dest.CodigoSexo, opt => opt.MapFrom(src => src.CodigoSexo))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<SeguroProduto, ProdutoViewModel>()
                .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.ProdutoItens));

            CreateMap<SeguroProdutoMeioPagamento, ProdutoMeioPagamentoViewModel>();

            CreateMap<SeguroProdutoItem, ProdutoItemViewModel>();

            CreateMap<Cliente, ClienteViewModel>()
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Enderecos.OrderBy(ce => ce.CreationDate).FirstOrDefault()));

            CreateMap<ClienteProduto, ClienteProdutoViewModel>();

            CreateMap<ClienteEndereco, ViewModels.v1.Cliente.EnderecoViewModel>();

            CreateMap<ClienteDependente, DependenteViewModel>()
                .ForPath(dest => dest.Endereco.Cep, opt => opt.MapFrom(src => src.Cep))
                .ForPath(dest => dest.Endereco.Logradouro, opt => opt.MapFrom(src => src.Logradouro))
                .ForPath(dest => dest.Endereco.Numero, opt => opt.MapFrom(src => src.Numero))
                .ForPath(dest => dest.Endereco.Complemento, opt => opt.MapFrom(src => src.Complemento))
                .ForPath(dest => dest.Endereco.Bairro, opt => opt.MapFrom(src => src.Bairro))
                .ForPath(dest => dest.Endereco.Cidade, opt => opt.MapFrom(src => src.Cidade))
                .ForPath(dest => dest.Endereco.Estado, opt => opt.MapFrom(src => src.Estado));

            CreateMap<ContratarProdutoRetorno, ContratarProdutoViewModel>();
        }
    }
}