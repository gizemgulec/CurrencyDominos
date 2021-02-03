using CurrencyDominos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyDominos.Service
{
    public interface ICurrencyServices
    {
        List<CurrencyItem> GetCurrencyList();
    }
}
