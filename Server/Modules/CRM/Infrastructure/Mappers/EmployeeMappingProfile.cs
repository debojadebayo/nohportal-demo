using AutoMapper;
using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;

public class EmployeeMappingProfile : Profile
{
	public EmployeeMappingProfile()
	{
		CreateMap<EmployeeDto, Employee>()
			.ForMember(src => src.CreatedBy, opt => opt.Ignore())
			.ForMember(src => src.LastModifiedBy, opt => opt.Ignore())
			.ForMember(src => src.CreatedDate, opt => opt.Ignore())
			.ForMember(src => src.ModifiedDate, opt => opt.Ignore());
		CreateMap<Employee, EmployeeDto>();

	}
}
