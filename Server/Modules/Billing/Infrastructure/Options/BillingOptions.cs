using System.ComponentModel.DataAnnotations;

namespace Server.Modules.Billing.Infrastructure.Options
{
    public class BillingOptions
    {
        // ...existing code...

        [Required]
        public string XeroApiUrl { get; set; } = string.Empty;

        [Required]
        public string XeroAccessToken { get; set; } = string.Empty;

        [Required]
        public string XeroTenantId { get; set; } = string.Empty;

        [Required]
        public string XeroClientId { get; set; } = string.Empty;

        [Required]
        public string XeroClientSecret { get; set; } = string.Empty;
    }
}