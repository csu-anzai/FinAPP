using BLL.Models.ViewModels;
using System.IO;
using System.Threading.Tasks;

namespace BLL.Helpers
{
    public static class DirectoryManager
    {
        public static async Task<string> SaveFileInFolder(string rootPath, string folderName, ImageViewModel imageVm)
        {
            // in helper create class for file saving
            var file = imageVm.Image;
            var newPath = Path.Combine(rootPath, $@"{folderName}\{imageVm.Path}");

            if (!Directory.Exists(newPath))
                Directory.CreateDirectory(newPath);

            if (file.Length <= 0)
                return null;

            // var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fullPath = Path.Combine(newPath, imageVm.Name);

            using (var stream = new FileStream(fullPath, FileMode.Create))
                await file.CopyToAsync(stream);

            return fullPath;
        }
    }
}
