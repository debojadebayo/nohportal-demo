using ComposedHealthBase.Server.Mappers;
using Server.Modules.Billing.Entities;
using Shared.DTOs.Billing;

namespace Server.Modules.Billing.Infrastructure.Mappers
{
    public class InvoiceMapper : IMapper<Invoice, InvoiceDto>
    {
        private readonly IMapper<LineItem, LineItemDto> _lineItemMapper;

        public InvoiceMapper(IMapper<LineItem, LineItemDto> lineItemMapper)
        {
            _lineItemMapper = lineItemMapper;
        }

        public InvoiceDto Map(Invoice entity)
        {
            return new InvoiceDto
            {
                Id = entity.Id,
                IsActive = entity.IsActive,
                CreatedBy = entity.CreatedBy,
                LastModifiedBy = entity.LastModifiedBy,
                CreatedDate = entity.CreatedDate,
                ModifiedDate = entity.ModifiedDate,
                InvoiceNumber = entity.InvoiceNumber,
                InvoiceDate = entity.InvoiceDate,
                DueDate = entity.DueDate,
                TotalAmount = entity.TotalAmount,
                NetAmount = entity.NetAmount,
                TaxAmount = entity.TaxAmount,
                TaxRate = entity.TaxRate,
                Status = entity.Status,
                Notes = entity.Notes,
                FromDate = entity.FromDate,
                ToDate = entity.ToDate,
                CustomerId = entity.CustomerId,
                ProductId = entity.ProductId,
                LineItems = entity.LineItems.Select(_lineItemMapper.Map).ToList()
            };
        }

        public Invoice Map(InvoiceDto dto)
        {
            return new Invoice
            {
                Id = dto.Id,
                IsActive = dto.IsActive,
                CreatedBy = dto.CreatedBy,
                LastModifiedBy = dto.LastModifiedBy,
                CreatedDate = dto.CreatedDate,
                ModifiedDate = dto.ModifiedDate,
                InvoiceNumber = dto.InvoiceNumber,
                InvoiceDate = dto.InvoiceDate,
                DueDate = dto.DueDate,
                TotalAmount = dto.TotalAmount,
                NetAmount = dto.NetAmount,
                TaxAmount = dto.TaxAmount,
                TaxRate = dto.TaxRate,
                Status = dto.Status,
                Notes = dto.Notes,
                FromDate = dto.FromDate,
                ToDate = dto.ToDate,
                CustomerId = dto.CustomerId,
                ProductId = dto.ProductId
            };
        }

        public IEnumerable<InvoiceDto> Map(IEnumerable<Invoice> entities)
        {
            return entities.Select(Map);
        }

        public IEnumerable<Invoice> Map(IEnumerable<InvoiceDto> dtos)
        {
            return dtos.Select(Map);
        }

        public void Map(InvoiceDto dto, Invoice entity)
        {
            entity.IsActive = dto.IsActive;
            entity.InvoiceNumber = dto.InvoiceNumber;
            entity.InvoiceDate = dto.InvoiceDate;
            entity.DueDate = dto.DueDate;
            entity.TotalAmount = dto.TotalAmount;
            entity.NetAmount = dto.NetAmount;
            entity.TaxAmount = dto.TaxAmount;
            entity.TaxRate = dto.TaxRate;
            entity.Status = dto.Status;
            entity.Notes = dto.Notes;
            entity.FromDate = dto.FromDate;
            entity.ToDate = dto.ToDate;
            entity.CustomerId = dto.CustomerId;
            entity.ProductId = dto.ProductId;
        }

        public void Map(Invoice entity, InvoiceDto dto)
        {
            dto.Id = entity.Id;
            dto.IsActive = entity.IsActive;
            dto.CreatedBy = entity.CreatedBy;
            dto.LastModifiedBy = entity.LastModifiedBy;
            dto.CreatedDate = entity.CreatedDate;
            dto.ModifiedDate = entity.ModifiedDate;
            dto.InvoiceNumber = entity.InvoiceNumber;
            dto.InvoiceDate = entity.InvoiceDate;
            dto.DueDate = entity.DueDate;
            dto.TotalAmount = entity.TotalAmount;
            dto.NetAmount = entity.NetAmount;
            dto.TaxAmount = entity.TaxAmount;
            dto.TaxRate = entity.TaxRate;
            dto.Status = entity.Status;
            dto.Notes = entity.Notes;
            dto.FromDate = entity.FromDate;
            dto.ToDate = entity.ToDate;
            dto.CustomerId = entity.CustomerId;
            dto.ProductId = entity.ProductId;
            dto.LineItems = entity.LineItems.Select(_lineItemMapper.Map).ToList();
        }

        public void Map(IEnumerable<InvoiceDto> dtos, IEnumerable<Invoice> entities)
        {
            var dtoList = dtos.ToList();
            var entityList = entities.ToList();

            for (int i = 0; i < Math.Min(dtoList.Count, entityList.Count); i++)
            {
                Map(dtoList[i], entityList[i]);
            }
        }

        public void Map(IEnumerable<Invoice> entities, IEnumerable<InvoiceDto> dtos)
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
