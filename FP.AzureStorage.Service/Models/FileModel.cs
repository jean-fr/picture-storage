using System.IO;

namespace FP.AzureStorage.Service.Models
{
   public class FileModel
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Base64 { get; set; }
        public Stream Stream { get; set; }
    }
}
