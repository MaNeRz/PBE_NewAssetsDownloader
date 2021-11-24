using System.Net.Http;
using System.Threading.Tasks;

namespace PBE_NewFileExtractor
{
    public class RequestsMaker
    {
        private const string CDragonExportedUrl = "https://raw.communitydragon.org/pbe/cdragon/files.exported.txt";
        private readonly HttpClient _httpClient;

        public RequestsMaker()
        {
            _httpClient = new HttpClient();
        }

        public Task<string> RequestCDragonFilesExportedAsync()
        {
            return _httpClient.GetStringAsync(CDragonExportedUrl);
        }
    }
}