using CurrencyDominos.Model;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CurrencyDominos.Service
{
    public class CurrencyServices : ICurrencyServices
    {
        public List<CurrencyItem> GetCurrencyList()
        {
            var client = new RestClient("https://www.tcmb.gov.tr/kurlar/today.xml");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            List<CurrencyItem> currencyItemList = new List<CurrencyItem>();
            if (response.StatusCode.ToString() == "OK")
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(response.Content);
                XmlNodeList nodeList = xmldoc.GetElementsByTagName("Tarih_Date");


                foreach (XmlNode node in nodeList)
                {
                    if (node.HasChildNodes)
                    {
                        foreach (var childNode in node.ChildNodes)
                        {
                            CurrencyItem item = new CurrencyItem();
                            var innerXml = ((XmlElement)childNode).InnerXml;
                            innerXml = "<Root>" + innerXml + "</Root>";
                            XmlDocument newXmldoc = new XmlDocument();
                            newXmldoc.LoadXml(innerXml);
                            var unit = newXmldoc.GetElementsByTagName("Unit");
                            if (unit != null)
                            {
                                item.Unit = unit[0].InnerText;
                            }


                            var isim = newXmldoc.GetElementsByTagName("Isim");
                            if (isim != null)
                            {
                                item.Isim = isim[0].InnerText;
                            }
                            //
                            var currencyName = newXmldoc.GetElementsByTagName("CurrencyName");
                            if (currencyName != null)
                            {
                                item.CurrencyName = currencyName[0].InnerText;
                            }

                            var forexBuying = newXmldoc.GetElementsByTagName("ForexBuying");
                            if (forexBuying != null)
                            {
                                item.ForexBuying = forexBuying[0].InnerText != "" ? Double.Parse(forexBuying[0].InnerText, CultureInfo.InvariantCulture) : 0.0;
                            }

                            var forexSelling = newXmldoc.GetElementsByTagName("ForexSelling");
                            if (forexSelling != null)
                            {
                                item.ForexSelling = forexSelling[0].InnerText != "" ? Double.Parse(forexSelling[0].InnerText, CultureInfo.InvariantCulture) : 0.0;
                            }

                            var banknoteBuying = newXmldoc.GetElementsByTagName("BanknoteBuying");
                            if (banknoteBuying != null)
                            {
                                item.BanknoteBuying = banknoteBuying[0].InnerText != "" ? Double.Parse(banknoteBuying[0].InnerText, CultureInfo.InvariantCulture) : 0.0;
                            }

                            var banknoteSelling = newXmldoc.GetElementsByTagName("BanknoteSelling");
                            if (banknoteSelling != null)
                            {
                                item.BanknoteSelling = banknoteSelling[0].InnerText != "" ? Double.Parse(banknoteSelling[0].InnerText, CultureInfo.InvariantCulture) : 0.0;
                            }

                            var crossRateUSD = newXmldoc.GetElementsByTagName("CrossRateUSD");
                            if (crossRateUSD != null)
                            {
                                item.CrossRateUSD = crossRateUSD[0].InnerText != "" ? Double.Parse(crossRateUSD[0].InnerText, CultureInfo.InvariantCulture) : 0.0;
                            }

                            var crossRateOther = newXmldoc.GetElementsByTagName("CrossRateOther");
                            if (crossRateOther != null)
                            {
                                item.CrossRateOther = crossRateOther[0].InnerText != "" ? Double.Parse(crossRateOther[0].InnerText, CultureInfo.InvariantCulture) : 0.0;
                            }
                            currencyItemList.Add(item);
                        }
                    }

                }

            }
            return currencyItemList;
        }
    }
}
