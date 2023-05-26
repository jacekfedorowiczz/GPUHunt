using GPUHunt.Application.Models;
using System.Globalization;

namespace GPUHunt.Application.Interfaces
{
    public interface IShopCrawler
    {
        Task<IEnumerable<GPU>> CrawlShop();
        
        static string SetVendor(string gpuName)
        {
            TextInfo textInfo = new CultureInfo("pl-PL", false).TextInfo;
            string vendor;

            if (textInfo.ToLower(gpuName).Contains("geforce") || textInfo.ToLower(gpuName).Contains("quadro") || textInfo.ToLower(gpuName).Contains("gtx"))
            {
                vendor = "NVIDIA";
            }
            else if (textInfo.ToLower(gpuName).Contains("radeon") || textInfo.ToLower(gpuName).Contains("rx"))
            {
                vendor = "AMD";
            }
            else if (textInfo.ToLower(gpuName).Contains("intel") || textInfo.ToLower(gpuName).Contains("arc"))
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
