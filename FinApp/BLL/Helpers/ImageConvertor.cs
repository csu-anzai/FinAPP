using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace BLL.Helpers
{
    static public class ImageConvertor
    {
        public static string GetImageFromPath(string fullPath)
        {
            var imageArray = File.ReadAllBytes(fullPath);
            var base64ImageRepresentation = Convert.ToBase64String(imageArray);
            return base64ImageRepresentation;
        }

        public static string GetByte64FromImage(IFormFile file)
        {
            if (file.Length <= 0)
                return null;

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                return Convert.ToBase64String(fileBytes);
            }
        }
    }
}
