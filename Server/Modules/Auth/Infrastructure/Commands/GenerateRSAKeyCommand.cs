using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Interfaces;
using Server.Modules.Auth.Entities;
using Server.Modules.Auth.Infrastructure.Database;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Server.Modules.Auth.Infrastructure.Commands
{
    public interface IGenerateRSAKeyCommand : ICommand
    {
        Task<LocalStorageKey> Handle(string objectTypeName, Guid objectGuid, ClaimsPrincipal user);
    }

    public class GenerateRSAKeyCommand : IGenerateRSAKeyCommand
    {
        private readonly IDbContext<AuthDbContext> _dbContext;

        public GenerateRSAKeyCommand(IDbContext<AuthDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LocalStorageKey> Handle(string objectTypeName, Guid objectGuid, ClaimsPrincipal user)
        {
            // Generate RSA-OAEP 256 keypair
            using var rsa = RSA.Create(2048); // 2048-bit key size for RSA-OAEP-256
            
            var privateKeyPem = rsa.ExportRSAPrivateKeyPem();
            var publicKeyPem = rsa.ExportRSAPublicKeyPem();

            var localStorageKey = new LocalStorageKey
            {
                ObjectTypeName = objectTypeName,
                ObjectGuid = objectGuid,
                PrivateKey = privateKeyPem,
                PublicKey = publicKeyPem,
                KeyGeneratedDate = DateTime.UtcNow,
                KeyExpiryDate = null, // Set to null for no expiry, or configure as needed
                IsActive = true
            };

            _dbContext.Set<LocalStorageKey>().Add(localStorageKey);
            await _dbContext.SaveChangesAsync();

            return localStorageKey;
        }
    }
}
