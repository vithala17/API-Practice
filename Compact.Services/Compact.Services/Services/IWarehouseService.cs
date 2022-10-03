using ABCPharma.Data;
using ABCPharma.Data.Enumerations;
using Compact.Services.Data;
using System.Collections.Generic;

namespace Compact.Services.Abstractions
{
    public interface IWarehouseService
    {

        public HealthMeter AddMedicine(Medicine medicine);

        public Medicine GetMedicine(string name);

        public IList<Medicine> GetAllMedicine();

    }
}
