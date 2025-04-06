using AutoMapper;
using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;

public class CustomerMappingProfile : Profile
{
	public CustomerMappingProfile()
	{
		CreateMap<CustomerDto, NOHCustomer>()
			.ForMember(dest => dest.HouseNumberOrName, opt => opt.MapFrom(src => src.Address))
			.ForMember(dest => dest.Contracts, opt => opt.MapFrom(src => src.Contracts))
			.ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

		CreateMap<NOHCustomer, CustomerDto>()
			.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.HouseNumberOrName))
			.ForMember(dest => dest.Contracts, opt => opt.MapFrom(src => src.Contracts))
			.ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
	}
}

