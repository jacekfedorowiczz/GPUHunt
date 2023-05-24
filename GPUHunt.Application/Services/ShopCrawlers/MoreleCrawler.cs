using GPUHunt.Application.Interfaces;
using GPUHunt.Application.Models;
using GPUHunt.Application.Services.ShopCrawlers.Info;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Application.Services.ShopCrawlers
{
    public class MoreleCrawler : MoreleInfo, IShopCrawler
    {
        readonly HtmlWeb Web = new();
        readonly NumberFormatInfo Nfi = new CultureInfo("pl-PL", false).NumberFormat;
        private readonly String _nextSiteSelector = "li.pagination-lg.next > link";
        private readonly String _gpuCardSelector = ".cat-product.card";
        private readonly String _gpuNameSelector = ".cat-product-name > h2 > a";
        private readonly String _gpuPriceSelector = ".price-new";
        private readonly String _characterToAvoid = "(";

        public async Task<IEnumerable<GPU>> CrawlShop()
        {
            try
            {
                var sites = await GetSites();
                var GPUs = await GetGPUs(sites);
                return GPUs;
            }
            catch (ArgumentNullException)
            {
                throw new Exception("Błąd crawlera morele.");
            }
        }

        private async Task<IEnumerable<string>> GetSites()
        {
            var document = Web.Load(MoreleBaseURL);
            var urls = new List<string>() { MoreleBaseURL };

            if (document.QuerySelector(_nextSiteSelector).Attributes["href"].Value != null)
            {
                var nextPageUrl = "https://www.morele.net" + document.QuerySelector(_nextSiteSelector).Attributes["href"].Value.ToString();
                while (nextPageUrl != null)
                {
                    urls.Add(nextPageUrl);
                    var newDocument = Web.Load(nextPageUrl);
                    if (newDocument.QuerySelector(_nextSiteSelector) != null)
                    {
                        nextPageUrl = "https://www.morele.net" + newDocument.QuerySelector(_nextSiteSelector).Attributes["href"].Value.ToString();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return urls;
        }

        private async Task<IEnumerable<GPU>> GetGPUs(IEnumerable<string> sites)
        {
            var gpusList = new List<GPU>();

            foreach (var site in sites)
            {
                var page = Web.Load(site);
                var gpus = page.QuerySelectorAll(_gpuCardSelector);

                foreach (var gpu in gpus)
                {
                    var entity = await GetGPUDetails(gpu);
                    if (entity != null)
                    {
                        gpusList.Add(entity);
                    }
                }
            }
            return gpusList;
        }

        private async Task<GPU> GetGPUDetails(HtmlNode gpu)
        {
            StringBuilder sb = new();
            var gpuName = string.Join(' ', gpu.QuerySelector(_gpuNameSelector)
                                    .Attributes["title"]
                                    .Value);
            var gpuPrice = decimal.Parse(gpu.QuerySelector(_gpuPriceSelector)
                                    .InnerText
                                    .ToString(Nfi)
                                    .Replace("zł", "")
                                    .Replace("od", ""));


            var start = gpuName.IndexOf("Karta");
            var end = gpuName.LastIndexOf("graficzna");

            if (gpuName.Contains(_characterToAvoid))
            {
                gpuName = gpuName.Substring(0, gpuName.IndexOf(_characterToAvoid)).Trim();
            }

            string gpuVendor = await SetVendor(gpuName);

            var gpuModel = sb.Append(gpuName).Remove(start, end + 9).ToString().Trim();

            var result = new GPU
            {
                FullName = gpuName,
                Vendor = gpuVendor,
                Model = gpuModel,
                Price = gpuPrice,
                Shop = "Morele"
            };

            return result;
        }

        private static async Task<string> SetVendor(string gpuName)
        {
            string vendor;

            if (gpuName.ToLower().Contains("geforce") || gpuName.ToLower().Contains("quadro") || gpuName.ToLower().Contains("gtx"))
            {
                vendor = "NVIDIA";
            }
            else if (gpuName.ToLower().Contains("radeon") || gpuName.ToLower().Contains("rx"))
            {
                vendor = "AMD";
            }
            else if (gpuName.ToLower().Contains("intel") || gpuName.ToLower().Contains("arc"))
            {
                vendor = "Intel";
            }
            else
            {
                vendor = "Undefinied";
            }

            return vendor;
        }
    }
}
