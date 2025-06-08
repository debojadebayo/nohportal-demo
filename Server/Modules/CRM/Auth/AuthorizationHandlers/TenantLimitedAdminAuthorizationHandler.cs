using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using NationOH.Server.Modules.Schedule.Entities;

// For CaseNote entity and repository (example, adjust module as needed)
// using NationOH.Server.Modules.CaseNotes.Domain.Entities; // Assuming CaseNote entity is here
// using NationOH.Server.Modules.CaseNotes.Application.Abstractions; // Assuming ICaseNoteRepository is here
// --- End of assumed using statements ---

namespace NationOH.Server.Modules.CRM.Auth.AuthorizationHandlers
{
    // Placeholder for the requirement. This would typically be defined elsewhere.
    public class TenantLimitedAdminRequirement : IAuthorizationRequirement
    {
        // Add any properties specific to the requirement if needed
    }

    public class TenantLimitedAdminAuthorizationHandler : AuthorizationHandler<TenantLimitedAdminRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IReferralRepository _referralRepository;
        // private readonly IScheduleRepository _scheduleRepository; // Uncomment and inject if needed for schedules
        // private readonly ICaseNoteRepository _caseNoteRepository; // Uncomment and inject if needed for case notes

        public TenantLimitedAdminAuthorizationHandler(
            IHttpContextAccessor httpContextAccessor,
            IReferralRepository referralRepository
            // IScheduleRepository scheduleRepository, // Uncomment if needed
            // ICaseNoteRepository caseNoteRepository  // Uncomment if needed
            )
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _referralRepository = referralRepository ?? throw new ArgumentNullException(nameof(referralRepository));
            // _scheduleRepository = scheduleRepository ?? throw new ArgumentNullException(nameof(scheduleRepository)); // Uncomment if needed
            // _caseNoteRepository = caseNoteRepository ?? throw new ArgumentNullException(nameof(caseNoteRepository)); // Uncomment if needed
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TenantLimitedAdminRequirement requirement)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                return;
            }

            // Replace "ClaimTypes.NameIdentifier" with the actual claim type for the user's Keycloak ID (e.g., "sub")
            var userKeycloakId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userKeycloakId))
            {
                return; // User not authenticated or KeycloakId claim missing
            }

            Guid? targetReferralId = null;

            // 1. Try to get referralId directly from route values
            if (httpContext.Request.RouteValues.TryGetValue("referralId", out var referralIdObj) && 
                Guid.TryParse(referralIdObj?.ToString(), out var parsedReferralId))
            {
                targetReferralId = parsedReferralId;
            }
            // 2. Else, try to get it from other resource IDs if applicable
            //    This section needs to be adapted based on your routing and resource structure.
            //    Example for a Schedule:
            /*
            else if (httpContext.Request.RouteValues.TryGetValue("scheduleId", out var scheduleIdObj) &&
                     Guid.TryParse(scheduleIdObj?.ToString(), out var parsedScheduleId) &&
                     _scheduleRepository != null) // Ensure repository is injected
            {
                // Assuming Schedule entity has a ReferralId property and IScheduleRepository has GetByIdAsync
                var schedule = await _scheduleRepository.GetByIdAsync(parsedScheduleId);
                if (schedule != null)
                {
                    targetReferralId = schedule.ReferralId; // Assuming Schedule entity has 'ReferralId'
                }
            }
            */
            // Add similar blocks for CaseNote or other entities if needed:
            /*
            else if (httpContext.Request.RouteValues.TryGetValue("caseNoteId", out var caseNoteIdObj) &&
                     Guid.TryParse(caseNoteIdObj?.ToString(), out var parsedCaseNoteId) &&
                     _caseNoteRepository != null) // Ensure repository is injected
            {
                var caseNote = await _caseNoteRepository.GetByIdAsync(parsedCaseNoteId);
                if (caseNote != null)
                {
                    targetReferralId = caseNote.ReferralId; // Assuming CaseNote entity has 'ReferralId'
                }
            }
            */

            if (!targetReferralId.HasValue)
            {
                // Unable to determine the target ReferralId for authorization check.
                // This might be an endpoint not covered by this specific logic, or a configuration issue.
                return;
            }

            // Fetch the anchor Referral object
            // Assuming IReferralRepository has GetByIdAsync and Referral entity has CreatedByKeycloakId
            var referral = await _referralRepository.GetByIdAsync(targetReferralId.Value); 
            if (referral == null)
            {
                // Referral not found, cannot verify ownership.
                return;
            }

            // Check if the Referral's CreatedByKeycloakId matches the user's KeycloakId
            // Ensure your Referral entity has a 'CreatedByKeycloakId' string property
            if (referral.CreatedByKeycloakId == userKeycloakId)
            {
                context.Succeed(requirement);
            }
        }
    }
}
