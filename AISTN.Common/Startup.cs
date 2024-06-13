using AISTN.Common.Helper;
using AISTN.Data;
using AISTN.Data.DataModel;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AISTN.Common
{
    public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }

        public void ConfigureServices(IServiceCollection serviceCollection, Assembly assembly)
        {
            var services = GetServices(assembly);
            var entityTypes = GetEntityTypes();
            var iRepository = typeof(IGenericRepository<>);
            var repository = typeof(GenericRepository<>);

            // inject service classes with injectable attribute
            foreach (var service in services)
            {
                serviceCollection.AddTransient(service);
            }

            // inject generic repository to all types
            foreach (var type in entityTypes)
            {
                serviceCollection.AddTransient(iRepository.MakeGenericType(new Type[] { type }), repository.MakeGenericType(new Type[] { type }));
            }

        }

        IEnumerable<Type> GetServices(Assembly assembly)
        {
            List<Type> assemblyList = assembly.GetTypes().Where(t => t.GetCustomAttributes(typeof(Injectable), true).Length > 0).ToList();
            assemblyList.AddRange(typeof(ServiceBase).Assembly.GetTypes().Where(t => t.GetCustomAttributes(typeof(Injectable), true).Length > 0));
            assemblyList.Add(typeof(ExceptionLogger));

            return assemblyList;
        }

        IEnumerable<Type> GetEntityTypes()
        {
            return typeof(AistnContext).Assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IEntity)));
        }
    }
}
