using System;
using System.Reflection;

namespace PBE_NewFileExtractor
{
    public static class ApplicationInfos
    {
        public static void Infos()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
            Console.Title = $"League Of Legends - PBE New Assets Extractor | v.{version}";
        }
    }
}