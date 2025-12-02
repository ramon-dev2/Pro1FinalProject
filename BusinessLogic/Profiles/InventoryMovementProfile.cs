using AutoMapper;
using BusinessLogic.Dtos;
using DataAccess.Entities;

namespace BusinessLogic.Profiles
{
    public class InventoryMovementProfile : Profile
    {
        public InventoryMovementProfile()
        {
            CreateMap<InventoryMovementDto, InventoryMovement>()
                .ForMember(dest => dest.Product, opt => opt.Ignore());
            
            CreateMap<InventoryMovement, InventoryMovementDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => 
                    src.Product != null ? src.Product.Name : null));

            CreateMap<InventoryMovementCreateDto, InventoryMovement>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.MovementDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<InventoryMovementCreateDto, InventoryMovementDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProductName, opt => opt.Ignore())
                .ForMember(dest => dest.MovementDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}

