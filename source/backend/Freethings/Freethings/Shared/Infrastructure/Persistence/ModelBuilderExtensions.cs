using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Freethings.Shared.Infrastructure.Persistence;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ApplyConfigurationsFromNamespace(this ModelBuilder modelBuilder, string namespaceString)
    {
        IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsClass && t.Namespace == namespaceString);

        foreach (Type type in types)
        {
            if (type.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance!);
            }
        }

        return modelBuilder;
    }
}