using GPUHunt.Application.Interfaces;
using GPUHunt.Application.Models;
using GPUHunt.Application.Services.ShopCrawlers.Info;
using GPUHunt.Domain.Enums;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Application.Services.ShopCrawlers
{
    public class XKomCrawler : XKomInfo, IShopCrawler
    {
        readonly HtmlWeb Web = new();
        readonly NumberFormatInfo nfi = new CultureInfo("pl-PL", false).NumberFormat;
        private readonly String _gpuCardSelector = ".gyHdpL";
        private readonly String _gpuNameSelector = "a > h3";
        private readonly String _gpuPriceSelector = ".gAlJbD > span.guFePW";
        private readonly String _nextSiteSelector = "a.kGuktN";
        private readonly String _optionalGpuNameSelector = ".emYvVh > a > span > img";

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
                throw new Exception("Something went wrong.");
            }
        }

        private async Task<IEnumerable<string>> GetSites()
        {
            var document = Web.Load(XKomBaseURL);
            var urls = new List<string>() { XKomBaseURL };

            if (document.QuerySelector(_nextSiteSelector).Attributes["href"].Value != null)
            {
                var nextPageUrl = "https://x-kom.pl/" + document.QuerySelector(_nextSiteSelector).Attributes["href"].Value.ToString();
                while (nextPageUrl != null)
                {
                    urls.Add(nextPageUrl);
                    var newDocument = Web.Load(nextPageUrl);
                    if (newDocument.QuerySelector(_nextSiteSelector) != null)
                    {
                        nextPageUrl = "https://x-kom.pl/" + newDocument.QuerySelector(_nextSiteSelector).Attributes["href"].Value.ToString();
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
            var gpuModel = gpu.QuerySelector(_gpuNameSelector).LastChild.InnerText.ToString();
            if (String.IsNullOrEmpty(gpuModel))
            {
                gpuModel = gpu.QuerySelector(_optionalGpuNameSelector).Attributes["alt"].Value.ToString();
            }

            var gpuPrice = decimal.Parse(gpu.QuerySelector(_gpuPriceSelector).InnerText.ToString(nfi).Replace("zł", "").Replace("od", ""));

            var gpuName = sb.Append(gpuModel).Insert(0, "Karta graficzna ").ToString();

            Vendors gpuVendor = IShopCrawler.SetVendor(gpuName);

            var result = new GPU
            {
                FullName = gpuName,
                Vendor = gpuVendor,
                Model = gpuModel,
                Price = gpuPrice,
                Store = "X-Kom"
            };

            return result;
        }
    }
}
