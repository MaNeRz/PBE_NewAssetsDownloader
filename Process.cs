using System.Drawing;
using System.Threading.Tasks;
using Colorful;
using Serilog;

namespace PBE_NewFileExtractor
{
    public class Process
    {
        public async Task MainProcess()
        {
            Log.Logger = LogSettings.CreateLogger();
            var requestsMaker = new RequestsMaker();
            var response = await requestsMaker.RequestCDragonRaw();
            var directoriesCreator = new DirectoriesCreator();
            var filesCreator = new FilesCreator();
            var filesComparator = new FilesComparator();
            var filesDownloader = new FilesDownloader();

            ApplicationInfos.Infos();
            if (response.IsSuccessStatusCode == false)
            {
                Log.Warning("Raw service is actually: {0}", "DOWN");
            }
            else
            {
                Log.Information("Raw service is actually: {0}", "UP");
                await directoriesCreator.CreateDirResourcesAsync();
                await directoriesCreator.CreateDirAssetsDownloadedAsync();
                await filesCreator.CreateFilesExtractedAsync();
                await filesCreator.CreateNewFilesExtractedAsync();
                await filesComparator.CheckFilesDiffAsync();
                await filesDownloader.DownloadAssetsAsync();
                filesComparator.ReplaceAssetsFile();
            }
            
            Console.WriteLine("\n\nPress any key to close the program...", Color.Aqua);
            Console.ReadKey();
        }
    }
}