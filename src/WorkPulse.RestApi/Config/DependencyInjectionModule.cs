using System.Reflection;
using Autofac;
using Microsoft.EntityFrameworkCore;
using WorkPulse.Contracts;
using WorkPulse.Infrastructure.Identity;
using WorkPulse.Infrastructure.Security;
using WorkPulse.Persistence.DataContext;
using WorkPulse.Persistence.UnitOfWorks;
using WorkPulse.Services.Common.Interfaces.Security;
using WorkPulse.Services.Identity;
using WorkPulse.Services.UnitOfWorks;
using WorkPulse.Services.Users.Contracts;

namespace WorkPulse.RestApi.Config;

public class DependencyInjectionModule : Autofac.Module
{
    
    protected override void Load(ContainerBuilder builder)
    {
        var serviceAssembly = typeof(IUserService).Assembly;
        var repositoryAssembly  = typeof(EfDataContext).Assembly;
        
        builder.RegisterType<EfDataContext>()
            .AsSelf()
            .InstancePerLifetimeScope();
        
        builder.RegisterAssemblyTypes(repositoryAssembly)
            .AssignableTo<Repository>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(serviceAssembly)
            .AssignableTo<Service>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<IdentityService>()
            .As<IIdentityService>()
            .SingleInstance();
        
        builder.RegisterType<TokenService>()
            .As<ITokenService>()
            .SingleInstance();
        
        builder.RegisterType<JwtSettings>()
            .AsSelf()
            .SingleInstance();
        
        builder.RegisterAssemblyTypes(repositoryAssembly)
            .AssignableTo<Query>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        
        base.Load(builder);
    }
}