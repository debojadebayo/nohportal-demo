namespace Server.Modules.CRM.Infrastructure.Auth
{
    internal sealed class Role
    {
        internal const string Administrator = "administrator";
        internal const string TenantAdministrator = "tenant_administrator";
        internal const string TenantLimitedAdministrator = "tenant_limited_administrator";
        internal const string Clinician = "clinician";
        internal const string Employee = "employee";
        internal const string Finance = "finance";
    }
}