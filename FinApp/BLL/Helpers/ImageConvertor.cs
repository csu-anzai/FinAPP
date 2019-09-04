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
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);

                var fileBytes = memoryStream.ToArray();

                return Convert.ToBase64String(fileBytes);
            }
        }
    }
}
