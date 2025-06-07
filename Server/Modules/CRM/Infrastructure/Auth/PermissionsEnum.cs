namespace Server.Modules.CRM.Infrastructure.Auth
{
    public enum PermissionsEnum
    {
        // Customer
        ViewCustomers = 0,
        EditCustomers = 1,
        DeleteCustomers = 2,

        // Contract
        ViewContracts = 3,
        EditContracts = 4,
        DeleteContracts = 5,

        // Product
        ViewProducts = 6,
        EditProducts = 7,
        DeleteProducts = 8,

        // ProductType
        ViewProductTypes = 9,
        EditProductTypes = 10,
        DeleteProductTypes = 11,

        // Employee
        ViewEmployees = 12,
        EditEmployees = 13,
        DeleteEmployees = 14,

        // EmployeeDocument
        ViewEmployeeDocuments = 15,
        EditEmployeeDocuments = 16,
        DeleteEmployeeDocuments = 17,

        // CustomerDocument
        ViewCustomerDocuments = 18,
        EditCustomerDocuments = 19,
        DeleteCustomerDocuments = 20,

        // Manager
        ViewManagers = 21,
        EditManagers = 22,
        DeleteManagers = 23,

        // Schedule
        ViewSchedules = 24,
        EditSchedules = 25,
        DeleteSchedules = 26,

        // Referral
        ViewReferrals = 27,
        EditReferrals = 28,
        DeleteReferrals = 29,

        // Clinician
        ViewClinicians = 30,
        EditClinicians = 31,
        DeleteClinicians = 32,

        // Users (existing)
        ManageUsers = 33
    }
}
