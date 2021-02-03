using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CurrencyDominos.Model
{
    public class CurrencyItem
    {
        public string Unit { get; set; }
        public string Isim { get; set; }
        public string CurrencyName { get; set; }
        public double ForexBuying { get; set; }
        public double ForexSelling { get; set; }
        public double BanknoteBuying { get; set; }
        public double BanknoteSelling { get; set; }
        public double CrossRateUSD { get; set; }
        public double CrossRateOther { get; set; }
    }
}




