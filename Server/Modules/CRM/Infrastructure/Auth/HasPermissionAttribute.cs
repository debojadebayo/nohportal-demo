using System;
using Microsoft.AspNetCore.Authorization;

namespace Server.Modules.CRM.Infrastructure.Auth
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission)
            : base(policy: permission.ToString())
        {
        }
    }
}
