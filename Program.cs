using System.Drawing;
using System.Threading.Tasks;
using Colorful;

namespace PBE_NewFileExtractor
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var directoriesCreator = new DirectoriesCreator();
            var filesCreator = new FilesCreator();
            var filesComparator = new FilesComparator();
            var filesDownloader = new FilesDownloader();

            ApplicationInfos.Infos();
            await directoriesCreator.CreateDirResourcesAsync();
            await directoriesCreator.CreateDirAssetsDownloadedAsync();
            await filesCreator.CreateFilesExtractedAsync();
            await filesCreator.CreateNewFilesExtractedAsync();
            await filesComparator.CheckFilesDiffAsync();
            await filesDownloader.DownloadAssetsAsync();
            filesComparator.ReplaceAssetsFile();
            Console.WriteLine("\n\nPress any key to close the program...", Color.Aqua);

            Console.ReadKey();
        }
    }
}