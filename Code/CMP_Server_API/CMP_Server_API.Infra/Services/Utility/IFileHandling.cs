using CMP_Server_API.DTO;
using Microsoft.AspNetCore.Http;

namespace CMP_Server_API.CMP_Server_API.Infra.Services.Utility
{
    public interface IFileHandling
    {
        public string storeFile(IFormFile file);
        public string storeFile(string file, string fileName);

        public bool deleteFile(string uniqueName);
    }
}
