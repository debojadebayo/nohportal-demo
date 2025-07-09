using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Auth.Requirements;
using ComposedHealthBase.Server.Auth.AuthorizationHandlers;
using Server.Modules.Scheduling.Entities;
using ComposedHealthBase.Server.Auth.Providers;
using Server.Modules.Scheduling.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;

namespace Server.Modules.Scheduling.Infrastructure.AuthorizationHandlers
{
    public class SchedulingResourceAccessRequirement : IAuthorizationRequirement
    {
        public string Description => "User must have access to Scheduling resources";
    }

    public class SchedulingResourceAccessAuthorizationHandler :
        AuthorizationHandler<SchedulingResourceAccessRequirement, IAuditEntity>
    {
        private readonly SchedulingDbContext _dbContext;

        public SchedulingResourceAccessAuthorizationHandler(SchedulingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            SchedulingResourceAccessRequirement requirement,
            IAuditEntity resource)
        {
            // Only handle Scheduling module entities
            if (resource is not Schedule && resource is not Referral)
            {
                return;
            }

            // Check if user is a senior or junior clinician
            if (!context.User.IsInRole("seniorclinician") && !context.User.IsInRole("juniorclinician"))
            {
                return;
            }

            // Get user's ID from JWT sub claim
            var userSubClaim = context.User.FindFirstValue("sub");
            if (string.IsNullOrEmpty(userSubClaim) || !Guid.TryParse(userSubClaim, out var userId))
            {
                return;
            }

            // Find the clinician record for this user (userId is now the same as clinician Id)
            var clinician = await _dbContext.Set<Clinician>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == userId);

            if (clinician == null)
            {
                return; // User is not a registered clinician
            }

            // Check access based on resource type
            bool hasAccess = resource switch
            {
                Schedule schedule => await HasAccessToSchedule(schedule, userId),
                Referral referral => await HasAccessToReferral(referral, userId),
                _ => false
            };

            if (hasAccess)
            {
                context.Succeed(requirement);
            }
        }

        private Task<bool> HasAccessToSchedule(Schedule schedule, Guid clinicianId)
        {
            // For schedules, check if the ClinicianId matches the logged-in clinician
            return Task.FromResult(schedule.ClinicianId == clinicianId);
        }

        private async Task<bool> HasAccessToReferral(Referral referral, Guid clinicianId)
        {
            // For referrals, check if there are any schedules associated with this referral
            // that belong to the logged-in clinician
            var hasAccess = await _dbContext.Set<Schedule>()
                .AsNoTracking()
                .AnyAsync(s => s.ReferralId == referral.Id && s.ClinicianId == clinicianId);

            return hasAccess;
        }
    }
}
