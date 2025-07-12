using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Auth.Requirements;
using ComposedHealthBase.Server.Auth.AuthorizationHandlers;
using Server.Modules.Clinical.Entities;
using ComposedHealthBase.Server.Auth.Providers;

namespace Server.Modules.Clinical.Infrastructure.AuthorizationHandlers
{
    public class ClinicalResourceAccessRequirement : IAuthorizationRequirement
    {
        public string Description => "User must be a clinician accessing their own clinical resources";
    }

    public class ClinicalResourceAccessAuthorizationHandler :
        AuthorizationHandler<ClinicalResourceAccessRequirement, IAuditEntity>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ClinicalResourceAccessRequirement requirement,
            IAuditEntity resource)
        {
            // Only handle clinical entities
            if (resource is not CaseNote && resource is not ClinicalReport)
            {
                return Task.CompletedTask;
            }

            // Check if user is a senior or junior clinician
            if (!context.User.IsInRole("seniorclinician") && !context.User.IsInRole("juniorclinician"))
            {
                return Task.CompletedTask;
            }

            // Get user's ID from JWT sub claim
            var userSubClaim = context.User.FindFirstValue("sub");
            if (string.IsNullOrEmpty(userSubClaim) || !Guid.TryParse(userSubClaim, out var userId))
            {
                return Task.CompletedTask;
            }

            // Check if resource has ClinicianId and if it matches the user's ID
            Guid? clinicianId = resource switch
            {
                CaseNote caseNote => caseNote.ClinicianId,
                ClinicalReport clinicalReport => clinicalReport.ClinicianId,
                _ => null
            };

            if (clinicianId.HasValue && clinicianId.Value == userId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
