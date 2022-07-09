using ABCPharma.Data;
using Compact.Services.Data;
using System.Collections.Generic;

namespace ABCPharma.Services.Models
{
    public interface IDbContext
    {
        void WriteToDb(Medicine medicine);

        IList<Medicine> ReadFromDb();

        IList<Medicine> GetMedicines();
    }
}