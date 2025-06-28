using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using ComposedHealthBase.Server.Auth.Requirements;
using ComposedHealthBase.Server.Auth.Constants;
using System.Reflection;
using System.Linq;

namespace ComposedHealthBase.Server.Auth.Extensions
{
    /// <summary>
    /// Extension methods for registering permission-based authorization policies
    /// </summary>
    public static class AuthorizationExtensions
    {
        /// <summary>
        /// Adds permission-based authorization policies for a specific entity type
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <param name="options">Authorization options</param>
        public static void AddPermissionPoliciesForEntity<TEntity>(this AuthorizationOptions options)
        {
            var entityName = typeof(TEntity).Name;
            AddPermissionPoliciesForEntity(options, entityName);
        }

        /// <summary>
        /// Adds permission-based authorization policies for a specific entity type by name
        /// </summary>
        /// <param name="options">Authorization options</param>
        /// <param name="entityName">The entity name</param>
        public static void AddPermissionPoliciesForEntity(this AuthorizationOptions options, string entityName)
        {
            var operations = new[] {
                PermissionOperations.Create,
                PermissionOperations.View,
                PermissionOperations.Update,
                PermissionOperations.Delete
            };

            foreach (var operation in operations)
            {
                var permission = PermissionHelper.GeneratePermission(operation, entityName);
                options.AddPolicy($"Permission:{permission}", policy =>
                {
                    policy.Requirements.Add(new PermissionRequirement(permission));
                });
            }
        }

        /// <summary>
        /// Adds permission-based authorization policies for all entity types that implement IEntity
        /// </summary>
        /// <param name="options">Authorization options</param>
        public static void AddPermissionPoliciesForEntities(this AuthorizationOptions options)
        {
            var entityTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(i => i.Name == "IEntity"))
                .ToList();

            foreach (var entityType in entityTypes)
            {
                AddPermissionPoliciesForEntity(options, entityType.Name);
            }
        }
    }
}
