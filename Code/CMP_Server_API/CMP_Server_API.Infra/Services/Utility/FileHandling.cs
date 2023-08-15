using CMP_Server_API.DTO;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using Microsoft.AspNetCore.Hosting;

namespace CMP_Server_API.CMP_Server_API.Infra.Services.Utility
{
    public class FileHandling : IFileHandling
    {
        private IHostingEnvironment _host;
        public FileHandling(IHostingEnvironment host)
        {
            _host= host;
        }

        public FileHandling() { }

        public string storeFile(IFormFile file)
        {
            string path = Path.Combine(_host.WebRootPath, "Images");
            string uniqueName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(path, uniqueName);
            file.CopyTo(new FileStream(filePath, FileMode.Create));
            return uniqueName;
        }

        public string storeFile(string file, string fileName)
        {
            string path = Path.Combine(_host.WebRootPath, "Images");
            string uniqueName = Guid.NewGuid().ToString() + "_" + fileName;
            string filePath = Path.Combine(path, uniqueName);
            File.WriteAllBytes(filePath, Convert.FromBase64String(file));
            return uniqueName;
        }

        public bool deleteFile(string uniqueName)
        {
            string path = Path.Combine(_host.WebRootPath, "Images");
            string filePath = Path.Combine(path, uniqueName);
            FileInfo file = new FileInfo(filePath);
            if (file.Exists)//check file exsit or not  
            {
                file.Delete();
                return true;
            }
            return false;
        }
    }
}
