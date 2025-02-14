using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ChatApp.Infrastructure.Data;
using ChatApp.Core.Interfaces;
using ChatApp.Infrastructure.Repositories;
using ChatApp.Infrastructure.ExternalServices;
using ChatApp.Core.Interfaces.Main;
using ChatApp.Infrastructure.Repositories.Main;
using ChatApp.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Infrastructure.Injection
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

            services.AddDbContext<ChatDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ChatConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();

            // ✅ Register Repositories and Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            services.AddScoped<IChatRoomMemberRepository, ChatRoomMemberRepository>();

            // ✅ Register AI Bot Strategies
            services.AddTransient<IBotStrategy, GeminiBotStrategy>();
            services.AddTransient<IBotStrategy, ChatGPTBotStrategy>();

            return services;
        }
    }
}
