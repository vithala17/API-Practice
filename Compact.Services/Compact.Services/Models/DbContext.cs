using ABCPharma.Data;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Compact.Services.Data;

namespace ABCPharma.Services.Models
{
    public class DbContext : IDbContext
    {
        public IList<Medicine> medicineContext = null;
        static string _filePath = string.Empty;

        public DbContext(string filePath)
        {
            _filePath = filePath;
            medicineContext = ReadFromDb();
        }

        public async void WriteToDb(Medicine medicine)
        {
            try
            {
                if (medicineContext == null)
                    medicineContext = ReadFromDb();

                medicineContext.Add(medicine);
                var updatedContent = JsonConvert.SerializeObject(medicineContext);

                await File.WriteAllTextAsync(_filePath, updatedContent);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<Medicine> GetMedicines()
        {
            return medicineContext;
        }

        public IList<Medicine> ReadFromDb()
        {
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(_filePath);
                var content = reader.ReadToEnd();
                if (content == @"{\r\n\r\n}\r\n")
                    medicineContext = new List<Medicine>();
                else
                    medicineContext = JsonConvert.DeserializeObject<List<Medicine>>(content);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                reader.Close();
            }
            if (medicineContext == null)
                return new List<Medicine>();
            else
                return medicineContext;
        }
    }
}