using System;

namespace PBE_NewFileExtractor
{
    public class ApplicationInfos
    {
        public static void Infos()
        {
            const string version = "Beta 1.1";
            Console.Title = $"League Of Legends - PBE New Assets Extractor | v.{version}";
        }
    }
}