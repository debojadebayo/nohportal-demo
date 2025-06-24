using ComposedHealthBase.Server.Mappers;
using Server.Modules.Auth.Entities;
using Shared.DTOs.Auth;

namespace Server.Modules.Auth.Infrastructure.Mappers
{
    public class LocalStorageKeyMapper : IMapper<LocalStorageKey, LocalStorageKeyDto>
    {
        public LocalStorageKeyDto Map(LocalStorageKey entity)
        {
            return new LocalStorageKeyDto
            {
                Id = entity.Id,
                ObjectTypeName = entity.ObjectTypeName,
                ObjectGuid = entity.ObjectGuid,
                PrivateKey = entity.PrivateKey,
                PublicKey = entity.PublicKey,
                KeyGeneratedDate = entity.KeyGeneratedDate,
                KeyExpiryDate = entity.KeyExpiryDate,
                IsActive = entity.IsActive,
                CreatedBy = entity.CreatedBy,
                LastModifiedBy = entity.LastModifiedBy,
                CreatedDate = entity.CreatedDate,
                ModifiedDate = entity.ModifiedDate
            };
        }

        public LocalStorageKey Map(LocalStorageKeyDto dto)
        {
            return new LocalStorageKey
            {
                Id = dto.Id,
                ObjectTypeName = dto.ObjectTypeName,
                ObjectGuid = dto.ObjectGuid,
                PrivateKey = dto.PrivateKey,
                PublicKey = dto.PublicKey,
                KeyGeneratedDate = dto.KeyGeneratedDate,
                KeyExpiryDate = dto.KeyExpiryDate,
                IsActive = dto.IsActive,
                CreatedBy = dto.CreatedBy,
                LastModifiedBy = dto.LastModifiedBy,
                CreatedDate = dto.CreatedDate,
                ModifiedDate = dto.ModifiedDate
            };
        }

        public IEnumerable<LocalStorageKeyDto> Map(IEnumerable<LocalStorageKey> entities)
        {
            return entities.Select(Map);
        }

        public IEnumerable<LocalStorageKey> Map(IEnumerable<LocalStorageKeyDto> dtos)
        {
            return dtos.Select(Map);
        }

        public void Map(LocalStorageKeyDto dto, LocalStorageKey entity)
        {
            entity.ObjectTypeName = dto.ObjectTypeName;
            entity.ObjectGuid = dto.ObjectGuid;
            entity.PrivateKey = dto.PrivateKey;
            entity.PublicKey = dto.PublicKey;
            entity.KeyGeneratedDate = dto.KeyGeneratedDate;
            entity.KeyExpiryDate = dto.KeyExpiryDate;
            entity.IsActive = dto.IsActive;
        }

        public void Map(LocalStorageKey entity, LocalStorageKeyDto dto)
        {
            dto.Id = entity.Id;
            dto.ObjectTypeName = entity.ObjectTypeName;
            dto.ObjectGuid = entity.ObjectGuid;
            dto.PrivateKey = entity.PrivateKey;
            dto.PublicKey = entity.PublicKey;
            dto.KeyGeneratedDate = entity.KeyGeneratedDate;
            dto.KeyExpiryDate = entity.KeyExpiryDate;
            dto.IsActive = entity.IsActive;
            dto.CreatedBy = entity.CreatedBy;
            dto.LastModifiedBy = entity.LastModifiedBy;
            dto.CreatedDate = entity.CreatedDate;
            dto.ModifiedDate = entity.ModifiedDate;
        }

        public void Map(IEnumerable<LocalStorageKeyDto> dtos, IEnumerable<LocalStorageKey> entities)
        {
            var dtosArray = dtos.ToArray();
            var entitiesArray = entities.ToArray();
            for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
            {
                Map(dtosArray[i], entitiesArray[i]);
            }
        }

        public void Map(IEnumerable<LocalStorageKey> entities, IEnumerable<LocalStorageKeyDto> dtos)
        {
            var dtosArray = dtos.ToArray();
            var entitiesArray = entities.ToArray();
            for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
            {
                Map(entitiesArray[i], dtosArray[i]);
            }
        }
    }
}
