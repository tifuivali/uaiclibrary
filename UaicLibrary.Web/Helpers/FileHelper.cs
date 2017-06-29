using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace UaicLibrary.Web.Helpers
{
    public static class FileHelper
    {
        public static async Task<byte[]> GetBookBytes(string bookUrl)
        {
            var httpClient = new HttpClient();
            var bytes = await httpClient.GetByteArrayAsync(new Uri(bookUrl));
            return bytes;
        }

        public static async Task<Stream> GetBooktream(string bookUrl)
        {
            var httpClient = new HttpClient();
            var stream = await httpClient.GetStreamAsync(new Uri(bookUrl));
            return stream;
        }

    }
}
