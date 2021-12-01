using System;
using System.IO;
using System.Threading.Tasks;
using Serilog;
using Console = Colorful.Console;

namespace PBE_NewFileExtractor
{
    public class DirectoriesCreator
    {
        public Task CreateDirResourcesAsync()
        {
            return CreateFoldersAsync(@".\Resources\");
        }

        public Task CreateDirAssetsDownloadedAsync()
        {
            return CreateFoldersAsync(@".\AssetsDownloaded\");
        }

        private async Task CreateFoldersAsync(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Log.Warning("Directory not found: {0}", Path.GetFullPath(path));
                    await Task.Delay(1000);
                    Directory.CreateDirectory(path);
                    Log.Information("Directory created: {0}", Path.GetFullPath(path));
                    await Task.Delay(1000);
                }
            }
            catch (Exception e)
            {
                Log.Error("Error during directory {1} creation!\nError:\n{0}", e, path.TrimStart('\\').TrimEnd('\\'));
                Console.ReadKey();
            }
        }
    }
}