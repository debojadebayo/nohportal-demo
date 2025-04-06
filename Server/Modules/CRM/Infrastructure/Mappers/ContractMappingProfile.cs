using AutoMapper;
using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;

public class ContractMappingProfile : Profile
{
	public ContractMappingProfile()
	{
		CreateMap<ContractDto, Contract>();
		CreateMap<Contract, ContractDto>();
	}
}

