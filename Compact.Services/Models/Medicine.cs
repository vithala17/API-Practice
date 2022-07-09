using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Compact.Services.Data
{
    public class Medicine
    {
        public string Name { get; set; }

        public string Brand { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string ExpiryDate { get; set; }

        public string Notes { get; set; }

        public override string ToString()
        {
            string medicineDetails = String.Format("\tName\t: {0}\n\tBrand\t: {1}\n\tPrice\t: {2}\n\tQuantity: {3}\n\tExpiry\t: {4}\n\tNotes\t: {5}", this.Name, this.Brand, Convert.ToString(this.Price), Convert.ToString(this.Quantity), this.ExpiryDate, this.Notes);
            return medicineDetails;
        }
    }

    static class MedicineInventory
    {
        static List<Medicine> Stock = null;

        static MedicineInventory()
        {
            Stock = JsonConvert.DeserializeObject<List<Medicine>>("");
        }
    }

}
