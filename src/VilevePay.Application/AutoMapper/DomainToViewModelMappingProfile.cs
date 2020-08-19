using AutoMapper;
using VilevePay.Application.ViewModels.v1.Property;
using VilevePay.Domain.Models;

namespace VilevePay.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Property, PropertyViewModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)src.Type));
        }
    }
}