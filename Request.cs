using System.Net.Http;
using System.Threading.Tasks;

namespace PBE_NewFileExtractor
{
    public class RequestsMaker
    {
        private const string CDragonExportedUrlPbe = "https://raw.communitydragon.org/pbe/cdragon/files.exported.txt";
        private const string CDragonExportedUrlLatest = "https://raw.communitydragon.org/latest/cdragon/files.exported.txt";
        private const string CDragonRaw = "https://raw.communitydragon.org";
        private readonly HttpClient _httpClient;

        public RequestsMaker()
        {
            _httpClient = new HttpClient();
        }

        public Task<string> RequestCDragonFilesExportedPbeAsync()
        {
            return _httpClient.GetStringAsync(CDragonExportedUrlPbe);
        }

        public Task<string> RequestCDragonFilesExportedLatestAsync()
        {
            return _httpClient.GetStringAsync(CDragonExportedUrlLatest);
        }

        public Task<HttpResponseMessage> RequestCDragonRaw()
        {
            return _httpClient.GetAsync(CDragonRaw);
        }
    }
}