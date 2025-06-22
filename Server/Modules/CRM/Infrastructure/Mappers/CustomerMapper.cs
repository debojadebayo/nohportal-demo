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
            Domain = entity.Domain,
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
            ModifiedDate = entity.ModifiedDate,
            RelatedDocumentIds = entity.RelatedDocumentIds.ToList(),
        };
    }

    public Customer Map(CustomerDto dto)
    {
        return new Customer
        {
            Id = dto.Id,
            Name = dto.Name,
            Domain = dto.Domain,
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
            IsActive = dto.IsActive,
            RelatedDocumentIds = dto.RelatedDocumentIds.ToArray(),
            SearchTags = $"{dto.Id} {dto.Name} {dto.Telephone} {dto.Site} {dto.OHServicesRequired} {dto.Address} {dto.Industry} {dto.Postcode} {dto.Website} {dto.Email} {dto.InvoiceEmail} {dto.Notes}",
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
        entity.Id = dto.Id;
        entity.Domain = dto.Domain;
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
        entity.RelatedDocumentIds = dto.RelatedDocumentIds.ToArray();
        entity.SearchTags = $"{entity.Id} {dto.Name} {dto.Telephone} {dto.Address} {dto.Industry} {dto.Postcode} {dto.Website} {dto.Email} {dto.InvoiceEmail}";
    }

    public void Map(Customer entity, CustomerDto dto)
    {
        dto.Id = entity.Id;
        dto.Name = entity.Name;
        dto.Domain = entity.Domain;
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
        dto.RelatedDocumentIds = entity.RelatedDocumentIds.ToList();
    }
}