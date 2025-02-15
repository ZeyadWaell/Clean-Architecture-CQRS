
using ChatApp.Infrastructure.Injection;
using ChatApp.API.Injection;
using ChatApp.Api.Hubs;
using ChatApp.Routes;

namespace ChatApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            builder.Services.AddApplicationServices()
                .AddApiServices(configuration)
                .AddInfrastructureServices(configuration);

            var corsPolicy = "AllowFrontend";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(corsPolicy, policy =>
                {
                    policy.WithOrigins("http://localhost:3000")  
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();  
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddSignalR();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(corsPolicy);

            app.UseAuthentication();  
            app.UseAuthorization();   

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>(ChatHubRoutes.Hub);
            });

            app.Run();
        }
    }
}
