using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BLL.Helpers
{
    static public class ImageConvertor
    {
        public static string GetImageFromPath(string fullPath)
        {
            byte[] imageArray = File.ReadAllBytes(fullPath);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            return $"data:image/png;base64,{base64ImageRepresentation}";
        }
    }
}
