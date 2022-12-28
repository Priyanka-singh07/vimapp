using Microsoft.EntityFrameworkCore;
using System.Configuration;
using VIM_WEBAPI.Data;
using VIM_WEBAPI.Repositories;
using VIM_WEBAPI.Services;

namespace VIM_WEBAPI
{
    public class ConfigureDependencies
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            //Database
            services.AddDbContext<AppDbContext>(options => options.UseOracle(configuration.GetConnectionString("DefaultConnection")));
        

            services.AddTransient<IbillsRepository, billsRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddMvc();


      
        }
    }
}
