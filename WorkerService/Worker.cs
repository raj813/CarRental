using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarRental.API.Data;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private CarRentalDbContext dbContext;
        //private CarRentalDbContext dbContextUpdate;
        //private CarRentalDbContext dbContextDelete;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceScopeFactory.CreateScope();
                dbContext = scope.ServiceProvider.GetRequiredService<CarRentalDbContext>();

               
                _logger.LogInformation("start services");

                //Update the isRented to false for each car in the expiredTrip before Deleting
                await UpdateIsRented();



                await Task.Delay(TimeSpan.FromHours(24), stoppingToken); //60*1000 one min
            }
        }

       
        private async Task UpdateIsRented()
        {
           
            //Update the isRented to false for each car in the expiredTrip before Deleting
            var deletedTrip = dbContext.Trips.Where(t => t.EndDate < DateTime.Today).FirstOrDefault();
            if (deletedTrip != null)
            {
                dbContext.Trips.Remove(deletedTrip);
                await dbContext.SaveChangesAsync();

                var car = dbContext.Cars.Where(c => c.CarId == 1).FirstOrDefault();
                car.IsRented = false;
                await dbContext.SaveChangesAsync();
            }
        }
        
    }
}
