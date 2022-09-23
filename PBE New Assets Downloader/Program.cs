using System.Threading.Tasks;

namespace PBE_NewFileExtractor
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            await new Process().MainProcess();
        }
    }
}