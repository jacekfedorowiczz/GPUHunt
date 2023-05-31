using GPUHunt.Application.Models;
using GPUHunt.Application.Models.Enums;
using System.Globalization;

namespace GPUHunt.Application.Interfaces
{
    public interface IShopCrawler
    {
        Task<IEnumerable<GPU>> CrawlShop();
        
        static Vendors SetVendor(string gpuName)
        {
            TextInfo textInfo = new CultureInfo("pl-PL", false).TextInfo;
            Vendors vendor;

            if (textInfo.ToLower(gpuName).Contains("geforce") || textInfo.ToLower(gpuName).Contains("quadro") || textInfo.ToLower(gpuName).Contains("gtx"))
            {
                vendor = Vendors.NVIDIA;
            }
            else if (textInfo.ToLower(gpuName).Contains("radeon") || textInfo.ToLower(gpuName).Contains("rx"))
            {
                vendor = Vendors.AMD;
            }
            else if (textInfo.ToLower(gpuName).Contains("intel") || textInfo.ToLower(gpuName).Contains("arc"))
            {
                vendor = Vendors.Intel;
            }
            else
            {
                vendor = Vendors.Undefinied;
            }

            return vendor;
        }
    }
}
