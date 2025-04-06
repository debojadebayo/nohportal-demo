using AutoMapper;
using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;

public class ProductMappingProfile : Profile
{
	public ProductMappingProfile()
	{
		CreateMap<ProductDto, Product>();
		CreateMap<Product, ProductDto>();
	}
}
