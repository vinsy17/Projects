using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Sample.Core.Services;
using Sample.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Utilities.Installers
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IUserService>().ImplementedBy<UserService>()
                .LifestyleTransient()
                );
            //container.Register(AllTypes.FromThisAssembly().Pick()
            //                       .If(Component.IsInSameNamespaceAs<FormsAuthenticationService>())
            //                       .LifestyleTransient()
            //                       .WithService.DefaultInterfaces());
        }
    }
}
