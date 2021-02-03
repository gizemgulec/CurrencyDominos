using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyDominos.Model;
using CurrencyDominos.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyDominos.Controller
{
    [Route("v1/")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyServices _services;

        public CurrencyController(ICurrencyServices services)
        {
            _services = services;
        }

        /// <summary>
        /// Get All Currency List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCurrencyList")]
        public ActionResult<List<CurrencyItem>> GetCurrencyList()
        {
            List<CurrencyItem> currencyList = _services.GetCurrencyList();
            if (currencyList == null)
            {
                return NotFound();
            }
            return currencyList;
        }

        /// <summary>
        /// Get Currency By Currency Name
        /// </summary>
        /// <param name="currencyName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCurrencyByCurrencyName")]
        public ActionResult<CurrencyItem> GetCurrencyByCurrencyName(String currencyName)
        {
            List<CurrencyItem> currencyList = _services.GetCurrencyList();
            if (currencyList == null)
            {
                return NotFound();
            }
            CurrencyItem currencyItem = new CurrencyItem();
            for (int k = 0; k < currencyList.Count; k++)
            {
                if (currencyList[k].CurrencyName == currencyName)
                {
                    currencyItem = currencyList[k];
                }
            }
            return currencyItem;
        }

        /// <summary>
        /// Get Currency By Interval Sell
        /// </summary>
        /// <param name="smallSell"></param>
        /// <param name="bigSell"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCurrencyByIntervalSell")]
        public ActionResult<List<CurrencyItem>> GetCurrencyByIntervalSell(double smallSell, double bigSell)
        {
            List<CurrencyItem> currencyList = _services.GetCurrencyList();
            if (currencyList == null)
            {
                return NotFound();
            }
            List<CurrencyItem> resultList = new List<CurrencyItem>();
           
            for (int k = 0; k < currencyList.Count; k++)
            {
                CurrencyItem currencyItem = new CurrencyItem();
                if (smallSell < currencyList[k].ForexSelling && currencyList[k].ForexSelling < bigSell)
                {
                    currencyItem = currencyList[k];
                    resultList.Add(currencyItem);
                }
            }
            return resultList;
        }
    }
}
