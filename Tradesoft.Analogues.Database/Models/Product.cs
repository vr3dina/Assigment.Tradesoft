using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Tradesoft.Analogues.Database.Models
{
    public class Product
    {
        public int Id { get; set; }

        private string vendorCode;

        [Required]
        public string VendorCode
        {
            get { return vendorCode; }
            set { vendorCode = Regex.Replace(value, @"[\,\s./-]", string.Empty); }
            //set { vendorCode = value; }
        }

        private string manufacturer;

        [Required]
        public string Manufacturer 
        {
            get { return manufacturer; }
            set { manufacturer = value.ToLower(); }
            //set { manufacturer = value; }
        }

        public override string ToString()
        {
            return VendorCode + " " + Manufacturer;
        }
    }
}
