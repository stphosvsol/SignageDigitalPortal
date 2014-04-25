using System;
using System.IO;

namespace Infrastructure.Extensions
{
    public static class FileExtension
    {
        public static string FileExtensionWoDot(this string sFileName)
        {
            var extension = Path.GetExtension(sFileName);
            return extension != null ? extension.ToLower().Replace(".", String.Empty) : String.Empty;
        }
    }
}
