using System;
using System.Linq;

using Microsoft.Extensions.Logging;
using Compact.Services.Abstractions;
using ABCPharma.Data;
using ABCPharma.Data.Enumerations;
using ABCPharma.Services.Models;
using System.Collections.Generic;
using Compact.Services.Data;

namespace Compact.Services.Implementations
{

    public class WarehouseService : IWarehouseService
    {
        ILogger<WarehouseService> logger;
        IDbContext context;

        public WarehouseService(ILogger<WarehouseService> logger, IDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public HealthMeter AddMedicine(Medicine medicine)
        {
            try
            {
                var medicineHealth = checkExpiry(medicine.ExpiryDate);
                if (medicineHealth == HealthMeter.Green)
                {
                    logger.LogInformation("{0} added to the store succesfully.", medicine.Name);
                    context.WriteToDb(medicine);
                    return HealthMeter.Green;
                }
                else if (medicineHealth == HealthMeter.Orange)
                {
                    logger.LogWarning("{0} is expiring in next 15 days.", medicine.Name);
                    logger.LogWarning("{0} added to the store succesfully.", medicine.Name);
                    context.WriteToDb(medicine);
                    return HealthMeter.Orange;
                }
                else
                {
                    logger.LogError(@"Can not add an expired medicine!! Please check {0}'s expiry date.", medicine.Name);
                    throw new Exception("Can not add an expired medicine!! Please check the expiry date.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Exception : {0}", ex.Message);
                logger.LogInformation("Trace:{0}", ex.StackTrace);
                throw;
            }
        }

        public Medicine GetMedicine(string name)
        {

            IList<Medicine> list = context.GetMedicines();
            var result =  list.Where(x => x.Name == name).ToList<Medicine>().FirstOrDefault();
            return result;
        }

        public IList<Medicine> GetAllMedicine()
        {
            return context.ReadFromDb();
            //return context.GetMedicines();
        }

        private HealthMeter checkExpiry(string expiryDate)
        {
            var date = expiryDate.Split('-');
            var eDate = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]));
            var lifeLeft = (eDate - DateTime.Today).TotalDays;

            if (lifeLeft >= 30)
                return HealthMeter.Green;
            else if (lifeLeft >= 15)
                return HealthMeter.Orange;
            else
                return HealthMeter.Red;

        }
    }
}