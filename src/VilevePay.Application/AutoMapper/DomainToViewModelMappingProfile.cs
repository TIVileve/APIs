using AutoMapper;
using VilevePay.Application.ViewModels.v1.Endereco;
using VilevePay.Application.ViewModels.v1.Property;
using VilevePay.Domain.Models;
using VilevePay.Domain.Responses;

namespace VilevePay.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Property, PropertyViewModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)src.Type));

            CreateMap<EnderecoCep, EnderecoViewModel>()
                .ForMember(dest => dest.CodigoCidade, opt => opt.MapFrom(src => src.CodigoCidade))
                .ForMember(dest => dest.CodigoUf, opt => opt.MapFrom(src => src.CodigoUf))
                .ForMember(dest => dest.IbgeMunicipio, opt => opt.MapFrom(src => src.IbgeMunicipio))
                .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.Cidade))
                .ForMember(dest => dest.Logradouro, opt => opt.MapFrom(src => src.Logradouro))
                .ForMember(dest => dest.TipoLogradouro, opt => opt.MapFrom(src => src.TipoLogradouro))
                .ForMember(dest => dest.Bairro, opt => opt.MapFrom(src => src.Bairro))
                .ForMember(dest => dest.Uf, opt => opt.MapFrom(src => src.Uf))
                .ForMember(dest => dest.Resultado, opt => opt.MapFrom(src => src.Resultado));
        }
    }
}