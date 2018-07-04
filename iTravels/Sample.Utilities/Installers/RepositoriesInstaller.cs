using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Sample.Core.Repositories;
using Sample.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Utilities.Installers
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IUserRepository>().ImplementedBy<UserRepository>()
                .LifestyleTransient()
                );
            //container.Register(Classes.FromThisAssembly()
            //                       .Where(Component.IsInSameNamespaceAs<UserRepository>())
            //                       .WithService.DefaultInterfaces()
            //                       .LifestyleTransient()
            //                       .Configure(c => c.DependsOn(new { pageSize = 20 })));
        }
    }
}
