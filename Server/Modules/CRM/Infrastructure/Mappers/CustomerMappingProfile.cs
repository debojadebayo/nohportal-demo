using AutoMapper;
using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;

public class CustomerMappingProfile : Profile
{
	public CustomerMappingProfile()
	{
		CreateMap<CustomerDto, NOHCustomer>()
			.ReverseMap();
	}
}

