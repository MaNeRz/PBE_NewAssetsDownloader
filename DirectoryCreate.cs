using System;
using System.IO;
using System.Threading.Tasks;
using Serilog;
using Console = Colorful.Console;

namespace PBE_NewFileExtractor
{
    public class DirectoriesCreator
    {
        private const string DirAssetsDownloadedPath = @".\AssetsDownloaded\";
        public readonly string DirSubAssetsDownloadedPath = Path.Combine(DirAssetsDownloadedPath, DateTime.Now.ToString("dd-M-yyyy--HH-mm"));
        public static Task CreateDirResourcesAsync()
        {
            return CreateFoldersAsync(@".\Resources\");
        }

        public static Task CreateDirAssetsDownloadedAsync()
        {
            return CreateFoldersAsync(@".\AssetsDownloaded\");
        }

        private static async Task CreateFoldersAsync(string path)
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

        public async Task CreateSubAssetsDownloadedFolderAsync()
        {
            try
            {
                Directory.CreateDirectory(Path.Combine(DirAssetsDownloadedPath, DateTime.Now.ToString("dd-M-yyyy--HH-mm")));
                Log.Information("Directory created: {0}", Path.GetFullPath(DirSubAssetsDownloadedPath));
                await Task.Delay(1000);
            }
            catch (Exception e)
            {
                Log.Error("Error during directory {1} creation!\nError:\n{0}", e, DirSubAssetsDownloadedPath.TrimStart('\\').TrimEnd('\\'));
                Console.ReadKey();
            }
        }
        
    }
}