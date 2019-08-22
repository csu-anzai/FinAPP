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
    }
}
