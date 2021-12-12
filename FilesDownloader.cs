using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Serilog;

namespace PBE_NewFileExtractor
{
    public class FilesDownloader
    {
        public async Task DownloadAssetsAsync()
        {
            using var client = new HttpClient {BaseAddress = new Uri("https://raw.communitydragon.org/pbe/")};
            const string differencesFilePath = @".\Resources\Differences.txt";
            var differencesFileReader = await File.ReadAllLinesAsync(differencesFilePath);
            var filesComparator = new FilesComparator();
            var directoryCreator = new DirectoriesCreator();
            var urlList = new List<string>();
            var differencesListCounter = filesComparator.DifferenceList();
            var counter = 0;

            try
            {
                switch (differencesListCounter)
                {
                    case 0:
                        Log.Information("No assets to download");
                        break;
                    case >0:
                        await directoryCreator.CreateSubAssetsDownloadedFolderAsync();
                        Log.Information("Ready to download {0} assets", differencesListCounter);
                        break;
                }
                await Task.Delay(1000);
                foreach (var line in differencesFileReader) urlList.Add(line);

                foreach (var url in urlList)
                {
                    var clientResponse = await client.GetAsync(url);
                    var assetExtractedName = url.Split('/').Last();
                    if ((Path.GetExtension(url).Contains(".pie_c_") || 
                         Path.GetExtension(url).Equals("") || 
                         !Path.GetExtension(url).Contains(".png")) &&
                        !Path.GetExtension(url).Contains(".jpg") &&
                        !Path.GetExtension(url).Contains(".ogg") &&
                        !Path.GetExtension(url).Contains(".webm")) continue;
                    if (clientResponse.IsSuccessStatusCode == false)
                    {
                        Log.Error("Asset not downloaded: {0}", assetExtractedName);
                    }
                    else
                    {
                        var response = await client.GetByteArrayAsync(url);
                        while (File.Exists(Path.Combine(directoryCreator.DirSubAssetsDownloadedPath, assetExtractedName)))
                        {
                            counter++;
                            assetExtractedName = $"{url.Split('/').Last().Split('.').First()}({counter}).{url.Split('/').Last().Split('.').Last()}";
                        }
                        await File.WriteAllBytesAsync(Path.Combine(directoryCreator.DirSubAssetsDownloadedPath, assetExtractedName), response);
                        counter = 0;
                        Log.Information("Asset downloaded: {0}", assetExtractedName);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Error during assets download!\nError:\n{0}", e);
                Console.ReadKey();
            }
        }
    }
}