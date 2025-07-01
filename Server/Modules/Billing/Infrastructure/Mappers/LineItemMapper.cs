using ComposedHealthBase.Server.Mappers;
using Server.Modules.Billing.Entities;
using Shared.DTOs.Billing;

namespace Server.Modules.Billing.Infrastructure.Mappers
{
    public class LineItemMapper : IMapper<LineItem, LineItemDto>
    {
        public LineItemDto Map(LineItem entity)
        {
            return new LineItemDto
            {
                Id = entity.Id,
                IsActive = entity.IsActive,
                CreatedBy = entity.CreatedBy,
                LastModifiedBy = entity.LastModifiedBy,
                CreatedDate = entity.CreatedDate,
                ModifiedDate = entity.ModifiedDate,
                InvoiceId = entity.InvoiceId,
                ScheduleId = entity.ScheduleId,
                ProductId = entity.ProductId,
                ProductName = entity.ProductName,
                ProductChargeCode = entity.ProductTypeChargeCode,
                UnitPrice = entity.UnitPrice,
                Quantity = entity.Quantity,
                LineTotal = entity.LineTotal,
                ServiceDate = entity.ServiceDate,
                Description = entity.Description
            };
        }

        public LineItem Map(LineItemDto dto)
        {
            return new LineItem
            {
                Id = dto.Id,
                IsActive = dto.IsActive,
                CreatedBy = dto.CreatedBy,
                LastModifiedBy = dto.LastModifiedBy,
                CreatedDate = dto.CreatedDate,
                ModifiedDate = dto.ModifiedDate,
                InvoiceId = dto.InvoiceId,
                ScheduleId = dto.ScheduleId,
                ProductId = dto.ProductId,
                ProductName = dto.ProductName,
                ProductTypeChargeCode = dto.ProductChargeCode,
                UnitPrice = dto.UnitPrice,
                Quantity = dto.Quantity,
                LineTotal = dto.LineTotal,
                ServiceDate = dto.ServiceDate,
                Description = dto.Description
            };
        }

        public IEnumerable<LineItemDto> Map(IEnumerable<LineItem> entities)
        {
            return entities.Select(Map);
        }

        public IEnumerable<LineItem> Map(IEnumerable<LineItemDto> dtos)
        {
            return dtos.Select(Map);
        }

        public void Map(LineItemDto dto, LineItem entity)
        {
            entity.IsActive = dto.IsActive;
            entity.InvoiceId = dto.InvoiceId;
            entity.ScheduleId = dto.ScheduleId;
            entity.ProductId = dto.ProductId;
            entity.ProductName = dto.ProductName;
            entity.ProductTypeChargeCode = dto.ProductChargeCode;
            entity.UnitPrice = dto.UnitPrice;
            entity.Quantity = dto.Quantity;
            entity.LineTotal = dto.LineTotal;
            entity.ServiceDate = dto.ServiceDate;
            entity.Description = dto.Description;
        }

        public void Map(LineItem entity, LineItemDto dto)
        {
            dto.Id = entity.Id;
            dto.IsActive = entity.IsActive;
            dto.CreatedBy = entity.CreatedBy;
            dto.LastModifiedBy = entity.LastModifiedBy;
            dto.CreatedDate = entity.CreatedDate;
            dto.ModifiedDate = entity.ModifiedDate;
            dto.InvoiceId = entity.InvoiceId;
            dto.ScheduleId = entity.ScheduleId;
            dto.ProductId = entity.ProductId;
            dto.ProductName = entity.ProductName;
            dto.ProductChargeCode = entity.ProductTypeChargeCode;
            dto.UnitPrice = entity.UnitPrice;
            dto.Quantity = entity.Quantity;
            dto.LineTotal = entity.LineTotal;
            dto.ServiceDate = entity.ServiceDate;
            dto.Description = entity.Description;
        }

        public void Map(IEnumerable<LineItemDto> dtos, IEnumerable<LineItem> entities)
        {
            var dtoList = dtos.ToList();
            var entityList = entities.ToList();

            for (int i = 0; i < Math.Min(dtoList.Count, entityList.Count); i++)
            {
                Map(dtoList[i], entityList[i]);
            }
        }

        public void Map(IEnumerable<LineItem> entities, IEnumerable<LineItemDto> dtos)
        {
            var entityList = entities.ToList();
            var dtoList = dtos.ToList();

            for (int i = 0; i < Math.Min(entityList.Count, dtoList.Count); i++)
            {
                Map(entityList[i], dtoList[i]);
            }
        }
    }
}
