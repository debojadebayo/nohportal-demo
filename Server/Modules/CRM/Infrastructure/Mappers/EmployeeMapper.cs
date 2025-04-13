using NationOH.Server.Base.Infrastructure.Mappers;
using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;

public class EmployeeMapper : IMapper<Employee, EmployeeDto>
{
    public EmployeeDto Map(Employee entity)
    {
        return new EmployeeDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            DOB = entity.DOB,
            Address1 = entity.Address1,
            Address2 = entity.Address2,
            Address3 = entity.Address3,
            Postcode = entity.Postcode,
            Email = entity.Email,
            Telephone = entity.Telephone,
            CompanyName = entity.CompanyName,
            JobRole = entity.JobRole,
            Department = entity.Department,
            LineManager = entity.LineManager
        };
    }

    public Employee Map(EmployeeDto dto)
    {
        return new Employee
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            DOB = dto.DOB,
            Address1 = dto.Address1,
            Address2 = dto.Address2,
            Address3 = dto.Address3,
            Postcode = dto.Postcode,
            Email = dto.Email,
            Telephone = dto.Telephone,
            CompanyName = dto.CompanyName,
            JobRole = dto.JobRole,
            Department = dto.Department,
            LineManager = dto.LineManager
        };
    }

    public IEnumerable<EmployeeDto> Map(IEnumerable<Employee> entities)
    {
        return entities.Select(Map);
    }

    public IEnumerable<Employee> Map(IEnumerable<EmployeeDto> dtos)
    {
        return dtos.Select(Map);
    }

    public void Map(EmployeeDto dto, Employee entity)
    {
        entity.FirstName = dto.FirstName;
        entity.LastName = dto.LastName;
        entity.DOB = dto.DOB;
        entity.Address1 = dto.Address1;
        entity.Address2 = dto.Address2;
        entity.Address3 = dto.Address3;
        entity.Postcode = dto.Postcode;
        entity.Email = dto.Email;
        entity.Telephone = dto.Telephone;
        entity.CompanyName = dto.CompanyName;
        entity.JobRole = dto.JobRole;
        entity.Department = dto.Department;
        entity.LineManager = dto.LineManager;
    }

    public void Map(Employee entity, EmployeeDto dto)
    {
        dto.FirstName = entity.FirstName;
        dto.LastName = entity.LastName;
        dto.DOB = entity.DOB;
        dto.Address1 = entity.Address1;
        dto.Address2 = entity.Address2;
        dto.Address3 = entity.Address3;
        dto.Postcode = entity.Postcode;
        dto.Email = entity.Email;
        dto.Telephone = entity.Telephone;
        dto.CompanyName = entity.CompanyName;
        dto.JobRole = entity.JobRole;
        dto.Department = entity.Department;
        dto.LineManager = entity.LineManager;
    }

    public void Map(IEnumerable<EmployeeDto> dtos, IEnumerable<Employee> entities)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(dtosArray[i], entitiesArray[i]);
        }
    }

    public void Map(IEnumerable<Employee> entities, IEnumerable<EmployeeDto> dtos)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(entitiesArray[i], dtosArray[i]);
        }
    }
}