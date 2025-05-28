using ComposedHealthBase.Server.Mappers;
using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;

public class CustomerMapper : IMapper<Customer, CustomerDto>
{
    public CustomerDto Map(Customer entity)
    {
        return new CustomerDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Telephone = entity.Telephone,
            NumberOfEmployees = entity.NumberOfEmployees,
            Site = entity.Site,
            OHServicesRequired = entity.OHServicesRequired,
            Address = entity.Address,
            Industry = entity.Industry,
            Postcode = entity.Postcode,
            Website = entity.Website,
            Email = entity.Email,
            InvoiceEmail = entity.InvoiceEmail,
            Notes = entity.Notes,
            IsActive = entity.IsActive,
            CreatedBy = entity.CreatedBy,
            LastModifiedBy = entity.LastModifiedBy,
            CreatedDate = entity.CreatedDate,
            ModifiedDate = entity.ModifiedDate
        };
    }

    public Customer Map(CustomerDto dto)
    {
        return new Customer
        {
            Name = dto.Name,
            Telephone = dto.Telephone,
            NumberOfEmployees = dto.NumberOfEmployees,
            Site = dto.Site,
            OHServicesRequired = dto.OHServicesRequired,
            Address = dto.Address,
            Industry = dto.Industry,
            Postcode = dto.Postcode,
            Website = dto.Website,
            Email = dto.Email,
            InvoiceEmail = dto.InvoiceEmail,
            Notes = dto.Notes,
            IsActive = dto.IsActive
        };
    }

    public IEnumerable<CustomerDto> Map(IEnumerable<Customer> entities)
    {
        return entities.Select(Map);
    }

    public IEnumerable<Customer> Map(IEnumerable<CustomerDto> dtos)
    {
        return dtos.Select(Map);
    }

    public void Map(CustomerDto dto, Customer entity)
    {
        entity.Name = dto.Name;
        entity.Telephone = dto.Telephone;
        entity.NumberOfEmployees = dto.NumberOfEmployees;
        entity.Site = dto.Site;
        entity.OHServicesRequired = dto.OHServicesRequired;
        entity.Address = dto.Address;
        entity.Industry = dto.Industry;
        entity.Postcode = dto.Postcode;
        entity.Website = dto.Website;
        entity.Email = dto.Email;
        entity.InvoiceEmail = dto.InvoiceEmail;
        entity.Notes = dto.Notes;
        entity.IsActive = dto.IsActive;
    }

    public void Map(Customer entity, CustomerDto dto)
    {
        dto.Id = entity.Id;
        dto.Name = entity.Name;
        dto.Telephone = entity.Telephone;
        dto.NumberOfEmployees = entity.NumberOfEmployees;
        dto.Site = entity.Site;
        dto.OHServicesRequired = entity.OHServicesRequired;
        dto.Address = entity.Address;
        dto.Industry = entity.Industry;
        dto.Postcode = entity.Postcode;
        dto.Website = entity.Website;
        dto.Email = entity.Email;
        dto.InvoiceEmail = entity.InvoiceEmail;
        dto.Notes = entity.Notes;
        dto.IsActive = entity.IsActive;
        dto.CreatedBy = entity.CreatedBy;
        dto.LastModifiedBy = entity.LastModifiedBy;
        dto.CreatedDate = entity.CreatedDate;
        dto.ModifiedDate = entity.ModifiedDate;
    }

    public void Map(IEnumerable<CustomerDto> dtos, IEnumerable<Customer> entities)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(dtosArray[i], entitiesArray[i]);
        }
    }

    public void Map(IEnumerable<Customer> entities, IEnumerable<CustomerDto> dtos)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(entitiesArray[i], dtosArray[i]);
        }
    }
}