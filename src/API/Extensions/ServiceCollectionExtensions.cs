using EnterpriseTaskManagement.Domain.Interfaces;
using EnterpriseTaskManagement.Infrastructure.Data;
using EnterpriseTaskManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EnterpriseTaskManagement.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Repositories 
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();
        services.AddScoped<IProjectMemberRepository, ProjectMemberRepository>();

        // MediatR - CQRS pattern
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("EnterpriseTaskManagement.Application")));

        return services;
        
    }
}