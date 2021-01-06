using AutoMapper;
using Vileve.Application.ViewModels.v1.Property;
using Vileve.Domain.Commands.Property;
using Vileve.Domain.Enums;

namespace Vileve.Application.AutoMapper
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