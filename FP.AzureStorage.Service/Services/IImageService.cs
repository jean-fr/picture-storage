using FP.AzureStorage.Service.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FP.AzureStorage.Service
{
    public interface IImageService
    {
        Task<string> Upload(FileModel file);
    }
}
