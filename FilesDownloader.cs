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
            const string dirAssetsDownloadedPath = @".\AssetsDownloaded\";
            var differencesFileReader = await File.ReadAllLinesAsync(differencesFilePath);
            var filesComparator = new FilesComparator();
            var urlList = new List<string>();
            var counter = 0;

            try
            {
                Log.Information("Ready to download {0} assets", filesComparator.DifferenceList());
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
                        while (File.Exists(Path.Combine(dirAssetsDownloadedPath, assetExtractedName)))
                        {
                            counter++;
                            assetExtractedName = $"{url.Split('/').Last().Split('.').First()}({counter}).{url.Split('/').Last().Split('.').Last()}";
                        }
                        await File.WriteAllBytesAsync(Path.Combine(dirAssetsDownloadedPath, assetExtractedName), response);
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