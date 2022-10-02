using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.API.Data;

namespace WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    //Configuration["ConnectionStrings:DefaultConnection"]
                    var optionsBuilder = new DbContextOptionsBuilder<CarRentalDbContext>();
                    optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database= CarRental;Trusted_Connection=True;");//,
                    services.AddScoped<CarRentalDbContext>(s => new CarRentalDbContext(optionsBuilder.Options));
                    services.AddHostedService<Worker>();
                });
    }
}
