## Admin 
- Full CRUD access to all resources. (DONE)

## TenantAdmin 
- Restricted to their assigned tenant.
- Access control via TenantKeycloakId on all resources. (CHBase item DONE)

## TenantLimitedAdmin 
- Restricted to cases they referred and associated downstream data.
- Access control via TenantKeycloakId on resources.
- Can create referrals (own KeycloakId marked on referral).
- Can fetch referral graph (includes schedule, employee details, clinician).
- Cannot create or update case notes or reports.
# Practical implementation
- For any operation, the TenantLimitedAdminAuthorizationHandler need to fetch Referral object (Anchor object) which going forward it will compare any schedule, case note etc to using ReferralId.
# Functions:
- Referral.Any.Create
- Referral.Own.Read
- Referral.Own.Update
- Referral.Own.Delete

- Schedule.Own.Create
- Schedule.Own.Read
- Schedule.Own.Update
- Schedule.Own.Delete

- CaseNote.Own.Read

## Employee 
- Read-only access restricted to items marked with their own KeycloakId. (DONE)

## Clinician 
- Restricted to cases assigned via their ClinicianId and associated downstream data (e.g., schedules, case notes).
- Similar to TenantLimitedAdmin for their assigned scope.
- Cannot create referrals.
- Can create and update case notes linked to specific schedule IDs.
Functions:
- Referral.Delegated.Read
- Schedule.Delegated.Create
- Schedule.Delegated.Read
- Schedule.Delegated.Update
- Schedule.Delegated.Delete
- CaseNote.Delegated.Read
- CaseNote.Delegated.Create
- CaseNote.Delegated.Update

## Finance 
- Restricted to finance-related data and functionalities.
- Responsibilities include managing invoices, payments, and transactions.
- Can generate financial reports.
Functions:
- Invoice.Any.Create
- Invoice.Any.Read
- Invoice.Any.Update
- Invoice.Any.Delete
- Payment.Any.Create
- Payment.Any.Read
- Transaction.Any.Read
- FinancialReport.Any.Read
- FinancialReport.Any.Generate