using BLL.Models.ViewModels;
using System.IO;
using System.Threading.Tasks;

namespace BLL.Helpers
{
    public static class DirectoryManager
    {
        public static async Task<string> SaveFileInFolder(string rootPath, ImageViewModel imageVm)
        {
            var file = imageVm.Image;
            var newPath = Path.Combine(rootPath, imageVm.Path);

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

        public static void RemoveFileFromFolder(string path)
        {
            var file = new FileInfo(path);

            if (file.Exists)
                file.Delete();
        }

        public static void RemoveFolder(string path)
        {
            var directory = new DirectoryInfo(path);

            if (directory.Exists)
                directory.Delete(true);
        }
    }
}
