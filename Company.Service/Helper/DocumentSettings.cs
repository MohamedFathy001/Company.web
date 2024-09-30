using Microsoft.AspNetCore.Http;

namespace Company.Service.Helper
{
    public class DocumentSettings
    {
        public static string Uploadfile(IFormFile file , string folerName)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folerName);
            var fileName = $"{Guid.NewGuid()}-{file.FileName}";
            var filePath = Path.Combine(folderPath,fileName);
            using var filestream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(filestream);
            return fileName;
        }
    }
}
