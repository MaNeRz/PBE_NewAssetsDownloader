using System;

namespace PBE_NewFileExtractor
{
    public class ApplicationInfos
    {
        public static void Infos()
        {
            const string version = "1.0.0";
            Console.Title = $"League Of Legends - PBE New Assets Extractor | v.{version}";
        }
    }
}