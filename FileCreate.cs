using System;
using System.IO;
using System.Threading.Tasks;
using Serilog;

namespace PBE_NewFileExtractor
{
    public class FilesCreator
    {
        private readonly RequestsMaker _request;

        public FilesCreator()
        {
            _request = new RequestsMaker();
        }

        public Task CreateFilesExtractedAsync()
        {
            return DownloadAssetsAsync("Assets.txt");
        }

        public Task CreateNewFilesExtractedAsync()
        {
            return DownloadAssetsAsync("NewAssets.txt");
        }

        private async Task DownloadAssetsAsync(string file)
        {
            const string path = @".\Resources\";
            try
            {
                if (Path.Combine(path, file) == @".\Resources\Assets.txt" &&
                    !File.Exists(Path.Combine(path, file)))
                {
                    var response = await _request.RequestCDragonFilesExportedLatestAsync();
                    Log.Warning("File not found: {0}", Path.GetFullPath(Path.Combine(path, file)));
                    await Task.Delay(1000);
                    await File.WriteAllTextAsync(Path.Combine(path, file), response);
                    Log.Information("File created: {0}", Path.GetFullPath(Path.Combine(path, file)));
                    await Task.Delay(1000);
                }
                if (Path.Combine(path, file) == @".\Resources\NewAssets.txt")
                {
                    var response = await _request.RequestCDragonFilesExportedPbeAsync();
                    switch (File.Exists(Path.Combine(path, file)))
                    {
                        case false:
                            Log.Warning("File not found: {0}", Path.GetFullPath(Path.Combine(path, file)));
                            await Task.Delay(1000);
                            Log.Information("File created: {0}", Path.GetFullPath(Path.Combine(path, file)));
                            break;
                        case true:
                            Log.Information("File updated: {0}", Path.GetFullPath(Path.Combine(path, file)));
                            break;
                    }
                    await File.WriteAllTextAsync(Path.Combine(path, file), response);
                    await Task.Delay(1000);
                }
            }
            catch (Exception e)
            {
                Log.Error("Error during file {1} creation!\nError:\n{0}", e, file);
                Console.ReadKey();
            }
        }
    }
}