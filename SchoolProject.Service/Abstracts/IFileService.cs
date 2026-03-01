using Microsoft.AspNetCore.Http;

namespace SchoolProject.Service.Abstracts
{
    public interface IFileService
    {
        public Task<string> UploadFileAsync(string Location, IFormFile file);
    }
}
