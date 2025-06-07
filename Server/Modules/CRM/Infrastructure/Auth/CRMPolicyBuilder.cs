// using Microsoft.AspNetCore.Authorization;
// using Server.Modules.CRM.Infrastructure.Auth.Requirements;
// using System; // For Enum.GetValues

// namespace Server.Modules.CRM.Infrastructure.Auth
// {
//     public static class CRMPolicyBuilder
//     {
//         public static void AddCRMPolicies(AuthorizationOptions options)
//         {
//             // 1. Administrator:
//             // Implicitly handled if "Administrator" role gets all "Entity.Action.Tenant" claims
//             // via CRMRolePermissionService and those claims are used by handlers.
//             // Or, a specific policy:
//             options.AddPolicy("IsApplicationAdministrator", policy => policy.RequireRole("Administrator"));

//             // 2. TenantAdministrator:
//             // This will primarily be handled by resource-based authorization handlers
//             // (e.g., GenericTenantResourceAuthorizationHandler) checking for the "TenantAdministrator" role
//             // and matching TenantId.
//             // We define general operation policies. The handlers enforce the tenant scoping.
//             options.AddPolicy(CRMPermissionConstants.Policy_Resource_Create, policy => policy.AddRequirements(OperationRequirements.Create));
//             options.AddPolicy(CRMPermissionConstants.Policy_Resource_Read,   policy => policy.AddRequirements(OperationRequirements.Read));
//             options.AddPolicy(CRMPermissionConstants.Policy_Resource_Update, policy => policy.AddRequirements(OperationRequirements.Update));
//             options.AddPolicy(CRMPermissionConstants.Policy_Resource_Delete, policy => policy.AddRequirements(OperationRequirements.Delete));

//             // 3. TenantLimitedAdministrator:
//             // Requires highly specific resource-based handlers for Referral, Schedule, Case.
//             // Policies would be similar to TenantAdministrator (using OperationRequirements),
//             // but the handlers (e.g., ReferralAuthorizationHandler) would check for "TenantLimitedAdministrator"
//             // role and CreatedByUserId / linked ReferralId.

//             // 4. SeniorClinician & 5. JuniorClinician:
//             // Also resource-based. Handlers for Clinician/Employee, Schedule, Casenote, Report entities.
//             // Example for updating their own clinician entry (assuming Clinician is an entity T):
//             // The policy "Resource.Update" would be used, and a ClinicianAuthorizationHandler
//             // would check if the resource's ID matches the user's ClinicianId and if the role is Senior/Junior.

//             // Policy for publishing reports (specifically for SeniorClinician)
//             options.AddPolicy(CRMPermissionConstants.Policy_CanPublishReport, policy =>
//             {
//                 policy.AddRequirements(new CanPublishReportRequirement());
//                 // This policy would be checked specifically when trying to publish.
//                 // The general "Update Report" permission would be separate.
//             });
//             // Note: The "JuniorClinician cannot set Published=true" is more of a business rule
//             // within the update logic after general update permission is granted.
//             // The CanPublishReportRequirement is for *allowing* publish.

//             // 6. Employee:
//             // Policy for employees reading their reports.
//             options.AddPolicy(CRMPermissionConstants.Policy_EmployeeCanReadReport, policy =>
//             {
//                 policy.AddRequirements(new EmployeeCanReadReportRequirement());
//                 // This policy would be checked when an employee tries to read a report.
//                 // The ReportAuthorizationHandler would verify the KeycloakId.
//             });


//             // --- Existing dynamic permission claim policies (still useful for basic checks) ---
//             // These are good for roles that get a wide array of simple permissions.
//             // For resource-based checks, the policies above using OperationRequirements are more common.
//             foreach (var permission in CRMPermissionConstants.AllPermissions)
//             {
//                 // Avoid re-adding if already defined by more specific policies above, or give them unique names.
//                 // For simplicity, we'll assume these are for more direct claim checks not covered by resource auth.
//                 if (options.GetPolicy(permission) == null)
//                 {
//                     options.AddPolicy(permission, policy =>
//                         policy.RequireClaim(CRMPermissionConstants.ClaimType, permission));
//                 }
//             }

//             // Register a policy for each PermissionsEnum value
//             foreach (PermissionsEnum permission in Enum.GetValues(typeof(PermissionsEnum)))
//             {
//                 var permissionName = permission.ToString();
//                 if (options.GetPolicy(permissionName) == null)
//                 {
//                     options.AddPolicy(permissionName, policy =>
//                         policy.RequireClaim(CRMPermissionConstants.ClaimType, permissionName));
//                 }
//             }
//         }
//     }
// }
