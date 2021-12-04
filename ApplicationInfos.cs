using System;

namespace PBE_NewFileExtractor
{
    public static class ApplicationInfos
    {
        public static void Infos()
        {
            const string version = "1.0";
            Console.Title = $"League Of Legends - PBE New Assets Extractor | v.{version}";
        }
    }
}