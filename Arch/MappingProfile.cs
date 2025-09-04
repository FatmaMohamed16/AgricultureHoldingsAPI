// MappingProfile.cs
using AutoMapper;
using AmlakState.Models;
using Arch.Models;
using Arch.DTO;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<AgriculturalHoldingCreateDto, AgriculturalHolding>()
            .ForMember(dest => dest.Attachments, opt => opt.Ignore())
            .ForMember(dest => dest.PropertyCoordinates, opt => opt.Ignore());
    }
}