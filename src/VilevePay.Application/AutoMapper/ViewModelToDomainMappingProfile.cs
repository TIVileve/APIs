using AutoMapper;
using VilevePay.Application.ViewModels.v1.Property;
using VilevePay.Domain.Commands.Property;
using VilevePay.Domain.Enums;

namespace VilevePay.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<RegisterNewPropertyViewModel, RegisterNewPropertyCommand>()
                .ConstructUsing(src => new RegisterNewPropertyCommand(src.Name, (Type)src.Type, src.IsRequired));

            CreateMap<UpdatePropertyViewModel, UpdatePropertyCommand>()
                .ConstructUsing(src => new UpdatePropertyCommand(src.Id, src.Name, (Type)src.Type, src.IsRequired));
        }
    }
}