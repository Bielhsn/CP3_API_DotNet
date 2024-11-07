using CP3.Application.Services;
using CP3.Data.AppData;
using CP3.Data.Repositories;
using CP3.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CP3.IoC
{
    public class Bootstrap
    {
        public static void Start(IServiceCollection services, IConfiguration configuration)
        {
            // Configuração do DbContext com o banco de dados Oracle
            services.AddDbContext<ApplicationContext>(options =>
                options.UseOracle(configuration["ConnectionStrings:Oracle"]));

            // Registro dos Repositórios
            services.AddScoped<IBarcoRepository, BarcoRepository>();

            // Registro dos Serviços de Aplicação
            services.AddScoped<IBarcoApplicationService, BarcoApplicationService>();
        }
    }
}